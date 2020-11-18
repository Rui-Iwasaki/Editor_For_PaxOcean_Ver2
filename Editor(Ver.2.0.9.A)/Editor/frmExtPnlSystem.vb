Public Class frmExtPnlSystem

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
    Private Sub frmExtPnlSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''コンボボックス初期設定
            Call gSetComboBox(cmbGroupOutputPattern, gEnmComboType.ctExtPnlSystem)

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
    Private Sub frmExtPnlSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

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
    Private Sub frmExtPnlSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

                ''アラームランプ数
                If optLedAlarmGroupCount1.Checked Then .shtLamps = 8
                If optLedAlarmGroupCount2.Checked Then .shtLamps = 9
                If optLedAlarmGroupCount3.Checked Then .shtLamps = 10
                If optLedAlarmGroupCount4.Checked Then .shtLamps = 11
                If optLedAlarmGroupCount5.Checked Then .shtLamps = 12

                ''ブザーパターン
                If optBzPattern1.Checked Then .shtBuzzer = 1
                If optBzPattern2.Checked Then .shtBuzzer = 2
                If optBzPattern3.Checked Then .shtBuzzer = 3
                If optBzABS.Checked Then .shtBuzzer = 4

                ''グループ出力パターン
                .shtGrpOut = cmbGroupOutputPattern.SelectedValue

                ''Group Effect設定
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

                ''Fire Sound Group設定
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

                ''ｸﾞﾙｰﾌﾟｱﾗｰﾑﾗﾝﾌﾟ出力選択
                .shtGrpAlarm = IIf(optExtGroupOutput1.Checked, 1, 0)

                'Fireﾌﾞｻﾞｰﾊﾟﾀｰﾝ
                .shtFireBuzzer = IIf(optFireBzPattern1.Checked, 1, 0)

                '' Ver1.11.5 2016.09.06  F.T Output設定
                .shtRsv = IIf(optFTOutPattern1.Checked, 1, 0)

                ' Ver.2.0.8.8 OASIS延長警報ﾊﾟﾈﾙﾌﾗｸﾞ
                .shtRsv = .shtRsv Or IIf(optOasisEapUse1.Checked, 2, 0)

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

                ''アラームランプ数
                Select Case .shtLamps
                    Case 8 : optLedAlarmGroupCount1.Checked = True
                    Case 9 : optLedAlarmGroupCount2.Checked = True
                    Case 10 : optLedAlarmGroupCount3.Checked = True
                    Case 11 : optLedAlarmGroupCount4.Checked = True
                    Case 12 : optLedAlarmGroupCount5.Checked = True
                End Select

                ''ブザーパターン
                Select Case .shtBuzzer
                    Case 1 : optBzPattern1.Checked = True
                    Case 2 : optBzPattern2.Checked = True
                    Case 3 : optBzPattern3.Checked = True
                    Case 4 : optBzABS.Checked = True
                End Select

                ''グループ出力パターン
                cmbGroupOutputPattern.SelectedValue = .shtGrpOut

                ''Group Effect設定
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

                ''Fire Sound Group設定
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

                ''ｸﾞﾙｰﾌﾟｱﾗｰﾑﾗﾝﾌﾟ出力選択
                optExtGroupOutput1.Checked = IIf(gBitCheck(.shtGrpAlarm, 0), True, False)
                optExtGroupOutput2.Checked = IIf(gBitCheck(.shtGrpAlarm, 0), False, True)

                ''Fireﾌﾞｻﾞｰﾊﾟﾀｰﾝ
                optFireBzPattern1.Checked = IIf(gBitCheck(.shtFireBuzzer, 0), True, False)
                optFireBzPattern2.Checked = IIf(gBitCheck(.shtFireBuzzer, 0), False, True)

                '' Ver1.11.5 2016.09.06  F.T Output設定
                optFTOutPattern1.Checked = IIf(gBitCheck(.shtRsv, 0), True, True)      '' Enable
                optFTOutPattern2.Checked = IIf(gBitCheck(.shtRsv, 0), False, True)     '' Disable
                ''//

                '' Ver2.0.8.8  OASIS延長警報ﾊﾟﾈﾙﾌﾗｸﾞ
                optOasisEapUse1.Checked = IIf(gBitCheck(.shtRsv, 1), True, True)      '' Enable
                optOasisEapUse2.Checked = IIf(gBitCheck(.shtRsv, 1), False, True)     '' Disable
                ''//

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

            udtTarget.shtLamps = udtSource.shtLamps
            udtTarget.shtBuzzer = udtSource.shtBuzzer
            udtTarget.shtGrpOut = udtSource.shtGrpOut
            udtTarget.shtGrpEffct = udtSource.shtGrpEffct
            udtTarget.shtGrpFire = udtSource.shtGrpFire
            udtTarget.shtGrpAlarm = udtSource.shtGrpAlarm
            udtTarget.shtFireBuzzer = udtSource.shtFireBuzzer
            udtTarget.shtRsv = udtSource.shtRsv     '' Ver1.11.5 2016.09.06  F.T Output設定

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

            If udt1.shtLamps <> udt2.shtLamps Then Return False
            If udt1.shtBuzzer <> udt2.shtBuzzer Then Return False
            If udt1.shtGrpOut <> udt2.shtGrpOut Then Return False
            If udt1.shtGrpEffct <> udt2.shtGrpEffct Then Return False
            If udt1.shtGrpFire <> udt2.shtGrpFire Then Return False
            If udt1.shtGrpAlarm <> udt2.shtGrpAlarm Then Return False
            If udt1.shtFireBuzzer <> udt2.shtFireBuzzer Then Return False
            If udt1.shtRsv <> udt2.shtRsv Then Return False '' Ver1.11.5 2016.09.06  F.T Output設定

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

#End Region

End Class
