Public Class frmFileSelect

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileMode As gEnmFileMode
    Private mudtFileInfoSave As gTypFileInfo
    Private mudtFileInfoTemp As gTypFileInfo
    Private mudt As clsStructure
    Private mblnUseVersionUP As Boolean

    '2014/5/14 T.Ueki
    Private CompareRead As Boolean
    Private CFReadFlg As Boolean
    Private CompareSet As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:キャンセル
    ' 　　　    : 1:処理成功
    ' 　　　    :-1:処理失敗（失敗あり）
    ' 引き数    : ARG1 - (I ) ファイルモード
    ' 　　　    : ARG1 - ( O) ファイル情報構造体
    ' 機能説明  : 画面表示を行い戻り値を返す
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileMode As gEnmFileMode, _
                          ByRef udtFileInfo As gTypFileInfo, _
                          ByRef udt As clsStructure, _
                          ByVal blnUseVersionUp As Boolean, _
                          ByVal CompareFlg As Boolean, _
                          ByVal CFCardReadFlg As Boolean, _
                          ByVal CompSet As Boolean) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数の保存
            mudtFileMode = udtFileMode
            mudtFileInfoSave = udtFileInfo
            mudtFileInfoTemp = udtFileInfo
            mudt = udt
            mblnUseVersionUP = blnUseVersionUp


            '2014/5/14 T.Ueki
            CompareRead = CompareFlg
            CFReadFlg = CFCardReadFlg
            CompareSet = CompSet

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            If mintRtn <> 0 Then

                ''決定された場合はファイル情報を更新する
                udtFileInfo = mudtFileInfoTemp

                If udtFileMode = gEnmFileMode.fmEdit Then
                    udt = mudt
                End If

            Else

                ''キャンセルされた場合はファイル情報を元に戻す
                udtFileInfo = mudtFileInfoSave

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
    Private Sub frmFileSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            chkExcel.Visible = False

            '■外販
            '外販の場合、Excelチェック,VerUP,ReadCompileFile,保安庁チェックは消す
            If gintNaiGai = 1 Then
                chkExcel.Checked = False
                chkExcel.Visible = False
                chkVersionUP.Visible = False
                chkCompile.Visible = False

                'Ver2.0.7.X 新規時保安庁
                chkHoan.Visible = False
            End If
            'JSH版の場合Excelチェックはデフォルトoff
            If gintNaiGai = 2 Then
                chkExcel.Checked = False
            End If

            With mudtFileInfoTemp

                Select Case mudtFileMode
                    Case gEnmFileMode.fmNew
                        'Ver2.0.7.X 新規時保安庁
                        chkHoan.Visible = True

                        ''新規の場合
                        Me.Text = "File Select - New"
                        txtFilePath.Text = .strFilePath
                        txtFileName.Text = ""

                        'T,Ueki フォルダ仕様管理変更
                        'numVersion.Value = 1

                        cmdRef.Enabled = True
                        txtFilePath.Enabled = True
                        txtFileName.Enabled = True

                        'T,Ueki フォルダ仕様管理変更
                        txtFileNameNew.Enabled = False
                        'numVersion.Enabled = False

                        chkVersionUP.Enabled = mblnUseVersionUP
                        chkCompile.Enabled = False

                        'Ver2.0.6.2 新規時端子表印刷のレンジは印刷しないとする
                        g_bytTermRange = 1

                        'Ver2.0.7.4 新規時の基板ﾊﾞｰｼﾞｮﾝ印刷は「しない」とする
                        g_bytTerVer = 1

                        'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
                        g_bytTerDIMsg = 1

                        Call txtFileName.Focus()
                        Call cmdOK.Focus()

                    Case gEnmFileMode.fmEdit
                        'Ver2.0.7.X 新規時保安庁
                        chkHoan.Visible = False

                        chkExcel.Visible = True

                        '■外販
                        '外販の場合、Excelチェック,VerUP,ReadCompileFileは消す
                        If gintNaiGai = 1 Then
                            chkExcel.Checked = False
                            chkExcel.Visible = False
                            chkVersionUP.Visible = False
                            chkCompile.Visible = False
                        End If
                        'JSH版の場合Excelチェックはデフォルトoff
                        If gintNaiGai = 2 Then
                            chkExcel.Checked = False
                        End If

                        ''更新の場合
                        Me.Text = "File Select - Open"

                        If CFReadFlg = True Then
                            txtFileName.Enabled = False
                        End If

                        txtFilePath.Text = .strFilePath
                        txtFileName.Text = .strFileName


                        'T,Ueki フォルダ仕様管理変更
                        'numVersion.Value = My.Settings.SelectVersion
                        cmdRef.Enabled = True
                        txtFilePath.Enabled = True

                        'T,Ueki フォルダ仕様管理変更
                        '2015/4/23
                        'txtFileName.Enabled = True
                        'txtFileName.Enabled = False
                        'numVersion.Enabled = True

                        chkVersionUP.Enabled = mblnUseVersionUP
                        chkCompile.Enabled = True

                        'T,Ueki フォルダ仕様管理変更
                        'numVersionUP.Value = IIf(numVersion.Value = numVersion.Maximum, numVersion.Value, numVersion.Value + 1)

                    Case gEnmFileMode.fmRename
                        'Ver2.0.7.X 新規時保安庁
                        chkHoan.Visible = False

                        ''名称変更の場合
                        Me.Text = "File Select - Save as"

                        txtFilePath.Text = .strFilePath
                        txtFileName.Text = .strFileName
                        'numVersion.Enabled = True
                        'numVersion.Value = My.Settings.SelectVersion
                        cmdRef.Enabled = False
                        txtFilePath.Enabled = False
                        txtFileName.Enabled = True
                        chkVersionUP.Enabled = mblnUseVersionUP
                        chkCompile.Enabled = False

                End Select

                ''バージョンコンボ設定
                Call mSetComboVersion(mudtFileMode, mudtFileInfoTemp)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォルダ参照ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォルダ選択ダイアログを表示する
    '--------------------------------------------------------------------
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click

        Try

            Dim strFilePath As String = ""
            Dim strFileName As String = ""
            Dim strVersions() As String = Nothing
            Dim blnExistVersion As Boolean
            Dim blnExistCompile As Boolean

            With mudtFileInfoTemp

                ''初期フォルダ設定
                If System.IO.Directory.Exists(txtFilePath.Text.Trim()) Then
                    fdgFolder.SelectedPath = txtFilePath.Text.Trim()
                Else
                    fdgFolder.SelectedPath = "C:\"
                End If

                ''フォルダ選択ダイアログ表示
                If fdgFolder.ShowDialog = Windows.Forms.DialogResult.OK Then

                    ''保存フォルダとして正しいフォルダが選択されたかチェック
                    If mChkSelectFolder(fdgFolder.SelectedPath, blnExistVersion, blnExistCompile) Then

                        Select Case mudtFileMode
                            Case gEnmFileMode.fmNew

                                ''新規の場合はそのまま表示
                                txtFilePath.Text = fdgFolder.SelectedPath
                                txtFileName.Text = ""

                            Case gEnmFileMode.fmEdit

                                ''ファイル情報表示
                                Call mGetPathAndFileName(fdgFolder.SelectedPath, .strFilePath, .strFileName)
                                txtFilePath.Text = .strFilePath
                                txtFileName.Text = .strFileName

                                ' ''更新の場合はバージョンコンボ設定
                                'Call mSetComboVersion(mudtFileMode, mudtFileInfoTemp)
                                'cmbVersion.SelectedIndex = cmbVersion.Items.Count - 1

                                If Not blnExistVersion Then

                                    ''バージョンフォルダが存在せずコンパイルフォルダのみが存在する場合
                                    If blnExistCompile Then
                                        'numVersion.Enabled = False
                                        chkCompile.Checked = True
                                    End If

                                    'Else

                                    ' ''バージョンフォルダが存在する場合は最新バージョン表示
                                    'Call mSetVersionNum(mudtFileMode, mudtFileInfoTemp)

                                End If

                        End Select

                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : モード別の処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            Dim intRtn As Integer
            Dim strPathNew As String = ""
            Dim strPathOld As String = ""

            With mudtFileInfoTemp

                ''入力チェック
                'If Not mChkSelectFolder(txtFilePath.Text) Then Return
                If Not mChkInput() Then Return

                ''画面設定値を構造体に格納
                .strFilePath = txtFilePath.Text
                .strFileName = txtFileName.Text

                'T.Ueki ファイル管理仕様変更
                '.strFileVersion = numVersion.Text

                ''モード毎に処理を分岐
                Select Case mudtFileMode
                    Case gEnmFileMode.fmNew

                        ''フォルダ作成
                        'strPathNew = System.IO.Path.Combine(.strFilePath, .strFileName)
                        'If gMakeFolder(strPathNew) <> 0 Then
                        '    Call MessageBox.Show("It failed in making the folder." & vbNewLine & vbNewLine & _
                        '                         strPathNew, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    Return
                        'End If

                        ''新規の場合のフォルダ名   2013.11.29
                        mudtFileInfoTemp.strFileVersion = txtFileName.Text
                        mudtFileInfoTemp.strFileVersionPrev = ""

                        '▼▼▼ 20110705 NEW時iniファイルフォルダに保存してあるMainMenu.datを読み込む ▼▼▼▼▼▼▼▼▼
                        ''新規の場合は構造体を初期化する
                        Call gInitOutputStructure(gudt)

                        'Ver2.0.4.7
                        '新規作成時、計測点リストはGR36にあらかじめﾃﾞｰﾀが入ったﾃﾞﾌｫﾙﾄﾃﾞｰﾀとする。
                        Call gLoadMPTdefault()


                        'Ver2.0.7.X 新規作成時保安庁対応
                        If chkHoan.Checked = True Then
                            g_bytHOAN = 1
                        End If

                        'Ver2.0.1.8 VDUは違うMainMenu.datを読み込む
                        ''MainMenu.datを読み込む
                        Call gLoadMenuMain(gudt.SetOpsPulldownMenuM)
                        Call gLoadMenuMain(gudt.SetOpsPulldownMenuC, True)

                        ''SelectionMenu.datを読み込む(デフォルト)　2015.01.20
                        Call gLoadSelectionMenuMain(gudt.SetOpsSelectionMenuM)
                        Call gLoadSelectionMenuMain(gudt.SetOpsSelectionMenuC)
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                        intRtn = 1

                    Case gEnmFileMode.fmEdit
                        gstrChSearchLog = ""

                        'Ver2.0.3.2
                        'Excelが存在しないならコンバータは起動させない
                        Dim strChk As String = System.IO.Path.Combine(System.IO.Path.Combine(.strFilePath, .strFileName), .strFileName & "_" & "list.xlsx")
                        If System.IO.File.Exists(strChk) = True And chkExcel.Checked = True Then
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
                            sw.WriteLine("1")
                            'EXCELファイル名
                            strCurFileName = .strFileName & "_" & "list.xlsx"
                            strOutData = System.IO.Path.Combine(System.IO.Path.Combine(.strFilePath, .strFileName), strCurFileName)
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
                            'Call frmToolExcelCnv.gShow(0)

                            'Ver2.0.3.6 Excel処理でエラーが発生＝file作成したら画面へﾛｸﾞを出す。
                            Dim strFileLine() As String = Nothing
                            Dim strLogPath As String = System.IO.Path.Combine(.strFilePath, .strFileName)
                            strLogPath = System.IO.Path.Combine(strLogPath, "ErrLog_EtoM.txt")
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
                                '用済みLogは消してはならない
                                'System.IO.File.Delete(strLogPath)
                            End If

                            'Ver2.0.3.6
                            'Excel読込した場合、必ず更新ﾌﾗｸﾞを立てる
                            gblExcelInDo = True
                            '■■■EXCELコンバータ処理終了■■■
                        End If


                        ''各設定ファイルから設定値を読み込み構造体に設定
                        intRtn = frmFileAccess.gShow(mudtFileInfoTemp, gEnmAccessMode.amLoad, chkCompile.Checked, False, chkVersionUP.Checked, mudt, CFReadFlg, CompareSet)

                        ''読み込み成功の場合は 1 、読み込み失敗の場合は -1 を戻り値に設定する
                        intRtn = IIf(intRtn = 0, 1, -1)

                        'Ver2.0.3.6 Excelから取り込んだ場合、PT,JPT変換処理を行う
                        If gblExcelInDo = True And intRtn = 1 Then
                            Call gsubSetPTJPT()
                        End If

                        ''バージョンアップの場合は情報設定
                        If chkVersionUP.Checked Then
                            mudtFileInfoTemp.blnVersionUP = True
                            mudtFileInfoTemp.strFileVersion = txtFileNameNew.Text
                            mudtFileInfoTemp.strFileVersionPrev = txtFileName.Text
                            mudtFileInfoTemp.strFileName = mudtFileInfoTemp.strFileVersion  '' 2013.11.29

                            '全ファイルの更新フラグを立てる
                            gblnUpdateAll = True

                        Else
                            mudtFileInfoTemp.blnVersionUP = False
                            mudtFileInfoTemp.strFileVersion = txtFileName.Text
                            mudtFileInfoTemp.strFileVersionPrev = 0
                        End If

                        ' ''バージョンアップの場合は情報設定
                        'If chkVersionUP.Checked Then
                        '    mudtFileInfoTemp.blnVersionUP = True
                        '    mudtFileInfoTemp.strFileVersion = numVersionUP.Text
                        '    mudtFileInfoTemp.strFileVersionPrev = numVersion.Text
                        'Else
                        '    mudtFileInfoTemp.blnVersionUP = False
                        '    mudtFileInfoTemp.strFileVersion = numVersion.Text
                        '    mudtFileInfoTemp.strFileVersionPrev = 0
                        'End If

                        '' Ver1.9.0 2015.12.15  ｺﾒﾝﾄになっていたが、ｺﾒﾝﾄ解除して
                        '設定値保存
                        My.Settings.SelectPath = mudtFileInfoTemp.strFilePath
                        My.Settings.SelectFile = mudtFileInfoTemp.strFileName
                        My.Settings.SelectVersion = mudtFileInfoTemp.strFileVersion
                        Call My.Settings.Save()

                    Case gEnmFileMode.fmRename

                        ''新旧フォルダ名作成
                        strPathNew = System.IO.Path.Combine(mudtFileInfoTemp.strFilePath, mudtFileInfoTemp.strFileName)
                        strPathOld = System.IO.Path.Combine(mudtFileInfoSave.strFilePath, mudtFileInfoSave.strFileName)

                        ''ファイル名が変更されていない場合
                        If strPathNew = strPathOld Then
                            Call MessageBox.Show("Please change the file name.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Call txtFileName.Focus()
                            Return
                        End If

                        ''確認メッセージ
                        If MessageBox.Show("Do you rename the file?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            Return
                        Else

                            ''全ファイルの更新フラグを立てる
                            gblnUpdateAll = True

                            'Rename用にバージョン更新
                            mudtFileInfoTemp.strFileVersionPrev = .strFileVersion
                            mudtFileInfoTemp.blnVersionUP = True
                            mudtFileInfoTemp.strFileVersion = txtFileName.Text

                            Call gInitSetEditorUpdateInfo(gudt.SetEditorUpdateInfo)

                            'mudtFileInfoTemp.strFileName = ""

                            ' ''フォルダをコピー
                            'Call gCopyDirectory(strPathOld, strPathNew, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)

                            ' ''存在するファイル全てをリネーム
                            'Call mRenameAllFile(strPathNew, mudtFileInfoSave.strFileName, mudtFileInfoTemp.strFileName)

                            ' ''メッセージ表示
                            'Call MessageBox.Show("File rename complete.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            intRtn = 1

                        End If

                End Select

            End With

            mintRtn = intRtn
            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキストフォーカス取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : テキストを選択する
    '--------------------------------------------------------------------
    Private Sub txtFileName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFileName.Enter

        Try


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイルパステキストキープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFilePath_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilePath.KeyPress

        Try

            e.Handled = gCheckTextInput(255, sender, e.KeyChar, False, , , True)

            If e.KeyChar = "?"c Then e.Handled = True
            If e.KeyChar = "/"c Then e.Handled = True
            If e.KeyChar = "*"c Then e.Handled = True
            If e.KeyChar = """"c Then e.Handled = True
            If e.KeyChar = "<"c Then e.Handled = True
            If e.KeyChar = ">"c Then e.Handled = True
            If e.KeyChar = "|"c Then e.Handled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイルパステキスト検証
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFilePath_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFilePath.Validating

        Try

            txtFileName.Text = txtFileName.Text.Replace("?", "")
            txtFileName.Text = txtFileName.Text.Replace("/", "")
            txtFileName.Text = txtFileName.Text.Replace("*", "")
            txtFileName.Text = txtFileName.Text.Replace("""", "")
            txtFileName.Text = txtFileName.Text.Replace(">", "")
            txtFileName.Text = txtFileName.Text.Replace("<", "")
            txtFileName.Text = txtFileName.Text.Replace("|", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキストキープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFileName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFileName.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

            ''半角のアンダーバーとハイフン以外、記号の使用不可
            If e.KeyChar = "\"c Then e.Handled = True
            If e.KeyChar = "?"c Then e.Handled = True
            If e.KeyChar = "/"c Then e.Handled = True
            If e.KeyChar = ":"c Then e.Handled = True
            If e.KeyChar = "*"c Then e.Handled = True
            If e.KeyChar = """"c Then e.Handled = True
            If e.KeyChar = "<"c Then e.Handled = True
            If e.KeyChar = ">"c Then e.Handled = True
            If e.KeyChar = "|"c Then e.Handled = True

            If e.KeyChar = "!"c Then e.Handled = True
            If e.KeyChar = "#"c Then e.Handled = True
            If e.KeyChar = "$"c Then e.Handled = True
            If e.KeyChar = "%"c Then e.Handled = True
            If e.KeyChar = "&"c Then e.Handled = True
            If e.KeyChar = "'"c Then e.Handled = True
            If e.KeyChar = "("c Then e.Handled = True
            If e.KeyChar = ")"c Then e.Handled = True
            If e.KeyChar = "="c Then e.Handled = True
            If e.KeyChar = "~"c Then e.Handled = True
            If e.KeyChar = "{"c Then e.Handled = True
            If e.KeyChar = "}"c Then e.Handled = True
            If e.KeyChar = "`"c Then e.Handled = True
            If e.KeyChar = "+"c Then e.Handled = True
            If e.KeyChar = "."c Then e.Handled = True
            If e.KeyChar = ","c Then e.Handled = True
            If e.KeyChar = ";"c Then e.Handled = True
            If e.KeyChar = "["c Then e.Handled = True
            If e.KeyChar = "]"c Then e.Handled = True
            If e.KeyChar = "@"c Then e.Handled = True
            If e.KeyChar = "^"c Then e.Handled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキスト検証
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFileName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFileName.Validating

        Try

            ''半角のアンダーバーとハイフン以外、記号の使用不可
            txtFileName.Text = txtFileName.Text.Replace("\", "")
            txtFileName.Text = txtFileName.Text.Replace("?", "")
            txtFileName.Text = txtFileName.Text.Replace("/", "")
            txtFileName.Text = txtFileName.Text.Replace(":", "")
            txtFileName.Text = txtFileName.Text.Replace("*", "")
            txtFileName.Text = txtFileName.Text.Replace("""", "")
            txtFileName.Text = txtFileName.Text.Replace(">", "")
            txtFileName.Text = txtFileName.Text.Replace("<", "")
            txtFileName.Text = txtFileName.Text.Replace("|", "")

            txtFileName.Text = txtFileName.Text.Replace("!", "")
            txtFileName.Text = txtFileName.Text.Replace("#", "")
            txtFileName.Text = txtFileName.Text.Replace("$", "")
            txtFileName.Text = txtFileName.Text.Replace("%", "")
            txtFileName.Text = txtFileName.Text.Replace("&", "")
            txtFileName.Text = txtFileName.Text.Replace("'", "")
            txtFileName.Text = txtFileName.Text.Replace("(", "")
            txtFileName.Text = txtFileName.Text.Replace(")", "")
            txtFileName.Text = txtFileName.Text.Replace("=", "")
            txtFileName.Text = txtFileName.Text.Replace("~", "")
            txtFileName.Text = txtFileName.Text.Replace("{", "")
            txtFileName.Text = txtFileName.Text.Replace("}", "")
            txtFileName.Text = txtFileName.Text.Replace("`", "")
            txtFileName.Text = txtFileName.Text.Replace("+", "")
            txtFileName.Text = txtFileName.Text.Replace(",", "")
            txtFileName.Text = txtFileName.Text.Replace(".", "")
            txtFileName.Text = txtFileName.Text.Replace(";", "")
            txtFileName.Text = txtFileName.Text.Replace("]", "")
            txtFileName.Text = txtFileName.Text.Replace("[", "")
            txtFileName.Text = txtFileName.Text.Replace("@", "")
            txtFileName.Text = txtFileName.Text.Replace("^", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バージョンアップチェック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コントロールの表示/非表示を設定する
    '--------------------------------------------------------------------
    Private Sub chkVersionUP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVersionUP.CheckedChanged

        Try

            'T.Ueki バージョンアップ仕様変更
            Dim OldVertxt As String
            Dim OldVer As String
            Dim OldVerLen As Integer
            Dim OldVerAsc As Integer
            Dim NewVer As String
            Dim NewVerStr As String

            'VerUP時の更新先フォルダ予想
            OldVertxt = txtFileName.Text
            OldVerLen = Len(OldVertxt)

            '旧バージョン長さ
            OldVer = Mid(OldVertxt, OldVerLen, 1)

            '文字を数値に変更
            OldVerAsc = Asc(OldVer)

            '数値変換後、1加算して文字に変換
            'Ver2.0.1.3
            'Z,zの次は「[」ではなくA,aとする
            Select Case Chr(OldVerAsc)
                Case "Z"
                    NewVerStr = "A"
                Case "z"
                    NewVerStr = "a"
                Case Else
                    NewVerStr = Chr(OldVerAsc + 1)
            End Select


            '新バージョンに置き換え
            NewVer = (Mid(OldVertxt, 1, OldVerLen - 1)) + NewVerStr

            lblVersionUP.Visible = chkVersionUP.Checked

            'T.Ueki フォルダ管理仕様変更による 
            txtFileNameNew.Visible = chkVersionUP.Checked
            txtFileNameNew.Text = NewVer

            'numVersionUP.Visible = chkVersionUP.Checked

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'T.Ueki ファイル管理仕様変更
    ''--------------------------------------------------------------------
    '' 機能      : バージョンテキストロストフォーカス
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : 値を表示する
    ''--------------------------------------------------------------------
    'Private Sub numVersion_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numVersion.Leave, numVersionUP.Leave

    '    Try

    '        numVersion.Text = numVersion.Value

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    ''--------------------------------------------------------------------
    '' 機能      : バージョンアップテキストロストフォーカス
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : 値を表示する
    ''--------------------------------------------------------------------
    'Private Sub numVersionUP_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numVersionUP.Leave

    '    Try

    '        numVersionUP.Text = numVersionUP.Value

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try


    'End Sub

    '--------------------------------------------------------------------
    ' 機能      : キャンセルボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面を閉じる
    '--------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            mintRtn = 0
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
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim strPathVersion As String = ""
            Dim strPathVersionUP As String = ""
            Dim strVersions() As String = Nothing

            Dim intPreVer As Integer
            Dim intVer As Integer

            Dim intPreVerLen As Integer
            Dim intVerLen As Integer

            Dim strPreVertxt As String
            Dim strVertxt As String

            ''パスが入力されていない場合
            If txtFilePath.Text.Trim() = "" Then
                Call MessageBox.Show("Please input the [Saving Place].", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFilePath.Focus()
                Return False
            End If

            ''パスが存在しない場合
            If Not System.IO.Directory.Exists(txtFilePath.Text.Trim()) Then
                Call MessageBox.Show("The folder set to [Saving Place] doesn't exist. ", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFilePath.Focus()
                Return False
            End If

            ''選択されたパスがドライブのみの場合（2010/06/29 ＰＭ指示により変更）
            'If txtFilePath.Text.Trim.Length = 3 Then
            '    Call MessageBox.Show("Please select not the drive to [Saving Place] but the folder.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Call txtFilePath.Focus()
            '    Return False
            'End If

            'T.Ueki ファイル管理仕様変更
            ''バージョンが選択されていない場合
            'If numVersion.Text = "" Then
            '    Call MessageBox.Show("Please input [Version] again.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Call numVersion.Focus()
            '    Return False
            'End If

            Select Case mudtFileMode
                Case gEnmFileMode.fmNew

                    ''ファイル名が入力されていない場合
                    If txtFileName.Text.Trim() = "" Then
                        Call MessageBox.Show("Please input the [File Name].", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call txtFileName.Focus()
                        Return False
                    End If

                    ''既に作成済のファイル名の場合
                    If System.IO.Directory.Exists(System.IO.Path.Combine(txtFilePath.Text.Trim(), txtFileName.Text.Trim())) Then
                        Call MessageBox.Show("The same file name already exists.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call txtFilePath.Focus()
                        Return False
                    End If

                Case gEnmFileMode.fmEdit

                    'T.Ueki ファイル管理仕様変更
                    If chkCompile.Checked Then
                        'CFｶｰﾄﾞ読込みの場合
                        If CFReadFlg = True Then
                            strPathVersion = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                        Else
                            strPathVersion = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                            strPathVersion = System.IO.Path.Combine(strPathVersion, gCstFolderNameCompile)
                        End If
                    Else
                        strPathVersion = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                    End If

                    'T.Ueki ファイル管理仕様変更
                    ' ''バージョン番号までのパスを作成
                    'strPathVersion = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                    'If chkCompile.Checked Then
                    '    strPathVersion = System.IO.Path.Combine(strPathVersion, gCstVersionPrefix & Format(CInt(numVersion.Value), "000"))
                    '    strPathVersion = System.IO.Path.Combine(strPathVersion, gCstFolderNameCompile)
                    'Else
                    '    strPathVersion = System.IO.Path.Combine(strPathVersion, gCstVersionPrefix & Format(CInt(numVersion.Value), "000"))
                    '    strPathVersion = System.IO.Path.Combine(strPathVersion, gCstFolderNameSave)
                    'End If

                    ''バージョン番号までのパスが存在しない場合
                    If Not System.IO.Directory.Exists(strPathVersion) Then
                        Call MessageBox.Show("The Order file folder doesn't exist. " & vbNewLine & vbNewLine & _
                                             strPathVersion, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call numVersion.Focus()
                        Return False
                    End If

                    ''バージョンアップの場合
                    If chkVersionUP.Checked Then

                        intPreVerLen = Len(txtFileName.Text)
                        intVerLen = Len(txtFileNameNew.Text)

                        intPreVer = Asc(Mid(txtFileName.Text, intPreVerLen, 1))
                        intVer = Asc(Mid(txtFileNameNew.Text, intVerLen, 1))

                        strPreVertxt = Mid(txtFileName.Text, 1, intPreVerLen - 1)
                        strVertxt = Mid(txtFileNameNew.Text, 1, intVerLen - 1)

                        ''アップバージョンが設定されていない場合
                        If intVer = 0 Then
                            Call MessageBox.Show("Please input [UP Version] again.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Call numVersionUP.Focus()
                            Return False
                        End If

                        ''バージョンが同じ場合
                        If intVer = intPreVer Then
                            '「バージョンが同じ」だけでフォルダ名が違う場合は作成　
                            If strPreVertxt = strVertxt Then
                                Call MessageBox.Show("Version is the same.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Call numVersionUP.Focus()
                                Return False
                            End If
                        End If

                        ''バージョンが下がっている場合
                        If intPreVer > intVer Then
                            '何もしない（監視無し）
                        End If

                        ' ''アップバージョンが設定されていない場合
                        'If numVersionUP.Text = "" Then
                        '    Call MessageBox.Show("Please input [UP Version] again.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Call numVersionUP.Focus()
                        '    Return False
                        'End If

                        ' ''バージョンが同じ場合
                        'If numVersion.Text = numVersionUP.Text Then
                        '    Call MessageBox.Show("Version is the same.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Call numVersionUP.Focus()
                        '    Return False
                        'End If

                        ' ''バージョンが下がっている場合
                        'If numVersion.Value > numVersionUP.Value Then
                        '    Call MessageBox.Show("Version has fallen.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Call numVersionUP.Focus()
                        '    Return False
                        'End If

                        ''アップバージョンの保存パスを作成
                        strPathVersionUP = txtFilePath.Text

                        ' ''アップバージョンの保存パスを作成
                        'strPathVersionUP = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                        'strPathVersionUP = System.IO.Path.Combine(strPathVersionUP, gCstVersionPrefix & Format(CInt(numVersion.Value), "000"))

                        ''アップバージョンの保存パスが存在する場合
                        If Not System.IO.Directory.Exists(strPathVersionUP) Then
                            Call MessageBox.Show("The up version folder is exist. " & vbNewLine & vbNewLine & _
                                                 strPathVersion, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Call numVersionUP.Focus()
                            Return False
                        End If

                        ' ''アップバージョンの保存パスが存在する場合
                        'If Not System.IO.Directory.Exists(strPathVersionUP) Then
                        '    Call MessageBox.Show("The up version folder is exist. " & vbNewLine & vbNewLine & _
                        '                         strPathVersion, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Call numVersionUP.Focus()
                        '    Return False
                        'End If

                    End If

                Case gEnmFileMode.fmRename

                    ''ファイル名が入力されていない場合
                    If txtFileName.Text.Trim() = "" Then
                        Call MessageBox.Show("Please input the [File Name].", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call txtFileName.Focus()
                        Return False
                    End If

            End Select

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 選択パスチェック
    ' 返り値    : True:OK、False:NG
    ' 引き数    : ARG1 - (I ) 選択パス
    ' 機能説明  : 選択パスが正しいかチェックする
    '--------------------------------------------------------------------
    Private Function mChkSelectFolder(ByVal strSelectPath As String, _
                                      ByRef blnExistVersion As Boolean, _
                                      ByRef blnExistCompile As Boolean) As Boolean

        Try

            ''ドライブのみの場合（2010/06/29 ＰＭ指示により変更）
            'If strSelectPath.Length = 3 Then
            '    Call MessageBox.Show("Please select not the drive but the folder.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            ''更新の場合
            If mudtFileMode = gEnmFileMode.fmEdit Then

                '2014/5/14 コンペアの場合でCFｶｰﾄﾞから読込む場合はバージョン確認を行わない。 T.Ueki

                ''選択したフォルダ配下にバージョンがあるかチェック
                If Not gExistFolderVersion(fdgFolder.SelectedPath, blnExistCompile) Then

                    Call MessageBox.Show("Under this Folder, there is no '" & gCstVersionPrefix & "xxx' folder." & vbNewLine & _
                                         "Try to select folder again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False

                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ファイルパス、ファイル名取得
    ' 返り値    : 0:OK、-1:選択パス不正
    ' 引き数    : ARG1 - (I ) 選択パス
    ' 　　　    : ARG2 - ( O) ファイルパス
    ' 　　　    : ARG3 - ( O) ファイル名
    ' 機能説明  : 選択パスからファイルパスとファイル名を取得する
    '--------------------------------------------------------------------
    Private Function mGetPathAndFileName(ByVal strSelectPath As String, _
                                         ByRef strFilePath As String, _
                                         ByRef strFileName As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim strwk() As String = Nothing

            strFilePath = ""
            strFileName = ""

            strwk = strSelectPath.Split("\")

            If strwk Is Nothing Then
                intRtn = -1
            Else

                For i As Integer = LBound(strwk) To UBound(strwk)

                    If i <> UBound(strwk) Then
                        strFilePath &= strwk(i) & "\"
                    Else
                        strFileName = strwk(i)
                    End If

                Next

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : バージョンコンボ設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ファイルモード
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 機能説明  : バージョンコンボを設定する
    '--------------------------------------------------------------------
    Private Sub mSetComboVersion(ByVal udtFileMode As gEnmFileMode, _
                                 ByVal udtFileInfo As gTypFileInfo)

        Try

            'Dim strVerNums() As String = Nothing

            ' ''コンボクリア
            'cmbVersion.Items.Clear()

            'Select Case udtFileMode
            '    Case gEnmFileMode.fmNew

            '        cmbVersion.Items.Add("1")
            '        cmbVersion.SelectedIndex = 0

            '    Case gEnmFileMode.fmEdit, gEnmFileMode.fmRename

            '        ''バージョン番号を取得
            '        Select Case gGetVerNums(udtFileInfo, strVerNums)
            '            Case 0

            '                ''コンボに既存のバージョンをセット
            '                For i As Integer = LBound(strVerNums) To UBound(strVerNums)
            '                    cmbVersion.Items.Add(strVerNums(i))
            '                Next

            '                ''初期選択値設定
            '                cmbVersion.SelectedIndex = cmbVersion.FindString(My.Settings.SelectVersion)

            '            Case 1

            '                ''フォルダ自体が存在しない
            '                txtFilePath.Text = "C:\"
            '                txtFileName.Text = ""

            '            Case 2

            '                ''バージョンフォルダが存在しない
            '                txtFilePath.Text = My.Settings.SelectPath
            '                txtFileName.Text = My.Settings.SelectFile

            '        End Select
            'End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バージョン番号設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ファイルモード
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 機能説明  : バージョン番号を設定する
    '--------------------------------------------------------------------
    Private Sub mSetVersionNum(ByVal udtFileMode As gEnmFileMode, _
                               ByVal udtFileInfo As gTypFileInfo)

        Try

            'Dim strVerNums() As String = Nothing

            'Select Case udtFileMode
            '    Case gEnmFileMode.fmNew

            '        ''新規の場合は１
            '        numVersion.Value = 1

            '    Case gEnmFileMode.fmEdit, gEnmFileMode.fmRename

            '        ''バージョン番号を取得
            '        Select Case gGetVerNums(udtFileInfo, strVerNums)
            '            Case 0

            '                '=====================================
            '                ''バージョンフォルダが見つかった場合
            '                '=====================================
            '                ''初期化ファイルに保存されている番号を表示
            '                numVersion.Value = My.Settings.SelectVersion

            '            Case 1

            '                ''フォルダ自体が存在しない
            '                txtFilePath.Text = "C:\"
            '                txtFileName.Text = ""
            '                numVersion.Value = 1

            '            Case 2

            '                ''バージョンフォルダが存在しない
            '                txtFilePath.Text = My.Settings.SelectPath
            '                txtFileName.Text = My.Settings.SelectFile
            '                numVersion.Value = 1

            '        End Select
            'End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class