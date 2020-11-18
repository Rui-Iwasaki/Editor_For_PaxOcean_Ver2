Public Class frmChGroupReposeDetail

#Region "定数定義"

    ''CH数
    Private Const mCstCodeBtnCnt As Integer = 6

    ''MaskBit数（0～7）
    Private Const mCstCodeMaskBitCnt As Integer = 8

#End Region

#Region "変数設定"

    ''ボタン配列用
    Private mCmdOn() As System.Windows.Forms.Button     ''Expect for the RUN,REPOSE
    Private mCmdOff() As System.Windows.Forms.Button    ''OFF time REPOSE

    ''画面処理で使用
    Dim mudtSetRepose As gTypSetChGroupReposeRec = Nothing
    Dim mintRtn As Integer
    Dim mintDispNo As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) リポーズ入力設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef udtSetResp As gTypSetChGroupReposeRec, _
                     ByVal intRow As Integer, _
                     ByRef frmOwner As Form)

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetRepose = udtSetResp
            mintDispNo = intRow + 1

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then udtSetResp = mudtSetRepose

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
    Private Sub frmChGroupReposeDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''ボタンクリックイベント用インスタンス作成
            mCmdOn = New System.Windows.Forms.Button(mCstCodeBtnCnt - 1) { _
                cmdOnRepose1, cmdOnRepose2, cmdOnRepose3, cmdOnRepose4, cmdOnRepose5, cmdOnRepose6}

            mCmdOff = New System.Windows.Forms.Button(mCstCodeBtnCnt - 1) { _
                cmdOffRepose1, cmdOffRepose2, cmdOffRepose3, cmdOffRepose4, cmdOffRepose5, cmdOffRepose6}

            ''イベントハンドラに関連付け
            For i As Integer = 0 To mCstCodeBtnCnt - 1
                AddHandler mCmdOn(i).MouseClick, AddressOf Me.mCmdOnReposeClick
                AddHandler mCmdOff(i).MouseClick, AddressOf Me.mCmdOffReposeClick
            Next

            ''画面左上 No設定
            lblIdNo.Text = mintDispNo

            ''画面設定
            Call mSetDisplay(mudtSetRepose)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChGroupReposeDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdRepose.CurrentCell = Nothing

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
            Call grdRepose.EndEdit()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtSetRepose)

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



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdRepose.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdGroupRepose_KeyPress

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdGroupRepose_KeyPress

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
    Private Sub grdGroupRepose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdRepose.KeyPress

        Try

            Dim strColumnName As String

            ''選択セルの名称取得
            strColumnName = grdRepose.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtChNo" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
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
    Private Sub grdGroupRepose_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdRepose.CellValidating

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRepose.CellValidated

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdRepose.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdRepose.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Then

                ''数値チェック
                If IsNumeric(grdRepose.Rows(e.RowIndex).Cells("txtChNo").Value) Then

                    ''フォーマット
                    grdRepose.Rows(e.RowIndex).Cells("txtChNo").Value = Integer.Parse(grdRepose.Rows(e.RowIndex).Cells("txtChNo").Value).ToString("0000")

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Expect for the RUN,REPOSE ボタンクリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub mCmdOnReposeClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Try

            Dim strName As String, intNo As String
            Dim i As Integer

            strName = CType(sender, System.Windows.Forms.Button).Name
            intNo = Integer.Parse(strName.Substring(strName.Length - 1, 1))

            With grdRepose

                For i = 1 To mCstCodeMaskBitCnt
                    .Rows(intNo - 1).Cells(i).Value = True
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OFF time REPOSE ボタンクリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub mCmdOffReposeClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Try

            Dim strName As String, intNo As String
            Dim i As Integer

            strName = CType(sender, System.Windows.Forms.Button).Name
            intNo = Integer.Parse(strName.Substring(strName.Length - 1, 1))

            With grdRepose

                For i = 1 To mCstCodeMaskBitCnt
                    .Rows(intNo - 1).Cells(i).Value = False
                Next

            End With

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
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chkMaskBit7"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chkMaskBit6"
            Dim Column4 As New DataGridViewCheckBoxColumn : Column4.Name = "chkMaskBit5"
            Dim Column5 As New DataGridViewCheckBoxColumn : Column5.Name = "chkMaskBit4"
            Dim Column6 As New DataGridViewCheckBoxColumn : Column6.Name = "chkMaskBit3"
            Dim Column7 As New DataGridViewCheckBoxColumn : Column7.Name = "chkMaskBit2"
            Dim Column8 As New DataGridViewCheckBoxColumn : Column8.Name = "chkMaskBit1"
            Dim Column9 As New DataGridViewCheckBoxColumn : Column9.Name = "chkMaskBit0"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .AllowUserToResizeColumns = False           ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "" : .Columns(0).Width = 86
                .Columns(1).HeaderText = "Mask Bit" : .Columns(1).Width = 320
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter    ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 60

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

            End With

            With grdRepose

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .Columns.Add(Column3)
                .Columns.Add(Column4)
                .Columns.Add(Column5)
                .Columns.Add(Column6)
                .Columns.Add(Column7)
                .Columns.Add(Column8)
                .Columns.Add(Column9)
                .AllowUserToResizeColumns = False       ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 86
                .Columns(1).HeaderText = "7" : .Columns(1).Width = 40
                .Columns(2).HeaderText = "6" : .Columns(2).Width = 40
                .Columns(3).HeaderText = "5" : .Columns(3).Width = 40
                .Columns(4).HeaderText = "4" : .Columns(4).Width = 40
                .Columns(5).HeaderText = "3" : .Columns(5).Width = 40
                .Columns(6).HeaderText = "2" : .Columns(6).Width = 40
                .Columns(7).HeaderText = "1" : .Columns(7).Width = 40
                .Columns(8).HeaderText = "0" : .Columns(8).Width = 40
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter    ''列ヘッダー　センタリング

                ''行
                .RowTemplate.Height = 48            ''行の高さ
                .RowCount = 6 + 1                   ''行数＋ヘッダー行
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 60
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = "CH" & i.ToString
                Next

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
                Call gSetGridCopyAndPaste(grdRepose)

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

            Dim i As Integer, j As Integer
            Dim strChNo As String
            Dim blnMsk0 As Boolean, blnMsk1 As Boolean, blnMsk2 As Boolean, blnMsk3 As Boolean
            Dim blnMsk4 As Boolean, blnMsk5 As Boolean, blnMsk6 As Boolean, blnMsk7 As Boolean

            ''グリッドの保留中の変更を全て適用させる
            Call grdRepose.EndEdit()

            With grdRepose

                For i = 0 To .RowCount - 1

                    ''-----------------------------
                    '' レンジチェック
                    ''-----------------------------
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                    ''-----------------------------
                    '' 入力チェック
                    ''-----------------------------
                    strChNo = gGetString(.Rows(i).Cells("txtChNo").Value)
                    blnMsk0 = gGetString(.Rows(i).Cells("chkMaskBit0").Value)
                    blnMsk1 = gGetString(.Rows(i).Cells("chkMaskBit1").Value)
                    blnMsk2 = gGetString(.Rows(i).Cells("chkMaskBit2").Value)
                    blnMsk3 = gGetString(.Rows(i).Cells("chkMaskBit3").Value)
                    blnMsk4 = gGetString(.Rows(i).Cells("chkMaskBit4").Value)
                    blnMsk5 = gGetString(.Rows(i).Cells("chkMaskBit5").Value)
                    blnMsk6 = gGetString(.Rows(i).Cells("chkMaskBit6").Value)
                    blnMsk7 = gGetString(.Rows(i).Cells("chkMaskBit7").Value)


                    If strChNo = "" And _
                       blnMsk0 = False And blnMsk1 = False And blnMsk2 = False And blnMsk3 = False And _
                       blnMsk4 = False And blnMsk5 = False And blnMsk6 = False And blnMsk7 = False Then
                        ''OK

                    Else

                        ''CH入力抜け
                        If strChNo = "" Then

                            Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If

                    End If

                    ''-----------------------------
                    '' CH番号の重複登録は不可
                    ''-----------------------------
                    For j = i + 1 To .RowCount - 1

                        If gGetString(grdRepose(0, i).Value) <> "" Then

                            If gGetString(grdRepose(0, i).Value) = gGetString(grdRepose(0, j).Value) Then

                                Call MessageBox.Show("The same name as [CH No.] cannot be set of CH No [" & grdRepose(0, i).Value & "] and CH No [" & grdRepose(0, j).Value & "].", _
                                                     "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False

                            End If

                        End If

                    Next j

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
    ' 引き数    : ARG1 - ( O) リポーズ入力設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChGroupReposeRec)

        Try

            For i As Integer = 0 To grdRepose.Rows.Count - 1

                With udtSet.udtReposeInf(i)

                    .shtChId = CCUInt16(grdRepose.Rows(i).Cells("txtChNo").Value)                                       ''CH_NO

                    .bytMask = gBitSet(.bytMask, 7, IIf(grdRepose.Rows(i).Cells("chkMaskBit7").Value, True, False))     ''MaskBit：7
                    .bytMask = gBitSet(.bytMask, 6, IIf(grdRepose.Rows(i).Cells("chkMaskBit6").Value, True, False))     ''MaskBit：6
                    .bytMask = gBitSet(.bytMask, 5, IIf(grdRepose.Rows(i).Cells("chkMaskBit5").Value, True, False))     ''MaskBit：5
                    .bytMask = gBitSet(.bytMask, 4, IIf(grdRepose.Rows(i).Cells("chkMaskBit4").Value, True, False))     ''MaskBit：4
                    .bytMask = gBitSet(.bytMask, 3, IIf(grdRepose.Rows(i).Cells("chkMaskBit3").Value, True, False))     ''MaskBit：3
                    .bytMask = gBitSet(.bytMask, 2, IIf(grdRepose.Rows(i).Cells("chkMaskBit2").Value, True, False))     ''MaskBit：2
                    .bytMask = gBitSet(.bytMask, 1, IIf(grdRepose.Rows(i).Cells("chkMaskBit1").Value, True, False))     ''MaskBit：1
                    .bytMask = gBitSet(.bytMask, 0, IIf(grdRepose.Rows(i).Cells("chkMaskBit0").Value, True, False))     ''MaskBit：0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) リポーズ入力設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByRef udtSet As gTypSetChGroupReposeRec)

        Try

            For i As Integer = 0 To grdRepose.Rows.Count - 1

                With udtSet.udtReposeInf(i)

                    grdRepose.Rows(i).Cells("txtChNo").Value = gConvZeroToNull(.shtChId, "0000")                ''CH_NO

                    grdRepose.Rows(i).Cells("chkMaskBit7").Value = IIf(gBitCheck(.bytMask, 7), True, False)     ''MaskBit：7
                    grdRepose.Rows(i).Cells("chkMaskBit6").Value = IIf(gBitCheck(.bytMask, 6), True, False)     ''MaskBit：6
                    grdRepose.Rows(i).Cells("chkMaskBit5").Value = IIf(gBitCheck(.bytMask, 5), True, False)     ''MaskBit：5
                    grdRepose.Rows(i).Cells("chkMaskBit4").Value = IIf(gBitCheck(.bytMask, 4), True, False)     ''MaskBit：4
                    grdRepose.Rows(i).Cells("chkMaskBit3").Value = IIf(gBitCheck(.bytMask, 3), True, False)     ''MaskBit：3
                    grdRepose.Rows(i).Cells("chkMaskBit2").Value = IIf(gBitCheck(.bytMask, 2), True, False)     ''MaskBit：2
                    grdRepose.Rows(i).Cells("chkMaskBit1").Value = IIf(gBitCheck(.bytMask, 1), True, False)     ''MaskBit：1
                    grdRepose.Rows(i).Cells("chkMaskBit0").Value = IIf(gBitCheck(.bytMask, 0), True, False)     ''MaskBit：0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
