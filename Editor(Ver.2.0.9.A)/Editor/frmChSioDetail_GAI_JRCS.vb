Public Class frmChSioDetail_GAI_JRCS

#Region "変数定義"

    Private mintRtn As Integer
    Private mintNowSelectIndex As Integer
    Private mblnInitFlg As Boolean
    Private mshtNum() As Short
    Private mudtVdr() As gTypSetChSioVdr
    Private mudtSioCh() As gTypSetChSioCh

    Private mudtSioExt() As gTypSetChSioExt

    Private mblnCopyPasteFlg As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (I ) 画面表示時のインデックス
    ' 　　　    : ARG2 - (IO) VDR情報構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intCurIndex As Integer, _
                          ByRef shtNum() As Short, _
                          ByRef udtVdr() As gTypSetChSioVdr, _
                          ByRef udtSioCh() As gTypSetChSioCh, _
                          ByRef udtSioExt() As gTypSetChSioExt, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mintNowSelectIndex = intCurIndex
            mshtNum = DeepCopyHelper.DeepCopy(shtNum)
            mudtVdr = DeepCopyHelper.DeepCopy(udtVdr)
            mudtSioCh = udtSioCh

            mudtSioExt = udtSioExt

            ''本画面表示
            Call gShowFormModelessForCloseWait22(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                shtNum = DeepCopyHelper.DeepCopy(mshtNum)
                udtVdr = DeepCopyHelper.DeepCopy(mudtVdr)
                udtSioCh = mudtSioCh
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
    Private Sub frmChSioDetail_GAI_JRCS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            '初期化開始
            mblnInitFlg = True

            'コンボボックス初期設定
            Call gSetComboBox(cmbPort, gEnmComboType.ctChSioDetailcmbPort)
            Call gSetComboBox(cmbCom, gEnmComboType.ctChSioDetailcmbCom)
            Call gSetComboBox(cmbParityBit, gEnmComboType.ctChSioDetailcmbParityBit)
            Call gSetComboBox(cmbDataBit, gEnmComboType.ctChSioDetailcmbDataBit)
            Call gSetComboBox(cmbStopBit, gEnmComboType.ctChSioDetailcmbStopBit)
            cmbPort.SelectedIndex = mintNowSelectIndex

            'initデータならばﾃﾞﾌｫﾙﾄ値を格納
            Dim i As Integer = 0
            Dim bInit As Boolean = False
            With mudtSioCh(cmbPort.SelectedIndex)
                For i = 0 To UBound(.udtSioChRec) Step 1
                    If .udtSioChRec(i).shtChNo <> 0 Or .udtSioChRec(i).shtChId <> 0 Then
                        bInit = True
                        Exit For
                    End If
                Next i
                If bInit = False Then
                    mudtVdr(cmbPort.SelectedIndex).udtCommInf.shtComBps = 2
                    mudtVdr(cmbPort.SelectedIndex).udtCommInf.shtStop = 1
                    mudtVdr(cmbPort.SelectedIndex).shtSendUseally = 10000
                End If
            End With


            'グリッド 初期設定
            Call mInitialDataGrid()

            '画面設定
            Call mSetDisplay(mudtVdr(cmbPort.SelectedIndex))
            Call mSetDisplaySioCh(mudtSioCh(cmbPort.SelectedIndex))

            '初期化終了
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub grdCH_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdCH.DataError

        Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    '----------------------------------------------------------------------------
    ' 機能説明  ： ポートコンボチェンジ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPort.SelectedIndexChanged

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            ''ここでの項目変更イベントは処理しない
            mblnInitFlg = True

            ''入力チェック
            If Not mChkInput() Then

                ''入力NGの場合はTableNoを元に戻す
                cmbPort.SelectedIndex = mintNowSelectIndex

            Else

                ''現在のPortNoに設定されている値を保存
                Call mSetStructure(mshtNum(mintNowSelectIndex), mudtVdr(mintNowSelectIndex), mudtSioCh(mintNowSelectIndex))

                ''選択されたPortNoの情報を表示
                Call mSetDisplay(mudtVdr(cmbPort.SelectedIndex))
                Call mSetDisplaySioCh(mudtSioCh(cmbPort.SelectedIndex))

                ''現在のTableNoを更新
                mintNowSelectIndex = cmbPort.SelectedIndex

            End If

            ''元に戻す
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

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
            Call mSetStructure(mshtNum(mintNowSelectIndex), mudtVdr(cmbPort.SelectedIndex), mudtSioCh(cmbPort.SelectedIndex))

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
    ' 機能説明  ： Key操作イベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCH.KeyDown

        If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.C Then

            mblnCopyPasteFlg = True

        ElseIf (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then

            mblnCopyPasteFlg = True

            For i As Integer = 0 To grdCH.RowCount - 1

                If Trim(grdCH(1, i).Value) <> "" And _
                   Trim(grdCH(2, i).Value) = "" Then

                    Call mDispTransmisionChName(i)

                End If

            Next

        Else
            mblnCopyPasteFlg = False
        End If

    End Sub

    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress

        Try

            If mblnCopyPasteFlg Then Return

            If grdCH.SelectedCells.Count = 1 Then

                If grdCH.CurrentCell.OwningColumn.Name = "txtChNo" Then

                    e.Handled = gCheckTextInput(5, sender, e.KeyChar, False)    '' 数値以外も入力可

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub grdCH_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCH.KeyUp
        mblnCopyPasteFlg = False
    End Sub

    Private Sub txtUseallyTransmit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUseallyTransmit.KeyPress
        Try
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbCom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCom.KeyPress
        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub cmbParityBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbParityBit.KeyPress
        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub cmbDataBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbDataBit.KeyPress
        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub cmbStopBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbStopBit.KeyPress
        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCH.CellValidating

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            Call grdCH.EndEdit()

            If strColumnName = "cmbType" Or strColumnName = "txtChNo" Then
                Call mDispTransmisionChName(e.RowIndex)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'コピペ対応：複数件コピペで名称が変わるように処理
    Private Sub grdCH_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellValueChanged
        Try
            If e.RowIndex < 0 Then
                Return
            End If
            If e.ColumnIndex < 0 Then
                Return
            End If

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            Call grdCH.EndEdit()

            If strColumnName = "cmbType" Or strColumnName = "txtChNo" Then
                Call mDispTransmisionChName(e.RowIndex)
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#Region "イベントハンドラ操作"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コンボ"

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボボックスを1回のクリックでドロップダウンさせるようにする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellEnter

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            'Dim dgv As DataGridView = CType(sender, DataGridView)

            'If dgv.Columns(e.ColumnIndex).Name.Substring(0, 3) = "cmb" Then

            '    SendKeys.Send("{F4}")

            'End If

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
            '共通数値入力チェック
            '送信間隔を0xFFFFまで設定可能とする
            If Not gChkInputNum(txtUseallyTransmit, 0, 65535, "UsuallyTransmit", True, True) Then Return False

            For i As Integer = 0 To grdCH.RowCount - 1
                With grdCH.Rows(i)
                    If .Cells(0).Value > 0 Then
                        If .Cells(1).Value <> Nothing Then
                            'Data
                            '区切り文字「@NEXT」追加
                            If (.Cells(1).Value = "@NEXT") Then

                            Else
                                If Not IsNumeric(.Cells(1).Value) Or .Cells(1).Value = "0000" Or .Cells(1).Value = "00000" Then
                                    MsgBox("Please set Transmission CH [Data]." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                                    Return False
                                End If

                                If Integer.Parse(.Cells(1).Value) > 65535 Then
                                    MsgBox("Please set Transmission CH [Data]. '0'-'65535'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End With
            Next i

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mDispTransmisionChName(ByVal intRow As Integer)

        Try

            '' If CCInt(grdCH("cmbType", intRow).Value) = 1 Then
            If grdCH("txtCHNo", intRow).Value <> "" Then
                grdCH("txtItemName", intRow).Value = gGetChNoToChName(CCInt(grdCH("txtChNo", intRow).Value))
            Else
                grdCH("txtItemName", intRow).Value = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbType"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChNo"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtItemName" : Column3.ReadOnly = True
            Dim column4 As New DataGridViewCheckBoxColumn : Column4.Name = "chkCheck"

            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(column4)
                .AllowUserToResizeColumns = False   '列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                '全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Type"
                .Columns(0).Width = 0
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Data"
                .Columns(1).Width = 80

                .Columns(2).HeaderText = "Item Name"
                .Columns(2).Width = 208

                .Columns(3).HeaderText = "Status"
                .Columns(3).Width = 80

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter '列ヘッダー　センタリング

                ''行
                .RowCount = 3000 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                '行番号を出す
                .RowHeadersVisible = True
                For i = 0 To .RowCount - 1 Step 1
                    .Rows(i).HeaderCell.Value = (i + 1).ToString
                Next i
                .RowHeadersWidth = 60

                '偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                'ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtItemName").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdCH)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) VDR情報構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef shtNum As Short, _
                              ByRef udtVdr As gTypSetChSioVdr, _
                              ByRef udtSioCh As gTypSetChSioCh)

        Try
            Dim intChCnt As Integer = 0

            Dim iSu As Integer = 0
            Dim iCnt As Integer = 0

            With udtVdr

                .shtExtComID = 0            '外部機器識別子 0固定
                .shtPriority = 0            '優先度 0固定
                .shtSysno = 0               'パート設定 0固定
                .shtCommType1 = &H5         'CommType1 0x05固定
                .shtCommType2 = &H21        'CommType2 0x21固定

                .udtCommInf.shtComm = 1     '回線種類 1固定
                .udtCommInf.shtDataBit = cmbDataBit.SelectedValue       'Data bit
                .udtCommInf.shtParity = cmbParityBit.SelectedValue      'Parity bit
                .udtCommInf.shtStop = cmbStopBit.SelectedValue          'Stop bit
                .udtCommInf.shtComBps = cmbCom.SelectedValue            'COM bps

                .shtReceiveInit = 0         '受信タイムアウト起動時 0固定
                .shtReceiveUseally = 0      '受信タイムアウト通常時 0固定
                .shtSendInit = CCUInt16(txtUseallyTransmit.Text)        '送信間隔　起動時
                .shtSendUseally = CCUInt16(txtUseallyTransmit.Text)     '送信間隔　通常時
                .shtRetry = 0               'リトライ回数 0固定
                .shtDuplexSet = 1           'Duplet set 1固定
                .shtSendCH = 0              '送信CH数 0固定
                .shtKakuTbl = 0             '0固定

                'Nodeテーブル 全0固定
                For i As Integer = LBound(.udtNode) To UBound(.udtNode)
                    .udtNode(i).shtCheck = 0
                    .udtNode(i).shtAddress = 0
                Next i

                .bytSetData(0) = 0
                .bytSetData(1) = 0
                .bytSetData(2) = 0
                .bytSetData(3) = 0
                'バイナリ値格納
                'センサフェイル有無
                .bytSetData(4) = IIf(chkON.Checked, 1, 0)
                .bytSetData(5) = 0

                'CHを格納
                iSu = 8
                iCnt = 0
                For i As Integer = 0 To grdCH.RowCount - 1
                    'CHnoが空白や0なら次レコードへ
                    If grdCH(1, i).Value.trim = "" Or grdCH(1, i).Value.trim = "0000" Then
                        Continue For
                    End If

                    'CHKがOFFなら次レコードへ
                    If grdCH(3, i).Value = False Then
                        Continue For
                    End If

                    'CHK ONならバイナリへ格納
                    Call gCopyByteArray(BitConverter.GetBytes(CShort(grdCH(1, i).Value)), .bytSetData, iSu)
                    iSu = iSu + 2
                    iCnt = iCnt + 1

                    'MAX126件なのでそれ以上は格納しない
                    If iCnt >= 126 Then
                        Exit For
                    End If
                Next i

                'CH数を格納(6,7)
                Call gCopyByteArray(BitConverter.GetBytes(CShort(iCnt)), .bytSetData, 6)
            End With




            'SIO CHテーブルは、一覧全点格納
            For i As Integer = 0 To UBound(udtSioCh.udtSioChRec)
                With udtSioCh.udtSioChRec(i)

                    If grdCH(1, i).Value <> "" Then      'CHNoが0で無ければ

                        If grdCH(1, i).Value = "@NEXT" Then
                            .shtChNo = 0
                            .shtChId = &HFFFE
                        Else
                            .shtChNo = CCUInt16(grdCH(1, i).Value)
                            .shtChId = 0
                        End If

                        intChCnt += 1
                    Else
                        .shtChNo = 0
                        .shtChId = 0
                    End If

                End With

            Next

            'CH設定数
            shtNum = intChCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : コンボ設定
    ' 返り値    : 0:コンボに存在する値を選択
    ' 　　　    : 1:コンボに存在しない値を追加して選択
    ' 引き数    : ARG1 - (I ) 指定値
    ' 　　　    : ARG2 - (IO) コンボボックス
    ' 　　　    : ARG3 - (I ) コンボタイプ
    ' 　　　    : ARG4 - (I ) デフォルト値
    ' 機能説明  : 指定値が存在するか確認して存在しない場合はデフォルト値
    '--------------------------------------------------------------------
    Private Function mSetComboAdd(ByVal shtValue As UInt16, _
                                  ByRef cmbCombo As ComboBox, _
                                  ByVal udtComboType As gEnmComboType, _
                         Optional ByVal strSub As String = "") As Integer

        Try

            Dim intRtn As Integer
            Dim blnFlg As Boolean = False

            '指定された値がコンボボックスに存在するかチェック
            For i As Integer = 0 To cmbCombo.Items.Count - 1
                If shtValue = cmbCombo.Items(i).item(0) Then
                    blnFlg = True
                End If
            Next

            If blnFlg Then

                '===============================================
                ''指定された値がコンボボックスに存在する場合
                '===============================================
                ''コンボアイテムを再セットして値を選択する
                cmbCombo.SelectedValue = shtValue
                intRtn = 0
            Else
                If strSub <> "" Then
                    cmbCombo.SelectedValue = strSub
                    intRtn = 0
                End If
            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mSetDisplay(ByVal udtVdr As gTypSetChSioVdr)
        Try
            With udtVdr

                Call mSetComboAdd(.udtCommInf.shtComBps, cmbCom, gEnmComboType.ctChSioDetailcmbCom, "2")                'COM bps DEF(4800)
                Call mSetComboAdd(.udtCommInf.shtParity, cmbParityBit, gEnmComboType.ctChSioDetailcmbParityBit, "0")    'Parity bit DEF(NONE)
                Call mSetComboAdd(.udtCommInf.shtDataBit, cmbDataBit, gEnmComboType.ctChSioDetailcmbDataBit, "0")       'Data bit DEF(8)
                Call mSetComboAdd(.udtCommInf.shtStop, cmbStopBit, gEnmComboType.ctChSioDetailcmbStopBit, "1")          'Stop bit DEF(1)

                txtUseallyTransmit.Text = .shtSendUseally       ''Transmit Waiting time(useally)

                chkON.Checked = IIf(.bytSetData(4) = 1, True, False)    'Sensor fail ON OFF
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplaySioCh(ByVal udtSioCh As gTypSetChSioCh)

        Dim intCommChType As Integer

        For i As Integer = 0 To UBound(udtSioCh.udtSioChRec)

            With udtSioCh.udtSioChRec(i)

                ''通信チャンネルタイプを取得
                If .shtChId = 0 And .shtChNo = 0 Then
                    ''全て 0 ならタイプ未選択
                    intCommChType = gCstCodeChSioCommChType1Nothing

                ElseIf .shtChId = &HFFFE And .shtChNo = 0 Then      '' 2014.01.14
                    ''NEXT 区切り文字
                    intCommChType = gCstCodeChSioCommChType1NEXT

                ElseIf .shtChId <> 0 Or .shtChNo <> 0 Then

                    ''チャンネルIDかNoが設定されていてデータ長が 0 ならCHデータ
                    intCommChType = gCstCodeChSioCommChType1ChData

                Else
                    ''上記以外ならタイプ未選択
                    intCommChType = gCstCodeChSioCommChType1Nothing
                End If

                Select Case intCommChType
                    Case gCstCodeChSioCommChType1Nothing
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1Nothing)
                        grdCH(1, i).Value = ""
                        grdCH(2, i).Value = ""
                        grdCH(3, i).Value = False
                    Case gCstCodeChSioCommChType1ChData
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1ChData)
                        grdCH(1, i).Value = .shtChNo.ToString("0000")
                        Call mDispTransmisionChName(i)
                        'CHnoをもとにバイナリからチェックを決定
                        grdCH(3, i).Value = fnGetChNoCHK(.shtChNo.ToString("0000"))

                    Case gCstCodeChSioCommChType1NEXT   '区切り文字
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1NEXT)
                        grdCH(1, i).Value = "@NEXT"
                        grdCH(3, i).Value = False
                End Select

            End With

        Next

    End Sub

    'CHNoをもとに、SIOのバイナリCHNoを探してあればTrueを戻す
    Private Function fnGetChNoCHK(pstrCHno As String) As Boolean
        Dim bRet As Boolean = False
        Try
            Dim shtCHno As Short = 0
            Dim strCHno As String = ""
            Dim iSu As Integer = 0

            With mudtVdr(cmbPort.SelectedIndex)
                iSu = 8
                Do
                    shtCHno = BitConverter.ToInt16(.bytSetData, iSu)
                    If shtCHno <= 0 Then
                        '0なら処理抜け
                        Exit Do
                    End If

                    strCHno = shtCHno.ToString("0000")
                    If pstrCHno = strCHno Then
                        '引数と一致すればTrueにして処理抜け
                        bRet = True
                        Exit Do
                    End If

                    iSu = iSu + 2

                    '全126CHのため 260が終わり
                    If iSu >= 260 Then
                        Exit Do
                    End If
                Loop
                
            End With

            Return bRet
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Function

