Public Class frmChTerminalDetail

#Region "定数定義"

    ''出力CH
    Private Const mCstCodeChOutPut As Integer = 9

    ''Function Set
    Private Const mCstCodeFunction As Integer = 10

#End Region

#Region "変数定義"

    ''画面引数格納
    Private Structure mTerminalInfo
        Public FCU As String        ''FCU_1 or FCU_2
        Public FuNo As Integer      ''FU番号：0～20
        Public FuName As String     ''FU名称：FCU OR A～T
        Public FcuFuName As String  ''FCU/FU名称
        Public TypeCode As Integer  ''スロット種別コード
        Public Type As String       ''スロット種別名称
        Public PortNo As String     ''ポート番号：1～8
        Public TerInfo As Short     ''端子台設定 Ver.2.0.8.P

        Public FirstFuNo As Integer     ''設定可能な最初の端子台のFU番号
        Public FirstPortNo As Integer   ''設定可能な最初の端子台のポート番号
        Public EndFuNo As Integer       ''設定可能な最後の端子台のFU番号
        Public EndPortNo As Integer     ''設定可能な最後の端子台のポート番号
    End Structure
    Private mTerminalData As mTerminalInfo

    ''初期のCH Noを退避しておく領域
    Private mintChNoBk(63) As Integer       ''連続するCHは先頭のみを格納
    Private mintChNoBk_2(63) As Integer     ''表示のまま格納

    ''クリアしたCH No.を一時退避しておく
    Private mIntClearChNo As Integer

    ''モーターのステータス情報格納
    Private mMotorStatus1() As String
    Private mMotorStatus2() As String
    Private mMotorBitPos1() As String
    Private mMotorBitPos2() As String

    ''延長警報盤用チェンジフラグ
    Private mintChange As Integer

    ''Saveフラグ
    Private mintSaveFlag As Integer

    ''イベントキャンセルフラグ
    Private mintEventCancelFlag As Integer

    ''Next スロット ボタン　クリックフラグ
    Private mintNextFlag As Integer = 0

    ''Before スロット ボタン　クリックフラグ
    Private mintBeforeFlag As Integer = 0

    ''チャンネル情報 ==================================================
    Private mudtSetChDispNew As gTypSetChDisp
    ''=================================================================

    ''出力チャンネル情報格納 ==========================================
    Public Structure mDoInfo
        Public No As Integer
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID 又は 論理出力 ID
        Public Type As String       ''CHデータ、論理出力チャネルデータ
        Public Status As String     ''Output Movement
        Public Mask As Integer      ''Output Movement マスクデータ（ビットパターン）
        Public Output As String     ''CH OUT Type Setup
        Public Funo As String       ''FU 番号
        Public Portno As String     ''FU ポート番号
        Public Pin As String        ''FU 計測点番号
        Public Core1 As String      ''CoreNoIn
        Public Core2 As String      ''CoreNoCom
        Public CableMark1 As String ''WIRE MARK
        Public CableMark2 As String ''WIRE MARK(CLASS)
        Public CableDest As String  ''DEST
        Public TerminalNo As String ''Terminal No
    End Structure
    Public mDoDetail As mDoInfo

    ''論理出力情報格納(24チャンネル分)
    Public Structure mOrAndInfo
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID
        Public Status As String     ''ステータス種類
        Public Mask As Integer      ''マスクデータ
    End Structure
    Public mOrAndDetail(23) As mOrAndInfo

    Private mudtSetCHOutPut As gTypSetChOutput      ''出力チャンネル情報
    Private mudtSetCHAndOr As gTypSetChAndOr        ''論理出力情報

    Private mudtSetCHOutPutNew As gTypSetChOutput
    Private mudtSetCHAndOrNew As gTypSetChAndOr
    ''=================================================================

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:SAVE  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) FCU_1 or FCU_2
    '           : ARG2 - (I ) FU番号：0～20
    '           : ARG3 - (I ) FU名称：FCU OR A～T
    '           : ARG4 - (I ) FCU/FU名称
    '           : ARG5 - (I ) スロット種別コード
    '           : ARG6 - (I ) スロット種別名称
    '           : ARG7 - (I ) ポート番号：1～8
    '           : ARG8 - (I ) 設定可能な最初の端子台のFU番号
    '           : ARG9 - (I ) 設定可能な最初の端子台のポート番号
    '           : ARG10 - (I ) 設定可能な最後の端子台のFU番号
    '           : ARG11 - (I ) 設定可能な最後の端子台のポート番号
    '　　　　　 : ARG12 - (IO) 1:次のスロットを続けて開く  2:前のスロットを続けて開く
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal hFCU As String, ByVal hFuNo As String, _
                          ByVal hFuName As String, ByVal hFcuFuName As String, _
                          ByVal hTypeCode As Integer, ByVal hType As String, ByVal hPortNo As String, _
                          ByVal hFirstFuNo As Integer, ByVal hFirstPortNo As Integer, _
                          ByVal hEndFuNo As Integer, ByVal hEndPortNo As Integer, _
                          ByRef hMode As Integer, ByVal hTerInfo As Short, _
                          ByRef frmOwner As Form) As Integer

        Try

            With mTerminalData
                .FCU = hFCU
                .FuNo = hFuNo
                .FuName = hFuName
                .FcuFuName = hFcuFuName
                .TypeCode = hTypeCode
                .Type = hType
                .PortNo = hPortNo
                .TerInfo = hTerInfo 'Ver.2.0.8.P
                .FirstFuNo = hFirstFuNo
                .FirstPortNo = hFirstPortNo
                .EndFuNo = hEndFuNo
                .EndPortNo = hEndPortNo
            End With

            ''================================================
            Call gShowFormModelessForCloseWait2(Me, frmOwner)
            ''================================================

            hMode = 0
            If mintNextFlag = 1 Then
                hMode = 1       ''Next スロット
            ElseIf mintBeforeFlag = 1 Then
                hMode = 2       ''Before スロット
            End If

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
    Private Sub frmChTerminalDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim blnDoSelect As Boolean = False
        Dim TypeCode As Integer = mTerminalData.TerInfo

        Try
            'Ver2.0.1.5 画面キャプションにファイル名追加
            Me.Text = "[" & gudtFileInfo.strFileVersion & "] " & Me.Text

            '' Ver1.9.3 2016.01.16 ｳｨﾝﾄﾞｳｻｲｽﾞを保持するように変更
            Me.Height = m_FuDetailWndH
            Me.Width = m_FuDetailWndW

            'Ver2.0.1.6 印刷で表示しているのFUNoを表記
            lblFUNo.Text = mTerminalData.FuNo.ToString

            'Ver.2.0.8.P
            If mTerminalData.TypeCode = 341 Then
                blnDoSelect = True
                mTerminalData.TypeCode = 1
            End If

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''コンボボックス初期化
            Call gSetComboBox(cmbChOutType, gEnmComboType.ctChOutputDoChOutType)
            Call gSetComboBox(cmbOutputMovement, gEnmComboType.ctChOutputDoStatus)

            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then      ''DI
                Call gSetComboBox(cmbFunction, gEnmComboType.ctChTerminalFunctionFuncDI)
            ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then  ''DO
                Call gSetComboBox(cmbFunction, gEnmComboType.ctChTerminalFunctionFuncDO)
            End If

            ''モーターチャンネルのステータス情報を獲得する
            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

            ''ヘッダ部分表示
            With mTerminalData

                LblFieldSt.Text = .FCU & "    " & .FuName
                lblFCU.Text = .FcuFuName
                lblType1.Text = .Type
                lblType2.Text = .PortNo

            End With

            'Ver.2.0.8.P
            If blnDoSelect = True Then
                Dim iSetIndex(4) As Integer

                'DO端子種類選択コンボボックス初期化
                RemoveHandler cmbDoTerm_a.SelectedIndexChanged, AddressOf cmbDoTerm_a_SelectedIndexChanged
                Call gSetComboBox(cmbDoTerm_a, gEnmComboType.ctChOutputDoTermType1)
                iSetIndex(0) = mGetFuDoTermSet(TypeCode, 0)
                AddHandler cmbDoTerm_a.SelectedIndexChanged, AddressOf cmbDoTerm_a_SelectedIndexChanged
                Call gSetComboBox(cmbDoTerm_b, gEnmComboType.ctChOutputDoTermType2)
                iSetIndex(1) = mGetFuDoTermSet(TypeCode, 1)
                RemoveHandler cmbDoTerm_c.SelectedIndexChanged, AddressOf cmbDoTerm_c_SelectedIndexChanged
                Call gSetComboBox(cmbDoTerm_c, gEnmComboType.ctChOutputDoTermType1)
                iSetIndex(2) = mGetFuDoTermSet(TypeCode, 2)
                AddHandler cmbDoTerm_c.SelectedIndexChanged, AddressOf cmbDoTerm_c_SelectedIndexChanged
                Call gSetComboBox(cmbDoTerm_d, gEnmComboType.ctChOutputDoTermType2)
                iSetIndex(3) = mGetFuDoTermSet(TypeCode, 3)

                cmbDoTerm_a.Enabled = True
                cmbDoTerm_b.Enabled = True
                cmbDoTerm_c.Enabled = True
                cmbDoTerm_d.Enabled = True

                cmbDoTerm_a.SelectedIndex = iSetIndex(0)
                cmbDoTerm_b.SelectedIndex = iSetIndex(1)
                cmbDoTerm_c.SelectedIndex = iSetIndex(2)
                cmbDoTerm_d.SelectedIndex = iSetIndex(3)

                '変更値保存変数に、現在の設定をコピー
                frmChTerminalList.mSetDoTermSettingForTemp(mTerminalData.FuNo, mTerminalData.PortNo - 1)

            Else
                cmbDoTerm_a.Enabled = False
                cmbDoTerm_b.Enabled = False
                cmbDoTerm_c.Enabled = False
                cmbDoTerm_d.Enabled = False
            End If

            ''配列再定義(CH)
            mudtSetChDispNew.InitArray()
            For i As Integer = LBound(mudtSetChDispNew.udtChDisp) To UBound(mudtSetChDispNew.udtChDisp)
                mudtSetChDispNew.udtChDisp(i).InitArray()
                For j As Integer = LBound(mudtSetChDispNew.udtChDisp(i).udtSlotInfo) To UBound(mudtSetChDispNew.udtChDisp(i).udtSlotInfo)
                    mudtSetChDispNew.udtChDisp(i).udtSlotInfo(j).InitArray()
                Next
            Next

            ''配列再定義(CH OutPut)
            mudtSetCHOutPut.InitArray()

            mudtSetCHAndOr.InitArray()
            For i = LBound(mudtSetCHAndOr.udtCHOut) To UBound(mudtSetCHAndOr.udtCHOut)
                mudtSetCHAndOr.udtCHOut(i).InitArray()
            Next

            mudtSetCHOutPutNew.InitArray()

            mudtSetCHAndOrNew.InitArray()
            For i = LBound(mudtSetCHAndOrNew.udtCHOut) To UBound(mudtSetCHAndOrNew.udtCHOut)
                mudtSetCHAndOrNew.udtCHOut(i).InitArray()
            Next

            ''構造体複製(CH OutPut) ---------------------------------------------------------------------------
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPut)
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPutNew)

            Call mCopyStructure2(gudt.SetChAndOr, mudtSetCHAndOr)
            Call mCopyStructure2(gudt.SetChAndOr, mudtSetCHAndOrNew)

            ''画面設定 ----------------------------------------------------------------------------------------
            Call mSetDisplayCh(gudt.SetChInfo)                                           ''チャンネル設定構造体
            Call mSetDisplayDisp(gudt.SetChDisp)                                         ''チャンネル情報データ構造体
            Call mSetDisplayChOutPut(gudt.SetChInfo, gudt.SetChOutput, gudt.SetChAndOr)  ''出力チャンネル設定構造体
            ''-------------------------------------------------------------------------------------------------

            mIntClearChNo = Val(grdTerminal(2, 0).Value)

            ''DO なら出力CH 設定可
            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                cmdOutput.Enabled = True
            Else
                cmdOutput.Enabled = False
            End If

            ''DI, DO なら FunctionSet 設定可
            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Or mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                cmdRtbSet.Enabled = True
            Else
                cmdRtbSet.Enabled = False
            End If

            ''Before/Nextボタン
            With mTerminalData

                cmdBefore.Enabled = True
                cmdNext.Enabled = True

                If .FuNo = .FirstFuNo And .PortNo = .FirstPortNo Then
                    cmdBefore.Enabled = False
                End If

                If .FuNo = .EndFuNo And .PortNo = .EndPortNo Then
                    cmdNext.Enabled = False
                End If

            End With

            mintSaveFlag = 0
            mintChange = 0
            'mintChangeDisp = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChTerminalDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            grdTerminal.CurrentCell = Nothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChTerminalDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

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

            Dim blnIsSave As Boolean = False
            Dim blnFuDoTermSetChg As Boolean = False 'Ver.2.0.8.P

            ''入力チェック
            If Not mChkInput() Then Return

            ''チャンネル情報データ構造体(Disp) ---------------------------

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChDispNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChDisp, mudtSetChDispNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructureDisp(mudtSetChDispNew, gudt.SetChDisp)

                blnIsSave = True
            End If

            ''チャンネル設定構造体(CH) ------------------------------------

            If mIsChangeCH() Or mintChange = 1 Then   ''CHが変更されているか？

                Call mSaveCH()

                blnIsSave = True

            End If

            ''出力チャンネル設定構造体(CH OutPut, CHAndOr) ---------------

            ''データが変更されているかチェック
            If Not mChkStructureEqualsChOutPut(mudtSetCHOutPut, mudtSetCHOutPutNew, mudtSetCHAndOr, mudtSetCHAndOrNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)
                Call mCopyStructure2(mudtSetCHAndOrNew, gudt.SetChAndOr)

                Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)
                Call mCopyStructure2(mudtSetCHAndOrNew, mudtSetCHAndOr)

                blnIsSave = True

            End If

            'Ver.2.0.8.P DO端子アレンジ設定 ---------------
            '設定の保存
            mCalcDoTermSetValue()

            '設定が変更されているか確認
            If mChkFuDoTerminal(mTerminalData.FuNo, mTerminalData.PortNo - 1) = True Then
                blnIsSave = True
                blnFuDoTermSetChg = True
                mSetFuDoTerminal()
            End If

            '-------------------------------------------------------------
            If blnIsSave Then

                ''↓　2011-6-9 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                mintEventCancelFlag = 1
                For rw As Integer = 0 To grdTerminal.RowCount - 1
                    For col As Integer = 0 To grdTerminal.ColumnCount - 3

                        ''グリッド クリア
                        If col <> 10 And col <> 12 Then
                            grdTerminal(col, rw).Value = Nothing
                        End If

                    Next
                Next

                ''画面設定 再表示
                Call mSetDisplayCh(gudt.SetChInfo)                                          ''チャンネル設定構造体
                Call mSetDisplayDisp(gudt.SetChDisp)                                        ''チャンネル情報データ構造体
                Call mSetDisplayChOutPut(gudt.SetChInfo, gudt.SetChOutput, gudt.SetChAndOr) ''出力チャンネル設定構造体

                mIntClearChNo = Val(grdTerminal(2, 0).Value)
                mintEventCancelFlag = 0
                ''↑　2011-6-9 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                gudt.SetEditorUpdateInfo.udtSave.bytOutPut = 1
                gudt.SetEditorUpdateInfo.udtSave.bytOrAnd = 1

                gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytOutPut = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytOrAnd = 1

                'Ver.2.0.8.P FU端子設定が変更されている場合、更新フラグをON
                If blnFuDoTermSetChg = True Then
                    gudt.SetEditorUpdateInfo.udtSave.bytFuChannel = 1
                    gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytFuChannel = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1
                End If

            End If

            mintChange = 0      ''延長警報盤用チェンジフラグ
            mintSaveFlag = 1    ''Saveフラグ

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Clearクリック
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        Try
            'Ver2.0.2.8 ﾒｯｾｰｼﾞを出す
            If MessageBox.Show("Do you Clear the sequence details?", _
                                           Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Call ClearCHORhSetting()
            End If

            'Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : CH OR設定ｸﾘｱ
    '--------------------------------------------------------------------
    Private Sub ClearCHORhSetting()

        For i As Integer = 0 To UBound(mudtSetCHAndOrNew.udtCHOut)

            For j As Integer = 0 To UBound(mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr)
                With mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j)
                    .shtSysno = 0       ''SYSTEM No.
                    .shtChid = 0        ''CH ID
                    .bytSpare = 0       ''予備
                    .bytStatus = 0      ''ステータス種類
                    .shtMask = 0        ''マスクデータ
                End With
            Next

        Next
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmChTerminalDetail_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim blFuDoSettingChange As Boolean = False
        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChDispNew)


            'Ver.2.0.8.P DO端子アレンジ設定 ---------------
            '設定の保存
            mCalcDoTermSetValue()

            '設定が変更されているか確認
            If mChkFuDoTerminal(mTerminalData.FuNo, mTerminalData.PortNo - 1) = True Then
                blFuDoSettingChange = True
            End If

            ''データが変更されているかチェック
            If (Not mChkStructureEquals(gudt.SetChDisp, mudtSetChDispNew)) Or _
                mIsChangeCH() Or _
                Not mChkStructureEqualsChOutPut(mudtSetCHOutPut, mudtSetCHOutPutNew, mudtSetCHAndOr, mudtSetCHAndOrNew) Or blFuDoSettingChange = True Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''チャンネル情報データ構造体(Disp) ---------------------
                        Call mCopyStructureDisp(mudtSetChDispNew, gudt.SetChDisp)

                        ''チャンネル設定構造体(CH) -----------------------------
                        Call mSaveCH()

                        ''出力チャンネル設定構造体(CH OutPut, CHAndOr) ---------
                        Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)
                        Call mCopyStructure2(mudtSetCHAndOrNew, gudt.SetChAndOr)

                        Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)
                        Call mCopyStructure2(mudtSetCHAndOrNew, mudtSetCHAndOr)

                        'Ver.2.0.8.P 
                        If blFuDoSettingChange = True Then
                            mSetFuDoTerminal()
                        End If

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1      '' ver1.4.0 2011.08.22
                        gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1     '' ver1.4.0 2011.08.22
                        gudt.SetEditorUpdateInfo.udtSave.bytOutPut = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytOrAnd = 1

                        gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1   '' ver1.4.0 2011.08.22
                        gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1  '' ver1.4.0 2011.08.22
                        gudt.SetEditorUpdateInfo.udtCompile.bytOutPut = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytOrAnd = 1

                        'Ver.2.0.8.P FU端子設定が変更されている場合、更新フラグをON
                        If blFuDoSettingChange = True Then
                            gudt.SetEditorUpdateInfo.udtSave.bytFuChannel = 1
                            gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytFuChannel = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1
                        End If

                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True
                        mintBeforeFlag = 0 : mintNextFlag = 0

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Beforeボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 次スロットへ移る
    '--------------------------------------------------------------------
    Private Sub cmdBefore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBefore.Click

        Try

            ''フラグ ON
            mintBeforeFlag = 1

            ''一旦閉じる
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Nextボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 前スロットへ移る
    '--------------------------------------------------------------------
    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click

        Try

            ''フラグ ON
            mintNextFlag = 1

            ''一旦閉じる
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdTerminal_ColumnDividerDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs) Handles grdTerminal.ColumnDividerDoubleClick

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTerminal.EditingControlShowing

        Try

            Dim strColumnName As String

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdTerminal_KeyPress

                ''該当する列ならイベントハンドラを追加する
                strColumnName = dgv.CurrentCell.OwningColumn.Name

                If strColumnName.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdTerminal_KeyPress

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdTerminal.KeyPress

        Try


            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub

            If grdTerminal.CurrentCell.ReadOnly Then Exit Sub

            Dim strColumnName As String = grdTerminal.CurrentCell.OwningColumn.Name

            Select Case strColumnName

                Case "txtChNo"
                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(5, dgv, e.KeyChar, True)

                Case "txtCoreNo1", "txtCoreNo2"
                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(4, dgv, e.KeyChar, False)

                Case "txtCableMark1", "txtCableMark2", "txtCableDest"
                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    '' 入力文字制限変更 24→20   2013.11.20 
                    'e.Handled = gCheckTextInput(24, dgv, e.KeyChar, False)
                    e.Handled = gCheckTextInput(20, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： F1ｷｰ入力ﾁｪｯｸ
    '               F1ｷｰが入力されたら↓を入力します
    '               2015.10.27 Ver1.7.5 追加
    ' 引数      ： 
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles grdTerminal.KeyDown

        Dim strColumnName As String = grdTerminal.CurrentCell.OwningColumn.Name

        Select Case strColumnName
            Case "txtCableMark1", "txtCableMark2", "txtCoreNo1", "txtCoreNo2", "txtCableDest"
                If e.KeyData = Keys.F1 Then
                    grdTerminal.CurrentCell.Value = "↓"
                End If
            Case "txtChNo"
                'Ver2.0.3.1
                'CHnoで、3線式の場合、↑↓Enterは該当行へ飛ぶ
                Dim intHosei As Integer = 0
                If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                    With grdTerminal
                        Select Case e.KeyData
                            Case Keys.Up
                                '現在行が3以下なら常に１
                                If .CurrentCell.RowIndex > 3 Then
                                    Select Case .CurrentCell.RowIndex Mod 3
                                        Case 0
                                            intHosei = 2
                                        Case 1
                                            intHosei = 3
                                        Case 2
                                            intHosei = 4
                                    End Select
                                    .CurrentCell = grdTerminal(2, .CurrentCell.RowIndex - intHosei)
                                Else
                                    .CurrentCell = grdTerminal(2, 1)
                                End If
                            Case Keys.Down
                                '現在行がMAX-3以上なら何もしない
                                If .CurrentCell.RowIndex < .Rows.Count - 3 Then
                                    Select Case .CurrentCell.RowIndex Mod 3
                                        Case 0
                                            intHosei = 2
                                        Case 1
                                            intHosei = 1
                                        Case 2
                                            intHosei = 0
                                    End Select
                                    .CurrentCell = grdTerminal(2, .CurrentCell.RowIndex + intHosei)
                                End If
                            Case Keys.Enter
                                '現在行がMAX-3以上なら何もしない
                                If .CurrentCell.RowIndex < .Rows.Count - 3 Then
                                    Select Case .CurrentCell.RowIndex Mod 3
                                        Case 0
                                            intHosei = 2
                                        Case 1
                                            intHosei = 1
                                        Case 2
                                            intHosei = 0
                                    End Select
                                    .CurrentCell = grdTerminal(2, .CurrentCell.RowIndex + intHosei)
                                End If
                        End Select
                    End With
                End If
        End Select

        'Ver2.0.3.1
        'Tabキーの場合、入力可能列へ飛ぶ
        If e.KeyData = Keys.Tab Then
            e.Handled = True
            '最終列なら次の行へ飛ぶ
            If grdTerminal.CurrentCell.ColumnIndex >= 16 Then
                grdTerminal.CurrentCell = grdTerminal(2, grdTerminal.CurrentCell.RowIndex + 1)
            Else
                If grdTerminal.CurrentCell.ReadOnly = False Then
                    grdTerminal.CurrentCell = grdTerminal(grdTerminal.CurrentCell.ColumnIndex + 1, grdTerminal.CurrentCell.RowIndex)
                End If
            End If

            While grdTerminal.CurrentCell.ReadOnly = True
                grdTerminal.CurrentCell = grdTerminal(grdTerminal.CurrentCell.ColumnIndex + 1, grdTerminal.CurrentCell.RowIndex)
            End While
        End If
        If e.KeyData = (Keys.Tab Or Keys.Shift) Then
            e.Handled = True
            '1列なら前の行へ飛ぶ
            If grdTerminal.CurrentCell.ColumnIndex <= 2 Then
                grdTerminal.CurrentCell = grdTerminal(16, grdTerminal.CurrentCell.RowIndex - 1)
                grdTerminal.FirstDisplayedScrollingColumnIndex = 16
            Else
                If grdTerminal.CurrentCell.ReadOnly = False Then
                    grdTerminal.CurrentCell = grdTerminal(grdTerminal.CurrentCell.ColumnIndex - 1, grdTerminal.CurrentCell.RowIndex)
                End If
            End If

            While grdTerminal.CurrentCell.ReadOnly = True
                grdTerminal.CurrentCell = grdTerminal(grdTerminal.CurrentCell.ColumnIndex - 1, grdTerminal.CurrentCell.RowIndex)
            End While
        End If

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 変更前のCHを保存する
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdTerminal_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTerminal.CellEnter

        Try
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''クリアされる前のCHを保存
            mIntClearChNo = Val(grdTerminal(2, e.RowIndex).Value)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック(CH)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdTerminal.CellValidating

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strValue As String
            Dim strMsg As String = ""

            ''クリアされる前のCHを保存
            'mIntClearChNo = Val(grdTerminal(2, e.RowIndex).Value)

            strValue = grdTerminal(e.ColumnIndex, e.RowIndex).EditedFormattedValue
            If IsNumeric(strValue) Then

                Select Case e.ColumnIndex

                    Case 2                                  ''CH No.
                        If Integer.Parse(strValue) > 65535 Then
                            MsgBox("Please set CH No. '1'-'65535'.", MsgBoxStyle.Exclamation, "Channel")
                            e.Cancel = True : Exit Sub
                        End If

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ★CHNo 入力時, クリア時 処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTerminal.CellValueChanged

        Try

            Dim strChNo As String
            Dim intOrAndIndex As Integer = 0
            Dim intWkIndex As Integer = 0
            Dim intAns As Integer = 0
            Static intFlag As Integer   ''イベントの再読み込みを防ぐフラグ

            If mintEventCancelFlag = 1 Then Exit Sub

            If intFlag = 1 Then intFlag = 0 : Exit Sub

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub


            Dim dgv As DataGridView = CType(sender, DataGridView)

            ''行選択されている場合も処理を行う  ver.1.4.0 2011.09.29
            ''行選択でクリアした場合にCH名称他の情報が表示されたままとなるため
            'If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Then
            If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Or dgv.SelectedRows.Count <> 0 Then

                If IsNumeric(grdTerminal.Rows(e.RowIndex).Cells(2).Value) Then

                    If grdTerminal.Rows(e.RowIndex).Cells(2).Value.ToString.Length > 4 Then
                        grdTerminal.Rows(e.RowIndex).Cells(2).Value = Integer.Parse(grdTerminal.Rows(e.RowIndex).Cells(2).Value).ToString("00000")

                    Else
                        intFlag = 1
                        grdTerminal.Rows(e.RowIndex).Cells(2).Value = Integer.Parse(grdTerminal.Rows(e.RowIndex).Cells(2).Value).ToString("0000")
                    End If

                    strChNo = grdTerminal.Rows(e.RowIndex).Cells(2).Value   ''CH No.

                    If strChNo <> "" Then

                        'Ver2.0.1.5 重複は認めるように変更
                        'Ver2.0.2.4 重複があった場合、もう一つを消す
                        ''CH Noの重複チェック
                        For i As Integer = 0 To grdTerminal.RowCount - 1

                            If i <> e.RowIndex Then     ''自身は除く

                                If grdTerminal(2, i).Value = strChNo And _
                                   grdTerminal(0, i).Value <> mCstCodeChOutPut And _
                                   grdTerminal(0, i).Value <> mCstCodeFunction Then

                                    'MsgBox("The same CHNo exists on the same circuit board.", MsgBoxStyle.Exclamation, "TERMINAL ORDER INPUT")
                                    'intFlag = 1
                                    grdTerminal.Rows(i).Cells(2).Value = ""
                                    'Exit Sub
                                End If
                            End If
                        Next i

                        ''<< チャンネル情報をサーチし表示する >> ---------------------------------------------------
                        intAns = mSetChDetail(Integer.Parse(grdTerminal.Rows(e.RowIndex).Cells(2).Value), e.RowIndex)

                        If intAns = 1 Then
                            ''スロット種別が合わなかった
                            MsgBox("This CHNo can't be set up on this circuit board.", MsgBoxStyle.Exclamation, "TERMINAL ORDER INPUT")

                            If mIntClearChNo > 0 Then
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = mIntClearChNo.ToString("0000") ''表示を元のCHに戻す
                            Else
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = ""
                            End If

                            Exit Sub

                        ElseIf intAns = -1 Then
                            ''該当するチャンネルがなかった
                            MsgBox("This CHNo doesn't exist.", MsgBoxStyle.Exclamation, "TERMINAL ORDER INPUT")

                            If mIntClearChNo > 0 Then
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = mIntClearChNo.ToString("0000") ''表示を元のCHに戻す
                            Else
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = ""
                            End If

                            Exit Sub
                        ElseIf intAns = 2 Then
                            'Ver2.0.2.4
                            'オーバーする
                            MsgBox("This CHNo can't ADD over PIN MAX count.", MsgBoxStyle.Exclamation, "TERMINAL ORDER INPUT")

                            If mIntClearChNo > 0 Then
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = mIntClearChNo.ToString("0000") ''表示を元のCHに戻す
                            Else
                                intFlag = 1
                                grdTerminal.Rows(e.RowIndex).Cells(2).Value = ""
                            End If

                            Exit Sub
                        End If
                        ''------------------------------------------------------------------------------------------
                        intFlag = 0 '' 2014.09.18
                    End If

                Else

                    ''<< CH No.が消されたので一覧をクリアする >>
                    If grdTerminal(0, e.RowIndex).Value <> Nothing Then

                        If grdTerminal(0, e.RowIndex).Value = gCstCodeChTypeMotor Then
                            ''モーターチャンネルの場合、最大で5個連続している分も消す
                            For i As Integer = 1 To 4
                                If e.RowIndex + i <= grdTerminal.RowCount - 1 Then
                                    If Val(grdTerminal(0, e.RowIndex + i).Value) = gCstCodeChTypeMotor And Val(grdTerminal(2, e.RowIndex + i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex + i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex + i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If

                                If e.RowIndex - i >= 0 Then
                                    If Val(grdTerminal(0, e.RowIndex - i).Value) = gCstCodeChTypeMotor And Val(grdTerminal(2, e.RowIndex - i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex - i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex - i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If
                            Next i

                        ElseIf grdTerminal(0, e.RowIndex).Value = gCstCodeChTypeValve Then
                            ''バルブチャンネルの場合、最大で8個連続している分も消す
                            For i As Integer = 1 To 7
                                If e.RowIndex + i <= grdTerminal.RowCount - 1 Then
                                    If Val(grdTerminal(0, e.RowIndex + i).Value) = gCstCodeChTypeValve And Val(grdTerminal(2, e.RowIndex + i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex + i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex + i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If

                                If e.RowIndex - i >= 0 Then
                                    If Val(grdTerminal(0, e.RowIndex - i).Value) = gCstCodeChTypeValve And Val(grdTerminal(2, e.RowIndex - i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex - i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex - i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If
                            Next i

                        ElseIf grdTerminal(0, e.RowIndex).Value = gCstCodeChTypeComposite Then
                            ''コンポジットチャンネルの場合、最大で8個連続している分も消す
                            For i As Integer = 1 To 7
                                If e.RowIndex + i <= grdTerminal.RowCount - 1 Then
                                    If Val(grdTerminal(0, e.RowIndex + i).Value) = gCstCodeChTypeComposite And Val(grdTerminal(2, e.RowIndex + i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex + i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex + i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If

                                If e.RowIndex - i >= 0 Then
                                    If Val(grdTerminal(0, e.RowIndex - i).Value) = gCstCodeChTypeComposite And Val(grdTerminal(2, e.RowIndex - i).Value) = mIntClearChNo Then
                                        ''一覧クリア
                                        For j As Integer = 0 To 19
                                            If j <> 10 And j <> 12 Then
                                                intFlag = 1
                                                grdTerminal(j, e.RowIndex - i).Value = Nothing
                                                If j = 18 Then grdTerminal(j, e.RowIndex - i).Value = -1
                                            End If
                                        Next
                                    End If
                                End If
                            Next i

                        ElseIf grdTerminal(0, e.RowIndex).Value = mCstCodeChOutPut Then

                            ''出力チャンネルの場合
                            With mudtSetCHOutPutNew.udtCHOutPut(grdTerminal(18, e.RowIndex).Value)

                                ''↓2010-3-30　論理出力データ構造体の上詰を止めたのでスライドさせる必要がなくなった

                                ' ''論理出力データの場合、削除したデータから後半のデータを上にスライドさせる
                                'If .bytType <> gCstCodeFuOutputChTypeCh Then

                                '    ''論理出力構造体のインデックスをGETする
                                '    intOrAndIndex = mGetIndexOrAnd(.bytType, grdTerminal(18, e.RowIndex).Value, mudtSetCHOutPutNew)

                                '    ''ANDの場合は33レコード目から
                                '    'If .bytType = 2 Then intOrAndIndex += gCstCntFuAndOrRecCntOr

                                '    'intWkIndex = IIf(.bytType = gCstCodeFuOutputChTypeOr, gCstCntFuAndOrRecCntOr - 1, gCstCntFuAndOrRecCntOr + gCstCntFuAndOrRecCntAnd - 1)
                                '    intWkIndex = gCstCntFuAndOrRecCnt - 1

                                '    ''上にずらす
                                '    For i = intOrAndIndex + 1 To intWkIndex
                                '        mudtSetCHAndOrNew.udtCHOut(i - 1) = mudtSetCHAndOrNew.udtCHOut(i)
                                '    Next

                                '    For i = 0 To 23
                                '        With mudtSetCHAndOrNew.udtCHOut(intWkIndex).udtCHAndOr(i)
                                '            .shtSysno = 0
                                '            .shtChid = 0
                                '            .bytStatus = 0
                                '            .shtMask = 0
                                '        End With
                                '    Next i

                                '    ''------------------------------------------------------------------------
                                '    For i = 0 To UBound(mudtSetCHOutPutNew.udtCHOutPut)
                                '        If mudtSetCHOutPutNew.udtCHOutPut(i).bytType <> 0 Then
                                '            If mudtSetCHOutPutNew.udtCHOutPut(i).shtChid >= intOrAndIndex + 1 Then
                                '                mudtSetCHOutPutNew.udtCHOutPut(i).shtChid -= 1
                                '            End If
                                '        End If
                                '    Next i

                                'End If

                                .shtSysno = 0
                                .shtChid = 0
                                .bytType = 0
                                .bytStatus = 0
                                .shtMask = 0
                                .bytOutput = 0
                                .bytFuno = gCstCodeChNotSetFuNoByte
                                .bytPortno = gCstCodeChNotSetFuPortByte
                                .bytPin = gCstCodeChNotSetFuPinByte

                            End With

                            intFlag = 1
                            grdTerminal(15, e.RowIndex).Value = -1

                        End If

                        ''1CHに2, 3行を占有している場合は、行数分消す   2011-6-9
                        Dim intRow As Integer = 0

                        ''スロット種別により行単位が異なる
                        '' 3線式のみ3行表示　ver1.4.0 2011.08.22
                        If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                            intRow = 3  ''3線式(3行毎)
                        Else
                            intRow = 1  ''他(1行毎)
                        End If

                        For rw As Integer = 0 To intRow - 1

                            For i As Integer = 0 To 19
                                If i <> 10 And i <> 12 Then
                                    intFlag = 1
                                    grdTerminal(i, e.RowIndex + rw).Value = Nothing
                                    If i = 18 Then grdTerminal(i, e.RowIndex + rw).Value = -1
                                End If
                            Next

                        Next

                        ''一覧クリア(先頭行)
                        'For i As Integer = 0 To 19
                        '    If i <> 10 And i <> 12 Then
                        '        intFlag = 1
                        '        grdTerminal(i, e.RowIndex).Value = Nothing
                        '        If i = 18 Then grdTerminal(i, e.RowIndex).Value = -1
                        '    End If
                        'Next

                    End If
                    intFlag = 0

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdTerminal_DoubleClick(sender As Object, e As System.EventArgs) Handles grdTerminal.DoubleClick
        'Ver2.0.3.6 ダブルクリックでOutDetailへ
        Call OutputSetting()
    End Sub



    'Ver2.0.0.2 基板指定印刷プレビュー
    '----------------------------------------------------------------------------
    ' 機能説明  ： Print Preview ボタンクリック →　基板指定印刷プレビュー   
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub btnPreview_Click(sender As System.Object, e As System.EventArgs) Handles btnPreview.Click
        gintTermFuNo = mTerminalData.FuNo
        gintTermSlotNo = mTerminalData.PortNo - 1

        '保存するべきか判定
        Call mSetStructure(mudtSetChDispNew)
        ''データが変更されているかチェック
        If (Not mChkStructureEquals(gudt.SetChDisp, mudtSetChDispNew)) Or _
            mIsChangeCH() Or _
            Not mChkStructureEqualsChOutPut(mudtSetCHOutPut, mudtSetCHOutPutNew, mudtSetCHAndOr, mudtSetCHAndOrNew) Then

            ''チャンネル情報データ構造体(Disp) ---------------------
            Call mCopyStructureDisp(mudtSetChDispNew, gudt.SetChDisp)

            ''チャンネル設定構造体(CH) -----------------------------
            Call mSaveCH()

            ''出力チャンネル設定構造体(CH OutPut, CHAndOr) ---------
            Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)
            Call mCopyStructure2(mudtSetCHAndOrNew, gudt.SetChAndOr)

            Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)
            Call mCopyStructure2(mudtSetCHAndOrNew, mudtSetCHAndOr)

            ''更新フラグ設定
            gblnUpdateAll = True
            gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1      '' ver1.4.0 2011.08.22
            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1     '' ver1.4.0 2011.08.22
            gudt.SetEditorUpdateInfo.udtSave.bytOutPut = 1
            gudt.SetEditorUpdateInfo.udtSave.bytOrAnd = 1

            gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1   '' ver1.4.0 2011.08.22
            gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1  '' ver1.4.0 2011.08.22
            gudt.SetEditorUpdateInfo.udtCompile.bytOutPut = 1
            gudt.SetEditorUpdateInfo.udtCompile.bytOrAnd = 1
        End If

        'Ver2.0.1.3 端子表印刷はいきなりﾌﾟﾚﾋﾞｭｰではなく印刷設定へ
        'frmChTerminalPreview.ShowDialog()
        frmPrtTerminal.ShowDialog()
    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： OutPut ボタンクリック →　出力CH設定画面を表示する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutput.Click

        Call OutputSetting()        ' Ver1.7.6  2015.11.06  関数に変更

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Function Setボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdRtbSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRtbSet.Click

        Try

            Dim intPortNo, intPin As Integer
            Dim intChNo, intDataType, intSysNo, intFunction As Integer
            Dim intGroupNo As Integer, intDispIndex As Integer
            Dim strFuNo, strFunction, strValue As String
            Dim strRemarks As String = ""

            If grdTerminal.SelectedCells.Count = 0 Then Exit Sub

            Dim row As Integer = grdTerminal.CurrentRow.Index

            With grdTerminal.Rows(row)

                ''行選択
                .Selected = True

                ''延長警報盤なら
                If .Cells(0).Value = mCstCodeFunction Then

                    intSysNo = .Cells(1).Value
                    intChNo = Val(.Cells(2).Value)
                    strFunction = .Cells(3).Value

                    strValue = .Cells(18).Value
                    If strValue <> "" Then
                        intGroupNo = Integer.Parse(strValue.Substring(0, 2))
                        intDispIndex = Integer.Parse(strValue.Substring(3, 3))
                        strRemarks = strValue.Substring(7)
                    End If
                Else
                    intSysNo = 0
                    intChNo = 0
                    strFunction = ""
                    strRemarks = ""
                End If

                strFuNo = mTerminalData.FuName
                intPortNo = mTerminalData.PortNo
                intPin = Integer.Parse(.HeaderCell.Value.ToString.Substring(mTerminalData.FuName.Length + 1))

                If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                    intDataType = 1 ''DI
                ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                    intDataType = 2 ''DO
                End If

                ''=================================================
                If frmChListRemoteIO.gShow(strFuNo, intPortNo, _
                                           intPin, intDataType, _
                                           intSysNo, intChNo, intGroupNo, intDispIndex, _
                                           intFunction, strFunction, strRemarks, Me) = 0 Then

                    mintEventCancelFlag = 1

                    .Cells(0).Value = mCstCodeFunction         ''延長警報盤SET
                    .Cells(1).Value = intSysNo
                    .Cells(2).Value = intChNo.ToString("0000")
                    .Cells(3).Value = strFunction
                    .Cells(18).Value = intGroupNo.ToString("00") & "," & _
                                       intDispIndex.ToString("000") & "," & _
                                       strRemarks                               ''非表示エリア

                    mintChange = 1  ''延長警報盤で変更あり

                    mintEventCancelFlag = 0

                End If
                ''=================================================

            End With

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

            Dim i As Integer

            mintEventCancelFlag = 1

            For i = 0 To grdTerminal.Rows.Count - 1
                If Not gChkInputText(grdTerminal(11, i), "WIRE MARK", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal(13, i), "WIRE MARK(CLASS)", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal(14, i), "CoreNoIn", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal(15, i), "CoreNoCom", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal(16, i), "DIST", i + 1, True, True) Then Return False '' Ver1.8.4  2015.11.27  DEST → DIST
            Next

            mintEventCancelFlag = 0

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : CH比較
    ' 返り値    : false：変更なし、true：変更あり
    ' 引き数    : なし
    ' 機能説明  : CHが変更されているかを判断する
    '--------------------------------------------------------------------
    Private Function mIsChangeCH() As Boolean

        Try

            Dim intChNoNew(gCstCntFuSlotPinMax - 1) As Integer
            Dim intCHNoBefore As Integer = 0
            Dim flgChange As Boolean = False

            ''現在のチャンネルを配列にセットする
            For i As Integer = 0 To grdTerminal.RowCount - 1

                ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

                    If grdTerminal(2, i).Value = Nothing Then
                        intChNoNew(i) = 0
                    Else
                        intChNoNew(i) = Val(grdTerminal(2, i).Value)
                    End If

                Else
                    intChNoNew(i) = 0
                End If

                intCHNoBefore = Val(grdTerminal(2, i).Value)

            Next

            ''最初に退避しておいたチャンネルと比較する
            For i = 0 To gCstCntFuSlotPinMax - 1
                If mintChNoBk(i) = intChNoNew(i) Then
                    ''変化なし
                Else
                    flgChange = True : Exit For
                End If
            Next

            mIsChangeCH = flgChange

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU Addressを書き換える
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : CH比較をしFU Addressを書き換える
    '--------------------------------------------------------------------
    Private Function mSaveCH() As Integer

        Try

            Dim intChNoNew(gCstCntFuSlotPinMax - 1) As Integer
            Dim intChNo As Integer, intPin As Integer
            Dim intCHNoBefore As Integer = 0

            ''比較用にCH Noを格納
            For i As Integer = 0 To grdTerminal.RowCount - 1

                ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

                    If grdTerminal(2, i).Value = Nothing Then
                        intChNoNew(i) = 0
                    Else
                        intChNoNew(i) = Val(grdTerminal(2, i).Value)
                    End If

                Else
                    intChNoNew(i) = 0
                End If

                intCHNoBefore = Val(grdTerminal(2, i).Value)
            Next

            '' ver.1.4.0 2011.07.28 CH削除処理追加　(移動を考慮し、先にクリアしておく)
            For i = 0 To grdTerminal.RowCount - 1
                ''計測点番号
                intPin = Integer.Parse(grdTerminal.Rows(i).HeaderCell.Value.ToString.Substring(mTerminalData.FuName.Length + 1))

                If mintChNoBk(i) <> 0 And intChNoNew(i) = 0 Then    ' 前回CH有、今回CH無
                    ''CHを削除:既設では変化なし
                    mSetNewFuAddress(mintChNoBk(i), &HFFFF, &HFFFF, &HFFFF)
                End If
            Next

            intCHNoBefore = 0
            For i = 0 To grdTerminal.RowCount - 1

                ''計測点番号
                intPin = Integer.Parse(grdTerminal.Rows(i).HeaderCell.Value.ToString.Substring(mTerminalData.FuName.Length + 1))

                If grdTerminal(0, i).Value < 9 Then    ''Type = 9 は出力チャンネル

                    ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                    If mintChNoBk(i) = intChNoNew(i) Then
                        ''変化なし

                    ElseIf mintChNoBk(i) <> 0 And intChNoNew(i) = 0 Then
                        ''CHを削除:既設では変化なし　(2011.07.28 ここでCH削除すると先にアドレス設定した場合に消える)

                    ElseIf mintChNoBk(i) = 0 And intChNoNew(i) <> 0 Then

                        ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                        If intChNoNew(i) <> intCHNoBefore Then

                            ''CHを追加：NewのCHのFuAddressを変更
                            intChNo = intChNoNew(i)

                            Call mSetNewFuAddress(intChNo, mTerminalData.FuNo, mTerminalData.PortNo, intPin)

                        End If

                    Else
                        ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                        If intChNoNew(i) <> intCHNoBefore Then

                            ''CHを上書き：NewのCHのFuAddressを変更
                            ''　　　　　：BkのCHはそのまま          →　同じFuAddressに2つのCH
                            intChNo = intChNoNew(i)

                            Call mSetNewFuAddress(intChNo, mTerminalData.FuNo, mTerminalData.PortNo, intPin)

                        End If

                    End If

                ElseIf grdTerminal(0, i).Value = 10 Then
                    ''延長警報盤

                    If mTerminalData.TypeCode = 2 Then
                        ''DI
                        ''CH削除時は何もしない処理を追加　ver.1.4.0 2011.09.29
                        ''以下処理を行うとCHNo0000が表示される
                        If mintChNoBk(i) <> 0 And intChNoNew(i) = 0 Then
                            ''CHを削除
                        Else

                            intChNo = intChNoNew(i)
                            Call mSetExtPanelDI(i, intChNo, mTerminalData.FuNo, mTerminalData.PortNo, intPin)
                        End If

                    ElseIf mTerminalData.TypeCode = 1 Then
                        ''DO
                        ''CH削除時は何もしない処理を追加　ver.1.4.0 2011.09.29
                        ''以下処理を行うとCHNo0000が表示される
                        If mintChNoBk(i) <> 0 And intChNoNew(i) = 0 Then
                            ''CHを削除
                        Else

                            intChNo = intChNoNew(i)
                            Call mSetExtPanelDO(i, intChNo, mTerminalData.FuNo, mTerminalData.PortNo, intPin)
                        End If

                    End If

                End If

                intCHNoBefore = intChNoNew(i)
            Next

            ''保存後の状態をとっておく
            intCHNoBefore = 0
            For i As Integer = 0 To grdTerminal.RowCount - 1

                ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

                    If grdTerminal(2, i).Value = Nothing Then
                        mintChNoBk(i) = 0
                    Else
                        mintChNoBk(i) = Val(grdTerminal(2, i).Value)
                    End If

                Else
                    mintChNoBk(i) = 0
                End If

                intCHNoBefore = Val(grdTerminal(2, i).Value)

                mintChNoBk_2(i) = 0 ''クリア
            Next

            ''チャンネルの連続にかかわらず、表示のまま退避
            ''但し２,３行を占有している場合は、行数分のCH Noを退避（2,3行目は非表示だが）
            Dim intRow As Integer
            ''スロット種別により行単位が異なる
            '' 3線式のみ3行表示　ver1.4.0 2011.08.22
            If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                intRow = 3  ''3線式(3行毎)
            Else
                intRow = 1  ''他(1行毎)
            End If

            'Ver2.0.2.2 Stepを合わせる
            For i As Integer = 0 To grdTerminal.RowCount - 1 Step intRow

                If grdTerminal(2, i).Value <> Nothing Then

                    For j As Integer = 0 To intRow - 1
                        mintChNoBk_2(i + j) = Val(grdTerminal(2, i).Value)
                    Next

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU Address 更新(CH)
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル番号
    '           : ARG2 - (I ) FU番号
    '           : ARG3 - (I ) FUポート番号
    '           : ARG4 - (I ) FU計測点番号
    ' 機能説明  : チャンネル番号のFU Addressを書き換える
    '--------------------------------------------------------------------
    Private Sub mSetNewFuAddress(ByVal intChNo As Integer, ByVal intFuNo As Integer, _
                                 ByVal intPortNo As Integer, ByVal intPin As Integer)

        Try

            Dim intType As Integer = mTerminalData.TypeCode ''スロット種別コード

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If gGet2Byte(.udtChCommon.shtChno) = intChNo Then

                        If intType = gCstCodeFuSlotTypeDO _
                        Or intType = gCstCodeFuSlotTypeAO Then

                            ''OUT
                            If .udtChCommon.shtChType = gCstCodeChTypeMotor Then
                                .MotorFuNo = intFuNo            ''FU番号
                                .MotorPortNo = intPortNo        ''ポート番号
                                .MotorPin = intPin              ''計測点番号

                            ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveDO Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveExt Then
                                    .ValveDiDoFuNo = intFuNo        ''FU番号
                                    .ValveDiDoPortNo = intPortNo    ''ポート番号
                                    .ValveDiDoPin = intPin          ''計測点番号

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                    .ValveAiDoFuNo = intFuNo        ''FU番号
                                    .ValveAiDoPortNo = intPortNo    ''ポート番号
                                    .ValveAiDoPin = intPin          ''計測点番号

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                    .ValveAiAoFuNo = intFuNo        ''FU番号
                                    .ValveAiAoPortNo = intPortNo    ''ポート番号
                                    .ValveAiAoPin = intPin          ''計測点番号
                                End If

                            End If

                        Else
                            ''IN
                            .udtChCommon.shtFuno = intFuNo      ''FU番号
                            .udtChCommon.shtPortno = intPortNo  ''ポート番号
                            .udtChCommon.shtPin = intPin        ''計測点番号
                        End If

                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 延長警報盤チャンネル(DI)　保存
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 行番号
    '           : ARG2 - (I ) チャンネル番号
    '           : ARG3 - (I ) FU番号
    '           : ARG4 - (I ) FUポート番号
    '           : ARG5 - (I ) FU計測点番号
    ' 機能説明  : 延長警報盤チャンネルの情報を保存する
    '--------------------------------------------------------------------
    Private Sub mSetExtPanelDI(ByVal intRow As Integer, ByVal intChNo As Integer, _
                               ByVal intFuNo As Integer, ByVal intPortNo As Integer, ByVal intPin As Integer)

        Try

            Dim flg As Integer = 0
            Dim intChId As Integer = 0, intIndex As Integer = 0
            Dim strValue As String

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが一致
                    If gGet2Byte(.shtChno) = intChNo Then

                        .shtSysno = grdTerminal(1, intRow).Value
                        .shtChType = gCstCodeChTypeDigital              ''デジタルCH
                        .shtData = gCstCodeChDataTypeDigitalExt         ''延長警報盤
                        .shtFlag1 = gBitSet(.shtFlag1, 1, True)         ''SC(隠しCH設定) 有

                        .shtFuno = intFuNo      ''FU番号
                        .shtPortno = intPortNo  ''ポート番号
                        .shtPin = intPin        ''計測点番号

                        cmbFunction.Text = grdTerminal(3, intRow).Value
                        .shtEccFunc = cmbFunction.SelectedValue

                        strValue = grdTerminal(18, intRow).Value
                        If strValue <> "" Then
                            .shtGroupNo = CCInt(strValue.Substring(0, 2))
                            .shtDispPos = CCInt(strValue.Substring(3, 3))
                            .strRemark = strValue.Substring(7)
                        End If

                        flg = 1
                        Exit For

                    End If

                End With

            Next

            ''一致するチャンネルがない場合は新規登録する
            If flg = 0 Then

                ''CHの空きインデックスをGET
                intIndex = mGetChId(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(intIndex).udtChCommon

                    .shtChid = CCUInt16(intChNo)
                    .shtChno = CCUInt16(intChNo)
                    .shtSysno = grdTerminal(1, intRow).Value

                    strValue = grdTerminal(18, intRow).Value
                    If strValue <> "" Then
                        .shtGroupNo = CCInt(strValue.Substring(0, 2))
                        .shtDispPos = CCInt(strValue.Substring(3, 3))
                        .strRemark = strValue.Substring(7)
                    End If

                    .shtChType = gCstCodeChTypeDigital              ''デジタルCH
                    .shtData = gCstCodeChDataTypeDigitalExt         ''延長警報盤
                    .shtFlag1 = gBitSet(.shtFlag1, 1, True)         ''SC(隠しCH設定) 有

                    .shtFuno = intFuNo      ''FU番号
                    .shtPortno = intPortNo  ''ポート番号
                    .shtPin = intPin        ''計測点番号

                    cmbFunction.Text = grdTerminal(3, intRow).Value
                    .shtEccFunc = cmbFunction.SelectedValue

                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 延長警報盤チャンネル(DO)　保存
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 行番号
    '           : ARG2 - (I ) チャンネル番号
    '           : ARG3 - (I ) FU番号
    '           : ARG4 - (I ) FUポート番号
    '           : ARG5 - (I ) FU計測点番号
    ' 機能説明  : 延長警報盤チャンネルの情報を保存する
    '--------------------------------------------------------------------
    Private Sub mSetExtPanelDO(ByVal intRow As Integer, ByVal intChNo As Integer, _
                               ByVal intFuNo As Integer, ByVal intPortNo As Integer, ByVal intPin As Integer)

        Try

            Dim flg As Integer = 0
            Dim intChId As Integer = 0, intIndex As Integer = 0
            Dim strValue As String

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが一致
                    If gGet2Byte(.shtChno) = intChNo Then

                        .shtSysno = grdTerminal(1, intRow).Value
                        .shtChType = gCstCodeChTypeValve            ''バルブCH
                        .shtData = gCstCodeChDataTypeValveExt       ''延長警報盤
                        .shtFlag1 = gBitSet(.shtFlag1, 1, True)     ''SC(隠しCH設定) 有

                        cmbFunction.Text = grdTerminal(3, intRow).Value
                        .shtEccFunc = cmbFunction.SelectedValue

                        strValue = grdTerminal(18, intRow).Value
                        If strValue <> "" Then
                            .shtGroupNo = CCInt(strValue.Substring(0, 2))
                            .shtDispPos = CCInt(strValue.Substring(3, 3))
                            .strRemark = strValue.Substring(7)
                        End If

                        With gudt.SetChInfo.udtChannel(i)

                            .ValveDiDoFuNo = intFuNo        ''FU番号
                            .ValveDiDoPortNo = intPortNo    ''ポート番号
                            .ValveDiDoPin = intPin          ''計測点番号
                            .ValveDiDoPinNo = 1             ''計測点番号

                        End With

                        ''入力側は設定なし
                        .shtFuno = gCstCodeChNotSetFuNo        ''FU No:設定なし(FFFF)
                        .shtPortno = gCstCodeChNotSetFuPort    ''Fu PortNo:設定なし(FFFF)
                        .shtPin = gCstCodeChNotSetFuPin        ''Fu Pin:設定なし(FFFF)

                        flg = 1
                        Exit For

                    End If

                End With

            Next

            ''一致するチャンネルがない場合は新規登録する
            If flg = 0 Then

                ''CHの空きインデックスをGET
                intIndex = mGetChId(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(intIndex).udtChCommon

                    .shtChid = CCUInt16(intChNo)
                    .shtChno = CCUInt16(intChNo)
                    .shtSysno = grdTerminal(1, intRow).Value

                    strValue = grdTerminal(18, intRow).Value
                    If strValue <> "" Then
                        .shtGroupNo = CCInt(strValue.Substring(0, 2))
                        .shtDispPos = CCInt(strValue.Substring(3, 3))
                        .strRemark = strValue.Substring(7)
                    End If

                    .shtChType = gCstCodeChTypeValve            ''バルブCH
                    .shtData = gCstCodeChDataTypeValveExt       ''延長警報盤
                    .shtFlag1 = gBitSet(.shtFlag1, 1, True)     ''SC(隠しCH設定) 有

                    cmbFunction.Text = grdTerminal(3, intRow).Value
                    .shtEccFunc = cmbFunction.SelectedValue

                    .shtExtGroup = gCstCodeChCommonExtGroupNothing
                    .shtDelay = gCstCodeChCommonDelayTimerNothing
                    .shtGRepose1 = gCstCodeChCommonGroupRepose1Nothing
                    .shtGRepose2 = gCstCodeChCommonGroupRepose2Nothing

                    With gudt.SetChInfo.udtChannel(intIndex)

                        .ValveDiDoFuNo = intFuNo        ''FU番号
                        .ValveDiDoPortNo = intPortNo    ''ポート番号
                        .ValveDiDoPin = intPin          ''計測点番号
                        .ValveDiDoPinNo = 1             ''計測点番号

                    End With

                    ''入力側は設定なし
                    .shtFuno = gCstCodeChNotSetFuNo        ''FU No:設定なし(FFFF)
                    .shtPortno = gCstCodeChNotSetFuPort    ''Fu PortNo:設定なし(FFFF)
                    .shtPin = gCstCodeChNotSetFuPin        ''Fu Pin:設定なし(FFFF)

                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : CH IDを発行する
    ' 返り値    : チャンネル設定構造体のインデックス
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 機能説明  : 連番の最後尾番号  or  連番の中の空き番号
    '--------------------------------------------------------------------
    Private Function mGetChId(ByVal udtSet() As gTypSetChRec) As Integer

        Try

            Dim intIndex As Integer = -1
            Dim i As Integer

            For i = LBound(udtSet) To UBound(udtSet)

                If udtSet(i).udtChCommon.shtChid = 0 And udtSet(i).udtChCommon.shtChno = 0 Then
                    intIndex = i
                    Exit For
                End If

            Next

            Return intIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納(Disp）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル情報データ構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal udtSet As gTypSetChDisp)

        Try
            Dim intRowCnt As Integer = 0, intWkCnt As Integer = 0
            Dim intRowBk As Integer = 0

            grdTerminal.EndEdit()

            With udtSet.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1)

                ''スロット種別により行単位が異なる
                ''3線式のみ3行表示　ver1.4.0 2011.08.22

                ''CH格納処理とCABLE MARKの処理を分ける
                ''3線式の3行使用するため、表示と格納位置は一致していない
                If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                    'intRowCnt = 3  ''3線式(3行毎)

                    For i As Integer = 0 To gCstFuSlotMaxAI_3Line - 1
                        .udtPinInfo(i).shtChid = CCUInt16(grdTerminal(2, i * 3).Value)
                    Next
                Else
                    'intRowCnt = 1  ''他(1行毎)
                    For i As Integer = 0 To grdTerminal.Rows.Count - 1
                        .udtPinInfo(i).shtChid = CCUInt16(grdTerminal(2, i).Value)
                    Next
                End If

                For i As Integer = 0 To grdTerminal.Rows.Count - 1

                    .udtPinInfo(i).strCoreNoIn = grdTerminal(14, i).Value
                    .udtPinInfo(i).strCoreNoCom = grdTerminal(15, i).Value
                    .udtPinInfo(i).strWireMark = grdTerminal(11, i).Value
                    .udtPinInfo(i).strWireMarkClass = grdTerminal(13, i).Value
                    .udtPinInfo(i).strDest = grdTerminal(16, i).Value
                    .udtPinInfo(i).shtTerminalNo = grdTerminal(17, i).Value

                    ' ''2行毎, 3行毎の場合は、先頭行にしかCH Noは表示されない
                    'If intWkCnt = 0 Then intRowBk = i
                    'intWkCnt += 1
                    'If intWkCnt <= intRowCnt Then
                    '    .udtPinInfo(i).shtChid = CCUInt16(grdTerminal(2, intRowBk).Value)
                    'End If

                    'If intWkCnt = intRowCnt Then intWkCnt = 0

                Next




            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ☆CH情報表示
    ' 返り値    :  0：OK　　
    '           :  1：スロット種別に合わないCH    
    '           : -1：対象のチャンネルなし
    ' 引き数    : ARG1 - (I ) チャンネル番号
    '           : ARG2 - (I ) 行番号
    ' 機能説明  : 画面上にチャンネル番号が "手入力" された時、一覧に
    '           : チャンネル情報を表示する
    '--------------------------------------------------------------------
    Private Function mSetChDetail(ByVal hChNo As Integer, ByVal hRow As Integer) As Integer

        Try

            Dim intPinNo As Integer
            Dim strValue As String
            Dim dblValue As Double
            Dim intDecimalP As Integer
            Dim strDecimalFormat As String
            Dim intFlag As Integer = 0
            Dim intType As Integer = mTerminalData.TypeCode ''スロット種別コード
            Dim dblLowValue As Double, dblHiValue As Double

            mintEventCancelFlag = 1

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If gGet2Byte(.udtChCommon.shtChno) = hChNo Then    ''チャンネル番号が一致 ☆  

                        ''スロット種別とチャンネルタイプが一致しているか？
                        intFlag = 0
                        Select Case intType

                            Case gCstCodeFuSlotTypeDO  ''DO

                                ''モーター
                                If .udtChCommon.shtChType = gCstCodeChTypeMotor Then
                                    'Ver2.0.0.2 モーター種別増加 R Device 追加
                                    If .udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Or .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Or .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then
                                        intFlag = 0
                                    Else
                                        intFlag = 1
                                    End If
                                End If

                                ''バルブ
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then intFlag = 1 ''DI -> DO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Then intFlag = 1 ''AI(1-5V)   -> DO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then intFlag = 1 ''AI(4-20mA) -> DO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then intFlag = 1 ''Digital
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveExt Then intFlag = 1 ''Ext Device (EXT PANEL)

                            Case gCstCodeFuSlotTypeDI  ''DI

                                ''デジタル
                                If .udtChCommon.shtChType = gCstCodeChTypeDigital And _
                                  (.udtChCommon.shtData = gCstCodeChDataTypeDigitalNC _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalNO _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalExt) Then intFlag = 1

                                ''モーター
                                If (.udtChCommon.shtChType = gCstCodeChTypeMotor And .udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom) Or _
                                   (.udtChCommon.shtChType = gCstCodeChTypeMotor And .udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom55) Then intFlag = 1

                                ''バルブ
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then intFlag = 1 ''DI -> DO

                                ''デジタルコンポジット
                                If .udtChCommon.shtChType = gCstCodeChTypeComposite Then intFlag = 1

                                ''パルス
                                If .udtChCommon.shtChType = gCstCodeChTypePulse Then intFlag = 1

                                ''システム(デジタルの機器状態) 4-26
                                'If .udtChCommon.shtChType = gCstCodeChTypeSystem Then intFlag = 1

                            Case gCstCodeFuSlotTypeAO  ''AO

                                ''バルブ
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Then intFlag = 1 ''AI(1-5V)   -> AO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Then intFlag = 1 ''AI(4-20mA) -> AO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then intFlag = 1 ''Analog 4-20 mA

                            Case gCstCodeFuSlotTypeAI_2  ''AI(2 Line)

                                ''アナログ
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And (.udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog2Jpt) Then intFlag = 1

                            Case gCstCodeFuSlotTypeAI_3  ''AI(3 Line)

                                ''アナログ
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And (.udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog3Jpt) Then intFlag = 1

                            Case gCstCodeFuSlotTypeAI_1_5  ''AI(1-5V)

                                ''アナログ
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v Then intFlag = 1

                                ''バルブ
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Then intFlag = 1 ''AI(1-5V)   -> DO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Then intFlag = 1 ''AI(1-5V)   -> AO

                            Case gCstCodeFuSlotTypeAI_4_20  ''AI(4-20mA)

                                ''アナログ
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA Then intFlag = 1

                                ''バルブ
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then intFlag = 1 ''AI(4-20mA) -> DO
                                If .udtChCommon.shtChType = gCstCodeChTypeValve And .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Then intFlag = 1 ''AI(4-20mA) -> AO

                            Case gCstCodeFuSlotTypeAI_K  ''AI(K)

                                ''アナログ
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogK Then intFlag = 1

                        End Select

                        If intFlag = 0 Then
                            mintEventCancelFlag = 0
                            Return 1 ''スロット種別に合わないCH
                        End If


                        'Ver2.0.2.4 複数Pin使用CHだが基板MAXを超えるならエラー(戻り値2)
                        Dim intHanPin As Integer = 0
                        Dim intHanMax As Integer = 0
                        '>>>基板MAX取得
                        Select Case mTerminalData.TypeCode
                            Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeDI     'DO, DI
                                intHanMax = gCstFuSlotMaxDO
                            Case gCstCodeFuSlotTypeAO                           'AO
                                intHanMax = gCstFuSlotMaxAO
                            Case gCstCodeFuSlotTypeAI_2                         '2線式
                                intHanMax = gCstFuSlotMaxAI_2Line
                            Case gCstCodeFuSlotTypeAI_3                         '3線式(3行毎)
                                intHanMax = gCstFuSlotMaxAI_3Line * 3
                            Case gCstCodeFuSlotTypeAI_1_5, gCstCodeFuSlotTypeAI_K   '1-5V, K
                                intHanMax = gCstFuSlotMaxAI_1_5
                            Case gCstCodeFuSlotTypeAI_4_20                      '4-20mA
                                intHanMax = gCstFuSlotMaxAI_4_20
                        End Select
                        '>>>PIN数取得 該当外は初期値のまま＝０となる
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeMotor        'モーター
                                If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                    'DI
                                    intHanPin = .udtChCommon.shtPinNo
                                ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                    'DO
                                    intHanPin = .MotorPinNo
                                End If
                            Case gCstCodeChTypeValve        'バルブ
                                If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                    'DI
                                    intHanPin = .udtChCommon.shtPinNo
                                ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_1_5 Or _
                                       mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_4_20 Then
                                    'AI
                                    intHanPin = .udtChCommon.shtPinNo
                                ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                    'DO
                                    If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then
                                        intHanPin = .ValveDiDoPinNo
                                    ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                        intHanPin = .ValveAiDoPinNo
                                    End If
                                ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAO Then
                                    'AO
                                    intHanPin = .ValveAiAoPinNo
                                End If
                            Case gCstCodeChTypeComposite    'コンポジット
                                intHanPin = .udtChCommon.shtPinNo
                        End Select
                        '>>>判定
                        '指定行+(PIN数-1) > MAX行-1 ならエラー
                        If hRow + (intHanPin - 1) > intHanMax - 1 Then
                            mintEventCancelFlag = 0
                            Return 2
                        End If


                        grdTerminal(0, hRow).Value = .udtChCommon.shtChType
                        grdTerminal(1, hRow).Value = .udtChCommon.shtSysno
                        grdTerminal(2, hRow).Value = gGet2Byte(.udtChCommon.shtChno).ToString("0000")
                        grdTerminal(3, hRow).Value = .udtChCommon.strChitem
                        grdTerminal(4, hRow).Value = "" : grdTerminal(5, hRow).Value = "" : grdTerminal(6, hRow).Value = "" : grdTerminal(7, hRow).Value = ""

                        ''<Status> <DataType> ------------------------------------------------------
                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then        ''アナログ -------------------------

                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            End If

                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeAnalog)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeDigital Then    ''デジタル -------------------------

                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                strValue = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                'Ver2.0.7.L
                                'If strValue.Length > 8 Then
                                If LenB(strValue) > 8 Then
                                    'Ver2.0.7.L
                                    'grdTerminal(4, hRow).Value = strValue.Substring(0, 8).Trim & "/" & strValue.Substring(8).Trim
                                    grdTerminal(4, hRow).Value = MidB(strValue, 0, 8).Trim & "/" & MidB(strValue, 8).Trim
                                Else
                                    grdTerminal(4, hRow).Value = Trim(strValue)
                                End If
                            End If

                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeDigital)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''延長警報盤
                            If .udtChCommon.shtData.ToString = gCstCodeChDataTypeDigitalExt Then
                                cmbFunction.SelectedValue = .udtChCommon.shtEccFunc.ToString
                                grdTerminal(3, hRow).Value = cmbFunction.Text

                                grdTerminal(0, hRow).Value = 10  ''延長警報盤　SET

                                ''非表示エリアに グループ番号, 行番号, Remark をSET
                                grdTerminal(18, hRow).Value = .udtChCommon.shtGroupNo.ToString("00") & ","
                                grdTerminal(18, hRow).Value += .udtChCommon.shtDispPos.ToString("000") & ","
                                grdTerminal(18, hRow).Value += .udtChCommon.strRemark
                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then    ''モーター --------------------------

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeMotor)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''Status
                            Dim strwk(7) As String
                            Dim strbp(7) As String

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)

                            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                ''DI
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString   ''入力ステータス
                                intPinNo = .udtChCommon.shtPinNo                            ''計測点個数

                                If cmbStatus.SelectedValue = gCstCodeChManualInputStatus Then
                                    grdTerminal(4, hRow).Value = .udtChCommon.strStatus  ''入力ステータス名称(手入力)
                                Else
                                    ''データ種別、ステータス種別により状態が変化する
                                    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then 'Ver2.0.0.2 モーター種別増加 JをKへ
                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")

                                        'Ver2.0.0.2 モーター種別増加 START
                                    ElseIf cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunJ Then
                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        'Ver2.0.0.2 モーター種別増加 END

                                    ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then   'Ver2.0.0.2 モーター種別増加 R Device ADD
                                        ''データ種別がDevice Operationの場合、入力側の表示ステータスは固定で"RUN/STOP"にする
                                        ''.status(入力側ステータス種別コード)には、0を設定
                                        ' 2013.07.22 MO表示変更  K.Fujimoto
                                        'strwk(0) = "RUN/STOP"
                                        strwk(0) = "RUN"
                                    Else
                                        strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                    End If

                                    grdTerminal(4, hRow).Value = strwk(0)
                                End If

                            ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                ''DO

                                ''ステータス種別コード=0 <-- Device Statsuの場合ステータスは固定で"STOP/RUN"とする
                                If .udtChCommon.shtStatus = 0 Then
                                    strwk(0) = "RUN"    ' 実際ここは通らないが、「STOP/RUN」→「RUN」に変更    2013.08.07 K.Fujimoto
                                Else
                                    ''入力ステータスのビット位置から出力ステータスの格納インデックスをGETする
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString   ''入力ステータス
                                    If cmbStatus.SelectedValue = gCstCodeChManualInputStatus Then
                                        '手入力のステータスの場合、計測点個数は1
                                        strwk(0) = gGetString(.MotorOutStatus1)
                                    Else
                                        If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                                           cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then     'Ver2.0.0.2 モーター種別増加 JをKへ
                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                            If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                                                cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunK Then
                                                '正常Rは正常ステータス扱い
                                                strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            Else
                                                strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                                strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            End If
                                        End If
                                    End If
                                End If

                                Dim wkMotorStatus(7) As String
                                wkMotorStatus(0) = gGetString(.MotorOutStatus1) : wkMotorStatus(1) = gGetString(.MotorOutStatus2)
                                wkMotorStatus(2) = gGetString(.MotorOutStatus3) : wkMotorStatus(3) = gGetString(.MotorOutStatus4)
                                wkMotorStatus(4) = gGetString(.MotorOutStatus5) : wkMotorStatus(5) = gGetString(.MotorOutStatus6)
                                wkMotorStatus(6) = gGetString(.MotorOutStatus7) : wkMotorStatus(7) = gGetString(.MotorOutStatus8)

                                If strbp(0) <> "" Then strwk(0) = wkMotorStatus(strbp(0))
                                If strbp(1) <> "" Then strwk(1) = wkMotorStatus(strbp(1))
                                If strbp(2) <> "" Then strwk(2) = wkMotorStatus(strbp(2))
                                If strbp(3) <> "" Then strwk(3) = wkMotorStatus(strbp(3))
                                If strbp(4) <> "" Then strwk(4) = wkMotorStatus(strbp(4))

                                grdTerminal(4, hRow).Value = strwk(0)

                                intPinNo = .MotorPinNo                  ''計測点個数

                            End If

                            ''連続するPINにも設定する
                            For ii As Integer = 1 To intPinNo - 1
                                For col As Integer = 0 To 7

                                    If col = 4 Then  ''Status
                                        grdTerminal(col, hRow + ii).Value = strwk(ii)
                                    Else
                                        grdTerminal(col, hRow + ii).Value = grdTerminal(col, hRow).Value
                                    End If

                                Next
                            Next

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    ''バルブ ----------------------------
                            Dim strwk(7) As String

                            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                ''DI
                                intPinNo = .udtChCommon.shtPinNo        ''計測点個数

                                ''コンポジット設定テーブルよりステータス情報GET
                                Dim intIndex As Integer = .ValveCompositeTableIndex  ''コンポジットテーブルインデックス
                                If intIndex > 0 Then
                                    If gudt.SetChComposite.udtComposite(intIndex - 1).shtChid = gGet2Byte(.udtChCommon.shtChno) Then
                                        For ii As Integer = 0 To 7
                                            strwk(ii) = gudt.SetChComposite.udtComposite(intIndex - 1).udtCompInf(ii).strStatusName
                                        Next
                                    End If
                                End If
                                grdTerminal(4, hRow).Value = strwk(0)

                                ''入力ステータス
                                'Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)

                                'If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                '    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                '    grdTerminal(4, hRow).Value = cmbStatus.Text
                                'Else
                                '    grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                'End If

                            ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_1_5 Or _
                                   mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_4_20 Then
                                ''AI
                                intPinNo = .udtChCommon.shtPinNo        ''計測点個数

                                ''入力ステータス
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)

                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    grdTerminal(4, hRow).Value = cmbStatus.Text
                                Else
                                    grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                End If

                            ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                ''DO

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then

                                    intPinNo = .ValveDiDoPinNo  ''計測点個数

                                    ''出力ステータス
                                    strwk(0) = gGetString(.ValveDiDoOutStatus1) : strwk(1) = gGetString(.ValveDiDoOutStatus2)
                                    strwk(2) = gGetString(.ValveDiDoOutStatus3) : strwk(3) = gGetString(.ValveDiDoOutStatus4)
                                    strwk(4) = gGetString(.ValveDiDoOutStatus5) : strwk(5) = gGetString(.ValveDiDoOutStatus6)
                                    strwk(6) = gGetString(.ValveDiDoOutStatus7) : strwk(7) = gGetString(.ValveDiDoOutStatus8)
                                    grdTerminal(4, hRow).Value = strwk(0)

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                    ''計測点個数
                                    intPinNo = .ValveAiDoPinNo

                                    ''出力ステータス
                                    strwk(0) = gGetString(.ValveAiDoOutStatus1) : strwk(1) = gGetString(.ValveAiDoOutStatus2)
                                    strwk(2) = gGetString(.ValveAiDoOutStatus3) : strwk(3) = gGetString(.ValveAiDoOutStatus4)
                                    strwk(4) = gGetString(.ValveAiDoOutStatus5) : strwk(5) = gGetString(.ValveAiDoOutStatus6)
                                    strwk(6) = gGetString(.ValveAiDoOutStatus7) : strwk(7) = gGetString(.ValveAiDoOutStatus8)
                                    grdTerminal(4, hRow).Value = strwk(0)
                                End If

                            ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAO Then
                                ''AO
                                intPinNo = .ValveAiAoPinNo  ''計測点個数

                                ''出力ステータス
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)

                                If .ValveAiAoOutStatus <> gCstCodeChManualInputStatus Then
                                    cmbStatus.SelectedValue = .ValveAiAoOutStatus
                                    grdTerminal(4, hRow).Value = cmbStatus.Text
                                Else
                                    grdTerminal(4, hRow).Value = gGetString(.ValveAiAoOutStatus1)     ''特殊コード対応
                                End If

                            End If

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeValve)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''Range
                            If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                               .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                intDecimalP = .ValveAiDoDecimalPosition     ''Decimal Position
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                dblLowValue = .ValveAiDoRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                dblHiValue = .ValveAiDoRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                grdTerminal(5, hRow).Value = strValue    ''Range Type

                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then

                                intDecimalP = .ValveAiAoDecimalPosition     ''Decimal Position
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                grdTerminal(5, hRow).Value = strValue    ''Range Type
                            End If

                            ''連続するPINにも設定する
                            If (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom) And _
                               (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom55) And _
                               (.udtChCommon.shtData <> gCstCodeChDataTypeValveExt) Then

                                For ii As Integer = 1 To intPinNo - 1
                                    For col As Integer = 0 To 7

                                        If col = 4 Then  ''Status

                                            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Or _
                                               mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                                grdTerminal(col, hRow + ii).Value = strwk(ii)
                                            Else
                                                grdTerminal(col, hRow + ii).Value = grdTerminal(col, hRow).Value
                                            End If

                                            'If mTerminalData.TypeCode <> gCstCodeFuSlotTypeDO Then
                                            '    grdTerminal(col, hRow + ii).Value = grdTerminal(col, hRow).Value
                                            'Else
                                            '    grdTerminal(col, hRow + ii).Value = strwk(ii)
                                            'End If
                                        Else
                                            grdTerminal(col, hRow + ii).Value = grdTerminal(col, hRow).Value
                                        End If

                                    Next
                                Next
                            End If

                            ''延長警報盤
                            If .udtChCommon.shtData.ToString = gCstCodeChDataTypeValveExt Then

                                cmbFunction.SelectedValue = .udtChCommon.shtEccFunc.ToString
                                grdTerminal(3, hRow).Value = cmbFunction.Text

                                grdTerminal(0, hRow).Value = mCstCodeFunction  ''延長警報盤　SET

                                ''出力ステータス
                                If .ValveDiDoStatus <> gCstCodeChManualInputStatus Then
                                    cmbStatus.SelectedValue = .ValveDiDoStatus
                                    grdTerminal(4, hRow).Value = cmbStatus.Text
                                Else
                                    grdTerminal(4, hRow).Value = ""
                                End If

                                ''非表示エリアにグループ番号, 行番号, RemarkをSET
                                grdTerminal(18, hRow).Value = .udtChCommon.shtGroupNo.ToString("00") & ","
                                grdTerminal(18, hRow).Value += .udtChCommon.shtDispPos.ToString("000") & ","
                                grdTerminal(18, hRow).Value += .udtChCommon.strRemark
                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeComposite Then    ''コンポジット ----------------------

                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeComposite)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''コンポジット設定テーブルよりステータス情報GET
                            Dim strwk(7) As String
                            Dim intIndex As Integer = .CompositeTableIndex  ''コンポジットテーブルインデックス
                            If intIndex > 0 Then
                                If gudt.SetChComposite.udtComposite(intIndex - 1).shtChid = hChNo Then
                                    For ii As Integer = 0 To 7
                                        strwk(ii) = gudt.SetChComposite.udtComposite(intIndex - 1).udtCompInf(ii).strStatusName
                                    Next
                                End If
                            End If
                            grdTerminal(4, hRow).Value = strwk(0)

                            ''連続するPINにも設定する
                            intPinNo = .udtChCommon.shtPinNo        ''計測点個数
                            For ii As Integer = 1 To intPinNo - 1
                                For col As Integer = 0 To 9
                                    If col = 4 Then  ''Status
                                        grdTerminal(col, hRow + ii).Value = strwk(ii)
                                    Else
                                        grdTerminal(col, hRow + ii).Value = grdTerminal(col, hRow).Value
                                    End If
                                Next
                            Next

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then    ''パルス ----------------------------

                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            End If

                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypePulse)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                        End If

                        ''<Range> --------------------------------------------------------------------
                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then        ''アナログ

                            intDecimalP = .AnalogDecimalPosition     ''Decimal Position
                            strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                ''データ種別コード　2,3線式
                                dblValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                strValue = dblValue.ToString(strDecimalFormat)            ''Range Type 上位 Low

                                dblValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblValue.ToString(strDecimalFormat)   ''Range Type 上位 Low　+　下位 High

                                grdTerminal(5, hRow).Value = strValue   ''Range Type("999 - 999")

                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogK _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA Then

                                ''データ種別コード　K, 1-5 V, 4-20 mA
                                dblValue = .AnalogRangeLow / (10 ^ intDecimalP)     ''Range  Low
                                strValue = dblValue.ToString(strDecimalFormat)

                                dblValue = .AnalogRangeHigh / (10 ^ intDecimalP)    ''Range Hi
                                strValue += " - " & dblValue.ToString(strDecimalFormat)

                                grdTerminal(5, hRow).Value = strValue   ''Range Type("999 - 999")

                            End If
                        End If

                        ''<Unit> --------------------------------------------------------------------
                        If (.udtChCommon.shtChType = gCstCodeChTypeAnalog) _
                        Or (.udtChCommon.shtChType = gCstCodeChTypePulse) _
                        Or (.udtChCommon.shtChType = gCstCodeChTypeValve And _
                           (.udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                            .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Or _
                            .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                            .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                            .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20)) Then    ''アナログ/パルス/バルブ(A)

                            If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                grdTerminal(6, hRow).Value = cmbUnit.Text
                            Else
                                grdTerminal(6, hRow).Value = gGetString(.udtChCommon.strUnit)     ''特殊コード対応
                            End If

                        End If

                        mintEventCancelFlag = 0

                        Return 0

                    End If

                End With

            Next

            mintEventCancelFlag = 0

            Return -1

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ☆設定値表示(CH）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplayCh(ByVal udtSet As gTypSetChInfo)

        Try

            Dim row As Integer
            Dim strValue As String
            Dim intDecimalP As Integer
            Dim strDecimalFormat As String
            Dim intRow As Integer
            Dim intFuNo, intPortNo, intPin, intPinNo As Integer
            Dim dblLowValue As Double, dblHiValue As Double

            Dim aryCheck As New ArrayList
            Dim strChNo As String = ""
            Dim strListIndex As String = ""
            Dim i As Integer
            Dim x As Integer
            Dim intChkChNo As Integer
            Dim intChkindex As Integer
            Dim intRHFlg As Integer

            'T.Ueki
            Dim CHViewFlg As Boolean

            ''単位コンボボックス（非表示）
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)

            ''スロット種別により行単位が異なる
            '' 3線式のみ3行表示　ver1.4.0 2011.08.22
            If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                intRow = 3  ''3線式(3行毎)
            Else
                intRow = 1  ''他(1行毎)
            End If

            ''チャンネル番号順に並べ替え 2015.07.10
            gMakeChNoOrderSort(aryCheck)

            'For i As Integer = LBound(udtSet.udtChannel) To UBound(udtSet.udtChannel)
            For x = 0 To aryCheck.Count - 1     '' CH順ソートでループ   2015.07.10

                ''ソート結果からリストのインデックス取得   2015.07.10
                gGetChNoOrder(aryCheck, x, strChNo, strListIndex)

                i = Val(strListIndex)   '' リストインデックスをセット    2015.07.10
                With udtSet.udtChannel(i)

                    If .udtChCommon.shtChno = 6602 Then
                        Dim a As Integer = 0
                    End If

                    CHViewFlg = False

                    Select Case .udtChCommon.shtChType

                        Case gCstCodeChTypeAnalog       'アナログ

                            '' Ver1.11.1 2016.07.12 緯度・経度追加
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogModbus Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev _
                                   Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhAve Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom55 Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeDigital      'デジタル

                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNC _
                                  Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNO Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNO Or _
                                  .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacom55NC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacom55NO Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeMotor        'モーター

                            'Ver2.0.7.S JACOMより大きい＝通信CHは非表示
                            'If .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                            If .udtChCommon.shtData >= gCstCodeChDataTypeMotorDeviceJacom Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeValve        'バルブ

                            If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                               .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeComposite    'コンポジット

                            '処理無し

                        Case gCstCodeChTypePulse        'パルス

                            '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                            ' Ver1.12.0.1 2017.01.13  運転積算種類追加
                            If .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Or _
                                .udtChCommon.shtData >= gCstCodeChDataTypePulseRevoExtDev Then
                                CHViewFlg = True
                            End If

                        Case Else

                            '処理無し
                    End Select


                    If CHViewFlg = False Then

                        'If .udtChCommon.shtData = gCstCodeChDataTypeAnalogModbus Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhAve Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose _
                        '   Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNO _
                        '    Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNO _
                        '    Or .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Or .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                        '    ''システムチャンネル(データ種別コードが機器状態)は端子台に表示しない

                        '    ' .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Or
                        '    ' .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Or

                        'Else
                        For j As Integer = 0 To 1   'InとOutの２回分 LOOP

                            If j = 1 Then
                                If (.udtChCommon.shtChType = gCstCodeChTypeMotor) _
                                Or (.udtChCommon.shtChType = gCstCodeChTypeValve) Then
                                    ''Motor、Valve は 外部出力FU Addressがある
                                Else
                                    Exit For
                                End If
                            End If

                            ''FU番号, FUポート番号が一致 ☆☆☆
                            row = 65535     ''初期化
                            If j = 0 Then
                                ''入力
                                If (.udtChCommon.shtFuno = mTerminalData.FuNo) And (.udtChCommon.shtPortno = mTerminalData.PortNo) Then

                                    row = gGet2Byte(.udtChCommon.shtPin)    ''計測点番号
                                    intPinNo = .udtChCommon.shtPinNo        ''計測点個数

                                End If

                            ElseIf j = 1 Then
                                ''出力
                                If .udtChCommon.shtChType = gCstCodeChTypeMotor Then        ''モーターCH
                                    intFuNo = .MotorFuNo            ''Fu No
                                    intPortNo = .MotorPortNo        ''Port No
                                    intPin = .MotorPin              ''Pin
                                    intPinNo = .MotorPinNo          ''計測点個数

                                ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    ''バルブCH

                                    If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveDO Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveExt Then
                                        ''DI->DO
                                        intFuNo = .ValveDiDoFuNo        ''Fu No
                                        intPortNo = .ValveDiDoPortNo    ''Port No
                                        intPin = .ValveDiDoPin          ''Pin
                                        intPinNo = .ValveDiDoPinNo      ''計測点個数

                                    ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                        ''AI->DO
                                        intFuNo = .ValveAiDoFuNo        ''Fu No
                                        intPortNo = .ValveAiDoPortNo    ''Port No
                                        intPin = .ValveAiDoPin          ''Pin
                                        intPinNo = .ValveAiDoPinNo      ''計測点個数

                                    ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                        ''AI->AO
                                        intFuNo = .ValveAiAoFuNo        ''Fu No
                                        intPortNo = .ValveAiAoPortNo    ''Port No
                                        intPin = .ValveAiAoPin          ''Pin
                                        intPinNo = .ValveAiAoPinNo      ''計測点個数

                                    End If
                                End If

                                If (intFuNo = mTerminalData.FuNo) And (intPortNo = mTerminalData.PortNo) Then

                                    row = intPin    ''計測点番号

                                    'If .udtChCommon.shtChType = gCstCodeChTypeMotor Then        ''モーターCH
                                    '    intPinNo = .MotorPinNo      ''計測点個数

                                    'ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    ''バルブCH
                                    '    intPinNo = .ValveDiDoPinNo  ''計測点個数
                                    'End If
                                End If
                            End If

                            'Ver2.0.2.4 複数Pin使用CHだが基板MAXを超えるならエラー(戻り値2)
                            If row <> 65535 Then
                                Dim intHanPin As Integer = 0
                                Dim intHanMax As Integer = 0
                                '>>>基板MAX取得
                                Select Case mTerminalData.TypeCode
                                    Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeDI     'DO, DI
                                        intHanMax = gCstFuSlotMaxDO
                                    Case gCstCodeFuSlotTypeAO                           'AO
                                        intHanMax = gCstFuSlotMaxAO
                                    Case gCstCodeFuSlotTypeAI_2                         '2線式
                                        intHanMax = gCstFuSlotMaxAI_2Line
                                    Case gCstCodeFuSlotTypeAI_3                         '3線式(3行毎)
                                        intHanMax = gCstFuSlotMaxAI_3Line * 3
                                    Case gCstCodeFuSlotTypeAI_1_5, gCstCodeFuSlotTypeAI_K   '1-5V, K
                                        intHanMax = gCstFuSlotMaxAI_1_5
                                    Case gCstCodeFuSlotTypeAI_4_20                      '4-20mA
                                        intHanMax = gCstFuSlotMaxAI_4_20
                                End Select
                                '>>>PIN数取得 該当外は初期値のまま＝０となる
                                Select Case .udtChCommon.shtChType
                                    Case gCstCodeChTypeMotor        'モーター
                                        If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                            'DI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                            'DO
                                            intHanPin = .MotorPinNo
                                        End If
                                    Case gCstCodeChTypeValve        'バルブ
                                        If mTerminalData.TypeCode = gCstCodeFuSlotTypeDI Then
                                            'DI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_1_5 Or _
                                               mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_4_20 Then
                                            'AI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then
                                            'DO
                                            If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                               .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then
                                                intHanPin = .ValveDiDoPinNo
                                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                                intHanPin = .ValveAiDoPinNo
                                            End If
                                        ElseIf mTerminalData.TypeCode = gCstCodeFuSlotTypeAO Then
                                            'AO
                                            intHanPin = .ValveAiAoPinNo
                                        End If
                                    Case gCstCodeChTypeComposite    'コンポジット
                                        intHanPin = .udtChCommon.shtPinNo
                                End Select
                                '>>>判定
                                '指定行+(PIN数-1) > MAX行-1 ならエラー
                                If row + (intHanPin - 1) > intHanMax Then
                                    'メッセージボックスを出してCHNoを教える(一覧には表示しない,自動で消去もしない)
                                    MsgBox("This CHNo can't ADD over PIN MAX count." & vbCrLf & "ChNo=" & gGet2Byte(.udtChCommon.shtChno).ToString("0000"), _
                                           MsgBoxStyle.Exclamation, "TERMINAL ORDER INPUT")
                                    '該当無しと同じ処置とする
                                    row = 65535
                                End If
                            End If


                            If row <> 65535 Then    ''計測点番号がセットされている

                                If intRow = 1 Then
                                    row = row - 1
                                ElseIf intRow = 2 Then
                                    row = (row - 1) * 2
                                ElseIf intRow = 3 Then
                                    row = (row - 1) * 3
                                End If

                                If row <= grdTerminal.RowCount - 1 Then

                                    'ver2.0.8.7 RHよりMOTOR CHを優先とする
                                    intRHFlg = 0
                                    If grdTerminal(2, row).Value <> "" Then
                                        intChkChNo = grdTerminal(2, row).Value
                                        intChkindex = gConvChNoToChArrayId(intChkChNo)
                                        If udtSet.udtChannel(intChkindex).udtChCommon.shtChType = gCstCodeChTypePulse Then
                                            intRHFlg = 1
                                        End If
                                    End If

                                    If grdTerminal(2, row).Value = "" _
                                    Or intRHFlg = 1 Then  ''未設定の場合、RHの場合は他設定を優先

                                        grdTerminal(0, row).Value = .udtChCommon.shtChType
                                        grdTerminal(1, row).Value = .udtChCommon.shtSysno
                                        grdTerminal(2, row).Value = gGet2Byte(.udtChCommon.shtChno).ToString("0000")
                                        grdTerminal(3, row).Value = .udtChCommon.strChitem

                                        ''<Unit> --------------------------------------------------------------------
                                        If intRHFlg = 1 Then
                                            grdTerminal(6, row).Value = ""  ''クリア
                                        End If

                                        If (.udtChCommon.shtChType = gCstCodeChTypeAnalog) _
                                        Or (.udtChCommon.shtChType = gCstCodeChTypePulse) _
                                        Or (.udtChCommon.shtChType = gCstCodeChTypeValve And _
                                            (.udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                             .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Or _
                                             .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                             .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                             .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20)) Then    ''アナログ/パルス/バルブ(A)

                                            If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                                grdTerminal(6, row).Value = cmbUnit.Text
                                            Else
                                                grdTerminal(6, row).Value = gGetString(.udtChCommon.strUnit)     ''特殊コード対応
                                            End If

                                        End If

                                        ''<Status> <DataType> ------------------------------------------------------
                                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then        ''アナログ ----

                                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                                grdTerminal(4, row).Value = cmbStatus.Text
                                            Else
                                                grdTerminal(4, row).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                            End If

                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeAnalog)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeDigital Then    ''デジタル ----

                                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                                grdTerminal(4, row).Value = cmbStatus.Text
                                            Else
                                                strValue = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                                'Ver2.0.7.L
                                                'If strValue.Length > 8 Then
                                                If LenB(strValue) > 8 Then
                                                    'Ver2.0.7.L
                                                    'grdTerminal(4, row).Value = strValue.Substring(0, 8).Trim & "/" & strValue.Substring(8).Trim
                                                    grdTerminal(4, row).Value = MidB(strValue, 0, 8).Trim & "/" & MidB(strValue, 8).Trim
                                                Else
                                                    grdTerminal(4, row).Value = Trim(strValue)
                                                End If
                                            End If

                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeDigital)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                            ''延長警報盤
                                            If .udtChCommon.shtData.ToString = gCstCodeChDataTypeDigitalExt Then
                                                cmbFunction.SelectedValue = .udtChCommon.shtEccFunc.ToString
                                                grdTerminal(3, row).Value = cmbFunction.Text

                                                grdTerminal(0, row).Value = mCstCodeFunction   ''延長警報盤　SET

                                                ''非表示エリアに　"グループ番号, 行番号, Remark" をSET
                                                grdTerminal(18, row).Value = .udtChCommon.shtGroupNo.ToString("00") & ","
                                                grdTerminal(18, row).Value += .udtChCommon.shtDispPos.ToString("000") & ","
                                                grdTerminal(18, row).Value += .udtChCommon.strRemark

                                            End If

                                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then    ''モーター -----

                                            ''Data Type
                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeMotor)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                            ''Status
                                            Dim strwk(7) As String
                                            Dim strbp(7) As String

                                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                            If j = 0 Then
                                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString   ''入力ステータス

                                                If cmbStatus.SelectedValue = gCstCodeChManualInputStatus Then
                                                    '手入力のステータスの場合、計測点個数は1
                                                    grdTerminal(4, row).Value = .udtChCommon.strStatus  ''入力ステータス名称(手入力)
                                                Else
                                                    ''データ種別、ステータス種別により状態が変化する
                                                    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then     'Ver2.0.0.2 モーター種別増加 JをKへ
                                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")


                                                        'Ver2.0.0.2 モーター種別増加 START
                                                    ElseIf cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunK Then 'Ver2.0.0.2 モーター種別増加 JをKへ
                                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                        'Ver2.0.0.2 モーター種別増加 END


                                                    ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then   'Ver2.0.0.2 モーター種別増加
                                                        ''データ種別がDevice Operationの場合、入力側の表示ステータスは固定で"RUN/STOP"にする
                                                        ''.status(入力側ステータス種別コード)には、0を設定
                                                        ' 2013.07.22 MO表示変更  K.Fujimoto
                                                        'strwk(0) = "RUN/STOP"
                                                        strwk(0) = "RUN"
                                                    Else
                                                        strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                                    End If

                                                    grdTerminal(4, row).Value = strwk(0)
                                                End If

                                            ElseIf j = 1 Then
                                                'cmbStatus.SelectedValue = .MotorStatus ''出力ステータス

                                                ''入力ステータスのビット位置から出力ステータスの格納インデックスをGETする
                                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString   ''入力ステータス
                                                If cmbStatus.SelectedValue = gCstCodeChManualInputStatus Then
                                                    '手入力のステータスの場合、計測点個数は1
                                                    strwk(0) = gGetString(.MotorOutStatus1)
                                                Else
                                                    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And _
                                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunK Then     'Ver2.0.0.2 モーター種別増加 JをKへ
                                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")


                                                        'Ver2.0.0.2 モーター種別増加 START
                                                    ElseIf cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorRManRun And _
                                                       cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorRManRunK Then
                                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                                        'Ver2.0.0.2 モーター種別増加 END


                                                    ElseIf cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then 'Ver2.0.0.2 モーター種別増加 R Device

                                                    Else
                                                        strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                                        strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                                    End If
                                                End If

                                                Dim wkMotorStatus(7) As String
                                                wkMotorStatus(0) = gGetString(.MotorOutStatus1) : wkMotorStatus(1) = gGetString(.MotorOutStatus2)
                                                wkMotorStatus(2) = gGetString(.MotorOutStatus3) : wkMotorStatus(3) = gGetString(.MotorOutStatus4)
                                                wkMotorStatus(4) = gGetString(.MotorOutStatus5) : wkMotorStatus(5) = gGetString(.MotorOutStatus6)
                                                wkMotorStatus(6) = gGetString(.MotorOutStatus7) : wkMotorStatus(7) = gGetString(.MotorOutStatus8)

                                                If strbp(0) <> "" Then strwk(0) = wkMotorStatus(strbp(0))
                                                If strbp(1) <> "" Then strwk(1) = wkMotorStatus(strbp(1))
                                                If strbp(2) <> "" Then strwk(2) = wkMotorStatus(strbp(2))
                                                If strbp(3) <> "" Then strwk(3) = wkMotorStatus(strbp(3))
                                                If strbp(4) <> "" Then strwk(4) = wkMotorStatus(strbp(4))

                                                grdTerminal(4, row).Value = strwk(0)
                                            End If

                                            ''連続するPINにも設定する
                                            For ii As Integer = 1 To intPinNo - 1
                                                For col As Integer = 0 To 7

                                                    If col = 4 Then  ''Status
                                                        grdTerminal(col, row + ii).Value = strwk(ii)
                                                    Else
                                                        grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                    End If

                                                Next
                                            Next

                                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    ''バルブ -----
                                            Dim strwk(7) As String
                                            If j = 0 Then
                                                ''入力ステータス
                                                If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then

                                                    ''コンポジット設定テーブルよりステータス情報GET
                                                    Dim intIndex As Integer = .ValveCompositeTableIndex  ''コンポジットテーブルインデックス
                                                    If intIndex > 0 Then
                                                        If gudt.SetChComposite.udtComposite(intIndex - 1).shtChid = gGet2Byte(.udtChCommon.shtChno) Then
                                                            For ii As Integer = 0 To 7
                                                                strwk(ii) = gudt.SetChComposite.udtComposite(intIndex - 1).udtCompInf(ii).strStatusName
                                                            Next
                                                        End If
                                                    End If
                                                    grdTerminal(4, row).Value = strwk(0)

                                                Else
                                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)

                                                    If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                                        cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                                        grdTerminal(4, row).Value = cmbStatus.Text
                                                    Else
                                                        grdTerminal(4, row).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                                    End If
                                                End If

                                            ElseIf j = 1 Then
                                                ''出力ステータス
                                                If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                                Else
                                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                                End If

                                                If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then         ''8
                                                    'cmbStatus.SelectedValue = .ValveDiDoStatus
                                                    'grdTerminal(4, row).Value = cmbStatus.Text
                                                    strwk(0) = gGetString(.ValveDiDoOutStatus1) : strwk(1) = gGetString(.ValveDiDoOutStatus2)
                                                    strwk(2) = gGetString(.ValveDiDoOutStatus3) : strwk(3) = gGetString(.ValveDiDoOutStatus4)
                                                    strwk(4) = gGetString(.ValveDiDoOutStatus5) : strwk(5) = gGetString(.ValveDiDoOutStatus6)
                                                    strwk(6) = gGetString(.ValveDiDoOutStatus7) : strwk(7) = gGetString(.ValveDiDoOutStatus8)

                                                    grdTerminal(4, row).Value = strwk(0)

                                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then    ''8
                                                    'cmbStatus.SelectedValue = .ValveAiDoOutStatus
                                                    'grdTerminal(4, row).Value = cmbStatus.Text
                                                    strwk(0) = gGetString(.ValveAiDoOutStatus1) : strwk(1) = gGetString(.ValveAiDoOutStatus2)
                                                    strwk(2) = gGetString(.ValveAiDoOutStatus3) : strwk(3) = gGetString(.ValveAiDoOutStatus4)
                                                    strwk(4) = gGetString(.ValveAiDoOutStatus5) : strwk(5) = gGetString(.ValveAiDoOutStatus6)
                                                    strwk(6) = gGetString(.ValveAiDoOutStatus7) : strwk(7) = gGetString(.ValveAiDoOutStatus8)

                                                    grdTerminal(4, row).Value = strwk(0)

                                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then    ''1

                                                    If .ValveAiAoOutStatus <> gCstCodeChManualInputStatus Then
                                                        cmbStatus.SelectedValue = .ValveAiAoOutStatus
                                                        grdTerminal(4, row).Value = cmbStatus.Text
                                                    Else
                                                        grdTerminal(4, row).Value = gGetString(.ValveAiAoOutStatus1)     ''特殊コード対応
                                                    End If

                                                End If
                                            End If

                                            ''Data Type　--------------------------------------------------------
                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeValve)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                            ''Range
                                            If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                               .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                                intDecimalP = .ValveAiDoDecimalPosition     ''Decimal Position
                                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                                dblLowValue = .ValveAiDoRangeLow / (10 ^ intDecimalP)
                                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                                dblHiValue = .ValveAiDoRangeHigh / (10 ^ intDecimalP)
                                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                                grdTerminal(5, row).Value = strValue    ''Range Type

                                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then

                                                intDecimalP = .ValveAiAoDecimalPosition     ''Decimal Position
                                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                                dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                                dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                                grdTerminal(5, row).Value = strValue    ''Range Type

                                            End If

                                            ''連続するPINにも設定する
                                            If (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom) And _
                                               (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom55) And _
                                               (.udtChCommon.shtData <> gCstCodeChDataTypeValveExt) Then

                                                For ii As Integer = 1 To intPinNo - 1
                                                    For col As Integer = 0 To 7

                                                        If col = 4 Then  ''Status

                                                            If j = 0 Then

                                                                If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then
                                                                    grdTerminal(col, row + ii).Value = strwk(ii)
                                                                Else
                                                                    grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                                End If

                                                            ElseIf j = 1 Then

                                                                If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                                                    grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                                Else
                                                                    grdTerminal(col, row + ii).Value = strwk(ii)
                                                                End If

                                                            End If
                                                            'If (j = 0) Or _
                                                            '       (.udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                                            '        .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                                            '        .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20) Then

                                                            '    grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                            'Else
                                                            '    grdTerminal(col, row + ii).Value = strwk(ii)
                                                            'End If
                                                        Else
                                                            grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                        End If

                                                    Next
                                                Next
                                            End If

                                            ''延長警報盤
                                            If .udtChCommon.shtData.ToString = gCstCodeChDataTypeValveExt Then

                                                cmbFunction.SelectedValue = .udtChCommon.shtEccFunc.ToString
                                                grdTerminal(3, row).Value = cmbFunction.Text

                                                grdTerminal(0, row).Value = mCstCodeFunction   ''延長警報盤　SET

                                                ''出力ステータス
                                                If .ValveDiDoStatus <> gCstCodeChManualInputStatus Then
                                                    cmbStatus.SelectedValue = .ValveDiDoStatus
                                                    grdTerminal(4, row).Value = cmbStatus.Text
                                                Else
                                                    grdTerminal(4, row).Value = ""
                                                End If

                                                ''非表示エリアに "グループ番号, 行番号, Remark" をSET
                                                grdTerminal(18, row).Value = .udtChCommon.shtGroupNo.ToString("00") & ","
                                                grdTerminal(18, row).Value += .udtChCommon.shtDispPos.ToString("000") & ","
                                                grdTerminal(18, row).Value += .udtChCommon.strRemark

                                            End If

                                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeComposite Then    ''コンポジット ---

                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeComposite)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                            ''コンポジット設定テーブルよりステータス情報GET
                                            Dim strwk(7) As String
                                            Dim intIndex As Integer = .CompositeTableIndex  ''コンポジットテーブルインデックス
                                            If intIndex > 0 Then
                                                If gudt.SetChComposite.udtComposite(intIndex - 1).shtChid = gGet2Byte(.udtChCommon.shtChno) Then
                                                    For ii As Integer = 0 To 7
                                                        strwk(ii) = gudt.SetChComposite.udtComposite(intIndex - 1).udtCompInf(ii).strStatusName
                                                    Next
                                                End If
                                            End If
                                            grdTerminal(4, row).Value = strwk(0)

                                            ''連続するPINにも設定する
                                            For ii As Integer = 1 To intPinNo - 1
                                                For col As Integer = 0 To 7
                                                    If col = 4 Then  ''Status
                                                        grdTerminal(col, row + ii).Value = strwk(ii)
                                                    Else
                                                        grdTerminal(col, row + ii).Value = grdTerminal(col, row).Value
                                                    End If

                                                Next
                                            Next

                                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then        ''パルス ------

                                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                                grdTerminal(4, row).Value = cmbStatus.Text
                                            Else
                                                grdTerminal(4, row).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                            End If

                                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypePulse)
                                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                                            grdTerminal(7, row).Value = cmbDataType.Text

                                        End If

                                        ''<Range> --------------------------------------------------------------------
                                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then        ''アナログ

                                            intDecimalP = .AnalogDecimalPosition     ''Decimal Position
                                            strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                                ''データ種別コード　2,3線式
                                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range Type 上位 Low

                                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Type 上位 Low　+　下位 High

                                                grdTerminal(5, row).Value = strValue        ''Range Type

                                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogK _
                                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v _
                                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA Then

                                                ''データ種別コード　K, 1-5 V, 4-20 mA
                                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                                grdTerminal(5, row).Value = strValue    ''Range Type
                                            End If
                                        End If



                                    End If

                                End If

                            End If

                        Next j
                    End If
                    'End If

                End With

            Next x

            ''初期状態をとっておく(保存処理の時に使う)
            Dim intCHNoBefore As Integer = 0
            For i = 0 To grdTerminal.RowCount - 1

                ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

                    If grdTerminal(2, i).Value = Nothing Then
                        mintChNoBk(i) = 0
                    Else
                        mintChNoBk(i) = Val(grdTerminal(2, i).Value)
                    End If
                Else
                    mintChNoBk(i) = 0
                End If

                intCHNoBefore = Val(grdTerminal(2, i).Value)

                mintChNoBk_2(i) = 0 ''クリア
            Next

            ''チャンネルの連続にかかわらず、表示のまま退避
            ''但し２,３行を占有している場合は、行数分のCH Noを退避（2,3行目は非表示だが）
            'Ver2.0.2.2 Step数を合わす
            For i = 0 To grdTerminal.RowCount - 1 Step intRow

                If grdTerminal(2, i).Value <> Nothing Then

                    For j As Integer = 0 To intRow - 1
                        mintChNoBk_2(i + j) = Val(grdTerminal(2, i).Value)
                    Next

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示(CHOutPut）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    '           : ARG2 - (I ) 出力チャンネル設定構造体
    '           : ARG3 - (I ) 論理出力設定構造体
    ' 機能説明  : 出力チャンネル構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplayChOutPut(ByVal udtSetCH As gTypSetChInfo, _
                                    ByVal udtSetOutPut As gTypSetChOutput, _
                                    ByVal udtSetAndOr As gTypSetChAndOr)

        Try

            Dim row As Integer
            Dim intRow As Integer
            Dim intChNo As Integer = 0
            Dim intOrAndIndex As Integer = 0
            Dim strChStart As String = "", strChEnd As String = ""
            Dim intValue As Integer

            ''スロット種別により行単位が異なる
            '' 3線式のみ3行表示　ver1.4.0 2011.08.22
            If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                intRow = 3  ''3線式(3行毎)
            Else
                intRow = 1  ''他(1行毎)
            End If

            For i As Integer = LBound(udtSetOutPut.udtCHOutPut) To UBound(udtSetOutPut.udtCHOutPut)

                With udtSetOutPut.udtCHOutPut(i)

                    ''FU番号, FUポート番号が一致 ☆☆☆
                    row = 65535

                    If (.bytFuno = mTerminalData.FuNo) And (.bytPortno = mTerminalData.PortNo) Then

                        row = .bytPin ''計測点番号

                    End If

                    If row <> 65535 Then

                        If intRow = 1 Then
                            row = row - 1
                        ElseIf intRow = 2 Then
                            row = (row - 1) * 2
                        ElseIf intRow = 3 Then
                            row = (row - 1) * 3
                        End If

                        'Ver2.0.2.1 行を超えるなら次のﾃﾞｰﾀへ
                        If grdTerminal.RowCount < row Then Continue For

                        grdTerminal(0, row).Value = mCstCodeChOutPut   ''出力チャンネル
                        grdTerminal(1, row).Value = .shtSysno

                        ''DOの場合のみ
                        'If i < 512 Then
                        cmbChOutType.SelectedValue = .bytOutput.ToString
                        grdTerminal(8, row).Value = cmbChOutType.Text

                        '' ver.1.4.5 2012.07.03 論理出力設定時は先頭CHの状態を表示
                        If .bytType = gCstCodeFuOutputChTypeCh Then
                            cmbOutputMovement.SelectedValue = .bytStatus
                            grdTerminal(9, row).Value = cmbOutputMovement.Text
                        Else
                            intOrAndIndex = .shtChid - 1
                            cmbOutputMovement.SelectedValue = udtSetAndOr.udtCHOut(intOrAndIndex).udtCHAndOr(0).bytStatus
                            grdTerminal(9, row).Value = cmbOutputMovement.Text
                        End If

                        'End If

                        intChNo = .shtChid

                        ''出力チャンネル設定構造体の配列インデックスを格納（非表示）
                        grdTerminal(18, row).Value = i

                        If .bytType = gCstCodeFuOutputChTypeCh Then
                            ''CHデータ
                            intValue = gGet2Byte(.shtChid)
                            grdTerminal(2, row).Value = IIf(intValue = 0, "", intValue.ToString("0000"))

                            Call mSetChInfoChOut(intChNo, row, udtSetCH)

                        ElseIf .bytType = gCstCodeFuOutputChTypeOr Or .bytType = gCstCodeFuOutputChTypeAnd Then
                            ''論理出力チャンネル
                            grdTerminal(2, row).Value = "---"

                            ''Item Name作成

                            ''論理出力構造体のインデックスをGETする
                            intOrAndIndex = .shtChid - 1
                            'intOrAndIndex = mGetIndexOrAnd(.bytType, i, udtSetOutPut)

                            ''ANDの場合は33レコード目から
                            'If .bytType = 2 Then intOrAndIndex += gCstCntFuAndOrRecCntOr

                            strChStart = gGet2Byte(udtSetAndOr.udtCHOut(intOrAndIndex).udtCHAndOr(0).shtChid)
                            For j = 0 To 23

                                With udtSetAndOr.udtCHOut(intOrAndIndex).udtCHAndOr(j)
                                    If gGet2Byte(.shtChid) <> 0 Then strChEnd = gGet2Byte(.shtChid)
                                End With

                            Next j

                            grdTerminal(3, row).Value = "CH No. " & strChStart & " - CH No. " & strChEnd

                        End If

                    End If

                End With

            Next i

            ''初期状態をとっておく(保存処理の時に使う)
            Dim intCHNoBefore As Integer = 0
            For i As Integer = 0 To grdTerminal.RowCount - 1

                ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
                If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

                    If grdTerminal(2, i).Value = Nothing Then
                        mintChNoBk(i) = 0
                    Else
                        mintChNoBk(i) = Val(grdTerminal(2, i).Value)
                    End If

                Else
                    mintChNoBk(i) = 0
                End If

                intCHNoBefore = Val(grdTerminal(2, i).Value)

                mintChNoBk_2(i) = 0 ''クリア
            Next

            ''チャンネルの連続にかかわらず、表示のまま退避
            ''但し２,３行を占有している場合は、行数分のCH Noを退避（2,3行目は非表示だが）
            'Ver2.0.2.2 Step数を合わす
            For i As Integer = 0 To grdTerminal.RowCount - 1 Step intRow

                If grdTerminal(2, i).Value <> Nothing Then

                    For j As Integer = 0 To intRow - 1
                        mintChNoBk_2(i + j) = Val(grdTerminal(2, i).Value)
                    Next

                End If

            Next


            'Ver2.0.0.0 2016.12.07 ADD
            '初期表示は10列目まで自動横スクロールさせておく=入力項目が初見できるように。
            grdTerminal.FirstDisplayedScrollingColumnIndex = 10

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : CH情報表示(出力チャンネル設定用）
    ' 返り値    : 0：OK　　-1：対象のチャンネルなし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 行番号
    '           : ARG3 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 出力チャンネル設定した時、一覧にチャンネル情報を表示する
    '           : CHデータのみ（論理出力CHは除く）
    '--------------------------------------------------------------------
    Private Function mSetChInfoChOut(ByVal hChNo As Integer, ByVal hRow As Integer, ByVal udtSetCH As gTypSetChInfo) As Integer

        Try
            Dim intDecimalP As Integer, p As Integer
            Dim strDecimalFormat As String, strValue As String
            Dim dblLowValue As Double, dblHiValue As Double


            For j As Integer = LBound(udtSetCH.udtChannel) To UBound(udtSetCH.udtChannel)

                With udtSetCH.udtChannel(j)

                    If .udtChCommon.shtChno = hChNo Then

                        ''Item Name
                        grdTerminal(3, hRow).Value = gGetString(.udtChCommon.strChitem)

                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then           ''<アナログ> ---------------------

                            ''Status
                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            End If

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeAnalog)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''Range
                            intDecimalP = .AnalogDecimalPosition     ''Decimal Position
                            strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                ''データ種別コード　2,3線式
                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString            ''Range Type 上位 Low

                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString     ''Range Type 上位 Low　+　下位 High

                                grdTerminal(5, hRow).Value = strValue        ''Range Type

                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogK _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA Then

                                ''データ種別コード　K, 1-5 V, 4-20 mA
                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                grdTerminal(5, hRow).Value = strValue    ''Range Type
                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeDigital Then      ''<デジタル> ---------------------

                            ''Status
                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                strValue = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                'Ver2.0.7.L
                                'If strValue.Length > 8 Then
                                If LenB(strValue) > 8 Then
                                    'Ver2.0.7.L
                                    'grdTerminal(4, hRow).Value = strValue.Substring(0, 8).Trim & "/" & strValue.Substring(8).Trim
                                    grdTerminal(4, hRow).Value = MidB(strValue, 0, 8).Trim & "/" & MidB(strValue, 8).Trim
                                Else
                                    grdTerminal(4, hRow).Value = Trim(strValue)
                                End If
                            End If

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeDigital)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then        ''<モーター> ---------------------

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeMotor)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''Status
                            ''JACOM(MO)追加、表示を「STOP/RUN」→「RUN」に変更   2013.08.07  K.Fujimoto
                            'Ver2.0.0.2 モーター種別増加 R Device ADD
                            If cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDevice Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom Or cmbDataType.SelectedValue = gCstCodeChDataTypeMotorDeviceJacom55 Or _
                               cmbDataType.SelectedValue = gCstCodeChDataTypeMotorRDevice Then
                                grdTerminal(4, hRow).Value = "RUN"
                            Else
                                strValue = cmbDataType.Text
                                p = strValue.IndexOf(":")
                                If p >= 0 Then
                                    strValue = strValue.Substring(p + 1)
                                    grdTerminal(4, hRow).Value = strValue
                                Else
                                    grdTerminal(4, hRow).Value = ""
                                End If
                            End If

                            'Dim strwk(7) As String
                            'Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)

                            'cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString   ''入力ステータス

                            'If cmbStatus.SelectedValue = gCstCodeChManualInputStatus Then
                            '    '手入力のステータスの場合、計測点個数は1
                            '    grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)  ''入力ステータス名称(手入力)
                            'Else
                            '    ''データ種別、ステータス種別により状態が変化する
                            '    If cmbDataType.SelectedValue >= gCstCodeChDataTypeMotorManRun And cmbDataType.SelectedValue <= gCstCodeChDataTypeMotorManRunJ Then
                            '        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                            '    Else
                            '        strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                            '    End If

                            '    grdTerminal(4, hRow).Value = strwk(0)
                            'End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then        ''<バルブ> ---------------------

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeValve)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeComposite Then    ''<コンポジット> ---------------

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeComposite)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then        ''<パルス> ---------------------

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypePulse)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            'Ver2.0.7.E PID
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePID Then           ''<PID> ---------------------

                            ''Status
                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                grdTerminal(4, hRow).Value = cmbStatus.Text
                            Else
                                grdTerminal(4, hRow).Value = gGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            End If

                            ''Data Type
                            Call gSetComboBox(cmbDataType, gEnmComboType.ctChListChannelListDataTypeAnalog)
                            cmbDataType.SelectedValue = .udtChCommon.shtData.ToString
                            grdTerminal(7, hRow).Value = cmbDataType.Text

                            ''Range
                            intDecimalP = .PidDecimalPosition     ''Decimal Position
                            strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                            If .udtChCommon.shtData >= gCstCodeChDataTypePID_3_Pt100_2 And _
                               .udtChCommon.shtData <= gCstCodeChDataTypePID_4_Pt100_3 Then

                                ''データ種別コード　2,3線式
                                dblLowValue = .PidRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString            ''Range Type 上位 Low

                                dblHiValue = .PidRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString     ''Range Type 上位 Low　+　下位 High

                                grdTerminal(5, hRow).Value = strValue        ''Range Type

                            Else

                                ''データ種別コード　K, 1-5 V, 4-20 mA
                                dblLowValue = .PidRangeLow / (10 ^ intDecimalP)
                                strValue = dblLowValue.ToString(strDecimalFormat)           ''Range  Low

                                dblHiValue = .PidRangeHigh / (10 ^ intDecimalP)
                                strValue += " - " & dblHiValue.ToString(strDecimalFormat)   ''Range Hi

                                grdTerminal(5, hRow).Value = strValue    ''Range Type
                            End If

                        End If

                        ''Unit
                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Or _
                           .udtChCommon.shtChType = gCstCodeChTypePulse Then

                            If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                grdTerminal(6, hRow).Value = cmbUnit.Text
                            Else
                                grdTerminal(6, hRow).Value = gGetString(.udtChCommon.strUnit)     ''特殊コード対応
                            End If

                        End If

                        Exit For

                    End If

                End With

            Next j

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 出力ャンネル設定ファイルの新規インデックスを獲得する
    ' 返り値    : インデックス
    ' 引き数    : ARG1 - (I ) 1:デジタル　2:アナログ
    ' 　　　    : ARG2 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 新規で採番する
    '--------------------------------------------------------------------
    Private Function mGetIndexChOutPut(ByVal intChKubun As Integer, _
                                       ByVal udtSet As gTypSetChOutput) As Integer

        Try

            Dim intIdx As Integer = 0
            Dim intStart As Integer = 0, intEnd As Integer

            'If intChKubun = 1 Then
            '    ''デジタル　512CH
            '    intStart = 0 : intEnd = 511

            'ElseIf intChKubun = 2 Then
            '    ''アナログ　64CH
            '    intStart = 512 : intEnd = 575
            'End If

            intStart = 0 : intEnd = 575

            For i As Integer = intStart To intEnd

                With udtSet.udtCHOutPut(i)

                    If .shtChid = 0 And .bytType = 0 Then
                        intIdx = i
                        Exit For
                    End If

                End With

            Next

            Return intIdx

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 論理出力設定ファイルのインデックスを獲得する(出力チャンネル設定用）
    ' 返り値    : インデックス
    ' 引き数    : ARG1 - (I ) 1:OR　2:AND
    '           : ARG2 - (I ) 出力チャンネル設定構造体のインデックス
    '           : ARG3 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 出力チャンネル設定構造体の中で何番目のAND(OR)かを判断する
    '--------------------------------------------------------------------
    'Private Function mGetIndexOrAnd(ByVal hintOrAnd As Integer, _
    '                                ByVal hintIndex As Integer, _
    '                                ByVal udtSet As gTypSetChOutput) As Integer

    '    Try

    '        Dim intCnt As Integer = 0

    '        For i As Integer = LBound(udtSet.udtCHOutPut) To hintIndex

    '            With udtSet.udtCHOutPut(i)

    '                If .bytType = gCstCodeFuOutputChTypeOr Or .bytType = gCstCodeFuOutputChTypeAnd Then
    '                    'If .bytType = hintOrAnd Then
    '                    intCnt += 1
    '                End If

    '            End With

    '        Next

    '        Return intCnt - 1

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Function

    '--------------------------------------------------------------------
    ' 機能      : 論理出力設定ファイルのインデックスを獲得する(出力チャンネル設定用）
    ' 返り値    : インデックス
    ' 引き数    : ARG1 - (I ) 1:OR　2:AND
    '           : ARG2 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 新規で採番する
    '--------------------------------------------------------------------
    Private Function mGetIndexOrAndNew(ByVal hintOrAnd As Integer, ByVal udtSet As gTypSetChOutput) As Integer

        Try

            Dim intIndex As Integer = 0

            ''1から順番に空きインデックスをサーチ
            For idx As Integer = 1 To gCstCntFuAndOrRecCnt

                intIndex = 0

                For i As Integer = LBound(udtSet.udtCHOutPut) To UBound(udtSet.udtCHOutPut)

                    With udtSet.udtCHOutPut(i)

                        If .bytType = gCstCodeFuOutputChTypeOr Or .bytType = gCstCodeFuOutputChTypeAnd Then

                            If idx = .shtChid Then
                                ''使用済み
                                intIndex = -1
                                Exit For
                            End If

                        End If

                    End With

                Next i

                If intIndex <> -1 Then
                    intIndex = idx
                    Exit For
                End If

            Next idx

            Return intIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示(Disp）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル情報データ構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplayDisp(ByVal udtSet As gTypSetChDisp)

        Try

            Dim intCnt As Integer = 0
            Dim strHeaderTextNow As String = ""
            Dim strHeaderTextPrev As String = ""

            With udtSet.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1)

                For i As Integer = 0 To UBound(.udtPinInfo)

                    If grdTerminal.RowCount - 1 < i Then Return

                    grdTerminal(14, i).Value = gGetString(.udtPinInfo(i).strCoreNoIn)
                    grdTerminal(15, i).Value = gGetString(.udtPinInfo(i).strCoreNoCom)
                    grdTerminal(11, i).Value = gGetString(.udtPinInfo(i).strWireMark)
                    grdTerminal(13, i).Value = gGetString(.udtPinInfo(i).strWireMarkClass)
                    grdTerminal(16, i).Value = gGetString(.udtPinInfo(i).strDest)
                    grdTerminal(17, i).Value = gGetString(.udtPinInfo(i).shtTerminalNo)

                    ''Type列に表示名称設定データのCHNOを表示する場合はコメントを外す（デバッグ用）
                    'strHeaderTextNow = grdTerminal.Rows(i).HeaderCell.Value
                    'If strHeaderTextNow <> strHeaderTextPrev Then
                    '    grdTerminal(0, i).Value = .udtPinInfo(intCnt).shtChid
                    '    strHeaderTextPrev = strHeaderTextNow
                    '    intCnt += 1
                    'End If


                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製(Disp）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructureDisp(ByVal udtSource As gTypSetChDisp, _
                                   ByRef udtTarget As gTypSetChDisp)

        Try
            Dim intChNoNew(63) As Integer
            Dim intCHNoBefore As Integer = 0
            Dim intFlag As Integer = 0, intFlagCH As Integer = 0, intFlagMove As Integer = 0

            ''比較用にCH Noを格納
            For i As Integer = 0 To grdTerminal.RowCount - 1

                If grdTerminal(2, i).Value = Nothing Then
                    intChNoNew(i) = 0
                Else
                    intChNoNew(i) = Val(grdTerminal(2, i).Value)
                End If

            Next
            'For i As Integer = 0 To grdTerminal.RowCount - 1

            '    ''チャンネルが連続してる場合（Motor, Valve)は先頭のみ
            '    If Val(grdTerminal(2, i).Value) <> intCHNoBefore Then

            '        If grdTerminal(2, i).Value = Nothing Then
            '            intChNoNew(i) = 0
            '        Else
            '            intChNoNew(i) = Val(grdTerminal(2, i).Value)
            '        End If

            '    Else
            '        intChNoNew(i) = 0
            '    End If

            '    intCHNoBefore = Val(grdTerminal(2, i).Value)
            'Next

            For i As Integer = 0 To UBound(udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo)

                intFlag = 0 : intFlagCH = 0 : intFlagMove = 0

                ''現CHが空欄、前CHに設定値が有る場合　→　CHを削除したと判断　
                ''  →　CH削除は認めないので設定は元のまま（変更した形跡がある場合は変更）
                ''　　　→　但し、前CHと同じCHが他の行にある場合は削除（CHを移動したと判断） 2011-6-9
                If mintChNoBk_2(i) <> 0 And intChNoNew(i) = 0 Then

                    ''移動していないか？
                    For j As Integer = 0 To UBound(intChNoNew)
                        If mintChNoBk_2(i) = intChNoNew(j) Then
                            intFlagMove = 1
                            Exit For
                        End If
                    Next

                    If intFlagMove = 0 Then

                        ''CH削除につき情報コピー必要　ver.1.4.0 2011.09.29 (コメント)
                        ''CHは元のまま
                        'intFlagCH = 1

                        With udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i)

                            ''変更した形跡があるか？
                            If .strCoreNoIn = "" And .strCoreNoCom = "" And .strWireMark = "" And .strWireMarkClass = "" And _
                               .strDest = "" And .shtTerminalNo = "0" Then

                                '端子台情報も元のまま
                                intFlag = 1

                            End If

                        End With

                    End If

                End If

                If intFlag = 0 Then

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoIn = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoIn

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoCom = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoCom

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMark = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMark

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMarkClass = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMarkClass

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strDest = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strDest

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtTerminalNo = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtTerminalNo

                End If

                If intFlagCH = 0 Then

                    udtTarget.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtChid = _
                    udtSource.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtChid

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較(Disp）
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２　NEW
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChDisp, ByVal udt2 As gTypSetChDisp) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo)

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoIn), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoIn)) Then Return False

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoCom), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strCoreNoCom)) Then Return False

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMark), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMark)) Then Return False

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMarkClass), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strWireMarkClass)) Then Return False

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strDest), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).strDest)) Then Return False

                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtTerminalNo), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtTerminalNo)) Then Return False

                '' 2011.08.17 下記比較処理がないと端子台設定(出力)のCHIDが設定されない
                ''↓↓↓　CH登録画面の方では、端子台設定がされていない場合は当構造体にはCHIDをセットしないのでイコールにならない場合がある ↓↓↓
                If Not gCompareString(Trim(udt1.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtChid), _
                                      Trim(udt2.udtChDisp(mTerminalData.FuNo).udtSlotInfo(mTerminalData.PortNo - 1).udtPinInfo(i).shtChid)) Then Return False
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較(CH OutPut, CHAndOr)
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２
    ' 　　　    : ARG3 - (I ) 構造体３
    ' 　　　    : ARG4 - (I ) 構造体４
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEqualsChOutPut(ByVal udt1 As gTypSetChOutput, ByVal udt2 As gTypSetChOutput, _
                                         ByVal udt3 As gTypSetChAndOr, ByVal udt4 As gTypSetChAndOr) As Boolean

        Try

            For i As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut)

                If udt1.udtCHOutPut(i).shtSysno <> udt2.udtCHOutPut(i).shtSysno Then Return False
                If udt1.udtCHOutPut(i).shtChid <> udt2.udtCHOutPut(i).shtChid Then Return False
                If udt1.udtCHOutPut(i).bytType <> udt2.udtCHOutPut(i).bytType Then Return False
                If udt1.udtCHOutPut(i).bytStatus <> udt2.udtCHOutPut(i).bytStatus Then Return False
                If udt1.udtCHOutPut(i).shtMask <> udt2.udtCHOutPut(i).shtMask Then Return False
                If udt1.udtCHOutPut(i).bytOutput <> udt2.udtCHOutPut(i).bytOutput Then Return False
                If udt1.udtCHOutPut(i).bytFuno <> udt2.udtCHOutPut(i).bytFuno Then Return False
                If udt1.udtCHOutPut(i).bytPortno <> udt2.udtCHOutPut(i).bytPortno Then Return False
                If udt1.udtCHOutPut(i).bytPin <> udt2.udtCHOutPut(i).bytPin Then Return False

            Next

            For i As Integer = LBound(udt3.udtCHOut) To UBound(udt3.udtCHOut)

                For j As Integer = LBound(udt3.udtCHOut(i).udtCHAndOr) To UBound(udt3.udtCHOut(i).udtCHAndOr)

                    If udt3.udtCHOut(i).udtCHAndOr(j).shtSysno <> udt4.udtCHOut(i).udtCHAndOr(j).shtSysno Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).shtChid <> udt4.udtCHOut(i).udtCHAndOr(j).shtChid Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).bytStatus <> udt4.udtCHOut(i).udtCHAndOr(j).bytStatus Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).shtMask <> udt4.udtCHOut(i).udtCHAndOr(j).shtMask Then Return False

                Next

            Next

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

            Dim i As Integer, cnt As Integer
            Dim str As String
            Dim cellStyle As New DataGridViewCellStyle

            mintEventCancelFlag = 1

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "tType" : Column1.ReadOnly = True
            'Column1.Visible = False 'Ver2.0.1.6 非表示化 Ver2.0.1.7 非表示化廃止
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "tSysNo" : Column2.ReadOnly = True
            'Column2.Visible = False 'Ver2.0.1.6 非表示化 Ver2.0.1.7 非表示化廃止
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtChNo"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "tItemName" : Column4.ReadOnly = True
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "tStatus" : Column5.ReadOnly = True
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "tRange" : Column6.ReadOnly = True
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "tUnit" : Column7.ReadOnly = True
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "tIns" : Column8.ReadOnly = True
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "tOutPutType" : Column9.ReadOnly = True
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "tOutPutMovement" : Column10.ReadOnly = True
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "tNo" : Column11.ReadOnly = True
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtCableMark1"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "tNo" : Column13.ReadOnly = True
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtCableMark2"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtCoreNo1"
            Dim Column16 As New DataGridViewTextBoxColumn : Column16.Name = "txtCoreNo2"
            Dim Column17 As New DataGridViewTextBoxColumn : Column17.Name = "txtCableDest"
            Dim Column18 As New DataGridViewComboBoxColumn : Column18.Name = "cmbTerminalNo"
            Column18.Visible = False

            Dim Column19 As New DataGridViewTextBoxColumn : Column19.Name = "txtChOutIndex" : Column19.ReadOnly = True
            Column19.Visible = False

            Dim Column20 As New DataGridViewTextBoxColumn : Column20.Name = "txtWork" : Column20.ReadOnly = True
            Column20.Visible = False

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdTerminal

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                .Columns.Add(Column16) : .Columns.Add(Column17) : .Columns.Add(Column18)
                .Columns.Add(Column19) : .Columns.Add(Column20)

                .AllowUserToResizeColumns = True   ''列幅の変更可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "T" : .Columns(0).Width = 15
                .Columns(1).HeaderText = "S" : .Columns(1).Width = 15
                .Columns(2).HeaderText = "CHNo" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "Item Name" : .Columns(3).Width = 200
                .Columns(4).HeaderText = "Status" : .Columns(4).Width = 120
                .Columns(5).HeaderText = "Range" : .Columns(5).Width = 100
                .Columns(6).HeaderText = "Unit" : .Columns(6).Width = 70
                .Columns(7).HeaderText = "Ins" : .Columns(7).Width = 140
                .Columns(8).HeaderText = "OutPut Type" : .Columns(8).Width = 80
                .Columns(9).HeaderText = "Movement" : .Columns(9).Width = 80
                .Columns(10).HeaderText = "No." : .Columns(10).Width = 40
                .Columns(11).HeaderText = "WIRE MARK" : .Columns(11).Width = 160
                .Columns(12).HeaderText = "No." : .Columns(12).Width = 40
                .Columns(13).HeaderText = "WIRE MARK(CLASS)" : .Columns(13).Width = 160
                .Columns(14).HeaderText = "CoreNoIn" : .Columns(14).Width = 60
                .Columns(15).HeaderText = "CoreNoCom" : .Columns(15).Width = 60
                .Columns(16).HeaderText = "DIST" : .Columns(16).Width = 160     '' Ver1.8.4  2015.11.27  DEST → DIST
                .Columns(17).HeaderText = "TerminalNo" : .Columns(17).Width = 85

                .Columns(18).HeaderText = "" : .Columns(18).Width = 50     ''非表示
                .Columns(19).HeaderText = "" : .Columns(19).Width = 40     ''非表示

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                'Ver2.0.1.6 左4列から5列へ変更
                .Columns(4).Frozen = True   ''左5列固定

                ''行
                Select Case mTerminalData.TypeCode
                    Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeDI     ''DO, DI(1行毎)
                        .RowCount = gCstFuSlotMaxDO + 1

                    Case gCstCodeFuSlotTypeAO                           ''AO(1行毎) ver1.4.0 2011.08.22
                        .RowCount = gCstFuSlotMaxAO + 1

                    Case gCstCodeFuSlotTypeAI_2                         ''2線式(1行毎) ver1.4.0 2011.08.22
                        .RowCount = gCstFuSlotMaxAI_2Line + 1

                    Case gCstCodeFuSlotTypeAI_3                         ''3線式(3行毎)
                        .RowCount = gCstFuSlotMaxAI_3Line * 3 + 1

                    Case gCstCodeFuSlotTypeAI_1_5, gCstCodeFuSlotTypeAI_K   ''1-5V, K(1行毎) ver1.4.0 2011.08.22
                        .RowCount = gCstFuSlotMaxAI_1_5 + 1

                    Case gCstCodeFuSlotTypeAI_4_20                      ''4-20mA(1行毎) ver1.4.0 2011.08.22
                        .RowCount = gCstFuSlotMaxAI_4_20 + 1

                End Select

                cellStyle.BackColor = gColorGridRowBack     ''グリッドの偶数色の背景色
                str = mTerminalData.FuName & mTerminalData.PortNo
                cnt = 1

                '' 3線式のみ3行表示　ver1.4.0 2011.08.22
                If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                    ''(3行毎)
                    For i = 0 To .RowCount - 3 Step 3
                        If (i Mod 3 = 0) Then
                            .Rows(i).HeaderCell.Value = str & cnt.ToString("00")
                            .Rows(i + 1).HeaderCell.Value = str & cnt.ToString("00")
                            .Rows(i + 2).HeaderCell.Value = str & cnt.ToString("00")
                            cnt += 1

                            .Rows(i + 2).DividerHeight = 1 ''太線
                            .Rows(i + 1).Cells(2).ReadOnly = True
                            .Rows(i + 2).Cells(2).ReadOnly = True

                        End If

                        ''行の背景色を変える
                        If (i Mod 6 = 0) Then
                            .Rows(i).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly

                            .Rows(i).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i).Cells(12).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).Cells(12).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).Cells(12).Style.BackColor = gColorGridRowBackReadOnly

                            ''書き込み可能セル
                            .Rows(i).Cells(2).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(2).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(2).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(11).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(11).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(11).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(13).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(13).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(13).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(14).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(14).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(14).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(15).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(15).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(15).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(16).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(16).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(16).Style.BackColor = gColorGridRowBack
                            .Rows(i).Cells(17).Style.BackColor = gColorGridRowBack : .Rows(i + 1).Cells(17).Style.BackColor = gColorGridRowBack : .Rows(i + 2).Cells(17).Style.BackColor = gColorGridRowBack

                        Else
                            .Rows(i).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly

                            .Rows(i).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i).Cells(12).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 1).Cells(12).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i + 2).Cells(12).Style.BackColor = gColorGridRowBackReadOnly

                            ''書き込み可能セル
                            .Rows(i).Cells(2).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(2).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(2).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(11).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(11).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(11).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(13).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(13).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(13).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(14).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(14).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(14).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(15).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(15).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(15).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(16).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(16).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(16).Style.BackColor = gColorGridRowBackBase
                            .Rows(i).Cells(17).Style.BackColor = gColorGridRowBackBase : .Rows(i + 1).Cells(17).Style.BackColor = gColorGridRowBackBase : .Rows(i + 2).Cells(17).Style.BackColor = gColorGridRowBackBase

                        End If

                    Next

                Else

                    ''(1行毎)
                    For i = 1 To .RowCount
                        .Rows(i - 1).HeaderCell.Value = str & i.ToString("00")


                        ''行の背景色を変える
                        If (i Mod 2 <> 0) Then
                            .Rows(i - 1).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly

                            .Rows(i - 1).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i - 1).Cells(12).Style.BackColor = gColorGridRowBackReadOnly

                            ''書き込み可能セル
                            .Rows(i - 1).Cells(2).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(11).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(13).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(14).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(15).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(16).Style.BackColor = gColorGridRowBack
                            .Rows(i - 1).Cells(17).Style.BackColor = gColorGridRowBack
                        Else
                            .Rows(i - 1).DefaultCellStyle.BackColor = gColorGridRowBackReadOnly

                            .Rows(i - 1).Cells(10).Style.BackColor = gColorGridRowBackReadOnly
                            .Rows(i - 1).Cells(12).Style.BackColor = gColorGridRowBackReadOnly

                            ''書き込み可能セル
                            .Rows(i - 1).Cells(2).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(11).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(13).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(14).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(15).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(16).Style.BackColor = gColorGridRowBackBase
                            .Rows(i - 1).Cells(17).Style.BackColor = gColorGridRowBackBase
                        End If

                    Next

                End If


                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 60
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Both

                .DefaultCellStyle.NullValue = ""

                ''Noを設定する
                cnt = 0
                For i = 1 To .RowCount
                    'Ver2.0.2.8 No採番を同行なら同じ番号に変更+3線式対応()
                    'cnt += 1 : .Rows(i - 1).Cells(10).Value = cnt.ToString
                    'cnt += 1 : .Rows(i - 1).Cells(12).Value = cnt.ToString
                    If mTerminalData.TypeCode = gCstCodeFuSlotTypeAI_3 Then
                        If i Mod 3 = 1 Then
                            cnt += 1
                        End If
                    Else
                        cnt += 1
                    End If
                    .Rows(i - 1).Cells(10).Value = cnt.ToString
                    .Rows(i - 1).Cells(12).Value = cnt.ToString


                    .Rows(i - 1).Cells(18).Value = -1   ''非表示エリア 初期値SET
                Next

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdTerminal)

                ''TerminalNoコンボ　初期値
                Call gSetComboBox(Column18, gEnmComboType.ctChTerminalListTerminalNo)

            End With

            mintEventCancelFlag = 0

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
    Private Sub mCopyStructure1(ByVal udtSource As gTypSetChOutput, _
                                ByRef udtTarget As gTypSetChOutput)

        Try

            For i As Integer = LBound(udtSource.udtCHOutPut) To UBound(udtSource.udtCHOutPut)

                udtTarget.udtCHOutPut(i).shtSysno = udtSource.udtCHOutPut(i).shtSysno
                udtTarget.udtCHOutPut(i).shtChid = udtSource.udtCHOutPut(i).shtChid
                udtTarget.udtCHOutPut(i).bytType = udtSource.udtCHOutPut(i).bytType
                udtTarget.udtCHOutPut(i).bytStatus = udtSource.udtCHOutPut(i).bytStatus
                udtTarget.udtCHOutPut(i).shtMask = udtSource.udtCHOutPut(i).shtMask
                udtTarget.udtCHOutPut(i).bytOutput = udtSource.udtCHOutPut(i).bytOutput
                udtTarget.udtCHOutPut(i).bytFuno = udtSource.udtCHOutPut(i).bytFuno
                udtTarget.udtCHOutPut(i).bytPortno = udtSource.udtCHOutPut(i).bytPortno
                udtTarget.udtCHOutPut(i).bytPin = udtSource.udtCHOutPut(i).bytPin

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure2(ByVal udtSource As gTypSetChAndOr, _
                                ByRef udtTarget As gTypSetChAndOr)

        Try

            For i As Integer = LBound(udtSource.udtCHOut) To UBound(udtSource.udtCHOut)

                For j As Integer = LBound(udtSource.udtCHOut(i).udtCHAndOr) To UBound(udtSource.udtCHOut(i).udtCHAndOr)

                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtSysno = udtSource.udtCHOut(i).udtCHAndOr(j).shtSysno
                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtChid = udtSource.udtCHOut(i).udtCHAndOr(j).shtChid
                    udtTarget.udtCHOut(i).udtCHAndOr(j).bytStatus = udtSource.udtCHOut(i).udtCHAndOr(j).bytStatus
                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtMask = udtSource.udtCHOut(i).udtCHAndOr(j).shtMask

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 文字列取得
    ' 返り値    : 変換後文字列
    ' 引き数    : ARG1 - (I ) 変換元文字列
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    '--------------------------------------------------------------------
    Public Function mGetString(ByVal strInput As String, _
                      Optional ByVal blnTrim As Boolean = True) As String

        Try

            Dim strRtn As String

            strRtn = strInput
            strRtn = Replace(strRtn, vbNullChar, "")

            If blnTrim Then
                'strRtn = Trim(strRtn)
                If strRtn <> Nothing Then
                    strRtn = strRtn.TrimEnd
                Else
                    strRtn = ""
                End If
            End If

            Return strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return strInput
        End Try

    End Function


