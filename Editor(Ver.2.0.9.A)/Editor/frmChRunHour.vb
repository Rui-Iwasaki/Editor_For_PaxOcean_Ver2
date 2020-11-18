Public Class frmChRunHour

#Region "変数定義"

    Private mudtSetChRunHour As gTypSetChRunHour

    ''イベントキャンセルフラグ
    Private mintCancelFlag As Integer

    ''モーターのステータス情報格納
    Private mMotorStatus1() As String
    Private mMotorStatus2() As String

    Private dataGridViewComboBox As DataGridViewComboBoxEditingControl = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChRunHour_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            mintCancelFlag = 1

            ''グリッドを初期化する
            Call mInitialDataGrid()

            mintCancelFlag = 0

            ''モーターチャンネルのステータス情報を獲得する
            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "RunHourMotor")

            ''配列再定義
            mudtSetChRunHour.InitArray()

            ''画面設定
            Call mSetDisplay(gudt.SetChRunHour)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Pick Upボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 運転積算情報をピックアップして表示する
    '--------------------------------------------------------------------
    Private Sub cmdPickUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPickUp.Click

        Try

            Dim intSetCnt As Integer = 0
            Dim intMotorFlg As Integer = 0

            ''確認メッセージ
            If MessageBox.Show("May I Pick Up?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                ''黄色を元に戻す
                Call mReturnCellColor()

                ''オールクリア
                For i As Integer = 0 To grdRunningHour.RowCount - 1

                    grdRunningHour.Rows(i).Cells("txtRhCH").Value = ""
                    grdRunningHour.Rows(i).Cells("txtTriggerCH").Value = ""
                    grdRunningHour.Rows(i).Cells("cmbStatus").Value = "0"
                    grdRunningHour.Rows(i).Cells(3).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(i).Cells(4).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(i).Cells(5).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(i).Cells(6).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(i).Cells(7).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(i).Cells(3).Style.BackColor = gColorGridRowBackReadOnly
                    grdRunningHour.Rows(i).Cells(4).Style.BackColor = gColorGridRowBackReadOnly
                    grdRunningHour.Rows(i).Cells(5).Style.BackColor = gColorGridRowBackReadOnly
                    grdRunningHour.Rows(i).Cells(6).Style.BackColor = gColorGridRowBackReadOnly
                    grdRunningHour.Rows(i).Cells(7).Style.BackColor = gColorGridRowBackReadOnly

                Next

                ''チャンネル種類がパルス積算で積算運転時間のチャンネルを表示
                For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                    With gudt.SetChInfo.udtChannel(i).udtChCommon

                        ''パルス積算チャンネル
                        If .shtChType = gCstCodeChTypePulse Then
                            If .shtChno = 701 Then
                                Dim DebugA As Integer = 0
                            End If
                            ''データタイプが積算運転時間
                            If .shtData = gCstCodeChDataTypePulseRevoTotalHour _
                            Or .shtData = gCstCodeChDataTypePulseRevoTotalMin _
                            Or .shtData = gCstCodeChDataTypePulseRevoDayHour _
                            Or .shtData = gCstCodeChDataTypePulseRevoDayMin _
                            Or .shtData = gCstCodeChDataTypePulseRevoLapHour _
                            Or .shtData = gCstCodeChDataTypePulseRevoLapMin _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDev _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDevTotalMin _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDevDayHour _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDevDayMin _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDevLapHour _
                            Or .shtData = gCstCodeChDataTypePulseRevoExtDevLapMin Then

                                'Ver2.0.3.2 ワークCH=演算もﾋﾟｯｸｱｯﾌﾟする
                                ''ワークCHではない
                                'If Not gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk) Then

                                ''運転積算チャンネル表示
                                grdRunningHour(0, intSetCnt).Value = Integer.Parse(.shtChno).ToString("0000")

                                ''FUアドレス「設定なし」が設定されている場合
                                If .shtFuno = gCstCodeChNotSetFuNo _
                                Or .shtPortno = gCstCodeChNotSetFuNo _
                                Or .shtPin = gCstCodeChNotSetFuNo Then

                                    ''FUアドレスに「設定なし」が設定されている場合はトリガーチャンネルの検索を行わない
                                    'Ver2.0.1.7 トリガーは何もしないが表示はする
                                    'ただし背景色を黄色とする
                                    grdRunningHour(1, intSetCnt).Style.BackColor = Color.Yellow
                                    intSetCnt += 1
                                Else

                                    ''トリガーチャンネルを検索して表示
                                    Dim blOK As Boolean = False
                                    For j As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                                        ''FUアドレスが同一の場合
                                        If .shtFuno = gudt.SetChInfo.udtChannel(j).udtChCommon.shtFuno And _
                                           .shtPortno = gudt.SetChInfo.udtChannel(j).udtChCommon.shtPortno And _
                                           .shtPin = gudt.SetChInfo.udtChannel(j).udtChCommon.shtPin Then

                                            ''今表示した運転積算チャンネル以外の場合
                                            'Ver2.0.6.3 ﾁｬﾝﾈﾙ種別が「デジタル」「モーター」のみとする
                                            If (.shtChno <> gudt.SetChInfo.udtChannel(j).udtChCommon.shtChno) And _
                                                (gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital Or _
                                                 gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeMotor) _
                                            Then
                                                grdRunningHour(1, intSetCnt).Value = Integer.Parse(gudt.SetChInfo.udtChannel(j).udtChCommon.shtChno).ToString("0000")
                                                'grdRunningHour(2, intSetCnt).Value = CStr(gudt.SetChInfo.udtChannel(j).udtChCommon.shtData)
                                                Dim cboTemp As DataGridViewComboBoxColumn = CType(grdRunningHour.Columns(2), DataGridViewComboBoxColumn)
                                                Dim blCbo As Boolean = False
                                                For za As Integer = 0 To cboTemp.Items.Count - 1 Step 1
                                                    'Ver2.0.3.2 ﾃﾞｼﾞﾀﾙNCは272
                                                    Dim intStatusData As Integer
                                                    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital Then
                                                        intStatusData = 272
                                                    Else
                                                        intStatusData = gudt.SetChInfo.udtChannel(j).udtChCommon.shtData
                                                    End If
                                                    Dim strStatusData As String = CStr(NZf(intStatusData))
                                                    If cboTemp.Items.Item(za).row.itemarray(0).ToString = strStatusData Then
                                                        blCbo = True
                                                        Exit For
                                                    End If
                                                Next za
                                                If blCbo = False Then
                                                    grdRunningHour(2, intSetCnt).Value = Nothing
                                                Else
                                                    Dim intStatusData As Integer
                                                    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital Then
                                                        intStatusData = 272
                                                    Else
                                                        intStatusData = gudt.SetChInfo.udtChannel(j).udtChCommon.shtData
                                                    End If
                                                    Dim strStatusData As String = CStr(NZf(intStatusData))
                                                    grdRunningHour(2, intSetCnt).Value = strStatusData
                                                End If

                                                'Ver2.0.1.7 ST/BYとAUTO以外は選択状態にする
                                                For z As Integer = 3 To 7 Step 1
                                                    If NZf(grdRunningHour(z, intSetCnt).Value) <> "" Then
                                                        Select Case grdRunningHour(z, intSetCnt).Value
                                                            Case "ST/BY", "AUTO"
                                                            Case Else
                                                                grdRunningHour(z, intSetCnt).Style.BackColor = Color.RoyalBlue
                                                                grdRunningHour(z, intSetCnt).Style.ForeColor = Color.White

                                                                grdRunningHour(z, intSetCnt).Style.SelectionBackColor = Color.RoyalBlue
                                                                grdRunningHour(z, intSetCnt).Style.SelectionForeColor = Color.White
                                                        End Select
                                                    End If
                                                Next z

                                                '' Ver2.0.8.7 2018.08.07
                                                '' 重複アドレスがないかチェック
                                                For x As Integer = j + 1 To UBound(gudt.SetChInfo.udtChannel)
                                                    ''FUアドレスが同一の場合
                                                    If .shtFuno = gudt.SetChInfo.udtChannel(x).udtChCommon.shtFuno And _
                                                      .shtPortno = gudt.SetChInfo.udtChannel(x).udtChCommon.shtPortno And _
                                                         .shtPin = gudt.SetChInfo.udtChannel(x).udtChCommon.shtPin Then
                                                        If (.shtChno <> gudt.SetChInfo.udtChannel(x).udtChCommon.shtChno) And _
                                                          (gudt.SetChInfo.udtChannel(x).udtChCommon.shtChType = gCstCodeChTypeMotor) Then
                                                            intMotorFlg = 1
                                                            Exit For
                                                        End If
                                                    End If
                                                Next x

                                                '' 重複アドレスがMOTORの場合はもう一巡させる（MOTORのCHをセットする)
                                                If intMotorFlg = 1 Then
                                                    intMotorFlg = 0
                                                Else
                                                    blOK = True
                                                    Exit For
                                                End If


                                            End If


                                        End If
                                    Next

                                    'Ver2.0.1.7 Triggerが取得できなかった場合は背景黄色
                                    If blOK = False Then
                                        grdRunningHour(1, intSetCnt).Style.BackColor = Color.Yellow
                                    End If

                                    intSetCnt += 1

                                End If

                                'End If

                            End If

                        End If

                    End With

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChRunHour)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChRunHour, gudt.SetChRunHour) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetChRunHour, gudt.SetChRunHour)

                ''チャンネル構造体のトリガチャンネルを設定する
                Call mSetTrrigerChannelInfo(gudt.SetChInfo)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytChRunHour = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChRunHour = 1

                ''更新フラグ設定   Ver1.10.4 2016.03.30 追加
                gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                gudt.SetEditorUpdateInfo.udtSave.bytChConvNow = 1
                gudt.SetEditorUpdateInfo.udtSave.bytChConvPrev = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChConvNow = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChConvPrev = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる（2010/12/15 追加）
            Call grdRunningHour.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChRunHour)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChRunHour, gudt.SetChRunHour) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        Call mCopyStructure(mudtSetChRunHour, gudt.SetChRunHour)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytChRunHour = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChRunHour = 1

                        ''更新フラグ設定   Ver1.10.4 2016.03.30 追加
                        gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytChConvNow = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytChConvPrev = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChConvNow = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChConvPrev = 1

                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームのインスタンスを破棄する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボボックスを1回のクリックでドロップダウンさせるようにする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub grdRunningHour_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRunningHour.CellEnter

    '    Dim dgv As DataGridView = CType(sender, DataGridView)

    '    If dgv.Columns(e.ColumnIndex).Name = "cmbStatus" Then

    '        SendKeys.Send("{F4}")

    '    End If

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： RUN-1 ～ RUN-5 をクリックした時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdRunningHour_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRunningHour.CellClick

        Try

            If e.RowIndex < 0 Then Exit Sub
            If e.ColumnIndex < 3 Or e.ColumnIndex > 7 Then Exit Sub

            With grdRunningHour.Rows(e.RowIndex).Cells(e.ColumnIndex)

                If .Value <> "" Then

                    If .Style.BackColor <> Color.RoyalBlue Then

                        .Style.BackColor = Color.RoyalBlue
                        .Style.ForeColor = Color.White

                        .Style.SelectionBackColor = Color.RoyalBlue
                        .Style.SelectionForeColor = Color.White

                    Else

                        .Style.SelectionBackColor = gColorGridRowBackReadOnly
                        .Style.SelectionForeColor = Color.Black

                        .Style.BackColor = gColorGridRowBackReadOnly
                        '.Style.BackColor = IIf(e.RowIndex Mod 2 <> 0, gColorGridRowBack, Color.White)
                        .Style.ForeColor = Color.Black

                    End If

                End If

            End With

            Call grdRunningHour.EndEdit()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH入力時、ステータス変更時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdRunningHour_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRunningHour.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)

            ''RH_CH, Trigger_CHを入力後にフォーマットする
            If e.ColumnIndex = 0 Or e.ColumnIndex = 1 Then

                If dgv.CurrentCell.OwningColumn.Name = "txtRhCH" Then

                    If grdRunningHour.Rows(e.RowIndex).Cells(0).Value <> Nothing Then

                        If IsNumeric(grdRunningHour.Rows(e.RowIndex).Cells(0).Value) Then

                            grdRunningHour(0, e.RowIndex).Value = Integer.Parse(grdRunningHour(0, e.RowIndex).Value).ToString("0000")

                        End If

                    End If

                End If

                If dgv.CurrentCell.OwningColumn.Name = "txtTriggerCH" Then

                    If grdRunningHour.Rows(e.RowIndex).Cells(1).Value <> Nothing Then

                        If IsNumeric(grdRunningHour.Rows(e.RowIndex).Cells(1).Value) Then

                            grdRunningHour(1, e.RowIndex).Value = Integer.Parse(grdRunningHour(1, e.RowIndex).Value).ToString("0000")

                        End If

                    End If

                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： Status を変更した時
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdRunningHour_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRunningHour.CellValueChanged

        Try

            If mintCancelFlag = 1 Then Exit Sub
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intValue As Integer
            Dim strwk() As String = Nothing

            'Ver2.0.1.7 TrigerもしくはRH_CHを入力した場合、背景色黄はクリアする
            Select Case e.ColumnIndex
                Case 0, 1
                    If grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.Yellow Or _
                        grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.Red Then
                        If e.RowIndex Mod 2 <> 0 Then
                            grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = gColorGridRowBack
                        Else
                            grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.White
                        End If
                    End If
            End Select

            'Ver2.0.3.1
            'TriggerCHを入力されたら、ｽﾃｰﾀｽを変える
            If e.ColumnIndex = 1 Then
                If grdRunningHour(1, e.RowIndex).Value = "" Then
                    'TriggerCHが空白になったらｽﾃｰﾀｽ等ｸﾘｱ
                    grdRunningHour(2, e.RowIndex).Value = Nothing
                    For i As Integer = 3 To 7
                        grdRunningHour(i, e.RowIndex).Value = ""
                        grdRunningHour.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Black
                        grdRunningHour.Rows(e.RowIndex).Cells(i).Style.BackColor = gColorGridRowBackReadOnly
                    Next i
                Else
                    '空白以外の場合CHのDataTypeをゲットしてｽﾃｰﾀｽにｾｯﾄ
                    Dim blCHari As Boolean = False
                    For i = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                        With gudt.SetChInfo.udtChannel(i)
                            If grdRunningHour(1, e.RowIndex).Value = .udtChCommon.shtChno Then
                                Dim cboTemp As DataGridViewComboBoxColumn = CType(grdRunningHour.Columns(2), DataGridViewComboBoxColumn)
                                Dim blCbo As Boolean = False
                                For za As Integer = 0 To cboTemp.Items.Count - 1 Step 1
                                    'Ver2.0.3.2 ﾃﾞｼﾞﾀﾙNCは272
                                    'Ver2.0.6.3 デジタル、モーター以外は、ｽﾃｰﾀｽ空白
                                    Dim intStatusData As Integer
                                    If .udtChCommon.shtChType = gCstCodeChTypeDigital Then
                                        intStatusData = 272
                                    ElseIf (.udtChCommon.shtChType = gCstCodeChTypeMotor) Then
                                        intStatusData = .udtChCommon.shtData
                                    Else
                                        blCbo = False
                                        Exit For
                                    End If
                                    Dim strStatusData As String = CStr(NZf(intStatusData))
                                    If cboTemp.Items.Item(za).row.itemarray(0).ToString = strStatusData Then
                                        blCbo = True
                                        Exit For
                                    End If
                                Next za
                                If blCbo = False Then
                                    grdRunningHour(2, e.RowIndex).Value = Nothing
                                    'Ver2.0.6.3 デジタルでもモーターでもない場合、背景色赤へ
                                    grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.Red
                                Else
                                    Dim intStatusData As Integer
                                    If .udtChCommon.shtChType = gCstCodeChTypeDigital Then
                                        intStatusData = 272
                                    Else
                                        intStatusData = .udtChCommon.shtData
                                    End If
                                    Dim strStatusData As String = CStr(NZf(intStatusData))
                                    grdRunningHour(2, e.RowIndex).Value = strStatusData
                                    'ｾｯﾄできたら3-7も行う
                                    'Ver2.0.1.7 ST/BYとAUTO以外は選択状態にする
                                    For z As Integer = 3 To 7 Step 1
                                        If NZf(grdRunningHour(z, e.RowIndex).Value) <> "" Then
                                            Select Case grdRunningHour(z, e.RowIndex).Value
                                                Case "ST/BY", "AUTO"
                                                Case Else
                                                    grdRunningHour(z, e.RowIndex).Style.BackColor = Color.RoyalBlue
                                                    grdRunningHour(z, e.RowIndex).Style.ForeColor = Color.White

                                                    grdRunningHour(z, e.RowIndex).Style.SelectionBackColor = Color.RoyalBlue
                                                    grdRunningHour(z, e.RowIndex).Style.SelectionForeColor = Color.White
                                            End Select
                                        End If
                                    Next z
                                End If
                                blCHari = True
                                Exit For
                            End If
                        End With
                    Next i
                    If blCHari = False Then
                        grdRunningHour(2, e.RowIndex).Value = Nothing
                        'Ver2.0.6.3 デジタルでもモーターでもない場合、背景色赤へ
                        grdRunningHour.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.Red
                    End If
                End If
            End If

            If e.ColumnIndex = 2 Then   ''Status

                ''クリア
                For i As Integer = 3 To 7
                    grdRunningHour(i, e.RowIndex).Value = ""
                    grdRunningHour.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Black
                    grdRunningHour.Rows(e.RowIndex).Cells(i).Style.BackColor = gColorGridRowBackReadOnly
                    'grdRunningHour(i, e.RowIndex).Style.BackColor = IIf(e.RowIndex Mod 2 <> 0, gColorGridRowBack, Color.White)
                    'grdRunningHour(i, e.RowIndex).Style.ForeColor = Color.Black
                Next

                intValue = dgv(2, e.RowIndex).Value     ''Status

                If intValue >= gCstCodeChDataTypeMotorManRun And intValue <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                    strwk = mMotorStatus1(intValue - 16).ToString.Split("_")

                ElseIf intValue >= gCstCodeChDataTypeMotorAbnorRun And intValue <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                    strwk = mMotorStatus2(intValue - 32).ToString.Split("_")


                    'Ver2.0.0.2 モーター種別増加 START
                    'Ver2.0.8.2 ランアワー設定、通信モーター分追加
                ElseIf intValue >= gCstCodeChDataTypeMotorRManRun And intValue <= gCstCodeChDataTypeMotorRManRunK Then
                    strwk = mMotorStatus1(intValue - 80).ToString.Split("_")
                ElseIf intValue >= gCstCodeChDataTypeMotorRAbnorRun And intValue <= gCstCodeChDataTypeMotorRAbnorRunK Then
                    strwk = mMotorStatus2(intValue - 96).ToString.Split("_")
                    'Ver2.0.0.2 モーター種別増加 END


                ElseIf intValue >= (gCstCodeChDataTypeDigitalNC + &H100) Then       '' Ver1.11.8.1 2016.10.26  NC対応
                    strwk = mMotorStatus1(UBound(mMotorStatus1)).ToString.Split("_")

                ElseIf intValue = gCstCodeChDataTypeMotorDevice Or _
                        intValue = gCstCodeChDataTypeMotorDeviceJacom Or _
                        intValue = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                        intValue = gCstCodeChDataTypeMotorRDevice Then    'Ver2.0.0.2 モーター種別増加 R Device 追加

                    ''strwk = mMotorStatus1(UBound(mMotorStatus1)).ToString.Split("_")
                    strwk = mMotorStatus1(UBound(mMotorStatus1) - 1).ToString.Split("_")    '' Ver1.11.8.1 2016.10.26  配列位置変更

                ElseIf intValue = 0 Then
                    dgv(3, e.RowIndex).Value = ""
                    dgv(4, e.RowIndex).Value = ""
                    dgv(5, e.RowIndex).Value = ""
                    dgv(6, e.RowIndex).Value = ""
                    dgv(7, e.RowIndex).Value = ""

                    Exit Sub

                Else
                    Exit Sub
                End If

                ''ステータス種別により状態が変化する
                Dim intCnt As Integer = UBound(strwk)

                If intCnt >= 0 Then
                    If strwk(0) <> "" Then dgv(3, e.RowIndex).Value = strwk(0)
                End If

                If intCnt >= 1 Then
                    If strwk(1) <> "" Then dgv(4, e.RowIndex).Value = strwk(1)
                End If

                If intCnt >= 2 Then
                    If strwk(2) <> "" Then dgv(5, e.RowIndex).Value = strwk(2)
                End If

                If intCnt >= 3 Then
                    If strwk(3) <> "" Then dgv(6, e.RowIndex).Value = strwk(3)
                End If

                If intCnt >= 4 Then
                    If strwk(4) <> "" Then dgv(7, e.RowIndex).Value = strwk(4)
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [CH]列には数字しか入力出来ないようにする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdRunningHour_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdRunningHour.EditingControlShowing

        Try

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdRunningHour_KeyPress

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name = "txtRhCH" _
                Or dgv.CurrentCell.OwningColumn.Name = "txtTriggerCH" Then

                    AddHandler tb.KeyPress, AddressOf grdRunningHour_KeyPress

                End If

            ElseIf TypeOf e.Control Is DataGridViewComboBoxEditingControl Then

                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim cb As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler cb.SelectedIndexChanged, AddressOf dataGridViewComboBox_SelectedIndexChanged

                If dgv.CurrentCell.OwningColumn.Name = "cmbStatus" Then

                    '編集のために表示されているコントロールを取得
                    Me.dataGridViewComboBox = CType(e.Control, DataGridViewComboBoxEditingControl)

                    'SelectedIndexChangedイベントハンドラを追加
                    AddHandler Me.dataGridViewComboBox.SelectedIndexChanged, AddressOf dataGridViewComboBox_SelectedIndexChanged

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub dataGridViewComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            '▼▼▼ 下記コードを有効にすれば、コンボの項目を変更しただけでステータスが表示されるようになるが、十分な動作検証を行っていないためコメント ▼▼▼▼▼▼▼▼▼▼▼▼
            Dim dgv As DataGridView = CType(grdRunningHour, DataGridView)
            Dim intValue As Integer
            Dim strwk() As String = Nothing

            ''クリア
            For i As Integer = 3 To 7
                grdRunningHour(i, grdRunningHour.CurrentCell.RowIndex).Value = ""
                grdRunningHour.Rows(grdRunningHour.CurrentCell.RowIndex).Cells(i).Style.ForeColor = Color.Black
                grdRunningHour.Rows(grdRunningHour.CurrentCell.RowIndex).Cells(i).Style.BackColor = gColorGridRowBackReadOnly
                'grdRunningHour(i, e.RowIndex).Style.BackColor = IIf(e.RowIndex Mod 2 <> 0, gColorGridRowBack, Color.White)
                'grdRunningHour(i, e.RowIndex).Style.ForeColor = Color.Black
            Next

            intValue = dataGridViewComboBox.SelectedValue 'dgv(2, grdRunningHour.CurrentCell.RowIndex).Value     ''Status

            If intValue >= gCstCodeChDataTypeMotorManRun And intValue <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                strwk = mMotorStatus1(intValue - 16).ToString.Split("_")

            ElseIf intValue >= gCstCodeChDataTypeMotorAbnorRun And intValue <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                strwk = mMotorStatus2(intValue - 32).ToString.Split("_")


                'Ver2.0.0.2 モーター種別増加 START
                'Ver2.0.8.2 ランアワー設定、通信モーター分追加
            ElseIf intValue >= gCstCodeChDataTypeMotorRManRun And intValue <= gCstCodeChDataTypeMotorRManRunK Then
                strwk = mMotorStatus1(intValue - 80).ToString.Split("_")
            ElseIf intValue >= gCstCodeChDataTypeMotorRAbnorRun And intValue <= gCstCodeChDataTypeMotorRAbnorRunK Then
                strwk = mMotorStatus2(intValue - 96).ToString.Split("_")
                'Ver2.0.0.2 モーター種別増加 END

            ElseIf intValue >= (gCstCodeChDataTypeDigitalNC + &H100) Then       '' Ver1.11.8.1 2016.10.26  NC対応
                strwk = mMotorStatus1(UBound(mMotorStatus1)).ToString.Split("_")

            ElseIf intValue >= gCstCodeChDataTypeMotorDevice Or _
                        intValue = gCstCodeChDataTypeMotorDeviceJacom Or _
                        intValue = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                        intValue = gCstCodeChDataTypeMotorRDevice Then    'Ver2.0.0.2 モーター種別増加 R Device 追加

                'strwk = mMotorStatus1(UBound(mMotorStatus1)).ToString.Split("_")
                strwk = mMotorStatus1(UBound(mMotorStatus1) - 1).ToString.Split("_")

            ElseIf intValue = 0 Then
                dgv(3, grdRunningHour.CurrentCell.RowIndex).Value = ""
                dgv(4, grdRunningHour.CurrentCell.RowIndex).Value = ""
                dgv(5, grdRunningHour.CurrentCell.RowIndex).Value = ""
                dgv(6, grdRunningHour.CurrentCell.RowIndex).Value = ""
                dgv(7, grdRunningHour.CurrentCell.RowIndex).Value = ""

                Exit Sub

            Else
                Exit Sub
            End If

            ''ステータス種別により状態が変化する
            Dim intCnt As Integer = UBound(strwk)

            If intCnt >= 0 Then
                If strwk(0) <> "" Then dgv(3, grdRunningHour.CurrentCell.RowIndex).Value = strwk(0)
            End If

            If intCnt >= 1 Then
                If strwk(1) <> "" Then dgv(4, grdRunningHour.CurrentCell.RowIndex).Value = strwk(1)
            End If

            If intCnt >= 2 Then
                If strwk(2) <> "" Then dgv(5, grdRunningHour.CurrentCell.RowIndex).Value = strwk(2)
            End If

            If intCnt >= 3 Then
                If strwk(3) <> "" Then dgv(6, grdRunningHour.CurrentCell.RowIndex).Value = strwk(3)
            End If

            If intCnt >= 4 Then
                If strwk(4) <> "" Then dgv(7, grdRunningHour.CurrentCell.RowIndex).Value = strwk(4)
            End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdRunningHour_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRunningHour.CellEndEdit

        'SelectedIndexChangedイベントハンドラを削除
        If Not (Me.dataGridViewComboBox Is Nothing) Then

            RemoveHandler Me.dataGridViewComboBox.SelectedIndexChanged, AddressOf dataGridViewComboBox_SelectedIndexChanged
            Me.dataGridViewComboBox = Nothing

        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdRunningHour_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdRunningHour.KeyPress

        Try

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub
            If grdRunningHour.CurrentCell.ReadOnly Then Exit Sub

            If grdRunningHour.CurrentCell.OwningColumn.Name = "txtRhCH" Or grdRunningHour.CurrentCell.OwningColumn.Name = "txtTriggerCH" Then

                Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                e.Handled = gCheckTextInput(5, dgv, e.KeyChar, True)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドエラー
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdRunningHour_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdRunningHour.DataError

        Try

            'If Not (e.Exception Is Nothing) Then
            '    MessageBox.Show(Me, _
            '        String.Format("(Column:{0}, Row:{1}) のセルでエラーが発生しました。" + _
            '            vbCrLf + vbCrLf + "説明: {2}", _
            '            e.ColumnIndex, e.RowIndex, e.Exception.Message), _
            '        "エラーが発生しました", _
            '        MessageBoxButtons.OK, _
            '        MessageBoxIcon.Error)
            'End If

            ''エラーが発生した時に、元の値に戻るようにする
            e.Cancel = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean
        'Ver2.0.3.1 ｽﾃｰﾀｽが一件も選択されてない場合YesNoメッセージ
        Try

            Dim i As Integer, j As Integer
            Dim flg As Integer = 0

            Dim blStatusZero As Boolean = False

            ''黄色を元に戻す
            Call mReturnCellColor()

            For i = 0 To grdRunningHour.RowCount - 1

                ''共通テキスト入力チェック
                If Not gChkInputText(grdRunningHour(0, i), "RH_CH", i + 1, True, True) Then Return False
                If Not gChkInputText(grdRunningHour(1, i), "Trigger_CH", i + 1, True, True) Then Return False

                ''CHは1～65535までか？
                If IsNumeric(grdRunningHour(0, i).Value) Then
                    If CInt(grdRunningHour(0, i).Value) = 0 Or CInt(grdRunningHour(0, i).Value) > 65535 Then
                        grdRunningHour(0, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If
                End If

                If IsNumeric(grdRunningHour(1, i).Value) Then
                    If CInt(grdRunningHour(1, i).Value) = 0 Or CInt(grdRunningHour(1, i).Value) > 65535 Then
                        grdRunningHour(1, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If
                End If

                ''RH_CHとTriggerCHは2つセットで入力されているか？
                If grdRunningHour(0, i).Value <> "" And grdRunningHour(1, i).Value = "" Then
                    grdRunningHour(1, i).Style.BackColor = Color.Yellow
                    flg += 1
                End If

                If grdRunningHour(0, i).Value = "" And grdRunningHour(1, i).Value <> "" Then
                    grdRunningHour(0, i).Style.BackColor = Color.Yellow
                    flg += 1
                End If


                If grdRunningHour(0, i).Value <> "" And grdRunningHour(1, i).Value <> "" Then

                    ''RH_CHとTriggerCHは同じCHがセットされていないか？
                    If grdRunningHour(0, i).Value = grdRunningHour(1, i).Value Then
                        grdRunningHour(1, i).Style.BackColor = Color.Yellow
                        grdRunningHour(0, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If

                    ''Statusは選択されているか？
                    If grdRunningHour(2, i).FormattedValue = "" Then
                        grdRunningHour(2, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If

                End If

                ''RH_CHに同じCHがセットされていないか？
                If grdRunningHour(0, i).Value <> "" Then

                    For j = i + 1 To grdRunningHour.RowCount - 1

                        If grdRunningHour(0, j).Value <> Nothing Then

                            If grdRunningHour(0, j).Value.ToString <> "" Then

                                If grdRunningHour(0, i).Value = grdRunningHour(0, j).Value Then
                                    grdRunningHour(0, i).Style.BackColor = Color.Yellow
                                    grdRunningHour(0, j).Style.BackColor = Color.Yellow
                                    flg += 1
                                End If

                            End If

                        End If

                    Next j

                End If

                ''Statusが選択されているのにCHがセットされていない？
                If grdRunningHour(2, i).FormattedValue <> "" Then

                    If IsNumeric(grdRunningHour(0, i).Value) = False Then
                        grdRunningHour(0, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If

                    If IsNumeric(grdRunningHour(1, i).Value) = False Then
                        grdRunningHour(1, i).Style.BackColor = Color.Yellow
                        flg += 1
                    End If
                End If

                'どれも選択してなければエラー
                If blStatusZero = False Then
                    For zz As Integer = 3 To 7 Step 1
                        If grdRunningHour(zz, i).Style.ForeColor = Color.White Then
                            blStatusZero = True
                        End If
                    Next zz
                End If

            Next

            If flg <> 0 Then MsgBox("The input is wrong.", MsgBoxStyle.Exclamation, "Running Hour Set")

            'ｽﾃｰﾀｽがゼロ件時のエラーメッセージ
            If flg = 0 And blStatusZero = False Then
                If MsgBox("Status not selected. Are you OK?", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Running Hour Set") = vbCancel Then
                    Return False
                End If
            End If

            Return IIf(flg = 0, True, False)

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
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtRhCH"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtTriggerCH"
            Dim Column3 As New DataGridViewComboBoxColumn : Column3.Name = "cmbStatus" : Column3.FlatStyle = FlatStyle.Popup
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtRun1" : Column4.ReadOnly = True
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtRun2" : Column5.ReadOnly = True
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtRun3" : Column6.ReadOnly = True
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtRun4" : Column7.ReadOnly = True
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtRun5" : Column8.ReadOnly = True

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdRunningHour

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Run Hour CH" : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Trigger CH" : .Columns(1).Width = 80
                .Columns(2).HeaderText = "STATUS" : .Columns(2).Width = 150
                .Columns(3).HeaderText = "RUN1" : .Columns(3).Width = 70
                .Columns(4).HeaderText = "RUN2" : .Columns(4).Width = 70
                .Columns(5).HeaderText = "RUN3" : .Columns(5).Width = 70
                .Columns(6).HeaderText = "RUN4" : .Columns(6).Width = 70
                .Columns(7).HeaderText = "RUN5" : .Columns(7).Width = 70
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 257
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 70
                For i = 1 To .Rows.Count
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
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
                    .Rows(i).Cells("txtRun1").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRun2").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRun3").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRun4").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRun5").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdRunningHour)

            End With

            ''Statusコンボ 初期設定
            Call gSetComboBox(Column3, gEnmComboType.ctChRunHourColumn3)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChRunHour)

        Try

            For i As Integer = 0 To grdRunningHour.RowCount - 1

                With udtSet.udtDetail(i)

                    If IsNumeric(grdRunningHour.Rows(i).Cells("txtRhCH").Value) Then

                        .shtChid = CCUInt16(grdRunningHour.Rows(i).Cells("txtRhCH").Value)
                        .shtTrgChid = CCUInt16(grdRunningHour.Rows(i).Cells("txtTriggerCH").Value)
                        .shtStatus = CCShort(grdRunningHour.Rows(i).Cells("cmbStatus").Value)

                        ''ステータスの選択によって分岐
                        If .shtStatus = gCstCodeChDataTypeMotorDevice Or .shtStatus = gCstCodeChDataTypeMotorRDevice Then  'Ver2.0.0.2 モーター種別増加 R Device

                            '====================================
                            ''機器運転（DeviceOperation）の場合
                            '====================================
                            ''ONが選択されている場合は、Bit6を立てる（変更前の設定がDeviceOperation以外の可能性があるので 0 クリアしてから）
                            .shtMask = 0
                            .shtMask = gBitSet(.shtMask, 1, IIf(grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue, True, False))
                            '.shtMask = gBitSet(.shtMask, 6, IIf(grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue, True, False))

                        ElseIf .shtStatus = (gCstCodeChDataTypeDigitalNC + &H100) Then    '' Ver1.11.8.1 2016.10.26 NC対応
                            .shtMask = 0
                            .shtMask = gBitSet(.shtMask, 6, IIf(grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue, True, False))
                        Else

                            '====================================
                            ''その他モータービットの場合
                            '====================================
                            ''選択された位置のビットを操作する（変更前の設定がDeviceOperationの可能性があるので 0 クリアしてから）
                            .shtMask = 0
                            .shtMask = gBitSet(.shtMask, 1, IIf(grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue, True, False))
                            .shtMask = gBitSet(.shtMask, 2, IIf(grdRunningHour.Rows(i).Cells(4).Style.BackColor = Color.RoyalBlue, True, False))
                            .shtMask = gBitSet(.shtMask, 3, IIf(grdRunningHour.Rows(i).Cells(5).Style.BackColor = Color.RoyalBlue, True, False))
                            .shtMask = gBitSet(.shtMask, 4, IIf(grdRunningHour.Rows(i).Cells(6).Style.BackColor = Color.RoyalBlue, True, False))
                            .shtMask = gBitSet(.shtMask, 5, IIf(grdRunningHour.Rows(i).Cells(7).Style.BackColor = Color.RoyalBlue, True, False))

                        End If

                    Else        '' Ver1.10.3  2016.03.09  ｸﾘｱ処理追加
                        .shtChid = 0
                        .shtTrgChid = 0
                        .shtStatus = 0
                        .shtMask = 0
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChRunHour)

        Try

            For i As Integer = 0 To grdRunningHour.RowCount - 1

                With udtSet.udtDetail(i)

                    mintCancelFlag = 1

                    If .shtChid > 0 Then grdRunningHour.Rows(i).Cells("txtRhCH").Value = .shtChid.ToString("0000")
                    If .shtTrgChid > 0 Then grdRunningHour.Rows(i).Cells("txtTriggerCH").Value = .shtTrgChid.ToString("0000")

                    mintCancelFlag = 0

                    grdRunningHour.Rows(i).Cells("cmbStatus").Value = .shtStatus.ToString

                    mintCancelFlag = 1

                    ''ステータスの選択によって分岐
                    If .shtStatus = gCstCodeChDataTypeMotorDevice Or .shtStatus = gCstCodeChDataTypeMotorRDevice Then  'Ver2.0.0.2 モーター種別増加 R Device追加

                        '====================================
                        ''機器運転（DeviceOperation）の場合
                        '====================================
                        ''Bit1を参照して画面を設定する
                        If gBitCheck(.shtMask, 1) = True Then
                            'If gBitCheck(.shtMask, 6) = True Then
                            grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(3).Style.ForeColor = Color.White
                        End If

                    ElseIf .shtStatus >= (gCstCodeChDataTypeDigitalNC + &H100) Then    '' Ver1.11.8.1 2016.10.26 NC対応

                        If gBitCheck(.shtMask, 6) = True Then
                            grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(3).Style.ForeColor = Color.White
                        End If

                    Else

                        '====================================
                        ''その他モータービットの場合
                        '====================================
                        ''ビット位置に従って画面を設定する
                        If gBitCheck(.shtMask, 1) = True Then
                            grdRunningHour.Rows(i).Cells(3).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(3).Style.ForeColor = Color.White
                        End If

                        If gBitCheck(.shtMask, 2) = True Then
                            grdRunningHour.Rows(i).Cells(4).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(4).Style.ForeColor = Color.White
                        End If

                        If gBitCheck(.shtMask, 3) = True Then
                            grdRunningHour.Rows(i).Cells(5).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(5).Style.ForeColor = Color.White
                        End If

                        If gBitCheck(.shtMask, 4) = True Then
                            grdRunningHour.Rows(i).Cells(6).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(6).Style.ForeColor = Color.White
                        End If

                        If gBitCheck(.shtMask, 5) = True Then
                            grdRunningHour.Rows(i).Cells(7).Style.BackColor = Color.RoyalBlue
                            grdRunningHour.Rows(i).Cells(7).Style.ForeColor = Color.White
                        End If
                    End If


                    mintCancelFlag = 0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChRunHour, _
                               ByRef udtTarget As gTypSetChRunHour)

        Try

            With udtTarget

                For i As Integer = 0 To grdRunningHour.Rows.Count - 1
                    udtTarget.udtDetail(i) = udtSource.udtDetail(i)
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
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChRunHour, _
                                         ByVal udt2 As gTypSetChRunHour) As Boolean

        Try

            For i As Integer = 0 To grdRunningHour.Rows.Count - 1

                If udt1.udtDetail(i).shtSysno <> udt2.udtDetail(i).shtSysno Then Return False
                If udt1.udtDetail(i).shtChid <> udt2.udtDetail(i).shtChid Then Return False
                If udt1.udtDetail(i).shtTrgSysno <> udt2.udtDetail(i).shtTrgSysno Then Return False
                If udt1.udtDetail(i).shtTrgChid <> udt2.udtDetail(i).shtTrgChid Then Return False
                If udt1.udtDetail(i).shtStatus <> udt2.udtDetail(i).shtStatus Then Return False
                If udt1.udtDetail(i).shtMask <> udt2.udtDetail(i).shtMask Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mSetTrrigerChannelInfo(ByRef udtChannelInfo As gTypSetChInfo)

        For i As Integer = 0 To UBound(udtChannelInfo.udtChannel)

            With udtChannelInfo.udtChannel(i)

                ''パルス積算チャンネル
                If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                    ''データタイプが積算運転時間
                    '' Ver1.11.8.3 2016.11.08  運転積算 通信CH追加
                    'If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                    'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then
                    '' Ver1.12.0.1 2017.01.13 関数に変更
                    If gChkRunHourCH(.udtChCommon._shtChno) Then

                        ''チャンネルが設定されている場合
                        If .udtChCommon.shtChno <> 0 Then

                            ''トリガチャンネル情報初期化
                            .RevoTrigerSysno = 0
                            .RevoTrigerChid = 0

                            ''グリッド行数分ループ
                            For j As Integer = 0 To grdRunningHour.RowCount - 1

                                ''RH_CHとTrriger_CHが設定されている場合
                                If IsNumeric(grdRunningHour(0, j).Value) And _
                                   IsNumeric(grdRunningHour(1, j).Value) Then

                                    ''チャンネル番号が同じ場合
                                    If CInt(.udtChCommon.shtChno) = CInt(grdRunningHour(0, j).Value) Then
                                        ''トリガチャンネルを設定
                                        .RevoTrigerChid = CInt(grdRunningHour(1, j).Value)

                                        Exit For

                                    End If

                                End If
                            Next

                        End If

                    End If

                End If

            End With

        Next

    End Sub

    ''黄色を元に戻す
    Private Sub mReturnCellColor()

        For i = 0 To grdRunningHour.Rows.Count - 1
            If i Mod 2 <> 0 Then
                For j = 0 To 2
                    grdRunningHour.Rows(i).Cells(j).Style.BackColor = gColorGridRowBack
                Next
            Else
                For j = 0 To 2
                    grdRunningHour.Rows(i).Cells(j).Style.BackColor = Color.White
                Next
            End If
        Next

    End Sub



#End Region

End Class
