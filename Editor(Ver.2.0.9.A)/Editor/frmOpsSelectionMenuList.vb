Public Class frmOpsSelectionMenuList

#Region "構造体定義"

    Friend Structure gTypFunctionSet
        Dim intScreenNo As Integer
        Dim intFunctionNo() As Integer
    End Structure

#End Region

#Region "変数定義"

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    ''構造体定義
    Private mudtSetOpsSelectionMenuWork As gTypSetOpsSelectionMenuEdit = Nothing
    Private mudtSetOpsSelectionMenuNewMach As gTypSetOpsSelectionMenuEdit = Nothing
    Private mudtSetOpsSelectionMenuNewCarg As gTypSetOpsSelectionMenuEdit = Nothing
    Private mudtSetOpsSelectionMenuDefault As gTypSetOpsSelectionMenu = Nothing

    ''画面名称構造体
    Private mudtIniScreenTitle() As gTypCodeName = Nothing

    ''ボタン機能構造体
    Private mudtIniFunctionList() As gTypCodeName = Nothing

    ''ボタン機能構造体
    Private mstrIniSelectionDisableList() As String = Nothing

    ''設定可能ボタンリスト
    Private mudtIniFunctionSet() As gTypFunctionSet

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
    Private Sub frmOpsSelectionMenuList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim strLine() As String = Nothing
            Dim SelectNameLen As Integer
            Dim OneName As String
            Dim strLineName As String
            Dim j As Integer

            ''初期化情報取得成功した場合
            If mGetIniInfo() = 0 Then

                ''初期化フラグ
                mblnInitFlg = True

                ''グリッドの初期化
                Call mInitialDataGrid()

                ''配列再定義
                Call mudtSetOpsSelectionMenuWork.InitArray()
                Call mudtSetOpsSelectionMenuNewMach.InitArray()
                Call mudtSetOpsSelectionMenuNewCarg.InitArray()
                For i As Integer = 0 To UBound(gudt.SetOpsSelectionMenuM.udtOpsSelectionOffSetRec)
                    Call mudtSetOpsSelectionMenuWork.udtOpsSelectionSetViewRecEdit(i).InitArray()
                    Call mudtSetOpsSelectionMenuNewMach.udtOpsSelectionSetViewRecEdit(i).InitArray()
                    Call mudtSetOpsSelectionMenuNewCarg.udtOpsSelectionSetViewRecEdit(i).InitArray()
                Next


                    ''Machinery/Cargoの情報を取得する
                    Call mCopyStructure2(gudt.SetOpsSelectionMenuM, mudtSetOpsSelectionMenuNewMach)
                    Call mCopyStructure2(gudt.SetOpsSelectionMenuC, mudtSetOpsSelectionMenuNewCarg)

                ''コンバイン切替をOPS/Ext.VDU切替に変更  2014.03.12
                ''Machinery/Cargoボタン設定
                'Call gSetCombineControl(optMachinery, optCargo)
                optOPS.Checked = True     ''OPS選択
                optOPS.Enabled = True     ''OPSボタン有効
                optVDU.Enabled = True     ''Ext.VDUボタン有効

                    ''画面設定
                If optOPS.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuNewMach, mudtSetOpsSelectionMenuWork)
                If optVDU.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuNewCarg, mudtSetOpsSelectionMenuWork)

                'T.Ueki セレクトメニュー名称iniファイルより参照する処理追加
                ''ボタン名称設定
                For i = 0 To mudtSetOpsSelectionMenuWork.udtOpsSelectionMenuNameKeyRecEdit.Length - 1
                    With mudtSetOpsSelectionMenuWork.udtOpsSelectionMenuNameKeyRecEdit(i)
                        .EditSelectMenuKeyName = ""       ''メインメニュー名称        
                    End With
                Next

                ''iniファイルからボタン名称取得
                If gGetIniFileLine(strLine, gEnmComboType.ctOpsSelectionFunctionList) = 0 Then
                    For i As Integer = 0 To UBound(strLine)

                        '初期化
                        strLineName = ""
                        OneName = ""

                        SelectNameLen = Len(strLine(i))

                        For j = 4 To SelectNameLen
                            OneName = Mid(strLine(i), j, 1)
                            strLineName = strLineName + OneName
                        Next

                        mudtSetOpsSelectionMenuWork.udtOpsSelectionMenuNameKeyRecEdit(i + 1).EditSelectMenuKeyName = MojiMake(strLineName, 16)

                    Next
                    mudtSetOpsSelectionMenuWork.udtOpsSelectionMenuNameKeyRecEdit(0).EditSelectMenuKeyName = MojiMake("", 16)

                End If

                Call mSetDisplay(mudtSetOpsSelectionMenuWork)

                ''設定不可スクリーン
                If Not mstrIniSelectionDisableList Is Nothing Then
                    For i = 0 To grdSelectionList.RowCount - 1
                        For j = 0 To UBound(mstrIniSelectionDisableList)
                            If grdSelectionList.Rows(i).Cells(1).Value = mstrIniSelectionDisableList(j) Then
                                grdSelectionList.Rows(i).Cells(0).ReadOnly = True
                                grdSelectionList.Rows(i).Cells(0).Style.BackColor = gColorGridRowBackReadOnly
                            End If
                        Next
                    Next
                End If

                ''初期化フラグ
                mblnInitFlg = False

            Else

                ''取得出来なかった場合は画面を開かない
                Call Me.Close()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsSelectionMenuList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''画面表示時のセル選択を解除
            grdSelectionList.CurrentCell = Nothing

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Machineryボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : パートの切替え
    '--------------------------------------------------------------------
    Private Sub optOPS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOPS.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''設定値の取得
            Call mSetStructure(mudtSetOpsSelectionMenuWork)

            If optOPS.Checked Then

                ''作業用構造体にOPS情報を設定
                Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewCarg)
                Call mCopyStructure(mudtSetOpsSelectionMenuNewMach, mudtSetOpsSelectionMenuWork)

            ElseIf optVDU.Checked Then

                ''作業用構造体にExt.VDU情報を設定
                Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewMach)
                Call mCopyStructure(mudtSetOpsSelectionMenuNewCarg, mudtSetOpsSelectionMenuWork)

            End If

            ''画面設定
            Call mSetDisplay(mudtSetOpsSelectionMenuWork)

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
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False
            Dim SetOpsSelectionMenuOldMach As gTypSetOpsSelectionMenuEdit = Nothing     '' 比較用 Machデータ  2014.04.15
            Dim SetOpsSelectionMenuOldCarg As gTypSetOpsSelectionMenuEdit = Nothing     '' 比較用 Cargoデータ 2014.04.15

            ''初期設定を比較用データにセット   2014.04.15
            ''配列再定義
            Call SetOpsSelectionMenuOldMach.InitArray()
            Call SetOpsSelectionMenuOldCarg.InitArray()
            For i As Integer = 0 To UBound(gudt.SetOpsSelectionMenuM.udtOpsSelectionOffSetRec)
                Call SetOpsSelectionMenuOldMach.udtOpsSelectionSetViewRecEdit(i).InitArray()
                Call SetOpsSelectionMenuOldCarg.udtOpsSelectionSetViewRecEdit(i).InitArray()
            Next

            ''Machinery/Cargoの情報を取得する
            Call mCopyStructure2(gudt.SetOpsSelectionMenuM, SetOpsSelectionMenuOldMach)
            Call mCopyStructure2(gudt.SetOpsSelectionMenuC, SetOpsSelectionMenuOldCarg)


            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsSelectionMenuWork)

            ''設定値の保存処理追加    2014.04.15
            If optOPS.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewMach)
            If optVDU.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewCarg)

            ''データが変更されているかチェック  初期設定と比較するように変更   2014.04.15
            'blnMach = mChkStructureEquals(mudtSetOpsSelectionMenuNewMach, mudtSetOpsSelectionMenuWork)
            'blnCarg = mChkStructureEquals(mudtSetOpsSelectionMenuNewCarg, mudtSetOpsSelectionMenuWork)
            blnMach = mChkStructureEquals(mudtSetOpsSelectionMenuNewMach, SetOpsSelectionMenuOldMach)
            blnCarg = mChkStructureEquals(mudtSetOpsSelectionMenuNewCarg, SetOpsSelectionMenuOldCarg)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''設定値の保存処理変更    2014.04.15
                'If Not blnMach Then Call mCopyStructure3(mudtSetOpsSelectionMenuWork, gudt.SetOpsSelectionMenuM)
                'If Not blnCarg Then Call mCopyStructure3(mudtSetOpsSelectionMenuWork, gudt.SetOpsSelectionMenuC)
                If Not blnMach Then Call mCopyStructure3(mudtSetOpsSelectionMenuNewMach, gudt.SetOpsSelectionMenuM)
                If Not blnCarg Then Call mCopyStructure3(mudtSetOpsSelectionMenuNewCarg, gudt.SetOpsSelectionMenuC)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuC = 1

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
    Private Sub frmOpsSelectionMenuList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False
            Dim SetOpsSelectionMenuOldMach As gTypSetOpsSelectionMenuEdit = Nothing     '' 比較用 Machデータ  2014.04.15
            Dim SetOpsSelectionMenuOldCarg As gTypSetOpsSelectionMenuEdit = Nothing     '' 比較用 Cargoデータ 2014.04.15
 
            ''初期設定を比較用データにセット   2014.04.15
            ''配列再定義
            Call SetOpsSelectionMenuOldMach.InitArray()
            Call SetOpsSelectionMenuOldCarg.InitArray()
            For i As Integer = 0 To UBound(gudt.SetOpsSelectionMenuM.udtOpsSelectionOffSetRec)
                Call SetOpsSelectionMenuOldMach.udtOpsSelectionSetViewRecEdit(i).InitArray()
                Call SetOpsSelectionMenuOldCarg.udtOpsSelectionSetViewRecEdit(i).InitArray()
            Next

            ''Machinery/Cargoの情報を取得する
            Call mCopyStructure2(gudt.SetOpsSelectionMenuM, SetOpsSelectionMenuOldMach)
            Call mCopyStructure2(gudt.SetOpsSelectionMenuC, SetOpsSelectionMenuOldCarg)


            ''グリッドの保留中の変更を全て適用させる
            Call grdSelectionList.EndEdit()

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsSelectionMenuWork)

            ''設定値の保存処理追加    2014.04.15
            If optOPS.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewMach)
            If optVDU.Checked Then Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewCarg)

            ''データが変更されているかチェック 初期設定と比較するように変更   2014.04.15
            'blnMach = mChkStructureEquals(mudtSetOpsSelectionMenuNewMach, mudtSetOpsSelectionMenuWork)
            'blnCarg = mChkStructureEquals(mudtSetOpsSelectionMenuNewCarg, mudtSetOpsSelectionMenuWork)
            blnMach = mChkStructureEquals(mudtSetOpsSelectionMenuNewMach, SetOpsSelectionMenuOldMach)
            blnCarg = mChkStructureEquals(mudtSetOpsSelectionMenuNewCarg, SetOpsSelectionMenuOldCarg)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''設定値の保存処理変更    2014.04.15
                        'If Not blnMach Then Call mCopyStructure3(mudtSetOpsSelectionMenuWork, gudt.SetOpsSelectionMenuM)
                        'If Not blnCarg Then Call mCopyStructure3(mudtSetOpsSelectionMenuWork, gudt.SetOpsSelectionMenuC)
                        If Not blnMach Then Call mCopyStructure3(mudtSetOpsSelectionMenuNewMach, gudt.SetOpsSelectionMenuM)
                        If Not blnCarg Then Call mCopyStructure3(mudtSetOpsSelectionMenuNewCarg, gudt.SetOpsSelectionMenuC)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuC = 1

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
    Private Sub frmOpsSelectionMenuList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSelectionList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSelectionList.CellDoubleClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdSelectionList.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdSelectionList.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合
            If grdSelectionList.Rows(e.RowIndex).Cells(0).Style.BackColor = gColorGridRowBackReadOnly Then Return ''設定不可行の場合

            If grdSelectionList.Rows(e.RowIndex).Cells(0).Value = False Then Return

            ''詳細画面の表示処理
            If frmOpsSelectionMenuDetail.gShow(mudtSetOpsSelectionMenuWork.udtOpsSelectionSetViewRecEdit, _
                                               mudtIniFunctionList, e.RowIndex, Me) = 0 Then

                'Call mCopyWorkStructure()

                ''画面表示
                Call mSetDisplayOne(mudtSetOpsSelectionMenuWork, e.RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 画面番号チェックボックスクリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSelectionList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSelectionList.CellClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdSelectionList.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex <> 0 Then Return ''チェックボックス箇所以外
            If grdSelectionList.Rows(e.RowIndex).Cells(0).Style.BackColor = gColorGridRowBackReadOnly Then Return ''設定不可行の場合


            mudtSetOpsSelectionMenuWork.udtOpsSelectionOffSetRecEdit(e.RowIndex).EditViewNo = 1

            ''画面表示
            Call mSetDisplayOne(mudtSetOpsSelectionMenuWork, e.RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region
#End Region

#Region "内部関数"

    Private Function mGetIniInfo() As Integer

        Dim strwk1() As String = Nothing
        Dim strwk2() As String = Nothing
        Dim strwk3() As String = Nothing

        ''デフォルト状態を作成、保存
        Call gInitSetOpsSelectionMenu(mudtSetOpsSelectionMenuDefault)

        ''スクリーンタイトル
        If gGetComboCodeName(mudtIniScreenTitle, gEnmComboType.ctOpsScreenTitle) <> 0 Then
            Return -1
        End If

        ''ボタン機能リスト
        If gGetComboCodeName(mudtIniFunctionList, gEnmComboType.ctOpsSelectionFunctionList) <> 0 Then
            Return -1
        End If

        ''設定可能ボタンリスト
        If gGetIniFileLine(strwk1, gEnmComboType.ctOpsSelectionFunctionSet) <> 0 Then
            Return -1
        Else

            ReDim mudtIniFunctionSet(UBound(strwk1))

            For i As Integer = 0 To UBound(mudtIniFunctionSet)

                ''１行分の情報を分割
                strwk2 = strwk1(i).Split(",")

                ''対象画面番号を保存
                mudtIniFunctionSet(i).intScreenNo = strwk2(0)

                ''対象画面番号に続く情報がある場合
                If UBound(strwk2) <> 0 Then

                    For j As Integer = 1 To UBound(strwk2)

                        ReDim Preserve mudtIniFunctionSet(i).intFunctionNo(j - 1)
                        mudtIniFunctionSet(i).intFunctionNo(j - 1) = strwk2(j)

                    Next

                End If

            Next

        End If

        ''設定不可ボタン
        If gGetIniFileLine(strwk2, gEnmComboType.ctOpsSelectionSelectionDisableList) <> 0 Then
            Return -1
        Else
            mstrIniSelectionDisableList = strwk2(0).Split(",")
        End If

        Return 0

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim j As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chkUse"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtScreenNo" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtName" : Column3.ReadOnly = True

            With grdSelectionList

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "No" : .Columns(1).Width = 100 : .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).HeaderText = "Screen Title" : .Columns(2).Width = 398

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング
                .ColumnHeadersHeight = 20

                ''行
                .RowCount = 202
                '.RowCount = UBound(mudtIniScreenTitle) + 2
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

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
                    For j = 1 To .ColumnCount - 1
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
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
                Call gSetGridCopyAndPaste(grdSelectionList)

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsSelectionMenuEdit)

        Try

            Dim Setno As Integer = 1

            For intRow As Integer = 0 To grdSelectionList.RowCount - 1

                If grdSelectionList.Rows(intRow).Cells(0).Value = True Then
                    udtSet.udtOpsSelectionOffSetRecEdit(intRow).EditViewNo = Setno
                    Setno = Setno + 1
                Else
                    udtSet.udtOpsSelectionOffSetRecEdit(intRow).EditViewNo = 0
                End If

                'udtSet.udtOpsSelectionOffSetRecEdit(intRow).EditViewNo = IIf(grdSelectionList.Rows(intRow).Cells(0).Value, Setno + 1, 0)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsSelectionMenuEdit)

        Try

            For intRow As Integer = 0 To grdSelectionList.RowCount - 1

                mSetDisplayOne(udtSet, intRow)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplayOne(ByVal udtSet As gTypSetOpsSelectionMenuEdit, ByVal intSelectedRow As Integer)

        Try

            Dim udtMC As gEnmMachineryCargo = IIf(optOPS.Checked, gEnmMachineryCargo.mcMachinery, gEnmMachineryCargo.mcCargo)

            With udtSet.udtOpsSelectionOffSetRecEdit(intSelectedRow)

                ''使用有無(セレクションメニューテーブルの画面番号が「0」の場合はチェックを外す)
                grdSelectionList.Rows(intSelectedRow).Cells(0).Value = IIf(.EditViewNo = 0, False, True)

            End With

            With udtSet.udtOpsSelectionSetViewRecEdit(intSelectedRow)

                ''画面番号
                grdSelectionList.Rows(intSelectedRow).Cells(1).Value = mGetFunctionName(intSelectedRow + 1)

                ''セレクションメニュー画面名称
                grdSelectionList.Rows(intSelectedRow).Cells(2).Value = .EditSelectName

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Function mGetFunctionName(ByVal ViewNo As Integer) As String

        Dim strRtn1 As String = "00"
        Dim strRtn2 As String = "0"
        Dim VNoLen As Integer
        Dim VNo As String

        VNo = ViewNo.ToString

        '表示数桁調査
        VNoLen = Len(VNo)

        Select Case VNoLen

            Case 1
                mGetFunctionName = strRtn1 + VNo
            Case 2
                mGetFunctionName = strRtn2 + VNo
            Case 3
                mGetFunctionName = VNo
            Case Else
                mGetFunctionName = "000"

        End Select

    End Function

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsSelectionMenuEdit, _
                               ByRef udtTarget As gTypSetOpsSelectionMenuEdit)

        Dim i As Integer
        Dim j As Integer

        Try

            For i = 0 To UBound(udtTarget.udtOpsSelectionOffSetRecEdit)

                ''画面表示番号
                udtTarget.udtOpsSelectionOffSetRecEdit(i) = udtSource.udtOpsSelectionOffSetRecEdit(i)
            Next

            For i = 0 To UBound(udtTarget.udtOpsSelectionSetViewRecEdit)

                ''セレクション名称
                udtTarget.udtOpsSelectionSetViewRecEdit(i).EditSelectName = udtSource.udtOpsSelectionSetViewRecEdit(i).EditSelectName

                For j = 0 To UBound(udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey)

                    ''キー
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1
                Next

            Next

            For i = 0 To UBound(udtTarget.udtOpsSelectionMenuNameKeyRecEdit)

                ''セレクションメニューキー名称
                udtTarget.udtOpsSelectionMenuNameKeyRecEdit(i) = udtSource.udtOpsSelectionMenuNameKeyRecEdit(i)

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
    Private Sub mCopyStructure2(ByVal udtSource As gTypSetOpsSelectionMenu, _
                               ByRef udtTarget As gTypSetOpsSelectionMenuEdit)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        Try

            k = 0

            For i = 0 To UBound(udtTarget.udtOpsSelectionOffSetRecEdit)

                ''画面表示番号
                udtTarget.udtOpsSelectionOffSetRecEdit(i).EditViewNo = udtSource.udtOpsSelectionOffSetRec(i).ViewNo

                If udtTarget.udtOpsSelectionOffSetRecEdit(i).EditViewNo <> 0 Then


                    ''セレクション名称
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditSelectName = udtSource.udtOpsSelectionSetViewRec(k).SelectName

                    For j = 0 To UBound(udtTarget.udtOpsSelectionSetViewRecEdit(k).EditudtKey)

                        ''キー
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1 = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType1
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2 = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType2
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3 = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType3
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4 = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType4
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).BytSelectName
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).NameCode
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1 = udtSource.udtOpsSelectionSetViewRec(k).udtKey(j).Yobi1
                    Next
                    k = k + 1


                Else
                    ''セレクション名称
                    udtTarget.udtOpsSelectionSetViewRecEdit(i).EditSelectName = ""

                    For j = 0 To UBound(udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey)

                        ''キー
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1 = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2 = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3 = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4 = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode = 0
                        udtTarget.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1 = 0
                    Next
                End If
            Next

            For i = 0 To UBound(udtTarget.udtOpsSelectionMenuNameKeyRecEdit)

                ''セレクションメニューキー名称
                udtTarget.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName = udtSource.udtOpsSelectionMenuNameKeyRec(i).SelectMenuKeyName

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
    Private Sub mCopyStructure3(ByVal udtSource As gTypSetOpsSelectionMenuEdit, _
                               ByRef udtTarget As gTypSetOpsSelectionMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        Try

            k = 0

            For i = 0 To UBound(udtSource.udtOpsSelectionOffSetRecEdit)

                ''画面表示番号
                udtTarget.udtOpsSelectionOffSetRec(i).ViewNo = udtSource.udtOpsSelectionOffSetRecEdit(i).EditViewNo

                If udtTarget.udtOpsSelectionOffSetRec(i).ViewNo <> 0 Then


                    ''セレクション名称
                    udtTarget.udtOpsSelectionSetViewRec(k).SelectName = udtSource.udtOpsSelectionSetViewRecEdit(i).EditSelectName

                    For j = 0 To UBound(udtTarget.udtOpsSelectionSetViewRec(k).udtKey)

                        ''キー
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType1 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType2 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType3 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).BytNameType4 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).BytSelectName = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).NameCode = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode
                        udtTarget.udtOpsSelectionSetViewRec(k).udtKey(j).Yobi1 = udtSource.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1
                    Next
                    k = k + 1
                End If
            Next

            For i = 0 To UBound(udtSource.udtOpsSelectionMenuNameKeyRecEdit)

                ''セレクションメニューキー名称
                udtTarget.udtOpsSelectionMenuNameKeyRec(i).SelectMenuKeyName = udtSource.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsSelectionMenuEdit, _
                                         ByVal udt2 As gTypSetOpsSelectionMenuEdit) As Boolean

        Try

            For i = 0 To UBound(udt1.udtOpsSelectionOffSetRecEdit)

                ''画面表示番号
                If udt1.udtOpsSelectionOffSetRecEdit(i).EditViewNo <> udt2.udtOpsSelectionOffSetRecEdit(i).EditViewNo Then Return False
            Next

            For i = 0 To UBound(udt1.udtOpsSelectionOffSetRecEdit)
                ''セレクション名称
                If udt1.udtOpsSelectionSetViewRecEdit(i).EditSelectName <> udt2.udtOpsSelectionSetViewRecEdit(i).EditSelectName Then Return False

                For j = 0 To UBound(udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey)

                    ''キー
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1 <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType1 Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2 <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType2 Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3 <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType3 Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4 <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytNameType4 Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditBytSelectName Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditNameCode Then Return False
                    If udt1.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1 <> udt2.udtOpsSelectionSetViewRecEdit(i).EditudtKey(j).EditYobi1 Then Return False
                Next

            Next

            For i = 0 To UBound(udt1.udtOpsSelectionMenuNameKeyRecEdit)
                If i = 68 Then
                    Dim debugA As Integer = 0
                End If
                ''セレクションメニューキー名称
                'If udt1.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName <> udt2.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName Then Return False
                If Not gCompareString(udt1.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName, udt2.udtOpsSelectionMenuNameKeyRecEdit(i).EditSelectMenuKeyName) Then Return False '' 2014.03.12

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値の取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 詳細設定画面の情報を該当構造体に設定
    '--------------------------------------------------------------------
    Private Sub mCopyWorkStructure()

        Try

            'Call mSetStructure(mudtSetOpsScreenTitleWork)

            If optOPS.Checked Then

                ''OPS構造体に設定
                Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewMach)

            ElseIf optVDU.Checked Then

                ''Ext.VDU構造体に設定
                Call mCopyStructure(mudtSetOpsSelectionMenuWork, mudtSetOpsSelectionMenuNewCarg)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Function mGetSetFunctionList(ByVal intScreenNo As Integer) As Integer()

        For i As Integer = 0 To UBound(mudtIniFunctionSet)

            If mudtIniFunctionSet(i).intScreenNo = intScreenNo Then

                Return mudtIniFunctionSet(i).intFunctionNo

            End If

        Next

        Return Nothing

    End Function

#End Region

End Class