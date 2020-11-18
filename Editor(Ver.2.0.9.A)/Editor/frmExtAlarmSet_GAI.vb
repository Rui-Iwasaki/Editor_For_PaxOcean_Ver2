Public Class frmExtAlarmSet_GAI

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
    Private Sub frmExtAlarmSet_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
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

            '入力チェック
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
    Private Sub frmExtAlarmSet_GAI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

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
    Private Sub frmExtAlarmSet_GAI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

                '川汽仕様（特殊仕様）設定 常に0
                .shtDutyMethod = 0

                'Group Effect機能
                If optGroupEffectFnc1.Checked Then : .shtEffect = gCstCodeExtDutyEffectExtOutput : End If
                If optGroupEffectFnc2.Checked Then : .shtEffect = gCstCodeExtDutyEffectFix : End If
                If optGroupEffectFnc3.Checked Then : .shtEffect = gCstCodeExtDutyEffectNormal : End If

                'Group Effect設定
                .shtGrpEffct = IIf(chkGroupEffect1.Checked, gBitSet(.shtGrpEffct, 0, True), gBitSet(.shtGrpEffct, 0, False))
                .shtGrpEffct = IIf(chkGroupEffect2.Checked, gBitSet(.shtGrpEffct, 1, True), gBitSet(.shtGrpEffct, 1, False))
                .shtGrpEffct = IIf(chkGroupEffect3.Checked, gBitSet(.shtGrpEffct, 2, True), gBitSet(.shtGrpEffct, 2, False))
                .shtGrpEffct = IIf(chkGroupEffect4.Checked, gBitSet(.shtGrpEffct, 3, True), gBitSet(.shtGrpEffct, 3, False))
                .shtGrpEffct = IIf(chkGroupEffect5.Checked, gBitSet(.shtGrpEffct, 4, True), gBitSet(.shtGrpEffct, 4, False))
                .shtGrpEffct = IIf(chkGroupEffect6.Checked, gBitSet(.shtGrpEffct, 5, True), gBitSet(.shtGrpEffct, 5, False))
                .shtGrpEffct = IIf(chkGroupEffect7.Checked, gBitSet(.shtGrpEffct, 6, True), gBitSet(.shtGrpEffct, 6, False))
                .shtGrpEffct = IIf(chkGroupEffect8.Checked, gBitSet(.shtGrpEffct, 7, True), gBitSet(.shtGrpEffct, 7, False))
                .shtGrpEffct = IIf(chkGroupEffect9.Checked, gBitSet(.shtGrpEffct, 8, True), gBitSet(.shtGrpEffct, 8, False))
                .shtGrpEffct = IIf(chkGroupEffect10.Checked, gBitSet(.shtGrpEffct, 9, True), gBitSet(.shtGrpEffct, 9, False))
                .shtGrpEffct = IIf(chkGroupEffect11.Checked, gBitSet(.shtGrpEffct, 10, True), gBitSet(.shtGrpEffct, 10, False))
                .shtGrpEffct = IIf(chkGroupEffect12.Checked, gBitSet(.shtGrpEffct, 11, True), gBitSet(.shtGrpEffct, 11, False))

                'Fire Sound Group設定
                .shtGrpFire = IIf(chkFireSoundGroup1.Checked, gBitSet(.shtGrpFire, 0, True), gBitSet(.shtGrpFire, 0, False))
                .shtGrpFire = IIf(chkFireSoundGroup2.Checked, gBitSet(.shtGrpFire, 1, True), gBitSet(.shtGrpFire, 1, False))
                .shtGrpFire = IIf(chkFireSoundGroup3.Checked, gBitSet(.shtGrpFire, 2, True), gBitSet(.shtGrpFire, 2, False))
                .shtGrpFire = IIf(chkFireSoundGroup4.Checked, gBitSet(.shtGrpFire, 3, True), gBitSet(.shtGrpFire, 3, False))
                .shtGrpFire = IIf(chkFireSoundGroup5.Checked, gBitSet(.shtGrpFire, 4, True), gBitSet(.shtGrpFire, 4, False))
                .shtGrpFire = IIf(chkFireSoundGroup6.Checked, gBitSet(.shtGrpFire, 5, True), gBitSet(.shtGrpFire, 5, False))
                .shtGrpFire = IIf(chkFireSoundGroup7.Checked, gBitSet(.shtGrpFire, 6, True), gBitSet(.shtGrpFire, 6, False))
                .shtGrpFire = IIf(chkFireSoundGroup8.Checked, gBitSet(.shtGrpFire, 7, True), gBitSet(.shtGrpFire, 7, False))
                .shtGrpFire = IIf(chkFireSoundGroup9.Checked, gBitSet(.shtGrpFire, 8, True), gBitSet(.shtGrpFire, 8, False))
                .shtGrpFire = IIf(chkFireSoundGroup10.Checked, gBitSet(.shtGrpFire, 9, True), gBitSet(.shtGrpFire, 9, False))
                .shtGrpFire = IIf(chkFireSoundGroup11.Checked, gBitSet(.shtGrpFire, 10, True), gBitSet(.shtGrpFire, 10, False))
                .shtGrpFire = IIf(chkFireSoundGroup12.Checked, gBitSet(.shtGrpFire, 11, True), gBitSet(.shtGrpFire, 11, False))

                'Fireﾌﾞｻﾞｰﾊﾟﾀｰﾝ
                .shtFireBuzzer = IIf(optFireBzPattern1.Checked, 1, 0)

                'NV(Confirm Switching to duty)
                .shtNv = IIf(optNvRule1.Checked, 1, 0)

                'DutyPart選択 常に0
                .shtPart1 = 0
                .shtPart2 = 0


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

            With udtSet
                'Group Effect機能（Group Effect Function）
                Select Case .shtEffect
                    Case gCstCodeExtDutyEffectExtOutput : optGroupEffectFnc1.Checked = True
                    Case gCstCodeExtDutyEffectFix : optGroupEffectFnc2.Checked = True
                    Case gCstCodeExtDutyEffectNormal : optGroupEffectFnc3.Checked = True
                End Select

                'Group Effect設定
                chkGroupEffect1.Checked = IIf(gBitCheck(.shtGrpEffct, 0), True, False)
                chkGroupEffect2.Checked = IIf(gBitCheck(.shtGrpEffct, 1), True, False)
                chkGroupEffect3.Checked = IIf(gBitCheck(.shtGrpEffct, 2), True, False)
                chkGroupEffect4.Checked = IIf(gBitCheck(.shtGrpEffct, 3), True, False)
                chkGroupEffect5.Checked = IIf(gBitCheck(.shtGrpEffct, 4), True, False)
                chkGroupEffect6.Checked = IIf(gBitCheck(.shtGrpEffct, 5), True, False)
                chkGroupEffect7.Checked = IIf(gBitCheck(.shtGrpEffct, 6), True, False)
                chkGroupEffect8.Checked = IIf(gBitCheck(.shtGrpEffct, 7), True, False)
                chkGroupEffect9.Checked = IIf(gBitCheck(.shtGrpEffct, 8), True, False)
                chkGroupEffect10.Checked = IIf(gBitCheck(.shtGrpEffct, 9), True, False)
                chkGroupEffect11.Checked = IIf(gBitCheck(.shtGrpEffct, 10), True, False)
                chkGroupEffect12.Checked = IIf(gBitCheck(.shtGrpEffct, 11), True, False)

                'Fire Sound Group設定
                chkFireSoundGroup1.Checked = IIf(gBitCheck(.shtGrpFire, 0), True, False)
                chkFireSoundGroup2.Checked = IIf(gBitCheck(.shtGrpFire, 1), True, False)
                chkFireSoundGroup3.Checked = IIf(gBitCheck(.shtGrpFire, 2), True, False)
                chkFireSoundGroup4.Checked = IIf(gBitCheck(.shtGrpFire, 3), True, False)
                chkFireSoundGroup5.Checked = IIf(gBitCheck(.shtGrpFire, 4), True, False)
                chkFireSoundGroup6.Checked = IIf(gBitCheck(.shtGrpFire, 5), True, False)
                chkFireSoundGroup7.Checked = IIf(gBitCheck(.shtGrpFire, 6), True, False)
                chkFireSoundGroup8.Checked = IIf(gBitCheck(.shtGrpFire, 7), True, False)
                chkFireSoundGroup9.Checked = IIf(gBitCheck(.shtGrpFire, 8), True, False)
                chkFireSoundGroup10.Checked = IIf(gBitCheck(.shtGrpFire, 9), True, False)
                chkFireSoundGroup11.Checked = IIf(gBitCheck(.shtGrpFire, 10), True, False)
                chkFireSoundGroup12.Checked = IIf(gBitCheck(.shtGrpFire, 11), True, False)

                'Fireﾌﾞｻﾞｰﾊﾟﾀｰﾝ
                optFireBzPattern1.Checked = IIf(gBitCheck(.shtFireBuzzer, 0), True, False)
                optFireBzPattern2.Checked = IIf(gBitCheck(.shtFireBuzzer, 0), False, True)

                'Confirm Switching to duty
                optNvRule1.Checked = IIf(gBitCheck(.shtNv, 0), True, False)
                optNvRule2.Checked = IIf(gBitCheck(.shtNv, 0), False, True)
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
            udtTarget.shtGrpEffct = udtSource.shtGrpEffct
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
            If udt1.shtGrpEffct <> udt2.shtGrpEffct Then Return False
            If udt1.shtGrpFire <> udt2.shtGrpFire Then Return False
            If udt1.shtNv <> udt2.shtNv Then Return False
            If udt1.shtPart1 <> udt2.shtPart1 Then Return False
            If udt1.shtPart2 <> udt2.shtPart2 Then Return False
            If udt1.shtFireBuzzer <> udt2.shtFireBuzzer Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class

