Public Class frmSysOps

#Region "変数定義"

    Private mudtSetSysOpsNew As gTypSetSysOps

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
    Private Sub frmSysOps_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '■外販
            '外販の場合
            If gintNaiGai = 1 Then
                Label11.Visible = False
                cmbSystemAlarm.Visible = False
            End If

            ''コンボボックス初期設定
            Call gSetComboBox(cmbChSetup, gEnmComboType.ctSysOpsChSetup)
            Call gSetComboBox(cmbChEdit, gEnmComboType.ctSysOpsChEdit)
            Call gSetComboBox(cmbSystemAlarm, gEnmComboType.ctSysOpsSystemAlarm)
            Call gSetComboBox(cmbDutySetting, gEnmComboType.ctSysOpsDutySetting)

            Call gSetComboBox(cmbAutoAlarmOrder, gEnmComboType.ctSysOpsAutoAlarmOrder)    '2015/5/27 T.Ueki

            ''グリッド初期設定
            Call mInitialDataGrid()

            ''構造体配列初期化
            mudtSetSysOpsNew.InitArray()
            For i As Integer = LBound(mudtSetSysOpsNew.udtOpsDetail) To UBound(mudtSetSysOpsNew.udtOpsDetail)
                mudtSetSysOpsNew.udtOpsDetail(i).InitArray()
            Next


            '' Ver1.12.0.6 2017.02.10 他の画面で設定したﾌﾗｸﾞの設定
            mudtSetSysOpsNew.shtTagMode = gudt.SetSystem.udtSysOps.shtTagMode       '' Tag
            mudtSetSysOpsNew.shtLRMode = gudt.SetSystem.udtSysOps.shtLRMode     '' ｱﾗｰﾑﾚﾍﾞﾙ

            If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 0) Then    '' ﾋｽﾄﾘ 自動更新
                mudtSetSysOpsNew.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 0, True)
            End If

            If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 1) Then    '' Mach/Hull
                mudtSetSysOpsNew.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, True)
            End If
            mudtSetSysOpsNew.shtBS_CHNo = gudt.SetSystem.udtSysOps.shtBS_CHNo       '' BS OUT CHNo.
            mudtSetSysOpsNew.shtFS_CHNo = gudt.SetSystem.udtSysOps.shtFS_CHNo       '' FS OUT CHNo.
            ''//


            ''画面設定
            Call mSetDisplay(gudt.SetSystem.udtSysOps)

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

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysOpsNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysOpsNew, gudt.SetSystem.udtSysOps) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetSysOpsNew, gudt.SetSystem.udtSysOps)
                'Ver1.12.0.8 ﾌﾟﾘﾝﾀﾊﾟｰﾄ設定
                With gudt.SetSystem.udtSysSystem
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, fnSetCombinePrinter())
                End With

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

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
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysOpsNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysOpsNew, gudt.SetSystem.udtSysOps) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        Call mCopyStructure(mudtSetSysOpsNew, gudt.SetSystem.udtSysOps)
                        'Ver1.12.0.8 ﾌﾟﾘﾝﾀﾊﾟｰﾄ設定
                        With gudt.SetSystem.udtSysSystem
                            .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, fnSetCombinePrinter())
                        End With

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

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
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OPS type 選択処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOPS_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOPS.CellValueChanged

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            With grdOPS.Rows(e.RowIndex)

                If dgv.Columns(e.ColumnIndex).Name = "chkOPSTypeMachinery" Then    ''OPS Type Machinery Select

                    If .Cells("chkOPSTypeCargo").Value = False Then

                        .Cells("chkMCMachinery").Value = IIf(.Cells("chkOPSTypeMachinery").Value, True, False)

                    Else
                        If .Cells("chkOPSTypeMachinery").Value Then

                        Else
                            If .Cells("chkMCMachinery").Value Then
                                .Cells("chkMCCargo").Value = True
                                .Cells("chkMCMachinery").Value = False
                            Else
                                .Cells("chkMCMachinery").Value = False
                                .Cells("chkMCCargo").Value = True
                            End If

                        End If

                    End If

                ElseIf dgv.Columns(e.ColumnIndex).Name = "chkOPSTypeCargo" Then ''OPS Type Cargo Select

                    If .Cells("chkOPSTypeMachinery").Value = False Then

                        .Cells("chkMCCargo").Value = IIf(.Cells("chkOPSTypeCargo").Value, True, False)

                    Else
                        If .Cells("chkOPSTypeCargo").Value Then

                        Else
                            If .Cells("chkOPSTypeMachinery").Value Then
                                .Cells("chkMCMachinery").Value = True
                                .Cells("chkMCCargo").Value = False
                            Else
                                .Cells("chkOPSTypeMachinery").Value = False
                                .Cells("chkOPSTypeCargo").Value = True
                            End If

                        End If

                    End If

                End If

                ''OPS Type のMachineryとCargoが共に選択されている場合のみ M/C の手動設定が可となる
                If .Cells("chkOPSTypeMachinery").Value And .Cells("chkOPSTypeCargo").Value Then
                    .Cells("chkMCMachinery").ReadOnly = False
                    .Cells("chkMCCargo").ReadOnly = False
                Else
                    .Cells("chkMCMachinery").ReadOnly = True
                    .Cells("chkMCCargo").ReadOnly = True
                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド　クリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOPS_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOPS.CellContentClick

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''グリッドの保留中の変更を全て適用させる
            dgv.EndEdit()

            ''M/Cが手動設定不可の場合は抜ける
            If grdOPS.Rows(e.RowIndex).Cells("chkMCMachinery").ReadOnly And grdOPS.Rows(e.RowIndex).Cells("chkMCCargo").ReadOnly Then
                Exit Sub
            End If

            ''M/CのMachineryとCargoをトグルにする
            If dgv.Columns(e.ColumnIndex).Name = "chkMCMachinery" Then ''M/C Machinery Select

                grdOPS.Rows(e.RowIndex).Cells("chkMCCargo").Value = False

            ElseIf dgv.Columns(e.ColumnIndex).Name = "chkMCCargo" Then ''M/C Cargo Select

                grdOPS.Rows(e.RowIndex).Cells("chkMCMachinery").Value = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド　ダブルクリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOPS_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOPS.CellContentDoubleClick

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''グリッドの保留中の変更を全て適用させる
            dgv.EndEdit()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドの横スクロールを連動させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOPS_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdOPS.Scroll

        Try

            grdHead.HorizontalScrollingOffset = grdOPS.HorizontalScrollingOffset

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOPS_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOPS.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdOPS_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdOPS_KeyPress

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
    Private Sub grdOPS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdOPS.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdOPS.CurrentCell.OwningColumn.Name

            ''[Proh Flag]
            If strColumnName = "txtProhFlag" Then
                'e.Handled = gCheckTextInput(1, sender, e.KeyChar, , , , , "0,1,2")
                e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,A,B,C,D,E,F")

                ''---------------------------------
                '' 入力値が小文字のa～fの場合
                ''---------------------------------
                If (e.KeyChar >= "a"c And e.KeyChar <= "f"c) Then

                    ''文字を大文字に変換
                    Select Case e.KeyChar
                        Case "a"c : e.KeyChar = "A"c
                        Case "b"c : e.KeyChar = "B"c
                        Case "c"c : e.KeyChar = "C"c
                        Case "d"c : e.KeyChar = "D"c
                        Case "e"c : e.KeyChar = "E"c
                        Case "f"c : e.KeyChar = "F"c
                    End Select

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub grdOPS_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdOPS.CellValidating

    '    Dim dgv As DataGridView = CType(sender, DataGridView)
    '    Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

    '    ''[Proh Flag]
    '    If strColumnName = "txtProhFlag" Then
    '        e.Cancel = gChkTextNumSpan(0, 2, e.FormattedValue)
    '    End If

    'End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            For i As Integer = 0 To grdOPS.RowCount - 1

                '=============
                ''ProhFlag
                '=============
                ''共通数値入力チェック
                'If Not gChkInputNum(grdOPS.Rows(i).Cells("txtProhFlag"), 0, 2, "ProhFlag", i + 1, True, True) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) OPS共通設定構造体
    ' 　　　    : ARG2 - ( O) OPS詳細設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSysOps)

        Try
            'Ver2.0.6.4 chkControlFunctionは、一覧のControl OPS Functionが一個でもONならON、0個ならOFFへ
            Dim blOPSfunc As Boolean = False

            With udtSet

                'Ver2.0.6.4 chkControlFunctionは、一覧判定後に格納
                '遠隔操作
                '.shtControl = IIf(chkControlFunction.Checked, 1, 0)

                ''Extグループ、グループリポーズ変更許可
                .shtProhibition = cmbChSetup.SelectedValue

                ''CHデータ変更許可
                .shtChannelEdit = cmbChEdit.SelectedValue

                ''アラーム表示方法
                .shtAlarm = cmbSystemAlarm.SelectedValue

                ''Duty表示フラグ
                .shtDuty = cmbDutySetting.SelectedValue

                ''コントロール　1台インターロック  2015.01.19
                .shtContOnlyFlag = IIf(chkControlOnly.Checked, 1, 0)

                ''Auto Alarm表示順序　2015/5/27 T.Ueki
                .shtAlarm_Order = cmbAutoAlarmOrder.SelectedValue

                ''SET権レベル有　Ver1.11.8.8 2016.11.17
                .shtSystem = gBitSet(.shtSystem, 2, chkSetLV.Checked)

                ''グリッド内容
                For i As Integer = 0 To grdOPS.Rows.Count - 1

                    With .udtOpsDetail(i)

                        .shtExist = IIf(grdOPS.Rows(i).Cells("chkEfect").Value, 1, 0)
                        .shtAlarmDisp = grdOPS.Rows(i).Cells("cmbSetup").Value
                        .shtEnable = IIf(grdOPS.Rows(i).Cells("chkEnableOpsIni").Value, 1, 0)
                        .shtControl = gBitSet(.shtControl, 0, IIf(grdOPS.Rows(i).Cells("chkFunc").Value, True, False))

                        'Ver2.0.6.4
                        If grdOPS.Rows(i).Cells("chkFunc").Value = True Then
                            blOPSfunc = True
                        End If

                        .shtControl = gBitSet(.shtControl, 1, IIf(grdOPS.Rows(i).Cells("chkControlOpsIni").Value, True, False))
                        .shtControlFlag = gBitSet(.shtControlFlag, 0, IIf(grdOPS.Rows(i).Cells("chk1").Value, True, False))
                        .shtControlFlag = gBitSet(.shtControlFlag, 1, IIf(grdOPS.Rows(i).Cells("chk2").Value, True, False))
                        .shtControlFlag = gBitSet(.shtControlFlag, 2, IIf(grdOPS.Rows(i).Cells("chk4").Value, True, False))
                        .shtControlFlag = gBitSet(.shtControlFlag, 3, IIf(grdOPS.Rows(i).Cells("chk8").Value, True, False))
                        'Ver2.0.7.R
                        .shtControlFlag = gBitSet(.shtControlFlag, 5, IIf(grdOPS.Rows(i).Cells("chk32").Value, True, False))
                        .shtControlFlag = gBitSet(.shtControlFlag, 6, IIf(grdOPS.Rows(i).Cells("chk64").Value, True, False))
                        '-
                        .shtControlProhFlag = CCUInt16Hex(grdOPS.Rows(i).Cells("txtProhFlag").Value)
                        .shtOperaionPanel = IIf(grdOPS.Rows(i).Cells("chkPanel").Value, 1, 0)
                        .shtAdjustLight = IIf(grdOPS.Rows(i).Cells("chkCCTV").Value, 1, 0)
                        '' Ver1.11.8.2 2016.11.01 LCDType
                        ''.shtHatteland = IIf(grdOPS.Rows(i).Cells("chkHATTELAND").Value, 1, 0)
                        .shtHatteland = grdOPS.Rows(i).Cells("chkHATTELAND").Value
                        ''..
                        .shtPrintPart = gBitSet(.shtPrintPart, 0, IIf(grdOPS.Rows(i).Cells("chkPrintPartMachinery").Value, True, False))
                        .shtPrintPart = gBitSet(.shtPrintPart, 1, IIf(grdOPS.Rows(i).Cells("chkPrintPartCargo").Value, True, False))
                        .shtOpsType = gBitSet(.shtOpsType, 0, IIf(grdOPS.Rows(i).Cells("chkOPSTypeMachinery").Value, True, False))
                        .shtOpsType = gBitSet(.shtOpsType, 1, IIf(grdOPS.Rows(i).Cells("chkOPSTypeCargo").Value, True, False))
                        .shtBootMode = gBitSet(.shtBootMode, 0, IIf(grdOPS.Rows(i).Cells("chkMCMachinery").Value, True, False))
                        .shtBootMode = gBitSet(.shtBootMode, 1, IIf(grdOPS.Rows(i).Cells("chkMCCargo").Value, True, False))
                        .shtRepSum = IIf(grdOPS.Rows(i).Cells("chkRepSummary").Value, 1, 0)
                        .shtEtherA = IIf(grdOPS.Rows(i).Cells("chkEthernetline").Value, 1, 0)
                        .shtResolution = grdOPS.Rows(i).Cells("cmbResolution").Value
                        .shtSysSet = gBitSet(.shtSysSet, 0, IIf(grdOPS.Rows(i).Cells("chkBSFS").Value, True, False))      '' Ver1.9.3 2016.01.22 追加

                    End With

                Next

                'Ver2.0.6.4
                chkControlFunction.Checked = blOPSfunc
                .shtControl = IIf(chkControlFunction.Checked, 1, 0)

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
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSysOps)

        Try

            With udtSet

                ''遠隔操作
                chkControlFunction.Checked = IIf(.shtControl = 1, True, False)

                ''Extグループ、グループリポーズ変更許可
                cmbChSetup.SelectedValue = .shtProhibition

                ''CHデータ変更許可
                cmbChEdit.SelectedValue = .shtChannelEdit

                ''アラーム表示方法
                cmbSystemAlarm.SelectedValue = .shtAlarm

                ''Duty表示フラグ
                cmbDutySetting.SelectedValue = .shtDuty

                ''コントロール　1台インターロック  2015.01.19
                chkControlOnly.Checked = IIf(.shtContOnlyFlag = 1, True, False)

                ''SET権レベル有　Ver1.11.8.8 2016.11.17
                chkSetLV.Checked = gBitCheck(.shtSystem, 2)

                ''Auto Alarm表示順序　2015/5/27 T.Ueki
                cmbAutoAlarmOrder.SelectedValue = .shtAlarm_Order

                ''グリッド内容
                For i As Integer = 0 To grdOPS.Rows.Count - 1

                    With .udtOpsDetail(i)

                        grdOPS.Rows(i).Cells("chkEfect").Value = IIf(.shtExist = 1, True, False)
                        grdOPS.Rows(i).Cells("cmbSetup").Value = CStr(.shtAlarmDisp)
                        grdOPS.Rows(i).Cells("chkEnableOpsIni").Value = IIf(.shtEnable = 1, True, False)
                        grdOPS.Rows(i).Cells("chkFunc").Value = IIf(gBitCheck(.shtControl, 0), True, False)
                        grdOPS.Rows(i).Cells("chkControlOpsIni").Value = IIf(gBitCheck(.shtControl, 1), True, False)
                        grdOPS.Rows(i).Cells("chk1").Value = IIf(gBitCheck(.shtControlFlag, 0), True, False)
                        grdOPS.Rows(i).Cells("chk2").Value = IIf(gBitCheck(.shtControlFlag, 1), True, False)
                        grdOPS.Rows(i).Cells("chk4").Value = IIf(gBitCheck(.shtControlFlag, 2), True, False)
                        grdOPS.Rows(i).Cells("chk8").Value = IIf(gBitCheck(.shtControlFlag, 3), True, False)
                        'Ver2.0.7.R
                        grdOPS.Rows(i).Cells("chk32").Value = IIf(gBitCheck(.shtControlFlag, 5), True, False)
                        grdOPS.Rows(i).Cells("chk64").Value = IIf(gBitCheck(.shtControlFlag, 6), True, False)
                        '-
                        grdOPS.Rows(i).Cells("txtProhFlag").Value = Hex(.shtControlProhFlag)
                        grdOPS.Rows(i).Cells("chkPanel").Value = IIf(.shtOperaionPanel = 1, True, False)
                        grdOPS.Rows(i).Cells("chkCCTV").Value = IIf(.shtAdjustLight = 1, True, False)
                        '' Ver1.11.8.2 2016.11.01 LCDType 変更
                        ''grdOPS.Rows(i).Cells("chkHATTELAND").Value = IIf(.shtHatteland = 1, True, False)
                        grdOPS.Rows(i).Cells("chkHATTELAND").Value = CStr(.shtHatteland)
                        ''
                        grdOPS.Rows(i).Cells("chkPrintPartMachinery").Value = IIf(gBitCheck(.shtPrintPart, 0), True, False)
                        grdOPS.Rows(i).Cells("chkPrintPartCargo").Value = IIf(gBitCheck(.shtPrintPart, 1), True, False)
                        grdOPS.Rows(i).Cells("chkOPSTypeMachinery").Value = IIf(gBitCheck(.shtOpsType, 0), True, False)
                        grdOPS.Rows(i).Cells("chkOPSTypeCargo").Value = IIf(gBitCheck(.shtOpsType, 1), True, False)
                        grdOPS.Rows(i).Cells("chkMCMachinery").Value = IIf(gBitCheck(.shtBootMode, 0), True, False)
                        grdOPS.Rows(i).Cells("chkMCCargo").Value = IIf(gBitCheck(.shtBootMode, 1), True, False)
                        grdOPS.Rows(i).Cells("chkRepSummary").Value = IIf(.shtRepSum = 1, True, False)
                        grdOPS.Rows(i).Cells("chkEthernetline").Value = IIf(.shtEtherA = 1, True, False)
                        grdOPS.Rows(i).Cells("cmbResolution").Value = CStr(.shtResolution)
                        grdOPS.Rows(i).Cells("chkBSFS").Value = IIf(gBitCheck(.shtSysSet, 0), True, False)    '' Ver1.9.3 2016.01.22 追加

                    End With

                Next

                'Ver1.12.0.8 コンバインプリンタ設定表示
                If gBitCheck(gudt.SetSystem.udtSysSystem.shtCombineSeparate, 1) = True Then
                    lblCombinePr.Text = "Combine Printer ON"
                Else
                    lblCombinePr.Text = ""
                End If
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            '■外販
            '外販の場合 一覧で表示する項目がExist,Alarm Display,Enable OPS,Control OPS,Equipmentのみ
            Dim hColumn1 As New DataGridViewCheckBoxColumn : hColumn1.Name = "Col1"
            Dim hColumn2 As New DataGridViewCheckBoxColumn : hColumn2.Name = "Col2"
            Dim hColumn3 As New DataGridViewCheckBoxColumn : hColumn3.Name = "Col3"
            Dim hColumn4 As New DataGridViewCheckBoxColumn : hColumn4.Name = "Col4"
            Dim hColumn5 As New DataGridViewCheckBoxColumn : hColumn5.Name = "Col5"
            Dim hColumn6 As New DataGridViewCheckBoxColumn : hColumn6.Name = "Col6"
            Dim hColumn7 As New DataGridViewCheckBoxColumn : hColumn7.Name = "Col7"
            Dim hColumn8 As New DataGridViewCheckBoxColumn : hColumn8.Name = "Col8"
            Dim hColumn9 As New DataGridViewCheckBoxColumn : hColumn9.Name = "Col9"
            Dim hColumn10 As New DataGridViewCheckBoxColumn : hColumn10.Name = "Col10"
            Dim hColumn11 As New DataGridViewCheckBoxColumn : hColumn11.Name = "Col11"
            Dim hColumn12 As New DataGridViewCheckBoxColumn : hColumn12.Name = "Col12"
            Dim hColumn13 As New DataGridViewCheckBoxColumn : hColumn13.Name = "Col13"
            Dim hColumn14 As New DataGridViewCheckBoxColumn : hColumn14.Name = "Col14"
            Dim hColumn15 As New DataGridViewCheckBoxColumn : hColumn15.Name = "Col15"
            Dim hColumn16 As New DataGridViewCheckBoxColumn : hColumn16.Name = "Col16"
            Dim hColumn17 As New DataGridViewCheckBoxColumn : hColumn17.Name = "Col17"
            Dim hColumn18 As New DataGridViewCheckBoxColumn : hColumn18.Name = "Col18"
            Dim hColumn19 As New DataGridViewCheckBoxColumn : hColumn19.Name = "Col19"
            Dim hColumn20 As New DataGridViewCheckBoxColumn : hColumn20.Name = "Col20"
            Dim hColumn21 As New DataGridViewCheckBoxColumn : hColumn21.Name = "Col21"
            Dim hColumn22 As New DataGridViewCheckBoxColumn : hColumn22.Name = "Col22"
            Dim hColumn23 As New DataGridViewCheckBoxColumn : hColumn23.Name = "Col23"
            Dim hColumn24 As New DataGridViewCheckBoxColumn : hColumn24.Name = "Col24"


            If gintNaiGai = 1 Then
                hColumn5.Visible = False
                hColumn6.Visible = False
                hColumn8.Visible = False
                hColumn9.Visible = False
                hColumn10.Visible = False
                hColumn11.Visible = False
                hColumn12.Visible = False
                hColumn13.Visible = False
                hColumn14.Visible = False
                hColumn15.Visible = False
                hColumn16.Visible = False
                hColumn17.Visible = False
                hColumn18.Visible = False
                hColumn19.Visible = False
                hColumn20.Visible = False
                hColumn21.Visible = False
                hColumn22.Visible = False
                hColumn23.Visible = False
                hColumn24.Visible = False
            End If


            With grdHead

                ''列
                .Columns.Clear()
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                '.Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())    '' Ver1.9.3 2016.01.22 追加
                .Columns.Add(hColumn1) : .Columns.Add(hColumn2)
                .Columns.Add(hColumn3) : .Columns.Add(hColumn4)
                .Columns.Add(hColumn5) : .Columns.Add(hColumn6)
                .Columns.Add(hColumn7) : .Columns.Add(hColumn8)
                .Columns.Add(hColumn9) : .Columns.Add(hColumn10)
                .Columns.Add(hColumn11) : .Columns.Add(hColumn12)
                .Columns.Add(hColumn13) : .Columns.Add(hColumn14)
                .Columns.Add(hColumn15) : .Columns.Add(hColumn16)
                .Columns.Add(hColumn17) : .Columns.Add(hColumn18)
                .Columns.Add(hColumn19) : .Columns.Add(hColumn20)
                .Columns.Add(hColumn21) : .Columns.Add(hColumn22)
                .Columns.Add(hColumn23) : .Columns.Add(hColumn24)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 60
                .Columns(1).HeaderText = "Alarm Display" : .Columns(1).Width = 100
                .Columns(2).HeaderText = "SET OPS" : .Columns(2).Width = 80
                .Columns(3).HeaderText = "Control OPS" : .Columns(3).Width = 120

                .Columns(4).HeaderText = "Control Affiliation Flag" : .Columns(4).Width = 240 'Ver2.0.7.R 160→240

                .Columns(5).HeaderText = "Control Affilia" : .Columns(5).Width = 100

                .Columns(6).HeaderText = "Equipment" : .Columns(6).Width = 80

                .Columns(7).HeaderText = "Adjust Light" : .Columns(7).Width = 90 '80
                .Columns(8).HeaderText = "" : .Columns(8).Width = 100       '' Ver1.11.8.2 2016.11.01  ｻｲｽﾞ 80 → 100
                .Columns(9).HeaderText = "Print Part" : .Columns(9).Width = 120
                .Columns(10).HeaderText = "OPS Type" : .Columns(10).Width = 120
                .Columns(11).HeaderText = "M/C" : .Columns(11).Width = 120
                .Columns(12).HeaderText = "Rep Summary" : .Columns(12).Width = 100
                .Columns(13).HeaderText = "Ethernet line" : .Columns(13).Width = 90 '84
                .Columns(14).HeaderText = "" : .Columns(14).Width = 100
                .Columns(15).HeaderText = "" : .Columns(15).Width = 40     '' Ver1.9.3 2016.01.22 追加
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                ''行ヘッダー
                .RowHeadersWidth = 100
                .ColumnHeadersHeightSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing


                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

            End With

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chkEfect"
            Dim Column2 As New DataGridViewComboBoxColumn : Column2.Name = "cmbSetup"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chkEnableOpsIni"
            Dim Column4 As New DataGridViewCheckBoxColumn : Column4.Name = "chkFunc"
            Dim Column5 As New DataGridViewCheckBoxColumn : Column5.Name = "chkControlOpsIni"
            Dim Column6 As New DataGridViewCheckBoxColumn : Column6.Name = "chk1"
            Dim Column7 As New DataGridViewCheckBoxColumn : Column7.Name = "chk2"
            Dim Column8 As New DataGridViewCheckBoxColumn : Column8.Name = "chk4"
            Dim Column9 As New DataGridViewCheckBoxColumn : Column9.Name = "chk8"
            'Ver2.0.7.R 32,64解放
            Dim Column24 As New DataGridViewCheckBoxColumn : Column24.Name = "chk32"
            Dim Column25 As New DataGridViewCheckBoxColumn : Column25.Name = "chk64"
            '-
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtProhFlag"
            Dim Column11 As New DataGridViewCheckBoxColumn : Column11.Name = "chkPanel"
            Dim Column12 As New DataGridViewCheckBoxColumn : Column12.Name = "chkCCTV"
            Dim Column13 As New DataGridViewComboBoxColumn : Column13.Name = "chkHATTELAND"     '' Ver1.11.8.2 2016.11.01  LCDType ｺﾝﾎﾞﾎﾞｯｸｽに変更
            Dim Column14 As New DataGridViewCheckBoxColumn : Column14.Name = "chkPrintPartMachinery"
            Dim Column15 As New DataGridViewCheckBoxColumn : Column15.Name = "chkPrintPartCargo"
            Dim Column16 As New DataGridViewCheckBoxColumn : Column16.Name = "chkOPSTypeMachinery"
            Dim Column17 As New DataGridViewCheckBoxColumn : Column17.Name = "chkOPSTypeCargo"
            Dim Column18 As New DataGridViewCheckBoxColumn : Column18.Name = "chkMCMachinery" : Column18.ReadOnly = True
            Dim Column19 As New DataGridViewCheckBoxColumn : Column19.Name = "chkMCCargo" : Column19.ReadOnly = True
            Dim Column20 As New DataGridViewCheckBoxColumn : Column20.Name = "chkRepSummary"
            Dim Column21 As New DataGridViewCheckBoxColumn : Column21.Name = "chkEthernetline"
            Dim Column22 As New DataGridViewComboBoxColumn : Column22.Name = "cmbResolution"
            Dim Column23 As New DataGridViewCheckBoxColumn : Column23.Name = "chkBSFS"  '' Ver1.9.3 2016.01.22 追加


            '■外販
            '外販の場合 一覧で表示する項目がExist,Alarm Display,Enable OPS,Control OPS,Equipmentのみ
            If gintNaiGai = 1 Then
                Column6.Visible = False
                Column7.Visible = False
                Column8.Visible = False
                Column9.Visible = False
                Column10.Visible = False
                Column12.Visible = False
                Column13.Visible = False
                Column14.Visible = False
                Column15.Visible = False
                Column16.Visible = False
                Column17.Visible = False
                Column18.Visible = False
                Column19.Visible = False
                Column20.Visible = False
                Column21.Visible = False
                Column22.Visible = False
                Column23.Visible = False
                'Ver2.0.7.R 32,64解放
                Column24.Visible = False
                Column25.Visible = False
                '-
            End If

            With grdOPS

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(Column4)
                .Columns.Add(Column5) : .Columns.Add(Column6) : .Columns.Add(Column7) : .Columns.Add(Column8)
                .Columns.Add(Column9)
                'Ver2.0.7.R
                .Columns.Add(Column24) : .Columns.Add(Column25)
                '-
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15) : .Columns.Add(Column16)
                .Columns.Add(Column17) : .Columns.Add(Column18) : .Columns.Add(Column19) : .Columns.Add(Column20)
                .Columns.Add(Column21) : .Columns.Add(Column22) : .Columns.Add(Column23)    '' Ver1.9.3 2016.01.22 追加
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Select" : .Columns(0).Width = 60
                .Columns(1).HeaderText = "Setting" : .Columns(1).Width = 100
                .Columns(2).HeaderText = "initial" : .Columns(2).Width = 80
                .Columns(3).HeaderText = "Func" : .Columns(3).Width = 60
                .Columns(4).HeaderText = "initial" : .Columns(4).Width = 60
                .Columns(5).HeaderText = "1" : .Columns(5).Width = 40
                .Columns(6).HeaderText = "2" : .Columns(6).Width = 40
                .Columns(7).HeaderText = "4" : .Columns(7).Width = 40
                .Columns(8).HeaderText = "8" : .Columns(8).Width = 40

                'Ver2.0.7.R
                .Columns(9).HeaderText = "32" : .Columns(9).Width = 40
                .Columns(10).HeaderText = "64" : .Columns(10).Width = 40
                '-

                'Ver2.0.7.R　添え字２後ろへ
                '.Columns(9).HeaderText = "Proh Flag(HEX)" : .Columns(9).Width = 100
                ''.Columns(10).HeaderText = "Operation Panel" : .Columns(10).Width = 80
                '.Columns(10).HeaderText = "Keyboard" : .Columns(10).Width = 80  'Ver2.0.4.0 名称変更
                '.Columns(11).HeaderText = "Set" : .Columns(11).Width = 90 '80
                '.Columns(12).HeaderText = "LCDType" : .Columns(12).Width = 100 '' Ver1.11.8.2 2016.11.01  名称 "HATTELAND" → "LCDType"/ ｻｲｽﾞ 80 → 100
                '.Columns(13).HeaderText = "Machinery" : .Columns(13).Width = 60
                '.Columns(14).HeaderText = "Cargo" : .Columns(14).Width = 60
                '.Columns(15).HeaderText = "Machinery" : .Columns(15).Width = 60
                '.Columns(16).HeaderText = "Cargo" : .Columns(16).Width = 60
                '.Columns(17).HeaderText = "Machinery" : .Columns(17).Width = 60
                '.Columns(18).HeaderText = "Cargo" : .Columns(18).Width = 60
                '.Columns(19).HeaderText = "(ALL)" : .Columns(19).Width = 100
                '.Columns(20).HeaderText = "A Only" : .Columns(20).Width = 90 '84
                '.Columns(21).HeaderText = "Resolution" : .Columns(21).Width = 100
                '.Columns(22).HeaderText = "BS/FS" : .Columns(22).Width = 40
                .Columns(11).HeaderText = "Proh Flag(HEX)" : .Columns(11).Width = 100
                .Columns(12).HeaderText = "Keyboard" : .Columns(12).Width = 80
                .Columns(13).HeaderText = "Set" : .Columns(13).Width = 90
                .Columns(14).HeaderText = "LCDType" : .Columns(14).Width = 100
                .Columns(15).HeaderText = "Machinery" : .Columns(15).Width = 60
                .Columns(16).HeaderText = "Cargo" : .Columns(16).Width = 60
                .Columns(17).HeaderText = "Machinery" : .Columns(17).Width = 60
                .Columns(18).HeaderText = "Cargo" : .Columns(18).Width = 60
                .Columns(19).HeaderText = "Machinery" : .Columns(19).Width = 60
                .Columns(20).HeaderText = "Cargo" : .Columns(20).Width = 60
                .Columns(21).HeaderText = "(ALL)" : .Columns(21).Width = 100
                .Columns(22).HeaderText = "A Only" : .Columns(22).Width = 90
                .Columns(23).HeaderText = "Resolution" : .Columns(23).Width = 100
                .Columns(24).HeaderText = "BS/FS" : .Columns(24).Width = 40
                '-
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 11
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = "OPS #" & i.ToString
                Next
                .RowHeadersWidth = 100

                ''コンボボックス初期設定
                Call gSetComboBox(Column2, gEnmComboType.ctSysOpsAlarmDisplay)
                Call gSetComboBox(Column22, gEnmComboType.ctSysOpsResolution)
                Call gSetComboBox(Column13, gEnmComboType.ctSysOpsLcdType)      '' Ver1.11.8.2 2016.11.01  LCDType ｺﾝﾎﾞﾎﾞｯｸｽに変更

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
                .ScrollBars = ScrollBars.Horizontal

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdOPS)

            End With

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSysOps, _
                               ByRef udtTarget As gTypSetSysOps)

        Try

            With udtTarget

                udtTarget.shtAlarm = udtSource.shtAlarm
                udtTarget.shtDuty = udtSource.shtDuty
                udtTarget.shtChannelEdit = udtSource.shtChannelEdit
                udtTarget.shtControl = udtSource.shtControl
                udtTarget.shtProhibition = udtSource.shtProhibition
                udtTarget.shtContOnlyFlag = udtSource.shtContOnlyFlag   '' 2015.01.19
                udtTarget.shtAlarm_Order = udtSource.shtAlarm_Order   '' 2015/5/27 T.Ueki

                'Ver2.0.0.0 2016.12.05 DEL START ﾛｲﾄﾞとﾀｸﾞはここでは、無関係のため更新しないように削除
                'udtTarget.shtTagMode = udtSource.shtTagMode     '' 2015.10.22 Ver1.7.5 ﾀｸﾞ表示ﾓｰﾄﾞ追加
                'udtTarget.shtLRMode = udtSource.shtLRMode       '' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ表示ﾓｰﾄﾞ追加
                'Ver2.0.0.0 2016.12.05 DEL END ﾛｲﾄﾞとﾀｸﾞはここでは、無関係のため更新しないように削除

                udtTarget.shtSystem = udtSource.shtSystem       '' Ver1.11.8.8 2016.11.17 SET権レベル対応


                '' Ver1.12.0.6 2017.02.10  変更漏れのため追加
                udtTarget.shtBS_CHNo = udtSource.shtBS_CHNo       '' BS OUT CHNo.
                udtTarget.shtFS_CHNo = udtSource.shtFS_CHNo       '' FS OUT CHNo.
                ''//

                For i As Integer = 0 To grdOPS.Rows.Count - 1
                    udtTarget.udtOpsDetail(i) = udtSource.udtOpsDetail(i)
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSysOps, _
                                         ByVal udt2 As gTypSetSysOps) As Boolean

        Try

            If udt1.shtAlarm <> udt2.shtAlarm Then Return False
            If udt1.shtDuty <> udt2.shtDuty Then Return False
            If udt1.shtChannelEdit <> udt2.shtChannelEdit Then Return False
            If udt1.shtControl <> udt2.shtControl Then Return False
            If udt1.shtProhibition <> udt2.shtProhibition Then Return False
            If udt1.shtContOnlyFlag <> udt2.shtContOnlyFlag Then Return False '' 2015.01.19
            If udt1.shtAlarm_Order <> udt2.shtAlarm_Order Then Return False '' 2015/5/27 T.Ueki
            If udt1.shtSystem <> udt2.shtSystem Then Return False '' Ver1.11.8.8 2016.11.17 SET権レベル対応

            For i As Integer = 0 To grdOPS.Rows.Count - 1

                If udt1.udtOpsDetail(i).shtAdjustLight <> udt2.udtOpsDetail(i).shtAdjustLight Then Return False
                If udt1.udtOpsDetail(i).shtAlarmDisp <> udt2.udtOpsDetail(i).shtAlarmDisp Then Return False
                If udt1.udtOpsDetail(i).shtBootMode <> udt2.udtOpsDetail(i).shtBootMode Then Return False
                If udt1.udtOpsDetail(i).shtControl <> udt2.udtOpsDetail(i).shtControl Then Return False
                If udt1.udtOpsDetail(i).shtControlFlag <> udt2.udtOpsDetail(i).shtControlFlag Then Return False
                If udt1.udtOpsDetail(i).shtControlProhFlag <> udt2.udtOpsDetail(i).shtControlProhFlag Then Return False
                If udt1.udtOpsDetail(i).shtEnable <> udt2.udtOpsDetail(i).shtEnable Then Return False
                If udt1.udtOpsDetail(i).shtEtherA <> udt2.udtOpsDetail(i).shtEtherA Then Return False
                If udt1.udtOpsDetail(i).shtExist <> udt2.udtOpsDetail(i).shtExist Then Return False
                If udt1.udtOpsDetail(i).shtHatteland <> udt2.udtOpsDetail(i).shtHatteland Then Return False
                If udt1.udtOpsDetail(i).shtOpsType <> udt2.udtOpsDetail(i).shtOpsType Then Return False
                If udt1.udtOpsDetail(i).shtOperaionPanel <> udt2.udtOpsDetail(i).shtOperaionPanel Then Return False
                If udt1.udtOpsDetail(i).shtPrintPart <> udt2.udtOpsDetail(i).shtPrintPart Then Return False
                If udt1.udtOpsDetail(i).shtRepSum <> udt2.udtOpsDetail(i).shtRepSum Then Return False
                If udt1.udtOpsDetail(i).shtResolution <> udt2.udtOpsDetail(i).shtResolution Then Return False
                If udt1.udtOpsDetail(i).shtSysSet <> udt2.udtOpsDetail(i).shtSysSet Then Return False '' Ver1.9.3 2016.01.22 追加

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class

