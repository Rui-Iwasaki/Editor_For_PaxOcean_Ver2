Public Class frmToolPIDtrend

#Region "定数定義"

#End Region

#Region "変数定義"
    '計測点情報
    Private Structure mChannelStr
        Dim SYSNo As String                     ''SYSTEM No
        Dim CHNo As String                      ''CH番号
        Dim CHNo_temp As String                 ''CH番号  Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時の補助表示用
        Dim CHItem As String                    ''CH名称
        Dim Status As String                    ''ステータス
        Dim Range As String                     ''レンジ
        Dim Unit As String                      ''単位
        Dim AlmInf() As mAlarmInfoStr           ''アラーム情報
        Dim INSIG As String                     ''INPUT SIGNAL
        Dim SIGType As String                   ''SIGNAL TYPE
        Dim OUTSIG As String                    ''OUTPUT SIGNAL
        Dim INAdd As String                     ''INPUT ADDRESS
        Dim OUTAdd As String                    ''OUTPUT ADDRESS
        Dim AL As String                        ''AL
        Dim RL As String                        ''RL
        Dim ShareType As String                 ''共有CHタイプ
        Dim ShareCHNo As String                 ''共有CH No
        Dim Remarks As String                   ''REMARKS
        Dim AlmLevel As String                  ''ﾛｲﾄﾞ対応表示追加　2015.11.12 Ver1.7.8
        Dim TermCount As Integer                ''端子数  Ver1.11.9.2 2016.11.26追加
        Dim OUT As String                       'Ver2.0.0.4 Output設定アリの場合「o」となる
    End Structure
    'アラーム情報
    Private Structure mAlarmInfoStr
        Dim Value As String                     ''警報値
        Dim ExtGrp As String                    ''Ext. Group No
        Dim Delay As String                     ''Delay
        Dim GrpRep1 As String                   ''Group Repose 1
        Dim GrpRep2 As String                   ''Group Repose 2
    End Structure

    Private praryCHLIST As ArrayList    '計測点CHno(配列格納順)


    Private mblnInitFlg As Boolean
    Private mudtSetOpsTrendGraphPID As gTypSetOpsTrendGraph = Nothing

    '現在選択されているコンボのインデックスを保存
    Private mintNowSelectIndex As Integer


    'PID コントロール画面のタイトル格納
    Private prGraphTitle() As String
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
    Private Sub frmToolPIDtrend_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '初期化開始
            mblnInitFlg = True

            '計測点リストのCHNoを配列順に格納(CHnoから配列番号を取得するために使用)
            praryCHLIST = New ArrayList
            With gudt.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    praryCHLIST.Add(.udtChannel(i).udtChCommon.shtChno.ToString("0000"))
                Next i
            End With


            '配列再定義
            Call gInitSetOpsTrendGraph(mudtSetOpsTrendGraphPID)

            'コンボボックス初期設定
            'コンボは、iniﾌｧｲﾙではなく自作とする
            Dim intCountPID As Integer = fnChkCountPID()
            If intCountPID > 0 Then
                ReDim prGraphTitle(intCountPID - 1)
                Call fnGetPIDctrlTitle(prGraphTitle)
            End If
            With cmbNo
                For i As Integer = 1 To intCountPID Step 1
                    .Items.Add(i.ToString & ":" & prGraphTitle(i - 1))
                Next i
            End With
            cmbNo.SelectedIndex = 0

            ''現在選択されているコンボのインデックスを保存
            mintNowSelectIndex = cmbNo.SelectedIndex

            ''グリッド初期設定
            Call mInitialDataGrid()

            ''構造体のコピー
            Call mCopyStructure(gudt.SetOpsTrendGraphPID, mudtSetOpsTrendGraphPID)

            ''画面設定
            Call mSetDisplay(cmbNo.SelectedIndex, mudtSetOpsTrendGraphPID)

            ''初期化開始
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： TableNoコンボチェンジ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNo.SelectedIndexChanged

        Try

            '初期化中は何もしない
            If mblnInitFlg Then Return

            ''入力チェック
            If Not mChkInput() Then

                'ここでの項目変更イベントは処理しない
                mblnInitFlg = True

                '入力NGの場合はTableNoを元に戻す
                cmbNo.SelectedIndex = mintNowSelectIndex

                '元に戻す
                mblnInitFlg = False

            Else

                '現在のTableNoに設定されている値を保存
                Call mSetStructure(mintNowSelectIndex, mudtSetOpsTrendGraphPID)

                '選択されたTableNoの情報を表示
                Call mSetDisplay(cmbNo.SelectedIndex, mudtSetOpsTrendGraphPID)

                '現在のTableNoを更新
                mintNowSelectIndex = cmbNo.SelectedIndex

            End If

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

            '入力チェック
            If Not mChkInput() Then Return

            '設定値を比較用構造体に格納
            Call mSetStructure(cmbNo.SelectedIndex, mudtSetOpsTrendGraphPID)

            'データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetOpsTrendGraphPID, gudt.SetOpsTrendGraphPID) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetOpsTrendGraphPID, gudt.SetOpsTrendGraphPID)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphPID = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphPID = 1

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
    Private Sub frmToolPIDtrend_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            'グリッドの保留中の変更を全て適用させる
            Call grdCH.EndEdit()

            '設定値を比較用構造体に格納
            Call mSetStructure(cmbNo.SelectedIndex, mudtSetOpsTrendGraphPID)

            'データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetOpsTrendGraphPID, gudt.SetOpsTrendGraphPID) Then

                '変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        '入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        '変更されている場合は設定を更新する
                        Call mCopyStructure(mudtSetOpsTrendGraphPID, gudt.SetOpsTrendGraphPID)

                        '更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphPID = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphPID = 1

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
    Private Sub frmToolPIDtrend_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#Region "KeyPressイベントの発生"
    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress

        Try
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力値フォーマット"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 1 Then Return

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If IsNumeric(grdCH.Rows(e.RowIndex).Cells(1).Value) Then
                grdCH.Rows(e.RowIndex).Cells(1).Value() = Integer.Parse(grdCH.Rows(e.RowIndex).Cells(1).Value).ToString("0000")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub grdCH_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCH.CellValidating
        Try
            If e.RowIndex < 0 Or e.ColumnIndex < 1 Then Exit Sub

            Dim strValue As String
            strValue = grdCH(e.ColumnIndex, e.RowIndex).EditedFormattedValue

            'ChNo取得
            Dim intChNo As Integer = CCUInt16(grdCH(1, e.RowIndex).Value)

            '選択セルの名称取得
            Dim strColumnName As String = grdCH.CurrentCell.OwningColumn.Name

            If NZfS(strValue).Trim <> "" Then
                'ChNo未指定の場合エラー
                If intChNo <= 0 And e.ColumnIndex > 1 Then
                    MsgBox("ChNo Not input.", MsgBoxStyle.Exclamation, Me.Text)
                    e.Cancel = True : Exit Sub
                End If

                Select Case strColumnName
                    Case "txtChNo"
                        '[CHNo] MAX5桁の数値のみ 101～11024
                        If strValue.Length > 5 Or (CCInt(strValue) > 11024 Or CCInt(strValue) < 101) Then
                            MsgBox("The value is illegal.", MsgBoxStyle.Exclamation, Me.Text)
                            e.Cancel = True : Exit Sub
                        Else
                            '正常CHNoの場合、InChInfo更新
                            Dim strRet As String = ""
                            Dim strSplit() As String = Nothing
                            strRet = fnGetCHdata(CCInt(strValue).ToString("0000"))
                            strSplit = strRet.Split(vbTab)
                            If strSplit.Length >= 5 Then
                                grdCH(1, e.RowIndex).Value = CCInt(strValue).ToString("0000")
                                grdCH(2, e.RowIndex).Value = strSplit(0)
                                grdCH(3, e.RowIndex).Value = strSplit(1)
                                grdCH(4, e.RowIndex).Value = strSplit(2)
                                grdCH(5, e.RowIndex).Value = strSplit(3)
                                grdCH(6, e.RowIndex).Value = strSplit(4)
                            Else
                                grdCH(1, e.RowIndex).Value = ""
                                grdCH(2, e.RowIndex).Value = ""
                                grdCH(3, e.RowIndex).Value = ""
                                grdCH(4, e.RowIndex).Value = ""
                                grdCH(5, e.RowIndex).Value = ""
                                grdCH(6, e.RowIndex).Value = ""
                            End If
                        End If
                End Select
            Else
                '未入力はOK
                Select Case strColumnName
                    Case "txtChNo"
                        grdCH(1, e.RowIndex).Value = ""
                        grdCH(2, e.RowIndex).Value = ""
                        grdCH(3, e.RowIndex).Value = ""
                        grdCH(4, e.RowIndex).Value = ""
                        grdCH(5, e.RowIndex).Value = ""
                        grdCH(6, e.RowIndex).Value = ""
                End Select
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim cellStyle As New DataGridViewCellStyle

            Dim Column00 As New DataGridViewTextBoxColumn : Column00.Name = "txtNo"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtChNo"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtChName"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtStatus"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtRange"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtUnit"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtINSIG"

            'CHNo,名称,STATUS,ﾚﾝｼﾞ,UNIT,INSIGの表形式とする。
            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column00)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                '.AllowUserToResizeColumns = False   ''列幅の変更可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing


                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Column00.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column15.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column00.ReadOnly = True
                Column10.ReadOnly = False
                Column11.ReadOnly = True
                Column12.ReadOnly = True
                Column13.ReadOnly = True
                Column14.ReadOnly = True
                Column15.ReadOnly = True

                '列ヘッダー
                .Columns(0).HeaderText = "No." : .Columns(0).Width = 40
                .Columns(1).HeaderText = "CHNo." : .Columns(1).Width = 40
                .Columns(2).HeaderText = "CH Name" : .Columns(2).Width = 100
                .Columns(3).HeaderText = "Status" : .Columns(3).Width = 80
                .Columns(4).HeaderText = "Range" : .Columns(4).Width = 80
                .Columns(5).HeaderText = "Unit" : .Columns(5).Width = 60
                .Columns(6).HeaderText = "INSIG" : .Columns(6).Width = 40
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング


                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                For i = 1 To .Rows.Count
                    .Rows(i - 1).Cells(0).Value = i.ToString("00")
                Next

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtNo").Style.BackColor = gColorGridRowBackReadOnly
                    '.Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChName").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtStatus").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRange").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtUnit").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtINSIG").Style.BackColor = gColorGridRowBackReadOnly
                Next i

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Horizontal

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

                '.ReadOnly = true

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

            'グリッドの保留中の変更を全て適用させる
            Call grdCH.EndEdit()

            With grdCH
                For i = 0 To .RowCount - 1
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo"), 1, 9999, "CH No.", i + 1, True, True) Then Return False
                Next
            End With


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 現在選択されているコンボのインデックス
    '           : ARG2 - ( O) 設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal intTableIndex As Integer, _
                              ByRef udtSet As gTypSetOpsTrendGraph)

        Try
            Dim blPage As Boolean = False

            With udtSet.udtTrendGraphRec(intTableIndex)
                blPage = False
                .strPageTitle = prGraphTitle(intTableIndex)
                For i As Integer = 0 To 7

                    'CHNo
                    .udtTrendGraphRecChno(i).shtChno = CCUInt16(grdCH.Rows(i).Cells("txtChNo").Value)

                    If .udtTrendGraphRecChno(i).shtChno <> 0 Then
                        blPage = True
                    End If
                Next

                'ページ番号の格納フロー
                If blPage = True Then
                    .bytNo = intTableIndex + 1
                Else
                    '1件もCHが無いならページ番号は０クリア
                    .bytNo = 0
                End If
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 現在選択されているコンボのインデックス
    '           : ARG2 - (I ) 設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal intTableIndex As Integer, _
                            ByVal udtSet As gTypSetOpsTrendGraph)

        Try
            '160件だが、実際に使うのは、0～7
            '従来は、OPS件数(10件)×16であるため、160
            'PIDは全OPS共通かつ、8件であるため、0～7で良い
            With udtSet.udtTrendGraphRec(intTableIndex)
                For i As Integer = 0 To 7

                    'CHNo
                    grdCH.Rows(i).Cells(1).Value = gConvZeroToNull(.udtTrendGraphRecChno(i).shtChno, "0000")

                    '詳細
                    'InChInfo表示
                    Dim strRet As String = ""
                    Dim strSplit() As String = Nothing
                    strRet = fnGetCHdata(grdCH(1, i).Value)
                    strSplit = strRet.Split(vbTab)
                    If strSplit.Length >= 5 Then
                        grdCH(1, i).Value = grdCH(1, i).Value
                        grdCH(2, i).Value = strSplit(0)
                        grdCH(3, i).Value = strSplit(1)
                        grdCH(4, i).Value = strSplit(2)
                        grdCH(5, i).Value = strSplit(3)
                        grdCH(6, i).Value = strSplit(4)
                    Else
                        grdCH(1, i).Value = ""
                        grdCH(2, i).Value = ""
                        grdCH(3, i).Value = ""
                        grdCH(4, i).Value = ""
                        grdCH(5, i).Value = ""
                        grdCH(6, i).Value = ""
                    End If

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'CHnoを元にデータを戻す
    '戻り値は各項目をタブ区切りで戻す
    '　戻り値＝名称,STATUS,ﾚﾝｼﾞ,UNIT,INSIGの表形式とする。
    Private Function fnGetCHdata(pstrCHNo As String) As String
        Dim strRet As String = ""

        'CHデータ
        Dim CHStr As mChannelStr
        '配列初期化
        Erase CHStr.AlmInf
        ReDim CHStr.AlmInf(9)
        '初期化
        CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
        CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
        For k As Integer = 0 To 9
            CHStr.AlmInf(k).Value = ""
            CHStr.AlmInf(k).ExtGrp = ""
            CHStr.AlmInf(k).Delay = ""
            CHStr.AlmInf(k).GrpRep1 = ""
            CHStr.AlmInf(k).GrpRep2 = ""
        Next
        CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
        CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
        CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
        CHStr.Remarks = "" : CHStr.AlmLevel = "" : CHStr.CHNo_temp = ""
        CHStr.OUT = ""

        Dim strCHname As String = ""
        Dim strStatus As String = ""
        Dim strRange As String = ""
        Dim strUnit As String = ""
        Dim strINSIG As String = ""

        Try
            Dim idx As Integer = 0
            idx = praryCHLIST.IndexOf(pstrCHNo)
            If idx < 0 Then
                '該当CHnoが存在しないなら空白で戻す
                Return strRet
            End If

            'CH表示文字列取得
            Call mMakeDrawCHData(gudt.SetChInfo.udtChannel(idx), CHStr)
            '>>>CH名称
            strCHname = CHStr.CHItem
            '>>>STATUS
            strStatus = CHStr.Status
            '>>>RANGE
            strRange = CHStr.Range
            '>>>UNIT
            strUnit = CHStr.Unit
            '>>>INSIG
            strINSIG = CHStr.INSIG

            '印刷用にタブ区切り格納
            strRet = ""
            strRet = strRet & strCHname & vbTab
            strRet = strRet & strStatus & vbTab
            strRet = strRet & strRange & vbTab
            strRet = strRet & strUnit & vbTab
            strRet = strRet & strINSIG

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRet
    End Function
