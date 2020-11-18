Public Class frmToolDispGrp

#Region "変数"

#End Region

#Region "画面"
    Private Sub frmToolDispGrp_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Form_Load
        'Grid初期化
        Call subInitGrid()

        'Grid再表示
        Call subGridDisp()
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        'Exitﾎﾞﾀﾝ押下
        Me.Close()
    End Sub
#End Region

#Region "関数"
    'Grid初期化
    Private Sub subInitGrid()
        Try
            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle
            Dim Column0 As New DataGridViewTextBoxColumn : Column0.Name = "txtNo" : Column0.ReadOnly = True
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtGrpNo" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column1.Name = "txtGrpPos" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column2.Name = "txtGrpName" : Column3.ReadOnly = True


            With grdChNo

                ''列
                .Columns.Clear()
                .Columns.Add(Column0) : .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "No" : .Columns(0).Width = 40
                .Columns(1).HeaderText = "GrNo" : .Columns(1).Width = 40
                .Columns(2).HeaderText = "GrPos" : .Columns(2).Width = 40
                .Columns(3).HeaderText = "GrName" : .Columns(3).Width = 340
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

        For i As Integer = 0 To UBound(gudt.SetChGroupSetM.udtGroup.udtGroupInfo) Step 1
            With grdChNo
                Dim strName As String = ""
                strName = strName & gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName1.Trim & " "
                strName = strName & gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName2.Trim & " "
                strName = strName & gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName3.Trim & " "


                'ChInfoデータを表示
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = i.ToString
                .Rows(.RowCount - 1).Cells(1).Value = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtGroupNo
                .Rows(.RowCount - 1).Cells(2).Value = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtDisplayPosition
                .Rows(.RowCount - 1).Cells(3).Value = strName
            End With
        Next i


    End Sub
#End Region

End Class