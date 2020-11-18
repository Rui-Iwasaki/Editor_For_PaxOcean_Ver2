Public Class frmSeqSetInputData_GAI

#Region "定数定義"

    Const cstExtGroupDumy As Integer = 9999

#End Region

#Region "変数定義"

    Dim mintRtn As Integer
    Dim mintSetNo As Integer
    Dim mudtSequenceSetInput As gTypSetSeqSetRecInput

    Dim mintLogicNo As Integer

    Private praryCHLIST As ArrayList    '計測点CHno(配列格納順)

    Private printCalcOpe(5) As Integer  'CalcのOperationの属性
    '-1:常にOK
    ' 0:常にNG
    ' 1:デジタル
    ' 2:アナログ
    ' 3:パルス
    ' 4:RH
    ' 5:エッジ

    Private mudtSetSequenceSet As gTypSetSeqSet
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) InputCH構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intSetNo As Integer, _
                          ByRef udtSetSequenceSet As gTypSetSeqSet, _
                          ByRef udtSequenceSetInput As gTypSetSeqSetRecInput, _
                          ByVal intLogicNo As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''戻り値初期化
            mintRtn = 1

            ''引数保存
            mintSetNo = intSetNo
            mudtSequenceSetInput = udtSequenceSetInput
            mintLogicNo = intLogicNo

            mudtSetSequenceSet = udtSetSequenceSet

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtSequenceSetInput = mudtSequenceSetInput
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetInputData_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '計測点リストのCHNoを配列順に格納(CHnoから配列番号を取得するために使用)
            praryCHLIST = New ArrayList
            With gudt.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    praryCHLIST.Add(.udtChannel(i).udtChCommon.shtChno.ToString("0000"))
                Next i
            End With


            ''画面初期化
            Call mInitDisplay()


            ''画面表示
            Call mSetDisplay(mudtSequenceSetInput)

            '入力制御
            Call subSetInputCtrl()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値格納
            Call mSetStructure(mudtSequenceSetInput)

            mintRtn = 0
            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetInputData_GAI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtData.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Reference status KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtManualInputReferenceStatus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualInputReferenceStatus.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Input mask KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtInputMask_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualInputInputMask.KeyPress, _
                                                                                                                         txtExtInputMask.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Input type KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtManualInputInputType_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualInputInputType.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.Validated

        Try

            If IsNumeric(txtData.Text) Then

                txtData.Text = Integer.Parse(txtData.Text).ToString("0000")

                'ここでLogicNoとCHTypeで入力制限を行う
                Call subSetInputCtrl()
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： CH No. 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtChNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChNo.Validating

    '    e.Cancel = gChkTextNumSpan(0, 65535, txtChNo.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Reference status　入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtManualInputReferenceStatus_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtManualInputReferenceStatus.Validating

    '    e.Cancel = gChkTextNumSpan(0, 255, txtManualInputReferenceStatus.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Input mask　入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtInputMask_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtManualInputInputMask.Validating, _
    '                                                                                                                      txtExtInputMask.Validating
    '    e.Cancel = gChkTextNumSpan(0, 65535, sender.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Input type　入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtManualInputInputType_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtManualInputInputType.Validating

    '    e.Cancel = gChkTextNumSpan(0, 255, txtManualInputInputType.Text)

    'End Sub

#End Region

#End Region

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面初期化
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mInitDisplay()

        Try

            fraAlarm.Location = fraData.Location
            fraManual.Location = fraData.Location
            fraCalc.Location = fraData.Location
            fraExtGroup.Location = fraData.Location

            optDataManual.Checked = True
            optTypeNonInvert.Checked = True

            optDataAnalog.Checked = True
            optAlarmAnalog.Checked = True
            optAlarmAnalogAnalog.Checked = True
            optOpeOutput1.Checked = True
            opExtGroupOut.Checked = True

            fraData.Visible = False
            fraAlarm.Visible = False
            fraManual.Visible = True
            fraCalc.Visible = False
            fraExtGroup.Visible = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 画面設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSeqSetRecInput)

        Try

            ''InputSetNo
            txtInputSetNo.Text = mintSetNo

            ''CH No
            txtData.Text = gConvZeroToNull(udtSet.shtChid, "0000")

            ''IOSel
            Select Case udtSet.shtIoSelect
                Case 0
                    'Ver2.0.7.7 新規入力時は、InputONとする。
                    'optIOSelInput.Checked = False
                    optIOSelInput.Checked = True
                    optIOSelOutput.Checked = False
                Case 1
                    optIOSelInput.Checked = True
                    optIOSelOutput.Checked = False
                Case 2
                    optIOSelInput.Checked = False
                    optIOSelOutput.Checked = True
            End Select

            ''Input Type 
            Select Case udtSet.bytType
                Case gCstCodeSeqInputTypeNonInvert : optTypeNonInvert.Checked = True   ''正転
                Case gCstCodeSeqInputTypeInvert : optTypeInver.Checked = True          ''反転
                Case gCstCodeSeqInputTypeOneShot : optTypeOneShot.Checked = True       ''１ショット
                Case Else : optTypeNonInvert.Checked = True                         ''ManualInputで手動入力をして上記３つ以外の値だった場合
            End Select

            'StatusとChSelectが不一致の場合
            'ChSelectにゼロを格納する
            Select Case udtSet.bytStatus And &HF0
                Case &H0
                    '完全に未設定とする
                    udtSet.shtChSelect = 0
                Case &H10
                    'DATA
                    If udtSet.shtChSelect <> gCstCodeSeqChSelectData Then
                        udtSet.shtChSelect = 0
                    End If
                Case &H20
                    'ALARM
                    If udtSet.shtChSelect <> gCstCodeSeqChSelectAnalog Then
                        udtSet.shtChSelect = 0
                    End If
                Case &H30
                    'CalcInput
                    If udtSet.shtChSelect <> gCstCodeSeqChSelectCalc Then
                        udtSet.shtChSelect = 0
                    End If
                Case &H40
                    'ExtGroupInput
                    If udtSet.shtChSelect <> gCstCodeSeqChSelectExtGroup Then
                        udtSet.shtChSelect = 0
                    End If
                Case Else
                    'ManualInput
                    If udtSet.shtChSelect <> gCstCodeSeqChSelectManual Then
                        udtSet.shtChSelect = 0
                    End If
            End Select



            Select Case udtSet.shtChSelect
                Case gCstCodeSeqChSelectData

                    ''DATA設定内容表示
                    optDataData.Checked = True
                    Call mDispStatusData(udtSet)

                Case gCstCodeSeqChSelectAnalog

                    ''ALARM設定内容表示
                    optDataAlarm.Checked = True
                    Call mDispStatusALARM(udtSet)

                Case gCstCodeSeqChSelectCalc

                    ''CalcInput設定内容表示
                    optDataCalc.Checked = True
                    Call mDispStatusCalcInput(udtSet)

                Case gCstCodeSeqChSelectExtGroup

                    ''ExtGroupInput設定内容表示
                    optDataExtGroup.Checked = True
                    Call mDispStatusExtGroupInput(udtSet)

                Case gCstCodeSeqChSelectManual

                    ''ManualInput設定内容表示
                    optDataManual.Checked = True
                    Call mDispStatusManualInput(udtSet)

                Case gCstCodeSeqChSelectFixed   '' 定義間違い修正　ver.1.4.4 2012.05.07

                    ''Fixed設定内容表示
                    optDataFixed.Checked = True
                    Call mDispStatusFixed(udtSet)

                Case Else
                    '未設定時
                    'STATUSに値が入っている場合＝前画面で手入力された
                    'そこから割り出す 0xF0でAND演算した結果で分岐
                    Select Case udtSet.bytStatus And &HF0
                        Case &H0
                            '完全に未設定とする
                            optDataData.Checked = True
                            optTypeNonInvert.Checked = True
                            optDataAnalog.Checked = True
                        Case &H10
                            'DATA設定内容表示
                            optDataData.Checked = True
                            Call mDispStatusData(udtSet)
                        Case &H20
                            'ALARM設定内容表示
                            optDataAlarm.Checked = True
                            Call mDispStatusALARM(udtSet)
                        Case &H30
                            'CalcInput設定内容表示
                            optDataCalc.Checked = True
                            Call mDispStatusCalcInput(udtSet)
                        Case &H40
                            'ExtGroupInput設定内容表示
                            optDataExtGroup.Checked = True
                            Call mDispStatusExtGroupInput(udtSet)
                        Case Else
                            'ManualInput設定内容表示
                            optDataManual.Checked = True
                            Call mDispStatusManualInput(udtSet)
                    End Select
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) InputCH構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            ''CH No が入力されていない場合は設定なしの値をセット
            If Trim(txtData.Text) = "" Then

                udtSet.shtChid = 0
                udtSet.shtIoSelect = 0
                udtSet.shtChSelect = 0
                udtSet.bytStatus = 0
                udtSet.bytType = 0
                udtSet.shtMask = 0

            Else

                ''CH No
                udtSet.shtChid = CCUInt16(txtData.Text)

                If optIOSelInput.Checked Then
                    udtSet.shtIoSelect = 1
                ElseIf optIOSelOutput.Checked Then
                    udtSet.shtIoSelect = 2
                Else
                    udtSet.shtIoSelect = 0
                End If

                ''マニュアルインプットの場合はInputTypeの保存は行わない
                ''（入力値を下で保存する）
                If Not optDataManual.Checked Then

                    ''Input Type
                    If optTypeNonInvert.Checked Then
                        udtSet.bytType = gCstCodeSeqInputTypeNonInvert     ''正転
                    ElseIf optTypeInver.Checked Then
                        udtSet.bytType = gCstCodeSeqInputTypeInvert        ''反転
                    ElseIf optTypeOneShot.Checked Then
                        udtSet.bytType = gCstCodeSeqInputTypeOneShot       ''１ショット
                    End If

                End If

                With udtSet

                    If optDataData.Checked Then

                        ''DATA型情報格納
                        udtSet.shtChSelect = gCstCodeSeqChSelectData
                        Call mSetStatusData(mudtSequenceSetInput)

                    ElseIf optDataAlarm.Checked Then

                        ''ALARM型情報格納
                        udtSet.shtChSelect = gCstCodeSeqChSelectAnalog
                        Call mSetStatusALARM(mudtSequenceSetInput)

                    ElseIf optDataCalc.Checked Then

                        ''CalcInput型情報格納
                        udtSet.shtChSelect = gCstCodeSeqChSelectCalc
                        Call mSetStatusCalcInput(mudtSequenceSetInput)

                    ElseIf optDataExtGroup.Checked Then

                        'ExtGroupの場合、CHNoはダミー番号
                        udtSet.shtChid = cstExtGroupDumy

                        ''ExtGroup型情報格納
                        udtSet.shtChSelect = gCstCodeSeqChSelectExtGroup
                        Call mSetStatusExtGroupInput(mudtSequenceSetInput)

                    ElseIf optDataManual.Checked Then

                        ''ManualInput型情報格納
                        udtSet.shtChSelect = gCstCodeSeqChSelectManual
                        Call mSetStatusManualInput(mudtSequenceSetInput)

                    ElseIf optDataFixed.Checked Then

                        ''Fixed型情報格納 2012/02/15 K.Tanigawa
                        udtSet.shtChSelect = gCstCodeSeqChSelectFixed
                        Call mSetStatusFixed(mudtSequenceSetInput)

                    End If


                End With

            End If


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

            ''共通数値入力チェック
            If optDataCalc.Checked Then
                If Not gChkInputNum(txtData, 10001, 11024, "Seq No.", False, True) Then Return False
            Else
                If Not gChkInputNum(txtData, 0, 65535, "CH No.", False, True) Then Return False
            End If


            ''ManualInput型チェック項目
            If optDataManual.Checked Then
                If Not gChkInputNum(txtManualInputReferenceStatus, 0, 255, "Reference status", False, True) Then Return False
                If Not gChkInputNum(txtManualInputInputMask, 0, 65535, "Input Mask", False, True) Then Return False
                If Not gChkInputNum(txtManualInputInputType, 0, 255, "Input Type", False, True) Then Return False
            End If

            ''ExtGroup型チェック項目
            If optDataExtGroup.Checked Then
                If Not gChkInputNum(txtExtInputMask, 0, 65535, "Input Mask", False, True) Then Return False
            End If


            If optDataAlarm.Checked Then
                If optAlarmAnalog.Checked Then
                    If optAlarmAnalogAnalog.Checked Then
                        If chkAlarmSelectHiHI.Checked Or chkAlarmSelectHi.Checked Or chkAlarmSelectLo.Checked Or chkAlarmSelectLoLo.Checked Then
                        Else
                            MsgBox("Please attach a check to ALARM.", MsgBoxStyle.Exclamation, "InputError")
                            Return False
                        End If
                    End If
                End If
            End If

            If optDataData.Checked Then
                If optDataMotor.Checked Then
                    If optDataMotorRun.Checked Then
                        If chkRunSelect1.Checked Or chkRunSelect2.Checked Or chkRunSelect3.Checked Or chkRunSelect4.Checked Then
                        Else
                            MsgBox("Please attach a check to RUN.", MsgBoxStyle.Exclamation, "InputError")
                            Return False
                        End If
                    End If
                End If
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#Region "Status別データ表示関数"

