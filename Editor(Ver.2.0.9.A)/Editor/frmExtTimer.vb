Public Class frmExtTimer

#Region "定数定義"

    ''列番号
    ''Public Const mCstCodeExtTimerColIndexName As Integer = 0
    Public Const mCstCodeExtTimerColIndexType As Integer = 1
    ''Public Const mCstCodeExtTimerColIndexTime As Integer = 2
    Public Const mCstCodeExtTimerColIndexInit As Integer = 3
    Public Const mCstCodeExtTimerColIndexLow As Integer = 4
    Public Const mCstCodeExtTimerColIndexHigh As Integer = 5
    ''※使う時にコメントアウト解除して下さい。

#End Region

#Region "変数定義"

    ''チェック用
    Private Enum mEnmCheckType
        ctCheck         ''上下限値を超えている場合はメッセージを表示
        ctCorrect       ''上下限値を超えている場合は強制補正
    End Enum

    Private mudtSetTimerNew() As gTypSetExtTimerRec = Nothing
    Private mudtSetTimerNameNew() As gTypSetExtTimerNameRec = Nothing

    ''初期化中イベントフラグ、分/秒 切替設定イベントフラグ 共用
    Private mintInitFlg As Boolean

    ''「分/秒 切替設定」前回値保持用
    Private mshtSelectTimePrv() As Short

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef frmOwner As Form)

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

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
    Private Sub frmExtTimer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mintInitFlg = True

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            ReDim mudtSetTimerNew(UBound(gudt.SetExtTimerSet.udtTimerInfo))              ''種類、初期値、上下限値
            ReDim mudtSetTimerNameNew(UBound(gudt.SetExtTimerName.udtTimerRec))          ''名称
            ReDim mshtSelectTimePrv(UBound(gudt.SetExtTimerName.udtTimerRec))            ''前回値（分/秒 切替設定）

            ''構造体コピー        
            Call mCopyStructure(gudt.SetExtTimerSet.udtTimerInfo, mudtSetTimerNew)       ''種類、初期値、上下限値
            Call mCopyStructure(gudt.SetExtTimerName.udtTimerRec, mudtSetTimerNameNew)   ''名称

            ''画面設定
            Call mSetDisplay(mudtSetTimerNew, mudtSetTimerNameNew)

            ''「分/秒 切替設定」前回値の取得
            For i As Integer = 0 To (UBound(gudt.SetExtTimerName.udtTimerRec))
                mshtSelectTimePrv(i) = mudtSetTimerNew(i).shtTimeDisp
            Next

            ''初期化フラグ
            mintInitFlg = False

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

            ''入力チェック
            If Not mChkInput() Then Return

            '設定値を比較用構造体に格納
            Call mSetStructure(mudtSetTimerNew, mudtSetTimerNameNew)

            'データが変更されているかチェック
            If mChkDataChange() Then

                ''変更された場合は設定を更新する        
                Call mCopyStructure(mudtSetTimerNew, gudt.SetExtTimerSet.udtTimerInfo)       ''種類、初期値、上下限値
                Call mCopyStructure(mudtSetTimerNameNew, gudt.SetExtTimerName.udtTimerRec)   ''名称

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytTimer = 1
                gudt.SetEditorUpdateInfo.udtSave.bytTimerName = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytTimer = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytTimerName = 1

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
    ' 機能      : Printボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面印刷を行う
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Call gPrintScreen(True)

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
    Private Sub frmExtTimer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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
    Private Sub frmExtTimer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''入力チェック、修正
            Call mChkInputValue(mEnmCheckType.ctCorrect)

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetTimerNew, mudtSetTimerNameNew)

            ''データが変更されているかチェック
            If mChkDataChange() Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        Call mCopyStructure(mudtSetTimerNew, gudt.SetExtTimerSet.udtTimerInfo)       ''名称
                        Call mCopyStructure(mudtSetTimerNameNew, gudt.SetExtTimerName.udtTimerRec)   ''種類、初期値、上下限値

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytTimer = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytTimerName = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytTimer = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytTimerName = 1

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





