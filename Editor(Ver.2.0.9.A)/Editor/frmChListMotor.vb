﻿Public Class frmChListMotor

#Region "変数定義"

    ''OKフラグ
    Private mintOkFlag As Integer

    ''Next CH ボタン　クリックフラグ
    Private mintNextChFlag As Integer = 0

    ''Before CH ボタン　クリックフラグ
    Private mintBeforeChFlag As Integer = 0

    ''イベント重複抑制用
    Private mblnFlg As Boolean

    ''イベントキャンセルフラグ
    Private mintStatusFlag As Integer

    ''初期フラグ
    Private mintFirst As Integer

    ''Delay Timer 設定単位区分
    Private mintDelayTimeKubun As Integer   ''0:秒　1:分

    ''モーターのステータス情報格納
    Private mMotorStatus1() As String
    Private mMotorStatus2() As String
    Private mMotorBitPos1() As String
    Private mMotorBitPos2() As String

    Public FeedBackKazu As Integer

    ''モーターチャンネル情報格納
    Private mMotorDetail As frmChListChannelList.mMotorInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) モーターチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hMotorDetail As frmChListChannelList.mMotorInfo, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            ReDim mMotorDetail.StatusDO(7)

            mMotorDetail.RowNo = hMotorDetail.RowNo
            mMotorDetail.RowNoFirst = hMotorDetail.RowNoFirst
            mMotorDetail.RowNoEnd = hMotorDetail.RowNoEnd
            mMotorDetail.SysNo = hMotorDetail.SysNo
            mMotorDetail.ChNo = hMotorDetail.ChNo
            mMotorDetail.TagNo = hMotorDetail.TagNo   '' 2015.10.27 Ver1.7.5 ﾀｸﾞ追加
            mMotorDetail.ItemName = hMotorDetail.ItemName
            mMotorDetail.AlmLevel = hMotorDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mMotorDetail.ExtGH = hMotorDetail.ExtGH
            mMotorDetail.GRep1H = hMotorDetail.GRep1H
            mMotorDetail.GRep2H = hMotorDetail.GRep2H
            mMotorDetail.FlagDmy = hMotorDetail.FlagDmy
            mMotorDetail.FlagSC = hMotorDetail.FlagSC
            mMotorDetail.FlagSIO = hMotorDetail.FlagSIO
            mMotorDetail.FlagGWS = hMotorDetail.FlagGWS
            mMotorDetail.FlagWK = hMotorDetail.FlagWK
            mMotorDetail.FlagRL = hMotorDetail.FlagRL
            mMotorDetail.FlagAC = hMotorDetail.FlagAC
            mMotorDetail.FlagEP = hMotorDetail.FlagEP
            mMotorDetail.FlagPLC = hMotorDetail.FlagPLC     '' 2014.11.18
            mMotorDetail.FlagSP = hMotorDetail.FlagSP
            mMotorDetail.FlagMin = hMotorDetail.FlagMin
            mMotorDetail.FlagMotorCol = hMotorDetail.FlagMotorCol  '' ver2.0.8.C 2018.11.14
            mMotorDetail.DIStart = hMotorDetail.DIStart
            mMotorDetail.DIPortStart = hMotorDetail.DIPortStart
            mMotorDetail.DIPinStart = hMotorDetail.DIPinStart
            mMotorDetail.DOStart = hMotorDetail.DOStart
            mMotorDetail.DOPortStart = hMotorDetail.DOPortStart
            mMotorDetail.DOPinStart = hMotorDetail.DOPinStart

            mMotorDetail.DataType = hMotorDetail.DataType
            mMotorDetail.PortNo = hMotorDetail.PortNo
            'mMotorDetail.StatusIn = hMotorDetail.StatusIn
            mMotorDetail.StatusOut = hMotorDetail.StatusOut
            mMotorDetail.FlagStatusAlarm = hMotorDetail.FlagStatusAlarm
            mMotorDetail.FilterCoef = hMotorDetail.FilterCoef
            mMotorDetail.AlarmTimeup = hMotorDetail.AlarmTimeup
            mMotorDetail.ControlType = hMotorDetail.ControlType
            mMotorDetail.PulseWidth = hMotorDetail.PulseWidth
            mMotorDetail.ShareType = hMotorDetail.ShareType
            mMotorDetail.ShareChNo = hMotorDetail.ShareChNo
            mMotorDetail.Remarks = hMotorDetail.Remarks
            mMotorDetail.ExtgDoAlarm = hMotorDetail.ExtgDoAlarm
            mMotorDetail.DelayDoAlarm = hMotorDetail.DelayDoAlarm
            mMotorDetail.GRep1DoAlarm = hMotorDetail.GRep1DoAlarm
            mMotorDetail.GRep2DoAlarm = hMotorDetail.GRep2DoAlarm
            mMotorDetail.StatusDoAlarm = hMotorDetail.StatusDoAlarm

            For i As Integer = 0 To 7
                mMotorDetail.StatusDO(i) = hMotorDetail.StatusDO(i)
            Next

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mMotorDetail.AlmMimic = hMotorDetail.AlmMimic


            '▼▼▼ 20110614 仮設定機能対応（モーター） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mMotorDetail.DummyExtG = hMotorDetail.DummyExtG
            mMotorDetail.DummyGroupRepose1 = hMotorDetail.DummyGroupRepose1
            mMotorDetail.DummyGroupRepose2 = hMotorDetail.DummyGroupRepose2
            mMotorDetail.DummyFuAddress = hMotorDetail.DummyFuAddress

            mMotorDetail.DummyOutFuAddress = hMotorDetail.DummyOutFuAddress
            mMotorDetail.DummyOutBitCount = hMotorDetail.DummyOutBitCount
            mMotorDetail.DummyOutStatusType = hMotorDetail.DummyOutStatusType

            mMotorDetail.DummyOutStatus1 = hMotorDetail.DummyOutStatus1
            mMotorDetail.DummyOutStatus2 = hMotorDetail.DummyOutStatus2
            mMotorDetail.DummyOutStatus3 = hMotorDetail.DummyOutStatus3
            mMotorDetail.DummyOutStatus4 = hMotorDetail.DummyOutStatus4
            mMotorDetail.DummyOutStatus5 = hMotorDetail.DummyOutStatus5

            mMotorDetail.DummyFaExtGr = hMotorDetail.DummyFaExtGr
            mMotorDetail.DummyFaDelay = hMotorDetail.DummyFaDelay
            mMotorDetail.DummyFaGrep1 = hMotorDetail.DummyFaGrep1
            mMotorDetail.DummyFaGrep2 = hMotorDetail.DummyFaGrep2
            mMotorDetail.DummyFaStaNm = hMotorDetail.DummyFaStaNm
            mMotorDetail.DummyFaTimeV = hMotorDetail.DummyFaTimeV
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''==================================================
            Call gShowFormModelessForCloseWait2(Me, frmOwner)
            ''==================================================

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hMotorDetail, mMotorDetail) = False Then

                    hMotorDetail.SysNo = mMotorDetail.SysNo
                    hMotorDetail.ChNo = mMotorDetail.ChNo
                    hMotorDetail.TagNo = mMotorDetail.TagNo   '' 2015.10.27 Ver1.7.5 ﾀｸﾞ追加
                    hMotorDetail.ItemName = mMotorDetail.ItemName
                    hMotorDetail.AlmLevel = mMotorDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hMotorDetail.ExtGH = mMotorDetail.ExtGH
                    hMotorDetail.GRep1H = mMotorDetail.GRep1H
                    hMotorDetail.GRep2H = mMotorDetail.GRep2H
                    hMotorDetail.FlagDmy = mMotorDetail.FlagDmy
                    hMotorDetail.FlagSC = mMotorDetail.FlagSC
                    hMotorDetail.FlagSIO = mMotorDetail.FlagSIO
                    hMotorDetail.FlagGWS = mMotorDetail.FlagGWS
                    hMotorDetail.FlagWK = mMotorDetail.FlagWK
                    hMotorDetail.FlagRL = mMotorDetail.FlagRL
                    hMotorDetail.FlagAC = mMotorDetail.FlagAC
                    hMotorDetail.FlagEP = mMotorDetail.FlagEP
                    hMotorDetail.FlagPLC = mMotorDetail.FlagPLC     '' 2014.11.18
                    hMotorDetail.FlagSP = mMotorDetail.FlagSP
                    hMotorDetail.FlagMin = mMotorDetail.FlagMin
                    hMotorDetail.FlagMotorCol = mMotorDetail.FlagMotorCol  ''ver2.0.8.C 2018.11.4

                    hMotorDetail.DIStart = mMotorDetail.DIStart
                    hMotorDetail.DIPortStart = mMotorDetail.DIPortStart
                    hMotorDetail.DIPinStart = mMotorDetail.DIPinStart
                    hMotorDetail.DOStart = mMotorDetail.DOStart
                    hMotorDetail.DOPortStart = mMotorDetail.DOPortStart
                    hMotorDetail.DOPinStart = mMotorDetail.DOPinStart

                    hMotorDetail.DataType = mMotorDetail.DataType
                    hMotorDetail.PortNo = mMotorDetail.PortNo
                    'hMotorDetail.StatusIn = mMotorDetail.StatusIn
                    hMotorDetail.StatusOut = mMotorDetail.StatusOut
                    hMotorDetail.FlagStatusAlarm = mMotorDetail.FlagStatusAlarm
                    hMotorDetail.FilterCoef = mMotorDetail.FilterCoef
                    hMotorDetail.AlarmTimeup = mMotorDetail.AlarmTimeup
                    hMotorDetail.ControlType = mMotorDetail.ControlType
                    hMotorDetail.PulseWidth = mMotorDetail.PulseWidth
                    hMotorDetail.ShareType = mMotorDetail.ShareType
                    hMotorDetail.ShareChNo = mMotorDetail.ShareChNo
                    hMotorDetail.Remarks = mMotorDetail.Remarks
                    hMotorDetail.ExtgDoAlarm = mMotorDetail.ExtgDoAlarm
                    hMotorDetail.DelayDoAlarm = mMotorDetail.DelayDoAlarm
                    hMotorDetail.GRep1DoAlarm = mMotorDetail.GRep1DoAlarm
                    hMotorDetail.GRep2DoAlarm = mMotorDetail.GRep2DoAlarm
                    hMotorDetail.StatusDoAlarm = mMotorDetail.StatusDoAlarm

                    For i As Integer = 0 To 7
                        hMotorDetail.StatusDO(i) = mMotorDetail.StatusDO(i)
                    Next

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hMotorDetail.AlmMimic = mMotorDetail.AlmMimic


                    '▼▼▼ 20110614 仮設定機能対応（モーター） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hMotorDetail.DummyExtG = mMotorDetail.DummyExtG
                    hMotorDetail.DummyGroupRepose1 = mMotorDetail.DummyGroupRepose1
                    hMotorDetail.DummyGroupRepose2 = mMotorDetail.DummyGroupRepose2
                    hMotorDetail.DummyFuAddress = mMotorDetail.DummyFuAddress

                    hMotorDetail.DummyOutFuAddress = mMotorDetail.DummyOutFuAddress
                    hMotorDetail.DummyOutBitCount = mMotorDetail.DummyOutBitCount
                    hMotorDetail.DummyOutStatusType = mMotorDetail.DummyOutStatusType

                    hMotorDetail.DummyOutStatus1 = mMotorDetail.DummyOutStatus1
                    hMotorDetail.DummyOutStatus2 = mMotorDetail.DummyOutStatus2
                    hMotorDetail.DummyOutStatus3 = mMotorDetail.DummyOutStatus3
                    hMotorDetail.DummyOutStatus4 = mMotorDetail.DummyOutStatus4
                    hMotorDetail.DummyOutStatus5 = mMotorDetail.DummyOutStatus5

                    hMotorDetail.DummyFaExtGr = mMotorDetail.DummyFaExtGr
                    hMotorDetail.DummyFaDelay = mMotorDetail.DummyFaDelay
                    hMotorDetail.DummyFaGrep1 = mMotorDetail.DummyFaGrep1
                    hMotorDetail.DummyFaGrep2 = mMotorDetail.DummyFaGrep2
                    hMotorDetail.DummyFaStaNm = mMotorDetail.DummyFaStaNm
                    hMotorDetail.DummyFaTimeV = mMotorDetail.DummyFaTimeV
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    intAns = 0  ''変更有り

                End If

            End If

            hMode = 0
            If mintNextChFlag = 1 Then
                hMode = 1   ''Next CH
            ElseIf mintBeforeChFlag = 1 Then
                hMode = 2   ''Before CH
            End If

            gShow = intAns

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
    Private Sub frmChListMotor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim intValue As Integer
            Dim strFuno As String = "", strValue As String = ""
            Dim strwk() As String = Nothing

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            mintFirst = 1

            ''画面初期化
            Call mInitial()

            With mMotorDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTagNo.Text = .TagNo      '' 2015.10.27 Ver1.7.5 ﾀｸﾞ追加
                txtItemName.Text = .ItemName
                txtRemarks.Text = .Remarks

                If .AlmLevel <> Nothing Then
                    cmbAlmLvl.SelectedValue = .AlmLevel
                Else
                    cmbAlmLvl.SelectedValue = "0"
                End If

                'cmbAlmLvl.SelectedValue = .AlmLevel    '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If .ShareType <> Nothing Then
                    cmbShareType.Enabled = True : lblShareType.Enabled = True
                    txtShareChid.Enabled = True : lblShareChid.Enabled = True

                    cmbShareType.SelectedValue = .ShareType
                    '■Share対応
                    If cmbShareType.SelectedValue = 1 Or cmbShareType.SelectedValue = 3 Then
                        txtShareChid.Text = .ShareChNo
                    Else
                        txtShareChid.Text = ""
                        txtShareChid.Enabled = False : lblShareChid.Enabled = False
                    End If

                Else
                    cmbShareType.Enabled = False : lblShareType.Enabled = False
                    txtShareChid.Enabled = False : lblShareChid.Enabled = False
                End If

                txtDmy.Text = .FlagDmy
                txtSC.Text = .FlagSC
                txtSio.Text = .FlagSIO
                txtGWS.Text = .FlagGWS
                txtWK.Text = .FlagWK
                txtRL.Text = .FlagRL
                txtAC.Text = .FlagAC
                txtEP.Text = .FlagEP
                txtPLC.Text = .FlagPLC      '' 2014.11.18
                txtSP.Text = .FlagSP
                txtMotorCol.Text = .FlagMotorCol      '' ver2.0.8.C 保安庁向け表示色変更　2018.11.14

                txtExtGroup.Text = .ExtGH
                txtDelayTimer.Text = .DelayH
                txtGRep1.Text = .GRep1H
                txtGRep2.Text = .GRep2H

                cmbDataType.SelectedValue = .DataType
                'Ver2.0.0.2 コンボインターフェース
                Call subSet3CBO()

                If .DataType = gCstCodeChDataTypeMotorDeviceJacom Then    ''外部機器
                    cmbExtDevice.SelectedValue = .PortNo
                End If
                If .DataType = gCstCodeChDataTypeMotorDeviceJacom55 Then    ''外部機器
                    cmbExtDevice.SelectedValue = .PortNo
                End If

                ''DI Start
                If .DIStart <> "" Then

                    txtFuNoDi.Text = Trim(.DIStart)
                    txtPortNoDi.Text = Trim(.DIPortStart)
                    txtPinDi.Text = Trim(.DIPinStart)

                End If

                ''DO Start
                If .DOStart <> "" Then

                    txtFuNoDo.Text = Trim(.DOStart)
                    txtPortNoDo.Text = Trim(.DOPortStart)
                    txtPinDo.Text = Trim(.DOPinStart)

                End If

                ''Status I
                'Ver2.0.0.2 モーター種別増加 D Device追加
                If .DataType = gCstCodeChDataTypeMotorDevice Or .DataType = gCstCodeChDataTypeMotorDeviceJacom Or .DataType = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                   .DataType = gCstCodeChDataTypeMotorRDevice Then
                    cmbStatusInChanged(0, -1)
                Else
                    ''Data Type からStatus IN を自動設定する
                    strValue = cmbDataType.Text
                    If strValue.IndexOf(":") >= 0 Then
                        strValue = strValue.Substring(strValue.IndexOf(":") + 1)
                    Else
                        strValue = "RUN"
                    End If

                    intValue = cmbStatusIn.FindStringExact(strValue)
                    If intValue >= 0 Then

                        cmbStatusIn.SelectedIndex = intValue
                        Call cmbStatusInChanged(cmbStatusIn.SelectedValue, cmbStatusIn.SelectedIndex)

                    Else
                        cmbStatusIn.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                        txtStatusIn.Text = .StatusIn
                    End If
                End If

                ''Status O
                cmbStatusOut.SelectedValue = .StatusOut
                'Ver2.0.1.2 STATUSが無いときは、マニュアル入力へ強制
                If cmbStatusOut.SelectedIndex < 0 Then
                    cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus
                End If

                Select Case .StatusOut
                    Case gCstCodeChStatusTypeMotorRun, gCstCodeChStatusTypeMotorRunG        '2箇所入力可
                        txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                        txtStatusDo1.Text = .StatusDO(0) : txtStatusDo2.Text = .StatusDO(1)

                    Case gCstCodeChStatusTypeMotorRunA, gCstCodeChStatusTypeMotorRunB, gCstCodeChStatusTypeMotorRunC, gCstCodeChStatusTypeMotorRunE, gCstCodeChStatusTypeMotorRunH, gCstCodeChStatusTypeMotorRunI                                       '3箇所入力可
                        txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                        txtStatusDo1.Text = .StatusDO(0) : txtStatusDo2.Text = .StatusDO(1) : txtStatusDo3.Text = .StatusDO(2)

                    Case gCstCodeChStatusTypeMotorRunJ, gCstCodeChStatusTypeMotorRunK       '4箇所入力可
                        txtStatusDo5.Enabled = False
                        txtStatusDo1.Text = .StatusDO(0) : txtStatusDo2.Text = .StatusDO(1) : txtStatusDo3.Text = .StatusDO(2) : txtStatusDo4.Text = .StatusDO(3)

                    Case gCstCodeChStatusTypeMotorRunF, gCstCodeChStatusTypeMotorRunD       '5箇所入力可(RUN-F,Dは全部)
                        txtStatusDo1.Text = .StatusDO(0) : txtStatusDo2.Text = .StatusDO(1) : txtStatusDo3.Text = .StatusDO(2) : txtStatusDo4.Text = .StatusDO(3) : txtStatusDo5.Text = .StatusDO(4)

                    Case Else                                                               'MANUAL INPUT
                        txtStatusDo1.Enabled = False : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False


                End Select

                If lblBitPosDi1.Text <> "" Then txtStatusDo1.Text = .StatusDO(0)
                If lblBitPosDi2.Text <> "" Then txtStatusDo2.Text = .StatusDO(1)
                If lblBitPosDi3.Text <> "" Then txtStatusDo3.Text = .StatusDO(2)
                If lblBitPosDi4.Text <> "" Then txtStatusDo4.Text = .StatusDO(3)
                If lblBitPosDi5.Text <> "" Then txtStatusDo5.Text = .StatusDO(4)

                Call lblBitPosDi_TextChanged(lblBitPosDi1, New EventArgs)
                Call lblBitPosDi_TextChanged(lblBitPosDi2, New EventArgs)
                Call lblBitPosDi_TextChanged(lblBitPosDi3, New EventArgs)
                Call lblBitPosDi_TextChanged(lblBitPosDi4, New EventArgs)
                Call lblBitPosDi_TextChanged(lblBitPosDi5, New EventArgs)

                chkStatusAlarm.Checked = .FlagStatusAlarm

                txtAlarmTimeup.Text = .AlarmTimeup

                cmbControlType.SelectedValue = CCInt(.ControlType)
                txtPulseWidth.Text = .PulseWidth
                '' 2015.10.27 Ver1.7.5 初期値設定
                '' 2015.11.16 Ver1.7.9  ﾌｨﾙﾀｰ値0の場合にｾｯﾄするように変更
                If .FilterCoef = 0 Then
                    txtFilterCoeficient.Text = 12
                Else
                    txtFilterCoeficient.Text = .FilterCoef
                End If

                cmbTime.SelectedValue = IIf(.FlagMin = "", 0, .FlagMin)

                ''DO Alarm
                txtExtGDo.Text = .ExtgDoAlarm
                txtDelayDo.Text = .DelayDoAlarm
                txtGRep1Do.Text = .GRep1DoAlarm
                txtGRep2Do.Text = .GRep2DoAlarm
                txtStatusDo.Text = .StatusDoAlarm

                cmdBeforeCH.Enabled = True
                cmdNextCH.Enabled = True
                If .RowNoFirst = .RowNo Then cmdBeforeCH.Enabled = False
                If .RowNoEnd = .RowNo Then cmdNextCH.Enabled = False


                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                If .AlmMimic = "0" Then
                    txtAlmMimic.Text = ""
                Else
                    txtAlmMimic.Text = .AlmMimic
                End If

            End With

            mintOkFlag = 0
            mintFirst = 0


            ''▼▼▼ 20110614 仮設定機能対応（モーター） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If mMotorDetail.DummyExtG Then Call objDummySetControl_KeyDown(txtExtGroup, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyGroupRepose1 Then Call objDummySetControl_KeyDown(txtGRep1, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyGroupRepose2 Then Call objDummySetControl_KeyDown(txtGRep2, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFuAddress Then Call txtFuAdrressDi_KeyDown(txtFuNoDi, New KeyEventArgs(gCstDummySetKey))

            If mMotorDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoDo, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyOutStatusType Then Call cmbStatusOut_KeyDown(cmbStatusOut, New KeyEventArgs(gCstDummySetKey))

            If mMotorDetail.DummyOutStatus1 Then Call objDummySetControl_KeyDown(txtStatusDo1, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyOutStatus2 Then Call objDummySetControl_KeyDown(txtStatusDo2, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyOutStatus3 Then Call objDummySetControl_KeyDown(txtStatusDo3, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyOutStatus4 Then Call objDummySetControl_KeyDown(txtStatusDo4, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyOutStatus5 Then Call objDummySetControl_KeyDown(txtStatusDo5, New KeyEventArgs(gCstDummySetKey))

            If mMotorDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))
            If mMotorDetail.DummyFaTimeV Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
            ''▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： データタイプコンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbDataType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectedIndexChanged

        Try
            Dim strValue As String = ""
            Dim intValue As Integer = 0

            If cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then   'Ver2.0.0.2 モーター種別増加 R Device追加
                ''機器運転(Device Operation) IN のみ
                cmbExtDevice.Visible = False

                txtFuNoDi.Enabled = True : txtPortNoDi.Enabled = True : txtPinDi.Enabled = True
                lblDiStart.Enabled = True

                txtFuNoDo.Text = "" : txtPortNoDo.Text = "" : txtPinDo.Text = ""
                lblDo1.Text = "" : lblDo2.Text = "" : lblDo3.Text = "" : lblDo4.Text = "" : lblDo5.Text = ""
                txtStatusDo1.Text = "" : txtStatusDo2.Text = "" : txtStatusDo3.Text = "" : txtStatusDo4.Text = "" : txtStatusDo5.Text = ""
                txtStatusDo1.Enabled = False : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                txtFuNoDo.Enabled = False : txtPortNoDo.Enabled = False : txtPinDo.Enabled = False
                lblDoStart.Enabled = False
                cmbStatusOut.Enabled = False : lblStatusOut.Enabled = False

                If mintFirst = 0 Then Call cmbStatusInChanged(0, -1)

            ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom55 Then
                ''外部機器（JACOM-22） IN のみ
                cmbExtDevice.Visible = True
                Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22DI)
                cmbExtDevice.SelectedValue = 1

                txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""
                lblDi1.Text = "" : lblDi2.Text = "" : lblDi3.Text = "" : lblDi4.Text = "" : lblDi5.Text = ""
                txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                lblDiStart.Enabled = False

                txtFuNoDo.Text = "" : txtPortNoDo.Text = "" : txtPinDo.Text = ""
                lblDo1.Text = "" : lblDo2.Text = "" : lblDo3.Text = "" : lblDo4.Text = "" : lblDo5.Text = ""
                txtStatusDo1.Text = "" : txtStatusDo2.Text = "" : txtStatusDo3.Text = "" : txtStatusDo4.Text = "" : txtStatusDo5.Text = ""
                txtStatusDo1.Enabled = False : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                txtFuNoDo.Enabled = False : txtPortNoDo.Enabled = False : txtPinDo.Enabled = False
                lblDoStart.Enabled = False
                cmbStatusOut.Enabled = False : lblStatusOut.Enabled = False

                If mintFirst = 0 Then Call cmbStatusInChanged(0, -1)

            Else
                ''RUN, RUN-A ～ RUN-J
                cmbExtDevice.Visible = False

                txtFuNoDi.Enabled = True : txtPortNoDi.Enabled = True : txtPinDi.Enabled = True
                lblDiStart.Enabled = True

                txtFuNoDo.Enabled = True : txtPortNoDo.Enabled = True : txtPinDo.Enabled = True
                lblDoStart.Enabled = True
                cmbStatusOut.Enabled = True : lblStatusOut.Enabled = True
                txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = True : txtStatusDo5.Enabled = True

                If mintFirst = 0 Then
                    If cmbStatusOut.SelectedIndex < 0 Then cmbStatusOut.SelectedIndex = 0
                    Call cmbStatusInChanged(cmbStatusIn.SelectedValue, cmbStatusIn.SelectedIndex)   ''2011-06-16
                    Call cmbStatusOutChanged(cmbStatusOut.SelectedValue, cmbStatusOut.SelectedIndex, cmbDataType.SelectedIndex)
                End If

            End If

            If mintFirst = 0 Then

                ''Status I を自動設定する
                strValue = cmbDataType.Text
                If strValue.IndexOf(":") >= 0 Then
                    strValue = strValue.Substring(strValue.IndexOf(":") + 1)
                Else
                    strValue = "RUN"
                End If

                intValue = cmbStatusIn.FindStringExact(strValue)
                If intValue >= 0 Then
                    cmbStatusIn.SelectedIndex = intValue
                Else
                    cmbStatusIn.SelectedIndex = 0
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ステータスコンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbStatusIn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatusIn.SelectedIndexChanged

        Try

            If mintStatusFlag = 1 Then Return

            Call cmbStatusInChanged(cmbStatusIn.SelectedValue, cmbStatusIn.SelectedIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatusOut_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatusOut.SelectedIndexChanged

        Try

            If mintStatusFlag = 1 Then Return
            If cmbStatusOut.SelectedIndex = -1 Then Return

            If cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus.ToString Then   ''手入力
                txtStatusDo1.Text = ""
                txtStatusDo2.Text = ""
                txtStatusDo3.Text = ""
                txtStatusDo4.Text = ""
                txtStatusDo5.Text = ""
            End If

            Call cmbStatusOutChanged(cmbStatusOut.SelectedValue, cmbStatusOut.SelectedIndex, cmbDataType.SelectedValue)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Share Type コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbShareType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShareType.SelectedIndexChanged

        Try

            If cmbShareType.SelectedValue = 1 Or cmbShareType.SelectedValue = 3 Then  '■Share対応
                ''Local
                txtShareChid.Enabled = True : lblShareChid.Enabled = True

            Else
                ''Remote
                txtShareChid.Text = ""
                txtShareChid.Enabled = False : lblShareChid.Enabled = False
            End If

            If cmbShareType.SelectedValue = 0 Then          'Nothingが選択されたとき:white
                txtChNo.BackColor = Color.White
            ElseIf cmbShareType.SelectedValue = 1 Then      'Localが選択されたとき:gray
                txtChNo.BackColor = Color.WhiteSmoke
            ElseIf cmbShareType.SelectedValue = 2 Then      'Remoteが選択されたとき:blue
                txtChNo.BackColor = Color.AliceBlue
            ElseIf cmbShareType.SelectedValue = 3 Then      'Shareが選択されたとき:light green
                txtChNo.BackColor = Color.LightGreen

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 出力制御種別 コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbControlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbControlType.SelectedIndexChanged

        Try

            If cmbControlType.SelectedValue = 0 Then
                ''連続出力
                txtPulseWidth.Enabled = False : lblPulseWidth.Enabled = False
                txtPulseWidth.Text = ""

            ElseIf cmbControlType.SelectedValue = 1 Then
                ''パルス出力
                txtPulseWidth.Enabled = True : lblPulseWidth.Enabled = True
                If txtPulseWidth.Text = "" Then txtPulseWidth.Text = "1"
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListMotor_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 内部メモリに画面上の情報を格納する
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''画面の設定値を内部メモリに取り込む
            Call mGetSetData()

            FeedBackKazu = Val(mMotorDetail.AlarmTimeup)

            mintOkFlag = 1

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： BeforeCH ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdBeforeCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeforeCH.Click

        Try
            ''入力チェック
            If Not mChkInput() Then Return

            ''画面の設定値を内部メモリに取り込む
            Call mGetSetData()

            mintOkFlag = 1

            ''フラグ ON
            mintBeforeChFlag = 1

            ''一旦閉じる
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： NextCH ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdNextCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextCH.Click

        Try
            ''入力チェック
            If Not mChkInput() Then Return

            ''画面の設定値を内部メモリに取り込む
            Call mGetSetData()

            mintOkFlag = 1

            ''フラグ ON
            mintNextChFlag = 1

            ''一旦閉じる
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            Dim strValue As String = CType(sender, System.Windows.Forms.TextBox).Text
            Dim strName As String = CType(sender, System.Windows.Forms.TextBox).Name

            If IsNumeric(strValue) Then

                CType(sender, System.Windows.Forms.TextBox).Text = Integer.Parse(strValue).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtShareChid_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShareChid.Validated

        Try

            If IsNumeric(txtShareChid.Text) Then
                txtShareChid.Text = Integer.Parse(txtShareChid.Text).ToString("0000")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： DI Start, DO Start をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFuAddress_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                Handles txtFuNoDi.Validated, txtPortNoDi.Validated, txtPinDi.Validated, _
                                                        txtFuNoDo.Validated, txtPortNoDo.Validated, txtPinDo.Validated
        Dim strName As String = CType(sender, System.Windows.Forms.TextBox).Name
        Try

            strName = strName.Substring(strName.Length - 1, 1)

            If strName = "i" Then   ''DI

                lblDi1.Text = ""
                lblDi2.Text = ""
                lblDi3.Text = ""
                lblDi4.Text = ""
                lblDi5.Text = ""

                If txtFuNoDi.Text <> "" And txtPortNoDi.Text <> "" And txtPinDi.Text <> "" Then

                    txtPinDi.Text = CInt(txtPinDi.Text).ToString("00")

                    If lblStatusDi1.Text <> "" Then lblDi1.Text = txtFuNoDi.Text & txtPortNoDi.Text & CInt(txtPinDi.Text).ToString("00")

                    If lblStatusDi2.Text <> "" Then
                        If 1 + CInt(txtPinDi.Text) <= 64 Then
                            lblDi2.Text = txtFuNoDi.Text & txtPortNoDi.Text & (1 + CInt(txtPinDi.Text)).ToString("00")
                        End If
                    End If

                    If lblStatusDi3.Text <> "" Then
                        If 2 + CInt(txtPinDi.Text) <= 64 Then
                            lblDi3.Text = txtFuNoDi.Text & txtPortNoDi.Text & (2 + CInt(txtPinDi.Text)).ToString("00")
                        End If
                    End If

                    If lblStatusDi4.Text <> "" Then
                        If 3 + CInt(txtPinDi.Text) <= 64 Then
                            lblDi4.Text = txtFuNoDi.Text & txtPortNoDi.Text & (3 + CInt(txtPinDi.Text)).ToString("00")
                        End If
                    End If

                    If lblStatusDi5.Text <> "" Then
                        If 4 + CInt(txtPinDi.Text) <= 64 Then
                            lblDi5.Text = txtFuNoDi.Text & txtPortNoDi.Text & (4 + CInt(txtPinDi.Text)).ToString("00")
                        End If
                    End If

                End If

            ElseIf strName = "o" Then   ''DO

                lblDo1.Text = ""
                lblDo2.Text = ""
                lblDo3.Text = ""
                lblDo4.Text = ""
                lblDo5.Text = ""

                If txtFuNoDo.Text <> "" And txtPortNoDo.Text <> "" And txtPinDo.Text <> "" Then

                    txtPinDo.Text = CInt(txtPinDo.Text).ToString("00")

                    If lblBitPosDi1.Text <> "" Then lblDo1.Text = txtFuNoDo.Text & txtPortNoDo.Text & CInt(txtPinDo.Text).ToString("00")

                    If lblBitPosDi2.Text <> "" Then
                        If 1 + CInt(txtPinDo.Text) <= 64 Then
                            lblDo2.Text = txtFuNoDo.Text & txtPortNoDo.Text & (1 + CInt(txtPinDo.Text)).ToString("00")
                        End If
                    End If

                    If lblBitPosDi3.Text <> "" Then
                        If 2 + CInt(txtPinDo.Text) <= 64 Then
                            lblDo3.Text = txtFuNoDo.Text & txtPortNoDo.Text & (2 + CInt(txtPinDo.Text)).ToString("00")
                        End If
                    End If

                    If lblBitPosDi4.Text <> "" Then
                        If 3 + CInt(txtPinDo.Text) <= 64 Then
                            lblDo4.Text = txtFuNoDo.Text & txtPortNoDo.Text & (3 + CInt(txtPinDo.Text)).ToString("00")
                        End If
                    End If

                    If lblBitPosDi5.Text <> "" Then
                        If 4 + CInt(txtPinDo.Text) <= 64 Then
                            lblDo5.Text = txtFuNoDo.Text & txtPortNoDo.Text & (4 + CInt(txtPinDo.Text)).ToString("00")
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： EXT.Gに値が設定された場合にStatus Alarmにチェックを入れる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtExtGroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExtGroup.Validated

        '▼▼▼ 20110705 Ext.Gを無しにした場合は自動でStatusAlarmのチェックを外す ▼▼▼▼▼▼▼▼▼
        '' 2013.11.30 Ext.Gが0の場合もアラーム設定は有とする
        ''If txtExtGroup.Text <> "" And Val(txtExtGroup.Text) <> 0 Then
        If txtExtGroup.Text <> "" Then
            chkStatusAlarm.Checked = True
        Else
            chkStatusAlarm.Checked = False
        End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： DO StatusのBit位置表示 を変更した場合、対応するTerminal Noの表示を更新する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub lblBitPosDi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                    lblBitPosDi1.TextChanged, lblBitPosDi2.TextChanged, lblBitPosDi3.TextChanged, lblBitPosDi4.TextChanged, lblBitPosDi5.TextChanged

        Try
            Dim strName As String = sender.Name

            If txtPinDo.Text <> "" Then

                Select Case strName.Substring(strName.Length - 1, 1)
                    Case "1"
                        If lblBitPosDi1.Text <> "" Then
                            lblDo1.Text = txtFuNoDo.Text & txtPortNoDo.Text & CInt(txtPinDo.Text).ToString("00")
                        Else
                            lblDo1.Text = ""
                        End If
                    Case "2"
                        If lblBitPosDi2.Text <> "" Then
                            lblDo2.Text = txtFuNoDo.Text & txtPortNoDo.Text & (1 + CInt(txtPinDo.Text)).ToString("00")
                        Else
                            lblDo2.Text = ""
                        End If
                    Case "3"
                        If lblBitPosDi3.Text <> "" Then
                            lblDo3.Text = txtFuNoDo.Text & txtPortNoDo.Text & (2 + CInt(txtPinDo.Text)).ToString("00")
                        Else
                            lblDo3.Text = ""
                        End If
                    Case "4"
                        If lblBitPosDi4.Text <> "" Then
                            lblDo4.Text = txtFuNoDo.Text & txtPortNoDo.Text & (3 + CInt(txtPinDo.Text)).ToString("00")
                        Else
                            lblDo4.Text = ""
                        End If
                    Case "5"
                        If lblBitPosDi5.Text <> "" Then
                            lblDo5.Text = txtFuNoDo.Text & txtPortNoDo.Text & (4 + CInt(txtPinDo.Text)).ToString("00")
                        Else
                            lblDo5.Text = ""
                        End If
                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress, txtShareChid.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtItemName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress

        Try

            e.Handled = gCheckTextInput(30, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusDo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                    txtStatusDo1.KeyPress, txtStatusDo2.KeyPress, txtStatusDo3.KeyPress, txtStatusDo4.KeyPress, txtStatusDo5.KeyPress, _
                    txtStatusDo.KeyPress
        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAlarm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtExtGroup.KeyPress, txtGRep1.KeyPress, txtGRep2.KeyPress, _
                            txtPinDi.KeyPress, txtPinDo.KeyPress, _
                            txtExtGDo.KeyPress, txtGRep1Do.KeyPress, txtGRep2Do.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPulseWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseWidth.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt4Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlarmTimeup.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilterCoeficient.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)   '' フィルタ定数変更　ver.1.4.4 2012.05.08

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFlag1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
            Handles txtDmy.KeyPress, txtSC.KeyPress, txtWK.KeyPress, txtRL.KeyPress, _
                    txtAC.KeyPress, txtEP.KeyPress, txtPLC.KeyPress, txtSP.KeyPress, txtMotorCol.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusIn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusIn.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusOut_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusOut.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                                Handles txtFuNoDi.KeyPress, txtFuNoDo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                                    Handles txtPortNoDi.KeyPress, txtPortNoDo.KeyPress

        Try
            '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(2, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9,51,52") '2019.04.01 二桁の数字を入力できるよう変更

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelayDo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDelayDo.KeyPress

        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAlmMimic_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmMimic.KeyPress
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Try
            '数値のみ。マイナスや小数点不可
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#Region "BitSet画面表示関連"

    Private Sub txtSio_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSio.KeyPress

        Try

            Dim intValue As Integer = CCInt(txtSio.Text)

            If e.KeyChar = Chr(Keys.Enter) Then
                If frmBitSetByte.gShow(intValue, 1, Me) = 1 Then
                    txtSio.Text = intValue
                End If
            End If

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtGWS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGWS.KeyPress

        Try

            Dim intValue As Integer = CCInt(txtGWS.Text)

            If e.KeyChar = Chr(Keys.Enter) Then
                If frmBitSetByte.gShow(intValue, 0, Me) = 1 Then
                    txtGWS.Text = intValue
                End If
            End If

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtSio_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSio.GotFocus
        lblBitSet.Visible = True
    End Sub

    Private Sub txtGWS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGWS.GotFocus
        lblBitSet.Visible = True
    End Sub

    Private Sub txtSio_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSio.LostFocus
        lblBitSet.Visible = False
    End Sub

    Private Sub txtGWS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGWS.LostFocus
        lblBitSet.Visible = False
    End Sub

#End Region

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能      : 設定値GET
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面の設定値を内部メモリに取り込む
    '----------------------------------------------------------------------------
    Private Sub mGetSetData()

        Try
            With mMotorDetail

                For i As Integer = 0 To UBound(.StatusDO)
                    .StatusDO(i) = ""
                Next

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTagNo.Text)  '' 2015.10.27 Ver1.7.5 ﾀｸﾞ追加
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If cmbShareType.Enabled = True Then
                    .ShareType = cmbShareType.SelectedValue
                    .ShareChNo = IIf(txtShareChid.Text = "", Nothing, txtShareChid.Text)
                End If

                .ExtGH = txtExtGroup.Text
                .GRep1H = txtGRep1.Text
                .GRep2H = txtGRep2.Text

                .FlagDmy = txtDmy.Text
                .FlagSC = txtSC.Text
                .FlagSIO = txtSio.Text
                .FlagGWS = txtGWS.Text
                .FlagWK = txtWK.Text
                .FlagRL = txtRL.Text
                .FlagAC = txtAC.Text
                .FlagEP = txtEP.Text
                .FlagPLC = txtPLC.Text      '' 2014.11.18
                .FlagSP = txtSP.Text
                .FlagMotorCol = txtMotorCol.Text    '' ver2.0.8.C 2018.11.14

                .DataType = cmbDataType.SelectedValue

                'Ver2.0.0.2 モーター種別増加 R Device追加
                If .DataType = gCstCodeChDataTypeMotorDevice Or .DataType = gCstCodeChDataTypeMotorRDevice Then
                    ''機器運転(Device Operation)

                    'If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    '    .StatusIn = cmbStatusIn.Text
                    'Else
                    '    .StatusIn = txtStatusIn.Text
                    'End If

                    .StatusOut = "255"

                ElseIf .DataType = gCstCodeChDataTypeMotorDeviceJacom Then
                    ''外部機器(Ext Device (JACOM-22))
                    .PortNo = cmbExtDevice.SelectedValue

                    'If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    '    .StatusIn = cmbStatusIn.Text
                    'Else
                    '    .StatusIn = txtStatusIn.Text
                    'End If

                    .StatusOut = "255"

                ElseIf .DataType = gCstCodeChDataTypeMotorDeviceJacom55 Then
                    ''外部機器(Ext Device (JACOM-22))
                    .PortNo = cmbExtDevice.SelectedValue

                    'If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    '    .StatusIn = cmbStatusIn.Text
                    'Else
                    '    .StatusIn = txtStatusIn.Text
                    'End If

                    .StatusOut = "255"

                Else
                    'If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    '    .StatusIn = cmbStatusIn.Text
                    'Else
                    '    .StatusIn = txtStatusIn.Text
                    'End If

                    If lblBitPosDi1.Text <> "" Then .StatusDO(0) = txtStatusDo1.Text
                    If lblBitPosDi2.Text <> "" Then .StatusDO(1) = txtStatusDo2.Text
                    If lblBitPosDi3.Text <> "" Then .StatusDO(2) = txtStatusDo3.Text
                    If lblBitPosDi4.Text <> "" Then .StatusDO(3) = txtStatusDo4.Text
                    If lblBitPosDi5.Text <> "" Then .StatusDO(4) = txtStatusDo5.Text

                    If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                        .StatusOut = cmbStatusOut.SelectedValue
                    Else
                        .StatusOut = gCstCodeChManualInputStatus.ToString
                    End If
                End If


                .DIStart = txtFuNoDi.Text
                .DIPortStart = txtPortNoDi.Text
                .DIPinStart = txtPinDi.Text

                .DOStart = txtFuNoDo.Text
                .DOPortStart = txtPortNoDo.Text
                .DOPinStart = txtPinDo.Text

                .FlagStatusAlarm = chkStatusAlarm.Checked

                .AlarmTimeup = txtAlarmTimeup.Text

                .ControlType = cmbControlType.SelectedValue
                .PulseWidth = txtPulseWidth.Text
                .FilterCoef = txtFilterCoeficient.Text
                .FlagMin = cmbTime.SelectedValue

                .ExtgDoAlarm = txtExtGDo.Text
                .DelayDoAlarm = txtDelayDo.Text
                .GRep1DoAlarm = txtGRep1Do.Text
                .GRep2DoAlarm = txtGRep2Do.Text
                .StatusDoAlarm = txtStatusDo.Text

                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text


                '▼▼▼ 20110614 仮設定機能対応（モーター） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .DummyExtG = gDummyCheckControl(txtExtGroup)
                .DummyGroupRepose1 = gDummyCheckControl(txtGRep1)
                .DummyGroupRepose2 = gDummyCheckControl(txtGRep2)
                .DummyFuAddress = gDummyCheckControl(txtFuNoDi)

                .DummyOutFuAddress = gDummyCheckControl(txtFuNoDo)
                .DummyOutStatusType = gDummyCheckControl(cmbStatusOut)

                .DummyOutStatus1 = gDummyCheckControl(txtStatusDo1)
                .DummyOutStatus2 = gDummyCheckControl(txtStatusDo2)
                .DummyOutStatus3 = gDummyCheckControl(txtStatusDo3)
                .DummyOutStatus4 = gDummyCheckControl(txtStatusDo4)
                .DummyOutStatus5 = gDummyCheckControl(txtStatusDo5)

                .DummyFaExtGr = gDummyCheckControl(txtExtGDo)
                .DummyFaDelay = gDummyCheckControl(txtDelayDo)
                .DummyFaGrep1 = gDummyCheckControl(txtGRep1)
                .DummyFaGrep2 = gDummyCheckControl(txtGRep2)
                .DummyFaStaNm = gDummyCheckControl(txtStatusOut)
                .DummyFaTimeV = gDummyCheckControl(txtAlarmTimeup)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 入力ステータス変更時処理
    ' 返り値    : 0
    ' 引き数    : ARG1 - (I ) ステータス種別コード
    '           : ARG2 - (I ) ステータスコンボ選択インデックス
    ' 機能説明  : ステータス種別により状態が変化する
    '--------------------------------------------------------------------
    Private Sub cmbStatusInChanged(ByVal intSelectValue As Integer, ByVal intSelectIndex As Integer)

        Try

            Dim strwk() As String = Nothing
            Dim strbp() As String = Nothing

            lblStatusDi1.Text = ""
            lblStatusDi2.Text = ""
            lblStatusDi3.Text = ""
            lblStatusDi4.Text = ""
            lblStatusDi5.Text = ""

            lblDi1.Text = ""
            lblDi2.Text = ""
            lblDi3.Text = ""
            lblDi4.Text = ""
            lblDi5.Text = ""

            lblBitPosDi1.Text = ""
            lblBitPosDi2.Text = ""
            lblBitPosDi3.Text = ""
            lblBitPosDi4.Text = ""
            lblBitPosDi5.Text = ""

            If intSelectValue = gCstCodeChManualInputStatus.ToString Then
                ''手入力
                txtStatusIn.Visible = True

            Else
                txtStatusIn.Visible = False

                'Ver2.0.0.2 モーター種別増加 R Device追加
                If cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then

                    ' 2013.07.22 MO表示変更  K.Fujimoto
                    'lblStatusDi1.Text = "RUN/STOP"
                    lblStatusDi1.Text = "RUN"

                Else
                    ''「_」区切りの文字列取得
                    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then 'Ver2.0.0.2 モーター種別増加 JをKへ
                        strwk = mMotorStatus1(intSelectIndex).ToString.Split("_")
                        strbp = mMotorBitPos1(intSelectIndex).ToString.Split("_")
                    Else
                        'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                        If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                            cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunK Then
                            '正常Rは正常ステータス扱い
                            strwk = mMotorStatus1(intSelectIndex).ToString.Split("_")
                            strbp = mMotorBitPos1(intSelectIndex).ToString.Split("_")
                        Else
                            strwk = mMotorStatus2(intSelectIndex).ToString.Split("_")
                            strbp = mMotorBitPos2(intSelectIndex).ToString.Split("_")
                        End If
                    End If

                    ''ステータス種別により状態が変化する
                    Dim intCnt As Integer = UBound(strwk)

                    If intCnt >= 0 Then
                        If strwk(0) <> "" Then
                            lblStatusDi1.Text = strwk(0)
                            lblBitPosDi1.Text = strbp(0)
                            If txtPinDi.Text <> "" Then
                                lblDi1.Text = txtFuNoDi.Text & txtPortNoDi.Text & CInt(txtPinDi.Text).ToString("00")
                            End If
                        End If
                    End If

                    If intCnt >= 1 Then
                        If strwk(1) <> "" Then
                            lblStatusDi2.Text = strwk(1)
                            lblBitPosDi2.Text = strbp(1)
                            If txtPinDi.Text <> "" Then
                                If 1 + CInt(txtPinDi.Text) <= gCstCntFuSlotPinMax Then
                                    lblDi2.Text = txtFuNoDi.Text & txtPortNoDi.Text & (1 + CInt(txtPinDi.Text)).ToString("00")
                                End If
                            End If
                        End If
                    End If

                    If intCnt >= 2 Then
                        If strwk(2) <> "" Then
                            lblStatusDi3.Text = strwk(2)
                            lblBitPosDi3.Text = strbp(2)
                            If txtPinDi.Text <> "" Then
                                If 2 + CInt(txtPinDi.Text) <= gCstCntFuSlotPinMax Then
                                    lblDi3.Text = txtFuNoDi.Text & txtPortNoDi.Text & (2 + CInt(txtPinDi.Text)).ToString("00")
                                End If
                            End If
                        End If
                    End If

                    If intCnt >= 3 Then
                        If strwk(3) <> "" Then
                            lblStatusDi4.Text = strwk(3)
                            lblBitPosDi4.Text = strbp(3)
                            If txtPinDi.Text <> "" Then
                                If 3 + CInt(txtPinDi.Text) <= gCstCntFuSlotPinMax Then
                                    lblDi4.Text = txtFuNoDi.Text & txtPortNoDi.Text & (3 + CInt(txtPinDi.Text)).ToString("00")
                                End If
                            End If
                        End If
                    End If

                    If intCnt >= 4 Then
                        If strwk(4) <> "" Then
                            lblStatusDi5.Text = strwk(4)
                            lblBitPosDi5.Text = strbp(4)
                            If txtPinDi.Text <> "" Then
                                If 4 + CInt(txtPinDi.Text) <= gCstCntFuSlotPinMax Then
                                    lblDi5.Text = txtFuNoDi.Text & txtPortNoDi.Text & (4 + CInt(txtPinDi.Text)).ToString("00")
                                End If
                            End If
                        End If
                    End If

                End If

            End If



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 出力ステータス変更時処理
    ' 返り値    : 0
    ' 引き数    : ARG1 - (I ) ステータス種別コード
    '           : ARG2 - (I ) ステータスコンボ選択インデックス
    ' 機能説明  : ステータス種別により状態が変化する
    '--------------------------------------------------------------------
    Private Sub cmbStatusOutChanged(ByVal intSelectValue As Integer, ByVal intSelectIndex As Integer, ByVal intSelectDataTypeIndex As Integer)

        Try

            Dim strwk() As String = Nothing
            Dim strbp() As String = Nothing

            If intSelectValue = gCstCodeChManualInputStatus.ToString Then
                ''手入力
                'データ種類に対応した数だけ
                Select Case intSelectDataTypeIndex
                    Case gCstCodeChDataTypeMotorManRun, gCstCodeChDataTypeMotorAbnorRun, gCstCodeChDataTypeMotorManRunG, gCstCodeChDataTypeMotorAbnorRunG        '2箇所入力可
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False

                    Case gCstCodeChDataTypeMotorManRunA, gCstCodeChDataTypeMotorManRunB, gCstCodeChDataTypeMotorManRunC, gCstCodeChDataTypeMotorManRunE, gCstCodeChDataTypeMotorManRunH, gCstCodeChDataTypeMotorManRunI, _
                        gCstCodeChDataTypeMotorAbnorRunA, gCstCodeChDataTypeMotorAbnorRunB, gCstCodeChDataTypeMotorAbnorRunC, gCstCodeChDataTypeMotorAbnorRunE, gCstCodeChDataTypeMotorAbnorRunH, gCstCodeChDataTypeMotorAbnorRunI '3箇所入力可
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False

                    Case gCstCodeChDataTypeMotorManRunJ, gCstCodeChDataTypeMotorAbnorRunJ, gCstCodeChDataTypeMotorAbnorRunK        '4箇所入力可
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = True : txtStatusDo5.Enabled = False

                    Case Else
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = True : txtStatusDo5.Enabled = True
                End Select

            Else

                txtStatusDo1.Text = "" : txtStatusDo1.Enabled = False
                txtStatusDo2.Text = "" : txtStatusDo2.Enabled = False
                txtStatusDo3.Text = "" : txtStatusDo3.Enabled = False
                txtStatusDo4.Text = "" : txtStatusDo4.Enabled = False
                txtStatusDo5.Text = "" : txtStatusDo5.Enabled = False

                ''「_」区切りの文字列取得
                If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                   cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then
                    strwk = mMotorStatus1(intSelectIndex).ToString.Split("_")
                    strbp = mMotorBitPos1(intSelectIndex).ToString.Split("_")
                Else
                    'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                        cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunK Then
                        '正常Rは正常ステータス扱い
                        strwk = mMotorStatus1(intSelectIndex).ToString.Split("_")
                        strbp = mMotorBitPos1(intSelectIndex).ToString.Split("_")
                    Else
                        strwk = mMotorStatus2(intSelectIndex).ToString.Split("_")
                        strbp = mMotorBitPos2(intSelectIndex).ToString.Split("_")
                    End If
                End If

                ''ステータス種別により状態が変化する
                Dim intCnt As Integer = UBound(strwk)

                If intCnt >= 0 Then
                    If strwk(0) <> "" Then
                        txtStatusDo1.Text = strwk(0)
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                    End If
                End If

                If intCnt >= 1 Then
                    If strwk(1) <> "" Then
                        txtStatusDo2.Text = strwk(1)
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                    End If
                End If

                If intCnt >= 2 Then
                    If strwk(2) <> "" Then
                        txtStatusDo3.Text = strwk(2)
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                    End If

                End If

                If intCnt >= 3 Then
                    If strwk(3) <> "" Then
                        txtStatusDo4.Text = strwk(3)
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = True : txtStatusDo5.Enabled = False
                    End If
                End If

                If intCnt >= 4 Then
                    If strwk(4) <> "" Then
                        txtStatusDo5.Text = strwk(4)
                        txtStatusDo1.Enabled = True : txtStatusDo2.Enabled = True : txtStatusDo3.Enabled = True : txtStatusDo4.Enabled = True : txtStatusDo5.Enabled = True
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

            ''共通テキスト入力チェック
            If Not gChkInputText(txtItemName, "Item Name", True, True) Then Return False
            If Not gChkInputText(txtRemarks, "Remarks", True, True) Then Return False
            'If Not gChkInputText(txtStatusIn, "Status I", True, True) Then Return False
            If Not gChkInputText(txtStatusDo, "DO Alarm[Status]", True, True) Then Return False

            If Not gChkInputText(txtStatusDo1, "DO Status 1", True, True) Then Return False
            If Not gChkInputText(txtStatusDo2, "DO Status 2", True, True) Then Return False
            If Not gChkInputText(txtStatusDo3, "DO Status 3", True, True) Then Return False
            If Not gChkInputText(txtStatusDo4, "DO Status 4", True, True) Then Return False
            If Not gChkInputText(txtStatusDo5, "DO Status 5", True, True) Then Return False

            If ChkTagInput(txtTagNo.Text) = False Then Return False '' 2015.10.27 Ver1.7.5

            ''共通数値入力チェック
            If Not gChkInputNum(txtChNo, 1, 65535, "CH No", False, True) Then Return False
            If Not gChkInputNum(txtDmy, 0, 1, "Dmy", True, True) Then Return False
            If Not gChkInputNum(txtSC, 0, 1, "SC", True, True) Then Return False
            If Not gChkInputNum(txtSio, 0, 511, "SIO", True, True) Then Return False
            If Not gChkInputNum(txtGWS, 0, 255, "GWS", True, True) Then Return False
            If Not gChkInputNum(txtWK, 0, 1, "WK", True, True) Then Return False
            If Not gChkInputNum(txtRL, 0, 1, "RL", True, True) Then Return False
            If Not gChkInputNum(txtAC, 0, 1, "AC", True, True) Then Return False
            If Not gChkInputNum(txtEP, 0, 1, "EP", True, True) Then Return False
            If Not gChkInputNum(txtPLC, 0, 1, "Prt1", True, True) Then Return False
            If Not gChkInputNum(txtSP, 0, 1, "Prt2", True, True) Then Return False
            If Not gChkInputNum(txtMotorCol, 0, 1, "MotorCol", True, True) Then Return False '' ver2.0.8.C 保安庁向け表示色変更　2018.11.14
            If Not gChkInputNum(txtExtGroup, 0, 24, "EXT.G", True, True) Then Return False
            If Not gChkInputNum(txtGRep1, 0, 48, "G REP1", True, True) Then Return False
            If Not gChkInputNum(txtGRep2, 0, 48, "G REP2", True, True) Then Return False

            'T.Ueki
            If Not gChkInputNum(txtAlarmTimeup, 0, 600, "Alarm Timeup Count", True, True) Then Return False
            'If Not gChkInputNum(txtAlarmTimeup, 0, 6000, "Alarm Timeup Count", True, True) Then Return False

            If Not gChkInputNum(txtFilterCoeficient, 1, 250, "Filter Coeficient", True, True) Then Return False '' フィルタ定数変更　ver.1.4.4 2012.05.08
            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

            If cmbControlType.SelectedValue = 1 Then
                If Not gChkInputNum(txtPulseWidth, 1, 200, "Output pulse width", False, True) Then Return False
            End If

            If Not gChkInputNum(txtExtGDo, 0, 24, "DO Alarm[EXT.G]", True, True) Then Return False
            If Not gChkInputNum(txtDelayDo, 0, 240, "DO Alarm[Delay]", True, True) Then Return False
            If Not gChkInputNum(txtGRep1Do, 0, 48, "DO Alarm[G REP1]", True, True) Then Return False
            If Not gChkInputNum(txtGRep2Do, 0, 48, "DO Alarm[G REP2]", True, True) Then Return False
            If Not gChkInputNum(txtDelayDo, 0, 240, "DO Alarm[Delay]", True, True) Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If


            ''共通FUアドレス入力チェック
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''If Not gChkInputFuAddress(txtFuNoDi, txtPortNoDi, txtPinDi, 64, True, True) Then Return False
            ''If Not gChkInputFuAddress(txtFuNoDo, txtPortNoDo, txtPinDo, 64, True, True) Then Return False

            ''Bit Count と　FUアドレス lblStatusDi1
            'If txtPinDi.Text <> "" Then
            '    If (lblStatusDi1.Text <> "" And lblDi1.Text = "") Or _
            '       (lblStatusDi2.Text <> "" And lblDi2.Text = "") Or _
            '       (lblStatusDi3.Text <> "" And lblDi3.Text = "") Or _
            '       (lblStatusDi4.Text <> "" And lblDi4.Text = "") Or _
            '       (lblStatusDi5.Text <> "" And lblDi5.Text = "") Then
            '        Call MessageBox.Show("PinNo is illegal. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Return False
            '    End If
            'End If

            If txtPinDo.Text <> "" Then
                If txtStatusDo1.Text = "" And txtStatusDo2.Text = "" And txtStatusDo3.Text = "" And _
                   txtStatusDo4.Text = "" And txtStatusDo5.Text = "" Then
                    Call MessageBox.Show("Please input Do Status. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
                'If (txtStatusDo1.Text <> "" And lblDo1.Text = "") Or _
                '   (txtStatusDo2.Text <> "" And lblDo2.Text = "") Or _
                '   (txtStatusDo3.Text <> "" And lblDo3.Text = "") Or _
                '   (txtStatusDo4.Text <> "" And lblDo4.Text = "") Or _
                '   (txtStatusDo5.Text <> "" And lblDo5.Text = "") Then
                '    Call MessageBox.Show("PinNo is illegal. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                'End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 画面初期化
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitial()

        Try

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)

            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeMotor)

            mintStatusFlag = 1
            Call gSetComboBox(cmbStatusIn, gEnmComboType.ctChListChannelListStatusMotor)
            Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusMotor)
            mintStatusFlag = 0

            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)
            Call gSetComboBox(cmbControlType, gEnmComboType.ctChListChannelListOutputControlType)

            Call gSetComboBox(cmbAlmLvl, gEnmComboType.ctChListChannelListAlmLevel)       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

            ''モーターチャンネルのステータス情報を獲得する
            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)


            'Ver2.0.0.2 コンボインターフェース
            'DataTypeインターフェースコンボ初期化
            Call subInitCbo()


            'Ver2.0.0.8
            'TagNoはﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then
                txtTagNo.Enabled = False
            End If

            'Ver2.0.0.9
            'Alarm Levelは、ﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                cmbAlmLvl.SelectedIndex = 0
                cmbAlmLvl.Enabled = False
            End If


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
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mMotorInfo, _
                                         ByVal udt2 As frmChListChannelList.mMotorInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.27 Ver1.7.5 ﾀｸﾞ追加
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            If udt1.ExtGH <> udt2.ExtGH Then Return False
            If udt1.GRep1H <> udt2.GRep1H Then Return False
            If udt1.GRep2H <> udt2.GRep2H Then Return False
            If udt1.FlagDmy <> udt2.FlagDmy Then Return False
            If udt1.FlagSC <> udt2.FlagSC Then Return False
            If udt1.FlagSIO <> udt2.FlagSIO Then Return False
            If udt1.FlagGWS <> udt2.FlagGWS Then Return False
            If udt1.FlagWK <> udt2.FlagWK Then Return False
            If udt1.FlagRL <> udt2.FlagRL Then Return False
            If udt1.FlagAC <> udt2.FlagAC Then Return False
            If udt1.FlagEP <> udt2.FlagEP Then Return False
            If udt1.FlagPLC <> udt2.FlagPLC Then Return False '' 2014.11.18
            If udt1.FlagSP <> udt2.FlagSP Then Return False
            If udt1.FlagMin <> udt2.FlagMin Then Return False
            If udt1.FlagMotorCol <> udt2.FlagMotorCol Then Return False '' ver2.0.8.C 2018.11.14

            If udt1.DIStart <> udt2.DIStart Then Return False
            If udt1.DIPortStart <> udt2.DIPortStart Then Return False
            If udt1.DIPinStart <> udt2.DIPinStart Then Return False

            If udt1.DOStart <> udt2.DOStart Then Return False
            If udt1.DIPortStart <> udt2.DIPortStart Then Return False
            If udt1.DIPinStart <> udt2.DIPinStart Then Return False

            If udt1.DataType <> udt2.DataType Then Return False
            If udt1.PortNo <> udt2.PortNo Then Return False
            'If udt1.StatusIn <> udt2.StatusIn Then Return False
            If udt1.StatusOut <> udt2.StatusOut Then Return False
            If udt1.FlagStatusAlarm <> udt2.FlagStatusAlarm Then Return False
            If udt1.FilterCoef <> udt2.FilterCoef Then Return False
            If udt1.AlarmTimeup <> udt2.AlarmTimeup Then Return False
            If udt1.ControlType <> udt2.ControlType Then Return False
            If udt1.PulseWidth <> udt2.PulseWidth Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False

            If udt1.ExtgDoAlarm <> udt2.ExtgDoAlarm Then Return False
            If udt1.DelayDoAlarm <> udt2.DelayDoAlarm Then Return False
            If udt1.GRep1DoAlarm <> udt2.GRep1DoAlarm Then Return False
            If udt1.GRep2DoAlarm <> udt2.GRep2DoAlarm Then Return False
            If udt1.StatusDoAlarm <> udt2.StatusDoAlarm Then Return False

            For i As Integer = 0 To UBound(udt1.StatusDO)
                If udt1.StatusDO(i) <> udt2.StatusDO(i) Then Return False
            Next i

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False


            '▼▼▼ 20110614 仮設定機能対応（モーター） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If udt1.DummyExtG <> udt2.DummyExtG Then Return False
            If udt1.DummyGroupRepose1 <> udt2.DummyGroupRepose1 Then Return False
            If udt1.DummyGroupRepose2 <> udt2.DummyGroupRepose2 Then Return False
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False

            If udt1.DummyOutFuAddress <> udt2.DummyOutFuAddress Then Return False
            If udt1.DummyOutStatusType <> udt2.DummyOutStatusType Then Return False

            If udt1.DummyOutStatus1 <> udt2.DummyOutStatus1 Then Return False
            If udt1.DummyOutStatus2 <> udt2.DummyOutStatus2 Then Return False
            If udt1.DummyOutStatus3 <> udt2.DummyOutStatus3 Then Return False
            If udt1.DummyOutStatus4 <> udt2.DummyOutStatus4 Then Return False
            If udt1.DummyOutStatus5 <> udt2.DummyOutStatus5 Then Return False

            If udt1.DummyFaExtGr <> udt2.DummyFaExtGr Then Return False
            If udt1.DummyFaDelay <> udt2.DummyFaDelay Then Return False
            If udt1.DummyFaGrep1 <> udt2.DummyFaGrep1 Then Return False
            If udt1.DummyFaGrep2 <> udt2.DummyFaGrep2 Then Return False
            If udt1.DummyFaStaNm <> udt2.DummyFaStaNm Then Return False
            If udt1.DummyFaTimeV <> udt2.DummyFaTimeV Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "仮設定関連"

    Private Sub objDummySetControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
        txtExtGroup.KeyDown, txtDelayTimer.KeyDown, txtGRep1.KeyDown, txtGRep2.KeyDown, _
        txtAlarmTimeup.KeyDown, txtStatusDo1.KeyDown, txtStatusDo2.KeyDown, txtStatusDo3.KeyDown, txtStatusDo4.KeyDown, txtStatusDo5.KeyDown, _
        txtExtGDo.KeyDown, txtDelayDo.KeyDown, txtGRep1Do.KeyDown, txtGRep2Do.KeyDown, txtStatusDo.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(sender)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatusIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatusIn.KeyDown, _
                                                                                                                txtStatusIn.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbStatusIn)
                Call gDummySetColorChange(txtStatusIn)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatusOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatusOut.KeyDown, _
                                                                                                                   txtStatusOut.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbStatusOut)
                Call gDummySetColorChange(txtStatusOut)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrressDi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoDi.KeyDown, txtPortNoDi.KeyDown, txtPinDi.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtFuNoDi)
                Call gDummySetColorChange(txtPortNoDi)
                Call gDummySetColorChange(txtPinDi)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrressDo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoDo.KeyDown, txtPortNoDo.KeyDown, txtPinDo.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtFuNoDo)
                Call gDummySetColorChange(txtPortNoDo)
                Call gDummySetColorChange(txtPinDo)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#Region "データタイプ選択インターフェース"
    '仕様
    ' 最終的なデータは、既存のDataTypeコンボとする。
    ' 本コンボ3種は、入力補助とする。
    ' 1つ目のコンボは、通信を選択。空白 or "R" or "J"
    ' 2つ目のコンボは、M0～M2を選択。
    ' 3つ目のコンボは、RUN A～Kを選択
    Private Sub subInitCbo()
        With cmbR
            .Items.Clear()
            .Items.Add("")
            .Items.Add("R")
            .Items.Add("J")
            .Items.Add("J55")
        End With

        With cmbM
            .Items.Clear()
            .Items.Add("")
            .Items.Add("M1")
            .Items.Add("M2")
            .Items.Add("M0")
        End With

        With cmbRUN
            .Items.Clear()
        End With
    End Sub

    'CboMが変わると、CboRUNが対応値に代わる
    Private Sub cmbM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbM.SelectedIndexChanged
        With cmbRUN
            Select Case cmbM.Text
                Case "M1", "M2"
                    If cmbR.Text = "J" Then
                        cmbR.Text = ""
                    End If
                    .Items.Clear()
                    .Items.Add("RUN")
                    .Items.Add("RUN-A")
                    .Items.Add("RUN-B")
                    .Items.Add("RUN-C")
                    .Items.Add("RUN-D")
                    .Items.Add("RUN-E")
                    .Items.Add("RUN-F")
                    .Items.Add("RUN-G")
                    .Items.Add("RUN-H")
                    .Items.Add("RUN-I")
                    .Items.Add("RUN-J")
                    .Items.Add("RUN-K")
                    .Text = "RUN"
                Case "M0"
                    .Items.Clear()
                    .Items.Add("RUN")
                    .Text = "RUN"
                Case Else
                    .Items.Clear()
            End Select
        End With

        Call subSetDataTypeUI()
    End Sub
    'CboRが変わると、他が対応値に代わる
    Private Sub cmbR_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbR.SelectedIndexChanged

        Select Case cmbR.Text
            Case "J", "J55"
                cmbM.Text = "M0"
        End Select

        Call subSetDataTypeUI()
    End Sub
    Private Sub cmbRUN_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRUN.SelectedIndexChanged
        Call subSetDataTypeUI()
    End Sub

    '3つcboを参考にDataTypeを変更する
    Private Sub subSetDataTypeUI()
        Dim intRUN As Integer = 0
        Dim intR As Integer = 0
        Dim intM As Integer = 0

        '該当無しの場合は、Manual Runにｾｯﾄ
        Dim intData As Integer = gCstCodeChDataTypeMotorManRun

        '強引に計算して出すこととする。
        'もし計算が通用しなくなればSelectCaseの組み合わせに変更。
        Select Case cmbRUN.Text
            Case "RUN"
                intRUN = &H0
            Case "RUN-A"
                intRUN = &H1
            Case "RUN-B"
                intRUN = &H2
            Case "RUN-C"
                intRUN = &H3
            Case "RUN-D"
                intRUN = &H4
            Case "RUN-E"
                intRUN = &H5
            Case "RUN-F"
                intRUN = &H6
            Case "RUN-G"
                intRUN = &H7
            Case "RUN-H"
                intRUN = &H8
            Case "RUN-I"
                intRUN = &H9
            Case "RUN-J"
                intRUN = &HA
            Case "RUN-K"
                intRUN = &HB
        End Select

        Select Case cmbR.Text
            Case "R"
                intR = &H40
            Case "J"
                'JはJACOM固定
                cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom
                Exit Sub
            Case "J55"
                'JはJACOM固定
                cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom55
                Exit Sub
            Case Else
                intR = 0
        End Select

        Select Case cmbM.Text
            Case "M1"
                intM = &H10
            Case "M2"
                intM = &H20
            Case "M0"
                intM = &H30
        End Select

        intData = intRUN + intR + intM

        cmbDataType.SelectedValue = intData
    End Sub

    'DataTpeを参考に3つcboを変更
    Private Sub subSet3CBO()
        'DataTypeを退避(3Cboが変わると変わってしまうため)
        Dim intData As Integer = cmbDataType.SelectedValue

        Select Case intData
            Case gCstCodeChDataTypeMotorManRun
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorManRunA
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-A"
            Case gCstCodeChDataTypeMotorManRunB
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-B"
            Case gCstCodeChDataTypeMotorManRunC
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-C"
            Case gCstCodeChDataTypeMotorManRunD
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-D"
            Case gCstCodeChDataTypeMotorManRunE
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-E"
            Case gCstCodeChDataTypeMotorManRunF
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-F"
            Case gCstCodeChDataTypeMotorManRunG
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-G"
            Case gCstCodeChDataTypeMotorManRunH
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-H"
            Case gCstCodeChDataTypeMotorManRunI
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-I"
            Case gCstCodeChDataTypeMotorManRunJ
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-J"
            Case gCstCodeChDataTypeMotorManRunK
                cmbR.Text = ""
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-K"
            Case gCstCodeChDataTypeMotorAbnorRun
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorAbnorRunA
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-A"
            Case gCstCodeChDataTypeMotorAbnorRunB
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-B"
            Case gCstCodeChDataTypeMotorAbnorRunC
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-C"
            Case gCstCodeChDataTypeMotorAbnorRunD
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-D"
            Case gCstCodeChDataTypeMotorAbnorRunE
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-E"
            Case gCstCodeChDataTypeMotorAbnorRunF
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-F"
            Case gCstCodeChDataTypeMotorAbnorRunG
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-G"
            Case gCstCodeChDataTypeMotorAbnorRunH
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-H"
            Case gCstCodeChDataTypeMotorAbnorRunI
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-I"
            Case gCstCodeChDataTypeMotorAbnorRunJ
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-J"
            Case gCstCodeChDataTypeMotorAbnorRunK
                cmbR.Text = ""
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-K"
            Case gCstCodeChDataTypeMotorDevice
                cmbR.Text = ""
                cmbM.Text = "M0"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorDeviceJacom
                cmbR.Text = "J"
                cmbM.Text = "M0"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorDeviceJacom55
                cmbR.Text = "J55"
                cmbM.Text = "M0"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorRManRun
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorRManRunA
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-A"
            Case gCstCodeChDataTypeMotorRManRunB
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-B"
            Case gCstCodeChDataTypeMotorRManRunC
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-C"
            Case gCstCodeChDataTypeMotorRManRunD
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-D"
            Case gCstCodeChDataTypeMotorRManRunE
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-E"
            Case gCstCodeChDataTypeMotorRManRunF
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-F"
            Case gCstCodeChDataTypeMotorRManRunG
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-G"
            Case gCstCodeChDataTypeMotorRManRunH
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-H"
            Case gCstCodeChDataTypeMotorRManRunI
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-I"
            Case gCstCodeChDataTypeMotorRManRunJ
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-J"
            Case gCstCodeChDataTypeMotorRManRunK
                cmbR.Text = "R"
                cmbM.Text = "M1"
                cmbRUN.Text = "RUN-K"
            Case gCstCodeChDataTypeMotorRAbnorRun
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN"
            Case gCstCodeChDataTypeMotorRAbnorRunA
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-A"
            Case gCstCodeChDataTypeMotorRAbnorRunB
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-B"
            Case gCstCodeChDataTypeMotorRAbnorRunC
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-C"
            Case gCstCodeChDataTypeMotorRAbnorRunD
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-D"
            Case gCstCodeChDataTypeMotorRAbnorRunE
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-E"
            Case gCstCodeChDataTypeMotorRAbnorRunF
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-F"
            Case gCstCodeChDataTypeMotorRAbnorRunG
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-G"
            Case gCstCodeChDataTypeMotorRAbnorRunH
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-H"
            Case gCstCodeChDataTypeMotorRAbnorRunI
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-I"
            Case gCstCodeChDataTypeMotorRAbnorRunJ
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-J"
            Case gCstCodeChDataTypeMotorRAbnorRunK
                cmbR.Text = "R"
                cmbM.Text = "M2"
                cmbRUN.Text = "RUN-K"
            Case gCstCodeChDataTypeMotorRDevice
                cmbR.Text = "R"
                cmbM.Text = "M0"
                cmbRUN.Text = "RUN"
            Case Else
                'cmbR.Text = ""
                'cmbM.Text = ""
                'cmbRUN.Text = ""
        End Select
    End Sub

    Private Sub btnHANEI_Click(sender As System.Object, e As System.EventArgs) Handles btnHANEI.Click
        'Ver2.0.0.2 コンボインターフェース
        Call subSet3CBO()
    End Sub

