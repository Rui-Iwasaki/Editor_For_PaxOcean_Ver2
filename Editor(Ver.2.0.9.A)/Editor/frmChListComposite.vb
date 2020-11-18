Public Class frmChListComposite

#Region "変数定義"

    Private mudtSetChCompositeNew As gTypSetChComposite

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

    Private mMode As Integer

    ' ''デジタルコンポジットチャンネル情報格納
    Private mCompositeDetail As frmChListChannelList.mCompositeInfo

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
    ' 引き数    : ARG1 - (IO) デジタルコンポジットチャンネル情報
    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hCompositeDetail As frmChListChannelList.mCompositeInfo, _
                          ByRef hblnCompositeTableUse() As Boolean, _
                          ByRef hMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = -1

            mMode = hMode

            'mCompositeDetail.Mode = hMode
            mCompositeDetail.RowNo = hCompositeDetail.RowNo
            mCompositeDetail.RowNoFirst = hCompositeDetail.RowNoFirst
            mCompositeDetail.RowNoEnd = hCompositeDetail.RowNoEnd
            mCompositeDetail.SysNo = hCompositeDetail.SysNo
            mCompositeDetail.ChNo = hCompositeDetail.ChNo
            mCompositeDetail.TagNo = hCompositeDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
            mCompositeDetail.ItemName = hCompositeDetail.ItemName
            mCompositeDetail.AlmLevel = hCompositeDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
            mCompositeDetail.ExtGH = hCompositeDetail.ExtGH
            mCompositeDetail.DelayH = hCompositeDetail.DelayH
            mCompositeDetail.GRep1H = hCompositeDetail.GRep1H
            mCompositeDetail.GRep2H = hCompositeDetail.GRep2H
            mCompositeDetail.FlagDmy = hCompositeDetail.FlagDmy
            mCompositeDetail.FlagSC = hCompositeDetail.FlagSC
            mCompositeDetail.FlagSIO = hCompositeDetail.FlagSIO
            mCompositeDetail.FlagGWS = hCompositeDetail.FlagGWS
            mCompositeDetail.FlagWK = hCompositeDetail.FlagWK
            mCompositeDetail.FlagRL = hCompositeDetail.FlagRL
            mCompositeDetail.FlagAC = hCompositeDetail.FlagAC
            mCompositeDetail.FlagEP = hCompositeDetail.FlagEP
            mCompositeDetail.FlagPLC = hCompositeDetail.FlagPLC     '' 2014.11.18
            mCompositeDetail.FlagSP = hCompositeDetail.FlagSP
            mCompositeDetail.FlagMin = hCompositeDetail.FlagMin
            mCompositeDetail.BitCount = hCompositeDetail.BitCount

            mCompositeDetail.StartNo = hCompositeDetail.StartNo
            mCompositeDetail.StartPortNo = hCompositeDetail.StartPortNo
            mCompositeDetail.StartPinNo = hCompositeDetail.StartPinNo

            mCompositeDetail.DataType = hCompositeDetail.DataType
            mCompositeDetail.CompositeStatus = hCompositeDetail.CompositeStatus
            mCompositeDetail.ShareType = hCompositeDetail.ShareType
            mCompositeDetail.ShareChNo = hCompositeDetail.ShareChNo
            mCompositeDetail.Remarks = hCompositeDetail.Remarks
            mCompositeDetail.Status = hCompositeDetail.Status

            mCompositeDetail.CompositeIndex = hCompositeDetail.CompositeIndex

            mblnCompositeTableUse = hblnCompositeTableUse

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            mCompositeDetail.AlmMimic = hCompositeDetail.AlmMimic


            '▼▼▼ 20110614 仮設定機能対応（コンポジット） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            mCompositeDetail.DummyExtG = hCompositeDetail.DummyExtG
            mCompositeDetail.DummyDelay = hCompositeDetail.DummyDelay
            mCompositeDetail.DummyGroupRepose1 = hCompositeDetail.DummyGroupRepose1
            mCompositeDetail.DummyGroupRepose2 = hCompositeDetail.DummyGroupRepose2
            mCompositeDetail.DummyFuAddress = hCompositeDetail.DummyFuAddress
            mCompositeDetail.DummyBitCount = hCompositeDetail.DummyBitCount
            mCompositeDetail.DummyStatusName = hCompositeDetail.DummyStatusName

            mCompositeDetail.DummyCmpStatus1Delay = hCompositeDetail.DummyCmpStatus1Delay
            mCompositeDetail.DummyCmpStatus1ExtGr = hCompositeDetail.DummyCmpStatus1ExtGr
            mCompositeDetail.DummyCmpStatus1GRep1 = hCompositeDetail.DummyCmpStatus1GRep1
            mCompositeDetail.DummyCmpStatus1GRep2 = hCompositeDetail.DummyCmpStatus1GRep2
            mCompositeDetail.DummyCmpStatus1StaNm = hCompositeDetail.DummyCmpStatus1StaNm

            mCompositeDetail.DummyCmpStatus2Delay = hCompositeDetail.DummyCmpStatus2Delay
            mCompositeDetail.DummyCmpStatus2ExtGr = hCompositeDetail.DummyCmpStatus2ExtGr
            mCompositeDetail.DummyCmpStatus2GRep1 = hCompositeDetail.DummyCmpStatus2GRep1
            mCompositeDetail.DummyCmpStatus2GRep2 = hCompositeDetail.DummyCmpStatus2GRep2
            mCompositeDetail.DummyCmpStatus2StaNm = hCompositeDetail.DummyCmpStatus2StaNm

            mCompositeDetail.DummyCmpStatus3Delay = hCompositeDetail.DummyCmpStatus3Delay
            mCompositeDetail.DummyCmpStatus3ExtGr = hCompositeDetail.DummyCmpStatus3ExtGr
            mCompositeDetail.DummyCmpStatus3GRep1 = hCompositeDetail.DummyCmpStatus3GRep1
            mCompositeDetail.DummyCmpStatus3GRep2 = hCompositeDetail.DummyCmpStatus3GRep2
            mCompositeDetail.DummyCmpStatus3StaNm = hCompositeDetail.DummyCmpStatus3StaNm

            mCompositeDetail.DummyCmpStatus4Delay = hCompositeDetail.DummyCmpStatus4Delay
            mCompositeDetail.DummyCmpStatus4ExtGr = hCompositeDetail.DummyCmpStatus4ExtGr
            mCompositeDetail.DummyCmpStatus4GRep1 = hCompositeDetail.DummyCmpStatus4GRep1
            mCompositeDetail.DummyCmpStatus4GRep2 = hCompositeDetail.DummyCmpStatus4GRep2
            mCompositeDetail.DummyCmpStatus4StaNm = hCompositeDetail.DummyCmpStatus4StaNm

            mCompositeDetail.DummyCmpStatus5Delay = hCompositeDetail.DummyCmpStatus5Delay
            mCompositeDetail.DummyCmpStatus5ExtGr = hCompositeDetail.DummyCmpStatus5ExtGr
            mCompositeDetail.DummyCmpStatus5GRep1 = hCompositeDetail.DummyCmpStatus5GRep1
            mCompositeDetail.DummyCmpStatus5GRep2 = hCompositeDetail.DummyCmpStatus5GRep2
            mCompositeDetail.DummyCmpStatus5StaNm = hCompositeDetail.DummyCmpStatus5StaNm

            mCompositeDetail.DummyCmpStatus6Delay = hCompositeDetail.DummyCmpStatus6Delay
            mCompositeDetail.DummyCmpStatus6ExtGr = hCompositeDetail.DummyCmpStatus6ExtGr
            mCompositeDetail.DummyCmpStatus6GRep1 = hCompositeDetail.DummyCmpStatus6GRep1
            mCompositeDetail.DummyCmpStatus6GRep2 = hCompositeDetail.DummyCmpStatus6GRep2
            mCompositeDetail.DummyCmpStatus6StaNm = hCompositeDetail.DummyCmpStatus6StaNm

            mCompositeDetail.DummyCmpStatus7Delay = hCompositeDetail.DummyCmpStatus7Delay
            mCompositeDetail.DummyCmpStatus7ExtGr = hCompositeDetail.DummyCmpStatus7ExtGr
            mCompositeDetail.DummyCmpStatus7GRep1 = hCompositeDetail.DummyCmpStatus7GRep1
            mCompositeDetail.DummyCmpStatus7GRep2 = hCompositeDetail.DummyCmpStatus7GRep2
            mCompositeDetail.DummyCmpStatus7StaNm = hCompositeDetail.DummyCmpStatus7StaNm

            mCompositeDetail.DummyCmpStatus8Delay = hCompositeDetail.DummyCmpStatus8Delay
            mCompositeDetail.DummyCmpStatus8ExtGr = hCompositeDetail.DummyCmpStatus8ExtGr
            mCompositeDetail.DummyCmpStatus8GRep1 = hCompositeDetail.DummyCmpStatus8GRep1
            mCompositeDetail.DummyCmpStatus8GRep2 = hCompositeDetail.DummyCmpStatus8GRep2
            mCompositeDetail.DummyCmpStatus8StaNm = hCompositeDetail.DummyCmpStatus8StaNm

            mCompositeDetail.DummyCmpStatus9Delay = hCompositeDetail.DummyCmpStatus9Delay
            mCompositeDetail.DummyCmpStatus9ExtGr = hCompositeDetail.DummyCmpStatus9ExtGr
            mCompositeDetail.DummyCmpStatus9GRep1 = hCompositeDetail.DummyCmpStatus9GRep1
            mCompositeDetail.DummyCmpStatus9GRep2 = hCompositeDetail.DummyCmpStatus9GRep2
            mCompositeDetail.DummyCmpStatus9StaNm = hCompositeDetail.DummyCmpStatus9StaNm
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintOkFlag = 1 Then

                ''構造体の設定値を比較する
                If mChkStructureEquals(hCompositeDetail, mCompositeDetail) = False Then

                    hCompositeDetail.SysNo = mCompositeDetail.SysNo
                    hCompositeDetail.ChNo = mCompositeDetail.ChNo
                    hCompositeDetail.TagNo = mCompositeDetail.TagNo   '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
                    hCompositeDetail.ItemName = mCompositeDetail.ItemName
                    hCompositeDetail.AlmLevel = mCompositeDetail.AlmLevel       '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                    hCompositeDetail.ExtGH = mCompositeDetail.ExtGH
                    hCompositeDetail.DelayH = mCompositeDetail.DelayH
                    hCompositeDetail.GRep1H = mCompositeDetail.GRep1H
                    hCompositeDetail.GRep2H = mCompositeDetail.GRep2H
                    hCompositeDetail.FlagDmy = mCompositeDetail.FlagDmy
                    hCompositeDetail.FlagSC = mCompositeDetail.FlagSC
                    hCompositeDetail.FlagSIO = mCompositeDetail.FlagSIO
                    hCompositeDetail.FlagGWS = mCompositeDetail.FlagGWS
                    hCompositeDetail.FlagWK = mCompositeDetail.FlagWK
                    hCompositeDetail.FlagRL = mCompositeDetail.FlagRL
                    hCompositeDetail.FlagAC = mCompositeDetail.FlagAC
                    hCompositeDetail.FlagEP = mCompositeDetail.FlagEP
                    hCompositeDetail.FlagPLC = mCompositeDetail.FlagPLC     '' 2014.11.18
                    hCompositeDetail.FlagSP = mCompositeDetail.FlagSP
                    hCompositeDetail.FlagMin = mCompositeDetail.FlagMin
                    hCompositeDetail.BitCount = mCompositeDetail.BitCount

                    hCompositeDetail.StartNo = mCompositeDetail.StartNo
                    hCompositeDetail.StartPortNo = mCompositeDetail.StartPortNo
                    hCompositeDetail.StartPinNo = mCompositeDetail.StartPinNo

                    hCompositeDetail.DataType = mCompositeDetail.DataType
                    hCompositeDetail.CompositeStatus = mCompositeDetail.CompositeStatus
                    hCompositeDetail.ShareType = mCompositeDetail.ShareType
                    hCompositeDetail.ShareChNo = mCompositeDetail.ShareChNo
                    hCompositeDetail.Remarks = mCompositeDetail.Remarks
                    hCompositeDetail.Status = mCompositeDetail.Status

                    hCompositeDetail.CompositeIndex = mCompositeDetail.CompositeIndex

                    hblnCompositeTableUse = mblnCompositeTableUse

                    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    hCompositeDetail.AlmMimic = mCompositeDetail.AlmMimic


                    '▼▼▼ 20110614 仮設定機能対応（コンポジット） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    hCompositeDetail.DummyExtG = mCompositeDetail.DummyExtG
                    hCompositeDetail.DummyDelay = mCompositeDetail.DummyDelay
                    hCompositeDetail.DummyGroupRepose1 = mCompositeDetail.DummyGroupRepose1
                    hCompositeDetail.DummyGroupRepose2 = mCompositeDetail.DummyGroupRepose2
                    hCompositeDetail.DummyFuAddress = mCompositeDetail.DummyFuAddress
                    hCompositeDetail.DummyBitCount = mCompositeDetail.DummyBitCount
                    hCompositeDetail.DummyStatusName = mCompositeDetail.DummyStatusName

                    hCompositeDetail.DummyCmpStatus1Delay = mCompositeDetail.DummyCmpStatus1Delay
                    hCompositeDetail.DummyCmpStatus1ExtGr = mCompositeDetail.DummyCmpStatus1ExtGr
                    hCompositeDetail.DummyCmpStatus1GRep1 = mCompositeDetail.DummyCmpStatus1GRep1
                    hCompositeDetail.DummyCmpStatus1GRep2 = mCompositeDetail.DummyCmpStatus1GRep2
                    hCompositeDetail.DummyCmpStatus1StaNm = mCompositeDetail.DummyCmpStatus1StaNm

                    hCompositeDetail.DummyCmpStatus2Delay = mCompositeDetail.DummyCmpStatus2Delay
                    hCompositeDetail.DummyCmpStatus2ExtGr = mCompositeDetail.DummyCmpStatus2ExtGr
                    hCompositeDetail.DummyCmpStatus2GRep1 = mCompositeDetail.DummyCmpStatus2GRep1
                    hCompositeDetail.DummyCmpStatus2GRep2 = mCompositeDetail.DummyCmpStatus2GRep2
                    hCompositeDetail.DummyCmpStatus2StaNm = mCompositeDetail.DummyCmpStatus2StaNm

                    hCompositeDetail.DummyCmpStatus3Delay = mCompositeDetail.DummyCmpStatus3Delay
                    hCompositeDetail.DummyCmpStatus3ExtGr = mCompositeDetail.DummyCmpStatus3ExtGr
                    hCompositeDetail.DummyCmpStatus3GRep1 = mCompositeDetail.DummyCmpStatus3GRep1
                    hCompositeDetail.DummyCmpStatus3GRep2 = mCompositeDetail.DummyCmpStatus3GRep2
                    hCompositeDetail.DummyCmpStatus3StaNm = mCompositeDetail.DummyCmpStatus3StaNm

                    hCompositeDetail.DummyCmpStatus4Delay = mCompositeDetail.DummyCmpStatus4Delay
                    hCompositeDetail.DummyCmpStatus4ExtGr = mCompositeDetail.DummyCmpStatus4ExtGr
                    hCompositeDetail.DummyCmpStatus4GRep1 = mCompositeDetail.DummyCmpStatus4GRep1
                    hCompositeDetail.DummyCmpStatus4GRep2 = mCompositeDetail.DummyCmpStatus4GRep2
                    hCompositeDetail.DummyCmpStatus4StaNm = mCompositeDetail.DummyCmpStatus4StaNm

                    hCompositeDetail.DummyCmpStatus5Delay = mCompositeDetail.DummyCmpStatus5Delay
                    hCompositeDetail.DummyCmpStatus5ExtGr = mCompositeDetail.DummyCmpStatus5ExtGr
                    hCompositeDetail.DummyCmpStatus5GRep1 = mCompositeDetail.DummyCmpStatus5GRep1
                    hCompositeDetail.DummyCmpStatus5GRep2 = mCompositeDetail.DummyCmpStatus5GRep2
                    hCompositeDetail.DummyCmpStatus5StaNm = mCompositeDetail.DummyCmpStatus5StaNm

                    hCompositeDetail.DummyCmpStatus6Delay = mCompositeDetail.DummyCmpStatus6Delay
                    hCompositeDetail.DummyCmpStatus6ExtGr = mCompositeDetail.DummyCmpStatus6ExtGr
                    hCompositeDetail.DummyCmpStatus6GRep1 = mCompositeDetail.DummyCmpStatus6GRep1
                    hCompositeDetail.DummyCmpStatus6GRep2 = mCompositeDetail.DummyCmpStatus6GRep2
                    hCompositeDetail.DummyCmpStatus6StaNm = mCompositeDetail.DummyCmpStatus6StaNm

                    hCompositeDetail.DummyCmpStatus7Delay = mCompositeDetail.DummyCmpStatus7Delay
                    hCompositeDetail.DummyCmpStatus7ExtGr = mCompositeDetail.DummyCmpStatus7ExtGr
                    hCompositeDetail.DummyCmpStatus7GRep1 = mCompositeDetail.DummyCmpStatus7GRep1
                    hCompositeDetail.DummyCmpStatus7GRep2 = mCompositeDetail.DummyCmpStatus7GRep2
                    hCompositeDetail.DummyCmpStatus7StaNm = mCompositeDetail.DummyCmpStatus7StaNm

                    hCompositeDetail.DummyCmpStatus8Delay = mCompositeDetail.DummyCmpStatus8Delay
                    hCompositeDetail.DummyCmpStatus8ExtGr = mCompositeDetail.DummyCmpStatus8ExtGr
                    hCompositeDetail.DummyCmpStatus8GRep1 = mCompositeDetail.DummyCmpStatus8GRep1
                    hCompositeDetail.DummyCmpStatus8GRep2 = mCompositeDetail.DummyCmpStatus8GRep2
                    hCompositeDetail.DummyCmpStatus8StaNm = mCompositeDetail.DummyCmpStatus8StaNm

                    hCompositeDetail.DummyCmpStatus9Delay = mCompositeDetail.DummyCmpStatus9Delay
                    hCompositeDetail.DummyCmpStatus9ExtGr = mCompositeDetail.DummyCmpStatus9ExtGr
                    hCompositeDetail.DummyCmpStatus9GRep1 = mCompositeDetail.DummyCmpStatus9GRep1
                    hCompositeDetail.DummyCmpStatus9GRep2 = mCompositeDetail.DummyCmpStatus9GRep2
                    hCompositeDetail.DummyCmpStatus9StaNm = mCompositeDetail.DummyCmpStatus9StaNm
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
    Private Sub frmChListComposite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim p As Integer
            Dim strFuno As String = ""

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)
            If gblnDispOtherForm Then cmdCompositeEdit.Enabled = False

            ''画面を初期化する
            Call mInitial()

            ''グリッドを初期化する
            Call mInitialDataGrid()
            Call gCompInitControl(grdBitStatusMap, grdAnyMap, txtFilterCoeficient, False)

            ''共通項目セット
            With mCompositeDetail

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

                If .Status <> "" Then
                    ''2つに分解する
                    p = .Status.IndexOf("/")
                    If p >= 0 Then
                        txtStatus1.Text = .Status.Substring(0, p)
                        txtStatus2.Text = .Status.Substring(p + 1)
                    Else
                        txtStatus1.Text = .Status
                        txtStatus2.Text = ""
                    End If

                    '    If .Status.Length > 8 Then
                    '        txtStatus1.Text = .Status.Substring(0, 8).Trim
                    '        txtStatus2.Text = .Status.Substring(8).Trim
                    '    Else
                    '        txtStatus1.Text = .Status
                    '        txtStatus2.Text = ""
                    '    End If

                    '    .Status = txtStatus1.Text.PadRight(8)
                    '    .Status += txtStatus2.Text.PadRight(8)
                    'Else
                    '    .Status = "".PadRight(16)
                End If

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

                If .BitCount = "0" Then
                    txtBitCount.Text = ""
                    .BitCount = ""
                End If

                If .StartNo <> "" Then

                    txtFuNo.Text = Trim(.StartNo)
                    txtPortNo.Text = Trim(.StartPortNo)
                    txtPin.Text = Trim(.StartPinNo)

                End If

                txtBitCount.Text = .BitCount

                If txtFuNo.Text <> "" And txtPortNo.Text <> "" And txtPin.Text <> "" And txtBitCount.Text <> "" Then

                    If .BitCount >= 1 Then lblDi1.Text = txtFuNo.Text & txtPortNo.Text & CInt(txtPin.Text).ToString("00")
                    If .BitCount >= 2 Then lblDi2.Text = txtFuNo.Text & txtPortNo.Text & (1 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 3 Then lblDi3.Text = txtFuNo.Text & txtPortNo.Text & (2 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 4 Then lblDi4.Text = txtFuNo.Text & txtPortNo.Text & (3 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 5 Then lblDi5.Text = txtFuNo.Text & txtPortNo.Text & (4 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 6 Then lblDi6.Text = txtFuNo.Text & txtPortNo.Text & (5 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 7 Then lblDi7.Text = txtFuNo.Text & txtPortNo.Text & (6 + CInt(txtPin.Text)).ToString("00")
                    If .BitCount >= 8 Then lblDi8.Text = txtFuNo.Text & txtPortNo.Text & (7 + CInt(txtPin.Text)).ToString("00")

                End If

                ''-----------------------------------------------------------
                txtCompositeIndex.Text = IIf(.CompositeIndex = 0, "", .CompositeIndex)
                txtCompositeIndex.ReadOnly = True
                txtCompositeIndex.BackColor = gColorGridRowBackReadOnly


                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                If .AlmMimic = "0" Then
                    txtAlmMimic.Text = ""
                Else
                    txtAlmMimic.Text = .AlmMimic
                End If


                ''配列再定義
                Call mudtSetChCompositeNew.InitArray()
                For i As Integer = 0 To UBound(mudtSetChCompositeNew.udtComposite)
                    mudtSetChCompositeNew.udtComposite(i).InitArray()
                Next

                ''構造体のコピー
                Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

                ''設定表示
                If .CompositeIndex <> 0 Then

                    Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(.CompositeIndex - 1), _
                                         grdBitStatusMap, _
                                         grdAnyMap, _
                                         txtFilterCoeficient)
                End If
                ''-----------------------------------------------------------

                'If .Mode = 5 Then
                If mMode = 5 Then
                    ''Composite Status ボタン クリックから遷移
                    cmdBeforeCH.Enabled = False
                    cmdNextCH.Enabled = False
                Else
                    cmdBeforeCH.Enabled = True
                    cmdNextCH.Enabled = True
                    If .RowNoFirst = .RowNo Then cmdBeforeCH.Enabled = False
                    If .RowNoEnd = .RowNo Then cmdNextCH.Enabled = False
                End If

            End With

            '▼▼▼ 20110614 仮設定機能対応（コンポジット） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If mCompositeDetail.DummyExtG Then Call objDummySetControl_KeyDown(txtExtGroup, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyDelay Then Call objDummySetControl_KeyDown(txtDelayTimer, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyGroupRepose1 Then Call objDummySetControl_KeyDown(txtGRep1, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyGroupRepose2 Then Call objDummySetControl_KeyDown(txtGRep2, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyFuAddress Then Call txtFuAdrress_KeyDown(txtFuNo, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyBitCount Then Call objDummySetControl_KeyDown(txtBitCount, New KeyEventArgs(gCstDummySetKey))
            If mCompositeDetail.DummyStatusName Then Call cmbStatus_KeyDown(txtStatus1, New KeyEventArgs(gCstDummySetKey))
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

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
    Private Sub frmChListComposite_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    ' 機能説明  ： SELECTボタン　クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： コンポジットテーブル編集画面へ遷移する
    '----------------------------------------------------------------------------
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        Try

            If frmChCompositeList.gShow2(True, _
                                    Val(txtChNo.Text), _
                                    mCompositeDetail.CompositeIndex, _
                                    Me, _
                                    gEnmCompositeEditType.cetComposite) = 1 Then

                ''選択テーブル番号SET
                txtCompositeIndex.Text = mCompositeDetail.CompositeIndex.ToString

                ''構造体のコピー
                Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

                ''再表示
                Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(mCompositeDetail.CompositeIndex - 1), _
                                             grdBitStatusMap, _
                                             grdAnyMap, _
                                             txtFilterCoeficient)

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

            Dim intIndex As Integer = 0

            If frmChCompositeList.gShowEdit(True, _
                                            Val(txtChNo.Text), _
                                            mCompositeDetail.CompositeIndex, _
                                            mCompositeDetail, _
                                            mblnCompositeTableUse, _
                                            Me, _
                                            gEnmCompositeEditType.cetComposite) = 1 Then

                ''選択テーブル番号SET
                txtCompositeIndex.Text = mCompositeDetail.CompositeIndex.ToString

                ''テーブル使用フラグTrue
                mblnCompositeTableUse(mCompositeDetail.CompositeIndex - 1) = True

                ''構造体のコピー
                Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

                ''再表示
                intIndex = mCompositeDetail.CompositeIndex
                intIndex = IIf(intIndex = 0, 0, intIndex - 1)

                Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(intIndex), _
                                     grdBitStatusMap, _
                                     grdAnyMap, _
                                     txtFilterCoeficient)

            End If

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

            If cmbDataType.SelectedValue = gCstCodeChDataTypeCompTankLevel Then
                ''タンクレベル（代表ステータス）
                txtStatus1.Visible = True : lblStatus.Visible = True
                txtStatus2.Visible = True

            Else
                txtStatus1.Visible = False : lblStatus.Visible = False
                txtStatus2.Visible = False
                txtStatus1.Text = ""
                txtStatus2.Text = ""
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
    ' 機能説明  ： Start Terminal No の連続するFU Addressをセットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFuAddress_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                Handles txtFuNo.Validated, txtPortNo.Validated, txtPin.Validated, _
                                                        txtBitCount.Validated

        Try

            Dim intBitCnt As Integer = Val(txtBitCount.Text)

            lblDi1.Text = ""
            lblDi2.Text = ""
            lblDi3.Text = ""
            lblDi4.Text = ""
            lblDi5.Text = ""
            lblDi6.Text = ""
            lblDi7.Text = ""
            lblDi8.Text = ""

            If txtFuNo.Text <> "" And txtPortNo.Text <> "" And txtPin.Text <> "" Then

                txtPin.Text = CInt(txtPin.Text).ToString("00")

                If intBitCnt >= 1 Then lblDi1.Text = txtFuNo.Text & txtPortNo.Text & CInt(txtPin.Text).ToString("00")

                If intBitCnt >= 2 Then
                    If 1 + CInt(txtPin.Text) <= 64 Then
                        lblDi2.Text = txtFuNo.Text & txtPortNo.Text & (1 + CInt(txtPin.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 3 Then
                    If 2 + CInt(txtPin.Text) <= 64 Then
                        lblDi3.Text = txtFuNo.Text & txtPortNo.Text & (2 + CInt(txtPin.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 4 Then
                    If 3 + CInt(txtPin.Text) <= 64 Then
                        lblDi4.Text = txtFuNo.Text & txtPortNo.Text & (3 + CInt(txtPin.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 5 Then
                    If 4 + CInt(txtPin.Text) <= 64 Then
                        lblDi5.Text = txtFuNo.Text & txtPortNo.Text & (4 + CInt(txtPin.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 6 Then
                    If 5 + CInt(txtPin.Text) <= 64 Then
                        lblDi6.Text = txtFuNo.Text & txtPortNo.Text & (5 + CInt(txtPin.Text)).ToString("00")
                    End If
                End If

                If intBitCnt >= 7 Then
                    If 6 + CInt(txtPin.Text) <= 64 Then
                        lblDi7.Text = txtFuNo.Text & txtPortNo.Text & (6 + CInt(txtPin.Text)).ToString("00")

                    End If
                End If

                If intBitCnt >= 8 Then
                    If 7 + CInt(txtPin.Text) <= 64 Then
                        lblDi8.Text = txtFuNo.Text & txtPortNo.Text & (7 + CInt(txtPin.Text)).ToString("00")
                    End If
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

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtRemarks.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtStatusD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtStatus1.KeyPress, txtStatus2.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAlarm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                    Handles txtExtGroup.KeyPress, txtGRep1.KeyPress, txtGRep2.KeyPress, txtPin.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDelayTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDelayTimer.KeyPress

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

    Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)


        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)       '' フィルタ定数変更　ver.1.4.4 2012.05.08

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtBitCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBitCount.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8")

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

    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdBitStatusMap_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intColumn As Integer

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdBitStatusMap_KeyPress

                ''該当する列ならイベントハンドラを追加する
                intColumn = dgv.CurrentCell.OwningColumn.Index

                If intColumn = 3 Or intColumn = 4 Or intColumn = 5 Or intColumn = 6 Or intColumn = 15 Then

                    AddHandler tb.KeyPress, AddressOf grdBitStatusMap_KeyPress

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdAnyMap_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intColumn As Integer

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdAnyMap_KeyPress

                ''該当する列ならイベントハンドラを追加する
                intColumn = dgv.CurrentCell.OwningColumn.Index

                If intColumn >= 3 And intColumn <= 76 Then

                    AddHandler tb.KeyPress, AddressOf grdAnyMap_KeyPress

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdBitStatusMap_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try
            ''KeyDownイベントを再度Call(USEを変更後、ReadOnlyを解除して再度ペースト)
            If Asc(e.KeyChar) = 22 Then 'Ctl + V

                If grdBitStatusMap.CurrentCell.OwningColumn.Name = "chkUse" Then

                    Dim cls As New clsDataGridViewPlus

                    ' クリップボードの内容から複数行のCOPYをした場合の行数をGET
                    Dim clipText As String = Clipboard.GetText()

                    ' 改行を変換
                    clipText = clipText.Replace(vbCrLf, vbLf)
                    clipText = clipText.Replace(vbCr, vbLf)

                    ' 改行で分割
                    Dim lines() As String = clipText.Split(vbLf)

                    For i As Integer = 0 To UBound(lines)

                        Call grdBitStatusMap_CellContentClick(grdBitStatusMap, New DataGridViewCellEventArgs(0, grdBitStatusMap.CurrentCell.RowIndex + i))

                    Next

                    Call grdBitStatusMap_CellContentClick(grdBitStatusMap, New DataGridViewCellEventArgs(0, grdBitStatusMap.CurrentCell.RowIndex))
                    Call cls.DataGridViewPlus_KeyDown(grdBitStatusMap, New KeyEventArgs(Keys.Control + Keys.V))
                    cls.Dispose()

                End If

            End If

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub
            If grdBitStatusMap.CurrentCell.ReadOnly Then Exit Sub

            Dim intColumn As Integer = grdBitStatusMap.CurrentCell.OwningColumn.Index

            Select Case intColumn
                Case 3, 5, 6        ''EXT G,  Grep1,  Grep2

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(2, dgv, e.KeyChar, True)

                Case 4              ''Delay

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(3, dgv, e.KeyChar, True)

                Case 15             ''Status Name

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(16, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdAnyMap_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try
            ''KeyDownイベントを再度Call(USEを変更後、ReadOnlyを解除して再度ペースト)
            If Asc(e.KeyChar) = 22 Then 'Ctl + V

                If grdAnyMap.CurrentCell.OwningColumn.Name = "chkUse" Then

                    Dim cls As New clsDataGridViewPlus
                    Call grdAnyMap_CellContentClick(grdAnyMap, New DataGridViewCellEventArgs(0, grdAnyMap.CurrentCell.RowIndex))
                    Call cls.DataGridViewPlus_KeyDown(grdAnyMap, New KeyEventArgs(Keys.Control + Keys.V))
                    cls.Dispose()

                End If

            End If

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub
            If grdBitStatusMap.CurrentCell.ReadOnly Then Exit Sub

            Dim intColumn As Integer = grdAnyMap.CurrentCell.OwningColumn.Index

            Select Case intColumn
                Case 3, 5, 6    ''EXT G,  Grep1,  Grep2

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(2, dgv, e.KeyChar, True)

                Case 4          ''Delay

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(3, dgv, e.KeyChar, True)

                Case 7          ''Status Name

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(16, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド　クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： Useチェック時に対象行を設定可にする
    '----------------------------------------------------------------------------
    Private Sub grdBitStatusMap_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        If e.ColumnIndex <> 0 Or e.RowIndex < 0 Then Exit Sub

        grdBitStatusMap.EndEdit()

        ''使用可/不可　切替
        'If grdBitStatusMap(0, e.RowIndex).EditedFormattedValue = True Then

        '    For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1
        '        grdBitStatusMap(i, e.RowIndex).ReadOnly = False
        '        If e.RowIndex Mod 2 <> 0 Then
        '            grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBack
        '        Else
        '            grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
        '        End If
        '    Next

        'Else
        '    grdBitStatusMap(1, e.RowIndex).Value = False
        '    grdBitStatusMap(2, e.RowIndex).Value = False
        '    grdBitStatusMap(3, e.RowIndex).Value = ""
        '    grdBitStatusMap(4, e.RowIndex).Value = ""
        '    grdBitStatusMap(5, e.RowIndex).Value = ""
        '    grdBitStatusMap(6, e.RowIndex).Value = ""
        '    For i As Integer = 7 To 14
        '        grdBitStatusMap(i, e.RowIndex).Value = False
        '    Next
        '    grdBitStatusMap(15, e.RowIndex).Value = ""

        '    For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1
        '        grdBitStatusMap(i, e.RowIndex).ReadOnly = True
        '        grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
        '    Next

        'End If

    End Sub
    Private Sub grdBitStatusMap_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = 0 Then

            ''使用可/不可　切替
            If grdBitStatusMap(0, e.RowIndex).Value = True Then

                For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1
                    grdBitStatusMap(i, e.RowIndex).ReadOnly = False
                    If e.RowIndex Mod 2 <> 0 Then
                        grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBack
                    Else
                        grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                    End If
                Next

            Else
                grdBitStatusMap(1, e.RowIndex).Value = False
                grdBitStatusMap(2, e.RowIndex).Value = False
                grdBitStatusMap(3, e.RowIndex).Value = ""
                grdBitStatusMap(4, e.RowIndex).Value = ""
                grdBitStatusMap(5, e.RowIndex).Value = ""
                grdBitStatusMap(6, e.RowIndex).Value = ""
                For i As Integer = 7 To 14
                    grdBitStatusMap(i, e.RowIndex).Value = False
                Next
                grdBitStatusMap(15, e.RowIndex).Value = ""

                For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1
                    grdBitStatusMap(i, e.RowIndex).ReadOnly = True
                    grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                Next

            End If

        End If

    End Sub

    Private Sub grdAnyMap_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        If e.ColumnIndex <> 0 Or e.RowIndex < 0 Then Exit Sub

        grdAnyMap.EndEdit()

        ''使用可/不可　切替
        'If grdAnyMap(0, e.RowIndex).EditedFormattedValue = True Then

        '    For i As Integer = 1 To grdAnyMap.ColumnCount - 1
        '        grdAnyMap(i, e.RowIndex).ReadOnly = False
        '        grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
        '    Next

        'Else
        '    grdAnyMap(1, e.RowIndex).Value = False
        '    grdAnyMap(2, e.RowIndex).Value = False
        '    grdAnyMap(3, e.RowIndex).Value = ""
        '    grdAnyMap(4, e.RowIndex).Value = ""
        '    grdAnyMap(5, e.RowIndex).Value = ""
        '    grdAnyMap(6, e.RowIndex).Value = ""
        '    grdAnyMap(7, e.RowIndex).Value = ""

        '    For i As Integer = 1 To grdAnyMap.ColumnCount - 1
        '        grdAnyMap(i, e.RowIndex).ReadOnly = True
        '        grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
        '    Next

        'End If

    End Sub
    Private Sub grdAnyMap_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        If e.RowIndex < 0 Then Exit Sub

        ''使用可/不可　切替
        If grdAnyMap(0, e.RowIndex).Value = True Then

            For i As Integer = 1 To grdAnyMap.ColumnCount - 1
                grdAnyMap(i, e.RowIndex).ReadOnly = False
                grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
            Next

        Else
            grdAnyMap(1, e.RowIndex).Value = False
            grdAnyMap(2, e.RowIndex).Value = False
            grdAnyMap(3, e.RowIndex).Value = ""
            grdAnyMap(4, e.RowIndex).Value = ""
            grdAnyMap(5, e.RowIndex).Value = ""
            grdAnyMap(6, e.RowIndex).Value = ""
            grdAnyMap(7, e.RowIndex).Value = ""

            For i As Integer = 1 To grdAnyMap.ColumnCount - 1
                grdAnyMap(i, e.RowIndex).ReadOnly = True
                grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
            Next

        End If

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

            With mCompositeDetail

                .SysNo = cmbSysNo.SelectedValue
                .ChNo = txtChNo.Text
                .TagNo = Trim(txtTagNo.Text) '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
                .ItemName = txtItemName.Text
                .Remarks = txtRemarks.Text

                .AlmLevel = cmbAlmLvl.SelectedValue     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応

                If txtStatus1.Text = "" And txtStatus2.Text = "" Then
                    .Status = ""
                Else
                    .Status = txtStatus1.Text & "/" & txtStatus2.Text
                End If

                '.Status = txtStatus1.Text.Trim.PadRight(8)
                '.Status += txtStatus2.Text.Trim.PadRight(8)

                .CompositeIndex = IIf(txtCompositeIndex.Text = "", 0, Val(txtCompositeIndex.Text))

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
                .BitCount = txtBitCount.Text

                .StartNo = txtFuNo.Text
                .StartPortNo = txtPortNo.Text
                .StartPinNo = txtPin.Text

                'Ver2.0.0.2 南日本M761対応 2017.02.27追加
                .AlmMimic = txtAlmMimic.Text

                '▼▼▼ 20110614 仮設定機能対応（コンポジット） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .DummyExtG = gDummyCheckControl(txtExtGroup)
                .DummyDelay = gDummyCheckControl(txtDelayTimer)
                .DummyGroupRepose1 = gDummyCheckControl(txtGRep1)
                .DummyGroupRepose2 = gDummyCheckControl(txtGRep2)
                .DummyFuAddress = gDummyCheckControl(txtFuNo)
                .DummyBitCount = gDummyCheckControl(txtBitCount)
                .DummyStatusName = gDummyCheckControl(txtStatus1)

                .DummyCmpStatus1ExtGr = mCompositeDetail.DummyCmpStatus1ExtGr
                .DummyCmpStatus1Delay = mCompositeDetail.DummyCmpStatus1Delay
                .DummyCmpStatus1GRep1 = mCompositeDetail.DummyCmpStatus1GRep1
                .DummyCmpStatus1GRep2 = mCompositeDetail.DummyCmpStatus1GRep2
                .DummyCmpStatus1StaNm = mCompositeDetail.DummyCmpStatus1StaNm

                .DummyCmpStatus2ExtGr = mCompositeDetail.DummyCmpStatus2ExtGr
                .DummyCmpStatus2Delay = mCompositeDetail.DummyCmpStatus2Delay
                .DummyCmpStatus2GRep1 = mCompositeDetail.DummyCmpStatus2GRep1
                .DummyCmpStatus2GRep2 = mCompositeDetail.DummyCmpStatus2GRep2
                .DummyCmpStatus2StaNm = mCompositeDetail.DummyCmpStatus2StaNm

                .DummyCmpStatus3ExtGr = mCompositeDetail.DummyCmpStatus3ExtGr
                .DummyCmpStatus3Delay = mCompositeDetail.DummyCmpStatus3Delay
                .DummyCmpStatus3GRep1 = mCompositeDetail.DummyCmpStatus3GRep1
                .DummyCmpStatus3GRep2 = mCompositeDetail.DummyCmpStatus3GRep2
                .DummyCmpStatus3StaNm = mCompositeDetail.DummyCmpStatus3StaNm

                .DummyCmpStatus4ExtGr = mCompositeDetail.DummyCmpStatus4ExtGr
                .DummyCmpStatus4Delay = mCompositeDetail.DummyCmpStatus4Delay
                .DummyCmpStatus4GRep1 = mCompositeDetail.DummyCmpStatus4GRep1
                .DummyCmpStatus4GRep2 = mCompositeDetail.DummyCmpStatus4GRep2
                .DummyCmpStatus4StaNm = mCompositeDetail.DummyCmpStatus4StaNm

                .DummyCmpStatus5ExtGr = mCompositeDetail.DummyCmpStatus5ExtGr
                .DummyCmpStatus5Delay = mCompositeDetail.DummyCmpStatus5Delay
                .DummyCmpStatus5GRep1 = mCompositeDetail.DummyCmpStatus5GRep1
                .DummyCmpStatus5GRep2 = mCompositeDetail.DummyCmpStatus5GRep2
                .DummyCmpStatus5StaNm = mCompositeDetail.DummyCmpStatus5StaNm

                .DummyCmpStatus6ExtGr = mCompositeDetail.DummyCmpStatus6ExtGr
                .DummyCmpStatus6Delay = mCompositeDetail.DummyCmpStatus6Delay
                .DummyCmpStatus6GRep1 = mCompositeDetail.DummyCmpStatus6GRep1
                .DummyCmpStatus6GRep2 = mCompositeDetail.DummyCmpStatus6GRep2
                .DummyCmpStatus6StaNm = mCompositeDetail.DummyCmpStatus6StaNm

                .DummyCmpStatus7ExtGr = mCompositeDetail.DummyCmpStatus7ExtGr
                .DummyCmpStatus7Delay = mCompositeDetail.DummyCmpStatus7Delay
                .DummyCmpStatus7GRep1 = mCompositeDetail.DummyCmpStatus7GRep1
                .DummyCmpStatus7GRep2 = mCompositeDetail.DummyCmpStatus7GRep2
                .DummyCmpStatus7StaNm = mCompositeDetail.DummyCmpStatus7StaNm

                .DummyCmpStatus8ExtGr = mCompositeDetail.DummyCmpStatus8ExtGr
                .DummyCmpStatus8Delay = mCompositeDetail.DummyCmpStatus8Delay
                .DummyCmpStatus8GRep1 = mCompositeDetail.DummyCmpStatus8GRep1
                .DummyCmpStatus8GRep2 = mCompositeDetail.DummyCmpStatus8GRep2
                .DummyCmpStatus8StaNm = mCompositeDetail.DummyCmpStatus8StaNm

                .DummyCmpStatus9ExtGr = mCompositeDetail.DummyCmpStatus9ExtGr
                .DummyCmpStatus9Delay = mCompositeDetail.DummyCmpStatus9Delay
                .DummyCmpStatus9GRep1 = mCompositeDetail.DummyCmpStatus9GRep1
                .DummyCmpStatus9GRep2 = mCompositeDetail.DummyCmpStatus9GRep2
                .DummyCmpStatus9StaNm = mCompositeDetail.DummyCmpStatus9StaNm
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

            Dim strBitMap1 As String, strBitMap2 As String
            Dim intValue As Integer
            Dim i As Integer, j As Integer

            ''共通テキスト入力チェック
            If Not gChkInputText(txtItemName, "Item Name", True, True) Then Return False
            If Not gChkInputText(txtRemarks, "Remarks", True, True) Then Return False
            If Not gChkInputText(txtStatus1, "Status", True, True) Then Return False
            If Not gChkInputText(txtStatus2, "Status", True, True) Then Return False

            If ChkTagInput(txtTagNo.Text) = False Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加

            ''共通数値入力チェック
            If Not gChkInputNum(txtCompositeIndex, 1, 64, "Composite Table No", False, True) Then Return False

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
            If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
            If Not gChkInputNum(txtBitCount, 1, 8, "Bit Count", True, True) Then Return False
            If Not gChkInputNum(txtFilterCoeficient, 1, 250, "Filter Coeficient", True, True) Then Return False '' フィルタ定数変更　ver.1.4.4 2012.05.08
            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

            For i = 0 To 7
                If Not gChkInputNum(grdBitStatusMap(3, i), 0, 24, "EXT.G", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(4, i), 0, 240, "Delay", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(5, i), 0, 48, "G.REP1", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(6, i), 0, 48, "G.REP2", i + 1, True, True) Then Return False
            Next

            If Not gChkInputNum(grdAnyMap(3, 0), 0, 24, "EXT.G", 1, True, True) Then Return False
            If Not gChkInputNum(grdAnyMap(5, 0), 0, 48, "G.REP1", 1, True, True) Then Return False
            If Not gChkInputNum(grdAnyMap(6, 0), 0, 48, "G.REP2", 1, True, True) Then Return False

            If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
            'If cmbTime.SelectedValue = 0 Then
            '    If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
            'Else
            '    If Not gChkInputNum(txtDelayTimer, 0, 4, "Delay", True, True) Then Return False
            'End If

            ''共通FUアドレス入力チェック
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 64, True, True) Then Return False


            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If txtAlmMimic.Text <> "0" Then
                '0ならＯＫ
                '201～299以外はNG　空白はOK
                If Not gChkInputNum(txtAlmMimic, 201, 299, "Alm Mimic", True, True) Then Return False
            End If



            ''Bit Count と　FUアドレス
            If txtPin.Text <> "" And txtBitCount.Text <> "" Then
                If CInt(txtPin.Text) + CInt(txtBitCount.Text) > 65 Then
                    Call MessageBox.Show("Bit Count and Pin No are illegal. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If

            ''Bit Count以上のBitMapにチェックが入っていないか？
            intValue = CCInt(txtBitCount.Text)
            If intValue < 8 Then

                For i = 0 To 7

                    For j = intValue To 7

                        If grdBitStatusMap(j + 7, i).Value = True Then

                            Call MessageBox.Show("Bit Status Map [" & i + 1 & "]  Please set only " & intValue.ToString & " bits. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If
                    Next

                Next

            End If

            ''同じ状態がダブっていないか？
            For i = 0 To 7

                strBitMap1 = IIf(grdBitStatusMap(1, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(2, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(7, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(8, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(9, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(10, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(11, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(12, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(13, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(14, i).Value, "1", "0")

                If strBitMap1 <> "0000000000" Then

                    If i < 7 Then
                        For j = i + 1 To 7

                            strBitMap2 = IIf(grdBitStatusMap(1, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(2, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(7, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(8, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(9, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(10, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(11, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(12, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(13, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(14, j).Value, "1", "0")

                            If strBitMap2 <> "0000000000" Then

                                If strBitMap1 = strBitMap2 Then
                                    Call MessageBox.Show("Bit Status Map [" & i + 1 & "] and [" & j + 1 & "]  are the same settings.", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return False
                                End If

                            End If

                        Next j
                    End If

                    ''状態チェックがされているのに Status Name が未入力でないか？
                    If gGetString(grdBitStatusMap(15, i).Value) = "" Then
                        Call MessageBox.Show("Bit Status Map [" & i + 1 & "]  Please set the Status Name. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                End If

            Next i

            If grdAnyMap(1, 0).Value Or grdAnyMap(2, 0).Value Then
                If gGetString(grdAnyMap(7, 0).Value) = "" Then
                    Call MessageBox.Show("Any Map : Please set the Status Name. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub mInitial()

        Try

            ''コンボボックス初期化
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)
            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeComposite)
            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)
            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chkUse"
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chkUse"

            With grdBitStatusMap

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "Repose" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "Alarm" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "EXT.G" : .Columns(3).Width = 50 : .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).HeaderText = "Delay" : .Columns(4).Width = 50 : .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).HeaderText = "G.REP1" : .Columns(5).Width = 50 : .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).HeaderText = "G.REP2" : .Columns(6).Width = 50 : .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).HeaderText = "0" : .Columns(7).Width = 30
                .Columns(8).HeaderText = "1" : .Columns(8).Width = 30
                .Columns(9).HeaderText = "2" : .Columns(9).Width = 30
                .Columns(10).HeaderText = "3" : .Columns(10).Width = 30
                .Columns(11).HeaderText = "4" : .Columns(11).Width = 30
                .Columns(12).HeaderText = "5" : .Columns(12).Width = 30
                .Columns(13).HeaderText = "6" : .Columns(13).Width = 30
                .Columns(14).HeaderText = "7" : .Columns(14).Width = 30
                .Columns(15).HeaderText = "Status Name" : .Columns(15).Width = 190
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdBitStatusMap)

                ''USE 以外はロック
                For i = 0 To .Rows.Count - 1
                    For j = 1 To .ColumnCount - 1
                        .Rows(i).Cells(j).ReadOnly = True
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
                Next

            End With

            With grdAnyMap

                ''列
                .Columns.Clear()
                .Columns.Add(Column2)
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .Columns.Add(New DataGridViewTextBoxColumn())
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "Repose" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "Alarm" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "EXT.G" : .Columns(3).Width = 50 : .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).HeaderText = "Delay" : .Columns(4).Width = 50 : .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).HeaderText = "G.REP1" : .Columns(5).Width = 50 : .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).HeaderText = "G.REP2" : .Columns(6).Width = 50 : .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).HeaderText = "Status Name" : .Columns(7).Width = 190
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 2
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 50

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdAnyMap)

                ''USE 以外はロック
                For i = 0 To .Rows.Count - 1
                    For j = 1 To .ColumnCount - 1
                        .Rows(i).Cells(j).ReadOnly = True
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
                Next

            End With

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
    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mCompositeInfo, _
                                         ByVal udt2 As frmChListChannelList.mCompositeInfo) As Boolean

        Try

            If udt1.SysNo <> udt2.SysNo Then Return False
            If udt1.ChNo <> udt2.ChNo Then Return False
            If udt1.TagNo <> udt2.TagNo Then Return False '' 2015.10.27  Ver1.7.5 ﾀｸﾞ追加
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
            If udt1.BitCount <> udt2.BitCount Then Return False

            If udt1.StartNo <> udt2.StartNo Then Return False
            If udt1.StartPortNo <> udt2.StartPortNo Then Return False
            If udt1.StartPinNo <> udt2.StartPinNo Then Return False

            If udt1.DataType <> udt2.DataType Then Return False
            If udt1.CompositeStatus <> udt2.CompositeStatus Then Return False
            If udt1.Remarks <> udt2.Remarks Then Return False
            If udt1.Status <> udt2.Status Then Return False
            If udt1.ShareType <> udt2.ShareType Then Return False
            If udt1.ShareChNo <> udt2.ShareChNo Then Return False

            If udt1.CompositeIndex <> udt2.CompositeIndex Then Return False

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            If udt1.AlmMimic <> udt2.AlmMimic Then Return False

            'For i = 0 To 8

            '    If udt1.mCompositeDetail(i).FlagUse <> udt2.mCompositeDetail(i).FlagUse Then Return False
            '    If udt1.mCompositeDetail(i).FlagRepose <> udt2.mCompositeDetail(i).FlagRepose Then Return False
            '    If udt1.mCompositeDetail(i).FlagAlarm <> udt2.mCompositeDetail(i).FlagAlarm Then Return False
            '    If udt1.mCompositeDetail(i).ExtG <> udt2.mCompositeDetail(i).ExtG Then Return False
            '    If udt1.mCompositeDetail(i).Delay <> udt2.mCompositeDetail(i).Delay Then Return False
            '    If udt1.mCompositeDetail(i).GRep1 <> udt2.mCompositeDetail(i).GRep1 Then Return False
            '    If udt1.mCompositeDetail(i).GRep2 <> udt2.mCompositeDetail(i).GRep2 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit0 <> udt2.mCompositeDetail(i).FlagBit0 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit1 <> udt2.mCompositeDetail(i).FlagBit1 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit2 <> udt2.mCompositeDetail(i).FlagBit2 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit3 <> udt2.mCompositeDetail(i).FlagBit3 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit4 <> udt2.mCompositeDetail(i).FlagBit4 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit5 <> udt2.mCompositeDetail(i).FlagBit5 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit6 <> udt2.mCompositeDetail(i).FlagBit6 Then Return False
            '    If udt1.mCompositeDetail(i).FlagBit7 <> udt2.mCompositeDetail(i).FlagBit7 Then Return False
            '    If udt1.mCompositeDetail(i).StatusName <> udt2.mCompositeDetail(i).StatusName Then Return False

            'Next

            '▼▼▼ 20110614 仮設定機能対応（コンポジット） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If udt1.DummyExtG <> udt2.DummyExtG Then Return False
            If udt1.DummyDelay <> udt2.DummyDelay Then Return False
            If udt1.DummyGroupRepose1 <> udt2.DummyGroupRepose1 Then Return False
            If udt1.DummyGroupRepose2 <> udt2.DummyGroupRepose2 Then Return False
            If udt1.DummyFuAddress <> udt2.DummyFuAddress Then Return False
            If udt1.DummyBitCount <> udt2.DummyBitCount Then Return False
            If udt1.DummyStatusName <> udt2.DummyStatusName Then Return False

            If udt1.DummyCmpStatus1ExtGr <> udt2.DummyCmpStatus1ExtGr Then Return False
            If udt1.DummyCmpStatus1Delay <> udt2.DummyCmpStatus1Delay Then Return False
            If udt1.DummyCmpStatus1GRep1 <> udt2.DummyCmpStatus1GRep1 Then Return False
            If udt1.DummyCmpStatus1GRep2 <> udt2.DummyCmpStatus1GRep2 Then Return False
            If udt1.DummyCmpStatus1StaNm <> udt2.DummyCmpStatus1StaNm Then Return False

            If udt1.DummyCmpStatus2ExtGr <> udt2.DummyCmpStatus2ExtGr Then Return False
            If udt1.DummyCmpStatus2Delay <> udt2.DummyCmpStatus2Delay Then Return False
            If udt1.DummyCmpStatus2GRep1 <> udt2.DummyCmpStatus2GRep1 Then Return False
            If udt1.DummyCmpStatus2GRep2 <> udt2.DummyCmpStatus2GRep2 Then Return False
            If udt1.DummyCmpStatus2StaNm <> udt2.DummyCmpStatus2StaNm Then Return False

            If udt1.DummyCmpStatus3ExtGr <> udt2.DummyCmpStatus3ExtGr Then Return False
            If udt1.DummyCmpStatus3Delay <> udt2.DummyCmpStatus3Delay Then Return False
            If udt1.DummyCmpStatus3GRep1 <> udt2.DummyCmpStatus3GRep1 Then Return False
            If udt1.DummyCmpStatus3GRep2 <> udt2.DummyCmpStatus3GRep2 Then Return False
            If udt1.DummyCmpStatus3StaNm <> udt2.DummyCmpStatus3StaNm Then Return False

            If udt1.DummyCmpStatus4ExtGr <> udt2.DummyCmpStatus4ExtGr Then Return False
            If udt1.DummyCmpStatus4Delay <> udt2.DummyCmpStatus4Delay Then Return False
            If udt1.DummyCmpStatus4GRep1 <> udt2.DummyCmpStatus4GRep1 Then Return False
            If udt1.DummyCmpStatus4GRep2 <> udt2.DummyCmpStatus4GRep2 Then Return False
            If udt1.DummyCmpStatus4StaNm <> udt2.DummyCmpStatus4StaNm Then Return False

            If udt1.DummyCmpStatus5ExtGr <> udt2.DummyCmpStatus5ExtGr Then Return False
            If udt1.DummyCmpStatus5Delay <> udt2.DummyCmpStatus5Delay Then Return False
            If udt1.DummyCmpStatus5GRep1 <> udt2.DummyCmpStatus5GRep1 Then Return False
            If udt1.DummyCmpStatus5GRep2 <> udt2.DummyCmpStatus5GRep2 Then Return False
            If udt1.DummyCmpStatus5StaNm <> udt2.DummyCmpStatus5StaNm Then Return False

            If udt1.DummyCmpStatus6ExtGr <> udt2.DummyCmpStatus6ExtGr Then Return False
            If udt1.DummyCmpStatus6Delay <> udt2.DummyCmpStatus6Delay Then Return False
            If udt1.DummyCmpStatus6GRep1 <> udt2.DummyCmpStatus6GRep1 Then Return False
            If udt1.DummyCmpStatus6GRep2 <> udt2.DummyCmpStatus6GRep2 Then Return False
            If udt1.DummyCmpStatus6StaNm <> udt2.DummyCmpStatus6StaNm Then Return False

            If udt1.DummyCmpStatus7ExtGr <> udt2.DummyCmpStatus7ExtGr Then Return False
            If udt1.DummyCmpStatus7Delay <> udt2.DummyCmpStatus7Delay Then Return False
            If udt1.DummyCmpStatus7GRep1 <> udt2.DummyCmpStatus7GRep1 Then Return False
            If udt1.DummyCmpStatus7GRep2 <> udt2.DummyCmpStatus7GRep2 Then Return False
            If udt1.DummyCmpStatus7StaNm <> udt2.DummyCmpStatus7StaNm Then Return False

            If udt1.DummyCmpStatus8ExtGr <> udt2.DummyCmpStatus8ExtGr Then Return False
            If udt1.DummyCmpStatus8Delay <> udt2.DummyCmpStatus8Delay Then Return False
            If udt1.DummyCmpStatus8GRep1 <> udt2.DummyCmpStatus8GRep1 Then Return False
            If udt1.DummyCmpStatus8GRep2 <> udt2.DummyCmpStatus8GRep2 Then Return False
            If udt1.DummyCmpStatus8StaNm <> udt2.DummyCmpStatus8StaNm Then Return False

            If udt1.DummyCmpStatus9ExtGr <> udt2.DummyCmpStatus9ExtGr Then Return False
            If udt1.DummyCmpStatus9Delay <> udt2.DummyCmpStatus9Delay Then Return False
            If udt1.DummyCmpStatus9GRep1 <> udt2.DummyCmpStatus9GRep1 Then Return False
            If udt1.DummyCmpStatus9GRep2 <> udt2.DummyCmpStatus9GRep2 Then Return False
            If udt1.DummyCmpStatus9StaNm <> udt2.DummyCmpStatus9StaNm Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChComposite, _
                               ByRef udtTarget As gTypSetChComposite)

        Try

            For i As Integer = 0 To UBound(udtSource.udtComposite)

                udtTarget.udtComposite(i).shtChid = udtSource.udtComposite(i).shtChid             ''CH ID
                udtTarget.udtComposite(i).shtDiFilter = udtSource.udtComposite(i).shtDiFilter     ''DI Filter

                '---------------------------
                ' 詳細画面
                '---------------------------
                For j As Integer = 0 To UBound(udtSource.udtComposite(i).udtCompInf)
                    udtTarget.udtComposite(i).udtCompInf(j).bytBitPattern = udtSource.udtComposite(i).udtCompInf(j).bytBitPattern   ''ステータスビットパターン
                    udtTarget.udtComposite(i).udtCompInf(j).bytAlarmUse = udtSource.udtComposite(i).udtCompInf(j).bytAlarmUse       ''ステータスビット仕様有無
                    udtTarget.udtComposite(i).udtCompInf(j).bytDelay = udtSource.udtComposite(i).udtCompInf(j).bytDelay             ''ディレィタイマ値(ｱﾗｰﾑ継続時間)
                    udtTarget.udtComposite(i).udtCompInf(j).bytExtGroup = udtSource.udtComposite(i).udtCompInf(j).bytExtGroup       ''EXT. グループ(延長警報ｸﾞﾙｰﾌﾟ)
                    udtTarget.udtComposite(i).udtCompInf(j).bytGRepose1 = udtSource.udtComposite(i).udtCompInf(j).bytGRepose1       ''グループ・リポーズ１
                    udtTarget.udtComposite(i).udtCompInf(j).bytGRepose2 = udtSource.udtComposite(i).udtCompInf(j).bytGRepose2       ''グループ・リポーズ２
                    udtTarget.udtComposite(i).udtCompInf(j).strStatusName = udtSource.udtComposite(i).udtCompInf(j).strStatusName   ''ステータス名称
                    udtTarget.udtComposite(i).udtCompInf(j).bytManualReposeState = udtSource.udtComposite(i).udtCompInf(j).bytManualReposeState     ''マニュアルリポーズ状態
                    udtTarget.udtComposite(i).udtCompInf(j).bytManualReposeSet = udtSource.udtComposite(i).udtCompInf(j).bytManualReposeSet         ''マニュアルリポーズ設定
                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "仮設定関連"

    Private Sub objDummySetControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtExtGroup.KeyDown, _
                                                                                                                         txtDelayTimer.KeyDown, _
                                                                                                                         txtGRep1.KeyDown, _
                                                                                                                         txtGRep2.KeyDown, _
                                                                                                                         txtBitCount.KeyDown
        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(sender)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmbStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStatus1.KeyDown, _
                                                                                                                txtStatus2.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then
                Call gDummySetColorChange(txtStatus1)
                Call gDummySetColorChange(txtStatus2)
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

    '    With mCompositeDetail

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

    '        If .DataType <> cmbDataType.SelectedValue Then Return True
    '        If .BitCount <> txtBitCount.Text Then Return True
    '        If .StartNo <> txtFuNo.Text & txtPortNo.Text & txtPin.Text Then Return True

    '        If .FilterCoef <> txtFilterCoeficient.Text Then Return True

    '        For i = 0 To 7
    '            If .mCompositeDetail(i).FlagUse <> grdBitStatusMap(0, i).Value Then Return True
    '            If .mCompositeDetail(i).FlagRepose <> IIf(grdBitStatusMap(1, i).Value = Nothing, False, grdBitStatusMap(1, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagAlarm <> IIf(grdBitStatusMap(2, i).Value = Nothing, False, grdBitStatusMap(2, i).Value) Then Return True

    '            If .mCompositeDetail(i).ExtG <> IIf(grdBitStatusMap(3, i).Value = "", gCstCodeChCompExtGroupNothing, grdBitStatusMap(3, i).Value) Then Return True
    '            If .mCompositeDetail(i).Delay <> IIf(grdBitStatusMap(4, i).Value = "", gCstCodeChCompDelayTimerNothing, grdBitStatusMap(4, i).Value) Then Return True
    '            If .mCompositeDetail(i).GRep1 <> IIf(grdBitStatusMap(5, i).Value = "", gCstCodeChCompGroupRepose1Nothing, grdBitStatusMap(5, i).Value) Then Return True
    '            If .mCompositeDetail(i).GRep2 <> IIf(grdBitStatusMap(6, i).Value = "", gCstCodeChCompGroupRepose2Nothing, grdBitStatusMap(6, i).Value) Then Return True

    '            If .mCompositeDetail(i).FlagBit7 <> IIf(grdBitStatusMap(7, i).Value = Nothing, False, grdBitStatusMap(7, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit6 <> IIf(grdBitStatusMap(8, i).Value = Nothing, False, grdBitStatusMap(8, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit5 <> IIf(grdBitStatusMap(9, i).Value = Nothing, False, grdBitStatusMap(9, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit4 <> IIf(grdBitStatusMap(10, i).Value = Nothing, False, grdBitStatusMap(10, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit3 <> IIf(grdBitStatusMap(11, i).Value = Nothing, False, grdBitStatusMap(11, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit2 <> IIf(grdBitStatusMap(12, i).Value = Nothing, False, grdBitStatusMap(12, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit1 <> IIf(grdBitStatusMap(13, i).Value = Nothing, False, grdBitStatusMap(13, i).Value) Then Return True
    '            If .mCompositeDetail(i).FlagBit0 <> IIf(grdBitStatusMap(14, i).Value = Nothing, False, grdBitStatusMap(14, i).Value) Then Return True

    '            If gGetString(.mCompositeDetail(i).StatusName) <> IIf(grdBitStatusMap(15, i).Value = Nothing, "", grdBitStatusMap(15, i).Value) Then Return True
    '        Next

    '        If .mCompositeDetail(8).FlagUse <> grdAnyMap(0, 0).Value Then Return True
    '        If .mCompositeDetail(8).FlagRepose <> grdAnyMap(1, 0).Value Then Return True
    '        If .mCompositeDetail(8).FlagAlarm <> grdAnyMap(2, 0).Value Then Return True

    '        If .mCompositeDetail(8).ExtG <> IIf(grdAnyMap(3, 0).Value = "", gCstCodeChCompExtGroupNothing, grdAnyMap(3, 0).Value) Then Return True
    '        If .mCompositeDetail(8).Delay <> IIf(grdAnyMap(4, 0).Value = "", gCstCodeChCompDelayTimerNothing, grdAnyMap(4, 0).Value) Then Return True
    '        If .mCompositeDetail(8).GRep1 <> IIf(grdAnyMap(5, 0).Value = "", gCstCodeChCompGroupRepose1Nothing, grdAnyMap(5, 0).Value) Then Return True
    '        If .mCompositeDetail(8).GRep2 <> IIf(grdAnyMap(6, 0).Value = "", gCstCodeChCompGroupRepose2Nothing, grdAnyMap(6, 0).Value) Then Return True

    '        If gGetString(.mCompositeDetail(8).StatusName) <> grdAnyMap(7, 0).Value Then Return True

    '    End With

    '    Return False

    'End Function

#End Region

End Class
