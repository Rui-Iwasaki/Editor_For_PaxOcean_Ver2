﻿Imports System.IO
Imports System.Reflection

Public Class frmSysSystem

#Region "構造体定義"
    '' 2016.01.21 Ver1.9.3 追加
    Public Structure OptionSet
        Dim shtSystem As UShort     ''ｼｽﾃﾑ動作ﾌﾗｸ
        Dim shtBS_CHNo As UShort    ''BS CH出力 CHNo.
        Dim shtFS_CHNo As UShort    ''FS CH出力 CHNo.
    End Structure

#End Region

#Region "変数定義"

    Private mudtSetSysSystemNew As gTypSetSysSystem

    '' 2016.01.21 Ver1.9.3 追加
    Private mudtOptionNew As OptionSet

    'Ver2.0.6.4 FCU Set画面と統合
    Private mudtSetSysFcuNew As gTypSetSysFcu
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
    Private Sub frmSysSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '■外販
            '外販の場合、BS/FS,Combine,FCUのFCU Extend Board以外を非表示
            If gintNaiGai = 1 Then
                fraBS_FS.Visible = False
                chkCombine.Visible = False
                fraCombine.Visible = False
                'FCU
                Label5.Visible = False
                CmbFCUCnt.Visible = False
                Label8.Visible = False
                cmbFCUNo.Visible = False
                GroupBox1.Visible = False
                chkFCU.Visible = False
                chkShareChUse.Visible = False
                chkPtJPt.Visible = False
                Label7.Visible = False
                numCorrectTime.Visible = False
                Label6.Visible = False

                'Ver2.0.7.M 保安庁
                grpHoan.Visible = False
                grpMode.Visible = False

                'Ver2.0.7.V
                chkDataSW.Visible = False
            End If


            'Call gDebugBit()
            'stc22kSysemMenu = stc22kSysemMenu.GetInstance()

            '' 構造体の値から画面コンポーネントの設定を変更
            'Me.msetStuructValueToComponets()

            Call gSetComboBox(cmbSystemClock, gEnmComboType.ctSysSystemSystemClock)
            Call gSetComboBox(cmbDataFormat, gEnmComboType.ctSysSystemDataFormat)
            Call gSetComboBox(cmbLanguage, gEnmComboType.ctSysSystemLanguage)
            Call gSetComboBox(cmbManual, gEnmComboType.ctSysSystemManual)       '' 2015.02.05
            Call gSetComboBox(cmbCombine, gEnmComboType.ctSysSystemCombine)
            Call gSetComboBox(cmbEthernetLine1, gEnmComboType.ctSysSystemEthernetLine)
            Call gSetComboBox(cmbEthernetLine2, gEnmComboType.ctSysSystemEthernetLine)

            Call gSetComboBox(cmbGL_SPEC, gEnmComboType.ctSysSystemGL_Spec)       '' 2015.05.27


            'Ver2.0.6.4 FCU Set画面と統合
            'コンボボックス初期設定
            Call gSetComboBox(CmbFCUCnt, gEnmComboType.ctSysFcuFcuCount)
            Call gSetComboBox(cmbFCUNo, gEnmComboType.ctSysFcuFcuCount)
            Call gSetComboBox(cmbPart, gEnmComboType.ctSysFcuPartSet)


            ''配列初期化
            Call mudtSetSysSystemNew.InitArray()

            'Ver2.0.6.4 FCU Set画面と統合
            Call mudtSetSysFcuNew.InitArray()


            ''画面設定
            Call mSetDisplay(gudt.SetSystem.udtSysSystem)
            'Ver2.0.6.4 FCU Set画面と統合
            Call mSetDisplay_FCU(gudt.SetSystem.udtSysFcu)


            ''コントロール使用可/不可設定
            Call mSetControlEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： FCU 台数チェンジ
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub cmbFCUCnt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbFCUCnt.SelectedIndexChanged

        If IsNumeric(CmbFCUCnt.Text) Then

            'Ver1.11.8.2 2016.11.01  FCU2台仕様の場合はﾊﾟｰﾄ分け設定を使用可とする
            If CInt(CmbFCUCnt.Text) >= 2 Then
                cmbPart.Enabled = True
                'Ver2.0.7.W DataSWフラグは2台仕様のみ編集可
                chkDataSW.Enabled = True
            Else
                cmbPart.Enabled = False
                'Ver2.0.7.W DataSWフラグは2台仕様のみ編集可
                chkDataSW.Enabled = False
                'Ver2.0.8.C DataSWフラグをコンバインと共用（自動OFFはコメント)
                'chkDataSW.Checked = False
            End If
        End If

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
            Call mSetStructure(mudtSetSysSystemNew)
            'Ver2.0.6.4 FCU Set画面と統合
            Call mSetStructure_FCU(mudtSetSysFcuNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetSystem.udtSysSystem, mudtSetSysSystemNew) Then
                
                ''変更された場合は設定を更新する
                gudt.SetSystem.udtSysSystem = mudtSetSysSystemNew
                Call SaveSys()         '' Ver1.9.3  2016.01.21
                'Ver2.0.6.4 FCU Set画面と統合
                gudt.SetSystem.udtSysFcu = mudtSetSysFcuNew
                'FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                If cmbPart.SelectedIndex = 1 Then      '' 今回はMach/Hull
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, True)
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, False)
                ElseIf cmbPart.SelectedIndex = 2 Then   'Stbd/Port hori
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, True)
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                Else                                   '' 今回はMach/Cargo
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, False)
                End If

                'Ver1.12.0.8 ﾌﾟﾘﾝﾀﾊﾟｰﾄ設定
                With gudt.SetSystem.udtSysSystem
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, fnSetCombinePrinter())

                    'Ver2.0.6.4 言語にともなって、PrinterTypeも更新
                    Select Case .shtLanguage
                        Case 0
                            '英語
                            gudt.SetSystem.udtSysPrinter.shtPrintType = 1
                        Case 1, 2   '和文仕様 20200220 hori
                            '日本語
                            gudt.SetSystem.udtSysPrinter.shtPrintType = 3
                        Case Else
                            gudt.SetSystem.udtSysPrinter.shtPrintType = 0
                    End Select

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

    'Ver2.0.6.4 FCU Set画面と統合
    '--------------------------------------------------------------------
    ' 機能      : 収集周期キープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 数値のみ入力可能とする
    '--------------------------------------------------------------------
    Private Sub txtCorrectTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try

            If gKeyPress(e.KeyChar) = True Then

                e.Handled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Combineチェッククリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コントロール使用可/不可設定を行う
    '--------------------------------------------------------------------
    Private Sub chkCombine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCombine.CheckedChanged

        Try

            ''コントロール使用可/不可設定
            Call mSetControlEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : GWS1チェッククリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コントロール使用可/不可設定を行う
    '--------------------------------------------------------------------
    Private Sub chkGWS1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGWS1.CheckedChanged

        Try

            ''コントロール使用可/不可設定
            Call mSetControlEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : GWS2チェッククリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コントロール使用可/不可設定を行う
    '--------------------------------------------------------------------
    Private Sub chkGWS2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGWS2.CheckedChanged

        Try

            ''コントロール使用可/不可設定
            Call mSetControlEnable()

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

            'Call mRenameAllFile("c:\editorsetting\test001", "test555")

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
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysSystemNew)
            'Ver2.0.6.4 FCU Set画面と統合
            Call mSetStructure_FCU(mudtSetSysFcuNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetSystem.udtSysSystem, mudtSetSysSystemNew) Then

                
                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?" & vbNewLine _
                                            , Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        gudt.SetSystem.udtSysSystem = mudtSetSysSystemNew
                        Call SaveSys()         '' Ver1.9.3  2016.01.21
                        'Ver2.0.6.4 FCU Set画面と統合
                        gudt.SetSystem.udtSysFcu = mudtSetSysFcuNew
                        'FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                        If cmbPart.SelectedIndex = 2 Then   'Stbd/Port追加 hori
                            gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, True)
                            gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                        Else
                            If cmbPart.SelectedIndex = 1 Then      '' 今回はMach/Hull
                                gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, True)
                                gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, False)
                            Else
                                gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 1, False)
                                gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 4, False)
                            End If
                        End If

                        'Ver1.12.0.8 ﾌﾟﾘﾝﾀﾊﾟｰﾄ設定
                        With gudt.SetSystem.udtSysSystem
                            .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, fnSetCombinePrinter())

                            'Ver2.0.6.4 言語にともなって、PrinterTypeも更新
                            Select Case .shtLanguage
                                Case 0
                                    '英語
                                    gudt.SetSystem.udtSysPrinter.shtPrintType = 1
                                Case 1
                                    '日本語
                                    gudt.SetSystem.udtSysPrinter.shtPrintType = 3
                                Case Else
                                    gudt.SetSystem.udtSysPrinter.shtPrintType = 0
                            End Select
                        End With

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
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtShipName KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtShipName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShipName.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '保安ﾓｰﾄﾞ
    Private Sub chkHOAN_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkHOAN.CheckedChanged
        Try
            Dim oldType As Byte

            'Ver2.0.7.M (保安庁)保安庁ﾌﾗｸﾞ
            oldType = g_bytHOAN
            If chkHOAN.Checked = True Then
                g_bytHOAN = 1
            Else
                g_bytHOAN = 0
            End If

            '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ(そうしなければ保存されない)
            If oldType <> g_bytHOAN Then
                'Verup処理中ならば何もしない
                If gudtFileInfo.blnVersionUP Then
                    'Debug.Print("Verup")
                Else
                    gblnUpdateAll = True
                End If
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '新デザインﾓｰﾄﾞ
    Private Sub chkNEWDES_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkNEWDES.CheckedChanged
        Try
            Dim oldType As Byte

            'Ver2.0.7.M (新デザイン)新デザインﾌﾗｸﾞ
            oldType = g_bytNEWDES
            If chkNEWDES.Checked = True Then
                g_bytNEWDES = 1
            Else
                g_bytNEWDES = 0
            End If

            '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ(そうしなければ保存されない)
            If oldType <> g_bytNEWDES Then
                'Verup処理中ならば何もしない
                If gudtFileInfo.blnVersionUP Then
                    'Debug.Print("Verup")
                Else
                    gblnUpdateAll = True
                End If
            End If
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

        Try

            ''共通テキスト入力チェック
            If Not gChkInputText(txtShipName, "ShipName", True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSysSystem)

        Try

            Dim blnGws1 As Boolean = False
            Dim blnGws2 As Boolean = False

            With udtSet

                ''システムクロック
                .shtClock = cmbSystemClock.SelectedValue

                ''日付フォーマット
                .shtDate = cmbDataFormat.SelectedValue

                ''言語
                .shtLanguage = cmbLanguage.SelectedValue


                ''取扱説明書(言語)     2015.02.05
                .shtManual = cmbManual.SelectedValue

                ''GL船級仕様     2015/5/27 T.Ueki
                .shtgl_spec = cmbGL_SPEC.SelectedValue

                ''船名
                .strShipName = txtShipName.Text

                '' Ver1.9.3 2016.01.21 追加
                '' ﾋｽﾄﾘ 自動更新
                'Ver2.0.4.1 Systemフラグは元を格納して、あらためてﾋｽﾄﾘﾌﾗｸﾞをON,OFFさす
                'mudtOptionNew.shtSystem = 0
                'mudtOptionNew.shtSystem = mudtOptionNew.shtSystem + IIf(chkHistoryAuto.Checked, 1, 0)
                mudtOptionNew.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 0, chkHistoryAuto.Checked)
                '' BS OUT CHNo.
                mudtOptionNew.shtBS_CHNo = CCUInt16(txtBSCHNo.Text)
                '' FS OUT CHNo.
                mudtOptionNew.shtFS_CHNo = CCUInt16(txtFSCHNo.Text)
                ''//

                ''コンバイン設定
                If Not chkCombine.Checked Then
                    .shtCombineUse = gCstCodeSysCombineNone
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 0, False)
                    'Ver2.0.7.H
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, False)
                Else
                    .shtCombineUse = cmbCombine.SelectedValue
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 0, IIf(chkSeparate.Checked, True, False))
                    'Ver2.0.7.H
                    .shtCombineSeparate = gBitSet(.shtCombineSeparate, 1, fnSetCombinePrinter())
                End If

                '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''システムステータス設定
                '.shtStatus = gBitSet(.shtStatus, 0, IIf(chkSystemStatusNone.Checked, False, True))
                '.shtStatus = gBitSet(.shtStatus, 1, IIf(chkSystemStatusFCU.Checked, True, False))
                '.shtStatus = gBitSet(.shtStatus, 2, IIf(chkSystemStatusOPS.Checked, True, False))
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                'Ver2.0.6.4 GWS1とGWS2はここでは操作しない
                'Ver2.0.7.H GWS1とGWS2は、そのまま値を格納するだけ
                .shtGWS1 = gudt.SetSystem.udtSysSystem.shtGWS1
                .shtGWS2 = gudt.SetSystem.udtSysSystem.shtGWS2


                ''GWS1
                '.shtGWS1 = gBitSet(.shtGWS1, 0, IIf(chkGWS1.Checked, True, False))

                ''GWS1が使用設定の場合
                If chkGWS1.Checked Then

                    ''EthernetLine1
                    Select Case cmbEthernetLine1.SelectedValue
                        Case 1
                            '.shtGWS1 = gBitSet(.shtGWS1, 1, True)
                            '.shtGWS1 = gBitSet(.shtGWS1, 2, False)
                        Case 2
                            '.shtGWS1 = gBitSet(.shtGWS1, 1, False)
                            '.shtGWS1 = gBitSet(.shtGWS1, 2, True)
                    End Select

                Else

                    ''GWS1が使用設定でない場合はEthernetLineは全てなしにする
                    '.shtGWS1 = gBitSet(.shtGWS1, 1, False)
                    '.shtGWS1 = gBitSet(.shtGWS1, 2, False)

                End If

                ''GWS2
                '.shtGWS2 = gBitSet(.shtGWS2, 0, IIf(chkGWS2.Checked, True, False))

                ''GWS2が使用設定の場合
                If chkGWS2.Checked Then

                    ''EthernetLine2
                    Select Case cmbEthernetLine2.SelectedValue
                        Case 1
                            '.shtGWS2 = gBitSet(.shtGWS2, 1, True)
                            '.shtGWS2 = gBitSet(.shtGWS2, 2, False)
                        Case 2
                            '.shtGWS2 = gBitSet(.shtGWS2, 1, False)
                            '.shtGWS2 = gBitSet(.shtGWS2, 2, True)
                    End Select

                Else

                    ''GWS2が使用設定でない場合はEthernetLineは全てなしにする
                    '.shtGWS2 = gBitSet(.shtGWS2, 1, False)
                    '.shtGWS2 = gBitSet(.shtGWS2, 2, False)

                End If


                'Ver2.0.7.H 保安庁対応
                If optOverView.Checked = True Then
                    txtHoanGno.Text = 0
                Else
                    txtHoanGno.Text = 201
                End If
                .shthoan_gno = CCShort(txtHoanGno.Text)


            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'Ver2.0.6.4 FCU Set画面と統合
    Private Sub mSetStructure_FCU(ByRef udtSet As gTypSetSysFcu)
        Try
            With udtSet
                'FCU台数
                .shtFcuCnt = CmbFCUCnt.SelectedValue

                'FCU No.
                .shtFcuNo = cmbFCUNo.SelectedValue

                '共有CH使用有無
                .shtShareChUse = IIf(chkShareChUse.Checked, 1, 0)

                '収集周期
                .shtCrrectTime = numCorrectTime.Value

                'FCU拡張ボード
                .shtFcuExtendBord = IIf(chkSIO.Checked, 1, 0)

                '通信用拡張FCU  Ver1.9.3 2016.01.21 追加
                .shtFCUOption = 0
                .shtFCUOption = .shtFCUOption + IIf(chkFCU.Checked, 1, 0)

                'Ver2.0.3.6
                'PT,JPT
                .shtPtJptFlg = 0
                .shtPtJptFlg = .shtPtJptFlg + IIf(chkPtJPt.Checked, 1, 0)

                'Ver2.0.7.V
                .shtFCU2Flg = 0
                .shtFCU2Flg = gBitSet(.shtFCU2Flg, 0, IIf(chkDataSW.Checked, True, False))

            End With

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
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSysSystem)

        Try

            With udtSet

                ''システムクロック
                cmbSystemClock.SelectedValue = .shtClock

                ''日付フォーマット
                cmbDataFormat.SelectedValue = .shtDate

                ''言語
                cmbLanguage.SelectedValue = .shtLanguage

                ''取扱説明書(言語)     2015.02.05
                cmbManual.SelectedValue = .shtManual

                ''GL船級仕様     2015/5/27 T.Ueki
                cmbGL_SPEC.SelectedValue = .shtgl_spec

                ''船名
                txtShipName.Text = .strShipName

                '' Ver1.9.3 2016.01.21 追加
                '' ﾋｽﾄﾘ 自動更新
                chkHistoryAuto.Checked = IIf(gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 0), True, False)
                '' BS OUT CHNo.
                txtBSCHNo.Text = gudt.SetSystem.udtSysOps.shtBS_CHNo.ToString
                '' FS OUT CHNo.
                txtFSCHNo.Text = gudt.SetSystem.udtSysOps.shtFS_CHNo.ToString
                ''//

                ''コンバイン設定
                Select Case .shtCombineUse
                    Case gCstCodeSysCombineNone
                        chkCombine.Checked = False
                        cmbCombine.SelectedIndex = 0
                        chkSeparate.Checked = IIf(gBitCheck(.shtCombineSeparate, 0), True, False)
                    Case gCstCodeSysCombineMC
                        chkCombine.Checked = True
                        cmbCombine.SelectedValue = .shtCombineUse
                        chkSeparate.Checked = IIf(gBitCheck(.shtCombineSeparate, 0), True, False)
                    Case gCstCodeSysCombineMH
                        chkCombine.Checked = True
                        cmbCombine.SelectedValue = .shtCombineUse
                        chkSeparate.Checked = IIf(gBitCheck(.shtCombineSeparate, 0), True, False)
                End Select

                'Ver2.0.8.C DataSWフラグをコンバインと共用
                chkDataSW2.Checked = IIf(gBitCheck(gudt.SetSystem.udtSysFcu.shtFCU2Flg, 0), True, False)


                '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''システムステータス設定
                'chkSystemStatusNone.Checked = IIf(gBitCheck(.shtStatus, 0), False, True)
                'chkSystemStatusFCU.Checked = IIf(gBitCheck(.shtStatus, 1), True, False)
                'chkSystemStatusOPS.Checked = IIf(gBitCheck(.shtStatus, 2), True, False)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''GWS1使用有無
                chkGWS1.Checked = IIf(gBitCheck(.shtGWS1, 0), True, False)

                ''GWS1EthernetLine
                If gBitCheck(.shtGWS1, 1) Then
                    cmbEthernetLine1.SelectedValue = 1
                ElseIf gBitCheck(.shtGWS1, 2) Then
                    cmbEthernetLine1.SelectedValue = 2
                Else
                    cmbEthernetLine1.SelectedValue = 0
                End If

                ''GWS2使用有無
                chkGWS2.Checked = IIf(gBitCheck(.shtGWS2, 0), True, False)

                ''GWS2EthernetLine
                If gBitCheck(.shtGWS2, 1) Then
                    cmbEthernetLine2.SelectedValue = 1
                ElseIf gBitCheck(.shtGWS2, 2) Then
                    cmbEthernetLine2.SelectedValue = 2
                Else
                    cmbEthernetLine2.SelectedValue = 0
                End If

                'Ver2.0.7.H 保安庁
                If .shthoan_gno = 0 Then
                    optOverView.Checked = True
                Else
                    optMimic.Checked = True
                End If
                txtHoanGno.Text = .shthoan_gno


                'Ver2.0.7.M 保安庁
                If g_bytHOAN = 0 Then
                    chkHOAN.Checked = False
                Else
                    chkHOAN.Checked = True
                End If

                'Ver2.0.7.M 新デザイン
                If g_bytNEWDES = 0 Then
                    chkNEWDES.Checked = False
                Else
                    chkNEWDES.Checked = True
                End If
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'Ver2.0.6.4 FCU Set画面と統合
    Private Sub mSetDisplay_FCU(ByVal udtSet As gTypSetSysFcu)
        Try

            With udtSet
                'FCU台数
                CmbFCUCnt.SelectedValue = .shtFcuCnt

                'FCU No.
                cmbFCUNo.SelectedValue = .shtFcuNo

                'Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
                If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 4) Then    'Stbd/Port hori
                    cmbPart.SelectedIndex = 2
                ElseIf gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 1) Then    '' Mach/Hull
                    cmbPart.SelectedIndex = 1
                Else        'Mach/Cargo
                    cmbPart.SelectedIndex = 0
                End If

                '共有CH使用有無
                chkShareChUse.Checked = IIf(.shtShareChUse = 1, True, False)

                '収集周期
                numCorrectTime.Value = .shtCrrectTime

                'SIO通信
                chkSIO.Checked = IIf(.shtFcuExtendBord = 1, True, False)

                '通信用拡張FCU  Ver1.9.3 2016.01.21 追加
                chkFCU.Checked = IIf(gBitCheck(.shtFCUOption, 0), True, False)

                'Ver2.0.3.6 PT,JPT
                chkPtJPt.Checked = IIf(gBitCheck(.shtPtJptFlg, 0), True, False)

                'Ver2.0.7.V
                chkDataSW.Checked = IIf(gBitCheck(.shtFCU2Flg, 0), True, False)
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 各チェックボックの状態から関連フレームの使用可/不可を設定する
    '--------------------------------------------------------------------
    Private Sub mSetControlEnable()

        Try

            ''コンバイン
            fraCombine.Enabled = chkCombine.Checked

            ''GWS1
            cmbEthernetLine1.Enabled = chkGWS1.Checked

            ''GWS2
            cmbEthernetLine2.Enabled = chkGWS2.Checked

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSysSystem, _
                               ByRef udtTarget As gTypSetSysSystem)

        Try

            udtTarget.shtClock = udtSource.shtClock
            udtTarget.shtCombineSeparate = udtSource.shtCombineSeparate
            udtTarget.shtCombineUse = udtSource.shtCombineUse
            udtTarget.shtDate = udtSource.shtDate
            udtTarget.shtGWS1 = udtSource.shtGWS1
            udtTarget.shtGWS2 = udtSource.shtGWS2
            udtTarget.shtLanguage = udtSource.shtLanguage
            udtTarget.shtManual = udtSource.shtManual   ''取扱説明書(言語)     2015.02.05

            udtTarget.shtgl_spec = udtSource.shtgl_spec   ''GL船級仕様     2015/5/27 T.Ueki

            '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'udtTarget.shtStatus = udtSource.shtStatus
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            udtTarget.strShipName = udtSource.strShipName

            'Ver2.0.7.H 保安庁
            udtTarget.shthoan_gno = udtSource.shthoan_gno

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSysSystem, _
                                         ByVal udt2 As gTypSetSysSystem) As Boolean

        Try

            If udt1.shtClock <> udt2.shtClock Then Return False
            If udt1.shtCombineSeparate <> udt2.shtCombineSeparate Then Return False
            If udt1.shtCombineUse <> udt2.shtCombineUse Then Return False
            If udt1.shtDate <> udt2.shtDate Then Return False
            If udt1.shtGWS1 <> udt2.shtGWS1 Then Return False
            If udt1.shtGWS2 <> udt2.shtGWS2 Then Return False
            If udt1.shtLanguage <> udt2.shtLanguage Then Return False
            If udt1.shtManual <> udt2.shtManual Then Return False ''取扱説明書(言語)     2015.02.05

            If udt1.shtgl_spec <> udt2.shtgl_spec Then Return False ''GL船級仕様　2015/5/27 T.Ueki

            If udt1.shthoan_gno <> udt2.shthoan_gno Then Return False 'Ver2.0.7.H 保安庁

            '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'If udt1.shtStatus <> udt2.shtStatus Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            If Not gCompareString(udt1.strShipName, udt2.strShipName) Then Return False
            'If udt1.strSpare <> udt2.strSpare Then Return False

            '’Ver1.9.3 2016.01.21 変更
            '' ｼｽﾃﾑ設定
            If gudt.SetSystem.udtSysOps.shtSystem <> mudtOptionNew.shtSystem Then Return False
            '' BS OUT CHNo.
            If gudt.SetSystem.udtSysOps.shtBS_CHNo <> mudtOptionNew.shtBS_CHNo Then Return False
            '' FS OUT CHNo.
            If gudt.SetSystem.udtSysOps.shtFS_CHNo <> mudtOptionNew.shtFS_CHNo Then Return False
            ''//

            'Ver2.0.6.4 FCU Set画面と統合
            If mChkStructureEquals_FCU(mudtSetSysFcuNew, gudt.SetSystem.udtSysFcu) = False Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    'Ver2.0.6.4 FCU Set画面と統合
    Private Function mChkStructureEquals_FCU(ByVal udt1 As gTypSetSysFcu, _
                                         ByVal udt2 As gTypSetSysFcu) As Boolean
        Try
            If udt1.shtCrrectTime <> udt2.shtCrrectTime Then Return False
            If udt1.shtFcuCnt <> udt2.shtFcuCnt Then Return False
            If udt1.shtFcuNo <> udt2.shtFcuNo Then Return False
            If udt1.shtShareChUse <> udt2.shtShareChUse Then Return False
            If udt1.shtFcuExtendBord <> udt2.shtFcuExtendBord Then Return False
            If udt1.shtFCUOption <> udt2.shtFCUOption Then Return False '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
            If udt1.shtPtJptFlg <> udt2.shtPtJptFlg Then Return False 'Ver2.0.3.6 PT,JPT
            If udt1.shtFCU2Flg <> udt2.shtFCU2Flg Then Return False 'Ver2.0.7.V FCU_FLG

            'Ver1.11.8.2 2016.11.01  FCU 2台仕様追加  表示ﾊﾟｰﾄ名称設定追加
            If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 4) Then    'Stbd/Portが設定済　Stbd/Port追加 hori
                If cmbPart.SelectedIndex <> 2 Then          '' 比較時、設定が変わっていた場合
                    Return False
                End If
            Else
                If gBitCheck(gudt.SetSystem.udtSysOps.shtSystem, 1) Then    '' Mach/Hullが設定済み
                    If cmbPart.SelectedIndex <> 1 Then      '' 比較時、設定が変わっていた場合
                        Return False
                    End If
                Else        '' Mach/Cargoが設定済み
                    If cmbPart.SelectedIndex <> 0 Then      '' 比較時、設定が変わっていた場合
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 保存処理
    '               Ver1.9.3  2016.01.16
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    ' 備考　　  : 
    '--------------------------------------------------------------------
    Public Sub SaveSys()

        gudt.SetSystem.udtSysOps.shtSystem = mudtOptionNew.shtSystem        '' 

        gudt.SetSystem.udtSysOps.shtBS_CHNo = mudtOptionNew.shtBS_CHNo      '' BS OUT CHNo.

        gudt.SetSystem.udtSysOps.shtFS_CHNo = mudtOptionNew.shtFS_CHNo      '' FS OUT CHNo.

    End Sub




#End Region

    Private Sub chkDataSW_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDataSW.CheckedChanged

        'ver2.0.8.C DataSWフラグをFCU2台仕様、コンバイン仕様と連動
        If chkDataSW.Checked = True Then ' FCU2台仕様用表示
            chkDataSW2.Checked = True
        Else
            chkDataSW2.Checked = False
        End If

    End Sub

    Private Sub chkDataSW2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDataSW2.CheckedChanged

        'ver2.0.8.C DataSWフラグをFCU2台仕様、コンバイン仕様と連動
        If chkDataSW2.Checked = True Then ' コンバイン仕様用表示
            chkDataSW.Checked = True
        Else
            chkDataSW.Checked = False
        End If


    End Sub

End Class
