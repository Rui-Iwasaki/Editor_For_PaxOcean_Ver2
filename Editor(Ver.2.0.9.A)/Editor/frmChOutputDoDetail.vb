Public Class frmChOutputDoDetail

#Region "定数定義"

    ''OutputMoveMentがAlarmの場合
    Private mcstStsAnalog() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER"}
    Private mcstBitAnalog() As Integer = {-1, 3, 1, 0, 2, 5, 6}

    Private mcstStsDigital() As String = {"ALARM"}
    Private mcstBitDigital() As Integer = {0}

    Private mcstStsMotor() As String = {"ALARM", "FA"}
    Private mcstBitMotor() As Integer = {0, 8}

    Private mcstStsValveDiDo() As String = {"ALARM1", "ALARM2", "ALARM3", "ALARM4", "ALARM5", "ALARM6", "ALARM7", "ALARM8", "ALARM9", "FA"}
    Private mcstBitValveDiDo() As Integer = {12, 13, 14, 15, 0, 1, 2, 3, 4, 8}

    Private mcstStsValveAiDo() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER", "FA"}
    Private mcstBitValveAiDo() As Integer = {-1, 3, 1, 0, 2, 5, 6, 8}

    Private mcstStsValveAiAo() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER", "FA"}
    Private mcstBitValveAiAo() As Integer = {-1, 3, 1, 0, 2, 5, 6, 8}

    Private mcstStsComposite() As String = {"ALARM1", "ALARM2", "ALARM3", "ALARM4", "ALARM5", "ALARM6", "ALARM7", "ALARM8", "ALARM9", "FA"}
    Private mcstBitComposite() As Integer = {12, 13, 14, 15, 0, 1, 2, 3, 4, 8}

    Private mcstStsPulse() As String = {"ALARM"}
    Private mcstBitPulse() As Integer = {0}

#End Region

