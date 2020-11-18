Public Class frmFileAccess

#Region "定数定義"

    Private Const mCstProgressValueMaxSave As Integer = 65 '61      ' 2013.07.22 K.Fujimoto 59->61  2014.02.04 61->62  Ver1.9.3 2016.01.25 62->63  Ver2.0.7.8 PID用ﾄﾚﾝﾄﾞ追加 63->65
    Private Const mCstProgressValueMaxLoad As Integer = 58 '55      ' 2012/10/26 K.Tanigawa 53->55  2014.02.04 55->56  Ver1.9.3 2016.01.25 56->57

#End Region

#Region "変数定義"

    Private mintRtn As Integer
    Private mblnReadCompile As Boolean
    Private mudtFileInfo As gTypFileInfo
    Private mudtAccessMode As gEnmAccessMode
    Private mblnExit As Boolean
    Private mblnVersionUP As Boolean
    Private mudt As clsStructure

    Private CompCFRead As Boolean
    Private CompareRead As Boolean

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
    Friend Function gShow(ByVal udtFileInfo As gTypFileInfo, _
                          ByVal udtAccessMode As gEnmAccessMode, _
                          ByVal blnReadCompile As Boolean, _
                          ByVal blnExit As Boolean, _
                          ByVal blnVersionUP As Boolean, _
                          ByRef udt As clsStructure, _
                          ByVal CompCFReadFlg As Boolean, _
                          ByVal CompareReadFlg As Boolean) As Integer


        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileInfo = udtFileInfo
            mudtAccessMode = udtAccessMode
            mblnReadCompile = blnReadCompile
            mblnExit = blnExit
            mblnVersionUP = blnVersionUP
            mudt = udt
            CompCFRead = CompCFReadFlg
            CompareRead = CompareReadFlg

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
            Select Case mudtAccessMode
                Case gEnmAccessMode.amSave : Me.Text = "Save Setting"
                Case gEnmAccessMode.amLoad : Me.Text = "Load Setting"
            End Select

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

            Select Case mudtAccessMode
                Case gEnmAccessMode.amSave
                    gstrChSearchLog = ""
                    ''設定値保存処理
                    'Ver2.0.3.6 PT,JPT自動変換
                    gblExcelInDo = False
                    If mudt.SetEditorUpdateInfo.udtSave.bytChannel <> 0 Then
                        gblExcelInDo = True
                        Call gsubSetPTJPT()
                    Else
                        'Ver2.0.4.9 Excelﾌｧｲﾙが存在しないなら保存方向へ
                        With mudtFileInfo
                            Dim strExcel As String = System.IO.Path.Combine(System.IO.Path.Combine(.strFilePath, .strFileName), .strFileName & "_" & "list.xlsx")
                            If System.IO.File.Exists(strExcel) = False Then
                                gblExcelInDo = True
                            End If
                        End With
                    End If

                    If mSaveSetting() = 0 Then
                        'Ver2.0.4.2 Excel出力しないﾌﾗｸﾞあり
                        If gblExcelInDo = True And gblExcelOut = True Then
                            gblExcelInDo = False
                            With mudtFileInfo
                                'Ver2.0.3.2
                                '■■■EXCELコンバータ処理開始■■■
                                '編集起動の場合は、EXCELコンバータを必ず起動
                                'Excelが保管されてるﾌｫﾙﾀﾞへ処理指定ﾌｧｲﾙを生成する
                                Dim strExcelPath As String = System.IO.Path.Combine(gGetAppPath(), "EXCELCNV")
                                Dim strExcelFile As String = System.IO.Path.Combine(strExcelPath, "SMS55_mode.txt")

                                'ﾃﾞｰﾀパス作成
                                Dim strDataPath As String = System.IO.Path.Combine(.strFilePath, .strFileName)
                                strDataPath = System.IO.Path.Combine(strDataPath, gCstFolderNameSave)
                                strDataPath = System.IO.Path.Combine(strDataPath, gCstPathChannel)
                                Dim strCurFileName As String = ""
                                Dim strOutData As String = ""

                                Dim sw As IO.StreamWriter
                                sw = New IO.StreamWriter(strExcelFile, False, System.Text.Encoding.GetEncoding("Shift-JIS"))
                                'Mode
                                'Ver2.0.7.M (保安庁)
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then
                                    sw.WriteLine("12")
                                Else
                                    sw.WriteLine("2")
                                End If
                                'EXCELファイル名
                                'MPTファイル名
                                strCurFileName = .strFileName & "_" & gCstFileChannel
                                strOutData = System.IO.Path.Combine(strDataPath, strCurFileName)
                                sw.WriteLine(strOutData)
                                'ファイル名
                                sw.WriteLine(.strFileName)
                                'ｸﾞﾙｰﾌﾟﾌｧｲﾙ名
                                strCurFileName = .strFileName & "_" & gCstFileGroupM
                                strOutData = System.IO.Path.Combine(strDataPath, strCurFileName)
                                sw.WriteLine(strOutData)
                                'パス
                                strOutData = System.IO.Path.Combine(.strFilePath, .strFileName)
                                sw.WriteLine(strOutData)
                                '書き込み終了
                                If sw Is Nothing = False Then sw.Close()

                                'ExcelConverterキック
                                strExcelFile = System.IO.Path.Combine(strExcelPath, "CHList_EXCEL.exe")
                                Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(strExcelFile)
                                Dim _thread As System.Threading.Thread = Nothing
                                frmSplash.ShowSplash(_thread)
                                p.WaitForExit()
                                frmSplash.Close()
                                _thread.Abort()
                                _thread.Join()
                                _thread = Nothing

                                'Ver2.0.3.6 Excel処理でエラーが発生＝file作成したら画面へﾛｸﾞを出す。
                                Dim strFileLine() As String = Nothing
                                Dim strLogPath As String = System.IO.Path.Combine(.strFilePath, .strFileName)
                                strLogPath = System.IO.Path.Combine(strLogPath, "ErrLog_MtoE.txt")
                                If System.IO.File.Exists(strLogPath) = True Then
                                    'ｺﾝﾊﾟｲﾙ結果ﾌｧｲﾙが存在すれば読み込んでｴﾃﾞｨﾀのﾛｸﾞへ書き出し
                                    Dim sr As IO.StreamReader
                                    sr = New IO.StreamReader(strLogPath, System.Text.Encoding.GetEncoding("shift_jis"))
                                    Dim strFileData As String = sr.ReadToEnd()
                                    strFileLine = Split(strFileData, vbCrLf)
                                    sr.Close()
                                    gstrChSearchLog = ""
                                    For z As Integer = LBound(strFileLine) To UBound(strFileLine) Step 1
                                        gstrChSearchLog = gstrChSearchLog & strFileLine(z) & vbCrLf
                                    Next z
                                    '用済みﾛｸﾞは消してはならない
                                    'System.IO.File.Delete(strLogPath)
                                End If

                                'Call frmToolExcelCnv.gShow(0)
                            End With
                        End If
                        gblExcelInDo = False
                        '■■■EXCELコンバータ処理終了■■■

                        mintRtn = 0

                        ''保存成功で終了フラグTrueの場合は自動で画面を閉じる
                        If mblnExit Then Me.Close()

                    Else
                        mintRtn = -1
                    End If

                Case gEnmAccessMode.amLoad

                    ''設定値読込処理
                    If mLoadSetting() = 0 Then

                        ''読込処理成功時は自動で画面を閉じる
                        mintRtn = 0
                        Call Me.Close()

                    Else
                        mintRtn = -1
                    End If

            End Select

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

    'T.Ueki 2015/5/25
    '--------------------------------------------------------------------
    ' 機能      : フォルダ操作
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 
    ' 機能説明  : フォルダの作成、削除等の処理
    '--------------------------------------------------------------------
    Private Function Folder_Operation(ByVal FilePass As String, ByVal blnVerUP As Boolean, ByVal blnCFread As Boolean) As Integer

        'ファイルまでのパス( 例：C:\Users\Ueki_Takuya\Desktop\SMS-55MPT\ )
        Dim strPathBase As String = ""

        ''EditorInfo配下のフォルダパス
        Dim strPathVerInfoPrev As String = ""
        Dim strPathUpdateInfo As String = ""

        Dim strPathSaveNow As String = ""
        Dim strPathSavePrev As String = ""
        Dim strPathCompileNow As String = ""

        Dim strPathTempNow As String = ""

        Dim TostrPathCompileNow As String = ""

        Dim strPathCompilePrev As String = ""
        Dim strPathCompilePrevDel As String = ""


        With mudtFileInfo

            ''ファイルまでのパス( 例：C:\Users\Ueki_Takuya\Desktop\SMS-55MPT\ )
            strPathBase = .strFilePath

            ''ファイル名までのパス( 例：C:\Users\Ueki_Takuya\Desktop\SMS-55MPT\1649N )
            strPathBase = System.IO.Path.Combine(strPathBase, .strFileName)

            ''バージョン番号までのパス（コピー用）
            strPathSaveNow = System.IO.Path.Combine(strPathBase, .strFileVersion)

            'Ver.UPする場合
            If gudtFileInfo.blnVersionUP Then

                strPathSavePrev = System.IO.Path.Combine(strPathBase, .strFileVersionPrev)
                strPathCompileNow = System.IO.Path.Combine(strPathBase, .strFileVersion)

                TostrPathCompileNow = System.IO.Path.Combine(strPathBase, .strFileVersion & "\Temp")

                strPathCompilePrev = System.IO.Path.Combine(strPathBase, .strFileVersionPrev & "\Temp")
                strPathCompilePrevDel = System.IO.Path.Combine(strPathBase, .strFileVersionPrev & "\Temp\")

                strPathSaveNow = System.IO.Path.Combine(strPathSaveNow, gCstFolderNameSave)
                strPathSavePrev = System.IO.Path.Combine(strPathSavePrev, gCstFolderNameSave)
                strPathCompileNow = System.IO.Path.Combine(strPathCompileNow, gCstFolderNameCompile)
                strPathCompilePrev = System.IO.Path.Combine(strPathCompilePrev, gCstFolderNameCompile)

                'Ver.UPしない場合
            Else

                'Tempフォルダまでのパス( 例：C:\Users\Ueki Takuya\Desktop\SMS-55MPT\1610P\Temp\ )
                TostrPathCompileNow = System.IO.Path.Combine(strPathBase, .strFileVersion & "\Temp")

                strPathTempNow = strPathSaveNow & "\"

            End If

            ''EditorInfo配下のフォルダパス
            strPathVerInfoPrev = System.IO.Path.Combine(System.IO.Path.Combine(strPathSaveNow, gCstFolderNameEditorInfo), gCstFolderNameVerInfoPre)
            strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathSaveNow, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

            ''Save or Compile までのパス
            If mblnReadCompile Then
                strPathBase = System.IO.Path.Combine(strPathSaveNow, gCstFolderNameCompile)
            Else
                strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
            End If

        End With

    End Function

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
    ' 機能      : 設定値保存
    ' 返り値    : 0:成功、<>0:失敗数
    ' 引き数    : なし
    ' 機能説明  : 設定値保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSetting() As Integer
        Try

            Dim intRtn As Integer
            Dim strPathBase As String = ""
            Dim strPathVerInfoPrev As String = ""
            Dim strPathUpdateInfo As String = ""

            Dim strPathSaveNow As String = ""
            Dim strPathSavePrev As String = ""
            Dim strPathCompileNow As String = ""

            Dim strPathTempNow As String = ""

            Dim TostrPathCompileNow As String = ""

            Dim strPathCompilePrev As String = ""
            Dim strPathCompilePrevDel As String = ""

            Dim strErrLogTempPath As String     ''2015.10.26 Ver1.7.5
            Dim strErrLogPath As String         ''2015.10.26 Ver1.7.5

            ''バージョン番号までのファイルパス作成
            With mudtFileInfo

                ''ファイル名までのパス
                strPathBase = .strFilePath

                'strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)

                ''バージョン番号までのパス（コピー用）
                strPathSaveNow = System.IO.Path.Combine(strPathBase, .strFileVersion)
                strPathSavePrev = System.IO.Path.Combine(strPathBase, .strFileVersionPrev)
                strPathCompileNow = System.IO.Path.Combine(strPathBase, .strFileVersion)

                strPathTempNow = strPathSaveNow & "\"

                TostrPathCompileNow = System.IO.Path.Combine(strPathBase, .strFileVersion & "\Temp")

                strPathCompilePrev = System.IO.Path.Combine(strPathBase, .strFileVersionPrev & "\Temp")
                strPathCompilePrevDel = System.IO.Path.Combine(strPathBase, .strFileVersionPrev & "\Temp\")

                ''バージョン番号までのパス
                strPathBase = System.IO.Path.Combine(strPathBase, .strFileVersion)

                'strPathSaveNow = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))
                'strPathSavePrev = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersionPrev), "000"))
                'strPathCompileNow = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))

                'strPathTempNow = strPathSaveNow & "\"

                'TostrPathCompileNow = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000") & "\Temp")

                'strPathCompilePrev = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersionPrev), "000") & "\Temp")
                'strPathCompilePrevDel = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersionPrev), "000") & "\Temp\")

                ' ''バージョン番号までのパス
                'strPathBase = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))

                ''EditorInfo配下のフォルダパス
                strPathVerInfoPrev = System.IO.Path.Combine(System.IO.Path.Combine(strPathBase, gCstFolderNameEditorInfo), gCstFolderNameVerInfoPre)
                strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathBase, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

                ''Save or Compile までのパス
                If mblnReadCompile Then
                    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                Else
                    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
                End If

                ''Save or Compile までのパス（コピー用）
                strPathSaveNow = System.IO.Path.Combine(strPathSaveNow, gCstFolderNameSave)
                strPathSavePrev = System.IO.Path.Combine(strPathSavePrev, gCstFolderNameSave)
                strPathCompileNow = System.IO.Path.Combine(strPathCompileNow, gCstFolderNameCompile)
                strPathCompilePrev = System.IO.Path.Combine(strPathCompilePrev, gCstFolderNameCompile)

            End With

            ''Saveフォルダ作成
            If gMakeFolder(strPathBase) <> 0 Then
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                lblMessage.Text = "File save error!!"
                Return -1
            End If

            ''VerInfoPrevフォルダ作成
            If gMakeFolder(strPathVerInfoPrev) <> 0 Then
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathVerInfoPrev)
                lblMessage.Text = "File save error!!"
                Return -1
            End If

            ''UpdateInfoフォルダ作成
            If gMakeFolder(strPathUpdateInfo) <> 0 Then
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathUpdateInfo)
                lblMessage.Text = "File save error!!"
                Return -1
            End If

            ''バージョンアップ保存の場合は旧バージョンの設定ファイルをコピーする
            If gudtFileInfo.blnVersionUP Then

                ''メッセージ表示
                lblMessage.Text = "Prev ver file is copy..." : Call lblMessage.Refresh()

                ''Saveフォルダ
                If System.IO.Directory.Exists(strPathSavePrev) Then
                    If gCopyDirectory(strPathSavePrev, strPathSaveNow, gudtFileInfo.strFileVersionPrev, gudtFileInfo.strFileVersion) <> 0 Then
                        Call mAddMsgList("It failed in prev ver file copy. [Path]" & strPathSaveNow)
                        lblMessage.Text = "File save error!!"
                        Return -1
                    End If
                End If

                ''Compileフォルダ
                If System.IO.Directory.Exists(strPathCompilePrev) Then
                    If gCopyDirectory(strPathCompilePrev, strPathCompileNow) <> 0 Then
                        Call mAddMsgList("It failed in prev ver file copy. [Path]" & strPathSaveNow)
                        lblMessage.Text = "File save error!!"
                        Return -1
                    Else
                        ''Compileフォルダコピー後Tempフォルダ削除
                        System.IO.Directory.Delete(strPathCompilePrevDel, True)
                    End If
                End If

                ''UpdateInfoフォルダ    Ver1.8.3  2015.11.26
                '' Setup.iniﾌｧｲﾙが存在すればｺﾋﾟｰ
                If System.IO.Directory.Exists(strPathUpdateInfo) Then
                    Dim strPathIniNow As String
                    Dim strPathIniPrev As String

                    strPathIniNow = strPathUpdateInfo & "\" & gCstIniFile
                    strPathIniPrev = strPathUpdateInfo & "\" & gCstIniFile
                    If System.IO.File.Exists(strPathIniPrev) Then
                        System.IO.File.Copy(strPathIniPrev, strPathIniNow)
                    End If
                End If

            End If

            ''TempファイルにバックアップしていたCompileフォルダを正規場所に戻す    2013.12.18
            ''Compileフォルダ
            If System.IO.Directory.Exists(TostrPathCompileNow) Then
                'Ver2.0.5.8 SIO拡張ﾌｧｲﾙ特殊処理
                'SIO拡張ﾌｧｲﾙは増えたり減ったりするため、元のCompileﾌｫﾙﾀﾞにある該当ﾌｧｲﾙを削除する
                Call subCompileSIOext()

                If gCopyDirectory(TostrPathCompileNow, strPathTempNow) <> 0 Then
                    Call mAddMsgList("It failed in Temp file. [Path]" & strPathSaveNow)
                    lblMessage.Text = "File save error!!"
                    Return -1
                Else
                    '' 2015.10.26 Ver1.7.5 Tempﾌｫﾙﾀﾞ内のｴﾗｰﾘｽﾄを計測点ﾌｫﾙﾀﾞにｺﾋﾟｰ
                    strErrLogTempPath = TostrPathCompileNow & "\" & mudtFileInfo.strFileName & "_err.txt"
                    strErrLogPath = mudtFileInfo.strFilePath & "\" & mudtFileInfo.strFileName & "\" & mudtFileInfo.strFileName & "_err.txt"

                    If System.IO.File.Exists(strErrLogTempPath) Then    ' ｴﾗｰﾛｸﾞが存在する場合
                        If System.IO.File.Exists(strErrLogPath) Then    ' ｺﾋﾟｰ先にﾛｸﾞが存在する場合、削除
                            System.IO.File.Delete(strErrLogPath)        ' 削除
                        End If
                        System.IO.File.Copy(strErrLogTempPath, strErrLogPath)   ' 計測点ﾌｫﾙﾀﾞにｺﾋﾟｰ

                        System.IO.File.Delete(strErrLogTempPath)        ' 削除
                    End If
                    ''//
                    ''Compileフォルダコピー後Tempフォルダ削除
                    System.IO.Directory.Delete(TostrPathCompileNow, True)
                End If
            End If

            With prgBar

                ''プログレスバー初期化
                .Minimum = 0
                .Maximum = mCstProgressValueMaxSave
                .Value = 0

                '********************************************************
                '========================================================
                ''注意！！
                ''ここに出力ファイルを追加した場合、コンパイル出力にも
                ''同様の追加が必要になります！！
                ''追加箇所：frmCmpCompier - mOutputCompileFile
                '========================================================
                '********************************************************

                'システム設定データ書き込み
                intRtn += mSaveSystem(mudt.SetSystem, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytSystem) : .Value += 1 : Application.DoEvents()

                ''FU チャンネル情報書き込み
                intRtn += mSaveFuChannel(mudt.SetFu, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytFuChannel) : .Value += 1 : Application.DoEvents()

                ''チャンネル情報データ（表示名設定データ）書き込み
                intRtn += mSaveChDisp(mudt.SetChDisp, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChDisp) : .Value += 1 : Application.DoEvents()

                ''チャンネル情報書き込み
                intRtn += mSaveChannel(mudt.SetChInfo, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChannel) : .Value += 1 : Application.DoEvents()

                ''コンポジット情報書き込み
                intRtn += mSaveComposite(mudt.SetChComposite, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytComposite) : .Value += 1 : Application.DoEvents()

                ''グループ設定書き込み
                intRtn += mSaveGroup(mudt.SetChGroupSetM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytGroupM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveGroup(mudt.SetChGroupSetC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytGroupC) : .Value += 1 : Application.DoEvents()

                ''リポーズ入力設定書き込み
                intRtn += mSaveRepose(mudt.SetChGroupRepose, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytRepose) : .Value += 1 : Application.DoEvents()

                ''出力チャンネル設定書き込み
                intRtn += mSaveOutPut(mudt.SetChOutput, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytOutPut) : .Value += 1 : Application.DoEvents()

                ''論理出力設定書き込み
                intRtn += mSaveOrAnd(mudt.SetChAndOr, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytOrAnd) : .Value += 1 : Application.DoEvents()

                ''積算データ設定書き込み
                intRtn += mSaveChRunHour(mudt.SetChRunHour, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChRunHour) : .Value += 1 : Application.DoEvents()

                ''コントロール使用可／不可設定書き込み
                intRtn += mSaveCtrlUseNotuse(mudt.SetChCtrlUseM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveCtrlUseNotuse(mudt.SetChCtrlUseC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseC) : .Value += 1 : Application.DoEvents()

                ''SIO設定書き込み
                intRtn += mSaveChSio(mudt.SetChSio, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChSio) : .Value += 1 : Application.DoEvents()

                ''SIO設定CH設定書き込み
                For i As Integer = 0 To UBound(mudt.SetChSioCh)
                    intRtn += mSaveChSioCh(mudt.SetChSioCh(i), mudtFileInfo, strPathBase, i + 1, mudt.SetEditorUpdateInfo.udtSave.bytChSioCh(i)) : .Value += 1 : Application.DoEvents()
                Next

                'Ver2.0.5.8
                'SIO設定拡張設定書き込み ※プログレスバーには加算しない
                For i As Integer = 0 To UBound(mudt.SetChSioExt)
                    intRtn += mSaveChSioExt(mudt.SetChSioExt(i), mudtFileInfo, strPathBase, i + 1, mudt.SetEditorUpdateInfo.udtSave.bytChSioExt(i), mudt.SetChSio.udtVdr(i).shtKakuTbl) : .Value += 0 : Application.DoEvents()
                Next


                ''排ガス処理演算設定書き込み
                intRtn += mSaveExhGus(mudt.SetChExhGus, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytExhGus) : .Value += 1 : Application.DoEvents()

                ''延長警報書き込み
                intRtn += mSaveExtAlarm(mudt.SetExtAlarm, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytExtAlarm) : .Value += 1 : Application.DoEvents()

                ''タイマ設定書き込み
                intRtn += mSaveTimer(mudt.SetExtTimerSet, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytTimer) : .Value += 1 : Application.DoEvents()

                ''タイマ表示名称設定書き込み
                intRtn += mSaveTimerName(mudt.SetExtTimerName, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytTimerName) : .Value += 1 : Application.DoEvents()

                ''シーケンスID書き込み
                intRtn += mSaveSeqSequenceID(mudt.SetSeqID, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceID) : .Value += 1 : Application.DoEvents()

                ''シーケンス設定書き込み
                intRtn += mSaveSeqSequenceSet(mudt.SetSeqSet, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceSet) : .Value += 1 : Application.DoEvents()

                'リニアライズテーブル書き込み
                intRtn += mSaveSeqLinear(mudt.SetSeqLinear, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytSeqLinear) : .Value += 1 : Application.DoEvents()

                '演算式テーブル書き込み
                intRtn += mSaveSeqOperationExpression(mudt.SetSeqOpeExp, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytSeqOperationExpression) : .Value += 1 : Application.DoEvents()

                ''データ保存テーブル設定
                intRtn += mSaveChDataSaveTable(mudt.SetChDataSave, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChDataSaveTable) : .Value += 1 : Application.DoEvents()

                ''データ転送テーブル設定
                intRtn += mSaveChDataForwardTableSet(mudt.SetChDataForward, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChDataForwardTableSet) : .Value += 1 : Application.DoEvents()

                'OPSスクリーンタイトル
                intRtn += mSaveOpsScreenTitle(mudt.SetOpsScreenTitleM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsScreenTitle(mudt.SetOpsScreenTitleC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleC) : .Value += 1 : Application.DoEvents()

                ''プルダウンメニュー
                intRtn += mSaveManuMain(mudt.SetOpsPulldownMenuM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveManuMain(mudt.SetOpsPulldownMenuC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC) : .Value += 1 : Application.DoEvents()

                ''セレクションメニュー
                intRtn += mSaveSelectionMenu(mudt.SetOpsSelectionMenuM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveSelectionMenu(mudt.SetOpsSelectionMenuC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsSelectionMenuC) : .Value += 1 : Application.DoEvents()

                ''OPSグラフ設定
                intRtn += mSaveOpsGraph(mudt.SetOpsGraphM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsGraph(mudt.SetOpsGraphC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC) : .Value += 1 : Application.DoEvents()

                ''フリーグラフ    2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
                intRtn += mSaveOpsFreeGraph(mudt.SetOpsFreeGraphM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsFreeGraphM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsFreeGraph(mudt.SetOpsFreeGraphC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsFreeGraphC) : .Value += 1 : Application.DoEvents()

                ''フリーディスプレイ
                intRtn += mSaveOpsFreeDisplay(mudt.SetOpsFreeDisplayM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsFreeDisplayM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsFreeDisplay(mudt.SetOpsFreeDisplayC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsFreeDisplayC) : .Value += 1 : Application.DoEvents()

                ''トレンドグラフ
                intRtn += mSaveOpsTrendGraph(mudt.SetOpsTrendGraphM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsTrendGraph(mudt.SetOpsTrendGraphC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphC) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsTrendGraph_PID(mudt.SetOpsTrendGraphPID, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphPID) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsTrendGraph_PID(mudt.SetOpsTrendGraphPID, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphPID2) : .Value += 1 : Application.DoEvents()


                ''ログフォーマット
                intRtn += mSaveOpsLogFormat(mudt.SetOpsLogFormatM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsLogFormat(mudt.SetOpsLogFormatC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatC) : .Value += 1 : Application.DoEvents()

                ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
                intRtn += mSaveOpsLogIdData(mudt.SetOpsLogIdDataM, mudtFileInfo, strPathBase, True, mudt.SetEditorUpdateInfo.udtSave.bytOpsLogIdDataM) : .Value += 1 : Application.DoEvents()
                intRtn += mSaveOpsLogIdData(mudt.SetOpsLogIdDataC, mudtFileInfo, strPathBase, False, mudt.SetEditorUpdateInfo.udtSave.bytOpsLogIdDataC) : .Value += 1 : Application.DoEvents()

                '' ﾛｸﾞｵﾌﾟｼｮﾝ設定　Ver1.9.3 2016.01.25 
                intRtn += mSaveOpsLogOption(mudt.SetOpsLogOption, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytOpsLogOption) : .Value += 1 : Application.DoEvents()

                ''GWS設定CH設定書き込み 2014.02.04
                intRtn += mSaveOpsGwsCh(mudt.SetOpsGwsCh, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytOpsGwsCh) : .Value += 1 : Application.DoEvents()

                ''CH変換テーブル（前VerのCH変換テーブルをVerInfoPrevフォルダに保存する）
                intRtn += mSaveChConv(mudt.SetChConvPrev, mudtFileInfo, strPathVerInfoPrev, mudt.SetEditorUpdateInfo.udtSave.bytChConvPrev, mudt.SetChInfo) : .Value += 1 : Application.DoEvents()

                ''CH変換テーブル（現VerのCH変換テーブルをSaveフォルダに保存する）
                intRtn += mSaveChConv(mudt.SetChConvNow, mudtFileInfo, strPathBase, mudt.SetEditorUpdateInfo.udtSave.bytChConvNow, mudt.SetChInfo) : .Value += 1 : Application.DoEvents()

                ''ファイル更新情報
                intRtn += mSaveEditorUpdateInfo(mudt.SetEditorUpdateInfo, mudtFileInfo, strPathUpdateInfo) : .Value += 1 : Application.DoEvents()

                '' Setup.iniﾌｧｲﾙ書き込み   Ver1.8.3  2015.11.26
                SaveEditIni(strPathUpdateInfo & "\" & gCstIniFile)

                .Value = .Maximum : Application.DoEvents()

                ''メッセージ表示
                If intRtn <> 0 Then

                    ''失敗
                    lblMessage.Text = "Saving failed."
                    lblMessage.ForeColor = Color.Red

                Else

                    ''成功
                    lblMessage.Text = "The files have been saved successfully."
                    lblMessage.ForeColor = Color.Blue

                End If

                Return intRtn

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function

    Private Sub subCompileSIOext()
        Dim intPortNo As Integer = 1
        Dim strPathBase As String = ""
        Dim strCpath As String = ""
        Dim strOrgPath As String = ""
        Dim strFullPath As String = ""
        Dim strFullOrgPath As String = ""
        Dim strFullTempOrgPath As String = ""
        Dim strTempOrg As String = ""

        strPathBase = mudtFileInfo.strFilePath
        strPathBase = System.IO.Path.Combine(strPathBase, mudtFileInfo.strFileVersion)
        strTempOrg = System.IO.Path.Combine(strPathBase, "Temp")
        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
        strTempOrg = System.IO.Path.Combine(strTempOrg, gCstFolderNameCompile)
        strCpath = System.IO.Path.Combine(strPathBase, gCstPathChSioExt)
        strOrgPath = System.IO.Path.Combine(strPathBase, gCstFolderNameOrg)
        strOrgPath = System.IO.Path.Combine(strOrgPath, gCstPathChSioExt)
        strTempOrg = System.IO.Path.Combine(strTempOrg, gCstFolderNameOrg)
        strTempOrg = System.IO.Path.Combine(strTempOrg, gCstPathChSioExt)

        '2019.03.18 ファイル数変更
        Dim strCurFileName As String
        For intPortNo = 1 To 16 Step 1
            strCurFileName = mGetOutputFileName(mudtFileInfo, gCstFileChSioExtName, True) & Format(intPortNo, "00") & gCstFileChSioExtExt
            strFullPath = System.IO.Path.Combine(strCpath, strCurFileName)
            strFullOrgPath = System.IO.Path.Combine(strOrgPath, strCurFileName)
            strFullTempOrgPath = System.IO.Path.Combine(strTempOrg, strCurFileName)

            If System.IO.File.Exists(strFullPath) = True Then
                Call System.IO.File.Delete(strFullPath)
            End If
            If System.IO.File.Exists(strFullOrgPath) = True Then
                Call System.IO.File.Delete(strFullOrgPath)
            End If
            If System.IO.File.Exists(strFullTempOrgPath) = True Then
                'Call System.IO.File.Delete(strFullTempOrgPath)
            End If
        Next

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値読み込み
    ' 返り値    : 0:成功、<>0:失敗数
    ' 引き数    : なし
    ' 機能説明  : 設定値保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSetting() As Integer

        Try

            Dim fileNo As Integer = FreeFile()

            Dim Fso As New Scripting.FileSystemObject
            Dim intRtn As Integer
            Dim strPathBase As String = ""
            Dim strPathComp As String = ""
            Dim strPathVerInfoPrev As String = ""
            Dim strPathUpdateInfo As String = ""
            Dim FromTempFolder As String = ""
            Dim FromTempFolderCopy As String = ""
            Dim ToTempFolder As String = ""

            '読込完了までファイル未アクセス状態  T.Ueki 2016/6/27
            FileAccessFlg = False

            ''バージョン番号までのファイルパス作成
            With mudtFileInfo

                strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)

                'T.Ueki ファイル管理仕様変更
                'strPathBase = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))

                'Mimic用データパスファイル作成
                FileOpen(fileNo, AppPassTXT, OpenMode.Output)
                PrintLine(fileNo, strPathBase)
                Print(fileNo, AppPass)
                FileClose(fileNo)

                If CompareRead = False Then

                    'コピー先
                    FromTempFolder = strPathBase & "\Temp\"
                    FromTempFolderCopy = strPathBase & "\Temp"

                    'コピー元
                    ToTempFolder = strPathBase & "\Compile"

                    ''Tempフォルダ内にCompileフォルダをコピー
                    If System.IO.Directory.Exists(FromTempFolder) Then
                        'Tempフォルダが存在する場合は一端すべて削除
                        System.IO.Directory.Delete(FromTempFolder, True)

                        'Tempフォルダを再作成
                        System.IO.Directory.CreateDirectory(FromTempFolderCopy)

                        'Compileフォルダがある場合のみコピー  2013.08.07 K.Fujimoto
                        If System.IO.Directory.Exists(ToTempFolder) Then
                            Fso.CopyFolder(ToTempFolder, FromTempFolder, True)
                        End If
                    Else
                        'Tempフォルダが存在しない場合は作成
                        System.IO.Directory.CreateDirectory(FromTempFolderCopy)

                        'Compileフォルダがある場合のみコピー  2013.08.07 K.Fujimoto
                        If System.IO.Directory.Exists(ToTempFolder) Then
                            Fso.CopyFolder(ToTempFolder, FromTempFolder, True)
                        End If
                    End If

                End If

                strPathVerInfoPrev = System.IO.Path.Combine(System.IO.Path.Combine(strPathBase, gCstFolderNameEditorInfo), gCstFolderNameVerInfoPre)
                strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathBase, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

                If mblnReadCompile Then
                    '2014/5/14 T.Ueki
                    If CompCFRead = True Then
                        'CFｶｰﾄﾞからの読み込みはフォルダはそのまま
                    Else
                        strPathComp = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                    End If

                Else
                    strPathComp = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
                End If

            End With


            With prgBar

                ''プログレスバー初期化
                .Minimum = 0
                .Maximum = mCstProgressValueMaxLoad
                .Value = 0

                '' Setup.iniﾌｧｲﾙ読み込み (最初に読み込む) 2018.12.13 倉重
                ReadEditIni(strPathUpdateInfo & "\" & gCstIniFile)

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

                ''グループ設定読み込み（植木）
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

                'Ver2.0.5.8
                'SIO設定拡張読み込み ※プログレスバーにプラスはしない
                For i As Integer = 0 To UBound(mudt.SetChSioExt)
                    intRtn += mLoadChSioExt(mudt.SetChSioExt(i), mudtFileInfo, strPathBase, i + 1) : .Value += 0 : Application.DoEvents()
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

                '---------------------------------------------------------------------------
                'フリーディスプレイとトレンドグラフは空データ出力なので読み込みは行わない
                '---------------------------------------------------------------------------
                '' ''フリーディスプレイ
                ''intRtn += mLoadOpsFreeDisplay(mudt.SetOpsFreeDisplayM, mudtFileInfo, strPathBase, True) : .Value += 1: Application.DoEvents()
                ''intRtn += mLoadOpsFreeDisplay(mudt.SetOpsFreeDisplayC, mudtFileInfo, strPathBase, False) : .Value += 1: Application.DoEvents()

                '' ''トレンドグラフ
                ''intRtn += mSaveOpsTrendGraph(mudt.SetOpsTrendGraphM, mudtFileInfo, strPathBase, True) : .Value += 1: Application.DoEvents()
                ''intRtn += mSaveOpsTrendGraph(mudt.SetOpsTrendGraphC, mudtFileInfo, strPathBase, False) : .Value += 1: Application.DoEvents()

                'PID
                intRtn += mLoadOpsTrendGraph_PID(mudt.SetOpsTrendGraphPID, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsTrendGraph_PID(mudt.SetOpsTrendGraphPIDprev, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()


                ''2014/5/14 コンパイル比較の場合は読み込まない　T.Ueki
                If mblnReadCompile = False Then
                    ''ログフォーマット
                    intRtn += mLoadOpsLogFormat(mudt.SetOpsLogFormatM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                    intRtn += mLoadOpsLogFormat(mudt.SetOpsLogFormatC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()
                End If

                ''ログフォーマットCHID 　☆2012/10/26 K.Tanigawa
                intRtn += mLoadOpsLogIdData(mudt.SetOpsLogIdDataM, mudtFileInfo, strPathBase, True) : .Value += 1 : Application.DoEvents()
                intRtn += mLoadOpsLogIdData(mudt.SetOpsLogIdDataC, mudtFileInfo, strPathBase, False) : .Value += 1 : Application.DoEvents()

                '' Ver1.9.3 2016.01.25 ﾛｸﾞｵﾌﾟｼｮﾝ設定追加
                intRtn += mLoadOpsLogOption(mudt.SetOpsLogOption, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''GWS設定CH設定読み込み 2014.02.04
                intRtn += mLoadopsGwsCh(mudt.SetOpsGwsCh, mudtFileInfo, strPathBase) : .Value += 1 : Application.DoEvents()

                ''CH変換テーブル
                If mblnVersionUP Then

                    '==============================
                    ''バージョンアップの場合
                    '==============================
                    ''コンパイルファイル読み込みの場合
                    If mblnReadCompile = False Then

                        ''バージョンアップ前のコンパイルフォルダから読み込む
                        intRtn += mLoadChConv(mudt.SetChConvPrev, mudtFileInfo, strPathComp)

                    Else

                        ''バージョンアップ前のSaveフォルダから読み込む
                        intRtn += mLoadChConv(mudt.SetChConvPrev, mudtFileInfo, strPathBase)

                    End If

                    ''現VerのCH変換テーブルを初期化する
                    Call gInitSetChConv(mudt.SetChConvNow)

                Else

                    '==============================
                    ''バージョンアップではない場合
                    '==============================
                    ''コンパイルファイル読み込みの場合
                    If mblnReadCompile = False Then
                        ''前バージョンのCH変換テーブルをVerInfoPrevフォルダから読み込む
                        intRtn += mLoadChConv(mudt.SetChConvPrev, mudtFileInfo, strPathVerInfoPrev)
                    Else

                        ''現バージョンのCH変換テーブルをSaveフォルダから読み込む
                        'intRtn += mLoadChConv(mudt.SetChConvNow, mudtFileInfo, strPathBase)
                        intRtn += mLoadChConv(mudt.SetChConvPrev, mudtFileInfo, strPathBase)
                    End If

                End If

                .Value += 1 : Application.DoEvents()

                ''コンパイルファイル読み込みの場合
                If mblnReadCompile = False Then
                    ''ファイル更新情報
                    intRtn += mLoadEditorUpdateInfo(mudt.SetEditorUpdateInfo, mudtFileInfo, strPathUpdateInfo) : .Value += 1 : Application.DoEvents()
                End If

                ''コンパイルファイル読み込み時はチャンネル ID - NO 変換を行う
                If mblnReadCompile Then
                    'compare時は変換しない T.Ueki 2015/5/14
                    If CompareRead = False Then
                        intRtn += mConvChidToChno()
                        .Value += 1 : Application.DoEvents()
                    End If
                End If

                .Value = .Maximum

                ''メッセージ表示
                If intRtn <> 0 Then

                    ''失敗
                    lblMessage.Text = "Loading the file failed."
                    lblMessage.ForeColor = Color.Red

                    'ソフトウェア起動から一度でもファイルアクセスした場合  T.Ueki 2016/6/27
                    FileAccessFlg = False

                Else

                    ''成功
                    lblMessage.Text = "Loading the file succeeded."
                    lblMessage.ForeColor = Color.Blue

                    'ソフトウェア起動から一度でもファイルアクセスした場合  T.Ueki 2016/6/27
                    FileAccessFlg = True

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

            ' ''SIO設定
            'Call gConvChannelChSIO(mudt.SetChSio, intErrCnt, strMsg, gEnmConvMode.cmID_NO, False)
            'Call mOutputChannelConvMsg("SIO Channel Setting", intErrCnt, strMsg)
            'intErrCnt = intErrCnt * -1 : intRtn += intErrCnt

            ''SIO設定CH設定はメンバにIDとNoを持っているので読み込み時の変換処理は必要なし
            'For i As Integer = 0 To UBound(mudt.SetChSioCh)
            '    Call gConvChannelChSIOCh(mudt.SetChSioCh(i), intErrCnt, strMsg, gEnmConvMode.cmID_NO)
            '    Call mOutputChannelConvMsg("SIO Channel Port " & i + 1 & " Setting", intErrCnt, strMsg)
            '    intErrCnt = intErrCnt * -1 : intRtn += intErrCnt
            'Next

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
    Private Function mGetOutputFileName(ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strFileName As String, _
                                        ByVal blnReadCompile As Boolean) As String

        Dim strRtn As String = ""

        Try

            If blnReadCompile Then

                strRtn = strFileName

            Else
                'バージョンアップならアップ後の名称を使用する T.Ueki
                If udtFileInfo.blnVersionUP = True Then
                    strRtn = udtFileInfo.strFileVersion & "_" & strFileName
                Else
                    'ファイル管理仕様変更
                    strRtn = udtFileInfo.strFileName & "_" & strFileName
                End If

                'strRtn = udtFileInfo.strFileName & "_" & _
                '         Format(CInt(udtFileInfo.strFileVersion), "000") & "_" & _
                '         strFileName

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
    ' 機能      : システム設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSystem(ByVal udtSetSystem As gTypSetSystem, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSystem
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSystem, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileSystem)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSystem.udtHeader, udtFileInfo.strFileVersion, gCstRecsSystem, gCstSizeSystem, , , , gCstFnumSystem)


            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try

                ''ファイル書き込み
                FilePut(intFileNo, udtSetSystem)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : システム設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSystem(ByRef udtSetSystem As gTypSetSystem, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSystem As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSystem
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSystem, mblnReadCompile)

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
    ' 機能      : ＦＵチャンネル情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveFuChannel(ByVal udtSetFuChannel As gTypSetFu, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathFuChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileFuChannel, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileFuChannel)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetFuChannel.udtHeader, udtFileInfo.strFileVersion, gCstRecsFuChannel, gCstSizeFuChannel, , , , gCstFnumFuChannel)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetFuChannel)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ＦＵチャンネル情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadFuChannel(ByRef udtSetFuChannel As gTypSetFu, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathFuChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileFuChannel, mblnReadCompile)

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
    ' 機能      : チャンネル情報データ保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDisp(ByVal udtSetFuChannel As gTypSetChDisp, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDisp
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDisp, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChDisp)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetFuChannel.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDisp, gCstSizeChDisp, , , , gCstFnumChDisp)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetFuChannel)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報データ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDisp(ByRef udtSetFuChannel As gTypSetChDisp, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDisp
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDisp, mblnReadCompile)

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
    ' 機能      : チャンネル情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChannel(ByVal udtSetChannel As gTypSetChInfo, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChannel, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChannel)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChannel.udtHeader, udtFileInfo.strFileVersion, gGetRecCntChannel(udtSetChannel), gCstSizeChannel, , , , gCstFnumChannel)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChannel)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChannel(ByRef udtSetChannel As gTypSetChInfo, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChannel, mblnReadCompile)

            Dim strBkUpFullPath As String = ""

            'Ver2.0.3.6 Excel取込の場合、読み込むファイルを変更
            If gblExcelInDo = True Then
                strCurFileName = mGetOutputFileName(udtFileInfo, "ex_" & gCstFileChannel, mblnReadCompile)
            End If

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)
            strBkUpFullPath = strFullPath

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = -1

            Else

                '▼▼▼ 20110214 チャンネル設定ファイルの可変長出力対応 ▼▼▼▼▼▼▼▼▼
                If mblnReadCompile Then
                    ''構造体読み用のファイルを作成する
                    Call mRemakeChannelFileLoad(strFullPath)
                End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChannel)
                    FileClose(intFileNo)

                    '▼▼▼ 20110214 チャンネル設定ファイルの可変長出力対応 ▼▼▼▼▼▼▼▼▼
                    If mblnReadCompile Then
                        ''構造体読み用に作成されたファイルを削除する
                        Call System.IO.File.Delete(strFullPath)
                    End If
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    'Ver2.0.3.6 Excel取込の場合、読み込んだファイルを削除
                    If gblExcelInDo = True Then
                        Call System.IO.File.Delete(strBkUpFullPath)
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
    ' 機能      : コンポジット情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) コンポジット情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveComposite(ByVal udtSetComposite As gTypSetChComposite, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathComposite
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileComposite, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileComposite)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetComposite.udtHeader, udtFileInfo.strFileVersion, gGetRecCntComposite(udtSetComposite), gCstSizeComposite, , , , gCstFnumComposite)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetComposite)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : コンポジット情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) コンポジット情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadComposite(ByRef udtSetComposite As gTypSetChComposite, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathComposite
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileComposite, mblnReadCompile)

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
    ' 機能      : グループ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) グループ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveGroup(ByVal udtSetGroup As gTypSetChGroupSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByVal blnMachinery As Boolean, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathGroup
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileGroupM, gCstFileGroupC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileGroupM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileGroupC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGroup.udtHeader, udtFileInfo.strFileVersion, gCstRecsGroup, gCstSizeGroup, , , , IIf(blnMachinery, gCstFnumGroupM, gCstFnumGroupC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetGroup)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : グループ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) グループ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadGroup(ByRef udtSetGroup As gTypSetChGroupSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathGroup
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileGroupM, gCstFileGroupC), mblnReadCompile)

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
    ' 機能      : リポーズ入力設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) リポーズ入力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リポーズ入力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveRepose(ByVal udtSetGroupRepose As gTypSetChGroupRepose, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathRepose
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileRepose, mblnReadCompile)
            Dim udt48 As gTypSetChGroupRepose48 = Nothing ''2018.12.13 倉重 グループリポーズが48の場合に使用する配列
            Dim intListRow As Integer = 0       ''2018.12.13 倉重 カウンタ変数
            Dim intListDetailRow As Integer = 0 ''2018.12.13 倉重 カウンタ変数

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileRepose)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGroupRepose.udtHeader, udtFileInfo.strFileVersion, gCstRecsRepose, gCstSizeRepose, , , , gCstFnumRepose)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                ''ADDのチェックボックスがチェックでない場合に格納する2018.12.13 倉重
                If g_bytGREPNUM = 0 Then
                    udt48.InitArray()
                    For intListRow = LBound(udt48.udtRepose) To UBound(udt48.udtRepose)
                        udt48.udtRepose(intListRow).InitArray()
                    Next

                    ''ヘッダの読み込み
                    udt48.udtHeader.strVersion = udtSetGroupRepose.udtHeader.strVersion
                    udt48.udtHeader.strDate = udtSetGroupRepose.udtHeader.strDate
                    udt48.udtHeader.strTime = udtSetGroupRepose.udtHeader.strTime
                    udt48.udtHeader.shtRecs = udtSetGroupRepose.udtHeader.shtRecs
                    udt48.udtHeader.shtSize1 = udtSetGroupRepose.udtHeader.shtSize1
                    udt48.udtHeader.shtSize2 = udtSetGroupRepose.udtHeader.shtSize2
                    udt48.udtHeader.shtSize3 = udtSetGroupRepose.udtHeader.shtSize3
                    udt48.udtHeader.shtSize4 = udtSetGroupRepose.udtHeader.shtSize4
                    udt48.udtHeader.shtSize5 = udtSetGroupRepose.udtHeader.shtSize5

                    ''リポーズデータの読み込み
                    For intListRow = LBound(udt48.udtRepose) To UBound(udt48.udtRepose)
                        ''CH ID
                        udt48.udtRepose(intListRow).shtChId = udtSetGroupRepose.udtRepose(intListRow).shtChId
                        ''データ種別コード
                        udt48.udtRepose(intListRow).shtData = udtSetGroupRepose.udtRepose(intListRow).shtData

                        For intListDetailRow = 0 To UBound(udt48.udtRepose(intListRow).udtReposeInf)
                            ''CH ID
                            udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId = udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId
                            ''マスク値
                            udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask = udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask
                        Next
                    Next
                    FilePut(intFileNo, udt48)
                Else
                    FilePut(intFileNo, udtSetGroupRepose)
                End If

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : リポーズ入力設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) リポーズ入力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リポーズ入力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadRepose(ByRef udtSetGroupRepose As gTypSetChGroupRepose, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathRepose
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileRepose, mblnReadCompile)
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
                    ''ファイル読込み
                    ''ADDのチェックボックスがチェックでない場合に格納する2018.12.13 倉重
                    If g_bytGREPNUM = 0 Then
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
    ' 機能      : 出力チャンネル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 出力チャンネル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 出力チャンネル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOutPut(ByVal udtSetCHOutPut As gTypSetChOutput, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOutPut
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOutPut, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOutPut)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetCHOutPut.udtHeader, udtFileInfo.strFileVersion, gCstRecsOutPut, gCstSizeOutPut, , , , gCstFnumOutPut)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetCHOutPut)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 出力チャンネル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 出力チャンネル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 出力チャンネル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOutPut(ByRef udtSetCHOutPut As gTypSetChOutput, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOutPut
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOutPut, mblnReadCompile)

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
    ' 機能      : 論理出力設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 論理出力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 論理出力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOrAnd(ByVal udtSetCHAndOr As gTypSetChAndOr, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOrAnd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOrAnd, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOrAnd)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetCHAndOr.udtHeader, udtFileInfo.strFileVersion, gCstRecsOrAnd, gCstSizeOrAnd, , , , gCstFnumOrAnd)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetCHAndOr)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      :論理出力設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 論理出力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 論理出力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOrAnd(ByRef udtSetCHAndOr As gTypSetChAndOr, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOrAnd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOrAnd, mblnReadCompile)

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
    ' 機能      : 積算データ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 積算データ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 積算データ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChRunHour(ByVal udtSetChRunHour As gTypSetChRunHour, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChAdd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChAdd, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)


            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChAdd)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChRunHour.udtHeader, udtFileInfo.strFileVersion, gCstRecsChAdd, gCstSizeChAdd, , , , gCstFnumChAdd)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChRunHour)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      :積算データ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 積算データ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 積算データ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChRunHour(ByRef udtSetChRunHour As gTypSetChRunHour, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChAdd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChAdd, mblnReadCompile)

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
    ' 機能      : 排ガス処理演算設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 排ガス処理演算設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 排ガス処理演算設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveExhGus(ByVal udtSetExhGus As gTypSetChExhGus, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExhGus
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExhGus, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileExhGus)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetExhGus.udtHeader, udtFileInfo.strFileVersion, gCstRecsExhGus, gCstSizeExhGus, , , , gCstFnumExhGus)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetExhGus)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 排ガス処理演算設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 排ガス処理演算設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 排ガス処理演算設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadexhGus(ByRef udtSetExhGus As gTypSetChExhGus, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExhGus
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExhGus, mblnReadCompile)

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
    ' 機能      : コントロール使用可／不可設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) コントロール使用可／不可設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : コントロール使用可／不可設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveCtrlUseNotuse(ByVal udtSetTimer As gTypSetChCtrlUse, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathCtrlUseNouse
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileCtrlUseNouseM, gCstFileCtrlUseNouseC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileCtrlUseNouseM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileCtrlUseNouseC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimer.udtHeader, udtFileInfo.strFileVersion, gCstRecsCtrlUseNouse, gCstSizeCtrlUseNouse, , , , IIf(blnMachinery, gCstFnumCtrlUseNouseM, gCstFnumCtrlUseNouseC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimer)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可／不可設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) コントロール使用可／不可設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : コントロール使用可／不可設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadCtrlUseNotuse(ByRef udtSetTimer As gTypSetChCtrlUse, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathCtrlUseNouse
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileCtrlUseNouseM, gCstFileCtrlUseNouseC), mblnReadCompile)
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
                bFileFg = SaveTempCtrlUseNotUseFile(strFullPath, strPathBase, strCurFileName, 1)
                '' Ver1.9.6 2016.02.17 ﾌｧｲﾙｻｲｽﾞ拡張のため、ﾌｧｲﾙｻｲｽﾞが旧ﾀｲﾌﾟならばﾃﾞｰﾀを追加して保存
                ''Dim fs As New System.IO.FileStream(strFullPath, System.IO.FileMode.Open, IO.FileAccess.ReadWrite)
                ''Dim nLength As Long = fs.Length

                ''If nLength = gOldUseNotUseFileSize Then

                ''    Dim strTemp As String = strPathBase & "\Temp"
                ''    If System.IO.Directory.Exists(strTemp) Then

                ''    End If

                ''    Dim os(gAmxControlUseNotUse - 1) As Byte
                ''    Dim ns((gAmxControlUseNotUse - 32) * 326 - 1) As Byte
                ''    fs.Read(os, 0, gAmxControlUseNotUse)
                ''    fs.Close()


                ''    ''fs.Write(ns, 0, ns.Length)
                ''    ''fs.Seek(0, IO.SeekOrigin.End)
                ''    ''fs.Write(ns, 0, ns.Length)
                ''    ''fs.Close()

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
    ' 機能      : SIO設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSio(ByVal udtSetSio As gTypSetChSio, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSio
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSio, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChSio)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSio.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSio, gCstSizeChSio1, gCstSizeChSio2, , , gCstFnumChSio)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSio)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : SIO設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChSio(ByRef udtSetSio As gTypSetChSio, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSio
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSio, mblnReadCompile)

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
    ' 機能      : SIO設定CH設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定CH設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSioCh(ByVal udtSetSioCh As gTypSetChSioCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioChName, mblnReadCompile) & Format(intPortNo, "00") & gCstFileChSioChExt

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChSioChName & Format(intPortNo, "00") & gCstFileChSioChExt)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSioCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSioCh, gCstSizeChSioCh, , , , gCstFnumChSioChStart + (intPortNo - 1))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSioCh)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : SIO設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChSioCh(ByRef udtSetSioCh As gTypSetChSioCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioChName, mblnReadCompile) & Format(intPortNo, "00") & gCstFileChSioChExt

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