#Region "DATA型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（DATA型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : DATA型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mDispStatusData(ByVal udtSet As gTypSetSeqSetRecInput)

        Try

            Select Case udtSet.bytStatus
                Case gCstCodeSeqStatusDataAnalog

                    ''DATA_ANALOG
                    optDataAnalog.Checked = True

                Case gCstCodeSeqStatusDataDigital

                    ''DATA_DIGITAL
                    optDataDigital.Checked = True

                Case gCstCodeSeqStatusDataPulse

                    ''DATA_PULSE
                    optDataPulse.Checked = True

                Case gCstCodeSeqStatusDataRunning

                    ''DATA_RUNNING
                    optDataRunning.Checked = True

                Case gCstCodeSeqStatusDataMotor

                    ''DATA_MOTOR
                    optDataMotor.Checked = True

                    If udtSet.shtMask = gCstCodeSeqMaskDataMotorSTBY Then

                        ''DATA_MOTOR_STBY
                        optDataMotorStBy.Checked = True

                    Else

                        ''DATA_MOTOR_RUN
                        optDataMotorRun.Checked = True
                        chkRunSelect1.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataMotorRun1, 2))) ''DATA_MOTOR_RUN1
                        chkRunSelect2.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataMotorRun2, 2))) ''DATA_MOTOR_RUN2
                        chkRunSelect3.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataMotorRun3, 2))) ''DATA_MOTOR_RUN3
                        chkRunSelect4.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataMotorRun4, 2))) ''DATA_MOTOR_RUN4

                    End If

                Case gCstCodeSeqStatusDataComposite

                    ''DATA_COMPOSITE
                    optDataComposite.Checked = True

                    ''DATA_COMPOSITE_BIT
                    chkCompositeSelect1.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit1, 2))) ''DATA_COMPOSITE_BIT1
                    chkCompositeSelect2.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit2, 2))) ''DATA_COMPOSITE_BIT2
                    chkCompositeSelect3.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit3, 2))) ''DATA_COMPOSITE_BIT3
                    chkCompositeSelect4.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit4, 2))) ''DATA_COMPOSITE_BIT4
                    chkCompositeSelect5.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit5, 2))) ''DATA_COMPOSITE_BIT5
                    chkCompositeSelect6.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit6, 2))) ''DATA_COMPOSITE_BIT6
                    chkCompositeSelect7.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit7, 2))) ''DATA_COMPOSITE_BIT7
                    chkCompositeSelect8.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskDataCompositeBit8, 2))) ''DATA_COMPOSITE_BIT8

                Case gCstCodeSeqStatusDataHigh

                    ''DATA_HIGH
                    optDataHigh.Checked = True

                Case gCstCodeSeqStatusDataLow

                    ''DATA_LOW
                    optDataLow.Checked = True

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ALARM型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（ALARM型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ALARM型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mDispStatusALARM(ByVal udtSet As gTypSetSeqSetRecInput)

        Try

            Select Case udtSet.bytStatus
                Case gCstCodeSeqStatusAlarmAnalog

                    ''ALARM_ANALOG
                    optAlarmAnalog.Checked = True

                    If udtSet.shtMask = gCstCodeSeqMaskAlarmAnalogSensor Then

                        ''ALARM_ANALOG_SENSOR
                        optAlarmAnalogSensor.Checked = True

                    Else

                        ''ALARM_ANALOG_ANALOG
                        optAlarmAnalogAnalog.Checked = True
                        chkAlarmSelectHiHI.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmAnalogHIHI, 2))) ''ALARM_ANALOG_ANALOG_HIHI
                        chkAlarmSelectHi.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmAnalogHI, 2))) ''ALARM_ANALOG_ANALOG_HI
                        chkAlarmSelectLo.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmAnalogLO, 2))) ''ALARM_ANALOG_ANALOG_LO
                        chkAlarmSelectLoLo.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmAnalogLOLO, 2))) ''ALARM_ANALOG_ANALOG_LOLO

                    End If

                Case gCstCodeSeqStatusAlarmDigital

                    ''ALARM_DIGITAL
                    optAlarmDigital.Checked = True

                Case gCstCodeSeqStatusAlarmMotor

                    ''ALARM_MOTOR
                    optAlarmMotor.Checked = True

                Case gCstCodeSeqStatusAlarmComposite

                    ''ALARM_COMPOSITE
                    optAlarmComposite.Checked = True

                    ''ALARM_COMPOSITE_ST
                    chkAlarmCompositeSelect1.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt1, 2))) ''ALARM_COMPOSITE_ST1
                    chkAlarmCompositeSelect2.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt2, 2))) ''ALARM_COMPOSITE_ST2
                    chkAlarmCompositeSelect3.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt3, 2))) ''ALARM_COMPOSITE_ST3
                    chkAlarmCompositeSelect4.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt4, 2))) ''ALARM_COMPOSITE_ST4
                    chkAlarmCompositeSelect5.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt5, 2))) ''ALARM_COMPOSITE_ST5
                    chkAlarmCompositeSelect6.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt6, 2))) ''ALARM_COMPOSITE_ST6
                    chkAlarmCompositeSelect7.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt7, 2))) ''ALARM_COMPOSITE_ST7
                    chkAlarmCompositeSelect8.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt8, 2))) ''ALARM_COMPOSITE_ST8
                    chkAlarmCompositeSelect9.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeSt9, 2))) ''ALARM_COMPOSITE_ST9
                    chkAlarmCompositeSelectFB.Checked = gBitCheck(udtSet.shtMask, CInt(System.Math.Log(gCstCodeSeqMaskAlarmCompositeStFB, 2))) ''ALARM_COMPOSITE_ST_FB

                Case gCstCodeSeqStatusAlarmHigh

                    ''ALARM_HighEdgeTrigger
                    optAlarmHigh.Checked = True

                Case gCstCodeSeqStatusAlarmLow

                    ''ALARM_LowEdgeTrigger
                    optAlarmLow.Checked = True

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ManualInput型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（ManualInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ManualInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mDispStatusManualInput(ByVal udtSet As gTypSetSeqSetRecInput)

        Try
            'Ver2.0.4.1 0なら空白にしないように変更

            ''ManualInput_ReferenceStatus
            'txtManualInputReferenceStatus.Text = IIf(udtSet.bytStatus = 0, "", udtSet.bytStatus)
            txtManualInputReferenceStatus.Text = IIf(udtSet.bytStatus = 0, "0", udtSet.bytStatus)

            ''ManualInput_InputMask
            'txtManualInputInputMask.Text = IIf(udtSet.shtMask = 0, "", udtSet.shtMask)
            txtManualInputInputMask.Text = IIf(udtSet.shtMask = 0, "0", udtSet.shtMask)

            ''ManualInput_InputType
            'txtManualInputInputType.Text = IIf(udtSet.bytType = 0, "", udtSet.bytType)
            txtManualInputInputType.Text = IIf(udtSet.bytType = 0, "0", udtSet.bytType)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "CalcInput型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（CalcInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : CalcInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mDispStatusCalcInput(ByVal udtSet As gTypSetSeqSetRecInput)

        Try

            Select Case udtSet.bytStatus
                Case gCstCodeSeqStatusCalcOutput1

                    ''CalcInput_OperationOutput1
                    optOpeOutput1.Checked = True

                Case gCstCodeSeqStatusCalcOutput2

                    ''CalcInput_OperationOutput2
                    optOpeOutput2.Checked = True

                Case gCstCodeSeqStatusCalcOutput3

                    ''CalcInput_OperationOutput3
                    optOpeOutput3.Checked = True

                Case gCstCodeSeqStatusCalcOutput4

                    ''CalcInput_OperationOutput4
                    optOpeOutput4.Checked = True

                Case gCstCodeSeqStatusCalcOutput5

                    ''CalcInput_OperationOutput5
                    optOpeOutput5.Checked = True

                Case gCstCodeSeqStatusCalcOutput6

                    ''CalcInput_OperationOutput6
                    optOpeOutput6.Checked = True

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ExtGroupInput型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（ExtGroupInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ExtGroupInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mDispStatusExtGroupInput(ByVal udtSet As gTypSetSeqSetRecInput)

        Try

            Select Case udtSet.bytStatus
                Case gCstCodeSeqStatusExtGroupOut

                    ''ExtGroupInput_ExtGroupOut
                    opExtGroupOut.Checked = True

                Case gCstCodeSeqStatusExtBzOut

                    ''ExtGroupInput_ExtBzOut
                    optExtBzOut.Checked = True

            End Select

            ''ExtGroupInput_InputMask
            txtExtInputMask.Text = IIf(udtSet.shtMask = 0, "", udtSet.shtMask)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "Fixed型"

    '--------------------------------------------------------------------
    ' 機能      : 画面設定（Fixed型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : Fixed型の設定表示を行う。ManualInput型と同じ内容。
    ' 備考      : 
    ' 
    ' 2012/02/16 K.Tanigawa Fixed型追加
    '--------------------------------------------------------------------
    Private Sub mDispStatusFixed(ByVal udtSet As gTypSetSeqSetRecInput)

        Try
            'Ver2.0.4.1 0なら空白にしないように変更

            ''ManualInput_ReferenceStatus
            'txtManualInputReferenceStatus.Text = IIf(udtSet.bytStatus = 0, "", udtSet.bytStatus)
            txtManualInputReferenceStatus.Text = IIf(udtSet.bytStatus = 0, "0", udtSet.bytStatus)

            ''ManualInput_InputMask
            'txtManualInputInputMask.Text = IIf(udtSet.shtMask = 0, "", udtSet.shtMask)
            txtManualInputInputMask.Text = IIf(udtSet.shtMask = 0, "0", udtSet.shtMask)

            ''ManualInput_InputType
            'txtManualInputInputType.Text = IIf(udtSet.bytType = 0, "", udtSet.bytType)
            txtManualInputInputType.Text = IIf(udtSet.bytType = 0, "0", udtSet.bytType)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "Status別データ設定関数"

