Public Class frmExtPnlLED_GAI

#Region "変数定義"

    Dim mintRtn As Integer
    Dim mudtSetExtAlm As gTypSetExtCommon
    Dim mudtSetExtAlmSep As gTypSetExtRec

    Dim mintAGno As Integer
    Dim mintRowNo As Integer

    '下記２ﾗﾍﾞﾙ配列変数は、画面右側のDutyﾗﾍﾞﾙ制御を容易にするために使用する
    Dim lblDuty(6) As Label
    Dim lblLigh(6) As Label
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    '           : ARG2 - (I ) 前画面のLED Alarm Group Count
    '           : ARG3 - (I ) 延長警報盤のパネル番号＠共通設定
    '           : ARG4 - (I ) 選択行番号（配列なのでー１した数）
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSetExtAlmSep As gTypSetExtRec, ByVal pintAGno As Integer, ByVal intRowIndex As Integer, ByRef frmOwner As Form) As Integer

        Try

            ''戻り値初期化
            mintRtn = 1

            ''引数保存
            mudtSetExtAlmSep = udtSetExtAlmSep
            mintAGno = pintAGno

            mintRowNo = intRowIndex


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
    Private Sub frmExtPnlLED_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Dutyﾗﾍﾞﾙ配列変数へ格納
            lblDuty(0) = lblLEDduty1
            lblDuty(1) = lblLEDduty2
            lblDuty(2) = lblLEDduty3
            lblDuty(3) = lblLEDduty4
            lblDuty(4) = lblLEDduty5
            lblDuty(5) = lblLEDduty6
            lblDuty(6) = lblLEDduty7
            '
            lblLigh(0) = lblLEDligh1
            lblLigh(1) = lblLEDligh2
            lblLigh(2) = lblLEDligh3
            lblLigh(3) = lblLEDligh4
            lblLigh(4) = lblLEDligh5
            lblLigh(5) = lblLEDligh6
            lblLigh(6) = lblLEDligh7


            '画面設定
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
    Private Sub frmExtPnlLED_GAI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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
                'LED表示方法選択
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

            'ID
            lblIdNo.Text = mintRowNo + 1

            'LED表示方法選択
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

            '画面右側 DUTY関連制御
            Call subSetLblDuty(mintAGno)

            'LED選択ｵﾌﾟｼｮﾝ制御
            If mintAGno <= 8 Then
                optLED10.Enabled = False
                optLED11.Enabled = False
                optLED12.Enabled = False
                optLED13.Enabled = False
            Else
                optLED10.Enabled = True
                optLED11.Enabled = True
                optLED12.Enabled = True
                optLED13.Enabled = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '画面右側 DUTY関連制御
    Private Sub subSetLblDuty(pintAGNO As Integer)
        Try
            Dim i As Integer = 0
            Dim iCnt As Integer = 0

            Select Case pintAGNO
                Case 9
                    '1のみ非表示
                    lblDuty(0).Visible = False
                    lblLigh(0).Visible = False
                    iCnt = 1
                    For i = 1 To UBound(lblDuty) Step 1
                        lblDuty(i).Visible = True
                        lblLigh(i).Visible = True
                        '
                        lblDuty(i).Text = "DUTY " & iCnt.ToString
                        If iCnt = 1 Then
                            lblLigh(i).Text = "ON DUTY"
                            lblLigh(i).BackColor = Color.Beige
                        Else
                            lblLigh(i).Text = "LIGH OFF"
                            lblLigh(i).BackColor = Color.Gainsboro
                        End If
                        iCnt = iCnt + 1
                    Next i
                Case 10
                    '1,2非表示
                    For i = 0 To 1 Step 1
                        lblDuty(i).Visible = False
                        lblLigh(i).Visible = False
                    Next i
                    iCnt = 1
                    For i = 2 To UBound(lblDuty) Step 1
                        lblDuty(i).Visible = True
                        lblLigh(i).Visible = True
                        '
                        lblDuty(i).Text = "DUTY " & iCnt.ToString
                        If iCnt = 1 Then
                            lblLigh(i).Text = "ON DUTY"
                            lblLigh(i).BackColor = Color.Beige
                        Else
                            lblLigh(i).Text = "LIGH OFF"
                            lblLigh(i).BackColor = Color.Gainsboro
                        End If
                        iCnt = iCnt + 1
                    Next i
                Case 11
                    '1,2,3非表示
                    For i = 0 To 2 Step 1
                        lblDuty(i).Visible = False
                        lblLigh(i).Visible = False
                    Next i
                    iCnt = 1
                    For i = 3 To UBound(lblDuty) Step 1
                        lblDuty(i).Visible = True
                        lblLigh(i).Visible = True
                        '
                        lblDuty(i).Text = "DUTY " & iCnt.ToString
                        If iCnt = 1 Then
                            lblLigh(i).Text = "ON DUTY"
                            lblLigh(i).BackColor = Color.Beige
                        Else
                            lblLigh(i).Text = "LIGH OFF"
                            lblLigh(i).BackColor = Color.Gainsboro
                        End If
                        iCnt = iCnt + 1
                    Next i
                Case 12
                    '1,2,3,4非表示
                    For i = 0 To 3 Step 1
                        lblDuty(i).Visible = False
                        lblLigh(i).Visible = False
                    Next i
                    iCnt = 1
                    For i = 4 To UBound(lblDuty) Step 1
                        lblDuty(i).Visible = True
                        lblLigh(i).Visible = True
                        '
                        lblDuty(i).Text = "DUTY " & iCnt.ToString
                        If iCnt = 1 Then
                            lblLigh(i).Text = "ON DUTY"
                            lblLigh(i).BackColor = Color.Beige
                        Else
                            lblLigh(i).Text = "LIGH OFF"
                            lblLigh(i).BackColor = Color.Gainsboro
                        End If
                        iCnt = iCnt + 1
                    Next i
                Case Else
                    '8より少ない
                    '全部表示
                    iCnt = 1
                    For i = 0 To UBound(lblDuty) Step 1
                        lblDuty(i).Visible = True
                        lblLigh(i).Visible = True
                        '
                        lblDuty(i).Text = "DUTY " & iCnt.ToString
                        If iCnt = 1 Then
                            lblLigh(i).Text = "ON DUTY"
                            lblLigh(i).BackColor = Color.Beige
                        Else
                            lblLigh(i).Text = "LIGH OFF"
                            lblLigh(i).BackColor = Color.Gainsboro
                        End If
                        iCnt = iCnt + 1
                    Next i

            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

End Class
