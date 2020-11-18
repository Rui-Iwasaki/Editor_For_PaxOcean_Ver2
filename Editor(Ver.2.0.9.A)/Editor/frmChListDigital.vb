Public Class frmChListDigital

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

    ''デジタルチャンネル情報格納
    Private mDigitalDetail As frmChListChannelList.mDigitalInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) デジタルチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hDigitalDetail As frmChListChannelList.mDigitalInfo, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            mDigitalDetail.RowNo = hDigitalDetail.RowNo
            mDigitalDetail.RowNoFirst = hDigitalDetail.RowNoFirst
            mDigitalDetail.RowNoEnd = hDigitalDetail.RowNoEnd
            mDigitalDetail.SysNo = hDigitalDetail.SysNo
            mDigitalDetail.ChNo = hDigitalDetail.ChNo
            mDigitalDetail.TagNo = hDigitalDetail.TagNo     '' 2015.10.27 Ver1.7.5 ﾀｸﾞ表示追加
            mDigitalDetail.ItemName = hDigitalDetail.ItemName
            mDigitalDetail.AlmLevel = hDigitalDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mDigitalDetail.ExtGH = hDigitalDetail.ExtGH
            mDigitalDetail.DelayH = hDigitalDetail.DelayH
            mDigitalDetail.GRep1H = hDigitalDetail.GRep1H
            mDigitalDetail.GRep2H = hDigitalDetail.GRep2H
            mDigitalDetail.FlagDmy = hDigitalDetail.FlagDmy
            mDigitalDetail.FlagSC = hDigitalDetail.FlagSC
            mDigitalDetail.FlagSIO = hDigitalDetail.FlagSIO
            mDigitalDetail.FlagGWS = hDigitalDetail.FlagGWS
            mDigitalDetail.FlagWK = hDigitalDetail.FlagWK
            mDigitalDetail.FlagRL = hDigitalDetail.FlagRL
            mDigitalDetail.FlagAC = hDigitalDetail.FlagAC
            mDigitalDetail.FlagEP = hDigitalDetail.FlagEP
            mDigitalDetail.FlagPLC = hDigitalDetail.FlagPLC     '' 2014.11.18
            mDigitalDetail.FlagSP = hDigitalDetail.FlagSP
            mDigitalDetail.FlagMin = hDigitalDetail.FlagMin
            mDigitalDetail.FuNo = hDigitalDetail.FuNo
            mDigitalDetail.FUPortNo = hDigitalDetail.FUPortNo
            mDigitalDetail.FUPin = hDigitalDetail.FUPin
            mDigitalDetail.DataType = hDigitalDetail.DataType
            mDigitalDetail.PortNo = hDigitalDetail.PortNo
            mDigitalDetail.Status = hDigitalDetail.Status
            mDigitalDetail.FlagStatusAlarm = hDigitalDetail.FlagStatusAlarm
            mDigitalDetail.FilterCoef = hDigitalDetail.FilterCoef
            mDigitalDetail.EccFunc = hDigitalDetail.EccFunc
            mDigitalDetail.ShareType = hDigitalDetail.ShareType
            mDigitalDetail.ShareChNo = hDigitalDetail.ShareChNo
            mDigitalDetail.Remarks = hDigitalDetail.Remarks

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mDigitalDetail.AlmMimic = hDigitalDetail.AlmMimic


            ReDim mDigitalDetail.DeviceStatusUse(15)
            ReDim mDigitalDetail.DeviceStatusCode(15)
            ReDim mDigitalDetail.DeviceStatusName(15)

            mDigitalDetail.DeviceStatus = hDigitalDetail.DeviceStatus
            For i As Integer = 0 To 15
                mDigitalDetail.DeviceStatusUse(i) = hDigitalDetail.DeviceStatusUse(i)
                mDigitalDetail.DeviceStatusCode(i) = hDigitalDetail.DeviceStatusCode(i)
                mDigitalDetail.DeviceStatusName(i) = hDigitalDetail.DeviceStatusName(i)
            Next

            '▼▼▼ 20110614 仮設定機能対応（デジタル） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mDigitalDetail.DummyExtG = hDigitalDetail.DummyExtG
            mDigitalDetail.DummyDelay = hDigitalDetail.DummyDelay
            mDigitalDetail.DummyGroupRepose1 = hDigitalDetail.DummyGroupRepose1
            mDigitalDetail.DummyGroupRepose2 = hDigitalDetail.DummyGroupRepose2
            mDigitalDetail.DummyFuAddress = hDigitalDetail.DummyFuAddress
            mDigitalDetail.DummyStatusName = hDigitalDetail.DummyStatusName
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hDigitalDetail, mDigitalDetail) = False Then

                    hDigitalDetail.SysNo = mDigitalDetail.SysNo
                    hDigitalDetail.ChNo = mDigitalDetail.ChNo
                    hDigitalDetail.TagNo = mDigitalDetail.TagNo   '' 2015.10.27 Ver1.7.5 ﾀｸﾞ表示追加
                    hDigitalDetail.ItemName = mDigitalDetail.ItemName
                    hDigitalDetail.AlmLevel = mDigitalDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hDigitalDetail.ExtGH = mDigitalDetail.ExtGH
                    hDigitalDetail.DelayH = mDigitalDetail.DelayH
                    hDigitalDetail.GRep1H = mDigitalDetail.GRep1H
                    hDigitalDetail.GRep2H = mDigitalDetail.GRep2H
                    hDigitalDetail.FlagDmy = mDigitalDetail.FlagDmy
                    hDigitalDetail.FlagSC = mDigitalDetail.FlagSC
                    hDigitalDetail.FlagSIO = mDigitalDetail.FlagSIO
                    hDigitalDetail.FlagGWS = mDigitalDetail.FlagGWS
                    hDigitalDetail.FlagWK = mDigitalDetail.FlagWK
                    hDigitalDetail.FlagRL = mDigitalDetail.FlagRL
                    hDigitalDetail.FlagAC = mDigitalDetail.FlagAC
                    hDigitalDetail.FlagEP = mDigitalDetail.FlagEP
                    hDigitalDetail.FlagPLC = mDigitalDetail.FlagPLC     '' 2014.11.18
                    hDigitalDetail.FlagSP = mDigitalDetail.FlagSP
                    hDigitalDetail.FlagMin = mDigitalDetail.FlagMin
                    hDigitalDetail.FuNo = mDigitalDetail.FuNo
                    hDigitalDetail.FUPortNo = mDigitalDetail.FUPortNo
                    hDigitalDetail.FUPin = mDigitalDetail.FUPin
                    hDigitalDetail.DataType = mDigitalDetail.DataType
                    hDigitalDetail.PortNo = mDigitalDetail.PortNo
                    hDigitalDetail.Status = mDigitalDetail.Status
                    hDigitalDetail.FlagStatusAlarm = mDigitalDetail.FlagStatusAlarm
                    hDigitalDetail.FilterCoef = mDigitalDetail.FilterCoef
                    hDigitalDetail.EccFunc = mDigitalDetail.EccFunc
                    hDigitalDetail.ShareType = mDigitalDetail.ShareType
                    hDigitalDetail.ShareChNo = mDigitalDetail.ShareChNo
                    hDigitalDetail.Remarks = mDigitalDetail.Remarks

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hDigitalDetail.AlmMimic = mDigitalDetail.AlmMimic


                    hDigitalDetail.DeviceStatus = mDigitalDetail.DeviceStatus
                    For i As Integer = 0 To 15
                        hDigitalDetail.DeviceStatusUse(i) = mDigitalDetail.DeviceStatusUse(i)
                        hDigitalDetail.DeviceStatusCode(i) = mDigitalDetail.DeviceStatusCode(i)
                        hDigitalDetail.DeviceStatusName(i) = mDigitalDetail.DeviceStatusName(i)
                    Next

                    '▼▼▼ 20110614 仮設定機能対応（デジタル） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hDigitalDetail.DummyExtG = mDigitalDetail.DummyExtG
                    hDigitalDetail.DummyDelay = mDigitalDetail.DummyDelay
                    hDigitalDetail.DummyGroupRepose1 = mDigitalDetail.DummyGroupRepose1
                    hDigitalDetail.DummyGroupRepose2 = mDigitalDetail.DummyGroupRepose2
                    hDigitalDetail.DummyFuAddress = mDigitalDetail.DummyFuAddress
                    hDigitalDetail.DummyStatusName = mDigitalDetail.DummyStatusName
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
    Private Sub frmChListDigital_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeDigital)
            Call gSetComboBox(cmbStatusD, gEnmComboType.ctChListChannelListStatusDigital)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)
            Call gSetComboBox(cmbAlmLvl, gEnmComboType.ctChListChannelListAlmLevel)       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            Call gSetComboBox(cmbDeviceStatus, gEnmComboType.ctChListChannelListDeviceStatus)
            Call gSetComboBox(cmbStatusS, gEnmComboType.ctChListChannelListStatusDigital)

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


            With mDigitalDetail

                cmbSysNo.SelectedValue = .SysNo
                txtChNo.Text = .ChNo
                txtTagNo.Text = .TagNo   '' 2015.10.27 Ver1.7.5 ﾀｸﾞ表示追加
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

                cmbTime.SelectedValue = IIf(.FlagMin = "", 0, .FlagMin)

                txtExtGroup.Text = .ExtGH
                txtDelayTimer.Text = .DelayH
                txtGRep1.Text = .GRep1H
                txtGRep2.Text = .GRep2H

                cmbDataType.SelectedValue = .DataType

                If .DataType = gCstCodeChDataTypeDigitalDeviceStatus Then
                    ''システムCH
                    fraSystem.Visible = True
                    fraDigital.Visible = False

                    cmbDeviceStatus.SelectedValue = .DeviceStatus
                    Call cmbDeviceStatus_SelectedIndexChanged(cmbDeviceStatus, New EventArgs)

                    For i As Integer = 0 To 15
                        grdDeviceStatus(0, i).Value = .DeviceStatusUse(i)
                    Next

                    ''Status
                    Dim intValue As Integer = cmbStatusS.FindStringExact(.Status)
                    If intValue >= 0 Then
                        cmbStatusS.SelectedIndex = intValue
                    Else
                        cmbStatusS.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
                        txtStatusS.Text = .Status
                    End If

                    chkStatusAlarmS.Checked = .FlagStatusAlarm

                Else
                    ''デジタルCH
                    fraDigital.Visible = True
                    fraSystem.Visible = False

                    txtFuNo.Text = .FuNo
                    txtPortNo.Text = .FUPortNo

                    Select Case .DataType

                        Case gCstCodeChDataTypeDigitalNC, gCstCodeChDataTypeDigitalNO
                            txtPin.Text = .FUPin

                        Case gCstCodeChDataTypeDigitalJacomNC, gCstCodeChDataTypeDigitalJacomNO, gCstCodeChDataTypeDigitalJacom55NC, gCstCodeChDataTypeDigitalJacom55NO
                            cmbExtDevice.SelectedValue = .FUPin

                        Case gCstCodeChDataTypeDigitalExt
                            cmbExtDevice.SelectedValue = .EccFunc
                            'Ver2.0.1.1 PINも格納
                            txtPin.Text = .FUPin
                        Case Else   'Ver2.0.1.2 その他は、PINを格納
                            txtPin.Text = .FUPin
                    End Select

                    Dim intValue As Integer = cmbStatusD.FindStringExact(.Status)
                    If intValue >= 0 Then
                        cmbStatusD.SelectedIndex = intValue

                        txtStatusD1.Visible = False
                        txtStatusD2.Visible = False

                    Else
                        cmbStatusD.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）

                        txtStatusD1.Visible = True
                        txtStatusD2.Visible = True

                        If .Status <> "" Then
                            '' Ver1.9.6 2016.02.03 ｽﾃｰﾀｽ取得方法変更
                            GetStatusString(.Status, txtStatusD1.Text, txtStatusD2.Text)

                            '' ''2つに分解する
                            ''p = .Status.IndexOf("/")
                            ''If p >= 0 Then
                            ''    txtStatusD1.Text = .Status.Substring(0, p)
                            ''    txtStatusD2.Text = .Status.Substring(p + 1)
                            ''Else
                            ''    txtStatusD1.Text = .Status
                            ''    txtStatusD2.Text = ""
                            ''End If

                        End If

                    End If

                    chkStatusAlarmD.Checked = .FlagStatusAlarm
                    '' 2015.10.27 Ver1.7.5  ﾌｨﾙﾀｰ値の初期値をｾｯﾄ
                    '' 2015.11.16 Ver1.7.9  ﾌｨﾙﾀｰ値0の場合にｾｯﾄするように変更
                    If .FilterCoef = 0 Then
                        txtFilterCoeficientD.Text = 12
                    Else
                        txtFilterCoeficientD.Text = .FilterCoef
                    End If


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


                '▼▼▼ 20110614 仮設定機能対応（デジタル） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If mDigitalDetail.DummyExtG Then Call objDummySetControl_KeyDown(txtExtGroup, New KeyEventArgs(gCstDummySetKey))
                If mDigitalDetail.DummyDelay Then Call objDummySetControl_KeyDown(txtDelayTimer, New KeyEventArgs(gCstDummySetKey))
                If mDigitalDetail.DummyGroupRepose1 Then Call objDummySetControl_KeyDown(txtGRep1, New KeyEventArgs(gCstDummySetKey))
                If mDigitalDetail.DummyGroupRepose2 Then Call objDummySetControl_KeyDown(txtGRep2, New KeyEventArgs(gCstDummySetKey))
                If mDigitalDetail.DummyFuAddress Then Call txtFuAdrress_KeyDown(txtFuNo, New KeyEventArgs(gCstDummySetKey))
                If mDigitalDetail.DummyStatusName Then Call cmbStatus_KeyDown(cmbStatusD, New KeyEventArgs(gCstDummySetKey))
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

            mintOkFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 機器状態コンボ選択(システムCH)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbDeviceStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDeviceStatus.SelectedIndexChanged

        Try
            If mblnFlg Then Exit Sub

            Dim intValue As Integer
            Dim udtKiki() As gTypCodeName = Nothing

            ''グリッド クリア
            For i As Integer = 0 To 15
                grdDeviceStatus(0, i).Value = False
                grdDeviceStatus(1, i).Value = ""
                grdDeviceStatus(2, i).Value = ""
            Next

            ''選択済み機器状態グループコードGET
            intValue = cmbDeviceStatus.SelectedValue

            ''グループコード内の機器状態を全てGET
            Call gGetComboCodeName(udtKiki, _
                                   gEnmComboType.ctChListChannelListDeviceStatus, _
                                   intValue.ToString("00"))

            For i As Integer = 0 To UBound(udtKiki)

                grdDeviceStatus(0, i).Value = True  ''初期値はチェック有り
                grdDeviceStatus(1, i).Value = udtKiki(i).shtCode
                grdDeviceStatus(2, i).Value = udtKiki(i).strName

            Next

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

            If cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalDeviceStatus Then
                ''システムCH
                fraSystem.Visible = True
                fraDigital.Visible = False

            Else
                ''デジタルCH
                fraSystem.Visible = False
                fraDigital.Visible = True

                txtFuNo.Enabled = True : txtPortNo.Enabled = True : txtPin.Enabled = True

                Select Case cmbDataType.SelectedValue

                    Case gCstCodeChDataTypeDigitalJacomNC, gCstCodeChDataTypeDigitalJacomNO, gCstCodeChDataTypeDigitalJacom55NC, gCstCodeChDataTypeDigitalJacom55NO             '外部機器(JACOM-22)

                        'FUアドレス全て入力不可
                        txtFuNo.Enabled = False : txtPortNo.Enabled = False : txtPin.Enabled = False
                        txtFuNo.Text = "" : txtPortNo.Text = "" : txtPin.Text = ""

                        cmbExtDevice.Enabled = True
                        Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22DI)
                        cmbExtDevice.SelectedValue = 1

                        lblDeviceStatus.Text = "Ext Device"

                    Case gCstCodeChDataTypeDigitalModbusNC, gCstCodeChDataTypeDigitalModbusNO           '外部機器(MODBUS)

                        'FUのみ入力不可
                        txtFuNo.Text = "0"
                        txtFuNo.Enabled = False

                        cmbExtDevice.Enabled = False

                    Case gCstCodeChDataTypeDigitalExt                                                   '延長警報盤

                        cmbExtDevice.Enabled = True
                        Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChTerminalFunctionFuncDI)
                        cmbExtDevice.SelectedValue = 17

                        lblDeviceStatus.Text = "Ext Panel"

                    Case Else
                        cmbExtDevice.Enabled = False

                End Select


                txtFilterCoeficientD.Text = 12





                ' ''JacomのみFU Addressの入力は不可 ver.1.4.0 2011.09.22
                'If cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalJacomNC Or cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalJacomNO Then
                '    txtFuNo.Enabled = False : txtPortNo.Enabled = False : txtPin.Enabled = False
                '    lblFuAddress.Enabled = False

                'Else
                '    txtFuNo.Enabled = True : txtPortNo.Enabled = True : txtPin.Enabled = True
                '    lblFuAddress.Enabled = True
                'End If

                'Select Case cmbDataType.SelectedValue

                '    Case gCstCodeChDataTypeDigitalJacomNC, gCstCodeChDataTypeDigitalJacomNO     ''外部機器（JACOM-22）

                '        cmbExtDevice.Enabled = True
                '        Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceJACOM22DI)
                '        cmbExtDevice.SelectedValue = 1

                '        lblDeviceStatus.Text = "Ext Device"

                '    Case gCstCodeChDataTypeDigitalModbusNC, gCstCodeChDataTypeDigitalModbusNO     ''外部機器（MODBUS）

                '        '' 外部機器の場合、コンボボックス表示なし(アドレス指定) ver.1.4.0 2011.09.22
                '        cmbExtDevice.Enabled = False

                '        'FUアドレスを強制0に変更し、入力不可とする
                '        txtFuNo.Text = 0
                '        txtFuNo.Enabled = False
                '        'cmbExtDevice.Enabled = True
                '        'Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChListAnalogExtDeviceMODBUSDI)
                '        'cmbExtDevice.SelectedValue = 1

                '        'lblDeviceStatus.Text = "Ext Device"

                '    Case gCstCodeChDataTypeDigitalExt     ''延長警報盤

                '        cmbExtDevice.Enabled = True
                '        Call gSetComboBox(cmbExtDevice, gEnmComboType.ctChTerminalFunctionFuncDI)
                '        cmbExtDevice.SelectedValue = 17

                '        lblDeviceStatus.Text = "Ext Panel"

                '    Case Else
                '        cmbExtDevice.Enabled = False

                '        '2015/4/23 処理不明のためコメント T.Ueki
                '        'If Len(txtPin.Text) >= 2 Then
                '        '    txtPin.Text = ""
                '        'End If

                'End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ステータスコンボ選択(デジタルCH)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbStatusD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatusD.SelectedIndexChanged

        Try

            If cmbStatusD.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                txtStatusD1.Visible = True
                txtStatusD2.Visible = True
            Else
                txtStatusD1.Visible = False
                txtStatusD2.Visible = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ステータスコンボ選択(システムCH)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbStatusS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatusS.SelectedIndexChanged

        Try

            If cmbStatusS.SelectedValue = gCstCodeChManualInputStatus.ToString Then
                txtStatusS.Visible = True
            Else
                txtStatusS.Visible = False
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
    Private Sub frmChListDigital_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
                    Handles txtExtGroup.KeyPress, txtGRep1.KeyPress, txtGRep2.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelayTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDelayTimer.KeyPress

        Try
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilterCoeficientD.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)   '' フィルタ定数変更　ver.1.4.4 2012.05.08

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

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try
            '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8,9")

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

    'ステータスD1とD2分離し8桁入力制限　T.Ueki
    Private Sub txtStatusD1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusD1.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'ステータスD1とD2分離し8桁入力制限　T.Ueki
    Private Sub txtStatusD2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusD2.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusS.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FU_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                                    Handles txtPin.KeyPress
        Try

            'T.Ueki
            Select Case cmbDataType.SelectedValue

                Case gCstCodeChDataTypeDigitalModbusNC, gCstCodeChDataTypeDigitalModbusNO

                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                Case Else
                    e.Handled = gCheckTextInput(2, sender, e.KeyChar)

            End Select

            'e.Handled = gCheckTextInput(2, sender, e.KeyChar)

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

                Case gCstCodeChDataTypeDigitalModbusNC     ''外部機器
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case gCstCodeChDataTypeDigitalModbusNO     ''外部機器
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00000")
                Case Else
                    If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")
            End Select

            'If txtPin.Text <> "" Then txtPin.Text = CInt(txtPin.Text).ToString("00")

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
            chkStatusAlarmD.Checked = True
            chkStatusAlarmS.Checked = True
        Else
            chkStatusAlarmD.Checked = False
            chkStatusAlarmS.Checked = False
        End If
        '-----------------------------------------------------------------------------------------------
        'If txtExtGroup.Text <> "" And Val(txtExtGroup.Text) <> 0 Then
        '    chkStatusAlarmD.Checked = True
        '    chkStatusAlarmS.Checked = True
        'End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

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
            Dim strValue As String = ""

            With mDigitalDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTagNo.Text) '' 2015.10.27 Ver1.7.5 ﾀｸﾞ表示追加
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If cmbShareType.Enabled = True Then
                    .ShareType = cmbShareType.SelectedValue
                    .ShareChNo = IIf(txtShareChid.Text = "", Nothing, txtShareChid.Text)
                End If

                .ExtGH = txtExtGroup.Text
                .DelayH = txtDelayTimer.Text
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

                .FlagMin = cmbTime.SelectedValue

                .DataType = cmbDataType.SelectedValue

                If .DataType = gCstCodeChDataTypeDigitalDeviceStatus Then
                    ''システムCH
                    If cmbStatusS.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                        .Status = cmbStatusS.Text
                    Else
                        .Status = txtStatusS.Text
                    End If

                    .FlagStatusAlarm = chkStatusAlarmS.Checked

                    .DeviceStatus = cmbDeviceStatus.SelectedValue
                    'Ver2.0.0.7 システムCHの詳細ﾌﾗｸﾞはｺﾝﾊﾟｲﾙ時に自動で行うが
                    'Ver2.0.2.0 一覧保存の関係で内部的には消去はしない。
                    For i As Integer = 0 To 15
                        If Val(grdDeviceStatus(1, i).Value) <> 0 Then
                            .DeviceStatusUse(i) = grdDeviceStatus(0, i).Value
                        Else
                            .DeviceStatusUse(i) = False
                        End If
                        .DeviceStatusCode(i) = grdDeviceStatus(1, i).Value
                        .DeviceStatusName(i) = grdDeviceStatus(2, i).Value
                    Next

                Else
                    ''デジタルCH
                    .FuNo = txtFuNo.Text
                    .FUPortNo = txtPortNo.Text
                    .FUPin = txtPin.Text

                    If (cmbDataType.SelectedValue >= gCstCodeChDataTypeDigitalJacomNC And cmbDataType.SelectedValue <= gCstCodeChDataTypeDigitalJacomNO) Or _
                       (cmbDataType.SelectedValue >= gCstCodeChDataTypeDigitalModbusNC And cmbDataType.SelectedValue <= gCstCodeChDataTypeDigitalModbusNO) Then
                        .PortNo = cmbExtDevice.SelectedValue
                    ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalJacom55NC Or _
                           cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalJacom55NO Then
                        .FuNo = 0
                        .FUPortNo = 51
                        .FUPin = cmbExtDevice.SelectedValue
                        .PortNo = cmbExtDevice.SelectedValue
                    ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalExt Then
                        .EccFunc = cmbExtDevice.SelectedValue
                    End If

                    If cmbStatusD.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
                        .Status = cmbStatusD.Text
                    Else
                        If txtStatusD1.Text = "" And txtStatusD2.Text = "" Then
                            .Status = ""
                        Else
                            .Status = txtStatusD1.Text & "/" & txtStatusD2.Text
                        End If

                    End If

                    .FlagStatusAlarm = chkStatusAlarmD.Checked
                    .FilterCoef = txtFilterCoeficientD.Text

                End If

                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text


                '▼▼▼ 20110614 仮設定機能対応（デジタル） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .DummyExtG = gDummyCheckControl(txtExtGroup)
                .DummyDelay = gDummyCheckControl(txtDelayTimer)
                .DummyGroupRepose1 = gDummyCheckControl(txtGRep1)
                .DummyGroupRepose2 = gDummyCheckControl(txtGRep2)
                If .DataType = gCstCodeChDataTypeDigitalModbusNC Or .DataType = gCstCodeChDataTypeDigitalModbusNO Then  '' Ver1.8.9 2015.12.12 通信CHの場合はﾎﾟｰﾄ番号を参照
                    .DummyFuAddress = gDummyCheckControl(txtPortNo)
                Else
                    .DummyFuAddress = gDummyCheckControl(txtFuNo)
                End If
                .DummyStatusName = gDummyCheckControl(cmbStatusD)
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
            If Not gChkInputText(txtStatusD1, "Status", True, True) Then Return False
            If Not gChkInputText(txtStatusD2, "Status", True, True) Then Return False
            If Not gChkInputText(txtStatusS, "Status", True, True) Then Return False

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
            If Not gChkInputNum(txtExtGroup, 0, 24, "EXT.G", True, True) Then Return False
            If Not gChkInputNum(txtGRep1, 0, 48, "G REP1", True, True) Then Return False
            If Not gChkInputNum(txtGRep2, 0, 48, "G REP2", True, True) Then Return False
            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False
            If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If


            If cmbDataType.SelectedValue <> gCstCodeChDataTypeDigitalDeviceStatus.ToString Then
                If Not gChkInputNum(txtFilterCoeficientD, 1, 250, "Filter Coeficient", False, True) Then Return False '' フィルタ定数変更　ver.1.4.4 2012.05.08
            End If

            '' Ver1.9.5 2016.02.03  ｱﾄﾞﾚｽがﾀﾞﾐｰ設定ならばﾁｪｯｸしない
            If gDummyCheckControl(txtPortNo) = True Then
                Return True
            End If
            ''//

            ''共通FUアドレス入力チェック
            'T.Ueki
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''Select Case cmbDataType.SelectedValue

            ''    Case gCstCodeChDataTypeDigitalModbusNC, gCstCodeChDataTypeDigitalModbusNO
            ''        '' Ver1.9.5 2016.02.03  条件参照位置変更
            ''        ''If gDummyCheckControl(txtPortNo) = False Then       '' Ver1.8.9 2015.12.11  ﾀﾞﾐｰでない場合はﾁｪｯｸする
            ''        ''If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 65535, True, True) Then Return False
            ''        ''End If

            ''    Case Else
            ''        If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False

            ''End Select

            'If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False

            Return True

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
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mDigitalInfo, _
                                         ByVal udt2 As frmChListChannelList.mDigitalInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.27 Ver1.7.5 ﾀｸﾞ表示追加
            If udt1.ItemName <> udt2.ItemName Then Return False
            If udt1.AlmLevel <> udt2.AlmLevel Then Return False '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
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
            If udt1.PortNo <> udt2.PortNo Then Return False
            If udt1.Status <> udt2.Status Then Return False
            If udt1.FlagStatusAlarm <> udt2.FlagStatusAlarm Then Return False
            If udt1.FilterCoef <> udt2.FilterCoef Then Return False
            If udt1.EccFunc <> udt2.EccFunc Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False

            If udt1.DeviceStatus <> udt2.DeviceStatus Then Return False
            For i As Integer = 0 To 15
                If udt1.DeviceStatusUse(i) <> udt2.DeviceStatusUse(i) Then Return False
                If udt1.DeviceStatusCode(i) <> udt2.DeviceStatusCode(i) Then Return False
                If udt1.DeviceStatusName(i) <> udt2.DeviceStatusName(i) Then Return False
            Next

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False


            '▼▼▼ 20110614 仮設定機能対応（デジタル） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If udt1.DummyExtG <> udt2.DummyExtG Then Return False
            If udt1.DummyDelay <> udt2.DummyDelay Then Return False
            If udt1.DummyGroupRepose1 <> udt2.DummyGroupRepose1 Then Return False
            If udt1.DummyGroupRepose2 <> udt2.DummyGroupRepose2 Then Return False
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False
            If udt1.DummyStatusName <> udt2.DummyStatusName Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

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

            With grdDeviceStatus

                Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "ChkUse"
                Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtCode" : Column2.ReadOnly = True
                Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtName" : Column3.ReadOnly = True

                '列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 40
                .Columns(1).HeaderText = "Code" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "Status" : .Columns(2).Width = 190
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 17
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 40
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                    .Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells(2).Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdDeviceStatus)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "仮設定関連"

    Private Sub objDummySetControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtExtGroup.KeyDown, _
                                                                                                                         txtDelayTimer.KeyDown, _
                                                                                                                         txtGRep1.KeyDown, _
                                                                                                                         txtGRep2.KeyDown
        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(sender)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatusD.KeyDown, _
                                                                                                                txtStatusD1.KeyDown, _
                                                                                                                txtStatusD2.KeyDown, _
                                                                                                                cmbStatusS.KeyDown, _
                                                                                                                txtStatusS.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(cmbStatusD)
                Call gDummySetColorChange(txtStatusD1)
                Call gDummySetColorChange(txtStatusD2)
                Call gDummySetColorChange(cmbStatusS)
                Call gDummySetColorChange(txtStatusS)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFuAdrress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuNo.KeyDown, _
                                                                                                                   txtPortNo.KeyDown, _
                                                                                                                   txtPin.KeyDown

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

    'Private Sub txtFuNo_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFuNo.EnabledChanged, txtPortNo.EnabledChanged, txtPin.EnabledChanged

    '    If sender.Enabled Then
    '        sender.BackColor = gDummyGetBackColor(IIf(sender.Tag = "1", True, False))
    '    Else
    '        sender.Tag = IIf(gDummyCheckControl(sender), "1", "")
    '        sender.BackColor = Nothing
    '    End If

    'End Sub

