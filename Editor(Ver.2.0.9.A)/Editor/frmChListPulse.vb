Public Class frmChListPulse

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

    ' ''パルス積算チャンネル情報格納
    Private mPulseDetail As frmChListChannelList.mPulseInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) バルブチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hPulseDetail As frmChListChannelList.mPulseInfo, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            mPulseDetail.RowNo = hPulseDetail.RowNo
            mPulseDetail.RowNoFirst = hPulseDetail.RowNoFirst
            mPulseDetail.RowNoEnd = hPulseDetail.RowNoEnd
            mPulseDetail.SysNo = hPulseDetail.SysNo
            mPulseDetail.ChNo = hPulseDetail.ChNo
            mPulseDetail.TagNo = hPulseDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
            mPulseDetail.ItemName = hPulseDetail.ItemName
            mPulseDetail.AlmLevel = hPulseDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mPulseDetail.ValueH = hPulseDetail.ValueH
            mPulseDetail.ExtGH = hPulseDetail.ExtGH
            mPulseDetail.DelayH = hPulseDetail.DelayH
            mPulseDetail.GRep1H = hPulseDetail.GRep1H
            mPulseDetail.GRep2H = hPulseDetail.GRep2H
            mPulseDetail.FlagDmy = hPulseDetail.FlagDmy
            mPulseDetail.FlagSC = hPulseDetail.FlagSC
            mPulseDetail.FlagSIO = hPulseDetail.FlagSIO
            mPulseDetail.FlagGWS = hPulseDetail.FlagGWS
            mPulseDetail.FlagWK = hPulseDetail.FlagWK
            mPulseDetail.FlagRL = hPulseDetail.FlagRL
            mPulseDetail.FlagAC = hPulseDetail.FlagAC
            mPulseDetail.FlagEP = hPulseDetail.FlagEP
            mPulseDetail.FlagPLC = hPulseDetail.FlagPLC     '' 2014.11.18
            mPulseDetail.FlagSP = hPulseDetail.FlagSP
            mPulseDetail.FlagMin = hPulseDetail.FlagMin
            mPulseDetail.FuNo = hPulseDetail.FuNo
            mPulseDetail.FUPortNo = hPulseDetail.FUPortNo
            mPulseDetail.FUPin = hPulseDetail.FUPin
            mPulseDetail.DataType = hPulseDetail.DataType
            mPulseDetail.Unit = hPulseDetail.Unit
            mPulseDetail.strString = hPulseDetail.strString
            mPulseDetail.strDecimalPoint = hPulseDetail.strDecimalPoint
            mPulseDetail.FilterCoef = hPulseDetail.FilterCoef
            mPulseDetail.ShareType = hPulseDetail.ShareType
            mPulseDetail.ShareChNo = hPulseDetail.ShareChNo
            mPulseDetail.Remarks = hPulseDetail.Remarks
            mPulseDetail.Status = hPulseDetail.Status
            mPulseDetail.StatusH = hPulseDetail.StatusH

            mPulseDetail.RangeLo = hPulseDetail.RangeLo
            mPulseDetail.RangeHi = hPulseDetail.RangeHi

            'T.Ueki
            mPulseDetail.FlagStatusAlarm = hPulseDetail.ExtGH

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mPulseDetail.AlmMimic = hPulseDetail.AlmMimic


            '▼▼▼ 20110614 仮設定機能対応（パルス） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mPulseDetail.DummyExtG = hPulseDetail.DummyExtG
            mPulseDetail.DummyDelay = hPulseDetail.DummyDelay
            mPulseDetail.DummyGroupRepose1 = hPulseDetail.DummyGroupRepose1
            mPulseDetail.DummyGroupRepose2 = hPulseDetail.DummyGroupRepose2
            mPulseDetail.DummyFuAddress = hPulseDetail.DummyFuAddress
            mPulseDetail.DummyUnitName = hPulseDetail.DummyUnitName
            mPulseDetail.DummyStatusName = hPulseDetail.DummyStatusName
            mPulseDetail.DummyAlarmValue = hPulseDetail.DummyAlarmValue
            mPulseDetail.DummyAlarmStatus = hPulseDetail.DummyAlarmStatus
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hPulseDetail, mPulseDetail) = False Then

                    hPulseDetail.SysNo = mPulseDetail.SysNo
                    hPulseDetail.ChNo = mPulseDetail.ChNo
                    hPulseDetail.TagNo = mPulseDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
                    hPulseDetail.ItemName = mPulseDetail.ItemName
                    hPulseDetail.AlmLevel = mPulseDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hPulseDetail.ValueH = mPulseDetail.ValueH
                    hPulseDetail.ExtGH = mPulseDetail.ExtGH
                    hPulseDetail.DelayH = mPulseDetail.DelayH
                    hPulseDetail.GRep1H = mPulseDetail.GRep1H
                    hPulseDetail.GRep2H = mPulseDetail.GRep2H
                    hPulseDetail.FlagDmy = mPulseDetail.FlagDmy
                    hPulseDetail.FlagSC = mPulseDetail.FlagSC
                    hPulseDetail.FlagSIO = mPulseDetail.FlagSIO
                    hPulseDetail.FlagGWS = mPulseDetail.FlagGWS
                    hPulseDetail.FlagWK = mPulseDetail.FlagWK
                    hPulseDetail.FlagRL = mPulseDetail.FlagRL
                    hPulseDetail.FlagAC = mPulseDetail.FlagAC
                    hPulseDetail.FlagEP = mPulseDetail.FlagEP
                    hPulseDetail.FlagPLC = mPulseDetail.FlagPLC     '' 2014.11.18
                    hPulseDetail.FlagSP = mPulseDetail.FlagSP
                    hPulseDetail.FlagMin = mPulseDetail.FlagMin
                    hPulseDetail.FuNo = mPulseDetail.FuNo
                    hPulseDetail.FUPortNo = mPulseDetail.FUPortNo
                    hPulseDetail.FUPin = mPulseDetail.FUPin
                    hPulseDetail.DataType = mPulseDetail.DataType
                    hPulseDetail.Unit = mPulseDetail.Unit
                    hPulseDetail.strString = mPulseDetail.strString
                    hPulseDetail.strDecimalPoint = mPulseDetail.strDecimalPoint
                    hPulseDetail.FilterCoef = mPulseDetail.FilterCoef
                    hPulseDetail.ShareType = mPulseDetail.ShareType
                    hPulseDetail.ShareChNo = mPulseDetail.ShareChNo
                    hPulseDetail.Remarks = mPulseDetail.Remarks
                    hPulseDetail.StatusH = mPulseDetail.StatusH
                    hPulseDetail.Status = mPulseDetail.Status

                    hPulseDetail.RangeLo = mPulseDetail.RangeLo
                    hPulseDetail.RangeHi = mPulseDetail.RangeHi

                    'T.Ueki
                    hPulseDetail.FlagStatusAlarm = mPulseDetail.ExtGH

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hPulseDetail.AlmMimic = mPulseDetail.AlmMimic


                    '▼▼▼ 20110614 仮設定機能対応（パルス） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hPulseDetail.DummyExtG = mPulseDetail.DummyExtG
                    hPulseDetail.DummyDelay = mPulseDetail.DummyDelay
                    hPulseDetail.DummyGroupRepose1 = mPulseDetail.DummyGroupRepose1
                    hPulseDetail.DummyGroupRepose2 = mPulseDetail.DummyGroupRepose2
                    hPulseDetail.DummyFuAddress = mPulseDetail.DummyFuAddress
                    hPulseDetail.DummyUnitName = mPulseDetail.DummyUnitName
                    hPulseDetail.DummyStatusName = mPulseDetail.DummyStatusName
                    hPulseDetail.DummyAlarmValue = mPulseDetail.DummyAlarmValue
                    hPulseDetail.DummyAlarmStatus = mPulseDetail.DummyAlarmStatus
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
    Private Sub frmChListPulse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim intValue As Integer = 0

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            ''画面を初期化する
            Call mInitial()

            With mPulseDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTagNo.Text = .TagNo      '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
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

                txtFuNo.Text = .FuNo
                txtPortNo.Text = .FUPortNo
                txtPin.Text = .FUPin

                cmbDataType.SelectedValue = .DataType

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

                txtValueHi.Text = .ValueH
                txtExtGHi.Text = .ExtGH
                txtDelayHi.Text = .DelayH
                txtGRep1Hi.Text = .GRep1H
                txtGRep2Hi.Text = .GRep2H

                txtFilterCoeficient.Text = .FilterCoef

                txtDecPoint.Text = .strDecimalPoint
                Call txtDecPoint_Validated(txtDecPoint, New EventArgs)

                'Lo側は[0]固定
                txtrangeLo.Text = "0"

                ''Status
                intValue = cmbStatus.FindStringExact(.Status)
                If intValue >= 0 Then
                    cmbStatus.SelectedIndex = intValue

                    txtStatusHi.Visible = False
                Else
                    cmbStatus.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                    txtStatusHi.Text = .StatusH.Trim

                    txtStatusHi.Visible = True
                End If

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


                '▼▼▼ 20110614 仮設定機能対応（パルス） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If mPulseDetail.DummyExtG Then Call objDummySetControl_KeyDown(txtExtGHi, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyDelay Then Call objDummySetControl_KeyDown(txtDelayHi, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyGroupRepose1 Then Call objDummySetControl_KeyDown(txtGRep1Hi, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyGroupRepose2 Then Call objDummySetControl_KeyDown(txtGRep2Hi, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyFuAddress Then Call txtFuAdrress_KeyDown(txtFuNo, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyUnitName Then Call cmbUnit_KeyDown(cmbUnit, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyStatusName Then Call cmbStatus_KeyDown(cmbStatus, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyAlarmValue Then Call objDummySetControl_KeyDown(txtValueHi, New KeyEventArgs(gCstDummySetKey))
                If mPulseDetail.DummyAlarmStatus Then Call objDummySetControl_KeyDown(txtStatusHi, New KeyEventArgs(gCstDummySetKey))
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

            mintOkFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListPulse_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    ' 機能説明  ： データ種別コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectedIndexChanged

        Try

            If (cmbDataType.SelectedValue >= gCstCodeChDataTypePulseTotal1_1 And _
               cmbDataType.SelectedValue <= gCstCodeChDataTypePulseDay1_100) Or _
               (cmbDataType.SelectedValue = gCstCodeChDataTypePulseExtDev) Then

                ''パルスCH
                txtDecPoint.Text = ""
                txtDecPoint.Enabled = True

                Call txtDecPoint_Validated(txtDecPoint, New EventArgs)

                '' Ver1.12.0.1 2017.01.13  運転積算種類追加
            ElseIf (cmbDataType.SelectedValue >= gCstCodeChDataTypePulseRevoTotalHour And _
                   cmbDataType.SelectedValue <= gCstCodeChDataTypePulseRevoLapMin) Or _
               (cmbDataType.SelectedValue >= gCstCodeChDataTypePulseRevoExtDev) Then

                ''積算CH
                Select Case cmbDataType.SelectedValue

                    Case gCstCodeChDataTypePulseRevoTotalHour, gCstCodeChDataTypePulseRevoDayHour, _
                         gCstCodeChDataTypePulseRevoLapHour, gCstCodeChDataTypePulseRevoExtDevDayHour, _
                         gCstCodeChDataTypePulseRevoExtDevLapHour, gCstCodeChDataTypePulseRevoExtDev

                        txtDecPoint.Text = "0"
                        Call txtDecPoint_Validated(txtDecPoint, New EventArgs)


                    Case gCstCodeChDataTypePulseRevoTotalMin, gCstCodeChDataTypePulseRevoDayMin, _
                         gCstCodeChDataTypePulseRevoLapMin, gCstCodeChDataTypePulseRevoExtDevTotalMin, _
                         gCstCodeChDataTypePulseRevoExtDevDayMin, gCstCodeChDataTypePulseRevoExtDevLapMin

                        txtDecPoint.Text = "2"
                        Call txtDecPoint_Validated(txtDecPoint, New EventArgs)

                End Select

                txtDecPoint.Enabled = False
                txtRangeHi.Enabled = False

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

                txtStatusHi.Visible = True
                lblStatus.Visible = True

            Else

                txtStatusHi.Visible = False
                lblStatus.Visible = False

            End If

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

            'If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")

            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypePulseExtDev     ''外部機器
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case Else
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 小数点以下桁数が入力された時、Valueのフォーマットを更新する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtDecPoint_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDecPoint.Validated

        Try
            Dim intDec As Integer = 0
            Dim strDecimalFormat As String = ""
            Dim lngValue As Long = 0
            Dim dblValue As Double = 0

            If txtDecPoint.Text = "" Or txtDecPoint.Text = "0" Then

                'Ver2.0.6.5 9が7個
                'Ver2.0.7.E DecPoint無しは9が8個
                txtRangeHi.Text = "99999999"    '"9999999" '"99999999"

                ''Value
                If txtValueHi.Text <> "" Then txtValueHi.Text = Int(txtValueHi.Text) ''整数部分のみ

            Else
                intDec = CCInt(txtDecPoint.Text)

                '' 運転積算(MIN)の場合は59(分)までの表示　ver.1.4.0 2011.09.26
                If cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoTotalMin Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoDayMin Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoLapMin Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                   cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoExtDevLapMin Then
                    txtRangeHi.Text = ".59"

                Else    '' パルス
                    If intDec <= 6 Then
                        txtRangeHi.Text = ".".PadRight(intDec + 1, "9"c)
                    Else
                        txtRangeHi.Text = ".".PadRight(7, "9"c)
                    End If
                End If

                txtRangeHi.Text = txtRangeHi.Text.PadLeft(8, "9"c)

                ''Value
                If txtValueHi.Text <> "" Then

                    ''警報設定値の小数点位置制限　ver.1.4.0 2011.09.26
                    If intDec <= 6 Then
                        strDecimalFormat = "0.".PadRight(intDec + 2, "0"c)
                    Else
                        strDecimalFormat = "0.".PadRight(8, "0"c)
                    End If

                    lngValue = Int(Val(txtValueHi.Text) * (10 ^ intDec) + 0.5)
                    dblValue = lngValue / (10 ^ intDec)

                    ''レンジOVERの場合はレンジの値とする　ver.1.4.0 2011.09.26
                    If dblValue < Val(txtRangeHi.Text) Then
                        txtValueHi.Text = dblValue.ToString(strDecimalFormat)
                    Else
                        txtValueHi.Text = txtRangeHi.Text
                    End If


                End If

            End If

            'If txtDecPoint.Text = "" Or txtDecPoint.Text = "0" Then

            '    lblDecPoint.Text = "99999999"

            '    ''Value
            '    If txtValueHi.Text <> "" Then txtValueHi.Text = Int(txtValueHi.Text) ''整数部分のみ

            'Else
            '    intDec = CCInt(txtDecPoint.Text)

            '    '' 運転積算(MIN)の場合は59(分)までの表示　ver.1.4.0 2011.09.26
            '    If cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoTotalMin Or _
            '       cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoDayMin Or _
            '       cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoLapMin Then
            '        lblDecPoint.Text = ".59"

            '    Else    '' パルス
            '        If intDec <= 6 Then
            '            lblDecPoint.Text = ".".PadRight(intDec + 1, "9"c)
            '        Else
            '            lblDecPoint.Text = ".".PadRight(7, "9"c)
            '        End If
            '    End If

            '    lblDecPoint.Text = lblDecPoint.Text.PadLeft(8, "9"c)

            '    ''Value
            '    If txtValueHi.Text <> "" Then

            '        ''警報設定値の小数点位置制限　ver.1.4.0 2011.09.26
            '        If intDec <= 6 Then
            '            strDecimalFormat = "0.".PadRight(intDec + 2, "0"c)
            '        Else
            '            strDecimalFormat = "0.".PadRight(8, "0"c)
            '        End If

            '        lngValue = Int(Val(txtValueHi.Text) * (10 ^ intDec) + 0.5)
            '        dblValue = lngValue / (10 ^ intDec)

            '        ''レンジOVERの場合はレンジの値とする　ver.1.4.0 2011.09.26
            '        If dblValue < Val(lblDecPoint.Text) Then
            '            txtValueHi.Text = dblValue.ToString(strDecimalFormat)
            '        Else
            '            txtValueHi.Text = lblDecPoint.Text
            '        End If


            '    End If

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： Valueが入力された時フォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtValueHi_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValueHi.Validated

        Try
            Dim intDec As Integer = 0
            Dim strDecimalFormat As String = ""
            Dim lngValue As Long = 0
            Dim dblValue As Double = 0

            intDec = CCInt(txtDecPoint.Text)

            If txtValueHi.Text <> "" Then

                If intDec = 0 Then

                    txtValueHi.Text = Int(txtValueHi.Text) ''整数部分のみ

                Else
                    ''警報設定値の小数点位置制限　ver.1.4.0 2011.09.26
                    If intDec <= 6 Then
                        strDecimalFormat = "0.".PadRight(intDec + 2, "0"c)
                    Else
                        strDecimalFormat = "0.".PadRight(8, "0"c)
                    End If

                    lngValue = Int(Val(txtValueHi.Text) * (10 ^ intDec) + 0.5)
                    dblValue = lngValue / (10 ^ intDec)

                    ''レンジOVERの場合はレンジの値とする　ver.1.4.0 2011.09.26
                    If dblValue < Val(txtRangeHi.Text) Then
                        txtValueHi.Text = dblValue.ToString(strDecimalFormat)
                    Else
                        txtValueHi.Text = txtRangeHi.Text
                    End If

                    '' 運転積算(MIN)の場合は59(分)までの表示　ver.1.4.0 2011.09.26
                    If cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoTotalMin Or _
                       cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoDayMin Or _
                       cmbDataType.SelectedValue = gCstCodeChDataTypePulseRevoLapMin Then

                        ''.59を超える場合は.59に差し替え　ver.1.4.0 2011.09.26
                        If (dblValue - Fix(dblValue)) >= 0.6 Then
                            txtValueHi.Text = (Fix(dblValue) + 0.59).ToString
                        End If

                    End If

                End If

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

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAlarm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtExtGHi.KeyPress, txtGRep1Hi.KeyPress, txtGRep2Hi.KeyPress, txtPin.KeyPress

        Try

            'T.Ueki
            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypePulseExtDev     ''外部機器（JACOM-22）
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                Case Else
                    e.Handled = gCheckTextInput(2, sender, e.KeyChar)

            End Select

            'e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelayHi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDelayHi.KeyPress

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

    Private Sub txtValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtValueHi.KeyPress

        Try

            ''マイナス入力不可　ver.1.4.0 2011.09.26
            e.Handled = gCheckTextInput(9, sender, e.KeyChar, True, False, True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtString_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                    txtString.KeyPress, txtDecPoint.KeyPress

        Try
            ''小数点以下桁数は 0-8 → 0-6 とする　ver.1.4.0 2011.09.26
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1,2,3,4,5,6")

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

    'Ver2.0.3.8 DEL
    'Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress

    '    Try
    '        '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
    '        e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9")

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnit.KeyPress, txtStatusHi.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilterCoeficient.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)       '' フィルタ定数変更　ver.1.4.4 2012.05.08

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


    '--------------------------------------------------------------------
    ' 機能      : 設定値GET
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面の設定値を内部メモリに取り込む
    '--------------------------------------------------------------------
    Private Sub mGetSetData()

        Try

            With mPulseDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = txtTagNo.Text      '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
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

                .FuNo = txtFuNo.Text
                .FUPortNo = txtPortNo.Text
                .FUPin = txtPin.Text

                .DataType = cmbDataType.SelectedValue

                If cmbUnit.SelectedValue <> gCstCodeChManualInputUnit.ToString Then
                    .Unit = cmbUnit.Text
                Else
                    .Unit = txtUnit.Text
                End If

                .strString = txtString.Text
                .strDecimalPoint = txtDecPoint.Text

                .ValueH = txtValueHi.Text
                .ExtGH = txtExtGHi.Text
                .DelayH = txtDelayHi.Text
                .GRep1H = txtGRep1Hi.Text
                .GRep2H = txtGRep2Hi.Text

                .RangeLo = "0"
                .RangeHi = txtRangeHi.Text

                .FilterCoef = txtFilterCoeficient.Text

                If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                    .Status = cmbStatus.Text
                Else
                    .Status = ""
                    .StatusH = txtStatusHi.Text.Trim
                End If

                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text


                '▼▼▼ 20110614 仮設定機能対応（パルス） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .DummyExtG = gDummyCheckControl(txtExtGHi)
                .DummyDelay = gDummyCheckControl(txtDelayHi)
                .DummyGroupRepose1 = gDummyCheckControl(txtGRep1Hi)
                .DummyGroupRepose2 = gDummyCheckControl(txtGRep2Hi)
                .DummyFuAddress = gDummyCheckControl(txtFuNo)
                .DummyUnitName = gDummyCheckControl(cmbUnit)
                .DummyStatusName = gDummyCheckControl(cmbStatus)
                .DummyAlarmValue = gDummyCheckControl(txtValueHi)
                .DummyAlarmStatus = gDummyCheckControl(txtStatusHi)
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
            Dim dblValue As Double
            Dim lngValue As Long
            Dim intDecimalP As Integer

            ''共通テキスト入力チェック
            If Not gChkInputText(txtItemName, "Item Name", True, True) Then Return False
            If Not gChkInputText(txtRemarks, "Remarks", True, True) Then Return False
            If Not gChkInputText(txtUnit, "Unit", True, True) Then Return False
            If Not gChkInputText(txtStatusHi, "Status HI", True, True) Then Return False
            If ChkTagInput(txtTagNo.Text) = False Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加

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
            If Not gChkInputNum(txtString, 0, 8, "String", True, True) Then Return False
            If Not gChkInputNum(txtDecPoint, 0, 6, "Decimal Position", True, True) Then Return False ''小数点以下桁数は 0-8 → 0-6 とする　ver.1.4.0 2011.09.26
            If Not gChkInputNum(txtFilterCoeficient, 1, 250, "Filter Coeficient", True, True) Then Return False '' フィルタ定数変更　ver.1.4.4 2012.05.08
            If Not gChkInputNum(txtValueHi, -99999999, 999999999, "Value", True, True) Then Return False
            If Not gChkInputNum(txtExtGHi, 0, 24, "EXT.G", True, True) Then Return False
            If Not gChkInputNum(txtGRep1Hi, 0, 48, "G REP1", True, True) Then Return False
            If Not gChkInputNum(txtGRep2Hi, 0, 48, "G REP2", True, True) Then Return False
            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

            If Not gChkInputNum(txtDelayHi, 0, 240, "Delay", True, True) Then Return False
            'If cmbTime.SelectedValue = 0 Then
            '    If Not gChkInputNum(txtDelayHi, 0, 240, "Delay", True, True) Then Return False
            'Else
            '    If Not gChkInputNum(txtDelayHi, 0, 4, "Delay", True, True) Then Return False
            'End If

            ''共通FUアドレス入力チェック
            'T.Ueki
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''Select Case cmbDataType.SelectedValue

            ''    Case gCstCodeChDataTypePulseExtDev     ''外部機器（JACOM-22）
            ''        If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 65535, True, True) Then Return False

            ''    Case Else
            ''        If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False

            ''End Select

            'If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False


            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If


            ''Value HI
            If txtValueHi.Text <> "" Then

                intDecimalP = CCInt(txtDecPoint.Text)

                dblValue = Double.Parse(txtValueHi.Text)
                ''小数以下桁合わせ　ver.1.4.0 2011.09.30
                lngValue = Int(dblValue * (10 ^ intDecimalP) + 0.5)

                If lngValue.ToString.Length > 9 And (lngValue > 999999999 Or lngValue < -99999999) Then
                    MsgBox("Value HI is wrong.", MsgBoxStyle.Exclamation, "Input error")
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 画面初期化
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub mInitial()

        Try

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypePulse)
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)
            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)

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
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mPulseInfo, _
                                         ByVal udt2 As frmChListChannelList.mPulseInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            If udt1.ValueH <> udt2.ValueH Then Return False
            If udt1.ExtGH <> udt2.ExtGH Then Return False
            If udt1.DelayH <> udt2.DelayH Then Return False
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
            If udt1.FuNo <> udt2.FuNo Then Return False
            If udt1.FUPortNo <> udt2.FUPortNo Then Return False
            If udt1.FUPin <> udt2.FUPin Then Return False
            If udt1.DataType <> udt2.DataType Then Return False
            If udt1.Unit <> udt2.Unit Then Return False
            If udt1.strString <> udt2.strString Then Return False
            If udt1.strDecimalPoint <> udt2.strDecimalPoint Then Return False
            If udt1.FilterCoef <> udt2.FilterCoef Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False
            If udt1.Status <> udt2.Status Then Return False
            If udt1.StatusH <> udt2.StatusH Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False


            '▼▼▼ 20110614 仮設定機能対応（パルス） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If udt1.DummyExtG <> udt2.DummyExtG Then Return False
            If udt1.DummyDelay <> udt2.DummyDelay Then Return False
            If udt1.DummyGroupRepose1 <> udt2.DummyGroupRepose1 Then Return False
            If udt1.DummyGroupRepose2 <> udt2.DummyGroupRepose2 Then Return False
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False
            If udt1.DummyUnitName <> udt2.DummyUnitName Then Return False
            If udt1.DummyStatusName <> udt2.DummyStatusName Then Return False
            If udt1.DummyAlarmValue <> udt2.DummyAlarmValue Then Return False
            If udt1.DummyAlarmStatus <> udt2.DummyAlarmStatus Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "仮設定関連"

    Private Sub objDummySetControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
        txtValueHi.KeyDown, txtExtGHi.KeyDown, txtDelayHi.KeyDown, txtGRep1Hi.KeyDown, txtGRep2Hi.KeyDown, txtStatusHi.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(sender)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUnit.KeyDown, txtUnit.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbUnit)
                Call gDummySetColorChange(txtUnit)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatus.KeyDown
        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbStatus)
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
    ' 機能説明  ： Delay Timer 設定単位 コンボ選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub cmbTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTime.SelectedIndexChanged

    '    If mintDelayTimeKubun <> cmbTime.SelectedValue Then

    '        If cmbTime.SelectedValue = 0 Then
    '            ''分 -- > 秒
    '            If txtDelayHi.Text <> "" Then txtDelayHi.Text = Format(CCDouble(txtDelayHi.Text) * 60)
    '        Else
    '            ''秒 --> 分
    '            If txtDelayHi.Text <> "" Then txtDelayHi.Text = Format(CCDouble(txtDelayHi.Text) / 60, "0.0")
    '        End If

    '    End If

    '    mintDelayTimeKubun = cmbTime.SelectedValue

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtDelayHi_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelayHi.Validated

    '    Try

    '        If cmbTime.SelectedValue = 1 Then

    '            If IsNumeric(txtDelayHi.Text) Then
    '                txtDelayHi.Text = Double.Parse(txtDelayHi.Text).ToString("0.0")
    '            Else
    '                txtDelayHi.Text = ""
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

    '    With mPulseDetail

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

    '        If cmbUnit.SelectedValue <> gCstCodeChManualInputUnit.ToString Then
    '            If .Unit <> cmbUnit.Text Then Return True
    '        Else
    '            If .Unit <> txtUnit.Text Then Return True
    '        End If

    '        If .strString <> txtString.Text Then Return True

    '        If .ValueH <> txtValueHi.Text Then Return True
    '        If .ExtGH <> txtExtGHi.Text Then Return True
    '        If .DelayH <> txtDelayHi.Text Then Return True
    '        If .GRep1H <> txtGRep1Hi.Text Then Return True
    '        If .GRep2H <> txtGRep2Hi.Text Then Return True

    '        If .FilterCoef <> txtFilterCoeficient.Text Then Return True

    '    End With

    '    Return False

    'End Function

#End Region

    Private Sub txtString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtString.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
