Public Class frmSeqSetSequenceDetail

#Region "変数定義"

    Dim mintRtn As Integer
    Dim mblnFlg As Boolean
    Dim mudtSequenceSetDetail As gTypSetSeqSetRec

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSequenceSetDetail As gTypSetSeqSetRec, ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mudtSequenceSetDetail = udtSequenceSetDetail

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                udtSequenceSetDetail = mudtSequenceSetDetail
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
    Private Sub frmSeqSetSequenceDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''コンボボックス初期設定
            Call gSetComboBox(cmbChStatus, gEnmComboType.ctSeqSetDetailStatus)
            Call gSetComboBox(cmbChOutputType, gEnmComboType.ctSeqSetDetailDataType)
            Call gSetComboBox(cmbFcuFuOutputType, gEnmComboType.ctSeqSetDetailOutputType)

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''ロジックテキスト背景色
            txtLogic.BackColor = gColorGridRowBackReadOnly

            ''画面表示
            Call mSetDisplay(mudtSequenceSetDetail)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellDoubleClick

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            If frmSeqSetInputData.gShow(e.RowIndex + 1, mudtSequenceSetDetail.udtInput(e.RowIndex), Me) = 0 Then

                ''InputCH情報表示
                Call mDispInputInfo(e.RowIndex, mudtSequenceSetDetail.udtInput(e.RowIndex))

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Select（ロジック）ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        Try

            Dim strSelectLogicCode As String = ""
            Dim strSelectLogicName As String = ""
            Dim udtSequenceLogic() As gTypCodeName = Nothing
            Dim udtSequenceLogicSub() As gTypCodeName = Nothing

            ''シーケンスロジック設定取得
            Call gGetComboCodeName(udtSequenceLogic, gEnmComboType.ctSeqSetDetailLogic)

            If frmSeqSetSequenceLogic.gShow(udtSequenceLogic, strSelectLogicCode, strSelectLogicName, Me) = 0 Then

                ''テキストに表示
                txtLogic.Text = strSelectLogicName
                mudtSequenceSetDetail.shtLogicType = CCShort(strSelectLogicCode)

                ''シーケンスロジックサブ設定取得
                Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(CInt(strSelectLogicCode), "00"))

                ''サブ情報表示
                For i As Integer = 0 To UBound(udtSequenceLogicSub)
                    grdLogic(0, i).Value = udtSequenceLogicSub(i).strName
                    grdLogic(1, i).Value = ""

                    grdLogic(3, i).Value = udtSequenceLogicSub(i).strOption1
                    grdLogic(4, i).Value = udtSequenceLogicSub(i).strOption2
                    grdLogic(5, i).Value = udtSequenceLogicSub(i).strOption3
                    grdLogic(6, i).Value = udtSequenceLogicSub(i).strOption4
                    grdLogic(7, i).Value = udtSequenceLogicSub(i).strOption5
                    grdLogic(8, i).Value = udtSequenceLogicSub(i).shtCode

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Deleteボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択行の設定を削除する
    '--------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            ''選択セルの行位置が0より小さい、もしくは最大行数より大きい場合は処理を抜ける
            If grdCH.CurrentCell.RowIndex < 0 Or _
               grdCH.CurrentCell.RowIndex > grdCH.RowCount - 1 Then Return

            If MessageBox.Show("Do you delete the selected input set?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                ''シーケンスIDリセット
                Call gInitSetSeqSequenceInputOne(mudtSequenceSetDetail.udtInput(grdCH.CurrentCell.RowIndex))

                ''画面更新
                Call mDispInputInfo(grdCH.CurrentCell.RowIndex, mudtSequenceSetDetail.udtInput(grdCH.CurrentCell.RowIndex))

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Linearボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdLinear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLinear.Click

        Try

            Call frmSeqLinearTable.gShow()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Operationボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOperation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOperation.Click

        Try

            Call frmSeqOperationFixed.gShow()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Remarksテキストキープレス
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値格納
            Call mSetStructure(mudtSequenceSetDetail)

            mintRtn = 1
            Call Me.Close()

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
    Private Sub frmSeqSetSequenceDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Data KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChOutputData.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Offdelay KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputOffdelay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChOutputOffdelay.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Offdelay KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuOutputTime.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtFcuFuFuNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFcuFuFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtPortNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFcuFuPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuPortNo.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtTerminalNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtTerminalNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuTerminalNo.KeyPress

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
    Private Sub grdLogic_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdLogic.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdLogic.CurrentCell.OwningColumn.Name

            ''[TABLE_NO.]
            If strColumnName = "txtTableNo" Then
                ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                If mudtSequenceSetDetail.shtLogicType = 26 And grdLogic.CurrentCellAddress.Y = 0 And grdLogic(2, grdLogic.CurrentCellAddress.Y).Value <> True Then
                    e.Handled = gCheckTextInput(4, sender, e.KeyChar, True, False, True)
                Else
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)
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
    Private Sub grdLogic_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLogic.GotFocus

        Try

            Dim bytSet As Byte

            If Not mblnFlg Then Return

            ''選択セルの名称取得
            Dim strColumnName As String = grdLogic.CurrentCell.OwningColumn.Name

            ''[TABLE_NO.]
            If strColumnName = "txtTableNo" Then

                ''Code が 2（Bit指定）の場合
                If grdLogic(8, grdLogic.CurrentCell.RowIndex).Value = 2 Then

                    ''値取得
                    If gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value) = 0 Then
                        bytSet = 0
                    ElseIf gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value) > 255 Then
                        bytSet = 255
                    Else
                        bytSet = gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value)
                    End If

                    ''GotFocusイベント重複抑制
                    mblnFlg = False

                    ''Bit設定画面表示
                    If frmBitSetByte.gShow(bytSet, 0, Me) = 1 Then
                        grdLogic(1, grdLogic.CurrentCell.RowIndex).Value = bytSet
                        grdLogic.EndEdit()
                    End If


                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            If IsNumeric(txtChNo.Text) Then

                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： CH No. 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtChNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChNo.Validating

    '    e.Cancel = gChkTextNumSpan(0, 65535, txtChNo.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Output Data 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputData_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChOutputData.Validating

    '    e.Cancel = gChkTextNumSpan(0, 65535, txtChOutputData.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Output Offdelay 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputOffdelay_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChOutputOffdelay.Validating

    '    e.Cancel = gChkTextNumSpan(0, 36000, txtChOutputOffdelay.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： txtOutputTime 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputTime_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuOutputTime.Validating

    '    e.Cancel = gChkTextNumSpan(0, 250, txtFcuFuOutputTime.Text)

    'End Sub


    ''----------------------------------------------------------------------------
    '' 機能説明  ： txtTerminalNo 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtFcuFuFuNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuFuNo.Validating

    '    'e.Cancel = gChkTextNumSpan(0, 20, txtFcuFuFuNo.Text)

    'End Sub

    'Private Sub txtFcuFuPortNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuPortNo.Validating

    '    e.Cancel = gChkTextNumSpan(1, 8, txtFcuFuPortNo.Text)

    'End Sub

    'Private Sub txtFcuFuTerminalNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuTerminalNo.Validating

    '    e.Cancel = gChkTextNumSpan(1, 64, txtFcuFuTerminalNo.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub grdLogic_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdLogic.CellValidating

    '    Dim dgv As DataGridView = CType(sender, DataGridView)
    '    Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

    '    ''[TABLE_NO.]
    '    If strColumnName = "txtTableNo" Then
    '        e.Cancel = gChkTextNumSpan(0, 32767, e.FormattedValue)
    '    End If

    'End Sub