#Region "SIO設定拡張設定"
    '--------------------------------------------------------------------
    ' 機能      : SIO設定拡張設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定拡張設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSioExt(ByVal udtSetSioExt As gTypSetChSioExt, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer, _
                                  ByRef bytOutputFlg As Byte,
                                  ByVal pintKakuTbl As Integer) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioExt
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioExtName, mblnReadCompile) & Format(intPortNo, "00") & gCstFileChSioExtExt

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChSioExtName & Format(intPortNo, "00") & gCstFileChSioExtExt)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) And pintKakuTbl = 1 Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成しない
            'Call gMakeHeader(udtSetSioCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSioCh, gCstSizeChSioCh, , , , gCstFnumChSioChStart + (intPortNo - 1))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            '拡張しないならそのまま処理抜け
            If pintKakuTbl <> 1 Then
                Return 0
            End If

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSioExt)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : SIO設定拡張設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定拡張設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChSioExt(ByRef udtSetSioExt As gTypSetChSioExt, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioExt
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioExtName, mblnReadCompile) & Format(intPortNo, "00") & gCstFileChSioExtExt

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                'ファイルが存在しない場合は読み飛ばし
                'Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                intRtn = 0
            Else
                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetSioExt)

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
    ' 機能      : 延長警報設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 延長警報設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 延長警報設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveExtAlarm(ByVal udtSetExAlm As gTypSetExtAlarm, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExtAlarm
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExtAlarm, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileExtAlarm)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetExAlm.udtHeader, udtFileInfo.strFileVersion, gCstRecsExtAlarm, gCstSizeExtAlarm1, gCstSizeExtAlarm2, , , gCstFnumExtAlarm)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetExAlm)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 延長警報設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 延長警報設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 延長警報設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadExtAlarm(ByRef udtSetExAlm As gTypSetExtAlarm, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExtAlarm
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExtAlarm, mblnReadCompile)

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
    ' 機能      : タイマ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) タイマ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveTimer(ByVal udtSetTimer As gTypSetExtTimerSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimer
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimer, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileTimer)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimer.udtHeader, udtFileInfo.strFileVersion, gCstRecsTimer, gCstSizeTimer, , , , gCstFnumTimer)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimer)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : タイマ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) タイマ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadTimer(ByRef udtSetTimer As gTypSetExtTimerSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimer
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimer, mblnReadCompile)

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
    ' 機能      : タイマ表示名称設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) タイマ表示名称設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ表示名称設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveTimerName(ByVal udtSetTimerName As gTypSetExtTimerName, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimerName
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimerName, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileTimerName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimerName.udtHeader, udtFileInfo.strFileVersion, gCstRecsTimerName, gCstSizeTimerName, , , , gCstFnumTimerName)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimerName)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : タイマ表示名称設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) タイマ表示名称設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ表示名称設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadTimerName(ByRef udtSetTimerName As gTypSetExtTimerName, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimerName
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimerName, mblnReadCompile)

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
    ' 機能      : シーケンスID保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) シーケンスID構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : シーケンスID保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqSequenceID(ByVal udtSetSeqSequenceID As gTypSetSeqID, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceID
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceID, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileSeqSequenceID)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqSequenceID.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqSequenceID, gCstSizeSeqSequenceID, , , , gCstFnumSeqSequenceID)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqSequenceID)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : シーケンスID読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) シーケンスID構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  :シーケンスID保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqSequenceID(ByRef udtSetSeqSequenceID As gTypSetSeqID, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceID
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceID, mblnReadCompile)

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
    ' 機能      : シーケンス設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) シーケンス設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : シーケンス設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqSequenceSet(ByVal udtSetSeqSequenceSet As gTypSetSeqSet, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceSet, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileSeqSequenceSet)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqSequenceSet.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqSequenceSet, gCstSizeSeqSequenceSet, , , , gCstFnumSeqSequenceSet)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqSequenceSet)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : シーケンス設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) シーケンス設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  :シーケンス設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqSequenceSet(ByRef udtSetSeqSequenceSet As gTypSetSeqSet, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceSet, mblnReadCompile)

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
    ' 機能      : リニアライズテーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) リニアライズテーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リニアライズテーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqLinear(ByVal udtSetSeqLinear As gTypSetSeqLinear, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqLinear
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqLinear, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileSeqLinear)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqLinear.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqLinear, gCstSizeSeqLinear1, gCstSizeSeqLinear2, , , gCstFnumSeqLinear)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqLinear)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : リニアライズテーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) リニアライズテーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リニアライズテーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqLinear(ByRef udtSetSeqLinear As gTypSetSeqLinear, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqLinear
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqLinear, mblnReadCompile)

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
    ' 機能      : 演算式テーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 演算式テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 演算式テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqOperationExpression(ByVal udtSetSeqOpeExp As gTypSetSeqOperationExpression, _
                                                 ByVal udtFileInfo As gTypFileInfo, _
                                                 ByVal strPathBase As String, _
                                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqOperationExpression
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqOperationExpression, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileSeqOperationExpression)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqOpeExp.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqOperationExpression, gCstSizeSeqOperationExpression, , , , gCstFnumSeqOperationExpression)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqOpeExp)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 演算式テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) 演算式テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 演算式テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSeqOperationExpression(ByRef udtSetSeqOpeExp As gTypSetSeqOperationExpression, _
                                                 ByVal udtFileInfo As gTypFileInfo, _
                                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqOperationExpression
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqOperationExpression, mblnReadCompile)

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
    ' 機能      : データ保存テーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ保存テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ保存テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDataSaveTable(ByVal udtSetChDataTable As gTypSetChDataSave, _
                                          ByVal udtFileInfo As gTypFileInfo, _
                                          ByVal strPathBase As String, _
                                          ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataSaveTable
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataSaveTable, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChDataSaveTable)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataTable.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDataSaveTable, gCstSizeChDataSaveTable, , , , gCstFnumChDataSaveTable)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataTable)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データ保存テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ保存テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ保存テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDataSaveTable(ByRef udtSetChDataSaveTable As gTypSetChDataSave, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataSaveTable
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataSaveTable, mblnReadCompile)

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
    ' 機能      : データ転送テーブル設定
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDataForwardTableSet(ByVal udtSetChDataForwardTableSet As gTypSetChDataForward, _
                                                ByVal udtFileInfo As gTypFileInfo, _
                                                ByVal strPathBase As String, _
                                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataForwardTableSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataForwardTableSet, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileChDataForwardTableSet)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataForwardTableSet.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDataForwardTableSet, gCstSizeChDataForwardTableSet, , , , gCstFnumChDataForwardTableSet)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataForwardTableSet)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChDataForwardTableSet(ByRef udtSetChDataForwardTableSet As gTypSetChDataForward, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataForwardTableSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataForwardTableSet, mblnReadCompile)

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
    ' 機能      : OPSスクリーンタイトル保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) OPSスクリーンタイトル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsScreenTitle(ByVal udtSetOpsScreenTitle As gTypSetOpsScreenTitle, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMchine As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsScreenTitle
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMchine, gCstFileOpsScreenTitleM, gCstFileOpsScreenTitleC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsScreenTitleM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsScreenTitleC)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                'If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                'Return 0
            End If

            'Ver2.0.7.B ScreenTitleは強制変更
            Call gInitSetOpsDisp(udtSetOpsScreenTitle)


            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOpsScreenTitle.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsScreenTitle, gCstSizeOpsScreenTitle, , , , IIf(blnMchine, gCstFnumOpsScreenTitleM, gCstFnumOpsScreenTitleC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsScreenTitle)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : OPSスクリーンタイトル読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) OPSスクリーンタイトル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsScreenTitle(ByRef udtSetOpsScreenTitle As gTypSetOpsScreenTitle, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsScreenTitle
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsScreenTitleM, gCstFileOpsScreenTitleC), mblnReadCompile)

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
    ' 機能      : プルダウンメニュー
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveManuMain(ByVal udtManuMain As gTypSetOpsPulldownMenu, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsPulldownMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsPulldownMenuM, gCstFileOpsPulldownMenuC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsPulldownMenuM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsPulldownMenuC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtManuMain.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsPulldownMenu, gCstSizeOpsPulldownMenu, , , , IIf(blnMachinery, gCstFnumOpsPulldownMenuM, gCstFnumOpsPulldownMenuC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtManuMain)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadManuMain(ByRef udtManuMain As gTypSetOpsPulldownMenu, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsPulldownMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsPulldownMenuM, gCstFileOpsPulldownMenuC), mblnReadCompile)

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
    ' 機能      : セレクションメニュー
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSelectionMenu(ByVal udtSelectionMenu As gTypSetOpsSelectionMenu, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsSelectionMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsSelectionMenuM, gCstFileOpsSelectionMenuC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsSelectionMenuM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsSelectionMenuC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSelectionMenu.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsSelectionMenu, gCstSizeOpsSelectionMenu, , , , IIf(blnMachinery, gCstFnumOpsSelectionMenuM, gCstFnumOpsSelectionMenuC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSelectionMenu)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : セレクション設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadSelectionMenu(ByRef udtSelectionMenu As gTypSetOpsSelectionMenu, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsSelectionMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsSelectionMenuM, gCstFileOpsSelectionMenuC), mblnReadCompile)

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
    ' 機能      : OPSグラフ設定書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) OPSグラフ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : OPSグラフ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsGraph(ByVal udtOpsGraph As gTypSetOpsGraph, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsGraphM, gCstFileOpsGraphC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsGraphM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsGraphC)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtOpsGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsGraph, , , , , IIf(blnMachinery, gCstFnumOpsGraphM, gCstFnumOpsGraphC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsGraph)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : OPSグラフ設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) OPSグラフ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : OPSグラフ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsGraph(ByRef udtOpsGraph As gTypSetOpsGraph, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsGraphM, gCstFileOpsGraphC), mblnReadCompile)

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


