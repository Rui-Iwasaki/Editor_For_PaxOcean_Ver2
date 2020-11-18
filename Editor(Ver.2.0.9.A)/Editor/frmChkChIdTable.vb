Public Class frmChkChIdTable

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

            ''画面表示
            Call Me.ShowDialog()

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
    Private Sub frmChGroupReposeList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドの初期化
            Call mInitialDataGrid()
            Call mInitialDataGridHeader()

            ''画面設定
            Call mSetDisplay()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()
            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '--------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChTableChId" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChTableChNo" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtConvTableNowChId" : Column3.ReadOnly = True
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtConvTablePrevChId" : Column4.ReadOnly = True
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdGrid

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
                .Columns(0).HeaderText = "CH ID" : .Columns(0).Width = 150
                .Columns(1).HeaderText = "CH No" : .Columns(1).Width = 150
                .Columns(2).HeaderText = "CH ID" : .Columns(2).Width = 150
                .Columns(3).HeaderText = "CH ID" : .Columns(3).Width = 150
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = gCstChannelIdMax + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 65
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtChTableChId").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChTableChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtConvTableNowChId").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtConvTablePrevChId").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdGrid)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '--------------------------------------------------------------------
    Private Sub mInitialDataGridHeader()

        Try

            'Dim i As Integer
            'Dim cellStyle As New DataGridViewCellStyle

            'With grdHeader

            '    ''列
            '    .Columns.Clear()
            '    .Columns.Add(New DataGridViewCheckBoxColumn())
            '    .Columns.Add(New DataGridViewCheckBoxColumn())
            '    .Columns.Add(New DataGridViewCheckBoxColumn())
            '    .AllowUserToResizeColumns = False   ''列幅の変更不可
            '    .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            '    ''列ヘッダー
            '    .Columns(0).HeaderText = "CH Table" : .Columns(0).Width = 300
            '    .Columns(1).HeaderText = "Conv Table(Now)" : .Columns(1).Width = 150
            '    .Columns(2).HeaderText = "Conv Table(Prev)" : .Columns(2).Width = 150
            '    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

            '    ''行
            '    .RowCount = 1
            '    .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
            '    .AllowUserToResizeRows = False      ''行の高さの変更不可

            '    ''行ヘッダー
            '    .RowHeadersWidth = 100

            '    ''罫線
            '    .EnableHeadersVisualStyles = False
            '    .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            '    .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            '    .CellBorderStyle = DataGridViewCellBorderStyle.Single
            '    .GridColor = Color.Gray

            '    ''スクロールバー
            '    .ScrollBars = ScrollBars.None

            'End With

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChTableChId" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtConvTableNow" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtConvTablePrev" : Column3.ReadOnly = True
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdHeader

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH Table" : .Columns(0).Width = 300
                .Columns(1).HeaderText = "Conv Table(Now)" : .Columns(1).Width = 150
                .Columns(2).HeaderText = "Conv Table(Prev)" : .Columns(2).Width = 150
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 65
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
                .ColumnHeadersHeight = 25
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing



                '.Row(0).Height = 20


                ' ''偶数行の背景色を変える
                'cellStyle.BackColor = gColorGridRowBack
                'For i = 0 To .Rows.Count - 1
                '    If i Mod 2 <> 0 Then
                '        .Rows(i).DefaultCellStyle = cellStyle
                '    End If
                'Next

                ' ''ReadOnly色設定
                'For i = 0 To .RowCount - 1
                '    .Rows(i).Cells("txtChTableChId").Style.BackColor = gColorGridRowBackReadOnly
                '    .Rows(i).Cells("txtChTableChNo").Style.BackColor = gColorGridRowBackReadOnly
                '    .Rows(i).Cells("txtConvTableNowChId").Style.BackColor = gColorGridRowBackReadOnly
                '    .Rows(i).Cells("txtConvTablePrevChId").Style.BackColor = gColorGridRowBackReadOnly
                'Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdHeader)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplay()

        Try

            For i As Integer = 0 To grdGrid.RowCount - 1

                grdGrid.Rows(i).Cells(0).Value = gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid
                grdGrid.Rows(i).Cells(1).Value = gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno.ToString("0000")
                grdGrid.Rows(i).Cells(2).Value = gudt.SetChConvNow.udtChConv(i).shtChid
                grdGrid.Rows(i).Cells(3).Value = gudt.SetChConvPrev.udtChConv(i).shtChid

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class