#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： プルダウンリストの項目を変更した時の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTimer_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTimer.CellValueChanged

        Try

            ''処理を抜ける条件
            If mintInitFlg Then Return ''初期化中、時刻表示切換えイベント発生中の時は処理を抜ける
            If grdTimer.CurrentCell.OwningColumn.Name <> "cmbTimeDisp" Then Return ''「分/秒 切替設定」以外の場合
            If e.RowIndex < 0 Or e.RowIndex > grdTimer.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdTimer.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''グリッドの保留中の変更を全て適用させる
            grdTimer.EndEdit()

            ''設定値取得
            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intRowIndex As Integer = grdTimer.CurrentCell.RowIndex
            Dim shtSelectType As Short = CCShort(grdTimer.Rows(intRowIndex).Cells("cmbType").Value)
            Dim shtSelectTime As Short = CCShort(grdTimer.Rows(intRowIndex).Cells("cmbTimeDisp").Value)

            ''分/秒の表示切り換え
            Call mConvDispTime(intRowIndex, shtSelectType, shtSelectTime)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTimer_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTimer.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdTimer_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdTimer_KeyPress

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
    Private Sub grdTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdTimer.KeyPress

        Try

            Dim strColumnName As String
            Dim intRowIndex As Integer

            ''選択セルの名称取得
            strColumnName = grdTimer.CurrentCell.OwningColumn.Name
            intRowIndex = grdTimer.CurrentCell.RowIndex

            '--------------------
            '' 表示名称
            '--------------------
            If strColumnName = "txtName" Then
                e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)
            End If

            '--------------------
            '' 初期値, 上下限値 
            '--------------------
            If strColumnName = "txtInit" Or strColumnName = "txtLowLimit" Or strColumnName = "txtHighLimit" Then

                ''選択されている「分/秒 切替設定」によって入力制限処理を分岐
                Select Case CCInt(grdTimer.Rows(intRowIndex).Cells("cmbTimeDisp").Value)
                    Case gCstCodeExtTimerTimerDispMin

                        ''分
                        e.Handled = gCheckTextInput(3, sender, e.KeyChar, True, False, True)

                    Case Else

                        ''秒、新規追加項目
                        e.Handled = gCheckTextInput(4, sender, e.KeyChar)

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTimer_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdTimer.CellValidating

        Try

            ''処理を抜ける条件
            If mintInitFlg Then Return ''初期化中、時刻表示切換えイベント発生中の時は処理を抜ける
            If e.RowIndex < 0 Or e.ColumnIndex > grdTimer.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdTimer.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''種類、初期値、上下限値を変更した時は入力チェック・修正を行う
            If e.ColumnIndex = mCstCodeExtTimerColIndexType Or _
               e.ColumnIndex = mCstCodeExtTimerColIndexInit Or _
               e.ColumnIndex = mCstCodeExtTimerColIndexLow Or _
               e.ColumnIndex = mCstCodeExtTimerColIndexHigh Then

                Dim dgv As DataGridView = CType(sender, DataGridView)

                ''グリッドの保留中の変更を全て適用させる
                dgv.EndEdit()

                ''入力チェック、修正
                Call mChkInputValue(mEnmCheckType.ctCorrect)

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

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbPart"          '' パート追加　ver.1.4.4 2012.05.08
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtName"
            Dim Column3 As New DataGridViewComboBoxColumn : Column3.Name = "cmbType"
            Dim Column4 As New DataGridViewComboBoxColumn : Column4.Name = "cmbTimeDisp"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtInit"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtLowLimit"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtHighLimit"

            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdTimer

                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Part" : .Columns(0).Width = 70            '' パート追加　ver.1.4.4 2012.05.08
                .Columns(1).HeaderText = "Name" : .Columns(1).Width = 280
                .Columns(2).HeaderText = "Type" : .Columns(2).Width = 120
                .Columns(3).HeaderText = "Time" : .Columns(3).Width = 120
                .Columns(4).HeaderText = "Initial Time" : .Columns(4).Width = 95
                .Columns(5).HeaderText = "Low Limit" : .Columns(5).Width = 95
                .Columns(6).HeaderText = "High Limit" : .Columns(6).Width = 95
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                '■外販
                '外販の場合、ﾊﾟｰﾄは非表示
                If gintNaiGai = 1 Then
                    .Columns(0).Visible = False
                End If

                ''行
                .RowCount = 16 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .TopLeftHeaderCell.Value = "No"
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 75
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

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
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コンボボックス初期設定
                Call gSetComboBox(Column1, gEnmComboType.ctExtTimerPart)       '' パート追加　ver.1.4.4 2012.05.08
                Call gSetComboBox(Column3, gEnmComboType.ctExtTimerType)
                Call gSetComboBox(Column4, gEnmComboType.ctExtTimerTimeDisp)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdTimer)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) タイマ設定構造体
    '           : ARG2 - ( O) タイマ表示名称設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet() As gTypSetExtTimerRec, _
                              ByRef udtSetName() As gTypSetExtTimerNameRec)

        Try

            For intRow As Integer = 0 To UBound(udtSet)

                ''パート
                udtSet(intRow).bytPart = CCbyte(grdTimer.Rows(intRow).Cells("cmbPart").Value)       '' パート追加　ver.1.4.4 2012.05.08

                ''名称
                udtSetName(intRow).strName = Trim(grdTimer.Rows(intRow).Cells("txtName").Value)

                ''種類
                udtSet(intRow).shtType = CCShort(grdTimer.Rows(intRow).Cells("cmbType").Value)

                ''分/秒 切替設定
                udtSet(intRow).shtTimeDisp = CCShort(grdTimer.Rows(intRow).Cells("cmbTimeDisp").Value)

                ''初期値
                udtSet(intRow).shtInit = mSaveTimeConvSec(CCShort(grdTimer.Rows(intRow).Cells("cmbTimeDisp").Value), _
                                                          CCDouble(grdTimer.Rows(intRow).Cells("txtInit").Value))

                ''下限値
                udtSet(intRow).shtLow = mSaveTimeConvSec(CCShort(grdTimer.Rows(intRow).Cells("cmbTimeDisp").Value), _
                                                         CCDouble(grdTimer.Rows(intRow).Cells("txtLowLimit").Value))

                ''上限値
                udtSet(intRow).shtHigh = mSaveTimeConvSec(CCShort(grdTimer.Rows(intRow).Cells("cmbTimeDisp").Value), _
                                                          CCDouble(grdTimer.Rows(intRow).Cells("txtHighLimit").Value))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) タイマ設定構造体
    '           : ARG2 - (I ) タイマ表示名称設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet() As gTypSetExtTimerRec, _
                            ByVal udtSetName() As gTypSetExtTimerNameRec)

        Try

            For i As Integer = 0 To UBound(udtSet)

                ''パート
                grdTimer.Rows(i).Cells("cmbPart").Value = udtSet(i).bytPart.ToString       '' パート追加　ver.1.4.4 2012.05.08

                ''名称
                grdTimer.Rows(i).Cells("txtName").Value = gGetString(udtSetName(i).strName)

                ''種類
                grdTimer.Rows(i).Cells("cmbType").Value = udtSet(i).shtType.ToString

                ''分/秒 切替設定
                grdTimer.Rows(i).Cells("cmbTimeDisp").Value = udtSet(i).shtTimeDisp.ToString

                ''時間換算処理
                Select Case udtSet(i).shtTimeDisp
                    Case gCstCodeExtTimerTimerDispMin

                        ''------------------------
                        '' 分表示
                        ''------------------------
                        mintInitFlg = True

                        grdTimer.Rows(i).Cells("txtInit").Value = mConvTimeToMin(udtSet(i).shtInit)      ''初期値
                        grdTimer.Rows(i).Cells("txtLowLimit").Value = mConvTimeToMin(udtSet(i).shtLow)   ''下限値
                        grdTimer.Rows(i).Cells("txtHighLimit").Value = mConvTimeToMin(udtSet(i).shtHigh) ''上限値

                        mintInitFlg = False

                    Case Else

                        ''------------------------
                        '' 秒表示（新規追加項目）
                        ''------------------------
                        mintInitFlg = True

                        grdTimer.Rows(i).Cells("txtInit").Value = udtSet(i).shtInit         ''初期値
                        grdTimer.Rows(i).Cells("txtLowLimit").Value = udtSet(i).shtLow      ''下限値
                        grdTimer.Rows(i).Cells("txtHighLimit").Value = udtSet(i).shtHigh    ''上限値

                        mintInitFlg = False

                End Select

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : データ変更チェック
    ' 返り値    : True：相違あり、False：相違なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Function mChkDataChange() As Boolean

        Try

            ''名称
            If Not mChkStructureEquals(gudt.SetExtTimerName.udtTimerRec, mudtSetTimerNameNew) Then Return True

            ''種類、時刻設定、初期値、上下限値
            If Not mChkStructureEquals(gudt.SetExtTimerSet.udtTimerInfo, mudtSetTimerNew) Then Return True


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return True
        End Try

        Return False

    End Function

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
    Private Sub mCopyStructure(ByVal udtSource() As gTypSetExtTimerNameRec, _
                               ByRef udtTarget() As gTypSetExtTimerNameRec)

        Try

            For i As Integer = 0 To UBound(udtSource)

                ''名称
                udtTarget(i).strName = udtSource(i).strName

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure(ByVal udtSource() As gTypSetExtTimerRec, _
                               ByRef udtTarget() As gTypSetExtTimerRec)

        Try

            For i As Integer = 0 To UBound(udtSource)

                udtTarget(i).shtType = udtSource(i).shtType           ''種類
                udtTarget(i).bytIndex = udtSource(i).bytIndex         ''レコード番号
                udtTarget(i).bytPart = udtSource(i).bytPart           ''パート             追加 ver.1.4.4 2012.05.08
                udtTarget(i).shtTimeDisp = udtSource(i).shtTimeDisp   ''分/秒 切替設定
                udtTarget(i).shtInit = udtSource(i).shtInit           ''初期値
                udtTarget(i).shtLow = udtSource(i).shtLow             ''下限値
                udtTarget(i).shtHigh = udtSource(i).shtHigh           ''上限値

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
    Private Function mChkStructureEquals(ByVal udt1() As gTypSetExtTimerNameRec, _
                                         ByVal udt2() As gTypSetExtTimerNameRec) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1)

                ''名称
                If Not gCompareString(udt1(i).strName, udt2(i).strName) Then Return False

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function

    Private Function mChkStructureEquals(ByVal udt1() As gTypSetExtTimerRec, _
                                         ByVal udt2() As gTypSetExtTimerRec) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1)

                If udt1(i).bytPart <> udt2(i).bytPart Then Return False ''パート       '' パート追加　ver.1.4.4 2012.05.08
                If udt1(i).shtType <> udt2(i).shtType Then Return False ''種類
                If udt1(i).shtTimeDisp <> udt2(i).shtTimeDisp Then Return False ''分/秒 切替設定
                If udt1(i).shtInit <> udt2(i).shtInit Then Return False ''初期値
                If udt1(i).shtLow <> udt2(i).shtLow Then Return False ''下限値
                If udt1(i).shtHigh <> udt2(i).shtHigh Then Return False ''上限値

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function



