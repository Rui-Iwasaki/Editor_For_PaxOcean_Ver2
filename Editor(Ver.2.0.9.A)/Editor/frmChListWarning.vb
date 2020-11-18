Public Class frmChListWarning

#Region "変数"

#End Region

#Region "画面"

    Private Sub frmChListWarning_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'エラー一覧初期化
        For i As Integer = 0 To UBound(gChListWarningData, 1) Step 1
            For j As Integer = 0 To UBound(gChListWarningData, 2) Step 1
                gChListWarningData(i, j) = ""
            Next j
        Next i
    End Sub
    Private Sub frmChListWarning_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Grid初期化
        Call subInitGrid()

        'Grid再表示
        Call subGridDisp()
    End Sub

#End Region

#Region "関数"
    'Grid初期化
    Private Sub subInitGrid()
        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle
            Dim Column0 As New DataGridViewTextBoxColumn : Column0.Name = "CHNo" : Column0.ReadOnly = True
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "Item" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column1.Name = "AlarmLV" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column2.Name = "Detail" : Column3.ReadOnly = True

            With grdChNo

                ''列
                .Columns.Clear()
                .Columns.Add(Column0) : .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                '.AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CHNo" : .Columns(0).Width = 40
                .Columns(1).HeaderText = "Item" : .Columns(1).Width = 100
                .Columns(2).HeaderText = "AlarmLV" : .Columns(2).Width = 60
                .Columns(3).HeaderText = "Detail" : .Columns(3).Width = 200
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowHeadersVisible = False
                '.RowCount = 257
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 70
                For i = 1 To .Rows.Count
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                'cellStyle.BackColor = gColorGridRowBack
                'For i = 0 To .Rows.Count - 1
                '	If i Mod 2 <> 0 Then
                '		.Rows(i).DefaultCellStyle = cellStyle
                '	End If
                'Next


                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                '行選択
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Grid再表示
    Private Sub subGridDisp()
        For i As Integer = 0 To UBound(gChListWarningData, 2) Step 1
            If gChListWarningData(0, i) <> "" Then
                With grdChNo
                    'ChInfoデータを表示
                    .Rows.Add()
                    .Rows(.RowCount - 1).Cells(0).Value = gChListWarningData(0, i)
                    .Rows(.RowCount - 1).Cells(1).Value = gChListWarningData(1, i)
                    .Rows(.RowCount - 1).Cells(2).Value = gChListWarningData(2, i)
                    .Rows(.RowCount - 1).Cells(3).Value = gChListWarningData(3, i)
                End With
            Else
                '空白がきたら処理抜け
                Exit For
            End If
        Next i
    End Sub

#End Region

End Class