Public Class frmExtPnlLED

#Region "変数定義"

    Dim mintRtn As Integer
    Dim mudtSetExtAlm As gTypSetExtCommon
    Dim mudtSetExtAlmSep As gTypSetExtRec

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    '           : ARG2 - (I ) 延長警報盤のパネル番号＠共通設定
    '           : ARG3 - (I ) 選択行番号（配列なのでー１した数）
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSetExtAlmSep As gTypSetExtRec, ByVal intRowIndex As Integer, ByRef frmOwner As Form) As Integer

        Try

            ''戻り値初期化
            mintRtn = 1

            ''引数保存
            mudtSetExtAlmSep = udtSetExtAlmSep

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtSetExtAlmSep = mudtSetExtAlmSep
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
    Private Sub frmExtPnlLED_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''コンボボックス初期設定
            Call gSetComboBox(cmbEngineerNo, gEnmComboType.ctExtPnlLedEngNo)    ''Eeengineer No.
            Call gSetComboBox(cmbDutyNo, gEnmComboType.ctExtPnlLedDutyNo)       ''Duty No. Set

            'コンバイン仕様選択時のみテキスト表示される      ver.1.4.4 2012.05.08
            If frmExtMenu.optCombine.Checked Then
                fraWatchLed.Visible = False
                txtWatchLed.Visible = True
            Else
                fraWatchLed.Visible = True
                txtWatchLed.Visible = False
            End If

            ''画面設定
            Call mSetDisplay(mudtSetExtAlmSep)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Okボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''設定値の保存
            Call mSetStructure(mudtSetExtAlmSep)

            mintRtn = 0
            Me.Close()

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

            mintRtn = 1
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
    Private Sub frmExtPnlLED_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント      ver.1.4.4 2012.05.08
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtWatchLed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWatchLed.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub




#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 延長警報盤構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtRec)

        Try

            With udtSet

                ''EngineerCallNo設定
                .shtEngNo = CCShort(cmbEngineerNo.SelectedValue)

                ''Duty番号
                .shtDuty = CCShort(cmbDutyNo.SelectedValue)

                ''Dutyブザーストップ
                .shtDutyBuzz = IIf(optALL.Checked, 1, 0)

                ''Watch LED表示方法選択
                'コンバイン仕様選択時のみテキスト表示される      ver.1.4.4 2012.05.08
                If frmExtMenu.optCombine.Checked Then
                    .shtWatchLed = CCUInt16(txtWatchLed.Text)
                Else
                    If optNone.Checked Then .shtWatchLed = gCstCodeExtPanelWatchLedNone
                    If optMan.Checked Then .shtWatchLed = gCstCodeExtPanelWatchLedMan
                    If optUnman.Checked Then .shtWatchLed = gCstCodeExtPanelWatchLedUnman
                    If optManUnman.Checked Then .shtWatchLed = gCstCodeExtPanelWatchLedManUnman
                End If


                ''LED表示方法選択
                If optLED0.Checked Then .shtLedOut = 0
                If optLED1.Checked Then .shtLedOut = 1
                If optLED2.Checked Then .shtLedOut = 2
                If optLED3.Checked Then .shtLedOut = 3
                If optLED4.Checked Then .shtLedOut = 4
                If optLED5.Checked Then .shtLedOut = 5
                If optLED6.Checked Then .shtLedOut = 6
                If optLED7.Checked Then .shtLedOut = 7
                If optLED8.Checked Then .shtLedOut = 8
                If optLED9.Checked Then .shtLedOut = 9
                If optLED10.Checked Then .shtLedOut = 10
                If optLED11.Checked Then .shtLedOut = 11
                If optLED12.Checked Then .shtLedOut = 12
                If optLED13.Checked Then .shtLedOut = 13

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 延長警報盤構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByRef udtSet As gTypSetExtRec)

        Try

            ''ID
            lblIdNo.Text = udtSet.shtNo.ToString

            ''EeengineerCallNo設定
            cmbEngineerNo.SelectedValue = udtSet.shtEngNo.ToString

            ''Duty番号
            cmbDutyNo.SelectedValue = udtSet.shtDuty.ToString

            ''Dutyブザーストップ
            optALL.Checked = IIf(udtSet.shtDutyBuzz, True, False)
            optDutyOnly.Checked = IIf(udtSet.shtDutyBuzz, False, True)

            ''Watch LED表示方法選択
            'コンバイン仕様選択時のみテキスト表示される      ver.1.4.4 2012.05.08
            If frmExtMenu.optCombine.Checked Then
                txtWatchLed.Text = udtSet.shtWatchLed.ToString
            Else
                Select Case udtSet.shtWatchLed
                    Case gCstCodeExtPanelWatchLedNone : optNone.Checked = True
                    Case gCstCodeExtPanelWatchLedMan : optMan.Checked = True
                    Case gCstCodeExtPanelWatchLedUnman : optUnman.Checked = True
                    Case gCstCodeExtPanelWatchLedManUnman : optManUnman.Checked = True
                    Case Else
                End Select
            End If

            ''LED表示方法選択
            Select Case udtSet.shtLedOut
                Case 0 : optLED0.Checked = True
                Case 1 : optLED1.Checked = True
                Case 2 : optLED2.Checked = True
                Case 3 : optLED3.Checked = True
                Case 4 : optLED4.Checked = True
                Case 5 : optLED5.Checked = True
                Case 6 : optLED6.Checked = True
                Case 7 : optLED7.Checked = True
                Case 8 : optLED8.Checked = True
                Case 9 : optLED9.Checked = True
                Case 10 : optLED10.Checked = True
                Case 11 : optLED11.Checked = True
                Case 12 : optLED12.Checked = True
                Case 13 : optLED13.Checked = True
                Case Else
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