#End Region

#Region "イベントハンドラ操作"


    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLogic.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdLogic_KeyPress
                RemoveHandler tb.GotFocus, AddressOf grdLogic_GotFocus

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name = "txtTableNo" Then

                    AddHandler tb.KeyPress, AddressOf grdLogic_KeyPress
                    AddHandler tb.GotFocus, AddressOf grdLogic_GotFocus

                    ''GotFocusイベント重複抑制
                    mblnFlg = True

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

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

            Dim intMin As Integer = 0
            Dim intMax As Integer = 0
            Dim intVal As Integer = 0
            Dim intChk As Integer = 0
            Dim blnFlg As Boolean = False
            Dim strChkNum As String = ""
            Dim strlen As Integer = 0
            Dim dp As Integer = 0

            ''共通FUアドレス入力チェック
            If Not gChkInputFuAddress(txtFcuFuFuNo, txtFcuFuPortNo, txtFcuFuTerminalNo, 64, True, True) Then Return False

            ''共通数値入力チェック
            If Not gChkInputNum(txtChNo, 0, 65535, "Output CH No.", True, True) Then Return False
            If Not gChkInputNum(txtChOutputData, 0, 65535, "Output Data", True, True) Then Return False
            If Not gChkInputNum(txtChOutputOffdelay, 0, 36000, "Output Offdelay", True, True) Then Return False
            If Not gChkInputNum(txtFcuFuOutputTime, 0, 200, "One shot output time", True, True) Then Return False

            ''ロジックテーブル入力チェック
            For i As Integer = 0 To grdLogic.RowCount - 1

                If Trim(grdLogic(0, i).Value) = "" Then

                    ''項目がない場合は強制的に空白をセット
                    grdLogic(1, i).Value = ""

                Else

                    ''数値以外
                    If Not IsNumeric(Trim(grdLogic(1, i).Value)) Then
                        Call MessageBox.Show("Please input the numerical value" & vbNewLine & vbNewLine & _
                                             "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & _
                                             "[ Row ] " & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    ''CH番号だったら範囲チェックしない。　Ver1.4.6 2012.07.31 K.Tanigawa
                    If (grdLogic(2, i).Value) = True Then

                        '' CHNo.は、チェックしない。
                        Return True
                    End If


                    ''オプション項目の３番目をチェック
                    Select Case Trim(grdLogic(3, i).Value)
                        Case 1

                            ''１の場合は入力範囲チェック
                            intMin = CInt(Trim(grdLogic(4, i).Value))
                            intMax = CInt(Trim(grdLogic(5, i).Value))
                            intVal = CInt(Trim(grdLogic(1, i).Value))

                            ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                            ' 小数点チェック時にIntで引き渡すと四捨五入される、関数内でdoubleに変換
                            'If gChkTextNumSpan(intMin, intMax, intVal, True, "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1) Then
                            If gChkTextNumSpan(intMin, intMax, Trim(grdLogic(1, i).Value), True, "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1) Then
                                Return False
                            End If

                            ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                            ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                            If mudtSequenceSetDetail.shtLogicType = 26 And i = 0 And grdLogic(2, i).Value <> True Then
                                strlen = Len(Trim(grdLogic(1, i).Value))
                                dp = InStr(Trim(grdLogic(1, i).Value), ".")
                                If dp > 0 Then
                                    If Len(Mid(grdLogic(1, i).Value, dp + 1, strlen - dp)) > 1 Then
                                        Call MessageBox.Show("Please set number among." & vbNewLine & vbNewLine & "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1, _
                                                "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Return False
                                    End If
                                End If

                            End If

                        Case 2

                            ''２の場合は入力固定値チェック
                            blnFlg = False
                            For j As Integer = 4 To (grdLogic.ColumnCount - 1) - 1 ''最後の -1 は一番後ろにあるCodeの分

                                ''指定文字が無くなったらチェック終了
                                If Trim(grdLogic(j, i).Value) = "" Then
                                    Exit For
                                Else

                                    ''指定文字かチェック
                                    intVal = CInt(Trim(grdLogic(1, i).Value))
                                    intChk = CInt(Trim(grdLogic(j, i).Value))

                                    If intVal = intChk Then
                                        blnFlg = True
                                        Exit For
                                    End If

                                    strChkNum &= intChk & " or "

                                End If

                            Next

                            ''全て一致しなかった場合
                            If Not blnFlg Then
                                Call MessageBox.Show("Please input the " & Mid(strChkNum, 1, strChkNum.Length - 4) & vbNewLine & vbNewLine & _
                                                     "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & _
                                                     "[ Row ] " & i + 1, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If

                    End Select

                End If

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) シーケンス設定詳細構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSeqSetRec)

        Try

            With udtSet

                ''シーケンスID
                .shtId = CCShort(txtSeqID.Text)

                '===================
                ''LogicSetフレーム
                '===================
                ''ロジックタイプ
                ''↑はSelectボタンクリックイベントで構造体に設定済み

                ''演算参照テーブル
                For i As Integer = 0 To grdLogic.Rows.Count - 1
                    ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                    ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                    If (mudtSequenceSetDetail.shtLogicType = 26 And i = 0 And grdLogic(2, i).Value <> True) Then
                        .shtLogicItem(i) = CCUInt16(Trim(grdLogic(1, i).Value) * 10)
                    Else
                        .shtLogicItem(i) = CCUInt16(Trim(grdLogic(1, i).Value))
                    End If

                    .shtUseCh(i) = IIf(grdLogic(2, i).Value, 1, 0)
                Next

                '===================
                ''CH Outputフレーム
                '===================
                .shtOutChid = CCUInt16(Trim(txtChNo.Text))

                If optIOSelInput.Checked Then
                    .bytOutIoSelect = 1
                ElseIf optIOSelOutput.Checked Then
                    .bytOutIoSelect = 2
                Else
                    .bytOutIoSelect = 0
                End If

                .bytOutStatus = cmbChStatus.SelectedValue
                '.shtOutData = CCShort(Trim(txtChOutputData.Text))
                .shtOutData = BitConverter.ToInt16(BitConverter.GetBytes(CCUInt32(txtChOutputData.Text)), 0)

                .bytOutDataType = cmbChOutputType.SelectedValue

                '.shtOutDelay = CCShort(Trim(txtChOutputOffdelay.Text))
                .shtOutDelay = BitConverter.ToInt16(BitConverter.GetBytes(CCUInt32(txtChOutputOffdelay.Text)), 0)

                .bytOutInv = IIf(optChNonInvert.Checked, 0, 1)

                '===================
                ''OutputFcuFuフレーム
                '===================
                .bytFuno = gGetFuNo(txtFcuFuFuNo.Text, True)
                .bytPort = IIf(CCbyte(Trim(txtFcuFuPortNo.Text)) = 0, gCstCodeChNotSetFuPortByte, CCbyte(Trim(txtFcuFuPortNo.Text)))
                .bytPin = IIf(CCbyte(Trim(txtFcuFuTerminalNo.Text)) = 0, gCstCodeChNotSetFuPinByte, CCbyte(Trim(txtFcuFuTerminalNo.Text)))
                .bytOutType = cmbFcuFuOutputType.SelectedValue
                .bytOneShot = CCbyte(Trim(txtFcuFuOutputTime.Text))

                '===================
                ''Contineフレーム
                '===================
                .bytContine = IIf(optContinuance.Checked, 1, 0)

                '===================
                ''InputSetフレーム
                '===================
                'For i As Integer = LBound(udtSet.udtInput) To UBound(udtSet.udtInput)

                '    With .udtInput(i)

                '        .shtChid = IIf(Trim(grdCH(1, i).Value) = "", 0, Trim(grdCH(1, i).Value))
                '        .bytStatus = IIf(Trim(grdCH(2, i).Value) = "", 0, Trim(grdCH(2, i).Value))
                '        .bytType = IIf(Trim(grdCH(3, i).Value) = "", 0, Trim(grdCH(3, i).Value))

                '    End With
                'Next

                '===================
                ''Remarksフレーム
                '===================
                .strRemarks = txtRemarks.Text

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) シーケンス設定詳細構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSeqSetRec)

        Try

            Dim udtSequenceLogicSub() As gTypCodeName = Nothing
            Dim intvar As UInt16 = 0

            With udtSet

                ''シーケンスID
                txtSeqID.Text = .shtId

                '===================
                ''LogicSetフレーム
                '===================
                ''ロジック名
                txtLogic.Text = gGetComboItemName(.shtLogicType, gEnmComboType.ctSeqSetDetailLogic)

                If .shtLogicType <> 0 Then

                    ''シーケンスロジックサブ設定取得
                    Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(.shtLogicType, "00"))

                    ''サブ情報表示
                    For i As Integer = 0 To UBound(udtSequenceLogicSub)
                        grdLogic(0, i).Value = udtSequenceLogicSub(i).strName
                        grdLogic(3, i).Value = udtSequenceLogicSub(i).strOption1
                        grdLogic(4, i).Value = udtSequenceLogicSub(i).strOption2
                        grdLogic(5, i).Value = udtSequenceLogicSub(i).strOption3
                        grdLogic(6, i).Value = udtSequenceLogicSub(i).strOption4
                        grdLogic(7, i).Value = udtSequenceLogicSub(i).strOption5
                        grdLogic(8, i).Value = udtSequenceLogicSub(i).shtCode
                    Next

                    ''演算参照テーブル
                    For i As Integer = 0 To grdLogic.Rows.Count - 1
                        ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                        ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可

                        If .shtLogicType = 26 And i = 0 And IIf(.shtUseCh(i) = 0, False, True) <> True Then
                            intvar = IIf(.shtLogicItem(i) = 0, IIf(Trim(grdLogic(0, i).Value) = "", "", .shtLogicItem(i)), .shtLogicItem(i))
                            grdLogic(1, i).Value = (intvar / 10).ToString("0.0")
                        Else
                            grdLogic(1, i).Value = IIf(.shtLogicItem(i) = 0, IIf(Trim(grdLogic(0, i).Value) = "", "", .shtLogicItem(i)), .shtLogicItem(i))
                        End If

                        grdLogic(2, i).Value = IIf(.shtUseCh(i) = 0, False, True)
                    Next


                End If

                '===================
                ''CH Outputフレーム
                '===================
                txtChNo.Text = gConvZeroToNull(.shtOutChid, "0000")

                Select Case .bytOutIoSelect
                    Case 0 ''設定なし
                        optIOSelInput.Checked = False
                        optIOSelOutput.Checked = False
                    Case 1 ''入力側
                        optIOSelInput.Checked = True
                        optIOSelOutput.Checked = False
                    Case 2 ''出力側
                        optIOSelInput.Checked = False
                        optIOSelOutput.Checked = True
                End Select

                cmbChStatus.SelectedValue = .bytOutStatus
                txtChOutputData.Text = gGet2Byte(.shtOutData)
                'txtChOutputData.Text = .shtOutData

                cmbChOutputType.SelectedValue = .bytOutDataType

                txtChOutputOffdelay.Text = gGet2Byte(.shtOutDelay)
                'txtChOutputOffdelay.Text = .shtOutDelay

                optChNonInvert.Checked = IIf(.bytOutInv = 0, True, False)
                optChInvert.Checked = IIf(.bytOutInv = 1, True, False)

                '===================
                ''OutputFcuFuフレーム
                '===================
                txtFcuFuFuNo.Text = gGetFuName2(.bytFuno)
                txtFcuFuPortNo.Text = IIf(.bytPort = gCstCodeChNotSetFuPortByte, "", .bytPort)
                txtFcuFuTerminalNo.Text = IIf(.bytPin = gCstCodeChNotSetFuPinByte, "", .bytPin)
                cmbFcuFuOutputType.SelectedValue = .bytOutType
                txtFcuFuOutputTime.Text = .bytOneShot

                '===================
                ''Contineフレーム
                '===================
                Select Case .bytContine
                    Case 0
                        optContinuance.Checked = False
                        optDiscontinuance.Checked = True
                    Case 1
                        optContinuance.Checked = True
                        optDiscontinuance.Checked = False
                End Select

                '===================
                ''InputSetフレーム
                '===================
                For i As Integer = LBound(udtSet.udtInput) To UBound(udtSet.udtInput)

                    ''InputCH情報表示
                    Call mDispInputInfo(i, udtSet.udtInput(i))

                Next

                '===================
                ''Remarksフレーム
                '===================
                txtRemarks.Text = .strRemarks

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mDispInputInfo(ByVal intRowIndex As Integer, ByVal udtSequenceSetInput As gTypSetSeqSetRecInput)

        Try

            With udtSequenceSetInput

                If .shtChid = 0 Then

                    ''未設定の場合は空白表示
                    grdCH(1, intRowIndex).Value = ""
                    grdCH(2, intRowIndex).Value = ""
                    grdCH(3, intRowIndex).Value = ""
                    grdCH(4, intRowIndex).Value = ""

                Else

                    grdCH(1, intRowIndex).Value = IIf(.shtChid = 0, "", Format(.shtChid, "0000"))
                    grdCH(2, intRowIndex).Value = Microsoft.VisualBasic.Right("0000" & Hex(.bytStatus), 2)
                    grdCH(3, intRowIndex).Value = mGetTypeName(.bytType)
                    grdCH(4, intRowIndex).Value = Microsoft.VisualBasic.Right("0000" & Hex(.shtMask), 4)

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Function mGetTypeName(ByVal intType As Integer) As String

        Select Case intType
            Case gCstCodeSeqInputTypeNonInvert
                Return "non invert"
            Case gCstCodeSeqInputTypeInvert
                Return "invert"
            Case gCstCodeSeqInputTypeOneShot
                Return "one shot"
        End Select

        Return "???"

    End Function