#Region "計測点データ取得関数"
    '--------------------------------------------------------------------
    ' 機能      : CH表示データ作成
    ' 返り値    : 文字列
    ' 引き数    : ARG1 - (I ) CHデータ
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    ' 履歴      : 計測点印刷機能からの移植とカスタマイズ
    '--------------------------------------------------------------------
    Private Sub mMakeDrawCHData(ByVal udtChannel As gTypSetChRec, ByRef CHStr As mChannelStr)

        Try
            Dim strTemp As String = ""
            Dim intSpace As Integer = 0
            Dim intDecimalP As Integer = 0
            Dim strDecimalFormat As String = ""
            Dim dblValue As Double = 0
            Dim dblLowValue As Double = 0
            Dim dblHiValue As Double = 0
            Dim strHH As String = ""
            Dim strH As String = ""
            Dim strL As String = ""
            Dim strLL As String = ""
            Dim intLen As Integer = 0
            Dim intCompIndex As Integer = 0
            Dim intStatusExist As Integer = 0
            Dim nLen As Integer = 0     '' Vver1.8.1 2015.11.18
            Dim nCenter As Integer = 1  '' Ver1.10.1 2016.02.29 力率表示対応

            ''初期化
            CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
            CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
            For k As Integer = 0 To 9
                CHStr.AlmInf(k).Value = ""
                CHStr.AlmInf(k).ExtGrp = ""
                CHStr.AlmInf(k).Delay = ""
                CHStr.AlmInf(k).GrpRep1 = ""
                CHStr.AlmInf(k).GrpRep2 = ""
            Next
            CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
            CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
            CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
            CHStr.Remarks = ""
            CHStr.AlmLevel = ""     '' Ver1.7.8 2015.11.12 　ﾛｲﾄﾞ対応表示追加
            CHStr.CHNo_temp = ""    '' Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時補助用変数追加
            CHStr.TermCount = 0     '' Ver1.11.9.3 2016.11.26 追加
            CHStr.OUT = ""          '' Ver2.0.0.4 Output設定がある場合「o」表記

            With udtChannel

                If .udtChCommon.shtChno = 0 Then    ''CH番号無し
                    Return
                End If

                ''☆Common---------------------------------------------------------------------------------------
                ''SYS
                CHStr.SYSNo = .udtChCommon.shtSysno.ToString.Trim

                ''CHNo
                CHStr.CHNo = gGet2Byte(.udtChCommon.shtChno).ToString("0000")
                CHStr.CHNo_temp = CHStr.CHNo    '' Ver1.8.5.2  2015.12.02 ﾀｸﾞ表示時補助用変数追加

                ''ITEM NAME
                ''アイテム名称取得
                CHStr.CHItem = gGetString(.udtChCommon.strChitem)


                ''UNIT
                If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                    'Ver2.0.1.6 ■ﾃﾞｼﾞﾀﾙとモーターは「*」は表示無し
                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeDigital, gCstCodeChTypeMotor
                            If .udtChCommon.shtUnit = 0 Then
                                '「*」
                                CHStr.Unit = ""
                            Else
                                Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                CHStr.Unit = cmbUnit.Text
                            End If
                        Case Else
                            '上記以外は「*」を表示
                            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                            cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                            CHStr.Unit = cmbUnit.Text
                    End Select
                Else
                    CHStr.Unit = gGetString(.udtChCommon.strUnit)         ''特殊コード対応
                End If
                ''Cを℃に変換
                If CHStr.Unit = "C" Then
                    CHStr.Unit = "ﾟC"
                End If

                ''FU ADDRESS(IN)
                'CHStr.INAdd = mPrtConvFuAddress(.udtChCommon.shtFuno, .udtChCommon.shtPortno, .udtChCommon.shtPin)

                ' 2015.09.15 M.Kaihara
                ' FUアドレスが不定値の際、リスト印字すると65535の数値が表示（印字）される不具合を修正。
                ' FUアドレス不定値(0xFFFF)の際はFUアドレス表示文字を消す。
                If .udtChCommon.shtPortno = &HFFFF Or .udtChCommon.shtPin = &HFFFF Then
                    CHStr.INAdd = ""
                End If

                ''RL(ログ印字)
                CHStr.RL = IIf(gBitCheck(.udtChCommon.shtFlag2, 0), "o", "")    ''RL

                ''REMARKS
                CHStr.Remarks = gGetString(.udtChCommon.strRemark)

                ' 2015.10.16 ﾀｸﾞ表示向け　強制的に差し替え
                ' 2015.10.22 Ver1.7.5 標準時とﾀｸﾞ表示時にて分ける
                If gudt.SetSystem.udtSysOps.shtTagMode <> 0 Then     ' CH表示でない場合
                    CHStr.CHNo = GetTagNo(udtChannel)
                End If
                '//

                ' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ表示 ｱﾗｰﾑﾚﾍﾞﾙ取得
                If gudt.SetSystem.udtSysOps.shtLRMode <> 0 Then
                    CHStr.AlmLevel = GetAlmLevelName(udtChannel)
                End If
                '//

                ''Share Type ■Share対応
                If .udtChCommon.shtShareType = 1 Then
                    CHStr.ShareType = "L"
                ElseIf .udtChCommon.shtShareType = 2 Then
                    CHStr.ShareType = "R"
                ElseIf .udtChCommon.shtShareType = 3 Then
                    CHStr.ShareType = "S"
                Else
                    CHStr.ShareType = ""
                End If

                CHStr.ShareCHNo = gGet2Byte(.udtChCommon.shtShareChid).ToString("0000")




                'Ver2.0.0.7 Output設定
                'モーターとバルブは「o」しない。それ以外は出す
                Select Case .udtChCommon.shtChType  ''CH種別
                    Case gCstCodeChTypeMotor, gCstCodeChTypeValve
                    Case Else
                        'CHStr.OUT = fnGetOrAnd(.udtChCommon.shtChno)
                End Select


                ''☆CH種別毎---------------------------------------------------------------------------------------
                Select Case .udtChCommon.shtChType  ''CH種別

                    Case gCstCodeChTypeAnalog       ''アナログ
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeAnalogK
                                CHStr.INSIG = "K"
                            Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog2Jpt, _
                                 gCstCodeChDataTypeAnalog3Pt, gCstCodeChDataTypeAnalog3Jpt
                                CHStr.INSIG = "TR"
                            Case gCstCodeChDataTypeAnalog1_5v
                                CHStr.INSIG = "AI"
                            Case gCstCodeChDataTypeAnalog4_20mA
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If
                            Case gCstCodeChDataTypeAnalogPT4_20mA
                                CHStr.INSIG = "PT"
                            Case gCstCodeChDataTypeAnalogJacom
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeAnalogJacom55
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If

                            Case gCstCodeChDataTypeAnalogModbus
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeAnalogExhAve
                                CHStr.INSIG = "MT"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExhRepose
                                CHStr.INSIG = "RP"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExtDev
                                CHStr.INSIG = "DV"                      '' 2013.10.25 追加
                            Case Else
                                '緯度、経度はAIの通信とする。Rangeの頭にN/S,E/Wをつけることで判別
                                CHStr.INSIG = "AI"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''Decimal Position --------------------------------------------
                        intDecimalP = .AnalogDecimalPosition
                        strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                        ''Range -------------------------------------------------------
                        ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                        If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                            CHStr.Range = ""
                        Else
                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                ''Range Type(2,3線式)     2014.05.19
                                'dblLowValue = .AnalogRangeLow
                                'dblHiValue = .
                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)
                            Else
                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)

                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)

                                '' Ver1.10.1 2016.02.29 力率表示対応
                                '' Ver1.10.1.1 2016.03.02 比較時のかっこ漏れ
                                ''If (.udtChCommon.shtFlag1 And &H20) = &H20 Then
                                If gBitCheck(.udtChCommon.shtFlag1, 5) Then
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & nCenter.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                ElseIf gBitCheck(.udtChCommon.shtFlag1, 8) Then     '' Ver1.11.9.3 2016.11.26 P/S表示対応
                                    CHStr.Range = "P/S" & dblHiValue.ToString(strDecimalFormat)

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Then     '' Ver1.10.6 2016.06.06 緯度追加
                                    CHStr.Range = "N/S" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 緯度はN/S (E/Wになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする
                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Then    '' Ver1.10.6 2016.06.06 経度追加
                                    CHStr.Range = "E/W" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 経度はE/W (N/Sになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする

                                Else
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                End If

                            End If

                            'Ver2.0.0.4
                            'グリーンマーク(ノーマルレンジ)対応
                            '設定アリの場合、「G」を付ける
                            If (.AnalogNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .AnalogNormalHigh <> 0) Or _
                                (.AnalogNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .AnalogNormalLow <> 0) Then
                                'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない　場所は、Remarksの頭に追加とする（仮）
                                If g_bytGreenMarkPrint = 1 Then
                                    CHStr.Remarks = "G:" & CHStr.Remarks
                                End If
                            End If

                        End If

                        ''Value -------------------------------------------------------
                        If .AnalogHiHiUse = 0 Then      ''Use HH アラーム無し
                            CHStr.AlmInf(0).Value = ""
                        Else
                            dblValue = .AnalogHiHiValue / (10 ^ intDecimalP)    ''Value HH
                            CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogHiUse = 0 Then        ''Use H  アラーム無し
                            CHStr.AlmInf(1).Value = ""
                        Else
                            dblValue = .AnalogHiValue / (10 ^ intDecimalP)      ''Value H
                            CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoUse = 0 Then        ''Use L  アラーム無し
                            CHStr.AlmInf(2).Value = ""
                        Else
                            dblValue = .AnalogLoValue / (10 ^ intDecimalP)      ''Value L
                            CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoLoUse = 0 Then      ''Use LL アラーム無し
                            CHStr.AlmInf(3).Value = ""
                        Else
                            dblValue = .AnalogLoLoValue / (10 ^ intDecimalP)    ''Value LL
                            CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        'Ver2.0.0.0 2016.12.06 ｾﾝｻｰﾌｪｲﾙ対応
                        'NotUse=空白
                        'Useだがｾﾝｻｰﾌｪｲﾙ無し=N
                        'Under=U
                        'Over=O
                        'Under,Over両方=o
                        If .AnalogSensorFailUse = 0 Then
                            CHStr.AlmInf(4).Value = ""
                        Else
                            If gBitCheck(.AnalogDisplay3, 1) And gBitCheck(.AnalogDisplay3, 2) Then
                                '両方
                                CHStr.AlmInf(4).Value = "o"
                            Else
                                If gBitCheck(.AnalogDisplay3, 1) Then
                                    'Underのみ
                                    CHStr.AlmInf(4).Value = "UDR"
                                Else
                                    If gBitCheck(.AnalogDisplay3, 2) Then
                                        'Overのみ
                                        CHStr.AlmInf(4).Value = "OVR"
                                    Else
                                        'Useだが無し
                                        CHStr.AlmInf(4).Value = "NON"
                                    End If
                                End If
                            End If
                        End If


                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(0).ExtGrp = IIf(.AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiHiExtGroup)                ''EXT.G HH
                        CHStr.AlmInf(1).ExtGrp = IIf(.AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiExtGroup)                    ''EXT.G H
                        CHStr.AlmInf(2).ExtGrp = IIf(.AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoExtGroup)                    ''EXT.G L
                        CHStr.AlmInf(3).ExtGrp = IIf(.AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoLoExtGroup)                ''EXT.G LL
                        CHStr.AlmInf(4).ExtGrp = IIf(.AnalogSensorFailExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogSensorFailExtGroup)    ''EXT.G SF

                        ''G Repose 1 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep1 = IIf(.AnalogHiHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiHiGroupRepose1)   ''G.Rep1 HH
                        CHStr.AlmInf(1).GrpRep1 = IIf(.AnalogHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiGroupRepose1)       ''G.Rep1 H
                        CHStr.AlmInf(2).GrpRep1 = IIf(.AnalogLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoGroupRepose1)       ''G.Rep1 L
                        CHStr.AlmInf(3).GrpRep1 = IIf(.AnalogLoLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoLoGroupRepose1)   ''G.Rep1 LL

                        ''G Repose 2 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep2 = IIf(.AnalogHiHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiHiGroupRepose2)   ''G.Rep2 HH
                        CHStr.AlmInf(1).GrpRep2 = IIf(.AnalogHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiGroupRepose2)       ''G.Rep2 H
                        CHStr.AlmInf(2).GrpRep2 = IIf(.AnalogLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoGroupRepose2)       ''G.Rep2 L
                        CHStr.AlmInf(3).GrpRep2 = IIf(.AnalogLoLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoLoGroupRepose2)   ''G.Rep2 LL

                        ''Delay -------------------------------------------------------
                        CHStr.AlmInf(0).Delay = IIf(.AnalogHiHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiHiDelay)                     ''Delay HH
                        CHStr.AlmInf(1).Delay = IIf(.AnalogHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiDelay)                         ''Delay H
                        CHStr.AlmInf(2).Delay = IIf(.AnalogLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoDelay)                         ''Delay L
                        CHStr.AlmInf(3).Delay = IIf(.AnalogLoLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoLoDelay)                     ''Delay LL
                        CHStr.AlmInf(4).Delay = IIf(.AnalogSensorFailDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogSensorFailDelay)         ''Delay SF

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            For i As Integer = 0 To 4
                                If CHStr.AlmInf(i).Delay <> "" Then
                                    CHStr.AlmInf(i).Delay += strTemp
                                End If
                            Next
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                            '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                If .udtChCommon.shtStatus = &H43 Then     '' LOW/NOR/HIGHならば差し替え
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        CHStr.Status = "正常/高"
                                    Else
                                        CHStr.Status = "NOR/HIGH"
                                    End If
                                End If
                            End If
                        Else
                            strHH = gGetString(.AnalogHiHiStatusInput)     ''特殊コード対応
                            strH = gGetString(.AnalogHiStatusInput)        ''特殊コード対応
                            strL = gGetString(.AnalogLoStatusInput)        ''特殊コード対応
                            strLL = gGetString(.AnalogLoLoStatusInput)     ''特殊コード対応

                            '' 2015.11.18  Ver1.8.1  ｽﾃｰﾀｽは未定の場合もあるので、表示方法変更
                            If LenB(strHH) = 0 And LenB(strH) = 0 And LenB(strL) = 0 And LenB(strLL) = 0 Then
                                strTemp = ""
                            Else
                                '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                    '' Ver1.9.8 2016.02.20 ｽﾃｰﾀｽﾁｪｯｸ方法変更
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "正常/" & strL
                                            Else
                                                strTemp = "正常/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "正常/" & strL & "/" & strLL
                                            Else
                                                strTemp = "正常/" & strH & "/" & strHH
                                            End If
                                        End If
                                    Else
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "NOR/" & strL
                                            Else
                                                strTemp = "NOR/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "NOR/" & strL & "/" & strLL
                                            Else
                                                strTemp = "NOR/" & strH & "/" & strHH
                                            End If
                                        End If
                                    End If
                                Else
                                    strTemp = ""    '' Ver1.11.5 2016.09.06  初期化追加

                                    '' Ver1.8.6.2  2015.12.04  ﾌﾗｸﾞは参照せずにｽﾃｰﾀｽが設定されていれば表示する
                                    ''                          設定値が決まっていなくてもｽﾃｰﾀｽのみ決まっていることがあるので
                                    ''If .AnalogLoLoUse = 1 And LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                    If LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                        strTemp += strLL & "/"
                                    Else
                                        strTemp = ""
                                    End If

                                    ''If .AnalogLoUse = 1 And LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                    If LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                        strTemp += strL & "/"
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        strTemp += "正常/"
                                    Else
                                        strTemp += "NOR/"
                                    End If

                                    ''If .AnalogHiUse = 1 And LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                    If LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                        strTemp += strH & "/"
                                    End If

                                    ''If .AnalogHiHiUse = 1 And LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                    If LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                        strTemp += strHH
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    'If strTemp = "NOR/" Then    '' NORのみならばｽﾃｰﾀｽ未定とする
                                    If strTemp = "NOR/" Or strTemp = "正常/" Then    'NORのみならばｽﾃｰﾀｽ未定とする
                                        strTemp = ""
                                    Else
                                        '' 文字列の最後尾ならば"/"を削除する
                                        nLen = LenB(strTemp)
                                        'Ver2.0.7.L
                                        'If strTemp.Substring(nLen - 1) = "/" Then
                                        If MidB(strTemp, nLen - 1) = "/" Then
                                            'strTemp = strTemp.Remove(nLen - 1)
                                            strTemp = MidB(strTemp, 0, nLen - 1)
                                        End If
                                    End If
                                End If


                            End If

                            CHStr.Status = strTemp

                        End If

                        If .AnalogHiHiUse = 1 Or .AnalogHiUse = 1 Or .AnalogLoUse = 1 Or .AnalogLoLoUse = 1 Or .AnalogSensorFailUse = 1 Then
                            '排ガスリポーズはアラームではないので除外（フラグは必要)   2013.07.25 K.Fujimoto
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                CHStr.AL = ""
                            Else
                                CHStr.AL = "o"
                            End If
                        Else
                            CHStr.AL = ""
                        End If

                    Case gCstCodeChTypeDigital      ''デジタル
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeDigitalNC
                                CHStr.INSIG = "NC"
                            Case gCstCodeChDataTypeDigitalNO
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalJacomNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacom55NC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacomNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacom55NO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If

                            Case gCstCodeChDataTypeDigitalModbusNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalModbusNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalExt
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalDeviceStatus
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "S"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                        Else
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            'Ver2.0.7.L
                            'If strTemp.Length > 8 Then
                            If LenB(strTemp) > 8 Then
                                'CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                                CHStr.Status = MidB(strTemp, 0, 8).Trim & "/" & MidB(strTemp, 8).Trim
                            Else
                                CHStr.Status = Trim(strTemp)
                            End If

                        End If

                        If .DigitalUse = 1 Then
                            CHStr.AL = "o"
                        Else
                            CHStr.AL = ""
                        End If

                    Case gCstCodeChTypeMotor        ''モーター
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        If .udtChCommon.shtData >= gCstCodeChDataTypeMotorManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M1"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M2"


                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK Then
                            CHStr.INSIG = "M1"
                            CHStr.SIGType = "R"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRAbnorRunK Then
                            CHStr.INSIG = "M2"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END


                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Then
                            CHStr.INSIG = "MO"

                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM-"
                            Else
                                CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                            End If

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM55-"
                            Else
                                CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                            End If
                        End If

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            ' 2013.07.22 MO表示変更  K.Fujimoto
                            If .udtChCommon.shtStatus = 20 Then     ' MO
                                CHStr.Status = "RUN"
                            Else
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                CHStr.Status = cmbStatus.Text
                            End If

                        Else
                            CHStr.Status = ""
                        End If

                        'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                        'If .MotorUse = 1 Then
                        If .MotorUse = 1 Or .MotorAlarmUse = 1 Then
                            CHStr.AL = "o"
                        Else
                            CHStr.AL = ""
                        End If

                        ''FU ADDRESS(OUT)
                        'CHStr.OUTAdd = mPrtConvFuAddress(.MotorFuNo, .MotorPortNo, .MotorPin)

                        If .MotorAlarmUse = 1 Then  ''フィードバックアラーム有り

                            '2015/4/23 T.Ueki Feedback タイマー msec→secに表示
                            CHStr.AlmInf(9).Value = Val(.MotorFeedback) / 10
                            'CHStr.AlmInf(9).Value = .MotorFeedback
                            CHStr.AlmInf(9).ExtGrp = IIf(.MotorAlarmExtGroup = gCstCodeChMotorExtGroupNothing, "", .MotorAlarmExtGroup)                 ''EXT.G
                            CHStr.AlmInf(9).GrpRep1 = IIf(.MotorAlarmGroupRepose1 = gCstCodeChMotorGroupRepose1Nothing, "", .MotorAlarmGroupRepose1)    ''G.Rep1
                            CHStr.AlmInf(9).GrpRep2 = IIf(.MotorAlarmGroupRepose2 = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmGroupRepose2)    ''G.Rep2
                            CHStr.AlmInf(9).Delay = IIf(.MotorAlarmDelay = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmDelay)                    ''DELAY
                        End If

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                            If CHStr.AlmInf(9).Delay <> "" Then
                                CHStr.AlmInf(9).Delay += strTemp
                            End If
                        End If

                        'Ver2.0.7.2
                        'モーターは、OutFuAdrが設定されていないとOUTSigは出さない
                        If CHStr.OUTAdd <> "" Then
                            If .MotorControl = 1 Then
                                CHStr.OUTSIG = "DOP"
                            Else
                                CHStr.OUTSIG = "DOM"
                            End If
                        End If
                        '-

                    Case gCstCodeChTypeValve        ''バルブ
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""

                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeValveDI_DO   ''DI/DO
                                CHStr.INSIG = "DC3"

                                ''デジタルコンポジット設定テーブルインデックス ----------------
                                intCompIndex = .ValveCompositeTableIndex
                                CHStr.Status = ""
                                intStatusExist = 0
                                CHStr.AL = ""

                                '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得処理追加
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text
                                    intStatusExist = -1
                                End If

                                With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                    For i As Integer = 0 To 8
                                        ''EXT Group ---------------------------------------------------
                                        CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                        ''G Repose 1 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                        ''G Repose 2 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                        ''Delay --------------------------------------------------------
                                        CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                        ''Status ------------------------------------------------------
                                        If intStatusExist <> -1 Then        '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得済
                                            If intStatusExist = 1 Then
                                                CHStr.Status += "/"
                                            End If
                                            If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                                CHStr.Status += .udtCompInf(i).strStatusName
                                                intStatusExist = 1
                                            Else
                                                CHStr.Status += ""
                                            End If
                                        End If

                                        If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                            CHStr.AL = "o"
                                        End If
                                    Next

                                End With

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)

                                If .ValveDiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    '2015/4/23 T.Ueki Feedback タイマー msec→secに表示 
                                    CHStr.AlmInf(9).Value = Val(.ValveDiDoFeedback) / 10
                                    'CHStr.AlmInf(9).Value = .ValveDiDoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveDiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveDiDoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveDiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveDiDoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveDiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveDiDoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveDiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveDiDoAlarmDelay)                    ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 9
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                End If

                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveDiDoControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If

                                CHStr.TermCount = .udtChCommon.shtPinNo       '' 端子数  Ver1.11.9.3 2016.11.26 追加

                            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If

                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiDoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiDoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiDoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.0.4
                                    'グリーンマーク(ノーマルレンジ)対応
                                    '設定アリの場合、「G」を付ける
                                    If (.ValveAiDoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiDoNormalHigh <> 0) Or _
                                        (.ValveAiDoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiDoNormalLow <> 0) Then
                                        'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                        If g_bytGreenMarkPrint = 1 Then
                                            CHStr.Range = "G " & CHStr.Range
                                        End If
                                    End If

                                End If


                                ''Value -------------------------------------------------------
                                If .ValveAiDoHiHiUse = 0 Then      ''Use HH アラーム無し
                                    CHStr.AlmInf(0).Value = ""
                                Else
                                    dblValue = .ValveAiDoHiHiValue / (10 ^ intDecimalP)    ''Value HH
                                    CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoHiUse = 0 Then        ''Use H  アラーム無し
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    dblValue = .ValveAiDoHiValue / (10 ^ intDecimalP)      ''Value H
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoLoUse = 0 Then        ''Use L  アラーム無し
                                    CHStr.AlmInf(2).Value = ""
                                Else
                                    dblValue = .ValveAiDoLoValue / (10 ^ intDecimalP)      ''Value L
                                    CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoLoLoUse = 0 Then      ''Use LL アラーム無し
                                    CHStr.AlmInf(3).Value = ""
                                Else
                                    dblValue = .ValveAiDoLoLoValue / (10 ^ intDecimalP)    ''Value LL
                                    CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoSensorFailUse = 0 Then
                                    CHStr.AlmInf(4).Value = ""
                                Else
                                    CHStr.AlmInf(4).Value = "o"
                                End If


                                ''EXT Group ---------------------------------------------------
                                CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiDoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiHiExtGroup)                ''EXT.G HH
                                CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiDoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiExtGroup)                    ''EXT.G H
                                CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiDoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoExtGroup)                    ''EXT.G L
                                CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiDoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoLoExtGroup)                ''EXT.G LL
                                CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiDoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoSensorFailExtGroup)    ''EXT.G SF

                                ''G Repose 1 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiDoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiHiGroupRepose1)   ''G.Rep1 HH
                                CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiDoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiGroupRepose1)       ''G.Rep1 H
                                CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiDoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoGroupRepose1)       ''G.Rep1 L
                                CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiDoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoLoGroupRepose1)   ''G.Rep1 LL

                                ''G Repose 2 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiDoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiHiGroupRepose2)   ''G.Rep2 HH
                                CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiDoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiGroupRepose2)       ''G.Rep2 H
                                CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiDoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoGroupRepose2)       ''G.Rep2 L
                                CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiDoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoLoGroupRepose2)   ''G.Rep2 LL

                                ''Delay -------------------------------------------------------
                                CHStr.AlmInf(0).Delay = IIf(.ValveAiDoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiHiDelay)                     ''Delay HH
                                CHStr.AlmInf(1).Delay = IIf(.ValveAiDoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiDelay)                         ''Delay H
                                CHStr.AlmInf(2).Delay = IIf(.ValveAiDoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoDelay)                         ''Delay L
                                CHStr.AlmInf(3).Delay = IIf(.ValveAiDoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoLoDelay)                     ''Delay LL
                                CHStr.AlmInf(4).Delay = IIf(.ValveAiDoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoSensorFailDelay)         ''Delay SF

                                ''Status -----------------------------------------------------
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text

                                Else
                                    strHH = gGetString(.ValveAiDoHiHiStatusInput)     ''特殊コード対応
                                    strH = gGetString(.ValveAiDoHiStatusInput)        ''特殊コード対応
                                    strL = gGetString(.ValveAiDoLoStatusInput)        ''特殊コード対応
                                    strLL = gGetString(.ValveAiDoLoLoStatusInput)     ''特殊コード対応

                                    ''2015.03.12 HIGH,LOWの並び順を変更
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        strTemp = ""
                                    Else
                                        'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp = "正常/"
                                        Else
                                            strTemp = "NOR/"
                                        End If
                                    End If

                                    ''LL/Lステータス
                                    ''HIGH,LOWの両ステータスがある場合はLL/L、LOWのみはL/LL
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        If .AnalogLoLoUse = 1 Then
                                            strTemp += strLL
                                        End If
                                        If .AnalogLoUse = 1 Then
                                            If .AnalogLoLoUse = 1 Then
                                                strTemp += "/" & strL
                                            Else
                                                strTemp += strL
                                            End If
                                        End If
                                    Else
                                        If .AnalogLoUse = 1 Then
                                            strTemp += strL
                                        End If
                                        If .AnalogLoLoUse = 1 Then
                                            If .AnalogLoUse = 1 Then
                                                strTemp += "/" & strLL
                                            Else
                                                strTemp += strLL
                                            End If
                                        End If
                                    End If

                                    ''HIGH,LOWの両ステータスがある場合は中間に"NOR"
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "/正常/"
                                        Else
                                            strTemp += "/NOR/"
                                        End If
                                    End If

                                    ''H/HHステータス
                                    If .AnalogHiUse = 1 Then
                                        strTemp += strH
                                    End If
                                    If .AnalogHiHiUse = 1 Then
                                        If .AnalogHiUse = 1 Then
                                            strTemp += "/" & strHH
                                        Else
                                            strTemp += strHH
                                        End If
                                    End If

                                    CHStr.Status = strTemp

                                End If

                                If .ValveAiDoHiHiUse = 1 Or .ValveAiDoHiUse = 1 Or .ValveAiDoLoUse = 1 Or .ValveAiDoLoLoUse = 1 Or .ValveAiDoSensorFailUse = 1 Then
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AL = ""
                                End If

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiDoFuNo, .ValveAiDoPortNo, .ValveAiDoPin)

                                If .ValveAiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    CHStr.AlmInf(9).Value = Val(.ValveAiDoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                    'CHStr.AlmInf(9).Value = .ValveAiDoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveAiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiDoAlarmDelay)                      ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 4
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                    If CHStr.AlmInf(9).Delay <> "" Then
                                        CHStr.AlmInf(9).Delay += strTemp
                                    End If
                                End If

                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveAiDoOutControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If


                            Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2    ''AI/AO
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If

                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiAoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.0.4
                                    'グリーンマーク(ノーマルレンジ)対応
                                    '設定アリの場合、「G」を付ける
                                    If (.ValveAiAoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiAoNormalHigh <> 0) Or _
                                        (.ValveAiAoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiAoNormalLow <> 0) Then
                                        'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                        If g_bytGreenMarkPrint = 1 Then
                                            CHStr.Range = "G " & CHStr.Range
                                        End If
                                    End If
                                End If

                                ''Value -------------------------------------------------------
                                If .ValveAiAoHiHiUse = 0 Then      ''Use HH アラーム無し
                                    CHStr.AlmInf(0).Value = ""
                                Else
                                    dblValue = .ValveAiAoHiHiUse / (10 ^ intDecimalP)    ''Value HH
                                    CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoHiUse = 0 Then        ''Use H  アラーム無し
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    dblValue = .ValveAiAoHiUse / (10 ^ intDecimalP)      ''Value H
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoLoUse = 0 Then        ''Use L  アラーム無し
                                    CHStr.AlmInf(2).Value = ""
                                Else
                                    dblValue = .ValveAiAoLoUse / (10 ^ intDecimalP)      ''Value L
                                    CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoLoLoUse = 0 Then      ''Use LL アラーム無し
                                    CHStr.AlmInf(3).Value = ""
                                Else
                                    dblValue = .ValveAiAoLoLoUse / (10 ^ intDecimalP)    ''Value LL
                                    CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoSensorFailUse = 0 Then
                                    CHStr.AlmInf(4).Value = ""
                                Else
                                    CHStr.AlmInf(4).Value = "o"
                                End If


                                ''EXT Group ---------------------------------------------------
                                CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiAoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiHiExtGroup)                ''EXT.G HH
                                CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiAoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiExtGroup)                    ''EXT.G H
                                CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiAoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoExtGroup)                    ''EXT.G L
                                CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiAoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoLoExtGroup)                ''EXT.G LL
                                CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiAoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoSensorFailExtGroup)    ''EXT.G SF

                                ''G Repose 1 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiAoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiHiGroupRepose1)   ''G.Rep1 HH
                                CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiAoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiGroupRepose1)       ''G.Rep1 H
                                CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiAoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoGroupRepose1)       ''G.Rep1 L
                                CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiAoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoLoGroupRepose1)   ''G.Rep1 LL

                                ''G Repose 2 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiAoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiHiGroupRepose2)   ''G.Rep2 HH
                                CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiAoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiGroupRepose2)       ''G.Rep2 H
                                CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiAoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoGroupRepose2)       ''G.Rep2 L
                                CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiAoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoLoGroupRepose2)   ''G.Rep2 LL

                                ''Delay -------------------------------------------------------
                                CHStr.AlmInf(0).Delay = IIf(.ValveAiAoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiHiDelay)                     ''Delay HH
                                CHStr.AlmInf(1).Delay = IIf(.ValveAiAoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiDelay)                         ''Delay H
                                CHStr.AlmInf(2).Delay = IIf(.ValveAiAoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoDelay)                         ''Delay L
                                CHStr.AlmInf(3).Delay = IIf(.ValveAiAoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoLoDelay)                     ''Delay LL
                                CHStr.AlmInf(4).Delay = IIf(.ValveAiAoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoSensorFailDelay)         ''Delay SF

                                ''Status -----------------------------------------------------
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text

                                Else
                                    strHH = gGetString(.ValveAiAoHiHiStatusInput)     ''特殊コード対応
                                    strH = gGetString(.ValveAiAoHiStatusInput)        ''特殊コード対応
                                    strL = gGetString(.ValveAiAoLoStatusInput)        ''特殊コード対応
                                    strLL = gGetString(.ValveAiAoLoLoStatusInput)     ''特殊コード対応


                                    If .ValveAiAoHiHiUse = 1 Then
                                        strTemp = strHH
                                        intLen = strTemp.Length
                                    End If
                                    If .ValveAiAoHiUse = 1 Then
                                        If .ValveAiAoHiHiUse = 1 Then
                                            strTemp += "/" & strH
                                        Else
                                            strTemp += strH
                                        End If
                                        intLen += strTemp.Length
                                    End If
                                    If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Then
                                        'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "/正常"
                                        Else
                                            strTemp += "/NOR"
                                        End If
                                    Else
                                        'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "正常"
                                        Else
                                            strTemp += "NOR"
                                        End If
                                    End If
                                    If .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Then
                                        strTemp += "/"
                                    End If
                                    If .ValveAiAoLoUse = 1 Then
                                        strTemp += strL
                                    End If
                                    If .ValveAiAoLoLoUse = 1 Then
                                        If .ValveAiAoLoUse = 1 Then
                                            strTemp += "/" & strLL
                                        Else
                                            strTemp += strLL
                                        End If
                                    End If

                                    CHStr.Status = strTemp

                                End If

                                If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Or .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Or .ValveAiAoSensorFailUse = 1 Then
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AL = ""
                                End If

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)
                                If .ValveAiAoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    CHStr.AlmInf(9).Value = Val(.ValveAiAoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                    'CHStr.AlmInf(9).Value = .ValveAiAoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 4
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                    If CHStr.AlmInf(9).Delay <> "" Then
                                        CHStr.AlmInf(9).Delay += strTemp
                                    End If
                                End If

                                If CHStr.OUTAdd <> "" Then
                                    CHStr.OUTSIG = "AO"
                                End If

                            Case gCstCodeChDataTypeValveAO_4_20 ''AO
                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiAoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                End If

                                ''FU ADDRESS(OUT)
                                CHStr.OUTAdd = gConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)

                                'CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                'CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                'CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                'CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY

                                If CHStr.OUTAdd <> "" Then
                                    CHStr.OUTSIG = "AO"
                                End If


                            Case gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt      ''DO

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)


                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveDiDoControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Then
                                    CHStr.SIGType = "J"
                                    If .ValveDiDoPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                        CHStr.OUTAdd = "JACOM-"
                                    Else
                                        CHStr.OUTAdd = "JACOM-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                    End If

                                End If

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then
                                    CHStr.SIGType = "J"
                                    If .ValveDiDoPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                        CHStr.OUTAdd = "JACOM55-"
                                    Else
                                        CHStr.OUTAdd = "JACOM55-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                    End If

                                End If

                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If



                    Case gCstCodeChTypeComposite    ''コンポジット
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        If .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevel Then              '' 代表ステータス
                            CHStr.INSIG = "DC1"

                            ''EXT Group ---------------------------------------------------
                            CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                            ''G Repose 1 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                            ''G Repose 2 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                            ''Delay --------------------------------------------------------
                            CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                            ''Status ------------------------------------------------------
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            'Ver2.0.7.L
                            'If strTemp.Length > 8 Then
                            If LenB(strTemp) > 8 Then
                                'CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                                CHStr.Status = MidB(strTemp, 0, 8).Trim & "/" & MidB(strTemp, 8).Trim
                            Else
                                CHStr.Status = Trim(strTemp)
                            End If

                            ''デジタルコンポジット設定テーブルインデックス ----------------
                            intCompIndex = .CompositeTableIndex
                            CHStr.AL = ""

                            With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                For i As Integer = 0 To 8
                                    If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                        CHStr.AL = "o"
                                    End If
                                Next

                            End With

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevelIndevi Then    '' 個別ステータス
                            CHStr.INSIG = "DC2"

                            ''デジタルコンポジット設定テーブルインデックス ----------------
                            intCompIndex = .CompositeTableIndex
                            CHStr.Status = ""
                            intStatusExist = 0
                            CHStr.AL = ""

                            With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                For i As Integer = 0 To 8
                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                    ''G Repose 1 ---------------------------------------------------
                                    CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                    ''G Repose 2 ---------------------------------------------------
                                    CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                    ''Delay --------------------------------------------------------
                                    CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                    ''Status ------------------------------------------------------
                                    If intStatusExist = 1 Then
                                        CHStr.Status += "/"
                                    End If
                                    If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                        CHStr.Status += .udtCompInf(i).strStatusName
                                        intStatusExist = 1
                                    Else
                                        CHStr.Status += ""
                                    End If

                                    If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                        CHStr.AL = "o"
                                    End If
                                Next

                            End With

                        End If

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            For i As Integer = 0 To 8
                                If CHStr.AlmInf(i).Delay <> "" Then
                                    CHStr.AlmInf(i).Delay += strTemp
                                End If
                            Next
                        End If


                    Case gCstCodeChTypePulse        ''パルス
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypePulseTotal1_1
                                CHStr.INSIG = "PU"
                            Case gCstCodeChDataTypePulseTotal1_10
                                CHStr.INSIG = "P1"
                            Case gCstCodeChDataTypePulseTotal1_100
                                CHStr.INSIG = "P2"
                            Case gCstCodeChDataTypePulseDay1_1
                                CHStr.INSIG = "PUD"
                            Case gCstCodeChDataTypePulseDay1_10
                                CHStr.INSIG = "P1D"
                            Case gCstCodeChDataTypePulseDay1_100
                                CHStr.INSIG = "P2D"
                            Case gCstCodeChDataTypePulseRevoTotalHour
                                CHStr.INSIG = "RH"
                            Case gCstCodeChDataTypePulseRevoTotalMin
                                CHStr.INSIG = "R2"
                            Case gCstCodeChDataTypePulseRevoDayHour
                                CHStr.INSIG = "RHD"
                            Case gCstCodeChDataTypePulseRevoDayMin
                                CHStr.INSIG = "R2D"
                            Case gCstCodeChDataTypePulseRevoLapHour
                                CHStr.INSIG = "RHL"
                            Case gCstCodeChDataTypePulseRevoLapMin
                                CHStr.INSIG = "R2L"
                            Case gCstCodeChDataTypePulseExtDev
                                CHStr.INSIG = "PU"      '' Ver1.11.8.5 2016.11.10 "RH" → "PU"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDev   '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                                'Ver2.0.1.9 CHG
                                'CHStr.INSIG = "RH"
                                CHStr.INSIG = "R2"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevTotalMin    '' Ver1.12.0.1 2017.01.13 
                                'CHStr.INSIG = "R2"
                                'CHStr.INSIG = "R2T" 'Ver2.0.5.9 ↑とかぶるため、打開策としてT付
                                CHStr.INSIG = "R2"  'Ver2.0.6.0 正しくは、「R2」である。上が違う
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevDayHour     '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "RHD"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevDayMin      '' Ver1.12.0.1 2017.01.13
                                CHStr.INSIG = "R2D"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevLapHour     '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "RHL"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevLapMin      '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "R2L"
                                CHStr.SIGType = "R"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text
                        Else
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            If strTemp.Length > 8 Then
                                'Ver2.0.7.M (保安庁)
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    CHStr.Status = "正常/" & strTemp.Substring(0, 8).Trim
                                Else
                                    CHStr.Status = "NOR/" & strTemp.Substring(0, 8).Trim
                                End If
                            Else
                                'Ver2.0.7.M (保安庁)
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    CHStr.Status = "正常/" & Trim(strTemp)
                                Else
                                    CHStr.Status = "NOR/" & Trim(strTemp)
                                End If
                            End If

                        End If

                        If .udtChCommon.shtData >= gCstCodeChDataTypePulseTotal1_1 And .udtChCommon.shtData <= gCstCodeChDataTypePulseDay1_100 Or _
                           .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                            ''Decimal Position --------------------------------------------
                            intDecimalP = .PulseDecPoint
                            If intDecimalP = 0 Then
                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                CHStr.Range = "99999999"    '"9999999" '"99999999"
                                strDecimalFormat = ""
                            Else
                                If intDecimalP <= 6 Then
                                    CHStr.Range = ".".PadRight(intDecimalP + 1, "9"c)
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                                Else
                                    CHStr.Range = ".".PadRight(7, "9"c)
                                    strDecimalFormat = "0.".PadRight(8, "0"c)
                                End If
                            End If

                            CHStr.Range = CHStr.Range.PadLeft(8, "9"c)

                            If .PulseUse = 1 Then
                                dblValue = .PulseValue / (10 ^ intDecimalP)      ''Value H
                                CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                CHStr.AL = "o"
                            Else
                                CHStr.AlmInf(1).Value = ""
                                CHStr.AL = ""
                            End If
                        Else
                            ''Decimal Position --------------------------------------------
                            intDecimalP = .RevoDecPoint
                            If intDecimalP = 0 Then
                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                CHStr.Range = "99999999"    '"9999999" '"99999999"
                                strDecimalFormat = ""
                            Else
                                CHStr.Range = "99999.59"
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                            End If

                            If .RevoUse = 1 Then        ''Value H
                                '' Ver1.9.1 2015.12.22  積算CHのｱﾗｰﾑ設定値の印字が異なる不具合修正
                                If intDecimalP = 2 Then     '' 時分
                                    dblValue = ((.RevoValue And &HFFFFFF00) >> 8)                       ''時
                                    dblValue = dblValue + (.RevoValue And &HFF) / (10 ^ intDecimalP)    ''時 + 分(小数点以下値)
                                Else
                                    dblValue = .RevoValue
                                End If

                                If dblValue = 0 Then
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If
                                ''//
                                ''dblValue = .RevoValue / (10 ^ intDecimalP)      ''Value H
                                ''CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                CHStr.AL = "o"
                            Else
                                CHStr.AlmInf(1).Value = ""
                                CHStr.AL = ""
                            End If
                        End If

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

        End Try

    End Sub
    '--------------------------------------------------------------------
    ' 機能      : 文字列取得
    ' 返り値    : 変換後文字列
    ' 引き数    : ARG1 - (I ) 変換元文字列
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    '--------------------------------------------------------------------
    Private Function mGetString(ByVal strInput As String, _
                      Optional ByVal blnTrim As Boolean = True) As String

        Try

            Dim strRtn As String

            strRtn = strInput
            strRtn = Replace(strRtn, vbNullChar, "")

            If blnTrim Then
                'strRtn = Trim(strRtn)
                If strRtn <> Nothing Then
                    strRtn = strRtn.TrimEnd
                Else
                    strRtn = ""
                End If
            End If

            Return strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return strInput
        End Try

    End Function
