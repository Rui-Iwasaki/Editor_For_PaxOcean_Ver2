Public Class frmChListAnalog

#Region "変数定義"

    ''OKフラグ
    Private mintOkFlag As Integer

    ''Next CH ボタン　クリックフラグ
    Private mintNextChFlag As Integer = 0

    ''Before CH ボタン　クリックフラグ
    Private mintBeforeChFlag As Integer = 0

    ''イベント重複抑制用
    Private mblnFlg As Boolean

    ''Delay Timer 設定単位区分
    Private mintDelayTimeKubun As Integer   ''0:秒　1:分

    ''アナログチャンネル情報格納
    Private mAnalogDetail As frmChListChannelList.mAnalogInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:変更あり  <> 0:変更なし
    ' 引き数    : ARG1 - (IO) アナログチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hAnalogDetail As frmChListChannelList.mAnalogInfo, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            mAnalogDetail.RowNo = hAnalogDetail.RowNo
            mAnalogDetail.RowNoFirst = hAnalogDetail.RowNoFirst
            mAnalogDetail.RowNoEnd = hAnalogDetail.RowNoEnd
            mAnalogDetail.SysNo = hAnalogDetail.SysNo
            mAnalogDetail.ChNo = hAnalogDetail.ChNo
            mAnalogDetail.TagNo = hAnalogDetail.TagNo       '' 2015.10.26  Ver1.7.4  ﾀｸﾞ追加
            mAnalogDetail.ItemName = hAnalogDetail.ItemName
            mAnalogDetail.AlmLevel = hAnalogDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mAnalogDetail.ValueHH = hAnalogDetail.ValueHH
            mAnalogDetail.ValueH = hAnalogDetail.ValueH
            mAnalogDetail.ValueL = hAnalogDetail.ValueL
            mAnalogDetail.ValueLL = hAnalogDetail.ValueLL
            mAnalogDetail.ValueSF = hAnalogDetail.ValueSF
            mAnalogDetail.ExtGHH = hAnalogDetail.ExtGHH
            mAnalogDetail.ExtGH = hAnalogDetail.ExtGH
            mAnalogDetail.ExtGL = hAnalogDetail.ExtGL
            mAnalogDetail.ExtGLL = hAnalogDetail.ExtGLL
            mAnalogDetail.ExtGSF = hAnalogDetail.ExtGSF
            mAnalogDetail.DelayHH = hAnalogDetail.DelayHH
            mAnalogDetail.DelayH = hAnalogDetail.DelayH
            mAnalogDetail.DelayL = hAnalogDetail.DelayL
            mAnalogDetail.DelayLL = hAnalogDetail.DelayLL
            mAnalogDetail.DelaySF = hAnalogDetail.DelaySF
            mAnalogDetail.GRep1HH = hAnalogDetail.GRep1HH
            mAnalogDetail.GRep1H = hAnalogDetail.GRep1H
            mAnalogDetail.GRep1L = hAnalogDetail.GRep1L
            mAnalogDetail.GRep1LL = hAnalogDetail.GRep1LL
            mAnalogDetail.GRep2HH = hAnalogDetail.GRep2HH
            mAnalogDetail.GRep2H = hAnalogDetail.GRep2H
            mAnalogDetail.GRep2L = hAnalogDetail.GRep2L
            mAnalogDetail.GRep2LL = hAnalogDetail.GRep2LL
            mAnalogDetail.StatusHH = hAnalogDetail.StatusHH
            mAnalogDetail.StatusH = hAnalogDetail.StatusH
            mAnalogDetail.StatusL = hAnalogDetail.StatusL
            mAnalogDetail.StatusLL = hAnalogDetail.StatusLL
            mAnalogDetail.FlagDmy = hAnalogDetail.FlagDmy
            mAnalogDetail.FlagSC = hAnalogDetail.FlagSC
            mAnalogDetail.FlagSIO = hAnalogDetail.FlagSIO
            mAnalogDetail.FlagGWS = hAnalogDetail.FlagGWS
            mAnalogDetail.FlagWK = hAnalogDetail.FlagWK
            mAnalogDetail.FlagRL = hAnalogDetail.FlagRL
            mAnalogDetail.FlagAC = hAnalogDetail.FlagAC
            mAnalogDetail.FlagEP = hAnalogDetail.FlagEP
            mAnalogDetail.FlagPLC = hAnalogDetail.FlagPLC       '' 2014.11.18
            mAnalogDetail.FlagSP = hAnalogDetail.FlagSP
            mAnalogDetail.FlagMin = hAnalogDetail.FlagMin
            mAnalogDetail.FuNo = hAnalogDetail.FuNo
            mAnalogDetail.FUPortNo = hAnalogDetail.FUPortNo
            mAnalogDetail.FUPin = hAnalogDetail.FUPin
            mAnalogDetail.DataType = hAnalogDetail.DataType
            mAnalogDetail.PortNo = hAnalogDetail.PortNo

            '未使用
            mAnalogDetail.RangeType = hAnalogDetail.RangeType


            mAnalogDetail.DecimalPosition = hAnalogDetail.DecimalPosition
            mAnalogDetail.RangeFrom = hAnalogDetail.RangeFrom
            mAnalogDetail.RangeTo = hAnalogDetail.RangeTo
            mAnalogDetail.Status = hAnalogDetail.Status
            mAnalogDetail.NormalLO = hAnalogDetail.NormalLO
            mAnalogDetail.NormalHI = hAnalogDetail.NormalHI
            mAnalogDetail.OffSet = hAnalogDetail.OffSet
            mAnalogDetail.Unit = hAnalogDetail.Unit
            mAnalogDetail.strString = hAnalogDetail.strString
            mAnalogDetail.FlagCenterGraph = hAnalogDetail.FlagCenterGraph
            mAnalogDetail.FlagPowerFactor = hAnalogDetail.FlagPowerFactor   '' Ver1.10.1 2016.02.29 力率対応 追加
            mAnalogDetail.FlagPSDisp = hAnalogDetail.FlagPSDisp     '' Ver1.11.9.3 2016.11.26 P/S表示追加
            mAnalogDetail.ShareType = hAnalogDetail.ShareType
            mAnalogDetail.ShareChNo = hAnalogDetail.ShareChNo
            mAnalogDetail.Remarks = hAnalogDetail.Remarks

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mAnalogDetail.AlmMimic = hAnalogDetail.AlmMimic

            'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
            mAnalogDetail.intAdjPSW = hAnalogDetail.intAdjPSW

            'Ver2.0.8.5 mmHgレンジ下限小数点対応
            mAnalogDetail.intMmHgFlg = hAnalogDetail.intMmHgFlg
            mAnalogDetail.intMmHgDec = hAnalogDetail.intMmHgDec


            '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mAnalogDetail.DummyFuAddress = hAnalogDetail.DummyFuAddress
            mAnalogDetail.DummyUnitName = hAnalogDetail.DummyUnitName
            mAnalogDetail.DummyStatusName = hAnalogDetail.DummyStatusName

            mAnalogDetail.DummyDelayHH = hAnalogDetail.DummyDelayHH
            mAnalogDetail.DummyValueHH = hAnalogDetail.DummyValueHH
            mAnalogDetail.DummyExtGrHH = hAnalogDetail.DummyExtGrHH
            mAnalogDetail.DummyGRep1HH = hAnalogDetail.DummyGRep1HH
            mAnalogDetail.DummyGRep2HH = hAnalogDetail.DummyGRep2HH
            mAnalogDetail.DummyStaNmHH = hAnalogDetail.DummyStaNmHH

            mAnalogDetail.DummyDelayH = hAnalogDetail.DummyDelayH
            mAnalogDetail.DummyValueH = hAnalogDetail.DummyValueH
            mAnalogDetail.DummyExtGrH = hAnalogDetail.DummyExtGrH
            mAnalogDetail.DummyGRep1H = hAnalogDetail.DummyGRep1H
            mAnalogDetail.DummyGRep2H = hAnalogDetail.DummyGRep2H
            mAnalogDetail.DummyStaNmH = hAnalogDetail.DummyStaNmH

            mAnalogDetail.DummyDelayL = hAnalogDetail.DummyDelayL
            mAnalogDetail.DummyValueL = hAnalogDetail.DummyValueL
            mAnalogDetail.DummyExtGrL = hAnalogDetail.DummyExtGrL
            mAnalogDetail.DummyGRep1L = hAnalogDetail.DummyGRep1L
            mAnalogDetail.DummyGRep2L = hAnalogDetail.DummyGRep2L
            mAnalogDetail.DummyStaNmL = hAnalogDetail.DummyStaNmL

            mAnalogDetail.DummyDelayLL = hAnalogDetail.DummyDelayLL
            mAnalogDetail.DummyValueLL = hAnalogDetail.DummyValueLL
            mAnalogDetail.DummyExtGrLL = hAnalogDetail.DummyExtGrLL
            mAnalogDetail.DummyGRep1LL = hAnalogDetail.DummyGRep1LL
            mAnalogDetail.DummyGRep2LL = hAnalogDetail.DummyGRep2LL
            mAnalogDetail.DummyStaNmLL = hAnalogDetail.DummyStaNmLL

            mAnalogDetail.DummyDelaySF = hAnalogDetail.DummyDelaySF
            mAnalogDetail.DummyValueSF = hAnalogDetail.DummyValueSF
            mAnalogDetail.DummyExtGrSF = hAnalogDetail.DummyExtGrSF
            mAnalogDetail.DummyGRep1SF = hAnalogDetail.DummyGRep1SF
            mAnalogDetail.DummyGRep2SF = hAnalogDetail.DummyGRep2SF
            mAnalogDetail.DummyStaNmSF = hAnalogDetail.DummyStaNmSF

            mAnalogDetail.DummyRangeScale = hAnalogDetail.DummyRangeScale
            mAnalogDetail.DummyRangeNormalHi = hAnalogDetail.DummyRangeNormalHi
            mAnalogDetail.DummyRangeNormalLo = hAnalogDetail.DummyRangeNormalLo
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hAnalogDetail, mAnalogDetail) = False Then

                    hAnalogDetail.SysNo = mAnalogDetail.SysNo
                    hAnalogDetail.ChNo = mAnalogDetail.ChNo
                    hAnalogDetail.TagNo = mAnalogDetail.TagNo       '' 2015.10.26 Ver1.7.5
                    hAnalogDetail.ItemName = mAnalogDetail.ItemName
                    hAnalogDetail.AlmLevel = mAnalogDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hAnalogDetail.ValueHH = mAnalogDetail.ValueHH
                    hAnalogDetail.ValueH = mAnalogDetail.ValueH
                    hAnalogDetail.ValueL = mAnalogDetail.ValueL
                    hAnalogDetail.ValueLL = mAnalogDetail.ValueLL
                    hAnalogDetail.ValueSF = mAnalogDetail.ValueSF
                    hAnalogDetail.ExtGHH = mAnalogDetail.ExtGHH
                    hAnalogDetail.ExtGH = mAnalogDetail.ExtGH
                    hAnalogDetail.ExtGL = mAnalogDetail.ExtGL
                    hAnalogDetail.ExtGLL = mAnalogDetail.ExtGLL
                    hAnalogDetail.ExtGSF = mAnalogDetail.ExtGSF
                    hAnalogDetail.DelayHH = mAnalogDetail.DelayHH
                    hAnalogDetail.DelayH = mAnalogDetail.DelayH
                    hAnalogDetail.DelayL = mAnalogDetail.DelayL
                    hAnalogDetail.DelayLL = mAnalogDetail.DelayLL
                    hAnalogDetail.DelaySF = mAnalogDetail.DelaySF
                    hAnalogDetail.GRep1HH = mAnalogDetail.GRep1HH
                    hAnalogDetail.GRep1H = mAnalogDetail.GRep1H
                    hAnalogDetail.GRep1L = mAnalogDetail.GRep1L
                    hAnalogDetail.GRep1LL = mAnalogDetail.GRep1LL

                    'T.ueki
                    hAnalogDetail.GRep1SF = mAnalogDetail.GRep1SF

                    'T.ueki
                    hAnalogDetail.GRep2SF = mAnalogDetail.GRep2SF

                    hAnalogDetail.GRep2HH = mAnalogDetail.GRep2HH
                    hAnalogDetail.GRep2H = mAnalogDetail.GRep2H
                    hAnalogDetail.GRep2L = mAnalogDetail.GRep2L
                    hAnalogDetail.GRep2LL = mAnalogDetail.GRep2LL
                    hAnalogDetail.StatusHH = mAnalogDetail.StatusHH
                    hAnalogDetail.StatusH = mAnalogDetail.StatusH
                    hAnalogDetail.StatusL = mAnalogDetail.StatusL
                    hAnalogDetail.StatusLL = mAnalogDetail.StatusLL
                    hAnalogDetail.FlagDmy = mAnalogDetail.FlagDmy
                    hAnalogDetail.FlagSC = mAnalogDetail.FlagSC
                    hAnalogDetail.FlagSIO = mAnalogDetail.FlagSIO
                    hAnalogDetail.FlagGWS = mAnalogDetail.FlagGWS
                    hAnalogDetail.FlagWK = mAnalogDetail.FlagWK
                    hAnalogDetail.FlagRL = mAnalogDetail.FlagRL
                    hAnalogDetail.FlagAC = mAnalogDetail.FlagAC
                    hAnalogDetail.FlagEP = mAnalogDetail.FlagEP
                    hAnalogDetail.FlagPLC = mAnalogDetail.FlagPLC       '' 2014.11.18
                    hAnalogDetail.FlagSP = mAnalogDetail.FlagSP
                    hAnalogDetail.FlagMin = mAnalogDetail.FlagMin
                    hAnalogDetail.FuNo = mAnalogDetail.FuNo
                    hAnalogDetail.FUPortNo = mAnalogDetail.FUPortNo
                    hAnalogDetail.FUPin = mAnalogDetail.FUPin
                    hAnalogDetail.DataType = mAnalogDetail.DataType
                    hAnalogDetail.PortNo = mAnalogDetail.PortNo
                    hAnalogDetail.RangeType = mAnalogDetail.RangeType
                    hAnalogDetail.DecimalPosition = mAnalogDetail.DecimalPosition
                    hAnalogDetail.RangeFrom = mAnalogDetail.RangeFrom
                    hAnalogDetail.RangeTo = mAnalogDetail.RangeTo
                    hAnalogDetail.Status = mAnalogDetail.Status
                    hAnalogDetail.NormalLO = mAnalogDetail.NormalLO
                    hAnalogDetail.NormalHI = mAnalogDetail.NormalHI
                    hAnalogDetail.OffSet = mAnalogDetail.OffSet
                    hAnalogDetail.Unit = mAnalogDetail.Unit
                    hAnalogDetail.strString = mAnalogDetail.strString
                    hAnalogDetail.FlagCenterGraph = mAnalogDetail.FlagCenterGraph
                    hAnalogDetail.FlagPowerFactor = mAnalogDetail.FlagPowerFactor   '' Ver1.10.1 2016.02.29 力率対応 追加
                    hAnalogDetail.FlagPSDisp = mAnalogDetail.FlagPSDisp     '' Ver1.11.9.3 2016.11.26 P/S表示追加
                    hAnalogDetail.ShareType = mAnalogDetail.ShareType
                    hAnalogDetail.ShareChNo = mAnalogDetail.ShareChNo
                    hAnalogDetail.Remarks = mAnalogDetail.Remarks

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hAnalogDetail.AlmMimic = mAnalogDetail.AlmMimic

                    'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
                    hAnalogDetail.intAdjPSW = mAnalogDetail.intAdjPSW

                    'Ver2.0.8.5 mmHgレンジ下限小数点対応
                    hAnalogDetail.intMmHgFlg = mAnalogDetail.intMmHgFlg
                    hAnalogDetail.intMmHgDec = mAnalogDetail.intMmHgDec


                    '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hAnalogDetail.DummyFuAddress = mAnalogDetail.DummyFuAddress
                    hAnalogDetail.DummyUnitName = mAnalogDetail.DummyUnitName
                    hAnalogDetail.DummyStatusName = mAnalogDetail.DummyStatusName

                    hAnalogDetail.DummyDelayHH = mAnalogDetail.DummyDelayHH
                    hAnalogDetail.DummyValueHH = mAnalogDetail.DummyValueHH
                    hAnalogDetail.DummyExtGrHH = mAnalogDetail.DummyExtGrHH
                    hAnalogDetail.DummyGRep1HH = mAnalogDetail.DummyGRep1HH
                    hAnalogDetail.DummyGRep2HH = mAnalogDetail.DummyGRep2HH
                    hAnalogDetail.DummyStaNmHH = mAnalogDetail.DummyStaNmHH

                    hAnalogDetail.DummyDelayH = mAnalogDetail.DummyDelayH
                    hAnalogDetail.DummyValueH = mAnalogDetail.DummyValueH
                    hAnalogDetail.DummyExtGrH = mAnalogDetail.DummyExtGrH
                    hAnalogDetail.DummyGRep1H = mAnalogDetail.DummyGRep1H
                    hAnalogDetail.DummyGRep2H = mAnalogDetail.DummyGRep2H
                    hAnalogDetail.DummyStaNmH = mAnalogDetail.DummyStaNmH

                    hAnalogDetail.DummyDelayL = mAnalogDetail.DummyDelayL
                    hAnalogDetail.DummyValueL = mAnalogDetail.DummyValueL
                    hAnalogDetail.DummyExtGrL = mAnalogDetail.DummyExtGrL
                    hAnalogDetail.DummyGRep1L = mAnalogDetail.DummyGRep1L
                    hAnalogDetail.DummyGRep2L = mAnalogDetail.DummyGRep2L
                    hAnalogDetail.DummyStaNmL = mAnalogDetail.DummyStaNmL

                    hAnalogDetail.DummyDelayLL = mAnalogDetail.DummyDelayLL
                    hAnalogDetail.DummyValueLL = mAnalogDetail.DummyValueLL
                    hAnalogDetail.DummyExtGrLL = mAnalogDetail.DummyExtGrLL
                    hAnalogDetail.DummyGRep1LL = mAnalogDetail.DummyGRep1LL
                    hAnalogDetail.DummyGRep2LL = mAnalogDetail.DummyGRep2LL
                    hAnalogDetail.DummyStaNmLL = mAnalogDetail.DummyStaNmLL

                    hAnalogDetail.DummyDelaySF = mAnalogDetail.DummyDelaySF
                    hAnalogDetail.DummyValueSF = mAnalogDetail.DummyValueSF
                    hAnalogDetail.DummyExtGrSF = mAnalogDetail.DummyExtGrSF
                    hAnalogDetail.DummyGRep1SF = mAnalogDetail.DummyGRep1SF
                    hAnalogDetail.DummyGRep2SF = mAnalogDetail.DummyGRep2SF
                    hAnalogDetail.DummyStaNmSF = mAnalogDetail.DummyStaNmSF

                    hAnalogDetail.DummyRangeScale = mAnalogDetail.DummyRangeScale
                    hAnalogDetail.DummyRangeNormalHi = mAnalogDetail.DummyRangeNormalHi
                    hAnalogDetail.DummyRangeNormalLo = mAnalogDetail.DummyRangeNormalLo
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
    Private Sub frmChListAnalog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim intValue As Integer

            Dim intDecPointFrom As Integer
            Dim intDecPointTo As Integer

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            ''画面初期化
            Call mInitial()

            With mAnalogDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTag.Text = .TagNo    '' 2015.10.26 Ver1.7.5 ﾀｸﾞ表示追加
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

                    '■Share対応
                    cmbShareType.SelectedValue = .ShareType
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
                txtPLC.Text = .FlagPLC
                txtSP.Text = .FlagSP

                cmbTime.SelectedValue = IIf(.FlagMin = "", 0, .FlagMin)

                cmbDataType.SelectedValue = .DataType

                txtFuNo.Text = Trim(.FuNo)
                txtPortNo.Text = Trim(.FUPortNo)

                Select Case .DataType
                    '外部機器通信
                    Case gCstCodeChDataTypeAnalogJacom
                        cmbExtDevice.SelectedValue = .PortNo
                    Case gCstCodeChDataTypeAnalogJacom55
                        cmbExtDevice.SelectedValue = .PortNo
                    Case Else
                        txtPin.Text = Trim(.FUPin)
                End Select

                'Ver1.11.1 2016.07.12  緯度・経度を通信に含める
                '2016/10/17 T.Ueki 2,3線式 プルダウン取り止め

                'Ver2.0.1.2 外部通信機器でもﾚﾝｼﾞ入力さす
                Select Case .DataType
                    '外部機器通信
                    'Case gCstCodeChDataTypeAnalogJacom, gCstCodeChDataTypeAnalogModbus, gCstCodeChDataTypeAnalogLatitude, gCstCodeChDataTypeAnalogLongitude

                    Case Else

                        '下限設定値txtRangeFrom.textの中に小数点が何桁目にあるか確認
                        intDecPointFrom = intDecimalPointSch(.RangeFrom)

                        '上限設定値txtRangeTo.textの中に小数点が何桁目にあるか確認
                        intDecPointTo = intDecimalPointSch(.RangeTo)

                        '下限と上限値の小数点位置確認
                        If intDecPointFrom <> intDecPointTo Then

                            If intDecPointFrom < intDecPointTo Then
                                'Decimal Point(display2)
                                txtDecimal.Text = Str(intDecPointTo)
                            Else
                                'Decimal Point(display2)
                                txtDecimal.Text = Str(intDecPointFrom)
                            End If
                        Else
                            'Decimal Point(display2)
                            txtDecimal.Text = Str(intDecPointFrom)
                        End If

                        '下限値
                        txtRangeFrom.Text = .RangeFrom

                        '上限値
                        txtRangeTo.Text = .RangeTo

                End Select

                '表示固定位置(display1)
                txtString.Text = .strString

                ''Status
                intValue = cmbStatus.FindStringExact(.Status)
                'Ver2.0.2.6 ｽﾃｰﾀｽとｱﾗｰﾑに矛盾があればマニュアル化
                'Ver2.0.2.7 ｽﾃｰﾀｽ不具合修正
                If intValue >= 0 Then
                    If fnAnalogStatusAlarmRel(.Status) < 0 Then
                        intValue = -1
                    End If
                End If
                If intValue >= 0 Then
                    cmbStatus.SelectedIndex = intValue
                Else
                    cmbStatus.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）

                    txtStatusHiHi.Text = .StatusHH.Trim
                    txtStatusHi.Text = .StatusH.Trim
                    txtStatusLoLo.Text = .StatusLL.Trim
                    txtStatusLo.Text = .StatusL.Trim
                End If

                '' 2013.11.30 ノーマルレンジの固定桁数対応
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

                txtOffset.Text = .OffSet

                'Ver2.0.4.3 unitで大文字小文字区別
                intValue = fnBackCmb(cmbUnit, .Unit)
                'intValue = cmbUnit.FindStringExact(.Unit)
                If intValue >= 0 Then
                    cmbUnit.SelectedIndex = intValue
                Else
                    cmbUnit.SelectedValue = gCstCodeChManualInputUnit  ''特殊コード（手入力）
                    txtUnit.Text = .Unit
                End If

                txtString.Text = .strString
                chkCenterGraph.Checked = .FlagCenterGraph

                '' Ver1.10.1 2016.02.29 力率対応 追加
                If .FlagPowerFactor = 1 Then
                    chkPowerFactor.Checked = True
                Else
                    If .FlagPowerFactor = 2 Then
                        chkPSDisp.Checked = True
                    Else
                        'Ver2.0.7.9 AF対応追加
                        If .FlagPowerFactor = 3 Then
                            chkAFDisp.Checked = True
                        Else
                            chkPowerFactor.Checked = False
                            chkPSDisp.Checked = False
                            chkAFDisp.Checked = False
                        End If
                    End If
                End If

                '' 2013.11.30 設定値の固定桁数対応
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

                'Ver2.0.0.0 ｾﾝｻｰﾌｪｲﾙ対応
                'cmbValueSensorFailure.SelectedValue = .ValueSF
                Dim intSF As Integer = .ValueSF
                'cmbValueSensorFailure.SelectedValue = IIf(gBitCheck(intSF, 0), 1, 0)
                'chkSunder.Checked = gBitCheck(intSF, 1)
                'chkSover.Checked = gBitCheck(intSF, 2)
                'Ver2.0.1.8 ｾﾝｻﾌｪｲﾙはｺﾝﾎﾞに統一
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

                'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
                If .intAdjPSW = 1 Then
                    chkPSW.Checked = True
                Else
                    chkPSW.Checked = False
                End If

                'Ver2.0.8.5 mmHgレンジ下限小数点対応
                If .intMmHgFlg = 1 Then
                    chkMmHgFlg.Checked = True
                Else
                    chkMmHgFlg.Checked = False
                End If
                txtMmHgDec.Text = .intMmHgDec



                '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If mAnalogDetail.DummyFuAddress Then Call txtFuAdrress_KeyDown(txtFuNo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyUnitName Then Call cmbUnit_KeyDown(cmbUnit, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyStatusName Then Call cmbStatus_KeyDown(cmbValueSensorFailure, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyDelayHH Then Call objDummySetControl_KeyDown(txtDelayHiHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyValueHH Then Call objDummySetControl_KeyDown(txtValueHiHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyExtGrHH Then Call objDummySetControl_KeyDown(txtExtGHiHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep1HH Then Call objDummySetControl_KeyDown(txtGRep1HiHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep2HH Then Call objDummySetControl_KeyDown(txtGRep2HiHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyStaNmHH Then Call objDummySetControl_KeyDown(txtStatusHiHi, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyDelayH Then Call objDummySetControl_KeyDown(txtDelayHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyValueH Then Call objDummySetControl_KeyDown(txtValueHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyExtGrH Then Call objDummySetControl_KeyDown(txtExtGHi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep1H Then Call objDummySetControl_KeyDown(txtGRep1Hi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep2H Then Call objDummySetControl_KeyDown(txtGRep2Hi, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyStaNmH Then Call objDummySetControl_KeyDown(txtStatusHi, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyDelayL Then Call objDummySetControl_KeyDown(txtDelayLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyValueL Then Call objDummySetControl_KeyDown(txtValueLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyExtGrL Then Call objDummySetControl_KeyDown(txtExtGLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep1L Then Call objDummySetControl_KeyDown(txtGRep1Lo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep2L Then Call objDummySetControl_KeyDown(txtGRep2Lo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyStaNmL Then Call objDummySetControl_KeyDown(txtStatusLo, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyDelayLL Then Call objDummySetControl_KeyDown(txtDelayLoLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyValueLL Then Call objDummySetControl_KeyDown(txtValueLoLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyExtGrLL Then Call objDummySetControl_KeyDown(txtExtGLoLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep1LL Then Call objDummySetControl_KeyDown(txtGRep1LoLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep2LL Then Call objDummySetControl_KeyDown(txtGRep2LoLo, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyStaNmLL Then Call objDummySetControl_KeyDown(txtStatusLoLo, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyDelaySF Then Call objDummySetControl_KeyDown(txtDelaySensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyValueSF Then Call objDummySetControl_KeyDown(cmbValueSensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyExtGrSF Then Call objDummySetControl_KeyDown(txtExtGSensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep1SF Then Call objDummySetControl_KeyDown(txtGRep1SensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyGRep2SF Then Call objDummySetControl_KeyDown(txtGRep2SensorFailure, New KeyEventArgs(gCstDummySetKey))
                'If mAnalogDetail.DummyStaNmSF Then Call objDummySetControl_KeyDown(txtStatusSF, New KeyEventArgs(gCstDummySetKey))

                If mAnalogDetail.DummyRangeScale Then Call cmbRangeType_KeyDown(cmbRangeType, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyRangeNormalHi Then Call objDummySetControl_KeyDown(txtHighNormal, New KeyEventArgs(gCstDummySetKey))
                If mAnalogDetail.DummyRangeNormalLo Then Call objDummySetControl_KeyDown(txtLowNormal, New KeyEventArgs(gCstDummySetKey))
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
            'Ver2.0.2.4 RangeType注意表記(2線式、3線式)
            Select Case cmbDataType.SelectedValue
                Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog3Pt     ''2線式Pt, 3線式Pt
                    Call gSetComboBox(cmbRanDumy, gEnmComboType.ctChListChannelListRangeType1)
                    lblRangeCaution.Text = "[Range Type]" & vbCrLf
                    For i As Integer = 0 To cmbRanDumy.Items.Count - 1 Step 1
                        lblRangeCaution.Text = lblRangeCaution.Text & cmbRanDumy.Items.Item(i).row.itemarray(1).ToString & vbCrLf
                    Next i
                Case gCstCodeChDataTypeAnalog2Jpt, gCstCodeChDataTypeAnalog3Jpt     ''2線式JPt, 3線式JPt
                    lblRangeCaution.Text = "[Range Type]" & vbCrLf
                    Call gSetComboBox(cmbRanDumy, gEnmComboType.ctChListChannelListRangeType2)
                    For i As Integer = 0 To cmbRanDumy.Items.Count - 1 Step 1
                        lblRangeCaution.Text = lblRangeCaution.Text & cmbRanDumy.Items.Item(i).row.itemarray(1).ToString & vbCrLf
                    Next i
                Case Else
                    lblRangeCaution.Text = ""
            End Select

            ''RangeTypeコンボはDataTypeが2,3線式の場合のみ選択可
            ''その他の場合はRangeを手入力する

            '2016/10/17 T.Ueki すべて手入力に変更
            'Select Case cmbDataType.SelectedValue

            '    Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog3Pt     ''2線式Pt, 3線式Pt

            '        cmbRangeType.Visible = True
            '        cmbRangeType.Enabled = True
            '        Call gSetComboBox(cmbRangeType, gEnmComboType.ctChListChannelListRangeType1)
            '        cmbRangeType.SelectedValue = gCstCodeChRangeAnalogPt0_700.ToString '1536

            '        txtRangeFrom.Visible = False : txtRangeTo.Visible = False : lblHyphen.Visible = False
            '        cmbUnit.SelectedValue = "1"     ''温度　℃

            '    Case gCstCodeChDataTypeAnalog2Jpt, gCstCodeChDataTypeAnalog3Jpt     ''2線式JPt, 3線式JPt

            '        cmbRangeType.Visible = True
            '        cmbRangeType.Enabled = True
            '        Call gSetComboBox(cmbRangeType, gEnmComboType.ctChListChannelListRangeType2)
            '        cmbRangeType.SelectedValue = gCstCodeChRangeAnalogJpt0_200.ToString '1024

            '        txtRangeFrom.Visible = False : txtRangeTo.Visible = False : lblHyphen.Visible = False
            '        cmbUnit.SelectedValue = "1"     ''温度　℃

            '    Case Else

            '        'Case gCstCodeChDataTypeAnalogK, gCstCodeChDataTypeAnalog1_5v, gCstCodeChDataTypeAnalog4_20mA, _
            '        '     gCstCodeChDataTypeAnalogExhAve, gCstCodeChDataTypeAnalogExhRepose, gCstCodeChDataTypeAnalogExtDev ''K, 1-5V、4-20mA, Exhaust Gus

            'cmbRangeType.Visible = False
            'txtRangeFrom.Visible = False : txtRangeTo.Visible = False : lblHyphen.Visible = False

            'End Select

            cmbRangeType.Visible = False
            txtRangeFrom.Visible = True : txtRangeTo.Visible = True : lblHyphen.Visible = True

            ' ''2,3線式の場合のみ、小数点以下桁数を手入力する
            'If cmbDataType.SelectedValue >= gCstCodeChDataTypeAnalog2Pt And _
            '   cmbDataType.SelectedValue <= gCstCodeChDataTypeAnalog3Jpt Then
            '    lblDecimal.Visible = True : txtDecimal.Visible = True
            '    lblDecPoint.Visible = True
            '    Call txtDecimal_Validated(Me, New EventArgs)
            'Else
            '    lblDecimal.Visible = False : txtDecimal.Visible = False : txtDecimal.Text = ""
            '    lblDecPoint.Visible = False : lblDecPoint.Text = ""
            'End If

            ''Jacom、排ガスはFU Addressの入力は不可 ver.1.4.0 2011.09.22
            If cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogJacom Or cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogExhAve Or cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogExhRepose Or _
               cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogExtDev Or cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogJacom55 Then
                txtFuNo.Enabled = False : txtPortNo.Enabled = False : txtPin.Enabled = False
                txtFuNo.Text = "" : txtPortNo.Text = "" : txtPin.Text = ""
            Else
                txtFuNo.Enabled = True : txtPortNo.Enabled = True : txtPin.Enabled = True
            End If

            ''外部機器
            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeAnalogJacom     ''外部機器（JACOM-22）

                    cmbRangeType.Visible = False

                    cmbExtDevice.Visible = True
                    Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22AI)
                    cmbExtDevice.SelectedValue = 1

                    If chkPowerFactor.Checked = True Then
                        chkCenterGraph.Checked = True
                    End If

                Case gCstCodeChDataTypeAnalogJacom55     ''外部機器（JACOM-55）

                    cmbRangeType.Visible = False

                    cmbExtDevice.Visible = True
                    Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22AI)
                    cmbExtDevice.SelectedValue = 1

                    If chkPowerFactor.Checked = True Then
                        chkCenterGraph.Checked = True
                    End If

                Case gCstCodeChDataTypeAnalogModbus, gCstCodeChDataTypeAnalogLatitude, gCstCodeChDataTypeAnalogLongitude     ''外部機器（MODBUS） '' Ver1.11.1 2016.07.12 緯度・経度追加

                    cmbRangeType.Visible = False

                    'FUアドレスを強制0に変更し、入力不可とする
                    txtFuNo.Text = 0
                    txtFuNo.Enabled = False
                    txtPortNo.MaxLength = 2

                    '' 外部機器の場合、コンボボックス表示なし(アドレス指定) ver.1.4.0 2011.09.22
                    cmbExtDevice.Visible = False

                Case gCstCodeChDataTtpeAnalogUTCyear, gCstCodeChDataTtpeAnalogUTCmonth, gCstCodeChDataTtpeAnalogUTCday, gCstCodeChDataTtpeAnalogUTChour, gCstCodeChDataTtpeAnalogUTCmin, gCstCodeChDataTtpeAnalogUTCsec
                    'Ver2.0.1.2
                    'UTC TIMEはRANGETYPE,FU入力禁止
                    cmbRangeType.Visible = False

                    txtFuNo.Enabled = False : txtPortNo.Enabled = False : txtPin.Enabled = False
                    txtFuNo.Text = "" : txtPortNo.Text = "" : txtPin.Text = ""

                    cmbExtDevice.Visible = False
                Case Else

                    cmbExtDevice.Visible = False

            End Select

            'T.Ueki
            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeAnalogExhRepose
                    txtExtGHiHi.Enabled = False : txtDelayHiHi.Enabled = False : txtGRep1HiHi.Enabled = False : txtGRep2HiHi.Enabled = False : txtStatusHiHi.Enabled = False
                    txtExtGHi.Enabled = False : txtDelayHi.Enabled = False : txtGRep1Hi.Enabled = False : txtGRep2Hi.Enabled = False : txtStatusHi.Enabled = False
                    txtExtGLo.Enabled = False : txtDelayLo.Enabled = False : txtGRep1Lo.Enabled = False : txtGRep2Lo.Enabled = False : txtStatusLo.Enabled = False
                    txtExtGLoLo.Enabled = False : txtDelayLoLo.Enabled = False : txtGRep1LoLo.Enabled = False : txtGRep2LoLo.Enabled = False : txtStatusLoLo.Enabled = False
                    txtExtGSensorFailure.Enabled = False : txtDelaySensorFailure.Enabled = False
                    cmbValueSensorFailure.Enabled = False
                    'cmbValueSensorFailure.SelectedIndex = 1
                    cmbValueSensorFailure.SelectedValue = 0     '' Ver1.11.9.8 2016.12.15 OFF値変更

                    txtExtGHiHi.Text = "" : txtDelayHiHi.Text = "" : txtGRep1HiHi.Text = "" : txtGRep2HiHi.Text = "" : txtStatusHiHi.Text = ""
                    txtExtGHi.Text = "" : txtDelayHi.Text = "" : txtGRep1Hi.Text = "" : txtGRep2Hi.Text = "" : txtStatusHi.Text = ""
                    txtExtGLo.Text = "" : txtDelayLo.Text = "" : txtGRep1Lo.Text = "" : txtGRep2Lo.Text = "" : txtStatusLo.Text = ""
                    txtExtGLoLo.Text = "" : txtDelayLoLo.Text = "" : txtGRep1LoLo.Text = "" : txtGRep2LoLo.Text = "" : txtStatusLoLo.Text = ""
                    txtExtGSensorFailure.Text = "" : txtDelaySensorFailure.Text = ""

                Case Else

                    txtExtGHiHi.Enabled = True : txtDelayHiHi.Enabled = True : txtGRep1HiHi.Enabled = True : txtGRep2HiHi.Enabled = True : txtStatusHiHi.Enabled = True
                    txtExtGHi.Enabled = True : txtDelayHi.Enabled = True : txtGRep1Hi.Enabled = True : txtGRep2Hi.Enabled = True : txtStatusHi.Enabled = True
                    txtExtGLo.Enabled = True : txtDelayLo.Enabled = True : txtGRep1Lo.Enabled = True : txtGRep2Lo.Enabled = True : txtStatusLo.Enabled = True
                    txtExtGLoLo.Enabled = True : txtDelayLoLo.Enabled = True : txtGRep1LoLo.Enabled = True : txtGRep2LoLo.Enabled = True : txtStatusLoLo.Enabled = True

                    If cmbDataType.SelectedValue <> gCstCodeChDataTypeAnalogExhAve And cmbDataType.SelectedValue <> gCstCodeChDataTypeAnalogExtDev Then
                        txtExtGSensorFailure.Enabled = True : txtDelaySensorFailure.Enabled = True
                        cmbValueSensorFailure.Enabled = True
                    Else
                        txtExtGSensorFailure.Enabled = False : txtDelaySensorFailure.Enabled = False
                        cmbValueSensorFailure.Enabled = False

                        cmbValueSensorFailure.Enabled = False
                        'cmbValueSensorFailure.SelectedIndex = 1
                        cmbValueSensorFailure.SelectedValue = 0     '' Ver1.11.9.8 2016.12.15 OFF値変更
                    End If

            End Select

            ''偏差CH バーグラフセンター表示をONにする ver1.4.0 2011.09.20
            If cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogExtDev Then
                chkCenterGraph.Checked = True
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： レンジタイプコンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbRangeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRangeType.SelectedIndexChanged

        Try
            Call txtDecimal_Validated(Me, New EventArgs)

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

            'Ver2.0.8.5 mmHgレンジ下限小数点対応
            If cmbUnit.SelectedValue = 5 Then
                chkMmHgFlg.Enabled = True
            Else
                chkMmHgFlg.Checked = False
                chkMmHgFlg.Enabled = False
                txtMmHgDec.Text = "0"
                txtMmHgDec.Enabled = False
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
    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged

        Try

            ''手入力の場合、アラーム毎のステータス入力欄を可視にする
            If cmbStatus.SelectedValue = gCstCodeChManualInputStatus.ToString Then

                txtStatusHiHi.Visible = True
                txtStatusHi.Visible = True
                txtStatusLoLo.Visible = True
                txtStatusLo.Visible = True
                txtStatusSF.Visible = True

                lblStatus.Visible = True

            Else

                txtStatusHiHi.Visible = False
                txtStatusHi.Visible = False
                txtStatusLoLo.Visible = False
                txtStatusLo.Visible = False
                txtStatusSF.Visible = False

                lblStatus.Visible = False

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

            If cmbShareType.SelectedValue = 1 Or cmbShareType.SelectedValue = 3 Then    '■Share対応
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
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListAnalog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
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

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtOffset_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOffset.KeyPress

        Try

            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, False, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtString_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtString.KeyPress, txtDecimal.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1,2,3,4,5,6,7,8")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                        txtValueHiHi.KeyPress, txtValueHi.KeyPress, txtValueLo.KeyPress, txtValueLoLo.KeyPress

        Try

            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtDelayHiHi.KeyPress, txtDelayHi.KeyPress, txtDelayLo.KeyPress, txtDelayLoLo.KeyPress, txtDelaySensorFailure.KeyPress

        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub txtGroupNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                Handles txtPin.KeyPress, _
                        txtExtGHiHi.KeyPress, txtExtGHi.KeyPress, txtExtGLo.KeyPress, txtExtGLoLo.KeyPress, txtExtGSensorFailure.KeyPress, _
                        txtGRep1HiHi.KeyPress, txtGRep1Hi.KeyPress, txtGRep1Lo.KeyPress, txtGRep1LoLo.KeyPress, _
                        txtGRep2HiHi.KeyPress, txtGRep2Hi.KeyPress, txtGRep2Lo.KeyPress, txtGRep2LoLo.KeyPress

        Try
            'T.Ueki
            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeAnalogJacom     ''外部機器（JACOM-22）
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                Case gCstCodeChDataTypeAnalogJacom55     ''外部機器（JACOM-55）
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                Case gCstCodeChDataTypeAnalogModbus, gCstCodeChDataTypeAnalogLatitude, gCstCodeChDataTypeAnalogLongitude     ''外部機器（MODBUS） '' Ver1.11.1 2016.07.12 緯度・経度追加
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)
                Case Else
                    e.Handled = gCheckTextInput(2, sender, e.KeyChar)

            End Select

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

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress

        Try
            '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(2, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9,51,52") ''2019　3/28 二桁の数字を入力できるよう変更

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnit.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatus.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusAlarm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusHiHi.KeyPress, _
                                                                                                                           txtStatusHi.KeyPress, _
                                                                                                                           txtStatusLoLo.KeyPress, _
                                                                                                                           txtStatusLo.KeyPress, _
                                                                                                                           txtStatusSF.KeyPress
        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtRangeFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRangeFrom.KeyPress, _
                                                                                                                         txtRangeTo.KeyPress, _
                                                                                                                         txtHighNormal.KeyPress, _
                                                                                                                         txtLowNormal.KeyPress

        Try
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)

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

    'Ver2.0.8.5 mmHgレンジ下限小数点対応
    Private Sub txtMmHgDec_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtMmHgDec.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1,2,3,4,5,6,7,8")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            If IsNumeric(txtChNo.Text) Then
                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")
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
    ' 機能説明  ： 計測点番号をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtPin_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPin.Validated

        Try


            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeAnalogJacom     ''外部機器（JACOM-22）
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case gCstCodeChDataTypeAnalogJacom55     ''外部機器（JACOM-55）
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case gCstCodeChDataTypeAnalogModbus, gCstCodeChDataTypeAnalogLatitude, gCstCodeChDataTypeAnalogLongitude     ''外部機器（MODBUS） '' Ver1.11.1 2016.07.12 緯度・経度追加
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case Else
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")
                    ''FU Address を設定した際に、Data Typeを自動設定する
                    Call mSetDataType()
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFuNo.Validated

        Try
            ''FU Address を設定した際に、Data Typeを自動設定する
            Call mSetDataType()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub txtPortNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPortNo.Validated

        Try
            ''FU Address を設定した際に、Data Typeを自動設定する
            Call mSetDataType()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 小数点以下桁数に対応するサンプルを表示する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtDecimal_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDecimal.Validated

        Try
            Dim intDecimalP As Integer = CCInt(txtDecimal.Text)
            Dim strRange As String = cmbRangeType.Text
            Dim strValue As String = "", strDecimalFormat As String
            Dim p As Integer, dblValue As Double

            If strRange <> "" Then
                strRange = strRange.Replace(" - ", "/")
                p = strRange.LastIndexOf("/")

                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                dblValue = Long.Parse(strRange.Substring(0, p))
                strValue = dblValue.ToString(strDecimalFormat)

                dblValue = Long.Parse(strRange.Substring(p + 1))
                strValue += " - " & dblValue.ToString(strDecimalFormat)

                lblDecPoint.Text = strValue
            End If

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
    'Ver2.0.2.6 アナログCHのステータスとセット値関係処理関数
    Private Function fnAnalogStatusAlarmRel(pstrValue As String) As Integer
        Dim intValue As Integer = 0
        Dim strCboText As String = ""
        Dim strStatusS() As String

        Dim strHHstatus As String = ""
        Dim strHstatus As String = ""
        Dim strLstatus As String = ""
        Dim strLLstatus As String = ""

        '>>>ステータスの格納
        'ｽﾃｰﾀｽ名称を取得
        Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
        intValue = cmbStatus.FindStringExact(pstrValue)
        cmbStatus.SelectedIndex = intValue
        strCboText = cmbStatus.Text

        '「*」ならそのまま戻る
        If strCboText = "*" Then
            Return 0
        End If

        'ｽﾃｰﾀｽを「/」で区切る
        strStatusS = strCboText.Split("/")

        'STATUSを該当箇所へ格納
        For i As Integer = 0 To UBound(strStatusS) Step 1
            'Ver2.0.8.2 保安庁対応
            Select Case strStatusS(i)
                Case "HH", "高高"
                    strHHstatus = strStatusS(i)
                Case "HIGH", "H", "EH", "高"
                    strHstatus = strStatusS(i)
                Case "LOW", "L", "EL", "低"
                    strLstatus = strStatusS(i)
                Case "LL", "低低"
                    strLLstatus = strStatusS(i)
            End Select
        Next i

        '>>>アラームの有り無し格納
        Dim blHHuse As Boolean = False
        Dim blHuse As Boolean = False
        Dim blLuse As Boolean = False
        Dim blLLuse As Boolean = False

        Dim strEx As String = ""
        Dim strDly As String = ""
        Dim strGR1 As String = ""
        Dim strGR2 As String = ""

        With mAnalogDetail
            '>>HH
            strEx = NZf(.ExtGHH)
            strDly = NZf(.DelayHH)
            strGR1 = NZf(.GRep1HH)
            strGR2 = NZf(.GRep2HH)
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHHuse = True
            End If
            '>>H
            strEx = NZf(.ExtGH)
            strDly = NZf(.DelayH)
            strGR1 = NZf(.GRep1H)
            strGR2 = NZf(.GRep2H)
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHuse = True
            End If
            '>>L
            strEx = NZf(.ExtGL)
            strDly = NZf(.DelayL)
            strGR1 = NZf(.GRep1L)
            strGR2 = NZf(.GRep2L)
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLuse = True
            End If
            '>>LL
            strEx = NZf(.ExtGLL)
            strDly = NZf(.DelayLL)
            strGR1 = NZf(.GRep1LL)
            strGR2 = NZf(.GRep2LL)
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLLuse = True
            End If
        End With


        '>>>判定
        'ｱﾗｰﾑ値が全て無いなら、そのまま
        If blHHuse = False And blHuse = False And blLuse = False And blLLuse = False Then
            Return 0
        End If

        '1件でも矛盾があれば-1で戻る
        '>>HH
        If strHHstatus = "" And blHHuse = True Then
            Return -1
        End If
        If strHHstatus <> "" And blHHuse = False Then
            Return -1
        End If
        '>>H
        If strHstatus = "" And blHuse = True Then
            Return -1
        End If
        If strHstatus <> "" And blHuse = False Then
            Return -1
        End If
        '>>L
        If strLstatus = "" And blLuse = True Then
            Return -1
        End If
        If strLstatus <> "" And blLuse = False Then
            Return -1
        End If
        '>>LL
        If strLLstatus = "" And blLLuse = True Then
            Return -1
        End If
        If strLLstatus <> "" And blLLuse = False Then
            Return -1
        End If


        Return 0
    End Function


    '--------------------------------------------------------------------
    ' 機能      : 設定値GET
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面の設定値を内部メモリに取り込む
    '--------------------------------------------------------------------
    Private Sub mGetSetData()

        Try

            Dim txtRangeToLen As Integer
            Dim txtLen As Integer

            With mAnalogDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTag.Text)    '' 2015.10.26  Ver1.7.4  ﾀｸﾞ追加
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If cmbShareType.Enabled = True Then
                    .ShareType = cmbShareType.SelectedValue
                    .ShareChNo = IIf(txtShareChid.Text = "", Nothing, txtShareChid.Text)
                End If

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

                .DataType = cmbDataType.SelectedValue

                .FuNo = Trim(txtFuNo.Text)
                .FUPortNo = Trim(txtPortNo.Text)

                If .DataType = gCstCodeChDataTypeAnalogJacom Or .DataType = gCstCodeChDataTypeAnalogJacom55 Then
                    .FUPin = cmbExtDevice.SelectedValue
                Else
                    .FUPin = Trim(txtPin.Text)
                End If

                'JACOM55は外部機器51固定
                If .DataType = gCstCodeChDataTypeAnalogJacom55 Then
                    .FuNo = 0
                    .FUPortNo = 51
                End If


                '' Ver1.11.0 2016.07.07 緯度・経度CHは通信CHのため、FUNo.は0をｾｯﾄ
                If ((.DataType = gCstCodeChDataTypeAnalogLatitude) Or (.DataType = gCstCodeChDataTypeAnalogLongitude)) Then
                    If ((.FUPortNo <> "0") And (.FUPin <> "0") And (.FUPortNo <> "") And (.FUPin <> "")) Then
                        .FuNo = "0"
                    End If
                End If
                ''//

                .RangeType = cmbRangeType.SelectedValue

                '現在設定されているレンジの少数点桁数位置を把握
                Dim intDecPointFrom As Integer
                Dim intDecPointTo As Integer

                '下限設定値txtRangeFrom.textの中に小数点が何桁目にあるか確認
                intDecPointFrom = intDecimalPointSch(Trim(txtRangeFrom.Text))

                '上限設定値txtRangeTo.textの中に小数点が何桁目にあるか確認
                intDecPointTo = intDecimalPointSch(Trim(txtRangeTo.Text))

                '下限と上限値の小数点位置確認(異なる場合は桁数が多いほうを設定)
                If intDecPointFrom <> intDecPointTo Then
                    If intDecPointFrom < intDecPointTo Then
                        'Decimal Point(display2)
                        .DecimalPosition = Str(intDecPointTo)
                    Else
                        .DecimalPosition = Str(intDecPointFrom)
                    End If
                Else
                    'Decimal Point(display2)
                    .DecimalPosition = Str(intDecPointFrom)
                End If


                '上限設定値txtRangeto.textの中に小数点があるか確認
                txtRangeToLen = Len(txtRangeTo.Text)

                If intDecPointFrom = 0 Then
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

                ''K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器
                '' Ver1.11.1 2016.07.12 緯度・経度追加
                'If .DataType = gCstCodeChDataTypeAnalogK Or .DataType = gCstCodeChDataTypeAnalog1_5v Or _
                '    .DataType = gCstCodeChDataTypeAnalog4_20mA Or .DataType = gCstCodeChDataTypeAnalogPT4_20mA Or _
                '    .DataType = gCstCodeChDataTypeAnalogExhAve Or _
                '    .DataType = gCstCodeChDataTypeAnalogExhRepose Or .DataType = gCstCodeChDataTypeAnalogExtDev Or _
                '    .DataType = gCstCodeChDataTypeAnalogJacom Or .DataType = gCstCodeChDataTypeAnalogModbus Or _
                '    .DataType = gCstCodeChDataTypeAnalogLatitude Or .DataType = gCstCodeChDataTypeAnalogLongitude Then

                '    .RangeFrom = txtRangeFrom.Text
                '    .RangeTo = Mid(txtRangeTo.Text, 1, txtRangeToLen - Val(txtString.Text))

                'End If

                If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    .Status = cmbStatus.Text
                Else
                    'Ver2.0.1.2 手入力ｽﾃｰﾀｽの場合、その値編集してをｽﾃｰﾀｽへ表示
                    Dim strManuStatus As String = ""
                    strManuStatus = fnGetManuSTATUS_4(txtStatusLoLo.Text, txtStatusLo.Text, txtStatusHi.Text, txtStatusHiHi.Text)
                    .Status = strManuStatus
                    '.Status = ""

                    .StatusHH = txtStatusHiHi.Text.Trim
                    .StatusH = txtStatusHi.Text.Trim
                    .StatusL = txtStatusLo.Text.Trim
                    .StatusLL = txtStatusLoLo.Text.Trim
                End If

                .PortNo = cmbExtDevice.SelectedValue

                '' 2013.11.30 ノーマルレンジの固定桁数対応
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

                .OffSet = txtOffset.Text

                If cmbUnit.SelectedValue <> gCstCodeChManualInputUnit.ToString Then
                    .Unit = cmbUnit.Text
                Else
                    .Unit = txtUnit.Text
                End If

                '.strString = txtString.Text
                .FlagCenterGraph = chkCenterGraph.Checked

                '' Ver1.10.1 2016.02.29 力率対応 追加
                If chkPowerFactor.Checked = True Then
                    .FlagPowerFactor = 1
                Else
                    If chkPSDisp.Checked = True Then
                        .FlagPowerFactor = 2
                    Else
                        'Ver2.0.7.9 AF対応
                        If chkAFDisp.Checked = True Then
                            .FlagPowerFactor = 3
                        Else
                            .FlagPowerFactor = 0
                        End If
                    End If
                End If

                '' 2013.11.30 設定値の固定桁数対応
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


                ''Ver2.0.0.0 2016.12.06 ｾﾝｻｰﾌｪｲﾙ対応
                ''.ValueSF = cmbValueSensorFailure.SelectedValue
                'Dim intSF As Integer = 0
                'If cmbValueSensorFailure.SelectedValue = 0 Then
                '    intSF = gBitSet(intSF, 0, False)
                'Else
                '    intSF = gBitSet(intSF, 0, True)
                'End If
                'intSF = gBitSet(intSF, 1, chkSunder.Checked)
                'intSF = gBitSet(intSF, 2, chkSover.Checked)
                '.ValueSF = intSF
                'Ver1.11.9.8 2016.12.15 ｺﾝﾎﾞﾎﾞｯｸｽからの選択に変更
                .ValueSF = cmbValueSensorFailure.SelectedValue




                .ExtGHH = txtExtGHiHi.Text
                .ExtGH = txtExtGHi.Text
                .ExtGL = txtExtGLo.Text
                .ExtGLL = txtExtGLoLo.Text
                .ExtGSF = txtExtGSensorFailure.Text

                .DelayHH = txtDelayHiHi.Text
                .DelayH = txtDelayHi.Text
                .DelayL = txtDelayLo.Text
                .DelayLL = txtDelayLoLo.Text
                .DelaySF = txtDelaySensorFailure.Text

                .GRep1HH = txtGRep1HiHi.Text
                .GRep1H = txtGRep1Hi.Text
                .GRep1L = txtGRep1Lo.Text
                .GRep1LL = txtGRep1LoLo.Text

                'T.Ueki
                .GRep1SF = txtGRep1SensorFailure.Text

                .GRep2HH = txtGRep2HiHi.Text
                .GRep2H = txtGRep2Hi.Text
                .GRep2L = txtGRep2Lo.Text
                .GRep2LL = txtGRep2LoLo.Text

                'T.Ueki
                .GRep2SF = txtGRep2SensorFailure.Text


                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text

                'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
                If chkPSW.Checked = True Then
                    .intAdjPSW = 1
                Else
                    .intAdjPSW = 0
                End If

                'Ver2.0.8.5 mmHgレンジ下限小数点対応
                If chkMmHgFlg.Checked = True Then
                    .intMmHgFlg = 1
                    .intMmHgDec = NZfZero(txtMmHgDec.Text)
                Else
                    .intMmHgFlg = 0
                    .intMmHgDec = 0
                End If


                '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If .DataType = gCstCodeChDataTypeAnalogModbus Or .DataType = gCstCodeChDataTypeAnalogLatitude Or .DataType = gCstCodeChDataTypeAnalogLongitude Then      '' Ver1.8.9 2015.12.11 通信CHの場合はﾎﾟｰﾄ番号を参照  '' Ver1.11.1 2016.07.12 緯度・経度追加
                    .DummyFuAddress = gDummyCheckControl(txtPortNo)
                Else
                    .DummyFuAddress = gDummyCheckControl(txtFuNo)
                End If

                .DummyUnitName = gDummyCheckControl(cmbUnit)
                .DummyStatusName = gDummyCheckControl(cmbStatus)

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
                '.DummyStaNmSF = gDummyCheckControl(txtStatusSF)

                .DummyRangeScale = gDummyCheckControl(cmbRangeType)
                .DummyRangeNormalHi = gDummyCheckControl(txtHighNormal)
                .DummyRangeNormalLo = gDummyCheckControl(txtLowNormal)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FU Address を設定した際に、Data Typeを自動設定する
    ' 返り値    : True:変更有り、False:変更なし
    ' 引き数    : なし
    ' 機能説明  : 基盤設定がされている場合のみ
    '--------------------------------------------------------------------
    Private Function mSetDataType() As Boolean

        Try
            Dim strFuAddress As String = txtFuNo.Text & txtPortNo.Text & txtPin.Text
            Dim intFuNo As Integer, intPortNo As Integer, intPinNo As Integer
            Dim intDataType As Integer
            Dim intSlotType As Integer
            Dim blnRet As Boolean = False

            If strFuAddress = "" Then Return False

            If strFuAddress.Length >= 4 Then

                intFuNo = Val(txtFuNo.Text)
                intPortNo = Val(txtPortNo.Text)
                intPinNo = Val(txtPin.Text)

                If intFuNo > 20 Or intPortNo = 0 Or intPinNo = 0 Then
                    blnRet = False 'NG
                Else
                    blnRet = True  'OK
                End If

            End If

            If blnRet = True Then

                intDataType = cmbDataType.SelectedValue     ''現設定のData Type

                If intDataType = gCstCodeChDataTypeAnalogJacom Or intDataType = gCstCodeChDataTypeAnalogModbus Or _
                    intDataType = gCstCodeChDataTypeAnalogLatitude Or intDataType = gCstCodeChDataTypeAnalogLongitude Or _
                    intDataType = gCstCodeChDataTypeAnalogJacom55 Then  '' 通信アドレスは除く    2014.12.01      '' Ver1.11.1 2016.07.12  緯度・経度CH
                    Return blnRet
                End If

                'Ver2.0.1.2 UTC TIMEも処理抜け
                Select Case intDataType
                    Case gCstCodeChDataTtpeAnalogUTCyear, gCstCodeChDataTtpeAnalogUTCmonth, gCstCodeChDataTtpeAnalogUTCday, gCstCodeChDataTtpeAnalogUTChour, gCstCodeChDataTtpeAnalogUTCmin, gCstCodeChDataTtpeAnalogUTCsec
                        Return blnRet
                End Select

                ''FU使用/未使用フラグ
                If gudt.SetFu.udtFu(intFuNo).shtUse = 1 Then

                    ''スロット種別 GET
                    intSlotType = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortNo - 1).shtType

                    Select Case intSlotType

                        Case gCstCodeFuSlotTypeAI_2
                            If intDataType = gCstCodeChDataTypeAnalog2Pt Or _
                               intDataType = gCstCodeChDataTypeAnalog2Jpt Then
                            Else

                                If MessageBox.Show("The FU address was changed." & vbNewLine & _
                                                   "May I change to the Data Type that exists in Analog Board Number?", _
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    cmbDataType.SelectedValue = gCstCodeChDataTypeAnalog2Pt
                                End If

                            End If

                        Case gCstCodeFuSlotTypeAI_3
                            If intDataType = gCstCodeChDataTypeAnalog3Pt Or _
                               intDataType = gCstCodeChDataTypeAnalog3Jpt Then
                            Else
                                If MessageBox.Show("The FU address was changed." & vbNewLine & _
                                                   "May I change to the Data Type that exists in Analog Board Number?", _
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    cmbDataType.SelectedValue = gCstCodeChDataTypeAnalog3Pt
                                End If
                            End If

                        Case gCstCodeFuSlotTypeAI_1_5
                            If intDataType = gCstCodeChDataTypeAnalog1_5v Then
                            Else
                                If MessageBox.Show("The FU address was changed." & vbNewLine & _
                                                   "May I change to the Data Type that exists in Analog Board Number?", _
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    cmbDataType.SelectedValue = gCstCodeChDataTypeAnalog1_5v
                                End If
                            End If

                        Case gCstCodeFuSlotTypeAI_4_20
                            If intDataType = gCstCodeChDataTypeAnalog4_20mA Or intDataType = gCstCodeChDataTypeAnalogPT4_20mA Then
                            Else
                                If MessageBox.Show("The FU address was changed." & vbNewLine & _
                                                   "May I change to the Data Type that exists in Analog Board Number?", _
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    cmbDataType.SelectedValue = gCstCodeChDataTypeAnalog4_20mA
                                End If
                            End If

                        Case gCstCodeFuSlotTypeAI_K
                            If intDataType = gCstCodeChDataTypeAnalogK Then
                            Else
                                If MessageBox.Show("The FU address was changed." & vbNewLine & _
                                                   "May I change to the Data Type that exists in Analog Board Number?", _
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogK
                                End If
                            End If

                        Case Else


                    End Select

                End If

            End If

            Return blnRet

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

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
            If Not gChkInputText(txtUnit, "Unit", True, True) Then Return False

            If Not gChkInputText(txtStatusHiHi, "Status HIHI", True, True) Then Return False
            If Not gChkInputText(txtStatusHi, "Status HI", True, True) Then Return False
            If Not gChkInputText(txtStatusLo, "Status LO", True, True) Then Return False
            If Not gChkInputText(txtStatusLoLo, "Status LOLO", True, True) Then Return False

            If ChkTagInput(txtTag.Text) = False Then Return False '' 2015.10.27 Ver1.7.5

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
            If Not gChkInputNum(txtPLC, 0, 1, "PLC", True, True) Then Return False '' 2014.11.18
            If Not gChkInputNum(txtSP, 0, 1, "SP", True, True) Then Return False
            If Not gChkInputNum(txtRangeFrom, -99999999, 999999999, "Range From", True, True) Then Return False
            If Not gChkInputNum(txtRangeTo, -99999999, 999999999, "Range To", True, True) Then Return False
            If Not gChkInputNum(txtDecimal, 0, 8, "Decimal Position", True, True) Then Return False
            If Not gChkInputNum(txtHighNormal, -99999999, 999999999, "HI Normal", True, True) Then Return False
            If Not gChkInputNum(txtLowNormal, -99999999, 999999999, "LO Normal", True, True) Then Return False
            If Not gChkInputNum(txtString, 0, 8, "String", True, True) Then Return False
            If Not gChkInputNum(txtOffset, -99999999, 999999999, "Offset", True, True) Then Return False
            If Not gChkInputNum(txtValueHiHi, -99999999, 999999999, "Value HIHI", True, True) Then Return False
            If Not gChkInputNum(txtValueHi, -99999999, 999999999, "Value HI", True, True) Then Return False
            If Not gChkInputNum(txtValueLoLo, -99999999, 999999999, "Value LOLO", True, True) Then Return False
            If Not gChkInputNum(txtValueLo, -99999999, 999999999, "Value LO", True, True) Then Return False
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
            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

            If Not gChkInputNum(txtDelayHiHi, 0, 240, "Delay HIHI", True, True) Then Return False
            If Not gChkInputNum(txtDelayHi, 0, 240, "Delay HI", True, True) Then Return False
            If Not gChkInputNum(txtDelayLo, 0, 240, "Delay LO", True, True) Then Return False
            If Not gChkInputNum(txtDelayLoLo, 0, 240, "Delay LOLO", True, True) Then Return False
            If Not gChkInputNum(txtDelaySensorFailure, 0, 240, "Delay SF", True, True) Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If


            ''共通FUアドレス入力チェック
            'T.Ueki
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''Select Case cmbDataType.SelectedValue

            ''    Case gCstCodeChDataTypeAnalogJacom     ''外部機器（JACOM-22）
            ''        If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 65535, True, True) Then Return False

            ''    Case gCstCodeChDataTypeAnalogModbus     ''外部機器（MODBUS）
            ''        '' Ver1.9.7 通信の場合はﾁｪｯｸしない
            ''        ''If gDummyCheckControl(txtPortNo) = False Then       '' Ver1.8.9 2015.12.11  ﾀﾞﾐｰでない場合はﾁｪｯｸする
            ''        ''    If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 65535, True, True) Then Return False
            ''        ''End If

            ''    Case Else
            ''        If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 16, True, True) Then Return False

            ''End Select

            'If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 16, True, True) Then Return False

            'AlarmSet T.Ueki
            If txtExtGHiHi.Text = "" Then
                If txtGRep1HiHi.Text <> "" Or txtGRep2HiHi.Text <> "" Or txtDelayHiHi.Text <> "" Then
                    MsgBox("設定値を削除して下さい。", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            ''Range 
            If txtRangeFrom.Text = "" And txtRangeTo.Text = "" Then
            ElseIf txtRangeFrom.Text <> "" And txtRangeTo.Text <> "" Then

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
            If txtLowNormal.Text = "" And txtHighNormal.Text = "" Then
            ElseIf txtLowNormal.Text <> "" And txtHighNormal.Text <> "" Then

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

            ''Range, Normal, Valueの桁あふれを防ぐためのチェック -------------------------------------------------
            Dim dblValue As Double
            Dim lngValue As Long
            Dim intDecimalP As Integer, p As Integer
            Dim intDataType As Integer = cmbDataType.SelectedValue

            '' Ver1.11.1 2016.07.12  緯度・経度CH追加
            If intDataType = gCstCodeChDataTypeAnalogK Or intDataType = gCstCodeChDataTypeAnalog1_5v Or _
               intDataType = gCstCodeChDataTypeAnalog4_20mA Or intDataType = gCstCodeChDataTypeAnalogPT4_20mA Or _
               intDataType = gCstCodeChDataTypeAnalogExhAve Or _
               intDataType = gCstCodeChDataTypeAnalogExhRepose Or intDataType = gCstCodeChDataTypeAnalogExtDev Or _
               intDataType = gCstCodeChDataTypeAnalogJacom Or intDataType = gCstCodeChDataTypeAnalogModbus Or _
               intDataType = gCstCodeChDataTypeAnalogLatitude Or intDataType = gCstCodeChDataTypeAnalogLongitude Or _
               intDataType = gCstCodeChDataTtpeAnalogUTCyear Or intDataType = gCstCodeChDataTtpeAnalogUTCmonth Or _
               intDataType = gCstCodeChDataTtpeAnalogUTCday Or intDataType = gCstCodeChDataTtpeAnalogUTChour Or _
               intDataType = gCstCodeChDataTtpeAnalogUTCmin Or intDataType = gCstCodeChDataTtpeAnalogUTCsec Or _
               intDataType = gCstCodeChDataTypeAnalogJacom55 Then
                'Ver2.0.1.2 UTC TIME追加

                ''K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器　→　レンジは手入力、小数点以下桁数はレンジの値からGET

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

            Else
                ''2,3線式　→　レンジは選択、小数点以下桁数は手入力

                intDecimalP = CCInt(txtDecimal.Text)

                Dim strRange As String = cmbRangeType.Text
                If strRange <> "" Then

                    strRange = strRange.Replace(" - ", "/")
                    p = strRange.LastIndexOf("/")

                    ''Range from
                    lngValue = Long.Parse(strRange.Substring(0, p))
                    ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                    lngValue = Int(lngValue * (10 ^ intDecimalP) + 0.5)

                    If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                        MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                        Return False
                    End If

                    ''Range to
                    lngValue = Long.Parse(strRange.Substring(p + 1))
                    ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                    lngValue = Int(lngValue * (10 ^ intDecimalP) + 0.5)

                    If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                        MsgBox("Range is wrong.", MsgBoxStyle.Exclamation, "Input error")
                        Return False
                    End If

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

            'Ver2.0.1.2 UTC TIMEの場合WKフラグを立てる
            If intDataType = gCstCodeChDataTtpeAnalogUTCyear Or intDataType = gCstCodeChDataTtpeAnalogUTCmonth Or _
               intDataType = gCstCodeChDataTtpeAnalogUTCday Or intDataType = gCstCodeChDataTtpeAnalogUTChour Or _
               intDataType = gCstCodeChDataTtpeAnalogUTCmin Or intDataType = gCstCodeChDataTtpeAnalogUTCsec Then
                txtWK.Text = "1"
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

    '----------------------------------------------------------------------------
    ' 機能説明  ： 画面初期化
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitial()

        Try

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)
            Call gSetComboBox(cmbValueSensorFailure, gEnmComboType.ctChListChannelListSF)
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeAnalog)

            ''CommonのAlarmは使用不可
            fraAlarm.Enabled = False

            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)

            Call gSetComboBox(cmbAlmLvl, gEnmComboType.ctChListChannelListAlmLevel)       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

            'Ver2.0.0.8
            'TagNoはﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then
                txtTag.Enabled = False
            End If

            'Ver2.0.0.9
            'Alarm Levelは、ﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                cmbAlmLvl.SelectedIndex = 0
                cmbAlmLvl.Enabled = False
            End If

            'Ver2.0.2.4 RangeType注意表記初期化
            lblRangeCaution.Text = ""

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
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mAnalogInfo, _
                                         ByVal udt2 As frmChListChannelList.mAnalogInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.26  Ver1.7.4  ﾀｸﾞ追加
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
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

            'T.ueki
            If udt1.GRep1SF <> udt2.GRep1SF Then Return False

            'T.ueki
            If udt1.GRep2SF <> udt2.GRep2SF Then Return False

            If udt1.GRep2HH <> udt2.GRep2HH Then Return False
            If udt1.GRep2H <> udt2.GRep2H Then Return False
            If udt1.GRep2L <> udt2.GRep2L Then Return False
            If udt1.GRep2LL <> udt2.GRep2LL Then Return False
            If udt1.StatusHH <> udt2.StatusHH Then Return False
            If udt1.StatusH <> udt2.StatusH Then Return False
            If udt1.StatusL <> udt2.StatusL Then Return False
            If udt1.StatusLL <> udt2.StatusLL Then Return False
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
            If udt1.FuNo <> udt2.FuNo Then Return False
            If udt1.FUPortNo <> udt2.FUPortNo Then Return False
            If udt1.FUPin <> udt2.FUPin Then Return False
            If udt1.DataType <> udt2.DataType Then Return False
            If udt1.PortNo <> udt2.PortNo Then Return False
            If udt1.RangeType <> udt2.RangeType Then Return False
            If udt1.DecimalPosition <> udt2.DecimalPosition Then Return False
            If udt1.RangeFrom <> udt2.RangeFrom Then Return False
            If udt1.RangeTo <> udt2.RangeTo Then Return False
            If udt1.Status <> udt2.Status Then Return False
            If udt1.NormalLO <> udt2.NormalLO Then Return False
            If udt1.NormalHI <> udt2.NormalHI Then Return False
            If udt1.OffSet <> udt2.OffSet Then Return False
            If udt1.Unit <> udt2.Unit Then Return False
            If udt1.strString <> udt2.strString Then Return False
            If udt1.FlagCenterGraph <> udt2.FlagCenterGraph Then Return False
            If udt1.FlagPowerFactor <> udt2.FlagPowerFactor Then Return False '' Ver1.10.1 2016.02.29 力率対応 追加
            If udt1.FlagPSDisp <> udt2.FlagPSDisp Then Return False '' Ver1.11.9.3 2916.11.26  P/S表示追加
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False

            'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
            If udt1.intAdjPSW <> udt2.intAdjPSW Then Return False

            'Ver2.0.8.5 mmHgレンジ下限小数点対応
            If udt1.intMmHgFlg <> udt2.intMmHgFlg Then Return False
            If udt1.intMmHgDec <> udt2.intMmHgDec Then Return False


            '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
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
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "仮設定関連"

    Private Sub objDummySetControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
        txtDelayHiHi.KeyDown, txtDelayHi.KeyDown, txtDelayLo.KeyDown, txtDelayLoLo.KeyDown, txtDelaySensorFailure.KeyDown, _
        txtValueHiHi.KeyDown, txtValueHi.KeyDown, txtValueLo.KeyDown, txtValueLoLo.KeyDown, cmbValueSensorFailure.KeyDown, _
        txtExtGHiHi.KeyDown, txtExtGHi.KeyDown, txtExtGLo.KeyDown, txtExtGLoLo.KeyDown, txtExtGSensorFailure.KeyDown, _
        txtGRep1HiHi.KeyDown, txtGRep1Hi.KeyDown, txtGRep1Lo.KeyDown, txtGRep1LoLo.KeyDown, txtGRep1SensorFailure.KeyDown, _
        txtGRep2HiHi.KeyDown, txtGRep2Hi.KeyDown, txtGRep2Lo.KeyDown, txtGRep2LoLo.KeyDown, txtGRep2SensorFailure.KeyDown, _
        txtStatusHiHi.KeyDown, txtStatusHi.KeyDown, txtStatusLo.KeyDown, txtStatusLoLo.KeyDown, txtStatusSF.KeyDown, _
        txtHighNormal.KeyDown, txtLowNormal.KeyDown

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

    Private Sub cmbStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatus.KeyDown, _
                                                                                                                txtStatus.KeyDown
        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbStatus)
                Call gDummySetColorChange(txtStatus)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbRangeType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbRangeType.KeyDown, txtRangeFrom.KeyDown, txtRangeTo.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbRangeType)
                Call gDummySetColorChange(txtRangeFrom)
                Call gDummySetColorChange(txtRangeTo)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNo.KeyDown, txtPortNo.KeyDown, txtPin.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtFuNo)
                Call gDummySetColorChange(txtPortNo)
                Call gDummySetColorChange(txtPin)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コメントアウト"

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 設定単位 コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub cmbTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTime.SelectedIndexChanged

    '    If mintDelayTimeKubun <> cmbTime.SelectedValue Then

    '        If cmbTime.SelectedValue = 0 Then
    '            ''分 -- > 秒
    '            If txtDelayHiHi.Text <> "" Then txtDelayHiHi.Text = Format(CCDouble(txtDelayHiHi.Text) * 60)
    '            If txtDelayHi.Text <> "" Then txtDelayHi.Text = Format(CCDouble(txtDelayHi.Text) * 60)
    '            If txtDelayLo.Text <> "" Then txtDelayLo.Text = Format(CCDouble(txtDelayLo.Text) * 60)
    '            If txtDelayLoLo.Text <> "" Then txtDelayLoLo.Text = Format(CCDouble(txtDelayLoLo.Text) * 60)
    '        Else
    '            ''秒 --> 分
    '            If txtDelayHiHi.Text <> "" Then txtDelayHiHi.Text = Format(CCDouble(txtDelayHiHi.Text) / 60, "0.0")
    '            If txtDelayHi.Text <> "" Then txtDelayHi.Text = Format(CCDouble(txtDelayHi.Text) / 60, "0.0")
    '            If txtDelayLo.Text <> "" Then txtDelayLo.Text = Format(CCDouble(txtDelayLo.Text) / 60, "0.0")
    '            If txtDelayLoLo.Text <> "" Then txtDelayLoLo.Text = Format(CCDouble(txtDelayLoLo.Text) / 60, "0.0")
    '        End If

    '    End If

    '    mintDelayTimeKubun = cmbTime.SelectedValue

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtDelay_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
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

    '--------------------------------------------------------------------
    ' 機能      : データ変更チェック
    ' 返り値    : True:変更有り、False:変更なし
    ' 引き数    : なし
    ' 機能説明  : データが変更されているかチェックを行う
    '--------------------------------------------------------------------
    'Private Function mChkDataChange() As Boolean

    '    Dim strValue As String = ""

    '    With mAnalogDetail

    '        If .SysNo <> cmbSysNo.SelectedValue Then Return True
    '        If .ChNo <> txtChNo.Text Then Return True
    '        If .ItemName <> txtItemName.Text Then Return True
    '        If .Remarks <> txtRemarks.Text Then Return True

    '        If cmbShareType.Enabled = True Then
    '            If .ShareType <> cmbShareType.SelectedValue Then Return True
    '            If .ShareChNo <> txtShareChid.Text Then Return True
    '        End If

    '        'If .FlagMrepose <> chkMrepose.Checked Then Return True

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

    '        If .FuNo <> txtFuNo.Text Then Return True
    '        If .FUPortNo <> txtPortNo.Text Then Return True
    '        If .FUPin <> txtPin.Text Then Return True

    '        If .DataType <> cmbDataType.SelectedValue Then Return True
    '        If .RangeType <> cmbRangeType.SelectedValue Then Return True

    '        If .DataType = gCstCodeChDataTypeAnalogK Or .DataType = gCstCodeChDataTypeAnalog1_5v Or _
    '           .DataType = gCstCodeChDataTypeAnalog4_20mA Then    ''K, 1-5 V, 4-20 mA
    '            If .RangeFrom <> txtRangeFrom.Text Then Return True
    '            If .RangeTo <> txtRangeTo.Text Then Return True
    '        End If

    '        If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '            If .Status <> cmbStatus.Text Then Return True
    '        Else
    '            If .StatusHH <> txtStatusHiHi.Text.Trim.PadRight(8) Then Return True
    '            If .StatusH <> txtStatusHi.Text.Trim.PadRight(8) Then Return True
    '            If .StatusL <> txtStatusLo.Text.Trim.PadRight(8) Then Return True
    '            If .StatusLL <> txtStatusLoLo.Text.Trim.PadRight(8) Then Return True
    '            If .StatusSF <> txtStatusSF.Text.Trim.PadRight(8) Then Return True
    '        End If

    '        If .PortNo <> cmbExtDevice.SelectedValue Then Return True

    '        If .NormalHI <> txtHighNormal.Text Then Return True
    '        If .NormalLO <> txtLowNormal.Text Then Return True
    '        If .OffSet <> txtOffset.Text Then Return True

    '        If cmbUnit.SelectedValue <> gCstCodeChManualInputUnit.ToString Then
    '            If .Unit <> cmbUnit.Text Then Return True
    '        Else
    '            If .Unit <> txtUnit.Text Then Return True
    '        End If

    '        If .strString <> txtString.Text Then Return True
    '        If .FlagCenterGraph <> chkCenterGraph.Checked Then Return True

    '        If .ValueHH <> txtValueHiHi.Text Then Return True
    '        If .ValueH <> txtValueHi.Text Then Return True
    '        If .ValueL <> txtValueLo.Text Then Return True
    '        If .ValueLL <> txtValueLoLo.Text Then Return True
    '        If .ValueSF <> cmbValueSensorFailure.SelectedValue Then Return True

    '        If .ExtGHH <> txtExtGHiHi.Text Then Return True
    '        If .ExtGH <> txtExtGHi.Text Then Return True
    '        If .ExtGL <> txtExtGLo.Text Then Return True
    '        If .ExtGLL <> txtExtGLoLo.Text Then Return True
    '        If .ExtGSF <> txtExtGSensorFailure.Text Then Return True

    '        If .DelayHH <> txtDelayHiHi.Text Then Return True
    '        If .DelayH <> txtDelayHi.Text Then Return True
    '        If .DelayL <> txtDelayLo.Text Then Return True
    '        If .DelayLL <> txtDelayLoLo.Text Then Return True

    '        If .GRep1HH <> txtGRep1HiHi.Text Then Return True
    '        If .GRep1H <> txtGRep1Hi.Text Then Return True
    '        If .GRep1L <> txtGRep1Lo.Text Then Return True
    '        If .GRep1LL <> txtGRep1LoLo.Text Then Return True

    '        If .GRep2HH <> txtGRep2HiHi.Text Then Return True
    '        If .GRep2H <> txtGRep2Hi.Text Then Return True
    '        If .GRep2L <> txtGRep2Lo.Text Then Return True
    '        If .GRep2LL <> txtGRep2LoLo.Text Then Return True

    '    End With

    '    Return False

    'End Function

