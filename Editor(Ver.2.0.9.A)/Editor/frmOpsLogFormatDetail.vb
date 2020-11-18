Public Class frmOpsLogFormatDetail

#Region "変数定義"

    ''グループCH構造体定義
    Private mudtChannelGroup As gTypChannelGroup

    ''自動生成用構造体
    Private mudtChRL As gTypLogFormatPickCH

    Private mintRtn As Integer                  ''ボタンフラグ
    Private mblnInitFlg As Boolean              ''初期化フラグ
    Private mintSelectType As Integer           ''選択タイプ（3：CH、4：GROUP）
    Private mintGroupNo As Integer              ''グループNO
    Private mintChNo As Integer                 ''チャンネルNo
    Private mblnModeGroup As Boolean            ''編集モード（TRUE:グループ設定, FALSE:個別設定）

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0：OK  <> 0：キャンセル
    ' 引き数    : ARG1 - (IO) チャンネルグループ構造体
    '           : ARG2 - (I ) 選択タイプ（3：CH、4：GROUP）
    '           : ARG3 - ( O) グループNO 
    '           : ARG4 - ( O) チャンネルNO
    '           : ARG5 - (I ) 編集モード（TRUE:グループ設定, FALSE:個別設定）
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal hudtChannelGroup As gTypChannelGroup, _
                          ByVal hintType As Integer, _
                          ByRef hintGroupNo As Integer, _
                          ByRef hintChNo As Integer, _
                          ByVal hblnModeGroup As Boolean, _
                          ByVal hudtChRL As gTypLogFormatPickCH, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtChannelGroup = hudtChannelGroup
            mintSelectType = hintType
            mintGroupNo = hintGroupNo
            mintChNo = hintChNo
            mblnModeGroup = hblnModeGroup
            mudtChRL = hudtChRL

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                hintGroupNo = mintGroupNo
                hintChNo = mintChNo
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
    Private Sub frmOpsLogFormatDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''コンボ設定（単位）
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)

            ''コンボ設定（グループNo）
            Call mMakeComboGroupNo(mudtChannelGroup)

            ''選択した項目によって表示する項目をかえる
            Select Case mintSelectType
                Case gCstCodeOpsLogFormatTypeCh

                    Me.Height = 545             ''高さ
                    lblChList.Visible = True    ''CHラベル可視化
                    grdDetails.Visible = True   ''グリッド可視化
                    Call mInitialDataGrid()     ''グリッドを初期化する

                Case gCstCodeOpsLogFormatTypeGroup

                    Me.Height = 150             ''高さ
                    lblChList.Visible = False   ''CHラベル不可視化
                    grdDetails.Visible = False  ''グリッド不可視化

            End Select

            ''VisibleFalseグループボックス不可視化
            grpVisibleFalse.Visible = False

            ''初期化フラグ
            mblnInitFlg = False

            ''選択済のグループNO再設定
            If (LBound(mudtChannelGroup.udtGroup) < mintGroupNo) And (mintGroupNo <= UBound(mudtChannelGroup.udtGroup)) Then
                cmbGroupNo.SelectedIndex = mintGroupNo - 1
                Call cmbGroupNo_SelectedIndexChanged(cmbGroupNo, New EventArgs)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Okボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''戻り値設定
            Select Case mintSelectType
                Case gCstCodeOpsLogFormatTypeCh

                    ''選択行取得
                    Dim intRowIndex As Integer = grdDetails.CurrentCell.RowIndex

                    ''グループNO取得
                    mintGroupNo = cmbGroupNo.SelectedIndex + 1

                    ''チャンネルNO取得
                    mintChNo = CCInt(grdDetails.Rows(intRowIndex).Cells("txtChNo").Value)

                Case gCstCodeOpsLogFormatTypeGroup

                    ''グループNO取得
                    mintGroupNo = cmbGroupNo.SelectedIndex + 1

            End Select

            ''OKボタンクリックフラグ
            mintRtn = 0

            ''画面を閉じる
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
    Private Sub frmOpsLogFormatDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : グループNoコンボインデックスチェンジ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択されたグループのチャンネルリストを表示する
    '----------------------------------------------------------------------------
    Private Sub cmbGroupNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroupNo.SelectedIndexChanged

        Try

            ''初期化中は処理をしない
            If mblnInitFlg Then Exit Sub

            ''タイプがCHの時だけ処理を実施
            If mintSelectType = gCstCodeOpsLogFormatTypeCh Then

                Call mSetChannelList(cmbGroupNo.SelectedIndex, mudtChannelGroup, mblnModeGroup, mudtChRL)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ダブルクリックイベント
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択したチャンネル情報で確定する
    '--------------------------------------------------------------------
    Private Sub grdDetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDetails.DoubleClick

        Try

            ''初期化中は処理を抜ける
            If mblnInitFlg Then Exit Sub

            ''グリッド情報取得
            Dim intRowIndex As Integer = grdDetails.CurrentCell.RowIndex                    ''行Index
            Dim strChNo As String = gGetString(grdDetails.Rows(intRowIndex).Cells(0).Value) ''チャンネルNO

            ''チャンネルNOが設定されている時のみ
            If strChNo <> "" Then

                ''選択情報確定
                cmdOK_Click(cmdOK, New EventArgs)

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
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtItemName"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtUnit"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdDetails

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
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 65
                .Columns(1).HeaderText = "ITEM NAME" : .Columns(1).Width = 200
                .Columns(2).HeaderText = "UNIT" : .Columns(2).Width = 100
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 100 + 1                 ''行数＋ヘッダー
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

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtItemName").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtUnit").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''行選択モード
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                .ReadOnly = True

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グループNoコンボ作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルグループ構造体
    ' 機能説明  : チャンネルグループ構造体からグループNoコンボを作成する
    '--------------------------------------------------------------------
    Private Sub mMakeComboGroupNo(ByVal udtChannelGroup As gTypChannelGroup)

        Try

            Dim dstTbl As New DataSet
            Dim strWk(1) As String

            dstTbl.Tables.Add("Table1")
            dstTbl.Tables(0).Columns.Add("Index")
            dstTbl.Tables(0).Columns.Add("Group")

            For i As Integer = 0 To gCstChannelGroupMax - 1

                With udtChannelGroup.udtGroup(i)

                    strWk(0) = i.ToString("00")
                    strWk(1) = (i + 1).ToString("00") & "：" & .strGroupName

                    Call dstTbl.Tables(0).Rows.Add(strWk)

                End With

            Next

            ''グループNoコンボ 値セット
            cmbGroupNo.DataSource = Nothing
            cmbGroupNo.Items.Clear()
            cmbGroupNo.ValueMember = dstTbl.Tables(0).Columns(0).ColumnName
            cmbGroupNo.DisplayMember = dstTbl.Tables(0).Columns(1).ColumnName
            cmbGroupNo.DataSource = dstTbl.Tables(0)
            cmbGroupNo.SelectedIndex = -1

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : チャンネルリスト作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル構造体－共通
    ' 機能説明  : 単位名称取得
    '----------------------------------------------------------------------------
    Private Function mSetChannelListGetUnit(ByVal hudtChCommon As gTypSetChRecCommon) As String

        Dim strRtn As String = ""

        Try

            ''データ取得
            Dim shtChType As Short = hudtChCommon.shtChType     ''CH種別
            Dim shtUnit As Short = hudtChCommon.shtUnit         ''単位コード
            Dim shtDataType As Short = hudtChCommon.shtData     ''データ種別コード

            Select Case shtChType
                Case gCstCodeChTypeAnalog, gCstCodeChTypePulse

                    ''アナログ
                    cmbUnit.SelectedValue = shtUnit   ''単位名称取得
                    strRtn = cmbUnit.Text               ''戻り値設定

                Case gCstCodeChTypeValve

                    ''バルブ－アナログ
                    If shtDataType = gCstCodeChDataTypeValveAI_AO1 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_AO2 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_DO1 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_DO2 Then

                        cmbUnit.SelectedValue = shtUnit   ''単位名称取得
                        strRtn = cmbUnit.Text               ''戻り値設定

                    End If

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルグループ名称作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 　　　    : ARG2 - ( O) チャンネルグループ構造体
    ' 機能説明  : チャンネル設定構造体からチャンネルグループ構造体を作成する
    '--------------------------------------------------------------------
    Private Sub mMakeChannelGroupName(ByVal udtSetChannelGroup As gTypSetChGroupSet, _
                                      ByRef udtChannelGroup As gTypChannelGroup)

        Try

            For i = 0 To UBound(udtSetChannelGroup.udtGroup.udtGroupInfo)

                ''グループ名称設定
                udtChannelGroup.udtGroup(i).strGroupName = gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName1) & _
                                                           gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName2) & _
                                                           gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName3)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : チャンネルリスト作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グループインデックス
    ' 　　　    : ARG2 - (I ) チャンネルグループ構造体
    ' 機能説明  : グリッドにチャンネルリストを表示する
    '----------------------------------------------------------------------------
    Private Sub mSetChannelList(ByVal intGroupIndex As Integer, _
                                ByVal udtChannelGroup As gTypChannelGroup, _
                                ByVal hblnModeGroup As Boolean, _
                                ByVal hudtChRL As gTypLogFormatPickCH)

        Try

            Dim intChArray As Integer
            Dim intRowIndex As Integer

            ''グリッドクリア
            Call grdDetails.Rows.Clear()

            ''グリッドを初期化する
            Call mInitialDataGrid()

            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup(intGroupIndex).udtChannelData)

                If hblnModeGroup Then

                    '====================
                    ''グループ設定モード
                    '====================
                    ''一致するグループ番号
                    If hudtChRL.udtSetChRL(i).intGroupNo = (intGroupIndex + 1) Then

                        ''チャンネル番号が0の時は処理を飛ばす
                        If hudtChRL.udtSetChRL(i).intChno <> 0 Then

                            intChArray = gConvChNoToChArrayId(hudtChRL.udtSetChRL(i).intChno.ToString("0000"))

                            With gudt.SetChInfo.udtChannel(intChArray)

                                If intChArray <> -1 Then

                                    grdDetails(0, intRowIndex).Value = gConvZeroToNull(.udtChCommon.shtChno, "0000")  ''チャンネルNO
                                    grdDetails(1, intRowIndex).Value = gGetString(.udtChCommon.strChitem)             ''ITEM_NAME
                                    grdDetails(2, intRowIndex).Value = mSetChannelListGetUnit(.udtChCommon)           ''単位

                                    intRowIndex += 1

                                End If

                            End With

                        End If

                    End If

                Else

                    '====================
                    ''個別設定モード
                    '====================
                    With udtChannelGroup.udtGroup(intGroupIndex).udtChannelData(i)

                        ''チャンネルNOが設定されている場合にデータ設定
                        If gConvZeroToNull(.udtChCommon.shtChno, "0000") <> "" Then

                            grdDetails(0, i).Value = gConvZeroToNull(.udtChCommon.shtChno, "0000")  ''チャンネルNO
                            grdDetails(1, i).Value = gGetString(.udtChCommon.strChitem)             ''ITEM_NAME
                            grdDetails(2, i).Value = mSetChannelListGetUnit(.udtChCommon)           ''単位

                        End If

                    End With

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class

