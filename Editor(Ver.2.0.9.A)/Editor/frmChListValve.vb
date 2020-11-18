Public Class frmChListValve

#Region "変数定義"

    ''OKフラグ
    Private mintOkFlag As Integer

    ''イベントキャンセルフラグ
    Private mintCancel As Integer

    ''Next CH ボタン　クリックフラグ
    Private mintNextChFlag As Integer = 0

    ''Before CH ボタン　クリックフラグ
    Private mintBeforeChFlag As Integer = 0

    ''イベント重複抑制用
    Private mblnFlg As Boolean

    ''Delay Timer 設定単位区分
    Private mintDelayTimeKubun As Integer   ''0:秒　1:分

    ''バルブチャンネル情報格納
    Private mValveDetail As frmChListChannelList.mValveInfo

    ''コンポジットテーブル使用フラグ
    ''既に使用CH設定済みのテーブル + 一時保存のテーブルのテーブル使用状況
    ''（ChList - ChCompositeSet   - CompositeList - CompositeDetail 間で使用する）
    ''           ChValve(DiDo)Set
    Private mblnCompositeTableUse() As Boolean

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) バルブチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hValveDetail As frmChListChannelList.mValveInfo, _
                          ByRef hblnCompositeTableUse() As Boolean, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            ReDim mValveDetail.StatusDO(7)

            mValveDetail.RowNo = hValveDetail.RowNo
            mValveDetail.RowNoFirst = hValveDetail.RowNoFirst
            mValveDetail.RowNoEnd = hValveDetail.RowNoEnd
            mValveDetail.SysNo = hValveDetail.SysNo
            mValveDetail.ChNo = hValveDetail.ChNo
            mValveDetail.TagNo = hValveDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
            mValveDetail.ItemName = hValveDetail.ItemName
            mValveDetail.AlmLevel = hValveDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mValveDetail.ExtGH_I = hValveDetail.ExtGH_I
            mValveDetail.DelayH_I = hValveDetail.DelayH_I
            mValveDetail.GRep1H_I = hValveDetail.GRep1H_I
            mValveDetail.GRep2H_I = hValveDetail.GRep2H_I
            mValveDetail.FlagDmy = hValveDetail.FlagDmy
            mValveDetail.FlagSC = hValveDetail.FlagSC
            mValveDetail.FlagSIO = hValveDetail.FlagSIO
            mValveDetail.FlagGWS = hValveDetail.FlagGWS
            mValveDetail.FlagWK = hValveDetail.FlagWK
            mValveDetail.FlagRL = hValveDetail.FlagRL
            mValveDetail.FlagAC = hValveDetail.FlagAC
            mValveDetail.FlagEP = hValveDetail.FlagEP
            mValveDetail.FlagPLC = hValveDetail.FlagPLC     '' 2014.11.18
            mValveDetail.FlagSP = hValveDetail.FlagSP
            mValveDetail.FlagMin = hValveDetail.FlagMin

            mValveDetail.BitCount = IIf(hValveDetail.BitCount = "0", "", hValveDetail.BitCount)
            hValveDetail.BitCount = IIf(hValveDetail.BitCount = "0", "", hValveDetail.BitCount)

            mValveDetail.DIStart = hValveDetail.DIStart
            mValveDetail.DIPortStart = hValveDetail.DIPortStart
            mValveDetail.DIPinStart = hValveDetail.DIPinStart

            mValveDetail.DOStart = hValveDetail.DOStart
            mValveDetail.DOPortStart = hValveDetail.DOPortStart
            mValveDetail.DOPinStart = hValveDetail.DOPinStart

            mValveDetail.AITerm = hValveDetail.AITerm
            mValveDetail.AIPortTerm = hValveDetail.AIPortTerm
            mValveDetail.AIPinTerm = hValveDetail.AIPinTerm

            mValveDetail.AOTerm = hValveDetail.AOTerm
            mValveDetail.AOPortTerm = hValveDetail.AOPortTerm
            mValveDetail.AOPinTerm = hValveDetail.AOPinTerm

            mValveDetail.DataType = hValveDetail.DataType
            mValveDetail.PortNo = hValveDetail.PortNo
            mValveDetail.StatusIn = hValveDetail.StatusIn
            mValveDetail.StatusOut = hValveDetail.StatusOut
            mValveDetail.FlagStatusAlarm = hValveDetail.FlagStatusAlarm
            mValveDetail.AlarmTimeup = hValveDetail.AlarmTimeup
            mValveDetail.EccFunc = hValveDetail.EccFunc
            mValveDetail.ShareType = hValveDetail.ShareType
            mValveDetail.ShareChNo = hValveDetail.ShareChNo
            mValveDetail.Remarks = hValveDetail.Remarks

            mValveDetail.Extg_O = hValveDetail.Extg_O
            mValveDetail.Delay_O = hValveDetail.Delay_O
            mValveDetail.GRep1_O = hValveDetail.GRep1_O
            mValveDetail.GRep2_O = hValveDetail.GRep2_O
            mValveDetail.Status_O = hValveDetail.Status_O

            mValveDetail.Sp1_O = hValveDetail.Sp1_O
            mValveDetail.Sp2_O = hValveDetail.Sp2_O
            mValveDetail.Hys1_O = hValveDetail.Hys1_O
            mValveDetail.Hys2_O = hValveDetail.Hys2_O
            mValveDetail.St_O = hValveDetail.St_O
            mValveDetail.Var_O = hValveDetail.Var_O

            For i As Integer = 0 To 7
                mValveDetail.StatusDO(i) = hValveDetail.StatusDO(i)
            Next

            mValveDetail.ControlType = hValveDetail.ControlType
            mValveDetail.PulseWidth = hValveDetail.PulseWidth

            mValveDetail.CompositeIndex = hValveDetail.CompositeIndex

            ''AI -----------------------------------------
            mValveDetail.ValueHH = hValveDetail.ValueHH
            mValveDetail.ValueH = hValveDetail.ValueH
            mValveDetail.ValueL = hValveDetail.ValueL
            mValveDetail.ValueLL = hValveDetail.ValueLL
            mValveDetail.ValueSF = hValveDetail.ValueSF
            mValveDetail.ExtGHH = hValveDetail.ExtGHH
            mValveDetail.ExtGH = hValveDetail.ExtGH
            mValveDetail.ExtGL = hValveDetail.ExtGL
            mValveDetail.ExtGLL = hValveDetail.ExtGLL
            mValveDetail.ExtGSF = hValveDetail.ExtGSF
            mValveDetail.DelayHH = hValveDetail.DelayHH
            mValveDetail.DelayH = hValveDetail.DelayH
            mValveDetail.DelayL = hValveDetail.DelayL
            mValveDetail.DelayLL = hValveDetail.DelayLL
            mValveDetail.DelaySF = hValveDetail.DelaySF
            mValveDetail.GRep1HH = hValveDetail.GRep1HH
            mValveDetail.GRep1H = hValveDetail.GRep1H
            mValveDetail.GRep1L = hValveDetail.GRep1L
            mValveDetail.GRep1LL = hValveDetail.GRep1LL
            mValveDetail.GRep2HH = hValveDetail.GRep2HH
            mValveDetail.GRep2H = hValveDetail.GRep2H
            mValveDetail.GRep2L = hValveDetail.GRep2L
            mValveDetail.GRep2LL = hValveDetail.GRep2LL
            mValveDetail.StatusHH = hValveDetail.StatusHH
            mValveDetail.StatusH = hValveDetail.StatusH
            mValveDetail.StatusL = hValveDetail.StatusL
            mValveDetail.StatusLL = hValveDetail.StatusLL
            mValveDetail.RangeFrom = hValveDetail.RangeFrom
            mValveDetail.RangeTo = hValveDetail.RangeTo
            mValveDetail.NormalLO = hValveDetail.NormalLO
            mValveDetail.NormalHI = hValveDetail.NormalHI
            mValveDetail.OffSet = hValveDetail.OffSet
            mValveDetail.Unit = hValveDetail.Unit
            mValveDetail.strString = hValveDetail.strString
            mValveDetail.FlagCenterGraph = hValveDetail.FlagCenterGraph
            ''-------------------------------------------------
            mblnCompositeTableUse = hblnCompositeTableUse

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mValveDetail.AlmMimic = hValveDetail.AlmMimic



            '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mValveDetail.DummyExtG = hValveDetail.DummyExtG
            mValveDetail.DummyDelay = hValveDetail.DummyDelay
            mValveDetail.DummyGroupRepose1 = hValveDetail.DummyGroupRepose1
            mValveDetail.DummyGroupRepose2 = hValveDetail.DummyGroupRepose2
            mValveDetail.DummyFuAddress = hValveDetail.DummyFuAddress
            mValveDetail.DummyBitCount = hValveDetail.DummyBitCount
            mValveDetail.DummyUnitName = hValveDetail.DummyUnitName
            mValveDetail.DummyStatusName = hValveDetail.DummyStatusName

            mValveDetail.DummyDelayHH = hValveDetail.DummyDelayHH
            mValveDetail.DummyValueHH = hValveDetail.DummyValueHH
            mValveDetail.DummyExtGrHH = hValveDetail.DummyExtGrHH
            mValveDetail.DummyGRep1HH = hValveDetail.DummyGRep1HH
            mValveDetail.DummyGRep2HH = hValveDetail.DummyGRep2HH
            mValveDetail.DummyStaNmHH = hValveDetail.DummyStaNmHH

            mValveDetail.DummyDelayH = hValveDetail.DummyDelayH
            mValveDetail.DummyValueH = hValveDetail.DummyValueH
            mValveDetail.DummyExtGrH = hValveDetail.DummyExtGrH
            mValveDetail.DummyGRep1H = hValveDetail.DummyGRep1H
            mValveDetail.DummyGRep2H = hValveDetail.DummyGRep2H
            mValveDetail.DummyStaNmH = hValveDetail.DummyStaNmH

            mValveDetail.DummyDelayL = hValveDetail.DummyDelayL
            mValveDetail.DummyValueL = hValveDetail.DummyValueL
            mValveDetail.DummyExtGrL = hValveDetail.DummyExtGrL
            mValveDetail.DummyGRep1L = hValveDetail.DummyGRep1L
            mValveDetail.DummyGRep2L = hValveDetail.DummyGRep2L
            mValveDetail.DummyStaNmL = hValveDetail.DummyStaNmL

            mValveDetail.DummyDelayLL = hValveDetail.DummyDelayLL
            mValveDetail.DummyValueLL = hValveDetail.DummyValueLL
            mValveDetail.DummyExtGrLL = hValveDetail.DummyExtGrLL
            mValveDetail.DummyGRep1LL = hValveDetail.DummyGRep1LL
            mValveDetail.DummyGRep2LL = hValveDetail.DummyGRep2LL
            mValveDetail.DummyStaNmLL = hValveDetail.DummyStaNmLL

            mValveDetail.DummyDelaySF = hValveDetail.DummyDelaySF
            mValveDetail.DummyValueSF = hValveDetail.DummyValueSF
            mValveDetail.DummyExtGrSF = hValveDetail.DummyExtGrSF
            mValveDetail.DummyGRep1SF = hValveDetail.DummyGRep1SF
            mValveDetail.DummyGRep2SF = hValveDetail.DummyGRep2SF
            mValveDetail.DummyStaNmSF = hValveDetail.DummyStaNmSF

            mValveDetail.DummyRangeScale = hValveDetail.DummyRangeScale
            mValveDetail.DummyRangeNormalHi = hValveDetail.DummyRangeNormalHi
            mValveDetail.DummyRangeNormalLo = hValveDetail.DummyRangeNormalLo

            mValveDetail.DummyOutFuAddress = hValveDetail.DummyOutFuAddress
            mValveDetail.DummyOutBitCount = hValveDetail.DummyOutBitCount
            mValveDetail.DummyOutStatusType = hValveDetail.DummyOutStatusType

            mValveDetail.DummyOutStatus1 = hValveDetail.DummyOutStatus1
            mValveDetail.DummyOutStatus2 = hValveDetail.DummyOutStatus2
            mValveDetail.DummyOutStatus3 = hValveDetail.DummyOutStatus3
            mValveDetail.DummyOutStatus4 = hValveDetail.DummyOutStatus4
            mValveDetail.DummyOutStatus5 = hValveDetail.DummyOutStatus5
            mValveDetail.DummyOutStatus6 = hValveDetail.DummyOutStatus6
            mValveDetail.DummyOutStatus7 = hValveDetail.DummyOutStatus7
            mValveDetail.DummyOutStatus8 = hValveDetail.DummyOutStatus8

            mValveDetail.DummyFaExtGr = hValveDetail.DummyFaExtGr
            mValveDetail.DummyFaDelay = hValveDetail.DummyFaDelay
            mValveDetail.DummyFaGrep1 = hValveDetail.DummyFaGrep1
            mValveDetail.DummyFaGrep2 = hValveDetail.DummyFaGrep2
            mValveDetail.DummyFaStaNm = hValveDetail.DummyFaStaNm
            mValveDetail.DummyFaTimeV = hValveDetail.DummyFaTimeV

            mValveDetail.DummySp1 = hValveDetail.DummySp1
            mValveDetail.DummySp2 = hValveDetail.DummySp2
            mValveDetail.DummyHysOpen = hValveDetail.DummyHysOpen
            mValveDetail.DummyHysClose = hValveDetail.DummyHysClose
            mValveDetail.DummySmpTime = hValveDetail.DummySmpTime
            mValveDetail.DummyVar = hValveDetail.DummyVar

            mValveDetail.DummyCmpStatus1Delay = hValveDetail.DummyCmpStatus1Delay
            mValveDetail.DummyCmpStatus1ExtGr = hValveDetail.DummyCmpStatus1ExtGr
            mValveDetail.DummyCmpStatus1GRep1 = hValveDetail.DummyCmpStatus1GRep1
            mValveDetail.DummyCmpStatus1GRep2 = hValveDetail.DummyCmpStatus1GRep2
            mValveDetail.DummyCmpStatus1StaNm = hValveDetail.DummyCmpStatus1StaNm

            mValveDetail.DummyCmpStatus2Delay = hValveDetail.DummyCmpStatus2Delay
            mValveDetail.DummyCmpStatus2ExtGr = hValveDetail.DummyCmpStatus2ExtGr
            mValveDetail.DummyCmpStatus2GRep1 = hValveDetail.DummyCmpStatus2GRep1
            mValveDetail.DummyCmpStatus2GRep2 = hValveDetail.DummyCmpStatus2GRep2
            mValveDetail.DummyCmpStatus2StaNm = hValveDetail.DummyCmpStatus2StaNm

            mValveDetail.DummyCmpStatus3Delay = hValveDetail.DummyCmpStatus3Delay
            mValveDetail.DummyCmpStatus3ExtGr = hValveDetail.DummyCmpStatus3ExtGr
            mValveDetail.DummyCmpStatus3GRep1 = hValveDetail.DummyCmpStatus3GRep1
            mValveDetail.DummyCmpStatus3GRep2 = hValveDetail.DummyCmpStatus3GRep2
            mValveDetail.DummyCmpStatus3StaNm = hValveDetail.DummyCmpStatus3StaNm

            mValveDetail.DummyCmpStatus4Delay = hValveDetail.DummyCmpStatus4Delay
            mValveDetail.DummyCmpStatus4ExtGr = hValveDetail.DummyCmpStatus4ExtGr
            mValveDetail.DummyCmpStatus4GRep1 = hValveDetail.DummyCmpStatus4GRep1
            mValveDetail.DummyCmpStatus4GRep2 = hValveDetail.DummyCmpStatus4GRep2
            mValveDetail.DummyCmpStatus4StaNm = hValveDetail.DummyCmpStatus4StaNm

            mValveDetail.DummyCmpStatus5Delay = hValveDetail.DummyCmpStatus5Delay
            mValveDetail.DummyCmpStatus5ExtGr = hValveDetail.DummyCmpStatus5ExtGr
            mValveDetail.DummyCmpStatus5GRep1 = hValveDetail.DummyCmpStatus5GRep1
            mValveDetail.DummyCmpStatus5GRep2 = hValveDetail.DummyCmpStatus5GRep2
            mValveDetail.DummyCmpStatus5StaNm = hValveDetail.DummyCmpStatus5StaNm

            mValveDetail.DummyCmpStatus6Delay = hValveDetail.DummyCmpStatus6Delay
            mValveDetail.DummyCmpStatus6ExtGr = hValveDetail.DummyCmpStatus6ExtGr
            mValveDetail.DummyCmpStatus6GRep1 = hValveDetail.DummyCmpStatus6GRep1
            mValveDetail.DummyCmpStatus6GRep2 = hValveDetail.DummyCmpStatus6GRep2
            mValveDetail.DummyCmpStatus6StaNm = hValveDetail.DummyCmpStatus6StaNm

            mValveDetail.DummyCmpStatus7Delay = hValveDetail.DummyCmpStatus7Delay
            mValveDetail.DummyCmpStatus7ExtGr = hValveDetail.DummyCmpStatus7ExtGr
            mValveDetail.DummyCmpStatus7GRep1 = hValveDetail.DummyCmpStatus7GRep1
            mValveDetail.DummyCmpStatus7GRep2 = hValveDetail.DummyCmpStatus7GRep2
            mValveDetail.DummyCmpStatus7StaNm = hValveDetail.DummyCmpStatus7StaNm

            mValveDetail.DummyCmpStatus8Delay = hValveDetail.DummyCmpStatus8Delay
            mValveDetail.DummyCmpStatus8ExtGr = hValveDetail.DummyCmpStatus8ExtGr
            mValveDetail.DummyCmpStatus8GRep1 = hValveDetail.DummyCmpStatus8GRep1
            mValveDetail.DummyCmpStatus8GRep2 = hValveDetail.DummyCmpStatus8GRep2
            mValveDetail.DummyCmpStatus8StaNm = hValveDetail.DummyCmpStatus8StaNm

            mValveDetail.DummyCmpStatus9Delay = hValveDetail.DummyCmpStatus9Delay
            mValveDetail.DummyCmpStatus9ExtGr = hValveDetail.DummyCmpStatus9ExtGr
            mValveDetail.DummyCmpStatus9GRep1 = hValveDetail.DummyCmpStatus9GRep1
            mValveDetail.DummyCmpStatus9GRep2 = hValveDetail.DummyCmpStatus9GRep2
            mValveDetail.DummyCmpStatus9StaNm = hValveDetail.DummyCmpStatus9StaNm
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''==================================================
            Call gShowFormModelessForCloseWait2(Me, frmOwner)
            ''==================================================

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hValveDetail, mValveDetail) = False Then

                    hValveDetail.SysNo = mValveDetail.SysNo
                    hValveDetail.ChNo = mValveDetail.ChNo
                    hValveDetail.TagNo = mValveDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
                    hValveDetail.ItemName = mValveDetail.ItemName
                    hValveDetail.AlmLevel = mValveDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hValveDetail.ExtGH_I = mValveDetail.ExtGH_I
                    hValveDetail.DelayH_I = mValveDetail.DelayH_I
                    hValveDetail.GRep1H_I = mValveDetail.GRep1H_I
                    hValveDetail.GRep2H_I = mValveDetail.GRep2H_I
                    hValveDetail.FlagDmy = mValveDetail.FlagDmy
                    hValveDetail.FlagSC = mValveDetail.FlagSC
                    hValveDetail.FlagSIO = mValveDetail.FlagSIO
                    hValveDetail.FlagGWS = mValveDetail.FlagGWS
                    hValveDetail.FlagWK = mValveDetail.FlagWK
                    hValveDetail.FlagRL = mValveDetail.FlagRL
                    hValveDetail.FlagAC = mValveDetail.FlagAC
                    hValveDetail.FlagEP = mValveDetail.FlagEP
                    hValveDetail.FlagPLC = mValveDetail.FlagPLC     '' 2014.11.18
                    hValveDetail.FlagSP = mValveDetail.FlagSP
                    hValveDetail.FlagMin = mValveDetail.FlagMin
                    hValveDetail.BitCount = mValveDetail.BitCount

                    hValveDetail.DIStart = mValveDetail.DIStart
                    hValveDetail.DIPortStart = mValveDetail.DIPortStart
                    hValveDetail.DIPinStart = mValveDetail.DIPinStart

                    hValveDetail.DOStart = mValveDetail.DOStart
                    hValveDetail.DOPortStart = mValveDetail.DOPortStart
                    hValveDetail.DOPinStart = mValveDetail.DOPinStart

                    hValveDetail.AITerm = mValveDetail.AITerm
                    hValveDetail.AIPortTerm = mValveDetail.AIPortTerm
                    hValveDetail.AIPinTerm = mValveDetail.AIPinTerm

                    hValveDetail.AOTerm = mValveDetail.AOTerm
                    hValveDetail.AOPortTerm = mValveDetail.AOPortTerm
                    hValveDetail.AOPinTerm = mValveDetail.AOPinTerm

                    hValveDetail.DataType = mValveDetail.DataType
                    hValveDetail.PortNo = mValveDetail.PortNo
                    hValveDetail.StatusIn = mValveDetail.StatusIn
                    hValveDetail.StatusOut = mValveDetail.StatusOut
                    hValveDetail.FlagStatusAlarm = mValveDetail.FlagStatusAlarm
                    hValveDetail.AlarmTimeup = mValveDetail.AlarmTimeup
                    hValveDetail.EccFunc = mValveDetail.EccFunc
                    hValveDetail.ShareType = mValveDetail.ShareType
                    hValveDetail.ShareChNo = mValveDetail.ShareChNo
                    hValveDetail.Remarks = mValveDetail.Remarks
                    hValveDetail.Extg_O = mValveDetail.Extg_O
                    hValveDetail.Delay_O = mValveDetail.Delay_O
                    hValveDetail.GRep1_O = mValveDetail.GRep1_O
                    hValveDetail.GRep2_O = mValveDetail.GRep2_O
                    hValveDetail.Status_O = mValveDetail.Status_O

                    hValveDetail.Sp1_O = mValveDetail.Sp1_O
                    hValveDetail.Sp2_O = mValveDetail.Sp2_O
                    hValveDetail.Hys1_O = mValveDetail.Hys1_O
                    hValveDetail.Hys2_O = mValveDetail.Hys2_O
                    hValveDetail.St_O = mValveDetail.St_O
                    hValveDetail.Var_O = mValveDetail.Var_O

                    For i As Integer = 0 To 7
                        hValveDetail.StatusDO(i) = mValveDetail.StatusDO(i)
                    Next

                    hValveDetail.ControlType = mValveDetail.ControlType
                    hValveDetail.PulseWidth = mValveDetail.PulseWidth
                    hValveDetail.CompositeIndex = mValveDetail.CompositeIndex

                    ''AI -----------------------------------------
                    hValveDetail.ValueHH = mValveDetail.ValueHH
                    hValveDetail.ValueH = mValveDetail.ValueH
                    hValveDetail.ValueL = mValveDetail.ValueL
                    hValveDetail.ValueLL = mValveDetail.ValueLL
                    hValveDetail.ValueSF = mValveDetail.ValueSF
                    hValveDetail.ExtGHH = mValveDetail.ExtGHH
                    hValveDetail.ExtGH = mValveDetail.ExtGH
                    hValveDetail.ExtGL = mValveDetail.ExtGL
                    hValveDetail.ExtGLL = mValveDetail.ExtGLL
                    hValveDetail.ExtGSF = mValveDetail.ExtGSF
                    hValveDetail.DelayHH = mValveDetail.DelayHH
                    hValveDetail.DelayH = mValveDetail.DelayH
                    hValveDetail.DelayL = mValveDetail.DelayL
                    hValveDetail.DelayLL = mValveDetail.DelayLL
                    hValveDetail.DelaySF = mValveDetail.DelaySF
                    hValveDetail.GRep1HH = mValveDetail.GRep1HH
                    hValveDetail.GRep1H = mValveDetail.GRep1H
                    hValveDetail.GRep1L = mValveDetail.GRep1L
                    hValveDetail.GRep1LL = mValveDetail.GRep1LL
                    hValveDetail.GRep2HH = mValveDetail.GRep2HH
                    hValveDetail.GRep2H = mValveDetail.GRep2H
                    hValveDetail.GRep2L = mValveDetail.GRep2L
                    hValveDetail.GRep2LL = mValveDetail.GRep2LL
                    hValveDetail.StatusHH = mValveDetail.StatusHH
                    hValveDetail.StatusH = mValveDetail.StatusH
                    hValveDetail.StatusL = mValveDetail.StatusL
                    hValveDetail.StatusLL = mValveDetail.StatusLL
                    hValveDetail.RangeFrom = mValveDetail.RangeFrom
                    hValveDetail.RangeTo = mValveDetail.RangeTo
                    hValveDetail.NormalLO = mValveDetail.NormalLO
                    hValveDetail.NormalHI = mValveDetail.NormalHI
                    hValveDetail.OffSet = mValveDetail.OffSet
                    hValveDetail.Unit = mValveDetail.Unit
                    hValveDetail.strString = mValveDetail.strString
                    hValveDetail.FlagCenterGraph = mValveDetail.FlagCenterGraph
                    ''-------------------------------------------------
                    hblnCompositeTableUse = mblnCompositeTableUse

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hValveDetail.AlmMimic = mValveDetail.AlmMimic


                    '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hValveDetail.DummyExtG = mValveDetail.DummyExtG
                    hValveDetail.DummyDelay = mValveDetail.DummyDelay
                    hValveDetail.DummyGroupRepose1 = mValveDetail.DummyGroupRepose1
                    hValveDetail.DummyGroupRepose2 = mValveDetail.DummyGroupRepose2
                    hValveDetail.DummyFuAddress = mValveDetail.DummyFuAddress
                    hValveDetail.DummyBitCount = mValveDetail.DummyBitCount
                    hValveDetail.DummyUnitName = mValveDetail.DummyUnitName
                    hValveDetail.DummyStatusName = mValveDetail.DummyStatusName

                    hValveDetail.DummyDelayHH = mValveDetail.DummyDelayHH
                    hValveDetail.DummyValueHH = mValveDetail.DummyValueHH
                    hValveDetail.DummyExtGrHH = mValveDetail.DummyExtGrHH
                    hValveDetail.DummyGRep1HH = mValveDetail.DummyGRep1HH
                    hValveDetail.DummyGRep2HH = mValveDetail.DummyGRep2HH
                    hValveDetail.DummyStaNmHH = mValveDetail.DummyStaNmHH

                    hValveDetail.DummyDelayH = mValveDetail.DummyDelayH
                    hValveDetail.DummyValueH = mValveDetail.DummyValueH
                    hValveDetail.DummyExtGrH = mValveDetail.DummyExtGrH
                    hValveDetail.DummyGRep1H = mValveDetail.DummyGRep1H
                    hValveDetail.DummyGRep2H = mValveDetail.DummyGRep2H
                    hValveDetail.DummyStaNmH = mValveDetail.DummyStaNmH

                    hValveDetail.DummyDelayL = mValveDetail.DummyDelayL
                    hValveDetail.DummyValueL = mValveDetail.DummyValueL
                    hValveDetail.DummyExtGrL = mValveDetail.DummyExtGrL
                    hValveDetail.DummyGRep1L = mValveDetail.DummyGRep1L
                    hValveDetail.DummyGRep2L = mValveDetail.DummyGRep2L
                    hValveDetail.DummyStaNmL = mValveDetail.DummyStaNmL

                    hValveDetail.DummyDelayLL = mValveDetail.DummyDelayLL
                    hValveDetail.DummyValueLL = mValveDetail.DummyValueLL
                    hValveDetail.DummyExtGrLL = mValveDetail.DummyExtGrLL
                    hValveDetail.DummyGRep1LL = mValveDetail.DummyGRep1LL
                    hValveDetail.DummyGRep2LL = mValveDetail.DummyGRep2LL
                    hValveDetail.DummyStaNmLL = mValveDetail.DummyStaNmLL

                    hValveDetail.DummyDelaySF = mValveDetail.DummyDelaySF
                    hValveDetail.DummyValueSF = mValveDetail.DummyValueSF
                    hValveDetail.DummyExtGrSF = mValveDetail.DummyExtGrSF
                    hValveDetail.DummyGRep1SF = mValveDetail.DummyGRep1SF
                    hValveDetail.DummyGRep2SF = mValveDetail.DummyGRep2SF
                    hValveDetail.DummyStaNmSF = mValveDetail.DummyStaNmSF

                    hValveDetail.DummyRangeScale = mValveDetail.DummyRangeScale
                    hValveDetail.DummyRangeNormalHi = mValveDetail.DummyRangeNormalHi
                    hValveDetail.DummyRangeNormalLo = mValveDetail.DummyRangeNormalLo

                    hValveDetail.DummyOutFuAddress = mValveDetail.DummyOutFuAddress
                    hValveDetail.DummyOutBitCount = mValveDetail.DummyOutBitCount
                    hValveDetail.DummyOutStatusType = mValveDetail.DummyOutStatusType

                    hValveDetail.DummyOutStatus1 = mValveDetail.DummyOutStatus1
                    hValveDetail.DummyOutStatus2 = mValveDetail.DummyOutStatus2
                    hValveDetail.DummyOutStatus3 = mValveDetail.DummyOutStatus3
                    hValveDetail.DummyOutStatus4 = mValveDetail.DummyOutStatus4
                    hValveDetail.DummyOutStatus5 = mValveDetail.DummyOutStatus5
                    hValveDetail.DummyOutStatus6 = mValveDetail.DummyOutStatus6
                    hValveDetail.DummyOutStatus7 = mValveDetail.DummyOutStatus7
                    hValveDetail.DummyOutStatus8 = mValveDetail.DummyOutStatus8

                    hValveDetail.DummyFaExtGr = mValveDetail.DummyFaExtGr
                    hValveDetail.DummyFaDelay = mValveDetail.DummyFaDelay
                    hValveDetail.DummyFaGrep1 = mValveDetail.DummyFaGrep1
                    hValveDetail.DummyFaGrep2 = mValveDetail.DummyFaGrep2
                    hValveDetail.DummyFaStaNm = mValveDetail.DummyFaStaNm
                    hValveDetail.DummyFaTimeV = mValveDetail.DummyFaTimeV

                    hValveDetail.DummySp1 = mValveDetail.DummySp1
                    hValveDetail.DummySp2 = mValveDetail.DummySp2
                    hValveDetail.DummyHysOpen = mValveDetail.DummyHysOpen
                    hValveDetail.DummyHysClose = mValveDetail.DummyHysClose
                    hValveDetail.DummySmpTime = mValveDetail.DummySmpTime
                    hValveDetail.DummyVar = mValveDetail.DummyVar

                    hValveDetail.DummyCmpStatus1Delay = mValveDetail.DummyCmpStatus1Delay
                    hValveDetail.DummyCmpStatus1ExtGr = mValveDetail.DummyCmpStatus1ExtGr
                    hValveDetail.DummyCmpStatus1GRep1 = mValveDetail.DummyCmpStatus1GRep1
                    hValveDetail.DummyCmpStatus1GRep2 = mValveDetail.DummyCmpStatus1GRep2
                    hValveDetail.DummyCmpStatus1StaNm = mValveDetail.DummyCmpStatus1StaNm

                    hValveDetail.DummyCmpStatus2Delay = mValveDetail.DummyCmpStatus2Delay
                    hValveDetail.DummyCmpStatus2ExtGr = mValveDetail.DummyCmpStatus2ExtGr
                    hValveDetail.DummyCmpStatus2GRep1 = mValveDetail.DummyCmpStatus2GRep1
                    hValveDetail.DummyCmpStatus2GRep2 = mValveDetail.DummyCmpStatus2GRep2
                    hValveDetail.DummyCmpStatus2StaNm = mValveDetail.DummyCmpStatus2StaNm

                    hValveDetail.DummyCmpStatus3Delay = mValveDetail.DummyCmpStatus3Delay
                    hValveDetail.DummyCmpStatus3ExtGr = mValveDetail.DummyCmpStatus3ExtGr
                    hValveDetail.DummyCmpStatus3GRep1 = mValveDetail.DummyCmpStatus3GRep1
                    hValveDetail.DummyCmpStatus3GRep2 = mValveDetail.DummyCmpStatus3GRep2
                    hValveDetail.DummyCmpStatus3StaNm = mValveDetail.DummyCmpStatus3StaNm

                    hValveDetail.DummyCmpStatus4Delay = mValveDetail.DummyCmpStatus4Delay
                    hValveDetail.DummyCmpStatus4ExtGr = mValveDetail.DummyCmpStatus4ExtGr
                    hValveDetail.DummyCmpStatus4GRep1 = mValveDetail.DummyCmpStatus4GRep1
                    hValveDetail.DummyCmpStatus4GRep2 = mValveDetail.DummyCmpStatus4GRep2
                    hValveDetail.DummyCmpStatus4StaNm = mValveDetail.DummyCmpStatus4StaNm

                    hValveDetail.DummyCmpStatus5Delay = mValveDetail.DummyCmpStatus5Delay
                    hValveDetail.DummyCmpStatus5ExtGr = mValveDetail.DummyCmpStatus5ExtGr
                    hValveDetail.DummyCmpStatus5GRep1 = mValveDetail.DummyCmpStatus5GRep1
                    hValveDetail.DummyCmpStatus5GRep2 = mValveDetail.DummyCmpStatus5GRep2
                    hValveDetail.DummyCmpStatus5StaNm = mValveDetail.DummyCmpStatus5StaNm

                    hValveDetail.DummyCmpStatus6Delay = mValveDetail.DummyCmpStatus6Delay
                    hValveDetail.DummyCmpStatus6ExtGr = mValveDetail.DummyCmpStatus6ExtGr
                    hValveDetail.DummyCmpStatus6GRep1 = mValveDetail.DummyCmpStatus6GRep1
                    hValveDetail.DummyCmpStatus6GRep2 = mValveDetail.DummyCmpStatus6GRep2
                    hValveDetail.DummyCmpStatus6StaNm = mValveDetail.DummyCmpStatus6StaNm

                    hValveDetail.DummyCmpStatus7Delay = mValveDetail.DummyCmpStatus7Delay
                    hValveDetail.DummyCmpStatus7ExtGr = mValveDetail.DummyCmpStatus7ExtGr
                    hValveDetail.DummyCmpStatus7GRep1 = mValveDetail.DummyCmpStatus7GRep1
                    hValveDetail.DummyCmpStatus7GRep2 = mValveDetail.DummyCmpStatus7GRep2
                    hValveDetail.DummyCmpStatus7StaNm = mValveDetail.DummyCmpStatus7StaNm

                    hValveDetail.DummyCmpStatus8Delay = mValveDetail.DummyCmpStatus8Delay
                    hValveDetail.DummyCmpStatus8ExtGr = mValveDetail.DummyCmpStatus8ExtGr
                    hValveDetail.DummyCmpStatus8GRep1 = mValveDetail.DummyCmpStatus8GRep1
                    hValveDetail.DummyCmpStatus8GRep2 = mValveDetail.DummyCmpStatus8GRep2
                    hValveDetail.DummyCmpStatus8StaNm = mValveDetail.DummyCmpStatus8StaNm

                    hValveDetail.DummyCmpStatus9Delay = mValveDetail.DummyCmpStatus9Delay
                    hValveDetail.DummyCmpStatus9ExtGr = mValveDetail.DummyCmpStatus9ExtGr
                    hValveDetail.DummyCmpStatus9GRep1 = mValveDetail.DummyCmpStatus9GRep1
                    hValveDetail.DummyCmpStatus9GRep2 = mValveDetail.DummyCmpStatus9GRep2
                    hValveDetail.DummyCmpStatus9StaNm = mValveDetail.DummyCmpStatus9StaNm
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
    Private Sub frmChListValve_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            Dim strFuno As String = ""
            Dim intValue As Integer
            Dim DecPoint As Boolean

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeValve)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)
            Call gSetComboBox(cmbControlType, gEnmComboType.ctChListChannelListOutputControlType)
            Call gSetComboBox(cmbValueSensorFailure, gEnmComboType.ctChListChannelListSF)
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
            Call gSetComboBox(cmbAlmLvl, gEnmComboType.ctChListChannelListAlmLevel)       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

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


            With mValveDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTagNo.Text = .TagNo        '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
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


                If cmbShareType.SelectedValue = 0 Then          'Nothingが選択されたとき:white
                    txtChNo.BackColor = Color.White
                ElseIf cmbShareType.SelectedValue = 1 Then      'Localが選択されたとき:gray
                    txtChNo.BackColor = Color.WhiteSmoke
                ElseIf cmbShareType.SelectedValue = 2 Then      'Remoteが選択されたとき:blue
                    txtChNo.BackColor = Color.AliceBlue
                ElseIf cmbShareType.SelectedValue = 3 Then      'Shareが選択されたとき:light green
                    txtChNo.BackColor = Color.LightGreen
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


                cmbTime.SelectedValue = IIf(.FlagMin = "", 0, .FlagMin)

                chkStatusAlarm.Checked = .FlagStatusAlarm

                If .BitCount = "0" Or .BitCount = "" Then
                    txtBitCount.Text = "" : Call txtFuAddress_Validated(New Object, New EventArgs)
                    .BitCount = ""
                Else
                    txtBitCount.Text = .BitCount.ToString
                    Call txtFuAddress_Validated(New Object, New EventArgs)
                End If

                cmbDataType.SelectedValue = .DataType

                ''DI -> DO,  AI -> DO,  AI -> AO
                If .DataType = gCstCodeChDataTypeValveDI_DO Or _
                   .DataType = gCstCodeChDataTypeValveAI_DO1 Or .DataType = gCstCodeChDataTypeValveAI_DO2 Or .DataType = gCstCodeChDataTypeValvePT_DO2 Or _
                   .DataType = gCstCodeChDataTypeValveAI_AO1 Or .DataType = gCstCodeChDataTypeValveAI_AO2 Or .DataType = gCstCodeChDataTypeValvePT_AO2 Then

                    ''Status I
                    intValue = cmbStatusIn.FindStringExact(.StatusIn)
                    If intValue >= 0 Then
                        cmbStatusIn.SelectedIndex = intValue
                    Else
                        cmbStatusIn.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                        txtStatusIn.Text = .StatusIn
                    End If

                    ''Status O
                    cmbStatusOut.SelectedValue = .StatusOut

                End If

                ''AI -> DO,  AI -> AO
                If .DataType = gCstCodeChDataTypeValveAI_DO1 Or .DataType = gCstCodeChDataTypeValveAI_DO2 Or .DataType = gCstCodeChDataTypeValvePT_DO2 Or _
                   .DataType = gCstCodeChDataTypeValveAI_AO1 Or .DataType = gCstCodeChDataTypeValveAI_AO2 Or .DataType = gCstCodeChDataTypeValvePT_AO2 Then

                    ''AI
                    ''バルブCH 固定桁数対応  2013.12.16
                    txtRangeFrom.Text = .RangeFrom

                    '上限設定値txtRangeto.textの中に小数点があるか確認
                    DecPoint = DecimalPoint(.RangeTo)

                    If DecPoint = True Then
                        txtRangeTo.Text = .RangeTo
                    Else
                        txtRangeTo.Text = Val(.RangeTo) * 10 ^ Val(.strString)

                        If txtRangeTo.Text = "0" Then
                            txtRangeTo.Text = ""
                        End If
                    End If

                    '' ノーマルレンジの固定桁数対応
                    If .NormalHI <> "" And Val(.strString) <> 0 Then
                        txtHighNormal.Text = Val(.NormalHI) * 10 ^ Val(.strString)
                    Else
                        txtHighNormal.Text = .NormalHI
                    End If

                    If .NormalLO <> "" And Val(.strString) <> 0 Then
                        txtLowNormal.Text = Val(.NormalLO) * 10 ^ Val(.strString)
                    Else
                        txtLowNormal.Text = .NormalLO
                    End If

                    '' 設定値の固定桁数対応
                    If .ValueHH <> "" And Val(.strString) <> 0 Then
                        txtValueHiHi.Text = Val(.ValueHH) * 10 ^ Val(.strString)
                    Else
                        txtValueHiHi.Text = .ValueHH
                    End If

                    If .ValueH <> "" And Val(.strString) <> 0 Then
                        txtValueHi.Text = Val(.ValueH) * 10 ^ Val(.strString)
                    Else
                        txtValueHi.Text = .ValueH
                    End If

                    If .ValueL <> "" And Val(.strString) <> 0 Then
                        txtValueLo.Text = Val(.ValueL) * 10 ^ Val(.strString)
                    Else
                        txtValueLo.Text = .ValueL
                    End If

                    If .ValueLL <> "" And Val(.strString) <> 0 Then
                        txtValueLoLo.Text = Val(.ValueLL) * 10 ^ Val(.strString)
                    Else
                        txtValueLoLo.Text = .ValueLL
                    End If


                    'txtValueHiHi.Text = .ValueHH
                    'txtValueHi.Text = .ValueH
                    'txtValueLo.Text = .ValueL
                    'txtValueLoLo.Text = .ValueLL
                    cmbValueSensorFailure.SelectedValue = .ValueSF

                    txtExtGHiHi.Text = .ExtGHH
                    txtExtGHi.Text = .ExtGH
                    txtExtGLo.Text = .ExtGL
                    txtExtGLoLo.Text = .ExtGLL
                    txtExtGSensorFailure.Text = .ExtGSF

                    txtDelayHiHi.Text = .DelayHH
                    txtDelayHi.Text = .DelayH
                    txtDelayLo.Text = .DelayL
                    txtDelayLoLo.Text = .DelayLL
                    txtDelaySensorFailure.Text = .DelaySF

                    txtGRep1HiHi.Text = .GRep1HH
                    txtGRep1Hi.Text = .GRep1H
                    txtGRep1Lo.Text = .GRep1L
                    txtGRep1LoLo.Text = .GRep1LL

                    txtGRep2HiHi.Text = .GRep2HH
                    txtGRep2Hi.Text = .GRep2H
                    txtGRep2Lo.Text = .GRep2L
                    txtGRep2LoLo.Text = .GRep2LL

                    txtStatusHiHi.Text = Trim(.StatusHH)
                    '.StatusHH = txtStatusHiHi.Text.PadRight(8)

                    txtStatusHi.Text = Trim(.StatusH)
                    '.StatusH = txtStatusHi.Text.PadRight(8)

                    txtStatusLoLo.Text = Trim(.StatusLL)
                    '.StatusLL = txtStatusLoLo.Text.PadRight(8)

                    txtStatusLo.Text = Trim(.StatusL)
                    '.StatusL = txtStatusLo.Text.PadRight(8)

                    'txtRangeFrom.Text = .RangeFrom
                    'txtRangeTo.Text = .RangeTo
                    'txtHighNormal.Text = .NormalHI
                    'txtLowNormal.Text = .NormalLO
                    txtOffset.Text = .OffSet
                    txtString.Text = .strString
                    chkCenterGraph.Checked = .FlagCenterGraph

                    'Ver2.0.4.3 unitで大文字小文字区別
                    intValue = fnBackCmb(cmbUnit, .Unit)
                    'intValue = cmbUnit.FindStringExact(.Unit)
                    If intValue >= 0 Then
                        cmbUnit.SelectedIndex = intValue
                    Else
                        cmbUnit.SelectedValue = gCstCodeChManualInputUnit  ''特殊コード（手入力）
                        txtUnit.Text = .Unit
                    End If

                End If

                Select Case .DataType

                    Case gCstCodeChDataTypeValveDI_DO       ''DI -> DO ------------------------------------

                        cmbControlType.SelectedValue = CCInt(.ControlType)
                        txtPulseWidth.Text = .PulseWidth

                        txtExtGHi.Text = .ExtGH_I
                        txtDelayHi.Text = .DelayH_I
                        txtGRep1Hi.Text = .GRep1H_I
                        txtGRep2Hi.Text = .GRep2H_I

                        'txtExtGroup.Text = ""
                        'txtDelayTimer.Text = ""
                        'txtGRep1.Text = ""
                        'txtGRep2.Text = ""

                        'txtExtGroup.Text = .ExtGH_I
                        'txtDelayTimer.Text = .DelayH_I
                        'txtGRep1.Text = .GRep1H_I
                        'txtGRep2.Text = .GRep2H_I

                        ''DO Alarm
                        txtStatusDo1.Text = .StatusDO(0)
                        txtStatusDo2.Text = .StatusDO(1)
                        txtStatusDo3.Text = .StatusDO(2)
                        txtStatusDo4.Text = .StatusDO(3)
                        txtStatusDo5.Text = .StatusDO(4)
                        txtStatusDo6.Text = .StatusDO(5)
                        txtStatusDo7.Text = .StatusDO(6)
                        txtStatusDo8.Text = .StatusDO(7)

                    Case gCstCodeChDataTypeValveAI_DO1, _
                         gCstCodeChDataTypeValveAI_DO2, _
                         gCstCodeChDataTypeValvePT_DO2      ''AI -> DO ------------------------------------

                        cmbControlType.SelectedValue = CCInt(.ControlType)
                        txtPulseWidth.Text = .PulseWidth

                        ''DO Alarm
                        txtStatusDo1.Text = .StatusDO(0)
                        txtStatusDo2.Text = .StatusDO(1)
                        txtStatusDo3.Text = .StatusDO(2)
                        txtStatusDo4.Text = .StatusDO(3)
                        txtStatusDo5.Text = .StatusDO(4)
                        txtStatusDo6.Text = .StatusDO(5)
                        txtStatusDo7.Text = .StatusDO(6)
                        txtStatusDo8.Text = .StatusDO(7)

                    Case gCstCodeChDataTypeValveAI_AO1, _
                         gCstCodeChDataTypeValveAI_AO2, _
                         gCstCodeChDataTypeValvePT_AO2      ''AI -> AO -------------------------------------

                    Case gCstCodeChDataTypeValveAO_4_20     ''Analog (AO) ----------------------------------

                        ''-----
                        txtRangeFrom.Text = .RangeFrom
                        txtRangeTo.Text = .RangeTo

                        'Ver2.0.4.3 unitで大文字小文字区別
                        intValue = fnBackCmb(cmbUnit, .Unit)
                        'intValue = cmbUnit.FindStringExact(.Unit)
                        If intValue >= 0 Then
                            cmbUnit.SelectedIndex = intValue
                        Else
                            cmbUnit.SelectedValue = gCstCodeChManualInputUnit  ''特殊コード（手入力）
                            txtUnit.Text = .Unit
                        End If
                        ''-------

                        ''Status O
                        cmbStatusOut.SelectedValue = .StatusOut

                        'intValue = cmbStatusOut.FindStringExact(.StatusOut)
                        'If intValue >= 0 Then
                        '    cmbStatusOut.SelectedIndex = intValue
                        'Else
                        '    cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                        '    txtStatusOut.Text = .StatusOut
                        'End If

                    Case gCstCodeChDataTypeValveDO          ''Digital (DO) ---------------------------------

                        cmbControlType.SelectedValue = CCInt(.ControlType)
                        txtPulseWidth.Text = .PulseWidth

                        ''Status O
                        cmbStatusOut.SelectedValue = .StatusOut

                        ''DO Alarm
                        txtStatusDo1.Text = .StatusDO(0)
                        txtStatusDo2.Text = .StatusDO(1)
                        txtStatusDo3.Text = .StatusDO(2)
                        txtStatusDo4.Text = .StatusDO(3)
                        txtStatusDo5.Text = .StatusDO(4)
                        txtStatusDo6.Text = .StatusDO(5)
                        txtStatusDo7.Text = .StatusDO(6)
                        txtStatusDo8.Text = .StatusDO(7)

                    Case gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55       ''外部機器(JACOM-22) ---------------------------
                        cmbExtDevice.SelectedValue = .PortNo

                        cmbControlType.SelectedValue = CCInt(.ControlType)
                        txtPulseWidth.Text = .PulseWidth

                        'Ver2.0.1.2 Status Outは変更不能だが格納はする
                        cmbStatusOut.SelectedValue = .StatusOut

                        ''Status O
                        'intValue = cmbStatusOut.FindStringExact(.StatusOut)
                        'If intValue >= 0 Then
                        '    cmbStatusOut.SelectedIndex = intValue
                        'Else
                        '    cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                        ''    txtStatusOut.Text = .StatusOut
                        'End If

                    Case gCstCodeChDataTypeValveExt         ''延長警報盤 (DO) --------------------------------
                        cmbExtDevice.SelectedValue = .EccFunc

                        cmbControlType.SelectedValue = CCInt(.ControlType)
                        txtPulseWidth.Text = .PulseWidth

                        ''Status O
                        cmbStatusOut.SelectedValue = .StatusOut

                End Select

                txtFuNoDi.Text = Trim(.DIStart)
                txtPortNoDi.Text = Trim(.DIPortStart)
                txtPinDi.Text = Trim(.DIPinStart)

                ''DI Start
                If .DIStart <> "" And .DIPortStart <> "" And .DIPinStart <> "" Then

                    'Call gSeparateFuAddress2(.DIStart, strFuno, intPortNo, intPin)
                    'txtFuNoDi.Text = strFuno
                    'txtPortNoDi.Text = intPortNo.ToString
                    'txtPinDi.Text = intPin.ToString("00")

                    If .BitCount <> "" Then
                        If .BitCount >= 1 Then lblDi1.Text = Trim(.DIStart) & Trim(.DIPortStart) & (0 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 2 Then lblDi2.Text = Trim(.DIStart) & Trim(.DIPortStart) & (1 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 3 Then lblDi3.Text = Trim(.DIStart) & Trim(.DIPortStart) & (2 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 4 Then lblDi4.Text = Trim(.DIStart) & Trim(.DIPortStart) & (3 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 5 Then lblDi5.Text = Trim(.DIStart) & Trim(.DIPortStart) & (4 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 6 Then lblDi6.Text = Trim(.DIStart) & Trim(.DIPortStart) & (5 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 7 Then lblDi7.Text = Trim(.DIStart) & Trim(.DIPortStart) & (6 + Trim(.DIPinStart)).ToString("00")
                        If .BitCount >= 8 Then lblDi8.Text = Trim(.DIStart) & Trim(.DIPortStart) & (7 + Trim(.DIPinStart)).ToString("00")
                    End If
                End If

                txtFuNoDo.Text = Trim(.DOStart)
                txtPortNoDo.Text = Trim(.DOPortStart)
                txtPinDo.Text = Trim(.DOPinStart)

                ''DO Start
                If .DOStart <> "" And .DOPortStart <> "" And .DOPinStart <> "" Then

                    'Call gSeparateFuAddress2(.DOStart, strFuno, intPortNo, intPin)
                    'txtFuNoDo.Text = strFuno
                    'txtPortNoDo.Text = intPortNo.ToString
                    'txtPinDo.Text = intPin.ToString("00")

                    If .BitCount <> "" Then
                        If .BitCount >= 1 Then lblDo1.Text = Trim(.DOStart) & Trim(.DOPortStart) & (0 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 2 Then lblDo2.Text = Trim(.DOStart) & Trim(.DOPortStart) & (1 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 3 Then lblDo3.Text = Trim(.DOStart) & Trim(.DOPortStart) & (2 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 4 Then lblDo4.Text = Trim(.DOStart) & Trim(.DOPortStart) & (3 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 5 Then lblDo5.Text = Trim(.DOStart) & Trim(.DOPortStart) & (4 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 6 Then lblDo6.Text = Trim(.DOStart) & Trim(.DOPortStart) & (5 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 7 Then lblDo7.Text = Trim(.DOStart) & Trim(.DOPortStart) & (6 + Trim(.DOPinStart)).ToString("00")
                        If .BitCount >= 8 Then lblDo8.Text = Trim(.DOStart) & Trim(.DOPortStart) & (7 + Trim(.DOPinStart)).ToString("00")
                    End If
                End If

                ''AI
                'If .AITerm <> "" Then

                txtFuNoAi.Text = Trim(.AITerm)
                txtPortNoAi.Text = Trim(.AIPortTerm)
                txtPinAi.Text = Trim(.AIPinTerm)

                'Call gSeparateFuAddress2(.AITerm, strFuno, intPortNo, intPin)
                'txtFuNoAi.Text = strFuno
                'txtPortNoAi.Text = intPortNo.ToString
                'txtPinAi.Text = intPin.ToString("00")
                'End If

                ''AO
                'If .AOTerm <> "" Then

                txtFuNoAo.Text = Trim(.AOTerm)
                txtPortNoAo.Text = Trim(.AOPortTerm)
                txtPinAo.Text = Trim(.AOPinTerm)

                'Call gSeparateFuAddress2(.AOTerm, strFuno, intPortNo, intPin)
                'txtFuNoAo.Text = strFuno
                'txtPortNoAo.Text = intPortNo.ToString
                'txtPinAo.Text = intPin.ToString("00")
                'End If

                '2015/4/23 T.Ueki
                txtAlarmTimeup.Text = mValveDetail.AlarmTimeup
                'txtAlarmTimeup.Text = Str(Val(mValveDetail.AlarmTimeup) / 10)

                ''Feedback Alarm
                txtExtGFa.Text = .Extg_O
                txtDelayFa.Text = .Delay_O
                txtGRep1Fa.Text = .GRep1_O
                txtGRep2Fa.Text = .GRep2_O
                txtStatusFa.Text = .Status_O
                txtSp1.Text = .Sp1_O
                txtSp2.Text = .Sp2_O
                txtHys1.Text = .Hys1_O
                txtHys2.Text = .Hys2_O
                txtSt.Text = .St_O
                txtVar.Text = .Var_O

                ''コンポジット設定テーブルインデックス
                txtCompositeIndex.Text = IIf(.CompositeIndex = 0, "", .CompositeIndex)
                txtCompositeIndex.ReadOnly = True
                txtCompositeIndex.BackColor = gColorGridRowBackReadOnly

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



                '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                Call mDummyBackColorSet(.DataType)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

            mintOkFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： データタイプコンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectedIndexChanged

        Try

            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeValveDI_DO
                    ''I/O : DI -> DO ----------------------------------------------------------------------

                    Call gSetComboBox(cmbStatusIn, gEnmComboType.ctChListChannelListStatusDigital)
                    Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusDigital)

                    cmbStatusIn.Enabled = True : cmbStatusIn.SelectedIndex = 0
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = True : lblBitCount.Enabled = True
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = False

                    lblDiStart.Enabled = True : txtFuNoDi.Enabled = True : txtPortNoDi.Enabled = True : txtPinDi.Enabled = True
                    lblDoStart.Enabled = True : txtFuNoDo.Enabled = True : txtPortNoDo.Enabled = True : txtPinDo.Enabled = True

                    lblAiTerminal.Enabled = False : txtFuNoAi.Enabled = False : txtPortNoAi.Enabled = False : txtPinAi.Enabled = False
                    txtFuNoAi.Text = "" : txtPortNoAi.Text = "" : txtPinAi.Text = ""

                    lblAoTerminal.Enabled = False : txtFuNoAo.Enabled = False : txtPortNoAo.Enabled = False : txtPinAo.Enabled = False
                    txtFuNoAo.Text = "" : txtPortNoAo.Text = "" : txtPinAo.Text = ""

                    fraComposite.Visible = True
                    fraFeAlarmInfo2.Enabled = False
                    txtSp1.Text = "" : txtSp2.Text = "" : txtHys1.Text = "" : txtHys2.Text = "" : txtSt.Text = "" : txtVar.Text = ""
                    fraInputAlrm.Visible = True : fraAiInfo.Visible = False

                    txtValueHiHi.Text = "" : txtValueHiHi.Enabled = False
                    txtExtGHiHi.Text = "" : txtExtGHiHi.Enabled = False
                    txtDelayHiHi.Text = "" : txtDelayHiHi.Enabled = False
                    txtGRep1HiHi.Text = "" : txtGRep1HiHi.Enabled = False
                    txtGRep2HiHi.Text = "" : txtGRep2HiHi.Enabled = False
                    txtStatusHiHi.Text = "" : txtStatusHiHi.Enabled = False

                    txtValueHi.Text = "" : txtValueHi.Enabled = False
                    txtStatusHi.Text = "" : txtStatusHi.Enabled = False

                    txtValueLo.Text = "" : txtValueLo.Enabled = False
                    txtExtGLo.Text = "" : txtExtGLo.Enabled = False
                    txtDelayLo.Text = "" : txtDelayLo.Enabled = False
                    txtGRep1Lo.Text = "" : txtGRep1Lo.Enabled = False
                    txtGRep2Lo.Text = "" : txtGRep2Lo.Enabled = False
                    txtStatusLo.Text = "" : txtStatusLo.Enabled = False

                    txtValueLoLo.Text = "" : txtValueLoLo.Enabled = False
                    txtExtGLoLo.Text = "" : txtExtGLoLo.Enabled = False
                    txtDelayLoLo.Text = "" : txtDelayLoLo.Enabled = False
                    txtGRep1LoLo.Text = "" : txtGRep1LoLo.Enabled = False
                    txtGRep2LoLo.Text = "" : txtGRep2LoLo.Enabled = False
                    txtStatusLoLo.Text = "" : txtStatusLoLo.Enabled = False

                    cmbValueSensorFailure.Enabled = False : cmbValueSensorFailure.SelectedValue = 0
                    txtExtGSensorFailure.Text = "" : txtExtGSensorFailure.Enabled = False
                    txtDelaySensorFailure.Text = "" : txtDelaySensorFailure.Enabled = False

                    cmbControlType.SelectedValue = 0 : cmbControlType.Enabled = True : lblControlType.Enabled = True
                    txtPulseWidth.Enabled = False

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                    ''I/O : AI -> DO -------------------------------------------------------------------------

                    Call gSetComboBox(cmbStatusIn, gEnmComboType.ctChListChannelListStatusAnalog)
                    Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusDigital)

                    cmbStatusIn.Enabled = True : cmbStatusIn.SelectedIndex = 0
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = True : lblBitCount.Enabled = True
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = False

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = True : txtFuNoDo.Enabled = True : txtPortNoDo.Enabled = True : txtPinDo.Enabled = True

                    lblAiTerminal.Enabled = True : txtFuNoAi.Enabled = True : txtPortNoAi.Enabled = True : txtPinAi.Enabled = True

                    lblAoTerminal.Enabled = False : txtFuNoAo.Enabled = False : txtPortNoAo.Enabled = False : txtPinAo.Enabled = False
                    txtFuNoAo.Text = "" : txtPortNoAo.Text = "" : txtPinAo.Text = ""

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = True
                    txtVar.Enabled = False : txtVar.Text = "" : lblVar.Enabled = False
                    fraInputAlrm.Visible = True

                    fraAiInfo.Visible = True
                    lblNormal.Enabled = True : txtLowNormal.Enabled = True : txtHighNormal.Enabled = True : lblNormalHif.Enabled = True
                    lblOffset.Enabled = True : txtOffset.Enabled = True
                    txtString.Enabled = True : lblString.Enabled = True
                    chkCenterGraph.Enabled = True

                    txtValueHiHi.Enabled = True
                    txtExtGHiHi.Enabled = True
                    txtDelayHiHi.Enabled = True
                    txtGRep1HiHi.Enabled = True
                    txtGRep2HiHi.Enabled = True
                    txtStatusHiHi.Enabled = True

                    txtValueHi.Enabled = True
                    txtStatusHi.Enabled = True

                    txtValueLo.Enabled = True
                    txtExtGLo.Enabled = True
                    txtDelayLo.Enabled = True
                    txtGRep1Lo.Enabled = True
                    txtGRep2Lo.Enabled = True
                    txtStatusLo.Enabled = True

                    txtValueLoLo.Enabled = True
                    txtExtGLoLo.Enabled = True
                    txtDelayLoLo.Enabled = True
                    txtGRep1LoLo.Enabled = True
                    txtGRep2LoLo.Enabled = True
                    txtStatusLoLo.Enabled = True

                    cmbValueSensorFailure.Enabled = True
                    txtExtGSensorFailure.Enabled = True
                    txtDelaySensorFailure.Enabled = True

                    cmbControlType.SelectedValue = 0 : cmbControlType.Enabled = True : lblControlType.Enabled = True
                    '' AI/DOは出力制御種別に関わらずパルス幅を設定可能とする    ver.1.4.4 2012.05.07
                    'txtPulseWidth.Enabled = False
                    txtPulseWidth.Enabled = False : lblPulseWidth.Enabled = False

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                    ''I/O : AI -> AO -------------------------------------------------------------------------

                    Call gSetComboBox(cmbStatusIn, gEnmComboType.ctChListChannelListStatusAnalog)
                    'Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusAnalog)
                    cmbStatusIn.Enabled = True : cmbStatusIn.SelectedIndex = 0
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedIndex = 0
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = False : txtBitCount.Text = "" : lblBitCount.Enabled = False
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = False

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = False : txtFuNoDo.Enabled = False : txtPortNoDo.Enabled = False : txtPinDo.Enabled = False
                    txtFuNoDo.Text = "" : txtPortNoDo.Text = "" : txtPinDo.Text = ""

                    txtStatusDo1.Enabled = False : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                    txtStatusDo1.Text = "" : txtStatusDo2.Text = "" : txtStatusDo3.Text = "" : txtStatusDo4.Text = "" : txtStatusDo5.Text = ""
                    '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    txtStatusDo6.Enabled = False : txtStatusDo7.Enabled = False : txtStatusDo8.Enabled = False
                    txtStatusDo6.Text = "" : txtStatusDo7.Text = "" : txtStatusDo8.Text = ""
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    lblAiTerminal.Enabled = True : txtFuNoAi.Enabled = True : txtPortNoAi.Enabled = True : txtPinAi.Enabled = True

                    lblAoTerminal.Enabled = True : txtFuNoAo.Enabled = True : txtPortNoAo.Enabled = True : txtPinAo.Enabled = True

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = True
                    txtVar.Enabled = True : lblVar.Enabled = True
                    fraInputAlrm.Visible = True

                    fraAiInfo.Visible = True
                    lblNormal.Enabled = True : txtLowNormal.Enabled = True : txtHighNormal.Enabled = True : lblNormalHif.Enabled = True
                    lblOffset.Enabled = True : txtOffset.Enabled = True
                    txtString.Enabled = True : lblString.Enabled = True
                    chkCenterGraph.Enabled = True

                    txtValueHiHi.Enabled = True
                    txtExtGHiHi.Enabled = True
                    txtDelayHiHi.Enabled = True
                    txtGRep1HiHi.Enabled = True
                    txtGRep2HiHi.Enabled = True
                    txtStatusHiHi.Enabled = True

                    txtValueHi.Enabled = True
                    txtStatusHi.Enabled = True

                    txtValueLo.Enabled = True
                    txtExtGLo.Enabled = True
                    txtDelayLo.Enabled = True
                    txtGRep1Lo.Enabled = True
                    txtGRep2Lo.Enabled = True
                    txtStatusLo.Enabled = True

                    txtValueLoLo.Enabled = True
                    txtExtGLoLo.Enabled = True
                    txtDelayLoLo.Enabled = True
                    txtGRep1LoLo.Enabled = True
                    txtGRep2LoLo.Enabled = True
                    txtStatusLoLo.Enabled = True

                    cmbControlType.SelectedIndex = -1 : cmbControlType.Enabled = False : lblControlType.Enabled = False
                    txtPulseWidth.Enabled = False : txtPulseWidth.Text = ""
                    cmbControlType.SelectedIndex = -1

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveAO_4_20
                    '' /O : Analog 4-20 mA --------------------------------------------------------------------

                    'Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusAnalog)
                    cmbStatusIn.Enabled = False : cmbStatusIn.SelectedIndex = -1
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedIndex = 0
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = False : txtBitCount.Text = "" : lblBitCount.Enabled = False
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = False

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = False : txtFuNoDo.Enabled = False : txtPortNoDo.Enabled = False : txtPinDo.Enabled = False
                    txtFuNoDo.Text = "" : txtPortNoDo.Text = "" : txtPinDo.Text = ""

                    txtStatusDo1.Enabled = False : txtStatusDo2.Enabled = False : txtStatusDo3.Enabled = False : txtStatusDo4.Enabled = False : txtStatusDo5.Enabled = False
                    txtStatusDo1.Text = "" : txtStatusDo2.Text = "" : txtStatusDo3.Text = "" : txtStatusDo4.Text = "" : txtStatusDo5.Text = ""
                    '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    txtStatusDo6.Enabled = False : txtStatusDo7.Enabled = False : txtStatusDo8.Enabled = False
                    txtStatusDo6.Text = "" : txtStatusDo7.Text = "" : txtStatusDo8.Text = ""
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    lblAiTerminal.Enabled = False : txtFuNoAi.Enabled = False : txtPortNoAi.Enabled = False : txtPinAi.Enabled = False
                    txtFuNoAi.Text = "" : txtPortNoAi.Text = "" : txtPinAi.Text = ""

                    lblAoTerminal.Enabled = True : txtFuNoAo.Enabled = True : txtPortNoAo.Enabled = True : txtPinAo.Enabled = True

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = True
                    txtVar.Enabled = True : lblVar.Enabled = True
                    fraInputAlrm.Visible = False

                    fraAiInfo.Visible = True
                    lblNormal.Enabled = False : txtLowNormal.Enabled = False : txtHighNormal.Enabled = False : lblNormalHif.Enabled = False
                    lblOffset.Enabled = False : txtOffset.Enabled = False
                    txtString.Enabled = False : lblString.Enabled = False
                    chkCenterGraph.Enabled = False

                    cmbControlType.SelectedIndex = -1 : cmbControlType.Enabled = False : lblControlType.Enabled = False
                    txtPulseWidth.Enabled = False : txtPulseWidth.Text = ""
                    cmbControlType.SelectedIndex = -1

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveDO
                    '' /O : Digital -----------------------------------------------------------------------------

                    Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusDigital)
                    cmbStatusIn.Enabled = False : cmbStatusIn.SelectedIndex = -1
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = True : lblBitCount.Enabled = True
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = False

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = True : txtFuNoDo.Enabled = True : txtPortNoDo.Enabled = True : txtPinDo.Enabled = True

                    lblAiTerminal.Enabled = False : txtFuNoAi.Enabled = False : txtPortNoAi.Enabled = False : txtPinAi.Enabled = False
                    txtFuNoAi.Text = "" : txtPortNoAi.Text = "" : txtPinAi.Text = ""

                    lblAoTerminal.Enabled = False : txtFuNoAo.Enabled = False : txtPortNoAo.Enabled = False : txtPinAo.Enabled = False
                    txtFuNoAo.Text = "" : txtPortNoAo.Text = "" : txtPinAo.Text = ""

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = False
                    txtSp1.Text = "" : txtSp2.Text = "" : txtHys1.Text = "" : txtHys2.Text = "" : txtSt.Text = "" : txtVar.Text = ""
                    fraInputAlrm.Visible = False : fraAiInfo.Visible = False

                    cmbControlType.SelectedValue = 0 : cmbControlType.Enabled = True : lblControlType.Enabled = True
                    txtPulseWidth.Enabled = False

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55
                    '' /O : 外部機器（JACOM-22）------------------------------------------------------------------

                    'Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusDigital)
                    cmbStatusIn.Enabled = False : cmbStatusIn.SelectedIndex = -1
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedIndex = 0
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = False : txtBitCount.Text = "" : lblBitCount.Enabled = False
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = True
                    Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22DO)
                    cmbExtDevice.SelectedValue = 1

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = False : txtFuNoDo.Enabled = False : txtPortNoDo.Enabled = False : txtPinDo.Enabled = False
                    txtFuNoDo.Text = "" : txtPortNoDo.Text = "" : txtPinDo.Text = ""

                    lblAiTerminal.Enabled = False : txtFuNoAi.Enabled = False : txtPortNoAi.Enabled = False : txtPinAi.Enabled = False
                    txtFuNoAi.Text = "" : txtPortNoAi.Text = "" : txtPinAi.Text = ""

                    lblAoTerminal.Enabled = False : txtFuNoAo.Enabled = False : txtPortNoAo.Enabled = False : txtPinAo.Enabled = False
                    txtFuNoAo.Text = "" : txtPortNoAo.Text = "" : txtPinAo.Text = ""

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = False
                    txtSp1.Text = "" : txtSp2.Text = "" : txtHys1.Text = "" : txtHys2.Text = "" : txtSt.Text = "" : txtVar.Text = ""
                    fraInputAlrm.Visible = False : fraAiInfo.Visible = False

                    cmbControlType.SelectedValue = 0 : cmbControlType.Enabled = True : lblControlType.Enabled = True
                    txtPulseWidth.Enabled = False

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

                Case gCstCodeChDataTypeValveExt
                    '' /O : 外部機器（延長警報盤） ----------------------------------------------------------------

                    'Call gSetComboBox(cmbStatusOut, gEnmComboType.ctChListChannelListStatusDigital)
                    cmbStatusIn.Enabled = False : cmbStatusIn.SelectedIndex = -1
                    cmbStatusOut.Enabled = False : cmbStatusOut.SelectedIndex = 0
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""

                    txtBitCount.Enabled = False : txtBitCount.Text = "" : lblBitCount.Enabled = False
                    Call txtFuAddress_Validated(New Object, New EventArgs)

                    cmbExtDevice.Visible = True
                    Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChTerminalFunctionFuncDO)
                    cmbExtDevice.SelectedValue = 1

                    lblDiStart.Enabled = False : txtFuNoDi.Enabled = False : txtPortNoDi.Enabled = False : txtPinDi.Enabled = False
                    txtFuNoDi.Text = "" : txtPortNoDi.Text = "" : txtPinDi.Text = ""

                    lblDoStart.Enabled = True : txtFuNoDo.Enabled = True : txtPortNoDo.Enabled = True : txtPinDo.Enabled = True

                    lblAiTerminal.Enabled = False : txtFuNoAi.Enabled = False : txtPortNoAi.Enabled = False : txtPinAi.Enabled = False
                    txtFuNoAi.Text = "" : txtPortNoAi.Text = "" : txtPinAi.Text = ""

                    lblAoTerminal.Enabled = False : txtFuNoAo.Enabled = False : txtPortNoAo.Enabled = False : txtPinAo.Enabled = False
                    txtFuNoAo.Text = "" : txtPortNoAo.Text = "" : txtPinAo.Text = ""

                    fraComposite.Visible = False
                    fraFeAlarmInfo2.Enabled = False
                    fraInputAlrm.Visible = False : fraAiInfo.Visible = False

                    cmbControlType.SelectedValue = 0 : cmbControlType.Enabled = True : lblControlType.Enabled = True
                    txtPulseWidth.Enabled = False

                    'fraAlarm.Enabled = False
                    fraAlarm.Visible = False

            End Select

            '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            Call mDummyBackColorClear()
            Call mDummyBackColorSet(cmbDataType.SelectedValue)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

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

            If cmbDataType.SelectedValue = gCstCodeChDataTypeValveDI_DO Then
                ''DI
                If cmbStatusIn.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                    txtStatusIn.Visible = True  ''手入力
                Else
                    txtStatusIn.Visible = False : txtStatusIn.Text = ""
                End If

            Else
                ''AI
                If cmbStatusIn.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                    txtStatusHiHi.Enabled = True
                    txtStatusHi.Enabled = True
                    txtStatusLo.Enabled = True
                    txtStatusLoLo.Enabled = True
                    txtStatusSF.Enabled = False
                Else
                    txtStatusHiHi.Enabled = False : txtStatusHiHi.Text = ""
                    txtStatusHi.Enabled = False : txtStatusHi.Text = ""
                    txtStatusLo.Enabled = False : txtStatusLo.Text = ""
                    txtStatusLoLo.Enabled = False : txtStatusLoLo.Text = ""
                    txtStatusSF.Enabled = False : txtStatusSF.Text = ""
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatusOut_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatusOut.SelectedIndexChanged

        Try
            ''AO のみ手入力あり
            If cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO1 Or _
               cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO2 Or _
               cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_AO2 Or _
               cmbDataType.SelectedValue = gCstCodeChDataTypeValveAO_4_20 Then

                If cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                    txtStatusOut.Visible = True ''手入力
                Else
                    txtStatusOut.Visible = False : txtStatusOut.Text = ""
                End If

            End If

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
            '' AI/DOは出力制御種別に関わらずパルス幅を設定可能とする    ver.1.4.4 2012.05.07
            'If cmbDataType.SelectedValue <> gCstCodeChDataTypeValveAI_DO1 And
            '   cmbDataType.SelectedValue <> gCstCodeChDataTypeValveAI_DO2 And
            '   cmbDataType.SelectedValue <> gCstCodeChDataTypeValvePT_DO2 Then

            If cmbControlType.SelectedValue = 0 Or cmbControlType.SelectedValue = 2 Then
                ''連続出力(OFF)/(KEEP)
                txtPulseWidth.Enabled = False : lblPulseWidth.Enabled = False
                txtPulseWidth.Text = ""

            ElseIf cmbControlType.SelectedValue = 1 Then
                ''パルス出力(Pulse)
                txtPulseWidth.Enabled = True : lblPulseWidth.Enabled = True
                If txtPulseWidth.Text = "" Then txtPulseWidth.Text = "1"
            End If
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 単位コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged

        Try

            If cmbUnit.SelectedValue = gCstCodeChManualInputUnit.ToString Then
                txtUnit.Visible = True
            Else
                txtUnit.Visible = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： SELECTボタン　クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： コンポジットテーブル編集画面へ遷移する
    '----------------------------------------------------------------------------
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        Try

            If frmChCompositeList.gShow2(True, _
                                    Val(txtChNo.Text), _
                                    mValveDetail.CompositeIndex, _
                                    Me, _
                                    gEnmCompositeEditType.cetValve) = 1 Then

                ''選択テーブル番号SET
                txtCompositeIndex.Text = mValveDetail.CompositeIndex.ToString

                ' ''構造体のコピー
                'Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

                ' ''再表示
                'Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(mCompositeDetail.CompositeIndex - 1), _
                '                             grdBitStatusMap, _
                '                             grdAnyMap, _
                '                             txtFilterCoeficient)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Editボタン　クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： コンポジットテーブル編集画面へ遷移する
    '----------------------------------------------------------------------------
    Private Sub cmdCompositeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompositeEdit.Click

        Try

            'Dim intIndex As Integer = 0

            If frmChCompositeList.gShowEdit(True, _
                                            Val(txtChNo.Text), _
                                            mValveDetail.CompositeIndex, _
                                            mValveDetail, _
                                            mblnCompositeTableUse, _
                                            Me, _
                                            gEnmCompositeEditType.cetValve) = 1 Then

                ''選択テーブル番号SET
                txtCompositeIndex.Text = mValveDetail.CompositeIndex.ToString

                ''テーブル使用フラグTrue
                mblnCompositeTableUse(mValveDetail.CompositeIndex - 1) = True

                ' ''構造体のコピー
                'Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

                ' ''再表示
                'intIndex = mValveDetail.CompositeIndex
                'intIndex = IIf(intIndex = 0, 0, intIndex - 1)

                'Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(intIndex), _
                '                     grdBitStatusMap, _
                '                     grdAnyMap, _
                '                     txtFilterCoeficient)

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
    Private Sub frmChListValve_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            Dim strValue As String = CType(sender, System.Windows.Forms.TextBox).Text
            Dim strName As String = CType(sender, System.Windows.Forms.TextBox).Name

            If strValue <> "" Then

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
    ' 機能説明  ： Terminal No をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFuAddress_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                Handles txtBitCount.Validated, _
                                                        txtFuNoDi.Validated, txtPortNoDi.Validated, txtPinDi.Validated, _
                                                        txtFuNoDo.Validated, txtPortNoDo.Validated, txtPinDo.Validated


        Try

            Dim intBitCnt As Integer = Val(txtBitCount.Text)

            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            Select Case intBitCnt
                Case 8
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = True
                    txtStatusDo5.Enabled = True
                    txtStatusDo6.Enabled = True
                    txtStatusDo7.Enabled = True
                    txtStatusDo8.Enabled = True
                Case 7
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = True
                    txtStatusDo5.Enabled = True
                    txtStatusDo6.Enabled = True
                    txtStatusDo7.Enabled = True
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 6
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = True
                    txtStatusDo5.Enabled = True
                    txtStatusDo6.Enabled = True
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 5
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = True
                    txtStatusDo5.Enabled = True
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 4
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = True
                    txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 3
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = True
                    txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
                    txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 2
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = True
                    txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
                    txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
                    txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 1
                    txtStatusDo1.Enabled = True
                    txtStatusDo2.Enabled = False : txtStatusDo2.Text = ""
                    txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
                    txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
                    txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
                Case 0
                    txtStatusDo1.Enabled = False : txtStatusDo1.Text = ""
                    txtStatusDo2.Enabled = False : txtStatusDo2.Text = ""
                    txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
                    txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
                    txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
                    txtStatusDo6.Enabled = False : txtStatusDo6.Text = ""
                    txtStatusDo7.Enabled = False : txtStatusDo7.Text = ""
                    txtStatusDo8.Enabled = False : txtStatusDo8.Text = ""
            End Select
            '-------------------------------------------------------------------------------------------------
            'Select Case intBitCnt
            '    Case 5
            '        txtStatusDo1.Enabled = True
            '        txtStatusDo2.Enabled = True
            '        txtStatusDo3.Enabled = True
            '        txtStatusDo4.Enabled = True
            '        txtStatusDo5.Enabled = True
            '    Case 4
            '        txtStatusDo1.Enabled = True
            '        txtStatusDo2.Enabled = True
            '        txtStatusDo3.Enabled = True
            '        txtStatusDo4.Enabled = True
            '        txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
            '    Case 3
            '        txtStatusDo1.Enabled = True
            '        txtStatusDo2.Enabled = True
            '        txtStatusDo3.Enabled = True
            '        txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
            '        txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
            '    Case 2
            '        txtStatusDo1.Enabled = True
            '        txtStatusDo2.Enabled = True
            '        txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
            '        txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
            '        txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
            '    Case 1
            '        txtStatusDo1.Enabled = True
            '        txtStatusDo2.Enabled = False : txtStatusDo2.Text = ""
            '        txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
            '        txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
            '        txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
            '    Case 0
            '        txtStatusDo1.Enabled = False : txtStatusDo1.Text = ""
            '        txtStatusDo2.Enabled = False : txtStatusDo2.Text = ""
            '        txtStatusDo3.Enabled = False : txtStatusDo3.Text = ""
            '        txtStatusDo4.Enabled = False : txtStatusDo4.Text = ""
            '        txtStatusDo5.Enabled = False : txtStatusDo5.Text = ""
            'End Select
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            lblDi1.Text = ""
            lblDi2.Text = ""
            lblDi3.Text = ""
            lblDi4.Text = ""
            lblDi5.Text = ""
            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            lblDi6.Text = ""
            lblDi7.Text = ""
            lblDi8.Text = ""
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            If txtFuNoDi.Text <> "" And txtPortNoDi.Text <> "" And txtPinDi.Text <> "" Then

                txtPinDi.Text = CInt(txtPinDi.Text).ToString("00")

                If intBitCnt >= 1 Then lblDi1.Text = txtFuNoDi.Text & txtPortNoDi.Text & CInt(txtPinDi.Text).ToString("00")

                If intBitCnt >= 2 Then
                    If 1 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi2.Text = txtFuNoDi.Text & txtPortNoDi.Text & (1 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 3 Then
                    If 2 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi3.Text = txtFuNoDi.Text & txtPortNoDi.Text & (2 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 4 Then
                    If 3 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi4.Text = txtFuNoDi.Text & txtPortNoDi.Text & (3 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 5 Then
                    If 4 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi5.Text = txtFuNoDi.Text & txtPortNoDi.Text & (4 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If intBitCnt >= 6 Then
                    If 5 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi6.Text = txtFuNoDi.Text & txtPortNoDi.Text & (5 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 7 Then
                    If 6 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi7.Text = txtFuNoDi.Text & txtPortNoDi.Text & (6 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 8 Then
                    If 7 + CInt(txtPinDi.Text) <= 64 Then
                        lblDi8.Text = txtFuNoDi.Text & txtPortNoDi.Text & (7 + CInt(txtPinDi.Text)).ToString("00")
                    End If
                End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End If

            lblDo1.Text = ""
            lblDo2.Text = ""
            lblDo3.Text = ""
            lblDo4.Text = ""
            lblDo5.Text = ""
            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            lblDo6.Text = ""
            lblDo7.Text = ""
            lblDo8.Text = ""
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            If txtFuNoDo.Text <> "" And txtPortNoDo.Text <> "" And txtPinDo.Text <> "" Then

                txtPinDo.Text = CInt(txtPinDo.Text).ToString("00")

                If intBitCnt >= 1 Then lblDo1.Text = txtFuNoDo.Text & txtPortNoDo.Text & CInt(txtPinDo.Text).ToString("00")

                If intBitCnt >= 2 Then
                    If 1 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo2.Text = txtFuNoDo.Text & txtPortNoDo.Text & (1 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 3 Then
                    If 2 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo3.Text = txtFuNoDo.Text & txtPortNoDo.Text & (2 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 4 Then
                    If 3 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo4.Text = txtFuNoDo.Text & txtPortNoDo.Text & (3 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 5 Then
                    If 4 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo5.Text = txtFuNoDo.Text & txtPortNoDo.Text & (4 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If intBitCnt >= 6 Then
                    If 5 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo6.Text = txtFuNoDo.Text & txtPortNoDo.Text & (5 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 7 Then
                    If 6 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo7.Text = txtFuNoDo.Text & txtPortNoDo.Text & (6 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 8 Then
                    If 7 + CInt(txtPinDo.Text) <= 64 Then
                        lblDo8.Text = txtFuNoDo.Text & txtPortNoDo.Text & (7 + CInt(txtPinDo.Text)).ToString("00")
                    End If
                End If

                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPinAi_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPinAi.Validated
        If IsNumeric(txtPinAi.Text) Then
            txtPinAi.Text = CInt(txtPinAi.Text).ToString("00")
        End If
    End Sub

    Private Sub txtPinAo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPinAo.Validated
        If IsNumeric(txtPinAo.Text) Then
            txtPinAo.Text = CInt(txtPinAo.Text).ToString("00")
        End If
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： EXT.Gに値が設定された場合にStatus Alarmにチェックを入れる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtExtGroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExtGroup.Validated

        ''バルブCHはデータ種別がDI/DOのみ　ver.1.4.0 2011.09.22
        If cmbDataType.SelectedValue = gCstCodeChDataTypeValveDI_DO Then
            '▼▼▼ 20110705 Ext.Gを無しにした場合は自動でStatusAlarmのチェックを外す ▼▼▼▼▼▼▼▼▼
            If txtExtGroup.Text <> "" And Val(txtExtGroup.Text) <> 0 Then
                chkStatusAlarm.Checked = True
            Else
                chkStatusAlarm.Checked = False
            End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                            Handles txtChNo.KeyPress, txtShareChid.KeyPress

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

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt2Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtExtGroup.KeyPress, txtGRep1.KeyPress, txtGRep2.KeyPress, _
                            txtExtGFa.KeyPress, txtGRep1Fa.KeyPress, txtGRep2Fa.KeyPress, _
                            txtExtGHiHi.KeyPress, txtExtGHi.KeyPress, txtExtGLo.KeyPress, txtExtGLoLo.KeyPress, txtExtGSensorFailure.KeyPress, _
                            txtGRep1HiHi.KeyPress, txtGRep1Hi.KeyPress, txtGRep1Lo.KeyPress, txtGRep1LoLo.KeyPress, _
                            txtGRep2HiHi.KeyPress, txtGRep2Hi.KeyPress, txtGRep2Lo.KeyPress, txtGRep2LoLo.KeyPress, _
                            txtPinDi.KeyPress, txtPinDo.KeyPress, txtPinAi.KeyPress, txtPinAo.KeyPress


        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt3Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtPulseWidth.KeyPress, txtSt.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt4Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtAlarmTimeup.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt8Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                    txtStatusDo1.KeyPress, txtStatusDo2.KeyPress, txtStatusDo3.KeyPress, txtStatusDo4.KeyPress, txtStatusDo5.KeyPress, _
                    txtStatusDo6.KeyPress, txtStatusDo7.KeyPress, txtStatusDo8.KeyPress, txtStatusFa.KeyPress, _
                    txtStatusHiHi.KeyPress, txtStatusHi.KeyPress, txtStatusLoLo.KeyPress, txtStatusLo.KeyPress, txtStatusSF.KeyPress, _
                    txtStatusOut.KeyPress, txtUnit.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub txt9Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtRangeFrom.KeyPress, txtRangeTo.KeyPress, _
                            txtHighNormal.KeyPress, txtLowNormal.KeyPress, _
                            txtValueHiHi.KeyPress, txtValueHi.KeyPress, txtValueLo.KeyPress, txtValueLoLo.KeyPress


        Try
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)  ''小数点あり

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt16Byte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtStatusIn.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtOffset_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                    txtOffset.KeyPress

        Try

            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, False, False) ''小数点なし, マイナスあり

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtVar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtVar.KeyPress

        Try
            Dim p As Integer = 0

            ''0～100.0（小数点以下桁数=1)
            If Asc(e.KeyChar) = 8 Then Exit Sub ''BackSpace
            If Asc(e.KeyChar) = 13 Then Exit Sub ''Enter


            If (e.KeyChar >= "0"c And e.KeyChar <= "9"c) Then    ''数値(0～9)が入力された

                p = txtVar.Text.IndexOf(".")

                If p < 0 Then
                    ''小数点がない場合は3文字まで
                    If txtVar.Text.Length >= 3 Then e.Handled = True
                Else
                    ''少数点がある場合
                    If txtVar.SelectionStart <= p Then

                        ''カーソルが整数部
                        If txtVar.Text.Substring(0, p).Length >= 3 Then e.Handled = True

                    Else
                        ''カーソルが小数点以下
                        If txtVar.Text.Substring(p + 1).Length >= 1 Then e.Handled = True

                    End If

                End If

            ElseIf Asc(e.KeyChar) = 46 Then ''小数点が入力された

                p = txtVar.Text.IndexOf(".")

                If p >= 0 Then
                    ''再度少数点が入力
                    If Asc(e.KeyChar) = 46 Then e.Handled = True
                End If

            Else
                e.Handled = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtVar_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVar.Validated

        Try
            If IsNumeric(txtVar.Text) Then
                txtVar.Text = Double.Parse(txtVar.Text).ToString("0.0")
            Else
                txtVar.Text = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txt5Byte_keyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtSp1.KeyPress, txtSp2.KeyPress, txtHys1.KeyPress, txtHys2.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            'e.Handled = gCheckTextInput(5, sender, e.KeyChar, True, True, False, False) ''小数点なし, マイナスあり

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtString_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtString.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1,2,3,4,5,6,7,8")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelayTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                Handles txtDelayTimer.KeyPress, txtDelayFa.KeyPress, _
                        txtDelayHiHi.KeyPress, txtDelayHi.KeyPress, txtDelayLo.KeyPress, txtDelayLoLo.KeyPress, txtDelaySensorFailure.KeyPress


        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'If cmbTime.SelectedValue = 0 Then
            '    ''Sec
            '    e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'Else
            '    ''Min
            '    e.Handled = gCheckTextInput(2, sender, e.KeyChar, True, False, True)
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtBitCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBitCount.KeyPress

        Try

            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5")
            '--------------------------------------------------------------------------------------------------
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8")
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFlag1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtDmy.KeyPress, txtSC.KeyPress, txtWK.KeyPress, txtRL.KeyPress, _
                    txtAC.KeyPress, txtEP.KeyPress, txtPLC.KeyPress, txtSP.KeyPress


        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtFuNoDi.KeyPress, txtFuNoDo.KeyPress, txtFuNoAi.KeyPress, txtFuNoAo.KeyPress


        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtPortNoDi.KeyPress, txtPortNoDo.KeyPress, txtPortNoAi.KeyPress, txtPortNoAo.KeyPress


        Try
            '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9")

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
            Dim DecPoint As Boolean
            Dim txtRangeToLen As Integer
            Dim txtLen As Integer

            With mValveDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTagNo.Text)    '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If cmbShareType.Enabled = True Then
                    .ShareType = cmbShareType.SelectedValue
                    .ShareChNo = IIf(txtShareChid.Text = "", Nothing, txtShareChid.Text)
                End If

                '.ExtGH_I = ""
                '.DelayH_I = ""
                '.GRep1H_I = ""
                '.GRep2H_I = ""

                '.ExtGH_I = txtExtGHi.Text
                '.DelayH_I = txtDelayHi.Text
                '.GRep1H_I = txtGRep1Hi.Text
                '.GRep2H_I = txtGRep2Hi.Text

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

                .FlagMin = cmbTime.SelectedValue

                .BitCount = txtBitCount.Text
                .DataType = cmbDataType.SelectedValue
                .FlagStatusAlarm = chkStatusAlarm.Checked

                Select Case cmbDataType.SelectedValue

                    Case gCstCodeChDataTypeValveDI_DO, _
                         gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2, _
                         gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2

                        If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                            .StatusIn = cmbStatusIn.Text
                        Else
                            .StatusIn = txtStatusIn.Text
                        End If

                        .StatusOut = cmbStatusOut.SelectedValue

                        'If cmbStatusOut.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                        '    '.StatusOut = cmbStatusOut.Text
                        '    .StatusOut = gCstCodeChManualInputStatus.ToString
                        'Else
                        '    .StatusOut = cmbStatusOut.SelectedIndex.ToString

                        '    'If cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO1 Or _
                        '    '   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO2 Or _
                        '    '   cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_AO2 Then
                        '    '    .StatusOut = txtStatusOut.Text
                        '    'End If
                        'End If

                        .StatusDO(0) = txtStatusDo1.Text
                        .StatusDO(1) = txtStatusDo2.Text
                        .StatusDO(2) = txtStatusDo3.Text
                        .StatusDO(3) = txtStatusDo4.Text
                        .StatusDO(4) = txtStatusDo5.Text
                        '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        .StatusDO(5) = txtStatusDo6.Text
                        .StatusDO(6) = txtStatusDo7.Text
                        .StatusDO(7) = txtStatusDo8.Text
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    Case gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveExt

                        .StatusIn = ""
                        .StatusOut = cmbStatusOut.SelectedValue


                        'If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                        '    .StatusOut = cmbStatusOut.Text
                        'Else
                        '    '.StatusOut = txtStatusOut.Text
                        '    .StatusOut = ""
                        'End If

                        .StatusDO(0) = txtStatusDo1.Text
                        .StatusDO(1) = txtStatusDo2.Text
                        .StatusDO(2) = txtStatusDo3.Text
                        .StatusDO(3) = txtStatusDo4.Text
                        .StatusDO(4) = txtStatusDo5.Text
                        '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        .StatusDO(5) = txtStatusDo6.Text
                        .StatusDO(6) = txtStatusDo7.Text
                        .StatusDO(7) = txtStatusDo8.Text
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    Case Else

                        .StatusIn = ""
                        .StatusOut = cmbStatusOut.SelectedValue

                        'If cmbDataType.SelectedValue <> gCstCodeChDataTypeValveJacom Then
                        '    If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                        '        .StatusOut = cmbStatusOut.Text
                        '    Else

                        '        .StatusOut = ""
                        '        If cmbDataType.SelectedValue = gCstCodeChDataTypeValveAO_4_20 Then
                        '            .StatusOut = txtStatusOut.Text
                        '        End If
                        '    End If
                        'End If

                        .StatusDO(0) = ""
                        .StatusDO(1) = ""
                        .StatusDO(2) = ""
                        .StatusDO(3) = ""
                        .StatusDO(4) = ""
                        '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        .StatusDO(5) = ""
                        .StatusDO(6) = ""
                        .StatusDO(7) = ""
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                End Select

                If cmbDataType.SelectedValue = gCstCodeChDataTypeValveExt Then
                    ''延長警報
                    .EccFunc = cmbExtDevice.SelectedValue
                End If

                .DIStart = txtFuNoDi.Text
                .DIPortStart = txtPortNoDi.Text
                .DIPinStart = txtPinDi.Text

                .DOStart = txtFuNoDo.Text
                .DOPortStart = txtPortNoDo.Text
                .DOPinStart = txtPinDo.Text

                .AITerm = txtFuNoAi.Text
                .AIPortTerm = txtPortNoAi.Text
                .AIPinTerm = txtPinAi.Text

                .AOTerm = txtFuNoAo.Text
                .AOPortTerm = txtPortNoAo.Text
                .AOPinTerm = txtPinAo.Text

                'T.Ueki Feedback変更 2015/4/23
                .AlarmTimeup = txtAlarmTimeup.Text
                '.AlarmTimeup = Str(Val(txtAlarmTimeup.Text) * 10)

                .Extg_O = txtExtGFa.Text
                .Delay_O = txtDelayFa.Text
                .GRep1_O = txtGRep1Fa.Text
                .GRep2_O = txtGRep2Fa.Text
                .Status_O = txtStatusFa.Text
                .Sp1_O = txtSp1.Text
                .Sp2_O = txtSp2.Text
                .Hys1_O = txtHys1.Text
                .Hys2_O = txtHys2.Text
                .St_O = txtSt.Text
                .Var_O = txtVar.Text

                .ControlType = cmbControlType.SelectedValue
                .PulseWidth = txtPulseWidth.Text

                .CompositeIndex = IIf(txtCompositeIndex.Text = "", 0, Val(txtCompositeIndex.Text))

                ''コンボボックスの場合、使っていなくても値が変わってしまうので注意
                If cmbDataType.SelectedValue = gCstCodeChDataTypeValveJacom Or cmbDataType.SelectedValue = gCstCodeChDataTypeValveJacom55 Then
                    .PortNo = cmbExtDevice.SelectedValue
                End If

                If cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_DO1 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_DO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_DO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO1 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_AO2 Then

                    .ValueSF = cmbValueSensorFailure.SelectedValue
                End If

                If cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_DO1 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_DO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_DO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO1 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAI_AO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValvePT_AO2 Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypeValveAO_4_20 Then

                    If cmbUnit.SelectedValue <> gCstCodeChManualInputUnit.ToString Then
                        .Unit = cmbUnit.Text
                    Else
                        .Unit = txtUnit.Text
                    End If
                End If

                ''AI -----------------------------------------
                '.ValueHH = txtValueHiHi.Text
                '.ValueH = txtValueHi.Text
                '.ValueL = txtValueLo.Text
                '.ValueLL = txtValueLoLo.Text

                 Select cmbDataType.SelectedValue

                    Case gCstCodeChDataTypeValveDI_DO
                        .ExtGH_I = txtExtGHi.Text
                        .DelayH_I = txtDelayHi.Text
                        .GRep1H_I = txtGRep1Hi.Text
                        .GRep2H_I = txtGRep2Hi.Text

                    Case Else
                        .ExtGH = txtExtGHi.Text
                        .DelayH = txtDelayHi.Text
                        .GRep1H = txtGRep1Hi.Text
                        .GRep2H = txtGRep2Hi.Text
                End Select


                .ExtGHH = txtExtGHiHi.Text
                .ExtGL = txtExtGLo.Text
                .ExtGLL = txtExtGLoLo.Text
                .ExtGSF = txtExtGSensorFailure.Text

                .DelayHH = txtDelayHiHi.Text
                .DelayL = txtDelayLo.Text
                .DelayLL = txtDelayLoLo.Text
                .DelaySF = txtDelaySensorFailure.Text

                .GRep1HH = txtGRep1HiHi.Text
                .GRep1L = txtGRep1Lo.Text
                .GRep1LL = txtGRep1LoLo.Text

                .GRep2HH = txtGRep2HiHi.Text
                .GRep2L = txtGRep2Lo.Text
                .GRep2LL = txtGRep2LoLo.Text

                If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    .StatusHH = ""
                    .StatusH = ""
                    .StatusL = ""
                    .StatusLL = ""
                Else
                    '.StatusIn = ""
                    .StatusHH = txtStatusHiHi.Text.Trim
                    .StatusH = txtStatusHi.Text.Trim
                    .StatusL = txtStatusLo.Text.Trim
                    .StatusLL = txtStatusLoLo.Text.Trim
                End If

                'バルブCH 固定桁数対応   2013.12.16
                '上限設定値txtRangeto.textの中に小数点があるか確認
                DecPoint = DecimalPoint(txtRangeTo.Text)
                txtRangeToLen = Len(txtRangeTo.Text)

                '' レンジの固定桁数対応
                If DecPoint = True Then
                    txtString.Text = "0"
                    .strString = txtString.Text
                Else
                    txtRangeToLen = Len(txtRangeTo.Text)

                    If txtRangeToLen <= Val(txtString.Text) Then
                        txtString.Text = "0"
                        .strString = txtString.Text
                    Else
                        .strString = txtString.Text
                    End If
                End If

                .RangeFrom = txtRangeFrom.Text
                .RangeTo = Mid(txtRangeTo.Text, 1, txtRangeToLen - Val(txtString.Text))

                '' ノーマルレンジの固定桁数対応
                If txtHighNormal.Text <> "" Then
                    txtLen = Len(txtHighNormal.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .NormalHI = Mid(txtHighNormal.Text, 1, txtLen)
                    Else
                        .NormalHI = txtHighNormal.Text
                    End If
                Else
                    .NormalHI = txtHighNormal.Text
                End If

                If txtLowNormal.Text <> "" Then
                    txtLen = Len(txtLowNormal.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .NormalLO = Mid(txtLowNormal.Text, 1, txtLen)
                    Else
                        .NormalLO = txtLowNormal.Text
                    End If
                Else
                    .NormalLO = txtLowNormal.Text
                End If

                '' 設定値の固定桁数対応
                If txtValueHiHi.Text <> "" Then
                    txtLen = Len(txtValueHiHi.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .ValueHH = Mid(txtValueHiHi.Text, 1, txtLen)
                    Else
                        .ValueHH = txtValueHiHi.Text
                    End If
                Else
                    .ValueHH = txtValueHiHi.Text
                End If

                If txtValueHi.Text <> "" Then
                    txtLen = Len(txtValueHi.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .ValueH = Mid(txtValueHi.Text, 1, txtLen)
                    Else
                        .ValueH = txtValueHi.Text
                    End If
                Else
                    .ValueH = txtValueHi.Text
                End If

                If txtValueLo.Text <> "" Then
                    txtLen = Len(txtValueLo.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .ValueL = Mid(txtValueLo.Text, 1, txtLen)
                    Else
                        .ValueL = txtValueLo.Text
                    End If
                Else
                    .ValueL = txtValueLo.Text
                End If

                If txtValueLoLo.Text <> "" Then
                    txtLen = Len(txtValueLoLo.Text) - Val(txtString.Text)
                    If txtLen > 0 Then
                        .ValueLL = Mid(txtValueLoLo.Text, 1, txtLen)
                    Else
                        .ValueLL = txtValueLoLo.Text
                    End If
                Else
                    .ValueLL = txtValueLoLo.Text
                End If

                '.RangeFrom = txtRangeFrom.Text
                '.RangeTo = txtRangeTo.Text
                '.NormalHI = txtHighNormal.Text
                '.NormalLO = txtLowNormal.Text
                .OffSet = txtOffset.Text
                '.strString = txtString.Text
                .FlagCenterGraph = chkCenterGraph.Checked


                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text


                ''--------------------------------------------

                '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ''DIの処理
                If .DataType = gCstCodeChDataTypeValveDI_DO Then

                    .DummyExtG = gDummyCheckControl(txtExtGroup)
                    .DummyDelay = gDummyCheckControl(txtDelayTimer)
                    .DummyGroupRepose1 = gDummyCheckControl(txtGRep1)
                    .DummyGroupRepose2 = gDummyCheckControl(txtGRep2)
                    .DummyBitCount = gDummyCheckControl(txtBitCount)
                    .DummyFuAddress = gDummyCheckControl(txtFuNoDi)
                    .DummyStatusName = gDummyCheckControl(cmbStatusIn)

                    .DummyCmpStatus1ExtGr = mValveDetail.DummyCmpStatus1ExtGr
                    .DummyCmpStatus1Delay = mValveDetail.DummyCmpStatus1Delay
                    .DummyCmpStatus1GRep1 = mValveDetail.DummyCmpStatus1GRep1
                    .DummyCmpStatus1GRep2 = mValveDetail.DummyCmpStatus1GRep2
                    .DummyCmpStatus1StaNm = mValveDetail.DummyCmpStatus1StaNm

                    .DummyCmpStatus2ExtGr = mValveDetail.DummyCmpStatus2ExtGr
                    .DummyCmpStatus2Delay = mValveDetail.DummyCmpStatus2Delay
                    .DummyCmpStatus2GRep1 = mValveDetail.DummyCmpStatus2GRep1
                    .DummyCmpStatus2GRep2 = mValveDetail.DummyCmpStatus2GRep2
                    .DummyCmpStatus2StaNm = mValveDetail.DummyCmpStatus2StaNm

                    .DummyCmpStatus3ExtGr = mValveDetail.DummyCmpStatus3ExtGr
                    .DummyCmpStatus3Delay = mValveDetail.DummyCmpStatus3Delay
                    .DummyCmpStatus3GRep1 = mValveDetail.DummyCmpStatus3GRep1
                    .DummyCmpStatus3GRep2 = mValveDetail.DummyCmpStatus3GRep2
                    .DummyCmpStatus3StaNm = mValveDetail.DummyCmpStatus3StaNm

                    .DummyCmpStatus4ExtGr = mValveDetail.DummyCmpStatus4ExtGr
                    .DummyCmpStatus4Delay = mValveDetail.DummyCmpStatus4Delay
                    .DummyCmpStatus4GRep1 = mValveDetail.DummyCmpStatus4GRep1
                    .DummyCmpStatus4GRep2 = mValveDetail.DummyCmpStatus4GRep2
                    .DummyCmpStatus4StaNm = mValveDetail.DummyCmpStatus4StaNm

                    .DummyCmpStatus5ExtGr = mValveDetail.DummyCmpStatus5ExtGr
                    .DummyCmpStatus5Delay = mValveDetail.DummyCmpStatus5Delay
                    .DummyCmpStatus5GRep1 = mValveDetail.DummyCmpStatus5GRep1
                    .DummyCmpStatus5GRep2 = mValveDetail.DummyCmpStatus5GRep2
                    .DummyCmpStatus5StaNm = mValveDetail.DummyCmpStatus5StaNm

                    .DummyCmpStatus6ExtGr = mValveDetail.DummyCmpStatus6ExtGr
                    .DummyCmpStatus6Delay = mValveDetail.DummyCmpStatus6Delay
                    .DummyCmpStatus6GRep1 = mValveDetail.DummyCmpStatus6GRep1
                    .DummyCmpStatus6GRep2 = mValveDetail.DummyCmpStatus6GRep2
                    .DummyCmpStatus6StaNm = mValveDetail.DummyCmpStatus6StaNm

                    .DummyCmpStatus7ExtGr = mValveDetail.DummyCmpStatus7ExtGr
                    .DummyCmpStatus7Delay = mValveDetail.DummyCmpStatus7Delay
                    .DummyCmpStatus7GRep1 = mValveDetail.DummyCmpStatus7GRep1
                    .DummyCmpStatus7GRep2 = mValveDetail.DummyCmpStatus7GRep2
                    .DummyCmpStatus7StaNm = mValveDetail.DummyCmpStatus7StaNm

                    .DummyCmpStatus8ExtGr = mValveDetail.DummyCmpStatus8ExtGr
                    .DummyCmpStatus8Delay = mValveDetail.DummyCmpStatus8Delay
                    .DummyCmpStatus8GRep1 = mValveDetail.DummyCmpStatus8GRep1
                    .DummyCmpStatus8GRep2 = mValveDetail.DummyCmpStatus8GRep2
                    .DummyCmpStatus8StaNm = mValveDetail.DummyCmpStatus8StaNm

                    .DummyCmpStatus9ExtGr = mValveDetail.DummyCmpStatus9ExtGr
                    .DummyCmpStatus9Delay = mValveDetail.DummyCmpStatus9Delay
                    .DummyCmpStatus9GRep1 = mValveDetail.DummyCmpStatus9GRep1
                    .DummyCmpStatus9GRep2 = mValveDetail.DummyCmpStatus9GRep2
                    .DummyCmpStatus9StaNm = mValveDetail.DummyCmpStatus9StaNm

                End If

                ''AIの処理
                If .DataType = gCstCodeChDataTypeValveAI_DO1 _
                Or .DataType = gCstCodeChDataTypeValveAI_DO2 _
                Or .DataType = gCstCodeChDataTypeValvePT_DO2 _
                Or .DataType = gCstCodeChDataTypeValveAI_AO1 _
                Or .DataType = gCstCodeChDataTypeValveAI_AO2 _
                Or .DataType = gCstCodeChDataTypeValvePT_AO2 Then

                    .DummyFuAddress = gDummyCheckControl(txtFuNoAi)
                    .DummyUnitName = gDummyCheckControl(cmbUnit)
                    .DummyStatusName = gDummyCheckControl(cmbStatusIn)

                    .DummyDelayHH = gDummyCheckControl(txtDelayHiHi)
                    .DummyValueHH = gDummyCheckControl(txtValueHiHi)
                    .DummyExtGrHH = gDummyCheckControl(txtExtGHiHi)
                    .DummyGRep1HH = gDummyCheckControl(txtGRep1HiHi)
                    .DummyGRep2HH = gDummyCheckControl(txtGRep2HiHi)
                    .DummyStaNmHH = gDummyCheckControl(txtStatusHiHi)

                    .DummyDelayH = gDummyCheckControl(txtDelayHi)
                    .DummyValueH = gDummyCheckControl(txtValueHi)
                    .DummyExtGrH = gDummyCheckControl(txtExtGHi)
                    .DummyGRep1H = gDummyCheckControl(txtGRep1Hi)
                    .DummyGRep2H = gDummyCheckControl(txtGRep2Hi)
                    .DummyStaNmH = gDummyCheckControl(txtStatusHi)

                    .DummyDelayL = gDummyCheckControl(txtDelayLo)
                    .DummyValueL = gDummyCheckControl(txtValueLo)
                    .DummyExtGrL = gDummyCheckControl(txtExtGLo)
                    .DummyGRep1L = gDummyCheckControl(txtGRep1Lo)
                    .DummyGRep2L = gDummyCheckControl(txtGRep2Lo)
                    .DummyStaNmL = gDummyCheckControl(txtStatusLo)

                    .DummyDelayLL = gDummyCheckControl(txtDelayLoLo)
                    .DummyValueLL = gDummyCheckControl(txtValueLoLo)
                    .DummyExtGrLL = gDummyCheckControl(txtExtGLoLo)
                    .DummyGRep1LL = gDummyCheckControl(txtGRep1LoLo)
                    .DummyGRep2LL = gDummyCheckControl(txtGRep2LoLo)
                    .DummyStaNmLL = gDummyCheckControl(txtStatusLoLo)

                    .DummyDelaySF = gDummyCheckControl(txtDelaySensorFailure)
                    .DummyValueSF = gDummyCheckControl(cmbValueSensorFailure)
                    .DummyExtGrSF = gDummyCheckControl(txtExtGSensorFailure)
                    .DummyGRep1SF = gDummyCheckControl(txtGRep1SensorFailure)
                    .DummyGRep2SF = gDummyCheckControl(txtGRep2SensorFailure)
                    .DummyStaNmSF = gDummyCheckControl(txtStatusSF)

                    .DummyRangeScale = gDummyCheckControl(txtRangeFrom)
                    .DummyRangeNormalHi = gDummyCheckControl(txtHighNormal)
                    .DummyRangeNormalLo = gDummyCheckControl(txtLowNormal)

                    .DummySp1 = gDummyCheckControl(txtSp1)
                    .DummySp2 = gDummyCheckControl(txtSp2)
                    .DummyHysOpen = gDummyCheckControl(txtHys1)
                    .DummyHysClose = gDummyCheckControl(txtHys2)
                    .DummySmpTime = gDummyCheckControl(txtSt)

                End If

                ''DOの処理
                If .DataType = gCstCodeChDataTypeValveDI_DO _
                Or .DataType = gCstCodeChDataTypeValveAI_DO1 _
                Or .DataType = gCstCodeChDataTypeValveAI_DO2 _
                Or .DataType = gCstCodeChDataTypeValvePT_DO2 _
                Or .DataType = gCstCodeChDataTypeValveDO Then

                    .DummyOutBitCount = gDummyCheckControl(txtBitCount)
                    .DummyOutFuAddress = gDummyCheckControl(txtFuNoDo)
                    .DummyOutStatusType = gDummyCheckControl(cmbStatusOut)

                    .DummyOutStatus1 = gDummyCheckControl(txtStatusDo1)
                    .DummyOutStatus2 = gDummyCheckControl(txtStatusDo2)
                    .DummyOutStatus3 = gDummyCheckControl(txtStatusDo3)
                    .DummyOutStatus4 = gDummyCheckControl(txtStatusDo4)
                    .DummyOutStatus5 = gDummyCheckControl(txtStatusDo5)
                    .DummyOutStatus6 = gDummyCheckControl(txtStatusDo6)
                    .DummyOutStatus7 = gDummyCheckControl(txtStatusDo7)
                    .DummyOutStatus8 = gDummyCheckControl(txtStatusDo8)

                    .DummyFaExtGr = gDummyCheckControl(txtExtGFa)
                    .DummyFaDelay = gDummyCheckControl(txtDelayFa)
                    .DummyFaGrep1 = gDummyCheckControl(txtGRep1Fa)
                    .DummyFaGrep2 = gDummyCheckControl(txtGRep2Fa)
                    .DummyFaStaNm = gDummyCheckControl(txtStatusFa)
                    .DummyFaTimeV = gDummyCheckControl(txtAlarmTimeup)

                End If

                ''AOの処理
                If .DataType = gCstCodeChDataTypeValveAI_AO1 _
                Or .DataType = gCstCodeChDataTypeValveAI_AO2 _
                Or .DataType = gCstCodeChDataTypeValvePT_AO2 _
                Or .DataType = gCstCodeChDataTypeValveAO_4_20 Then

                    .DummyOutFuAddress = gDummyCheckControl(txtFuNoAo)
                    .DummyOutStatusType = gDummyCheckControl(cmbStatusOut)
                    .DummyOutStatus1 = gDummyCheckControl(txtStatusOut)

                    .DummySp1 = gDummyCheckControl(txtSp1)
                    .DummySp2 = gDummyCheckControl(txtSp2)
                    .DummyHysOpen = gDummyCheckControl(txtHys1)
                    .DummyHysClose = gDummyCheckControl(txtHys2)
                    .DummySmpTime = gDummyCheckControl(txtSt)

                    .DummyFaExtGr = gDummyCheckControl(txtExtGFa)
                    .DummyFaDelay = gDummyCheckControl(txtDelayFa)
                    .DummyFaGrep1 = gDummyCheckControl(txtGRep1Fa)
                    .DummyFaGrep2 = gDummyCheckControl(txtGRep2Fa)
                    .DummyFaStaNm = gDummyCheckControl(txtStatusFa)
                    .DummyFaTimeV = gDummyCheckControl(txtAlarmTimeup)

                    .DummyVar = gDummyCheckControl(txtVar)

                    .DummyUnitName = gDummyCheckControl(cmbUnit)
                    .DummyRangeScale = gDummyCheckControl(txtRangeFrom)

                End If

                ''Jacomの処理
                If .DataType = gCstCodeChDataTypeValveJacom Or .DataType = gCstCodeChDataTypeValveJacom55 Then

                    .DummyFaExtGr = gDummyCheckControl(txtExtGFa)
                    .DummyFaDelay = gDummyCheckControl(txtDelayFa)
                    .DummyFaGrep1 = gDummyCheckControl(txtGRep1Fa)
                    .DummyFaGrep2 = gDummyCheckControl(txtGRep2Fa)
                    .DummyFaStaNm = gDummyCheckControl(txtStatusFa)
                    .DummyFaTimeV = gDummyCheckControl(txtAlarmTimeup)

                End If

                ''Extの処理
                If .DataType = gCstCodeChDataTypeValveExt Then

                    .DummyOutFuAddress = gDummyCheckControl(txtFuNoDo)
                    .DummyOutStatusType = gDummyCheckControl(cmbStatusOut)
                    .DummyFaExtGr = gDummyCheckControl(txtExtGFa)
                    .DummyFaDelay = gDummyCheckControl(txtDelayFa)
                    .DummyFaGrep1 = gDummyCheckControl(txtGRep1Fa)
                    .DummyFaGrep2 = gDummyCheckControl(txtGRep2Fa)
                    .DummyFaStaNm = gDummyCheckControl(txtStatusFa)
                    .DummyFaTimeV = gDummyCheckControl(txtAlarmTimeup)

                End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

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
            If Not gChkInputText(txtStatusIn, "Status I", True, True) Then Return False
            If Not gChkInputText(txtStatusOut, "Status O", True, True) Then Return False
            If Not gChkInputText(txtStatusFa, "DO Alarm[Status]", True, True) Then Return False
            If Not gChkInputText(txtUnit, "Unit", True, True) Then Return False

            If ChkTagInput(txtTagNo.Text) = False Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加

            If Not gChkInputText(txtStatusDo1, "DO Status 1", True, True) Then Return False
            If Not gChkInputText(txtStatusDo2, "DO Status 2", True, True) Then Return False
            If Not gChkInputText(txtStatusDo3, "DO Status 3", True, True) Then Return False
            If Not gChkInputText(txtStatusDo4, "DO Status 4", True, True) Then Return False
            If Not gChkInputText(txtStatusDo5, "DO Status 5", True, True) Then Return False
            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If Not gChkInputText(txtStatusDo6, "DO Status 6", True, True) Then Return False
            If Not gChkInputText(txtStatusDo7, "DO Status 7", True, True) Then Return False
            If Not gChkInputText(txtStatusDo8, "DO Status 8", True, True) Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            If Not gChkInputText(txtStatusHiHi, "Status HIHI", True, True) Then Return False
            If Not gChkInputText(txtStatusHi, "Status HI", True, True) Then Return False
            If Not gChkInputText(txtStatusLo, "Status LO", True, True) Then Return False
            If Not gChkInputText(txtStatusLoLo, "Status LOLO", True, True) Then Return False

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
            If Not gChkInputNum(txtExtGroup, 0, 24, "EXT.G", True, True) Then Return False
            If Not gChkInputNum(txtGRep1, 0, 48, "G REP1", True, True) Then Return False
            If Not gChkInputNum(txtGRep2, 0, 48, "G REP2", True, True) Then Return False
            '▼▼▼ 20110428 バルブ８端子対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'If Not gChkInputNum(txtBitCount, 1, 5, "Bit Count", True, True) Then Return False
            '-------------------------------------------------------------------------------------------------
            If Not gChkInputNum(txtBitCount, 1, 8, "Bit Count", True, True) Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            'T.Ueki 入力制限変更
            If Not gChkInputNum(txtAlarmTimeup, 0, 600, "Alarm Timeup Count", True, True) Then Return False
            'If Not gChkInputNum(txtAlarmTimeup, 0, 6000, "Alarm Timeup Count", True, True) Then Return False

            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

            If Not gChkInputNum(txtExtGFa, 0, 24, "DO Alarm[EXT.G]", True, True) Then Return False
            If Not gChkInputNum(txtGRep1Fa, 0, 48, "DO Alarm[G REP1]", True, True) Then Return False
            If Not gChkInputNum(txtGRep2Fa, 0, 48, "DO Alarm[G REP2]", True, True) Then Return False

            If Not gChkInputNum(txtSp1, 0, 65535, "Sp1", True, True) Then Return False
            If Not gChkInputNum(txtSp2, 0, 65535, "Sp2", True, True) Then Return False
            If Not gChkInputNum(txtHys1, 0, 65535, "Hys1", True, True) Then Return False
            If Not gChkInputNum(txtHys2, 0, 65535, "Hys2", True, True) Then Return False
            If Not gChkInputNum(txtSt, 1, 600, "St", True, True) Then Return False
            If Not gChkInputNum(txtVar, 0, 100, "Var", True, True) Then Return False

            'If cmbTime.SelectedValue = 0 Then
            If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
            If Not gChkInputNum(txtDelayFa, 0, 240, "DO Alarm[Delay]", True, True) Then Return False

            If Not gChkInputNum(txtDelayHiHi, 0, 240, "Delay HIHI", True, True) Then Return False
            If Not gChkInputNum(txtDelayHi, 0, 240, "Delay HI", True, True) Then Return False
            If Not gChkInputNum(txtDelayLo, 0, 240, "Delay LO", True, True) Then Return False
            If Not gChkInputNum(txtDelayLoLo, 0, 240, "Delay LOLO", True, True) Then Return False
            If Not gChkInputNum(txtDelaySensorFailure, 0, 240, "Delay SF", True, True) Then Return False
            'Else
            'If Not gChkInputNum(txtDelayTimer, 0, 4, "Delay", True, True) Then Return False
            'If Not gChkInputNum(txtDelayDo, 0, 4, "DO Alarm[Delay]", True, True) Then Return False

            'If Not gChkInputNum(txtDelayHiHi, 0, 4, "Delay HIHI", True, True) Then Return False
            'If Not gChkInputNum(txtDelayHi, 0, 4, "Delay HI", True, True) Then Return False
            'If Not gChkInputNum(txtDelayLo, 0, 4, "Delay LO", True, True) Then Return False
            'If Not gChkInputNum(txtDelayLoLo, 0, 4, "Delay LOLO", True, True) Then Return False
            'End If

            If cmbControlType.SelectedValue = 1 Then
                If Not gChkInputNum(txtPulseWidth, 1, 200, "Output pulse width", False, True) Then Return False
            End If

            If cmbDataType.SelectedValue = gCstCodeChDataTypeValveDI_DO Then
                If Not gChkInputNum(txtCompositeIndex, 1, 64, "Composite Table No", False, True) Then Return False
            End If

            If Not gChkInputNum(txtRangeFrom, -99999999, 999999999, "Range From", True, True) Then Return False
            If Not gChkInputNum(txtRangeTo, -99999999, 999999999, "Range To", True, True) Then Return False
            If Not gChkInputNum(txtHighNormal, -99999999, 999999999, "HI Normal", True, True) Then Return False
            If Not gChkInputNum(txtLowNormal, -99999999, 999999999, "LO Normal", True, True) Then Return False
            If Not gChkInputNum(txtString, 0, 8, "String", True, True) Then Return False
            If Not gChkInputNum(txtOffset, -99999999, 999999999, "Offset", True, True) Then Return False
            If Not gChkInputNum(txtValueHiHi, -99999999, 999999999, "Value HIHI", True, True) Then Return False
            If Not gChkInputNum(txtValueHi, -99999999, 999999999, "Value HI", True, True) Then Return False
            If Not gChkInputNum(txtValueLo, -99999999, 999999999, "Value LO", True, True) Then Return False
            If Not gChkInputNum(txtValueLoLo, -99999999, 999999999, "Value LOLO", True, True) Then Return False
            If Not gChkInputNum(txtExtGHiHi, 0, 24, "EXT.G HIHI", True, True) Then Return False
            If Not gChkInputNum(txtExtGHi, 0, 24, "EXT.G HI", True, True) Then Return False
            If Not gChkInputNum(txtExtGLo, 0, 24, "EXT.G LO", True, True) Then Return False
            If Not gChkInputNum(txtExtGLoLo, 0, 24, "EXT.G LOLO", True, True) Then Return False
            If Not gChkInputNum(txtExtGSensorFailure, 0, 24, "EXT.G Sensor Failure", True, True) Then Return False
            If Not gChkInputNum(txtGRep1HiHi, 0, 48, "G REP1 HIHI", True, True) Then Return False
            If Not gChkInputNum(txtGRep1Hi, 0, 48, "G REP1 HI", True, True) Then Return False
            If Not gChkInputNum(txtGRep1Lo, 0, 48, "G REP1 LO", True, True) Then Return False
            If Not gChkInputNum(txtGRep1LoLo, 0, 48, "G REP1 LOLO", True, True) Then Return False
            If Not gChkInputNum(txtGRep2HiHi, 0, 48, "G REP2 HIHI", True, True) Then Return False
            If Not gChkInputNum(txtGRep2Hi, 0, 48, "G REP2 HI", True, True) Then Return False
            If Not gChkInputNum(txtGRep2Lo, 0, 48, "G REP2 LO", True, True) Then Return False
            If Not gChkInputNum(txtGRep2LoLo, 0, 48, "G REP2 LOLO", True, True) Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If



            ''Range 
            If txtRangeFrom.Text = "" Or txtRangeTo.Text = "" Then
            ElseIf txtRangeFrom.Text <> "" Or txtRangeTo.Text <> "" Then

                ''大小チェック
                If Val(txtRangeFrom.Text) > Val(txtRangeTo.Text) Then
                    MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            Else
                MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                Return False
            End If

            ''Normal Range 
            If txtLowNormal.Text = "" Or txtHighNormal.Text = "" Then
            ElseIf txtLowNormal.Text <> "" Or txtHighNormal.Text <> "" Then

                ''大小チェック
                If Val(txtLowNormal.Text) > Val(txtHighNormal.Text) Then
                    MsgBox("Normal Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            Else
                If txtLowNormal.Text = "" Then
                    MsgBox("LO Normal is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If
                If txtHighNormal.Text = "" Then
                    MsgBox("HI Normal is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If
            End If

            ''共通FUアドレス入力チェック
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''If Not gChkInputFuAddress(txtFuNoDi, txtPortNoDi, txtPinDi, 64, True, True) Then Return False
            ''If Not gChkInputFuAddress(txtFuNoDo, txtPortNoDo, txtPinDo, 64, True, True) Then Return False
            ''If Not gChkInputFuAddress(txtFuNoAi, txtPortNoAi, txtPinAi, 16, True, True) Then Return False
            ''If Not gChkInputFuAddress(txtFuNoAo, txtPortNoAo, txtPinAo, 8, True, True) Then Return False

            ''組み合わせチェック
            If (txtFuNoDi.Text <> "" And txtFuNoAi.Text <> "") Then
                Call MessageBox.Show("Please select only one FUNo from among DI and AI. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If (txtFuNoDo.Text <> "" And txtFuNoAo.Text <> "") Then
                Call MessageBox.Show("Please select only one FUNo from among DO and AO. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            ''Bit Count と　FUアドレス
            If txtPinDi.Text <> "" Then
                If CInt(txtPinDi.Text) + Val(txtBitCount.Text) > 65 Then
                    Call MessageBox.Show("Bit Count and Pin No are illegal. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If

            If txtPinDo.Text <> "" Then
                If CInt(txtPinDo.Text) + Val(txtBitCount.Text) > 65 Then
                    Call MessageBox.Show("Bit Count and Pin No are illegal. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If

            ''Bit Count以上のコンポジットテーブル(BitMap)にチェックが入っていないか？
            If cmbDataType.SelectedValue = gCstCodeChDataTypeValveDI_DO Then
                If CCInt(txtCompositeIndex.Text) >= 1 Then
                    Dim intValue As Integer = CCInt(txtBitCount.Text)
                    If intValue < 8 Then

                        For i As Integer = 0 To 7

                            With gudt.SetChComposite.udtComposite(CCInt(txtCompositeIndex.Text) - 1).udtCompInf(i)

                                For j As Integer = intValue To 7

                                    If gBitCheck(.bytBitPattern, j) Then

                                        Call MessageBox.Show("Bit Status Map [" & i + 1 & "]  Please set only " & intValue.ToString & " bits. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Return False

                                    End If

                                Next

                            End With

                        Next

                    End If
                End If
            End If

            ''Range, Normal, Valueの桁あふれを防ぐためのチェック -------------------------------------------------
            Dim dblValue As Double
            Dim lngValue As Long
            Dim intDecimalP As Integer
            Dim intDataType As Integer = cmbDataType.SelectedValue

            intDecimalP = mGetDecimalPosition()

            ''Range from
            If txtRangeFrom.Text <> "" Then

                dblValue = Double.Parse(txtRangeFrom.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Range to
            If txtRangeTo.Text <> "" Then

                dblValue = Double.Parse(txtRangeTo.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Hi Normal
            If txtHighNormal.Text <> "" Then

                dblValue = Double.Parse(txtHighNormal.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("HI Normal is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Lo Normal
            If txtLowNormal.Text <> "" Then

                dblValue = Double.Parse(txtLowNormal.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("LO Normal is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Value HIHI
            If txtValueHiHi.Text <> "" Then

                dblValue = Double.Parse(txtValueHiHi.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Value HIHI is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Value HI
            If txtValueHi.Text <> "" Then

                dblValue = Double.Parse(txtValueHi.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Value HI is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Value LO
            If txtValueLo.Text <> "" Then

                dblValue = Double.Parse(txtValueLo.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Value LO is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Value LOLO
            If txtValueLoLo.Text <> "" Then

                dblValue = Double.Parse(txtValueLoLo.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Value LOLO is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 少数点以下桁数を獲得する
    ' 返り値    : 少数点以下桁数
    ' 引き数    : なし
    ' 機能説明  : RangeLo, RangeHi の内、最も小数点以下桁数の大きい桁を返す
    '--------------------------------------------------------------------
    Private Function mGetDecimalPosition() As Integer

        Try

            Dim p1 As Integer = 0, p2 As Integer = 0
            Dim ans As Integer = 0
            Dim strValue As String

            ''RangeLo
            strValue = txtRangeFrom.Text
            If strValue <> "" Then
                p1 = strValue.IndexOf(".")
                If p1 > 0 Then p1 = strValue.Substring(p1 + 1).Length ''P1 <-- 小数点以下桁数
            End If

            ''RangeHi
            strValue = txtRangeTo.Text
            If strValue <> "" Then
                p2 = strValue.IndexOf(".")
                If p2 > 0 Then p2 = strValue.Substring(p2 + 1).Length ''P2 <-- 小数点以下桁数
            End If

            If p1 >= p2 Then
                ans = p1
            Else
                ans = p2
            End If

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mValveInfo, _
                                         ByVal udt2 As frmChListChannelList.mValveInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            If udt1.ExtGH_I <> udt2.ExtGH_I Then Return False
            If udt1.DelayH_I <> udt2.DelayH_I Then Return False
            If udt1.GRep1H_I <> udt2.GRep1H_I Then Return False
            If udt1.GRep2H_I <> udt2.GRep2H_I Then Return False
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
            If udt1.BitCount <> udt2.BitCount Then Return False


            If udt1.DIStart <> udt2.DIStart Then Return False
            If udt1.DIPortStart <> udt2.DIPortStart Then Return False
            If udt1.DIPinStart <> udt2.DIPinStart Then Return False
            If udt1.DOStart <> udt2.DOStart Then Return False
            If udt1.DOPortStart <> udt2.DOPortStart Then Return False
            If udt1.DOPinStart <> udt2.DOPinStart Then Return False
            If udt1.AITerm <> udt2.AITerm Then Return False
            If udt1.AIPortTerm <> udt2.AIPortTerm Then Return False
            If udt1.AIPinTerm <> udt2.AIPinTerm Then Return False
            If udt1.AOTerm <> udt2.AOTerm Then Return False
            If udt1.AOPortTerm <> udt2.AOPortTerm Then Return False
            If udt1.AOPinTerm <> udt2.AOPinTerm Then Return False


            If udt1.DataType <> udt2.DataType Then Return False
            If udt1.PortNo <> udt2.PortNo Then Return False
            If udt1.StatusIn <> udt2.StatusIn Then Return False
            If udt1.StatusOut <> udt2.StatusOut Then Return False
            If udt1.FlagStatusAlarm <> udt2.FlagStatusAlarm Then Return False
            If udt1.AlarmTimeup <> udt2.AlarmTimeup Then Return False
            If udt1.EccFunc <> udt2.EccFunc Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False

            If udt1.Extg_O <> udt2.Extg_O Then Return False
            If udt1.Delay_O <> udt2.Delay_O Then Return False
            If udt1.GRep1_O <> udt2.GRep1_O Then Return False
            If udt1.GRep2_O <> udt2.GRep2_O Then Return False
            If udt1.Status_O <> udt2.Status_O Then Return False
            If udt1.Sp1_O <> udt2.Sp1_O Then Return False
            If udt1.Sp2_O <> udt2.Sp2_O Then Return False
            If udt1.Hys1_O <> udt2.Hys1_O Then Return False
            If udt1.Hys2_O <> udt2.Hys2_O Then Return False
            If udt1.St_O <> udt2.St_O Then Return False
            If udt1.Var_O <> udt2.Var_O Then Return False

            If udt1.ControlType <> udt2.ControlType Then Return False
            If udt1.PulseWidth <> udt2.PulseWidth Then Return False

            For i As Integer = 0 To UBound(udt1.StatusDO)
                If udt1.StatusDO(i) <> udt2.StatusDO(i) Then Return False
            Next i

            If udt1.CompositeIndex <> udt2.CompositeIndex Then Return False

            ''AI----------------------------------------------------------
            If udt1.ValueHH <> udt2.ValueHH Then Return False
            If udt1.ValueH <> udt2.ValueH Then Return False
            If udt1.ValueL <> udt2.ValueL Then Return False
            If udt1.ValueLL <> udt2.ValueLL Then Return False
            If udt1.ValueSF <> udt2.ValueSF Then Return False
            If udt1.ExtGHH <> udt2.ExtGHH Then Return False
            If udt1.ExtGH <> udt2.ExtGH Then Return False
            If udt1.ExtGL <> udt2.ExtGL Then Return False
            If udt1.ExtGLL <> udt2.ExtGLL Then Return False
            If udt1.ExtGSF <> udt2.ExtGSF Then Return False
            If udt1.DelayHH <> udt2.DelayHH Then Return False
            If udt1.DelayH <> udt2.DelayH Then Return False
            If udt1.DelayL <> udt2.DelayL Then Return False
            If udt1.DelayLL <> udt2.DelayLL Then Return False
            If udt1.DelaySF <> udt2.DelaySF Then Return False
            If udt1.GRep1HH <> udt2.GRep1HH Then Return False
            If udt1.GRep1H <> udt2.GRep1H Then Return False
            If udt1.GRep1L <> udt2.GRep1L Then Return False
            If udt1.GRep1LL <> udt2.GRep1LL Then Return False
            If udt1.GRep2HH <> udt2.GRep2HH Then Return False
            If udt1.GRep2H <> udt2.GRep2H Then Return False
            If udt1.GRep2L <> udt2.GRep2L Then Return False
            If udt1.GRep2LL <> udt2.GRep2LL Then Return False
            If udt1.StatusHH <> udt2.StatusHH Then Return False
            If udt1.StatusH <> udt2.StatusH Then Return False
            If udt1.StatusL <> udt2.StatusL Then Return False
            If udt1.StatusLL <> udt2.StatusLL Then Return False
            If udt1.RangeFrom <> udt2.RangeFrom Then Return False
            If udt1.RangeTo <> udt2.RangeTo Then Return False
            If udt1.NormalLO <> udt2.NormalLO Then Return False
            If udt1.NormalHI <> udt2.NormalHI Then Return False
            If udt1.OffSet <> udt2.OffSet Then Return False
            If udt1.Unit <> udt2.Unit Then Return False
            If udt1.strString <> udt2.strString Then Return False
            If udt1.FlagCenterGraph <> udt2.FlagCenterGraph Then Return False
            ''------------------------------------------------------------


            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False



            '▼▼▼ 20110614 仮設定機能対応（バルブ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False
            If udt1.DummyUnitName <> udt2.DummyUnitName Then Return False
            If udt1.DummyStatusName <> udt2.DummyStatusName Then Return False

            If udt1.DummyDelayHH <> udt2.DummyDelayHH Then Return False
            If udt1.DummyValueHH <> udt2.DummyValueHH Then Return False
            If udt1.DummyExtGrHH <> udt2.DummyExtGrHH Then Return False
            If udt1.DummyGRep1HH <> udt2.DummyGRep1HH Then Return False
            If udt1.DummyGRep2HH <> udt2.DummyGRep2HH Then Return False
            If udt1.DummyStaNmHH <> udt2.DummyStaNmHH Then Return False

            If udt1.DummyDelayH <> udt2.DummyDelayH Then Return False
            If udt1.DummyValueH <> udt2.DummyValueH Then Return False
            If udt1.DummyExtGrH <> udt2.DummyExtGrH Then Return False
            If udt1.DummyGRep1H <> udt2.DummyGRep1H Then Return False
            If udt1.DummyGRep2H <> udt2.DummyGRep2H Then Return False
            If udt1.DummyStaNmH <> udt2.DummyStaNmH Then Return False

            If udt1.DummyDelayL <> udt2.DummyDelayL Then Return False
            If udt1.DummyValueL <> udt2.DummyValueL Then Return False
            If udt1.DummyExtGrL <> udt2.DummyExtGrL Then Return False
            If udt1.DummyGRep1L <> udt2.DummyGRep1L Then Return False
            If udt1.DummyGRep2L <> udt2.DummyGRep2L Then Return False
            If udt1.DummyStaNmL <> udt2.DummyStaNmL Then Return False

            If udt1.DummyDelayLL <> udt2.DummyDelayLL Then Return False
            If udt1.DummyValueLL <> udt2.DummyValueLL Then Return False
            If udt1.DummyExtGrLL <> udt2.DummyExtGrLL Then Return False
            If udt1.DummyGRep1LL <> udt2.DummyGRep1LL Then Return False
            If udt1.DummyGRep2LL <> udt2.DummyGRep2LL Then Return False
            If udt1.DummyStaNmLL <> udt2.DummyStaNmLL Then Return False

            If udt1.DummyDelaySF <> udt2.DummyDelaySF Then Return False
            If udt1.DummyValueSF <> udt2.DummyValueSF Then Return False
            If udt1.DummyExtGrSF <> udt2.DummyExtGrSF Then Return False
            If udt1.DummyGRep1SF <> udt2.DummyGRep1SF Then Return False
            If udt1.DummyGRep2SF <> udt2.DummyGRep2SF Then Return False
            If udt1.DummyStaNmSF <> udt2.DummyStaNmSF Then Return False

            If udt1.DummyRangeScale <> udt2.DummyRangeScale Then Return False
            If udt1.DummyRangeNormalHi <> udt2.DummyRangeNormalHi Then Return False
            If udt1.DummyRangeNormalLo <> udt2.DummyRangeNormalLo Then Return False

            If udt1.DummyExtG <> udt2.DummyExtG Then Return False
            If udt1.DummyDelay <> udt2.DummyDelay Then Return False
            If udt1.DummyGroupRepose1 <> udt2.DummyGroupRepose1 Then Return False
            If udt1.DummyGroupRepose2 <> udt2.DummyGroupRepose2 Then Return False
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False
            If udt1.DummyBitCount <> udt2.DummyBitCount Then Return False
            If udt1.DummyUnitName <> udt2.DummyUnitName Then Return False
            If udt1.DummyStatusName <> udt2.DummyStatusName Then Return False

            If udt1.DummyDelayHH <> udt2.DummyDelayHH Then Return False
            If udt1.DummyValueHH <> udt2.DummyValueHH Then Return False
            If udt1.DummyExtGrHH <> udt2.DummyExtGrHH Then Return False
            If udt1.DummyGRep1HH <> udt2.DummyGRep1HH Then Return False
            If udt1.DummyGRep2HH <> udt2.DummyGRep2HH Then Return False
            If udt1.DummyStaNmHH <> udt2.DummyStaNmHH Then Return False

            If udt1.DummyDelayH <> udt2.DummyDelayH Then Return False
            If udt1.DummyValueH <> udt2.DummyValueH Then Return False
            If udt1.DummyExtGrH <> udt2.DummyExtGrH Then Return False
            If udt1.DummyGRep1H <> udt2.DummyGRep1H Then Return False
            If udt1.DummyGRep2H <> udt2.DummyGRep2H Then Return False
            If udt1.DummyStaNmH <> udt2.DummyStaNmH Then Return False

            If udt1.DummyDelayL <> udt2.DummyDelayL Then Return False
            If udt1.DummyValueL <> udt2.DummyValueL Then Return False
            If udt1.DummyExtGrL <> udt2.DummyExtGrL Then Return False
            If udt1.DummyGRep1L <> udt2.DummyGRep1L Then Return False
            If udt1.DummyGRep2L <> udt2.DummyGRep2L Then Return False
            If udt1.DummyStaNmL <> udt2.DummyStaNmL Then Return False

            If udt1.DummyDelayLL <> udt2.DummyDelayLL Then Return False
            If udt1.DummyValueLL <> udt2.DummyValueLL Then Return False
            If udt1.DummyExtGrLL <> udt2.DummyExtGrLL Then Return False
            If udt1.DummyGRep1LL <> udt2.DummyGRep1LL Then Return False
            If udt1.DummyGRep2LL <> udt2.DummyGRep2LL Then Return False
            If udt1.DummyStaNmLL <> udt2.DummyStaNmLL Then Return False

            If udt1.DummyDelaySF <> udt2.DummyDelaySF Then Return False
            If udt1.DummyValueSF <> udt2.DummyValueSF Then Return False
            If udt1.DummyExtGrSF <> udt2.DummyExtGrSF Then Return False
            If udt1.DummyGRep1SF <> udt2.DummyGRep1SF Then Return False
            If udt1.DummyGRep2SF <> udt2.DummyGRep2SF Then Return False
            If udt1.DummyStaNmSF <> udt2.DummyStaNmSF Then Return False

            If udt1.DummyRangeScale <> udt2.DummyRangeScale Then Return False
            If udt1.DummyRangeNormalHi <> udt2.DummyRangeNormalHi Then Return False
            If udt1.DummyRangeNormalLo <> udt2.DummyRangeNormalLo Then Return False

            If udt1.DummyOutFuAddress <> udt2.DummyOutFuAddress Then Return False
            If udt1.DummyOutBitCount <> udt2.DummyOutBitCount Then Return False
            If udt1.DummyOutStatusType <> udt2.DummyOutStatusType Then Return False

            If udt1.DummyOutStatus1 <> udt2.DummyOutStatus1 Then Return False
            If udt1.DummyOutStatus2 <> udt2.DummyOutStatus2 Then Return False
            If udt1.DummyOutStatus3 <> udt2.DummyOutStatus3 Then Return False
            If udt1.DummyOutStatus4 <> udt2.DummyOutStatus4 Then Return False
            If udt1.DummyOutStatus5 <> udt2.DummyOutStatus5 Then Return False
            If udt1.DummyOutStatus6 <> udt2.DummyOutStatus6 Then Return False
            If udt1.DummyOutStatus7 <> udt2.DummyOutStatus7 Then Return False
            If udt1.DummyOutStatus8 <> udt2.DummyOutStatus8 Then Return False

            If udt1.DummyFaExtGr <> udt2.DummyFaExtGr Then Return False
            If udt1.DummyFaDelay <> udt2.DummyFaDelay Then Return False
            If udt1.DummyFaGrep1 <> udt2.DummyFaGrep1 Then Return False
            If udt1.DummyFaGrep2 <> udt2.DummyFaGrep2 Then Return False
            If udt1.DummyFaStaNm <> udt2.DummyFaStaNm Then Return False
            If udt1.DummyFaTimeV <> udt2.DummyFaTimeV Then Return False

            If udt1.DummySp1 <> udt2.DummySp1 Then Return False
            If udt1.DummySp2 <> udt2.DummySp2 Then Return False
            If udt1.DummyHysOpen <> udt2.DummyHysOpen Then Return False
            If udt1.DummyHysClose <> udt2.DummyHysClose Then Return False
            If udt1.DummySmpTime <> udt2.DummySmpTime Then Return False
            If udt1.DummyVar <> udt2.DummyVar Then Return False

            If udt1.DummyCmpStatus1Delay <> udt2.DummyCmpStatus1Delay Then Return False
            If udt1.DummyCmpStatus1ExtGr <> udt2.DummyCmpStatus1ExtGr Then Return False
            If udt1.DummyCmpStatus1GRep1 <> udt2.DummyCmpStatus1GRep1 Then Return False
            If udt1.DummyCmpStatus1GRep2 <> udt2.DummyCmpStatus1GRep2 Then Return False
            If udt1.DummyCmpStatus1StaNm <> udt2.DummyCmpStatus1StaNm Then Return False

            If udt1.DummyCmpStatus2Delay <> udt2.DummyCmpStatus2Delay Then Return False
            If udt1.DummyCmpStatus2ExtGr <> udt2.DummyCmpStatus2ExtGr Then Return False
            If udt1.DummyCmpStatus2GRep1 <> udt2.DummyCmpStatus2GRep1 Then Return False
            If udt1.DummyCmpStatus2GRep2 <> udt2.DummyCmpStatus2GRep2 Then Return False
            If udt1.DummyCmpStatus2StaNm <> udt2.DummyCmpStatus2StaNm Then Return False

            If udt1.DummyCmpStatus3Delay <> udt2.DummyCmpStatus3Delay Then Return False
            If udt1.DummyCmpStatus3ExtGr <> udt2.DummyCmpStatus3ExtGr Then Return False
            If udt1.DummyCmpStatus3GRep1 <> udt2.DummyCmpStatus3GRep1 Then Return False
            If udt1.DummyCmpStatus3GRep2 <> udt2.DummyCmpStatus3GRep2 Then Return False
            If udt1.DummyCmpStatus3StaNm <> udt2.DummyCmpStatus3StaNm Then Return False

            If udt1.DummyCmpStatus4Delay <> udt2.DummyCmpStatus4Delay Then Return False
            If udt1.DummyCmpStatus4ExtGr <> udt2.DummyCmpStatus4ExtGr Then Return False
            If udt1.DummyCmpStatus4GRep1 <> udt2.DummyCmpStatus4GRep1 Then Return False
            If udt1.DummyCmpStatus4GRep2 <> udt2.DummyCmpStatus4GRep2 Then Return False
            If udt1.DummyCmpStatus4StaNm <> udt2.DummyCmpStatus4StaNm Then Return False

            If udt1.DummyCmpStatus5Delay <> udt2.DummyCmpStatus5Delay Then Return False
            If udt1.DummyCmpStatus5ExtGr <> udt2.DummyCmpStatus5ExtGr Then Return False
            If udt1.DummyCmpStatus5GRep1 <> udt2.DummyCmpStatus5GRep1 Then Return False
            If udt1.DummyCmpStatus5GRep2 <> udt2.DummyCmpStatus5GRep2 Then Return False
            If udt1.DummyCmpStatus5StaNm <> udt2.DummyCmpStatus5StaNm Then Return False

            If udt1.DummyCmpStatus6Delay <> udt2.DummyCmpStatus6Delay Then Return False
            If udt1.DummyCmpStatus6ExtGr <> udt2.DummyCmpStatus6ExtGr Then Return False
            If udt1.DummyCmpStatus6GRep1 <> udt2.DummyCmpStatus6GRep1 Then Return False
            If udt1.DummyCmpStatus6GRep2 <> udt2.DummyCmpStatus6GRep2 Then Return False
            If udt1.DummyCmpStatus6StaNm <> udt2.DummyCmpStatus6StaNm Then Return False

            If udt1.DummyCmpStatus7Delay <> udt2.DummyCmpStatus7Delay Then Return False
            If udt1.DummyCmpStatus7ExtGr <> udt2.DummyCmpStatus7ExtGr Then Return False
            If udt1.DummyCmpStatus7GRep1 <> udt2.DummyCmpStatus7GRep1 Then Return False
            If udt1.DummyCmpStatus7GRep2 <> udt2.DummyCmpStatus7GRep2 Then Return False
            If udt1.DummyCmpStatus7StaNm <> udt2.DummyCmpStatus7StaNm Then Return False

            If udt1.DummyCmpStatus8Delay <> udt2.DummyCmpStatus8Delay Then Return False
            If udt1.DummyCmpStatus8ExtGr <> udt2.DummyCmpStatus8ExtGr Then Return False
            If udt1.DummyCmpStatus8GRep1 <> udt2.DummyCmpStatus8GRep1 Then Return False
            If udt1.DummyCmpStatus8GRep2 <> udt2.DummyCmpStatus8GRep2 Then Return False
            If udt1.DummyCmpStatus8StaNm <> udt2.DummyCmpStatus8StaNm Then Return False

            If udt1.DummyCmpStatus9Delay <> udt2.DummyCmpStatus9Delay Then Return False
            If udt1.DummyCmpStatus9ExtGr <> udt2.DummyCmpStatus9ExtGr Then Return False
            If udt1.DummyCmpStatus9GRep1 <> udt2.DummyCmpStatus9GRep1 Then Return False
            If udt1.DummyCmpStatus9GRep2 <> udt2.DummyCmpStatus9GRep2 Then Return False
            If udt1.DummyCmpStatus9StaNm <> udt2.DummyCmpStatus9StaNm Then Return False
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
        txtDelayHiHi.KeyDown, txtDelayHi.KeyDown, txtDelayLo.KeyDown, txtDelayLoLo.KeyDown, txtDelaySensorFailure.KeyDown, _
        txtValueHiHi.KeyDown, txtValueHi.KeyDown, txtValueLo.KeyDown, txtValueLoLo.KeyDown, cmbValueSensorFailure.KeyDown, _
        txtExtGHiHi.KeyDown, txtExtGHi.KeyDown, txtExtGLo.KeyDown, txtExtGLoLo.KeyDown, txtExtGSensorFailure.KeyDown, _
        txtGRep1HiHi.KeyDown, txtGRep1Hi.KeyDown, txtGRep1Lo.KeyDown, txtGRep1LoLo.KeyDown, txtGRep1SensorFailure.KeyDown, _
        txtGRep2HiHi.KeyDown, txtGRep2Hi.KeyDown, txtGRep2Lo.KeyDown, txtGRep2LoLo.KeyDown, txtGRep2SensorFailure.KeyDown, _
        txtStatusHiHi.KeyDown, txtStatusHi.KeyDown, txtStatusLo.KeyDown, txtStatusLoLo.KeyDown, txtStatusSF.KeyDown, _
        txtHighNormal.KeyDown, txtLowNormal.KeyDown, _
        txtAlarmTimeup.KeyDown, txtBitCount.KeyDown, _
        txtExtGFa.KeyDown, txtDelayFa.KeyDown, txtGRep1Fa.KeyDown, txtGRep2Fa.KeyDown, txtStatusFa.KeyDown, _
        txtStatusDo1.KeyDown, txtStatusDo2.KeyDown, txtStatusDo3.KeyDown, txtStatusDo4.KeyDown, txtStatusDo5.KeyDown, txtStatusDo6.KeyDown, txtStatusDo7.KeyDown, txtStatusDo8.KeyDown, _
        txtSp1.KeyDown, txtSp2.KeyDown, txtHys1.KeyDown, txtHys2.KeyDown, txtSt.KeyDown, txtVar.KeyDown, _
        cmbStatusOut.KeyDown, txtStatusOut.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(sender)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUnit.KeyDown, _
                                                                                                              txtUnit.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbUnit)
                Call gDummySetColorChange(txtUnit)
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

    'Private Sub cmbStatusOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatusOut.KeyDown, _
    '                                                                                                               txtStatusOut.KeyDown
    '    Try

    '        If e.KeyCode = gCstDummySetKey Then
    '            Call gDummySetColorChange(cmbStatusOut)
    '            Call gDummySetColorChange(txtStatusOut)
    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    Private Sub txtRangeType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRangeFrom.KeyDown, txtRangeTo.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtRangeFrom)
                Call gDummySetColorChange(txtRangeTo)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrressDi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoDi.KeyDown, _
                                                                                                                   txtPortNoDi.KeyDown, _
                                                                                                                   txtPinDi.KeyDown

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

    Private Sub txtFuAdrressDo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoDo.KeyDown, _
                                                                                                                     txtPortNoDo.KeyDown, _
                                                                                                                     txtPinDo.KeyDown

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

    Private Sub txtFuAdrressAi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoAi.KeyDown, _
                                                                                                                   txtPortNoAi.KeyDown, _
                                                                                                                   txtPinAi.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtFuNoAi)
                Call gDummySetColorChange(txtPortNoAi)
                Call gDummySetColorChange(txtPinAi)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrressAo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNoAo.KeyDown, _
                                                                                                                   txtPortNoAo.KeyDown, _
                                                                                                                   txtPinAo.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtFuNoAo)
                Call gDummySetColorChange(txtPortNoAo)
                Call gDummySetColorChange(txtPinAo)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mDummyBackColorClear()

        cmbStatusIn.BackColor = Nothing
        cmbStatusOut.BackColor = Nothing
        cmbUnit.BackColor = Nothing
        cmbValueSensorFailure.BackColor = Nothing
        txtAlarmTimeup.BackColor = Nothing
        txtBitCount.BackColor = Nothing
        txtDelayFa.BackColor = Nothing
        txtDelayHi.BackColor = Nothing
        txtDelayHiHi.BackColor = Nothing
        txtDelayLo.BackColor = Nothing
        txtDelayLoLo.BackColor = Nothing
        txtDelaySensorFailure.BackColor = Nothing
        txtDelayTimer.BackColor = Nothing
        txtExtGFa.BackColor = Nothing
        txtExtGHi.BackColor = Nothing
        txtExtGHiHi.BackColor = Nothing
        txtExtGLo.BackColor = Nothing
        txtExtGLoLo.BackColor = Nothing
        txtExtGroup.BackColor = Nothing
        txtExtGSensorFailure.BackColor = Nothing
        txtFuNoAi.BackColor = Nothing
        txtFuNoAo.BackColor = Nothing
        txtFuNoDi.BackColor = Nothing
        txtFuNoDo.BackColor = Nothing
        txtGRep1.BackColor = Nothing
        txtGRep1Fa.BackColor = Nothing
        txtGRep1Hi.BackColor = Nothing
        txtGRep1HiHi.BackColor = Nothing
        txtGRep1Lo.BackColor = Nothing
        txtGRep1LoLo.BackColor = Nothing
        txtGRep1SensorFailure.BackColor = Nothing
        txtGRep2.BackColor = Nothing
        txtGRep2Fa.BackColor = Nothing
        txtGRep2Hi.BackColor = Nothing
        txtGRep2HiHi.BackColor = Nothing
        txtGRep2Lo.BackColor = Nothing
        txtGRep2LoLo.BackColor = Nothing
        txtGRep2SensorFailure.BackColor = Nothing
        txtHighNormal.BackColor = Nothing
        txtHys1.BackColor = Nothing
        txtHys2.BackColor = Nothing
        txtLowNormal.BackColor = Nothing
        txtPinAi.BackColor = Nothing
        txtPinAo.BackColor = Nothing
        txtPinDi.BackColor = Nothing
        txtPinDo.BackColor = Nothing
        txtPortNoAi.BackColor = Nothing
        txtPortNoAo.BackColor = Nothing
        txtPortNoDi.BackColor = Nothing
        txtPortNoDo.BackColor = Nothing
        txtRangeFrom.BackColor = Nothing
        txtRangeTo.BackColor = Nothing
        txtSp1.BackColor = Nothing
        txtSp2.BackColor = Nothing
        txtSt.BackColor = Nothing
        txtStatusFa.BackColor = Nothing
        txtStatusDo1.BackColor = Nothing
        txtStatusDo2.BackColor = Nothing
        txtStatusDo3.BackColor = Nothing
        txtStatusDo4.BackColor = Nothing
        txtStatusDo5.BackColor = Nothing
        txtStatusDo6.BackColor = Nothing
        txtStatusDo7.BackColor = Nothing
        txtStatusDo8.BackColor = Nothing
        txtStatusHi.BackColor = Nothing
        txtStatusHiHi.BackColor = Nothing
        txtStatusIn.BackColor = Nothing
        txtStatusLo.BackColor = Nothing
        txtStatusLoLo.BackColor = Nothing
        txtStatusOut.BackColor = Nothing
        txtStatusSF.BackColor = Nothing
        txtUnit.BackColor = Nothing
        txtValueHi.BackColor = Nothing
        txtValueHiHi.BackColor = Nothing
        txtValueLo.BackColor = Nothing
        txtValueLoLo.BackColor = Nothing
        txtVar.BackColor = Nothing

    End Sub

    Private Sub mDummyBackColorSet(ByVal intDataType As Integer)

        ''DIの処理
        If intDataType = gCstCodeChDataTypeValveDI_DO Then

            Call gDummySetColor(txtExtGroup, mValveDetail.DummyExtG)
            Call gDummySetColor(txtDelayTimer, mValveDetail.DummyDelay)
            Call gDummySetColor(txtGRep1, mValveDetail.DummyGroupRepose1)
            Call gDummySetColor(txtGRep2, mValveDetail.DummyGroupRepose2)
            Call gDummySetColor(txtFuNoDi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(txtPortNoDi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(txtPinDi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(txtBitCount, mValveDetail.DummyBitCount)
            Call gDummySetColor(cmbStatusIn, mValveDetail.DummyStatusName)
            Call gDummySetColor(txtStatusIn, mValveDetail.DummyStatusName)

        End If

        ''AIの処理
        If intDataType = gCstCodeChDataTypeValveAI_DO1 _
        Or intDataType = gCstCodeChDataTypeValveAI_DO2 _
        Or intDataType = gCstCodeChDataTypeValvePT_DO2 _
        Or intDataType = gCstCodeChDataTypeValveAI_AO1 _
        Or intDataType = gCstCodeChDataTypeValveAI_AO2 _
        Or intDataType = gCstCodeChDataTypeValvePT_AO2 Then

            Call gDummySetColor(txtFuNoAi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(txtPortNoAi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(txtPinAi, mValveDetail.DummyFuAddress)
            Call gDummySetColor(cmbStatusIn, mValveDetail.DummyStatusName)
            Call gDummySetColor(txtStatusIn, mValveDetail.DummyStatusName)
            Call gDummySetColor(cmbUnit, mValveDetail.DummyUnitName)
            Call gDummySetColor(txtUnit, mValveDetail.DummyUnitName)

            Call gDummySetColor(txtDelayHiHi, mValveDetail.DummyDelayHH)
            Call gDummySetColor(txtValueHiHi, mValveDetail.DummyValueHH)
            Call gDummySetColor(txtExtGHiHi, mValveDetail.DummyExtGrHH)
            Call gDummySetColor(txtGRep1HiHi, mValveDetail.DummyGRep1HH)
            Call gDummySetColor(txtGRep2HiHi, mValveDetail.DummyGRep2HH)
            Call gDummySetColor(txtStatusHiHi, mValveDetail.DummyStaNmHH)

            Call gDummySetColor(txtDelayHi, mValveDetail.DummyDelayH)
            Call gDummySetColor(txtValueHi, mValveDetail.DummyValueH)
            Call gDummySetColor(txtExtGHi, mValveDetail.DummyExtGrH)
            Call gDummySetColor(txtGRep1Hi, mValveDetail.DummyGRep1H)
            Call gDummySetColor(txtGRep2Hi, mValveDetail.DummyGRep2H)
            Call gDummySetColor(txtStatusHi, mValveDetail.DummyStaNmH)

            Call gDummySetColor(txtDelayLo, mValveDetail.DummyDelayL)
            Call gDummySetColor(txtValueLo, mValveDetail.DummyValueL)
            Call gDummySetColor(txtExtGLo, mValveDetail.DummyExtGrL)
            Call gDummySetColor(txtGRep1Lo, mValveDetail.DummyGRep1L)
            Call gDummySetColor(txtGRep2Lo, mValveDetail.DummyGRep2L)
            Call gDummySetColor(txtStatusLo, mValveDetail.DummyStaNmL)

            Call gDummySetColor(txtDelayLoLo, mValveDetail.DummyDelayLL)
            Call gDummySetColor(txtValueLoLo, mValveDetail.DummyValueLL)
            Call gDummySetColor(txtExtGLoLo, mValveDetail.DummyExtGrLL)
            Call gDummySetColor(txtGRep1LoLo, mValveDetail.DummyGRep1LL)
            Call gDummySetColor(txtGRep2LoLo, mValveDetail.DummyGRep2LL)
            Call gDummySetColor(txtStatusLoLo, mValveDetail.DummyStaNmLL)

            Call gDummySetColor(txtDelaySensorFailure, mValveDetail.DummyDelaySF)
            Call gDummySetColor(cmbValueSensorFailure, mValveDetail.DummyValueSF)
            Call gDummySetColor(txtExtGSensorFailure, mValveDetail.DummyExtGrSF)
            Call gDummySetColor(txtGRep1SensorFailure, mValveDetail.DummyGRep1SF)
            Call gDummySetColor(txtGRep2SensorFailure, mValveDetail.DummyGRep2SF)
            Call gDummySetColor(txtStatusSF, mValveDetail.DummyStaNmSF)

            Call gDummySetColor(txtRangeFrom, mValveDetail.DummyRangeScale)
            Call gDummySetColor(txtRangeTo, mValveDetail.DummyRangeScale)
            Call gDummySetColor(txtHighNormal, mValveDetail.DummyRangeNormalHi)
            Call gDummySetColor(txtLowNormal, mValveDetail.DummyRangeNormalLo)

            Call gDummySetColor(txtSp1, mValveDetail.DummySp1)
            Call gDummySetColor(txtSp2, mValveDetail.DummySp2)
            Call gDummySetColor(txtHys1, mValveDetail.DummyHysOpen)
            Call gDummySetColor(txtHys2, mValveDetail.DummyHysClose)
            Call gDummySetColor(txtSt, mValveDetail.DummySmpTime)

        End If

        ''DOの処理
        If intDataType = gCstCodeChDataTypeValveDI_DO _
        Or intDataType = gCstCodeChDataTypeValveAI_DO1 _
        Or intDataType = gCstCodeChDataTypeValveAI_DO2 _
        Or intDataType = gCstCodeChDataTypeValvePT_DO2 _
        Or intDataType = gCstCodeChDataTypeValveDO Then

            Call gDummySetColor(txtFuNoDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPortNoDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPinDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtBitCount, mValveDetail.DummyOutBitCount)
            Call gDummySetColor(cmbStatusOut, mValveDetail.DummyOutStatusType)
            Call gDummySetColor(txtStatusOut, mValveDetail.DummyOutStatusType)

            Call gDummySetColor(txtStatusDo1, mValveDetail.DummyOutStatus1)
            Call gDummySetColor(txtStatusDo2, mValveDetail.DummyOutStatus2)
            Call gDummySetColor(txtStatusDo3, mValveDetail.DummyOutStatus3)
            Call gDummySetColor(txtStatusDo4, mValveDetail.DummyOutStatus4)
            Call gDummySetColor(txtStatusDo5, mValveDetail.DummyOutStatus5)
            Call gDummySetColor(txtStatusDo6, mValveDetail.DummyOutStatus6)
            Call gDummySetColor(txtStatusDo7, mValveDetail.DummyOutStatus7)
            Call gDummySetColor(txtStatusDo8, mValveDetail.DummyOutStatus8)

            Call gDummySetColor(txtExtGFa, mValveDetail.DummyFaExtGr)
            Call gDummySetColor(txtDelayFa, mValveDetail.DummyFaDelay)
            Call gDummySetColor(txtGRep1Fa, mValveDetail.DummyFaGrep1)
            Call gDummySetColor(txtGRep2Fa, mValveDetail.DummyFaGrep2)
            Call gDummySetColor(txtStatusFa, mValveDetail.DummyFaStaNm)
            Call gDummySetColor(txtAlarmTimeup, mValveDetail.DummyFaTimeV)

        End If

        ''AOの処理
        If intDataType = gCstCodeChDataTypeValveAI_AO1 _
        Or intDataType = gCstCodeChDataTypeValveAI_AO2 _
        Or intDataType = gCstCodeChDataTypeValvePT_AO2 _
        Or intDataType = gCstCodeChDataTypeValveAO_4_20 Then

            Call gDummySetColor(txtFuNoAo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPortNoAo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPinAo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(cmbStatusOut, mValveDetail.DummyOutStatusType)
            Call gDummySetColor(txtStatusOut, mValveDetail.DummyOutStatus1)

            Call gDummySetColor(txtSp1, mValveDetail.DummySp1)
            Call gDummySetColor(txtSp2, mValveDetail.DummySp2)
            Call gDummySetColor(txtHys1, mValveDetail.DummyHysOpen)
            Call gDummySetColor(txtHys2, mValveDetail.DummyHysClose)
            Call gDummySetColor(txtSt, mValveDetail.DummySmpTime)

            Call gDummySetColor(txtExtGFa, mValveDetail.DummyFaExtGr)
            Call gDummySetColor(txtDelayFa, mValveDetail.DummyFaDelay)
            Call gDummySetColor(txtGRep1Fa, mValveDetail.DummyFaGrep1)
            Call gDummySetColor(txtGRep2Fa, mValveDetail.DummyFaGrep2)
            Call gDummySetColor(txtStatusFa, mValveDetail.DummyFaStaNm)
            Call gDummySetColor(txtAlarmTimeup, mValveDetail.DummyFaTimeV)

            Call gDummySetColor(txtVar, mValveDetail.DummyVar)

            Call gDummySetColor(cmbUnit, mValveDetail.DummyUnitName)
            Call gDummySetColor(txtUnit, mValveDetail.DummyUnitName)
            Call gDummySetColor(txtRangeFrom, mValveDetail.DummyRangeScale)
            Call gDummySetColor(txtRangeTo, mValveDetail.DummyRangeScale)

        End If

        ''Jacomの処理
        If intDataType = gCstCodeChDataTypeValveJacom Or intDataType = gCstCodeChDataTypeValveJacom55 Then

            Call gDummySetColor(txtExtGFa, mValveDetail.DummyFaExtGr)
            Call gDummySetColor(txtDelayFa, mValveDetail.DummyFaDelay)
            Call gDummySetColor(txtGRep1Fa, mValveDetail.DummyFaGrep1)
            Call gDummySetColor(txtGRep2Fa, mValveDetail.DummyFaGrep2)
            Call gDummySetColor(txtStatusFa, mValveDetail.DummyFaStaNm)
            Call gDummySetColor(txtAlarmTimeup, mValveDetail.DummyFaTimeV)

        End If

        ''Extの処理
        If intDataType = gCstCodeChDataTypeValveExt Then

            Call gDummySetColor(txtFuNoDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPortNoDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(txtPinDo, mValveDetail.DummyOutFuAddress)
            Call gDummySetColor(cmbStatusOut, mValveDetail.DummyOutStatusType)
            Call gDummySetColor(txtStatusOut, mValveDetail.DummyOutStatusType)
            Call gDummySetColor(txtExtGFa, mValveDetail.DummyFaExtGr)
            Call gDummySetColor(txtDelayFa, mValveDetail.DummyFaDelay)
            Call gDummySetColor(txtGRep1Fa, mValveDetail.DummyFaGrep1)
            Call gDummySetColor(txtGRep2Fa, mValveDetail.DummyFaGrep2)
            Call gDummySetColor(txtStatusFa, mValveDetail.DummyFaStaNm)
            Call gDummySetColor(txtAlarmTimeup, mValveDetail.DummyFaTimeV)

        End If

        '-----------------------------------------------------------------------------------------------------------------------
        'Select Case intDataType
        '    Case gCstCodeChDataTypeValveDI_DO

        '        If mValveDetail.DummyExtG Then Call objDummySetControl_KeyDown(txtExtGroup, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyDelay Then Call objDummySetControl_KeyDown(txtDelayTimer, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGroupRepose1 Then Call objDummySetControl_KeyDown(txtGRep1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGroupRepose2 Then Call objDummySetControl_KeyDown(txtGRep2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFuAddress Then Call txtFuAdrressDi_KeyDown(txtFuNoDi, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyBitCount Then Call objDummySetControl_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey)) ''要注意
        '        If mValveDetail.DummyStatusName Then Call cmbStatusIn_KeyDown(cmbStatusIn, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutBitCount Then Call txtFuAdrressDo_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey)) ''要注意
        '        If mValveDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatusType Then Call cmbStatusOut_KeyDown(cmbStatusOut, New KeyEventArgs(gCstDummySetKey))

        '        ''入力点数、出力点数は、画面上では１つのコントロールなのでこうする
        '        If mValveDetail.DummyBitCount Or mValveDetail.DummyOutBitCount Then
        '            objDummySetControl_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey))
        '        End If

        '        If mValveDetail.DummyOutStatus1 Then Call objDummySetControl_KeyDown(txtStatusDo1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus2 Then Call objDummySetControl_KeyDown(txtStatusDo2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus3 Then Call objDummySetControl_KeyDown(txtStatusDo3, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus4 Then Call objDummySetControl_KeyDown(txtStatusDo4, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus5 Then Call objDummySetControl_KeyDown(txtStatusDo5, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus6 Then Call objDummySetControl_KeyDown(txtStatusDo6, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus7 Then Call objDummySetControl_KeyDown(txtStatusDo7, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus8 Then Call objDummySetControl_KeyDown(txtStatusDo8, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        '    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, _
        '         gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

        '        If mValveDetail.DummyFuAddress Then Call txtFuAdrressDi_KeyDown(txtFuNoDi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStatusName Then Call cmbStatusIn_KeyDown(cmbStatusIn, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyUnitName Then Call cmbUnit_KeyDown(cmbUnit, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyDelayHH Then Call objDummySetControl_KeyDown(txtDelayHiHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyValueHH Then Call objDummySetControl_KeyDown(txtValueHiHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyExtGrHH Then Call objDummySetControl_KeyDown(txtExtGHiHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep1HH Then Call objDummySetControl_KeyDown(txtGRep1HiHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep2HH Then Call objDummySetControl_KeyDown(txtGRep2HiHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStaNmHH Then Call objDummySetControl_KeyDown(txtStatusHiHi, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyDelayH Then Call objDummySetControl_KeyDown(txtDelayHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyValueH Then Call objDummySetControl_KeyDown(txtValueHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyExtGrH Then Call objDummySetControl_KeyDown(txtExtGHi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep1H Then Call objDummySetControl_KeyDown(txtGRep1Hi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep2H Then Call objDummySetControl_KeyDown(txtGRep2Hi, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStaNmH Then Call objDummySetControl_KeyDown(txtStatusHi, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyDelayL Then Call objDummySetControl_KeyDown(txtDelayLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyValueL Then Call objDummySetControl_KeyDown(txtValueLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyExtGrL Then Call objDummySetControl_KeyDown(txtExtGLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep1L Then Call objDummySetControl_KeyDown(txtGRep1Lo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep2L Then Call objDummySetControl_KeyDown(txtGRep2Lo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStaNmL Then Call objDummySetControl_KeyDown(txtStatusLo, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyDelayLL Then Call objDummySetControl_KeyDown(txtDelayLoLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyValueLL Then Call objDummySetControl_KeyDown(txtValueLoLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyExtGrLL Then Call objDummySetControl_KeyDown(txtExtGLoLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep1LL Then Call objDummySetControl_KeyDown(txtGRep1LoLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep2LL Then Call objDummySetControl_KeyDown(txtGRep2LoLo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStaNmLL Then Call objDummySetControl_KeyDown(txtStatusLoLo, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyDelaySF Then Call objDummySetControl_KeyDown(txtDelaySensorFailure, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyValueSF Then Call objDummySetControl_KeyDown(cmbValueSensorFailure, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyExtGrSF Then Call objDummySetControl_KeyDown(txtExtGSensorFailure, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep1SF Then Call objDummySetControl_KeyDown(txtGRep1SensorFailure, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyGRep2SF Then Call objDummySetControl_KeyDown(txtGRep2SensorFailure, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyStaNmSF Then Call objDummySetControl_KeyDown(txtStatusSF, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyRangeScale Then Call txtRangeType_KeyDown(txtRangeFrom, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyRangeNormalHi Then Call objDummySetControl_KeyDown(txtHighNormal, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyRangeNormalLo Then Call objDummySetControl_KeyDown(txtLowNormal, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutBitCount Then Call objDummySetControl_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey)) ''要注意
        '        If mValveDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoAo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatusType Then Call cmbStatusOut_KeyDown(cmbStatusOut, New KeyEventArgs(gCstDummySetKey))

        '        'If mValveDetail.DummyOutStatus1 Then Call objDummySetControl_KeyDown(txtStatusDo1, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus2 Then Call objDummySetControl_KeyDown(txtStatusDo2, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus3 Then Call objDummySetControl_KeyDown(txtStatusDo3, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus4 Then Call objDummySetControl_KeyDown(txtStatusDo4, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus5 Then Call objDummySetControl_KeyDown(txtStatusDo5, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus6 Then Call objDummySetControl_KeyDown(txtStatusDo6, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus7 Then Call objDummySetControl_KeyDown(txtStatusDo7, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyOutStatus8 Then Call objDummySetControl_KeyDown(txtStatusDo8, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummySp1 Then Call objDummySetControl_KeyDown(txtSp1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummySp2 Then Call objDummySetControl_KeyDown(txtSp2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyHysOpen Then Call objDummySetControl_KeyDown(txtHys1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyHysClose Then Call objDummySetControl_KeyDown(txtHys2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummySmpTime Then Call objDummySetControl_KeyDown(txtSt, New KeyEventArgs(gCstDummySetKey))
        '        'If mValveDetail.DummyVar Then Call objDummySetControl_KeyDown(txtVar, New KeyEventArgs(gCstDummySetKey))

        '        Select Case intDataType
        '            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

        '                ''DOがある場合はBitCountとステータス名称1～8の処理を行う（AOにBitCountとステータス名称1～8はない）
        '                If mValveDetail.DummyOutBitCount Then Call objDummySetControl_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey)) ''要注意
        '                If mValveDetail.DummyOutStatus1 Then Call objDummySetControl_KeyDown(txtStatusDo1, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus2 Then Call objDummySetControl_KeyDown(txtStatusDo2, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus3 Then Call objDummySetControl_KeyDown(txtStatusDo3, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus4 Then Call objDummySetControl_KeyDown(txtStatusDo4, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus5 Then Call objDummySetControl_KeyDown(txtStatusDo5, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus6 Then Call objDummySetControl_KeyDown(txtStatusDo6, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus7 Then Call objDummySetControl_KeyDown(txtStatusDo7, New KeyEventArgs(gCstDummySetKey))
        '                If mValveDetail.DummyOutStatus8 Then Call objDummySetControl_KeyDown(txtStatusDo8, New KeyEventArgs(gCstDummySetKey))

        '            Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

        '                ''AOがある場合はVarの処理を行う（DOにVarはない）
        '                If mValveDetail.DummyVar Then Call objDummySetControl_KeyDown(txtVar, New KeyEventArgs(gCstDummySetKey))

        '        End Select

        '    Case gCstCodeChDataTypeValveDO

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutBitCount Then Call txtFuAdrressDo_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey)) ''要注意
        '        If mValveDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatusType Then Call cmbStatusOut_KeyDown(cmbStatusOut, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyOutStatus1 Then Call objDummySetControl_KeyDown(txtStatusDo1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus2 Then Call objDummySetControl_KeyDown(txtStatusDo2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus3 Then Call objDummySetControl_KeyDown(txtStatusDo3, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus4 Then Call objDummySetControl_KeyDown(txtStatusDo4, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus5 Then Call objDummySetControl_KeyDown(txtStatusDo5, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus6 Then Call objDummySetControl_KeyDown(txtStatusDo6, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus7 Then Call objDummySetControl_KeyDown(txtStatusDo7, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatus8 Then Call objDummySetControl_KeyDown(txtStatusDo8, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        '    Case gCstCodeChDataTypeValveAO_4_20

        '        If mValveDetail.DummyUnitName Then Call cmbUnit_KeyDown(cmbUnit, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyRangeScale Then Call txtRangeType_KeyDown(txtRangeFrom, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoAo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutStatusType Then Call cmbStatusOut_KeyDown(cmbStatusOut, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        '        If mValveDetail.DummySp1 Then Call objDummySetControl_KeyDown(txtSp1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummySp2 Then Call objDummySetControl_KeyDown(txtSp2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyHysOpen Then Call objDummySetControl_KeyDown(txtHys1, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyHysClose Then Call objDummySetControl_KeyDown(txtHys2, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummySmpTime Then Call objDummySetControl_KeyDown(txtSt, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyVar Then Call objDummySetControl_KeyDown(txtVar, New KeyEventArgs(gCstDummySetKey))

        '    Case gCstCodeChDataTypeValveExt

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        '    Case gCstCodeChDataTypeValveJacom

        '        If mValveDetail.DummyFaTimerValue Then Call objDummySetControl_KeyDown(txtAlarmTimeup, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyOutFuAddress Then Call txtFuAdrressDo_KeyDown(txtFuNoDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaExtGr Then Call objDummySetControl_KeyDown(txtExtGDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaDelay Then Call objDummySetControl_KeyDown(txtDelayDo, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep1 Then Call objDummySetControl_KeyDown(txtGRep1Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaGrep2 Then Call objDummySetControl_KeyDown(txtGRep2Do, New KeyEventArgs(gCstDummySetKey))
        '        If mValveDetail.DummyFaStaNm Then Call objDummySetControl_KeyDown(txtStatusDo, New KeyEventArgs(gCstDummySetKey))

        'End Select

    End Sub


#End Region

#Region "コメントアウト"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 設定単位 コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub cmbTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTime.SelectedIndexChanged

    '    Try

    '        If mintDelayTimeKubun <> cmbTime.SelectedValue Then

    '            If cmbTime.SelectedValue = 0 Then
    '                ''分 -- > 秒
    '                If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) * 60)
    '                If txtDelayDo.Text <> "" Then txtDelayDo.Text = Format(CCDouble(txtDelayDo.Text) * 60)
    '            Else
    '                ''秒 --> 分
    '                If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) / 60, "0.0")
    '                If txtDelayDo.Text <> "" Then txtDelayDo.Text = Format(CCDouble(txtDelayDo.Text) / 60, "0.0")
    '            End If

    '        End If

    '        mintDelayTimeKubun = cmbTime.SelectedValue

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtDelay_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    '                                     txtDelayTimer.Validated, txtDelayDo.Validated, _
    '                                     txtDelayHiHi.Validated, txtDelayHi.Validated, _
    '                                     txtDelayLo.Validated, txtDelayLoLo.Validated, txtDelaySensorFailure.Validated

    '    Try
    '        Dim myTextBox As TextBox = CType(sender, TextBox)

    '        If cmbTime.SelectedValue = 1 Then

    '            If IsNumeric(myTextBox.Text) Then
    '                myTextBox.Text = Double.Parse(myTextBox.Text).ToString("0.0")
    '            Else
    '                myTextBox.Text = ""
    '            End If

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

    '    With mValveDetail

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

    '        If .FlagMin <> cmbTime.SelectedValue.ToString Then Return True

    '        If .BitCount <> txtBitCount.Text Then Return True
    '        If .DataType <> cmbDataType.SelectedValue Then Return True
    '        If .PortNo <> cmbExtDevice.SelectedValue Then Return True
    '        If .FlagStatusAlarm <> chkStatusAlarm.Checked Then Return True

    '        Select Case cmbDataType.SelectedValue

    '            Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValveAI_AO

    '                If cmbStatusIn.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                    If .StatusIn <> cmbStatusIn.Text Then Return True
    '                Else
    '                    If .StatusIn <> txtStatusIn.Text Then Return True
    '                End If

    '                If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                    If .StatusOut <> cmbStatusOut.Text Then Return True
    '                Else
    '                    If .StatusOut <> txtStatusOut.Text Then Return True
    '                End If

    '            Case Else

    '                .StatusIn = ""

    '                If cmbStatusOut.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                    If .StatusOut <> cmbStatusOut.Text Then Return True
    '                Else
    '                    If .StatusOut <> txtStatusOut.Text Then Return True
    '                End If

    '        End Select

    '        If cmbDataType.SelectedValue = gCstCodeChDataTypeValveExt Then
    '            ''延長警報
    '            If .EccFunc <> cmbExtDevice.SelectedValue Then Return True
    '        End If

    '        If .DIStart <> txtFuNoDi.Text & txtPortNoDi.Text & txtPinDi.Text Then Return True
    '        If .DOStart <> txtFuNoDo.Text & txtPortNoDo.Text & txtPinDo.Text Then Return True
    '        If .AITerm <> txtFuNoAi.Text & txtPortNoAi.Text & txtPinAi.Text Then Return True
    '        If .AOTerm <> txtFuNoAo.Text & txtPortNoAo.Text & txtPinAo.Text Then Return True

    '        If .AlarmTimeup <> txtAlarmTimeup.Text Then Return True
    '        If .FilterCoef <> txtFilterCoeficient.Text Then Return True
    '        If .CompValue <> txtCompValue.Text Then Return True
    '        If .CompExp <> cmbCompEx.SelectedValue Then Return True

    '    End With

    '    Return False

    'End Function

#End Region
  
    Private Sub TabPage1_Click(sender As System.Object, e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub txtCompositeIndex_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCompositeIndex.TextChanged

    End Sub
End Class