#End Region

    'T.Ueki
    Private Sub cmbValueSensorFailure_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbValueSensorFailure.TextChanged

        Try

            Select Case cmbValueSensorFailure.SelectedValue

                Case 0
                    txtExtGSensorFailure.Enabled = False : txtDelaySensorFailure.Enabled = False
                    txtExtGSensorFailure.Text = "" : txtDelaySensorFailure.Text = ""


                Case 1
                    txtExtGSensorFailure.Enabled = True : txtDelaySensorFailure.Enabled = True
            End Select




        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtTag_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTag.KeyPress
        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '' Ver1.10.1 2016.02.29 力率対応 追加
    ''  力率CHの場合、ｾﾝﾀｰ分けｸﾞﾗﾌのﾁｪｯｸも入れる
    Private Sub chkPowerFactor_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPowerFactor.CheckedChanged

        'P/S Displayチェックを外す
        If chkPSDisp.Checked = True Then
            chkPSDisp.Checked = False
        End If

        'Ver2.0.7.9 A/F Displayチェックを外す
        If chkAFDisp.Checked = True Then
            chkAFDisp.Checked = False
        End If

        If cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogJacom Or cmbDataType.SelectedValue = gCstCodeChDataTypeAnalogJacom55 Then
            If chkPowerFactor.Checked = True Then
                chkCenterGraph.Checked = True
            End If
        End If

    End Sub

    Private Sub chkPSDisp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPSDisp.CheckedChanged

        'Power Factorチェックを外す
        If chkPowerFactor.Checked = True Then
            chkPowerFactor.Checked = False
        End If

        'Ver2.0.7.9 A/F Displayチェックを外す
        If chkAFDisp.Checked = True Then
            chkAFDisp.Checked = False
        End If

    End Sub


    'Ver2.0.7.9 AF対応
    Private Sub chkAFDisp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAFDisp.CheckedChanged

        'Power Factorチェックを外す
        If chkPowerFactor.Checked = True Then
            chkPowerFactor.Checked = False
        End If

        'P/S Displayチェックを外す
        If chkPSDisp.Checked = True Then
            chkPSDisp.Checked = False
        End If

    End Sub

    'Ver2.0.8.5 mmHgレンジ下限小数点対応
    Private Sub chkMmHgFlg_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMmHgFlg.CheckedChanged
        Try
            If chkMmHgFlg.Checked = True Then
                txtMmHgDec.Enabled = True
            Else
                txtMmHgDec.Enabled = False
                txtMmHgDec.Text = ""
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub Label25_Click(sender As System.Object, e As System.EventArgs) Handles Label25.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class


