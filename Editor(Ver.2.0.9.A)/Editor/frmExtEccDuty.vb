Public Class frmExtEccDuty

#Region "変数定義"

    Private mudtSetExtAlmNew As gTypSetExtCommon = Nothing

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
    Private Sub frmExtEccDuty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'コンバイン仕様選択時のみ表示される
            If frmExtMenu.optCombine.Checked Then
                fraDutyPartChoice.Enabled = True
            Else
                fraDutyPartChoice.Enabled = False
            End If

            ''画面設定
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarmCommon)

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
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

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
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmExtEccDuty_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

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
                        Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

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
    Private Sub frmExtEccDuty_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtCommon)

        Try

            With udtSet

                ''Duty機能 有無
                .shtDutyFunc = IIf(optDutyFunction1.Checked, 1, 0)

                ''川汽仕様（特殊仕様）設定
                .shtDutyMethod = IIf(optDutyMethod1.Checked, 1, 0)

                ''Group Effect機能
                If optGroupEffectFnc1.Checked Then : .shtEffect = gCstCodeExtDutyEffectExtOutput : End If
                If optGroupEffectFnc2.Checked Then : .shtEffect = gCstCodeExtDutyEffectFix : End If
                If optGroupEffectFnc3.Checked Then : .shtEffect = gCstCodeExtDutyEffectNormal : End If

                ''NVルール
                .shtNv = IIf(optNvRule1.Checked, 1, 0)

                ''DutyPart選択
                .shtPart1 = IIf(chkMach1.Checked, gBitSet(udtSet.shtPart1, 0, True), gBitSet(udtSet.shtPart1, 0, False))
                .shtPart1 = IIf(chkMach2.Checked, gBitSet(udtSet.shtPart1, 1, True), gBitSet(udtSet.shtPart1, 1, False))
                .shtPart1 = IIf(chkMach3.Checked, gBitSet(udtSet.shtPart1, 2, True), gBitSet(udtSet.shtPart1, 2, False))
                .shtPart1 = IIf(chkMach4.Checked, gBitSet(udtSet.shtPart1, 3, True), gBitSet(udtSet.shtPart1, 3, False))
                .shtPart1 = IIf(chkMach5.Checked, gBitSet(udtSet.shtPart1, 4, True), gBitSet(udtSet.shtPart1, 4, False))
                .shtPart1 = IIf(chkMach6.Checked, gBitSet(udtSet.shtPart1, 5, True), gBitSet(udtSet.shtPart1, 5, False))
                .shtPart1 = IIf(chkMach7.Checked, gBitSet(udtSet.shtPart1, 6, True), gBitSet(udtSet.shtPart1, 6, False))
                .shtPart1 = IIf(chkMach8.Checked, gBitSet(udtSet.shtPart1, 7, True), gBitSet(udtSet.shtPart1, 7, False))
                .shtPart1 = IIf(chkMach9.Checked, gBitSet(udtSet.shtPart1, 8, True), gBitSet(udtSet.shtPart1, 8, False))
                .shtPart1 = IIf(chkMach10.Checked, gBitSet(udtSet.shtPart1, 9, True), gBitSet(udtSet.shtPart1, 9, False))
                .shtPart1 = IIf(chkMach11.Checked, gBitSet(udtSet.shtPart1, 10, True), gBitSet(udtSet.shtPart1, 10, False))
                .shtPart1 = IIf(chkMach12.Checked, gBitSet(udtSet.shtPart1, 11, True), gBitSet(udtSet.shtPart1, 11, False))
                .shtPart1 = IIf(chkMach13.Checked, gBitSet(udtSet.shtPart1, 12, True), gBitSet(udtSet.shtPart1, 12, False))
                .shtPart1 = IIf(chkMach14.Checked, gBitSet(udtSet.shtPart1, 13, True), gBitSet(udtSet.shtPart1, 13, False))
                .shtPart1 = IIf(chkMach15.Checked, gBitSet(udtSet.shtPart1, 14, True), gBitSet(udtSet.shtPart1, 14, False))

                ''DutyPart選択
                .shtPart2 = IIf(chkCargo1.Checked, gBitSet(udtSet.shtPart2, 0, True), gBitSet(udtSet.shtPart2, 0, False))
                .shtPart2 = IIf(chkCargo2.Checked, gBitSet(udtSet.shtPart2, 1, True), gBitSet(udtSet.shtPart2, 1, False))
                .shtPart2 = IIf(chkCargo3.Checked, gBitSet(udtSet.shtPart2, 2, True), gBitSet(udtSet.shtPart2, 2, False))
                .shtPart2 = IIf(chkCargo4.Checked, gBitSet(udtSet.shtPart2, 3, True), gBitSet(udtSet.shtPart2, 3, False))
                .shtPart2 = IIf(chkCargo5.Checked, gBitSet(udtSet.shtPart2, 4, True), gBitSet(udtSet.shtPart2, 4, False))
                .shtPart2 = IIf(chkCargo6.Checked, gBitSet(udtSet.shtPart2, 5, True), gBitSet(udtSet.shtPart2, 5, False))
                .shtPart2 = IIf(chkCargo7.Checked, gBitSet(udtSet.shtPart2, 6, True), gBitSet(udtSet.shtPart2, 6, False))
                .shtPart2 = IIf(chkCargo8.Checked, gBitSet(udtSet.shtPart2, 7, True), gBitSet(udtSet.shtPart2, 7, False))
                .shtPart2 = IIf(chkCargo9.Checked, gBitSet(udtSet.shtPart2, 8, True), gBitSet(udtSet.shtPart2, 8, False))
                .shtPart2 = IIf(chkCargo10.Checked, gBitSet(udtSet.shtPart2, 9, True), gBitSet(udtSet.shtPart2, 9, False))
                .shtPart2 = IIf(chkCargo11.Checked, gBitSet(udtSet.shtPart2, 10, True), gBitSet(udtSet.shtPart2, 10, False))
                .shtPart2 = IIf(chkCargo12.Checked, gBitSet(udtSet.shtPart2, 11, True), gBitSet(udtSet.shtPart2, 11, False))
                .shtPart2 = IIf(chkCargo13.Checked, gBitSet(udtSet.shtPart2, 12, True), gBitSet(udtSet.shtPart2, 12, False))
                .shtPart2 = IIf(chkCargo14.Checked, gBitSet(udtSet.shtPart2, 13, True), gBitSet(udtSet.shtPart2, 13, False))
                .shtPart2 = IIf(chkCargo15.Checked, gBitSet(udtSet.shtPart2, 14, True), gBitSet(udtSet.shtPart2, 14, False))

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetExtCommon)

        Try

            With udtSet.shtDutyFunc

                ''Duty機能 有無（Duty Function）
                optDutyFunction1.Checked = IIf(gBitCheck(udtSet.shtDutyFunc, 0), True, False)
                optDutyFunction2.Checked = IIf(gBitCheck(udtSet.shtDutyFunc, 0), False, True)

                ''川汽仕様（特殊仕様）設定（Duty Method）
                optDutyMethod1.Checked = IIf(gBitCheck(udtSet.shtDutyMethod, 0), True, False)
                optDutyMethod2.Checked = IIf(gBitCheck(udtSet.shtDutyMethod, 0), False, True)

                ''Group Effect機能（Group Effect Function）
                Select Case udtSet.shtEffect
                    Case gCstCodeExtDutyEffectExtOutput : optGroupEffectFnc1.Checked = True
                    Case gCstCodeExtDutyEffectFix : optGroupEffectFnc2.Checked = True
                    Case gCstCodeExtDutyEffectNormal : optGroupEffectFnc3.Checked = True
                End Select

                ''NVルール（NVRule）
                optNvRule1.Checked = IIf(gBitCheck(udtSet.shtNv, 0), True, False)
                optNvRule2.Checked = IIf(gBitCheck(udtSet.shtNv, 0), False, True)

                ''DutyPart選択1～15（Mach）
                chkMach1.Checked = IIf(gBitCheck(udtSet.shtPart1, 0), True, False)
                chkMach2.Checked = IIf(gBitCheck(udtSet.shtPart1, 1), True, False)
                chkMach3.Checked = IIf(gBitCheck(udtSet.shtPart1, 2), True, False)
                chkMach4.Checked = IIf(gBitCheck(udtSet.shtPart1, 3), True, False)
                chkMach5.Checked = IIf(gBitCheck(udtSet.shtPart1, 4), True, False)
                chkMach6.Checked = IIf(gBitCheck(udtSet.shtPart1, 5), True, False)
                chkMach7.Checked = IIf(gBitCheck(udtSet.shtPart1, 6), True, False)
                chkMach8.Checked = IIf(gBitCheck(udtSet.shtPart1, 7), True, False)
                chkMach9.Checked = IIf(gBitCheck(udtSet.shtPart1, 8), True, False)
                chkMach10.Checked = IIf(gBitCheck(udtSet.shtPart1, 9), True, False)
                chkMach11.Checked = IIf(gBitCheck(udtSet.shtPart1, 10), True, False)
                chkMach12.Checked = IIf(gBitCheck(udtSet.shtPart1, 11), True, False)
                chkMach13.Checked = IIf(gBitCheck(udtSet.shtPart1, 12), True, False)
                chkMach14.Checked = IIf(gBitCheck(udtSet.shtPart1, 13), True, False)
                chkMach15.Checked = IIf(gBitCheck(udtSet.shtPart1, 14), True, False)

                ''DutyPart選択1～15（Cargo）
                chkCargo1.Checked = IIf(gBitCheck(udtSet.shtPart2, 0), True, False)
                chkCargo2.Checked = IIf(gBitCheck(udtSet.shtPart2, 1), True, False)
                chkCargo3.Checked = IIf(gBitCheck(udtSet.shtPart2, 2), True, False)
                chkCargo4.Checked = IIf(gBitCheck(udtSet.shtPart2, 3), True, False)
                chkCargo5.Checked = IIf(gBitCheck(udtSet.shtPart2, 4), True, False)
                chkCargo6.Checked = IIf(gBitCheck(udtSet.shtPart2, 5), True, False)
                chkCargo7.Checked = IIf(gBitCheck(udtSet.shtPart2, 6), True, False)
                chkCargo8.Checked = IIf(gBitCheck(udtSet.shtPart2, 7), True, False)
                chkCargo9.Checked = IIf(gBitCheck(udtSet.shtPart2, 8), True, False)
                chkCargo10.Checked = IIf(gBitCheck(udtSet.shtPart2, 9), True, False)
                chkCargo11.Checked = IIf(gBitCheck(udtSet.shtPart2, 10), True, False)
                chkCargo12.Checked = IIf(gBitCheck(udtSet.shtPart2, 11), True, False)
                chkCargo13.Checked = IIf(gBitCheck(udtSet.shtPart2, 12), True, False)
                chkCargo14.Checked = IIf(gBitCheck(udtSet.shtPart2, 13), True, False)
                chkCargo15.Checked = IIf(gBitCheck(udtSet.shtPart2, 14), True, False)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetExtCommon, _
                               ByRef udtTarget As gTypSetExtCommon)

        Try

            udtTarget.shtDutyFunc = udtSource.shtDutyFunc
            udtTarget.shtDutyMethod = udtSource.shtDutyMethod
            udtTarget.shtEffect = udtSource.shtEffect
            udtTarget.shtNv = udtSource.shtNv
            udtTarget.shtPart1 = udtSource.shtPart1
            udtTarget.shtPart2 = udtSource.shtPart2

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtCommon, _
                                         ByVal udt2 As gTypSetExtCommon) As Boolean

        Try

            If udt1.shtDutyFunc <> udt2.shtDutyFunc Then Return False
            If udt1.shtDutyMethod <> udt2.shtDutyMethod Then Return False
            If udt1.shtEffect <> udt2.shtEffect Then Return False
            If udt1.shtNv <> udt2.shtNv Then Return False
            If udt1.shtPart1 <> udt2.shtPart1 Then Return False
            If udt1.shtPart2 <> udt2.shtPart2 Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class

