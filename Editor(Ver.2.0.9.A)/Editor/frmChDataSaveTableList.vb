Public Class frmChDataSaveTableList

#Region "変数定義"

    Private mudtSetChDataSaveTable As gTypSetChDataSave = Nothing

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
            Call gShowFormModelessForCloseWait1(Me)

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
    Private Sub frmChDataSaveTableList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call InitialDataGrid()

            ''配列再定義
            Call mudtSetChDataSaveTable.InitArray()

            ''画面設定
            Call mSetDisplay(gudt.SetChDataSave)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChDataSaveTableList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdData.CurrentCell = Nothing

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

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChDataSaveTable)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChDataSaveTable, gudt.SetChDataSave) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetChDataSaveTable, gudt.SetChDataSave)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytChDataSaveTable = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChDataSaveTable = 1

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

            ''グリッドの保留中の変更を全て適用させる
            grdData.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChDataSaveTable)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChDataSaveTable, gudt.SetChDataSave) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        Call mCopyStructure(mudtSetChDataSaveTable, gudt.SetChDataSave)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytChDataSaveTable = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChDataSaveTable = 1

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdData_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdData.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdData_KeyPress

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name.Substring(0, 3) = "txt" Then
                    AddHandler tb.KeyPress, AddressOf grdData_KeyPress
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
    Private Sub grdData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdData.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdData.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtChNo" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            End If

            ''[Default]
            If strColumnName = "txtDefault" Then
                'ver1.4.0 2011.07.22 マイナス入力を可能とする
                'e.Handled = gCheckTextInput(7, sender, e.KeyChar, True, False, True)
                e.Handled = gCheckTextInput(7, sender, e.KeyChar, True, True, True)
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
    Private Sub grdData_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdData.CellValidated

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strWork As String
            Dim decimal_p As UInt16 = 0
            Dim strDecimalFormat As String = ""
            Dim strDefault As String
            'Dim lngValue As Long = 0
            Dim lngValue As Single = 0
            Dim dblValue As Double = 0

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Then

                If IsNumeric(grdData.Rows(e.RowIndex).Cells(0).Value) Then

                    grdData.Rows(e.RowIndex).Cells(0).Value() = Integer.Parse(grdData.Rows(e.RowIndex).Cells(0).Value).ToString("0000")

                End If

            ElseIf grdData.CurrentCell.OwningColumn.Name = "txtDefault" Then

                If grdData.Rows(e.RowIndex).Cells(1).Value <> Nothing Then

                    strWork = grdData.Rows(e.RowIndex).Cells(1).Value

                    If strWork = "." Then
                        grdData.Rows(e.RowIndex).Cells(1).Value = ""
                    ElseIf strWork.Substring(0, 1) = "." Then
                        grdData.Rows(e.RowIndex).Cells(1).Value = "0" & strWork
                    ElseIf strWork.Substring(strWork.Length - 1, 1) = "." Then
                        grdData.Rows(e.RowIndex).Cells(1).Value = strWork & "0"
                    End If

                    'ver1.4.0 2011.09.26 小数点設定対応
                    If IsNumeric(grdData.Rows(e.RowIndex).Cells(0).Value) Then
                        ''CHの小数点位置取得
                        decimal_p = gGetChNoToDecimalPoint(Integer.Parse(grdData.Rows(e.RowIndex).Cells(0).Value))

                        If decimal_p <> 0 Then  ''小数点有り
                            ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                            lngValue = Int(Val(grdData.Rows(e.RowIndex).Cells(1).Value) * (10 ^ decimal_p) + 0.5)
                            dblValue = lngValue / (10 ^ decimal_p)

                            strDecimalFormat = "0.".PadRight(decimal_p + 2, "0"c)
                            grdData.Rows(e.RowIndex).Cells(1).Value = dblValue.ToString(strDecimalFormat)

                        Else                      ''小数点無し
                            ''設定に小数点がある場合は小数点削除
                            strDefault = grdData.Rows(e.RowIndex).Cells(1).Value
                            If strDefault.IndexOf(".") > 0 Then
                                grdData.Rows(e.RowIndex).Cells(1).Value = Int(Val(grdData.Rows(e.RowIndex).Cells(1).Value)).ToString
                            End If
                        End If
                        End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub InitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtDefault"
            Dim Column3 As New DataGridViewComboBoxColumn : Column3.Name = "cmbDataSet"
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdData

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
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Default" : .Columns(1).Width = 90
                .Columns(2).HeaderText = "DataSet" : .Columns(2).Width = 90
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 64 + 1
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
                Call gSetComboBox(Column3, gEnmComboType.ctChDataSaveTableListColumn3)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdData)

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
            Dim strCH As String, strDefault As String, strDataSet As String

            ''グリッドの保留中の変更を全て適用させる
            Call grdData.EndEdit()

            For i = 0 To grdData.RowCount - 1

                ''-----------------------------
                '' レンジチェック
                ''-----------------------------
                If Not gChkInputNum(grdData.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                strCH = gGetString(grdData.Rows(i).Cells(0).Value)
                strDefault = gGetString(grdData.Rows(i).Cells(1).Value)
                strDataSet = gGetString(grdData.Rows(i).Cells(2).Value)

                ''-----------------------------
                '' 入力チェック
                ''-----------------------------
                If strCH = "" And strDefault = "0" And strDataSet = "0" Then
                    ''OK

                ElseIf strCH = "" And strDefault = "" And strDataSet = "0" Then
                    ''OK

                Else

                    If strCH = "" Then
                        Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If strDefault = "" Then
                        Call MessageBox.Show("Please set 'Default' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If strCH = "" And strDataSet = "1" Then
                        Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                End If

                ''-----------------------------
                '' CH番号の重複登録は不可
                ''-----------------------------
                For j = i + 1 To grdData.RowCount - 1

                    If gGetString(grdData(0, i).Value) <> "" Then

                        If gGetString(grdData(0, i).Value) = gGetString(grdData(0, j).Value) Then

                            Call MessageBox.Show("The same name as [Cylinder CH No.] cannot be set of CH No [" & grdData(0, i).Value & "] and CH No [" & grdData(0, j).Value & "].", _
                                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If

                    End If

                Next j

            Next i

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) データ保存テーブル設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChDataSave)

        Try
            Dim decimal_p As UInt16 = 0

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''CH_NO
                    .shtChid = CCUInt16(grdData.Rows(i).Cells("txtChNo").Value)

                    ''デフォルト値
                    'ver1.4.0 2011.07.22 型変更　CCSingle → CCInt()
                    If (.shtChid <> 0) Then
                        decimal_p = gGetChNoToDecimalPoint(.shtChid)
                        If decimal_p <> 0 Then
                            .intDefault = CCInt(grdData.Rows(i).Cells("txtDefault").Value * (10 ^ decimal_p))
                        Else
                            .intDefault = CCInt(grdData.Rows(i).Cells("txtDefault").Value)
                        End If
                    End If


                    ''立上げ時のデータ保存方法
                    .shtSet = CCbyte(grdData.Rows(i).Cells("cmbDataSet").Value)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) データ保存テーブル設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChDataSave)

        Try
            Dim decimal_p As UInt16 = 0
            Dim strDecimalFormat As String = ""

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''CH_NO
                    grdData.Rows(i).Cells("txtChNo").Value = gConvZeroToNull(.shtChid, "0000")

                    ''デフォルト値
                    'ver1.4.0 2011.07.22 小数点設定対応
                    If (.shtChid <> 0) Then
                        decimal_p = gGetChNoToDecimalPoint(.shtChid)

                        If decimal_p <> 0 Then
                            strDecimalFormat = "0.".PadRight(decimal_p + 2, "0"c)
                            grdData.Rows(i).Cells("txtDefault").Value = (.intDefault / (10 ^ decimal_p)).ToString(strDecimalFormat)
                        Else
                            grdData.Rows(i).Cells("txtDefault").Value = .intDefault
                        End If
                    End If

                    ''立上げ時のデータ保存方法
                    grdData.Rows(i).Cells("cmbDataSet").Value = .shtSet.ToString

                End With

            Next

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChDataSave, _
                               ByRef udtTarget As gTypSetChDataSave)

        Try

            For i As Integer = LBound(udtTarget.udtDetail) To UBound(udtTarget.udtDetail)

                udtTarget.udtDetail(i) = udtSource.udtDetail(i)

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChDataSave, _
                                         ByVal udt2 As gTypSetChDataSave) As Boolean

        Try

            For i As Integer = LBound(udt1.udtDetail) To UBound(udt1.udtDetail)

                ''CH_NO
                If udt1.udtDetail(i).shtChid <> udt2.udtDetail(i).shtChid Then Return False

                ''デフォルト値
                If udt1.udtDetail(i).intDefault <> udt2.udtDetail(i).intDefault Then Return False

                ''立上げ時のデータ保存方法
                If udt1.udtDetail(i).shtSet <> udt2.udtDetail(i).shtSet Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

End Class
