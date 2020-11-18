Public Class frmSeqSetSequenceLogic

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtSeqLogic() As gTypCodeName
    Private mstrSelectLogicCode As String
    Private mstrSelectLogicName As String

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) シーケンスロジック構造体
    ' 　　　    : ARG2 - ( O) 選択シーケンスロジックコード
    ' 　　　    : ARG3 - ( O) 選択シーケンスロジック名称
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtSeqLogic() As gTypCodeName, _
                          ByRef strSelectLogicCode As String, _
                          ByRef strSelectLogicName As String, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''戻り値初期化
            mintRtn = 1

            ''引数保存
            mudtSeqLogic = udtSeqLogic

            ''画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''戻り値設定
            If mintRtn = 0 Then
                strSelectLogicCode = mstrSelectLogicCode
                strSelectLogicName = mstrSelectLogicName
            Else
                strSelectLogicCode = ""
                strSelectLogicName = ""
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceLogic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call InitialDataGrid()

            ''先頭行を選択状態にする
            grdLogic.CurrentCell = grdLogic.Rows(0).Cells(0)
            grdLogic.Rows(0).Cells(0).Selected = True
            mstrSelectLogicName = grdLogic.Rows(0).Cells(0).Value
            mstrSelectLogicCode = gGetComboItemCode(mstrSelectLogicName, gEnmComboType.ctSeqSetDetailLogic)

            'mstrSelectLogic = grdLogic.Rows(0).Cells(0).Value
            'grdLogic.CurrentCell = grdLogic.Rows(0).Cells(0)
            'mblnCancelFlag = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceLogic_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 一覧ダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogic.CellDoubleClick

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 一覧クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogic.CellClick

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            mstrSelectLogicName = grdLogic.Rows(e.RowIndex).Cells(0).Value
            mstrSelectLogicCode = gGetComboItemCode(mstrSelectLogicName, gEnmComboType.ctSeqSetDetailLogic)

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
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtLogicName"

            With grdLogic

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Logic Name" : .Columns(0).Width = 320
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                '■外販
                '外販の場合、33,34,35,37-46の１３件は除く＝36までとし、33～35を除外する=33件とする
                '.RowCount = UBound(mudtSeqLogic) + 2
                If gintNaiGai = 1 Then
                    .RowCount = 33 + 1
                Else
                    .RowCount = UBound(mudtSeqLogic) + 2
                End If

                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

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

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtLogicName").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdLogic)

                .ReadOnly = True

                ''値セット
                '■外販
                '外販の場合、33,34,35,37-46の１３件は除く＝36までとし、33～35を除外する
                'For i = LBound(mudtSeqLogic) To UBound(mudtSeqLogic)
                '    .Rows(i).Cells(0).Value = mudtSeqLogic(i).strName
                'Next
                If gintNaiGai = 1 Then
                    Dim z As Integer = 0
                    For i = LBound(mudtSeqLogic) To UBound(mudtSeqLogic)
                        If i > 35 Then
                            Continue For
                        Else
                            Select Case i
                                Case 32, 33, 34
                                    Continue For
                            End Select
                        End If
                        .Rows(z).Cells(0).Value = mudtSeqLogic(i).strName
                        z = z + 1
                    Next i
                    
                Else
                    For i = LBound(mudtSeqLogic) To UBound(mudtSeqLogic)
                        .Rows(i).Cells(0).Value = mudtSeqLogic(i).strName
                    Next
                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
