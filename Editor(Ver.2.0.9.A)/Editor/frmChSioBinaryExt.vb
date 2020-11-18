Public Class frmChSioBinaryext

#Region "変数定義"

    Private mintRtn As Integer
    Private mintPortNo As Integer
    Private mbytArray() As Byte

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (I ) ポート番号
    ' 　　　    : ARG2 - (IO) バイト配列
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intPortNo As Integer, _
                          ByRef bytArray() As Byte, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mintPortNo = intPortNo
            mbytArray = bytArray

            ''本画面表示
            Call gShowFormModelessForCloseWait22(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                bytArray = mbytArray
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
    Private Sub frmChSioBinaryExt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''PortNo表示
            lblPortNo.Text = mintPortNo

            ''画面設定
            Call mSetDisplay(mbytArray)

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

            '設定内容をバイト配列に格納
            Call mSetStructure(mbytArray)

            mintRtn = 1
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
    Private Sub frmChSioBinary_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    Private Sub grdSioBinary_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSioBinary.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdSioBinary_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdSioBinary_KeyPress

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
    Private Sub grdSioBinary_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdSioBinary.KeyPress

        Try

            ''文字を大文字に変換
            Select Case e.KeyChar
                Case "a"c : e.KeyChar = "A"c
                Case "b"c : e.KeyChar = "B"c
                Case "c"c : e.KeyChar = "C"c
                Case "d"c : e.KeyChar = "D"c
                Case "e"c : e.KeyChar = "E"c
                Case "f"c : e.KeyChar = "F"c
            End Select

            e.Handled = gCheckTextInput(2, sender, e.KeyChar, False, False, False, False, "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) バイト配列
    ' 機能説明  : バイト配列に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef bytArray() As Byte)

        Try
            Dim intRow As Integer
            Dim intCol As Integer

            For i As Integer = LBound(bytArray) To UBound(bytArray)

                intRow = (i \ 16)
                intCol = (i Mod 16)

                bytArray(i) = CCbyteHex(grdSioBinary(intCol, intRow).Value)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) バイト配列
    ' 機能説明  : バイト配列の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal bytArray() As Byte)

        Try

            Dim intRow As Integer
            Dim intCol As Integer

            For i As Integer = LBound(bytArray) To UBound(bytArray)

                intRow = (i \ 16)
                intCol = (i Mod 16)

                grdSioBinary(intCol, intRow).Value = Hex(bytArray(i))
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

            Dim CellRow As Integer
            Dim CellCol As Integer

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtCol1"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtCol2"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtCol3"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtCol4"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtCol5"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtCol6"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtCol7"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtCol8"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtCol9"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtCol10"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtCol11"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtCol12"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtCol13"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtCol14"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtCol15"
            Dim Column16 As New DataGridViewTextBoxColumn : Column16.Name = "txtCol16"

            With grdSioBinary

                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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
                .Columns.Add(Column10)
                .Columns.Add(Column11)
                .Columns.Add(Column12)
                .Columns.Add(Column13)
                .Columns.Add(Column14)
                .Columns.Add(Column15)
                .Columns.Add(Column16)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                For i = 0 To .ColumnCount - 1
                    .Columns(i).HeaderText = Hex(i)
                    .Columns(i).Width = 30
                Next

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行 4032バイト
                .RowCount = (gCstRecsChSioExt / 16) + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 0 To .RowCount - 1
                    .Rows(i).HeaderCell.Value = Hex(i) & "0"
                Next
                .RowHeadersWidth = 60
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '背景色を変更
                For i = 0 To gCstRecsChSioExt - 1
                    CellRow = (i \ 16)
                    CellCol = (i Mod 16)

                    .Rows(CellRow).Cells(CellCol).Style.BackColor = gColorGridRowBack
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
                Call gSetGridCopyAndPaste(grdSioBinary)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class