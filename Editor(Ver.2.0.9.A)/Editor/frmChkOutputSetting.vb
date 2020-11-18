Public Class frmChkOutputSetting

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileInfo As gTypFileInfo
    Private mblnCancelFlg As Boolean

    Private mblnEnglish As Boolean

    Dim mstrCsv() As String = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 返り値    : 0:キャンセル、1:実行、-1:失敗
    ' 機能説明  : 画面表示を行う
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileInfo As gTypFileInfo) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileInfo = udtFileInfo
            'mudtCompileType = udtCompileType

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
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
    Private Sub frmCmpCompier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            mblnEnglish = True

            ''ヘッダーメッセージ出力
            txtMsg.Text = ""
            Call mAddMsgText(" --- SEARCH FOR THE OUTPUT CHANNEL SETTING --- ", " --- 出力チャンネル検索 --- ")
            Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： サーチボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Try
            Dim strWaitMsg1 As String = ""
            Dim strWaitMsg2 As String = ""
            Dim udtMsgResult As DialogResult
            Dim intFuNo As Integer, intPortNo As Integer, intPin As Integer
            Dim strChNo As String, strFuAddress As String = "", strChType As String = ""

            If mblnEnglish Then
                udtMsgResult = MessageBox.Show("Do you start output channel search? ", _
                                               "Search", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                strWaitMsg1 = "Now Searching"
                strWaitMsg2 = "Please wait..."
            Else
                udtMsgResult = MessageBox.Show("出力チャンネル検索処理を開始します。" & vbCrLf & _
                                               "よろしいですか？", "検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                strWaitMsg1 = "サーチ中です。"
                strWaitMsg2 = "しばらくお待ち下さい..."
            End If

            If udtMsgResult = Windows.Forms.DialogResult.Yes Then

                ''画面設定
                Call mSetDisplayEnable(False, strWaitMsg1, strWaitMsg2)

                ''テキストクリア
                txtMsg.Text = ""

                ''ヘッダーメッセージ出力
                Call mAddMsgText(" --- SEARCH FOR THE OUTPUT CHANNEL SETTING --- ", " --- 出力チャンネル検索 --- ")
                Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
                Call mAddMsgText("", "")

                Call mAddMsgText("The search begins.", "検索を開始します。")
                Call mAddMsgText("", "")

                If optText.Checked Then
                    Call mAddMsgText(" CH NO | FU Address | CH Type", " CH NO | FU Address | CH Type")
                    Call mAddMsgText("-------+------------+-----------------", "-------+------------+-----------------")
                ElseIf optCsv.Checked Then
                    Call mAddMsgText("CH NO,FU Address,CH Type", " CH NO,FU Address,CH Type")
                End If

                ''出力ファイル用ヘッダー
                If optText.Checked Then
                    ReDim mstrCsv(1)
                    mstrCsv(0) = " CH NO | FU Address | CH Type"
                    mstrCsv(1) = "-------+------------+-----------------"

                ElseIf optCsv.Checked Then
                    ReDim mstrCsv(0)
                    mstrCsv(0) = "CH NO,FU Address,CH Type"
                End If


                ''検索開始 ------------------------------------------------------------------
                For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                    With gudt.SetChInfo.udtChannel(i)

                        If .udtChCommon.shtChType <> 0 And .udtChCommon.shtChno <> 0 Then

                            ''CH No
                            strChNo = gGet2Byte(.udtChCommon.shtChno).ToString("0000")

                            If .udtChCommon.shtChType = gCstCodeChTypeMotor Then        ''<モーターCH> ---------------------

                                ''CH Type
                                strChType = "Motor"

                                'Ver2.0.0.2 モーター種別増加 R Device追加
                                If .udtChCommon.shtData <> gCstCodeChDataTypeMotorDevice And _
                                   .udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom And _
                                   .udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom55 And _
                                   .udtChCommon.shtData <> gCstCodeChDataTypeMotorRDevice Then

                                    ''FU Address
                                    intFuNo = .MotorFuNo     ''Fu No
                                    intPortNo = .MotorPortNo ''Port No
                                    intPin = .MotorPin       ''Pin
                                    strFuAddress = gConvFuAddress(intFuNo, intPortNo, intPin)

                                End If

                            ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    ''<バルブCH> -----------------------

                                ''CH Type
                                strChType = ""
                                Select Case .udtChCommon.shtData
                                    Case gCstCodeChDataTypeValveDI_DO : strChType = "Valve(DI-DO)"
                                    Case gCstCodeChDataTypeValveDO : strChType = "Valve(DO)"
                                    Case gCstCodeChDataTypeValveJacom : strChType = "Valve(Jacom)"
                                    Case gCstCodeChDataTypeValveJacom : strChType = "Valve(Jacom55)"
                                    Case gCstCodeChDataTypeValveExt : strChType = "Valve(Ext Panel)"
                                    Case gCstCodeChDataTypeValveAI_DO1 : strChType = "Valve(AI-DO)"
                                    Case gCstCodeChDataTypeValveAI_DO2 : strChType = "Valve(AI-DO)"
                                    Case gCstCodeChDataTypeValveAI_AO1 : strChType = "Valve(AI-AO)"
                                    Case gCstCodeChDataTypeValveAI_AO2 : strChType = "Valve(AI-AO)"
                                    Case gCstCodeChDataTypeValveAO_4_20 : strChType = "Valve(AO 4_20mA)"
                                End Select

                                ''FU Address
                                Select Case .udtChCommon.shtData

                                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveExt

                                        intFuNo = .ValveDiDoFuNo         ''Fu No
                                        intPortNo = .ValveDiDoPortNo     ''Port No
                                        intPin = .ValveDiDoPin           ''Pin
                                        strFuAddress = gConvFuAddress(intFuNo, intPortNo, intPin)

                                    Case gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55

                                        strFuAddress = .ValveDiDoPin.ToString   ''Port No

                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                        intFuNo = .ValveAiDoFuNo         ''Fu No
                                        intPortNo = .ValveAiDoPortNo     ''Port No
                                        intPin = .ValveAiDoPin           ''Pin
                                        strFuAddress = gConvFuAddress(intFuNo, intPortNo, intPin)

                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20

                                        intFuNo = .ValveAiAoFuNo         ''Fu No
                                        intPortNo = .ValveAiAoPortNo     ''Port No
                                        intPin = .ValveAiAoPin           ''Pin
                                        strFuAddress = gConvFuAddress(intFuNo, intPortNo, intPin)

                                End Select

                            End If

                            ''出力 ---------------------------------------------------------------------------------------------------
                            If .udtChCommon.shtChType = gCstCodeChTypeMotor Or .udtChCommon.shtChType = gCstCodeChTypeValve Then

                                If strFuAddress <> "" Then  ''出力側は設定なしの場合あり

                                    ''出力ファイル用データ行作成
                                    ReDim Preserve mstrCsv(UBound(mstrCsv) + 1)

                                    If optText.Checked Then     ''テキスト形式

                                        Call mAddMsgText(" " & strChNo.PadLeft(5) & " | " & strFuAddress.PadRight(10) & " | " & strChType, _
                                                         " " & strChNo.PadLeft(5) & " | " & strFuAddress.PadRight(10) & " | " & strChType)

                                        ''出力ファイル用
                                        mstrCsv(UBound(mstrCsv)) = " " & strChNo.PadLeft(5) & " | " & strFuAddress.PadRight(10) & " | " & strChType

                                    ElseIf optCsv.Checked Then  ''CSV形式

                                        Call mAddMsgText(strChNo & "," & strFuAddress & "," & strChType, _
                                                         strChNo & "," & strFuAddress & "," & strChType)

                                        ''出力ファイル用
                                        mstrCsv(UBound(mstrCsv)) = strChNo & "," & strFuAddress & "," & strChType

                                    End If

                                End If

                            End If

                            If mblnCancelFlg Then Exit For

                        End If

                    End With
                Next

                '▼▼▼ 20110705 出力設定ファイル、シーケンス設定の出力設定の検索はここに追加する ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''検索終了 ------------------------------------------------------------------

                If mblnCancelFlg = False Then
                    Call mAddMsgText("", "")
                    Call mAddMsgText("Finished searching.", "検索が終了しました。")
                    Call mAddMsgText("", "")
                Else
                    Call mAddMsgText("", "")
                    Call mAddMsgText("The search was canceled.", "検索をキャンセルしました。")
                    Call mAddMsgText("", "")
                End If

                ''画面設定
                Call mSetDisplayEnable(True)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ファイル出力ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutput.Click

        Try

            Dim dlgOutput As New SaveFileDialog()
            Dim udtMsgResult As DialogResult

            If mstrCsv Is Nothing Then Return

            With dlgOutput

                If optText.Checked Then
                    .FileName = "OutPut_CH_" & Format(Now, "yyyyMMddHHmm") & ".txt"
                    .Filter = "TEXT File(*.txt)|*.txt"

                ElseIf optCsv.Checked Then
                    .FileName = "OutPut_CH_" & Format(Now, "yyyyMMddHHmm") & ".csv"
                    .Filter = "CSV File(*.txt)|*.csv"
                End If

                .InitialDirectory = gGetAppPath()
                .FilterIndex = 1
                '.Title = "保存先のファイルを選択してください"
                .RestoreDirectory = True
                .OverwritePrompt = True
                .CheckPathExists = True

                ''ダイアログを表示する
                If .ShowDialog() = DialogResult.OK Then

                    Try

                        ''ファイル出力
                        Dim intFileNum As Integer = FreeFile()
                        Call FileOpen(intFileNum, .FileName, OpenMode.Append)
                        For i As Integer = 0 To UBound(mstrCsv)
                            Call Print(intFileNum, mstrCsv(i) & vbNewLine)
                        Next
                        Call FileClose(intFileNum)

                        udtMsgResult = MessageBox.Show("The file is output successfully." & vbNewLine & vbNewLine & .FileName, _
                                                       "CH OutPut Setting", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception

                    End Try

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmCmpCompier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Dispose()
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "内部関数"

#Region "ログテキスト追加"

    '--------------------------------------------------------------------
    ' 機能      : テキスト表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) メッセージ
    ' 機能説明  : 処理メッセージをテキストに表示する
    '--------------------------------------------------------------------
    Private Sub mAddMsgText(ByVal strMsgEng As String, _
                            ByVal strMsgJpn As String)

        Try

            ''メッセージ追加
            txtMsg.Text &= IIf(mblnEnglish, strMsgEng, strMsgJpn) & vbCrLf '  vbNewLine

            ''表示位置設定
            txtMsg.SelectionStart = Len(txtMsg.Text)
            txtMsg.Focus()
            txtMsg.ScrollToCaret()

            ''テキスト更新
            Call txtMsg.Refresh()
            Call Application.DoEvents()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面設定"

    Private Sub mSetDisplayEnable(ByVal blnFlg As Boolean, _
                         Optional ByVal strWaitMsg1 As String = "", _
                         Optional ByVal strWaitMsg2 As String = "")

        If blnFlg Then

            ''Waitフレーム
            fraWait.Visible = False
            cmdPrint.Enabled = True
            cmdOutput.Enabled = True
            cmdExit.Enabled = True
            cmdSearch.Text = "Search"

            mblnCancelFlg = True

        Else

            ''Waitフレーム
            lblWait1.Text = strWaitMsg1
            lblWait2.Text = strWaitMsg2
            fraWait.Visible = True
            fraWait.Refresh()

            cmdPrint.Enabled = False
            cmdOutput.Enabled = False
            cmdExit.Enabled = False
            'cmdSearch.Text = "Search Cancel"

            mblnCancelFlg = False

        End If

    End Sub

#End Region

#End Region

End Class