#End Region

#Region "コメントアウト"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtDelayTimer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelayTimer.Validated

    '    Try

    '        If cmbTime.SelectedValue = 1 Then

    '            If IsNumeric(txtDelayTimer.Text) Then
    '                txtDelayTimer.Text = Double.Parse(txtDelayTimer.Text).ToString("0.0")
    '            Else
    '                txtDelayTimer.Text = ""
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
    '            If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) * 60)
    '        Else
    '            ''秒 --> 分
    '            If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) / 60, "0.0")
    '        End If

    '    End If

    '    mintDelayTimeKubun = cmbTime.SelectedValue

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

    '    Dim strValue As String = ""

    '    With mDigitalDetail

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

    '        If .FuNo <> txtFuNo.Text Then Return True
    '        If .FUPortNo <> txtPortNo.Text Then Return True
    '        If .FUPin <> txtPin.Text Then Return True

    '        If .DataType <> cmbDataType.SelectedValue Then Return True

    '        If cmbDataType.SelectedValue >= gCstCodeChDataTypeDigitalJacomNC And _
    '           cmbDataType.SelectedValue <= gCstCodeChDataTypeDigitalModbusNO Then
    '            If .PortNo <> cmbExtDevice.SelectedValue Then Return True
    '        ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeDigitalExt Then
    '            If .EccFunc <> cmbExtDevice.SelectedValue Then Return True
    '        End If

    '        If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '            If .Status <> cmbStatus.Text Then Return True
    '        Else

    '            If txtStatus2.Text.Trim <> "" Then
    '                strValue = txtStatus1.Text.Trim.PadRight(8)
    '                strValue += txtStatus2.Text.Trim.PadRight(8)
    '            Else
    '                strValue = txtStatus1.Text.Trim.PadRight(16)
    '            End If
    '            If .Status <> strValue Then Return True

    '        End If

    '        If .FlagStatusAlarm <> chkStatusAlarm.Checked Then Return True
    '        If .FilterCoef <> txtFilterCoeficient.Text Then Return True

    '    End With

    '    Return False

    'End Function

#End Region

End Class