#End Region

#Region "コメントアウト"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力可能列の周囲に線を描画する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub grdTerminal_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles grdTerminal.RowPostPaint

    '    Dim dgv As DataGridView = sender
    '    Dim startX As Integer = dgv.RowHeadersWidth + dgv.Columns(0).Width + dgv.Columns(1).Width
    '    Dim endX As Integer = dgv.RowHeadersWidth + dgv.Columns(0).Width + dgv.Columns(1).Width + dgv.Columns(2).Width
    '    'Dim startY As Integer = dgv.ColumnHeadersHeight
    '    Dim startY As Integer = 1
    '    Dim endY As Integer = e.RowBounds.Top + e.RowBounds.Height - 1
    '    Dim pen As New Pen(Color.SteelBlue, 3)

    '    ''If lblType1.Text = "AI(3LINE)" Or lblType1.Text = "AI(4-20mA)" Then
    '    ''Else
    '    ''    Exit Sub
    '    ''End If

    '    startX -= dgv.HorizontalScrollingOffset
    '    endX -= dgv.HorizontalScrollingOffset

    '    ''CH No
    '    e.Graphics.DrawLine(pen, startX, startY, endX, startY)      ''上
    '    e.Graphics.DrawLine(pen, startX, startY, startX, endY)      ''左
    '    e.Graphics.DrawLine(pen, endX, startY, endX, endY)          ''右
    '    If e.RowIndex = grdTerminal.RowCount - 1 Then
    '        e.Graphics.DrawLine(pen, startX, endY, endX, endY)      ''下
    '    End If

    '    ''Cable Mark, Core No, Cable Dest
    '    startX += dgv.Columns(2).Width + dgv.Columns(3).Width + dgv.Columns(4).Width + dgv.Columns(5).Width + _
    '              dgv.Columns(6).Width + dgv.Columns(7).Width + dgv.Columns(8).Width + dgv.Columns(9).Width
    '    endX = startX + dgv.Columns(10).Width + dgv.Columns(11).Width + dgv.Columns(12).Width + _
    '           dgv.Columns(13).Width + dgv.Columns(14).Width + dgv.Columns(15).Width + dgv.Columns(16).Width - 1

    '    e.Graphics.DrawLine(pen, startX, startY, endX, startY)      ''上
    '    e.Graphics.DrawLine(pen, startX, startY, startX, endY)      ''左
    '    e.Graphics.DrawLine(pen, endX, startY, endX, endY)          ''右
    '    If e.RowIndex = grdTerminal.RowCount - 1 Then
    '        e.Graphics.DrawLine(pen, startX, endY, endX, endY)      ''下
    '    End If

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub txtCableMark_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCableMark.KeyPress

    '    Try

    '        e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    'Private Sub txtCableClass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCableClass.KeyPress

    '    Try

    '        e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    'Private Sub txtDistnation_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDistnation.KeyPress

    '    Try

    '        e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub


