Public Class frmChOutputAoList

#Region "変数定義"

    Private mudtSetCHOutPut As gTypSetCHOutPut
    Private mudtSetCHOutPutNew As gTypSetCHOutPut

    ''アナログ出力情報格納
    Public Structure mAoInfo
        Public No As Integer
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID 又は 論理出力 ID
        Public Funo As String       ''FU 番号
        Public Portno As String     ''FU ポート番号
        Public Pin As String        ''FU 計測点番号
    End Structure
    Private mAoData As mAoInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChOutputAoList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            mudtSetCHOutPut.InitArray()
            mudtSetCHOutPutNew.InitArray()

            ''構造体複製
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPut)
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPutNew)

            ''画面設定
            Call mSetDisplay(mudtSetCHOutPut)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdAO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAO.CellDoubleClick

        Try

            If e.RowIndex < 0 Then Exit Sub

            ''詳細画面表示
            Call mEdit(e.RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： cmdEditボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try

            If grdAO.CurrentRow.Index < 0 Then Exit Sub

            ''詳細画面表示
            Call mEdit(grdAO.CurrentRow.Index)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Addボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Try

            If grdAO.CurrentRow.Index < 0 Then Exit Sub

            If MsgBox("May I insert AO Setting here ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "AO insertion") = MsgBoxResult.Yes Then

                ''一覧を1行づつ下にずらす
                Dim intRow As Integer = grdAO.CurrentRow.Index
                Dim rw, col As Integer

                For rw = grdAO.RowCount - 2 To intRow Step -1

                    For col = 0 To grdAO.ColumnCount - 1
                        grdAO(col, rw + 1).Value = grdAO(col, rw).Value
                    Next

                    mudtSetCHOutPutNew.udtCHOutPut(rw + 1 + 512) = mudtSetCHOutPutNew.udtCHOutPut(rw + 512)

                Next

                For col = 0 To grdAO.ColumnCount - 1
                    grdAO(col, intRow).Value = ""
                Next

                With mudtSetCHOutPutNew.udtCHOutPut(intRow + 512)
                    .shtSysno = 0
                    .shtChid = 0
                    .bytType = 0
                    .bytStatus = 0
                    .shtMask = 0
                    .bytOutput = 0
                    .bytFuno = 255
                    .bytPortno = 255
                    .bytPin = 255
                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Deleteボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            If grdAO.CurrentRow.Index < 0 Then Exit Sub

            If MsgBox("May I remove this AO Setting ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "AO Delete") = MsgBoxResult.Yes Then

                ''一覧を1行づつ上にずらす
                Dim intRow As Integer = grdAO.CurrentRow.Index
                Dim rw, col As Integer

                For rw = intRow + 1 To grdAO.RowCount - 1

                    For col = 0 To grdAO.ColumnCount - 1
                        grdAO(col, rw - 1).Value = grdAO(col, rw).Value
                    Next

                    mudtSetCHOutPutNew.udtCHOutPut(rw - 1 + 512) = mudtSetCHOutPutNew.udtCHOutPut(rw + 512)

                Next

                For col = 0 To grdAO.ColumnCount - 1
                    grdAO(col, grdAO.RowCount - 1).Value = ""
                Next

                With mudtSetCHOutPutNew.udtCHOutPut(grdAO.RowCount - 1 + 512)
                    .shtSysno = 0
                    .shtChid = 0
                    .bytType = 0
                    .bytStatus = 0
                    .shtMask = 0
                    .bytOutput = 0
                    .bytFuno = 255
                    .bytPortno = 255
                    .bytPin = 255
                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Saveボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetCHOutPut, mudtSetCHOutPutNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)

                Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

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
    Private Sub frmChOutputDoList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetCHOutPut, mudtSetCHOutPutNew) Then

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
                        Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)

                        Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChOutputAoList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： cmdExitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSetChOutPut As gTypSetCHOutPut)

        Try

            For i As Integer = 0 To grdAO.RowCount - 1

                With grdAO.Rows(i)

                    If (udtSetChOutPut.udtCHOutPut(i + 512).shtChid > 0) And (udtSetChOutPut.udtCHOutPut(i + 512).bytOutput = 255) Then

                        ''チャンネルID → チャンネルNO 変換
                        '.Cells(0).Value = gConvChIdToChNo(udtSetChOutPut.udtCHOutPut(i + 512).shtChid, True)
                        .Cells(0).Value = udtSetChOutPut.udtCHOutPut(i + 512).shtChid.ToString("0000")

                        ''Fu Address
                        .Cells(1).Value = gGetFuName2(udtSetChOutPut.udtCHOutPut(i + 512).bytFuno)
                        .Cells(2).Value = IIf(udtSetChOutPut.udtCHOutPut(i + 512).bytPortno = 255, "", udtSetChOutPut.udtCHOutPut(i + 512).bytPortno)
                        .Cells(3).Value = IIf(udtSetChOutPut.udtCHOutPut(i + 512).bytPin = 255, "", udtSetChOutPut.udtCHOutPut(i + 512).bytPin)
                    End If

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
    Private Sub mCopyStructure1(ByVal udtSource As gTypSetCHOutPut, _
                                ByRef udtTarget As gTypSetCHOutPut)

        Try

            For i As Integer = 512 To UBound(udtSource.udtCHOutPut)

                udtTarget.udtCHOutPut(i).shtSysno = udtSource.udtCHOutPut(i).shtSysno
                udtTarget.udtCHOutPut(i).shtChid = udtSource.udtCHOutPut(i).shtChid
                udtTarget.udtCHOutPut(i).bytType = udtSource.udtCHOutPut(i).bytType
                udtTarget.udtCHOutPut(i).bytStatus = udtSource.udtCHOutPut(i).bytStatus
                udtTarget.udtCHOutPut(i).shtMask = udtSource.udtCHOutPut(i).shtMask
                udtTarget.udtCHOutPut(i).bytOutput = udtSource.udtCHOutPut(i).bytOutput
                udtTarget.udtCHOutPut(i).bytFuno = udtSource.udtCHOutPut(i).bytFuno
                udtTarget.udtCHOutPut(i).bytPortno = udtSource.udtCHOutPut(i).bytPortno
                udtTarget.udtCHOutPut(i).bytPin = udtSource.udtCHOutPut(i).bytPin

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetCHOutPut, ByVal udt2 As gTypSetCHOutPut) As Boolean

        Try

            For i As Integer = 512 To UBound(udt1.udtCHOutPut)

                If udt1.udtCHOutPut(i).shtSysno <> udt2.udtCHOutPut(i).shtSysno Then Return False
                If udt1.udtCHOutPut(i).shtChid <> udt2.udtCHOutPut(i).shtChid Then Return False
                If udt1.udtCHOutPut(i).bytType <> udt2.udtCHOutPut(i).bytType Then Return False
                If udt1.udtCHOutPut(i).bytStatus <> udt2.udtCHOutPut(i).bytStatus Then Return False
                If udt1.udtCHOutPut(i).shtMask <> udt2.udtCHOutPut(i).shtMask Then Return False
                If udt1.udtCHOutPut(i).bytOutput <> udt2.udtCHOutPut(i).bytOutput Then Return False
                If udt1.udtCHOutPut(i).bytFuno <> udt2.udtCHOutPut(i).bytFuno Then Return False
                If udt1.udtCHOutPut(i).bytPortno <> udt2.udtCHOutPut(i).bytPortno Then Return False
                If udt1.udtCHOutPut(i).bytPin <> udt2.udtCHOutPut(i).bytPin Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 詳細画面表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 行番号
    ' 機能説明  : 対象行の詳細設定画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mEdit(ByVal intRow As Integer)

        Try


            With mudtSetCHOutPutNew.udtCHOutPut(intRow + 512)

                ''アナログ出力情報
                mAoData.No = intRow + 1 + 512
                mAoData.Sysno = .shtSysno
                mAoData.Chid = .shtChid.ToString("0000")

                mAoData.Funo = .bytFuno
                mAoData.Portno = .bytPortno
                mAoData.Pin = .bytPin

                ''アナログ出力情報詳細画面表示 ==========================
                If frmChOutputAoDetail.gShow(intRow, mAoData) = 0 Then

                    If mAoData.Chid <> 0 Then

                        ''アナログ出力情報更新
                        .shtSysno = mAoData.Sysno
                        .shtChid = mAoData.Chid

                        .bytFuno = mAoData.Funo
                        .bytPortno = mAoData.Portno
                        .bytPin = mAoData.Pin

                        ''一覧を更新
                        'grdAO.Rows(intRow).Cells(0).Value = gConvChIdToChNo(mAoData.Chid, True)
                        grdAO.Rows(intRow).Cells(0).Value = IIf(mAoData.Chid = "", "", Val(mAoData.Chid).ToString("0000"))
                        grdAO.Rows(intRow).Cells(1).Value = gGetFuName2(mAoData.Funo)
                        grdAO.Rows(intRow).Cells(2).Value = mAoData.Portno
                        grdAO.Rows(intRow).Cells(3).Value = mAoData.Pin

                    End If

                End If

            End With

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
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtFcuFuAdd1"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtFcuFuAdd2"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtFcuFuAdd3"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No."
                .Columns(0).Width = 80

                .Columns(1).HeaderText = "FCU/FU Add"
                .Columns(1).Width = 160

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可

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

                .MultiSelect = False

            End With

            With grdAO

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .Columns.Add(Column3)
                .Columns.Add(Column4)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .ColumnHeadersVisible = False
                .Columns(0).Width = 80
                .Columns(1).Width = 50
                .Columns(2).Width = 55
                .Columns(3).Width = 55
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 65
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 60
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

                ''行選択モード
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ReadOnly = True    ''書込み不可！
                .MultiSelect = False

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
