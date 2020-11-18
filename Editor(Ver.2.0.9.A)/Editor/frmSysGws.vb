Public Class frmSysGws

#Region "変数定義"

    Private mudtSetSysGwsNew As gTypSetSysGws

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
    Private Sub frmSysGws_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''コンボボックス初期設定
            Call gSetComboBox(cmbGws1Type, gEnmComboType.ctSysGws1Type)
            Call gSetComboBox(cmbGws2Type, gEnmComboType.ctSysGws2Type)

            ''コンボボックス初期設定
            Call gSetComboBox(cmbGws1File1, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws1File2, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws1File3, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws1File4, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws2File1, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws2File2, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws2File3, gEnmComboType.ctSysGwsFile)
            Call gSetComboBox(cmbGws2File4, gEnmComboType.ctSysGwsFile)

            'Ver2.0.6.4
            Call gSetComboBox(cmbEthernetLine1, gEnmComboType.ctSysSystemEthernetLine)
            Call gSetComboBox(cmbEthernetLine2, gEnmComboType.ctSysSystemEthernetLine)


            ''配列初期化
            Call mudtSetSysGwsNew.InitArray()
            For i As Integer = LBound(mudtSetSysGwsNew.udtGwsDetail) To UBound(mudtSetSysGwsNew.udtGwsDetail)
                mudtSetSysGwsNew.udtGwsDetail(i).InitArray()
            Next

            ''画面設定
            Call mSetDisplay(gudt.SetSystem.udtSysGws)

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
            Call mSetStructure(mudtSetSysGwsNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysGwsNew, gudt.SetSystem.udtSysGws) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetSysGwsNew, gudt.SetSystem.udtSysGws)

                'Ver2.0.6.4
                With gudt.SetSystem.udtSysSystem
                    'GWS1
                    .shtGWS1 = gBitSet(.shtGWS1, 0, IIf(chkGWS1.Checked, True, False))

                    'GWS1が使用設定の場合
                    If chkGWS1.Checked Then
                        'EthernetLine1
                        Select Case cmbEthernetLine1.SelectedValue
                            Case 1
                                .shtGWS1 = gBitSet(.shtGWS1, 1, True)
                                .shtGWS1 = gBitSet(.shtGWS1, 2, False)
                            Case 2
                                .shtGWS1 = gBitSet(.shtGWS1, 1, False)
                                .shtGWS1 = gBitSet(.shtGWS1, 2, True)
                        End Select
                    Else
                        'GWS1が使用設定でない場合はEthernetLineは全てなしにする
                        .shtGWS1 = gBitSet(.shtGWS1, 1, False)
                        .shtGWS1 = gBitSet(.shtGWS1, 2, False)
                    End If
                    'GWS2
                    .shtGWS2 = gBitSet(.shtGWS2, 0, IIf(chkGWS2.Checked, True, False))

                    'GWS2が使用設定の場合
                    If chkGWS2.Checked Then
                        'EthernetLine2
                        Select Case cmbEthernetLine2.SelectedValue
                            Case 1
                                .shtGWS2 = gBitSet(.shtGWS2, 1, True)
                                .shtGWS2 = gBitSet(.shtGWS2, 2, False)
                            Case 2
                                .shtGWS2 = gBitSet(.shtGWS2, 1, False)
                                .shtGWS2 = gBitSet(.shtGWS2, 2, True)
                        End Select
                    Else
                        'GWS2が使用設定でない場合はEthernetLineは全てなしにする
                        .shtGWS2 = gBitSet(.shtGWS2, 1, False)
                        .shtGWS2 = gBitSet(.shtGWS2, 2, False)
                    End If
                End With


                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

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

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysGwsNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSysGwsNew, gudt.SetSystem.udtSysGws) Then

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
                        Call mCopyStructure(mudtSetSysGwsNew, gudt.SetSystem.udtSysGws)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

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

    'Ver2.0.6.4
    'GWS1 Typeを選択すると、ともなって、cmbEthernetLine1が変わる処理
    Private Sub cmbGws1Type_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbGws1Type.SelectedIndexChanged
        Try
            Dim intRet As Integer = 0
            intRet = gDefaultEthernetLineChar(cmbGws1Type.SelectedValue)
            cmbEthernetLine1.SelectedValue = intRet
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'GWS2 Typeを選択すると、ともなって、cmbEthernetLine2が変わる処理
    Private Sub cmbGws2Type_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbGws2Type.SelectedIndexChanged
        Try
            Dim intRet As Integer = 0
            intRet = gDefaultEthernetLineChar(cmbGws2Type.SelectedValue)
            cmbEthernetLine2.SelectedValue = intRet
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "入力関連"


