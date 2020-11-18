Public Class frmOpsGwsCh

#Region "変数定義"

    Private mudtSetOpsGwsChNew As gTypSetOpsGwsCh
    Private mblnInitFlg As Boolean
    Private mintNowSelectIndex As Integer
    Private mshtNum(7) As Short
    Private mblnCopyPasteFlg As Boolean

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
            Call gShowFormModelessForCloseWait11(Me)

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
    Private Sub frmChGwsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化開始
            mblnInitFlg = True

            ''コンボボックス初期設定
            Call gSetComboBox(cmbPort, gEnmComboType.ctChGwsDetailCH)

            Call mInitialDataGrid()

            ''構造体配列初期化
            mudtSetOpsGwsChNew.InitArray()                                      '' FileRec
            For i As Integer = 0 To UBound(mudtSetOpsGwsChNew.udtGwsFileRec)
                Call mudtSetOpsGwsChNew.udtGwsFileRec(i).InitArray()            '' CHRec
            Next

            ''構造体コピー
            Call mCopyStructure(gudt.SetOpsGwsCh, mudtSetOpsGwsChNew)

            ''画面設定
            Call mSetDisplayGwsCh(mudtSetOpsGwsChNew.udtGwsFileRec(cmbPort.SelectedIndex))  '' 1ファイル分表示

            mintNowSelectIndex = cmbPort.SelectedIndex      '' 現在のテーブル番号保存
            mDrawlblGwsFile(mintNowSelectIndex)             '' ファイル名表示

            ''初期化終了
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Makeボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMake.Click

        Try

            Dim udtGwsCh As gTypSetOpsGwsCh = Nothing

            ''確認メッセージ
            If MessageBox.Show("Do you make transmission CH data of file " & cmbPort.SelectedIndex + 1 & "?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Call udtGwsCh.InitArray()
                Call gMakeGwsTransmissionChData(cmbPort.SelectedIndex, udtGwsCh)
                Call mSetDisplayGwsCh(udtGwsCh.udtGwsFileRec(cmbPort.SelectedIndex))
                Call MessageBox.Show("Data creation was completed.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Importクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : CSVからデータを読み込む
    '--------------------------------------------------------------------
    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click

        Try

            Dim strCol1() As String = Nothing
            Dim strCol2() As String = Nothing
            Dim dlgFile As New OpenFileDialog

            With dlgFile

                ''[ファイルの種類] ボックスに表示される選択肢を設定する
                .Filter = "csv file (*.csv)|*.csv"

                ''ダイアログ ボックスを表示
                If dlgFile.ShowDialog() = DialogResult.OK Then

                    ''CSVデータ取得
                    If gGetCsvData(dlgFile.FileName, grdCH.RowCount - 1, strCol1, strCol2) = 0 Then

                        For i As Integer = 0 To grdCH.RowCount - 1
                            'grdCH(0, i).Value = strCol1(i)
                            grdCH(1, i).Value = strCol1(i)
                        Next

                    End If

                End If

            End With

            For i As Integer = 0 To grdCH.RowCount - 1
                Call mDispTransmisionChName(i)
            Next

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

            Dim blnFlgGwsCh As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mshtNum(cmbPort.SelectedIndex), mudtSetOpsGwsChNew.udtGwsFileRec(cmbPort.SelectedIndex))

            ''SIO設定Ch設定が変更されている場合は設定を更新する
            For i As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
                If Not mChkStructureEquals(mudtSetOpsGwsChNew.udtGwsFileRec(i), gudt.SetOpsGwsCh.udtGwsFileRec(i)) Then
                    Call mCopyStructure(mudtSetOpsGwsChNew, gudt.SetOpsGwsCh)
                    blnFlgGwsCh = True
                    gudt.SetEditorUpdateInfo.udtSave.bytOpsGwsCh = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsGwsCh = 1
                End If
            Next

            If blnFlgGwsCh Then

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''全体更新フラグ設定
                gblnUpdateAll = True

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
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim blnFlgSioChAll As Boolean = False
            Dim blnFlgSioCh(gCstCntChSioPort - 1) As Boolean

            ''グリッドの保留中の変更を全て適用させる
            Call grdCH.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mshtNum(cmbPort.SelectedIndex), mudtSetOpsGwsChNew.udtGwsFileRec(cmbPort.SelectedIndex))

            ''SIO設定Ch設定が変更されているかチェック
            For i As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
                If Not mChkStructureEquals(mudtSetOpsGwsChNew.udtGwsFileRec(i), gudt.SetOpsGwsCh.udtGwsFileRec(i)) Then
                    blnFlgSioChAll = True
                    blnFlgSioCh(i) = True
                End If
            Next


            ''データが変更されている場合
            If blnFlgSioChAll Then

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
                        Call mCopyStructure(mudtSetOpsGwsChNew, gudt.SetOpsGwsCh)

                        ''更新フラグ設定
                        gblnUpdateAll = True

                        For i As Integer = 0 To UBound(blnFlgSioCh)
                            If blnFlgSioCh(i) Then
                                gudt.SetEditorUpdateInfo.udtSave.bytOpsGwsCh = 1
                                gudt.SetEditorUpdateInfo.udtCompile.bytOpsGwsCh = 1
                            End If
                        Next

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
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

                    e.Handled = gCheckTextInput(4, sender, e.KeyChar)

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdCH_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCH.KeyUp
        mblnCopyPasteFlg = False
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

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Type"     '' 非表示
                .Columns(0).Width = 0
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "CH No."
                .Columns(1).Width = 80

                .Columns(2).HeaderText = "Item Name"
                .Columns(2).Width = 288                 ' '' 188 -> 288

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 3000 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsGwsCh, _
                               ByRef udtTarget As gTypSetOpsGwsCh)

        Try

            ''CH設定コピー
            For i As Integer = LBound(udtSource.udtGwsFileRec) To UBound(udtSource.udtGwsFileRec)
                For j As Integer = LBound(udtSource.udtGwsFileRec(i).udtGwsChRec) To UBound(udtSource.udtGwsFileRec(i).udtGwsChRec)

                    udtTarget.udtGwsFileRec(i).udtGwsChRec(j).shtChNo = udtSource.udtGwsFileRec(i).udtGwsChRec(j).shtChNo             ''チャンネルNo
                    udtTarget.udtGwsFileRec(i).udtGwsChRec(j).shtChId = udtSource.udtGwsFileRec(i).udtGwsChRec(j).shtChId             ''チャンネルID

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
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsGwsFileRec, _
                                         ByVal udt2 As gTypSetOpsGwsFileRec) As Boolean

        Try

            ''CH設定比較
            For i As Integer = LBound(udt1.udtGwsChRec) To UBound(udt1.udtGwsChRec)

                If udt1.udtGwsChRec(i).shtChNo <> udt2.udtGwsChRec(i).shtChNo Then Return False ''チャンネルNo
                If udt1.udtGwsChRec(i).shtChId <> udt2.udtGwsChRec(i).shtChId Then Return False ''チャンネルID

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


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
                Call mSetStructure(mshtNum(mintNowSelectIndex), mudtSetOpsGwsChNew.udtGwsFileRec(mintNowSelectIndex))

                ''選択されたPortNoの情報を表示
                Call mSetDisplayGwsCh(mudtSetOpsGwsChNew.udtGwsFileRec(cmbPort.SelectedIndex))

                ''現在のTableNoを更新
                mintNowSelectIndex = cmbPort.SelectedIndex
                mDrawlblGwsFile(mintNowSelectIndex)             '' ファイル名表示

            End If

            ''元に戻す
            mblnInitFlg = False

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

            For i As Integer = 0 To grdCH.RowCount - 1

                With grdCH.Rows(i)

                    If .Cells(0).Value > 0 Then

                        If .Cells(1).Value <> Nothing Then

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

                End With

            Next


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "設定値表示"
    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ポート番号
    ' 機能説明  : ポート番号に対する設定値表示
    ' 備考　　  : 
    '--------------------------------------------------------------------
    Private Sub mSetDisplayGwsCh(ByVal udtGwsCh As gTypSetOpsGwsFileRec)

        Dim intCommChType As Integer

        For i As Integer = 0 To UBound(udtGwsCh.udtGwsChRec)

            With udtGwsCh.udtGwsChRec(i)

                ''通信チャンネルタイプを取得
                If .shtChId = 0 And .shtChNo = 0 Then
                    ''全て 0 ならタイプ未選択
                    intCommChType = gCstCodeChGwsCommChNothing

                ElseIf .shtChId <> 0 Or .shtChNo <> 0 Then

                    ''チャンネルIDかNoが設定されていてデータ長が 0 ならCHデータ
                    intCommChType = gCstCodeChGwsCommChChData

                Else
                    ''上記以外ならタイプ未選択
                    intCommChType = gCstCodeChGwsCommChNothing
                End If

                Select Case intCommChType
                    Case gCstCodeChGwsCommChNothing
                        grdCH(0, i).Value = CStr(gCstCodeChGwsCommChNothing)
                        grdCH(1, i).Value = ""
                        grdCH(2, i).Value = ""
                    Case gCstCodeChGwsCommChChData
                        grdCH(0, i).Value = CStr(gCstCodeChGwsCommChChData)
                        grdCH(1, i).Value = .shtChNo.ToString("0000")
                        Call mDispTransmisionChName(i)
                End Select

            End With

        Next

    End Sub

    Private Sub mDispTransmisionChName(ByVal intRow As Integer)

        Try

            If grdCH("txtCHNo", intRow).Value <> "" Then
                grdCH("txtItemName", intRow).Value = gGetChNoToChName(CCInt(grdCH("txtChNo", intRow).Value))
            Else
                grdCH("txtItemName", intRow).Value = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region
    
#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) VDR情報構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef shtNum As Short, _
                              ByRef udtGwsCh As gTypSetOpsGwsFileRec)

        Try

            Dim intChCnt As Integer = 0

            For i As Integer = 0 To UBound(udtGwsCh.udtGwsChRec)

                With udtGwsCh.udtGwsChRec(i)

                    If grdCH(1, i).Value <> "" Then      'CHNoが0で無ければ

                        .shtChNo = CCUInt16(grdCH(1, i).Value)
                        '.shtChId = 0
                        intChCnt += 1

                    Else
                        .shtChNo = 0
                        '.shtChId = 0

                    End If

                End With

            Next

            ''CH設定数
            shtNum = intChCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ラベル表示"
    '--------------------------------------------------------------------
    ' 機能      : ラベル表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) VDR情報構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mDrawlblGwsFile(ByVal shtIndex As Integer)

        Try

            If shtIndex >= 0 And shtIndex < 4 Then
                lblGwsFile.Text = "GWS1 " & "File" & shtIndex + 1
            Else
                lblGwsFile.Text = "GWS2 " & "File" & (shtIndex + 1) - 4
            End If
            
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

End Class