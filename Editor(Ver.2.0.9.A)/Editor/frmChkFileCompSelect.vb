Public Class frmChkFileCompSelect

#Region "変数"
    Private pstrGridName() As String
#End Region

#Region "画面"
    Private Sub frmChkFileCompSelect_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call subSetGridName()
        Call subInitGrid()
        Call subGridDisp()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        'Save
        'チェックを格納
        For i As Integer = 0 To grdChk.RowCount - 1
            With grdChk.Rows(i)
                gCompareChk(i) = .Cells(0).Value()
            End With
        Next i

        Me.Close()
    End Sub

    Private Sub btnEXIT_Click(sender As System.Object, e As System.EventArgs) Handles btnEXIT.Click
        'Exit
        Me.Close()
    End Sub

    Private Sub btnDetail_Click(sender As System.Object, e As System.EventArgs) Handles btnDetail.Click
        Call frmChkFileCompSelectDetail.ShowDialog()
    End Sub


#End Region

#Region "関数"
    Private Sub subSetGridName()
        ReDim pstrGridName(UBound(gCompareChk))

        pstrGridName(0) = "Channel AddDel"
        pstrGridName(1) = "Channel Info"
        pstrGridName(2) = "System"
        pstrGridName(3) = "Terminal Info"
        pstrGridName(4) = "Composite"
        pstrGridName(5) = "Repose"
        pstrGridName(6) = "CHOutPut"
        pstrGridName(7) = "CH AndOr"
        pstrGridName(8) = "Ch RunHour"
        pstrGridName(9) = "Ctrl UseNotuse"
        pstrGridName(10) = "ExhGus"
        pstrGridName(11) = "Ch Sio"
        pstrGridName(12) = "Ch Sio Ch"
        pstrGridName(13) = "Ext Alarm"
        pstrGridName(14) = "Timer"
        pstrGridName(15) = "Timer Name"
        pstrGridName(16) = "SeqSequence"
        pstrGridName(17) = "Seq Linear Table"
        pstrGridName(18) = "Seq Operation Expression"
        pstrGridName(19) = "Ch DataSave Table"
        pstrGridName(20) = "Ch DataForward Table Set"
        pstrGridName(21) = "Ops Disp"
        pstrGridName(22) = "Ops Pulldown Menu"
        pstrGridName(23) = "Ops Selection Menu"
        pstrGridName(24) = "Ops Graph"
        pstrGridName(25) = "Ops FreeGraph"
        pstrGridName(26) = "Ops FreeDisplay"
        pstrGridName(27) = "Group Disp"
        pstrGridName(28) = "Ops TrendGraph"
        pstrGridName(29) = "Ops LogFormat"
        pstrGridName(30) = "Ops Gws Ch"
        pstrGridName(31) = "Ch Conv"
        pstrGridName(32) = "Mimic Files"
    End Sub

    'Grid初期化
    Private Sub subInitGrid()
        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle
            Dim Column0 As New DataGridViewCheckBoxColumn : Column0.Name = "chkSel"
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtName" : Column1.ReadOnly = True

            'Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdChk

                ''列
                .Columns.Clear()
                .Columns.Add(Column0) : .Columns.Add(Column1)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "C" : .Columns(0).Width = 20
                .Columns(1).HeaderText = "Name" : .Columns(1).Width = 240
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

        For i As Integer = 0 To UBound(gCompareChk) Step 1
            With grdChk
                'Chkデータを表示
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = gCompareChk(i)
                .Rows(.RowCount - 1).Cells(1).Value = pstrGridName(i)
            End With
        Next i


    End Sub

    '保存
#End Region

End Class