#End Region


#Region "アドレス変換"

    '--------------------------------------------------------------------
    ' 機能      : アドレス数値変換
    ' 返り値    : 変換後値
    ' 引き数    : ARG1 - ( O) 変換前文字列
    ' 機能説明  : アドレスを数値に変換する
    '--------------------------------------------------------------------
    Private Function mConvNodeAddress(ByVal intInput As Integer) As String

        Dim strRtn As Integer

        Try

            Select Case intInput
                Case 49 : strRtn = "RA"
                Case 50 : strRtn = "RB"
                Case 51 : strRtn = "RC"
                Case 52 : strRtn = "RD"
                Case 53 : strRtn = "RE"
                Case 54 : strRtn = "RF"
                Case 55 : strRtn = "RG"
                Case 56 : strRtn = "RH"
                Case 57 : strRtn = "RI"
                Case 58 : strRtn = "RJ"
                Case 59 : strRtn = "RK"
                Case 60 : strRtn = "RL"
                Case 61 : strRtn = "RM"
                Case 62 : strRtn = "RN"
                Case 63 : strRtn = "RO"
                Case 65 : strRtn = "PA"
                Case 66 : strRtn = "PB"
                Case 67 : strRtn = "PC"
                Case 68 : strRtn = "PD"
                Case 69 : strRtn = "PE"
                Case 70 : strRtn = "PF"
                Case 71 : strRtn = "PG"
                Case 72 : strRtn = "PH"
                Case 73 : strRtn = "PI"
                Case 74 : strRtn = "PJ"
                Case 75 : strRtn = "PK"
                Case 76 : strRtn = "PL"
                Case 77 : strRtn = "PM"
                Case 78 : strRtn = "PN"
                Case 79 : strRtn = "PO"
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : アドレス数値変換
    ' 返り値    : 変換後値
    ' 引き数    : ARG1 - ( O) 変換前文字列
    ' 機能説明  : アドレスを数値に変換する
    '--------------------------------------------------------------------
    Private Function mConvNodeAddress(ByVal strInput As String) As Integer

        Try

            Dim intRtn As Integer

            Select Case strInput
                Case "RA", "ra" : intRtn = "49"
                Case "RB", "rb" : intRtn = "50"
                Case "RC", "rc" : intRtn = "51"
                Case "RD", "rd" : intRtn = "52"
                Case "RE", "re" : intRtn = "53"
                Case "RF", "rf" : intRtn = "54"
                Case "RG", "rg" : intRtn = "55"
                Case "RH", "rh" : intRtn = "56"
                Case "RI", "ri" : intRtn = "57"
                Case "RJ", "rj" : intRtn = "58"
                Case "RK", "rk" : intRtn = "59"
                Case "RL", "rl" : intRtn = "60"
                Case "RM", "rm" : intRtn = "61"
                Case "RN", "rn" : intRtn = "62"
                Case "RO", "ro" : intRtn = "63"
                Case "PA", "pa" : intRtn = "65"
                Case "PB", "pb" : intRtn = "66"
                Case "PC", "pc" : intRtn = "67"
                Case "PD", "pd" : intRtn = "68"
                Case "PE", "pe" : intRtn = "69"
                Case "PF", "pf" : intRtn = "70"
                Case "PG", "pg" : intRtn = "71"
                Case "PH", "ph" : intRtn = "72"
                Case "PI", "pi" : intRtn = "73"
                Case "PJ", "pj" : intRtn = "74"
                Case "PK", "pk" : intRtn = "75"
                Case "PL", "pl" : intRtn = "76"
                Case "PM", "pm" : intRtn = "77"
                Case "PN", "pn" : intRtn = "78"
                Case "PO", "po" : intRtn = "79"
            End Select

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

End Class
