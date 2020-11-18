Public Class frmChListSystem

#Region "システムチャンネルはデジタルチャンネルに含めるため全てコメントアウト"

    '#Region "変数定義"

    '    ''OKフラグ
    '    Private mintOkFlag As Integer

    '    ''Next CH ボタン　クリックフラグ
    '    Private mintNextChFlag As Integer = 0

    '    ''Before CH ボタン　クリックフラグ
    '    Private mintBeforeChFlag As Integer = 0

    '    ''イベント重複抑制用
    '    Private mblnFlg As Boolean

    '    ''Delay Timer 設定単位区分
    '    Private mintDelayTimeKubun As Integer   ''0:秒　1:分

    '    ''システムチャンネル情報格納
    '    Private Structure mSystemInfo
    '        Public RowNo As Integer
    '        Public RowNoFirst As Integer
    '        Public RowNoEnd As Integer
    '        Public SysNo As String
    '        Public ChNo As String
    '        Public ItemName As String
    '        Public ExtGH As String
    '        Public DelayH As String
    '        Public GRep1H As String
    '        Public GRep2H As String
    '        Public FlagDmy As String
    '        Public FlagSC As String
    '        Public FlagSIO As String
    '        Public FlagGWS As String
    '        Public FlagWK As String
    '        Public FlagRL As String
    '        Public FlagAC As String
    '        Public FlagEP As String
    '        Public FlagPrt1 As String
    '        Public FlagPrt2 As String
    '        Public FlagMin As String
    '        Public DataType As String
    '        Public Status As String
    '        Public FlagStatusAlarm As Boolean
    '        Public FilterCoef As String
    '        Public ShareType As String
    '        Public ShareChNo As String
    '        'Public FlagMrepose As Boolean
    '        Public Remarks As String

    '        ''16ステータス分
    '        Public DeviceStatus As String
    '        Public DeviceStatusUse() As Boolean
    '        Public DeviceStatusCode() As String
    '        Public DeviceStatusName() As String
    '    End Structure
    '    Private mSystemDetail As mSystemInfo

    '#End Region

    '#Region "画面イベント"

    '    '--------------------------------------------------------------------
    '    ' 機能      : 画面表示関数
    '    ' 返り値    : 0:OK  <> 0:キャンセル
    '    ' 引き数    : ARG1 - (IO) デジタルチャンネル情報
    '    '　　　　　 : ARG2 - (IO) 1:次のCH情報を続けて開く  2:前のCH情報を続けて開く
    '    ' 機能説明  : 
    '    '--------------------------------------------------------------------
    '    Friend Function gShow(ByRef hSysmteDetail As frmChListChannelList.mSystemInfo, _
    '                          ByRef hMode As Integer, _
    '                          ByRef frmOwner As Form) As Integer

    '        Try

    '            Dim intAns As Integer = -1

    '            mSystemDetail.RowNo = hSysmteDetail.RowNo
    '            mSystemDetail.RowNoFirst = hSysmteDetail.RowNoFirst
    '            mSystemDetail.RowNoEnd = hSysmteDetail.RowNoEnd
    '            mSystemDetail.SysNo = hSysmteDetail.SysNo
    '            mSystemDetail.ChNo = hSysmteDetail.ChNo
    '            mSystemDetail.ItemName = hSysmteDetail.ItemName
    '            mSystemDetail.ExtGH = hSysmteDetail.ExtGH
    '            mSystemDetail.DelayH = hSysmteDetail.DelayH
    '            mSystemDetail.GRep1H = hSysmteDetail.GRep1H
    '            mSystemDetail.GRep2H = hSysmteDetail.GRep2H
    '            mSystemDetail.FlagDmy = hSysmteDetail.FlagDmy
    '            mSystemDetail.FlagSC = hSysmteDetail.FlagSC
    '            mSystemDetail.FlagSIO = hSysmteDetail.FlagSIO
    '            mSystemDetail.FlagGWS = hSysmteDetail.FlagGWS
    '            mSystemDetail.FlagWK = hSysmteDetail.FlagWK
    '            mSystemDetail.FlagRL = hSysmteDetail.FlagRL
    '            mSystemDetail.FlagAC = hSysmteDetail.FlagAC
    '            mSystemDetail.FlagEP = hSysmteDetail.FlagEP
    '            mSystemDetail.FlagPrt1 = hSysmteDetail.FlagPrt1
    '            mSystemDetail.FlagPrt2 = hSysmteDetail.FlagPrt2
    '            mSystemDetail.FlagMin = hSysmteDetail.FlagMin
    '            mSystemDetail.Status = hSysmteDetail.Status
    '            mSystemDetail.FlagStatusAlarm = hSysmteDetail.FlagStatusAlarm
    '            mSystemDetail.ShareType = hSysmteDetail.ShareType
    '            mSystemDetail.ShareChNo = hSysmteDetail.ShareChNo
    '            mSystemDetail.Remarks = hSysmteDetail.Remarks

    '            ReDim mSystemDetail.DeviceStatusUse(15)
    '            ReDim mSystemDetail.DeviceStatusCode(15)
    '            ReDim mSystemDetail.DeviceStatusName(15)

    '            mSystemDetail.DeviceStatus = hSysmteDetail.DeviceStatus
    '            For i As Integer = 0 To 15
    '                mSystemDetail.DeviceStatusUse(i) = hSysmteDetail.DeviceStatusUse(i)
    '                mSystemDetail.DeviceStatusCode(i) = hSysmteDetail.DeviceStatusCode(i)
    '                mSystemDetail.DeviceStatusName(i) = hSysmteDetail.DeviceStatusName(i)
    '            Next

    '            Call gShowFormModelessForCloseWait2(Me, frmOwner)

    '            If mintOkFlag = 1 Then

    '                ''構造体の設定値を比較する
    '                If mChkStructureEquals(hSysmteDetail, mSystemDetail) = False Then

    '                    hSysmteDetail.SysNo = mSystemDetail.SysNo
    '                    hSysmteDetail.ChNo = mSystemDetail.ChNo
    '                    hSysmteDetail.ItemName = mSystemDetail.ItemName
    '                    hSysmteDetail.ExtGH = mSystemDetail.ExtGH
    '                    hSysmteDetail.DelayH = mSystemDetail.DelayH
    '                    hSysmteDetail.GRep1H = mSystemDetail.GRep1H
    '                    hSysmteDetail.GRep2H = mSystemDetail.GRep2H
    '                    hSysmteDetail.FlagDmy = mSystemDetail.FlagDmy
    '                    hSysmteDetail.FlagSC = mSystemDetail.FlagSC
    '                    hSysmteDetail.FlagSIO = mSystemDetail.FlagSIO
    '                    hSysmteDetail.FlagGWS = mSystemDetail.FlagGWS
    '                    hSysmteDetail.FlagWK = mSystemDetail.FlagWK
    '                    hSysmteDetail.FlagRL = mSystemDetail.FlagRL
    '                    hSysmteDetail.FlagAC = mSystemDetail.FlagAC
    '                    hSysmteDetail.FlagEP = mSystemDetail.FlagEP
    '                    hSysmteDetail.FlagPrt1 = mSystemDetail.FlagPrt1
    '                    hSysmteDetail.FlagPrt2 = mSystemDetail.FlagPrt2
    '                    hSysmteDetail.FlagMin = mSystemDetail.FlagMin
    '                    hSysmteDetail.Status = mSystemDetail.Status
    '                    hSysmteDetail.FlagStatusAlarm = mSystemDetail.FlagStatusAlarm
    '                    hSysmteDetail.ShareType = mSystemDetail.ShareType
    '                    hSysmteDetail.ShareChNo = mSystemDetail.ShareChNo
    '                    hSysmteDetail.Remarks = mSystemDetail.Remarks

    '                    hSysmteDetail.DeviceStatus = mSystemDetail.DeviceStatus
    '                    For i As Integer = 0 To 15
    '                        hSysmteDetail.DeviceStatusUse(i) = mSystemDetail.DeviceStatusUse(i)
    '                        hSysmteDetail.DeviceStatusCode(i) = mSystemDetail.DeviceStatusCode(i)
    '                        hSysmteDetail.DeviceStatusName(i) = mSystemDetail.DeviceStatusName(i)
    '                    Next

    '                    intAns = 0  ''変更有り

    '                End If

    '            End If

    '            hMode = 0
    '            If mintNextChFlag = 1 Then
    '                hMode = 1   ''Next CH
    '            ElseIf mintBeforeChFlag = 1 Then
    '                hMode = 2   ''Before CH
    '            End If

    '            gShow = intAns

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Function

    ''--------------------------------------------------------------------
    '' 機能      : フォームロード
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : 画面表示初期処理を行う
    ''--------------------------------------------------------------------
    '    Private Sub frmChListDigital_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    '        Try
    '            ''参照モードの設定
    '            Call gSetChListDispOnly(Me, cmdOK)

    '            mblnFlg = True

    '            ''グリッドを初期化する
    '            Call mInitialDataGrid()

    '            ''画面初期化
    '            Call mInitial()

    '            mblnFlg = False

    '            With mSystemDetail

    '                cmbSysNo.SelectedValue = .SysNo
    '                txtChNo.Text = .ChNo
    '                txtItemName.Text = .ItemName
    '                txtRemarks.Text = .Remarks

    '                If .ShareType <> Nothing Then
    '                    cmbShareType.Enabled = True : lblShareType.Enabled = True
    '                    txtShareChid.Enabled = True : lblShareChid.Enabled = True

    '                    cmbShareType.SelectedValue = .ShareType
    '                    If cmbShareType.SelectedValue = 1 Then txtShareChid.Text = .ShareChNo

    '                Else
    '                    cmbShareType.Enabled = False : lblShareType.Enabled = False
    '                    txtShareChid.Enabled = False : lblShareChid.Enabled = False
    '                End If

    '                txtDmy.Text = .FlagDmy
    '                txtSC.Text = .FlagSC
    '                txtSio.Text = .FlagSIO
    '                txtGWS.Text = .FlagGWS
    '                txtWK.Text = .FlagWK
    '                txtRL.Text = .FlagRL
    '                txtAC.Text = .FlagAC
    '                txtEP.Text = .FlagEP
    '                txtPr1.Text = .FlagPrt1
    '                txtPr2.Text = .FlagPrt2

    '                cmbTime.SelectedValue = IIf(.FlagMin = "", 0, .FlagMin)

    '                txtExtGroup.Text = .ExtGH
    '                txtDelayTimer.Text = .DelayH
    '                txtGRep1.Text = .GRep1H
    '                txtGRep2.Text = .GRep2H

    '                cmbDataType.SelectedIndex = 0
    '                chkStatusAlarm.Checked = .FlagStatusAlarm
    '                'txtFilterCoeficient.Text = .FilterCoef

    '                cmbDeviceStatus.SelectedValue = .DeviceStatus
    '                Call cmbDeviceStatus_SelectedIndexChanged(cmbDeviceStatus, New EventArgs)

    '                For i As Integer = 0 To 15
    '                    grdDeviceStatus(0, i).Value = .DeviceStatusUse(i)
    '                Next

    '                ''Status
    '                Dim intValue As Integer = cmbStatus.FindStringExact(.Status)
    '                If intValue >= 0 Then
    '                    cmbStatus.SelectedIndex = intValue
    '                Else
    '                    cmbStatus.SelectedValue = gCstCodeChManualInputStatus  ''特殊コード（手入力）
    '                    txtStatus.Text = .Status
    '                End If

    '                cmdBeforeCH.Enabled = True
    '                cmdNextCH.Enabled = True
    '                If .RowNoFirst = .RowNo Then cmdBeforeCH.Enabled = False
    '                If .RowNoEnd = .RowNo Then cmdNextCH.Enabled = False

    '            End With

    '            mintOkFlag = 0

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： 機器状態コンボ選択
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub cmbDeviceStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDeviceStatus.SelectedIndexChanged

    '        Try
    '            If mblnFlg Then Exit Sub

    '            Dim intValue As Integer
    '            Dim udtKiki() As gTypCodeName = Nothing

    '            ''グリッド クリア
    '            For i As Integer = 0 To 15
    '                grdDeviceStatus(0, i).Value = False
    '                grdDeviceStatus(1, i).Value = ""
    '                grdDeviceStatus(2, i).Value = ""
    '            Next

    '            ''選択済み機器状態グループコードGET
    '            intValue = cmbDeviceStatus.SelectedValue

    '            ''グループコード内の機器状態を全てGET
    '            Call gGetComboCodeName(udtKiki, _
    '                                   gEnmComboType.ctChListChannelListDeviceStatus, _
    '                                   intValue.ToString("00"))

    '            For i As Integer = 0 To UBound(udtKiki)

    '                grdDeviceStatus(1, i).Value = udtKiki(i).shtCode
    '                grdDeviceStatus(2, i).Value = udtKiki(i).strName

    '            Next

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： ステータスコンボ選択
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged

    '        Try

    '            If cmbStatus.SelectedValue = gCstCodeChManualInputStatus.ToString Then
    '                txtStatus.Visible = True
    '            Else
    '                txtStatus.Visible = False
    '            End If

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： Share Type コンボ選択
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub cmbShareType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShareType.SelectedIndexChanged

    '        Try

    '            If cmbShareType.SelectedValue = 1 Then
    '                ''Local
    '                txtShareChid.Enabled = True : lblShareChid.Enabled = True

    '            Else
    '                ''Remote
    '                txtShareChid.Text = ""
    '                txtShareChid.Enabled = False : lblShareChid.Enabled = False
    '            End If

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： Delay Timer 設定単位 コンボ選択
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    'Private Sub cmbTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTime.SelectedIndexChanged

    '    '    If mintDelayTimeKubun <> cmbTime.SelectedValue Then

    '    '        If cmbTime.SelectedValue = 0 Then
    '    '            ''分 -- > 秒
    '    '            If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) * 60)
    '    '        Else
    '    '            ''秒 --> 分
    '    '            If txtDelayTimer.Text <> "" Then txtDelayTimer.Text = Format(CCDouble(txtDelayTimer.Text) / 60, "0.0")
    '    '        End If

    '    '    End If

    '    '    mintDelayTimeKubun = cmbTime.SelectedValue

    '    'End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： フォームクローズ
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub frmChListDigital_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    '        Try
    '            Me.Dispose()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： Cancelボタンクリック
    '    ' 引数      ： なし
    '    ' 戻値      ： なし 
    '    '----------------------------------------------------------------------------
    '    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

    '        Try

    '            Me.Close()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '--------------------------------------------------------------------
    '    ' 機能      : OKボタンクリック
    '    ' 返り値    : なし
    '    ' 引き数    : なし
    '    ' 機能説明  : 内部メモリに画面上の情報を格納する
    '    ' 備考      : 
    '    '--------------------------------------------------------------------
    '    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

    '        Try

    '            ''入力チェック
    '            If Not mChkInput() Then Return

    '            ''画面の設定値を内部メモリに取り込む
    '            Call mGetSetData()

    '            mintOkFlag = 1

    '            Me.Close()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： BeforeCH ボタンクリック
    '    ' 引数      ： なし
    '    ' 戻値      ： なし 
    '    '----------------------------------------------------------------------------
    '    Private Sub cmdBeforeCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeforeCH.Click

    '        Try
    '            ''入力チェック
    '            If Not mChkInput() Then Return

    '            ''画面の設定値を内部メモリに取り込む
    '            Call mGetSetData()

    '            mintOkFlag = 1

    '            ''フラグ ON
    '            mintBeforeChFlag = 1

    '            ''一旦閉じる
    '            Me.Close()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： NextCH ボタンクリック
    '    ' 引数      ： なし
    '    ' 戻値      ： なし 
    '    '----------------------------------------------------------------------------
    '    Private Sub cmdNextCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextCH.Click

    '        Try
    '            ''入力チェック
    '            If Not mChkInput() Then Return

    '            ''画面の設定値を内部メモリに取り込む
    '            Call mGetSetData()

    '            mintOkFlag = 1

    '            ''フラグ ON
    '            mintNextChFlag = 1

    '            ''一旦閉じる
    '            Me.Close()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： KeyPressイベント
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress, txtShareChid.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtItemName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(30, sender, e.KeyChar, False)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtAlarm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    '                    Handles txtExtGroup.KeyPress, txtGRep1.KeyPress, txtGRep2.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtDelayTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDelayTimer.KeyPress

    '        Try
    '            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
    '            'If cmbTime.SelectedValue = 0 Then
    '            '    ''Sec
    '            '    e.Handled = gCheckTextInput(3, sender, e.KeyChar)
    '            'Else
    '            '    ''Min
    '            '    e.Handled = gCheckTextInput(2, sender, e.KeyChar, True, False, True)
    '            'End If

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtSio_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    '                    Handles txtSio.KeyPress, txtGWS.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtStatus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatus.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtFlag1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    '            Handles txtDmy.KeyPress, txtSC.KeyPress, txtWK.KeyPress, txtRL.KeyPress, _
    '                    txtAC.KeyPress, txtEP.KeyPress, txtPr1.KeyPress, txtPr2.KeyPress

    '        Try

    '            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "0,1")

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： EXT.Gに値が設定された場合にStatus Alarmにチェックを入れる
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    'Private Sub txtExtGroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExtGroup.Validated

    '    '    If txtExtGroup.Text <> "" And Val(txtExtGroup.Text) <> 0 Then
    '    '        chkStatusAlarm.Checked = True
    '    '    End If

    '    'End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： CH No.入力値をフォーマットする
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

    '        Try

    '            If IsNumeric(txtChNo.Text) Then
    '                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")
    '            End If

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    Private Sub txtShareChid_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShareChid.Validated

    '        Try

    '            If IsNumeric(txtShareChid.Text) Then
    '                txtShareChid.Text = Integer.Parse(txtShareChid.Text).ToString("0000")
    '            End If

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： Delay Timer 単位がMinの時、Delay設定値をフォーマットする
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    'Private Sub txtDelayTimer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelayTimer.Validated

    '    '    Try

    '    '        If cmbTime.SelectedValue = 1 Then

    '    '            If IsNumeric(txtDelayTimer.Text) Then
    '    '                txtDelayTimer.Text = Double.Parse(txtDelayTimer.Text).ToString("0.0")
    '    '            Else
    '    '                txtDelayTimer.Text = ""
    '    '            End If

    '    '        End If

    '    '    Catch ex As Exception
    '    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    '    End Try

    '    'End Sub

    '    Private Sub txtSioGws_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSio.GotFocus, txtGWS.GotFocus

    '        Dim intRtn As Integer
    '        Dim intMode As Integer = IIf(sender.name = "txtSio", 1, 0)

    '        ''イベント重複時は処理を抜ける
    '        If mblnFlg Then Return

    '        ''イベント重複回避フラグを設定してBit設定画面表示
    '        mblnFlg = True
    '        intRtn = CCInt(sender.Text)
    '        Call frmBitSetByte.gShow(intRtn, intMode, Me)
    '        sender.Text = intRtn
    '        mblnFlg = False

    '    End Sub

    '#End Region

    '#Region "内部関数"

    '    '---------------------------------------------------------------------------
    '    ' 機能      : 設定値GET
    '    ' 返り値    : なし
    '    ' 引き数    : なし
    '    ' 機能説明  : 画面の設定値を内部メモリに取り込む
    '    '---------------------------------------------------------------------------
    '    Private Sub mGetSetData()

    '        Try
    '            With mSystemDetail

    '                .SysNo = cmbSysNo.SelectedValue
    '                .ChNo = txtChNo.Text
    '                .ItemName = txtItemName.Text
    '                .Remarks = txtRemarks.Text

    '                If cmbShareType.Enabled = True Then
    '                    .ShareType = cmbShareType.SelectedValue
    '                    .ShareChNo = IIf(txtShareChid.Text = "", Nothing, txtShareChid.Text)
    '                End If

    '                .ExtGH = txtExtGroup.Text
    '                .DelayH = txtDelayTimer.Text
    '                .GRep1H = txtGRep1.Text
    '                .GRep2H = txtGRep2.Text

    '                .FlagDmy = txtDmy.Text
    '                .FlagSC = txtSC.Text
    '                .FlagSIO = txtSio.Text
    '                .FlagGWS = txtGWS.Text
    '                .FlagWK = txtWK.Text
    '                .FlagRL = txtRL.Text
    '                .FlagAC = txtAC.Text
    '                .FlagEP = txtEP.Text
    '                .FlagPrt1 = txtPr1.Text
    '                .FlagPrt2 = txtPr2.Text

    '                .FlagMin = cmbTime.SelectedValue

    '                If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '                    .Status = cmbStatus.Text
    '                Else
    '                    .Status = txtStatus.Text
    '                End If

    '                .FlagStatusAlarm = chkStatusAlarm.Checked

    '                .DeviceStatus = cmbDeviceStatus.SelectedValue
    '                For i As Integer = 0 To 15
    '                    If Val(grdDeviceStatus(1, i).Value) <> 0 Then
    '                        .DeviceStatusUse(i) = grdDeviceStatus(0, i).Value
    '                    Else
    '                        .DeviceStatusUse(i) = False
    '                    End If
    '                    .DeviceStatusCode(i) = grdDeviceStatus(1, i).Value
    '                    .DeviceStatusName(i) = grdDeviceStatus(2, i).Value
    '                Next

    '            End With

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '--------------------------------------------------------------------
    '    ' 機能      : 入力チェック
    '    ' 返り値    : True:入力OK、False:入力NG
    '    ' 引き数    : なし
    '    ' 機能説明  : 入力チェックを行う
    '    '--------------------------------------------------------------------
    '    Private Function mChkInput() As Boolean

    '        Try

    '            ''共通テキスト入力チェック
    '            If Not gChkInputText(txtItemName, "Item Name", True, True) Then Return False
    '            If Not gChkInputText(txtRemarks, "Remarks", True, True) Then Return False
    '            If Not gChkInputText(txtStatus, "Status", True, True) Then Return False

    '            ''共通数値入力チェック
    '            If Not gChkInputNum(txtChNo, 1, 65535, "CH No", False, True) Then Return False
    '            If Not gChkInputNum(txtDmy, 0, 1, "Dmy", True, True) Then Return False
    '            If Not gChkInputNum(txtSC, 0, 1, "SC", True, True) Then Return False
    '            If Not gChkInputNum(txtSio, 0, 511, "SIO", True, True) Then Return False
    '            If Not gChkInputNum(txtGWS, 0, 511, "GWS", True, True) Then Return False
    '            If Not gChkInputNum(txtWK, 0, 1, "WK", True, True) Then Return False
    '            If Not gChkInputNum(txtRL, 0, 1, "RL", True, True) Then Return False
    '            If Not gChkInputNum(txtAC, 0, 1, "AC", True, True) Then Return False
    '            If Not gChkInputNum(txtEP, 0, 1, "EP", True, True) Then Return False
    '            If Not gChkInputNum(txtPr1, 0, 1, "Prt1", True, True) Then Return False
    '            If Not gChkInputNum(txtPr2, 0, 1, "Prt2", True, True) Then Return False
    '            If Not gChkInputNum(txtExtGroup, 0, 24, "EXT.G", True, True) Then Return False
    '            If Not gChkInputNum(txtGRep1, 0, 48, "G REP1", True, True) Then Return False
    '            If Not gChkInputNum(txtGRep2, 0, 48, "G REP2", True, True) Then Return False
    '            If Not gChkInputNum(txtShareChid, 1, 65535, "Remote CH No", True, True) Then Return False

    '            If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
    '            'If cmbTime.SelectedValue = 0 Then
    '            '    If Not gChkInputNum(txtDelayTimer, 0, 240, "Delay", True, True) Then Return False
    '            'Else
    '            '    If Not gChkInputNum(txtDelayTimer, 0, 4, "Delay", True, True) Then Return False
    '            'End If

    '            Return True

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Function

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： 画面初期化
    '    ' 引数      ：
    '    ' 戻値      ：
    '    '----------------------------------------------------------------------------
    '    Private Sub mInitial()

    '        Try

    '            ''コンボボックス初期化
    '            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)

    '            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeSystem)

    '            Call gSetComboBox(cmbDeviceStatus, gEnmComboType.ctChListChannelListDeviceStatus)

    '            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)

    '            Call gSetComboBox(cmbTime, gEnmComboType.ctChListChannelListTime)

    '            Call gSetComboBox(cmbShareType, gEnmComboType.ctChListChannelListShareType)

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '--------------------------------------------------------------------
    '    ' 機能      : 構造体比較
    '    ' 返り値    : True:相違なし、False:相違あり
    '    ' 引き数    : ARG1 - (I ) 構造体１
    '    ' 　　　    : ARG1 - (I ) 構造体２
    '    ' 機能説明  : 構造体の設定値を比較する
    '    '--------------------------------------------------------------------
    '    Private Function mChkStructureEquals(ByVal udt1 As frmChListChannelList.mSystemInfo, _
    '                                         ByVal udt2 As mSystemInfo) As Boolean

    '        Try

    '            If udt1.SysNo <> udt2.SysNo Then Return False
    '            If udt1.ChNo <> udt2.ChNo Then Return False
    '            If udt1.ItemName <> udt2.ItemName Then Return False
    '            If udt1.ExtGH <> udt2.ExtGH Then Return False
    '            If udt1.DelayH <> udt2.DelayH Then Return False
    '            If udt1.GRep1H <> udt2.GRep1H Then Return False
    '            If udt1.GRep2H <> udt2.GRep2H Then Return False
    '            If udt1.FlagDmy <> udt2.FlagDmy Then Return False
    '            If udt1.FlagSC <> udt2.FlagSC Then Return False
    '            If udt1.FlagSIO <> udt2.FlagSIO Then Return False
    '            If udt1.FlagGWS <> udt2.FlagGWS Then Return False
    '            If udt1.FlagWK <> udt2.FlagWK Then Return False
    '            If udt1.FlagRL <> udt2.FlagRL Then Return False
    '            If udt1.FlagAC <> udt2.FlagAC Then Return False
    '            If udt1.FlagEP <> udt2.FlagEP Then Return False
    '            If udt1.FlagPrt1 <> udt2.FlagPrt1 Then Return False
    '            If udt1.FlagPrt2 <> udt2.FlagPrt2 Then Return False
    '            If udt1.FlagMin <> udt2.FlagMin Then Return False
    '            If udt1.Status <> udt2.Status Then Return False
    '            If udt1.FlagStatusAlarm <> udt2.FlagStatusAlarm Then Return False
    '            'If udt1.FilterCoef <> udt2.FilterCoef Then Return False
    '            If udt1.Remarks <> udt2.Remarks Then Return False
    '            If udt1.ShareType <> udt2.ShareType Then Return False
    '            If udt1.ShareChNo <> udt2.ShareChNo Then Return False
    '            'If udt1.FlagMrepose <> udt2.FlagMrepose Then Return False

    '            If udt1.DeviceStatus <> udt2.DeviceStatus Then Return False
    '            For i As Integer = 0 To 15
    '                If udt1.DeviceStatusUse(i) <> udt2.DeviceStatusUse(i) Then Return False
    '                If udt1.DeviceStatusCode(i) <> udt2.DeviceStatusCode(i) Then Return False
    '                If udt1.DeviceStatusName(i) <> udt2.DeviceStatusName(i) Then Return False
    '            Next

    '            Return True

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Function

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： グリッドを初期化する
    '    ' 引数      ： なし
    '    ' 戻値      ： なし 
    '    '----------------------------------------------------------------------------
    '    Private Sub mInitialDataGrid()

    '        Try
    '            Dim i As Integer
    '            Dim cellStyle As New DataGridViewCellStyle

    '            With grdDeviceStatus

    '                Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "ChkUse"
    '                Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtCode" : Column2.ReadOnly = True
    '                Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtName" : Column3.ReadOnly = True

    '                '列
    '                .Columns.Clear()
    '                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)

    '                .AllowUserToResizeColumns = False   ''列幅の変更不可
    '                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

    '                Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    '                ''全ての列の並び替えを禁止
    '                For Each c As DataGridViewColumn In .Columns
    '                    c.SortMode = DataGridViewColumnSortMode.NotSortable
    '                Next c

    '                ''列ヘッダー
    '                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 40
    '                .Columns(1).HeaderText = "Code" : .Columns(1).Width = 50
    '                .Columns(2).HeaderText = "Status" : .Columns(2).Width = 190
    '                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

    '                ''行
    '                .RowCount = 17
    '                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
    '                .AllowUserToResizeRows = False      ''行の高さの変更不可
    '                .AllowUserToDeleteRows = False      ''行の削除を不可にする

    '                ''行ヘッダー
    '                For i = 1 To .RowCount
    '                    .Rows(i - 1).HeaderCell.Value = i.ToString
    '                Next
    '                .RowHeadersWidth = 40
    '                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    '                ''偶数行の背景色を変える
    '                cellStyle.BackColor = gColorGridRowBack
    '                For i = 0 To .Rows.Count - 1
    '                    If i Mod 2 <> 0 Then
    '                        .Rows(i).DefaultCellStyle = cellStyle
    '                    End If
    '                    .Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
    '                    .Rows(i).Cells(2).Style.BackColor = gColorGridRowBackReadOnly
    '                Next

    '                ''罫線
    '                .EnableHeadersVisualStyles = False
    '                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
    '                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
    '                .CellBorderStyle = DataGridViewCellBorderStyle.Single
    '                .GridColor = Color.Gray

    '                .DefaultCellStyle.NullValue = ""

    '                ''コピー＆ペースト共通設定
    '                Call gSetGridCopyAndPaste(grdDeviceStatus)

    '            End With

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '#End Region

    '#Region "コメントアウト"

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： フォームクローズ
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    'Private Sub frmChListAnalog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    '    '    Try
    '    '        If mintOkFlag <> 1 Then

    '    '            ''データが変更されているかチェック
    '    '            If mChkDataChange() Then

    '    '                ''変更されている場合はメッセージ表示
    '    '                Select Case MessageBox.Show("Setting has been changed. Do you save it?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

    '    '                    Case Windows.Forms.DialogResult.Yes

    '    '                        Call cmdOK_Click(cmdOK, New EventArgs)

    '    '                        If mintOkFlag <> 1 Then e.Cancel = True

    '    '                    Case Windows.Forms.DialogResult.No

    '    '                        ''何もしない

    '    '                    Case Windows.Forms.DialogResult.Cancel

    '    '                        ''画面を閉じない
    '    '                        e.Cancel = True
    '    '                        mintBeforeChFlag = 0 : mintNextChFlag = 0
    '    '                        Exit Sub

    '    '                End Select

    '    '            End If

    '    '        End If

    '    '    Catch ex As Exception
    '    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    '    End Try

    '    'End Sub

    '    '--------------------------------------------------------------------
    '    ' 機能      : データ変更チェック
    '    ' 返り値    : True:変更有り、False:変更なし
    '    ' 引き数    : なし
    '    ' 機能説明  : データが変更されているかチェックを行う
    '    '--------------------------------------------------------------------
    '    'Private Function mChkDataChange() As Boolean

    '    '    With mSystemDetail

    '    '        If .SysNo <> cmbSysNo.SelectedValue Then Return True
    '    '        If .ChNo <> txtChNo.Text Then Return True
    '    '        If .ItemName <> txtItemName.Text Then Return True
    '    '        If .Remarks <> txtRemarks.Text Then Return True

    '    '        If cmbShareType.Enabled = True Then
    '    '            If .ShareType <> cmbShareType.SelectedValue Then Return True
    '    '            If .ShareChNo <> txtShareChid.Text Then Return True
    '    '        End If

    '    '        'If .FlagMrepose <> chkMrepose.Checked Then Return True

    '    '        If .ExtGH <> txtExtGroup.Text Then Return True
    '    '        If .DelayH <> txtDelayTimer.Text Then Return True
    '    '        If .GRep1H <> txtGRep1.Text Then Return True
    '    '        If .GRep2H <> txtGRep2.Text Then Return True

    '    '        If .FlagDmy <> txtDmy.Text Then Return True
    '    '        If .FlagSC <> txtSC.Text Then Return True
    '    '        If .FlagSIO <> txtSio.Text Then Return True
    '    '        If .FlagGWS <> txtGWS.Text Then Return True
    '    '        If .FlagWK <> txtWK.Text Then Return True
    '    '        If .FlagRL <> txtRL.Text Then Return True
    '    '        If .FlagAC <> txtAC.Text Then Return True
    '    '        If .FlagEP <> txtEP.Text Then Return True
    '    '        If .FlagPrt1 <> txtPr1.Text Then Return True
    '    '        If .FlagPrt2 <> txtPr2.Text Then Return True

    '    '        If .FlagMin <> cmbTime.SelectedValue.ToString Then Return True

    '    '        If cmbStatus.SelectedValue <> gCstCodeChManualInputStatus.ToString Then
    '    '            If .Status <> cmbStatus.Text Then Return True
    '    '        Else
    '    '            If .Status <> txtStatus.Text Then Return True
    '    '        End If

    '    '        If .FlagStatusAlarm <> chkStatusAlarm.Checked Then Return True
    '    '        'If .FilterCoef <> txtFilterCoeficient.Text Then Return True

    '    '        If .DeviceStatus <> cmbDeviceStatus.SelectedValue Then Return True
    '    '        For i As Integer = 0 To 15
    '    '            If .DeviceStatusUse(i) <> grdDeviceStatus(0, i).Value Then Return True
    '    '        Next

    '    '    End With

    '    '    Return False

    '    'End Function

    '    'Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilterCoeficient.KeyPress

    '    '    Try

    '    '        e.Handled = gCheckTextInput(2, sender, e.KeyChar)

    '    '    Catch ex As Exception
    '    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    '    End Try

    '    'End Sub

    '#End Region

#End Region

   
End Class
