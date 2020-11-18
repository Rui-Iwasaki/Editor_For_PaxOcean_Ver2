Public Class frmSysFcu

#Region "変数定義"

    Private mudtSetSysFcuNew As gTypSetSysFcu
    'Private mudtSetSysFcuOld As gTypSetSysFcu

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
    Private Sub frmSysFcu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''コンボボックス初期設定 2011/12/13 K.Tanigawa
            Call gSetComboBox(CmbFCUCnt, gEnmComboType.ctSysFcuFcuCount)

            ''コンボボックス初期設定
            Call gSetComboBox(cmbFCUNo, gEnmComboType.ctSysFcuFcuCount)

            ''コンボボックス初期設定   '' Ver1.11.8.2 2016.11.01 FCU2台仕様 Cargo/Hull選択
            Call gSetComboBox(cmbPart, gEnmComboType.ctSysFcuPartSet)

            ''配列初期化
            Call mudtSetSysFcuNew.InitArray()

            ''画面設定
            Call mSetDisplay(gudt.SetSystem.udtSysFcu)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： FCU Countチェンジ
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub cmbFCUNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFCUNo.SelectedIndexChanged

        If IsNumeric(cmbFCUNo.Text) Then

            ''FCU番号に変更につき、share typeのロック削除　ver.1.4.0 2011.09.29
            ' FCU番号の変更時、台数以上の設定ができないロック追加
            'If CInt(cmbFCUNo.Text) > CInt(CmbFCUCnt.Text) Then
            '    cmbFCUNo.Text = CmbFCUCnt.Text
            'Else
            '    '    chkShareChUse.Checked = False
            '    '    chkShareChUse.Enabled = False
            'End If

        End If

    End Sub
    '----------------------------------------------------------------------------
    ' 機能説明  ： FCU 台数チェンジ
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub cmbFCUCnt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbFCUCnt.SelectedIndexChanged

        If IsNumeric(CmbFCUCnt.Text) Then

            '' Ver1.11.8.2 2016.11.01  FCU2台仕様の場合はﾊﾟｰﾄ分け設定を使用可とする
            If CInt(CmbFCUCnt.Text) >= 2 Then
                cmbPart.Enabled = True
            Else
                cmbPart.Enabled = False
            End If

            ''FCU番号に変更につき、share typeのロック削除　ver.1.4.0 2011.09.29
            'If CInt(cmbFCU.Text) >= 2 Then
            '    chkShareChUse.Enabled = True
            'Else
            '    chkShareChUse.Checked = False
            '    chkShareChUse.Enabled = False
            'End If

        End If

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
            Call mSetStructure(mudtSetSysFcuNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysFcuNew, gudt.SetSystem.udtSysFcu) Then

                ''変更された場合は設定を更新する
                gudt.SetSystem.udtSysFcu = mudtSetSysFcuNew

                '' Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                If cmbPart.SelectedIndex = 1 Then      '' 今回はMach/Hull
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, True)
                Else
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                End If
                ''

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
    ' 機能      : 収集周期キープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 数値のみ入力可能とする
    '--------------------------------------------------------------------
    Private Sub txtCorrectTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try

            If gKeyPress(e.KeyChar) = True Then

                e.Handled = True

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
            Call mSetStructure(mudtSetSysFcuNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysFcuNew, gudt.SetSystem.udtSysFcu) Then

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
                        'mudtSetSysFcuOld = mudtSetSysFcuNew
                        gudt.SetSystem.udtSysFcu = mudtSetSysFcuNew

                        '' Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                        If cmbPart.SelectedIndex = 1 Then      '' 今回はMach/Hull
                            gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, True)
                        Else
                            gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                        End If
                        ''

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
    ' 引き数    : ARG1 - ( O) FCU設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSysFcu)

        Try

            With udtSet

                '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''イベントログバックアップ
                '.shtLogBackup = IIf(chkEventLogBackup.Checked, 1, 0)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ' ''CANBUS対応
                '.shtCanbus = IIf(chkCanBus.Checked, 1, 0)

                ' ''MODBUS対応
                '.shtModbus = IIf(chkModBus.Checked, 1, 0)

                ''FCU台数 2011.12.13 K.Tanigawa
                .shtFcuCnt = CmbFCUCnt.SelectedValue

                ''FCU No.
                .shtFcuNo = cmbFCUNo.SelectedValue

                ''共有CH使用有無
                .shtShareChUse = IIf(chkShareChUse.Checked, 1, 0)

                ''収集周期
                .shtCrrectTime = numCorrectTime.Value

                ''FCU拡張ボード
                .shtFcuExtendBord = IIf(chkSIO.Checked, 1, 0)

                '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
                .shtFCUOption = 0
                .shtFCUOption = .shtFCUOption + IIf(chkFCU.Checked, 1, 0)

                'Ver2.0.3.6
                'PT,JPT
                .shtPtJptFlg = 0
                .shtPtJptFlg = .shtPtJptFlg + IIf(chkPtJPt.Checked, 1, 0)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) FCU設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSysFcu)

        Try

            With udtSet

                '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''イベントログバックアップ
                'chkEventLogBackup.Checked = IIf(.shtLogBackup = 1, True, False)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ' ''CANBUS対応
                'chkCanBus.Checked = IIf(.shtCanbus = 1, True, False)

                ' ''MODBUS対応
                'chkModBus.Checked = IIf(.shtModbus = 1, True, False)

                ''FCU台数 2011.12.13 K.Tanigawa
                CmbFCUCnt.SelectedValue = .shtFcuCnt

                ''FCU No.
                cmbFCUNo.SelectedValue = .shtFcuNo

                '' Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 1) Then    '' Mach/Hull
                    cmbPart.SelectedIndex = 1
                Else        '' Mach/Cargo
                    cmbPart.SelectedIndex = 0
                End If
                ''


                ''共有CH使用有無
                chkShareChUse.Checked = IIf(.shtShareChUse = 1, True, False)

                ''収集周期
                numCorrectTime.Value = .shtCrrectTime

                ''SIO通信
                chkSIO.Checked = IIf(.shtFcuExtendBord = 1, True, False)

                '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
                chkFCU.Checked = IIf(gBitCheck(.shtFCUOption, 0), True, False)

                'Ver2.0.3.6 PT,JPT
                chkPtJPt.Checked = IIf(gBitCheck(.shtPtJptFlg, 0), True, False)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSysFcu, _
                               ByRef udtTarget As gTypSetSysFcu)

        Try

            udtTarget.shtCrrectTime = udtSource.shtCrrectTime
            udtTarget.shtFcuCnt = udtSource.shtFcuCnt ' 2011.12.13 K.Tanigawa
            udtTarget.shtFcuNo = udtSource.shtFcuNo
            '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'udtTarget.shtLogBackup = udtSource.shtLogBackup
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            udtTarget.shtShareChUse = udtSource.shtShareChUse
            'udtTarget.shtCanbus = udtSource.shtCanbus
            'udtTarget.shtModbus = udtSource.shtModbus
            udtTarget.shtFcuExtendBord = udtSource.shtFcuExtendBord

            '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
            udtTarget.shtFCUOption = udtSource.shtFCUOption

            'Ver2.0.3.6 PT,JPT
            udtTarget.shtPtJptFlg = udtSource.shtPtJptFlg

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSysFcu, _
                                         ByVal udt2 As gTypSetSysFcu) As Boolean

        Try

            If udt1.shtCrrectTime <> udt2.shtCrrectTime Then Return False
            If udt1.shtFcuCnt <> udt2.shtFcuCnt Then Return False '2011.12.13 K.Tanigawa
            If udt1.shtFcuNo <> udt2.shtFcuNo Then Return False
            If udt1.shtShareChUse <> udt2.shtShareChUse Then Return False
            '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'If udt1.shtLogBackup <> udt2.shtLogBackup Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            'If udt1.shtCanbus <> udt2.shtCanbus Then Return False
            'If udt1.shtModbus <> udt2.shtModbus Then Return False
            If udt1.shtFcuExtendBord <> udt2.shtFcuExtendBord Then Return False
            'If udt1.strSpare <> udt2.strSpare Then Return False
            If udt1.shtFCUOption <> udt2.shtFCUOption Then Return False '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
            If udt1.shtPtJptFlg <> udt2.shtPtJptFlg Then Return False 'Ver2.0.3.6 PT,JPT

            '' Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
            If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 1) Then    '' Mach/Hullが設定済み
                If cmbPart.SelectedIndex <> 1 Then      '' 今回はMach/Cargo
                    Return False
                End If
            Else        '' Mach/Cargoが設定済み
                If cmbPart.SelectedIndex = 1 Then      '' 今回はMach/Hull
                    Return False
                End If
            End If
            ''

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
