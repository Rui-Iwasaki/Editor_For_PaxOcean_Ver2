Public Class frmChSioDetail_GAI_MOD

#Region "変数定義"

    Private mintRtn As Integer
    Private mintNowSelectIndex As Integer
    Private mblnInitFlg As Boolean
    Private mshtNum() As Short
    Private mudtVdr() As gTypSetChSioVdr
    Private mudtSioCh() As gTypSetChSioCh

    Private mudtSioExt() As gTypSetChSioExt

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
    Private Sub frmChSioDetail_GAI_MOD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim i As Integer = 0

            ''初期化開始
            mblnInitFlg = True

            ''コンボボックス初期設定
            Call gSetComboBox(cmbPort, gEnmComboType.ctChSioDetailcmbPort)
            Call gSetComboBox(cmbCom, gEnmComboType.ctChSioDetailcmbCom)
            Call gSetComboBox(cmbParityBit, gEnmComboType.ctChSioDetailcmbParityBit)
            Call gSetComboBox(cmbStopBit, gEnmComboType.ctChSioDetailcmbStopBit)
            Call gSetComboBox(cmbDuplet, gEnmComboType.ctChSioDetailcmbDuplet)

            'Retryのコンボは、iniﾌｧｲﾙではなく自作とする
            With cmbRetry
                For i = 1 To 5 Step 1
                    .Items.Add(i.ToString)
                Next i
            End With

            cmbPort.SelectedIndex = mintNowSelectIndex

            'initデータならばﾃﾞﾌｫﾙﾄ値を格納
            Dim bInit As Boolean = False
            With mudtVdr(cmbPort.SelectedIndex)
                For i = 0 To UBound(.bytSetData) Step 1
                    If .bytSetData(i) <> 0 Then
                        bInit = True
                        Exit For
                    End If
                Next i
                If bInit = False Then
                    mudtVdr(cmbPort.SelectedIndex).udtCommInf.shtComBps = 4
                    mudtVdr(cmbPort.SelectedIndex).udtCommInf.shtStop = 1
                    mudtVdr(cmbPort.SelectedIndex).shtDuplexSet = 1
                End If
            End With


            'グリッド 初期設定
            Call mInitialDataGrid()

            '画面設定
            Call mSetDisplay(mudtVdr(cmbPort.SelectedIndex))

            ''初期化終了
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

    Private Sub txtTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInitialTransmit.KeyPress, txtInitialTimeout.KeyPress
        Try
            '文字を大文字に変換
            Select Case e.KeyChar
                Case "a"c : e.KeyChar = "A"c
                Case "b"c : e.KeyChar = "B"c
                Case "c"c : e.KeyChar = "C"c
                Case "d"c : e.KeyChar = "D"c
                Case "e"c : e.KeyChar = "E"c
                Case "f"c : e.KeyChar = "F"c
            End Select

            '4桁で、16進数のみを認める
            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, False, False, False, _
                                        "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")

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
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress
        Try
            If grdCH.SelectedCells.Count = 1 Then
                '文字を大文字に変換
                Select Case e.KeyChar
                    Case "a"c : e.KeyChar = "A"c
                    Case "b"c : e.KeyChar = "B"c
                    Case "c"c : e.KeyChar = "C"c
                    Case "d"c : e.KeyChar = "D"c
                    Case "e"c : e.KeyChar = "E"c
                    Case "f"c : e.KeyChar = "F"c
                End Select

                Select Case grdCH.CurrentCell.OwningColumn.Name
                    Case "txtSlaveAdd", "txtFincCode", "txtNoOfRegister"
                        '16進のみ。2桁
                        e.Handled = gCheckTextInput(2, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")
                    Case "txtStartAdd"
                        '16進のみ。4桁
                        e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")
                End Select
            End If
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
    Private Sub cmbStopBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbStopBit.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbDuplet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbDuplet.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

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
            Dim intNum As Integer = 0
            Dim intChk1 As Integer = 0
            Dim intChk2 As Integer = 0
            Dim intChk3 As Integer = 0
            Dim intChk4 As Integer = 0

            '入力チェック
            '>>>Query send
            '空白はダメ
            If txtInitialTransmit.Text = "" Then
                MsgBox("Please set Query send cycle. '64'-'2710'.", MsgBoxStyle.Exclamation, "Input error")
                txtInitialTransmit.Focus()
                Return False
            End If
            ' HEXであること。
            ' 0x64(100)～0x2710(10000)の範囲内であること
            intNum = Convert.ToInt32(txtInitialTransmit.Text, 16)
            If intNum < 100 Or intNum > 10000 Then
                MsgBox("Please set Query send cycle. '64'-'2710'.", MsgBoxStyle.Exclamation, "Input error")
                txtInitialTransmit.Focus()
                Return False
            End If

            '>>>Response Timeout
            '空白はダメ
            If txtInitialTimeout.Text = "" Then
                MsgBox("Please set Query send cycle. '64'-'4E20'.", MsgBoxStyle.Exclamation, "Input error")
                txtInitialTimeout.Focus()
                Return False
            End If
            ' HEXであること。
            ' 0x64(100)～0x4E20(20000)の範囲内であること
            intNum = Convert.ToInt32(txtInitialTimeout.Text, 16)
            If intNum < 100 Or intNum > 20000 Then
                MsgBox("Please set Response Timeout. '64'-'4E20'.", MsgBoxStyle.Exclamation, "Input error")
                txtInitialTimeout.Focus()
                Return False
            End If


            '一覧
            For i As Integer = 0 To grdCH.RowCount - 1
                With grdCH.Rows(i)
                    '全部ゼロならﾁｪｯｸしない
                    If NZfS(.Cells(0).Value) = "" And NZfS(.Cells(1).Value) = "" And _
                        NZfS(.Cells(2).Value) = "" And NZfS(.Cells(3).Value) = "" Then
                        Continue For
                    End If
                    If NZfS(.Cells(0).Value) <> "" Then
                        intChk1 = Convert.ToInt32(.Cells(0).Value, 16)
                    Else
                        intChk1 = 0
                    End If
                    If NZfS(.Cells(1).Value) <> "" Then
                        intChk2 = Convert.ToInt32(.Cells(1).Value, 16)
                    Else
                        intChk2 = 0
                    End If
                    If NZfS(.Cells(2).Value) <> "" Then
                        intChk3 = Convert.ToInt32(.Cells(2).Value, 16)
                    Else
                        intChk3 = 0
                    End If
                    If NZfS(.Cells(3).Value) <> "" Then
                        intChk4 = Convert.ToInt32(.Cells(3).Value, 16)
                    Else
                        intChk4 = 0
                    End If
                    If intChk1 = 0 And intChk2 = 0 And intChk3 = 0 And intChk4 = 0 Then
                        Continue For
                    End If


                    '0:Slave add. 0x01(1)～0xFF(255)
                    If NZfS(.Cells(0).Value) <> "" Then
                        intNum = Convert.ToInt32(.Cells(0).Value, 16)
                        If intNum < 1 Or intNum > 255 Then
                            MsgBox("Please set Slave add. '1'-'FF'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                            Return False
                        End If
                    End If
                    '1:Func. Code 0x01(1)～0x04(4)
                    If NZfS(.Cells(1).Value) <> "" Then
                        intNum = Convert.ToInt32(.Cells(1).Value, 16)
                        If intNum < 1 Or intNum > 4 Then
                            MsgBox("Please set Func Code. '1'-'4'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                            Return False
                        End If
                    End If
                    '2:Start Add. 0x00(0)～0xFFFF(65535)
                    If NZfS(.Cells(2).Value) <> "" Then
                        intNum = Convert.ToInt32(.Cells(2).Value, 16)
                        If intNum < 0 Or intNum > 65535 Then
                            MsgBox("Please set Start Add. '0'-'FFFF'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                            Return False
                        End If
                    End If
                    '3:No. of Register 0x01(1)～0x7D(125)
                    If NZfS(.Cells(3).Value) <> "" Then
                        intNum = Convert.ToInt32(.Cells(3).Value, 16)
                        If intNum < 1 Or intNum > 125 Then
                            MsgBox("Please set No. of Register. '1'-'7D'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                            Return False
                        End If
                    End If
                End With
            Next i


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtSlaveAdd"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtFincCode"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtStartAdd"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtNoOfRegister"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            With grdCH
                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(Column4)

                .AllowUserToResizeColumns = False   '列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Slave add."
                .Columns(0).Width = 100

                .Columns(1).HeaderText = "Func. Code"
                .Columns(1).Width = 100

                .Columns(2).HeaderText = "Start Add."
                .Columns(2).Width = 100

                .Columns(3).HeaderText = "No. of Register"
                .Columns(3).Width = 100

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                '行
                .RowCount = 33
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                '行番号を出す
                .RowHeadersVisible = True

                For i = 0 To .RowCount - 1 Step 1
                    .Rows(i).HeaderCell.Value = (i + 1).ToString
                Next i
                .RowHeadersWidth = 60

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
            Dim i As Integer = 0

            Dim intQeryCnt As Integer = 0
            Dim iSu As Integer = 0

            Dim strTemp As String = ""

            With udtVdr
                .shtExtComID = 0            '外部機器識別子 0固定
                .shtPriority = 0            '優先度 0固定
                .shtSysno = 0               'パート設定 0固定
                .shtCommType1 = &H3         'CommType1 0x03固定
                .shtCommType2 = &H11        'CommType2 0x11固定

                .udtCommInf.shtComm = 1     '回線種類 1固定
                .udtCommInf.shtDataBit = 0  'Data bit 0固定(常に8のため設定しない)
                .udtCommInf.shtParity = cmbParityBit.SelectedValue      'Parity bit
                .udtCommInf.shtStop = cmbStopBit.SelectedValue          'Stop bit
                .udtCommInf.shtComBps = cmbCom.SelectedValue            'COM bps

                .shtReceiveInit = Convert.ToInt32(txtInitialTimeout.Text, 16)       '受信タイムアウト起動時
                .shtReceiveUseally = Convert.ToInt32(txtInitialTimeout.Text, 16)    '受信タイムアウト通常時
                .shtSendInit = Convert.ToInt32(txtInitialTransmit.Text, 16)         '送信間隔　起動時
                .shtSendUseally = Convert.ToInt32(txtInitialTransmit.Text, 16)      '送信間隔　通常時
                .shtRetry = cmbRetry.Text                                           'リトライ回数
                .shtDuplexSet = cmbDuplet.SelectedValue                             'Duplet set
                .shtSendCH = 0                                                      '送信CH数 0固定
                .shtKakuTbl = 0

                'Nodeテーブル 全0固定
                For i = LBound(.udtNode) To UBound(.udtNode)
                    .udtNode(i).shtCheck = 0
                    .udtNode(i).shtAddress = 0
                Next i

                .bytSetData(0) = 0
                .bytSetData(1) = 0
                .bytSetData(2) = 0
                .bytSetData(3) = 0
                'バイナリ部
                '4-13は0固定
                For i = 4 To 13 Step 1
                    .bytSetData(i) = 0
                Next i
                '14はクエリ件数のため後で
                '15-35は0固定
                For i = 15 To 35 Step 1
                    .bytSetData(i) = 0
                Next i

                '36からクエリ格納
                '一覧からクエリを格納していく
                intQeryCnt = 0
                iSu = 36
                For i = 0 To grdCH.RowCount - 1 Step 1

                    '4項目全部空白なら次のレコードへ
                    If NZfS(grdCH(0, i).Value) = "" And NZfS(grdCH(1, i).Value) = "" _
                        And NZfS(grdCH(2, i).Value) = "" And NZfS(grdCH(3, i).Value) = "" Then
                        Continue For
                    End If

                    'CHK ONならバイナリへ格納
                    'スレーブアドレス=Slave add
                    .bytSetData(iSu + 0) = Convert.ToInt32(NZfZero(grdCH(0, i).Value), 16)
                    'ファンクションコード=Func. Code
                    .bytSetData(iSu + 1) = Convert.ToInt32(NZfZero(grdCH(1, i).Value), 16)
                    'MODBUSアドレス=Start Add.
                    strTemp = NZfS(grdCH(2, i).Value).PadLeft(4, "0"c)
                    .bytSetData(iSu + 2) = Convert.ToInt32(strTemp.Substring(2, 2), 16)
                    .bytSetData(iSu + 3) = Convert.ToInt32(strTemp.Substring(0, 2), 16)
                    'ﾃﾞｰﾀ数=No. of Register
                    .bytSetData(iSu + 4) = Convert.ToInt32(NZfZero(grdCH(3, i).Value), 16)
                    .bytSetData(iSu + 5) = 0
                    'データ構成
                    Select Case .bytSetData(iSu + 1)
                        Case 1, 2
                            .bytSetData(iSu + 6) = 1
                        Case 3, 4
                            .bytSetData(iSu + 6) = 2
                        Case Else
                            .bytSetData(iSu + 6) = 0
                    End Select

                    iSu = iSu + 8

                    'クエリ件数はスレーブアドレスが０以外の時のみ
                    If Convert.ToInt32(NZfZero(grdCH(0, i).Value), 16) > 0 Then
                        intQeryCnt = intQeryCnt + 1
                    End If

                Next i

                '14 クエリ件数
                .bytSetData(14) = intQeryCnt
            End With

            'SIO CHテーブルは、全クリア
            For i = 0 To UBound(udtSioCh.udtSioChRec)
                With udtSioCh.udtSioChRec(i)
                    .shtChNo = 0
                    .shtChId = 0
                End With
            Next i


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
    ' 　　　    : ARG4 - (I ) サブコード
    ' 機能説明  : 指定値が存在するか確認して存在しない場合は追加する
    '--------------------------------------------------------------------
    Private Function mSetComboAdd(ByVal shtValue As UInt16, _
                                  ByRef cmbCombo As ComboBox, _
                         Optional ByVal strSub As String = "") As Integer

        Try

            Dim intRtn As Integer
            Dim blnFlg As Boolean = False

            ''指定された値がコンボボックスに存在するかチェック
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
                Call mSetComboAdd(.udtCommInf.shtComBps, cmbCom, "4")           'Baudrate
                Call mSetComboAdd(.udtCommInf.shtStop, cmbStopBit, "1")         'Stop bit
                Call mSetComboAdd(.udtCommInf.shtParity, cmbParityBit, "0")     'Parity bit
                Call mSetComboAdd(.shtDuplexSet, cmbDuplet, "1")                'Duplet set
                'Retry count
                Select Case .shtRetry
                    Case 1, 2, 3, 4, 5
                        cmbRetry.SelectedItem = .shtRetry.ToString
                    Case Else
                        cmbRetry.SelectedItem = "5"
                End Select

                txtInitialTimeout.Text = Hex(.shtReceiveInit)                   'Query send cycle
                txtInitialTransmit.Text = Hex(.shtSendInit)                     'Response timeout

                '一覧部 bytSetDataは、36から開始
                'クエリ数32件
                Dim bByts(7) As Byte
                Dim strTemp As String
                Dim strBytes() As String
                Dim iSu As Integer = 36
                For i As Integer = 0 To 31 Step 1
                    iSu = 36 + (i * 8)

                    'バイト配列を取り出して格納
                    For j As Integer = 0 To 7 Step 1
                        bByts(j) = .bytSetData(iSu + j)
                    Next j
                    'バイト配列をHEX文字列へ変換
                    strTemp = BitConverter.ToString(bByts)
                    'HEX文字列は「-」で区切られているためスプリット
                    strBytes = strTemp.Split("-")

                    'Slave add.
                    grdCH(0, i).Value = strBytes(0)
                    'Func. Code
                    grdCH(1, i).Value = strBytes(1)
                    'Start Add.
                    grdCH(2, i).Value = strBytes(3) & strBytes(2)
                    'No. of Register
                    grdCH(3, i).Value = strBytes(4)
                Next i
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


#End Region

#Region "バイト配列変換"

    '--------------------------------------------------------------------
    ' 機能      : 構造体からバイト配列に変換
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) VDR情報詳細構造体
    ' 　　　    : ARG2 - ( O) バイト配列
    ' 機能説明  : 構造体の設定値をバイト配列に変換する
    '--------------------------------------------------------------------
    Private Sub mConvTypeToByteArray(ByVal udtVdr As gTypSetChSioVdr, ByRef bytArray() As Byte)

        Try

            Const cstByteCnt As Integer = 76

            ''配列確保
            ReDim bytArray(cstByteCnt - 1)

            With udtVdr

                Call gCopyByteArray(BitConverter.GetBytes(.shtPort), bytArray, 0)                   ''00～01（ポート番号）
                Call gCopyByteArray(BitConverter.GetBytes(.shtExtComID), bytArray, 2)               ''02～03（外部機器識別子）
                Call gCopyByteArray(BitConverter.GetBytes(.shtPriority), bytArray, 4)               ''04～05（優先度）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSysno), bytArray, 6)                  ''06～07（M/C設定）
                Call gCopyByteArray(BitConverter.GetBytes(.shtCommType1), bytArray, 8)              ''08～09（通信種類１）
                Call gCopyByteArray(BitConverter.GetBytes(.shtCommType2), bytArray, 10)             ''10～11（通信種類２）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtComm), bytArray, 12)       ''12～13（回線情報：回線種類）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtDataBit), bytArray, 14)    ''14～15（回線情報：データビット）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtParity), bytArray, 16)     ''16～17（回線情報：パリティ）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtStop), bytArray, 18)       ''18～19（回線情報：ストップビット）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtComBps), bytArray, 20)     ''20～21（回線情報：通信速度）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare1), bytArray, 22)     ''22～23（回線情報：予備1）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare2), bytArray, 24)     ''24～25（回線情報：予備2）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare3), bytArray, 26)     ''26～27（回線情報：予備3）
                Call gCopyByteArray(BitConverter.GetBytes(.shtReceiveInit), bytArray, 28)           ''28～29（受信タイムアウト（秒）起動時
                Call gCopyByteArray(BitConverter.GetBytes(.shtReceiveUseally), bytArray, 30)        ''30～31（受信タイムアウト（秒）起動後
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendInit), bytArray, 32)              ''32～33（送信間隔（秒）起動時）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendUseally), bytArray, 34)           ''34～35（送信間隔（秒）起動後）
                Call gCopyByteArray(BitConverter.GetBytes(.shtRetry), bytArray, 36)                 ''36～37（リトライ回数）
                Call gCopyByteArray(BitConverter.GetBytes(.shtDuplexSet), bytArray, 38)             ''38～39（Duplex 設定）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendCH), bytArray, 40)                ''40～41（送信CH）
                Call gCopyByteArray(BitConverter.GetBytes(.shtKakuTbl), bytArray, 42)               ''42～43（拡張ﾃｰﾌﾞﾙ有無）Ver2.0.5.8
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(0).shtCheck), bytArray, 44)      ''44～45（ノード情報１使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(0).shtAddress), bytArray, 46)    ''46～47（ノード情報１アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(1).shtCheck), bytArray, 48)      ''48～49（ノード情報２使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(1).shtAddress), bytArray, 50)    ''50～51（ノード情報２アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(2).shtCheck), bytArray, 52)      ''52～53（ノード情報３使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(2).shtAddress), bytArray, 54)    ''54～55（ノード情報３アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(3).shtCheck), bytArray, 56)      ''56～57（ノード情報４使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(3).shtAddress), bytArray, 58)    ''58～59（ノード情報４アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(4).shtCheck), bytArray, 60)      ''60～61（ノード情報５使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(4).shtAddress), bytArray, 62)    ''62～63（ノード情報５アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(5).shtCheck), bytArray, 64)      ''64～65（ノード情報６使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(5).shtAddress), bytArray, 66)    ''66～67（ノード情報６アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(6).shtCheck), bytArray, 68)      ''68～69（ノード情報７使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(6).shtAddress), bytArray, 70)    ''70～71（ノード情報７アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(7).shtCheck), bytArray, 72)      ''72～73（ノード情報８使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(7).shtAddress), bytArray, 74)    ''74～75（ノード情報８アドレス）

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バイト配列から構造体に変換
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) バイト配列
    ' 　　　    : ARG2 - ( O) VDR情報詳細構造体
    ' 機能説明  : バイト配列を構造体の設定値に変換する
    '--------------------------------------------------------------------
    Private Sub mConvByteArrayToType(ByVal bytArray() As Byte, ByRef udtVdr As gTypSetChSioVdr)

        Try

            With udtVdr

                .shtPort = BitConverter.ToUInt16(bytArray, 0)                                           ''00～01（ポート番号）
                .shtExtComID = BitConverter.ToUInt16(bytArray, 2)                                       ''02～03（外部機器識別子）
                .shtPriority = BitConverter.ToUInt16(bytArray, 4)                                       ''04～05（優先度）
                .shtSysno = BitConverter.ToUInt16(bytArray, 6)                                          ''06～07（M/C設定）
                .shtCommType1 = BitConverter.ToUInt16(bytArray, 8)                                      ''08～09（通信種類１）
                .shtCommType2 = BitConverter.ToUInt16(bytArray, 10)                                     ''10～11（通信種類２）
                .udtCommInf.shtComm = BitConverter.ToUInt16(bytArray, 12)                               ''12～13（回線情報：回線種類）
                .udtCommInf.shtDataBit = BitConverter.ToUInt16(bytArray, 14)                            ''14～15（回線情報：データビット）
                .udtCommInf.shtParity = BitConverter.ToUInt16(bytArray, 16)                             ''16～17（回線情報：パリティ）
                .udtCommInf.shtStop = BitConverter.ToUInt16(bytArray, 18)                               ''18～19（回線情報：ストップビット）
                .udtCommInf.shtComBps = BitConverter.ToUInt16(bytArray, 20)                             ''20～21（回線情報：通信速度）
                .udtCommInf.shtSpare1 = BitConverter.ToUInt16(bytArray, 22)                              ''22～23（回線情報：予備1）
                .udtCommInf.shtSpare2 = BitConverter.ToUInt16(bytArray, 24)                              ''24～25（回線情報：予備2）
                .udtCommInf.shtSpare3 = BitConverter.ToUInt16(bytArray, 26)                              ''26～27（回線情報：予備3）
                .shtReceiveInit = BitConverter.ToUInt16(bytArray, 28)                                   ''28～29（受信タイムアウト（秒）起動時）
                .shtReceiveUseally = BitConverter.ToUInt16(bytArray, 30)                                ''30～31（受信タイムアウト（秒）起動後）
                .shtSendInit = BitConverter.ToUInt16(bytArray, 32)                                      ''32～33（送信間隔（秒）起動時）
                .shtSendUseally = BitConverter.ToUInt16(bytArray, 34)                                   ''34～35（送信間隔（秒）起動後）
                .shtRetry = BitConverter.ToUInt16(bytArray, 36)                                         ''36～37（リトライ回数）
                .shtDuplexSet = BitConverter.ToUInt16(bytArray, 38)                                     ''38～39（Duplex 設定）
                .shtSendCH = BitConverter.ToUInt16(bytArray, 40)                                        ''40～41（送信CH）
                .shtKakuTbl = BitConverter.ToUInt16(bytArray, 42)                                       ''42～43（拡張ﾃｰﾌﾞﾙ有無）Ver2.0.5.8
                .udtNode(0).shtCheck = BitConverter.ToUInt16(bytArray, 44)                              ''44～45（ノード情報１使用有無）
                .udtNode(0).shtAddress = BitConverter.ToUInt16(bytArray, 46)                            ''46～47（ノード情報１アドレス）
                .udtNode(1).shtCheck = BitConverter.ToUInt16(bytArray, 48)                              ''48～49（ノード情報２使用有無）
                .udtNode(1).shtAddress = BitConverter.ToUInt16(bytArray, 50)                            ''50～51（ノード情報２アドレス）
                .udtNode(2).shtCheck = BitConverter.ToUInt16(bytArray, 52)                              ''52～53（ノード情報３使用有無）
                .udtNode(2).shtAddress = BitConverter.ToUInt16(bytArray, 54)                            ''54～55（ノード情報３アドレス）
                .udtNode(3).shtCheck = BitConverter.ToUInt16(bytArray, 56)                              ''56～57（ノード情報４使用有無）
                .udtNode(3).shtAddress = BitConverter.ToUInt16(bytArray, 58)                            ''58～59（ノード情報４アドレス）
                .udtNode(4).shtCheck = BitConverter.ToUInt16(bytArray, 60)                              ''60～61（ノード情報５使用有無）
                .udtNode(4).shtAddress = BitConverter.ToUInt16(bytArray, 62)                            ''62～63（ノード情報５アドレス）
                .udtNode(5).shtCheck = BitConverter.ToUInt16(bytArray, 64)                              ''64～65（ノード情報６使用有無）
                .udtNode(5).shtAddress = BitConverter.ToUInt16(bytArray, 66)                            ''66～67（ノード情報６アドレス）
                .udtNode(6).shtCheck = BitConverter.ToUInt16(bytArray, 68)                              ''68～69（ノード情報７使用有無）
                .udtNode(6).shtAddress = BitConverter.ToUInt16(bytArray, 70)                            ''70～71（ノード情報７アドレス）
                .udtNode(7).shtCheck = BitConverter.ToUInt16(bytArray, 72)                              ''72～73（ノード情報８使用有無）
                .udtNode(7).shtAddress = BitConverter.ToUInt16(bytArray, 74)                            ''74～75（ノード情報８アドレス）

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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
