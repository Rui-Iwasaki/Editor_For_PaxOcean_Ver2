Public Class frmCompareFileAccess

#Region "定数定義"

    Private Const mCstProgressValueMaxLoad As Integer = 56

#End Region

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileInfo As gTypCompareFileInfo
    Private mudt As clsStructure

    Private mblnCFRead As Boolean
    Private mblnSaveRead As Boolean
    Private mblnCompileRead As Boolean
    Private mblnCForgRead As Boolean

    Private mudt2 As clsStructure

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:処理成功
    ' 　　　    :-1:処理失敗（失敗あり）
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 　　　    : ARG1 - (I ) アクセスモード
    ' 機能説明  : 画面表示を行い戻り値を返す
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileInfo As gTypCompareFileInfo, _
                          ByVal blnCFRead As Boolean, _
                          ByVal blnSaveRead As Boolean, _
                          ByVal blnCompileRead As Boolean, _
                          ByVal blnCForgRead As Boolean, _
                          ByRef udt As clsStructure) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileInfo = udtFileInfo
            mblnCFRead = blnCFRead
            mblnSaveRead = blnSaveRead
            mblnCompileRead = blnCompileRead
            mblnCForgRead = blnCForgRead
            mudt = udt

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            udt = mudt
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
    Private Sub frmFileSave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''フォームタイトル表示
            Me.Text = "Load Setting"

            ''タイマースタート
            tmrStart.Interval = 100
            tmrStart.Enabled = True
            tmrStart.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : スタートタイマー
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理/読込処理を行う
    '--------------------------------------------------------------------
    Private Sub tmrStart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrStart.Tick

        Try

            ''タイマーストップ
            tmrStart.Enabled = False
            tmrStart.Stop()

            ''画面設定（処理中はExitボタン操作不可）
            cmdExit.Enabled = False

            ''リストボックスクリア
            Call lstMsg.Items.Clear()

            ''読込処理
            If mCompareLoadSetting() = 0 Then

                ''読込処理成功時は自動で画面を閉じる
                mintRtn = 0
                Call Me.Close()

            Else
                mintRtn = -1
            End If

            ''画面設定
            cmdExit.Enabled = True

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

            Call Me.Close()

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
    ' 機能      : メッセージ追加
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) メッセージ
    ' 機能説明  : ログリストにログを追加する
    '--------------------------------------------------------------------
    Private Sub mAddMsgList(ByVal strMsg As String)

        Try

            Call lstMsg.Items.Add(strMsg)

            ''画面表示ログが指定行数以上の場合に最終行を削除する
            If lstMsg.Items.Count > 1000 Then
                Call lstMsg.Items.RemoveAt(lstMsg.Items.Count - 1)
            End If

            Call lstMsg.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値読み込み
    ' 返り値    : 0:成功、<>0:失敗数
    ' 引き数    : なし
    ' 機能説明  : 設定値保存処理を行う
    '--------------------------------------------------------------------
    Private Function mCompareLoadSetting() As Integer

        Try

            Dim fileNo As Integer = FreeFile()

            Dim Fso As New Scripting.FileSystemObject
            Dim intRtn As Integer
            Dim strPathBase As String = ""
            Dim strPathUpdateInfo As String = ""    'リポーズの最大値設定のみ読み込む為の配列2018.12.13 倉重

            ''バージョン番号までのファイルパス作成
            With mudtFileInfo

                If mblnCForgRead = False And mblnCFRead = True Then
                    strPathBase = System.IO.Path.Combine(.strFileOrgPath, .strFileName)
                Else
                    strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)
                End If

                ''EditorInfo配下のフォルダパス 2019.03.19
                strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathBase, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

                If mblnSaveRead Then
                    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
                ElseIf mblnCompileRead Then
                    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                End If


            End With

            With prgBar

                ''プログレスバー初期化
                .Minimum = 0
                .Maximum = mCstProgressValueMaxLoad
                .Value = 0

                '' Setup.iniﾌｧｲﾙのリポーズ配列のみ読み込み (最初に読み込む) 2018.12.13 倉重
                ReadEditRepIni(strPathUpdateInfo & "\" & gCstIniFile)

                ''システム設定データ読み込み
                intRtn += mLoadSystem(mudt.SetSystem, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''FU チャンネル情報読み込み
                intRtn += mLoadFuChannel(mudt.SetFu, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''チャンネル情報データ（表示名設定データ）読み込み
                intRtn += mLoadChDisp(mudt.SetChDisp, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''チャンネル情報読み込み
                intRtn += mLoadChannel(mudt.SetChInfo, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''コンポジット情報読み込み
                intRtn += mLoadComposite(mudt.SetChComposite, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''グループ設定読み込み
                intRtn += mLoadGroup(mudt.SetChGroupSetM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadGroup(mudt.SetChGroupSetC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''リポーズ入力設定読み込み
                intRtn += mLoadRepose(mudt.SetChGroupRepose, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''出力チャンネル設定読み込み
                intRtn += mLoadOutPut(mudt.SetChOutput, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''論理出力設定読み込み
                intRtn += mLoadOrAnd(mudt.SetChAndOr, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''積算データ設定読み込み
                intRtn += mLoadChRunHour(mudt.SetChRunHour, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''コントロール使用可／不可設定書き込み
                intRtn += mLoadCtrlUseNotuse(mudt.SetChCtrlUseM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadCtrlUseNotuse(mudt.SetChCtrlUseC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''SIO設定読み込み
                intRtn += mLoadChSio(mudt.SetChSio, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''SIO設定CH設定読み込み
                For i As Integer = 0 To UBound(mudt.SetChSioCh)
                    intRtn += mLoadChSioCh(mudt.SetChSioCh(i), mudtFileInfo, strPathBase, i + 1) : .Value += 1 : Application.DoEvents()
                Next

                ''排ガス処理演算設定読み込み
                intRtn += mLoadexhGus(mudt.SetChExhGus, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''延長警報設定読み込み
                intRtn += mLoadExtAlarm(mudt.SetExtAlarm, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''タイマ設定読み込み
                intRtn += mLoadTimer(mudt.SetExtTimerSet, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''タイマ表示名称設定読み込み
                intRtn += mLoadTimerName(mudt.SetExtTimerName, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''シーケンスID読み込み
                intRtn += mLoadSeqSequenceID(mudt.SetSeqID, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''シーケンス設定読み込み
                intRtn += mLoadSeqSequenceSet(mudt.SetSeqSet, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                'リニアライズテーブル読み込み
                intRtn += mLoadSeqLinear(mudt.SetSeqLinear, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                '演算式テーブル読み込み
                intRtn += mLoadSeqOperationExpression(mudt.SetSeqOpeExp, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''データ保存テーブル設定
                intRtn += mLoadChDataSaveTable(mudt.SetChDataSave, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''データ転送テーブル設定
                intRtn += mLoadChDataForwardTableSet(mudt.SetChDataForward, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''OPSスクリーンタイトルデータ読み込み
                intRtn += mLoadOpsScreenTitle(mudt.SetOpsScreenTitleM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsScreenTitle(mudt.SetOpsScreenTitleC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''プルダウンメニュー
                intRtn += mLoadManuMain(mudt.SetOpsPulldownMenuM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadManuMain(mudt.SetOpsPulldownMenuC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''セレクションメニュー
                intRtn += mLoadSelectionMenu(mudt.SetOpsSelectionMenuM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadSelectionMenu(mudt.SetOpsSelectionMenuC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''OPSグラフ設定
                intRtn += mLoadOpsGraph(mudt.SetOpsGraphM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsGraph(mudt.SetOpsGraphC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                ''ログフォーマットCHID
                intRtn += mLoadOpsLogIdData(mudt.SetOpsLogIdDataM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsLogIdData(mudt.SetOpsLogIdDataC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                '' Ver1.7.8 2015.11.12 ﾛｸﾞﾌｫｰﾏｯﾄ読み込み
                intRtn += mLoadOpsLogFormat(mudt.SetOpsLogFormatM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsLogFormat(mudt.SetOpsLogFormatC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()
                ''/

                '' Ver1.9.3 2016.01.25 ﾛｸﾞｵﾌﾟｼｮﾝ設定追加
                ''intRtn += mLoadOpsLogOption(mudt.SetOpsLogOption, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''GWS設定CH設定読み込み 2014.02.04
                intRtn += mLoadopsGwsCh(mudt.SetOpsGwsCh, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                .Value = .Maximum

                ''メッセージ表示
                If intRtn <> 0 Then

                    ''失敗
                    lblMessage.Text = "Loading the file failed."
                    lblMessage.ForeColor = Color.Red

                Else

                    ''成功
                    lblMessage.Text = "Loading the file succeeded."
                    lblMessage.ForeColor = Color.Blue

                End If

                Return intRtn

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルID、チャンネルNo、システムNo変換
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function mConvChidToChno() As Integer

        Try

            Dim intRtn As Integer
            Dim intErrCnt As Integer
            Dim strMsg() As String = Nothing

            ''メッセージ更新
            lblMessage.Text = "Converting CH ID to CH NO..." : Call lblMessage.Refresh()
            Call mAddMsgList("Converting CH ID to CH NO...")

            ''出力チャンネル設定
            Call gConvChannelChOutput(mudt.SetChOutput, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Output Set Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''グループリポーズ設定
            Call gConvChannelChGroupRepose(mudt.SetChGroupRepose, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Group Repose Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''論理出力設定
            Call gConvChannelChAndOr(mudt.SetChAndOr, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("And Or Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''運転積算トリガチャンネル設定
            Call gConvChannelChRevoTrriger(mudt.SetChInfo, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Pulse Revolution Trigger Channnel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''積算データ設定
            Call gConvChannelChRunHour(mudt.SetChRunHour, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("RUN HOUR Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''シーケンス設定
            Call gConvChannelSeqSequence(mudt.SetSeqSet, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Sequence Set Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''排ガス演算設定
            Call gConvChannelExhGus(mudt.SetChExhGus, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Exh Gus Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''データ保存テーブル設定
            Call gConvChannelDataSaveTable(mudt.SetChDataSave, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Data SaveTable Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''コンポジット設定
            Call gConvChannelComposite(mudt.SetChComposite, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Composite Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''演算式テーブル
            Call gConvChannelOpeExp(mudt.SetSeqOpeExp, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Operation Expression Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''チャンネル情報データ（表示名設定データ）
            Call gConvChannelChDisp(mudt.SetChDisp, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            Call mOutputChannelConvMsg("Slot Information(CH Disp) Channel Setting", intErrCnt, strMsg)
            intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            Return intRtn
            'Call mAddMsgText("")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mOutputChannelConvMsg(ByVal strCurName As String, _
                                      ByVal intErrCnt As Integer, _
                                      ByVal strMsg() As String)

        Try

            ''メッセージ
            If intErrCnt = 0 Then

                ''全て成功
                Call mAddMsgList(" -" & strCurName & " ... Success")

            Else

                ''失敗
                Call mAddMsgList(" -" & strCurName & " ... Failure")

                ''エラー内容を追記
                For i As Integer = 0 To UBound(strMsg)
                    Call mAddMsgList(strMsg(i))
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "出力ファイル名取得"

    '--------------------------------------------------------------------
    ' 機能      : 出力ファイル名取得
    ' 返り値    : 出力ファイル名取得
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 　　　    : ARG2 - (I ) 基本ファイル名
    ' 機能説明  : [ファイル名]_[バージョン番号]_[基本ファイル名]を作成して返す
    '--------------------------------------------------------------------
    Private Function mGetOutputFileName(ByVal udtFileInfo As gTypCompareFileInfo, _
                                        ByVal strFileName As String, _
                                        ByVal blnCFRead As Boolean, _
                                        ByVal blnSaveRead As Boolean, _
                                        ByVal blnCompileRead As Boolean) As String

        Dim strRtn As String = ""

        Try

            If blnCFRead Or blnCompileRead Then
                strRtn = strFileName
            ElseIf blnSaveRead Then
                strRtn = udtFileInfo.strFileName & "_" & strFileName
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "チャンネル設定ファイルの可変長出力対応"

    Private Sub mRemakeChannelFileLoad(ByRef strFullPath As String)

        Try

            Dim intFileNo As Integer
            Dim udtSetChInfo As gTypSetChInfo = Nothing
            Dim lngByteCntAll As Integer = gCstByteCntHeader + (gCstByteCntChannelOne * gCstChannelIdMax)
            Dim bytInit(lngByteCntAll - 1) As Byte
            Dim bytChCmp() As Byte
            Dim strFilePathTemp1 As String = ""
            Dim strFilePathTemp2 As String = ""

            ''初期化したチャンネル情報構造体を作成する
            Call gInitSetChannelDisp(udtSetChInfo)

            ''tmp1出力パスを作成する（既に存在する場合は削除する）
            strFilePathTemp1 = strFullPath & ".tmp1"
            If System.IO.File.Exists(strFilePathTemp1) Then Call My.Computer.FileSystem.DeleteFile(strFilePathTemp1)

            ''初期化したチャンネル情報構造体を出力する
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp1, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
            FilePut(intFileNo, udtSetChInfo)
            FileClose(intFileNo)

            ''初期化したチャンネル情報構造体をバイト配列で読み込む
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp1, OpenMode.Binary, OpenAccess.Read, OpenShare.LockWrite)
            FileGet(intFileNo, bytInit)
            FileClose(intFileNo)

            ''可変長で出力された channel.cfg のファイルサイズ（バイト数）を取得する
            Dim objFileInfo As New System.IO.FileInfo(strFullPath)
            Dim lngByteCnt As Long = objFileInfo.Length

            ''可変長で出力された channel.cfg の読み込み用バイト配列を再定義する
            ReDim bytChCmp(lngByteCnt - 1)

            ''可変長で出力された channel.cfg をバイト配列で読み込む
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.LockWrite)
            FileGet(intFileNo, bytChCmp)
            FileClose(intFileNo)

            ''初期化したチャンネル情報を読み込んだバイト配列に、可変長で出力された channel.cfg を読み込んだバイト配列を上書きする
            Call Array.Copy(bytChCmp, bytInit, lngByteCnt)

            ''上記の情報を統合したバイト配列を出力する
            strFilePathTemp2 = strFullPath & ".tmp2"
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp2, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
            FilePut(intFileNo, bytInit)
            FileClose(intFileNo)

            ''読み込むべきファイルフルパスをout引数に設定する
            strFullPath = strFilePathTemp2

            ''tmp1を削除する（tmp2は読み込んだ後に削除する）
            If System.IO.File.Exists(strFilePathTemp1) Then Call My.Computer.FileSystem.DeleteFile(strFilePathTemp1)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "ファイル入出力関数"

#Region "システム設定"

    '--------------------------------------------------------------------
    ' 機能      : システム設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSystem(ByRef udtSetSystem As gTypSetSystem, _
                                 ByVal udtFileInfo As gTypCompareFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSystem As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSystem
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSystem, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSystem = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSystem, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try

                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSystem)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ＦＵチャンネル情報"

    '--------------------------------------------------------------------
    ' 機能      : ＦＵチャンネル情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadFuChannel(ByRef udtSetFuChannel As gTypSetFu, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathFuChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileFuChannel, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetFuChannel)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報データ"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報データ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDisp(ByRef udtSetFuChannel As gTypSetChDisp, _
                                 ByVal udtFileInfo As gTypCompareFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDisp
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDisp, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetFuChannel)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChannel(ByRef udtSetChannel As gTypSetChInfo, _
                                  ByVal udtFileInfo As gTypCompareFileInfo, _
                                  ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChannel, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                If mblnCFRead Or mblnCompileRead Then
                    ''構造体読み用のファイルを作成する
                    Call mRemakeChannelFileLoad(strFullPath)
                End If

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChannel)
                    FileClose(intFileNo)

                    If mblnCFRead Or mblnCompileRead Then
                        ''構造体読み用に作成されたファイルを削除する
                        Call System.IO.File.Delete(strFullPath)
                    End If

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "コンポジット情報"

    '--------------------------------------------------------------------
    ' 機能      : コンポジット情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) コンポジット情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadComposite(ByRef udtSetComposite As gTypSetChComposite, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathComposite
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileComposite, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetComposite)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

#Region "グループ設定"

    '--------------------------------------------------------------------
    ' 機能      : グループ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) グループ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadGroup(ByRef udtSetGroup As gTypSetChGroupSet, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String, _
                                ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathGroup
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileGroupM, gCstFileGroupC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetGroup)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "リポーズ入力設定"

    '--------------------------------------------------------------------
    ' 機能      : リポーズ入力設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) リポーズ入力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リポーズ入力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadRepose(ByRef udtSetGroupRepose As gTypSetChGroupRepose, _
                                 ByVal udtFileInfo As gTypCompareFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathRepose
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileRepose, mblnCFRead, mblnSaveRead, mblnCompileRead)
            Dim udt48 As gTypSetChGroupRepose48 = Nothing ''2018.12.13 倉重 グループリポーズが48の場合に使用する配列
            Dim intListRow As Integer = 0       ''2018.12.13 倉重 カウンタ変数
            Dim intListDetailRow As Integer = 0 ''2018.12.13 倉重 カウンタ変数

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読み込み
                    ''ADDのチェックボックスがチェックでない場合に格納する2018.12.13 倉重
                    If g_bytSrcGREPNUM = 0 Then
                        udt48.InitArray()
                        FileGet(intFileNo, udt48)

                        ''ヘッダの読み込み
                        udtSetGroupRepose.udtHeader.strVersion = udt48.udtHeader.strVersion
                        udtSetGroupRepose.udtHeader.strDate = udt48.udtHeader.strDate
                        udtSetGroupRepose.udtHeader.strTime = udt48.udtHeader.strTime
                        udtSetGroupRepose.udtHeader.shtRecs = udt48.udtHeader.shtRecs
                        udtSetGroupRepose.udtHeader.shtSize1 = udt48.udtHeader.shtSize1
                        udtSetGroupRepose.udtHeader.shtSize2 = udt48.udtHeader.shtSize2
                        udtSetGroupRepose.udtHeader.shtSize3 = udt48.udtHeader.shtSize3
                        udtSetGroupRepose.udtHeader.shtSize4 = udt48.udtHeader.shtSize4
                        udtSetGroupRepose.udtHeader.shtSize5 = udt48.udtHeader.shtSize5

                        ''リポーズデータの読み込み
                        For intListRow = LBound(udt48.udtRepose) To UBound(udt48.udtRepose)
                            ''CH ID
                            udtSetGroupRepose.udtRepose(intListRow).shtChId = udt48.udtRepose(intListRow).shtChId

                            ''データ種別コード
                            udtSetGroupRepose.udtRepose(intListRow).shtData = udt48.udtRepose(intListRow).shtData

                            For intListDetailRow = 0 To UBound(udt48.udtRepose(intListRow).udtReposeInf)

                                ''CH ID
                                udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId = udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId

                                ''マスク値
                                udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask = udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask

                            Next
                        Next
                    Else
                        FileGet(intFileNo, udtSetGroupRepose)
                    End If
                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "出力チャンネル設定"

    '--------------------------------------------------------------------
    ' 機能      : 出力チャンネル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 出力チャンネル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 出力チャンネル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOutPut(ByRef udtSetCHOutPut As gTypSetChOutput, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOutPut
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOutPut, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)


            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetCHOutPut)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "論理出力設定"

    '--------------------------------------------------------------------
    ' 機能      :論理出力設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 論理出力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 論理出力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOrAnd(ByRef udtSetCHAndOr As gTypSetChAndOr, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOrAnd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOrAnd, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetCHAndOr)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "積算データ設定"

    '--------------------------------------------------------------------
    ' 機能      :積算データ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 積算データ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 積算データ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChRunHour(ByRef udtSetChRunHour As gTypSetChRunHour, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChAdd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChAdd, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChRunHour)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "排ガス処理演算設定"

    '--------------------------------------------------------------------
    ' 機能      : 排ガス処理演算設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 排ガス処理演算設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 排ガス処理演算設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadexhGus(ByRef udtSetExhGus As gTypSetChExhGus, _
                                 ByVal udtFileInfo As gTypCompareFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExhGus
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExhGus, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetExhGus)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "コントロール使用可／不可設定"

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可／不可設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) コントロール使用可／不可設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : コントロール使用可／不可設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadCtrlUseNotuse(ByRef udtSetTimer As gTypSetChCtrlUse, _
                                        ByVal udtFileInfo As gTypCompareFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathCtrlUseNouse
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileCtrlUseNouseM, gCstFileCtrlUseNouseC), mblnCFRead, mblnSaveRead, mblnCompileRead)
            Dim bFileFg As Boolean      '' Ver1.9.7 2016.02.17 追加

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                '' Ver1.9.7 2016.02.17 関数に変更
                bFileFg = SaveTempCtrlUseNotUseFile(strFullPath, strPathBase, strCurFileName, 0)

                '' '' Ver1.9.7 2016.02.17 ﾌｧｲﾙｻｲｽﾞ拡張のため、ﾌｧｲﾙｻｲｽﾞが旧ﾀｲﾌﾟならばﾃﾞｰﾀを追加して保存
                ''Dim fs As New System.IO.FileStream(strFullPath, System.IO.FileMode.Open, IO.FileAccess.Write)
                ''Dim nLength As Long = fs.Length

                ''If nLength = gOldUseNotUseFileSize Then

                ''    Dim ns((gAmxControlUseNotUse - 32) * 326 - 1) As Byte

                ''    fs.Seek(0, IO.SeekOrigin.End)
                ''    fs.Write(ns, 0, ns.Length)
                ''    fs.Close()

                ''End If
                ''//


                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetTimer)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)

                    '' Ver1.9.7 2016.02.17  ﾌｧｲﾙ削除
                    If (bFileFg) Then
                        Kill(strFullPath)
                    End If
                    ''//

                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChSio(ByRef udtSetSio As gTypSetChSio, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSio
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSio, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSio)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定CH設定"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChSioCh(ByRef udtSetSioCh As gTypSetChSioCh, _
                                  ByVal udtFileInfo As gTypCompareFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioChName, mblnCFRead, mblnSaveRead, mblnCompileRead) & Format(intPortNo, "00") & gCstFileChSioChExt

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSioCh)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "延長警報設定"

    '--------------------------------------------------------------------
    ' 機能      : 延長警報設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 延長警報設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 延長警報設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadExtAlarm(ByRef udtSetExAlm As gTypSetExtAlarm, _
                                   ByVal udtFileInfo As gTypCompareFileInfo, _
                                   ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExtAlarm
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExtAlarm, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetExAlm)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ設定"

    '--------------------------------------------------------------------
    ' 機能      : タイマ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) タイマ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadTimer(ByRef udtSetTimer As gTypSetExtTimerSet, _
                                ByVal udtFileInfo As gTypCompareFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimer
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimer, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetTimer)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ表示名称設定"

    '--------------------------------------------------------------------
    ' 機能      : タイマ表示名称設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) タイマ表示名称設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ表示名称設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadTimerName(ByRef udtSetTimerName As gTypSetExtTimerName, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimerName
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimerName, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetTimerName)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "シーケンスID"

    '--------------------------------------------------------------------
    ' 機能      : シーケンスID読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) シーケンスID構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  :シーケンスID保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqSequenceID(ByRef udtSetSeqSequenceID As gTypSetSeqID, _
                                        ByVal udtFileInfo As gTypCompareFileInfo, _
                                        ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceID
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceID, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSeqSequenceID)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "シーケンス設定"

    '--------------------------------------------------------------------
    ' 機能      : シーケンス設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) シーケンス設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  :シーケンス設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqSequenceSet(ByRef udtSetSeqSequenceSet As gTypSetSeqSet, _
                                         ByVal udtFileInfo As gTypCompareFileInfo, _
                                         ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceSet, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSeqSequenceSet)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "リニアライズテーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : リニアライズテーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) リニアライズテーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リニアライズテーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqLinear(ByRef udtSetSeqLinear As gTypSetSeqLinear, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqLinear
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqLinear, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSeqLinear)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "演算式テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 演算式テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 演算式テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 演算式テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqOperationExpression(ByRef udtSetSeqOpeExp As gTypSetSeqOperationExpression, _
                                                 ByVal udtFileInfo As gTypCompareFileInfo, _
                                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqOperationExpression
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqOperationExpression, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSeqOpeExp)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ保存テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : データ保存テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ保存テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ保存テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDataSaveTable(ByRef udtSetChDataSaveTable As gTypSetChDataSave, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataSaveTable
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataSaveTable, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChDataSaveTable)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ転送テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDataForwardTableSet(ByRef udtSetChDataForwardTableSet As gTypSetChDataForward, _
                                    ByVal udtFileInfo As gTypCompareFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataForwardTableSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataForwardTableSet, mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChDataForwardTableSet)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPSスクリーンタイトル"

    '--------------------------------------------------------------------
    ' 機能      : OPSスクリーンタイトル読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) OPSスクリーンタイトル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsScreenTitle(ByRef udtSetOpsScreenTitle As gTypSetOpsScreenTitle, _
                                         ByVal udtFileInfo As gTypCompareFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsScreenTitle
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsScreenTitleM, gCstFileOpsScreenTitleC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetOpsScreenTitle)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "プルダウンメニュー"

    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadManuMain(ByRef udtManuMain As gTypSetOpsPulldownMenu, _
                                   ByVal udtFileInfo As gTypCompareFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsPulldownMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsPulldownMenuM, gCstFileOpsPulldownMenuC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtManuMain)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "セレクションメニュー"

    '--------------------------------------------------------------------
    ' 機能      : セレクション設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSelectionMenu(ByRef udtSelectionMenu As gTypSetOpsSelectionMenu, _
                                   ByVal udtFileInfo As gTypCompareFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsSelectionMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsSelectionMenuM, gCstFileOpsSelectionMenuC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSelectionMenu)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPSグラフ設定"

    '--------------------------------------------------------------------
    ' 機能      : OPSグラフ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) OPSグラフ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : OPSグラフ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsGraph(ByRef udtOpsGraph As gTypSetOpsGraph, _
                                   ByVal udtFileInfo As gTypCompareFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsGraphM, gCstFileOpsGraphC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtOpsGraph)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "フリーディスプレイ"

    '--------------------------------------------------------------------
    ' 機能      : フリーディスプレイ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) フリーディスプレイ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーディスプレイ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsFreeDisplay(ByRef udtFreeDisplay As gTypSetOpsFreeDisplay, _
                                         ByVal udtFileInfo As gTypCompareFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeDisplay
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeDisplayM, gCstFileOpsFreeDisplayC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtFreeDisplay)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "トレンドグラフ"

    '--------------------------------------------------------------------
    ' 機能      : トレンドグラフ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) トレンドグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : トレンドグラフ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsTrendGraph(ByRef udtManuMain As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypCompareFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsTrendGraphM, gCstFileOpsTrendGraphC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtManuMain)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログフォーマット"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsLogFormat(ByRef udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                                       ByVal udtFileInfo As gTypCompareFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogFormatM, gCstFileOpsLogFormatC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetOpsLogFormat)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログフォーマットCHID"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマットCHID読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    ' ☆2012/10/26 K.Tanigawa
    '--------------------------------------------------------------------
    Private Function mLoadOpsLogIdData(ByRef udtSetOpsLogIdData As gTypSetOpsLogIdData, _
                                       ByVal udtFileInfo As gTypCompareFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean) As Integer


        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogIdData
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogIdDataM, gCstFileOpsLogIdDataC), mblnCFRead, mblnSaveRead, mblnCompileRead)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''  ☆2012/10/26 K.Tanigawa
                ' ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetOpsLogIdData)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

#Region "GWS設定CH設定"     '' 2014.02.04

    '--------------------------------------------------------------------
    ' 機能      : GWS設定読込     2014.02.04
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadopsGwsCh(ByRef udtSetGwsCh As gTypSetOpsGwsCh, _
                                  ByVal udtFileInfo As gTypCompareFileInfo, _
                                  ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGwsCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsGwsChName, mblnCFRead, mblnSaveRead, mblnCompileRead) & gCstFileOpsGwsChExt

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetGwsCh)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "CH変換テーブル"

    '--------------------------------------------------------------------
    ' 機能      : CH変換テーブル読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) CH変換テーブル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : CH変換テーブル保存処理を行う
    '--------------------------------------------------------------------
    'Private Function mLoadChConv(ByRef udtSetChConv As gTypSetChConv, _
    '                             ByVal udtFileInfo As gTypFileInfo, _
    '                             ByVal strPathBase As String) As Integer

    '    Try

    '        Dim intRtn As Integer = 0
    '        Dim intFileNo As Integer
    '        Dim strPathSave As String
    '        Dim strFullPath As String
    '        Dim strCurPathName As String = gCstPathChConv
    '        Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChConv, True)

    '        ''メッセージ更新
    '        lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

    '        ''システム設定のパスを作成
    '        strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

    '        ''フルパス作成
    '        strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

    '        ''ファイル存在確認
    '        If Not System.IO.File.Exists(strFullPath) Then

    '            Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
    '            intRtn = -1

    '        Else

    '            ''ファイルオープン
    '            intFileNo = FreeFile()
    '            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

    '            Try
    '                ''ファイル読込み
    '                FileGet(intFileNo, udtSetChConv)

    '                ''メッセージ出力
    '                Call mAddMsgList("Load complete. [" & strFullPath & "]")

    '            Catch ex As Exception
    '                Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
    '                intRtn = -1
    '            Finally
    '                FileClose(intFileNo)
    '            End Try

    '        End If

    '        Return intRtn

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Function

#End Region

#Region "ファイル更新情報"

    '--------------------------------------------------------------------
    ' 機能      : ファイル更新情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) ファイル更新情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ファイル更新情報保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadEditorUpdateInfo(ByRef udtSetEditorUpdateInfo As gTypSetEditorUpdateInfo, _
                                           ByVal udtFileInfo As gTypFileInfo, _
                                           ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathEditorUpdateInfo
            Dim strCurFileName As String = gCstFileEditorUpdateInfo

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetEditorUpdateInfo)

                    ''メッセージ出力
                    Call mAddMsgList("Load complete. [" & strFullPath & "]")

                Catch ex As Exception
                    Call mAddMsgList("Load Error!! [" & strFullPath & "] " & ex.Message & "")
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

End Class