#Region "入力チェック"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック全体
    ' 返り値    : True：入力値OK、False：入力値NG
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim strName As String = ""
            Dim intSelectType As Integer, intSelectTime As Integer
            Dim strValInit As String = "", strValLow As String = "", strValHigh As String = ""

            With grdTimer

                For intRowIndex As Integer = 0 To .RowCount - 1

                    strName = gGetString(.Rows(intRowIndex).Cells("txtName").Value)               ''名称
                    intSelectType = CCInt(grdTimer.Rows(intRowIndex).Cells("cmbType").Value)      ''種類
                    intSelectTime = CCInt(grdTimer.Rows(intRowIndex).Cells("cmbTimeDisp").Value)  ''分/秒 切替設定
                    strValInit = gGetString(.Rows(intRowIndex).Cells("txtInit").Value)            ''初期値
                    strValLow = gGetString(.Rows(intRowIndex).Cells("txtLowLimit").Value)         ''下限値
                    strValHigh = gGetString(.Rows(intRowIndex).Cells("txtHighLimit").Value)       ''上限値

                    ''---------------------------------
                    '' 名称入力チェック
                    ''---------------------------------
                    If intSelectType <> gCstCodeExtTimerTypeNothing Or _
                       strValInit <> "0" Or _
                       strValLow <> "0" Or _
                       strValHigh <> "0" Then

                        If Not gChkInputText(.Rows(intRowIndex).Cells("txtName"), "Name", intRowIndex + 1, False, True) Then Return False

                    End If

                    ''---------------------------------
                    '' 種類入力チェック
                    ''---------------------------------
                    If strName <> "" Or _
                       strValInit <> "0" Or _
                       strValLow <> "0" Or _
                       strValHigh <> "0" Then

                        If intSelectType = 0 Then
                            Call MessageBox.Show("Please set the item." & vbNewLine & vbNewLine & _
                                                 "[ Col ] " & "Type" & " " & vbNewLine & "[ Row ] " & intRowIndex + 1, _
                                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                    End If

                    ''---------------------------------
                    '' 初期値・上下限値入力チェック
                    ''---------------------------------
                    If Not mChkInputValue(mEnmCheckType.ctCheck) Then Return False

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック（初期値・上下限値）
    ' 返り値    : True：入力値OK、False：入力値NG
    ' 引き数    : ARG1 - (I ) チェック関数の処理タイプ
    ' 備考      : 設定値範囲エラーでメッセージを表示して処理を抜ける
    '--------------------------------------------------------------------
    Private Function mChkInputValue(ByVal udtCheckType As mEnmCheckType) As Boolean

        Try

            Dim hintRowIndex As Integer = grdTimer.CurrentCell.RowIndex
            Dim hshtSelectType As Integer = CCInt(grdTimer.Rows(hintRowIndex).Cells("cmbType").Value)
            Dim hsthSelectTime As Integer = CCInt(grdTimer.Rows(hintRowIndex).Cells("cmbTimeDisp").Value)

            Select Case hsthSelectTime
                Case gCstCodeExtTimerTimerDispMin

                    ''=========================
                    '' 切替設定：分
                    ''=========================
                    Select Case hshtSelectType
                        Case gCstCodeExtTimerTypeEeengineerCall

                            ''エンジニアコール
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowMinEng, gCstCodeExtTimerLimitHighMinEng) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowMinEng, gCstCodeExtTimerLimitHighMinEng)
                            End Select

                        Case gCstCodeExtTimerTypeDeadman1

                            ''デッドマンアラーム１
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowMinDeadMan1, gCstCodeExtTimerLimitHighMinDeadMan1) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowMinDeadMan1, gCstCodeExtTimerLimitHighMinDeadMan1)
                            End Select

                        Case gCstCodeExtTimerTypeDeadman2

                            ''デッドマンアラーム２
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecDeadMan2, gCstCodeExtTimerLimitHighSecDeadMan2) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowMinDeadMan2, gCstCodeExtTimerLimitHighMinDeadMan2)
                            End Select

                        Case Else

                            ''新規に追加した項目等
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecElse, gCstCodeExtTimerLimitHighSecElse) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowMinElse, gCstCodeExtTimerLimitHighMinElse)
                            End Select

                    End Select

                Case Else

                    ''=========================
                    '' 切替設定：秒、その他
                    ''=========================
                    Select Case hshtSelectType
                        Case gCstCodeExtTimerTypeEeengineerCall

                            ''エンジニアコール
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecEng, gCstCodeExtTimerLimitHighSecEng) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowSecEng, gCstCodeExtTimerLimitHighSecEng)
                            End Select

                        Case gCstCodeExtTimerTypeDeadman1

                            ''デッドマンアラーム１
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecDeadMan1, gCstCodeExtTimerLimitHighSecDeadMan1) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowSecDeadMan1, gCstCodeExtTimerLimitHighSecDeadMan1)
                            End Select

                        Case gCstCodeExtTimerTypeDeadman2

                            ''デッドマンアラーム２
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecDeadMan2, gCstCodeExtTimerLimitHighSecDeadMan2) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowSecDeadMan2, gCstCodeExtTimerLimitHighSecDeadMan2)
                            End Select

                        Case Else

                            ''新規に追加した項目等
                            Select Case udtCheckType
                                Case mEnmCheckType.ctCheck : If Not mChkInputShowMsg(hintRowIndex, gCstCodeExtTimerLimitLowSecElse, gCstCodeExtTimerLimitHighSecElse) Then Return False
                                Case mEnmCheckType.ctCorrect : Call mChkInputCorrectValue(hsthSelectTime, hintRowIndex, gCstCodeExtTimerLimitLowSecElse, gCstCodeExtTimerLimitHighSecElse)
                            End Select

                    End Select

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力値の有効範囲チェック
    ' 返り値    : True：入力値OK、False：入力値NG
    ' 引き数    : ARG1 - (I ) 処理行数
    '           : ARG2 - (I ) 有効範囲 下限値
    '           : ARG3 - (I ) 有効範囲 上限値
    ' 備考      : gChkTextNumSpanは戻値Trueで入力範囲エラー
    '--------------------------------------------------------------------
    Private Function mChkInputShowMsg(ByVal hintRowIndex As Integer, _
                                      ByVal hdblLimitLow As Double, _
                                      ByVal hdblLimitHigh As Double) As Boolean

        Try

            With grdTimer

                If Not gChkInputNum(.Rows(hintRowIndex).Cells("txtInit"), hdblLimitLow, hdblLimitHigh, "Initial Time", hintRowIndex + 1, False, True) Then Return False
                If Not gChkInputNum(.Rows(hintRowIndex).Cells("txtLowLimit"), hdblLimitLow, hdblLimitHigh, "Low Limit", hintRowIndex + 1, False, True) Then Return False
                If Not gChkInputNum(.Rows(hintRowIndex).Cells("txtHighLimit"), hdblLimitLow, hdblLimitHigh, "High Limit", hintRowIndex + 1, False, True) Then Return False

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力値の有効範囲チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 種類
    '           : ARG2 - (I ) 処理行Index
    '           : ARG3 - (I ) 下限値
    '           : ARG4 - (I ) 上限値
    ' 備考      : 上下限値をそれぞれ超えた場合、強制補正
    '--------------------------------------------------------------------
    Private Sub mChkInputCorrectValue(ByVal hshtSelectType As Short, _
                                      ByVal hintRowIndex As Integer, _
                                      ByVal hdblLimitLow As Double, _
                                      ByVal hdblLimitHigh As Double)

        Try

            With grdTimer

                ''設定値の取得
                Dim dblInit As Double = CCDouble(.Rows(hintRowIndex).Cells("txtInit").Value)         ''初期値
                Dim dblLow As Double = CCDouble(.Rows(hintRowIndex).Cells("txtLowLimit").Value)      ''下限値
                Dim dblHigh As Double = CCDouble(.Rows(hintRowIndex).Cells("txtHighLimit").Value)    ''上限値

                ''設定値が最小値より小さい場合
                If dblInit < hdblLimitLow Then .Rows(hintRowIndex).Cells("txtInit").Value = hdblLimitLow
                If dblLow < hdblLimitLow Then .Rows(hintRowIndex).Cells("txtLowLimit").Value = hdblLimitLow
                If dblHigh < hdblLimitLow Then .Rows(hintRowIndex).Cells("txtHighLimit").Value = hdblLimitLow

                ''設定値が最大値より大きい場合
                Select Case hshtSelectType

                    Case gCstCodeExtTimerTimerDispMin

                        ''切替設定：「分」の場合
                        If dblInit > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtInit").Value = hdblLimitHigh.ToString("0.0")
                        If dblLow > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtLowLimit").Value = hdblLimitHigh.ToString("0.0")
                        If dblHigh > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtHighLimit").Value = hdblLimitHigh.ToString("0.0")

                    Case Else

                        ''切替設定：「秒」の場合
                        If dblInit > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtInit").Value = hdblLimitHigh
                        If dblLow > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtLowLimit").Value = hdblLimitHigh
                        If dblHigh > hdblLimitHigh Then .Rows(hintRowIndex).Cells("txtHighLimit").Value = hdblLimitHigh

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "分/秒の表示切り換え"

    '--------------------------------------------------------------------
    ' 機能      : 時刻換算処理
    ' 返り値    : 換算後の時間（秒）
    ' 引き数    : ARG1 - (I ) 分/秒 切替設定
    '           : ARG2 - (I ) 設定時刻
    ' 機能説明  : 設定時刻の保存は「秒」で行う
    '--------------------------------------------------------------------
    Private Function mSaveTimeConvSec(ByVal hshtSelectTime As Short, _
                                      ByVal hdblTime As Double) As Short

        Dim shtRtn As Short = 0

        Try

            ''時間換算処理
            Select Case hshtSelectTime
                Case gCstCodeExtTimerTimerDispMin

                    ''分設定
                    shtRtn = (hdblTime * 60)

                Case Else

                    ''秒設定、新規追加項目
                    shtRtn = hdblTime

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return shtRtn
        End Try

        Return shtRtn

    End Function



