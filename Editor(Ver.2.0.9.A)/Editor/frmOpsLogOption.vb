
'
'   Ver1.9.3  追加
'
'

Public Class frmOpsLogOption

#Region "変数定義"
    Private mLogOption() As gLogOptionCH
#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞﾌｫｰﾏｯﾄ　ｵﾌﾟｼｮﾝ設定画面表示
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub frmOpsLogOption_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '' ｵﾌﾟｼｮﾝ設定ﾃﾞｰﾀ一時保存
        ReDim mLogOption(gCstOpsLogOptionMax - 1)
        mLogOption = gudt.SetOpsLogOption.udtLogOption

        ''グリッド初期設定
        Call mInitialDataGrid()

        Call mSetDisplay()

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveﾎﾞﾀﾝ　ｸﾘｯｸ時処理
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Call SaveData()
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitﾎﾞﾀﾝ　ｸﾘｯｸ時処理
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click

        Dim result As DialogResult

        Try

            If mChkInput() = False Then     '' ﾃﾞｰﾀ値変更あり
                result = MessageBox.Show("Setting has been changed." & vbNewLine & _
                                                "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                Select Case result
                    Case Windows.Forms.DialogResult.Yes     '' Yes = 保存して画面を閉じる
                        Call SaveData()     '' 保存処理

                        Me.Close()

                    Case Windows.Forms.DialogResult.No      '' No = 何もせずに画面を閉じる
                        Me.Close()

                    Case Else

                End Select
            Else
                Me.Close()
            End If

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtCHNo"
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chkType"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtX1"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtY1"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtX2"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtY2"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtX3"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtY3"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtX4"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtY4"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtX5"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtY5"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtX6"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtY6"

            '' 数値を右揃えに設定
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdLog

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(Column4)
                .Columns.Add(Column5) : .Columns.Add(Column6) : .Columns.Add(Column7) : .Columns.Add(Column8)
                .Columns.Add(Column9) : .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14)
                .AllowUserToResizeColumns = True   ''列幅の変更可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CHNo." : .Columns(0).Width = 60
                .Columns(1).HeaderText = "Noon" : .Columns(1).Width = 40
                .Columns(2).HeaderText = "X1" : .Columns(2).Width = 60
                .Columns(3).HeaderText = "Y1" : .Columns(3).Width = 60
                .Columns(4).HeaderText = "X2" : .Columns(4).Width = 60
                .Columns(5).HeaderText = "Y2" : .Columns(5).Width = 60
                .Columns(6).HeaderText = "X3" : .Columns(6).Width = 60
                .Columns(7).HeaderText = "Y3" : .Columns(7).Width = 60
                .Columns(8).HeaderText = "X4" : .Columns(8).Width = 60
                .Columns(9).HeaderText = "Y4" : .Columns(9).Width = 60
                .Columns(10).HeaderText = "X5" : .Columns(10).Width = 60
                .Columns(11).HeaderText = "Y5" : .Columns(11).Width = 60
                .Columns(12).HeaderText = "X6" : .Columns(12).Width = 60
                .Columns(13).HeaderText = "Y6" : .Columns(13).Width = 60
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1500 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー不可視化
                .RowHeadersVisible = False

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50

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
                Call gSetGridCopyAndPaste(grdLog)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) OPS共通設定構造体
    ' 　　　    : ARG2 - (I ) OPS詳細設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay()

        Try

            '' ﾌｧｲﾙ番号表示
            txtFileNo.Text = gudt.SetSystem.udtSysPrinter.shtLogDrawNo

            '' 動作ﾀｲﾌﾟ
            If (gudt.SetOpsLogOption.bytSetting And &H1) = &H1 Then
                radType1.Checked = True
            End If

            ''グリッド内容
            For i As Integer = 0 To UBound(mLogOption)

                With mLogOption(i)
                    If .shtCHNo = 0 Then
                        Continue For
                    End If

                    grdLog.Rows(i).Cells("txtCHNo").Value = .shtCHNo.ToString
                    grdLog.Rows(i).Cells("chkType").Value = IIf(gBitCheck(.bytType, 0), True, False)
                    grdLog.Rows(i).Cells("txtX1").Value = .gLogPosition(0).shtPosX
                    grdLog.Rows(i).Cells("txtY1").Value = .gLogPosition(0).shtPosY
                    grdLog.Rows(i).Cells("txtX2").Value = .gLogPosition(1).shtPosX
                    grdLog.Rows(i).Cells("txtY2").Value = .gLogPosition(1).shtPosY
                    grdLog.Rows(i).Cells("txtX3").Value = .gLogPosition(2).shtPosX
                    grdLog.Rows(i).Cells("txtY3").Value = .gLogPosition(2).shtPosY
                    grdLog.Rows(i).Cells("txtX4").Value = .gLogPosition(3).shtPosX
                    grdLog.Rows(i).Cells("txtY4").Value = .gLogPosition(3).shtPosY
                    grdLog.Rows(i).Cells("txtX5").Value = .gLogPosition(4).shtPosX
                    grdLog.Rows(i).Cells("txtY5").Value = .gLogPosition(4).shtPosY
                    grdLog.Rows(i).Cells("txtX6").Value = .gLogPosition(5).shtPosX
                    grdLog.Rows(i).Cells("txtY6").Value = .gLogPosition(5).shtPosY
                End With
            Next


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : 設定変更有無ﾁｪｯｸ
    ' 返り値    : False ： 変更あり     True ： 変更なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        '' ﾌｧｲﾙ番号
        If CInt(txtFileNo.Text) <> gudt.SetSystem.udtSysPrinter.shtLogDrawNo Then Return False

        '' ﾌｧｲﾙ番号が存在するのに、設定が入っていない場合は変更ありとする
        If (CInt(txtFileNo.Text) <> 0) And ((gudt.SetOpsLogOption.bytSetting And &H1) = 0) Then Return False

        For i As Integer = 0 To UBound(mLogOption)

            With mLogOption(i)
                If .shtCHNo <> CInt(grdLog.Rows(i).Cells("txtCHNo").Value) Then Return False

                If Not ((((.bytType And &H1) = &H1) And grdLog.Rows(i).Cells("chkType").Value) Or _
                    (((.bytType And &H1) = 0) And (grdLog.Rows(i).Cells("chkType").Value = 0))) Then
                    Return False
                End If

                If .gLogPosition(0).shtPosX <> grdLog.Rows(i).Cells("txtX1").Value Then Return False
                If .gLogPosition(0).shtPosY <> grdLog.Rows(i).Cells("txtY1").Value Then Return False
                If .gLogPosition(1).shtPosX <> grdLog.Rows(i).Cells("txtX2").Value Then Return False
                If .gLogPosition(1).shtPosY <> grdLog.Rows(i).Cells("txtY2").Value Then Return False
                If .gLogPosition(2).shtPosX <> grdLog.Rows(i).Cells("txtX3").Value Then Return False
                If .gLogPosition(2).shtPosY <> grdLog.Rows(i).Cells("txtY3").Value Then Return False
                If .gLogPosition(3).shtPosX <> grdLog.Rows(i).Cells("txtX4").Value Then Return False
                If .gLogPosition(3).shtPosY <> grdLog.Rows(i).Cells("txtY4").Value Then Return False
                If .gLogPosition(4).shtPosX <> grdLog.Rows(i).Cells("txtX5").Value Then Return False
                If .gLogPosition(4).shtPosY <> grdLog.Rows(i).Cells("txtY5").Value Then Return False
                If .gLogPosition(5).shtPosX <> grdLog.Rows(i).Cells("txtX6").Value Then Return False
                If .gLogPosition(5).shtPosY <> grdLog.Rows(i).Cells("txtY6").Value Then Return False

            End With
        Next


        Return True

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ﾃﾞｰﾀｺﾋﾟｰ
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub mCopyStructure()


        '' ﾌｧｲﾙ番号保存
        gudt.SetSystem.udtSysPrinter.shtLogDrawNo = CInt(txtFileNo.Text)

        If CInt(txtFileNo.Text) <> 0 Then
            gudt.SetOpsLogOption.bytSetting = 1
        Else
            gudt.SetOpsLogOption.bytSetting = 0
        End If

        For i As Integer = 0 To UBound(mLogOption)

            With mLogOption(i)
                .shtCHNo = CInt(grdLog.Rows(i).Cells("txtCHNo").Value)
                If .shtCHNo = 0 Then      '' CH未設定時はｸﾘｱ
                    .bytType = 0
                    For j As Integer = 0 To UBound(.gLogPosition)
                        .gLogPosition(j).shtPosX = 0
                        .gLogPosition(j).shtPosY = 0
                    Next
                Else
                    .bytType = IIf(grdLog.Rows(i).Cells("chkType").Value, 1, 0)

                    .gLogPosition(0).shtPosX = grdLog.Rows(i).Cells("txtX1").Value
                    .gLogPosition(0).shtPosY = grdLog.Rows(i).Cells("txtY1").Value
                    .gLogPosition(1).shtPosX = grdLog.Rows(i).Cells("txtX2").Value
                    .gLogPosition(1).shtPosY = grdLog.Rows(i).Cells("txtY2").Value
                    .gLogPosition(2).shtPosX = grdLog.Rows(i).Cells("txtX3").Value
                    .gLogPosition(2).shtPosY = grdLog.Rows(i).Cells("txtY3").Value
                    .gLogPosition(3).shtPosX = grdLog.Rows(i).Cells("txtX4").Value
                    .gLogPosition(3).shtPosY = grdLog.Rows(i).Cells("txtY4").Value
                    .gLogPosition(4).shtPosX = grdLog.Rows(i).Cells("txtX5").Value
                    .gLogPosition(4).shtPosY = grdLog.Rows(i).Cells("txtY5").Value
                    .gLogPosition(5).shtPosX = grdLog.Rows(i).Cells("txtX6").Value
                    .gLogPosition(5).shtPosY = grdLog.Rows(i).Cells("txtY6").Value
                End If


            End With
        Next
        
        gudt.SetOpsLogOption.udtLogOption = mLogOption

        Call gMakeHeader(gudt.SetOpsLogOption.udtHeader, gudtFileInfo.strFileVersion, gCstRecsOpsLogOption, gCstSizeOpsLogOption, , , , gCstFnumOpsLogOption)

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 保存処理
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub SaveData()

        Call mCopyStructure()

        ''更新フラグ設定
        gblnUpdateAll = True
        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
        gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1
        gudt.SetEditorUpdateInfo.udtSave.bytOpsLogOption = 1

        Call frmOpsLogFormatList.SetOptionColor()       '' ﾎﾞﾀﾝの色替え

    End Sub
#End Region



End Class