#End Region

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtItem"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtTableNo"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chkUse"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtOption1"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtOption2"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtOption3"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtOption4"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtOption5"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtCode"

            Column1.ReadOnly = True
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdLogic

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6) : .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).Width = 148
                .Columns(1).Width = 90
                .Columns(2).Width = 90

                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False

                .ColumnHeadersVisible = False

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 6
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

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
                    .Rows(i).Cells("txtItem").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption1").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption2").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption3").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption4").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption5").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtCode").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdLogic)

            End With

            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtNo"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtChNo"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtStatus"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtInputType"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtInputMask"

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column10.ReadOnly = True

                ''列ヘッダー
                .Columns(0).HeaderText = "No." : .Columns(0).Width = 40
                .Columns(1).HeaderText = "Data" : .Columns(1).Width = 80
                .Columns(2).HeaderText = "Status(Hex)" : .Columns(2).Width = 80
                .Columns(3).HeaderText = "Input Type" : .Columns(3).Width = 100
                .Columns(4).HeaderText = "Input Mask(Hex)" : .Columns(4).Width = 105
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                For i = 1 To .Rows.Count
                    .Rows(i - 1).Cells(0).Value = i.ToString("00")
                Next

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtStatus").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtInputType").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtInputMask").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdCH)

                .ReadOnly = True

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    
End Class