#Region "フリーグラフ"

    '--------------------------------------------------------------------
    ' 機能      : フリーグラフ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) フリーグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーグラフ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsFreeGraph(ByVal udtFreeGraph As gTypSetOpsFreeGraph, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeGraphM, gCstFileOpsFreeGraphC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsFreeGraphM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsFreeGraphC)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtFreeGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsFreeGraph, gCstSizeOpsFreeGraph, , , , IIf(blnMachinery, gCstFnumOpsFreeGraphM, gCstFnumOpsFreeGraphC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtFreeGraph)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "フリーディスプレイ"

    '--------------------------------------------------------------------
    ' 機能      : フリーディスプレイ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) フリーディスプレイ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーディスプレイ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsFreeDisplay(ByVal udtFreeDisplay As gTypSetOpsFreeDisplay, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeDisplay
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeDisplayM, gCstFileOpsFreeDisplayC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsFreeDisplayM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsFreeDisplayC)


            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtFreeDisplay.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsFreeDisplay, gCstSizeOpsFreeDisplay, , , , IIf(blnMachinery, gCstFnumOpsFreeDisplayM, gCstFnumOpsFreeDisplayC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtFreeDisplay)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : フリーディスプレイ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) フリーディスプレイ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーディスプレイ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsFreeDisplay(ByRef udtFreeDisplay As gTypSetOpsFreeDisplay, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeDisplay
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeDisplayM, gCstFileOpsFreeDisplayC), mblnReadCompile)

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
    ' 機能      : トレンドグラフ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) トレンドグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : トレンドグラフ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsTrendGraph(ByVal udtManuMain As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsTrendGraphM, gCstFileOpsTrendGraphC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsTrendGraphM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsTrendGraphC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtManuMain.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , IIf(blnMachinery, gCstFnumOpsTrendGraphM, gCstFnumOpsTrendGraphC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtManuMain)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    'PID専用ﾄﾚﾝﾄﾞｸﾞﾗﾌ
    Private Function mSaveOpsTrendGraph_PID(ByVal udtManuMain As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = ""
            If blnMachinery = True Then
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID, mblnReadCompile)
            Else
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID2, mblnReadCompile)
            End If

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsTrendGraphPID)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsTrendGraphPID2)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            If blnMachinery = True Then
                Call gMakeHeader(udtManuMain.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , gCstFnumOpsTrendGraphPID)
            Else
                Call gMakeHeader(udtManuMain.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , gCstFnumOpsTrendGraphPID2)
            End If


            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtManuMain)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    '--------------------------------------------------------------------
    ' 機能      : トレンドグラフ読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) トレンドグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : トレンドグラフ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsTrendGraph(ByRef udtManuMain As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsTrendGraphM, gCstFileOpsTrendGraphC), mblnReadCompile)

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

    'PID専用
    Private Function mLoadOpsTrendGraph_PID(ByRef udtManuMain As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = ""
            If blnMachinery = True Then
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID, mblnReadCompile)
            Else
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID2, mblnReadCompile)
            End If

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                'PID用ファイルは、途中から加わったファイルのため、存在しない場合は新規生成
                'Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                'intRtn = -1
                Call gInitSetOpsTrendGraph(udtManuMain)
                Call gInitSetOpsTrendGraph(gudt.SetOpsTrendGraphPIDprev)
                mudt.SetEditorUpdateInfo.udtSave.bytOpsTrendGraphPID = 1

                'Ver2.0.7.T コメントアウト
                'gblExcelInDo = True

                'メッセージ出力
                Call mAddMsgList("Load complete. [" & strFullPath & "]")
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
    ' 機能      : ログフォーマット保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogFormat(ByVal udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogFormatM, gCstFileOpsLogFormatC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsLogFormatM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsLogFormatC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOpsLogFormat.udtHeader, udtFileInfo.strFileVersion, gCstSizeOpsLogFormat, gCstRecsOpsLogFormat, , , , IIf(blnMachinery, gCstFnumOpsLogFormatM, gCstFnumOpsLogFormatC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsLogFormat)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsLogFormat(ByRef udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogFormatM, gCstFileOpsLogFormatC), mblnReadCompile)

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
    ' 機能      : ログフォーマットCHID保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    ' ☆2012/10/26 K.Tanigawa
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogIdData(ByVal udtSetOpsLogIdData As gTypSetOpsLogIdData, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogIdData
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogIdDataM, gCstFileOpsLogIdDataC), mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsLogIdDataM)
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsLogIdDataC)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOpsLogIdData.udtheader, udtFileInfo.strFileVersion, gCstRecsOpsLogIdData, gCstSizeOpsLogIdData, , , , IIf(blnMachinery, gCstFnumOpsLogIdDataM, gCstFnumOpsLogIdDataC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsLogIdData)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

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
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean) As Integer


        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogIdData
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogIdDataM, gCstFileOpsLogIdDataC), mblnReadCompile)

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

