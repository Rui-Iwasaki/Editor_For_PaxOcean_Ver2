Public Class frmSeqSetInputData

#Region "定数定義"



#End Region

#Region "変数定義"

    Dim mintRtn As Integer
    Dim mintSetNo As Integer
    Dim mudtSequenceSetInput As gTypSetSeqSetRecInput

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
                          ByRef udtSequenceSetInput As gTypSetSeqSetRecInput, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''戻り値初期化
            mintRtn = 1

            ''引数保存
            mintSetNo = intSetNo
            mudtSequenceSetInput = udtSequenceSetInput

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
    Private Sub frmSeqSetInputData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''画面初期化
            Call mInitDisplay()

            ''画面表示
            Call mSetDisplay(mudtSequenceSetInput)

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
    Private Sub frmSeqSetInputData_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

#Region "イベントハンドラ操作"

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

                    ''未設定時
                    optDataData.Checked = True
                    optTypeNonInvert.Checked = True
                    optDataAnalog.Checked = True

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

#End Region

End Class