#End Region


    Private Sub grdTerminal_ColumnHeaderMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTerminal.ColumnHeaderMouseDoubleClick

    End Sub

    ' ///////////// Ver1.7.6  2015.11.06 追加

    '----------------------------------------------------------------------------
    ' 機能説明  ： 行ヘッダ　ダブルクリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_RowHeaderMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTerminal.RowHeaderMouseDoubleClick

        If cmdOutput.Enabled = True Then        ' Output Setﾎﾞﾀﾝが使用可能時のみ対応
            Call OutputSetting()
        End If
    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： Output設定画面表示
    '               ボタンクリック時の処理を関数にまとめました
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub OutputSetting()
        Try

            If grdTerminal.SelectedCells.Count = 0 Then Exit Sub

            Dim row As Integer = grdTerminal.CurrentRow.Index
            Dim intChOutIndex As Integer, intOrAndIndex As Integer
            Dim flgOrAndBK As Integer = 0
            Dim intStep1 As Integer = 0, intStep2 As Integer = 0
            Dim strChStart As String = "", strChEnd As String = ""
            Dim intValue As Integer

            If mTerminalData.TypeCode = gCstCodeFuSlotTypeDO Then  ''DO ------------------------------

                ''デジタル出力情報
                If grdTerminal(0, row).Value = mCstCodeFunction Or grdTerminal(18, row).Value = "-1" Then    ''非表示のインデックス格納エリア

                    ''新規
                    intChOutIndex = mGetIndexChOutPut(1, mudtSetCHOutPutNew)

                    mDoDetail.Sysno = 0
                    mDoDetail.Chid = 0
                    mDoDetail.Type = 0
                    mDoDetail.Status = 0
                    mDoDetail.Mask = 0
                    mDoDetail.Output = 0

                    mDoDetail.Funo = mTerminalData.FuNo
                    mDoDetail.Portno = mTerminalData.PortNo
                    mDoDetail.Pin = Integer.Parse(grdTerminal.Rows(row).HeaderCell.Value.ToString.Substring(mTerminalData.FuName.Length + 1))

                Else

                    intChOutIndex = grdTerminal(18, row).Value    ''出力CH設定構造体インデックス

                    With mudtSetCHOutPutNew.udtCHOutPut(intChOutIndex)

                        mDoDetail.Sysno = .shtSysno
                        mDoDetail.Chid = gGet2Byte(.shtChid).ToString("0000")
                        mDoDetail.Type = .bytType

                        cmbOutputMovement.SelectedValue = .bytStatus.ToString
                        mDoDetail.Status = cmbOutputMovement.SelectedValue

                        mDoDetail.Mask = .shtMask

                        cmbChOutType.SelectedValue = .bytOutput.ToString
                        mDoDetail.Output = cmbChOutType.Text

                        mDoDetail.Funo = mTerminalData.FuNo
                        mDoDetail.Portno = mTerminalData.PortNo
                        mDoDetail.Pin = Integer.Parse(grdTerminal.Rows(row).HeaderCell.Value.ToString.Substring(mTerminalData.FuName.Length + 1))

                        ''論理出力チャンネルの場合は論理出力データも渡す
                        If .bytType = gCstCodeFuOutputChTypeOr Or .bytType = gCstCodeFuOutputChTypeAnd Then

                            ''論理出力構造体のインデックスをGETする
                            intOrAndIndex = .shtChid - 1

                            For i = 0 To 23

                                mOrAndDetail(i).Sysno = mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i).shtSysno
                                mOrAndDetail(i).Chid = gGet2Byte(mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i).shtChid)
                                mOrAndDetail(i).Status = mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i).bytStatus
                                mOrAndDetail(i).Mask = mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i).shtMask

                            Next i

                            flgOrAndBK = .bytType

                        End If

                    End With

                End If

                ''=====================================================================================================
                ''デジタル出力情報詳細画面表示 ========================================================================
                If frmChOutputDoDetail.gShowTerminal(intChOutIndex, mDoDetail, mOrAndDetail, Me) = 0 Then

                    If mDoDetail.Chid <> 0 Then

                        ''前回と同じ
                        If mDoDetail.Type = gCstCodeFuOutputChTypeCh And flgOrAndBK = gCstCodeFuOutputChTypeCh Then intStep1 = 1
                        If mDoDetail.Type = gCstCodeFuOutputChTypeOr And flgOrAndBK = gCstCodeFuOutputChTypeOr Then intStep1 = 1
                        If mDoDetail.Type = gCstCodeFuOutputChTypeAnd And flgOrAndBK = gCstCodeFuOutputChTypeAnd Then intStep1 = 1

                        ''AndとORが逆だが前回と同じ
                        If mDoDetail.Type = gCstCodeFuOutputChTypeOr And flgOrAndBK = gCstCodeFuOutputChTypeAnd Then intStep1 = 1
                        If mDoDetail.Type = gCstCodeFuOutputChTypeAnd And flgOrAndBK = gCstCodeFuOutputChTypeOr Then intStep1 = 1

                        ''前回AndかOrがあったが今回はなし
                        If mDoDetail.Type = gCstCodeFuOutputChTypeCh And (flgOrAndBK = gCstCodeFuOutputChTypeOr Or flgOrAndBK = gCstCodeFuOutputChTypeAnd) Then intStep1 = 3

                        ''前回はなかったが今回はAndかOrがある
                        If mDoDetail.Type <> gCstCodeFuOutputChTypeCh And (flgOrAndBK = gCstCodeFuOutputChTypeCh) Then intStep1 = 2 ''新規

                        If intStep1 = 1 Then

                            If mDoDetail.Type <> gCstCodeFuOutputChTypeCh Then

                                ''前と同じ論理出力チャンネルなので同じ位置に上書き
                                For i = 0 To 23
                                    With mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i)

                                        .shtSysno = mOrAndDetail(i).Sysno

                                        intValue = mOrAndDetail(i).Chid
                                        .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                                        intValue = mOrAndDetail(i).Mask
                                        .shtMask = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                                        cmbOutputMovement.SelectedValue = mOrAndDetail(i).Status
                                        .bytStatus = cmbOutputMovement.SelectedValue

                                    End With
                                Next i

                            End If

                        ElseIf intStep1 = 2 Then
                            ''新規で論理出力チャンネル
                            If mDoDetail.Type <> gCstCodeFuOutputChTypeCh Then

                                ''論理出力構造体のインデックス(新規)をGETする 1～64(一杯の場合は-1)
                                intOrAndIndex = mGetIndexOrAndNew(mDoDetail.Type, mudtSetCHOutPutNew)

                                ''論理出力設定ファイルが一杯か？
                                If intOrAndIndex < 0 Then
                                    MsgBox("The logical output configuration file is full.", MsgBoxStyle.Exclamation, "DO")
                                    Exit Sub
                                End If

                                intOrAndIndex = intOrAndIndex - 1

                                For i = 0 To 23
                                    With mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i)

                                        .shtSysno = mOrAndDetail(i).Sysno

                                        intValue = mOrAndDetail(i).Chid
                                        .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                                        intValue = mOrAndDetail(i).Mask
                                        .shtMask = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                                        'cmbOutputMovement.SelectedValue = mOrAndDetail(i).Status
                                        '.bytStatus = cmbOutputMovement.SelectedValue

                                        .bytStatus = mOrAndDetail(i).Status

                                    End With
                                Next i

                            End If


                        ElseIf intStep1 = 3 Then
                            ''前は論理出力チャンネルだったので、痕跡を消す
                            For i = 0 To 23
                                With mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i)
                                    .shtSysno = 0
                                    .shtChid = 0
                                    .bytStatus = 0
                                    .shtMask = 0
                                End With
                            Next i

                        End If

                        ''デジタル出力情報更新
                        With mudtSetCHOutPutNew.udtCHOutPut(intChOutIndex)

                            .shtSysno = mDoDetail.Sysno

                            mDoDetail.Chid = IIf(mDoDetail.Chid = -1, 0, mDoDetail.Chid)    ''@@@@@@@

                            If mDoDetail.Type = gCstCodeFuOutputChTypeCh Then
                                intValue = mDoDetail.Chid
                                .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)
                            Else
                                intValue = intOrAndIndex + 1    ''
                                .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)
                            End If

                            .bytType = mDoDetail.Type

                            cmbOutputMovement.SelectedValue = mDoDetail.Status
                            .bytStatus = cmbOutputMovement.SelectedValue

                            intValue = mDoDetail.Mask
                            .shtMask = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                            cmbChOutType.Text = mDoDetail.Output
                            .bytOutput = cmbChOutType.SelectedValue

                            .bytFuno = mDoDetail.Funo
                            .bytPortno = mDoDetail.Portno
                            .bytPin = mDoDetail.Pin

                        End With

                        ''一覧を更新
                        If mudtSetCHOutPutNew.udtCHOutPut(intChOutIndex).bytOutput = 0 Then
                            ''Invalid
                            grdTerminal(2, row).Value = ""  ''← grdTerminal_CellValueChangedイベントが発生してクリア処理を行う

                        Else
                            mintEventCancelFlag = 1

                            grdTerminal(0, row).Value = mCstCodeChOutPut    ''出力CH set
                            grdTerminal(1, row).Value = mDoDetail.Sysno

                            grdTerminal(2, row).Value = "" : grdTerminal(3, row).Value = "" : grdTerminal(4, row).Value = ""
                            grdTerminal(5, row).Value = "" : grdTerminal(6, row).Value = "" : grdTerminal(7, row).Value = ""

                            If mDoDetail.Type = gCstCodeFuOutputChTypeCh Then
                                ''CH情報表示
                                grdTerminal(2, row).Value = IIf(mDoDetail.Chid = 0, "", Val(mDoDetail.Chid).ToString("0000"))

                                Call mSetChInfoChOut(mDoDetail.Chid, row, gudt.SetChInfo)

                            Else
                                ''論理出力CH用表示
                                grdTerminal(2, row).Value = "---"

                                ''Item Name
                                For i = 0 To 23
                                    strChStart = gGet2Byte(mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(0).shtChid)

                                    With mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(i)
                                        If gGet2Byte(.shtChid) <> 0 Then strChEnd = gGet2Byte(.shtChid)
                                    End With
                                Next i
                                grdTerminal(3, row).Value = "CH No. " & strChStart & " - CH No. " & strChEnd
                            End If

                            cmbChOutType.SelectedValue = mudtSetCHOutPutNew.udtCHOutPut(intChOutIndex).bytOutput.ToString
                            grdTerminal(8, row).Value = cmbChOutType.Text

                            '' ver.1.4.5 2012.07.03 論理出力設定時は先頭CHの状態を表示
                            If mDoDetail.Type = gCstCodeFuOutputChTypeCh Then
                                cmbOutputMovement.SelectedValue = mudtSetCHOutPutNew.udtCHOutPut(intChOutIndex).bytStatus
                                grdTerminal(9, row).Value = cmbOutputMovement.Text
                            Else
                                cmbOutputMovement.SelectedValue = mudtSetCHAndOrNew.udtCHOut(intOrAndIndex).udtCHAndOr(0).bytStatus
                                grdTerminal(9, row).Value = cmbOutputMovement.Text
                            End If


                            grdTerminal(18, row).Value = intChOutIndex
                        End If

                        mintEventCancelFlag = 0

                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    '  ｳｨﾝﾄﾞｳｻｲｽﾞ変更完了時
    '
    '   Ver1.9.3 2016.01.16 ｳｨﾝﾄﾞｳｻｲｽﾞを保持するように変更
    '----------------------------------------------------------------------------
    Private Sub frmChTerminalDetail_ResizeEnd(sender As Object, e As System.EventArgs) Handles Me.ResizeEnd

        m_FuDetailWndH = Me.Height
        m_FuDetailWndW = Me.Width

    End Sub

    '----------------------------------------------------------------------------
    '  aの端子選択
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Sub cmbDoTerm_a_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoTerm_a.SelectedIndexChanged

        If cmbDoTerm_a.SelectedValue = "2" Then '2はTMDO
            cmbDoTerm_b.SelectedIndex = 0
            cmbDoTerm_b.Enabled = False
        Else
            cmbDoTerm_b.Enabled = True
        End If

    End Sub

    '----------------------------------------------------------------------------
    '  cの端子選択
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Sub cmbDoTerm_c_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoTerm_c.SelectedIndexChanged

        If cmbDoTerm_c.SelectedValue = "2" Then '2はTMDO
            cmbDoTerm_d.SelectedIndex = 0
            cmbDoTerm_d.Enabled = False
        Else
            cmbDoTerm_d.Enabled = True
        End If

    End Sub

    '----------------------------------------------------------------------------
    '  DO端子設定の保存
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Sub mCalcDoTermSetValue()

        Dim shtAtermValue As UShort
        Dim shtBtermValue As UShort
        Dim shtCtermValue As UShort
        Dim shtDtermValue As UShort
        Dim shtTermSetSumValue As UShort

        'A端子コンボボックスがFalse = M03A(Select)以外の基板の場合なので処理しないで終了
        If cmbDoTerm_a.Enabled = False Then
            Exit Sub
        End If

        shtAtermValue = CUShort(cmbDoTerm_a.SelectedValue) * 10
        shtBtermValue = CUShort(cmbDoTerm_b.SelectedValue) * 100
        shtCtermValue = CUShort(cmbDoTerm_c.SelectedValue) * 1000
        shtDtermValue = CUShort(cmbDoTerm_d.SelectedValue) * 10000

        shtTermSetSumValue = shtAtermValue + shtBtermValue + shtCtermValue + shtDtermValue

        frmChTerminalList.mSetDoTermSetting(shtTermSetSumValue, mTerminalData.FuNo, mTerminalData.PortNo - 1)

    End Sub

    '----------------------------------------------------------------------------
    '  DO端子設定 端子毎の設定取得
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Function mGetFuDoTermSet(TypeCode As Short, term As Integer) As Integer

        Dim UshtSetValue As UShort
        Dim index As Integer = 0
        Dim a As Integer

        UshtSetValue = Convert.ToUInt16(TypeCode.ToString("X4"), 16)

        If UshtSetValue >= 0 And UshtSetValue < 20 Then
            Return index
        End If

        Select Case term
            Case 0
                UshtSetValue = UshtSetValue Mod 100
                UshtSetValue = UshtSetValue / 10
            Case 1
                If UshtSetValue < 100 Then
                    Return 0
                End If
                UshtSetValue = UshtSetValue Mod 1000
                a = UshtSetValue
                UshtSetValue = UshtSetValue Mod 100
                UshtSetValue = a - UshtSetValue
                UshtSetValue = UshtSetValue / 100
            Case 2
                If UshtSetValue < 1000 Then
                    Return 0
                End If
                UshtSetValue = UshtSetValue Mod 10000
                a = UshtSetValue
                UshtSetValue = UshtSetValue Mod 1000
                UshtSetValue = a - UshtSetValue
                UshtSetValue = UshtSetValue / 1000
            Case 3
                If UshtSetValue < 10000 Then
                    Return 0
                End If
                a = UshtSetValue
                UshtSetValue = UshtSetValue Mod 10000
                UshtSetValue = a - UshtSetValue
                UshtSetValue = UshtSetValue / 10000
            Case Else


        End Select

        If UshtSetValue >= 0 And UshtSetValue <= 5 Then
            index = UshtSetValue
            If term = 0 Or term = 2 Then
                If index > 0 Then
                    index = index - 1
                End If
            Else
                If index > 0 Then
                    index = index - 2
                End If
            End If
        End If

        Return index

    End Function

    '----------------------------------------------------------------------------
    '  DO端子設定 端子毎の設定変更箇所有無のチェック
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Function mChkFuDoTerminal(FuNo As Integer, PortNo As Integer) As Boolean

        Return frmChTerminalList.mChkFuDoTermSetting(FuNo, PortNo)

    End Function

    '----------------------------------------------------------------------------
    '  DO端子設定 端子毎の設定変更箇所のセーブ
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Sub mSetFuDoTerminal()
        frmChTerminalList.mSaveFuDoTermSetting()
    End Sub

End Class