#Region "ログオプション"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogOption(ByVal udtSetOpsLogOption As gTypLogOption, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsLogOption, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsLogOption)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            ''Call gMakeHeader(udtSetOpsLogOption.udtHeader, udtFileInfo.strFileVersion, gCstSizeOpsLogFormat, gCstRecsOpsLogFormat, , , , IIf(blnMachinery, gCstFnumOpsLogFormatM, gCstFnumOpsLogFormatC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsLogOption)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) ﾛｸﾞｵﾌﾟｼｮﾝ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログオプション設定データ読み込み処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadOpsLogOption(ByRef udtSetOpsLogOption As gTypLogOption, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsLogOption, mblnReadCompile)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                '' ﾌｧｲﾙが存在しない場合でもｴﾗｰにはしない

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetOpsLogOption)

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
    ' 機能      : GWS設定CH設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定CH設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsGwsCh(ByVal udtSetGwsCh As gTypSetOpsGwsCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGwsCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsGwsChName, mblnReadCompile) & gCstFileOpsGwsChExt

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            'Ver2.0.7.B 無関係なファイルを削除する
            Call subDelNoShipFile(udtFileInfo, strPathSave, gCstFileOpsGwsChName & gCstFileOpsGwsChExt)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGwsCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsGwsCh, gCstSizeOpsGwsCh, , , , gCstFnumOpsGwsChStart)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetGwsCh)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : GWS設定読込     2014.02.04
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadopsGwsCh(ByRef udtSetGwsCh As gTypSetOpsGwsCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGwsCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsGwsChName, mblnReadCompile) & gCstFileOpsGwsChExt

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
    ' 機能      : CH変換テーブル保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) CH変換テーブル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : CH変換テーブル保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChConv(ByVal udtSetChConv As gTypSetChConv, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte,
                                 ByRef udtSetChannel As gTypSetChInfo) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChConv
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChConv, True)

            ''メッセージ更新
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgList("Save cancel.   [" & strFullPath & "] not output. because it is not updated.")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChConv.udtHeader, udtFileInfo.strFileVersion, gCstRecsChConv, gCstSizeChConv, gGetChConvPacketSize(udtSetChannel), , , gCstFnumChConv)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChConv)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : CH変換テーブル読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) CH変換テーブル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : CH変換テーブル保存処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChConv(ByRef udtSetChConv As gTypSetChConv, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChConv
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChConv, True)

            ''メッセージ更新
            lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                'Ver2.0.2.0 メッセージ出さない
                'Call mAddMsgList("Load Error!! The file [" & strFullPath & "] doesn't exist.")
                'intRtn = -1
                intRtn = 0
            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChConv)

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

