Public Class frmOpsGraphFreeDetail

#Region "定数定義"

    Private Const mCstChTypeNotExist As String = "not exist."

#End Region

#Region "変数定義"

    Private mintRtn As Integer
    Private mintCurPos As Integer
    Private mblnInitFlg As Boolean
    Private mblnSetingFlg As Boolean
    Private mudtFreeGraphDetail() As gTypSetOpsFreeGraphDetail  ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    Private mudtChannelGroup As gTypChannelGroup

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  1:キャンセル
    ' 引き数    : ARG1 - (I ) 対象位置
    ' 　　　    : ARG2 - (IO) フリーグラフ詳細
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intCurPos As Integer, _
                          ByRef udtFreeGraphDetail() As gTypSetOpsFreeGraphDetail, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mintCurPos = intCurPos
            mudtFreeGraphDetail = udtFreeGraphDetail.Clone

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtFreeGraphDetail = mudtFreeGraphDetail.Clone
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmOpsGraphFreeDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''グリッドを初期化する
            Call mInitialDataGrid()
            Call mInitialDataGridBit()

            ''コンボボックス初期化
            Call gSetComboBox(cmbDeviceStatus, gEnmComboType.ctChListChannelListDeviceStatus)

            ''入力不可テキスト背景色設定
            txtChTypeName.BackColor = gColorGridRowBackReadOnly
            txtChTypeCode.BackColor = gColorGridRowBackReadOnly

            ''表示灯詳細フレーム使用不可
            fraIndicatorDetail.Enabled = False
            Call mClearBitGridCheck()

            ''アナログ詳細フレーム使用不可
            fraAnalogDetail.Visible = False

            ''設定コントロールクリア
            Call mClearSetControl()

            ''チャンネルグループ情報取得
            Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroup)

            ''グループNoコンボ作成
            Call mMakeComboGroupNo(mudtChannelGroup)

            ''画面表示
            Call mSetDisplay(mudtFreeGraphDetail)

            ''グループが選択されていない場合はグループ１を選択
            If cmbGroupList.SelectedIndex = -1 Then
                cmbGroupList.SelectedIndex = 0
            End If

            grdBit.EndEdit()

            ''初期化フラグ
            mblnInitFlg = False

            ''更新時はチャンネル変更不可
            If optGraphTypeAnalog.Checked _
            Or optGraphTypeBar.Checked _
            Or optGraphTypeCounter.Checked _
            Or optGraphTypeIndicator.Checked Then

                ''テキストボックス使用不可
                txtSelectChannel.ReadOnly = True
                txtSelectChannel.BackColor = gColorGridRowBackReadOnly

                ''グリッド使用不可
                fraChannelSelect.Enabled = False

                Call optIndicator_CheckedChanged(optGraphTypeIndicator, New EventArgs)

            End If

            ' ''グラフタイプが設定済みの場合は表示灯チェックイベントを呼ぶ
            ' ''（設定済み内容に基づくコントロール使用可/不可の設定をする）
            'If optGraphTypeAnalog.Checked = True _
            'Or optGraphTypeBar.Checked = True _
            'Or optGraphTypeCounter.Checked = True _
            'Or optGraphTypeIndicator.Checked = True Then
            '    'Call optIndicator_CheckedChanged(optGraphTypeIndicator, New EventArgs)
            'End If

            If txtSelectChannel.Text = "" Then

                ''選択されたチャンネルをテキストに表示
                txtSelectChannel.Text = grdCH(0, grdCH.SelectedCells(0).RowIndex).Value

                ''チャンネル選択設定
                Call mSetSelectChannel(cmbGroupList.SelectedIndex, grdCH.SelectedCells(0).RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グループNoコンボインデックスチェンジ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択されたグループのチャンネルリストを表示する
    '--------------------------------------------------------------------
    Private Sub cmbGroupList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroupList.SelectedIndexChanged

        Try

            Call mSetChannelList(cmbGroupList.SelectedIndex, mudtChannelGroup)

            ''選択されたチャンネルをテキストに表示
            txtSelectChannel.Text = grdCH(0, grdCH.SelectedCells(0).RowIndex).Value

            ''チャンネル選択設定
            Call mSetSelectChannel(cmbGroupList.SelectedIndex, grdCH.SelectedCells(0).RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドエンター
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellEnter

        Try

            'Dim blnCheck As Boolean

            If mblnInitFlg Then Return

            ' ''チェック状態を保存
            'blnCheck = optGraphTypeAnalog.Checked

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''選択されたチャンネルをテキストに表示
            txtSelectChannel.Text = grdCH(0, grdCH.SelectedCells(0).RowIndex).Value

            ''チャンネル選択設定
            Call mSetSelectChannel(cmbGroupList.SelectedIndex, grdCH.SelectedCells(0).RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Counter 選択時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraphTypeCounter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphTypeCounter.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''チェックが外れた場合は処理しない
            If Not optGraphTypeCounter.Checked Then
                Return
            End If

            ''初期化中フラグ設定
            mblnInitFlg = True

            fraAnalogDetail.Visible = False
            Call mClearSetControl()
            Call mClearBitGridCheck()

            ''チェック状態を元に戻す
            optGraphTypeCounter.Checked = True

            ''初期化中フラグ解除
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Analog 選択時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraphTypeAnalog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphTypeAnalog.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''チェックが外れた場合は処理しない
            If Not optGraphTypeAnalog.Checked Then
                Return
            End If

            ''初期化中フラグ設定
            mblnInitFlg = True

            fraAnalogDetail.Visible = True

            ''Analog Detail コントロール使用可／不可
            Call mControlEnableAnalogDetail()
            Call mClearSetControl()
            Call mClearBitGridCheck()

            ''チェック状態を元に戻す
            optGraphTypeAnalog.Checked = True

            ''初期化中フラグ解除
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Bar 選択時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraphTypeBar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphTypeBar.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''チェックが外れた場合は処理しない
            If Not optGraphTypeBar.Checked Then
                Return
            End If

            ''初期化中フラグ設定
            mblnInitFlg = True

            fraAnalogDetail.Visible = True

            ''Analog Detail コントロール使用可／不可
            Call mControlEnableAnalogDetail()
            Call mClearSetControl()
            Call mClearBitGridCheck()

            ''チェック状態を元に戻す
            optGraphTypeBar.Checked = True

            ''初期化中フラグ解除
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mControlEnableAnalogDetail()

        Try

            If optGraphTypeAnalog.Checked Then

                lblAnalogScale.Visible = True
                txtAnalogScale.Visible = True
                lblAnalogColor.Visible = True
                txtAnalogColor.Visible = True

            ElseIf optGraphTypeBar.Checked Then

                lblAnalogScale.Visible = True
                txtAnalogScale.Visible = True
                lblAnalogColor.Visible = False
                txtAnalogColor.Visible = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Indicator 選択時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optIndicator_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphTypeIndicator.CheckedChanged

        Try

            If mblnInitFlg Then Return

            ''初期化中フラグ設定
            mblnInitFlg = True

            If optGraphTypeIndicator.Checked Then

                fraIndicatorDetail.Enabled = True
                fraAnalogDetail.Visible = False

                Select Case txtChTypeCode.Text
                    Case gCstCodeChTypeAnalog

                        '============================
                        ''アナログ
                        '============================
                        If optIndTypeData.Checked Then
                            optIndTypeAlarm.Checked = True
                            optIndTypeData.Checked = False
                        End If
                        optIndTypeData.Enabled = False
                        cmbDeviceStatus.Enabled = False

                    Case gCstCodeChTypePulse

                        '============================
                        ''パルス積算
                        '============================
                        If optIndTypeData.Checked Then
                            optIndTypeAlarm.Checked = True
                            optIndTypeData.Checked = False
                        End If
                        optIndTypeData.Enabled = False
                        cmbDeviceStatus.Enabled = False

                    Case gCstCodeChTypeValve

                        Select Case txtChDataType.Text
                            Case gCstCodeChDataTypeValveDI_DO

                                '============================
                                ''バルブDIDO
                                '============================
                                optIndTypeData.Enabled = True
                                cmbDeviceStatus.Enabled = False

                                ' ''Bitグリッド設定
                                'grdDeviceStatus.RowCount = 8
                                'For i As Integer = 0 To grdDeviceStatus.RowCount - 1
                                '    grdDeviceStatus(0, i).Value = False
                                '    grdDeviceStatus(1, i).Value = "Bit" & i
                                '    grdDeviceStatus.Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                                'Next

                            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, _
                                 gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

                                '============================
                                ''バルブAIDO、AIAO
                                '============================
                                If optIndTypeData.Checked Then
                                    optIndTypeAlarm.Checked = True
                                    optIndTypeData.Checked = False
                                End If
                                optIndTypeData.Enabled = False
                                cmbDeviceStatus.Enabled = False

                        End Select

                    Case gCstCodeChTypeDigital

                        If txtChDataType.Text = gCstCodeChDataTypeDigitalDeviceStatus Then

                            '============================
                            ''デジタル
                            '============================
                            optIndTypeData.Enabled = True
                            cmbDeviceStatus.Enabled = False

                        Else

                            '============================
                            ''システム
                            '============================
                            optIndTypeData.Enabled = True
                            cmbDeviceStatus.Enabled = True

                            Dim udtChInfo As gTypSetChRec = Nothing

                            If gGetChannelInfo(txtSelectChannel.Text, udtChInfo) Then

                                Dim intIndex As Integer = gGetChannelSystemDeviceStatus(cmbDeviceStatus, udtChInfo.SystemInfoKikiCode01)
                                If intIndex <> 0 Then
                                    cmbDeviceStatus.SelectedValue = CStr(intIndex)

                                    'Dim intValue As Integer
                                    'Dim udtKiki() As gTypCodeName = Nothing

                                    ' ''グリッド クリア
                                    'grdDeviceStatus.RowCount = 16
                                    'For i As Integer = 0 To grdDeviceStatus.RowCount - 1
                                    '    grdDeviceStatus(0, i).Value = False
                                    '    grdDeviceStatus(1, i).Value = ""
                                    '    grdDeviceStatus.Rows(i).Cells(0).ReadOnly = True
                                    '    grdDeviceStatus.Rows(i).Cells(0).Style.BackColor = gColorGridRowBackReadOnly
                                    '    grdDeviceStatus.Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                                    'Next

                                    ' ''選択済み機器状態グループコードGET
                                    'intValue = cmbDeviceStatus.SelectedValue

                                    ' ''グループコード内の機器状態を全てGET
                                    'Call gGetComboCodeName(udtKiki, _
                                    '                       gEnmComboType.ctChListChannelListDeviceStatus, _
                                    '                       intValue.ToString("00"))

                                    'For i As Integer = 0 To UBound(udtKiki)

                                    '    grdDeviceStatus(1, i).Value = udtKiki(i).strName

                                    '    If Trim(udtKiki(i).strName) <> "" Then
                                    '        grdDeviceStatus.Rows(i).Cells(0).ReadOnly = False
                                    '        grdDeviceStatus.Rows(i).Cells(0).Style.BackColor = Color.White
                                    '    End If

                                    'Next

                                End If

                            End If

                        End If

                    Case gCstCodeChTypeMotor

                        '============================
                        ''モーター
                        '============================
                        optIndTypeData.Enabled = True
                        cmbDeviceStatus.Enabled = False

                        ' ''Bitグリッド設定
                        'grdDeviceStatus.RowCount = 5
                        'For i As Integer = 0 To grdDeviceStatus.RowCount - 1
                        '    grdDeviceStatus(0, i).Value = False
                        '    grdDeviceStatus(1, i).Value = "RUN" & i + 1
                        '    grdDeviceStatus.Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                        'Next

                    Case gCstCodeChTypeComposite

                        '============================
                        ''コンポジット
                        '============================
                        optIndTypeData.Enabled = True
                        cmbDeviceStatus.Enabled = False

                        ' ''Bitグリッド設定
                        'grdDeviceStatus.RowCount = 8
                        'For i As Integer = 0 To grdDeviceStatus.RowCount - 1
                        '    grdDeviceStatus(0, i).Value = False
                        '    grdDeviceStatus(1, i).Value = "Bit" & i
                        '    grdDeviceStatus.Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                        'Next

                    Case Else

                        optIndTypeData.Enabled = False
                        cmbDeviceStatus.Enabled = False

                End Select

            ElseIf optGraphTypeAnalog.Checked Or optGraphTypeBar.Checked Then

                fraAnalogDetail.Visible = True
                fraIndicatorDetail.Enabled = False

                ''Analog Detail コントロール使用可／不可
                Call mControlEnableAnalogDetail()

            Else

                fraIndicatorDetail.Enabled = False

            End If

            ''初期化中フラグ解除
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 表示種別なしチェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optIndTypeNoSet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optIndTypeNoSet.CheckedChanged

        ''Bitグリッドの設定状態をクリアする
        Call mClearBitGridCheck()

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 表示種別なし以外チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optIndTypeBitOK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optIndTypeData.CheckedChanged, _
                                                                                                                   optIndTypeAlarm.CheckedChanged, _
                                                                                                                   optIndTypeRepose.CheckedChanged, _
                                                                                                                   optIndTypeSensor.CheckedChanged
        ''Bitグリッドの状態を設定する
        Call mSetBitGridUse()

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
            Call mSetStructure(mudtFreeGraphDetail)

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtSelectChannel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSelectChannel.KeyPress, txtChTypeCode.KeyPress, txtChTypeName.KeyPress, txtChDataType.KeyPress

        Try

            If txtSelectChannel.ReadOnly Then Return
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSelectChannel.Validated, txtChTypeCode.Validated, txtChTypeName.Validated, txtChDataType.Validated

        Try

            If txtSelectChannel.ReadOnly Then Return
            If IsNumeric(txtSelectChannel.Text) Then

                txtSelectChannel.Text = Integer.Parse(txtSelectChannel.Text).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSelectChannel.Validating

        Try

            If txtSelectChannel.ReadOnly Then Return
            If Not IsNumeric(txtSelectChannel.Text) Then Return
            e.Cancel = gChkTextNumSpan(0, 65535, txtSelectChannel.Text)

            ''チャンネル番号設定後の処理
            Call mSetChannelText()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Analog Scale 入力制限
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub txtAnalogScale_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAnalogScale.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Analog Color 入力制限
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub txtAnalogColor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAnalogColor.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "内部関数"

    Private Sub mSetChannelText()

        Try

            Dim intGroupIndex As Integer
            Dim intRowIndex As Integer

            ''チャンネル検索
            If gSearchChannel(txtSelectChannel.Text, mudtChannelGroup, intGroupIndex, intRowIndex) Then

                ''グリッドを選択状態にする
                cmbGroupList.SelectedIndex = intGroupIndex
                Call Application.DoEvents()
                grdCH.SelectedCells(0).Selected = False
                grdCH.Rows(intRowIndex).Cells(0).Selected = True
                grdCH.FirstDisplayedScrollingRowIndex = intRowIndex

                ''チャンネル選択設定
                Call mSetSelectChannel(intGroupIndex, intRowIndex)

            Else

                txtChTypeName.Text = mCstChTypeNotExist
                optGraphTypeAnalog.Enabled = False
                optGraphTypeBar.Enabled = False
                optGraphTypeCounter.Enabled = False
                optGraphTypeIndicator.Enabled = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル選択設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グループインデックス
    ' 　　　    : ARG2 - (I ) 行インデックス
    ' 機能説明  : チャンネル選択時の諸処理を行う
    '--------------------------------------------------------------------
    Private Sub mSetSelectChannel(ByVal intGroupIndex As Integer, ByVal intRowIndex As Integer)

        Try

            ''チャンネルタイプを表示
            With mudtChannelGroup.udtGroup(intGroupIndex).udtChannelData(intRowIndex).udtChCommon

                ''初期化中以外でチャンネルタイプが変わったらコントロールクリア
                If Not mblnInitFlg Then
                    'If txtChTypeName.Text <> gGetNameChannelType(.shtChType) Then
                    Call mClearSetControl()
                    Call mClearBitGridCheck()
                    'End If
                End If

                txtSelectChannel.Text = grdCH(0, grdCH.SelectedCells(0).RowIndex).Value
                txtChTypeName.Text = gGetNameChannelType(.shtChType)
                txtChTypeCode.Text = .shtChType
                txtChDataType.Text = .shtData

                Call mSetGraphTypeEnable(mudtFreeGraphDetail, .shtChType, .shtData)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定コントロールクリア
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定コントロールをクリアする
    '--------------------------------------------------------------------
    Private Sub mClearSetControl()

        Try

            optGraphTypeAnalog.Checked = False
            optGraphTypeBar.Checked = False
            optGraphTypeCounter.Checked = False
            optGraphTypeIndicator.Checked = False

            optIndTypeData.Checked = False
            optIndTypeAlarm.Checked = False
            optIndTypeRepose.Checked = False
            optIndTypeSensor.Checked = False

            txtAnalogScale.Text = ""
            txtAnalogColor.Text = ""

            optIndTypeData.Checked = False
            optIndTypeAlarm.Checked = False
            optIndTypeRepose.Checked = False
            optIndTypeSensor.Checked = False
            optIndTypeNoSet.Checked = False

            ''デバイスステータスクリア
            cmbDeviceStatus.SelectedIndex = -1
            'grdBit.RowCount = 0

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

            Dim blnFlg As Boolean = False

            ''共通数値入力チェック
            If Not gChkInputNum(txtSelectChannel, 0, 65535, "CH No.", False, True) Then Return False

            ''存在しないチャンネルを設定中
            If txtChTypeName.Text = mCstChTypeNotExist Then
                Call MessageBox.Show("Please set the existing channel in [CH No.]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtSelectChannel.Focus()
                Return False
            End If

            ' ''アナログ、デジタル、モーター以外のチャンネルを選択中
            'If txtChTypeCode.Text <> gCstCodeChTypeAnalog And _
            '   txtChTypeCode.Text <> gCstCodeChTypeDigital And _
            '   txtChTypeCode.Text <> gCstCodeChTypeMotor Then
            '    Call MessageBox.Show("Please set the analog or digital or motor channel", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Call txtSelectChannel.Focus()
            '    Return False
            'End If

            ''グラフタイプが全て使用不可の場合
            If optGraphTypeAnalog.Enabled = False _
            And optGraphTypeBar.Enabled = False _
            And optGraphTypeCounter.Enabled = False _
            And optGraphTypeIndicator.Enabled = False Then

                Call MessageBox.Show("CH No. " & txtSelectChannel.Text & " cannot set it to FreeGraph.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False

            End If

            ''グラフタイプが選択されていない場合
            If optGraphTypeAnalog.Checked = False And _
               optGraphTypeBar.Checked = False And _
               optGraphTypeCounter.Checked = False And _
               optGraphTypeIndicator.Checked = False Then

                Call MessageBox.Show("Please select [Graph Type]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False

            End If

            ''アナログメーターの場合
            If optGraphTypeAnalog.Checked Then

                ''共通数値入力チェック
                If Not gChkInputNum(txtAnalogScale, 3, 7, "Scale", False, True) Then Return False
                If Not gChkInputNum(txtAnalogColor, 0, 8, "Color", False, True) Then Return False

            End If

            ''バーグラフの場合
            If optGraphTypeBar.Checked Then

                ''共通数値入力チェック
                If Not gChkInputNum(txtAnalogScale, 3, 7, "Scale", False, True) Then Return False

            End If

            ''表示灯の場合
            If optGraphTypeIndicator.Checked Then

                ''DisplayTypeが選択されていない場合
                If Not optIndTypeData.Checked And _
                   Not optIndTypeAlarm.Checked And _
                   Not optIndTypeRepose.Checked And _
                   Not optIndTypeSensor.Checked And _
                   Not optIndTypeNoSet.Checked Then

                    Call MessageBox.Show("Please select [Display Type]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False

                End If


            End If

            ''表示灯の場合
            'If optGraphTypeIndicator.Checked Then

            '    Select Case txtChTypeCode.Text

            '        Case gCstCodeChTypeAnalog

            '            '============================
            '            ''アナログ
            '            '============================
            '            ''アナログ用設定項目の全てにチェックがない場合
            '            If chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False And _
            '               chkStatSensor.Checked = False Then

            '                Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Return False

            '            End If

            '        Case gCstCodeChTypePulse

            '            '============================
            '            ''パルス積算
            '            '============================
            '            ''パルス用設定項目の全てにチェックがない場合
            '            If chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False Then

            '                Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Return False

            '            End If

            '        Case gCstCodeChTypeValve

            '            Select Case txtChDataType.Text
            '                Case gCstCodeChDataTypeValveDI_DO

            '                    '============================
            '                    ''バルブDIDO
            '                    '============================
            '                    ''設定項目の全てにチェックがない場合
            '                    If chkStatAlm.Checked = False And _
            '                       chkStatRepose.Checked = False Then

            '                        For i As Integer = 0 To grdDeviceStatus.RowCount - 1
            '                            If grdDeviceStatus.Rows(i).Cells(0).Value Then
            '                                blnFlg = True
            '                                Exit For
            '                            End If
            '                        Next

            '                        If Not blnFlg Then
            '                            Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                            Return False
            '                        End If

            '                    End If

            '                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, _
            '                     gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

            '                    '============================
            '                    ''バルブAIDO、AIAO
            '                    '============================
            '                    ''アナログ用設定項目の全てにチェックがない場合
            '                    If chkStatAlm.Checked = False And _
            '                       chkStatRepose.Checked = False And _
            '                       chkStatSensor.Checked = False Then

            '                        Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                        Return False

            '                    End If

            '            End Select

            '        Case gCstCodeChTypeDigital

            '            '============================
            '            ''デジタル
            '            '============================
            '            ''デジタル用設定項目の全てにチェックがない場合
            '            If chkDigitalON.Checked = False And _
            '               chkDigitalOFF.Checked = False And _
            '               chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False Then

            '                Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Return False

            '            End If

            '        Case gCstCodeChTypeMotor

            '            '============================
            '            ''モーター
            '            '============================
            '            ''設定項目の全てにチェックがない場合
            '            If chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False Then

            '                For i As Integer = 0 To grdDeviceStatus.RowCount - 1
            '                    If grdDeviceStatus.Rows(i).Cells(0).Value Then
            '                        blnFlg = True
            '                        Exit For
            '                    End If
            '                Next

            '                If Not blnFlg Then
            '                    Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                    Return False
            '                End If

            '            End If

            '        Case gCstCodeChTypeComposite

            '            '============================
            '            ''コンポジット
            '            '============================
            '            ''設定項目の全てにチェックがない場合
            '            If chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False Then

            '                For i As Integer = 0 To grdDeviceStatus.RowCount - 1
            '                    If grdDeviceStatus.Rows(i).Cells(0).Value Then
            '                        blnFlg = True
            '                        Exit For
            '                    End If
            '                Next

            '                If Not blnFlg Then
            '                    Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                    Return False
            '                End If

            '            End If

            '        Case gCstCodeChTypeSystem

            '            '============================
            '            ''システム
            '            '============================
            '            ''設定項目の全てにチェックがない場合
            '            If chkStatAlm.Checked = False And _
            '               chkStatRepose.Checked = False Then

            '                For i As Integer = 0 To grdDeviceStatus.RowCount - 1
            '                    If grdDeviceStatus.Rows(i).Cells(0).Value Then
            '                        blnFlg = True
            '                        Exit For
            '                    End If
            '                Next

            '                If Not blnFlg Then
            '                    Call MessageBox.Show("Please set [Indicator Detail]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                    Return False
            '                End If

            '            End If

            '        Case Else

            '    End Select

            'End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 画面設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フリーグラフ詳細構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    ' 備考      : 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtFreeGraphDetail() As gTypSetOpsFreeGraphDetail)

        Try

            Dim blnClearFlg As Boolean = False

            ' ''設定内容から使用可能なグラフタイプを設定
            'Call mSetGraphTypeEnable(udtFreeGraphDetail)

            With udtFreeGraphDetail(mintCurPos - 1)

                ' ''CH NO
                'txtSelectChannel.Text = IIf(.shtChNo = 0, "", .shtChNo.ToString("0000"))

                ' ''チャンネル番号設定後の処理
                'Call mSetChannelText()

                ''グラフタイプ
                Select Case .bytType
                    Case gCstCodeOpsFreeGrapTypeCounter

                        ''カウンタ
                        optGraphTypeCounter.Checked = True

                        ''アナログ詳細非表示
                        fraAnalogDetail.Visible = False

                    Case gCstCodeOpsFreeGrapTypeBar

                        ''バー
                        optGraphTypeBar.Checked = True

                        ''メモリ分割数
                        txtAnalogScale.Text = .bytScale

                        ''アナログ詳細表示
                        fraAnalogDetail.Visible = True
                        Call mControlEnableAnalogDetail()

                    Case gCstCodeOpsFreeGrapTypeAnalog

                        ''アナログメーター
                        optGraphTypeAnalog.Checked = True

                        ''メモリ分割数
                        txtAnalogScale.Text = .bytScale

                        ''表示色
                        txtAnalogColor.Text = .bytColor

                        ''アナログ詳細表示
                        fraAnalogDetail.Visible = True
                        Call mControlEnableAnalogDetail()

                    Case gCstCodeOpsFreeGrapTypeIndicator

                        ''表示灯
                        mblnInitFlg = False
                        optGraphTypeIndicator.Checked = True
                        mblnInitFlg = True

                        ''表示種別
                        Select Case .bytIndicatorKind
                            Case gCstNameOpsFreeIndKindNoSet
                                optIndTypeNoSet.Checked = True
                            Case gCstNameOpsFreeIndKindData
                                optIndTypeData.Checked = True
                            Case gCstNameOpsFreeIndKindAlarm
                                optIndTypeAlarm.Checked = True
                            Case gCstNameOpsFreeIndKindRepose
                                optIndTypeRepose.Checked = True
                            Case gCstNameOpsFreeIndKindSensor
                                optIndTypeSensor.Checked = True
                        End Select

                        ''表示灯詳細
                        For i As Integer = 0 To grdBit.RowCount - 1

                            ''チェック状態を設定
                            grdBit.Rows(i).Cells(0).Value = gBitCheck(.shtIndicatorPattern, i)

                        Next

                        ''アナログ詳細非表示
                        fraAnalogDetail.Visible = False

                        ' ''表示灯詳細（デジタル）
                        'Select Case .bytIndicatorDigital
                        '    Case gCstNameOpsFreeIndDigitalOFF : chkDigitalOFF.Checked = True
                        '    Case gCstNameOpsFreeIndDigitalON : chkDigitalON.Checked = True
                        'End Select

                        ' ''表示灯詳細（アナログ）
                        'Select Case .bytIndicatorAnalog
                        '    Case gCstNameOpsFreeIndAnalogALM : chkStatAlm.Checked = True
                        '    Case gCstNameOpsFreeIndAnalogREPOSE : chkStatRepose.Checked = True
                        '    Case gCstNameOpsFreeIndAnalogSENSOR : chkStatSensor.Checked = True
                        'End Select

                        ' ''表示灯詳細
                        'For i As Integer = 0 To grdDeviceStatus.RowCount - 1

                        '    ''チェック状態を設定
                        '    grdDeviceStatus.Rows(i).Cells(0).Value = gBitCheck(.shtIndicatorMotor, i)

                        '    ''読み取り専用チェックボックスにチェックが入った場合
                        '    ''（フリーグラフ設定後にシステムCHのデバイスステータスが変更された場合等）
                        '    If grdDeviceStatus.Rows(i).Cells(0).Style.BackColor = gColorGridRowBackReadOnly And _
                        '       gBitCheck(.shtIndicatorMotor, i) Then
                        '        ''クリアフラグを立てる
                        '        blnClearFlg = True
                        '    End If

                        'Next

                        ' ''クリアフラグが立っている場合
                        'If blnClearFlg Then

                        '    ''チェック状態をクリアする
                        '    For i As Integer = 0 To grdDeviceStatus.RowCount - 1
                        '        grdDeviceStatus.Rows(i).Cells(0).Value = False
                        '    Next

                        'End If

                End Select

                ''CH NO
                txtSelectChannel.Text = IIf(.shtChNo = 0, "", .shtChNo.ToString("0000"))

                ''チャンネル番号設定後の処理
                Call mSetChannelText()

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) フリーグラフ詳細構造体
    ' 機能説明  : 構造体に設定を格納する
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet() As gTypSetOpsFreeGraphDetail)

        Try

            Dim intIndex1 As Integer = 0
            Dim intIndex2 As Integer = 0
            Dim intIndex3 As Integer = 0
            Dim intIndex4 As Integer = 0

            '==================
            ''カウンタ
            '==================
            If optGraphTypeCounter.Checked Then

                ''位置情報作成
                intIndex1 = mintCurPos - 1          ''対象位置
                intIndex2 = (mintCurPos - 1) + 1    ''対象位置の１つ右

                ''データを設定
                Call mSetStructureOne(udtSet(intIndex1))
                Call mSetStructureOne(udtSet(intIndex2))

                ''対象位置以外の配列にTopPosを設定
                udtSet(intIndex2).bytTopPos = mintCurPos

            End If

            '==================
            ''バー
            '==================
            If optGraphTypeBar.Checked Then

                ''位置情報作成
                intIndex1 = mintCurPos - 1          ''対象位置
                intIndex2 = (mintCurPos - 1) + 8    ''対象位置の１つ下

                ''データを設定
                Call mSetStructureOne(udtSet(intIndex1))
                Call mSetStructureOne(udtSet(intIndex2))

                ''対象位置以外の配列にTopPosを設定
                udtSet(intIndex2).bytTopPos = mintCurPos

            End If

            '==================
            ''アナログメーター
            '==================
            If optGraphTypeAnalog.Checked Then

                ''位置情報作成
                intIndex1 = mintCurPos - 1              ''対象位置
                intIndex2 = (mintCurPos - 1) + 1        ''対象位置の１つ右
                intIndex3 = (mintCurPos - 1) + 8        ''対象位置の１つ下
                intIndex4 = (mintCurPos - 1) + 8 + 1    ''対象位置の右斜め下

                ''データを設定
                Call mSetStructureOne(udtSet(intIndex1))
                Call mSetStructureOne(udtSet(intIndex2))
                Call mSetStructureOne(udtSet(intIndex3))
                Call mSetStructureOne(udtSet(intIndex4))

                ''対象位置以外の配列にTopPosを設定
                udtSet(intIndex2).bytTopPos = mintCurPos
                udtSet(intIndex3).bytTopPos = mintCurPos
                udtSet(intIndex4).bytTopPos = mintCurPos

            End If

            '==================
            ''表示灯
            '==================
            If optGraphTypeIndicator.Checked Then

                ''位置情報作成
                intIndex1 = mintCurPos - 1              ''対象位置

                ''データを設定
                Call mSetStructureOne(udtSet(intIndex1))

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) フリーグラフ詳細構造体
    ' 機能説明  : 構造体に設定を格納する
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mSetStructureOne(ByRef udtSet As gTypSetOpsFreeGraphDetail)

        Try

            With udtSet

                ''CH NO
                .shtChNo = CCUInt16(txtSelectChannel.Text)

                ''グラフタイプ
                If optGraphTypeCounter.Checked Then .bytType = gCstCodeOpsFreeGrapTypeCounter

                ''======================
                '' バー
                ''======================
                If optGraphTypeBar.Checked Then

                    ''グラフタイプ
                    .bytType = gCstCodeOpsFreeGrapTypeBar

                    ''メモリ分割数
                    .bytScale = CCbyte(txtAnalogScale.Text)

                End If

                ''======================
                '' アナログメーター
                ''======================
                If optGraphTypeAnalog.Checked Then

                    ''グラフタイプ
                    .bytType = gCstCodeOpsFreeGrapTypeAnalog

                    ''メモリ分割数
                    .bytScale = CCbyte(txtAnalogScale.Text)

                    ''表示色
                    .bytColor = CCbyte(txtAnalogColor.Text)

                End If

                ''======================
                '' 表示灯
                ''======================
                If optGraphTypeIndicator.Checked Then

                    ''グラフタイプ
                    .bytType = gCstCodeOpsFreeGrapTypeIndicator

                    ''表示種別
                    If optIndTypeNoSet.Checked Then
                        .bytIndicatorKind = gCstNameOpsFreeIndKindNoSet
                    ElseIf optIndTypeData.Checked Then
                        .bytIndicatorKind = gCstNameOpsFreeIndKindData
                    ElseIf optIndTypeAlarm.Checked Then
                        .bytIndicatorKind = gCstNameOpsFreeIndKindAlarm
                    ElseIf optIndTypeRepose.Checked Then
                        .bytIndicatorKind = gCstNameOpsFreeIndKindRepose
                    ElseIf optIndTypeSensor.Checked Then
                        .bytIndicatorKind = gCstNameOpsFreeIndKindSensor
                    End If

                    ''表示灯詳細
                    For i As Integer = 0 To grdBit.RowCount - 1
                        .shtIndicatorPattern = gBitSet(.shtIndicatorPattern, i, IIf(grdBit.Rows(i).Cells(0).Value, True, False))
                    Next

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グラフタイプ使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) フリーグラフ詳細構造体
    ' 機能説明  : 設定状況からグラフタイプの使用可/不可を設定する
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mSetGraphTypeEnable(ByVal udtFreeGraphDetail() As gTypSetOpsFreeGraphDetail, _
                                    ByVal intChType As Integer, _
                                    ByVal intData As Integer)

        Try

            With udtFreeGraphDetail(mintCurPos - 1)

                If optGraphTypeAnalog.Checked = True _
                Or optGraphTypeBar.Checked = True _
                Or optGraphTypeCounter.Checked = True _
                Or optGraphTypeIndicator.Checked = True Then

                    ''とりあえず全て使用不可
                    optGraphTypeCounter.Enabled = False
                    optGraphTypeBar.Enabled = False
                    optGraphTypeAnalog.Enabled = False
                    optGraphTypeIndicator.Enabled = False

                    ''選択中のグラフタイプのみ使用可能
                    Select Case .bytType
                        Case gCstCodeOpsFreeGrapTypeCounter
                            optGraphTypeCounter.Enabled = True
                        Case gCstCodeOpsFreeGrapTypeBar
                            optGraphTypeBar.Enabled = True
                        Case gCstCodeOpsFreeGrapTypeAnalog
                            optGraphTypeAnalog.Enabled = True
                        Case gCstCodeOpsFreeGrapTypeIndicator
                            optGraphTypeIndicator.Enabled = True
                    End Select

                Else

                    '=============================
                    ''グラフタイプが未選択の場合
                    '=============================
                    ''とりあえず全て使用可
                    optGraphTypeCounter.Enabled = True
                    optGraphTypeBar.Enabled = True
                    optGraphTypeAnalog.Enabled = True
                    optGraphTypeIndicator.Enabled = True

                    Select Case mintCurPos
                        Case 8, 16, 24

                            '=========================
                            ''選択位置が右端の場合
                            '=========================
                            ''カウンタとアナログメーター使用不可
                            optGraphTypeCounter.Enabled = False
                            optGraphTypeAnalog.Enabled = False

                            ''真下が設定済みの場合はバーも使用不可
                            If udtFreeGraphDetail((mintCurPos - 1) + 8).bytType <> gCstCodeOpsFreeGrapTypeNone Then
                                optGraphTypeBar.Enabled = False
                            End If

                        Case 25, 26, 27, 28, 29, 30, 31

                            '=========================
                            ''選択位置が最下段の場合
                            '=========================
                            ''バーとアナログメーター使用不可
                            optGraphTypeBar.Enabled = False
                            optGraphTypeAnalog.Enabled = False

                            ''右隣が設定済みの場合はカウンタも使用不可
                            If udtFreeGraphDetail((mintCurPos - 1) + 1).bytType <> gCstCodeOpsFreeGrapTypeNone Then
                                optGraphTypeCounter.Enabled = False
                            End If

                        Case 32

                            '=========================
                            ''選択位置が最右下の場合
                            '=========================
                            ''カウンタとバーとアナログメーター使用不可
                            optGraphTypeCounter.Enabled = False
                            optGraphTypeBar.Enabled = False
                            optGraphTypeAnalog.Enabled = False

                        Case Else

                            ''右隣が設定済みの場合はカウンタとアナログメーター使用不可
                            If udtFreeGraphDetail((mintCurPos - 1) + 1).bytType <> gCstCodeOpsFreeGrapTypeNone Then
                                optGraphTypeCounter.Enabled = False
                                optGraphTypeAnalog.Enabled = False
                            End If

                            ''真下が設定済みの場合はバーとアナログメーター使用不可
                            If udtFreeGraphDetail((mintCurPos - 1) + 8).bytType <> gCstCodeOpsFreeGrapTypeNone Then
                                optGraphTypeBar.Enabled = False
                                optGraphTypeAnalog.Enabled = False
                            End If

                            ''右斜下が設定済みの場合はアナログメーター使用不可
                            If udtFreeGraphDetail((mintCurPos - 1) + 8 + 1).bytType <> gCstCodeOpsFreeGrapTypeNone Then
                                optGraphTypeAnalog.Enabled = False
                            End If

                    End Select

                    ''チャンネルタイプによる使用制限
                    Select Case intChType
                        Case gCstCodeChTypeAnalog, gCstCodeChTypePulse

                            '============================
                            ''アナログ、パルス積算
                            '============================
                            ''何もしない

                        Case gCstCodeChTypeValve

                            Select Case intData
                                Case gCstCodeChDataTypeValveDI_DO

                                    '============================
                                    ''バルブDIDO
                                    '============================
                                    optGraphTypeAnalog.Enabled = False
                                    optGraphTypeBar.Enabled = False
                                    fraAnalogDetail.Visible = False

                                Case gCstCodeChDataTypeValveAI_DO1, _
                                     gCstCodeChDataTypeValveAI_DO2, _
                                     gCstCodeChDataTypeValveAI_AO1, _
                                     gCstCodeChDataTypeValveAI_AO2

                                    '============================
                                    ''バルブAIDO、AIAO
                                    '============================
                                    ''何もしない

                                Case Else

                                    optGraphTypeAnalog.Enabled = False
                                    optGraphTypeBar.Enabled = False
                                    optGraphTypeCounter.Enabled = False
                                    optGraphTypeIndicator.Enabled = False
                                    fraAnalogDetail.Visible = False

                            End Select

                        Case gCstCodeChTypeDigital, _
                             gCstCodeChTypeMotor, _
                             gCstCodeChTypeComposite

                            '=============================================
                            ''デジタル、モーター、コンポジット
                            '=============================================
                            optGraphTypeAnalog.Enabled = False
                            optGraphTypeBar.Enabled = False
                            fraAnalogDetail.Visible = False

                        Case Else

                            optGraphTypeAnalog.Enabled = False
                            optGraphTypeBar.Enabled = False
                            optGraphTypeCounter.Enabled = False
                            optGraphTypeIndicator.Enabled = False
                            fraAnalogDetail.Visible = False

                    End Select

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グループNoコンボ作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルグループ構造体
    ' 機能説明  : チャンネルグループ構造体からグループNoコンボを作成する
    '--------------------------------------------------------------------
    Private Sub mMakeComboGroupNo(ByVal udtChannelGroup As gTypChannelGroup)

        Try

            Call cmbGroupList.Items.Clear()

            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup)
                cmbGroupList.Items.Add(i + 1)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルリスト作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グループインデックス
    ' 　　　    : ARG2 - (I ) チャンネルグループ構造体
    ' 機能説明  : グリッドにチャンネルリストを表示する
    '--------------------------------------------------------------------
    Private Sub mSetChannelList(ByVal intGroupIndex As Integer, _
                                ByVal udtChannelGroup As gTypChannelGroup)

        Try

            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup(intGroupIndex).udtChannelData)

                With udtChannelGroup.udtGroup(intGroupIndex).udtChannelData(i).udtChCommon

                    grdCH(0, i).Value = gConvZeroToNull(.shtChno, "0000")
                    grdCH(1, i).Value = gGetString(.strChitem)
                    grdCH(2, i).Value = gGetNameChannelType(.shtChType)

                End With

            Next

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChName"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtChType"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 60
                .Columns(1).HeaderText = "CH Name" : .Columns(1).Width = 170
                .Columns(2).HeaderText = "CH Type" : .Columns(2).Width = 80
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 101
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

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChName").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChType").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

                .ReadOnly = True

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGridBit()

        Try
            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            With grdBit

                Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "ChkUse"
                Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtName" : Column2.ReadOnly = True
                Column2.DefaultCellStyle.BackColor = gColorGridRowBackReadOnly

                '列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
                .RowHeadersVisible = False

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 40
                .Columns(1).HeaderText = "Status" : .Columns(1).Width = 194
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

                For i = 0 To .RowCount - 1
                    grdBit(0, i).Value = False
                    grdBit(1, i).Value = "Bit" & i
                    grdBit.Rows(i).Cells(1).Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdBit)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mClearBitGridCheck()

        For i = 0 To grdBit.RowCount - 1

            ''チェックを解除
            grdBit(0, i).Value = False

            ''選択を解除
            grdBit(0, i).Selected = False
            grdBit(1, i).Selected = False

            ''Use列の色を変更
            grdBit(0, i).Style.BackColor = gColorGridRowBackReadOnly

        Next

        ''左上隅を表示
        grdBit.FirstDisplayedCell = grdBit.Rows(0).Cells(0)

        ''グリッド使用不可
        grdBit.Enabled = False

    End Sub

    Private Sub mSetBitGridUse()

        For i = 0 To grdBit.RowCount - 1

            ''Use列の色を変更
            grdBit(0, i).Style.BackColor = gColorGridRowBackBase

            ''選択を解除
            grdBit(0, i).Selected = False
            grdBit(1, i).Selected = False

        Next

        ''左上隅を表示
        grdBit(0, 0).Selected = True
        grdBit.FirstDisplayedCell = grdBit.Rows(0).Cells(0)

        ''グリッド使用可
        grdBit.Enabled = True

    End Sub

#End Region

End Class