#Region "変数定義"

    ''キャンセルボタンフラグ
    Private mintCancelFlag As Integer

    ''イベントキャンセルフラグ
    Private mintEventCancelFlag As Integer

    ''初期処理キャンセルフラグ
    Private mintFirstFlag As Integer

    Private mFlagTerminal As Boolean = False

    ''grdChNo1グリッドの選択行を退避しておく
    Private mgrdChRowIndex As Integer   ''0～23

    ''モーターのステータス情報格納
    Private mMotorStatus1() As String
    Private mMotorStatus2() As String
    Private mMotorBitPos1() As String
    Private mMotorBitPos2() As String

    ''デジタル出力情報格納
    Private Structure mDoInfo
        Public No As Integer
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID 又は 論理出力 ID
        Public Type As String       ''CHデータ、論理出力チャネルデータ
        Public Status As String     ''Output Movement
        Public Mask As Integer      ''Output Movement マスクデータ（ビットパターン）
        Public Output As String     ''CH OUT Type Setup
        Public Funo As String       ''FU 番号
        Public Portno As String     ''FU ポート番号
        Public Pin As String        ''FU 計測点番号
    End Structure
    Private mDoDetail As mDoInfo

    ''論理出力情報格納(24チャンネル分)
    Private Structure mOrAndInfo
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID
        Public Status As String     ''ステータス種類
        Public Mask As Integer      ''マスクデータ
    End Structure
    Private mOrAndDetail(23) As mOrAndInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数(端子台設定画面から)
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 構造体インデックス
    '           : ARG5 - (IO) デジタル出力情報
    '           : ARG6 - (IO) 論理出力情報格納(24チャンネル分)
    '--------------------------------------------------------------------
    Friend Function gShowTerminal(ByVal hRowNo As Integer, _
                                  ByRef hDoDetail As frmChTerminalDetail.mDoInfo, _
                                  ByRef hOrAndDetail() As frmChTerminalDetail.mOrAndInfo, _
                                  ByRef frmOwner As Form) As Integer

        Try

            ''デジタル出力情報
            With hDoDetail
                mDoDetail.No = .No
                mDoDetail.Sysno = .Sysno
                mDoDetail.Chid = .Chid
                mDoDetail.Type = .Type
                mDoDetail.Status = .Status
                mDoDetail.Mask = .Mask
                mDoDetail.Output = .Output
                mDoDetail.Funo = .Funo
                mDoDetail.Portno = .Portno
                mDoDetail.Pin = .Pin
            End With

            ''論理出力情報
            If hDoDetail.Type = 1 Or hDoDetail.Type = 2 Then

                For i As Integer = 0 To 23
                    mOrAndDetail(i).Sysno = hOrAndDetail(i).Sysno
                    mOrAndDetail(i).Chid = hOrAndDetail(i).Chid
                    mOrAndDetail(i).Status = hOrAndDetail(i).Status
                    mOrAndDetail(i).Mask = hOrAndDetail(i).Mask
                Next

            End If

            mFlagTerminal = True

            ''==================================================
            Call gShowFormModelessForCloseWait2(Me, frmOwner)
            ''==================================================

            If mintCancelFlag = 0 Then

                ''デジタル出力情報
                With hDoDetail
                    .Sysno = mDoDetail.Sysno
                    .Chid = mDoDetail.Chid
                    .Type = mDoDetail.Type
                    .Status = mDoDetail.Status
                    .Mask = mDoDetail.Mask
                    .Output = mDoDetail.Output
                    .Funo = mDoDetail.Funo
                    .Portno = mDoDetail.Portno
                    .Pin = mDoDetail.Pin
                End With

                ''論理出力情報
                If hDoDetail.Type = 1 Or hDoDetail.Type = 2 Then

                    For i As Integer = 0 To 23

                        hOrAndDetail(i).Sysno = mOrAndDetail(i).Sysno
                        hOrAndDetail(i).Chid = mOrAndDetail(i).Chid

                        If mOrAndDetail(i).Chid <> 0 Then
                            hOrAndDetail(i).Status = mOrAndDetail(i).Status
                            hOrAndDetail(i).Mask = mOrAndDetail(i).Mask
                        Else
                            hOrAndDetail(i).Status = 0
                            hOrAndDetail(i).Mask = 0
                        End If

                    Next

                End If

            End If

            gShowTerminal = mintCancelFlag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数(DO SETUP LISTから)
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 行番号
    '           : ARG5 - (IO) デジタル出力情報
    '           : ARG6 - (IO) 論理出力情報格納(24チャンネル分)
    ' 機能説明  : 
    ' 備考      : 未使用
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal hRowNo As Integer, _
                          ByRef hDoDetail As frmChOutputDoList.mDoInfo, _
                          ByRef hOrAndDetail() As frmChOutputDoList.mOrAndInfo) As Integer

        Try

            ''デジタル出力情報
            With hDoDetail
                mDoDetail.No = .No
                mDoDetail.Sysno = .Sysno
                mDoDetail.Chid = .Chid
                mDoDetail.Type = .Type
                mDoDetail.Status = .Status
                mDoDetail.Mask = .Mask
                mDoDetail.Output = .Output
                mDoDetail.Funo = .Funo
                mDoDetail.Portno = .Portno
                mDoDetail.Pin = .Pin
            End With

            ''論理出力情報
            If hDoDetail.Type = 1 Or hDoDetail.Type = 2 Then

                For i As Integer = 0 To 23
                    mOrAndDetail(i).Sysno = hOrAndDetail(i).Sysno
                    mOrAndDetail(i).Chid = hOrAndDetail(i).Chid
                    mOrAndDetail(i).Status = hOrAndDetail(i).Status
                    mOrAndDetail(i).Mask = hOrAndDetail(i).Mask
                Next

            End If

            ''==============
            Me.ShowDialog()
            ''==============

            If mintCancelFlag = 0 Then

                ''デジタル出力情報
                With hDoDetail
                    .Sysno = mDoDetail.Sysno
                    .Chid = mDoDetail.Chid
                    .Type = mDoDetail.Type
                    .Status = mDoDetail.Status
                    .Mask = mDoDetail.Mask
                    .Output = mDoDetail.Output
                    .Funo = mDoDetail.Funo
                    .Portno = mDoDetail.Portno
                    .Pin = mDoDetail.Pin
                End With

                ''論理出力情報
                If hDoDetail.Type = 1 Or hDoDetail.Type = 2 Then

                    For i As Integer = 0 To 23
                        hOrAndDetail(i).Sysno = mOrAndDetail(i).Sysno
                        hOrAndDetail(i).Chid = mOrAndDetail(i).Chid
                        hOrAndDetail(i).Status = mOrAndDetail(i).Status
                        hOrAndDetail(i).Mask = mOrAndDetail(i).Mask
                    Next

                End If

            End If

            gShow = mintCancelFlag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChOutputDoDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim intChID As Integer = 0
            Dim intChType As Integer = 0, intDataType As Integer = 0
            Dim blnAnalogUse(4) As Boolean, blnValveUse(5) As Boolean

            mintEventCancelFlag = 1

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''コンボボックス初期化
            Call gSetComboBox(cmbChOutType, gEnmComboType.ctChOutputDoChOutType)
            cmbChOutType.SelectedIndex = 0

            Call gSetComboBox(cmbOutputMovement, gEnmComboType.ctChOutputDoStatus)
            cmbOutputMovement.SelectedIndex = -1

            lblRowNo.Visible = False
            lblNo.Visible = False

            ''データ設定
            With mDoDetail

                If .Type = 1 Then
                    chkMaskOR.Checked = True    ''OR
                ElseIf .Type = 2 Then
                    chkMaskAnd.Checked = True   ''AND
                End If

                ''.Statusより先にセットする必要有り!
                If .Type = 0 Then
                    grdChNo1(1, 0).Value = If(.Chid = 0, "", Val(.Chid).ToString("0000"))
                Else
                    grdChNo1(1, 0).Value = IIf(mOrAndDetail(0).Chid = 0, "", CInt(mOrAndDetail(0).Chid).ToString("0000"))
                End If

                mintEventCancelFlag = 0
                mintFirstFlag = 1
                cmbChOutType.Text = .Output
                cmbOutputMovement.SelectedValue = .Status   ''.Status

                ''FU Address
                txtFuNo.Text = gGetFuName2(.Funo)
                txtPortNo.Text = IIf(.Portno = gCstCodeChNotSetFuPortByte, "", .Portno)
                txtPin.Text = IIf(.Pin = gCstCodeChNotSetFuPinByte, "", CInt(.Pin).ToString("00"))

                ''論理出力CHデータの場合
                If .Type = 1 Or .Type = 2 Then

                    For i As Integer = 0 To 23
                        grdChNo1(1, i).Value = IIf(mOrAndDetail(i).Chid = 0, "", CInt(mOrAndDetail(i).Chid).ToString("0000"))
                    Next

                End If

            End With

            If mFlagTerminal Then
                txtFuNo.ReadOnly = True
                txtPortNo.ReadOnly = True
                txtPin.ReadOnly = True
            End If

            mgrdChRowIndex = 0
            mintCancelFlag = 0
            mintFirstFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームShown
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChOutputDoDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            grdOutputMovement.CurrentCell = Nothing
            'grdChNo1.CurrentCell = Nothing
            'grdChNo1(1, 0).Selected = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChOutputDoDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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

            mintCancelFlag = 1
            Me.Close()

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

            Dim intChID As Integer = 0, intSystemNo As Integer = 0
            Dim intChType As Integer = 0, intStatus As Integer = 0, intDataType As Integer = 0
            Dim intCompIdx As Integer = 0, intPinNo As Integer = 0
            Dim strStatus As String = ""
            Dim blnAnalogUse(5) As Boolean, blnValveUse(6) As Boolean
            Dim intMask As Integer = 0, rw As Integer = 0

            Dim intKikicode As Integer = 0      '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加

            ''入力チェック
            If Not mChkInput() Then Return

            If cmbChOutType.SelectedValue <> 0 Then     ''Invalid 以外

                If chkMaskOR.Checked Then
                    mDoDetail.Type = 1
                ElseIf chkMaskAnd.Checked Then
                    mDoDetail.Type = 2
                Else
                    mDoDetail.Type = 0
                End If

                ''現在表示されているビットパターンを取り込む-------------------------------
                'grdChNo1.EndEdit()
                'rw = grdChNo1.CurrentRow.Index
                rw = mgrdChRowIndex     ''カレント行 GET
                intChID = Val(grdChNo1(1, rw).Value)

                ''チャンネル情報 GET
                If intChID > 0 Then
                    '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加
                    Call mGetChInfo(intChID, intSystemNo, intChType, intStatus, _
                                    intDataType, intCompIdx, intPinNo, strStatus, _
                                    blnAnalogUse, blnValveUse, intKikicode)
                End If

                ''マスクデータ（ビットパターン）
                intMask = 0
                If cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusAlarm Then         ''<ALARM>

                    Select Case intChType

                        Case gCstCodeChTypeAnalog

                            For i As Integer = LBound(mcstBitAnalog) To UBound(mcstBitAnalog)

                                If mcstBitAnalog(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                    ''対象CHの.useが1のアラームのビットを全てONにする
                                    For j As Integer = LBound(blnAnalogUse) To UBound(blnAnalogUse)
                                        intMask = gBitSet(intMask, mcstBitAnalog(j + 1), blnAnalogUse(j))
                                    Next
                                    Exit For
                                Else
                                    intMask = gBitSet(intMask, mcstBitAnalog(i), grdOutputMovement(0, i).Value)
                                End If

                            Next

                        Case gCstCodeChTypeDigital


                                For i As Integer = LBound(mcstBitDigital) To UBound(mcstBitDigital)
                                    intMask = gBitSet(intMask, mcstBitDigital(i), grdOutputMovement(0, i).Value)
                                Next

                        Case gCstCodeChTypeMotor

                            For i As Integer = LBound(mcstBitMotor) To UBound(mcstBitMotor)
                                intMask = gBitSet(intMask, mcstBitMotor(i), grdOutputMovement(0, i).Value)
                            Next

                        Case gCstCodeChTypeValve

                            Select Case intDataType

                                Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt
                                    For i As Integer = LBound(mcstBitValveDiDo) To UBound(mcstBitValveDiDo)
                                        intMask = gBitSet(intMask, mcstBitValveDiDo(i), grdOutputMovement(0, i).Value)
                                    Next

                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                                    For i As Integer = LBound(mcstBitValveAiDo) To UBound(mcstBitValveAiDo)

                                        If mcstBitValveAiDo(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                            ''対象CHの.useが1のアラームのビットを全てONにする
                                            For j As Integer = LBound(blnValveUse) To UBound(blnValveUse)
                                                intMask = gBitSet(intMask, mcstBitValveAiDo(j + 1), blnValveUse(j))
                                            Next
                                            Exit For
                                        Else
                                            intMask = gBitSet(intMask, mcstBitValveAiDo(i), grdOutputMovement(0, i).Value)
                                        End If

                                    Next

                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                                    For i As Integer = LBound(mcstBitValveAiAo) To UBound(mcstBitValveAiAo)

                                        If mcstBitValveAiAo(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                            ''対象CHの.useが1のアラームのビットを全てONにする
                                            For j As Integer = LBound(blnValveUse) To UBound(blnValveUse)
                                                intMask = gBitSet(intMask, mcstBitValveAiAo(j + 1), blnValveUse(j))
                                            Next
                                            Exit For
                                        Else
                                            intMask = gBitSet(intMask, mcstBitValveAiAo(i), grdOutputMovement(0, i).Value)
                                        End If

                                    Next

                            End Select

                        Case gCstCodeChTypeComposite

                            For i As Integer = LBound(mcstBitComposite) To UBound(mcstBitComposite)
                                intMask = gBitSet(intMask, mcstBitComposite(i), grdOutputMovement(0, i).Value)
                            Next

                        Case gCstCodeChTypePulse

                            For i As Integer = LBound(mcstBitPulse) To UBound(mcstBitPulse)
                                intMask = gBitSet(intMask, mcstBitPulse(i), grdOutputMovement(0, i).Value)
                            Next

                    End Select

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusOnOff Then     ''<ON/OFF>

                    If intKikicode = 602 Then       '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ:602 特殊処理
                        If grdOutputMovement(0, 0).Value = True Then    '' OFF
                            intMask = 0
                        Else        '' ON
                            intMask = 1
                        End If

                    Else
                        If grdOutputMovement(0, 0).Value = True Then

                            intMask = gBitSet(intMask, 6, True)
                        End If

                    End If

                    ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusMotor Then     ''<MOTOR>

                        If intChType = gCstCodeChTypeMotor Then
                            ''モーターCHの場合

                        ''MO追加「0x02」固定   2013.08.07  K.Fujimoto
                        'Ver2.0.0.2 モーター種別増加 R Device追加
                        If intDataType = gCstCodeChDataTypeMotorDevice Or intDataType = gCstCodeChDataTypeMotorDeviceJacom Or intDataType = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                           intDataType = gCstCodeChDataTypeMotorRDevice Then

                            If grdOutputMovement(0, 0).Value = True Then
                                intMask = gBitSet(intMask, 1, True)
                            End If

                        Else

                            Dim strwk() As String = Nothing
                            Dim strbp() As String = Nothing

                            ''ステータス情報を獲得する
                            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                            cmbStatus.SelectedValue = intStatus

                            If cmbStatus.SelectedIndex >= 0 Then

                                If intStatus = gCstCodeChManualInputStatus.ToString Then
                                    ''手入力

                                Else
                                    ''「_」区切りの文字列取得
                                    If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                       intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")

                                    Else
                                        'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                        If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                            intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                            '正常Rは正常ステータス扱い
                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                        End If
                                    End If

                                    ''取得したデータビット位置に基づいてマスクビットの作成
                                    For i = 0 To UBound(strwk)
                                        intMask = gBitSet(intMask, CCInt(strbp(i)), grdOutputMovement(0, i).Value)
                                    Next

                                End If

                            End If

                        End If

                        ElseIf intChType = gCstCodeChTypeComposite Then
                            ''デジタルコンポジットCHの場合
                            For i = 0 To grdOutputMovement.RowCount - 1
                                intMask = gBitSet(intMask, i, grdOutputMovement(0, i).Value)
                            Next

                        ElseIf intChType = gCstCodeChTypeValve Then     '' ver.1.4.5 2012.07.03 バルブCH追加
                            ''バルブCH(DI/DO)の場合
                            If intDataType = gCstCodeChDataTypeValveDI_DO Then
                                For i = 0 To grdOutputMovement.RowCount - 1
                                    intMask = gBitSet(intMask, i, grdOutputMovement(0, i).Value)
                                Next
                            End If
                        End If

                    End If

                    ''カレント行のビットパターン更新
                    If mDoDetail.Type = 0 Then
                    Else
                        With mOrAndDetail(rw)
                            .Chid = intChID                             ''CH
                            .Sysno = intSystemNo                        ''System No
                            .Status = cmbOutputMovement.SelectedValue   ''OutputMovement
                            .Mask = intMask
                        End With
                    End If
                    ''--------------------------------------------------------------------

                    ''保存処理------------------------------------------------------------
                    If mDoDetail.Type = 0 Then
                        ''CHデータ
                    intChID = Val(grdChNo1(1, 0).Value)
                    '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加
                    Call mGetChInfo(intChID, intSystemNo, intChType, intStatus, _
                                    intDataType, intCompIdx, intPinNo, strStatus, _
                                    blnAnalogUse, blnValveUse, intKikicode)

                        With mDoDetail
                            .Chid = intChID                             ''CH
                            .Sysno = intSystemNo                        ''System No
                            .Status = cmbOutputMovement.SelectedValue   ''OutputMovement

                            If rw = 0 Then .Mask = intMask ''Mask
                            .Output = cmbChOutType.Text                 ''CH Out Type
                        'T.Ueki
                        .Funo = txtFuNo.Text
                        '.Funo = gGetFuNo(txtFuNo.Text)              ''FU No
                            .Portno = txtPortNo.Text                    ''Port No
                            .Pin = txtPin.Text                          ''Pin No
                        End With

                    Else
                        ''論理出力チャンネル(OR, AND)
                        With mDoDetail
                            .Chid = -1  ''0以外を設定(frmChTerminalDetailでセット)
                            .Sysno = 0
                            .Status = ""
                            .Mask = 0
                            .Output = cmbChOutType.Text                 ''CH Out Type
                        .Funo = txtFuNo.Text
                        ' .Funo = gGetFuNo(txtFuNo.Text)              ''FU No
                            .Portno = txtPortNo.Text                    ''Port No
                            .Pin = txtPin.Text                          ''Pin No
                        End With
                    End If

            Else
                ''Invalid
                With mDoDetail
                    .Chid = "-1"    ''0 以外をセット
                    .Sysno = "0"
                    .Type = "0"
                    .Status = "0"
                    .Mask = 0
                    .Output = cmbChOutType.Text
                    .Funo = "0"
                    .Portno = "0"
                    .Pin = "0"
                End With

            End If

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： デジタル出力種類　変更時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbChOutType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChOutType.SelectedIndexChanged

        Try

            Select Case cmbChOutType.SelectedValue

                Case 0  ''Invalid
                    chkMaskOR.Checked = False
                    chkMaskAnd.Checked = False

                    chkMaskOR.Enabled = False
                    chkMaskAnd.Enabled = False

                    cmbOutputMovement.Enabled = False
                    cmbOutputMovement.Text = "ALARM"

                    grdOutputMovement.Enabled = False

                Case 1  ''ALM(FT,LT"
                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

                    cmbOutputMovement.Enabled = False
                    cmbOutputMovement.Text = "ALARM"
                    grdOutputMovement.Enabled = True

                Case 2  ''ALM(FT,-)
                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

                    cmbOutputMovement.Enabled = False
                    cmbOutputMovement.Text = "ALARM"
                    grdOutputMovement.Enabled = True

                Case 3  ''ALM(-,LT)
                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

                    cmbOutputMovement.Enabled = False
                    cmbOutputMovement.Text = "ALARM"
                    grdOutputMovement.Enabled = True

                Case 4  ''ALM(-,-)
                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

                    cmbOutputMovement.Enabled = False
                    cmbOutputMovement.Text = "ALARM"
                    grdOutputMovement.Enabled = True

                Case 5  ''CH(-,-)
                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

                    cmbOutputMovement.Enabled = True
                    grdOutputMovement.Enabled = True

                Case 6  ''RUN(-,LT)
                    'Ver2.0.2.1 Enable処理を変更
                    'chkMaskOR.Enabled = False
                    'chkMaskAnd.Enabled = False
                    'chkMaskOR.Checked = False
                    'chkMaskAnd.Checked = False

                    cmbOutputMovement.Enabled = True
                    grdOutputMovement.Enabled = True

                    chkMaskOR.Enabled = True
                    chkMaskAnd.Enabled = True

            End Select

            ''設定条件により複数CHの設定を可能にする
            Call mSetColorToCH()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OR/AND 設定変更時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub chkMaskOR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaskOR.CheckedChanged

        Try
            If cmbChOutType.SelectedValue = 0 Then Exit Sub

            If mintEventCancelFlag = 0 Then

                If chkMaskOR.Checked = False And chkMaskAnd.Checked = False Then

                    Call mChangeMaskBit(0)
                    mgrdChRowIndex = 0
                Else

                    Call mChangeMaskBit(grdChNo1.CurrentRow.Index)
                    mgrdChRowIndex = grdChNo1.CurrentRow.Index

                End If

            End If

            If chkMaskOR.Checked Then
                If chkMaskAnd.Checked Then chkMaskAnd.Checked = False
            End If
            Call mSetColorToCH()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub chkMaskAnd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaskAnd.CheckedChanged

        Try
            If cmbChOutType.SelectedValue = 0 Then Exit Sub

            If mintEventCancelFlag = 0 Then

                If chkMaskOR.Checked = False And chkMaskAnd.Checked = False Then

                    Call mChangeMaskBit(0)
                    mgrdChRowIndex = 0

                Else

                    Call mChangeMaskBit(grdChNo1.CurrentRow.Index)
                    mgrdChRowIndex = grdChNo1.CurrentRow.Index

                End If

            End If

            If chkMaskAnd.Checked Then
                If chkMaskOR.Checked Then chkMaskOR.Checked = False
            End If
            Call mSetColorToCH()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 出力条件　変更時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbOutputMovement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOutputMovement.SelectedIndexChanged

        Try
            Dim intRowIndexBk As Integer = 0

            If cmbChOutType.SelectedValue = 0 Then Exit Sub

            If mintEventCancelFlag = 1 Then Exit Sub

            If grdChNo1.CurrentRow.Index = Nothing Then
                Call mChangeMaskBit(0)
                mgrdChRowIndex = 0
            Else

                If chkMaskOR.Checked Or chkMaskAnd.Checked Then
                    ''論理出力
                    intRowIndexBk = grdChNo1.CurrentRow.Index
                    Call mChangeMaskBit(grdChNo1.CurrentRow.Index)
                    mgrdChRowIndex = intRowIndexBk

                Else
                    ''CHデータ
                    Call mChangeMaskBit(0)
                    mgrdChRowIndex = 0
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 出力条件チェック入力時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdOutputMovement_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOutputMovement.CellContentClick

        Try

            If e.ColumnIndex <> 0 Then Exit Sub

            With grdOutputMovement

                ''グリッドの保留中の変更を全て適用させる
                .EndEdit()

                ''空白の行はチェック不可
                If .Rows(e.RowIndex).Cells(1).Value = "" Then
                    .Rows(e.RowIndex).Cells(0).Value = False
                End If

                If cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusAlarm Then     ''ALARM

                    If e.RowIndex = 0 Then
                        ''ALL ALARM選択時はALL ALARM以外は選択不可
                        If .Rows(0).Cells(0).Value = True And .Rows(0).Cells(1).Value = "ALL ALARM" Then

                            For i As Integer = 1 To 9
                                .Rows(i).Cells(0).Value = False
                            Next

                        End If

                    Else
                        ''ALL-ALARM以外を選択した時はALL-ALARMの選択は不可
                        If .Rows(e.RowIndex).Cells(0).Value = True Then

                            If .Rows(0).Cells(1).Value = "ALL ALARM" Then
                                .Rows(0).Cells(0).Value = False
                            End If

                        End If
                    End If

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusOnOff Then     ''ON/OFF

                    ''チェックボックスをトグルにする
                    If e.RowIndex = 0 Then
                        If .Rows(0).Cells(0).Value = True Then
                            .Rows(1).Cells(0).Value = False
                        End If
                    Else
                        If .Rows(e.RowIndex).Cells(0).Value = True Then
                            .Rows(0).Cells(0).Value = False
                        End If
                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.クリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChNo1.CellClick
        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''論理出力CHの場合のみ
            If chkMaskOR.Checked Or chkMaskAnd.Checked Then

                Call mChangeMaskBit(e.RowIndex)

                mgrdChRowIndex = e.RowIndex

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 矢印ボタン（↓↑）, Enter　クリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdChNo1.KeyDown

        Try
            Dim intRowIndexBk As Integer = 0

            ''論理出力CHの場合のみ
            If chkMaskOR.Checked Or chkMaskAnd.Checked Then

                If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Enter Then     ''下矢印ボタン（↓） or  Enter

                    If grdChNo1.CurrentRow.Index = grdChNo1.RowCount - 1 Then Exit Sub

                    intRowIndexBk = grdChNo1.CurrentRow.Index + 1

                    Call mChangeMaskBit(grdChNo1.CurrentRow.Index + 1)

                    mgrdChRowIndex = intRowIndexBk

                ElseIf e.KeyCode = Keys.Up Then                             ''上矢印ボタン（↑）

                    If grdChNo1.CurrentRow.Index = 0 Then Exit Sub

                    intRowIndexBk = grdChNo1.CurrentRow.Index - 1

                    Call mChangeMaskBit(grdChNo1.CurrentRow.Index - 1)

                    mgrdChRowIndex = intRowIndexBk

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.　入力時、変更時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChNo1.CellValueChanged
        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            If mintEventCancelFlag = 1 Then Exit Sub
            If mintFirstFlag = 1 Then Exit Sub

            If chkMaskOR.Checked Or chkMaskAnd.Checked Then
                ''論理出力CHの場合のみ

                Call mChangeMaskBit(e.RowIndex)
                mgrdChRowIndex = e.RowIndex

                If grdChNo1(1, e.RowIndex).Value = "" Then
                    ''クリアした場合←次のセル（下のセル）には移動しない
                Else
                    ''Enter押下にて入力した場合、次のセルに自動で移動してしまうので、表示も合せるようにする
                    If e.RowIndex < grdChNo1.RowCount - 1 Then
                        Call mChangeMaskBit(e.RowIndex + 1)
                        mgrdChRowIndex = e.RowIndex + 1
                    End If
                End If

            Else
                ''CHデータの場合はクリアした時のみ
                If grdChNo1(1, e.RowIndex).Value = "" Then
                    Call mChangeMaskBit(e.RowIndex)
                    mgrdChRowIndex = e.RowIndex
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
                                                        Handles grdChNo1.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name = "txtChNo_frmChOutputDoDetail" Then

                If IsNumeric(dgv.Rows(e.RowIndex).Cells(1).Value) Then

                    mintEventCancelFlag = 1
                    dgv.Rows(e.RowIndex).Cells(1).Value() = Integer.Parse(dgv.Rows(e.RowIndex).Cells(1).Value).ToString("0000")
                    mintEventCancelFlag = 0

                    ''CHデータの場合のみ
                    If chkMaskOR.Checked Or chkMaskAnd.Checked Then
                    Else

                        Call mChangeMaskBit(mgrdChRowIndex)

                        mgrdChRowIndex = e.RowIndex

                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.をのチェックを行う
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) _
                                                        Handles grdChNo1.CellValidating

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strValue As String
            Dim strMsg As String = ""

            ''グリッドの保留中の変更を全て適用させる
            'dgv.EndEdit()

            strValue = grdChNo1(1, e.RowIndex).EditedFormattedValue
            If IsNumeric(strValue) Then

                Select Case e.ColumnIndex

                    Case 1                                  ''CH No.
                        If Integer.Parse(strValue) > 65535 Then
                            MsgBox("Please set CH No. '1'-'65535'.", MsgBoxStyle.Exclamation, "Channel")
                            e.Cancel = True : Exit Sub
                        End If

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdChNo1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) _
                                                                                            Handles grdChNo1.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdChNo1_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdChNo1_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdChNo1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdChNo1.KeyPress

        Try

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub

            If grdChNo1.CurrentCell.ReadOnly Then Exit Sub

            Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)

            e.Handled = gCheckTextInput(5, dgv, e.KeyChar, True)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPin.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドエラー
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdChNo1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdChNo1.DataError

        Try

            ''エラーが発生した時に、元の値に戻るようにする
            e.Cancel = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報を取得する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 取得対象のチャンネル番号
    '           : ARG2 - (IO) システム番号
    '           : ARG3 - (IO) チャンネルタイプ
    '           : ARG4 - (IO) ステータス種別コード
    '           : ARG5 - (IO) データタイプ
    '           : ARG6 - (IO) コンポジットインデックス
    '           : ARG7 - (IO) 計測点番号
    '           : ARG8 - (IO) ステータス（手入力値）
    '           : ARG9 - (IO) アナログ.useの値
    '           : ARG10- (IO) バルブ.useの値
    '
    '   Ver1.11.8.3 2016.11.08 引数に機器ｺｰﾄﾞ追加
    '--------------------------------------------------------------------
    Private Sub mGetChInfo(ByVal intChNo As Integer, _
                           ByRef intSystemNo As Integer, _
                           ByRef intChType As Integer, _
                           ByRef intStatus As Integer, _
                           ByRef intDataType As Integer, _
                           ByRef intCompIdx As Integer, _
                           ByRef intPinNo As Integer, _
                           ByRef strStatus As String, _
                           ByRef blnAnalogUse() As Boolean, _
                           ByRef blnValveUse() As Boolean, _
                           ByRef intKikiCode As Integer)

        Try

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If intChNo = .udtChCommon.shtChno Then

                        intSystemNo = .udtChCommon.shtSysno
                        intChType = .udtChCommon.shtChType
                        'intStatus = .MotorStatus   ''Status O
                        intStatus = .udtChCommon.shtStatus
                        strStatus = gGetString(.udtChCommon.strStatus)
                        intDataType = .udtChCommon.shtData
                        intKikiCode = 0     '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加

                        If intChType = gCstCodeChTypeAnalog Then
                            blnAnalogUse(0) = IIf(.AnalogHiHiUse = 1, True, False)
                            blnAnalogUse(1) = IIf(.AnalogHiUse = 1, True, False)
                            blnAnalogUse(2) = IIf(.AnalogLoUse = 1, True, False)
                            blnAnalogUse(3) = IIf(.AnalogLoLoUse = 1, True, False)
                            blnAnalogUse(4) = IIf(.AnalogSensorFailUse = 1, True, False)    ''Under
                            blnAnalogUse(5) = IIf(.AnalogSensorFailUse = 1, True, False)    ''Over

                        ElseIf intChType = gCstCodeChTypeValve Then
                            If intDataType = gCstCodeChDataTypeValveDI_DO Then

                                intPinNo = .udtChCommon.shtPinNo
                                intCompIdx = .CompositeTableIndex   ''コンポジット設定テーブルインデックス

                            ElseIf intDataType = gCstCodeChDataTypeValveAI_DO1 Or intDataType = gCstCodeChDataTypeValveAI_DO2 Then
                                blnValveUse(0) = IIf(.ValveAiDoHiHiUse = 1, True, False)
                                blnValveUse(1) = IIf(.ValveAiDoHiUse = 1, True, False)
                                blnValveUse(2) = IIf(.ValveAiDoLoUse = 1, True, False)
                                blnValveUse(3) = IIf(.ValveAiDoLoLoUse = 1, True, False)
                                blnValveUse(4) = IIf(.ValveAiDoSensorFailUse = 1, True, False)  ''Under
                                blnValveUse(5) = IIf(.ValveAiDoSensorFailUse = 1, True, False)  ''Over
                                blnValveUse(6) = IIf(.ValveAiDoAlarmUse = 1, True, False)       ''Feedback Alarm

                            ElseIf intDataType = gCstCodeChDataTypeValveAI_AO1 Or intDataType = gCstCodeChDataTypeValveAI_AO2 Or _
                                   intDataType = gCstCodeChDataTypeValveAO_4_20 Then
                                blnValveUse(0) = IIf(.ValveAiAoHiHiUse = 1, True, False)
                                blnValveUse(1) = IIf(.ValveAiAoHiUse = 1, True, False)
                                blnValveUse(2) = IIf(.ValveAiAoLoUse = 1, True, False)
                                blnValveUse(3) = IIf(.ValveAiAoLoLoUse = 1, True, False)
                                blnValveUse(4) = IIf(.ValveAiAoSensorFailUse = 1, True, False)  ''Under
                                blnValveUse(5) = IIf(.ValveAiAoSensorFailUse = 1, True, False)  ''Over
                                blnValveUse(6) = IIf(.ValveAiAoAlarmUse = 1, True, False)       ''Feedback Alarm
                            End If

                        ElseIf intChType = gCstCodeChTypeComposite Then

                            intPinNo = .udtChCommon.shtPinNo
                            intCompIdx = .CompositeTableIndex   ''コンポジット設定テーブルインデックス

                        ElseIf intChType = gCstCodeChTypeDigital Then       '' Ver1.11.8.3 2016.11.08 CH特殊設定追加
                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then        '' ｼｽﾃﾑCH 
                                intKikiCode = .SystemInfoKikiCode01     '' 機器ｺｰﾄﾞ 
                            End If

                        End If

                        Exit For
                    End If
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 該当チャンネルのマスクデータ（ビットパターン）を表示する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 対象行
    ' 概要　　　: 現状のマスクデータを作業領域に格納し、該当するマスクデータを表示する
    '--------------------------------------------------------------------
    Private Sub mChangeMaskBit(ByVal RowIndex As Integer)

        Try
            Dim intChNo As Integer = 0, intSystemNo As Integer = 0
            Dim intChType As Integer = 0, intStatus As Integer = 0, intDataType As Integer = 0
            Dim intCompIdx As Integer = 0, intPinNo As Integer = 0
            Dim strStatus As String = ""
            Dim blnAnalogUse(5) As Boolean, blnValveUse(6) As Boolean
            Dim strwk() As String = Nothing, strValue As String = ""
            Dim strbp() As String = Nothing
            Dim intType As Integer = 0, intMask As Integer = 0
            Dim intFlag As Integer = 0

            Dim intKikicode As Integer = 0      '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加

            If chkMaskOR.Checked Then
                intType = 1
            ElseIf chkMaskAnd.Checked Then
                intType = 2
            Else
                intType = 0
            End If

            ''①現状のマスクデータを作業領域に格納する------------------------------------------
            If mintFirstFlag = 0 Then
                intChNo = Val(grdChNo1(1, mgrdChRowIndex).Value)    ''Val(grdChNo1(1, mgrdChRowIndex).Value)

                ''現状のチャンネル情報 GET
                If intChNo > 0 Then
                    '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加
                    Call mGetChInfo(intChNo, intSystemNo, intChType, intStatus, _
                                intDataType, intCompIdx, intPinNo, strStatus, _
                                blnAnalogUse, blnValveUse, intKikicode)

                    ''現状のチャンネルのマスクデータ（ビットパターン）GET＆SET
                    intMask = 0
                    If cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusAlarm Then         ''<ALARM>

                        Select Case intChType

                            Case gCstCodeChTypeAnalog

                                For i As Integer = LBound(mcstBitAnalog) To UBound(mcstBitAnalog)

                                    If mcstBitAnalog(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                        ''対象CHの.useが1のアラームのビットを全てONにする
                                        For j As Integer = LBound(blnAnalogUse) To UBound(blnAnalogUse)
                                            intMask = gBitSet(intMask, mcstBitAnalog(j + 1), blnAnalogUse(j))
                                        Next
                                        Exit For
                                    Else
                                        intMask = gBitSet(intMask, mcstBitAnalog(i), grdOutputMovement(0, i).Value)
                                    End If

                                Next

                            Case gCstCodeChTypeDigital

                                For i As Integer = LBound(mcstBitDigital) To UBound(mcstBitDigital)
                                    intMask = gBitSet(intMask, mcstBitDigital(i), grdOutputMovement(0, i).Value)
                                Next

                            Case gCstCodeChTypeMotor

                                For i As Integer = LBound(mcstBitMotor) To UBound(mcstBitMotor)
                                    intMask = gBitSet(intMask, mcstBitMotor(i), grdOutputMovement(0, i).Value)
                                Next

                            Case gCstCodeChTypeValve

                                Select Case intDataType

                                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt
                                        For i As Integer = LBound(mcstBitValveDiDo) To UBound(mcstBitValveDiDo)
                                            intMask = gBitSet(intMask, mcstBitValveDiDo(i), grdOutputMovement(0, i).Value)
                                        Next

                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                                        For i As Integer = LBound(mcstBitValveAiDo) To UBound(mcstBitValveAiDo)

                                            If mcstBitValveAiDo(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                                ''対象CHの.useが1のアラームのビットを全てONにする
                                                For j As Integer = LBound(blnValveUse) To UBound(blnValveUse)
                                                    intMask = gBitSet(intMask, mcstBitValveAiDo(j + 1), blnValveUse(j))
                                                Next
                                                Exit For
                                            Else
                                                intMask = gBitSet(intMask, mcstBitValveAiDo(i), grdOutputMovement(0, i).Value)
                                            End If

                                        Next

                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                                        For i As Integer = LBound(mcstBitValveAiAo) To UBound(mcstBitValveAiAo)

                                            If mcstBitValveAiAo(i) = -1 And grdOutputMovement(0, i).Value = True Then
                                                ''対象CHの.useが1のアラームのビットを全てONにする
                                                For j As Integer = LBound(blnValveUse) To UBound(blnValveUse)
                                                    intMask = gBitSet(intMask, mcstBitValveAiAo(j + 1), blnValveUse(j))
                                                Next
                                                Exit For
                                            Else
                                                intMask = gBitSet(intMask, mcstBitValveAiAo(i), grdOutputMovement(0, i).Value)
                                            End If

                                        Next

                                End Select

                            Case gCstCodeChTypeComposite

                                For i As Integer = LBound(mcstBitComposite) To UBound(mcstBitComposite)
                                    intMask = gBitSet(intMask, mcstBitComposite(i), grdOutputMovement(0, i).Value)
                                Next

                            Case gCstCodeChTypePulse

                                For i As Integer = LBound(mcstBitPulse) To UBound(mcstBitPulse)
                                    intMask = gBitSet(intMask, mcstBitPulse(i), grdOutputMovement(0, i).Value)
                                Next

                        End Select

                    ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusOnOff Then     ''<ON/OFF>

                        If grdOutputMovement(0, 0).Value = True Then
                            intMask = gBitSet(intMask, 6, True)
                        End If

                    ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusMotor Then     ''<MOTOR>

                        If intChType = gCstCodeChTypeMotor Then
                            ''モーターCHの場合

                            'Ver2.0.0.2 モーター種別増加 R Device 追加
                            ''MO追加「0x02」固定   2013.08.07  K.Fujimoto
                            If intDataType = gCstCodeChDataTypeMotorDevice Or intDataType = gCstCodeChDataTypeMotorDeviceJacom Or intDataType = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                               intDataType = gCstCodeChDataTypeMotorRDevice Then

                                If grdOutputMovement(0, 0).Value = True Then
                                    intMask = gBitSet(intMask, 1, True)
                                End If

                            Else

                                Erase strwk
                                Erase strbp

                                ''ステータス情報を獲得する
                                Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                cmbStatus.SelectedValue = intStatus

                                If cmbStatus.SelectedIndex >= 0 Then

                                    If intStatus = gCstCodeChManualInputStatus.ToString Then
                                        ''手入力

                                    Else
                                        ''「_」区切りの文字列取得
                                        If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                           intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")

                                        Else
                                            'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                            If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                                intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                                '正常Rは正常ステータス扱い
                                                strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            Else
                                                strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                                strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            End If
                                        End If
                                        ''取得したデータビット位置に基づいてマスクビットの作成
                                        For i = 0 To UBound(strwk)
                                            intMask = gBitSet(intMask, CCInt(strbp(i)), grdOutputMovement(0, i).Value)
                                        Next

                                    End If
                                End If

                            End If

                        ElseIf intChType = gCstCodeChTypeComposite Then
                            ''デジタルコンポジットCHの場合
                            For i = 0 To grdOutputMovement.RowCount - 1
                                intMask = gBitSet(intMask, i, grdOutputMovement(0, i).Value)
                            Next

                        ElseIf intChType = gCstCodeChTypeValve Then     '' ver.1.4.5 2012.07.03 バルブCH追加
                            ''バルブCH(DI/DO)の場合
                            If intDataType = gCstCodeChDataTypeValveDI_DO Then
                                For i = 0 To grdOutputMovement.RowCount - 1
                                    intMask = gBitSet(intMask, i, grdOutputMovement(0, i).Value)
                                Next
                            End If
                        End If

                    End If

                End If

                ''論理出力CH OR/AND
                With mOrAndDetail(mgrdChRowIndex)
                    .Chid = intChNo
                    .Sysno = intSystemNo
                    .Status = cmbOutputMovement.SelectedValue   ''OutputMovement
                    .Mask = intMask                             ''マスクデータ
                End With

                ''CHデータ
                If mgrdChRowIndex = 0 Then
                    With mDoDetail
                        .Status = cmbOutputMovement.SelectedValue   ''OutputMovement
                        .Mask = intMask                             ''マスクデータ
                    End With
                End If

            End If

            ''②該当チャンネルのOutputMovementをセットする--------------------------------------
            mintEventCancelFlag = 1

            ''該当チャンネル GET
            intChNo = Val(grdChNo1(1, RowIndex).Value)

            If intChNo > 0 Then
                If intType = 0 Then
                    ''CHデータ
                    With mDoDetail
                        cmbOutputMovement.SelectedValue = Val(.Status)  ''OutputMovement
                    End With
                Else
                    ''論理出力CH OR/AND
                    With mOrAndDetail(RowIndex)
                        cmbOutputMovement.SelectedValue = Val(.Status)  ''OutputMovement
                    End With
                End If
            End If

            mintEventCancelFlag = 0

            ''③該当チャンネルのステータスを表示する--------------------------------------------

            ''該当チャンネル GET
            intChNo = Val(grdChNo1(1, RowIndex).Value)

            ''チャンネル情報 GET
            intSystemNo = 0 : intChType = 0 : intStatus = 0 : intDataType = 0 : intCompIdx = 0
            intPinNo = 0 : strStatus = ""
            If intChNo > 0 Then
                '' Ver1.11.8.3 2016.11.08 機器ｺｰﾄﾞ追加
                Call mGetChInfo(intChNo, intSystemNo, intChType, intStatus, _
                                intDataType, intCompIdx, intPinNo, strStatus, _
                                blnAnalogUse, blnValveUse, intKikicode)
            End If
            With grdOutputMovement

                ''クリア
                For i As Integer = 0 To 9
                    .Rows(i).Cells(0).Value = False
                    .Rows(i).Cells(1).Value = ""
                Next

                If cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusAlarm Then         ''<ALARM>

                    Select Case intChType

                        Case gCstCodeChTypeAnalog

                            For i As Integer = LBound(mcstStsAnalog) To UBound(mcstStsAnalog)
                                .Rows(i).Cells(1).Value = mcstStsAnalog(i)
                            Next

                        Case gCstCodeChTypeDigital

                            For i As Integer = LBound(mcstStsDigital) To UBound(mcstStsDigital)
                                .Rows(i).Cells(1).Value = mcstStsDigital(i)
                            Next

                        Case gCstCodeChTypeMotor

                            For i As Integer = LBound(mcstStsMotor) To UBound(mcstStsMotor)
                                .Rows(i).Cells(1).Value = mcstStsMotor(i)
                            Next

                        Case gCstCodeChTypeValve

                            Select Case intDataType

                                Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt
                                    For i As Integer = LBound(mcstStsValveDiDo) To UBound(mcstStsValveDiDo)
                                        .Rows(i).Cells(1).Value = mcstStsValveDiDo(i)
                                    Next

                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                                    For i As Integer = LBound(mcstStsValveAiDo) To UBound(mcstStsValveAiDo)
                                        .Rows(i).Cells(1).Value = mcstStsValveAiDo(i)
                                    Next

                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                                    For i As Integer = LBound(mcstStsValveAiAo) To UBound(mcstStsValveAiAo)
                                        .Rows(i).Cells(1).Value = mcstStsValveAiAo(i)
                                    Next

                            End Select

                        Case gCstCodeChTypeComposite

                            For i As Integer = LBound(mcstStsComposite) To UBound(mcstStsComposite)
                                .Rows(i).Cells(1).Value = mcstStsComposite(i)
                            Next

                        Case gCstCodeChTypePulse

                            For i As Integer = LBound(mcstStsPulse) To UBound(mcstStsPulse)
                                .Rows(i).Cells(1).Value = mcstStsPulse(i)
                            Next

                    End Select

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusOnOff Then     ''<ON/OFF>

                    ''デジタルCHの場合、設定済みのステータス情報を表示する
                    If intChType = gCstCodeChTypeDigital Then

                        If intStatus = gCstCodeChManualInputStatus.ToString Then
                            ''手入力値
                            If strStatus <> "" Then
                                ''2つに分解する
                                'Ver2.0.8.3 (保安庁)ｽﾃｰﾀｽ日本語対応
                                'If strStatus.Length > 8 Then
                                If LenB(strStatus) > 8 Then
                                    '.Rows(0).Cells(1).Value = strStatus.Substring(0, 8)
                                    '.Rows(1).Cells(1).Value = strStatus.Substring(8)
                                    .Rows(0).Cells(1).Value = MidB(strStatus, 0, 8)
                                    .Rows(1).Cells(1).Value = MidB(strStatus, 8)
                                Else
                                    .Rows(0).Cells(1).Value = strStatus
                                End If
                            End If
                        Else
                            If intStatus = 0 Then
                                ''デジタルのステータス種別コード = 0  <-- ステータスなし
                            Else
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                cmbStatus.SelectedValue = intStatus
                                strValue = cmbStatus.Text
                                strwk = strValue.Split("/")

                                For i As Integer = 0 To UBound(strwk)
                                    .Rows(i).Cells(1).Value = strwk(i)
                                Next
                            End If
                        End If

                    End If

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusMotor Then     ''<MOTOR>

                    If intChType = gCstCodeChTypeMotor Then
                        ''モーターCHの場合、設定済みのステータス情報を表示する

                        ''MO追加「RUN」固定   2013.08.07  K.Fujimoto
                        'Ver2.0.0.2 モーター種別増加
                        If intDataType = gCstCodeChDataTypeMotorDevice Or intDataType = gCstCodeChDataTypeMotorDeviceJacom Or intDataType = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                           intDataType = gCstCodeChDataTypeMotorRDevice Then

                            Erase strwk
                            ReDim strwk(0)
                            strwk(0) = "RUN"

                            For i As Integer = 0 To UBound(strwk)
                                .Rows(i).Cells(1).Value = strwk(i)
                            Next

                        Else

                            ''ステータス情報を獲得する
                            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor")
                            'Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "BitMotor")     ''4/26 out

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                            cmbStatus.SelectedValue = intStatus

                            If cmbStatus.SelectedIndex >= 0 Then

                                If intStatus = gCstCodeChManualInputStatus.ToString Then
                                    ''手入力
                                    .Rows(0).Cells(1).Value = strStatus
                                Else
                                    ''「_」区切りの文字列取得
                                    Erase strwk
                                    If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                       intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                    Else
                                        'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                        If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                            intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                            '正常Rは正常ステータス扱い
                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                        End If
                                    End If
                                    For i As Integer = 0 To UBound(strwk)
                                        .Rows(i).Cells(1).Value = strwk(i)
                                    Next

                                End If

                            End If

                        End If

                    ElseIf intChType = gCstCodeChTypeComposite Then
                        ''デジタルコンポジットCHの場合
                        If intPinNo > 0 Then
                            For i As Integer = 0 To intPinNo - 1
                                .Rows(i).Cells(1).Value = "BIT-" & CStr(i)
                            Next
                        End If

                    ElseIf intChType = gCstCodeChTypeValve Then     '' ver.1.4.5 2012.07.03 バルブCH追加
                        ''バルブCH(DI/DO)の場合
                        If intDataType = gCstCodeChDataTypeValveDI_DO Then
                            If intPinNo > 0 Then
                                For i As Integer = 0 To intPinNo - 1
                                    .Rows(i).Cells(1).Value = "BIT-" & CStr(i)
                                Next
                            End If
                        End If
                    End If

                End If

                intFlag = 0
                For i As Integer = 0 To 9

                    If .Rows(i).Cells(1).Value <> "" Then

                        .Rows(i).Cells(0).ReadOnly = False

                        If i Mod 2 <> 0 Then
                            .Rows(i).Cells(0).Style.BackColor = gColorGridRowBack
                        Else
                            .Rows(i).Cells(0).Style.BackColor = gColorGridRowBackBase
                        End If

                        intFlag = 1     ''マスクデータ有り
                    Else
                        .Rows(i).Cells(0).ReadOnly = True
                        .Rows(i).Cells(0).Style.BackColor = gColorGridRowBackReadOnly
                    End If

                Next

            End With

            ''④該当チャンネルのビットパターンを表示する--------------------------------------------
            If intFlag = 1 Then

                If intType = 0 Then
                    intMask = mDoDetail.Mask
                Else
                    intMask = mOrAndDetail(RowIndex).Mask
                End If

                If cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusAlarm Then         ''<ALARM>

                    Select Case intChType

                        Case gCstCodeChTypeAnalog

                            For i As Integer = LBound(mcstBitAnalog) To UBound(mcstBitAnalog)

                                If mcstBitAnalog(i) = -1 Then
                                    ''ALL ALARM
                                Else
                                    grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitAnalog(i))
                                End If

                            Next

                            ''チェックされたビットと.useの設定が同じ場合、ALL ALAEMと判断する
                            If grdOutputMovement(0, 1).Value = blnAnalogUse(0) And _
                               grdOutputMovement(0, 2).Value = blnAnalogUse(1) And _
                               grdOutputMovement(0, 3).Value = blnAnalogUse(2) And _
                               grdOutputMovement(0, 4).Value = blnAnalogUse(3) And _
                               grdOutputMovement(0, 5).Value = blnAnalogUse(4) And _
                               grdOutputMovement(0, 6).Value = blnAnalogUse(5) Then

                                grdOutputMovement(0, 0).Value = True    ''ALL ALARM
                                grdOutputMovement(0, 1).Value = False
                                grdOutputMovement(0, 2).Value = False
                                grdOutputMovement(0, 3).Value = False
                                grdOutputMovement(0, 4).Value = False
                                grdOutputMovement(0, 5).Value = False
                                grdOutputMovement(0, 6).Value = False
                            End If

                        Case gCstCodeChTypeDigital

                            For i As Integer = LBound(mcstBitDigital) To UBound(mcstBitDigital)
                                grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitDigital(i))
                            Next

                        Case gCstCodeChTypeMotor

                            For i As Integer = LBound(mcstBitMotor) To UBound(mcstBitMotor)
                                grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitMotor(i))
                            Next

                        Case gCstCodeChTypeValve

                            Select Case intDataType

                                Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt

                                    For i As Integer = LBound(mcstBitValveDiDo) To UBound(mcstBitValveDiDo)
                                        grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitValveDiDo(i))
                                    Next

                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                    For i As Integer = LBound(mcstBitValveAiDo) To UBound(mcstBitValveAiDo)

                                        If mcstBitValveAiDo(i) = -1 Then
                                            ''ALL ALARM
                                        Else
                                            grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitValveAiDo(i))
                                        End If

                                    Next

                                    ''チェックされたビットと.useの設定が同じ場合、ALL ALAEMと判断する
                                    If grdOutputMovement(0, 1).Value = blnValveUse(0) And _
                                       grdOutputMovement(0, 2).Value = blnValveUse(1) And _
                                       grdOutputMovement(0, 3).Value = blnValveUse(2) And _
                                       grdOutputMovement(0, 4).Value = blnValveUse(3) And _
                                       grdOutputMovement(0, 5).Value = blnValveUse(4) And _
                                       grdOutputMovement(0, 6).Value = blnValveUse(5) And _
                                       grdOutputMovement(0, 7).Value = blnValveUse(6) Then

                                        grdOutputMovement(0, 0).Value = True    ''ALL ALARM
                                        grdOutputMovement(0, 1).Value = False
                                        grdOutputMovement(0, 2).Value = False
                                        grdOutputMovement(0, 3).Value = False
                                        grdOutputMovement(0, 4).Value = False
                                        grdOutputMovement(0, 5).Value = False
                                        grdOutputMovement(0, 6).Value = False
                                        grdOutputMovement(0, 7).Value = False
                                    End If

                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20

                                    For i As Integer = LBound(mcstBitValveAiAo) To UBound(mcstBitValveAiAo)

                                        If mcstBitValveAiAo(i) = -1 Then
                                            ''ALL ALARM
                                        Else
                                            grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitValveAiAo(i))
                                        End If

                                    Next

                                    ''チェックされたビットと.useの設定が同じ場合、ALL ALAEMと判断する
                                    If grdOutputMovement(0, 1).Value = blnValveUse(0) And _
                                       grdOutputMovement(0, 2).Value = blnValveUse(1) And _
                                       grdOutputMovement(0, 3).Value = blnValveUse(2) And _
                                       grdOutputMovement(0, 4).Value = blnValveUse(3) And _
                                       grdOutputMovement(0, 5).Value = blnValveUse(4) And _
                                       grdOutputMovement(0, 6).Value = blnValveUse(5) And _
                                       grdOutputMovement(0, 7).Value = blnValveUse(6) Then

                                        grdOutputMovement(0, 0).Value = True    ''ALL ALARM
                                        grdOutputMovement(0, 1).Value = False
                                        grdOutputMovement(0, 2).Value = False
                                        grdOutputMovement(0, 3).Value = False
                                        grdOutputMovement(0, 4).Value = False
                                        grdOutputMovement(0, 5).Value = False
                                        grdOutputMovement(0, 6).Value = False
                                        grdOutputMovement(0, 7).Value = False
                                    End If

                            End Select

                        Case gCstCodeChTypeComposite

                            For i As Integer = LBound(mcstBitComposite) To UBound(mcstBitComposite)
                                grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitComposite(i))
                            Next

                        Case gCstCodeChTypePulse

                            For i As Integer = LBound(mcstBitPulse) To UBound(mcstBitPulse)
                                grdOutputMovement(0, i).Value = gBitCheck(intMask, mcstBitPulse(i))
                            Next

                    End Select

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusOnOff Then     ''<ON/OFF>

                    If gBitCheck(intMask, 6) Then
                        grdOutputMovement(0, 0).Value = True
                    Else
                        grdOutputMovement(0, 1).Value = True
                    End If

                ElseIf cmbOutputMovement.SelectedValue = gCstCodeFuOutputStatusMotor Then     ''<MOTOR>

                    If intChType = gCstCodeChTypeMotor Then
                        ''モーターCHの場合

                        ''MO追加「0x02」固定   2013.08.07  K.Fujimoto
                        'Ver2.0.0.2 モーター種別増加
                        If intDataType = gCstCodeChDataTypeMotorDevice Or intDataType = gCstCodeChDataTypeMotorDeviceJacom Or intDataType = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                           intDataType = gCstCodeChDataTypeMotorRDevice Then

                            If gBitCheck(intMask, 1) Then
                                grdOutputMovement(0, 0).Value = True
                            End If

                        Else

                            Erase strwk
                            Erase strbp

                            ''ステータス情報を獲得する
                            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                            cmbStatus.SelectedValue = intStatus

                            If cmbStatus.SelectedIndex >= 0 Then

                                If intStatus = gCstCodeChManualInputStatus.ToString Then
                                    ''手入力

                                Else
                                    ''「_」区切りの文字列取得
                                    If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                       intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")

                                    Else
                                        'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                        If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                            intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                            '正常Rは正常ステータス扱い
                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                        End If
                                    End If

                                    ''取得したデータビット位置に基づいてビットを表示
                                    For i = 0 To UBound(strwk)
                                        grdOutputMovement(0, i).Value = gBitCheck(intMask, CCInt(strbp(i)))
                                    Next

                                End If
                            End If

                        End If



                    ElseIf intChType = gCstCodeChTypeComposite Then
                        ''デジタルコンポジットCHの場合
                        For i = 0 To 7
                            grdOutputMovement(0, i).Value = gBitCheck(intMask, i)
                        Next

                    ElseIf intChType = gCstCodeChTypeValve Then     '' ver.1.4.5 2012.07.03 バルブCH追加
                        ''バルブCH(DI/DO)の場合
                        If intDataType = gCstCodeChDataTypeValveDI_DO Then
                            For i = 0 To 7
                                grdOutputMovement(0, i).Value = gBitCheck(intMask, i)
                            Next
                        End If
                    End If

                End If

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
            Dim i As Integer, intFlg As Integer = 0

            mintEventCancelFlag = 1

            ''Invalidでない?
            If cmbChOutType.SelectedValue <> 0 Then

                ''論理出力CH
                If (chkMaskOR.Checked Or chkMaskAnd.Checked) Then

                    For i = 0 To grdChNo1.Rows.Count - 1

                        If Not gChkInputNum(grdChNo1(1, i), 1, 65535, "CH No.", i + 1, True, True) Then Return False

                        If IsNumeric(grdChNo1(1, i).Value) And grdChNo1(1, i).Value <> "0000" And grdChNo1(1, i).Value <> "00000" Then
                            intFlg = 1
                        End If

                    Next

                    ''1個以上のCHが設定されていること
                    If intFlg = 0 Then
                        MsgBox("Please set CH No.", MsgBoxStyle.Exclamation, "Input error")
                        Return False
                    End If

                Else
                    ''先頭にCHが設定されていること
                    If Not gChkInputNum(grdChNo1(1, 0), 1, 65535, "CH No.", 1, True, True) Then Return False

                    If Not IsNumeric(grdChNo1(1, 0).Value) Or grdChNo1(1, 0).Value = "0000" Or grdChNo1(1, 0).Value = "00000" Then
                        MsgBox("Please set CH No.", MsgBoxStyle.Exclamation, "Input error")
                        Return False
                    End If

                End If

                'Ver2.0.2.1
                'RUN(-/LT)時
                If cmbChOutType.SelectedValue = 6 Then
                    'MovementでALARMを選択していればエラー
                    If cmbOutputMovement.SelectedIndex = 0 Then
                        MsgBox("Movement select [ALARM] is Error.", MsgBoxStyle.Exclamation, "Input error")
                        Return False
                    End If
                End If
            End If

            mintEventCancelFlag = 0

            ''共通FUアドレス入力チェック
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            With grdOutputMovement

                Dim Column10 As New DataGridViewCheckBoxColumn : Column10.Name = "ChkOutput"
                Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtOutput" : Column11.ReadOnly = True

                ''列
                .Columns.Clear()
                .Columns.Add(Column10)
                .Columns.Add(Column11)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .ColumnHeadersVisible = False
                .Columns(0).Width = 40
                .Columns(1).Width = 100

                ''行
                .RowCount = 11
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                    .Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdOutputMovement)

            End With

            With grdChNo1

                Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtNo"
                Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChNo_frmChOutputDoDetail"
                Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)

                Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column1.ReadOnly = True

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "" : .Columns(0).Width = 30
                .Columns(1).HeaderText = "CH No." : .Columns(1).Width = 80
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 25
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersVisible = False
                For i = 1 To 24
                    .Rows(i - 1).Cells(0).Value = i.ToString("00")
                    .Rows(i - 1).Cells(0).Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.DarkGray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdChNo1)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 設定条件により複数CHの設定を可能にする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mSetColorToCH()

        Try

            Dim i As Integer
            Dim WkColor As Color

            If (chkMaskOR.Checked Or chkMaskAnd.Checked) And (cmbChOutType.SelectedValue <> 0) Then ''Invalid 以外

                For i = 0 To 23

                    If i Mod 2 <> 0 Then
                        WkColor = gColorGridRowBack
                    Else
                        WkColor = gColorGridRowBackBase
                    End If

                    grdChNo1.Rows(i).DefaultCellStyle.BackColor = WkColor
                    grdChNo1.Rows(i).ReadOnly = False

                Next

            ElseIf (chkMaskOR.Checked = False And chkMaskAnd.Checked = False) And (cmbChOutType.SelectedValue <> 0) Then ''Invalid 以外

                For i = 0 To 23

                    If i Mod 2 <> 0 Then
                        WkColor = gColorGridRowBack
                    Else
                        WkColor = gColorGridRowBackBase
                    End If

                    If i = 0 Then   ''先頭のみ有効
                        grdChNo1.Rows(i).DefaultCellStyle.BackColor = WkColor
                        grdChNo1.Rows(i).ReadOnly = False
                    Else
                        grdChNo1.Rows(i).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                        grdChNo1.Rows(i).ReadOnly = True

                        mintEventCancelFlag = 1
                        grdChNo1.Rows(i).Cells(1).Value = ""
                        mintEventCancelFlag = 0
                    End If

                Next

            Else
                ''Invalid
                For i = 0 To 23

                    grdChNo1.Rows(i).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                    grdChNo1.Rows(i).ReadOnly = True
                    grdChNo1.Rows(i).Cells(1).Value = ""

                Next

                For i = 0 To 9
                    grdOutputMovement.Rows(i).Cells(0).Style.BackColor = gColorGridRowBackReadOnly
                    grdOutputMovement.Rows(i).Cells(0).ReadOnly = True
                    grdOutputMovement.Rows(i).Cells(0).Value = False
                    grdOutputMovement.Rows(i).Cells(1).Value = ""
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    Private Sub txtFuNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFuNo.TextChanged

    End Sub
End Class