#Region "ファイル更新情報"

    '--------------------------------------------------------------------
    ' 機能      : ファイル更新情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ファイル更新情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ファイル更新情報保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveEditorUpdateInfo(ByVal udtSetChDataTable As gTypSetEditorUpdateInfo, _
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
            lblMessage.Text = "saving " & strCurFileName : Call lblMessage.Refresh()

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgList("Output failed. [" & strFullPath & "]")
                Call mAddMsgList("It failed in making the folder. [Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataTable.udtHeader, udtFileInfo.strFileVersion, gCstRecsEditorUpdateInfo, gCstSizeEditorUpdateInfo, , , , gCstFnumEditorUpdateInfo)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataTable)

                ''メッセージ出力
                Call mAddMsgList("Save complete. [" & strFullPath & "]")

            Catch ex As Exception
                Call mAddMsgList("Save Error!! [" & strFullPath & "] " & ex.Message & "")
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

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


#Region "無関係なファイルをみつけて削除する"
    Private Sub subDelNoShipFile(ByVal udtFileInfo As gTypFileInfo, ByVal strPath As String, ByVal strFileName As String)
        Try
            Dim i As Integer

            'フォルダの存在チェック
            If System.IO.Directory.Exists(strPath) = False Then
                Exit Sub
            End If

            Dim files As String() = System.IO.Directory.GetFiles(strPath, "*" & strFileName, System.IO.SearchOption.AllDirectories)
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, strFileName, mblnReadCompile)
            Dim strTrueFile As String = System.IO.Path.Combine(strPath, strCurFileName)

            For i = 0 To UBound(files) Step 1
                If strTrueFile <> files(i) Then
                    '変なファイルのため削除
                    Call IO.File.Delete(files(i))
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

End Class