#Region "DATA型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（DATA型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : DATA型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetStatusData(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            ''DATA_ANALOG
            If optDataAnalog.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataAnalog
                udtSet.shtMask = gCstCodeSeqMaskDataAnalog
            End If

            ''DATA_DIGITAL
            If optDataDigital.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataDigital
                udtSet.shtMask = gCstCodeSeqMaskDataDigital
            End If

            ''DATA_PULSE
            If optDataPulse.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataPulse
                udtSet.shtMask = gCstCodeSeqMaskDataPulse
            End If

            ''DATA_RUNNING
            If optDataRunning.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataRunning
                udtSet.shtMask = gCstCodeSeqMaskDataRunning
            End If

            ''DATA_MOTOR
            If optDataMotor.Checked Then

                ''.Statusはモーター
                udtSet.bytStatus = gCstCodeSeqStatusDataMotor

                ''DATA_MOTOR_RUN
                If optDataMotorRun.Checked Then
                    udtSet.shtMask = 0
                    If chkRunSelect1.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataMotorRun1 ''DATA_MOTOR_RUN1
                    If chkRunSelect2.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataMotorRun2 ''DATA_MOTOR_RUN2
                    If chkRunSelect3.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataMotorRun3 ''DATA_MOTOR_RUN3
                    If chkRunSelect4.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataMotorRun4 ''DATA_MOTOR_RUN4
                End If

                ''DATA_MOTOR_STBY
                If optDataMotorStBy.Checked Then
                    udtSet.shtMask = gCstCodeSeqMaskDataMotorSTBY
                End If

            End If

            ''DATA_COMPOSITE
            If optDataComposite.Checked Then

                ''.Statusはコンポジット
                udtSet.bytStatus = gCstCodeSeqStatusDataComposite

                ''DATA_COMPOSITE_BIT
                udtSet.shtMask = 0
                If chkCompositeSelect1.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit1 ''DATA_COMPOSITE_BIT1
                If chkCompositeSelect2.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit2 ''DATA_COMPOSITE_BIT2
                If chkCompositeSelect3.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit3 ''DATA_COMPOSITE_BIT3
                If chkCompositeSelect4.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit4 ''DATA_COMPOSITE_BIT4
                If chkCompositeSelect5.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit5 ''DATA_COMPOSITE_BIT5
                If chkCompositeSelect6.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit6 ''DATA_COMPOSITE_BIT6
                If chkCompositeSelect7.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit7 ''DATA_COMPOSITE_BIT7
                If chkCompositeSelect8.Checked Then udtSet.shtMask += gCstCodeSeqMaskDataCompositeBit8 ''DATA_COMPOSITE_BIT8

            End If

            ''DATA_HIGH
            If optDataHigh.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataHigh
                udtSet.shtMask = gCstCodeSeqMaskDataHigh
            End If

            ''DATA_LOW
            If optDataLow.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusDataLow
                udtSet.shtMask = gCstCodeSeqMaskDataLow
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ALARM型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（ALARM型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ALARM型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetStatusALARM(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            ''ALARM_ANALOG
            If optAlarmAnalog.Checked Then

                ''.Statusはアナログ
                udtSet.bytStatus = gCstCodeSeqStatusAlarmAnalog

                ''ALARM_ANALOG_ANALOG
                If optAlarmAnalogAnalog.Checked Then
                    udtSet.shtMask = 0
                    If chkAlarmSelectHiHI.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmAnalogHIHI ''ALARM_ANALOG_ANALOG_HIHI
                    If chkAlarmSelectHi.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmAnalogHI ''ALARM_ANALOG_ANALOG_HI
                    If chkAlarmSelectLo.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmAnalogLO ''ALARM_ANALOG_ANALOG_LO
                    If chkAlarmSelectLoLo.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmAnalogLOLO ''ALARM_ANALOG_ANALOG_LOLO
                End If

                ''ALARM_ANALOG_SENSOR
                If optAlarmAnalogSensor.Checked Then
                    udtSet.shtMask = gCstCodeSeqMaskAlarmAnalogSensor
                End If

            End If

            ''ALARM_DIGITAL
            If optAlarmDigital.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusAlarmDigital
                udtSet.shtMask = gCstCodeSeqMaskAlarmDigital
            End If

            ''ALARM_MOTOR
            If optAlarmMotor.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusAlarmMotor
                udtSet.shtMask = gCstCodeSeqMaskAlarmMotor
            End If

            ''ALARM_COMPOSITE
            If optAlarmComposite.Checked Then

                ''.Statusはコンポジット
                udtSet.bytStatus = gCstCodeSeqStatusAlarmComposite

                ''ALARM_COMPOSITE_ST
                udtSet.shtMask = 0
                If chkAlarmCompositeSelect1.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt1 ''ALARM_COMPOSITE_ST1
                If chkAlarmCompositeSelect2.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt2 ''ALARM_COMPOSITE_ST2
                If chkAlarmCompositeSelect3.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt3 ''ALARM_COMPOSITE_ST3
                If chkAlarmCompositeSelect4.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt4 ''ALARM_COMPOSITE_ST4
                If chkAlarmCompositeSelect5.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt5 ''ALARM_COMPOSITE_ST5
                If chkAlarmCompositeSelect6.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt6 ''ALARM_COMPOSITE_ST6
                If chkAlarmCompositeSelect7.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt7 ''ALARM_COMPOSITE_ST7
                If chkAlarmCompositeSelect8.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt8 ''ALARM_COMPOSITE_ST8
                If chkAlarmCompositeSelect9.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeSt9 ''ALARM_COMPOSITE_ST9
                If chkAlarmCompositeSelectFB.Checked Then udtSet.shtMask += gCstCodeSeqMaskAlarmCompositeStFB ''ALARM_COMPOSITE_ST_FB

            End If

            ''ALARM_HighEdgeTrigger
            If optAlarmHigh.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusAlarmHigh
                udtSet.shtMask = gCstCodeSeqMaskAlarmHigh
            End If

            ''ALARM_LowEdgeTrigger
            If optAlarmLow.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusAlarmLow
                udtSet.shtMask = gCstCodeSeqMaskAlarmLow
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ManualInput型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（ManualInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ManualInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetStatusManualInput(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            udtSet.bytStatus = CCbyte(Trim(txtManualInputReferenceStatus.Text))
            udtSet.bytType = CCbyte(Trim(txtManualInputInputType.Text))
            udtSet.shtMask = CCUInt16(Trim(txtManualInputInputMask.Text))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "CalcInput型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（CalcInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : CalcInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetStatusCalcInput(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            ''CalcInput_OperationOutput1
            If optOpeOutput1.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput1
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput1
            End If

            ''CalcInput_OperationOutput2
            If optOpeOutput2.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput2
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput2
            End If

            ''CalcInput_OperationOutput3
            If optOpeOutput3.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput3
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput3
            End If

            ''CalcInput_OperationOutput4
            If optOpeOutput4.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput4
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput4
            End If

            ''CalcInput_OperationOutput5
            If optOpeOutput5.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput5
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput5
            End If

            ''CalcInput_OperationOutput6
            If optOpeOutput6.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusCalcOutput6
                udtSet.shtMask = gCstCodeSeqMaskCalcOutput6
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ExtGroupInput型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（ExtGroupInput型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : ExtGroupInput型設定の表示を行う
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mSetStatusExtGroupInput(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            ''ExtGroupInput_ExtGroupOut
            If opExtGroupOut.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusExtGroupOut
            End If

            ''ExtGroupInput_ExtBzOut
            If optExtBzOut.Checked Then
                udtSet.bytStatus = gCstCodeSeqStatusExtBzOut
            End If

            ''ExtGroupInput_InputMask
            udtSet.shtMask = CCUInt16(Trim(txtExtInputMask.Text))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "Fixed型"

    '--------------------------------------------------------------------
    ' 機能      : 構造体設定（Fixed型）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) InputCH構造体
    ' 機能説明  : Fixed型の設定表示を行う。ManualInput型と同じ内容。
    ' 備考      : 
    ' 
    ' 2012/02/16 K.Tanigawa Fixed型追加
    '--------------------------------------------------------------------
    Private Sub mSetStatusFixed(ByRef udtSet As gTypSetSeqSetRecInput)

        Try

            udtSet.bytStatus = CCbyte(Trim(txtManualInputReferenceStatus.Text))
            udtSet.bytType = CCbyte(Trim(txtManualInputInputType.Text))
            udtSet.shtMask = CCUInt16(Trim(txtManualInputInputMask.Text))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "フレーム表示/非表示関連"

    '----------------------------------------------------------------------------
    ' 機能説明  ： DATA クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDataData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataData.CheckedChanged

        Try

            fraData.Visible = True
            fraAlarm.Visible = False
            fraManual.Visible = False
            fraCalc.Visible = False
            fraExtGroup.Visible = False
            fraInputType.Enabled = True

            lblData.Text = "CH No."

            pnlIO.Visible = True
            'optIOSelInput.Checked = True
            'optIOSelOutput.Checked = False

            lblData.Visible = True
            txtData.Visible = True
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Alarm クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDataAlarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataAlarm.CheckedChanged

        Try

            fraData.Visible = False
            fraAlarm.Visible = True
            fraManual.Visible = False
            fraCalc.Visible = False
            fraExtGroup.Visible = False
            fraInputType.Enabled = True

            lblData.Text = "CH No."

            pnlIO.Visible = True
            'optIOSelInput.Checked = True
            'optIOSelOutput.Checked = False

            lblData.Visible = True
            txtData.Visible = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Manual input クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDataManual_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataManual.CheckedChanged

        Try

            fraData.Visible = False
            fraAlarm.Visible = False
            fraManual.Visible = True
            fraCalc.Visible = False
            fraExtGroup.Visible = False
            fraInputType.Enabled = False

            lblData.Text = "CH No."

            pnlIO.Visible = True
            'optIOSelInput.Checked = True
            'optIOSelOutput.Checked = False

            lblData.Visible = True
            txtData.Visible = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Calculation input クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDataCalc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataCalc.CheckedChanged

        Try

            fraData.Visible = False
            fraAlarm.Visible = False
            fraManual.Visible = False
            fraCalc.Visible = True
            fraExtGroup.Visible = False
            fraInputType.Enabled = True

            lblData.Text = "Seq No."

            pnlIO.Visible = False
            'optIOSelInput.Checked = False
            'optIOSelOutput.Checked = False

            lblData.Visible = True
            txtData.Visible = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： EXT Group input クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDataExtGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataExtGroup.CheckedChanged

        Try

            fraData.Visible = False
            fraAlarm.Visible = False
            fraManual.Visible = False
            fraCalc.Visible = False
            fraExtGroup.Visible = True
            fraInputType.Enabled = True

            lblData.Text = "CH No."

            pnlIO.Visible = True
            'optIOSelInput.Checked = True
            'optIOSelOutput.Checked = False


            'ExtGroupの場合 CHNoを隠す
            lblData.Visible = False
            txtData.Visible = False
            If txtData.Text = "" Then
                txtData.Text = cstExtGroupDumy
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Fixed クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '
    ' 2012/02/15 K.Tanigawa Fixed:定数6追加
    '----------------------------------------------------------------------------
    Private Sub optDataFixed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataFixed.CheckedChanged

        Try

            fraData.Visible = False
            fraAlarm.Visible = False
            fraManual.Visible = True
            fraCalc.Visible = False
            fraExtGroup.Visible = False
            fraInputType.Enabled = False

            lblData.Text = "Tbl No."

            pnlIO.Visible = True
            'optIOSelInput.Checked = True
            'optIOSelOutput.Checked = False

            lblData.Visible = True
            txtData.Visible = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    ''DATA型
    Private Sub optDataAnalog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataAnalog.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataDigital_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataDigital.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataPulse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataPulse.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataRunning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataRunning.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataMotor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataMotor.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = True
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataHigh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataHigh.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataLow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataLow.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optMotorRun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataMotorRun.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorRunSelect.Enabled = True
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optMotorStBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataMotorStBy.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorRunSelect.Enabled = False
            fraDataCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optDataComposite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDataComposite.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraDataMotorSelect.Enabled = False
            fraDataCompositeSelect.Enabled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''ALARM型
    Private Sub optAnalogSelectAnalog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmAnalogAnalog.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogAlarmSelect.Enabled = True
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAnalogSelectSensor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmAnalogSensor.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogAlarmSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAlarmAnalog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmAnalog.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = True
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAlarmDigital_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmDigital.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''2010/12/09 変更（Compositeを選択した際にEnableが打ち消されるため、Handles から Composite の条件を削除）
    'Private Sub optAlarmMotor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmMotor.CheckedChanged, optAlarmComposite.CheckedChanged
    Private Sub optAlarmMotor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmMotor.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAlarmHigh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmHigh.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAlarmLow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmLow.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub optAlarmComposite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAlarmComposite.CheckedChanged

        Try

            If Not sender.Checked Then Return

            fraAlarmAnalogSelect.Enabled = False
            fraAlarmCompositeSelect.Enabled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#Region "LogicとCHTypeによる入力制限"
    Private Sub subSetInputCtrl()
        Try
            '一旦全OKにする
            Call subSetCtrlALL(True)

            'CHnoが空白なら何もしないで処理抜け
            If txtData.Text.Trim = "" Then
                Return
            End If
            'CHNoが数値ではないなら何もしないで処理抜け
            If IsNumeric(txtData.Text) = False Then
                Return
            End If

            'CHNoが10001以上ならCalc強制
            If CInt(txtData.Text) >= 10001 Then
                optDataData.Enabled = False
                optDataAlarm.Enabled = False
                optDataCalc.Enabled = True
                optDataExtGroup.Enabled = False

                optDataCalc.Checked = True

                'Calc制御処理
                Call subCalcCTRL(mintLogicNo, CInt(txtData.Text))
                Return
            End If

            'CHType(拡張)取得
            Dim idx As Integer = 0
            idx = praryCHLIST.IndexOf(txtData.Text)
            If idx < 0 Then
                '該当CHnoが存在しないなら何もしない
                Return
            End If
            Dim intCHtype As Integer = fnGetChType(idx)

            'mintLogicNo と intCHTypeで入力制限
            Call subInpCTRL(mintLogicNo, intCHtype)



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'CHTypeを割り出す（拡張）
    Private Function fnGetChType(pidx As Integer) As Integer
        '戻り値
        ' -1:該当無し＝異常
        '  1:アナログ
        '  2:デジタル
        '  3:モーター
        '  4:バルブDIDO
        '  5:バルブAIDO
        '  6:バルブAIAO
        '  7:バルブそれ以外
        '  8:デジタルコンポジット
        '  9:パルス
        ' 10:RH
        Dim intRet As Integer = -1
        Try
            Dim intCHtype As Integer = gudt.SetChInfo.udtChannel(pidx).udtChCommon.shtChType
            Dim intCHdata As Integer = gudt.SetChInfo.udtChannel(pidx).udtChCommon.shtData

            Select Case intCHtype
                Case gCstCodeChTypeAnalog
                    'アナログ
                    intRet = 1
                Case gCstCodeChTypeDigital
                    'デジタル
                    intRet = 2
                Case gCstCodeChTypeMotor
                    'モーター
                    intRet = 3
                Case gCstCodeChTypeValve
                    Select Case intCHdata
                        Case gCstCodeChDataTypeValveDI_DO
                            'バルブDIDO
                            intRet = 4
                        Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                            'バルブAIDO
                            intRet = 5
                        Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                            'バルブAIAO
                            intRet = 6
                        Case Else
                            'バルブそれ以外
                            intRet = 7
                    End Select
                Case gCstCodeChTypeComposite
                    'デジタルコンポジット
                    intRet = 8
                Case gCstCodeChTypePulse
                    Select Case intCHdata
                        Case gCstCodeChDataTypePulseTotal1_1, gCstCodeChDataTypePulseTotal1_10, _
                            gCstCodeChDataTypePulseTotal1_100, gCstCodeChDataTypePulseDay1_1, _
                            gCstCodeChDataTypePulseDay1_10, gCstCodeChDataTypePulseDay1_100, gCstCodeChDataTypePulseExtDev
                            'パルス
                            intRet = 9
                        Case gCstCodeChDataTypePulseRevoTotalHour, gCstCodeChDataTypePulseRevoTotalMin, _
                            gCstCodeChDataTypePulseRevoDayHour, gCstCodeChDataTypePulseRevoDayMin, _
                            gCstCodeChDataTypePulseRevoLapHour, gCstCodeChDataTypePulseRevoLapMin, _
                            gCstCodeChDataTypePulseRevoExtDev, gCstCodeChDataTypePulseRevoExtDevTotalMin, _
                            gCstCodeChDataTypePulseRevoExtDevDayHour, gCstCodeChDataTypePulseRevoExtDevDayMin, _
                            gCstCodeChDataTypePulseRevoExtDevLapHour, gCstCodeChDataTypePulseRevoExtDevLapMin
                            'RH
                            intRet = 10
                    End Select
                Case Else
                    '該当無し
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRet
    End Function

    '制御まとめて実行
    Private Sub subSetCtrlALL(pbONOFF As Boolean)
        Try
            optDataData.Enabled = pbONOFF
            optDataAnalog.Enabled = pbONOFF
            optDataDigital.Enabled = pbONOFF
            optDataPulse.Enabled = pbONOFF
            optDataRunning.Enabled = pbONOFF
            optDataMotor.Enabled = pbONOFF
            optDataComposite.Enabled = pbONOFF
            optDataHigh.Enabled = pbONOFF
            optDataLow.Enabled = pbONOFF

            optDataAlarm.Enabled = pbONOFF
            optAlarmAnalog.Enabled = pbONOFF
            optAlarmDigital.Enabled = pbONOFF
            optAlarmMotor.Enabled = pbONOFF
            optAlarmComposite.Enabled = pbONOFF
            optAlarmHigh.Enabled = pbONOFF
            optAlarmLow.Enabled = pbONOFF

            optDataCalc.Enabled = pbONOFF

            optDataExtGroup.Enabled = pbONOFF

            cmdOK.Enabled = pbONOFF
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '入力制御
    Private Sub subInpCTRL(pintLogic As Integer, pintCHtype As Integer)
        Try
            Select Case pintLogic
                Case 1 'digital AND
                    Call subLogic1(pintCHtype)
                Case 2 'digital OR
                    Call subLogic1(pintCHtype)  'digital ANDと同じ
                Case 3 'digital D Latch
                    Call subLogic3(pintCHtype)
                Case 4 'digital AND-OR
                    Call subLogic1(pintCHtype)  'digital ANDと同じ
                Case 5 'digital OR-AND
                    Call subLogic1(pintCHtype)  'digital ANDと同じ
                Case 6 'digital AND-AND-OR
                    Call subLogic1(pintCHtype)  'digital ANDと同じ
                Case 7 'analog through
                    Call subLogic7(pintCHtype)
                Case 8 'analog gate(ON/OFF)
                    Call subLogic8(pintCHtype)
                Case 9 'analog gate(an output data change at the Gate OFF)
                    Call subLogic8(pintCHtype) 'analog gate(ON/OFF)と同じ
                Case 10 'analog multiplexer
                    Call subLogic10(pintCHtype)
                Case 11 'average logic
                    Call subLogic11(pintCHtype)
                Case 12 'time subtraction(1input)
                    Call subLogic7(pintCHtype)  'analog throughと同じ
                Case 13 'time subtraction(2input)
                    Call subLogic13(pintCHtype)
                Case 14 'conditional addition
                    Call subLogic14(pintCHtype)
                Case 15 'linear table logic
                    Call subLogic7(pintCHtype)  'analog throughと同じ
                Case 16 'calculate logic
                    Call subLogic16(pintCHtype)
                Case 17 'data comparison
                    Call subLogic13(pintCHtype) 'time subtraction(2input)と同じ
                Case 18 'event timer
                    Call subLogic18(pintCHtype)
                Case 19 'logic pulse count
                    Call subLogic19(pintCHtype)
                Case 20 'Pulse count
                    Call subLogic20(pintCHtype)
                Case 21 'calculate Running hour
                    Call subLogic21(pintCHtype)
                Case 22 'Clear Running hour
                    Call subLogic22(pintCHtype)
                Case 23 'Save Date
                    Call subLogic22(pintCHtype) 'Clear Running hourと同じ
                Case 24 'Save Time
                    Call subLogic22(pintCHtype) 'Clear Running hourと同じ
                Case 25 'Pulse double lap
                    Call subLogic19(pintCHtype) 'logic pulse countと同じ
                Case 26 'Soft Switch
                    Call subLogic26(pintCHtype)
                Case 27 'Position Control
                    Call subLogic14(pintCHtype) 'conditional additionと同じ
                Case 28 'Integer Calculate Logic
                    Call subLogic16(pintCHtype) 'calculate logicと同じ
                Case 29 'Valve(AI-DO)
                    Call subLogic8(pintCHtype)  'analog gate(ON/OFF)と同じ
                Case 30 'Valve(AI-AO)
                    Call subLogic8(pintCHtype)  'analog gate(ON/OFF)と同じ
                Case 31 'Valve(DI-DO)
                    Call subLogic31(pintCHtype)
                Case 32 'Motor(Input-Output)
                    Call subLogic31(pintCHtype) 'Valve(DI-DO)と同じ
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#Region "入力制御詳細"
    'digital AND ,OR, ANDor,orAND,ANDandOr
    Private Sub subLogic1(pintCHtype As Integer)
        Try
            'mintSetNo
            Call subSetDigital(pintCHtype)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'digital D Latch
    Private Sub subLogic3(pintCHtype As Integer)
        Try
            'mintSetNo = 2のみ別処理
            If mintSetNo = 2 Then
                'デジタル制御＋エッジ
                Select Case pintCHtype
                    Case 1
                        '  1:アナログ
                        Call subSetCtrlALL(False)
                        'DATA Hi,LoのみOK
                        optDataData.Enabled = True
                        optDataHigh.Enabled = True
                        optDataLow.Enabled = True
                        'ALARM ANALOGのみOK
                        optDataAlarm.Enabled = True
                        optAlarmAnalog.Enabled = True
                        optAlarmHigh.Enabled = True
                        optAlarmLow.Enabled = True
                        'EXT
                        optDataExtGroup.Enabled = True
                        '選択
                        If optDataHigh.Checked = False And optDataLow.Checked = False Then
                            optDataHigh.Checked = True
                        End If
                        If optAlarmAnalog.Checked = False And optAlarmHigh.Checked = False And optAlarmLow.Checked = False Then
                            optAlarmAnalog.Checked = True
                        End If

                        If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                            optDataData.Checked = True
                        End If

                        'OKボタン
                        cmdOK.Enabled = True
                    Case 2
                        '  2:デジタル
                        Call subSetCtrlALL(False)
                        'DATA DIGITAL,Hi,LoのみOK
                        optDataData.Enabled = True
                        optDataDigital.Enabled = True
                        optDataHigh.Enabled = True
                        optDataLow.Enabled = True
                        'ALARM DIGITAL,Hi,LoのみOK
                        optDataAlarm.Enabled = True
                        optAlarmDigital.Enabled = True
                        optAlarmHigh.Enabled = True
                        optAlarmLow.Enabled = True
                        'EXT
                        optDataExtGroup.Enabled = True
                        '選択
                        If optDataDigital.Checked = False And optDataHigh.Checked = False And optDataLow.Checked = False Then
                            optDataDigital.Checked = True
                        End If
                        If optAlarmDigital.Checked = False And optAlarmHigh.Checked = False And optAlarmLow.Checked = False Then
                            optAlarmDigital.Checked = True
                        End If

                        If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                            optDataData.Checked = True
                        End If

                        'OKボタン
                        cmdOK.Enabled = True
                    Case 3
                        '  3:モーター
                        Call subSetCtrlALL(False)
                        'DATA MOTORのみOK
                        optDataData.Enabled = True
                        optDataMotor.Enabled = True
                        optDataHigh.Enabled = True
                        optDataLow.Enabled = True
                        'ALARM MOTORのみOK
                        optDataAlarm.Enabled = True
                        optAlarmMotor.Enabled = True
                        optAlarmHigh.Enabled = True
                        optAlarmLow.Enabled = True
                        'EXT
                        optDataExtGroup.Enabled = True
                        '選択
                        If optDataMotor.Checked = False And optDataHigh.Checked = False And optDataLow.Checked = False Then
                            optDataMotor.Checked = True
                        End If
                        If optAlarmMotor.Checked = False And optAlarmHigh.Checked = False And optAlarmLow.Checked = False Then
                            optAlarmMotor.Checked = True
                        End If

                        If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                            optDataData.Checked = True
                        End If

                        'OKボタン
                        cmdOK.Enabled = True
                    Case 8
                        '  8:デジタルコンポジット
                        Call subSetCtrlALL(False)
                        'DATA COMPOSITEのみOK
                        optDataData.Enabled = True
                        optDataComposite.Enabled = True
                        optDataHigh.Enabled = True
                        optDataLow.Enabled = True
                        'ALARM COMPOSITEのみOK
                        optDataAlarm.Enabled = True
                        optAlarmComposite.Enabled = True
                        optAlarmHigh.Enabled = True
                        optAlarmLow.Enabled = True
                        'EXT
                        optDataExtGroup.Enabled = True
                        '選択
                        If optDataComposite.Checked = False And optDataHigh.Checked = False And optDataLow.Checked = False Then
                            optDataComposite.Checked = True
                        End If
                        If optAlarmComposite.Checked = False And optAlarmHigh.Checked = False And optAlarmLow.Checked = False Then
                            optAlarmComposite.Checked = True
                        End If

                        If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                            optDataData.Checked = True
                        End If

                        'OKボタン
                        cmdOK.Enabled = True
                    Case 9
                        '  9:パルス
                        '全部不可
                        Call subSetCtrlALL(False)
                    Case 10
                        ' 10:RH
                        '全部不可
                        Call subSetCtrlALL(False)
                    Case 4, 5, 6, 7
                        '  4:バルブDIDO
                        '  5:バルブAIDO
                        '  6:バルブAIAO
                        '  7:バルブそれ以外
                        '全部不可
                        Call subSetCtrlALL(False)
                    Case Else
                        '不明 or 未設定
                        '全部許可
                        Call subSetCtrlALL(True)
                End Select
            Else
                Call subSetDigital(pintCHtype)
            End If
            
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog through ,linear table logic
    Private Sub subLogic7(pintCHtype As Integer)
        Try
            'mintSetNo = 1のみ処理 他は全禁止
            If mintSetNo = 1 Then
                Call subSetAnalog(pintCHtype)
            Else
                'InpCH1以外は全禁止
                Call subSetCtrlALL(False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog gate(ON/OFF) ,analog gate2 ,Valve(AI-DO) ,Valve(AI-AO)
    Private Sub subLogic8(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1
                    'アナログデータ
                    Call subSetAnalog(pintCHtype)
                Case 2
                    'ゲート デジタル
                    Call subSetDigital(pintCHtype)
                Case Else
                    'InpCH1,2以外は全禁止
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog multiplexer
    Private Sub subLogic10(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2,3のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1, 2
                    'アナログデータ
                    Call subSetAnalog(pintCHtype)
                Case 3
                    'ゲート デジタル
                    Call subSetDigital(pintCHtype)
                Case Else
                    'InpCH1,2,3以外は全禁止
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'average logic
    Private Sub subLogic11(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2,3のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1
                    'アナログデータ
                    Call subSetAnalog(pintCHtype)
                Case 2, 3
                    '開始トリガ,リセットトリガ デジタル
                    Call subSetDigital(pintCHtype)
                Case Else
                    'InpCH1,2,3以外は全禁止
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'time subtraction(2input) ,data comparison
    Private Sub subLogic13(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1, 2
                    'アナログデータ
                    Call subSetAnalog(pintCHtype)
                Case Else
                    'InpCH1,2,3以外は全禁止
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'conditional addition ,Position Control
    Private Sub subLogic14(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2,3,4アナログ処理 5,6,7,8デジタル処理
            Select Case mintSetNo
                Case 1, 2, 3, 4
                    'アナログデータ
                    Call subSetAnalog(pintCHtype)
                Case 5, 6, 7, 8
                    '条件 デジタル
                    Call subSetDigital(pintCHtype)
                Case Else
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'calculate logic ,Integer Calculate Logic
    Private Sub subLogic16(pintCHtype As Integer)
        Try
            'mintSetNo 全アナログ
            Call subSetAnalog(pintCHtype)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'event timer
    Private Sub subLogic18(pintCHtype As Integer)
        Try
            'mintSetNo = 1,2,3,4デジタル処理
            Select Case mintSetNo
                Case 1, 2, 3, 4
                    'デジタルデータ
                    Call subSetDigital(pintCHtype)
                Case Else
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'logic pulse count ,Pulse double lap
    Private Sub subLogic19(pintCHtype As Integer)
        Try
            'mintSetNo = 1のみ処理 他は全禁止
            If mintSetNo = 1 Then
                Call subSetPulse(pintCHtype)
            Else
                'InpCH1以外は全禁止
                Call subSetCtrlALL(False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Pulse count
    Private Sub subLogic20(pintCHtype As Integer)
        Try
            'mintSetNo
            Call subSetPulse(pintCHtype)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'calculate Running hour
    Private Sub subLogic21(pintCHtype As Integer)
        Try
            'mintSetNo
            Call subSetRH(pintCHtype)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Clear Running hour ,Save Date ,Save Time
    Private Sub subLogic22(pintCHtype As Integer)
        Try
            'mintSetNo = 1のみ処理
            If mintSetNo = 1 Then
                Call subSetDigitalHi(pintCHtype)
            Else
                '全部不可
                Call subSetCtrlALL(False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Soft Switch
    Private Sub subLogic26(pintCHtype As Integer)
        Try
            'mintSetNo = 1デジタル処理 2,3アナログ処理
            Select Case mintSetNo
                Case 1
                    'デジタル
                    Call subSetDigital(pintCHtype)
                Case 2, 3
                    'アナログ
                    Call subSetAnalog(pintCHtype)
                Case Else
                    Call subSetCtrlALL(False)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Valve(DI-DO) ,Motor(Input-Output)
    Private Sub subLogic31(pintCHtype As Integer)
        Try
            'mintSetNo 全禁止
            '全禁止
            Call subSetCtrlALL(False)
            
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#Region "入力制御詳細パーツ"
    Private Sub subSetAnalog(pintCHtype As Integer)
        Try
            Select Case pintCHtype
                Case 1
                    '  1:アナログ
                    Call subSetCtrlALL(False)
                    'DATA ANALOGのみOK
                    optDataData.Enabled = True
                    optDataAnalog.Enabled = True
                    '選択
                    optDataData.Checked = True
                    optDataAnalog.Checked = True
                    'OKボタン
                    cmdOK.Enabled = True
                Case 2
                    '  2:デジタル
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 3
                    '  3:モーター
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 8
                    '  8:デジタルコンポジット
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 9
                    '  9:パルス
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 10
                    ' 10:RH
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 4, 5, 6, 7
                    '  4:バルブDIDO
                    '  5:バルブAIDO
                    '  6:バルブAIAO
                    '  7:バルブそれ以外
                    '全部不可
                    Call subSetCtrlALL(False)
                Case Else
                    '不明 or 未設定
                    '全部許可
                    Call subSetCtrlALL(True)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetDigital(pintCHtype As Integer)
        Try
            Select Case pintCHtype
                Case 1
                    '  1:アナログ
                    Call subSetCtrlALL(False)
                    'ALARM ANALOG,EXTのみOK
                    optDataAlarm.Enabled = True
                    optAlarmAnalog.Enabled = True
                    optDataExtGroup.Enabled = True
                    '選択
                    If optDataAlarm.Checked = False Then
                        optDataAlarm.Checked = True
                        optAlarmAnalog.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 2
                    '  2:デジタル
                    Call subSetCtrlALL(False)
                    'DATA DIGITALのみOK
                    optDataData.Enabled = True
                    optDataDigital.Enabled = True
                    'ALARM DIGITALのみOK
                    optDataAlarm.Enabled = True
                    optAlarmDigital.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    optDataDigital.Checked = True
                    optAlarmDigital.Checked = True

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 3
                    '  3:モーター
                    Call subSetCtrlALL(False)
                    'DATA MOTORのみOK
                    optDataData.Enabled = True
                    optDataMotor.Enabled = True
                    'ALARM MOTORのみOK
                    optDataAlarm.Enabled = True
                    optAlarmMotor.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    optDataMotor.Checked = True
                    optAlarmMotor.Checked = True

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 8
                    '  8:デジタルコンポジット
                    Call subSetCtrlALL(False)
                    'DATA COMPOSITEのみOK
                    optDataData.Enabled = True
                    optDataComposite.Enabled = True
                    'ALARM COMPOSITEのみOK
                    optDataAlarm.Enabled = True
                    optAlarmComposite.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    optDataComposite.Checked = True
                    optAlarmComposite.Checked = True

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 9
                    '  9:パルス
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 10
                    ' 10:RH
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 4, 5, 6, 7
                    '  4:バルブDIDO
                    '  5:バルブAIDO
                    '  6:バルブAIAO
                    '  7:バルブそれ以外
                    '全部不可
                    Call subSetCtrlALL(False)
                Case Else
                    '不明 or 未設定
                    '全部許可
                    Call subSetCtrlALL(True)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetPulse(pintCHtype As Integer)
        Try
            Select Case pintCHtype
                Case 1
                    '  1:アナログ
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 2
                    '  2:デジタル
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 3
                    '  3:モーター
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 8
                    '  8:デジタルコンポジット
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 9
                    '  9:パルス
                    Call subSetCtrlALL(False)
                    'DATA PULSEのみOK
                    optDataData.Enabled = True
                    optDataPulse.Enabled = True
                    '選択
                    optDataPulse.Checked = True

                    optDataData.Checked = True

                    'OKボタン
                    cmdOK.Enabled = True
                Case 10
                    ' 10:RH
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 4, 5, 6, 7
                    '  4:バルブDIDO
                    '  5:バルブAIDO
                    '  6:バルブAIAO
                    '  7:バルブそれ以外
                    '全部不可
                    Call subSetCtrlALL(False)
                Case Else
                    '不明 or 未設定
                    '全部許可
                    Call subSetCtrlALL(True)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetRH(pintCHtype As Integer)
        Try
            Select Case pintCHtype
                Case 1
                    '  1:アナログ
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 2
                    '  2:デジタル
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 3
                    '  3:モーター
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 8
                    '  8:デジタルコンポジット
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 9
                    '  9:パルス
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 10
                    ' 10:RH
                    'DATA RHのみOK
                    optDataData.Enabled = True
                    optDataRunning.Enabled = True
                    '選択
                    optDataRunning.Checked = True

                    optDataData.Checked = True

                    'OKボタン
                    cmdOK.Enabled = True
                Case 4, 5, 6, 7
                    '  4:バルブDIDO
                    '  5:バルブAIDO
                    '  6:バルブAIAO
                    '  7:バルブそれ以外
                    '全部不可
                    Call subSetCtrlALL(False)
                Case Else
                    '不明 or 未設定
                    '全部許可
                    Call subSetCtrlALL(True)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub subSetDigitalHi(pintCHtype As Integer)
        Try
            'デジタル制御＋Hiエッジ
            Select Case pintCHtype
                Case 1
                    '  1:アナログ
                    Call subSetCtrlALL(False)
                    'DATA HiのみOK
                    optDataData.Enabled = True
                    optDataHigh.Enabled = True
                    'ALARM ANALOGのみOK
                    optDataAlarm.Enabled = True
                    optAlarmAnalog.Enabled = True
                    optAlarmHigh.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    If optDataHigh.Checked = False Then
                        optDataHigh.Checked = True
                    End If
                    If optAlarmAnalog.Checked = False And optAlarmHigh.Checked = False Then
                        optAlarmAnalog.Checked = True
                    End If

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 2
                    '  2:デジタル
                    Call subSetCtrlALL(False)
                    'DATA DIGITAL,HiのみOK
                    optDataData.Enabled = True
                    optDataDigital.Enabled = True
                    optDataHigh.Enabled = True
                    'ALARM DIGITAL,HiのみOK
                    optDataAlarm.Enabled = True
                    optAlarmDigital.Enabled = True
                    optAlarmHigh.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    If optDataDigital.Checked = False And optDataHigh.Checked = False Then
                        optDataDigital.Checked = True
                    End If
                    If optAlarmDigital.Checked = False And optAlarmHigh.Checked = False Then
                        optAlarmDigital.Checked = True
                    End If

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 3
                    '  3:モーター
                    Call subSetCtrlALL(False)
                    'DATA MOTORのみOK
                    optDataData.Enabled = True
                    optDataMotor.Enabled = True
                    optDataHigh.Enabled = True
                    'ALARM MOTORのみOK
                    optDataAlarm.Enabled = True
                    optAlarmMotor.Enabled = True
                    optAlarmHigh.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    If optDataMotor.Checked = False And optDataHigh.Checked = False Then
                        optDataMotor.Checked = True
                    End If
                    If optAlarmMotor.Checked = False And optAlarmHigh.Checked = False Then
                        optAlarmMotor.Checked = True
                    End If

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 8
                    '  8:デジタルコンポジット
                    Call subSetCtrlALL(False)
                    'DATA COMPOSITEのみOK
                    optDataData.Enabled = True
                    optDataComposite.Enabled = True
                    optDataHigh.Enabled = True
                    'ALARM COMPOSITEのみOK
                    optDataAlarm.Enabled = True
                    optAlarmComposite.Enabled = True
                    optAlarmHigh.Enabled = True
                    'EXT
                    optDataExtGroup.Enabled = True
                    '選択
                    If optDataComposite.Checked = False And optDataHigh.Checked = False Then
                        optDataComposite.Checked = True
                    End If
                    If optAlarmComposite.Checked = False And optAlarmHigh.Checked = False Then
                        optAlarmComposite.Checked = True
                    End If

                    If optDataData.Checked = False And optDataAlarm.Checked = False And optDataExtGroup.Checked = False Then
                        optDataData.Checked = True
                    End If

                    'OKボタン
                    cmdOK.Enabled = True
                Case 9
                    '  9:パルス
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 10
                    ' 10:RH
                    '全部不可
                    Call subSetCtrlALL(False)
                Case 4, 5, 6, 7
                    '  4:バルブDIDO
                    '  5:バルブAIDO
                    '  6:バルブAIAO
                    '  7:バルブそれ以外
                    '全部不可
                    Call subSetCtrlALL(False)
                Case Else
                    '不明 or 未設定
                    '全部許可
                    Call subSetCtrlALL(True)
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

    'Calc制御
    Private Sub subCalcCTRL(pintLogic As Integer, pintNo As Integer)
        Try
            Dim intMyLogic As Integer
            '入力番号から、その番号のロジックを割り出し、選択肢の名称とEnable制御
            intMyLogic = fnSetCalcName(pintNo)
            If intMyLogic < 0 Then
                'ロジック番号が正常ではないなら処理抜け
                Return
            End If

            '画面のロジック番号を元に、指定したロジックが設定できるか処理
            Select pintLogic
                Case 1 'digital AND
                    Call subCalc1()
                Case 2 'digital OR
                    Call subCalc1()  'digital ANDと同じ
                Case 3 'digital D Latch
                    Call subCalc3()
                Case 4 'digital AND-OR
                    Call subCalc1()  'digital ANDと同じ
                Case 5 'digital OR-AND
                    Call subCalc1()  'digital ANDと同じ
                Case 6 'digital AND-AND-OR
                    Call subCalc1()  'digital ANDと同じ
                Case 7 'analog through
                    Call subCalc7()
                Case 8 'analog gate(ON/OFF)
                    Call subCalc8()
                Case 9 'analog gate(an output data change at the Gate OFF)
                    Call subCalc8() 'analog gate(ON/OFF)と同じ
                Case 10 'analog multiplexer
                    Call subCalc10()
                Case 11 'average logic
                    Call subCalc11()
                Case 12 'time subtraction(1input)
                    Call subCalc7()  'analog throughと同じ
                Case 13 'time subtraction(2input)
                    Call subCalc13()
                Case 14 'conditional addition
                    Call subCalc14()
                Case 15 'linear table logic
                    Call subCalc7()  'analog throughと同じ
                Case 16 'calculate logic
                    Call subCalc16()
                Case 17 'data comparison
                    Call subCalc13() 'time subtraction(2input)と同じ
                Case 18 'event timer
                    Call subCalc18()
                Case 19 'logic pulse count
                    Call subCalc19()
                Case 20 'Pulse count
                    Call subCalc20()
                Case 21 'calculate Running hour
                    Call subCalc21()
                Case 22 'Clear Running hour
                    Call subCalc22()
                Case 23 'Save Date
                    Call subCalc22() 'Clear Running hourと同じ
                Case 24 'Save Time
                    Call subCalc22() 'Clear Running hourと同じ
                Case 25 'Pulse double lap
                    Call subCalc19() 'logic pulse countと同じ
                Case 26 'Soft Switch
                    Call subCalc26()
                Case 27 'Position Control
                    Call subCalc14() 'conditional additionと同じ
                Case 28 'Integer Calculate Logic
                    Call subCalc16() 'calculate logicと同じ
                Case 29 'Valve(AI-DO)
                    Call subCalc8()  'analog gate(ON/OFF)と同じ
                Case 30 'Valve(AI-AO)
                    Call subCalc8()  'analog gate(ON/OFF)と同じ
                Case 31 'Valve(DI-DO)
                    Call subCalc31()
                Case 32 'Motor(Input-Output)
                    Call subCalc31() 'Valve(DI-DO)と同じ
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '入力番号から、その番号のロジックを割り出し、選択肢の名称とEnable制御
    Private Function fnSetCalcName(pintNo As Integer) As Integer
        Dim intRet As Integer = -1

        Try
            Dim i As Integer = 0
            Dim intNo As Integer = -1
            Dim intLogic As Integer = -1

            '名称を基本にする
            optOpeOutput1.Text = "Operation Output 1"
            optOpeOutput2.Text = "Operation Output 2"
            optOpeOutput3.Text = "Operation Output 3"
            optOpeOutput4.Text = "Operation Output 4"
            optOpeOutput5.Text = "Operation Output 5"
            optOpeOutput6.Text = "Operation Output 6"

            '全て設定可能にする
            optOpeOutput1.Enabled = True
            optOpeOutput2.Enabled = True
            optOpeOutput3.Enabled = True
            optOpeOutput4.Enabled = True
            optOpeOutput5.Enabled = True
            optOpeOutput6.Enabled = True

            '属性を全てOK(-1)にする
            For i = 0 To UBound(printCalcOpe) Step 1
                printCalcOpe(i) = -1
            Next i

            '入力番号からロジックを割り出す
            intNo = pintNo - 10001
            If intNo < 0 Or intNo > 1023 Then
                '入力番号がロジック番号ではないなら処理抜け
                Return intRet
            End If
            'mudtSetSequenceSet
            intLogic = mudtSetSequenceSet.udtDetail(intNo).shtLogicType
            If intLogic <= 0 Then
                'ロジック指定されてないなら処理抜け
                Return intRet
            End If

            '指定されたロジックから、名称と制御と属性を設定
            '■属性
            '-1:常にOK
            ' 0:常にNG
            ' 1:デジタル
            ' 2:アナログ
            ' 3:パルス
            ' 4:RH
            ' 5:エッジ
            Select Case intLogic
                Case 1 'digital AND
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital AND result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 2 'digital OR
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital OR result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 3 'digital D Latch
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital D Latch result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 4 'digital AND-OR
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital AND OR result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 5 'digital OR-AND
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital OR AND result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 6 'digital AND-AND-OR
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "digital AND AND OR result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 1
                Case 7 'analog through
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "analog through result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 8 'analog gate(ON/OFF)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "analog gate(ON/OFF) result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 9 'analog gate(an output data change at the Gate OFF)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "analog gate(output) result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 10 'analog multiplexer
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "analog multiplexer result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 11 'average logic
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "average result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 12 'time subtraction(1input)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "time subtraction 1 result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 13 'time subtraction(2input)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "time subtraction 2 result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 14 'conditional addition
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "conditional result result"
                    optOpeOutput2.Text = "conditional count result"
                    optOpeOutput1.Enabled = True
                    optOpeOutput2.Enabled = True
                    printCalcOpe(0) = 2
                    printCalcOpe(1) = 2
                Case 15 'linear table logic
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "linear Y"
                    optOpeOutput2.Text = "linear Xn"
                    optOpeOutput3.Text = "linear Xn+1"
                    optOpeOutput4.Text = "linear Yn"
                    optOpeOutput5.Text = "linear Yn+1"
                    optOpeOutput6.Text = "linear N"
                    optOpeOutput1.Enabled = True
                    optOpeOutput2.Enabled = True
                    optOpeOutput3.Enabled = True
                    optOpeOutput4.Enabled = True
                    optOpeOutput5.Enabled = True
                    optOpeOutput6.Enabled = True
                    For i = 0 To UBound(printCalcOpe) Step 1
                        printCalcOpe(i) = 2
                    Next i
                Case 16 'calculate logic
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "calc result"
                    optOpeOutput2.Text = "calc result(float)"
                    optOpeOutput1.Enabled = True
                    optOpeOutput2.Enabled = True
                    printCalcOpe(0) = 2
                    printCalcOpe(1) = 2
                Case 17 'data comparison
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "comp result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2 'デジタル多端子
                Case 18 'event timer
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "timer result"
                    optOpeOutput2.Text = "High Edge"
                    optOpeOutput1.Enabled = True
                    optOpeOutput2.Enabled = True
                    printCalcOpe(0) = 1
                    printCalcOpe(1) = 5
                Case 19 'logic pulse count
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "logic pulse count result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 3
                Case 20 'Pulse count
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "pulse count result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 3
                Case 21 'calculate Running hour
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "calc Runnning hour result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 4
                Case 22 'Clear Running hour
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    '0件
                Case 23 'Save Date
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Save Data result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 24 'Save Time
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Save Time result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 25 'Pulse double lap
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Pulse double lap result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 3
                Case 26 'Soft Switch
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Soft Switch result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 27 'Position Control
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Position Control result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2 'デジタル多端子
                Case 28 'Integer Calculate Logic
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Int calc result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 29 'Valve(AI-DO)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Valve(AI-DO) result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2 'デジタル多端子
                Case 30 'Valve(AI-AO)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Valve(AI-AO) result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2
                Case 31 'Valve(DI-DO)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Valve(DI-DO) result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2 'デジタル多端子
                Case 32 'Motor(Input-Output)
                    '一旦全禁止
                    Call subSetCalcNameInit()
                    '設定
                    optOpeOutput1.Text = "Motor result"
                    optOpeOutput1.Enabled = True
                    printCalcOpe(0) = 2 'デジタル多端子
            End Select

            intRet = intLogic
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRet
    End Function
    Private Sub subSetCalcNameInit()
        Try
            'Calcを全部不可に一旦設定する
            Dim i As Integer = 0

            '名称
            optOpeOutput1.Text = "-"
            optOpeOutput2.Text = "-"
            optOpeOutput3.Text = "-"
            optOpeOutput4.Text = "-"
            optOpeOutput5.Text = "-"
            optOpeOutput6.Text = "-"

            '設定
            optOpeOutput1.Enabled = False
            optOpeOutput2.Enabled = False
            optOpeOutput3.Enabled = False
            optOpeOutput4.Enabled = False
            optOpeOutput5.Enabled = False
            optOpeOutput6.Enabled = False

            '属性を全てNG(0)にする
            For i = 0 To UBound(printCalcOpe) Step 1
                printCalcOpe(i) = 0
            Next i

            'OKボタン
            cmdOK.Enabled = False
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#Region "Calc制御詳細"
    'digital AND ,OR, ANDor,orAND,ANDandOr
    Private Sub subCalc1()
        Try
            'mintSetNo
            Call subSetCalcDigital()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'digital D Latch
    Private Sub subCalc3()
        Try
            'mintSetNo = 2のみ別処理
            If mintSetNo = 2 Then
                'デジタル制御＋エッジ
                Call subSetCalcDigitalEdg()
            Else
                Call subSetCalcDigital()
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog through ,linear table logic
    Private Sub subCalc7()
        Try
            'mintSetNo = 1のみ処理 他は全禁止
            If mintSetNo = 1 Then
                Call subSetCalcAnalog()
            Else
                'InpCH1以外は全禁止
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog gate(ON/OFF) ,analog gate2 ,Valve(AI-DO) ,Valve(AI-AO)
    Private Sub subCalc8()
        Try
            'mintSetNo = 1,2のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1
                    'アナログデータ
                    Call subSetCalcAnalog()
                Case 2
                    'ゲート デジタル
                    Call subSetCalcDigital()
                Case Else
                    'InpCH1,2以外は全禁止
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'analog multiplexer
    Private Sub subCalc10()
        Try
            'mintSetNo = 1,2,3のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1, 2
                    'アナログデータ
                    Call subSetCalcAnalog()
                Case 3
                    'ゲート デジタル
                    Call subSetCalcDigital()
                Case Else
                    'InpCH1,2,3以外は全禁止
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'average logic
    Private Sub subCalc11()
        Try
            'mintSetNo = 1,2,3のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1
                    'アナログデータ
                    Call subSetCalcAnalog()
                Case 2, 3
                    '開始トリガ,リセットトリガ デジタル
                    Call subSetCalcDigital()
                Case Else
                    'InpCH1,2,3以外は全禁止
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'time subtraction(2input) ,data comparison
    Private Sub subCalc13()
        Try
            'mintSetNo = 1,2のみ処理 他は全禁止
            Select Case mintSetNo
                Case 1, 2
                    'アナログデータ
                    Call subSetCalcAnalog()
                Case Else
                    'InpCH1,2,3以外は全禁止
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'conditional addition ,Position Control
    Private Sub subCalc14()
        Try
            'mintSetNo = 1,2,3,4アナログ処理 5,6,7,8デジタル処理
            Select Case mintSetNo
                Case 1, 2, 3, 4
                    'アナログデータ
                    Call subSetCalcAnalog()
                Case 5, 6, 7, 8
                    '条件 デジタル
                    Call subSetCalcDigital()
                Case Else
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'calculate logic ,Integer Calculate Logic
    Private Sub subCalc16()
        Try
            'mintSetNo 全アナログ
            Call subSetCalcAnalog()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'event timer
    Private Sub subCalc18()
        Try
            'mintSetNo = 1,2,3,4デジタル処理
            Select Case mintSetNo
                Case 1, 2, 3, 4
                    'デジタルデータ
                    Call subSetCalcDigital()
                Case Else
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'logic pulse count ,Pulse double lap
    Private Sub subCalc19()
        Try
            'mintSetNo = 1のみ処理 他は全禁止
            If mintSetNo = 1 Then
                Call subSetCalcPulse()
            Else
                'InpCH1以外は全禁止
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Pulse count
    Private Sub subCalc20()
        Try
            'mintSetNo
            Call subSetCalcPulse()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'calculate Running hour
    Private Sub subCalc21()
        Try
            'mintSetNo
            Call subSetCalcRH()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Clear Running hour ,Save Date ,Save Time
    Private Sub subCalc22()
        Try
            'mintSetNo = 1のみ処理
            If mintSetNo = 1 Then
                Call subSetCalcDigitalEdg()
            Else
                '全部不可
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Soft Switch
    Private Sub subCalc26()
        Try
            'mintSetNo = 1デジタル処理 2,3アナログ処理
            Select Case mintSetNo
                Case 1
                    'デジタル
                    Call subSetCalcDigital()
                Case 2, 3
                    'アナログ
                    Call subSetCalcAnalog()
                Case Else
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Valve(DI-DO) ,Motor(Input-Output)
    Private Sub subCalc31()
        Try
            'mintSetNo 全禁止
            '全禁止

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#Region "Calc制御詳細パーツ"
    Private Sub subSetCalcDigital()
        Try
            Dim i As Integer = 0

            '配列に格納することでループ処理可能にする
            Dim optTemp(5) As RadioButton
            optTemp(0) = optOpeOutput1
            optTemp(1) = optOpeOutput2
            optTemp(2) = optOpeOutput3
            optTemp(3) = optOpeOutput4
            optTemp(4) = optOpeOutput5
            optTemp(5) = optOpeOutput6

            '属性デジタルが入力OK 1,-1がOK
            For i = 0 To UBound(printCalcOpe) Step 1
                If printCalcOpe(i) = 1 Or printCalcOpe(i) = -1 Then
                    optTemp(i).Enabled = True
                    'OKボタン
                    cmdOK.Enabled = True
                Else
                    optTemp(i).Enabled = False
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetCalcAnalog()
        Try
            Dim i As Integer = 0

            '配列に格納することでループ処理可能にする
            Dim optTemp(5) As RadioButton
            optTemp(0) = optOpeOutput1
            optTemp(1) = optOpeOutput2
            optTemp(2) = optOpeOutput3
            optTemp(3) = optOpeOutput4
            optTemp(4) = optOpeOutput5
            optTemp(5) = optOpeOutput6

            '属性アナログが入力OK 2,-1がOK
            For i = 0 To UBound(printCalcOpe) Step 1
                If printCalcOpe(i) = 2 Or printCalcOpe(i) = -1 Then
                    optTemp(i).Enabled = True
                    'OKボタン
                    cmdOK.Enabled = True
                Else
                    optTemp(i).Enabled = False
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetCalcPulse()
        Try
            Dim i As Integer = 0

            '配列に格納することでループ処理可能にする
            Dim optTemp(5) As RadioButton
            optTemp(0) = optOpeOutput1
            optTemp(1) = optOpeOutput2
            optTemp(2) = optOpeOutput3
            optTemp(3) = optOpeOutput4
            optTemp(4) = optOpeOutput5
            optTemp(5) = optOpeOutput6

            '属性パルスが入力OK 3,-1がOK
            For i = 0 To UBound(printCalcOpe) Step 1
                If printCalcOpe(i) = 3 Or printCalcOpe(i) = -1 Then
                    optTemp(i).Enabled = True
                    'OKボタン
                    cmdOK.Enabled = True
                Else
                    optTemp(i).Enabled = False
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetCalcRH()
        Try
            Dim i As Integer = 0

            '配列に格納することでループ処理可能にする
            Dim optTemp(5) As RadioButton
            optTemp(0) = optOpeOutput1
            optTemp(1) = optOpeOutput2
            optTemp(2) = optOpeOutput3
            optTemp(3) = optOpeOutput4
            optTemp(4) = optOpeOutput5
            optTemp(5) = optOpeOutput6

            '属性RHが入力OK 4,-1がOK
            For i = 0 To UBound(printCalcOpe) Step 1
                If printCalcOpe(i) = 4 Or printCalcOpe(i) = -1 Then
                    optTemp(i).Enabled = True
                    'OKボタン
                    cmdOK.Enabled = True
                Else
                    optTemp(i).Enabled = False
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetCalcDigitalEdg()
        Try
            Dim i As Integer = 0

            '配列に格納することでループ処理可能にする
            Dim optTemp(5) As RadioButton
            optTemp(0) = optOpeOutput1
            optTemp(1) = optOpeOutput2
            optTemp(2) = optOpeOutput3
            optTemp(3) = optOpeOutput4
            optTemp(4) = optOpeOutput5
            optTemp(5) = optOpeOutput6

            '属性デジタル,エッジが入力OK 1,5,-1がOK
            For i = 0 To UBound(printCalcOpe) Step 1
                If printCalcOpe(i) = 1 Or printCalcOpe(i) = -1 Or printCalcOpe(i) = 5 Then
                    optTemp(i).Enabled = True
                    'OKボタン
                    cmdOK.Enabled = True
                Else
                    optTemp(i).Enabled = False
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

#End Region

#End Region

End Class