#End Region

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG2 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsTrendGraph, _
                               ByRef udtTarget As gTypSetOpsTrendGraph)

        Try
            Dim i As Integer = 0
            Dim j As Integer = 0

            For i = 0 To UBound(udtTarget.udtTrendGraphRec) Step 1
                udtTarget.udtTrendGraphRec(i).bytNo = udtSource.udtTrendGraphRec(i).bytNo
                udtTarget.udtTrendGraphRec(i).bytOps = udtSource.udtTrendGraphRec(i).bytOps
                udtTarget.udtTrendGraphRec(i).bytSnpTime = udtSource.udtTrendGraphRec(i).bytSnpTime
                udtTarget.udtTrendGraphRec(i).bytSnpType = udtSource.udtTrendGraphRec(i).bytSnpType
                udtTarget.udtTrendGraphRec(i).bytSpare = udtSource.udtTrendGraphRec(i).bytSpare
                udtTarget.udtTrendGraphRec(i).shtDelay = udtSource.udtTrendGraphRec(i).shtDelay
                udtTarget.udtTrendGraphRec(i).shtTrgChno = udtSource.udtTrendGraphRec(i).shtTrgChno
                udtTarget.udtTrendGraphRec(i).shtTrgSelect = udtSource.udtTrendGraphRec(i).shtTrgSelect
                udtTarget.udtTrendGraphRec(i).shtTrgSet = udtSource.udtTrendGraphRec(i).shtTrgSet
                udtTarget.udtTrendGraphRec(i).shtTrgUse = udtSource.udtTrendGraphRec(i).shtTrgUse
                udtTarget.udtTrendGraphRec(i).shtTrgValue = udtSource.udtTrendGraphRec(i).shtTrgValue
                udtTarget.udtTrendGraphRec(i).strPageTitle = udtSource.udtTrendGraphRec(i).strPageTitle

                ''---------------------
                '' 詳細設定
                ''---------------------
                For j = 0 To UBound(udtTarget.udtTrendGraphRec(i).udtTrendGraphRecChno) Step 1
                    udtTarget.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno = udtSource.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno
                    udtTarget.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask = udtSource.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask
                Next j
            Next i

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsTrendGraph, _
                                         ByVal udt2 As gTypSetOpsTrendGraph) As Boolean

        Try
            Dim i As Integer = 0
            Dim j As Integer = 0

            For i = 0 To UBound(udt1.udtTrendGraphRec) Step 1
                If udt1.udtTrendGraphRec(i).bytNo <> udt2.udtTrendGraphRec(i).bytNo Then Return False
                If udt1.udtTrendGraphRec(i).bytOps <> udt2.udtTrendGraphRec(i).bytOps Then Return False
                If udt1.udtTrendGraphRec(i).bytSnpTime <> udt2.udtTrendGraphRec(i).bytSnpTime Then Return False
                If udt1.udtTrendGraphRec(i).bytSnpType <> udt2.udtTrendGraphRec(i).bytSnpType Then Return False
                If udt1.udtTrendGraphRec(i).bytSpare <> udt2.udtTrendGraphRec(i).bytSpare Then Return False
                If udt1.udtTrendGraphRec(i).shtDelay <> udt2.udtTrendGraphRec(i).shtDelay Then Return False
                If udt1.udtTrendGraphRec(i).shtTrgChno <> udt2.udtTrendGraphRec(i).shtTrgChno Then Return False
                If udt1.udtTrendGraphRec(i).shtTrgSelect <> udt2.udtTrendGraphRec(i).shtTrgSelect Then Return False
                If udt1.udtTrendGraphRec(i).shtTrgSet <> udt2.udtTrendGraphRec(i).shtTrgSet Then Return False
                If udt1.udtTrendGraphRec(i).shtTrgUse <> udt2.udtTrendGraphRec(i).shtTrgUse Then Return False
                If udt1.udtTrendGraphRec(i).shtTrgValue <> udt2.udtTrendGraphRec(i).shtTrgValue Then Return False
                If udt1.udtTrendGraphRec(i).strPageTitle <> udt2.udtTrendGraphRec(i).strPageTitle Then Return False

                ''---------------------
                '' 詳細設定
                ''---------------------
                For j = 0 To UBound(udt1.udtTrendGraphRec(i).udtTrendGraphRecChno) Step 1
                    If udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno <> _
                        udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno Then Return False
                    If udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask <> _
                        udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask Then Return False
                Next j
            Next i


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