#Region "分秒表示切換えイベント"

    '----------------------------------------------------------------------------
    ' 機能      : 分/秒の表示切り換え
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理行Index
    '           : ARG2 - (I ) 種類
    '           : ARG3 - (I ) 分/秒 切替設定
    ' 機能説明  : 時刻表示切替えコンボボックス操作時の分/秒表示切り換え
    '----------------------------------------------------------------------------
    Private Sub mConvDispTime(ByVal hintRowIndex As Integer, _
                              ByVal hshtSelectType As Short, _
                              ByVal hshtSelectTime As Short)

        ''----------------------------------------------------
        ''  時間換算フローについて
        ''----------------------------------------------------
        ''                              ①Ｘ60
        ''    　　⑤　②　④            ②÷60
        ''  その他→秒→分→その他      ③÷60
        ''  その他←秒←分←その他      ④Ｘ60
        ''  　　　⑥　①　③            ⑤変換処理なし
        ''                              ⑥変換処理なし
        ''
        ''  ※その他：新規追加項目。[秒]扱いとする

        Try

            Select Case hshtSelectTime
                Case gCstCodeExtTimerTimerDispSec

                    ''------------------------
                    '' 秒表示
                    ''------------------------
                    mintInitFlg = True

                    If (mshtSelectTimePrv(hintRowIndex) <> gCstCodeExtTimerTimerDispSec And _
                        mshtSelectTimePrv(hintRowIndex) <> gCstCodeExtTimerTimerDispMin) Then

                        ''⑤その他→秒（変換処理なし）
                        grdTimer.Rows(hintRowIndex).Cells("txtInit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtInit").Value)
                        grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value)
                        grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value)

                    Else

                        ''①分→秒（Ｘ60）
                        grdTimer.Rows(hintRowIndex).Cells("txtInit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtInit").Value))
                        grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value))
                        grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value))

                    End If

                    mintInitFlg = False

                Case gCstCodeExtTimerTimerDispMin

                    ''------------------------
                    '' 分表示
                    ''------------------------
                    mintInitFlg = True

                    ''②秒→分      （÷60）
                    ''③その他→分  （÷60）
                    grdTimer.Rows(hintRowIndex).Cells("txtInit").Value = mConvTimeToMin(CCShort(grdTimer.Rows(hintRowIndex).Cells("txtInit").Value))
                    grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value = mConvTimeToMin(CCShort(grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value))
                    grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value = mConvTimeToMin(CCShort(grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value))

                    mintInitFlg = False

                Case Else

                    ''------------------------
                    '' 新規追加項目
                    ''------------------------
                    mintInitFlg = True

                    If mshtSelectTimePrv(hintRowIndex) = gCstCodeExtTimerTimerDispSec Then

                        ''⑥秒→その他（変換処理なし）
                        grdTimer.Rows(hintRowIndex).Cells("txtInit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtInit").Value)
                        grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value)
                        grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value = CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value)

                    ElseIf mshtSelectTimePrv(hintRowIndex) = gCstCodeExtTimerTimerDispMin Then

                        ''④分→その他（Ｘ60）
                        grdTimer.Rows(hintRowIndex).Cells("txtInit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtInit").Value))
                        grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtLowLimit").Value))
                        grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value = mConvTimeToSec(hshtSelectTime, CCDouble(grdTimer.Rows(hintRowIndex).Cells("txtHighLimit").Value))

                    End If

                    mintInitFlg = False

            End Select

            ''分/秒 切替設定 前回値保持
            mshtSelectTimePrv(hintRowIndex) = hshtSelectTime

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''秒→分
    Private Function mConvTimeToMin(ByVal hshtTime As Short) As String

        Dim strRtn As String = "0.0"
        Dim dblBuf As Double = 0.0

        Try

            ''時間換算処理（秒→分）
            dblBuf = (hshtTime / 60).ToString("0.0")

            strRtn = dblBuf.ToString("0.0")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return strRtn
        End Try

        Return strRtn

    End Function

    ''分→秒
    Private Function mConvTimeToSec(ByVal hshtSelectTime As Short, _
                                    ByVal hdblTime As Double) As Short

        Dim shtRtn As Short = 0

        Try

            ''時間換算処理（分→秒）
            shtRtn = hdblTime * 60

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return shtRtn
        End Try

        Return shtRtn

    End Function

#End Region


#End Region



#End Region

End Class