#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： IP KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtGws1IP1.KeyPress, txtGws1IP2.KeyPress, txtGws1IP3.KeyPress, txtGws1IP4.KeyPress, _
        txtGws2IP1.KeyPress, txtGws2IP2.KeyPress, txtGws2IP3.KeyPress, txtGws2IP4.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Backup Count KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtBackupCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtGws1Bkup1.KeyPress, txtGws1Bkup2.KeyPress, txtGws1Bkup3.KeyPress, txtGws1Bkup4.KeyPress, _
        txtGws2Bkup1.KeyPress, txtGws2Bkup2.KeyPress, txtGws2Bkup3.KeyPress, txtGws2Bkup4.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Interval KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtInterval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtGws1Interval1.KeyPress, txtGws1Interval2.KeyPress, txtGws1Interval3.KeyPress, txtGws1Interval4.KeyPress, _
        txtGws2Interval1.KeyPress, txtGws2Interval2.KeyPress, txtGws2Interval3.KeyPress, txtGws2Interval4.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            '=============
            ''IPAddress
            '=============
            ''共通数値入力チェック
            If Not mChkInputIP(txtGws1IP1, txtGws1IP2, txtGws1IP3, txtGws1IP4, "GWS1 IP") Then Return False
            If Not mChkInputIP(txtGws2IP1, txtGws2IP2, txtGws2IP3, txtGws2IP4, "GWS2 IP") Then Return False

            '=============
            ''Backup Count
            '=============
            If Not gChkInputNum(txtGws1Bkup1, 0, 24, "GWS1 File1 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws1Bkup2, 0, 24, "GWS1 File2 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws1Bkup3, 0, 24, "GWS1 File3 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws1Bkup4, 0, 24, "GWS1 File4 Backup Count", True, True) Then Return False

            If Not gChkInputNum(txtGws2Bkup1, 0, 24, "GWS2 File1 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws2Bkup2, 0, 24, "GWS2 File2 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws2Bkup3, 0, 24, "GWS2 File3 Backup Count", True, True) Then Return False
            If Not gChkInputNum(txtGws2Bkup4, 0, 24, "GWS2 File4 Backup Count", True, True) Then Return False

            '=============
            ''Interval
            '=============
            If Not gChkInputNum(txtGws1Interval1, 0, 3600, "GWS1 File1 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws1Interval2, 0, 3600, "GWS1 File2 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws1Interval3, 0, 3600, "GWS1 File3 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws1Interval4, 0, 3600, "GWS1 File4 Interval", True, True) Then Return False

            If Not gChkInputNum(txtGws2Interval1, 0, 3600, "GWS2 File1 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws2Interval2, 0, 3600, "GWS2 File2 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws2Interval3, 0, 3600, "GWS2 File3 Interval", True, True) Then Return False
            If Not gChkInputNum(txtGws2Interval4, 0, 3600, "GWS2 File4 Interval", True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mChkInputIP(ByRef txt1 As TextBox, _
                                 ByRef txt2 As TextBox, _
                                 ByRef txt3 As TextBox, _
                                 ByRef txt4 As TextBox, _
                                 ByVal strName As String) As Boolean

        Try

            ''全て入力なしの場合は入力OK
            If Trim(txt1.Text) = "" And _
               Trim(txt2.Text) = "" And _
               Trim(txt3.Text) = "" And _
               Trim(txt4.Text) = "" Then

                Return True

            Else

                ''共通数値入力チェック
                If Not gChkInputNum(txt1, 0, 255, strName & "1", False, True) Then Return False
                If Not gChkInputNum(txt2, 0, 255, strName & "2", False, True) Then Return False
                If Not gChkInputNum(txt3, 0, 255, strName & "3", False, True) Then Return False
                If Not gChkInputNum(txt4, 0, 255, strName & "4", False, True) Then Return False

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) FCU設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSysGws)

        Try

            '' GWS1
            With udtSet.udtGwsDetail(0)

                .shtGwsType = cmbGws1Type.SelectedValue         '' GWS Type
                .bytIP1 = CCbyte(txtGws1IP1.Text)               '' IPアドレス
                .bytIP2 = CCbyte(txtGws1IP2.Text)               '' IPアドレス
                .bytIP3 = CCbyte(txtGws1IP3.Text)               '' IPアドレス
                .bytIP4 = CCbyte(txtGws1IP4.Text)               '' IPアドレス

                'File1
                With .udtGwsFileInfo(0)
                    .bytType = cmbGws1File1.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws1AllCH1.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws1Counter1.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws1Flicker1.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws1Sensor1.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws1Bkup1.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws1Interval1.Text)                                  '' 更新間隔
                End With

                'File2
                With .udtGwsFileInfo(1)
                    .bytType = cmbGws1File2.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws1AllCH2.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws1Counter2.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws1Flicker2.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws1Sensor2.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws1Bkup2.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws1Interval2.Text)                                  '' 更新間隔
                End With

                'File3
                With .udtGwsFileInfo(2)
                    .bytType = cmbGws1File3.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws1AllCH3.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws1Counter3.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws1Flicker3.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws1Sensor3.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws1Bkup3.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws1Interval3.Text)                                  '' 更新間隔
                End With

                'File4
                With .udtGwsFileInfo(3)
                    .bytType = cmbGws1File4.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws1AllCH4.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws1Counter4.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws1Flicker4.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws1Sensor4.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws1Bkup4.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws1Interval4.Text)                                  '' 更新間隔
                End With

            End With

            '' GWS2
            With udtSet.udtGwsDetail(1)

                .shtGwsType = cmbGws2Type.SelectedValue         '' GWS Type
                .bytIP1 = CCbyte(txtGws2IP1.Text)               '' IPアドレス
                .bytIP2 = CCbyte(txtGws2IP2.Text)               '' IPアドレス
                .bytIP3 = CCbyte(txtGws2IP3.Text)               '' IPアドレス
                .bytIP4 = CCbyte(txtGws2IP4.Text)               '' IPアドレス

                'File1
                With .udtGwsFileInfo(0)
                    .bytType = cmbGws2File1.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws2AllCH1.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws2Counter1.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws2Flicker1.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws2Sensor1.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws2Bkup1.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws2Interval1.Text)                                  '' 更新間隔
                End With

                'File2
                With .udtGwsFileInfo(1)
                    .bytType = cmbGws2File2.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws2AllCH2.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws2Counter2.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws2Flicker2.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws2Sensor2.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws2Bkup2.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws2Interval2.Text)                                  '' 更新間隔
                End With

                'File3
                With .udtGwsFileInfo(2)
                    .bytType = cmbGws2File3.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws2AllCH3.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws2Counter3.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws2Flicker3.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws2Sensor3.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws2Bkup3.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws2Interval3.Text)                                  '' 更新間隔
                End With

                'File4
                With .udtGwsFileInfo(3)
                    .bytType = cmbGws2File4.SelectedValue                                           '' ファイル種類
                    .bytSetFlg = gBitSet(.bytSetFlg, 0, IIf(chkGws2AllCH4.Checked, True, False))    '' 全CH印字
                    .bytSetFlg = gBitSet(.bytSetFlg, 1, IIf(chkGws2Counter4.Checked, True, False))  '' バイナリカウンタ
                    .bytSetFlg = gBitSet(.bytSetFlg, 2, IIf(chkGws2Flicker4.Checked, True, False))  '' フリッカ有無
                    .bytSetFlg = gBitSet(.bytSetFlg, 3, IIf(chkGws2Sensor4.Checked, True, False))   '' センサ'S'出力
                    .bytBkupCnt = CCbyte(txtGws2Bkup4.Text)                                         '' バックアップ有無
                    .shtInterval = CCUInt16(txtGws2Interval4.Text)                                  '' 更新間隔
                End With

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) FCU設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSysGws)

        Try
            ''GWS1
            With udtSet.udtGwsDetail(0)
                cmbGws1Type.SelectedValue = .shtGwsType     ''GWS Type
                txtGws1IP1.Text = .bytIP1                   ''IPアドレス
                txtGws1IP2.Text = .bytIP2                   ''IPアドレス
                txtGws1IP3.Text = .bytIP3                   ''IPアドレス
                txtGws1IP4.Text = .bytIP4                   ''IPアドレス

                'File1
                With .udtGwsFileInfo(0)
                    cmbGws1File1.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws1AllCH1.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws1Counter1.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws1Flicker1.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws1Sensor1.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws1Bkup1.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws1Interval1.Text = .shtInterval                                  '' 更新間隔
                End With


                'File2
                With .udtGwsFileInfo(1)
                    cmbGws1File2.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws1AllCH2.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws1Counter2.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws1Flicker2.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws1Sensor2.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws1Bkup2.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws1Interval2.Text = .shtInterval                                  '' 更新間隔
                End With


                'File3
                With .udtGwsFileInfo(2)
                    cmbGws1File3.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws1AllCH3.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws1Counter3.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws1Flicker3.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws1Sensor3.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws1Bkup3.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws1Interval3.Text = .shtInterval                                  '' 更新間隔
                End With


                'File4
                With .udtGwsFileInfo(3)
                    cmbGws1File4.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws1AllCH4.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws1Counter4.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws1Flicker4.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws1Sensor4.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws1Bkup4.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws1Interval4.Text = .shtInterval                                  '' 更新間隔
                End With


            End With

            ''GWS2
            With udtSet.udtGwsDetail(1)
                cmbGws2Type.SelectedValue = .shtGwsType     ''GWS Type
                txtGws2IP1.Text = .bytIP1                   ''IPアドレス
                txtGws2IP2.Text = .bytIP2                   ''IPアドレス
                txtGws2IP3.Text = .bytIP3                   ''IPアドレス
                txtGws2IP4.Text = .bytIP4                   ''IPアドレス

                'File1
                With .udtGwsFileInfo(0)
                    cmbGws2File1.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws2AllCH1.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws2Counter1.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws2Flicker1.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws2Sensor1.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws2Bkup1.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws2Interval1.Text = .shtInterval                                  '' 更新間隔
                End With


                'File2
                With .udtGwsFileInfo(1)
                    cmbGws2File2.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws2AllCH2.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws2Counter2.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws2Flicker2.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws2Sensor2.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws2Bkup2.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws2Interval2.Text = .shtInterval                                  '' 更新間隔
                End With


                'File3
                With .udtGwsFileInfo(2)
                    cmbGws2File3.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws2AllCH3.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws2Counter3.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws2Flicker3.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws2Sensor3.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws2Bkup3.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws2Interval3.Text = .shtInterval                                  '' 更新間隔
                End With


                'File4
                With .udtGwsFileInfo(3)
                    cmbGws2File4.SelectedValue = .bytType                                 '' ファイル種類
                    chkGws2AllCH4.Checked = IIf(gBitCheck(.bytSetFlg, 0), True, False)    '' 全CH印字
                    chkGws2Counter4.Checked = IIf(gBitCheck(.bytSetFlg, 1), True, False)  '' バイナリカウンタ
                    chkGws2Flicker4.Checked = IIf(gBitCheck(.bytSetFlg, 2), True, False)  '' フリッカ有無
                    chkGws2Sensor4.Checked = IIf(gBitCheck(.bytSetFlg, 3), True, False)   '' センサ'S'出力
                    txtGws2Bkup4.Text = .bytBkupCnt                                       '' バックアップ有無
                    txtGws2Interval4.Text = .shtInterval                                  '' 更新間隔
                End With


            End With

            'Ver2.0.6.4 SystemSetから移行分
            'GWS Typeの変更で、EthernetLineが変わってしまうため、GWS Typeの後
            With gudt.SetSystem.udtSysSystem
                'GWS1
                chkGWS1.Checked = IIf(gBitCheck(.shtGWS1, 0), True, False)
                If gBitCheck(.shtGWS1, 1) Then
                    cmbEthernetLine1.SelectedValue = 1
                ElseIf gBitCheck(.shtGWS1, 2) Then
                    cmbEthernetLine1.SelectedValue = 2
                Else
                    cmbEthernetLine1.SelectedValue = 0
                End If
                'GWS2
                chkGWS2.Checked = IIf(gBitCheck(.shtGWS2, 0), True, False)
                If gBitCheck(.shtGWS2, 1) Then
                    cmbEthernetLine2.SelectedValue = 1
                ElseIf gBitCheck(.shtGWS2, 2) Then
                    cmbEthernetLine2.SelectedValue = 2
                Else
                    cmbEthernetLine2.SelectedValue = 0
                End If
            End With


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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSysGws, _
                               ByRef udtTarget As gTypSetSysGws)

        Try

            With udtTarget

                For i As Integer = LBound(udtTarget.udtGwsDetail) To UBound(udtTarget.udtGwsDetail)
                    udtTarget.udtGwsDetail(i) = udtSource.udtGwsDetail(i)
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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSysGws, _
                                         ByVal udt2 As gTypSetSysGws) As Boolean

        Try
            'Ver2.0.6.4 SystemSetから移行分
            With gudt.SetSystem.udtSysSystem
                'GWS1
                If chkGWS1.Checked <> IIf(gBitCheck(.shtGWS1, 0), True, False) Then
                    Return False
                End If
                If gBitCheck(.shtGWS1, 1) Then
                    If cmbEthernetLine1.SelectedValue <> 1 Then
                        Return False
                    End If
                ElseIf gBitCheck(.shtGWS1, 2) Then
                    If cmbEthernetLine1.SelectedValue <> 2 Then
                        Return False
                    End If
                Else
                    If cmbEthernetLine1.SelectedValue <> 0 Then
                        Return False
                    End If
                End If
                'GWS2
                If chkGWS2.Checked <> IIf(gBitCheck(.shtGWS2, 0), True, False) Then
                    Return False
                End If
                If gBitCheck(.shtGWS2, 1) Then
                    If cmbEthernetLine2.SelectedValue <> 1 Then
                        Return False
                    End If
                ElseIf gBitCheck(.shtGWS2, 2) Then
                    If cmbEthernetLine2.SelectedValue <> 2 Then
                        Return False
                    End If
                Else
                    If cmbEthernetLine2.SelectedValue <> 0 Then
                        Return False
                    End If
                End If
            End With



            For i As Integer = LBound(udt1.udtGwsDetail) To UBound(udt1.udtGwsDetail)

                If udt1.udtGwsDetail(i).shtGwsType <> udt2.udtGwsDetail(i).shtGwsType Then Return False
                If udt1.udtGwsDetail(i).bytIP1 <> udt2.udtGwsDetail(i).bytIP1 Then Return False
                If udt1.udtGwsDetail(i).bytIP2 <> udt2.udtGwsDetail(i).bytIP2 Then Return False
                If udt1.udtGwsDetail(i).bytIP3 <> udt2.udtGwsDetail(i).bytIP3 Then Return False
                If udt1.udtGwsDetail(i).bytIP4 <> udt2.udtGwsDetail(i).bytIP4 Then Return False

                For j As Integer = LBound(udt1.udtGwsDetail(i).udtGwsFileInfo) To UBound(udt1.udtGwsDetail(i).udtGwsFileInfo)
                    If udt1.udtGwsDetail(i).udtGwsFileInfo(j).bytType <> udt2.udtGwsDetail(i).udtGwsFileInfo(j).bytType Then Return False
                    If udt1.udtGwsDetail(i).udtGwsFileInfo(j).bytSetFlg <> udt2.udtGwsDetail(i).udtGwsFileInfo(j).bytSetFlg Then Return False
                    If udt1.udtGwsDetail(i).udtGwsFileInfo(j).bytBkupCnt <> udt2.udtGwsDetail(i).udtGwsFileInfo(j).bytBkupCnt Then Return False
                    If udt1.udtGwsDetail(i).udtGwsFileInfo(j).shtInterval <> udt2.udtGwsDetail(i).udtGwsFileInfo(j).shtInterval Then Return False
                Next
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