#End Region


#Region "コメントアウト"

    '----------------------------------------------------------------------------
    ' 機能説明  ： DO Status を変更した場合、対応するTerminal Noの表示を更新する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtStatusDo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    '               txtStatusDo1.Validated, txtStatusDo2.Validated, txtStatusDo3.Validated, txtStatusDo4.Validated, txtStatusDo5.Validated

    '    Try
    '        Dim strName As String = sender.Name

    '        If txtPinDo.Text <> "" Then

    '            Select Case strName.Substring(strName.Length - 1, 1)

    '                Case "1"
    '                    If txtStatusDo1.Text <> "" Then
    '                        lblDo1.Text = txtFuNoDo.Text & txtPortNoDo.Text & CInt(txtPinDo.Text).ToString("00")
    '                    Else
    '                        lblDo1.Text = ""
    '                    End If
    '                Case "2"
    '                    If txtStatusDo2.Text <> "" Then
    '                        lblDo2.Text = txtFuNoDo.Text & txtPortNoDo.Text & (1 + CInt(txtPinDo.Text)).ToString("00")
    '                    Else
    '                        lblDo2.Text = ""
    '                    End If
    '                Case "3"
    '                    If txtStatusDo3.Text <> "" Then
    '                        lblDo3.Text = txtFuNoDo.Text & txtPortNoDo.Text & (2 + CInt(txtPinDo.Text)).ToString("00")
    '                    Else
    '                        lblDo3.Text = ""
    '                    End If
    '                Case "4"
    '                    If txtStatusDo4.Text <> "" Then
    '                        lblDo4.Text = txtFuNoDo.Text & txtPortNoDo.Text & (3 + CInt(txtPinDo.Text)).ToString("00")
    '                    Else
    '                        lblDo4.Text = ""
    '                    End If
    '                Case "5"
    '                    If txtStatusDo5.Text <> "" Then
    '                        lblDo5.Text = txtFuNoDo.Text & txtPortNoDo.Text & (4 + CInt(txtPinDo.Text)).ToString("00")
    '                    Else
    '                        lblDo5.Text = ""
    '                    End If
    '            End Select

    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 設定単位 コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub cmbTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTime.SelectedIndexChanged

    '    If mintDelayTimeKubun <> cmbTime.SelectedValue Then

    '        If cmbTime.SelectedValue = 0 Then
    '            ''分 -- > 秒
    '            If txtDelayDo.Text <> "" Then txtDelayDo.Text = Format(CCDouble(txtDelayDo.Text) * 60)
    '        Else
    '            ''秒 --> 分
    '            If txtDelayDo.Text <> "" Then txtDelayDo.Text = Format(CCDouble(txtDelayDo.Text) / 60, "0.0")
    '        End If

    '    End If

    '    mintDelayTimeKubun = cmbTime.SelectedValue

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtDelayDo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelayDo.Validated

    '    Try

    '        If cmbTime.SelectedValue = 1 Then
    '            If txtDelayDo.Text <> "" Then txtDelayDo.Text = Double.Parse(txtDelayDo.Text).ToString("0.0")
    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub frmChListAnalog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    '    Try

    '        If mintOkFlag <> 1 Then

    '            ''データが変更されているかチェック
    '            If mChkDataChange() Then

    '                ''変更されている場合はメッセージ表示
    '                Select Case MessageBox.Show("Setting has been changed. Do you save it?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

    '                    Case Windows.Forms.DialogResult.Yes

    '                        Call cmdOK_Click(cmdOK, New EventArgs)

    '                        If mintOkFlag <> 1 Then e.Cancel = True

    '                    Case Windows.Forms.DialogResult.No

    '                        ''何もしない

    '                    Case Windows.Forms.DialogResult.Cancel

    '                        ''画面を閉じない
    '                        e.Cancel = True
    '                        mintBeforeChFlag = 0 : mintNextChFlag = 0
    '                        Exit Sub

    '                End Select

    '            End If

    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '--------------------------------------------------------------------
    ' 機能      : データ変更チェック
    ' 返り値    : True:変更有り、False:変更なし
    ' 引き数    : なし
    ' 機能説明  : データが変更されているかチェックを行う
    '--------------------------------------------------------------------
    'Private Function mChkDataChange() As Boolean

    '    With mMotorDetail

    '        If .SysNo <> cmbSysNo.SelectedValue Then Return True
    '        If .ChNo <> txtChNo.Text Then Return True
    '        If .ItemName <> txtItemName.Text Then Return True
    '        If .Remarks <> txtRemarks.Text Then Return True

    '        If cmbShareType.Enabled = True Then
    '            If .ShareType <> cmbShareType.SelectedValue Then Return True
    '            If .ShareChNo <> txtShareChid.Text Then Return True
    '        End If

    '        'If .FlagMrepose <> chkMrepose.Checked Then Return True

    '        If .ExtGH <> txtExtGroup.Text Then Return True
    '        If .DelayH <> txtDelayTimer.Text Then Return True
    '        If .GRep1H <> txtGRep1.Text Then Return True
    '        If .GRep2H <> txtGRep2.Text Then Return True

    '        If .FlagDmy <> txtDmy.Text Then Return True
    '        If .FlagSC <> txtSC.Text Then Return True
    '        If .FlagSIO <> txtSio.Text Then Return True
    '        If .FlagGWS <> txtGWS.Text Then Return True
    '        If .FlagWK <> txtWK.Text Then Return True
    '        If .FlagRL <> txtRL.Text Then Return True
    '        If .FlagAC <> txtAC.Text Then Return True
    '        If .FlagEP <> txtEP.Text Then Return True
    '        If .FlagPrt1 <> txtPr1.Text Then Return True
    '        If .FlagPrt2 <> txtPr2.Text Then Return True

    '        'If .FlagMin <> cmbTime.SelectedValue.ToString Then Return True

    '        If .FlagStatusAlarm <> chkStatusAlarm.Checked Then Return True

    '        If .DIStart <> txtFuNoDi.Text & txtPortNoDi.Text & txtPinDi.Text Then Return True
    '        If .DOStart <> txtFuNoDo.Text & txtPortNoDo.Text & txtPinDo.Text Then Return True

    '        If .AlarmTimeup <> txtAlarmTimeup.Text Then Return True
    '        If .FilterCoef <> txtFilterCoeficient.Text Then Return True

    '        If .DataType <> cmbDataType.SelectedValue Then Return True

    '        If .DataType = gCstCodeChDataTypeMotorDevice Then
    '            ''機器運転(Device Operation)

    '            If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                If .StatusIn <> cmbStatusIn.Text Then Return True
    '            Else
    '                If .StatusIn <> txtStatusIn.Text Then Return True
    '            End If

    '        ElseIf .DataType = gCstCodeChDataTypeMotorDeviceJacom Then
    '            ''外部機器(Ext Device (JACOM-22))
    '            If .PortNo <> cmbExtDevice.SelectedValue Then Return True

    '            If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                If .StatusIn <> cmbStatusIn.Text Then Return True
    '            Else
    '                If .StatusIn <> txtStatusIn.Text Then Return True
    '            End If

    '        Else

    '            If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                If .StatusIn <> cmbStatusIn.Text Then Return True
    '            Else
    '                If .StatusIn <> txtStatusIn.Text Then Return True
    '            End If

    '            If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                If .StatusOut <> cmbStatusOut.Text Then Return True
    '            Else
    '                If .StatusOut <> txtStatusOut.Text Then Return True
    '            End If

    '        End If

    '    End With

    '    Return False

    'End Function


#End Region

 
    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
