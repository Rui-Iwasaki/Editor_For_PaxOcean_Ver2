Public Class frmChListPID

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

    'PIDチャンネル情報格納
    Private mPidDetail As frmChListChannelList.mPIDInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:変更あり  <> 0:変更なし
    ' 引き数    : ARG1 - (IO) PIDチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hPidDetail As frmChListChannelList.mPIDInfo, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            mPidDetail.RowNo = hPidDetail.RowNo
            mPidDetail.RowNoFirst = hPidDetail.RowNoFirst
            mPidDetail.RowNoEnd = hPidDetail.RowNoEnd
            mPidDetail.SysNo = hPidDetail.SysNo
            mPidDetail.ChNo = hPidDetail.ChNo
            mPidDetail.TagNo = hPidDetail.TagNo
            mPidDetail.ItemName = hPidDetail.ItemName
            mPidDetail.AlmLevel = hPidDetail.AlmLevel
            mPidDetail.ValueHH = hPidDetail.ValueHH
            mPidDetail.ValueH = hPidDetail.ValueH
            mPidDetail.ValueL = hPidDetail.ValueL
            mPidDetail.ValueLL = hPidDetail.ValueLL
            mPidDetail.ValueSF = hPidDetail.ValueSF
            mPidDetail.ExtGHH = hPidDetail.ExtGHH
            mPidDetail.ExtGH = hPidDetail.ExtGH
            mPidDetail.ExtGL = hPidDetail.ExtGL
            mPidDetail.ExtGLL = hPidDetail.ExtGLL
            mPidDetail.ExtGSF = hPidDetail.ExtGSF
            mPidDetail.DelayHH = hPidDetail.DelayHH
            mPidDetail.DelayH = hPidDetail.DelayH
            mPidDetail.DelayL = hPidDetail.DelayL
            mPidDetail.DelayLL = hPidDetail.DelayLL
            mPidDetail.DelaySF = hPidDetail.DelaySF
            mPidDetail.GRep1HH = hPidDetail.GRep1HH
            mPidDetail.GRep1H = hPidDetail.GRep1H
            mPidDetail.GRep1L = hPidDetail.GRep1L
            mPidDetail.GRep1LL = hPidDetail.GRep1LL
            mPidDetail.GRep2HH = hPidDetail.GRep2HH
            mPidDetail.GRep2H = hPidDetail.GRep2H
            mPidDetail.GRep2L = hPidDetail.GRep2L
            mPidDetail.GRep2LL = hPidDetail.GRep2LL
            mPidDetail.StatusHH = hPidDetail.StatusHH
            mPidDetail.StatusH = hPidDetail.StatusH
            mPidDetail.StatusL = hPidDetail.StatusL
            mPidDetail.StatusLL = hPidDetail.StatusLL
            mPidDetail.FlagDmy = hPidDetail.FlagDmy
            mPidDetail.FlagSC = hPidDetail.FlagSC
            mPidDetail.FlagSIO = hPidDetail.FlagSIO
            mPidDetail.FlagGWS = hPidDetail.FlagGWS
            mPidDetail.FlagWK = hPidDetail.FlagWK
            mPidDetail.FlagRL = hPidDetail.FlagRL
            mPidDetail.FlagAC = hPidDetail.FlagAC
            mPidDetail.FlagEP = hPidDetail.FlagEP
            mPidDetail.FlagPLC = hPidDetail.FlagPLC
            mPidDetail.FlagSP = hPidDetail.FlagSP
            mPidDetail.FlagMin = hPidDetail.FlagMin
            mPidDetail.FuNo = hPidDetail.FuNo
            mPidDetail.FUPortNo = hPidDetail.FUPortNo
            mPidDetail.FUPin = hPidDetail.FUPin
            mPidDetail.DataType = hPidDetail.DataType
            mPidDetail.PortNo = hPidDetail.PortNo

            mPidDetail.RangeType = hPidDetail.RangeType


            mPidDetail.DecimalPosition = hPidDetail.DecimalPosition
            mPidDetail.RangeFrom = hPidDetail.RangeFrom
            mPidDetail.RangeTo = hPidDetail.RangeTo
            mPidDetail.Status = hPidDetail.Status
            mPidDetail.NormalLO = hPidDetail.NormalLO
            mPidDetail.NormalHI = hPidDetail.NormalHI
            mPidDetail.OffSet = hPidDetail.OffSet
            mPidDetail.Unit = hPidDetail.Unit
            mPidDetail.strString = hPidDetail.strString
            mPidDetail.FlagCenterGraph = hPidDetail.FlagCenterGraph
            mPidDetail.FlagPowerFactor = hPidDetail.FlagPowerFactor
            mPidDetail.FlagPSDisp = hPidDetail.FlagPSDisp
            mPidDetail.ShareType = hPidDetail.ShareType
            mPidDetail.ShareChNo = hPidDetail.ShareChNo
            mPidDetail.Remarks = hPidDetail.Remarks

            'PID
            mPidDetail.OutFuNo = hPidDetail.OutFuNo
            mPidDetail.OutFUPortNo = hPidDetail.OutFUPortNo
            mPidDetail.OutFUPin = hPidDetail.OutFUPin
            mPidDetail.OutPinNo = hPidDetail.OutPinNo

            mPidDetail.OutMode = hPidDetail.OutMode
            mPidDetail.CasMode = hPidDetail.CasMode
            mPidDetail.SpTracking = hPidDetail.SpTracking

            'PID DEF
            mPidDetail.DefSpHi = hPidDetail.DefSpHi
            mPidDetail.DefSpLo = hPidDetail.DefSpLo
            mPidDetail.DefMvHi = hPidDetail.DefMvHi
            mPidDetail.DefMvLo = hPidDetail.DefMvLo
            mPidDetail.DefPB = hPidDetail.DefPB
            mPidDetail.DefTI = hPidDetail.DefTI
            mPidDetail.DefTD = hPidDetail.DefTD
            mPidDetail.DefGAP = hPidDetail.DefGAP
            'PID EXT
            ' 1
            mPidDetail.ExtPara1 = hPidDetail.ExtPara1
            mPidDetail.ExtParaHi1 = hPidDetail.ExtParaHi1
            mPidDetail.ExtParaLo1 = hPidDetail.ExtParaLo1
            mPidDetail.ExtParaName1 = hPidDetail.ExtParaName1
            mPidDetail.ExtParaUnit1 = hPidDetail.ExtParaUnit1
            ' 2
            mPidDetail.ExtPara2 = hPidDetail.ExtPara2
            mPidDetail.ExtParaHi2 = hPidDetail.ExtParaHi2
            mPidDetail.ExtParaLo2 = hPidDetail.ExtParaLo2
            mPidDetail.ExtParaName2 = hPidDetail.ExtParaName2
            mPidDetail.ExtParaUnit2 = hPidDetail.ExtParaUnit2
            ' 3
            mPidDetail.ExtPara3 = hPidDetail.ExtPara3
            mPidDetail.ExtParaHi3 = hPidDetail.ExtParaHi3
            mPidDetail.ExtParaLo3 = hPidDetail.ExtParaLo3
            mPidDetail.ExtParaName3 = hPidDetail.ExtParaName3
            mPidDetail.ExtParaUnit3 = hPidDetail.ExtParaUnit3
            ' 4
            mPidDetail.ExtPara4 = hPidDetail.ExtPara4
            mPidDetail.ExtParaHi4 = hPidDetail.ExtParaHi4
            mPidDetail.ExtParaLo4 = hPidDetail.ExtParaLo4
            mPidDetail.ExtParaName4 = hPidDetail.ExtParaName4
            mPidDetail.ExtParaUnit4 = hPidDetail.ExtParaUnit4

            '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mPidDetail.DummyFuAddress = hPidDetail.DummyFuAddress
            mPidDetail.DummyUnitName = hPidDetail.DummyUnitName
            mPidDetail.DummyStatusName = hPidDetail.DummyStatusName

            mPidDetail.DummyDelayHH = hPidDetail.DummyDelayHH
            mPidDetail.DummyValueHH = hPidDetail.DummyValueHH
            mPidDetail.DummyExtGrHH = hPidDetail.DummyExtGrHH
            mPidDetail.DummyGRep1HH = hPidDetail.DummyGRep1HH
            mPidDetail.DummyGRep2HH = hPidDetail.DummyGRep2HH
            mPidDetail.DummyStaNmHH = hPidDetail.DummyStaNmHH

            mPidDetail.DummyDelayH = hPidDetail.DummyDelayH
            mPidDetail.DummyValueH = hPidDetail.DummyValueH
            mPidDetail.DummyExtGrH = hPidDetail.DummyExtGrH
            mPidDetail.DummyGRep1H = hPidDetail.DummyGRep1H
            mPidDetail.DummyGRep2H = hPidDetail.DummyGRep2H
            mPidDetail.DummyStaNmH = hPidDetail.DummyStaNmH

            mPidDetail.DummyDelayL = hPidDetail.DummyDelayL
            mPidDetail.DummyValueL = hPidDetail.DummyValueL
            mPidDetail.DummyExtGrL = hPidDetail.DummyExtGrL
            mPidDetail.DummyGRep1L = hPidDetail.DummyGRep1L
            mPidDetail.DummyGRep2L = hPidDetail.DummyGRep2L
            mPidDetail.DummyStaNmL = hPidDetail.DummyStaNmL

            mPidDetail.DummyDelayLL = hPidDetail.DummyDelayLL
            mPidDetail.DummyValueLL = hPidDetail.DummyValueLL
            mPidDetail.DummyExtGrLL = hPidDetail.DummyExtGrLL
            mPidDetail.DummyGRep1LL = hPidDetail.DummyGRep1LL
            mPidDetail.DummyGRep2LL = hPidDetail.DummyGRep2LL
            mPidDetail.DummyStaNmLL = hPidDetail.DummyStaNmLL

            mPidDetail.DummyDelaySF = hPidDetail.DummyDelaySF
            mPidDetail.DummyValueSF = hPidDetail.DummyValueSF
            mPidDetail.DummyExtGrSF = hPidDetail.DummyExtGrSF
            mPidDetail.DummyGRep1SF = hPidDetail.DummyGRep1SF
            mPidDetail.DummyGRep2SF = hPidDetail.DummyGRep2SF
            mPidDetail.DummyStaNmSF = hPidDetail.DummyStaNmSF

            mPidDetail.DummyRangeScale = hPidDetail.DummyRangeScale
            mPidDetail.DummyRangeNormalHi = hPidDetail.DummyRangeNormalHi
            mPidDetail.DummyRangeNormalLo = hPidDetail.DummyRangeNormalLo
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hPidDetail, mPidDetail) = False Then

                    hPidDetail.SysNo = mPidDetail.SysNo
                    hPidDetail.ChNo = mPidDetail.ChNo
                    hPidDetail.TagNo = mPidDetail.TagNo
                    hPidDetail.ItemName = mPidDetail.ItemName
                    hPidDetail.AlmLevel = mPidDetail.AlmLevel
                    hPidDetail.ValueHH = mPidDetail.ValueHH
                    hPidDetail.ValueH = mPidDetail.ValueH
                    hPidDetail.ValueL = mPidDetail.ValueL
                    hPidDetail.ValueLL = mPidDetail.ValueLL
                    hPidDetail.ValueSF = mPidDetail.ValueSF
                    hPidDetail.ExtGHH = mPidDetail.ExtGHH
                    hPidDetail.ExtGH = mPidDetail.ExtGH
                    hPidDetail.ExtGL = mPidDetail.ExtGL
                    hPidDetail.ExtGLL = mPidDetail.ExtGLL
                    hPidDetail.ExtGSF = mPidDetail.ExtGSF
                    hPidDetail.DelayHH = mPidDetail.DelayHH
                    hPidDetail.DelayH = mPidDetail.DelayH
                    hPidDetail.DelayL = mPidDetail.DelayL
                    hPidDetail.DelayLL = mPidDetail.DelayLL
                    hPidDetail.DelaySF = mPidDetail.DelaySF
                    hPidDetail.GRep1HH = mPidDetail.GRep1HH
                    hPidDetail.GRep1H = mPidDetail.GRep1H
                    hPidDetail.GRep1L = mPidDetail.GRep1L
                    hPidDetail.GRep1LL = mPidDetail.GRep1LL
                    hPidDetail.GRep1SF = mPidDetail.GRep1SF
                    hPidDetail.GRep2SF = mPidDetail.GRep2SF
                    hPidDetail.GRep2HH = mPidDetail.GRep2HH
                    hPidDetail.GRep2H = mPidDetail.GRep2H
                    hPidDetail.GRep2L = mPidDetail.GRep2L
                    hPidDetail.GRep2LL = mPidDetail.GRep2LL
                    hPidDetail.StatusHH = mPidDetail.StatusHH
                    hPidDetail.StatusH = mPidDetail.StatusH
                    hPidDetail.StatusL = mPidDetail.StatusL
                    hPidDetail.StatusLL = mPidDetail.StatusLL
                    hPidDetail.FlagDmy = mPidDetail.FlagDmy
                    hPidDetail.FlagSC = mPidDetail.FlagSC
                    hPidDetail.FlagSIO = mPidDetail.FlagSIO
                    hPidDetail.FlagGWS = mPidDetail.FlagGWS
                    hPidDetail.FlagWK = mPidDetail.FlagWK
                    hPidDetail.FlagRL = mPidDetail.FlagRL
                    hPidDetail.FlagAC = mPidDetail.FlagAC
                    hPidDetail.FlagEP = mPidDetail.FlagEP
                    hPidDetail.FlagPLC = mPidDetail.FlagPLC
                    hPidDetail.FlagSP = mPidDetail.FlagSP
                    hPidDetail.FlagMin = mPidDetail.FlagMin
                    hPidDetail.FuNo = mPidDetail.FuNo
                    hPidDetail.FUPortNo = mPidDetail.FUPortNo
                    hPidDetail.FUPin = mPidDetail.FUPin
                    hPidDetail.DataType = mPidDetail.DataType
                    hPidDetail.PortNo = mPidDetail.PortNo
                    hPidDetail.RangeType = mPidDetail.RangeType
                    hPidDetail.DecimalPosition = mPidDetail.DecimalPosition
                    hPidDetail.RangeFrom = mPidDetail.RangeFrom
                    hPidDetail.RangeTo = mPidDetail.RangeTo
                    hPidDetail.Status = mPidDetail.Status
                    hPidDetail.NormalLO = mPidDetail.NormalLO
                    hPidDetail.NormalHI = mPidDetail.NormalHI
                    hPidDetail.OffSet = mPidDetail.OffSet
                    hPidDetail.Unit = mPidDetail.Unit
                    hPidDetail.strString = mPidDetail.strString
                    hPidDetail.FlagCenterGraph = mPidDetail.FlagCenterGraph
                    hPidDetail.FlagPowerFactor = mPidDetail.FlagPowerFactor
                    hPidDetail.FlagPSDisp = mPidDetail.FlagPSDisp
                    hPidDetail.ShareType = mPidDetail.ShareType
                    hPidDetail.ShareChNo = mPidDetail.ShareChNo
                    hPidDetail.Remarks = mPidDetail.Remarks

                    hPidDetail.OutFuNo = mPidDetail.OutFuNo
                    hPidDetail.OutFUPortNo = mPidDetail.OutFUPortNo
                    hPidDetail.OutFUPin = mPidDetail.OutFUPin
                    hPidDetail.OutPinNo = mPidDetail.OutPinNo

                    hPidDetail.OutMode = mPidDetail.OutMode
                    hPidDetail.CasMode = mPidDetail.CasMode
                    hPidDetail.SpTracking = mPidDetail.SpTracking

                    'PID DEF
                    hPidDetail.DefSpHi = mPidDetail.DefSpHi
                    hPidDetail.DefSpLo = mPidDetail.DefSpLo
                    hPidDetail.DefMvHi = mPidDetail.DefMvHi
                    hPidDetail.DefMvLo = mPidDetail.DefMvLo
                    hPidDetail.DefPB = mPidDetail.DefPB
                    hPidDetail.DefTI = mPidDetail.DefTI
                    hPidDetail.DefTD = mPidDetail.DefTD
                    hPidDetail.DefGAP = mPidDetail.DefGAP
                    'PID EXT
                    ' 1
                    hPidDetail.ExtPara1 = mPidDetail.ExtPara1
                    hPidDetail.ExtParaHi1 = mPidDetail.ExtParaHi1
                    hPidDetail.ExtParaLo1 = mPidDetail.ExtParaLo1
                    hPidDetail.ExtParaName1 = mPidDetail.ExtParaName1
                    hPidDetail.ExtParaUnit1 = mPidDetail.ExtParaUnit1
                    ' 2
                    hPidDetail.ExtPara2 = mPidDetail.ExtPara2
                    hPidDetail.ExtParaHi2 = mPidDetail.ExtParaHi2
                    hPidDetail.ExtParaLo2 = mPidDetail.ExtParaLo2
                    hPidDetail.ExtParaName2 = mPidDetail.ExtParaName2
                    hPidDetail.ExtParaUnit2 = mPidDetail.ExtParaUnit2
                    ' 3
                    hPidDetail.ExtPara3 = mPidDetail.ExtPara3
                    hPidDetail.ExtParaHi3 = mPidDetail.ExtParaHi3
                    hPidDetail.ExtParaLo3 = mPidDetail.ExtParaLo3
                    hPidDetail.ExtParaName3 = mPidDetail.ExtParaName3
                    hPidDetail.ExtParaUnit3 = mPidDetail.ExtParaUnit3
                    ' 4
                    hPidDetail.ExtPara4 = mPidDetail.ExtPara4
                    hPidDetail.ExtParaHi4 = mPidDetail.ExtParaHi4
                    hPidDetail.ExtParaLo4 = mPidDetail.ExtParaLo4
                    hPidDetail.ExtParaName4 = mPidDetail.ExtParaName4
                    hPidDetail.ExtParaUnit4 = mPidDetail.ExtParaUnit4

                    '▼▼▼ 20110614 仮設定機能対応（PID） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hPidDetail.DummyFuAddress = mPidDetail.DummyFuAddress
                    hPidDetail.DummyUnitName = mPidDetail.DummyUnitName
                    hPidDetail.DummyStatusName = mPidDetail.DummyStatusName

                    hPidDetail.DummyDelayHH = mPidDetail.DummyDelayHH
                    hPidDetail.DummyValueHH = mPidDetail.DummyValueHH
                    hPidDetail.DummyExtGrHH = mPidDetail.DummyExtGrHH
                    hPidDetail.DummyGRep1HH = mPidDetail.DummyGRep1HH
                    hPidDetail.DummyGRep2HH = mPidDetail.DummyGRep2HH
                    hPidDetail.DummyStaNmHH = mPidDetail.DummyStaNmHH

                    hPidDetail.DummyDelayH = mPidDetail.DummyDelayH
                    hPidDetail.DummyValueH = mPidDetail.DummyValueH
                    hPidDetail.DummyExtGrH = mPidDetail.DummyExtGrH
                    hPidDetail.DummyGRep1H = mPidDetail.DummyGRep1H
                    hPidDetail.DummyGRep2H = mPidDetail.DummyGRep2H
                    hPidDetail.DummyStaNmH = mPidDetail.DummyStaNmH

                    hPidDetail.DummyDelayL = mPidDetail.DummyDelayL
                    hPidDetail.DummyValueL = mPidDetail.DummyValueL
                    hPidDetail.DummyExtGrL = mPidDetail.DummyExtGrL
                    hPidDetail.DummyGRep1L = mPidDetail.DummyGRep1L
                    hPidDetail.DummyGRep2L = mPidDetail.DummyGRep2L
                    hPidDetail.DummyStaNmL = mPidDetail.DummyStaNmL

                    hPidDetail.DummyDelayLL = mPidDetail.DummyDelayLL
                    hPidDetail.DummyValueLL = mPidDetail.DummyValueLL
                    hPidDetail.DummyExtGrLL = mPidDetail.DummyExtGrLL
                    hPidDetail.DummyGRep1LL = mPidDetail.DummyGRep1LL
                    hPidDetail.DummyGRep2LL = mPidDetail.DummyGRep2LL
                    hPidDetail.DummyStaNmLL = mPidDetail.DummyStaNmLL

                    hPidDetail.DummyDelaySF = mPidDetail.DummyDelaySF
                    hPidDetail.DummyValueSF = mPidDetail.DummyValueSF
                    hPidDetail.DummyExtGrSF = mPidDetail.DummyExtGrSF
                    hPidDetail.DummyGRep1SF = mPidDetail.DummyGRep1SF
                    hPidDetail.DummyGRep2SF = mPidDetail.DummyGRep2SF
                    hPidDetail.DummyStaNmSF = mPidDetail.DummyStaNmSF

                    hPidDetail.DummyRangeScale = mPidDetail.DummyRangeScale
                    hPidDetail.DummyRangeNormalHi = mPidDetail.DummyRangeNormalHi
                    hPidDetail.DummyRangeNormalLo = mPidDetail.DummyRangeNormalLo
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
    Private Sub frmChListPID_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim intValue As Integer

            Dim intDecPointFrom As Integer
            Dim intDecPointTo As Integer

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            ''画面初期化
            Call mInitial()

            With mPidDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTag.Text = .TagNo
                txtItemName.Text = .ItemName
                txtRemarks.Text = .Remarks

                If .AlmLevel <> Nothing Then
                    cmbAlmLvl.SelectedValue = .AlmLevel
                Else
                    cmbAlmLvl.SelectedValue = "0"
                End If


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

                '表示固定位置(display1)
                txtString.Text = .strString

                'Status
                intValue = cmbStatus.FindStringExact(.Status)
                'ｽﾃｰﾀｽとｱﾗｰﾑに矛盾があればマニュアル化
                'ｽﾃｰﾀｽ不具合修正
                If intValue >= 0 Then
                    If fnAnalogStatusAlarmRel(.Status) < 0 Then
                        intValue = -1
                    End If
                End If
                If intValue >= 0 Then
                    cmbStatus.SelectedIndex = intValue
                Else
                    cmbStatus.SelectedValue = gCstCodeChManualInputStatus  '特殊コード（手入力）

                    txtStatusHiHi.Text = .StatusHH.Trim
                    txtStatusHi.Text = .StatusH.Trim
                    txtStatusLoLo.Text = .StatusLL.Trim
                    txtStatusLo.Text = .StatusL.Trim
                End If

                'ノーマルレンジの固定桁数対応
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

                'unitで大文字小文字区別
                intValue = fnBackCmb(cmbUnit, .Unit)
                If intValue >= 0 Then
                    cmbUnit.SelectedIndex = intValue
                Else
                    cmbUnit.SelectedValue = gCstCodeChManualInputUnit  '特殊コード（手入力）
                    txtUnit.Text = .Unit
                End If

                txtString.Text = .strString
                chkCenterGraph.Checked = .FlagCenterGraph

                If .FlagPowerFactor = 1 Then
                    chkPowerFactor.Checked = True
                Else
                    If .FlagPowerFactor = 2 Then
                        chkPSDisp.Checked = True
                    Else
                        chkPowerFactor.Checked = False
                        chkPSDisp.Checked = False
                    End If
                End If

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

                Dim intSF As Integer = .ValueSF
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


                'PID
                txtFuNoDo.Text = NZf(.OutFuNo).Trim
                txtPortNoDo.Text = NZf(.OutFUPortNo).Trim
                txtPinDo.Text = NZf(.OutFUPin).Trim
                txtPinNoDo.Text = NZf(.OutPinNo).Trim

                If .OutMode = "1" Then
                    chkOutMode.Checked = True
                Else
                    chkOutMode.Checked = False
                End If
                If .CasMode = "1" Then
                    chkCasMode.Checked = True
                Else
                    chkCasMode.Checked = False
                End If
                If .SpTracking = "1" Then
                    chkSpTracking.Checked = True
                Else
                    chkSpTracking.Checked = False
                End If

                'PID_DEF
                txtPidDefSpHigh.Text = NZf(.DefSpHi).Trim
                txtPidDefSpLow.Text = NZf(.DefSpLo).Trim
                txtPidDefMvHigh.Text = NZf(.DefMvHi).Trim
                txtPidDefMvLow.Text = NZf(.DefMvLo).Trim
                txtPidDefPB.Text = NZf(.DefPB).Trim
                txtPidDefTI.Text = NZf(.DefTI).Trim
                txtPidDefTD.Text = NZf(.DefTD).Trim
                txtPidDefGAP.Text = NZf(.DefGAP).Trim
                'PID_EXT
                ' 1
                txtPidExtPara1.Text = NZf(.ExtPara1).Trim
                txtPidExtParaHigh1.Text = NZf(.ExtParaHi1).Trim
                txtPidExtParaLow1.Text = NZf(.ExtParaLo1).Trim
                txtPidExtParaName1.Text = NZf(.ExtParaName1).Trim
                txtPidExtParaUnit1.Text = NZf(.ExtParaUnit1).Trim
                ' 2
                txtPidExtPara2.Text = NZf(.ExtPara2).Trim
                txtPidExtParaHigh2.Text = NZf(.ExtParaHi2).Trim
                txtPidExtParaLow2.Text = NZf(.ExtParaLo2).Trim
                txtPidExtParaName2.Text = NZf(.ExtParaName2).Trim
                txtPidExtParaUnit2.Text = NZf(.ExtParaUnit2).Trim
                ' 3
                txtPidExtPara3.Text = NZf(.ExtPara3).Trim
                txtPidExtParaHigh3.Text = NZf(.ExtParaHi3).Trim
                txtPidExtParaLow3.Text = NZf(.ExtParaLo3).Trim
                txtPidExtParaName3.Text = NZf(.ExtParaName3).Trim
                txtPidExtParaUnit3.Text = NZf(.ExtParaUnit3).Trim
                ' 4
                txtPidExtPara4.Text = NZf(.ExtPara4).Trim
                txtPidExtParaHigh4.Text = NZf(.ExtParaHi4).Trim
                txtPidExtParaLow4.Text = NZf(.ExtParaLo4).Trim
                txtPidExtParaName4.Text = NZf(.ExtParaName4).Trim
                txtPidExtParaUnit4.Text = NZf(.ExtParaUnit4).Trim


                '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If mPidDetail.DummyFuAddress Then Call txtFuAdrress_KeyDown(txtFuNo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyUnitName Then Call cmbUnit_KeyDown(cmbUnit, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyStatusName Then Call cmbStatus_KeyDown(cmbValueSensorFailure, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyDelayHH Then Call objDummySetControl_KeyDown(txtDelayHiHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyValueHH Then Call objDummySetControl_KeyDown(txtValueHiHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyExtGrHH Then Call objDummySetControl_KeyDown(txtExtGHiHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep1HH Then Call objDummySetControl_KeyDown(txtGRep1HiHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep2HH Then Call objDummySetControl_KeyDown(txtGRep2HiHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyStaNmHH Then Call objDummySetControl_KeyDown(txtStatusHiHi, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyDelayH Then Call objDummySetControl_KeyDown(txtDelayHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyValueH Then Call objDummySetControl_KeyDown(txtValueHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyExtGrH Then Call objDummySetControl_KeyDown(txtExtGHi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep1H Then Call objDummySetControl_KeyDown(txtGRep1Hi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep2H Then Call objDummySetControl_KeyDown(txtGRep2Hi, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyStaNmH Then Call objDummySetControl_KeyDown(txtStatusHi, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyDelayL Then Call objDummySetControl_KeyDown(txtDelayLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyValueL Then Call objDummySetControl_KeyDown(txtValueLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyExtGrL Then Call objDummySetControl_KeyDown(txtExtGLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep1L Then Call objDummySetControl_KeyDown(txtGRep1Lo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep2L Then Call objDummySetControl_KeyDown(txtGRep2Lo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyStaNmL Then Call objDummySetControl_KeyDown(txtStatusLo, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyDelayLL Then Call objDummySetControl_KeyDown(txtDelayLoLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyValueLL Then Call objDummySetControl_KeyDown(txtValueLoLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyExtGrLL Then Call objDummySetControl_KeyDown(txtExtGLoLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep1LL Then Call objDummySetControl_KeyDown(txtGRep1LoLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep2LL Then Call objDummySetControl_KeyDown(txtGRep2LoLo, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyStaNmLL Then Call objDummySetControl_KeyDown(txtStatusLoLo, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyDelaySF Then Call objDummySetControl_KeyDown(txtDelaySensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyValueSF Then Call objDummySetControl_KeyDown(cmbValueSensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyExtGrSF Then Call objDummySetControl_KeyDown(txtExtGSensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep1SF Then Call objDummySetControl_KeyDown(txtGRep1SensorFailure, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyGRep2SF Then Call objDummySetControl_KeyDown(txtGRep2SensorFailure, New KeyEventArgs(gCstDummySetKey))
                'If mpidDetail.DummyStaNmSF Then Call objDummySetControl_KeyDown(txtStatusSF, New KeyEventArgs(gCstDummySetKey))

                If mPidDetail.DummyRangeScale Then Call cmbRangeType_KeyDown(cmbRangeType, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyRangeNormalHi Then Call objDummySetControl_KeyDown(txtHighNormal, New KeyEventArgs(gCstDummySetKey))
                If mPidDetail.DummyRangeNormalLo Then Call objDummySetControl_KeyDown(txtLowNormal, New KeyEventArgs(gCstDummySetKey))
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

            mintOkFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： データタイプコンボ選択　処理無し
    ' 引数      ： 
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectedIndexChanged
        Try
            Select Case cmbDataType.SelectedValue
                Case gCstCodeChDataTypePID_3_Pt100_2, gCstCodeChDataTypePID_4_Pt100_3
                    Call gSetComboBox(cmbRanDumy, gEnmComboType.ctChListChannelListRangeType1)
                    lblRangeCaution.Text = "[Range Type]" & vbCrLf
                    For i As Integer = 0 To cmbRanDumy.Items.Count - 1 Step 1
                        lblRangeCaution.Text = lblRangeCaution.Text & cmbRanDumy.Items.Item(i).row.itemarray(1).ToString & vbCrLf
                    Next i
                Case Else
                    lblRangeCaution.Text = ""
            End Select


            cmbRangeType.Visible = False
            txtRangeFrom.Visible = True : txtRangeTo.Visible = True : lblHyphen.Visible = True

            txtFuNo.Enabled = True : txtPortNo.Enabled = True : txtPin.Enabled = True


            cmbExtDevice.Visible = False

            txtExtGHiHi.Enabled = True : txtDelayHiHi.Enabled = True : txtGRep1HiHi.Enabled = True : txtGRep2HiHi.Enabled = True : txtStatusHiHi.Enabled = True
            txtExtGHi.Enabled = True : txtDelayHi.Enabled = True : txtGRep1Hi.Enabled = True : txtGRep2Hi.Enabled = True : txtStatusHi.Enabled = True
            txtExtGLo.Enabled = True : txtDelayLo.Enabled = True : txtGRep1Lo.Enabled = True : txtGRep2Lo.Enabled = True : txtStatusLo.Enabled = True
            txtExtGLoLo.Enabled = True : txtDelayLoLo.Enabled = True : txtGRep1LoLo.Enabled = True : txtGRep2LoLo.Enabled = True : txtStatusLoLo.Enabled = True

            txtExtGSensorFailure.Enabled = True : txtDelaySensorFailure.Enabled = True
            cmbValueSensorFailure.Enabled = True

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
            '手入力の場合、アラーム毎のステータス入力欄を可視にする
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
    Private Sub frmChListPID_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress

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

    Private Sub txtString_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtString.KeyPress, txtDecimal.KeyPress
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
            e.Handled = gCheckTextInput(2, sender, e.KeyChar)
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

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress, txtPortNoDo.KeyPress
        Try
            '9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress, txtFuNoDo.KeyPress
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
            If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")
            'FU Address を設定した際に、Data Typeを自動設定する
            Call mSetDataType()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFuNo.Validated

        Try
            'FU Address を設定した際に、Data Typeを自動設定する
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

    '力率CHの場合、ｾﾝﾀｰ分けｸﾞﾗﾌのﾁｪｯｸも入れる
    Private Sub chkPowerFactor_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPowerFactor.CheckedChanged

        'P/S Displayチェックを外す
        If chkPSDisp.Checked = True Then
            chkPSDisp.Checked = False
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

    End Sub

#Region "PID DEF KEY PRESS"
    Private Sub txtPinNoDo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPinNoDo.KeyPress
        'OUT PIN NO
        Try
            '9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9")
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefSp_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefSpHigh.KeyPress, txtPidDefSpLow.KeyPress
        'PID DEF SP HIGH及びLOW
        Try
            '数値のみ、マイナス可、小数点可
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefMv_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefMvHigh.KeyPress, txtPidDefMvLow.KeyPress
        'PID DEF MV HIGH及びLOW
        Try
            '数値のみ、マイナス不可、小数点可 0.0～100.0
            e.Handled = gCheckTextInput(4, sender, e.KeyChar, True, False, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefPB_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefPB.KeyPress
        'PID DEF PB
        Try
            '数値のみ、マイナス不可、小数点可 0.0～800.0
            e.Handled = gCheckTextInput(4, sender, e.KeyChar, True, False, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefTI_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefTI.KeyPress
        'PID DEF TI
        Try
            '数値のみ、マイナス不可、小数点可 0.0～3000.0
            e.Handled = gCheckTextInput(5, sender, e.KeyChar, True, False, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefTD_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefTD.KeyPress
        'PID DEF TD
        Try
            '数値のみ、マイナス不可、小数点可 0.0～3000.0
            e.Handled = gCheckTextInput(5, sender, e.KeyChar, True, False, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidDefGAP_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPidDefGAP.KeyPress
        'PID DEF GAP
        Try
            '数値のみ、マイナス不可、小数点可 0.0～100.0
            e.Handled = gCheckTextInput(4, sender, e.KeyChar, True, False, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region
#Region "PID EXT KEY PRESS"
    Private Sub txtPidExtPara_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtPidExtPara1.KeyPress, _
        txtPidExtPara2.KeyPress, _
        txtPidExtPara3.KeyPress, _
        txtPidExtPara4.KeyPress
        'PID EXT PARA1～4
        Try
            '数値のみ、マイナス可、小数点可
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidExtParaHigh_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtPidExtParaHigh1.KeyPress, _
        txtPidExtParaHigh2.KeyPress, _
        txtPidExtParaHigh3.KeyPress, _
        txtPidExtParaHigh4.KeyPress
        'PID EXT PARA_HIGH1～4
        Try
            '数値のみ、マイナス可、小数点可
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidExtParaLow_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtPidExtParaLow1.KeyPress, _
        txtPidExtParaLow2.KeyPress, _
        txtPidExtParaLow3.KeyPress, _
        txtPidExtParaLow4.KeyPress
        'PID EXT PARA_LOW1～4
        Try
            '数値のみ、マイナス可、小数点可
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, True, True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidExtParaName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtPidExtParaName1.KeyPress, _
        txtPidExtParaName2.KeyPress, _
        txtPidExtParaName3.KeyPress, _
        txtPidExtParaName4.KeyPress
        'PID EXT PARA NAME1～4
        Try
            '８文字TEXT
            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtPidExtParaUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtPidExtParaUnit1.KeyPress, _
        txtPidExtParaUnit2.KeyPress, _
        txtPidExtParaUnit3.KeyPress, _
        txtPidExtParaUnit4.KeyPress
        'PID EXT PARA UNIT1～4
        Try
            '８文字TEXT
            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

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
    'アナログCHのステータスとセット値関係処理関数
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

        With mPidDetail
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

            With mPidDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTag.Text)
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue

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
                .FlagPLC = txtPLC.Text
                .FlagSP = txtSP.Text

                .FlagMin = cmbTime.SelectedValue

                .DataType = cmbDataType.SelectedValue

                .FuNo = Trim(txtFuNo.Text)
                .FUPortNo = Trim(txtPortNo.Text)
                .FUPin = Trim(txtPin.Text)

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

                If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    .Status = cmbStatus.Text
                Else
                    '手入力ｽﾃｰﾀｽの場合、その値編集してをｽﾃｰﾀｽへ表示
                    Dim strManuStatus As String = ""
                    strManuStatus = fnGetManuSTATUS_4(txtStatusLoLo.Text, txtStatusLo.Text, txtStatusHi.Text, txtStatusHiHi.Text)
                    .Status = strManuStatus

                    .StatusHH = txtStatusHiHi.Text.Trim
                    .StatusH = txtStatusHi.Text.Trim
                    .StatusL = txtStatusLo.Text.Trim
                    .StatusLL = txtStatusLoLo.Text.Trim
                End If

                .PortNo = cmbExtDevice.SelectedValue

                'ノーマルレンジの固定桁数対応
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

                .FlagCenterGraph = chkCenterGraph.Checked

                '力率
                If chkPowerFactor.Checked = True Then
                    .FlagPowerFactor = 1
                Else
                    If chkPSDisp.Checked = True Then
                        .FlagPowerFactor = 2
                    Else
                        .FlagPowerFactor = 0
                    End If
                End If

                '設定値の固定桁数対応
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

                .GRep1SF = txtGRep1SensorFailure.Text

                .GRep2HH = txtGRep2HiHi.Text
                .GRep2H = txtGRep2Hi.Text
                .GRep2L = txtGRep2Lo.Text
                .GRep2LL = txtGRep2LoLo.Text

                .GRep2SF = txtGRep2SensorFailure.Text


                'PID
                .OutFuNo = txtFuNoDo.Text.Trim
                .OutFUPortNo = txtPortNoDo.Text.Trim
                .OutFUPin = txtPinDo.Text.Trim
                .OutPinNo = txtPinNoDo.Text.Trim

                If chkOutMode.Checked = True Then
                    .OutMode = "1"
                Else
                    .OutMode = "0"
                End If
                If chkCasMode.Checked = True Then
                    .CasMode = "1"
                Else
                    .CasMode = "0"
                End If
                If chkSpTracking.Checked = True Then
                    .SpTracking = "1"
                Else
                    .SpTracking = "0"
                End If

                'PID DEF
                Dim intDecimalP As Integer
                Dim strDecimalFormat As String
                Dim dblValue As Double
                intDecimalP = .DecimalPosition
                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                '.DefSpHi = fnChg00(txtPidDefSpHigh.Text.Trim)
                If IsNumeric(txtPidDefSpHigh.Text.Trim) = True Then
                    dblValue = CDbl(txtPidDefSpHigh.Text.Trim)
                Else
                    dblValue = 0
                End If
                .DefSpHi = dblValue.ToString(strDecimalFormat)

                '.DefSpLo = fnChg00(txtPidDefSpLow.Text.Trim)
                If IsNumeric(txtPidDefSpLow.Text.Trim) = True Then
                    dblValue = CDbl(txtPidDefSpLow.Text.Trim)
                Else
                    dblValue = 0
                End If
                .DefSpLo = dblValue.ToString(strDecimalFormat)


                .DefMvHi = fnChg00(txtPidDefMvHigh.Text.Trim)
                .DefMvLo = fnChg00(txtPidDefMvLow.Text.Trim)
                .DefPB = fnChg00(txtPidDefPB.Text.Trim)
                .DefTI = fnChg00(txtPidDefTI.Text.Trim)
                .DefTD = fnChg00(txtPidDefTD.Text.Trim)
                .DefGAP = fnChg00(txtPidDefGAP.Text.Trim)
                'PID EXT
                ' 1
                .ExtPara1 = fnChg00(txtPidExtPara1.Text.Trim)
                .ExtParaHi1 = fnChg00(txtPidExtParaHigh1.Text.Trim)
                .ExtParaLo1 = fnChg00(txtPidExtParaLow1.Text.Trim)
                .ExtParaName1 = txtPidExtParaName1.Text.Trim
                .ExtParaUnit1 = txtPidExtParaUnit1.Text.Trim
                ' 2
                .ExtPara2 = fnChg00(txtPidExtPara2.Text.Trim)
                .ExtParaHi2 = fnChg00(txtPidExtParaHigh2.Text.Trim)
                .ExtParaLo2 = fnChg00(txtPidExtParaLow2.Text.Trim)
                .ExtParaName2 = txtPidExtParaName2.Text.Trim
                .ExtParaUnit2 = txtPidExtParaUnit2.Text.Trim
                ' 3
                .ExtPara3 = fnChg00(txtPidExtPara3.Text.Trim)
                .ExtParaHi3 = fnChg00(txtPidExtParaHigh3.Text.Trim)
                .ExtParaLo3 = fnChg00(txtPidExtParaLow3.Text.Trim)
                .ExtParaName3 = txtPidExtParaName3.Text.Trim
                .ExtParaUnit3 = txtPidExtParaUnit3.Text.Trim
                ' 4
                .ExtPara4 = fnChg00(txtPidExtPara4.Text.Trim)
                .ExtParaHi4 = fnChg00(txtPidExtParaHigh4.Text.Trim)
                .ExtParaLo4 = fnChg00(txtPidExtParaLow4.Text.Trim)
                .ExtParaName4 = txtPidExtParaName4.Text.Trim
                .ExtParaUnit4 = txtPidExtParaUnit4.Text.Trim

                '▼▼▼ 20110614 仮設定機能対応（アナログ） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .DummyFuAddress = gDummyCheckControl(txtFuNo)

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
    ' 機能説明  : なにもしない
    '--------------------------------------------------------------------
    Private Function mSetDataType() As Boolean
        Try
            '何もしない
            Return False
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

            If ChkTagInput(txtTag.Text) = False Then Return False

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
            If Not gChkInputNum(txtPLC, 0, 1, "PLC", True, True) Then Return False
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


            'AlarmSet
            If txtExtGHiHi.Text = "" Then
                If txtGRep1HiHi.Text <> "" Or txtGRep2HiHi.Text <> "" Or txtDelayHiHi.Text <> "" Then
                    MsgBox("Please delete SettingValue.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If
            End If

            'Range 
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

            'PID DEF
            Dim dblRangHi As Double
            Dim dblRangLo As Double
            If IsNumeric(txtRangeFrom.Text) = True Then
                dblRangLo = CDbl(txtRangeFrom.Text)
            Else
                dblRangLo = 0
            End If
            If IsNumeric(txtRangeTo.Text) = True Then
                dblRangHi = CDbl(txtRangeTo.Text)
            Else
                dblRangHi = 0
            End If
            ' SP HIGH,LOW RangeFrom-RangeTo
            If Not gChkInputNumDbl(txtPidDefSpHigh, dblRangLo, dblRangHi, "PID SP HIGH", True, True) Then Return False
            If Not gChkInputNumDbl(txtPidDefSpLow, dblRangLo, dblRangHi, "PID SP LOW", True, True) Then Return False

            ' MV HIGH 0.0～100.0
            If Not gChkInputNumDbl(txtPidDefMvHigh, 0.0, 100.0, "PID MV HIGH", True, True) Then Return False
            ' MV LOW 0.0～100.0
            If Not gChkInputNumDbl(txtPidDefMvLow, 0.0, 100.0, "PID MV LOW", True, True) Then Return False
            ' PB 0.0～800.0
            If Not gChkInputNumDbl(txtPidDefPB, 0.0, 800.0, "PID PB", True, True) Then Return False
            ' TI 0.0～3000.0
            If Not gChkInputNumDbl(txtPidDefTI, 0.0, 3000.0, "PID TI", True, True) Then Return False
            ' TD 0.0～3000.0
            If Not gChkInputNumDbl(txtPidDefTD, 0.0, 3000.0, "PID TD", True, True) Then Return False
            ' GAP 0.0～100.0
            If Not gChkInputNumDbl(txtPidDefGAP, 0.0, 100.0, "PID GAP", True, True) Then Return False

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
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypePID)

            ''CommonのAlarmは使用不可
            fraAlarm.Enabled = False

            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)

            Call gSetComboBox(cmbAlmLvl, gEnmComboType.ctChListChannelListAlmLevel)

            'TagNoはﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then
                txtTag.Enabled = False
            End If

            'Alarm Levelは、ﾌﾗｸﾞが立っていないと使用不可
            If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                cmbAlmLvl.SelectedIndex = 0
                cmbAlmLvl.Enabled = False
            End If

            'RangeType注意表記初期化
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
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mPIDInfo, _
                                         ByVal udt2 As frmChListChannelList.mPIDInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False
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
            If udt1.GRep1SF <> udt2.GRep1SF Then Return False
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
            If udt1.FlagPLC <> udt2.FlagPLC Then Return False
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
            If udt1.FlagPowerFactor <> udt2.FlagPowerFactor Then Return False
            If udt1.FlagPSDisp <> udt2.FlagPSDisp Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False
            'PID
            If udt1.OutFuNo <> udt2.OutFuNo Then Return False
            If udt1.OutFUPortNo <> udt2.OutFUPortNo Then Return False
            If udt1.OutFUPin <> udt2.OutFUPin Then Return False
            If udt1.OutPinNo <> udt2.OutPinNo Then Return False

            If udt1.OutMode <> udt2.OutMode Then Return False
            If udt1.CasMode <> udt2.CasMode Then Return False
            If udt1.SpTracking <> udt2.SpTracking Then Return False

            'PID DEF
            If udt1.DefSpHi <> udt2.DefSpHi Then Return False
            If udt1.DefSpLo <> udt2.DefSpLo Then Return False
            If udt1.DefMvHi <> udt2.DefMvHi Then Return False
            If udt1.DefMvLo <> udt2.DefMvLo Then Return False
            If udt1.DefPB <> udt2.DefPB Then Return False
            If udt1.DefTI <> udt2.DefTI Then Return False
            If udt1.DefTD <> udt2.DefTD Then Return False
            If udt1.DefGAP <> udt2.DefGAP Then Return False
            'PID EXT
            ' 1
            If udt1.ExtPara1 <> udt2.ExtPara1 Then Return False
            If udt1.ExtParaHi1 <> udt2.ExtParaHi1 Then Return False
            If udt1.ExtParaLo1 <> udt2.ExtParaLo1 Then Return False
            If udt1.ExtParaName1 <> udt2.ExtParaName1 Then Return False
            If udt1.ExtParaUnit1 <> udt2.ExtParaUnit1 Then Return False
            ' 2
            If udt1.ExtPara2 <> udt2.ExtPara2 Then Return False
            If udt1.ExtParaHi2 <> udt2.ExtParaHi2 Then Return False
            If udt1.ExtParaLo2 <> udt2.ExtParaLo2 Then Return False
            If udt1.ExtParaName2 <> udt2.ExtParaName2 Then Return False
            If udt1.ExtParaUnit2 <> udt2.ExtParaUnit2 Then Return False
            ' 3
            If udt1.ExtPara3 <> udt2.ExtPara3 Then Return False
            If udt1.ExtParaHi3 <> udt2.ExtParaHi3 Then Return False
            If udt1.ExtParaLo3 <> udt2.ExtParaLo3 Then Return False
            If udt1.ExtParaName3 <> udt2.ExtParaName3 Then Return False
            If udt1.ExtParaUnit3 <> udt2.ExtParaUnit3 Then Return False
            ' 4
            If udt1.ExtPara4 <> udt2.ExtPara4 Then Return False
            If udt1.ExtParaHi4 <> udt2.ExtParaHi4 Then Return False
            If udt1.ExtParaLo4 <> udt2.ExtParaLo4 Then Return False
            If udt1.ExtParaName4 <> udt2.ExtParaName4 Then Return False
            If udt1.ExtParaUnit4 <> udt2.ExtParaUnit4 Then Return False



            '▼▼▼ 仮設定機能対応（PID） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
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

    '入力値を0.0formatに変更する関数。ただし妙な入力値の場合""を戻す
    Private Function fnChg00(pstrIn As String) As String
        Dim strRet As String = ""
        Try
            Dim strIn As String
            '入力文字列がnullやnothingの場合""とする。
            strIn = NZfS(pstrIn)

            '入力値が数値ではない場合、""を戻す
            If IsNumeric(strIn) = False Then
                Return ""
            End If

            'Format("0.0")に変換
            strRet = CDbl(pstrIn).ToString("0.0")
        Catch ex As Exception

        End Try

        Return strRet
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

    Private Sub GroupBox5_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox5.Enter

    End Sub
End Class


