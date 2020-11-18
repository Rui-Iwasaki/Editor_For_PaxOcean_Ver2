Imports System.Runtime.InteropServices
Imports System
Imports System.IO
Public Class frmMenuMain

#Region "変数定義"

    Private mudtFileMode As gEnmFileMode

    'Ver2.0.1.5 画面キャプション用変数
    Private mstrFormName As String
#End Region

    Private FileInfomation As gTypFileInfo

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmMenuMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim fileNo As Integer = FreeFile()

            ''バージョン情報表示
            Me.Text = "Editor ver." & gGetVersionChar()
            mstrFormName = Me.Text

            '内外取得 
            gintNaiGai = gGetNaiGaiChar()

            '■外販
            '外販の場合、22kコンバータ,ControlUseNotUse,DataForwardTable,PIDtrendSetは消す
            If gintNaiGai = 1 Then
                cmdFile5.Visible = False
                cmdChannel7.Visible = False
                cmdChannel9.Visible = False
                btnPIDtrendSET.Visible = False
            End If


            ''編集対象を設定するまでは使用不可
            Call mSetEnableButton(False)

            ''ファイル初期情報設定
            With gudtFileInfo
                .strFilePath = My.Settings.SelectPath
                .strFileName = My.Settings.SelectFile
                .strFileVersion = My.Settings.SelectVersion
            End With

            '実行場所取得
            AppPass = gGetAppPath()
            AppPassTXT = AppPass + "\Usefilefolderpass.txt"

            'FileOpen(fileNo, AppPassTxt, OpenMode.Output)
            'Print(fileNo, AppPass)
            'FileClose(fileNo)

            ''出力構造体初期化
            Call gInitOutputStructure(gudt)

            Call gSetComboBox(CmbTag, gEnmComboType.ctSysSystemTag)     '' Ver1.11.8.6 2016.11.10 ﾀｸﾞ設定有無をｺﾝﾎﾞﾎﾞｯｸｽに変更
            Call gSetComboBox(CmbAlmLvl, gEnmComboType.ctSysSystemAlmLevel)     '' Ver1.11.8.6 2016.11.10 ｱﾗｰﾑ設定有無をｺﾝﾎﾞﾎﾞｯｸｽに変更



            'Ver2.0.0.2 比較項目選択フラグ初期化
            Dim i As Integer = 0
            For i = 0 To UBound(gCompareChk) Step 1
                gCompareChk(i) = True
            Next i
            'Detail
            For i = 0 To UBound(gCompareChk_1_Common) Step 1
                gCompareChk_1_Common(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_2_Analog) Step 1
                gCompareChk_2_Analog(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_3_Digital) Step 1
                gCompareChk_3_Digital(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_4_System) Step 1
                gCompareChk_4_System(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_5_Motor) Step 1
                gCompareChk_5_Motor(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_6_DIDO) Step 1
                gCompareChk_6_DIDO(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_7_AIDO) Step 1
                gCompareChk_7_AIDO(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_8_AIAO) Step 1
                gCompareChk_8_AIAO(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_9_Comp) Step 1
                gCompareChk_9_Comp(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_10_Puls) Step 1
                gCompareChk_10_Puls(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_11_RunHour) Step 1
                gCompareChk_11_RunHour(i) = True
            Next i
            For i = 0 To UBound(gCompareChk_12_Pid) Step 1
                gCompareChk_12_Pid(i) = True
            Next i
            'Ver2.0.0.7 比較項目選択ﾌフラグ初期化(大項目)
            gCompareChkBIG(0) = True    '全比較はON
            gCompareChkBIG(1) = False   '計測点のみ比較はOFF
            gCompareChkBIG(2) = False   '端子表のみ比較はOFF
            gCompareChkBIG(3) = True    'FUアドレスを付けるはON
            gCompareChkBIG(4) = True    '関連テーブルを検索するはON

            'Ver2.0.1.3 計測点一覧入力画面から印刷へ用変数の初期化
            gintChPrintGrNo = -1
            'Ver2.0.1.3 端子表入力画面から印刷へ用変数の初期化
            gintTermFuNo = -1
            gintTermSlotNo = -1


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'PID TREND SETボタン
    Private Sub btnPIDtrendSET_Click(sender As System.Object, e As System.EventArgs) Handles btnPIDtrendSET.Click
        Try
            'メニューに1件もPIDコントロールが無いなら画面遷移しない
            If fnChkCountPID() <= 0 Then
                MessageBox.Show("No PID CONTROL. Please Main Menu Set.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Call mSetShowDispButtonEnable(False, False)
            Call frmToolPIDtrend.gShow()
            Call mSetShowDispButtonEnable(True, False)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Newボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル新規作成画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdFile1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile1.Click

        Try

            ''現在の設定が更新されている場合はメッセージ表示
            If gblnUpdateAll Then
                If MessageBox.Show("There is an update file." & vbNewLine & "Do you make new file?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Return
                End If
            End If

            With gudtFileInfo

                ''ファイルセレクト画面表示
                If frmFileSelect.gShow(gEnmFileMode.fmNew, gudtFileInfo, gudt, False, False, False, False) = 1 Then

                    ''ファイルモード設定
                    mudtFileMode = gEnmFileMode.fmNew

                    ''出力構造体初期化（2010/12/09 処理の順番を変更）
                    ''mDispFileInfo で「船番表示」を行っているため、ファイル情報を表示する前に構造体の初期化を実施。
                    ''（変更前：「ファイル情報を表示」後に構造体の初期化を実施）

                    '▼▼▼ 20110705 NEW時iniファイルフォルダに保存してあるMainMenu.datを読み込む ▼▼▼▼▼▼▼▼▼
                    ''FileSelect画面のOKボタンで初期化するのでここでは初期化しない
                    'Call gInitOutputStructure(gudt)
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    ''ファイル情報を表示
                    Call mDispFileInfo(gudtFileInfo)

                    ''コントロール使用可/不可設定
                    Call mSetEnableButton(True)

                    ''全体更新フラグ初期化
                    gblnUpdateAll = False

                    ''Renameボタン使用不可
                    cmdFile4.Enabled = False

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Openボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル更新画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdFile2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile2.Click

        Try

            ''現在の設定が更新されている場合はメッセージ表示
            If gblnUpdateAll Then
                If MessageBox.Show("There is an update file." & vbNewLine & "Do you open other file?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Return
                End If

                'Ver2.0.2.0 新規で保存されていない場合はﾒｯｾｰｼﾞを出して処理させない
                Dim strPath As String = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
                strPath = System.IO.Path.Combine(strPath, gCstFolderNameSave)
                If System.IO.Directory.Exists(strPath) = False Then
                    MessageBox.Show("There is an update file." & vbNewLine & "Please Save", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If



            With gudtFileInfo

                ''ファイルセレクト画面表示
                If frmFileSelect.gShow(gEnmFileMode.fmEdit, gudtFileInfo, gudt, True, False, False, False) <> 0 Then

                    ''ファイルモード設定
                    mudtFileMode = gEnmFileMode.fmEdit

                    ''ファイル情報を表示
                    Call mDispFileInfo(gudtFileInfo)

                    ''コントロール使用可/不可設定
                    Call mSetEnableButton(True)

                    'T.Ueki
                    If .blnVersionUP = True Then
                        ''全体更新フラグ初期化させない
                        gblnUpdateAll = True
                        'Ver2.0.4.9
                        'Excel読込の場合は更新ﾌﾗｸﾞを更新
                        If gblExcelInDo = True Then
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1
                            gblExcelInDo = False

                            'Ver2.0.5.9 端子表印字も更新ﾌﾗｸﾞを立てる
                            gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                        End If
                    Else
                        ''全体更新フラグ初期化
                        gblnUpdateAll = False
                        'Ver2.0.3.6
                        'Excel読込の場合は更新ﾌﾗｸﾞを更新
                        If gblExcelInDo = True Then
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytChannel = 1
                            gblExcelInDo = False

                            'Ver2.0.5.9 端子表印字も更新ﾌﾗｸﾞを立てる
                            gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                        End If
                    End If

                    ''Renameボタン使用可
                    cmdFile4.Enabled = True

                    'Ver2.0.3.8 Excelﾛｸﾞ
                    If gstrChSearchLog <> "" Then
                        'Excelエラーがあればﾀﾞｲｱﾛｸﾞ表示
                        frmToolChkChListLog.Text = "Excel Input Error Log"
                        frmToolChkChListLog.TopMost = False
                        frmToolChkChListLog.Show()
                    End If

                End If

            End With

            ' 2015.10.22 Ver1.7.5  ﾀｸﾞ表示ﾓｰﾄﾞ追加
            '' Ver1.11.8.6 2016.11.10 ｺﾝﾎﾞﾎﾞｯｸｽに変更
            CmbTag.SelectedValue = gudt.SetSystem.udtSysOps.shtTagMode

            ' 2015.11.12 Ver1.7.8  ﾛｲﾄﾞ表示ﾓｰﾄﾞ追加
            '' Ver1.11.8.6 2016.11.10 ｺﾝﾎﾞﾎﾞｯｸｽに変更
            CmbAlmLvl.SelectedValue = gudt.SetSystem.udtSysOps.shtLRMode

            'Ver2.0.7.4
            '基板ﾊﾞｰｼﾞｮﾝ印刷する、しないはここで指定 DEL
            'If gudt.SetSystem.udtSysOps.shtTerVer = 1 Then
            '    chkTerVer.Checked = True
            'Else
            '    chkTerVer.Checked = False
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル保存画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdFile3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile3.Click

        Try

            ''保存を実行した場合
            If mSaveFile(False) <> 0 Then

                ''ファイルモード更新
                mudtFileMode = gEnmFileMode.fmEdit

                ''設定値保存
                My.Settings.SelectVersion = gudtFileInfo.strFileVersion
                Call My.Settings.Save()

                ''ファイル情報を表示
                Call mDispFileInfo(gudtFileInfo)

                ''全体更新フラグ初期化
                gblnUpdateAll = False

                ''Renameボタン使用可
                cmdFile4.Enabled = True

                'Ver2.0.3.8
                'Excel吐き出し時にエラーがあればｳｨﾝﾄﾞｳ表示
                If gstrChSearchLog <> "" Then
                    frmToolChkChListLog.Text = "Excel Output Error Log"
                    frmToolChkChListLog.TopMost = False
                    frmToolChkChListLog.Show()
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Renameボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル名称変更画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdFile4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile4.Click

        Try

            With gudtFileInfo

                ''ファイルセレクト画面表示
                If frmFileSelect.gShow(gEnmFileMode.fmRename, gudtFileInfo, gudt, False, False, False, False) = 1 Then

                    ''ファイル情報を表示
                    Call mDispFileInfo(gudtFileInfo)

                    ''コントロール使用可/不可設定
                    Call mSetEnableButton(True)

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 22KConverterボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コンバート画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmd22KConverter1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile5.Click

        Try

            ''現在の設定が更新されている場合はメッセージ表示
            If gblnUpdateAll Then
                If MessageBox.Show("There is an update file." & vbNewLine & "Do you make new file?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Return
                End If
            End If

            frmFileConvert.ShowDialog()
            'frmFileConvert_Y.ShowDialog()

            ''ファイル情報を表示
            Call mDispFileInfo(gudtFileInfo)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 基本システム設定関連画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 基本システム設定関連画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdSystem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem1.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmSysSystem.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdSystem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem2.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmSysFcu.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdSystem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem3.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmSysOps.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdSystem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem4.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmSysPrinter.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdSystem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem5.Click

        Try
            '' 2014.02.03 GWS設定追加
            Call mSetShowDispButtonEnable(False, False)
            Call frmSysGws.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル関連画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : チャンネル関連画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdChannel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel1.Click

        Try

            Call mSetShowDispButtonEnable(False, True)
            Call frmChListViewGroup.gShow()
            Call mSetShowDispButtonEnable(True, True)

            'Call frmChListViewGroup.ShowDialog()

            ''船番表示更新
            Call mDispShipNo()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel2.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChTerminalList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdComposite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComposite.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChCompositeList.gShow1(False, 0, 0, gEnmCompositeEditType.cetNone)
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel3.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChOutputSelect.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel4.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChGroupReposeList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel5.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChRunHour.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel6.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChExhGusGroup.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel7.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChControlUseNotuseList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel8.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            '■外販
            '外販の場合、ｺｰﾙする画面は外販専用とする
            If gintNaiGai = 1 Then
                Call frmChSioList_GAI.gShow()
            Else
                Call frmChSioList.gShow()
            End If

            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel9.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChDataForwardTableList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel10.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChDataSaveTableList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChannel12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannel12.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmChExtLanList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    '--------------------------------------------------------------------
    ' 機能      : シーケンス設定画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : シーケンス設定画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdSequence1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSequence1.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            '■外販
            '外販の場合、外販専用の画面を起動
            If gintNaiGai = 1 Then
                frmSeqSetSequenceList_GAI.gShow()
            Else
                frmSeqSetSequenceList.gShow()
            End If


            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OPS関連画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : OPS関連画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdOPS1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS1.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmOpsScreenTitle.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOPS2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS2.Click
        Try

            Call mSetShowDispButtonEnable(False, False)
            '■外販
            '外販の場合、外販専用のMenu設定画面を起動
            If gintNaiGai = 1 Then
                Call frmOpsPulldownMenu_GAI.gShow()
            Else
                Call frmOpsPulldownMenu.gShow()
            End If

            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOPS3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS3.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmOpsGraphList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOPS4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS4.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmOpsLogFormatList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOPS5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS5.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmOpsSelectionMenuList.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOPS6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOPS6.Click

        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmOpsGwsCh.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 延長警報関連画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 延長警報関連画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdExtAlarm1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExtAlarm1.Click

        Try

            Call mSetShowDispButtonEnable(False, False)

            '■外販
            '外販の場合、ｺｰﾙする画面は外販専用とする
            If gintNaiGai = 1 Then
                Call frmExtMenu_GAI.gShow()
            Else
                Call frmExtMenu.gShow()
            End If

            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 印刷関連画面ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  :印刷関連画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdPrint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint1.Click

        Try

            frmPrtChannel.ShowDialog()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPrint2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint2.Click

        Try

            frmPrtTerminal.ShowDialog()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPrint3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint3.Click

        Try

            frmPrtLocalUnit.ShowDialog()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPrint4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint4.Click

        Try

            frmPrtOverView.ShowDialog()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPrint5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint5.Click

        Try

            frmPrtGraph.ShowDialog()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コンパイラボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コンパイラ画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdCompile1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompile1.Click

        Try

            Call frmCmpCompier.gShow(gudtFileInfo, gEnmCompileType.cpCompile)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdErrorCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdErrorCheck.Click

        Try

            Call frmCmpCompier.gShow(gudtFileInfo, gEnmCompileType.cpErrorCheck)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '' 2019.03.12 計測点チェック
    Private Sub cmdMeasuringCheck_Click(sender As System.Object, e As System.EventArgs) Handles cmdMeasuringCheck.Click
        Try

            Call frmCmpCompier.gShow(gudtFileInfo, gEnmCompileType.cpMeasuringCheck)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    Private Sub cmdChkChUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChkChUse.Click

        Try

            Call frmChkChUseTable.gShow(gudtFileInfo)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChkChID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChkChID.Click

        Try

            Call frmChkChIdTable.gShow()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChkChOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChkChOutput.Click

        Try

            Call frmChkOutputSetting.gShow(gudtFileInfo)

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdChkFileCompare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChkFileCompare.Click

        Try

            Call frmChkFileCompare.gShow()

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面を閉じる
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try
            'T.Ueki ファイル削除（Usefilefolderpass.txt）
            If Dir(AppPass) <> "" Then
                Kill(AppPassTXT)
            End If

            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdOtherTool_Click(sender As System.Object, e As System.EventArgs) Handles cmdOtherTool.Click
        'Other Tool Menuボタン
        Call frmToolMenu.ShowDialog()
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 未保存の場合は保存確認を行う
    '--------------------------------------------------------------------
    Private Sub frmMenuMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            Dim strPathBase As String = ""
            Dim TostrPathCompileNow As String = ""

            If gblnUpdateAll Then

                ''確認メッセージ表示
                Select Case MessageBox.Show("There is an update file." & vbNewLine & "Do you save?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    Case Windows.Forms.DialogResult.Yes

                        ''保存を実行した場合
                        If mSaveFile(True) = 1 Then
                            ''何もしない（そのままアプリを終了する）
                            'Tempフォルダを正規Compileフォルダに移設しTempフォルダを削除する
                            ''Compileフォルダコピー後Tempフォルダ削除

                        Else

                            ''保存をキャンセルor失敗した場合はフォームクローズをキャンセルする
                            e.Cancel = True

                        End If

                    Case Windows.Forms.DialogResult.No

                        'Tempフォルダを削除する（コンパイル後の内容は破棄）    2013.12.18

                        strPathBase = gudtFileInfo.strFilePath
                        TostrPathCompileNow = System.IO.Path.Combine(strPathBase, gudtFileInfo.strFileVersion & "\Temp")

                        If System.IO.Directory.Exists(TostrPathCompileNow) Then
                            ''Compileフォルダコピー後Tempフォルダ削除
                            System.IO.Directory.Delete(TostrPathCompileNow, True)
                        End If

                        'Ver2.0.2.0
                        Dim strMyFolder As String = System.IO.Path.Combine(strPathBase, gudtFileInfo.strFileVersion)
                        'Tempﾌｫﾙﾀﾞ削除後、ﾌｧｲﾙ、ﾌｫﾙﾀﾞが無い場合は自身のﾌｫﾙﾀﾞも削除
                        If System.IO.Directory.Exists(strMyFolder) Then
                            Dim intFileCount As Integer = System.IO.Directory.GetFileSystemEntries(strMyFolder).Length
                            If intFileCount <= 0 Then
                                System.IO.Directory.Delete(strMyFolder, True)
                            End If
                        End If
                    Case Windows.Forms.DialogResult.Cancel

                        ''キャンセルする
                        e.Cancel = True

                End Select

            Else
                'Tempフォルダを削除する（コンパイル後の内容は破棄）    2013.12.18

                strPathBase = gudtFileInfo.strFilePath
                TostrPathCompileNow = System.IO.Path.Combine(strPathBase, gudtFileInfo.strFileVersion & "\Temp")

                If System.IO.Directory.Exists(TostrPathCompileNow) Then
                    ''Compileフォルダコピー後Tempフォルダ削除
                    System.IO.Directory.Delete(TostrPathCompileNow, True)
                End If

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

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : ボタン使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フラグ
    ' 機能説明  : 操作対象選択前と選択後のコントロール使用可/不可設定を行う
    '--------------------------------------------------------------------
    Private Sub mSetEnableButton(ByVal blnFlg As Boolean)

        Try

            cmdFile3.Enabled = blnFlg
            cmdFile4.Enabled = blnFlg
            'cmdFile5.Enabled = blnFlg  'Ver2.0.2.8 22kConverterﾎﾞﾀﾝは常に押せるように変更

            fraSys.Enabled = blnFlg
            fraCh.Enabled = blnFlg
            fraSeq.Enabled = blnFlg
            fraOps.Enabled = blnFlg
            fraExt.Enabled = blnFlg
            fraPrt.Enabled = blnFlg
            fraCmp.Enabled = blnFlg

            fraOtherTools.Enabled = blnFlg

            '2015.4.23 コンペアボタンのみ開放のため処理変更 T.Ueki
            'fraChk.Enabled = blnFlg
            cmdChkChUse.Enabled = blnFlg
            cmdChkChID.Enabled = blnFlg
            cmdChkChOutput.Enabled = blnFlg

            ' 2015.10.26 Ver1.7.5   ｸﾘｱﾎﾞﾀﾝ
            cmdCelar.Enabled = blnFlg

            ' 2015.10.22 Ver1.7.5 ﾀｸﾞ表示ﾓｰﾄﾞﾌﾗｸﾞ
            '' Ver1.11.8.6 2016.11.10 ﾀｸﾞ設定有無をｺﾝﾎﾞﾎﾞｯｸｽに変更
            CmbTag.Enabled = blnFlg

            ' 2015.11.12 Ver1.7.8 ﾀｸﾞ表示ﾓｰﾄﾞﾌﾗｸﾞ
            '' Ver1.11.8.6 2016.11.10 ｱﾗｰﾑﾚﾍﾞﾙ設定有無をｺﾝﾎﾞﾎﾞｯｸｽに変更
            CmbAlmLvl.Enabled = blnFlg

            'Ver2.0.7.4
            '基板ﾊﾞｰｼﾞｮﾝ印刷するしないのｺﾝﾄﾛｰﾙ制御 DEL
            'chkTerVer.Enabled = blnFlg



            ''======================================
            '' メイン画面（グレイアウト設定）
            ''======================================
            'If blnFlg Then

            '    cmdOPS5.BackColor = Color.Gray
            '    cmdChkChUse.BackColor = Color.Gray
            '    cmdChkChOutput.BackColor = Color.Gray
            '    cmdChkFileCompare.BackColor = Color.Gray

            'End If

            ''本画面にフォーカス
            Me.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル情報表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 機能説明  : ファイル情報の画面表示を行う
    '--------------------------------------------------------------------
    Private Sub mDispFileInfo(ByVal udtFileInfo As gTypFileInfo)

        Try

            With gudtFileInfo

                lblFilePath.Text = .strFilePath

                'ファイル管理仕様変更 T.Ueki
                lblFileName.Text = .strFileVersion
                'lblVersion.Text = .strFileVersion
                'Ver2.0.1.5
                '画面キャプションの先頭にもファイル名表示
                Me.Text = "[" & .strFileVersion & "] " & mstrFormName

                ''船番表示
                Call mDispShipNo()

                Select Case mudtFileMode
                    Case gEnmFileMode.fmNew
                        lblFileMode.Text = "New"
                    Case gEnmFileMode.fmEdit
                        lblFileMode.Text = "Edit"
                    Case gEnmFileMode.fmRename
                        lblFileMode.Text = "Rename"
                    Case Else
                        lblFileMode.Text = "???"
                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 船番表示
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 船番を表示する
    '--------------------------------------------------------------------
    Private Sub mDispShipNo()

        Try
            ''コンバイン時のパート判別無し　ver.1.4.0 2011.09.21
            'Ver2.0.4.9「^」は消す
            lblShipNoMachinery.Text = gudt.SetChGroupSetM.udtGroup.strShipNo.Replace("^", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル保存
    ' 返り値    : 0:キャンセル、1:実行、-1:失敗
    ' 引き数    : ARG1 - (I ) 終了フラグ
    ' 機能説明  : ファイルを保存する
    '--------------------------------------------------------------------
    Private Function mSaveFile(ByVal blnExit As Boolean) As Integer

        Try

            ''ファイルバージョン画面表示
            Return frmFileVersion.gShow(mudtFileMode, gudtFileInfo, blnExit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mSetShowDispButtonEnable(ByVal blnEnable As Boolean, _
                                         ByVal blnChannelList As Boolean)

        If blnEnable Then
            '========================
            ''画面から戻ってきた場合
            '========================

            If blnChannelList Then
                '======================================
                ''チャンネルリストから戻ってきた場合
                '======================================
                gblnDispChannelList = False

                If gblnDispOtherForm Then
                    '=================================================
                    ''チャンネルリスト以外のフォームが開いている場合
                    '=================================================

                    fraFile.Enabled = False
                    fraSys.Enabled = False
                    'fraCh.Enabled = false
                    fraSeq.Enabled = False
                    fraOps.Enabled = False
                    fraExt.Enabled = False
                    fraPrt.Enabled = False
                    fraCmp.Enabled = False
                    fraChk.Enabled = False

                    cmdChannel1.Enabled = True
                    cmdChannel2.Enabled = False
                    cmdChannel3.Enabled = False
                    cmdChannel4.Enabled = False
                    cmdChannel5.Enabled = False
                    cmdChannel6.Enabled = False
                    cmdChannel7.Enabled = False
                    cmdChannel8.Enabled = False
                    cmdChannel9.Enabled = False
                    cmdChannel10.Enabled = False
                    cmdComposite.Enabled = False

                    btnPIDtrendSET.Enabled = False

                    cmdExit.Enabled = False

                Else
                    '=================================================
                    ''チャンネルリスト以外のフォームが開いていない場合
                    '=================================================

                    fraFile.Enabled = True
                    fraSys.Enabled = True
                    'fraCh.Enabled = True
                    fraSeq.Enabled = True
                    fraOps.Enabled = True
                    fraExt.Enabled = True
                    fraPrt.Enabled = True
                    fraCmp.Enabled = True
                    fraChk.Enabled = True

                    cmdChannel1.Enabled = True
                    cmdChannel2.Enabled = True
                    cmdChannel3.Enabled = True
                    cmdChannel4.Enabled = True
                    cmdChannel5.Enabled = True
                    cmdChannel6.Enabled = True
                    cmdChannel7.Enabled = True
                    cmdChannel8.Enabled = True
                    cmdChannel9.Enabled = True
                    cmdChannel10.Enabled = True
                    cmdComposite.Enabled = True

                    btnPIDtrendSET.Enabled = True

                    cmdExit.Enabled = True

                End If

            Else
                '========================================
                ''チャンネルリスト以外から戻ってきた場合
                '========================================
                gblnDispOtherForm = False

                If gblnDispChannelList Then
                    '========================================
                    ''チャンネルリストが開いている場合
                    '========================================

                    fraFile.Enabled = False
                    fraSys.Enabled = False
                    'fraCh.Enabled = false
                    fraSeq.Enabled = False
                    fraOps.Enabled = False
                    fraExt.Enabled = False
                    fraPrt.Enabled = False
                    fraCmp.Enabled = False
                    fraChk.Enabled = False

                    cmdChannel1.Enabled = False
                    cmdChannel2.Enabled = False
                    cmdChannel3.Enabled = False
                    cmdChannel4.Enabled = False
                    cmdChannel5.Enabled = False
                    cmdChannel6.Enabled = False
                    cmdChannel7.Enabled = False
                    cmdChannel8.Enabled = False
                    cmdChannel9.Enabled = False
                    cmdChannel10.Enabled = False
                    cmdComposite.Enabled = False

                    btnPIDtrendSET.Enabled = False

                    cmdExit.Enabled = False

                Else
                    '========================================
                    ''チャンネルリストが開いていない場合
                    '========================================
                    fraFile.Enabled = True
                    fraSys.Enabled = True
                    'fraCh.Enabled = True
                    fraSeq.Enabled = True
                    fraOps.Enabled = True
                    fraExt.Enabled = True
                    fraPrt.Enabled = True
                    fraCmp.Enabled = True
                    fraChk.Enabled = True

                    cmdChannel1.Enabled = True
                    cmdChannel2.Enabled = True
                    cmdChannel3.Enabled = True
                    cmdChannel4.Enabled = True
                    cmdChannel5.Enabled = True
                    cmdChannel6.Enabled = True
                    cmdChannel7.Enabled = True
                    cmdChannel8.Enabled = True
                    cmdChannel9.Enabled = True
                    cmdChannel10.Enabled = True
                    cmdComposite.Enabled = True

                    btnPIDtrendSET.Enabled = True

                    cmdExit.Enabled = True

                End If

            End If

        Else
            '==================
            ''画面を開く場合
            '==================

            If blnChannelList Then
                '========================================
                ''チャンネルリストを開く場合
                '========================================
                gblnDispChannelList = True

                fraFile.Enabled = False
                fraSys.Enabled = False
                'fraCh.Enabled = false
                fraSeq.Enabled = False
                fraOps.Enabled = False
                fraExt.Enabled = False
                fraPrt.Enabled = False
                fraCmp.Enabled = False
                fraChk.Enabled = False

                cmdChannel1.Enabled = False
                cmdChannel2.Enabled = False
                cmdChannel3.Enabled = False
                cmdChannel4.Enabled = False
                cmdChannel5.Enabled = False
                cmdChannel6.Enabled = False
                cmdChannel7.Enabled = False
                cmdChannel8.Enabled = False
                cmdChannel9.Enabled = False
                cmdChannel10.Enabled = False
                cmdComposite.Enabled = False

                btnPIDtrendSET.Enabled = False

                cmdExit.Enabled = False

            Else
                '========================================
                ''チャンネルリスト以外を開く場合
                '========================================
                gblnDispOtherForm = True

                fraFile.Enabled = False
                fraSys.Enabled = False
                'fraCh.Enabled = false
                fraSeq.Enabled = False
                fraOps.Enabled = False
                fraExt.Enabled = False
                fraPrt.Enabled = False
                fraCmp.Enabled = False
                fraChk.Enabled = False

                cmdChannel1.Enabled = True
                cmdChannel2.Enabled = False
                cmdChannel3.Enabled = False
                cmdChannel4.Enabled = False
                cmdChannel5.Enabled = False
                cmdChannel6.Enabled = False
                cmdChannel7.Enabled = False
                cmdChannel8.Enabled = False
                cmdChannel9.Enabled = False
                cmdChannel10.Enabled = False
                cmdComposite.Enabled = False

                btnPIDtrendSET.Enabled = False

                cmdExit.Enabled = False

            End If

        End If

        ''本画面にフォーカス
        Me.Focus()

    End Sub

#End Region






    '--------------------------------------------------------------------
    ' 機能      : ｸﾘｱﾎﾞﾀﾝ動作
    ' 返り値    :　
    ' 引き数    : 
    ' 機能説明  : アナログ設定が情報が正しいか確認する
    '
    '       ' 2015.10.26 Ver1.7.5  追加
    '--------------------------------------------------------------------
    Private Sub cmdCelar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCelar.Click
        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmClear.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ﾀｸﾞ設定有無　ｺﾝﾎﾞﾎﾞｯｸｽ
    ' 返り値    :　
    ' 引き数    : 
    ' 機能説明  : 
    '
    '       ' Ver1.11.8.6  2016.11.10  追加
    '       ' Ver1.12.0.6  2017.02.10 ｲﾍﾞﾝﾄを間違えていたので修正
    '       ' Ver2.0.0.0   2016.12.05  Handlesが消えており、変数更新がおこなわれていなかったため修正
    '--------------------------------------------------------------------
    Private Sub CmbTag_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbTag.SelectedIndexChanged
        Dim sTagValue As Short

        sTagValue = CmbTag.SelectedValue

        ' 前回と値が異なっていたら変更ﾌﾗｸﾞをｾｯﾄ
        If sTagValue <> gudt.SetSystem.udtSysOps.shtTagMode Then
            gudt.SetSystem.udtSysOps.shtTagMode = sTagValue
            gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1          ' ｼｽﾃﾑﾌｧｲﾙ更新
            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1         ' ﾌｧｲﾙ更新も念のため実施
            gblnUpdateAll = True
            'MsgBox("TagNo. Setting Change", MsgBoxStyle.Exclamation)   '' Ver1.11.9.8 2016.12.15 追加
        End If
    End Sub

    'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応 DEL
    'Private Sub chkTerVer_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTerVer.CheckedChanged
    '    Dim oldType As Short

    '    oldType = gudt.SetSystem.udtSysOps.shtTerVer

    '    If chkTerVer.Checked = True Then
    '        gudt.SetSystem.udtSysOps.shtTerVer = 1
    '    Else
    '        gudt.SetSystem.udtSysOps.shtTerVer = 0
    '    End If

    '    ' 設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
    '    If oldType <> gudt.SetSystem.udtSysOps.shtTerVer Then
    '        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
    '        gblnUpdateAll = True
    '    End If
    'End Sub


    '--------------------------------------------------------------------
    ' 機能      : ｱﾗｰﾑﾚﾍﾞﾙ設定有無　ｺﾝﾎﾞﾎﾞｯｸｽ
    ' 返り値    :　
    ' 引き数    : 
    ' 機能説明  : 
    '
    '       ' Ver1.11.8.6  2016.11.10  追加
    '--------------------------------------------------------------------
    Private Sub CmbAlmLvl_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbAlmLvl.SelectedIndexChanged

        Dim sAlmLevel As Short

        sAlmLevel = CmbAlmLvl.SelectedValue

        ' 前回と値が異なっていたら変更ﾌﾗｸﾞをｾｯﾄ
        If sAlmLevel <> gudt.SetSystem.udtSysOps.shtLRMode Then
            gudt.SetSystem.udtSysOps.shtLRMode = sAlmLevel
            gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1          ' ｼｽﾃﾑﾌｧｲﾙ更新
            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1         ' ﾌｧｲﾙ更新も念のため実施
            gblnUpdateAll = True
            'MsgBox("Alarm Level Setting Change", MsgBoxStyle.Exclamation)   '' Ver1.11.9.8 2016.12.15 追加
        End If
    End Sub

#Region "JRCS BATCH"
    Private Sub btnJRCSbatch_Click(sender As System.Object, e As System.EventArgs) Handles btnJRCSbatch.Click
        Dim strPath As String = gGetAppPath()
        Dim strRLpath As String = strPath & "\RL_CH.csv"
        Dim strLOGpath As String = strPath & "\LOG_CH.csv"

        Dim aryRLch As New ArrayList
        Dim aryLOGch As New ArrayList

        Dim i As Integer = 0

        '計測点リストでRL_ONのCH一覧をﾌｧｲﾙへ保存
        For i = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
            With gudt.SetChInfo.udtChannel(i)
                If .udtChCommon.shtChno > 0 Then
                    If gBitCheck(.udtChCommon.shtFlag2, 0) = True Then
                        aryRLch.Add(.udtChCommon.shtChno.ToString("0000"))
                    End If
                End If
            End With
        Next i
        aryRLch.Sort()
        Call subOpen(strRLpath)
        For i = 0 To aryRLch.Count - 1 Step 1
            subWrite(aryRLch(i))
        Next i
        Call subClose()


        'ﾛｸﾞﾌｫｰﾏｯﾄの計測点CH一覧をﾌｧｲﾙへ保存
        Dim strData As String = ""
        Dim intData As Integer = -1
        For i = 0 To UBound(gudt.SetOpsLogFormatM.strCol1) Step 1
            With gudt.SetOpsLogFormatM
                strData = .strCol1(i)
                strData = strData.Substring(2)
                If IsNumeric(strData) = True Then
                    intData = CInt(strData)
                    If intData > 100 Then
                        aryLOGch.Add(intData.ToString("0000"))
                    End If
                End If
                strData = .strCol2(i)
                strData = strData.Substring(2)
                If IsNumeric(strData) = True Then
                    intData = CInt(strData)
                    If intData > 100 Then
                        aryLOGch.Add(intData.ToString("0000"))
                    End If
                End If
            End With
        Next
        aryLOGch.Sort()
        Call subOpen(strLOGpath)
        For i = 0 To aryLOGch.Count - 1 Step 1
            subWrite(aryLOGch(i))
        Next i
        Call subClose()

    End Sub

#Region "出力ファイル関連"
    Private sw As IO.StreamWriter
    'ファイルオープン
    Private Sub subOpen(pstrPath As String)
        Dim dt As DateTime = Now
        Dim strPathBase As String = pstrPath

        sw = Nothing
        Try
            sw = New IO.StreamWriter(strPathBase, False, System.Text.Encoding.GetEncoding("Shift-JIS"))
        Catch ex As Exception
        End Try
    End Sub
    'データ書き込み
    Private Sub subWrite(pstrMsg As String)
        sw.WriteLine(pstrMsg)
    End Sub
    'ファイルクローズ
    Private Sub subClose()
        If sw Is Nothing = False Then sw.Close()
    End Sub
#End Region
#End Region
    Private sflg As Boolean = False
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function ScreenToClient(ByVal hWnd As IntPtr, ByRef pt As Point) As Boolean
    End Function
    <DllImport("user32.dll")> _
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)

        Dim axis_x As Integer = 0
        Dim axis_y As Integer = 0

        'If m.Msg <> &H202 And sflg = True Then
        '    Me.Capture = True

        '    Dim strName As String = ""
        '    Dim p As System.Diagnostics.Process
        '    Dim mdrawP As System.Diagnostics.Process = Nothing
        '    For Each p In System.Diagnostics.Process.GetProcesses()
        '        'メインウィンドウのタイトルがある時だけ列挙する
        '        If p.MainWindowTitle.Length <> 0 Then
        '            strName = p.ProcessName
        '            If strName = "MDraw55" Then
        '                mdrawP = p
        '                Exit For
        '            End If
        '        End If
        '    Next
        '    SetForegroundWindow(mdrawP.MainWindowHandle)
        'End If

        '&H202は、マウスの左ボタン押下を表す
        If m.Msg = &H202 And sflg = True Then
            Me.Capture = sflg

            Dim strName As String = ""
            Dim p As System.Diagnostics.Process
            Dim mdrawP As System.Diagnostics.Process = Nothing
            For Each p In System.Diagnostics.Process.GetProcesses()
                'メインウィンドウのタイトルがある時だけ列挙する
                If p.MainWindowTitle.Length <> 0 Then
                    strName = p.ProcessName
                    If strName = "MDraw55" Then
                        mdrawP = p
                        Exit For
                    End If
                End If
            Next

            Dim pt As Point = Cursor.Position
            If mdrawP Is Nothing Then
            Else
                ScreenToClient(mdrawP.MainWindowHandle, pt)
            End If

            'クリックした座標を取得する
            axis_x = pt.X
            axis_y = pt.Y

            Me.Text = axis_x.ToString & "," & axis_y.ToString


            sflg = False
        End If
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        sflg = True
        Me.Capture = sflg
    End Sub

    ''負荷曲線作成　20200213 hori
    Private Sub cmdLoadCurve_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadCurve.Click
        Try

            Call mSetShowDispButtonEnable(False, False)
            Call frmLoadCurve.gShow()
            Call mSetShowDispButtonEnable(True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


End Class

