Imports System.Runtime.InteropServices
Public Class frmChkFileCompare
    'Ver2.0.3.2　ｴﾗｰﾃﾞｰﾀ件数を5000件で打ち止めではなく無限に変更できるように修正


#Region "GetMimCH DLL Call"
    <System.Runtime.InteropServices.DllImport("GetMimCH", CharSet:=Runtime.InteropServices.CharSet.Ansi)> _
    Shared Function mainProc(pstrPath As String, pintCHNo As Integer) As IntPtr
    End Function
#End Region


#Region "定数定義"

    Private Const mCstStatusFileOpen As String = "File Open"
    Private Const mCstStatusOpenError As String = "Open Error"
    Private Const mCstStatusCompareReady As String = "Ready"

    Private Const mCstProgressValueMaxSave As Integer = 45 '44

    'Ver2.0.2.9 OUTとANDORのMASK処理比較にて使用
    Private mcstStsAnalog() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER"}
    Private mcstBitAnalog() As Integer = {-1, 3, 1, 0, 2, 5, 6}

    Private mcstStsDigital() As String = {"ALARM"}
    Private mcstBitDigital() As Integer = {0}

    Private mcstStsMotor() As String = {"ALARM", "FA"}
    Private mcstBitMotor() As Integer = {0, 8}

    Private mcstStsValveDiDo() As String = {"ALARM1", "ALARM2", "ALARM3", "ALARM4", "ALARM5", "ALARM6", "ALARM7", "ALARM8", "ALARM9", "FA"}
    Private mcstBitValveDiDo() As Integer = {12, 13, 14, 15, 0, 1, 2, 3, 4, 8}

    Private mcstStsValveAiDo() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER", "FA"}
    Private mcstBitValveAiDo() As Integer = {-1, 3, 1, 0, 2, 5, 6, 8}

    Private mcstStsValveAiAo() As String = {"ALL ALARM", "HH-Set", "H-Set", "L-Set", "LL-Set", "UNDER", "OVER", "FA"}
    Private mcstBitValveAiAo() As Integer = {-1, 3, 1, 0, 2, 5, 6, 8}

    Private mcstStsComposite() As String = {"ALARM1", "ALARM2", "ALARM3", "ALARM4", "ALARM5", "ALARM6", "ALARM7", "ALARM8", "ALARM9", "FA"}
    Private mcstBitComposite() As Integer = {12, 13, 14, 15, 0, 1, 2, 3, 4, 8}

    Private mcstStsPulse() As String = {"ALARM"}
    Private mcstBitPulse() As Integer = {0}
#End Region

#Region "変数定義"

    ''出力構造体クラス
    Private mudtSource As New clsStructure  ''比較元
    Private mudtTarget As New clsStructure  ''比較先

    ''ファイル情報
    Private mudtFileSource As gTypCompareFileInfo = Nothing
    Private mudtFileTarget As gTypCompareFileInfo = Nothing

    ''ファイル情報
    Private mudttSource As clsStructure
    Private mudttTarget As gTypCompareFileInfo = Nothing

    '2016/5/19 T.Ueki
    Private SourceCHNo As String    '比較元CHNo
    Private TargetCHNo As String    '比較先CHNo
    Private SourceIDNo As String    '比較元CHID
    Private TargetIDNo As String    '比較先CHID

    Private strDellCH(3000) As String
    Private strAddCH(3000) As String
    Private strChgCH(3000) As String

    Private strLogTxtData() As String
    Private intLogGyo As Integer

    Private msgtemp(10000) As String      'チャンネル情報用
    Private msgSYStemp(10000) As String   'システム
    Private strFUAddress As String       '比較元アドレス
    Private strMtrAddress As String    '比較元モーターアドレス

    Private mintRtn As Integer

    Private mstrPrintingText As String
    Private mstrPrintingPosition As Integer
    Private mstrPrintFont As Font


    '' 2015.11.03 Ver1.7.5 追加
    ''比較CHﾘｽﾄ　構造体   
    Private Structure mCompareCH
        Dim index As Integer
        Dim ch_id As Integer
        Dim ch_no As Integer
    End Structure

    Private mChkCH(gCstChannelIdMax - 1) As mCompareCH

    Private mTitle As String
    Private mFirstFg As Byte    ' CH変更箇所有無
    Private mChkFg As Byte      ' 個別CH変更有無
    ''//
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 返り値    : 0:キャンセル、1:実行、-1:失敗
    ' 機能説明  : 画面表示を行う
    '--------------------------------------------------------------------
    Friend Function gShow() As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
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
    Private Sub frmCmpCompier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Ver2.0.1.5　画面キャプションにﾌｧｲﾙ番号追加
            Me.Text = "[" & gudtFileInfo.strFileVersion & "] " & Me.Text

            ''構造体初期化
            Call gInitOutputStructure(mudtSource)
            Call gInitOutputStructure(mudtTarget)

            ''ファイル情報をコピー
            mudtFileSource = gudtCompareFileInfo
            mudtFileTarget = gudtCompareFileInfo

            Save_Read.Checked = True                                'Saveモードにする
            cmdLogTxt.Enabled = False                               'コンペア完了までログファイル参照操作不可
            cmdPrint.Enabled = False                                'コンペア完了まで印刷操作不可

            'ファイルアクセス済み
            If FileAccessFlg = True Then
                txtTargetPath.Text = gudtFileInfo.strFilePath       '計測点ﾌｫﾙﾀﾞ
                txtTargetFile.Text = gudtFileInfo.strFileVersion    'Ver
                lblTargetStatus.Text = mCstStatusFileOpen           'ステータス表示
                Call SetSourceData()                                'データをコピー
                cmdTargetOpen.Enabled = True                        'ターゲット側ボタン

                'Ver2.0.0.2 2016.12.09 MIMICﾌｧｲﾙレベル比較機能用に変数格納
                mudtFileTarget.strFilePath = gudtFileInfo.strFilePath
                mudtFileTarget.strFileName = gudtFileInfo.strFileVersion

                'Ver2.0.7.1 CHConvertファイルは、上記では読み込まないためここで読み込む
                Call mLoadChConv(mudtTarget.SetChConvNow, mudtFileTarget.strFilePath, mudtFileTarget.strFileName)
            Else
                txtTargetPath.Text = ""                             '計測点ﾌｫﾙﾀﾞ
                txtTargetFile.Text = ""                             'Ver
                lblTargetStatus.Text = mCstStatusCompareReady       'ステータス表示
                cmdTargetOpen.Enabled = True                        'ターゲット側ボタン
            End If

            ''Compareボタン使用可/不可設定
            Call mSetCompareButtonEnable()

            prgBar.Visible = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 現在読み込んでいるﾃﾞｰﾀを比較用ﾃﾞｰﾀにｺﾋﾟｰ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub SetSourceData()

        ''システム設定
        mudtTarget.SetSystem = gudt.SetSystem

        ''FU設定（チャンネル情報データ）
        mudtTarget.SetFu = gudt.SetFu

        ''チャンネル情報データ（表示名設定データ）
        mudtTarget.SetChDisp = gudt.SetChDisp

        ''チャンネル情報
        mudtTarget.SetChInfo = gudt.SetChInfo

        ''コンポジット情報
        mudtTarget.SetChComposite = gudt.SetChComposite

        ''グループ設定
        mudtTarget.SetChGroupSetM = gudt.SetChGroupSetM
        mudtTarget.SetChGroupSetC = gudt.SetChGroupSetC

        ''リポーズ入力設定
        mudtTarget.SetChGroupRepose = gudt.SetChGroupRepose

        ''出力チャンネル設定
        mudtTarget.SetChOutput = gudt.SetChOutput

        ''論理出力設定
        mudtTarget.SetChAndOr = gudt.SetChAndOr

        ''積算データ設定ファイル
        mudtTarget.SetChRunHour = gudt.SetChRunHour

        ''コントロール使用可／不可設定
        mudtTarget.SetChCtrlUseM = gudt.SetChCtrlUseM
        mudtTarget.SetChCtrlUseC = gudt.SetChCtrlUseC

        ''排ガス演算処理設定
        mudtTarget.SetChExhGus = gudt.SetChExhGus

        ''SIO設定（外部機器VDR情報設定）
        mudtTarget.SetChSio = gudt.SetChSio

        ''SIO設定（外部機器VDR情報設定）CH設定データ
        mudtTarget.SetChSioCh = gudt.SetChSioCh

        ''延長警報設定
        mudtTarget.SetExtAlarm = gudt.SetExtAlarm

        ''タイマ設定
        mudtTarget.SetExtTimerSet = gudt.SetExtTimerSet

        ''タイマ表示名称設定
        mudtTarget.SetExtTimerName = gudt.SetExtTimerName

        ''シーケンス設定
        mudtTarget.SetSeqID = gudt.SetSeqID
        mudtTarget.SetSeqSet = gudt.SetSeqSet

        ''リニアライズテーブル
        mudtTarget.SetSeqLinear = gudt.SetSeqLinear

        ''演算式テーブル
        mudtTarget.SetSeqOpeExp = gudt.SetSeqOpeExp

        ''データ保存テーブル設定
        mudtTarget.SetChDataSave = gudt.SetChDataSave

        ''データ保存テーブル設定
        mudtTarget.SetChDataForward = gudt.SetChDataForward

        ''OPS画面タイトル
        mudtTarget.SetOpsScreenTitleM = gudt.SetOpsScreenTitleM
        mudtTarget.SetOpsScreenTitleC = gudt.SetOpsScreenTitleC

        ''プルダウンメニュー
        mudtTarget.SetOpsPulldownMenuM = gudt.SetOpsPulldownMenuM
        mudtTarget.SetOpsPulldownMenuC = gudt.SetOpsPulldownMenuC

        ''セレクションメニュー
        mudtTarget.SetOpsSelectionMenuM = gudt.SetOpsSelectionMenuM
        mudtTarget.SetOpsSelectionMenuC = gudt.SetOpsSelectionMenuC

        ''OPS設定
        mudtTarget.SetOpsGraphM = gudt.SetOpsGraphM
        mudtTarget.SetOpsGraphC = gudt.SetOpsGraphC

        ''フリーグラフ
        mudtTarget.SetOpsFreeGraphM = gudt.SetOpsFreeGraphM
        mudtTarget.SetOpsFreeGraphC = gudt.SetOpsFreeGraphC

        ''フリーディスプレイ
        mudtTarget.SetOpsFreeDisplayM = gudt.SetOpsFreeDisplayM
        mudtTarget.SetOpsFreeDisplayC = gudt.SetOpsFreeDisplayC

        ''トレンドグラフ
        mudtTarget.SetOpsTrendGraphM = gudt.SetOpsTrendGraphM
        mudtTarget.SetOpsTrendGraphC = gudt.SetOpsTrendGraphC

        ''ログフォーマット
        mudtTarget.SetOpsLogFormatM = gudt.SetOpsLogFormatM
        mudtTarget.SetOpsLogFormatC = gudt.SetOpsLogFormatC

        ''GWS設定 CH設定データ
        mudtTarget.SetOpsGwsCh = gudt.SetOpsGwsCh

        ''CH変換テーブル
        mudtTarget.SetChConvNow = gudt.SetChConvNow

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Openボタンクリック（比較元）
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOpenSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSourceOpen.Click

        Try

            Dim CFCardRead As Boolean
            Dim SaveFolderRead As Boolean
            Dim CompileFolderRead As Boolean

            'CFカード
            CFCardRead = CF_Read.Checked

            'Saveフォルダ
            SaveFolderRead = Save_Read.Checked

            'Compileフォルダ
            CompileFolderRead = Compile_Read.Checked

            ''ファイルセレクト画面表示
            Select Case frmCompareFileSelect.gShow(gEnmFileMode.fmEdit, mudtFileSource, mudtSource, mudtTarget, CFCardRead, SaveFolderRead, CompileFolderRead)

                Case 0

                    ''キャンセル時は何もしない

                Case 1

                    '読込成功
                    'Ver2.0.6.5 CHConvertファイルは、上記では読み込まないためここで読み込む
                    Call mLoadChConv(mudtSource.SetChConvNow, mudtFileSource.strFilePath, mudtFileSource.strFileName)

                    'CFカード時はターゲット側も表示
                    If CFCardRead Then
                        txtSourcePath.Text = mudtFileSource.strFileOrgPath
                        txtSourceFile.Text = mudtFileSource.strFileName
                        lblSourceStatus.Text = mCstStatusFileOpen

                        txtTargetPath.Text = mudtFileSource.strFilePath
                        txtTargetFile.Text = mudtFileSource.strFileName
                        lblTargetStatus.Text = mCstStatusFileOpen
                    Else
                        txtSourcePath.Text = mudtFileSource.strFilePath
                        txtSourceFile.Text = mudtFileSource.strFileName
                        lblSourceStatus.Text = mCstStatusFileOpen   ' 2015.11.03 Ver1.7.5 Ready → File Open　変更
                    End If

                Case -1

                    ''読込失敗
                    txtSourcePath.Text = mudtFileSource.strFilePath
                    txtSourceFile.Text = mudtFileSource.strFileName
                    lblSourceStatus.Text = mCstStatusOpenError

            End Select

            ''Compareボタン使用可/不可設定
            Call mSetCompareButtonEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'Ver2.0.6.5 SourceCHConvertファイル読み込み
    Private Function mLoadChConv(ByRef udtSetChConv As gTypSetChConv, _
                                 ByVal strPathBase As String, ByVal strFileName As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChConv
            Dim strCurFileName As String = gCstFileChConv

            'システム設定のパスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strFileName)
            strPathSave = System.IO.Path.Combine(strPathSave, gCstFolderNameSave)
            strPathSave = System.IO.Path.Combine(strPathSave, strCurPathName)

            'フルパス作成
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                'メッセージ出さない
                intRtn = 0
            Else
                'ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChConv)

                Catch ex As Exception
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


    '----------------------------------------------------------------------------
    ' 機能説明  ： Openボタンクリック（比較先）
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOpenTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTargetOpen.Click
        Try

            Dim CFCardTargetRead As Boolean
            Dim SaveFolderTargetRead As Boolean
            Dim CompileFolderTargetRead As Boolean

            'CFカード
            CFCardTargetRead = CF_Read.Checked

            'Saveフォルダ
            SaveFolderTargetRead = Save_Read.Checked

            'Compileフォルダ
            CompileFolderTargetRead = Compile_Read.Checked

            ''ファイルセレクト画面表示
            Select Case frmCompareFileSelect.gShow(gEnmFileMode.fmEdit, mudtFileTarget, mudtTarget, mudtTarget, CFCardTargetRead, SaveFolderTargetRead, CompileFolderTargetRead)

                Case 0

                    ''キャンセル時は何もしない

                Case 1

                    'Ver2.0.6.5 CHConvertファイルは、上記では読み込まないためここで読み込む
                    Call mLoadChConv(mudtTarget.SetChConvNow, mudtFileTarget.strFilePath, mudtFileTarget.strFileName)

                    ''読込成功
                    txtTargetPath.Text = mudtFileTarget.strFilePath
                    txtTargetFile.Text = mudtFileTarget.strFileName
                    lblTargetStatus.Text = mCstStatusFileOpen

                Case -1

                    ''読込失敗
                    txtTargetPath.Text = mudtFileTarget.strFilePath
                    txtTargetFile.Text = mudtFileTarget.strFileName
                    lblTargetStatus.Text = mCstStatusOpenError

            End Select

            ''Compareボタン使用可/不可設定
            Call mSetCompareButtonEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
        

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンペアボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdComapre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComapre.Click

        Try
            Dim intUseCnt As Integer = 0

            Dim strWaitMsg1 As String = ""
            Dim strWaitMsg2 As String = ""
            Dim udtMsgResult As DialogResult

            'Ver2.0.3.5
            'Targetﾌｫﾙﾀﾞが存在しない＝ﾊﾞｰｼﾞｮﾝｱｯﾌﾟや新規で保存してない
            '場合は比較させない
            Dim strChkPath As String = System.IO.Path.Combine(txtTargetPath.Text, txtTargetFile.Text)
            If System.IO.Directory.Exists(strChkPath) = False Then
                MessageBox.Show("No Target Directory." & vbCrLf _
                                & "[" & strChkPath & "]" & vbCrLf _
                                & "Please SAVE." _
                                , "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If


            udtMsgResult = MessageBox.Show("Do you start comparing the difference of the setting files?", _
                                           "Compare", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            strWaitMsg1 = "Now Comparing"
            strWaitMsg2 = "Please wait..."

            If udtMsgResult = Windows.Forms.DialogResult.Yes Then
                cmdComapre.Enabled = False

                'Ver2.0.3.2 結果格納配列初期化
                strLogTxtData = Nothing

                With prgBar

                    ''プログレスバー初期化
                    .Minimum = 0
                    .Maximum = mCstProgressValueMaxSave
                    .Value = 0

                    prgBar.Visible = True

                    ''画面設定
                    Call mSetDisplayEnable(False, strWaitMsg1, strWaitMsg2) : .Value += 1 : Application.DoEvents()

                    txtResult.Clear()

                    '比較するチャンネルバージョン情報
                    Call mMsgGridInfo(txtSourceFile.Text, txtTargetFile.Text) : .Value += 1 : Application.DoEvents()

                    'Ver2.0.0.2 MC比較と処理分岐 完全に分岐さす
                    If chkMC.Checked = True Then
                        '■MC比較
                        ''チャンネル情報
                        If gCompareChk(gConmCompareSetChannelDisp) = True Then
                            Call mCompareSetChannelDisp_MC(mudtSource.SetChInfo, mudtTarget.SetChInfo) : .Value += 1 : Application.DoEvents()
                        End If


                        ''システム設定(通常比較と同じ)
                        If gCompareChk(gConmCompareSetSystem) = True Then
                            Call mCompareSetSystem(mudtSource.SetSystem, mudtTarget.SetSystem) : .Value += 1 : Application.DoEvents()
                        End If
                        ''リポーズ入力設定(通常比較と同じ)
                        If gCompareChk(gConmCompareSetRepose) = True Then
                            Call mCompareSetRepose(mudtSource.SetChGroupRepose, mudtTarget.SetChGroupRepose) : .Value += 1 : Application.DoEvents()
                        End If
                    Else
                        '■通常比較

                        ''チャンネル追加/削除
                        If gCompareChk(gConmCompareSetChannelAddDel) = True Then
                            Call mCompareSetChannelAddDel(mudtSource.SetChInfo, mudtTarget.SetChInfo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''チャンネル情報
                        If gCompareChk(gConmCompareSetChannelDisp) = True Then
                            Call mCompareSetChannelDisp(mudtSource.SetChInfo, mudtTarget.SetChInfo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''システム設定
                        If gCompareChk(gConmCompareSetSystem) = True Then
                            Call mCompareSetSystem(mudtSource.SetSystem, mudtTarget.SetSystem) : .Value += 1 : Application.DoEvents()
                        End If

                        'Ver2.0.0.1 2016.12.8 端子表比較追加
                        If gCompareChk(gConmCompareTerminalInfo) = True Then
                            ''端子表比較
                            Call mCompareTerminalInfo(mudtSource.SetFu, mudtSource.SetChDisp, mudtTarget.SetFu, mudtTarget.SetChDisp)
                        End If

                        ''コンポジット情報
                        If gCompareChk(gConmCompareSetCompositeDisp) = True Then
                            Call mCompareSetCompositeDisp(mudtSource.SetChComposite, mudtTarget.SetChComposite) : .Value += 1 : Application.DoEvents()
                        End If

                        ''リポーズ入力設定
                        If gCompareChk(gConmCompareSetRepose) = True Then
                            Call mCompareSetRepose(mudtSource.SetChGroupRepose, mudtTarget.SetChGroupRepose) : .Value += 1 : Application.DoEvents()
                        End If

                        ''出力チャンネル設定
                        If gCompareChk(gConmCompareSetCHOutPut_FU) = True Then
                            'Call mCompareSetCHOutPut(mudtSource.SetChOutput, mudtTarget.SetChOutput) : .Value += 1 : Application.DoEvents()
                            'Ver2.0.2.8 CH追加削除を検出
                            'Ver2.0.3.1 OUTPUT検出にまぜる
                            'Call mCompareSetCHOutPut_FU_DELADD(mudtSource.SetChOutput, mudtTarget.SetChOutput) : .Value += 1 : Application.DoEvents()

                            Call mCompareSetCHOutPut_FU(mudtSource.SetChOutput, mudtTarget.SetChOutput) : .Value += 1 : Application.DoEvents()
                        End If

                        ''論理出力設定
                        If gCompareChk(gConmCompareSetCHAndOr) = True Then
                            Call mCompareSetCHAndOr(mudtSource.SetChAndOr, mudtTarget.SetChAndOr) : .Value += 1 : Application.DoEvents()
                        End If

                        ''積算データ設定ファイル
                        If gCompareChk(gConmCompareSetChRunHour) = True Then
                            Call mCompareSetChRunHour(mudtSource.SetChRunHour, mudtTarget.SetChRunHour) : .Value += 1 : Application.DoEvents()
                        End If

                        ''コントロール使用可／不可設定
                        If gCompareChk(gConmCompareSetCtrlUseNotuse) = True Then
                            Call mCompareSetCtrlUseNotuse(mudtSource.SetChCtrlUseM, mudtTarget.SetChCtrlUseM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetCtrlUseNotuse(mudtSource.SetChCtrlUseC, mudtTarget.SetChCtrlUseC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''排ガス演算処理設定
                        If gCompareChk(gConmCompareSetExhGus) = True Then
                            Call mCompareSetExhGus(mudtSource.SetChExhGus, mudtTarget.SetChExhGus) : .Value += 1 : Application.DoEvents()
                        End If

                        ''SIO設定（外部機器VDR情報設定）
                        If gCompareChk(gConmCompareSetChSio) = True Then
                            Call mCompareSetChSio(mudtSource.SetChSio, mudtTarget.SetChSio) : .Value += 1 : Application.DoEvents()
                        End If

                        ''SIO設定（外部機器VDR情報設定）CH設定データ
                        If gCompareChk(gConmCompareSetChSioCh) = True Then
                            Call mCompareSetChSioCh(mudtSource.SetChSioCh, mudtTarget.SetChSioCh) : .Value += 1 : Application.DoEvents()
                        End If

                        ''延長警報設定
                        If gCompareChk(gConmCompareSetExtAlarm) = True Then
                            Call mCompareSetExtAlarm(mudtSource.SetExtAlarm, mudtTarget.SetExtAlarm) : .Value += 1 : Application.DoEvents()
                        End If

                        ''タイマ設定
                        If gCompareChk(gConmCompareSetTimer) = True Then
                            Call mCompareSetTimer(mudtSource.SetExtTimerSet, mudtTarget.SetExtTimerSet) : .Value += 1 : Application.DoEvents()
                        End If

                        ''タイマ表示名称設定
                        If gCompareChk(gConmCompareSetTimerName) = True Then
                            Call mCompareSetTimerName(mudtSource.SetExtTimerName, mudtTarget.SetExtTimerName) : .Value += 1 : Application.DoEvents()
                        End If

                        ''シーケンス設定
                        If gCompareChk(gConmCompareSetSeqSequence) = True Then
                            Call mCompareSetSeqSequence(mudtSource.SetSeqID, mudtSource.SetSeqSet, mudtTarget.SetSeqID, mudtTarget.SetSeqSet) : .Value += 1 : Application.DoEvents()
                        End If

                        ''リニアライズテーブル
                        If gCompareChk(gConmCompareSetSeqLinearTable) = True Then
                            Call mCompareSetSeqLinearTable(mudtSource.SetSeqLinear, mudtTarget.SetSeqLinear) : .Value += 1 : Application.DoEvents()
                        End If

                        ''演算式テーブル
                        If gCompareChk(gConmCompareSetSeqOperationExpression) = True Then
                            Call mCompareSetSeqOperationExpression(mudtSource.SetSeqOpeExp, mudtTarget.SetSeqOpeExp) : .Value += 1 : Application.DoEvents()
                        End If

                        ''データ保存テーブル設定
                        If gCompareChk(gConmCompareSetChDataSaveTable) = True Then
                            Call mCompareSetChDataSaveTable(mudtSource.SetChDataSave, mudtTarget.SetChDataSave) : .Value += 1 : Application.DoEvents()
                        End If

                        ''データ保存テーブル設定
                        If gCompareChk(gConmCompareSetChDataForwardTableSet) = True Then
                            Call mCompareSetChDataForwardTableSet(mudtSource.SetChDataForward, mudtTarget.SetChDataForward) : .Value += 1 : Application.DoEvents()
                        End If

                        ''OPS画面タイトル
                        If gCompareChk(gConmCompareSetOpsDisp) = True Then
                            'Ver2.0.7.B ScreenTitleは比較対象外
                            'Call mCompareSetOpsDisp(mudtSource.SetOpsScreenTitleM, mudtTarget.SetOpsScreenTitleM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            'Call mCompareSetOpsDisp(mudtSource.SetOpsScreenTitleC, mudtTarget.SetOpsScreenTitleC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''プルダウンメニュー
                        If gCompareChk(gConmCompareSetOpsPulldownMenu) = True Then
                            Call mCompareSetOpsPulldownMenu(mudtSource.SetOpsPulldownMenuM, mudtTarget.SetOpsPulldownMenuM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsPulldownMenu(mudtSource.SetOpsPulldownMenuC, mudtTarget.SetOpsPulldownMenuC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''セレクションメニュー
                        If gCompareChk(gConmCompareSetOpsSelectionMenu) = True Then
                            Call mCompareSetOpsSelectionMenu(mudtSource.SetOpsSelectionMenuM, mudtTarget.SetOpsSelectionMenuM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsSelectionMenu(mudtSource.SetOpsSelectionMenuC, mudtTarget.SetOpsSelectionMenuC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''OPS設定
                        If gCompareChk(gConmCompareSetOpsGraph) = True Then
                            Call mCompareSetOpsGraph(mudtSource.SetOpsGraphM, mudtTarget.SetOpsGraphM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsGraph(mudtSource.SetOpsGraphC, mudtTarget.SetOpsGraphC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''フリーグラフ
                        If gCompareChk(gConmCompareSetOpsFreeGraph) = True Then
                            Call mCompareSetOpsFreeGraph(mudtSource.SetOpsFreeGraphM, mudtTarget.SetOpsFreeGraphM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsFreeGraph(mudtSource.SetOpsFreeGraphC, mudtTarget.SetOpsFreeGraphC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''フリーディスプレイ
                        If gCompareChk(gConmCompareSetOpsFreeDisplay) = True Then
                            Call mCompareSetOpsFreeDisplay(mudtSource.SetOpsFreeDisplayM, mudtTarget.SetOpsFreeDisplayM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsFreeDisplay(mudtSource.SetOpsFreeDisplayC, mudtTarget.SetOpsFreeDisplayC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''グループ設定
                        If gCompareChk(gConmCompareSetGroupDisp) = True Then
                            Call mCompareSetGroupDisp(mudtSource.SetChGroupSetM, mudtTarget.SetChGroupSetM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetGroupDisp(mudtSource.SetChGroupSetC, mudtTarget.SetChGroupSetC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''トレンドグラフ
                        If gCompareChk(gConmCompareSetOpsTrendGraph) = True Then
                            Call mCompareSetOpsTrendGraph(mudtSource.SetOpsTrendGraphM, mudtTarget.SetOpsTrendGraphM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsTrendGraph(mudtSource.SetOpsTrendGraphC, mudtTarget.SetOpsTrendGraphC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''ログフォーマット
                        If gCompareChk(gConmCompareSetOpsLogFormat) = True Then
                            Call mCompareSetOpsLogFormat(mudtSource.SetOpsLogFormatM, mudtTarget.SetOpsLogFormatM, gEnmMachineryCargo.mcMachinery) : .Value += 1 : Application.DoEvents()
                            Call mCompareSetOpsLogFormat(mudtSource.SetOpsLogFormatC, mudtTarget.SetOpsLogFormatC, gEnmMachineryCargo.mcCargo) : .Value += 1 : Application.DoEvents()
                        End If

                        ''GWS設定 CH設定データ
                        If gCompareChk(gConmCompareSetOpsGwsCh) = True Then
                            Call mCompareSetOpsGwsCh(mudtSource.SetOpsGwsCh, mudtTarget.SetOpsGwsCh) : .Value += 1 : Application.DoEvents()
                        End If

                        ''CH変換テーブル
                        If gCompareChk(gConmCompareSetChConv) = True Then
                            Call mCompareSetChConv(mudtSource.SetChConvNow, mudtTarget.SetChConvNow) : .Value += 1 : Application.DoEvents()
                        End If

                        'Ver2.0.0.2 2016.12.09 MIMICﾌｧｲﾙレベル比較機能
                        ''Mimic ﾌｧｲﾙレベル比較
                        If gCompareChk(gConmCompareMimicFiles) = True Then
                            Call mCompareMimicFiles(mudtFileSource, mudtFileTarget)
                        End If

                        'Ver2.0.7.M CFｶｰﾄﾞ以外の設定も比較
                        Call fnCHKfiles(mudtFileSource, mudtFileTarget)


                        'Ver2.0.0.2 MC分岐ココマデ
                        End If

                        ''画面設定
                        Call mSetDisplayEnable(True) : .Value += 1 : Application.DoEvents()

                End With

                Call CompareLogTxtWrite()

                cmdComapre.Enabled = True
            End If




            prgBar.Visible = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ファイル出力ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCSVoutput.Click

        Try

            Dim dlgOutput As New SaveFileDialog()
            Dim udtMsgResult As DialogResult
            Dim strLine As String = ""

            If grdCompare.RowCount = 0 Then Return

            With dlgOutput

                .FileName = "FILE_COMPARE_" & Format(Now, "yyyyMMddHHmm") & ".csv"
                .InitialDirectory = gGetAppPath()
                .Filter = "TEXT File(*.txt)|*.txt"
                .FilterIndex = 1
                .RestoreDirectory = True
                .OverwritePrompt = True
                .CheckPathExists = True

                ''ダイアログを表示する
                If .ShowDialog() = DialogResult.OK Then

                    Try

                        ''ファイル出力
                        Dim intFileNum As Integer = FreeFile()
                        Call FileOpen(intFileNum, .FileName, OpenMode.Append)

                        For i As Integer = 0 To grdCompare.RowCount - 1

                            strLine = ""

                            For j As Integer = 0 To grdCompare.ColumnCount - 1
                                strLine &= CStr(grdCompare.Rows(i).Cells(j).Value) & ","
                            Next

                            strLine = Mid(strLine, 1, strLine.Length - 1)

                            Call Print(intFileNum, strLine & vbNewLine)

                        Next

                        Call FileClose(intFileNum)

                        udtMsgResult = MessageBox.Show("The CSV file is output successfully." & vbNewLine & vbNewLine & .FileName, _
                                                           "File compare", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception

                    End Try

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmCmpCompier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            'Ver2.0.1.5 比較項目選択フラグを初期化
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

            Me.Dispose()
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 印刷開始処理
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try
            Dim strMsg As String
            Dim strTitle As String

            strMsg = "May I print compare massage?"
            strTitle = "Compare Print"

            If MessageBox.Show(strMsg, strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                'PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                '印刷する文字列と位置を設定する
                mstrPrintingText = txtResult.Text
                mstrPrintingPosition = 0

                '印刷に使うフォントを指定する
                mstrPrintFont = New Font("Arial", 10)

                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                'PrintDialogクラスの作成
                Dim pdlg As New PrintDialog
                'PrintDocumentを指定
                pdlg.Document = pd

                '******************************
                '取りあえず、ページ指定はしない
                '******************************

                '印刷の選択ダイアログを表示
                If pdlg.ShowDialog() = DialogResult.OK Then
                    pd.Print()
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コンペア後のテキストファイル保存先フォルダを表示
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub cmdLogTxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogTxt.Click

        Dim strFullFolderPath As String
        Dim strLogTxtFileName As String
        Dim OpenFile As Long

        '初期フォルダ
        strFullFolderPath = txtTargetPath.Text & txtTargetFile.Text

        '目的のファイル
        strLogTxtFileName = txtTargetPath.Text & txtTargetFile.Text & "\comp_" & txtTargetFile.Text & ".txt"

        'OpenFileDialogクラスのインスタンスを作成
        Dim ofd As New OpenFileDialog()

        'ファイル名表示
        ofd.FileName = strLogTxtFileName

        '初期フォルダを指定する
        ofd.InitialDirectory = strFullFolderPath

        'テキストファイルのみ表示
        ofd.Filter = "TXTFile(*.txt)|*.txt"

        'タイトルを設定
        ofd.Title = "Please select a Log text file."

        'ダイアログボックスを閉じる前に現在のディレクトリを復元
        ofd.RestoreDirectory = True

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then

            With CreateObject("Wscript.Shell")
                OpenFile = .Run(ofd.FileName, 7, True)
            End With

            If OpenFile <> 0 Then MsgBox("File open fail") : Exit Sub

        End If

    End Sub

    '***************************
    'CF_Read_チェックボタン
    '***************************
    Private Sub CF_Read_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CF_Read.CheckedChanged

        cmdTargetOpen.Enabled = False

    End Sub

    '***************************
    'Save_Read_チェックボタン
    '***************************
    Private Sub Save_Read_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save_Read.CheckedChanged

        cmdTargetOpen.Enabled = True

    End Sub

    '***************************
    'Compile_Read_チェックボタン
    '***************************
    Private Sub Compile_Read_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Compile_Read.CheckedChanged

        cmdTargetOpen.Enabled = True

    End Sub

    'Ver2.0.0.2
    '比較項目選択画面起動
    Private Sub btnChkSelect_Click(sender As System.Object, e As System.EventArgs) Handles btnChkSelect.Click
        'Ver2.0.0.7 比較項目選択画面は大項目へ変更
        'frmChkFileCompSelect.ShowDialog()
        frmChkFileCompSelectBig.ShowDialog()
    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : Compareボタン使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : Compareボタンの使用可/不可を設定する
    '--------------------------------------------------------------------
    Private Sub mSetCompareButtonEnable()

        ' 2015.11.03  Ver1.7.5  比較元・先がﾌｧｲﾙ読み込み完了時に変更
        If lblSourceStatus.Text = mCstStatusFileOpen And _
           lblTargetStatus.Text = mCstStatusFileOpen Then
            cmdComapre.Enabled = True
        Else
            cmdComapre.Enabled = False
        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 状態表示
    ' 引数      ： 
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Sub mSetDisplayEnable(ByVal blnFlg As Boolean, _
                         Optional ByVal strWaitMsg1 As String = "", _
                         Optional ByVal strWaitMsg2 As String = "")

        If blnFlg Then

            ''Waitフレーム
            fraWait.Visible = False
            cmdPrint.Enabled = True
            cmdCSVoutput.Enabled = True
            cmdExit.Enabled = True
            cmdComapre.Text = "Compare"

        Else

            ''Waitフレーム
            lblWait1.Text = strWaitMsg1
            lblWait2.Text = strWaitMsg2
            fraWait.Visible = True
            fraWait.Refresh()

            cmdPrint.Enabled = False
            cmdCSVoutput.Enabled = False
            cmdExit.Enabled = False
            'cmdComapre.Text = "Compare Cancel"

        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 変更点情報表示
    ' 引数      ： 
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Sub mMsgGridInfo(ByVal strSourceInfo As String, ByVal strTargetInfo As String)

        Dim strTitle As String

        '比較データVer.題目表示
        strTitle = ("＊＊＊ " & strSourceInfo & " → " & strTargetInfo & "　＊＊＊")
        txtResult.AppendText(strTitle & vbCrLf)

        'Ver2.0.3.2 結果保存件数を無限に変更
        ReDim Preserve strLogTxtData(intLogGyo)

        strLogTxtData(intLogGyo) = strTitle
        intLogGyo = intLogGyo + 1

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 変更点情報表示（追加/削除チャンネル用）
    ' 引数      ： 行数
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mMsgGridAddDel(ByVal AddGyo As Integer, ByVal DelGyo As Integer)
        Dim strAddCHNo As String
        Dim strDelCHNo As String

        Dim strAddChinfo(10000) As String
        Dim strDelChinfo(10000) As String

        Dim intAddGyo As Integer = AddGyo
        If intAddGyo > 10000 Then
            intAddGyo = 10000
        End If
        Dim intDelGyo As Integer = DelGyo
        If intDelGyo > 10000 Then
            intDelGyo = 10000
        End If

        Dim intAddNow As Integer = 0
        Dim intDelNow As Integer = 0

        Dim intMsgGyo As Integer = 0

        '題目表示
        strAddChinfo(0) = ("■■■■ ADD CHANNEL　■■■■")
        txtResult.AppendText(strAddChinfo(0) & vbCrLf)

        If AddGyo <> 1 Then

            intAddNow = 1
            'チャンネル番号が3桁の場合は4桁表示に変更
            For i As Integer = 1 To intAddGyo - 1

                If Len(strAddCH(i)) = 3 Then
                    strAddCHNo = "0" & strAddCH(i)
                Else
                    strAddCHNo = strAddCH(i)
                End If

                Dim strFuADR As String = fnGetFUadr_ADD(CInt(strAddCHNo))
                strAddCHNo = strAddCHNo & " / (" & strFuADR & ")"

                'CH番号表示
                txtResult.AppendText(strAddCHNo & vbCrLf)

                'ログファイル作成用に情報保持
                strAddChinfo(intAddNow) = strAddCHNo
                intAddNow = intAddNow + 1

                'Ver2.0.2.8 関連ﾃｰﾌﾞﾙ検索
                If gCompareChkBIG(4) = True Then
                    intMsgGyo = 0
                    Call ChkProc(strAddCH(i), intMsgGyo)
                    Call mMsgGrid2(intMsgGyo, strAddChinfo, intAddNow)
                End If

            Next
        Else
            txtResult.AppendText("NONE" & vbCrLf)

            'ログファイル作成用に情報保持
            strAddChinfo(1) = "NONE"
            intAddNow = intAddNow + 1
            AddGyo = 2
        End If

        '題目表示
        strDelChinfo(0) = ("■■■■ DEL CHANNEL　■■■■")
        txtResult.AppendText(strDelChinfo(0) & vbCrLf)

        If DelGyo <> 1 Then

            'チャンネル番号が3桁の場合は4桁表示に変更
            intDelNow = 1
            For i As Integer = 1 To intDelGyo - 1

                If Len(strDellCH(i)) = 3 Then
                    strDelCHNo = "0" & strDellCH(i)
                Else
                    strDelCHNo = strDellCH(i)
                End If

                Dim strFuADR As String = fnGetFUadr_DEL(CInt(strDelCHNo))
                strDelCHNo = strDelCHNo & " / (" & strFuADR & ")"

                'CH番号表示
                txtResult.AppendText(strDelCHNo & vbCrLf)

                'ログファイル作成用に情報保持
                strDelChinfo(intDelNow) = strDelCHNo
                intDelNow = intDelNow + 1

                'Ver2.0.2.8 関連ﾃｰﾌﾞﾙ検索
                'DEL_CHは対象をTarget(比較先)にするのが基本だが、ゴミを探すためにSource(比較元)で良い
                If gCompareChkBIG(4) = True Then
                    intMsgGyo = 0
                    Call ChkProc(strDellCH(i), intMsgGyo)
                    Call mMsgGrid2(intMsgGyo, strDelChinfo, intDelNow)
                End If
            Next
        Else
            txtResult.AppendText("NONE" & vbCrLf)

            'ログファイル作成用に情報保持
            strDelChinfo(1) = "NONE"
            intDelNow = intDelNow + 1
            DelGyo = 2
        End If

        'ログファイル作成用に転記
        '(追加)
        For x As Integer = 0 To intAddNow - 1
            'Ver2.0.3.2 結果保存件数を無限に変更
            ReDim Preserve strLogTxtData(intLogGyo)

            strLogTxtData(intLogGyo) = strAddChinfo(x)
            intLogGyo = intLogGyo + 1
        Next

        '(削除)
        For y As Integer = 0 To intDelNow - 1
            'Ver2.0.3.2 結果保存件数を無限に変更
            ReDim Preserve strLogTxtData(intLogGyo)

            strLogTxtData(intLogGyo) = strDelChinfo(y)
            intLogGyo = intLogGyo + 1
        Next

    End Sub

    'Ver2.0.0.2 CHnoからFUアドレスを作成
    Private Function fnGetFUadr_ADD(pintCHno As Integer) As String
        Dim strRet As String = ""

        '追加分のため比較先から取得
        For i As Integer = 0 To UBound(mudtTarget.SetChInfo.udtChannel) Step 1
            With mudtTarget.SetChInfo.udtChannel(i)
                If .udtChCommon.shtChno = pintCHno Then
                    Dim strFUAdd As String = GetFUAddress(.udtChCommon.shtFuno, 1)
                    Dim strPortAdd As String = GetFUAddress(.udtChCommon.shtPortno, 2)
                    Dim strPinAdd As String = GetFUAddress(.udtChCommon.shtPin, 3)
                    strRet = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

                    Exit For
                End If
            End With
        Next i

        Return strRet
    End Function
    Private Function fnGetFUadr_DEL(pintCHno As Integer) As String
        Dim strRet As String = ""

        '削除分のため比較元から取得
        For i As Integer = 0 To UBound(mudtSource.SetChInfo.udtChannel) Step 1
            With mudtSource.SetChInfo.udtChannel(i)
                If .udtChCommon.shtChno = pintCHno Then
                    Dim strFUAdd As String = GetFUAddress(.udtChCommon.shtFuno, 1)
                    Dim strPortAdd As String = GetFUAddress(.udtChCommon.shtPortno, 2)
                    Dim strPinAdd As String = GetFUAddress(.udtChCommon.shtPin, 3)
                    strRet = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

                    Exit For
                End If
            End With
        Next i

        Return strRet
    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 変更点情報表示（チャンネル用）
    ' 引数      ： 行数
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mMsgGrid(ByVal Gyo As Integer, ByVal TitleFlg As Boolean, ByVal FUAddress As String, ByVal CHChgContFlg As Boolean)

        Dim strCHNo As String
        Dim strTag As String
        '数値型のCHNoを入れる変数定義
        Dim iCHNo As Short = 0
        '#を書くか書かないかのフラグ定義
        Dim DummyFlg As Boolean
        DummyFlg = False
        'For文のｶｳﾝﾀ定義
        Dim j As Integer
        If CHChgContFlg = True Then

            'チャンネル番号が3桁の場合は4桁表示に変更
            strCHNo = msgtemp(1)

            strTag = fnGetTagNo(strCHNo)

            'strCHNoが数値型に変換できる場合(例外回避のため)
            If IsNumeric(strCHNo) = True Then
                'strCHNo(文字列)を数値型変換
                iCHNo = Integer.Parse(strCHNo)
            End If

            'strCHNoが数値型変換され、正常にiCHNoに代入できた場合
            If iCHNo <> 0 Then
                'CHNOが一致するところを検索(For)
                For j = 0 To 2999
                    If mudtTarget.SetChInfo.udtChannel(j).udtChCommon._shtChno = iCHNo Then
                        'DummyCommonFuAddressがTrueか確認(if)
                        If mudtTarget.SetChInfo.udtChannel(j).DummyCommonFuAddress = True Then
                            'TrueだったらフラグON
                            DummyFlg = True
                            Exit For
                        End If
                    End If
                Next
            End If

            'Ver2.0.0.7 FUつけるつけないｵﾌﾟｼｮﾝ
            If gCompareChkBIG(3) = True Then
                'FUつける＝通常
                'mudtTarget.udtChannel(i).udtChCommon._shtChno
                'mudtTarget.udtChannel(i).udtChCommon.DummyCommonFuAddress
                'DummyFlg = True の場合、#を付ける
                If DummyFlg = True Then
                    If Len(strCHNo) = 3 Then
                        strCHNo = "0" & strCHNo & strTag & " / (#" & FUAddress & ")" & vbCrLf
                    Else
                        strCHNo = strCHNo & strTag & " / (#" & FUAddress & ")" & vbCrLf
                    End If
                    'DummyFlg = False
                Else
                    If Len(strCHNo) = 3 Then
                        strCHNo = "0" & strCHNo & strTag & " / (" & FUAddress & ")" & vbCrLf
                    Else
                        strCHNo = strCHNo & strTag & " / (" & FUAddress & ")" & vbCrLf
                    End If
                End If

            Else
                'FUつけない
                If Len(strCHNo) = 3 Then
                    strCHNo = "0" & strCHNo & strTag & vbCrLf
                Else
                    strCHNo = strCHNo & strTag & vbCrLf
                End If
            End If


                'タイトル表示
                If TitleFlg = True Then
                    txtResult.AppendText(msgtemp(0) & vbCrLf)
                End If

                'CH番号表示
                txtResult.AppendText(strCHNo)
                msgtemp(1) = strCHNo.Replace(vbCrLf, "")

                '内容表示
                For x As Integer = 2 To Gyo - 1
                    txtResult.AppendText(msgtemp(x))
                    txtResult.AppendText(vbCrLf)
                Next

                For i As Integer = 0 To Gyo - 1
                    If TitleFlg = True Then
                        'Ver2.0.3.2 結果保存件数を無限に変更
                        ReDim Preserve strLogTxtData(intLogGyo)

                        'ログファイル作成用に転記
                        strLogTxtData(intLogGyo) = msgtemp(i)
                        intLogGyo = intLogGyo + 1
                    Else
                        If msgtemp(i + 1) <> "" Then
                            'Ver2.0.3.2 結果保存件数を無限に変更
                            ReDim Preserve strLogTxtData(intLogGyo)

                            strLogTxtData(intLogGyo) = msgtemp(i + 1)
                            intLogGyo = intLogGyo + 1
                        End If
                    End If
                Next

            Else
                msgtemp(1) = "NONE"

                txtResult.AppendText(msgtemp(0) & vbCrLf)
                txtResult.AppendText("NONE" & vbCrLf)

                For x As Integer = 0 To 1
                    'Ver2.0.3.2 結果保存件数を無限に変更
                    ReDim Preserve strLogTxtData(intLogGyo)

                    strLogTxtData(intLogGyo) = msgtemp(x)
                    intLogGyo = intLogGyo + 1
                Next

            End If

            '内容削除
            For x As Integer = 0 To Gyo - 1
                msgtemp(x) = ""
            Next

    End Sub
    'Ver2.0.2.8 ADD,DEL関連ﾃｰﾌﾞﾙ専用
    Private Sub mMsgGrid2(ByVal Gyo As Integer, ByRef pstrData() As String, ByRef pintCount As Integer)

        '内容表示
        For x As Integer = 0 To Gyo - 1
            txtResult.AppendText(msgtemp(x))
            txtResult.AppendText(vbCrLf)
        Next

        For i As Integer = 0 To Gyo - 1
            If msgtemp(i) <> "" Then
                ''Ver2.0.3.2 結果保存件数を無限に変更
                'ReDim Preserve strLogTxtData(intLogGyo)

                'strLogTxtData(intLogGyo) = msgtemp(i + 1)
                'intLogGyo = intLogGyo + 1
                pstrData(pintCount) = msgtemp(i)
                pintCount = pintCount + 1
            End If
        Next


        '内容削除
        For x As Integer = 0 To Gyo - 1
            msgtemp(x) = ""
        Next

    End Sub

    'CHnoからTagNoを取得する
    Private Function fnGetTagNo(pstrCHno As String) As String
        Dim strRet As String = ""
        Try
            'Tagモードじゃないなら""を戻す
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then
                Return ""
            End If

            '数値じゃないなら""を戻す
            If IsNumeric(NZf(pstrCHno)) = False Then
                Return ""
            End If

            'CHno数値変換
            Dim intCHno As Integer = CInt(pstrCHno)

            'CHnoからChInfoの該当index取得
            Dim strTag As String = ""
            Dim intIndex As Integer = gConvChNoToChArrayId(intCHno)
            If intIndex >= 0 Then
                'CHtypeからタグ番号を取得
                With gudt.SetChInfo.udtChannel(intIndex)
                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeAnalog
                            'ｱﾅﾛｸﾞ
                            strTag = .AnalogTagNo
                        Case gCstCodeChTypeDigital
                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                                'ｼｽﾃﾑ
                                strTag = .SystemTagNo
                            Else
                                'ﾃﾞｼﾞﾀﾙ
                                strTag = .DigitalTagNo
                            End If
                        Case gCstCodeChTypeMotor
                            'ﾓｰﾀｰ
                            strTag = .MotorTagNo
                        Case gCstCodeChTypeValve
                            'ﾊﾞﾙﾌﾞ
                            Select Case .udtChCommon.shtData
                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                    strTag = .ValveAiDoTagNo
                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2, gCstCodeChDataTypeValveAO_4_20
                                    strTag = .ValveAiAoTagNo
                                Case Else
                                    strTag = .ValveDiDoTagNo
                            End Select
                        Case gCstCodeChTypeComposite
                            'ｺﾝﾎﾟｼﾞｯﾄ
                            strTag = .CompositeTagNo
                        Case gCstCodeChTypePulse
                            If .udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                                'ﾊﾟﾙｽ
                                strTag = .PulseTagNo
                            Else
                                '積算
                                strTag = .RevoTagNo
                            End If
                        Case Else
                            strTag = ""
                    End Select
                End With
            Else
                'CHnoがChInfoに無いなら""を戻す
                Return ""
            End If

            If NZfS(strTag) = "" Then
                strTag = ""
            Else
                strTag = "(" & strTag.Trim & ")"
            End If

            strRet = strTag
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRet
    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： 変更点情報表示（システム用）
    ' 引数      ： 行数
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mMsgSysGrid(ByVal Gyo As Integer)

        '内容表示
        For x As Integer = 0 To Gyo - 1
            txtResult.AppendText(msgSYStemp(x))
            txtResult.AppendText(vbCrLf)

            'Ver2.0.3.2 結果保存件数を無限に変更
            ReDim Preserve strLogTxtData(intLogGyo)

            'ログファイル作成用に転記
            strLogTxtData(intLogGyo) = msgSYStemp(x)
            intLogGyo = intLogGyo + 1
        Next

        '内容削除
        For i As Integer = 0 To Gyo - 1
            msgSYStemp(i) = ""
        Next

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（チャンネル用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateStr(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String) As String

        mMsgCreateStr = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateStr = mMsgContentLen(mMsgCreateStr, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（チャンネル用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSht(ByVal strContent As String, ByVal shtOldValue As UShort, ByVal shtNewValue As UShort) As String

        Dim strOldValue As String = ""
        Dim strNewValue As String = ""

        If shtOldValue = &HFFFF Or shtOldValue = &HFF Then
            strOldValue = " "
        Else
            strOldValue = Trim(Str(shtOldValue))
        End If

        If shtNewValue = &HFFFF Or shtNewValue = &HFF Then
            strNewValue = " "
        Else
            strNewValue = Trim(Str(shtNewValue))
        End If

        mMsgCreateSht = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateSht = mMsgContentLen(mMsgCreateSht, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（CHID用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSht_CHID(ByVal strContent As String, ByVal shtOldValue As UShort, ByVal shtNewValue As UShort) As String

        Dim strOldValue As String = ""
        Dim strNewValue As String = ""
        'FF=255はありえるため、空白ではない

        If shtOldValue = &HFFFF Then
            strOldValue = " "
        Else
            strOldValue = Trim(Str(shtOldValue))
        End If

        If shtNewValue = &HFFFF Then
            strNewValue = " "
        Else
            strNewValue = Trim(Str(shtNewValue))
        End If

        mMsgCreateSht_CHID = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateSht_CHID = mMsgContentLen(mMsgCreateSht_CHID, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（チャンネル用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateByt(ByVal strContent As String, ByVal bytOldValue As Byte, ByVal bytNewValue As Byte) As String

        Dim strOldValue As String = ""
        Dim strNewValue As String = ""

        If bytOldValue = &HFF Or bytOldValue = &HFFFF Then
            strOldValue = " "
        Else
            strOldValue = Trim(Str(bytOldValue))
        End If

        If bytNewValue = &HFF Or bytNewValue = &HFFFF Then
            strNewValue = " "
        Else
            strNewValue = Trim(Str(bytNewValue))
        End If

        mMsgCreateByt = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateByt = mMsgContentLen(mMsgCreateByt, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（チャンネル用）
    '               Ver1.11.5 2016.08.25 小数点以下桁数表示対応
    '               Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定統合
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateint(ByVal strContent As String, ByVal bytOldValue As Integer, ByVal bytNewValue As Integer, _
                                   ByVal bytOldPoint As Byte, ByVal bytNewPoint As Byte, _
                                   ByVal bOldDummy As Boolean, ByVal bNewDummy As Boolean) As String

        Dim strOldValue As String = ""
        Dim strNewValue As String = ""

        Dim strOldDummy As String = ""      '' Ver1.11.6 2016.09.15 
        Dim strNewDummy As String = ""      '' Ver1.11.6 2016.09.15 


        'Ver2.0.4.0 0は表示

        'If bytOldValue = 0 Or bytOldValue = &HFF Or bytOldValue = &HFFFF Then
        If bytOldValue = &HFF Or bytOldValue = &HFFFF Then
            strOldValue = " "
        ElseIf bytOldPoint <> 0 Then        '' Ver1.11.5 2016.08.24  小数点以下対応
            strOldValue = GetNumFormat(bytOldValue, bytOldPoint)
        Else
            strOldValue = Trim(Str(bytOldValue))
        End If

        'If bytNewValue = 0 Or bytNewValue = &HFF Or bytNewValue = &HFFFF Then
        If bytNewValue = &HFF Or bytNewValue = &HFFFF Then
            strNewValue = " "
        ElseIf bytNewPoint <> 0 Then        '' Ver1.11.5 2016.08.24  小数点以下対応
            strNewValue = GetNumFormat(bytNewValue, bytNewPoint)
        Else
            strNewValue = Trim(Str(bytNewValue))
        End If

        '’Ver1.11.6 2016.09.15 ﾀﾞﾐｰ表示追加
        If bOldDummy = True Then
            strOldDummy = "#"
        End If

        If bNewDummy = True Then
            strNewDummy = "#"
        End If
        ''

        '’Ver1.11.6 2016.09.15 ﾀﾞﾐｰ表示追加
        mMsgCreateint = strContent & " " & ":" & " " & strOldDummy & strOldValue & " " & "→" & " " & strNewDummy & strNewValue

        '表示調整
        mMsgCreateint = mMsgContentLen(mMsgCreateint, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（システム用1段階）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSys(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strEquipname As String, ByVal strNo As String) As String

        '機器の番号がある場合
        If strEquipname = "" Then
            mMsgCreateSys = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            '機器番号がない場合
        Else
            mMsgCreateSys = strEquipname & strNo & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
        End If

        '表示調整
        mMsgCreateSys = mMsgContentLen(mMsgCreateSys, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（システム用2段階）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSys1(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strEquipname As String, ByVal strNo As String, ByVal strEquipname1 As String, ByVal strNo1 As String) As String

        '機器の番号がある場合
        If strEquipname1 = "" Then
            mMsgCreateSys1 = strEquipname & strNo & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            '機器番号がない場合
        Else
            mMsgCreateSys1 = strEquipname & strNo & " " & strEquipname1 & strNo1 & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
        End If

        '表示調整
        mMsgCreateSys1 = mMsgContentLen(mMsgCreateSys1, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（システム用3段階）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSys2(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strEquipname As String, ByVal strNo As String, ByVal strEquipname1 As String, ByVal strNo1 As String, ByVal strEquipname2 As String, ByVal strNo2 As String) As String

        mMsgCreateSys2 = strEquipname & strNo & " " & strEquipname1 & strNo1 & " " & strEquipname2 & strNo2 & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateSys2 = mMsgContentLen(mMsgCreateSys2, False)

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（端子表比較専用）
    ' 引数      ： 変更内容/変更前値/変更値/FUアドレス
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSysTer(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strFUadr As String) As String

        mMsgCreateSysTer = strFUadr & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue

        '表示調整
        mMsgCreateSysTer = mMsgContentLen(mMsgCreateSysTer, False)

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（シーケンス用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateSEQ(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strSEQ As String, ByVal strNo1 As String, ByVal strInput As String, ByVal strNo2 As String) As String

        If strInput = "" Then
            mMsgCreateSEQ = strSEQ & strNo1 & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
        Else
            mMsgCreateSEQ = strSEQ & strNo1 & " " & strInput & strNo2 & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
        End If

        '表示調整
        mMsgCreateSEQ = mMsgContentLen(mMsgCreateSEQ, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（OPS用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateOPS(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, _
                                   ByVal strMenu As String, ByVal strMenuNo As String, ByVal strSubMenu As String, ByVal strSubMenuNo As String, ByVal strSubgroup As String, ByVal strSubgroupNo As String) As String

        If strSubMenu = "" Then
            mMsgCreateOPS = strMenu & strMenuNo & " : " & strOldValue & " " & "→" & " " & strNewValue
        ElseIf strSubgroup = "" Then
            mMsgCreateOPS = strMenu & strMenuNo & " " & strSubMenu & strSubMenuNo & " : " & strOldValue & " " & "→" & " " & strNewValue
        Else
            mMsgCreateOPS = strMenu & strMenuNo & " " & strSubMenu & strSubMenuNo & " " & strSubgroup & strSubgroupNo & " : " & strOldValue & " " & "→" & " " & strNewValue
        End If

        '表示調整
        mMsgCreateOPS = mMsgContentLen(mMsgCreateOPS, False)

    End Function
    'Ver2.0.7.B
    Private Function mMsgCreateOPS_MENU(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, _
                                   ByVal strMenu As String, ByVal strMenuNo As String, ByVal strSubMenu As String, ByVal strSubMenuNo As String, ByVal strSubgroup As String, ByVal strSubgroupNo As String) As String

        If strSubMenu = "" Then
            mMsgCreateOPS_MENU = strMenu & strMenuNo & " " & strContent & " : " & strOldValue & " " & "→" & " " & strNewValue
        ElseIf strSubgroup = "" Then
            mMsgCreateOPS_MENU = strMenu & strMenuNo & " " & strContent & " " & strSubMenu & strSubMenuNo & " : " & strOldValue & " " & "→" & " " & strNewValue
        Else
            mMsgCreateOPS_MENU = strMenu & strMenuNo & " " & strContent & " " & strSubMenu & strSubMenuNo & " " & strSubgroup & strSubgroupNo & " : " & strOldValue & " " & "→" & " " & strNewValue
        End If

        '表示調整
        mMsgCreateOPS_MENU = mMsgContentLen(mMsgCreateOPS_MENU, False)

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（ダミー用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgDummyCreateStr(ByVal strContent As String, ByVal strCondition As String) As String

        mMsgDummyCreateStr = strContent & " : " & strCondition

        '表示調整
        mMsgDummyCreateStr = mMsgContentLen(mMsgDummyCreateStr, False)

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（テーブルファイル用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateTbl(ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strCHNo As String, ByVal intNo As Integer, _
                                   ByVal strTableName As String, ByVal intTblNo As Integer, ByVal CHFlg As Boolean) As String

        Dim StrViewCHNo As String

        'CHNo表示変更(3桁の場合→4桁に変更)
        StrViewCHNo = mMsgCHConv(intNo)

        'チャンネル番号
        If CHFlg = True Then
            mMsgCreateTbl = strTableName & intTblNo & "/" & strCHNo & StrViewCHNo & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            mMsgCreateTbl = mMsgContentLen(mMsgCreateTbl, True)
        Else
            mMsgCreateTbl = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            mMsgCreateTbl = mMsgContentLen(mMsgCreateTbl, False)
        End If

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 比較点表示用変換（出力チャンネル設定専用）
    ' 引数      ： 変更内容/変更前値/変更値
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCreateTbl_OUT( _
                        ByVal pstrFuno As String, _
                        ByVal pstrPortNo As String, _
                        ByVal pstrPinNo As String, _
                        ByVal strContent As String, ByVal strOldValue As String, ByVal strNewValue As String, ByVal strCHNo As String, _
                        ByVal strTableName As String, ByVal intTblNo As Integer, ByVal CHFlg As Boolean) As String

        Dim StrViewFuAdr As String

        'CHNo表示変更(3桁の場合→4桁に変更)
        StrViewFuAdr = pstrFuno & "-" & pstrPortNo & "-" & pstrPinNo

        'チャンネル番号
        If CHFlg = True Then
            mMsgCreateTbl_OUT = strTableName & intTblNo & "/" & strCHNo & StrViewFuAdr & " " & strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            mMsgCreateTbl_OUT = mMsgContentLen(mMsgCreateTbl_OUT, True)
        Else
            mMsgCreateTbl_OUT = strContent & " " & ":" & " " & strOldValue & " " & "→" & " " & strNewValue
            mMsgCreateTbl_OUT = mMsgContentLen(mMsgCreateTbl_OUT, False)
        End If

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 内容表示文字数調整
    ' 引数      ： 
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgContentLen(ByVal strmsg As String, ByVal RightSideFlg As Boolean) As String

        Dim MAXLen As Integer = 30
        Dim strmsgLen As Integer
        Dim RemainLen As Integer
        Dim MojiPlace As Integer
        Dim strMidmsg As String = ""
        Dim strMsgBF As String = ""
        Dim strMsgAF As String = ""

        '全体の長さ
        strmsgLen = Len(strmsg)

        '":"の位置を探す
        MojiPlace = InStr(strmsg, ":")

        '":"位置前部の文字を抜き出し
        For x As Integer = 0 To MojiPlace - 2
            strMidmsg = strmsg.Substring(x, 1)
            strMsgBF = strMsgBF & strMidmsg
        Next

        '":"位置後部の文字を抜き出し
        For y As Integer = MojiPlace + 1 To strmsgLen - 1
            strMidmsg = strmsg.Substring(y, 1)

            If y = MojiPlace + 1 And strMidmsg = " " Then
                strMsgAF = strMsgAF & strMidmsg
            Else
                If strMidmsg <> vbNullChar Then
                    strMsgAF = strMsgAF & strMidmsg
                End If
            End If
        Next

        '不要な文字削除
        'strMsgAF = Trim(strMsgAF)

        '":"位置前部長さを調整
        If Len(strmsgLen) < MAXLen Then
            RemainLen = MAXLen - Len(strmsgLen)

            If RightSideFlg = False Then
                strMsgBF = "     " & strMsgBF.PadRight(MAXLen)
            Else
                strMsgBF = strMsgBF.PadRight(MAXLen + 5)
            End If

        End If

            mMsgContentLen = strMsgBF & ":" & " " & strMsgAF

    End Function

    Private Function mGetMCEng(ByVal udtMC As gEnmMachineryCargo) As String

        Select Case udtMC
            Case gEnmMachineryCargo.mcMachinery
                Return "Machinery "
            Case gEnmMachineryCargo.mcCargo
                Return "Cargo "
            Case Else
                Return ""
        End Select

    End Function

    Private Function mGetMCEng_MENU(ByVal udtMC As gEnmMachineryCargo) As String

        Select Case udtMC
            Case gEnmMachineryCargo.mcMachinery
                Return "OPS "
            Case gEnmMachineryCargo.mcCargo
                Return "VDU "
            Case Else
                Return ""
        End Select

    End Function


    '--------------------------------------------------------------------
    ' 機能      : ログファイル書込み
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub CompareLogTxtWrite()

        'Dim intFileNo As Integer
        Dim strFullPath As String

        Try
            'Ver2.0.1.5 MC比較は別ファイル名
            '書込み用ファイル指定
            If chkMC.Checked = True Then
                'MC比較
                strFullPath = txtTargetPath.Text & txtTargetFile.Text & "\compMC_" & txtTargetFile.Text & ".txt"
            Else
                '通常比較
                strFullPath = txtTargetPath.Text & txtTargetFile.Text & "\comp_" & txtTargetFile.Text & ".txt"
            End If



            '既に存在する場合は削除
            If System.IO.File.Exists(strFullPath) = True Then
                Call My.Computer.FileSystem.DeleteFile(strFullPath)
            End If

            'Ver2.0.3.2 ﾌｧｲﾙの書き込みをStreamへ変更
            'ファイルを開く
            'intFileNo = FreeFile()
            'FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)
            Dim sw As IO.StreamWriter
            sw = New IO.StreamWriter(strFullPath, False, System.Text.Encoding.GetEncoding("Shift-JIS"))
            

            '内容書込み
            For i As Integer = 0 To UBound(strLogTxtData)
                'FilePut(intFileNo, strLogTxtData(i) + vbCrLf)
                sw.WriteLine(strLogTxtData(i))
            Next

            'ファイルを閉じる
            'FileClose(intFileNo)
            If sw Is Nothing = False Then sw.Close()


            'Ver2.0.0.0 連続比較対策 ログ変数と件数をクリア
            Dim j As Integer = 0
            For j = 0 To UBound(strLogTxtData) Step 1
                strLogTxtData(j) = ""
            Next j
            intLogGyo = 0

            cmdLogTxt.Enabled = True               'コンペア完了までログファイル参照操作不可
            cmdPrint.Enabled = True                'コンペア完了まで印刷操作不可

        Catch ex As Exception
            MsgBox("comp_" & txtTargetFile.Text & ".txt file is already open.", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 印刷処理
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            If mstrPrintingPosition = 0 Then
                '改行記号を'\n'に統一する
                mstrPrintingText = mstrPrintingText.Replace(vbCrLf, vbLf)
            End If

            '印刷する初期位置を決定
            Dim x As Integer = e.MarginBounds.Left
            Dim y As Integer = e.MarginBounds.Top

            '1ページに収まらなくなるか、印刷する文字がなくなるかまでループ
            While e.MarginBounds.Bottom > y + mstrPrintFont.Height AndAlso _
                    mstrPrintingPosition < mstrPrintingText.Length
                Dim line As String = ""

                While True
                    '印刷する文字がなくなるか、
                    '改行の時はループから抜けて印刷する
                    If mstrPrintingPosition >= mstrPrintingText.Length OrElse _
                            mstrPrintingText.Chars(mstrPrintingPosition) = vbLf Then
                        mstrPrintingPosition += 1
                        Exit While
                    End If
                    '一文字追加し、印刷幅を超えるか調べる
                    line += mstrPrintingText.Chars(mstrPrintingPosition)
                    If e.Graphics.MeasureString(line, mstrPrintFont).Width _
                            > e.MarginBounds.Width Then
                        '印刷幅を超えたため、折り返す
                        line = line.Substring(0, line.Length - 1)
                        Exit While
                    End If
                    '印刷文字位置を次へ
                    mstrPrintingPosition += 1
                End While
                '一行書き出す
                e.Graphics.DrawString(line, mstrPrintFont, Brushes.Black, x, y)
                '次の行の印刷位置を計算
                y += mstrPrintFont.GetHeight(e.Graphics)
            End While

            '次のページがあるか調べる
            If mstrPrintingPosition >= mstrPrintingText.Length Then
                e.HasMorePages = False
                '初期化する
                mstrPrintingPosition = 0
            Else
                e.HasMorePages = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "比較関数"

#Region "システム設定OK"

    Friend Function mCompareSetSystem(ByVal udt1 As gTypSetSystem, ByVal udt2 As gTypSetSystem) As Integer

        Try

            Dim i As Integer = 1

            Dim udtSetSystem As gTypSetSystem = Nothing

            Dim intCnt As Integer = 0
            Dim strSystem() As String = Nothing
            Dim strFcu() As String = Nothing
            Dim strOpsComm() As String = Nothing
            Dim strOpsN() As String = Nothing
            Dim strGwsN() As String = Nothing
            Dim strPrinterComm() As String = Nothing
            Dim strPeinterN() As String = Nothing

            '========================
            ''システム設定
            '========================

            msgSYStemp(0) = "■■■■　SYSTEM　■■■■"

            With udtSetSystem.udtSysSystem

                ''システムクロック
                If udt1.udtSysSystem.shtClock <> udt2.udtSysSystem.shtClock Then
                    msgSYStemp(i) = mMsgCreateSys("CLOCK", udt1.udtSysSystem.shtClock, udt2.udtSysSystem.shtClock, "", "")
                    i = i + 1
                End If

                ''日付フォーマット
                If udt1.udtSysSystem.shtDate <> udt2.udtSysSystem.shtDate Then
                    msgSYStemp(i) = mMsgCreateSys("DATE", "0x" & Hex(udt1.udtSysSystem.shtDate).PadLeft(2, "0"), "0x" & Hex(udt2.udtSysSystem.shtDate).PadLeft(2, "0"), "", "")
                    i = i + 1
                End If

                ''日本語対応
                If udt1.udtSysSystem.shtLanguage <> udt2.udtSysSystem.shtLanguage Then
                    msgSYStemp(i) = mMsgCreateSys("LANGUAGE", udt1.udtSysSystem.shtLanguage, udt2.udtSysSystem.shtLanguage, "", "")
                    i = i + 1
                End If

                ''取扱説明書(言語)
                If udt1.udtSysSystem.shtManual <> udt2.udtSysSystem.shtManual Then
                    msgSYStemp(i) = mMsgCreateSys("MANUAL", udt1.udtSysSystem.shtManual, udt2.udtSysSystem.shtManual, "", "")
                    i = i + 1
                End If

                ''GL船級仕様
                If udt1.udtSysSystem.shtgl_spec <> udt2.udtSysSystem.shtgl_spec Then
                    msgSYStemp(i) = mMsgCreateSys("GL_SPECIFICATION", udt1.udtSysSystem.shtgl_spec, udt2.udtSysSystem.shtgl_spec, "", "")
                    i = i + 1
                End If

                ''船名
                If Not gCompareString(udt1.udtSysSystem.strShipName, udt2.udtSysSystem.strShipName) Then
                    msgSYStemp(i) = mMsgCreateSys("SHIP NAME", gGetString(udt1.udtSysSystem.strShipName), gGetString(udt2.udtSysSystem.strShipName), "", "")
                    i = i + 1
                End If

                ''コンバイン設定（パート）
                If udt1.udtSysSystem.shtCombineUse <> udt2.udtSysSystem.shtCombineUse Then
                    msgSYStemp(i) = mMsgCreateSys("COMBINE_USE", udt1.udtSysSystem.shtCombineUse, udt2.udtSysSystem.shtCombineUse, "", "")
                    i = i + 1
                End If

                ''コンバイン設定（FS/BS）
                If gBitCheck(udt1.udtSysSystem.shtCombineSeparate, 0) <> gBitCheck(udt2.udtSysSystem.shtCombineSeparate, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("COMBINE_FS/BS", gBitValue(udt1.udtSysSystem.shtCombineSeparate, 0), gBitValue(udt2.udtSysSystem.shtCombineSeparate, 0), "", "")
                    i = i + 1
                End If

                'Ver2.0.7.H
                'コンバイン設定(ログプリンタパートフラグ)
                If gBitCheck(udt1.udtSysSystem.shtCombineSeparate, 1) <> gBitCheck(udt2.udtSysSystem.shtCombineSeparate, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("COMBINE LOG PRINT PART FLG", gBitValue(udt1.udtSysSystem.shtCombineSeparate, 1), gBitValue(udt2.udtSysSystem.shtCombineSeparate, 1), "", "")
                    i = i + 1
                End If


                ''GWS1有無
                If gBitCheck(udt1.udtSysSystem.shtGWS1, 0) <> gBitCheck(udt2.udtSysSystem.shtGWS1, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS1_USE", gBitValue(udt1.udtSysSystem.shtGWS1, 0), gBitValue(udt2.udtSysSystem.shtGWS1, 0), "", "")
                    i = i + 1
                End If

                ''GWS1 Aonly設定
                If gBitCheck(udt1.udtSysSystem.shtGWS1, 1) <> gBitCheck(udt2.udtSysSystem.shtGWS1, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS1_ONLY", gBitValue(udt1.udtSysSystem.shtGWS1, 1), gBitValue(udt2.udtSysSystem.shtGWS1, 1), "", "")
                    i = i + 1
                End If

                ''GWS1 AandB設定
                If gBitCheck(udt1.udtSysSystem.shtGWS1, 2) <> gBitCheck(udt2.udtSysSystem.shtGWS1, 2) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS1_A_and_B", gBitValue(udt1.udtSysSystem.shtGWS1, 2), gBitValue(udt2.udtSysSystem.shtGWS1, 2), "", "")
                    i = i + 1
                End If

                ''GWS2有無
                If gBitCheck(udt1.udtSysSystem.shtGWS2, 0) <> gBitCheck(udt2.udtSysSystem.shtGWS2, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS2_USE", gBitValue(udt1.udtSysSystem.shtGWS2, 0), gBitValue(udt2.udtSysSystem.shtGWS2, 0), "", "")
                    i = i + 1
                End If

                ''GWS2 Aonly設定
                If gBitCheck(udt1.udtSysSystem.shtGWS2, 1) <> gBitCheck(udt2.udtSysSystem.shtGWS2, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS2_ONLY", gBitValue(udt1.udtSysSystem.shtGWS2, 1), gBitValue(udt2.udtSysSystem.shtGWS2, 1), "", "")
                    i = i + 1
                End If

                ''GWS2 AandB設定
                If gBitCheck(udt1.udtSysSystem.shtGWS2, 2) <> gBitCheck(udt2.udtSysSystem.shtGWS2, 2) Then
                    msgSYStemp(i) = mMsgCreateSys("GWS2_A_and_B", gBitValue(udt1.udtSysSystem.shtGWS2, 2), gBitValue(udt2.udtSysSystem.shtGWS2, 2), "", "")
                    i = i + 1
                End If

            End With

            ''========================
            ' ''FCU設定
            ''========================

            If i <> 1 Then
                msgSYStemp(i) = "■■■■　FCU　■■■■"
                i = i + 1
            Else
                msgSYStemp(0) = "■■■■　FCU　■■■■"
            End If

            ''FCU台数
            If udt1.udtSysFcu.shtFcuNo <> udt2.udtSysFcu.shtFcuNo Then
                msgSYStemp(i) = mMsgCreateSys("FCU No", udt1.udtSysFcu.shtFcuNo, udt2.udtSysFcu.shtFcuNo, "", "")
                i = i + 1
            End If

            ''収集周期
            If udt1.udtSysFcu.shtCrrectTime <> udt2.udtSysFcu.shtCrrectTime Then
                msgSYStemp(i) = mMsgCreateSys("CORRECT_TIME", udt1.udtSysFcu.shtCrrectTime, udt2.udtSysFcu.shtCrrectTime, "", "")
                i = i + 1
            End If

            ''FCU拡張ボード
            If udt1.udtSysFcu.shtFcuExtendBord <> udt2.udtSysFcu.shtFcuExtendBord Then
                msgSYStemp(i) = mMsgCreateSys("SIO1", udt1.udtSysFcu.shtFcuExtendBord, udt2.udtSysFcu.shtFcuExtendBord, "", "")
                i = i + 1
            End If

            ''FCU拡張ボード
            If udt1.udtSysFcu.shtFcuExtendBord2 <> udt2.udtSysFcu.shtFcuExtendBord2 Then
                msgSYStemp(i) = mMsgCreateSys("SIO2", udt1.udtSysFcu.shtFcuExtendBord2, udt2.udtSysFcu.shtFcuExtendBord2, "", "")
                i = i + 1
            End If

            ''共有チャンネル使用
            If udt1.udtSysFcu.shtShareChUse <> udt2.udtSysFcu.shtShareChUse Then
                msgSYStemp(i) = mMsgCreateSys("SHARE_CH_USE", udt1.udtSysFcu.shtShareChUse, udt2.udtSysFcu.shtShareChUse, "", "")
                i = i + 1
            End If

            'Ver2.0.7.V
            'FCUフラグ
            If gBitCheck(udt1.udtSysFcu.shtFCU2Flg, 0) <> gBitCheck(udt2.udtSysFcu.shtFCU2Flg, 0) Then
                msgSYStemp(i) = mMsgCreateSys("FCU DataSetSW", gBitValue(udt1.udtSysFcu.shtFCU2Flg, 0), gBitValue(udt2.udtSysFcu.shtFCU2Flg, 0), "", "")
                i = i + 1
            End If


            ''========================
            ' ''OPS設定
            ''========================

            If i <> 1 Then
                msgSYStemp(i) = "■■■■　OPS　■■■■"
                i = i + 1
            Else
                msgSYStemp(0) = "■■■■　OPS　■■■■"
            End If

            ''遠隔操作
            If udt1.udtSysOps.shtControl <> udt2.udtSysOps.shtControl Then
                msgSYStemp(i) = mMsgCreateSys("CONTROL", udt1.udtSysOps.shtControl, udt2.udtSysOps.shtControl, "", "")
                i = i + 1
            End If

            ''EXT.G、G.REP変更禁止
            If udt1.udtSysOps.shtProhibition <> udt2.udtSysOps.shtProhibition Then
                msgSYStemp(i) = mMsgCreateSys("PROHIBITION", udt1.udtSysOps.shtProhibition, udt2.udtSysOps.shtProhibition, "", "")
                i = i + 1
            End If

            ''CHデータ変更許可
            If udt1.udtSysOps.shtChannelEdit <> udt2.udtSysOps.shtChannelEdit Then
                msgSYStemp(i) = mMsgCreateSys("CH_EDIT", udt1.udtSysOps.shtChannelEdit, udt2.udtSysOps.shtChannelEdit, "", "")
                i = i + 1
            End If

            ''アラーム表示方法
            If udt1.udtSysOps.shtAlarm <> udt2.udtSysOps.shtAlarm Then
                msgSYStemp(i) = mMsgCreateSys("ALARM", udt1.udtSysOps.shtAlarm, udt2.udtSysOps.shtAlarm, "", "")
                i = i + 1
            End If

            ''Duty使用可/不可
            If udt1.udtSysOps.shtDuty <> udt2.udtSysOps.shtDuty Then
                msgSYStemp(i) = mMsgCreateSys("DUTY_SET", udt1.udtSysOps.shtDuty, udt2.udtSysOps.shtDuty, "", "")
                i = i + 1
            End If

            ''コントロール　1台インターロック
            If udt1.udtSysOps.shtContOnlyFlag <> udt2.udtSysOps.shtContOnlyFlag Then
                msgSYStemp(i) = mMsgCreateSys("CONT_ONLY", udt1.udtSysOps.shtContOnlyFlag, udt2.udtSysOps.shtContOnlyFlag, "", "")
                i = i + 1
            End If

            ''Auto Alarm表示順序
            If udt1.udtSysOps.shtAlarm_Order <> udt2.udtSysOps.shtAlarm_Order Then
                msgSYStemp(i) = mMsgCreateSys("AUTO_ALARM", udt1.udtSysOps.shtAlarm_Order, udt2.udtSysOps.shtAlarm_Order, "", "")
                i = i + 1
            End If

            '' ﾀｸﾞ表示ﾀｲﾌﾟ Ver1.11.8.6 2016.11.10
            If udt1.udtSysOps.shtTagMode <> udt2.udtSysOps.shtTagMode Then
                msgSYStemp(i) = mMsgCreateSys("TagMode", udt1.udtSysOps.shtTagMode, udt2.udtSysOps.shtTagMode, "", "")
                i = i + 1
            End If

            '' ｱﾗｰﾑﾚﾍﾞﾙ Ver1.11.8.6 2016.11.10
            If udt1.udtSysOps.shtLRMode <> udt2.udtSysOps.shtLRMode Then
                msgSYStemp(i) = mMsgCreateSys("LR(ALM Level)", udt1.udtSysOps.shtLRMode, udt2.udtSysOps.shtLRMode, "", "")
                i = i + 1
            End If

            ''BS CH 20200415 hori
            If udt1.udtSysOps._shtBS_CHNo <> udt2.udtSysOps._shtBS_CHNo Then
                msgSYStemp(i) = mMsgCreateSys("BS CH", udt1.udtSysOps._shtBS_CHNo, udt2.udtSysOps._shtBS_CHNo, "", "")
                i = i + 1
            End If

            ''FS CH 20200415 hori
            If udt1.udtSysOps._shtFS_CHNo <> udt2.udtSysOps._shtFS_CHNo Then
                msgSYStemp(i) = mMsgCreateSys("FS CH", udt1.udtSysOps._shtFS_CHNo, udt2.udtSysOps._shtFS_CHNo, "", "")
                i = i + 1
            End If

            'Ver2.0.0.0 2016.12.07 システム動作フラグ比較
            'システム動作フラグ
            ' History
            If gBitCheck(udt1.udtSysOps.shtSystem, 0) <> gBitCheck(udt2.udtSysOps.shtSystem, 0) Then
                msgSYStemp(i) = mMsgCreateSys("System(history)", gBitCheck(udt1.udtSysOps.shtSystem, 0), gBitCheck(udt2.udtSysOps.shtSystem, 0), "", "")
                i = i + 1
            End If

            ' FCU2台仕様   'hori
            If gBitCheck(udt1.udtSysOps.shtSystem, 1) <> gBitCheck(udt2.udtSysOps.shtSystem, 1) Or _
               gBitCheck(udt1.udtSysOps.shtSystem, 4) <> gBitCheck(udt2.udtSysOps.shtSystem, 4) Then

                Dim OldSysFCU As String = ""
                Dim NewSysFCU As String = ""

                '旧データのFCU構成の文字列割り当て 'hori
                If gBitCheck(udt1.udtSysOps.shtSystem, 1) = False And gBitCheck(udt1.udtSysOps.shtSystem, 4) = False Then
                    OldSysFCU = "Machinery/Cargo"
                ElseIf gBitCheck(udt1.udtSysOps.shtSystem, 1) = True And gBitCheck(udt1.udtSysOps.shtSystem, 4) = False Then
                    OldSysFCU = "Machinery/Hull"
                Else
                    OldSysFCU = "Starboard/Port"
                End If

                '新データの文字列割り当て   'hori
                If gBitCheck(udt2.udtSysOps.shtSystem, 1) = False And gBitCheck(udt2.udtSysOps.shtSystem, 4) = False Then
                    NewSysFCU = "Machinery/Cargo"
                ElseIf gBitCheck(udt2.udtSysOps.shtSystem, 1) = True And gBitCheck(udt2.udtSysOps.shtSystem, 4) = False Then
                    NewSysFCU = "Machinery/Hull"
                Else
                    NewSysFCU = "Starboard/Port"
                End If

                msgSYStemp(i) = mMsgCreateSys("System(Fcu2)", OldSysFCU, NewSysFCU, "", "")
                i = i + 1
            End If

            ' SET LV
            If gBitCheck(udt1.udtSysOps.shtSystem, 2) <> gBitCheck(udt2.udtSysOps.shtSystem, 2) Then
                msgSYStemp(i) = mMsgCreateSys("System(SET LV)", gBitCheck(udt1.udtSysOps.shtSystem, 2), gBitCheck(udt2.udtSysOps.shtSystem, 2), "", "")
                i = i + 1
            End If

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            ' AUTO ALARM FIRE MIMIC
            If gBitCheck(udt1.udtSysOps.shtSystem, 3) <> gBitCheck(udt2.udtSysOps.shtSystem, 3) Then
                msgSYStemp(i) = mMsgCreateSys("System(FIRE ALARM)", gBitCheck(udt1.udtSysOps.shtSystem, 3), gBitCheck(udt2.udtSysOps.shtSystem, 3), "", "")
                i = i + 1
            End If

            'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷するしない設定の比較
            'If udt1.udtSysOps.shtTerVer <> udt2.udtSysOps.shtTerVer Then
            '    Dim strOld As String = ""
            '    Dim strNew As String = ""
            '    If udt1.udtSysOps.shtTerVer = 1 Then
            '        strOld = "Not Print"
            '    Else
            '        strOld = "Print"
            '    End If
            '    If udt2.udtSysOps.shtTerVer = 1 Then
            '        strNew = "Not Print"
            '    Else
            '        strNew = "Print"
            '    End If
            '    msgSYStemp(i) = mMsgCreateSys("Terminal Version Print", strOld, strNew, "", "")
            '    i = i + 1
            'End If



            ''詳細設定
            For ix As Integer = LBound(udt1.udtSysOps.udtOpsDetail) To UBound(udt1.udtSysOps.udtOpsDetail)

                ''OPS接続有無
                If udt1.udtSysOps.udtOpsDetail(ix).shtExist <> udt2.udtSysOps.udtOpsDetail(ix).shtExist Then
                    msgSYStemp(i) = mMsgCreateSys("EXIST", udt1.udtSysOps.udtOpsDetail(ix).shtExist, udt2.udtSysOps.udtOpsDetail(ix).shtExist, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''アラーム表示モード
                If udt1.udtSysOps.udtOpsDetail(ix).shtAlarmDisp <> udt2.udtSysOps.udtOpsDetail(ix).shtAlarmDisp Then
                    msgSYStemp(i) = mMsgCreateSys("ALARM_DISPLAY", udt1.udtSysOps.udtOpsDetail(ix).shtAlarmDisp, udt2.udtSysOps.udtOpsDetail(ix).shtAlarmDisp, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''OPS設定変更
                If udt1.udtSysOps.udtOpsDetail(ix).shtEnable <> udt2.udtSysOps.udtOpsDetail(ix).shtEnable Then
                    msgSYStemp(i) = mMsgCreateSys("ENABLE", udt1.udtSysOps.udtOpsDetail(ix).shtEnable, udt2.udtSysOps.udtOpsDetail(ix).shtEnable, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''遠隔操作（Func）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControl, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControl, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL(FUNCTION)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControl, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControl, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''遠隔操作（Initial）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControl, 1) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControl, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL(INITIAL)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControl, 1), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControl, 1), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''コントロール制御1
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(1)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''コントロール制御2
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 1) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(2)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 1), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 1), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''コントロール制御4
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 2) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 2) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(4)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 2), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 2), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''コントロール制御8
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 3) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 3) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(8)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 3), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 3), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                'Ver2.0.7.R 32,64解放分
                'コントロール制御32
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 5) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 5) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(32)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 5), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 5), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                'Ver2.0.7.R 32,64解放分
                'コントロール制御64
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 6) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 6) Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_FLAG(64)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtControlFlag, 6), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtControlFlag, 5), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''コントロール入力禁止
                If udt1.udtSysOps.udtOpsDetail(ix).shtControlProhFlag <> udt2.udtSysOps.udtOpsDetail(ix).shtControlProhFlag Then
                    msgSYStemp(i) = mMsgCreateSys("CONTROL_PROH_FLAG", udt1.udtSysOps.udtOpsDetail(ix).shtControlProhFlag, udt2.udtSysOps.udtOpsDetail(ix).shtControlProhFlag, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''オペレーションパネル
                If udt1.udtSysOps.udtOpsDetail(ix).shtOperaionPanel <> udt2.udtSysOps.udtOpsDetail(ix).shtOperaionPanel Then
                    msgSYStemp(i) = mMsgCreateSys("OPERATION_PANEL", udt1.udtSysOps.udtOpsDetail(ix).shtOperaionPanel, udt2.udtSysOps.udtOpsDetail(ix).shtOperaionPanel, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''調光機能
                If udt1.udtSysOps.udtOpsDetail(ix).shtAdjustLight <> udt2.udtSysOps.udtOpsDetail(ix).shtAdjustLight Then
                    msgSYStemp(i) = mMsgCreateSys("ADJUST_LIGHT", udt1.udtSysOps.udtOpsDetail(ix).shtAdjustLight, udt2.udtSysOps.udtOpsDetail(ix).shtAdjustLight, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''HATTELAND液晶
                If udt1.udtSysOps.udtOpsDetail(ix).shtHatteland <> udt2.udtSysOps.udtOpsDetail(ix).shtHatteland Then
                    msgSYStemp(i) = mMsgCreateSys("HATTELAND", udt1.udtSysOps.udtOpsDetail(ix).shtHatteland, udt2.udtSysOps.udtOpsDetail(ix).shtHatteland, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''印字パート（Machinery）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtPrintPart, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtPrintPart, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("PRINT_PART(ENGINE)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtPrintPart, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtPrintPart, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''印字パート（Cargo）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtPrintPart, 1) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtPrintPart, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("PRINT_PART(CARGO)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtPrintPart, 1), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtPrintPart, 1), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''OPS表示モード（Machinery）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtOpsType, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtOpsType, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("OPS_TYPE(ENGINE)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtOpsType, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtOpsType, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''OPS表示モード（Cargo）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtOpsType, 1) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtOpsType, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("OPS_TYPE(CARGO)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtOpsType, 1), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtOpsType, 1), "OPS", Str(ix + 1))
                    i = i + 1
                End If

                'Ver2.0.2.0 Mac,Carg別々に判定するように戻す
                'Ver2.0.0.4
                '起動モードは、どちらかがONになればもう片方はOFFになるため結果を1行にする
                ''起動モード（Machinery）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtBootMode, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("MC(ENGINE)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtBootMode, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If
                ''起動モード（Cargo）
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtBootMode, 1) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("MC(CARGO)", gBitValue(udt1.udtSysOps.udtOpsDetail(ix).shtBootMode, 1), gBitValue(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 1), "OPS", Str(ix + 1))
                    i = i + 1
                End If
                'If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix).shtBootMode, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 0) Then
                '    If gBitCheck(udt2.udtSysOps.udtOpsDetail(ix).shtBootMode, 0) = True Then
                '        'MacがONならば、C→M
                '        msgSYStemp(i) = mMsgCreateSys("MC", "CARGO", "ENGINE", "OPS", Str(ix + 1))
                '    Else
                '        'MacがOFFならば、M→C
                '        msgSYStemp(i) = mMsgCreateSys("MC", "ENGINE", "CARGO", "OPS", Str(ix + 1))
                '    End If
                '    i = i + 1
                'End If

                ''BS/FS 20200415 hori
                If gBitCheck(udt1.udtSysOps.udtOpsDetail(ix)._shtSysSet, 0) <> gBitCheck(udt2.udtSysOps.udtOpsDetail(ix)._shtSysSet, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("BS / FS", gBitValue(udt1.udtSysOps.udtOpsDetail(ix)._shtSysSet, 0), gBitValue(udt2.udtSysOps.udtOpsDetail(ix)._shtSysSet, 0), "OPS", Str(ix + 1))
                    i = i + 1
                End If


                ''リポーズサマリ
                If udt1.udtSysOps.udtOpsDetail(ix).shtRepSum <> udt2.udtSysOps.udtOpsDetail(ix).shtRepSum Then
                    msgSYStemp(i) = mMsgCreateSys("REP. SUMMARY", udt1.udtSysOps.udtOpsDetail(ix).shtRepSum, udt2.udtSysOps.udtOpsDetail(ix).shtRepSum, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''通信Aライン
                If udt1.udtSysOps.udtOpsDetail(ix).shtEtherA <> udt2.udtSysOps.udtOpsDetail(ix).shtEtherA Then
                    msgSYStemp(i) = mMsgCreateSys("ETHERNET_LINE", udt1.udtSysOps.udtOpsDetail(ix).shtEtherA, udt2.udtSysOps.udtOpsDetail(ix).shtEtherA, "OPS", Str(ix + 1))
                    i = i + 1
                End If

                ''OPS解像度
                If udt1.udtSysOps.udtOpsDetail(ix).shtResolution <> udt2.udtSysOps.udtOpsDetail(ix).shtResolution Then
                    msgSYStemp(i) = mMsgCreateSys("RESOLUTION", udt1.udtSysOps.udtOpsDetail(ix).shtResolution, udt2.udtSysOps.udtOpsDetail(ix).shtResolution, "OPS", Str(ix + 1))
                    i = i + 1
                End If

            Next

            'Ver2.0.7.P GWSの比較を追加
            If i <> 1 Then
                msgSYStemp(i) = "■■■■　GWS　■■■■"
                i = i + 1
            Else
                msgSYStemp(0) = "■■■■　GWS　■■■■"
            End If

            Dim ii As Integer = 0
            Dim ij As Integer = 0
            Dim strGWSold As String = ""
            Dim strGWSnew As String = ""

            For ii = LBound(udt1.udtSysGws.udtGwsDetail) To UBound(udt1.udtSysGws.udtGwsDetail)
                'OPS接続有無
                If udt1.udtSysGws.udtGwsDetail(ii).shtGwsType <> udt2.udtSysGws.udtGwsDetail(ii).shtGwsType Then
                    msgSYStemp(i) = mMsgCreateSys("GWS Type", udt1.udtSysGws.udtGwsDetail(ii).shtGwsType, udt2.udtSysGws.udtGwsDetail(ii).shtGwsType, "GWS", Str(ii + 1))
                    i = i + 1
                End If
                'IPアドレス
                If _
                    udt1.udtSysGws.udtGwsDetail(ii).bytIP1 <> udt2.udtSysGws.udtGwsDetail(ii).bytIP1 Or _
                    udt1.udtSysGws.udtGwsDetail(ii).bytIP2 <> udt2.udtSysGws.udtGwsDetail(ii).bytIP2 Or _
                    udt1.udtSysGws.udtGwsDetail(ii).bytIP3 <> udt2.udtSysGws.udtGwsDetail(ii).bytIP3 Or _
                    udt1.udtSysGws.udtGwsDetail(ii).bytIP4 <> udt2.udtSysGws.udtGwsDetail(ii).bytIP4 _
                Then
                    strGWSold = _
                        udt1.udtSysGws.udtGwsDetail(ii).bytIP1 & "." & _
                        udt1.udtSysGws.udtGwsDetail(ii).bytIP2 & "." & _
                        udt1.udtSysGws.udtGwsDetail(ii).bytIP3 & "." & _
                        udt1.udtSysGws.udtGwsDetail(ii).bytIP4
                    strGWSnew = _
                        udt2.udtSysGws.udtGwsDetail(ii).bytIP1 & "." & _
                        udt2.udtSysGws.udtGwsDetail(ii).bytIP2 & "." & _
                        udt2.udtSysGws.udtGwsDetail(ii).bytIP3 & "." & _
                        udt2.udtSysGws.udtGwsDetail(ii).bytIP4
                    msgSYStemp(i) = mMsgCreateSys("IP", strGWSold, strGWSnew, "GWS", Str(ii + 1))
                    i = i + 1
                End If
                '>>>詳細
                For ij = LBound(udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo) To UBound(udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo)
                    'File Type
                    If udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytType <> udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytType Then
                        msgSYStemp(i) = mMsgCreateSys("File Type", udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytType, udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytType, "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                        i = i + 1
                    End If
                    'Set Flg
                    If udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg <> udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg Then
                        Dim bytSetFlg1 As Byte = udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg
                        Dim bytSetFlg2 As Byte = udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg
                        If gBitCheck(bytSetFlg1, 0) <> gBitCheck(bytSetFlg2, 0) Then
                            msgSYStemp(i) = mMsgCreateSys("Set All Channel", IIf(gBitCheck(bytSetFlg1, 0), "ON", "OFF"), IIf(gBitCheck(bytSetFlg2, 0), "ON", "OFF"), "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                            i = i + 1
                        End If
                        If gBitCheck(bytSetFlg1, 1) <> gBitCheck(bytSetFlg2, 1) Then
                            msgSYStemp(i) = mMsgCreateSys("Set Binary Counter", IIf(gBitCheck(bytSetFlg1, 1), "ON", "OFF"), IIf(gBitCheck(bytSetFlg2, 1), "ON", "OFF"), "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                            i = i + 1
                        End If
                        If gBitCheck(bytSetFlg1, 2) <> gBitCheck(bytSetFlg2, 2) Then
                            msgSYStemp(i) = mMsgCreateSys("Set FLK NONE", IIf(gBitCheck(bytSetFlg1, 2), "ON", "OFF"), IIf(gBitCheck(bytSetFlg2, 2), "ON", "OFF"), "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                            i = i + 1
                        End If
                        If gBitCheck(bytSetFlg1, 3) <> gBitCheck(bytSetFlg2, 3) Then
                            msgSYStemp(i) = mMsgCreateSys("Set Sensor[=S]", IIf(gBitCheck(bytSetFlg1, 3), "ON", "OFF"), IIf(gBitCheck(bytSetFlg2, 3), "ON", "OFF"), "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                            i = i + 1
                        End If

                        'msgSYStemp(i) = mMsgCreateSys("Set", udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg, udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytSetFlg, "GWS" & Str(ii + 1), Str(ij + 1))
                        'i = i + 1
                    End If
                    'Backup Count
                    If udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytBkupCnt <> udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytBkupCnt Then
                        msgSYStemp(i) = mMsgCreateSys("Backup Count", udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytBkupCnt, udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).bytBkupCnt, "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                        i = i + 1
                    End If
                    'Interval
                    If udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).shtInterval <> udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).shtInterval Then
                        msgSYStemp(i) = mMsgCreateSys("Interval", udt1.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).shtInterval, udt2.udtSysGws.udtGwsDetail(ii).udtGwsFileInfo(ij).shtInterval, "GWS" & (ii + 1).ToString & "File", Str(ij + 1))
                        i = i + 1
                    End If
                Next ij

            Next ii
            '-


            '========================
            ''Printer設定
            '========================

            'Ver2.0.0.4 「■■■■　PRINTER　■■■■」行のみの場合は表示しない
            Dim iTemp As Integer = 0
            If i <> 1 Then
                msgSYStemp(i) = "■■■■　PRINTER　■■■■"
                i = i + 1
                iTemp = i   'Ver2.0.0.4
            Else
                msgSYStemp(0) = "■■■■　PRINTER　■■■■"
                iTemp = 1   'Ver2.0.0.4
            End If

            ''自動印字最大数
            If udt1.udtSysPrinter.shtAutoCnt <> udt2.udtSysPrinter.shtAutoCnt Then
                msgSYStemp(i) = mMsgCreateSys("Auto_Print_Events_per_page", udt1.udtSysPrinter.shtAutoCnt, udt2.udtSysPrinter.shtAutoCnt, "", "")
                i = i + 1
            End If

            ''自動印字最大数(Cargo) Ver2.0.8.O  2019.07.04　岩﨑
            If udt1.udtSysPrinter.shtAutoCntCargo <> udt2.udtSysPrinter.shtAutoCntCargo Then
                msgSYStemp(i) = mMsgCreateSys("Auto_Print_Events_per_page(Cargo)", udt1.udtSysPrinter.shtAutoCntCargo, udt2.udtSysPrinter.shtAutoCntCargo, "", "")
                i = i + 1
            End If

            ''英数・日本語設定
            If udt1.udtSysPrinter.shtPrintType <> udt2.udtSysPrinter.shtPrintType Then
                msgSYStemp(i) = mMsgCreateSys("print_type", udt1.udtSysPrinter.shtPrintType, udt2.udtSysPrinter.shtPrintType, "", "")
                i = i + 1
            End If

            ''イベントプリント
            If udt1.udtSysPrinter.shtEventPrint <> udt2.udtSysPrinter.shtEventPrint Then
                msgSYStemp(i) = mMsgCreateSys("event_print", udt1.udtSysPrinter.shtEventPrint, udt2.udtSysPrinter.shtEventPrint, "", "")
                i = i + 1
            End If

            ''ヌーンログ下線
            If udt1.udtSysPrinter.shtNoonUnder <> udt2.udtSysPrinter.shtNoonUnder Then
                msgSYStemp(i) = mMsgCreateSys("noon_under", udt1.udtSysPrinter.shtNoonUnder, udt2.udtSysPrinter.shtNoonUnder, "", "")
                i = i + 1
            End If

            ''デマンドログ改ページ
            If udt1.udtSysPrinter.shtDemandPage <> udt2.udtSysPrinter.shtDemandPage Then
                msgSYStemp(i) = mMsgCreateSys("demand_page", udt1.udtSysPrinter.shtDemandPage, udt2.udtSysPrinter.shtDemandPage, "", "")
                i = i + 1
            End If

            ''Machinery/Cargo印字
            If udt1.udtSysPrinter.shtMachineryCargoPrint <> udt2.udtSysPrinter.shtMachineryCargoPrint Then
                msgSYStemp(i) = mMsgCreateSys("ec_print", udt1.udtSysPrinter.shtMachineryCargoPrint, udt2.udtSysPrinter.shtMachineryCargoPrint, "", "")
                i = i + 1
            End If

            ''詳細設定
            For ix As Integer = LBound(udt1.udtSysPrinter.udtPrinterDetail) To UBound(udt1.udtSysPrinter.udtPrinterDetail)
                'Ver2.0.2.9 プリンタの設定を名称表記するための一時コンボ設定
                Dim strPrinterName As String = ""
                Select Case ix
                    Case 0
                        strPrinterName = "(LogPrinter1)"
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterLogPrinter1)
                    Case 1
                        strPrinterName = "(LogPrinter2)"
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterLogPrinter2)
                    Case 2
                        strPrinterName = "(AlarmPrinter1)"
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterAlarmPrinter1)
                    Case 3
                        strPrinterName = "(AlarmPrinter2)"
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterAlarmPrinter2)
                    Case 4
                        strPrinterName = "(HCPrinter)"
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterHcPrinter)
                    Case 5
                        '予備のためLogPrinter1と仮にしておく
                        Call gSetComboBox(cmbDataChk, gEnmComboType.ctSysPrinterLogPrinter1)
                End Select


                ''プリンタ有無
                If udt1.udtSysPrinter.udtPrinterDetail(ix).bytPrinter <> udt2.udtSysPrinter.udtPrinterDetail(ix).bytPrinter Then
                    'OLD
                    cmbDataChk.SelectedValue = udt1.udtSysPrinter.udtPrinterDetail(ix).bytPrinter
                    Dim strOldData As String = cmbDataChk.Text
                    'NEW
                    cmbDataChk.SelectedValue = udt2.udtSysPrinter.udtPrinterDetail(ix).bytPrinter
                    Dim strNewData As String = cmbDataChk.Text
                    'msgSYStemp(i) = mMsgCreateSys("printer", udt1.udtSysPrinter.udtPrinterDetail(ix).bytPrinter, udt2.udtSysPrinter.udtPrinterDetail(ix).bytPrinter, "PRINTER", Str(ix + 1))
                    msgSYStemp(i) = mMsgCreateSys("printer", strOldData, strNewData, strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''印字有無（通常）
                If gBitCheck(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 0) <> gBitCheck(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("print_use", gBitValue(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 0), gBitValue(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 0), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''印字有無（バックアップ）
                If gBitCheck(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 1) <> gBitCheck(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("print_use(backup)", gBitValue(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 1), gBitValue(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 1), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''Paper Size A3
                If gBitCheck(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 2) <> gBitCheck(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 2) Then
                    msgSYStemp(i) = mMsgCreateSys("page_A3", gBitValue(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 2), gBitValue(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPrintUse, 2), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''印字パート（Machinery）
                If gBitCheck(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPart, 0) <> gBitCheck(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPart, 0) Then
                    msgSYStemp(i) = mMsgCreateSys("print_part(engine)", gBitValue(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPart, 0), gBitValue(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPart, 0), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''印字パート（Cargo）
                If gBitCheck(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPart, 1) <> gBitCheck(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPart, 1) Then
                    msgSYStemp(i) = mMsgCreateSys("print_part(cargo)", gBitValue(udt1.udtSysPrinter.udtPrinterDetail(ix).shtPart, 1), gBitValue(udt2.udtSysPrinter.udtPrinterDetail(ix).shtPart, 1), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''ドライバ
                If Not gCompareString(udt1.udtSysPrinter.udtPrinterDetail(ix).strDriver, udt2.udtSysPrinter.udtPrinterDetail(ix).strDriver) Then
                    msgSYStemp(i) = mMsgCreateSys("driver", gGetString(udt1.udtSysPrinter.udtPrinterDetail(ix).strDriver), gGetString(udt2.udtSysPrinter.udtPrinterDetail(ix).strDriver), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''デバイス
                If Not gCompareString(udt1.udtSysPrinter.udtPrinterDetail(ix).strDevice, udt2.udtSysPrinter.udtPrinterDetail(ix).strDevice) Then
                    msgSYStemp(i) = mMsgCreateSys("device", gGetString(udt1.udtSysPrinter.udtPrinterDetail(ix).strDevice), gGetString(udt2.udtSysPrinter.udtPrinterDetail(ix).strDevice), strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''IPアドレス1
                If udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP1 <> udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP1 Then
                    msgSYStemp(i) = mMsgCreateSys("ip1", udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP1, udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP1, strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''IPアドレス2
                If udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP2 <> udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP2 Then
                    msgSYStemp(i) = mMsgCreateSys("ip2", udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP2, udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP2, strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''IPアドレス3
                If udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP3 <> udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP3 Then
                    msgSYStemp(i) = mMsgCreateSys("ip3", udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP3, udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP3, strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

                ''IPアドレス4
                If udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP4 <> udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP4 Then
                    msgSYStemp(i) = mMsgCreateSys("ip4", udt1.udtSysPrinter.udtPrinterDetail(ix).bytIP4, udt2.udtSysPrinter.udtPrinterDetail(ix).bytIP4, strPrinterName & "PRINTER", Str(ix + 1))
                    i = i + 1
                End If

            Next

            'Ver2.0.0.4 「■■■■　PRINTER　■■■■」行のみの場合は表示しない
            If i = iTemp Then
                '「■■■■　PRINTER　■■■■」行のみの場合は表示しない
                msgSYStemp(iTemp - 1) = ""
            End If

            '何も変更がない場合は表示しない
            If i <> 1 Then
                Call mMsgSysGrid(i)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定（外部機器VDR情報設定）OK"
    Dim prI As Integer = 0
    Friend Function mCompareSetChSio(ByVal udt1 As gTypSetChSio, _
                                     ByVal udt2 As gTypSetChSio) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　SIO VDR INFORMATION SETTING　■■■■"

            ''チャンネル設定数レコード
            For i As Integer = LBound(udt1.shtNum) To UBound(udt1.shtNum)

                ''チャンネル設定数
                If udt1.shtNum(i) <> udt2.shtNum(i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("NUM", udt1.shtNum(i), udt2.shtNum(i), "NUM", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            ''VDR情報
            For i As Integer = LBound(udt1.udtVdr) To UBound(udt1.udtVdr)

                ''使用有無
                If udt1.udtVdr(i).shtPort <> udt2.udtVdr(i).shtPort Then
                    msgSYStemp(ix) = mMsgCreateSys1("PORT", udt1.udtVdr(i).shtPort, udt2.udtVdr(i).shtPort, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''外部機器識別子
                If udt1.udtVdr(i).shtExtComID <> udt2.udtVdr(i).shtExtComID Then
                    msgSYStemp(ix) = mMsgCreateSys1("EXTCOM ID", "0x" & Hex(udt1.udtVdr(i).shtExtComID).PadLeft(2, "0"), "0x" & Hex(udt2.udtVdr(i).shtExtComID).PadLeft(2, "0"), "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''優先度
                If udt1.udtVdr(i).shtPriority <> udt2.udtVdr(i).shtPriority Then
                    msgSYStemp(ix) = mMsgCreateSys1("PRIORITY", udt1.udtVdr(i).shtPriority, udt2.udtVdr(i).shtPriority, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''M/C設定
                If udt1.udtVdr(i).shtSysno <> udt2.udtVdr(i).shtSysno Then
                    msgSYStemp(ix) = mMsgCreateSys1("M/C SET", udt1.udtVdr(i).shtSysno, udt2.udtVdr(i).shtSysno, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''通信種類１
                If udt1.udtVdr(i).shtCommType1 <> udt2.udtVdr(i).shtCommType1 Then
                    msgSYStemp(ix) = mMsgCreateSys1("COMM TYPE1", udt1.udtVdr(i).shtCommType1, udt2.udtVdr(i).shtCommType1, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''通信種類２
                If udt1.udtVdr(i).shtCommType2 <> udt2.udtVdr(i).shtCommType2 Then
                    msgSYStemp(ix) = mMsgCreateSys1("COMM TYPE2", "0x" & Hex(udt1.udtVdr(i).shtCommType2).PadLeft(2, "0"), "0x" & Hex(udt2.udtVdr(i).shtCommType2).PadLeft(2, "0"), "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''回線種類
                If udt1.udtVdr(i).udtCommInf.shtComm <> udt2.udtVdr(i).udtCommInf.shtComm Then
                    msgSYStemp(ix) = mMsgCreateSys1("COMM", udt1.udtVdr(i).udtCommInf.shtComm, udt2.udtVdr(i).udtCommInf.shtComm, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''データビット
                If udt1.udtVdr(i).udtCommInf.shtDataBit <> udt2.udtVdr(i).udtCommInf.shtDataBit Then
                    msgSYStemp(ix) = mMsgCreateSys1("LENGTH(DATABIT)", udt1.udtVdr(i).udtCommInf.shtDataBit, udt2.udtVdr(i).udtCommInf.shtDataBit, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''パリティ
                If udt1.udtVdr(i).udtCommInf.shtParity <> udt2.udtVdr(i).udtCommInf.shtParity Then
                    msgSYStemp(ix) = mMsgCreateSys1("PARITY", udt1.udtVdr(i).udtCommInf.shtParity, udt2.udtVdr(i).udtCommInf.shtParity, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''ストップビット
                If udt1.udtVdr(i).udtCommInf.shtStop <> udt2.udtVdr(i).udtCommInf.shtStop Then
                    msgSYStemp(ix) = mMsgCreateSys1("STOP", udt1.udtVdr(i).udtCommInf.shtStop, udt2.udtVdr(i).udtCommInf.shtStop, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''ボーレート
                If udt1.udtVdr(i).udtCommInf.shtComBps <> udt2.udtVdr(i).udtCommInf.shtComBps Then
                    msgSYStemp(ix) = mMsgCreateSys1("BAUDRATE", udt1.udtVdr(i).udtCommInf.shtComBps, udt2.udtVdr(i).udtCommInf.shtComBps, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''受信タイムアウト（秒）起動時
                If udt1.udtVdr(i).shtReceiveInit <> udt2.udtVdr(i).shtReceiveInit Then
                    msgSYStemp(ix) = mMsgCreateSys1("TIMEOUT", udt1.udtVdr(i).shtReceiveInit, udt2.udtVdr(i).shtReceiveInit, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''受信タイムアウト（秒）起動後
                If udt1.udtVdr(i).shtReceiveUseally <> udt2.udtVdr(i).shtReceiveUseally Then
                    msgSYStemp(ix) = mMsgCreateSys1("TIMEOUT USUALLY", udt1.udtVdr(i).shtReceiveUseally, udt2.udtVdr(i).shtReceiveUseally, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''送信間隔（秒）起動時
                If udt1.udtVdr(i).shtSendInit <> udt2.udtVdr(i).shtSendInit Then
                    msgSYStemp(ix) = mMsgCreateSys1("INTERVAL INIT", udt1.udtVdr(i).shtSendInit, udt2.udtVdr(i).shtSendInit, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''送信間隔（秒）起動後
                If udt1.udtVdr(i).shtSendUseally <> udt2.udtVdr(i).shtSendUseally Then
                    msgSYStemp(ix) = mMsgCreateSys1("INTERVAL USUALLY", udt1.udtVdr(i).shtSendUseally, udt2.udtVdr(i).shtSendUseally, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''リトライ回数
                If udt1.udtVdr(i).shtRetry <> udt2.udtVdr(i).shtRetry Then
                    msgSYStemp(ix) = mMsgCreateSys1("RETRY", udt1.udtVdr(i).shtRetry, udt2.udtVdr(i).shtRetry, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Duplex設定
                If udt1.udtVdr(i).shtDuplexSet <> udt2.udtVdr(i).shtDuplexSet Then
                    msgSYStemp(ix) = mMsgCreateSys1("DUPLEXSET", udt1.udtVdr(i).shtDuplexSet, udt2.udtVdr(i).shtDuplexSet, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''送信CH
                If udt1.udtVdr(i).shtSendCH <> udt2.udtVdr(i).shtSendCH Then
                    msgSYStemp(ix) = mMsgCreateSys1("NUMBER CH", udt1.udtVdr(i).shtSendCH, udt2.udtVdr(i).shtSendCH, "INF PORT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''ノード情報
                For j As Integer = LBound(udt1.udtVdr(i).udtNode) To UBound(udt1.udtVdr(i).udtNode)

                    ''使用有無
                    If udt1.udtVdr(i).udtNode(j).shtCheck <> udt2.udtVdr(i).udtNode(j).shtCheck Then
                        msgSYStemp(ix) = mMsgCreateSys1("CHECK", udt1.udtVdr(i).udtNode(j).shtCheck, udt2.udtVdr(i).udtNode(j).shtCheck, "INF PORT", Str(i + 1), "NODE", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''アドレス
                    If udt1.udtVdr(i).udtNode(j).shtAddress <> udt2.udtVdr(i).udtNode(j).shtAddress Then
                        msgSYStemp(ix) = mMsgCreateSys1("ADDRESS", "0x" & Hex(udt1.udtVdr(i).udtNode(j).shtAddress).PadLeft(2, "0"), "0x" & Hex(udt2.udtVdr(i).udtNode(j).shtAddress).PadLeft(2, "0"), "INF PORT", Str(i + 1), "NODE", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''詳細設定データ
                For j As Integer = LBound(udt1.udtVdr(i).bytSetData) To UBound(udt1.udtVdr(i).bytSetData)

                    ''詳細設定データ
                    If udt1.udtVdr(i).bytSetData(j) <> udt2.udtVdr(i).bytSetData(j) Then
                        'Ver2.0.0.5 VDRテーブル添え字をHexへ変更
                        'msgSYStemp(ix) = mMsgCreateSys1("DATA", "0x" & Hex(udt1.udtVdr(i).bytSetData(j)).PadLeft(2, "0"), "0x" & Hex(udt2.udtVdr(i).bytSetData(j)).PadLeft(2, "0"), "INF PORT", Str(i + 1), "SET DATA", Str(j + 1))
                        msgSYStemp(ix) = mMsgCreateSys1("DATA", "0x" & Hex(udt1.udtVdr(i).bytSetData(j)).PadLeft(2, "0"), "0x" & Hex(udt2.udtVdr(i).bytSetData(j)).PadLeft(2, "0"), "INF PORT", Str(i + 1), "SET DATA ", Hex(j + 76))
                        ix = ix + 1
                    End If

                Next

                prI = i
            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定（外部機器VDR情報設定）CH設定データOK"

    Friend Function mCompareSetChSioCh(ByVal udt1() As gTypSetChSioCh, _
                                       ByVal udt2() As gTypSetChSioCh) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　SIO CHANNEL SETTING DATA　■■■■"

            For i As Integer = 0 To UBound(udt1)

                For j As Integer = 0 To UBound(udt1(i).udtSioChRec)

                    ''チャンネル番号
                    If udt1(i).udtSioChRec(j).shtChId <> udt2(i).udtSioChRec(j).shtChId Then
                        msgSYStemp(ix) = mMsgCreateSys1("CHNO", udt1(i).udtSioChRec(j).shtChId, udt2(i).udtSioChRec(j).shtChId, "CHINF", Str(i + 1), "REC", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''チャンネルID
                    If udt1(i).udtSioChRec(j).shtChNo <> udt2(i).udtSioChRec(j).shtChNo Then
                        msgSYStemp(ix) = mMsgCreateSys1("CHID", udt1(i).udtSioChRec(j).shtChNo, udt2(i).udtSioChRec(j).shtChNo, "CHINF", Str(i + 1), "REC", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ設定OK"

    Friend Function mCompareSetTimer(ByVal udt1 As gTypSetExtTimerSet, _
                                     ByVal udt2 As gTypSetExtTimerSet) As Integer

        Try

            Dim i As Integer = 1

            msgSYStemp(0) = "■■■■　TIMER　■■■■"

            For ix As Integer = 0 To UBound(udt1.udtTimerInfo)

                ''種類
                If udt1.udtTimerInfo(ix).shtType <> udt2.udtTimerInfo(ix).shtType Then
                    msgSYStemp(i) = mMsgCreateSys("TYPE", udt1.udtTimerInfo(ix).shtType, udt2.udtTimerInfo(ix).shtType, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''レコード番号
                If udt1.udtTimerInfo(ix).bytIndex <> udt2.udtTimerInfo(ix).bytIndex Then
                    msgSYStemp(i) = mMsgCreateSys("INDEX", udt1.udtTimerInfo(ix).bytIndex, udt2.udtTimerInfo(ix).bytIndex, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''パート
                If udt1.udtTimerInfo(ix).bytPart <> udt2.udtTimerInfo(ix).bytPart Then
                    msgSYStemp(i) = mMsgCreateSys("PART", udt1.udtTimerInfo(ix).bytPart, udt2.udtTimerInfo(ix).bytPart, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''分/秒切替設定
                If udt1.udtTimerInfo(ix).shtTimeDisp <> udt2.udtTimerInfo(ix).shtTimeDisp Then
                    msgSYStemp(i) = mMsgCreateSys("MIN/SEC", udt1.udtTimerInfo(ix).shtTimeDisp, udt2.udtTimerInfo(ix).shtTimeDisp, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''初期値
                If udt1.udtTimerInfo(ix).shtInit <> udt2.udtTimerInfo(ix).shtInit Then
                    msgSYStemp(i) = mMsgCreateSys("INIT", udt1.udtTimerInfo(ix).shtInit, udt2.udtTimerInfo(ix).shtInit, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''下限値
                If udt1.udtTimerInfo(ix).shtLow <> udt2.udtTimerInfo(ix).shtLow Then
                    msgSYStemp(i) = mMsgCreateSys("LOW", udt1.udtTimerInfo(ix).shtLow, udt2.udtTimerInfo(ix).shtLow, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

                ''上限値
                If udt1.udtTimerInfo(ix).shtHigh <> udt2.udtTimerInfo(ix).shtHigh Then
                    msgSYStemp(i) = mMsgCreateSys("HIGH", udt1.udtTimerInfo(ix).shtHigh, udt2.udtTimerInfo(ix).shtHigh, "TIMER", Str(ix + 1))
                    i = i + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If i <> 1 Then
                Call mMsgSysGrid(i)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ表示名称設定OK"

    Friend Function mCompareSetTimerName(ByVal udt1 As gTypSetExtTimerName, _
                                         ByVal udt2 As gTypSetExtTimerName) As Integer

        Try

            Dim i As Integer = 1

            msgSYStemp(0) = "■■■■　TIMER DISPLAY NAME　■■■■"

            For ix As Integer = 0 To UBound(udt1.udtTimerRec)

                ''タイマ表示名称
                If Not gCompareString(udt1.udtTimerRec(ix).strName, udt2.udtTimerRec(ix).strName) Then
                    msgSYStemp(i) = mMsgCreateSys("NAME", gGetString(udt1.udtTimerRec(ix).strName), gGetString(udt2.udtTimerRec(ix).strName), "TIMER_NAME", Str(ix + 1))
                    i = i + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If i <> 1 Then
                Call mMsgSysGrid(i)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "シーケンス設定OK"

    Friend Function mCompareSetSeqSequence(ByVal udtID1 As gTypSetSeqID, ByVal udtSet1 As gTypSetSeqSet, _
                                           ByVal udtID2 As gTypSetSeqID, ByVal udtSet2 As gTypSetSeqSet) As Integer

        Try

            Dim SEQIDNo(2000) As Integer
            Dim z As Integer = 0
            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　SEQUENCE　■■■■"

            ''シーケンスID
            For i As Integer = LBound(udtID1.shtID) To UBound(udtID1.shtID)

                ''数値（レコード）
                If udtID1.shtID(i) <> udtID2.shtID(i) Then

                    msgSYStemp(ix) = mMsgCreateSEQ("ID", udtID1.shtID(i), udtID2.shtID(i), "SEQ_USE", Str(i + 1), "", "")
                    ix = ix + 1

                    SEQIDNo(z) = i
                    z = z + 1
                End If
            Next

            For i As Integer = 0 To UBound(udtSet1.udtDetail)

                'Ver2.0.1.5 比較でSEQIDNo(z)では、絶対フローされないので削除
                'シーケンスIDの使用/未使用が変更されている場合は比較しない
                'If i = SEQIDNo(z) Then

                ''シーケンスＩＤ
                If udtSet1.udtDetail(i).shtId <> udtSet2.udtDetail(i).shtId Then
                    msgSYStemp(ix) = mMsgCreateSEQ("ID", udtSet1.udtDetail(i).shtId, udtSet2.udtDetail(i).shtId, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力ロジックタイプ
                If udtSet1.udtDetail(i).shtLogicType <> udtSet2.udtDetail(i).shtLogicType Then
                    msgSYStemp(ix) = mMsgCreateSEQ("LOGIC TYPE", udtSet1.udtDetail(i).shtLogicType, udtSet2.udtDetail(i).shtLogicType, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''入力CH情報
                For j As Integer = LBound(udtSet1.udtDetail(i).udtInput) To UBound(udtSet1.udtDetail(i).udtInput)

                    ''SYSTEM No.
                    If udtSet1.udtDetail(i).udtInput(j).shtSysno <> udtSet2.udtDetail(i).udtInput(j).shtSysno Then
                        msgSYStemp(ix) = mMsgCreateSEQ("SYSNO", udtSet1.udtDetail(i).udtInput(j).shtSysno, udtSet2.udtDetail(i).udtInput(j).shtSysno, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''CH ID
                    If udtSet1.udtDetail(i).udtInput(j).shtChid <> udtSet2.udtDetail(i).udtInput(j).shtChid Then
                        msgSYStemp(ix) = mMsgCreateSEQ("CHID or SEQID", udtSet1.udtDetail(i).udtInput(j).shtChid, udtSet2.udtDetail(i).udtInput(j).shtChid, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''CH選択
                    If udtSet1.udtDetail(i).udtInput(j).shtChSelect <> udtSet2.udtDetail(i).udtInput(j).shtChSelect Then
                        msgSYStemp(ix) = mMsgCreateSEQ("CH_SEL", udtSet1.udtDetail(i).udtInput(j).shtChSelect, udtSet2.udtDetail(i).udtInput(j).shtChSelect, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''入出力区分
                    If udtSet1.udtDetail(i).udtInput(j).shtIoSelect <> udtSet2.udtDetail(i).udtInput(j).shtIoSelect Then
                        msgSYStemp(ix) = mMsgCreateSEQ("IO_SEL", udtSet1.udtDetail(i).udtInput(j).shtIoSelect, udtSet2.udtDetail(i).udtInput(j).shtIoSelect, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''ステータス
                    If udtSet1.udtDetail(i).udtInput(j).bytStatus <> udtSet2.udtDetail(i).udtInput(j).bytStatus Then
                        msgSYStemp(ix) = mMsgCreateSEQ("STATUS", "0x" & Hex(udtSet1.udtDetail(i).udtInput(j).bytStatus).PadLeft(2, "0"), "0x" & Hex(udtSet2.udtDetail(i).udtInput(j).bytStatus).PadLeft(2, "0"), "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''タイプ
                    If udtSet1.udtDetail(i).udtInput(j).bytType <> udtSet2.udtDetail(i).udtInput(j).bytType Then
                        msgSYStemp(ix) = mMsgCreateSEQ("TYPE", udtSet1.udtDetail(i).udtInput(j).bytType, udtSet2.udtDetail(i).udtInput(j).bytType, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''マスク値
                    If udtSet1.udtDetail(i).udtInput(j).shtMask <> udtSet2.udtDetail(i).udtInput(j).shtMask Then
                        msgSYStemp(ix) = mMsgCreateSEQ("MASK", "0x" & Hex(udtSet1.udtDetail(i).udtInput(j).shtMask).PadLeft(4, "0"), "0x" & Hex(udtSet2.udtDetail(i).udtInput(j).shtMask).PadLeft(4, "0"), "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''アナログ入力種別
                    If udtSet1.udtDetail(i).udtInput(j).shtAnalogType <> udtSet2.udtDetail(i).udtInput(j).shtAnalogType Then
                        msgSYStemp(ix) = mMsgCreateSEQ("AN_TYPE", udtSet1.udtDetail(i).udtInput(j).shtAnalogType, udtSet2.udtDetail(i).udtInput(j).shtAnalogType, "SEQ", Str(i + 1), "input", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''備考
                If Not gCompareString(udtSet1.udtDetail(i).strRemarks, udtSet2.udtDetail(i).strRemarks) Then
                    msgSYStemp(ix) = mMsgCreateSEQ("REMARKS", gGetString(udtSet1.udtDetail(i).strRemarks), gGetString(udtSet2.udtDetail(i).strRemarks), "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''演算参照テーブル
                For j As Integer = LBound(udtSet1.udtDetail(i)._shtLogicItem) To UBound(udtSet1.udtDetail(i)._shtLogicItem)

                    ''演算参照テーブル
                    If udtSet1.udtDetail(i)._shtLogicItem(j) <> udtSet2.udtDetail(i)._shtLogicItem(j) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("TABLE", udtSet1.udtDetail(i)._shtLogicItem(j), udtSet2.udtDetail(i)._shtLogicItem(j), "SEQ", Str(i + 1), "CAL_TBL", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''チャンネル使用有無
                For j As Integer = LBound(udtSet1.udtDetail(i).shtUseCh) To UBound(udtSet1.udtDetail(i).shtUseCh)

                    ''演算参照テーブル使用有無
                    If udtSet1.udtDetail(i).shtUseCh(j) <> udtSet2.udtDetail(i).shtUseCh(j) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("USE", udtSet1.udtDetail(i)._shtLogicItem(j), udtSet2.udtDetail(i)._shtLogicItem(j), "SEQ", Str(i + 1), "CAL_TBL", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''出力システムNo
                If udtSet1.udtDetail(i).shtOutSysno <> udtSet2.udtDetail(i).shtOutSysno Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_SYSNO", udtSet1.udtDetail(i).shtOutSysno, udtSet2.udtDetail(i).shtOutSysno, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力チャンネル番号
                If udtSet1.udtDetail(i).shtOutChid <> udtSet2.udtDetail(i).shtOutChid Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_CHID", udtSet1.udtDetail(i).shtOutChid, udtSet2.udtDetail(i).shtOutChid, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力データ
                If udtSet1.udtDetail(i).shtOutData <> udtSet2.udtDetail(i).shtOutData Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_DATA", "0x" & Hex(udtSet1.udtDetail(i).shtOutData).PadLeft(2, "0"), "0x" & Hex(udtSet2.udtDetail(i).shtOutData).PadLeft(2, "0"), "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力オフディレイ
                If udtSet1.udtDetail(i).shtOutData <> udtSet2.udtDetail(i).shtOutData Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_DERAY", udtSet1.udtDetail(i).shtOutData, udtSet2.udtDetail(i).shtOutData, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力ステータス
                If udtSet1.udtDetail(i).bytOutStatus <> udtSet2.udtDetail(i).bytOutStatus Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_STATUS", "0x" & Hex(udtSet1.udtDetail(i).bytOutStatus).PadLeft(2, "0"), "0x" & Hex(udtSet2.udtDetail(i).bytOutStatus).PadLeft(2, "0"), "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''入出力区分
                If udtSet1.udtDetail(i).bytOutIoSelect <> udtSet2.udtDetail(i).bytOutIoSelect Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_IO_SEL", udtSet1.udtDetail(i).bytOutIoSelect, udtSet2.udtDetail(i).bytOutIoSelect, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力データタイプ
                If udtSet1.udtDetail(i).bytOutDataType <> udtSet2.udtDetail(i).bytOutDataType Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_DTYPE", "0x" & Hex(udtSet1.udtDetail(i).bytOutDataType).PadLeft(2, "0"), "0x" & Hex(udtSet2.udtDetail(i).bytOutDataType).PadLeft(2, "0"), "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力反転
                If udtSet1.udtDetail(i).bytOutInv <> udtSet2.udtDetail(i).bytOutInv Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_INV", udtSet1.udtDetail(i).bytOutInv, udtSet2.udtDetail(i).bytOutInv, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FU番号
                If udtSet1.udtDetail(i).bytFuno <> udtSet2.udtDetail(i).bytFuno Then
                    msgSYStemp(ix) = mMsgCreateSEQ("FUNO", udtSet1.udtDetail(i).bytFuno, udtSet2.udtDetail(i).bytFuno, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FUポート番号
                If udtSet1.udtDetail(i).bytPort <> udtSet2.udtDetail(i).bytPort Then
                    msgSYStemp(ix) = mMsgCreateSEQ("PORT", udtSet1.udtDetail(i).bytPort, udtSet2.udtDetail(i).bytPort, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FU計測点位置
                If udtSet1.udtDetail(i).bytPin <> udtSet2.udtDetail(i).bytPin Then
                    msgSYStemp(ix) = mMsgCreateSEQ("PIN", udtSet1.udtDetail(i).bytPin, udtSet2.udtDetail(i).bytPin, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FU計測点個数
                If udtSet1.udtDetail(i).bytPinNo <> udtSet2.udtDetail(i).bytPinNo Then
                    msgSYStemp(ix) = mMsgCreateSEQ("PINNO", udtSet1.udtDetail(i).bytPinNo, udtSet2.udtDetail(i).bytPinNo, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力タイプ
                If udtSet1.udtDetail(i).bytOutType <> udtSet2.udtDetail(i).bytOutType Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_TYPE", "0x" & Hex(udtSet1.udtDetail(i).bytOutType).PadLeft(2, "0"), "0x" & Hex(udtSet2.udtDetail(i).bytOutType).PadLeft(2, "0"), "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''出力ワンショット時間
                If udtSet1.udtDetail(i).bytOneShot <> udtSet2.udtDetail(i).bytOneShot Then
                    msgSYStemp(ix) = mMsgCreateSEQ("OUT_SHOT", udtSet1.udtDetail(i).bytOneShot, udtSet2.udtDetail(i).bytOneShot, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''処理継続中止
                If udtSet1.udtDetail(i).bytContine <> udtSet2.udtDetail(i).bytContine Then
                    msgSYStemp(ix) = mMsgCreateSEQ("SEQ_CONTINE", udtSet1.udtDetail(i).bytContine, udtSet2.udtDetail(i).bytContine, "SEQ", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                'End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

#Region "リニアライズテーブルOK"

    Friend Function mCompareSetSeqLinearTable(ByVal udt1 As gTypSetSeqLinear, _
                                              ByVal udt2 As gTypSetSeqLinear) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　LINEAR TABLE　■■■■"

            For i As Integer = LBound(udt1.udtPoints) To UBound(udt1.udtPoints)

                ''ポイント数
                If udt1.udtPoints(i).shtPoint <> udt2.udtPoints(i).shtPoint Then
                    msgSYStemp(ix) = mMsgCreateSEQ("POINT", udt1.udtPoints(i).shtPoint, udt2.udtPoints(i).shtPoint, "POINT", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            For i As Integer = LBound(udt1.udtTables) To UBound(udt1.udtTables)

                For j As Integer = LBound(udt1.udtTables(i).udtRow) To UBound(udt1.udtTables(i).udtRow)

                    ''X値
                    If udt1.udtTables(i).udtRow(j).sngPtX <> udt2.udtTables(i).udtRow(j).sngPtX Then
                        msgSYStemp(ix) = mMsgCreateSEQ("X", udt1.udtTables(i).udtRow(j).sngPtX, udt2.udtTables(i).udtRow(j).sngPtX, "LTABLE", Str(i + 1), "ROW", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''Y値
                    If udt1.udtTables(i).udtRow(j).sngPtY <> udt2.udtTables(i).udtRow(j).sngPtY Then
                        msgSYStemp(ix) = mMsgCreateSEQ("Y", udt1.udtTables(i).udtRow(j).sngPtY, udt2.udtTables(i).udtRow(j).sngPtY, "LTABLE", Str(i + 1), "ROW", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "演算式テーブルOK"

    Friend Function mCompareSetSeqOperationExpression(ByVal udt1 As gTypSetSeqOperationExpression, _
                                                      ByVal udt2 As gTypSetSeqOperationExpression) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　SEQ OPE EXPRESSION TABLE　■■■■"

            ''詳細初期化
            For i As Integer = LBound(udt1.udtTables) To UBound(udt1.udtTables)

                ''演算式
                If Not gCompareString(udt1.udtTables(i).strExp, udt2.udtTables(i).strExp) Then
                    msgSYStemp(ix) = mMsgCreateSEQ("EXP", gGetString(udt1.udtTables(i).strExp), gGetString(udt2.udtTables(i).strExp), "TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''VariableName
                For j As Integer = LBound(udt1.udtTables(i).strVariavleName) To UBound(udt1.udtTables(i).strVariavleName)

                    ''VariableName
                    If Not gCompareString(udt1.udtTables(i).strVariavleName(j), udt2.udtTables(i).strVariavleName(j)) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("VAR NAME", gGetString(udt1.udtTables(i).strVariavleName(j)), gGetString(udt2.udtTables(i).strVariavleName(j)), "TABLE", Str(i + 1), "VAR NAME", Str(j + 1))
                        ix = ix + 1
                    End If


                Next

                ''AryInf初期化
                For j As Integer = LBound(udt1.udtTables(i).udtAryInf) To UBound(udt1.udtTables(i).udtAryInf)

                    ''定数種類
                    If udt1.udtTables(i).udtAryInf(j).shtType <> udt2.udtTables(i).udtAryInf(j).shtType Then
                        msgSYStemp(ix) = mMsgCreateSEQ("TYPE", udt1.udtTables(i).udtAryInf(j).shtType, udt2.udtTables(i).udtAryInf(j).shtType, "TABLE", Str(i + 1), "ROW", Str(j + 1))
                        ix = ix + 1
                    Else

                        Dim intSysNo1 As Integer
                        Dim intSysNo2 As Integer
                        Dim intChNo1 As Integer
                        Dim intChNo2 As Integer
                        Dim sngValue1 As Integer
                        Dim sngValue2 As Integer
                        Dim lngValue1 As Integer
                        Dim lngValue2 As Integer

                        Select Case udt1.udtTables(i).udtAryInf(j).shtType
                            Case gCstCodeSeqFixTypeChData, _
                                 gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                                 gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                                ''システムNo1
                                intSysNo1 = gConnect2Byte(udt1.udtTables(i).udtAryInf(j).bytInfo(0), _
                                                          udt1.udtTables(i).udtAryInf(j).bytInfo(1))
                                ''チャンネル番号1
                                intChNo1 = gConnect2Byte(udt1.udtTables(i).udtAryInf(j).bytInfo(2), _
                                                         udt1.udtTables(i).udtAryInf(j).bytInfo(3))

                                ''システムNo2
                                intSysNo2 = gConnect2Byte(udt2.udtTables(i).udtAryInf(j).bytInfo(0), _
                                                          udt2.udtTables(i).udtAryInf(j).bytInfo(1))
                                ''チャンネル番号2
                                intChNo2 = gConnect2Byte(udt2.udtTables(i).udtAryInf(j).bytInfo(2), _
                                                         udt2.udtTables(i).udtAryInf(j).bytInfo(3))

                                ''システムNo
                                If intSysNo1 <> intSysNo2 Then
                                    msgSYStemp(ix) = mMsgCreateSEQ("SYSNo", intSysNo1, intSysNo2, "TABLE", Str(i + 1), "ROW", Str(j + 1))
                                    ix = ix + 1
                                End If

                                ''チャンネル番号
                                If intChNo1 <> intChNo2 Then
                                    msgSYStemp(ix) = mMsgCreateSEQ("CHNo", intChNo1, intChNo2, "TABLE", Str(i + 1), "ROW", Str(j + 1))
                                    ix = ix + 1
                                End If


                            Case gCstCodeSeqFixTypeFixFloat

                                sngValue1 = BitConverter.ToSingle(udt1.udtTables(i).udtAryInf(j).bytInfo, 0)
                                sngValue2 = BitConverter.ToSingle(udt2.udtTables(i).udtAryInf(j).bytInfo, 0)

                                ''定数
                                If sngValue1 <> sngValue2 Then
                                    msgSYStemp(ix) = mMsgCreateSEQ("VALUE", sngValue1, sngValue2, "TABLE", Str(i + 1), "ROW", Str(j + 1))
                                    ix = ix + 1
                                End If

                            Case gCstCodeSeqFixTypeFixLong

                                lngValue1 = BitConverter.ToInt16(udt1.udtTables(i).udtAryInf(j).bytInfo, 0) ''返り値の型を変更　2.0.8.H 2019.02.05
                                lngValue2 = BitConverter.ToInt16(udt2.udtTables(i).udtAryInf(j).bytInfo, 0) ''返り値の型を変更　2.0.8.H 2019.02.05
                                
                                ''定数
                                If lngValue1 <> lngValue2 Then
                                    msgSYStemp(ix) = mMsgCreateSEQ("VALUE", lngValue1, lngValue2, "TABLE", Str(i + 1), "ROW", Str(j + 1))
                                    ix = ix + 1
                                End If
                        End Select
                    End If
                Next
            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPS画面タイトルOK"

    Friend Function mCompareSetOpsDisp(ByVal udt1 As gTypSetOpsScreenTitle, _
                                       ByVal udt2 As gTypSetOpsScreenTitle, _
                                       ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　OPS TITLE　■■■■"

            For i As Integer = LBound(udt1.udtOpsScreenTitle) To UBound(udt1.udtOpsScreenTitle)

                ''画面番号
                If udt1.udtOpsScreenTitle(i).bytScreenNo <> udt2.udtOpsScreenTitle(i).bytScreenNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("No", udt1.udtOpsScreenTitle(i).bytScreenNo, udt2.udtOpsScreenTitle(i).bytScreenNo, "SCREEN", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''画面名称
                If Not gCompareString(udt1.udtOpsScreenTitle(i).strScreenName, udt2.udtOpsScreenTitle(i).strScreenName) Then
                    msgSYStemp(ix) = mMsgCreateOPS("NAME", gGetString(udt1.udtOpsScreenTitle(i).strScreenName), gGetString(udt2.udtOpsScreenTitle(i).strScreenName), "SCREEN", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "プルダウンメニューOK"

    Friend Function mCompareSetOpsPulldownMenu(ByVal udt1 As gTypSetOpsPulldownMenu, _
                                               ByVal udt2 As gTypSetOpsPulldownMenu, _
                                               ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　OPS PULLDOWN MENU（" & mGetMCEng_MENU(udtMC) & "） ■■■■"

            ''メインメニュー設定
            For i = 0 To UBound(udt1.udtDetail)

                ''メニュー名称
                If Not gCompareString(udt1.udtDetail(i).strName, udt2.udtDetail(i).strName) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MANU_NAME", gGetString(udt1.udtDetail(i).strName), gGetString(udt2.udtDetail(i).strName), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''メインメニューの動作開始地点（左上X座標）
                If Not gCompareString(udt1.udtDetail(i).tx, udt2.udtDetail(i).tx) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MainMenu LeftUp X", gGetString(udt1.udtDetail(i).tx), gGetString(udt2.udtDetail(i).tx), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''メインメニューの動作開始地点（左上Y座標)
                If Not gCompareString(udt1.udtDetail(i).ty, udt2.udtDetail(i).ty) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MainMenu LeftUp Y", gGetString(udt1.udtDetail(i).ty), gGetString(udt2.udtDetail(i).ty), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''メインメニューの動作開始地点（右下X座標）
                If Not gCompareString(udt1.udtDetail(i).bx, udt2.udtDetail(i).bx) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MainMenu RightDown X", gGetString(udt1.udtDetail(i).bx), gGetString(udt2.udtDetail(i).bx), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''メインメニューの動作開始地点（右下Y座標）
                If Not gCompareString(udt1.udtDetail(i).by, udt2.udtDetail(i).by) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MainMenu RightDown Y", gGetString(udt1.udtDetail(i).by), gGetString(udt2.udtDetail(i).by), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''OPS禁止フラグ
                If Not gCompareString(udt1.udtDetail(i).OPSSTFLG1, udt2.udtDetail(i).OPSSTFLG1) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("OPS ST FLG1", gGetString(udt1.udtDetail(i).OPSSTFLG1), gGetString(udt2.udtDetail(i).OPSSTFLG1), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                If Not gCompareString(udt1.udtDetail(i).OPSSTFLG2, udt2.udtDetail(i).OPSSTFLG2) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("OPS ST FLG2", gGetString(udt1.udtDetail(i).OPSSTFLG2), gGetString(udt2.udtDetail(i).OPSSTFLG2), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''予備
                If Not gCompareString(udt1.udtDetail(i).Spare1, udt2.udtDetail(i).Spare1) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("SP1", gGetString(udt1.udtDetail(i).Spare1), gGetString(udt2.udtDetail(i).Spare1), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                If Not gCompareString(udt1.udtDetail(i).Spare2, udt2.udtDetail(i).Spare2) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("SP2", gGetString(udt1.udtDetail(i).Spare2), gGetString(udt2.udtDetail(i).Spare2), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                If Not gCompareString(udt1.udtDetail(i).Spare3, udt2.udtDetail(i).Spare3) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("SP3", gGetString(udt1.udtDetail(i).Spare3), gGetString(udt2.udtDetail(i).Spare3), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                If Not gCompareString(udt1.udtDetail(i).Spare4, udt2.udtDetail(i).Spare4) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("SP4", gGetString(udt1.udtDetail(i).Spare4), gGetString(udt2.udtDetail(i).Spare4), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                If Not gCompareString(udt1.udtDetail(i).Spare5, udt2.udtDetail(i).Spare5) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("SP5", gGetString(udt1.udtDetail(i).Spare5), gGetString(udt2.udtDetail(i).Spare5), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''メニュータイプ
                If Not gCompareString(udt1.udtDetail(i).bytMenuType, udt2.udtDetail(i).bytMenuType) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("MenuType", gGetString(udt1.udtDetail(i).bytMenuType), gGetString(udt2.udtDetail(i).bytMenuType), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''セレクトされているグループメニュー番号(未使用)
                If Not gCompareString(udt1.udtDetail(i).Yobi1, udt2.udtDetail(i).Yobi1) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("Selected GroupMenuNo(NotUse)", gGetString(udt1.udtDetail(i).Yobi1), gGetString(udt2.udtDetail(i).Yobi1), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''セレクトされているグループメニュー番号(保持型)(未使用)
                If Not gCompareString(udt1.udtDetail(i).Yobi2, udt2.udtDetail(i).Yobi2) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("Selected GroupMenuNo(StayType)(NotUse)", gGetString(udt1.udtDetail(i).Yobi2), gGetString(udt2.udtDetail(i).Yobi2), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グループメニューセット数
                If Not gCompareString(udt1.udtDetail(i).bytMenuSet, udt2.udtDetail(i).bytMenuSet) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu Set Count", gGetString(udt1.udtDetail(i).bytMenuSet), gGetString(udt2.udtDetail(i).bytMenuSet), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グループメニューの表示位置X
                If Not gCompareString(udt1.udtDetail(i).groupviewx, udt2.udtDetail(i).groupviewx) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu X", gGetString(udt1.udtDetail(i).groupviewx), gGetString(udt2.udtDetail(i).groupviewx), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グループメニューの表示位置Y
                If Not gCompareString(udt1.udtDetail(i).groupviewy, udt2.udtDetail(i).groupviewy) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu Y", gGetString(udt1.udtDetail(i).groupviewy), gGetString(udt2.udtDetail(i).groupviewy), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グループメニューの横サイズ位置
                If Not gCompareString(udt1.udtDetail(i).groupsizex, udt2.udtDetail(i).groupsizex) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu Width", gGetString(udt1.udtDetail(i).groupsizex), gGetString(udt2.udtDetail(i).groupsizex), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グループメニューの縦サイズ位置
                If Not gCompareString(udt1.udtDetail(i).groupsizey, udt2.udtDetail(i).groupsizey) Then
                    msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu Height", gGetString(udt1.udtDetail(i).groupsizey), gGetString(udt2.udtDetail(i).groupsizey), "MENU", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''サブメニュー
                For j As Integer = 0 To UBound(udt1.udtDetail(i).udtGroup)

                    ''サブメニュー名称
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).strName, udt2.udtDetail(i).udtGroup(j).strName) Then ''20200513 iwasaki サブメニュー比較
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("Sub Name", gGetString(udt1.udtDetail(i).udtGroup(j).strName), gGetString(udt2.udtDetail(i).udtGroup(j).strName), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''グループメニューの動作開始地点（左上X座標）
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).grouptx, udt2.udtDetail(i).udtGroup(j).grouptx) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu LeftUp X", gGetString(udt1.udtDetail(i).udtGroup(j).grouptx), gGetString(udt2.udtDetail(i).udtGroup(j).grouptx), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''グループメニューの動作開始地点（左上X座標）
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupty, udt2.udtDetail(i).udtGroup(j).groupty) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu LeftUp Y", gGetString(udt1.udtDetail(i).udtGroup(j).groupty), gGetString(udt2.udtDetail(i).udtGroup(j).groupty), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''グループメニューの動作開始地点（右下X座標）
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupbx, udt2.udtDetail(i).udtGroup(j).groupbx) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu RightDown X", gGetString(udt1.udtDetail(i).udtGroup(j).groupbx), gGetString(udt2.udtDetail(i).udtGroup(j).groupbx), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''グループメニューの動作開始地点（右下Y座標）
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupby, udt2.udtDetail(i).udtGroup(j).groupby) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("GroupMenu RightDown Y", gGetString(udt1.udtDetail(i).udtGroup(j).groupby), gGetString(udt2.udtDetail(i).udtGroup(j).groupby), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''予備
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupSpare1, udt2.udtDetail(i).udtGroup(j).groupSpare1) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubSP1", gGetString(udt1.udtDetail(i).udtGroup(j).groupSpare1), gGetString(udt2.udtDetail(i).udtGroup(j).groupSpare1), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupSpare2, udt2.udtDetail(i).udtGroup(j).groupSpare2) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubSP2", gGetString(udt1.udtDetail(i).udtGroup(j).groupSpare2), gGetString(udt2.udtDetail(i).udtGroup(j).groupSpare2), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupSpare3, udt2.udtDetail(i).udtGroup(j).groupSpare3) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubSP3", gGetString(udt1.udtDetail(i).udtGroup(j).groupSpare3), gGetString(udt2.udtDetail(i).udtGroup(j).groupSpare3), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupSpare4, udt2.udtDetail(i).udtGroup(j).groupSpare4) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubSP4", gGetString(udt1.udtDetail(i).udtGroup(j).groupSpare4), gGetString(udt2.udtDetail(i).udtGroup(j).groupSpare4), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''メニュータイプ(処理項目・未使用)
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).groupbytMenuType, udt2.udtDetail(i).udtGroup(j).groupbytMenuType) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubMenuType(NotUse)", gGetString(udt1.udtDetail(i).udtGroup(j).groupbytMenuType), gGetString(udt2.udtDetail(i).udtGroup(j).groupbytMenuType), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''セレクトされているサブメニュー番号(未使用)
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).SubgroupYobi1, udt2.udtDetail(i).udtGroup(j).SubgroupYobi1) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("Selected SubMenuNo(NotUse)", gGetString(udt1.udtDetail(i).udtGroup(j).SubgroupYobi1), gGetString(udt2.udtDetail(i).udtGroup(j).SubgroupYobi1), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''セレクトされているサブメニュー番号(保持型)(未使用)
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).SubgroupYobi2, udt2.udtDetail(i).udtGroup(j).SubgroupYobi2) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("Selected SubMenuNo(StayType)(NotUse)", gGetString(udt1.udtDetail(i).udtGroup(j).SubgroupYobi2), gGetString(udt2.udtDetail(i).udtGroup(j).SubgroupYobi2), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''サブメニューの表示位置X
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).Subviewx, udt2.udtDetail(i).udtGroup(j).Subviewx) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubMenu X", gGetString(udt1.udtDetail(i).udtGroup(j).Subviewx), gGetString(udt2.udtDetail(i).udtGroup(j).Subviewx), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''サブメニューの表示位置Y
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).Subviewy, udt2.udtDetail(i).udtGroup(j).Subviewy) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubMenu Y", gGetString(udt1.udtDetail(i).udtGroup(j).Subviewy), gGetString(udt2.udtDetail(i).udtGroup(j).Subviewy), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''サブメニューの横サイズ位置
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).Subsizex, udt2.udtDetail(i).udtGroup(j).Subsizex) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubMenu Width", gGetString(udt1.udtDetail(i).udtGroup(j).Subsizex), gGetString(udt2.udtDetail(i).udtGroup(j).Subsizex), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''サブメニューの縦サイズ位置
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).Subsizey, udt2.udtDetail(i).udtGroup(j).Subsizey) Then
                        msgSYStemp(ix) = mMsgCreateOPS_MENU("SubMenu Height", gGetString(udt1.udtDetail(i).udtGroup(j).Subsizey), gGetString(udt2.udtDetail(i).udtGroup(j).Subsizey), "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "", "")
                        ix = ix + 1
                    End If


                    ''サブグループ
                    'Ver2.0.4.1 ﾙｰﾌﾟ回数算出配列が間違っていたので修正
                    For k As Integer = 0 To UBound(udt1.udtDetail(i).udtGroup(j).udtSub)

                        ''サブメニュー名称 
                        If Not gCompareString(udt1.udtDetail(i).udtGroup(j).udtSub(k).strName, udt2.udtDetail(i).udtGroup(j).udtSub(k).strName) Then ''20200513 iwasaki サブメニュー比較
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup Name", udt1.udtDetail(i).udtGroup(j).udtSub(k).strName, udt2.udtDetail(i).udtGroup(j).udtSub(k).strName, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''メニュータイプ(Bラベル処理項目1)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup MenuType(B1)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''メニュータイプ(Bラベル処理項目2)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup MenuType(B2)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''メニュータイプ(Bラベル処理項目3)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup MenuType(B3)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''メニュータイプ(Bラベル処理項目4)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup MenuType(B4)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''画面モード（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("DispMode(NotUse)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''現在操作可能な画面の表示位置（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("ImpossibleDispPos(NotUse)", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''キーコード
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode <> udt2.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("KeyCode", udt1.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode, udt2.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''予備
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup SP", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''画面番号0
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("DispNo0", udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1, udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''画面番号1
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("DispNo1", udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2, udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''画面番号2
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("DispNo2", udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3, udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''画面番号3
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("DispNo3", udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4, udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''サブメニューの動作開始地点（左上X座標）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup LeftUp X", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''サブメニューの動作開始地点（左上Y座標)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup LeftUp Y", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''サブメニューの動作開始地点（右下X座標）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup RightDown X", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                        ''サブメニューの動作開始地点（右下Y座標）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby Then
                            msgSYStemp(ix) = mMsgCreateOPS_MENU("SubGroup RightDown Y", udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby, udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby, "MENU", Str(i + 1), "SUBMENU", Str(j + 1), "SUBGROUP", Str(k + 1))
                            ix = ix + 1
                        End If

                    Next
                Next
            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "セレクションメニューOK"

    Friend Function mCompareSetOpsSelectionMenu(ByVal udt1 As gTypSetOpsSelectionMenu, _
                                                ByVal udt2 As gTypSetOpsSelectionMenu, _
                                                ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　OPS SELECTION MENU（" & mGetMCEng(udtMC) & "） ■■■■"

            ''セレクションオフセット設定
            For i = 0 To UBound(udt1.udtOpsSelectionOffSetRec)

                ''画面番号
                If udt1.udtOpsSelectionOffSetRec(i).ViewNo <> udt2.udtOpsSelectionOffSetRec(i).ViewNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("ViewNo.", udt1.udtOpsSelectionOffSetRec(i).ViewNo, udt2.udtOpsSelectionOffSetRec(i).ViewNo, "SELECTION", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

            Next

            ''セレクションメニュー設定
            For i = 0 To UBound(udt1.udtOpsSelectionSetViewRec)

                ''セレクション名称
                If udt1.udtOpsSelectionSetViewRec(i).SelectName <> udt2.udtOpsSelectionSetViewRec(i).SelectName Then
                    msgSYStemp(ix) = mMsgCreateOPS("SelectionName", udt1.udtOpsSelectionSetViewRec(i).SelectName, udt2.udtOpsSelectionSetViewRec(i).SelectName, "SELECTION", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''キー
                For j As Integer = 0 To UBound(udt1.udtOpsSelectionSetViewRec(i).udtKey)

                    ''処理項目1
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType1 <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType1 Then
                        msgSYStemp(ix) = mMsgCreateOPS("function1", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType1, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType1, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''処理項目2
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType2 <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType2 Then
                        msgSYStemp(ix) = mMsgCreateOPS("function2", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType2, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType2, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''処理項目3
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType3 <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType3 Then
                        msgSYStemp(ix) = mMsgCreateOPS("function3", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType3, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType3, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''処理項目4
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType4 <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType4 Then
                        msgSYStemp(ix) = mMsgCreateOPS("function4", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType4, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytNameType4, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''画面番号
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytSelectName <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytSelectName Then
                        msgSYStemp(ix) = mMsgCreateOPS("ViewNo.", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).BytSelectName, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).BytSelectName, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''名称コード
                    If udt1.udtOpsSelectionSetViewRec(i).udtKey(j).NameCode <> udt2.udtOpsSelectionSetViewRec(i).udtKey(j).NameCode Then
                        msgSYStemp(ix) = mMsgCreateOPS("NameCode", udt1.udtOpsSelectionSetViewRec(i).udtKey(j).NameCode, udt2.udtOpsSelectionSetViewRec(i).udtKey(j).NameCode, "SELECTION", Str(i + 1), "Key", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "フリーディスプレイOK"

    Friend Function mCompareSetOpsFreeDisplay(ByVal udt1 As gTypSetOpsFreeDisplay, _
                                              ByVal udt2 As gTypSetOpsFreeDisplay, _
                                              ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　FREE DISPLAY（" & mGetMCEng(udtMC) & "） ■■■■"

            For i As Integer = LBound(udt1.udtFreeDisplayRec) To UBound(udt1.udtFreeDisplayRec)

                ''OPS番号
                If udt1.udtFreeDisplayRec(i).bytOps <> udt2.udtFreeDisplayRec(i).bytOps Then
                    msgSYStemp(ix) = mMsgCreateOPS("OPS_No.", udt1.udtFreeDisplayRec(i).bytOps, udt2.udtFreeDisplayRec(i).bytOps, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''ページ番号
                If udt1.udtFreeDisplayRec(i).bytPage <> udt2.udtFreeDisplayRec(i).bytPage Then
                    msgSYStemp(ix) = mMsgCreateOPS("PAGE_No.", udt1.udtFreeDisplayRec(i).bytPage, udt2.udtFreeDisplayRec(i).bytPage, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''ページタイトル
                If Not gCompareString(udt1.udtFreeDisplayRec(i).strPageTitle, udt2.udtFreeDisplayRec(i).strPageTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("PAGE_TITLE", gGetString(udt1.udtFreeDisplayRec(i).strPageTitle), gGetString(udt2.udtFreeDisplayRec(i).strPageTitle), "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                For j As Integer = 0 To UBound(udt1.udtFreeDisplayRec(i).udtFreeDisplayRecChno)

                    ''チャンネル番号
                    If udt1.udtFreeDisplayRec(i).udtFreeDisplayRecChno(j).shtChno <> udt2.udtFreeDisplayRec(i).udtFreeDisplayRecChno(j).shtChno Then
                        msgSYStemp(ix) = mMsgCreateOPS("CHNO", udt1.udtFreeDisplayRec(i).udtFreeDisplayRecChno(j).shtChno, udt2.udtFreeDisplayRec(i).udtFreeDisplayRecChno(j).shtChno, "OPS", Str(i + 1), "CHNO", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                Next
            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "トレンドグラフOK"

    Friend Function mCompareSetOpsTrendGraph(ByVal udt1 As gTypSetOpsTrendGraph, _
                                             ByVal udt2 As gTypSetOpsTrendGraph, _
                                             ByVal udtMC As gEnmMachineryCargo) As Integer

        Try
            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　TRENDGRAPH（" & mGetMCEng(udtMC) & "） ■■■■"

            For i As Integer = LBound(udt1.udtTrendGraphRec) To UBound(udt1.udtTrendGraphRec)

                ''OPS番号
                If udt1.udtTrendGraphRec(i).bytOps <> udt2.udtTrendGraphRec(i).bytOps Then
                    msgSYStemp(ix) = mMsgCreateOPS("OPS_No.", udt1.udtTrendGraphRec(i).bytOps, udt2.udtTrendGraphRec(i).bytOps, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グラフ番号
                If udt1.udtTrendGraphRec(i).bytNo <> udt2.udtTrendGraphRec(i).bytNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("GRAPH_No.", udt1.udtTrendGraphRec(i).bytNo, udt2.udtTrendGraphRec(i).bytNo, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''グラフタイトル
                If Not gCompareString(udt1.udtTrendGraphRec(i).strPageTitle, udt2.udtTrendGraphRec(i).strPageTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("TITLE", gGetString(udt1.udtTrendGraphRec(i).strPageTitle), gGetString(udt2.udtTrendGraphRec(i).strPageTitle), "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''サンプリング時間 種別
                If udt1.udtTrendGraphRec(i).bytSnpType <> udt2.udtTrendGraphRec(i).bytSnpType Then
                    msgSYStemp(ix) = mMsgCreateOPS("TYPE", udt1.udtTrendGraphRec(i).bytSnpType, udt2.udtTrendGraphRec(i).bytSnpType, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''サンプリング時間 時間値
                If udt1.udtTrendGraphRec(i).bytSnpTime <> udt2.udtTrendGraphRec(i).bytSnpTime Then
                    msgSYStemp(ix) = mMsgCreateOPS("TIME", udt1.udtTrendGraphRec(i).bytSnpTime, udt2.udtTrendGraphRec(i).bytSnpTime, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''トリガ CH有無
                If udt1.udtTrendGraphRec(i).shtTrgUse <> udt2.udtTrendGraphRec(i).shtTrgUse Then
                    msgSYStemp(ix) = mMsgCreateOPS("TRG_USE", udt1.udtTrendGraphRec(i).shtTrgUse, udt2.udtTrendGraphRec(i).shtTrgUse, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''トリガ チャンネル番号
                If udt1.udtTrendGraphRec(i).shtTrgChno <> udt2.udtTrendGraphRec(i).shtTrgChno Then
                    msgSYStemp(ix) = mMsgCreateOPS("TRG_CH", udt1.udtTrendGraphRec(i).shtTrgChno, udt2.udtTrendGraphRec(i).shtTrgChno, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''トリガ 種別
                If udt1.udtTrendGraphRec(i).shtTrgSelect <> udt2.udtTrendGraphRec(i).shtTrgSelect Then
                    msgSYStemp(ix) = mMsgCreateOPS("TRG_SELECT", udt1.udtTrendGraphRec(i).shtTrgSelect, udt2.udtTrendGraphRec(i).shtTrgSelect, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''トリガ 比較条件
                If udt1.udtTrendGraphRec(i).shtTrgSet <> udt2.udtTrendGraphRec(i).shtTrgSet Then
                    msgSYStemp(ix) = mMsgCreateOPS("TRG_SET", udt1.udtTrendGraphRec(i).shtTrgSet, udt2.udtTrendGraphRec(i).shtTrgSet, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''トリガ 値
                If udt1.udtTrendGraphRec(i).shtTrgValue <> udt2.udtTrendGraphRec(i).shtTrgValue Then
                    msgSYStemp(ix) = mMsgCreateOPS("TRG_VALUE", udt1.udtTrendGraphRec(i).shtTrgSet, udt2.udtTrendGraphRec(i).shtTrgSet, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''ディレイポイント値
                If udt1.udtTrendGraphRec(i).shtDelay <> udt2.udtTrendGraphRec(i).shtDelay Then
                    msgSYStemp(ix) = mMsgCreateOPS("delay", udt1.udtTrendGraphRec(i).shtDelay, udt2.udtTrendGraphRec(i).shtDelay, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If

                For j As Integer = 0 To UBound(udt1.udtTrendGraphRec(i).udtTrendGraphRecChno)

                    ''番号
                    If udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno <> udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno Then
                        msgSYStemp(ix) = mMsgCreateOPS("chno", udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno, udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno, "OPS", Str(i + 1), "TD_GRAPH", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                    ''マスクビット
                    If udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask <> udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask Then
                        msgSYStemp(ix) = mMsgCreateOPS("MASK", udt1.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask, udt2.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask, "OPS", Str(i + 1), "TD_GRAPH", Str(j + 1), "", "")
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログフォーマットOK"

    Friend Function mCompareSetOpsLogFormat(ByVal udt1 As gTypSetOpsLogFormat, _
                                            ByVal udt2 As gTypSetOpsLogFormat, _
                                            ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　LOG FORMAT（" & mGetMCEng(udtMC) & "） ■■■■"

            For i As Integer = LBound(udt1.strCol1) To UBound(udt1.strCol1)

                ''列１
                If Not gCompareString(udt1.strCol1(i), udt2.strCol1(i)) Then
                    msgSYStemp(ix) = mMsgCreateSys("Col1", gGetString(udt1.strCol1(i)), gGetString(udt2.strCol1(i)), "LOG_FORMAT", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            For i As Integer = LBound(udt1.strCol2) To UBound(udt1.strCol2)

                ''列２
                If Not gCompareString(udt1.strCol2(i), udt2.strCol2(i)) Then
                    msgSYStemp(ix) = mMsgCreateSys("Col2", gGetString(udt1.strCol2(i)), gGetString(udt2.strCol2(i)), "LOG_FORMAT", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログフォーマットCHIDOK"

    Friend Function mCompareSetOpsLogIdData(ByVal udt1 As gTypSetOpsLogIdData, _
                                            ByVal udt2 As gTypSetOpsLogIdData, _
                                            ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　LOG FORMAT CH ID（" & mGetMCEng(udtMC) & "） ■■■■"

            For i As Integer = LBound(udt1.shtLogChTbl) To UBound(udt1.shtLogChTbl)

                ''列
                If Not gCompareString(udt1.shtLogChTbl(i), udt2.shtLogChTbl(i)) Then
                    msgSYStemp(ix) = mMsgCreateSys("CHID", gGetString(udt1.shtLogChTbl(i)), gGetString(udt2.shtLogChTbl(i)), "ID", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "CH変換テーブルOK"

    Friend Function mCompareSetChConv(ByVal udt1 As gTypSetChConv, _
                                      ByVal udt2 As gTypSetChConv) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　CH CONVERT TABLE 　■■■■"

            Dim CHIDChange As Integer

            For i As Integer = LBound(udt1.udtChConv) To UBound(udt1.udtChConv)

                'Ver2.0.6.1 比較先のCHIDに変更
                'CHIDChange = gConvChIdToChNoComp(udt1.udtChConv(i).shtChid)
                'CHIDChange = gConvChIdToChNoComp(udt2.udtChConv(i).shtChid)
                CHIDChange = gConvChIdToChNoCompTarget(udt2.udtChConv(i).shtChid)

                ''チャンネルID
                If udt1.udtChConv(i).shtChid <> udt2.udtChConv(i).shtChid Then
                    msgSYStemp(ix) = mMsgCreateSys("ID", udt1.udtChConv(i).shtChid, udt2.udtChConv(i).shtChid, "CHNo.", CHIDChange)
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログ印字設定OK"

    Friend Function mCompareSetOtherLogTime(ByVal udt1 As gTypSetOtherLogTime, _
                                            ByVal udt2 As gTypSetOtherLogTime) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　LOG PRINT SETTING 　■■■■"

            ''レギュラーログ
            For i As Integer = 0 To UBound(udt1.udtLogTimeRec.udtLogTimeRegular)

                ''使用有無
                If udt1.udtLogTimeRec.udtLogTimeRegular(i).bytUse <> udt2.udtLogTimeRec.udtLogTimeRegular(i).bytUse Then
                    msgSYStemp(ix) = mMsgCreateSys("USE", udt1.udtLogTimeRec.udtLogTimeRegular(i).bytUse, udt2.udtLogTimeRec.udtLogTimeRegular(i).bytUse, "REGULAR", Str(i + 1))
                    ix = ix + 1
                End If

                ''時
                If udt1.udtLogTimeRec.udtLogTimeRegular(i).bytTimeHH <> udt2.udtLogTimeRec.udtLogTimeRegular(i).bytTimeHH Then
                    msgSYStemp(ix) = mMsgCreateSys("TIME_hh", udt1.udtLogTimeRec.udtLogTimeRegular(i).bytTimeHH, udt2.udtLogTimeRec.udtLogTimeRegular(i).bytTimeHH, "REGULAR", Str(i + 1))
                    ix = ix + 1
                End If

                ''分
                If udt1.udtLogTimeRec.udtLogTimeRegular(i).bytTimeMM <> udt2.udtLogTimeRec.udtLogTimeRegular(i).bytTimeMM Then
                    msgSYStemp(ix) = mMsgCreateSys("TIME_mm", udt1.udtLogTimeRec.udtLogTimeRegular(i).bytTimeMM, udt2.udtLogTimeRec.udtLogTimeRegular(i).bytTimeMM, "REGULAR", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            ''レポートログ
            For i As Integer = 0 To UBound(udt1.udtLogTimeRec.udtLogTimeReport)

                ''使用有無
                If udt1.udtLogTimeRec.udtLogTimeReport(i).bytUse <> udt2.udtLogTimeRec.udtLogTimeReport(i).bytUse Then
                    msgSYStemp(ix) = mMsgCreateSys("USE", udt1.udtLogTimeRec.udtLogTimeReport(i).bytUse, udt2.udtLogTimeRec.udtLogTimeReport(i).bytUse, "REPORT", Str(i + 1))
                    ix = ix + 1
                End If

                ''時
                If udt1.udtLogTimeRec.udtLogTimeReport(i).bytTimeHH <> udt2.udtLogTimeRec.udtLogTimeReport(i).bytTimeHH Then
                    msgSYStemp(ix) = mMsgCreateSys("TIME_hh", udt1.udtLogTimeRec.udtLogTimeReport(i).bytTimeHH, udt2.udtLogTimeRec.udtLogTimeReport(i).bytTimeHH, "REPORT", Str(i + 1))
                    ix = ix + 1
                End If

                ''分
                If udt1.udtLogTimeRec.udtLogTimeReport(i).bytTimeMM <> udt2.udtLogTimeRec.udtLogTimeReport(i).bytTimeMM Then
                    msgSYStemp(ix) = mMsgCreateSys("TIME_mm", udt1.udtLogTimeRec.udtLogTimeReport(i).bytTimeMM, udt2.udtLogTimeRec.udtLogTimeReport(i).bytTimeMM, "REPORT", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            ''インターバルログ
            For i As Integer = 0 To UBound(udt1.udtLogTimeRec.udtLogTimeInterval)

                ''使用有無
                If udt1.udtLogTimeRec.udtLogTimeInterval(i).bytUse <> udt2.udtLogTimeRec.udtLogTimeInterval(i).bytUse Then
                    msgSYStemp(ix) = mMsgCreateSys("USE", udt1.udtLogTimeRec.udtLogTimeInterval(i).bytUse, udt2.udtLogTimeRec.udtLogTimeInterval(i).bytUse, "INTERVAL", Str(i + 1))
                    ix = ix + 1
                End If

                ''時
                If udt1.udtLogTimeRec.udtLogTimeInterval(i).bytTimeHH <> udt2.udtLogTimeRec.udtLogTimeInterval(i).bytTimeHH Then
                    msgSYStemp(ix) = mMsgCreateSys("TIME_hh", udt1.udtLogTimeRec.udtLogTimeInterval(i).bytTimeHH, udt2.udtLogTimeRec.udtLogTimeInterval(i).bytTimeHH, "INTERVAL", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPS設定OK"

    Friend Function mCompareSetOpsGraph(ByVal udt1 As gTypSetOpsGraph, _
                                        ByVal udt2 As gTypSetOpsGraph, _
                                        ByVal udtMC As gEnmMachineryCargo) As Integer

        Dim ix As Integer = 1

        msgSYStemp(0) = "■■■■　OPS SETTING（" & mGetMCEng(udtMC) & "） ■■■■"


        ''グラフタイトル
        For i = 0 To UBound(udt1.udtGraphTitleRec)
            ix = mCompareOpsGraphTitle(i, udt1.udtGraphTitleRec(i), udt2.udtGraphTitleRec(i), udtMC, ix)
        Next

        ''偏差グラフ
        For i = 0 To UBound(udt1.udtGraphExhaustRec)
            ix = mCompareOpsGraphExhaust(i, udt1.udtGraphExhaustRec(i), udt2.udtGraphExhaustRec(i), udtMC, ix)
        Next

        ''バーグラフ
        For i As Integer = 0 To UBound(udt1.udtGraphBarRec)
            ix = mCompareOpsGraphBar(i, udt1.udtGraphBarRec(i), udt2.udtGraphBarRec(i), udtMC, ix)
        Next

        ''アナログメーター
        For i As Integer = 0 To UBound(udt1.udtGraphAnalogMeterRec)
            ix = mCompareOpsGraphAnalogMeter(i, udt1.udtGraphAnalogMeterRec(i), udt2.udtGraphAnalogMeterRec(i), udtMC, ix)
        Next

        ''アナログメーター設定
        ''チャンネル名称表示位置
        If udt1.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint <> udt2.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint Then
            msgSYStemp(ix) = mMsgCreateOPS("disp_point", udt1.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint, udt2.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint, "METER", "", "", "", "", "")
            ix = ix + 1
        End If

        ''目盛数値表示方法
        If udt1.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue <> udt2.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue Then
            msgSYStemp(ix) = mMsgCreateOPS("mark_num", udt1.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue, udt2.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue, "METER", "", "", "", "", "")
            ix = ix + 1
        End If

        ''指針の縁取り
        If udt1.udtGraphAnalogMeterSettingRec.bytPointerFrame <> udt2.udtGraphAnalogMeterSettingRec.bytPointerFrame Then
            msgSYStemp(ix) = mMsgCreateOPS("point_frame", udt1.udtGraphAnalogMeterSettingRec.bytPointerFrame, udt2.udtGraphAnalogMeterSettingRec.bytPointerFrame, "METER", "", "", "", "", "")
            ix = ix + 1
        End If

        ''指針の色変更
        If udt1.udtGraphAnalogMeterSettingRec.bytPointerColorChange <> udt2.udtGraphAnalogMeterSettingRec.bytPointerColorChange Then
            msgSYStemp(ix) = mMsgCreateOPS("point_color", udt1.udtGraphAnalogMeterSettingRec.bytPointerColorChange, udt2.udtGraphAnalogMeterSettingRec.bytPointerColorChange, "METER", "", "", "", "", "")
            ix = ix + 1
        End If

        '何も変更がない場合は表示しない
        If ix <> 1 Then
            Call mMsgSysGrid(ix)
        Else
            '変更がないためタイトルクリア
            msgSYStemp(0) = ""
        End If


    End Function

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <グラフタイトル>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) グラフタイトル構造体
    '--------------------------------------------------------------------
    Friend Function mCompareOpsGraphTitle(ByVal intNo As Integer, _
                                          ByVal udt1 As gTypSetOpsGraphTitle, _
                                          ByVal udt2 As gTypSetOpsGraphTitle, _
                                          ByVal udtMC As gEnmMachineryCargo, _
                                          ByVal Gyo As Integer) As Integer

        Try

            Dim ix As Integer = Gyo

            ''グラフ番号
            If udt1.bytNo <> udt2.bytNo Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_No.", udt1.bytNo, udt2.bytNo, "TITLE", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''グラフタイプ
            If udt1.bytType <> udt2.bytType Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_TYPE", udt1.bytType, udt2.bytType, "TITLE", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''グラフ名称
            If Not gCompareString(udt1.strName, udt2.strName) Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_NAME", gGetString(udt1.strName), gGetString(udt2.strName), "TITLE", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            mCompareOpsGraphTitle = ix

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <偏差グラフ（排ガス）>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) 偏差グラフ構造体
    '--------------------------------------------------------------------
    Friend Function mCompareOpsGraphExhaust(ByVal intNo As Integer, _
                                            ByVal udt1 As gTypSetOpsGraphExhaust, _
                                            ByVal udt2 As gTypSetOpsGraphExhaust, _
                                            ByVal udtMC As gEnmMachineryCargo, _
                                            ByVal Gyo As Integer) As Integer

        Try

            Dim ix As Integer = Gyo

            ''グラフ番号
            If udt1.bytNo <> udt2.bytNo Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_No.", udt1.bytNo, udt2.bytNo, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''タイトル
            If Not gCompareString(udt1.strTitle, udt2.strTitle) Then
                msgSYStemp(ix) = mMsgCreateOPS("TITLE", gGetString(udt1.strTitle), gGetString(udt2.strTitle), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''データ名称（上段）
            If Not gCompareString(udt1.strItemUp, udt2.strItemUp) Then
                msgSYStemp(ix) = mMsgCreateOPS("ITEM_UP", gGetString(udt1.strItemUp), gGetString(udt2.strItemUp), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''データ名称（下段）
            If Not gCompareString(udt1.strItemDown, udt2.strItemDown) Then
                msgSYStemp(ix) = mMsgCreateOPS("ITEM_DOWN", gGetString(udt1.strItemDown), gGetString(udt2.strItemDown), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''平均CH
            If udt1.shtAveCh <> udt2.shtAveCh Then
                msgSYStemp(ix) = mMsgCreateOPS("AVE_CH", udt1.shtAveCh, udt2.shtAveCh, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''偏差目盛の上下限値
            If udt1.bytDevMark <> udt2.bytDevMark Then
                msgSYStemp(ix) = mMsgCreateOPS("DEV_MARK", udt1.bytDevMark, udt2.bytDevMark, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''Line設定
            If udt1.bytLine <> udt2.bytLine Then
                msgSYStemp(ix) = mMsgCreateOPS("LINE", udt1.bytLine, udt2.bytLine, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''シリンダ数
            If udt1.bytCyCnt <> udt2.bytCyCnt Then
                msgSYStemp(ix) = mMsgCreateOPS("CY_CNT", udt1.bytCyCnt, udt2.bytCyCnt, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''Cylinder Graph
            For i = 0 To UBound(udt1.udtCylinder)

                ''シリンダCH番号
                If udt1.udtCylinder(i).shtChCylinder <> udt2.udtCylinder(i).shtChCylinder Then
                    msgSYStemp(ix) = mMsgCreateOPS("cy_chno", udt1.udtCylinder(i).shtChCylinder, udt2.udtCylinder(i).shtChCylinder, "EXH", Str(intNo + 1), "Cylinder", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''偏差CH番号
                If udt1.udtCylinder(i).shtChDeviation <> udt2.udtCylinder(i).shtChDeviation Then
                    msgSYStemp(ix) = mMsgCreateOPS("dev_chno", udt1.udtCylinder(i).shtChDeviation, udt2.udtCylinder(i).shtChDeviation, "EXH", Str(intNo + 1), "Cylinder", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''タイトル
                If Not gCompareString(udt1.udtCylinder(i).strTitle, udt2.udtCylinder(i).strTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("title", gGetString(udt1.udtCylinder(i).strTitle), gGetString(udt2.udtCylinder(i).strTitle), "EXH", Str(intNo + 1), "Cylinder", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            ''T/Cグラフタイトル
            If Not gCompareString(udt1.strTcTitle, udt2.strTcTitle) Then
                msgSYStemp(ix) = mMsgCreateOPS("tc_title", gGetString(udt1.strTcTitle), gGetString(udt2.strTcTitle), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''T/Cコメント1
            If Not gCompareString(udt1.strTcComm1, udt2.strTcComm1) Then
                msgSYStemp(ix) = mMsgCreateOPS("tc_comm1", gGetString(udt1.strTcComm1), gGetString(udt2.strTcComm1), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''T/Cコメント2
            If Not gCompareString(udt1.strTcComm2, udt2.strTcComm2) Then
                msgSYStemp(ix) = mMsgCreateOPS("tc_comm2", gGetString(udt1.strTcComm2), gGetString(udt2.strTcComm2), "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''T/C数
            If udt1.bytTcCnt <> udt2.bytTcCnt Then
                msgSYStemp(ix) = mMsgCreateOPS("tc_cnt", udt1.bytTcCnt, udt2.bytTcCnt, "EXH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            For i = 0 To UBound(udt1.udtTurboCharger)

                ''T/Cチャンネル番号
                If udt1.udtTurboCharger(i).shtChTurboCharger <> udt2.udtTurboCharger(i).shtChTurboCharger Then
                    msgSYStemp(ix) = mMsgCreateOPS("tc_chno", udt1.udtTurboCharger(i).shtChTurboCharger, udt2.udtTurboCharger(i).shtChTurboCharger, "EXH", Str(intNo + 1), "T/C", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''T/Cチャンネルタイトル
                If Not gCompareString(udt1.udtTurboCharger(i).strTitle, udt2.udtTurboCharger(i).strTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("tc_title", gGetString(udt1.udtTurboCharger(i).strTitle), gGetString(udt2.udtTurboCharger(i).strTitle), "EXH", Str(intNo + 1), "T/C", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''T/C区切り線
                If udt1.udtTurboCharger(i).bytSplitLine <> udt2.udtTurboCharger(i).bytSplitLine Then
                    msgSYStemp(ix) = mMsgCreateOPS("tc_split", udt1.udtTurboCharger(i).bytSplitLine, udt2.udtTurboCharger(i).bytSplitLine, "EXH", Str(intNo + 1), "T/C", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            mCompareOpsGraphExhaust = ix

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <バーグラフ>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) バーグラフ構造体
    '--------------------------------------------------------------------
    Friend Function mCompareOpsGraphBar(ByVal intNo As Integer, _
                                        ByVal udt1 As gTypSetOpsGraphBar, _
                                        ByVal udt2 As gTypSetOpsGraphBar, _
                                        ByVal udtMC As gEnmMachineryCargo, _
                                        ByVal Gyo As Integer) As Integer

        Try

            Dim ix As Integer = Gyo

            ''グラフ番号
            If udt1.bytNo <> udt2.bytNo Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_No.", udt1.bytNo, udt2.bytNo, "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''グラフタイトル
            If Not gCompareString(udt1.strTitle, udt2.strTitle) Then
                msgSYStemp(ix) = mMsgCreateOPS("TITLE", gGetString(udt1.strTitle), gGetString(udt2.strTitle), "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''データ名称（上段）
            If Not gCompareString(udt1.strItemUp, udt2.strItemUp) Then
                msgSYStemp(ix) = mMsgCreateOPS("ITEM_UP", gGetString(udt1.strItemUp), gGetString(udt2.strItemUp), "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''データ名称（下段）
            If Not gCompareString(udt1.strItemDown, udt2.strItemDown) Then
                msgSYStemp(ix) = mMsgCreateOPS("ITEM_DOWN", gGetString(udt1.strItemDown), gGetString(udt2.strItemDown), "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''表示切替指定
            If udt1.bytDisplay <> udt2.bytDisplay Then
                msgSYStemp(ix) = mMsgCreateOPS("disp_type", udt1.bytDisplay, udt2.bytDisplay, "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''数値の分け方
            If udt1.bytLine <> udt2.bytLine Then
                msgSYStemp(ix) = mMsgCreateOPS("line", udt1.bytLine, udt2.bytLine, "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''分割数
            If udt1.bytDevision <> udt2.bytDevision Then
                msgSYStemp(ix) = mMsgCreateOPS("devision", udt1.bytDevision, udt2.bytDevision, "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''シリンダ数
            If udt1.bytCyCnt <> udt2.bytCyCnt Then
                msgSYStemp(ix) = mMsgCreateOPS("cy_cnt", udt1.bytCyCnt, udt2.bytCyCnt, "BAR_GRAPH", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            For i As Integer = 0 To UBound(udt1.udtCylinder)

                ''チャンネル番号
                If udt1.udtCylinder(i).shtChCylinder <> udt2.udtCylinder(i).shtChCylinder Then
                    msgSYStemp(ix) = mMsgCreateOPS("cy_chno", udt1.udtCylinder(i).shtChCylinder, udt2.udtCylinder(i).shtChCylinder, "BAR_GRAPH", Str(intNo + 1), "Cylinder", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''タイトル
                If Not gCompareString(udt1.udtCylinder(i).strTitle, udt2.udtCylinder(i).strTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("title", gGetString(udt1.udtCylinder(i).strTitle), gGetString(udt2.udtCylinder(i).strTitle), "BAR_GRAPH", Str(intNo + 1), "Cylinder", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            mCompareOpsGraphBar = ix

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <アナログメーター>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) アナログメーター構造体
    '--------------------------------------------------------------------
    Friend Function mCompareOpsGraphAnalogMeter(ByVal intNo As Integer, _
                                                ByVal udt1 As gTypSetOpsGraphAnalogMeter, _
                                                ByVal udt2 As gTypSetOpsGraphAnalogMeter, _
                                                ByVal udtMC As gEnmMachineryCargo, _
                                                ByVal Gyo As Integer) As Integer

        Try

            Dim ix As Integer = Gyo

            ''グラフ番号
            If udt1.bytNo <> udt2.bytNo Then
                msgSYStemp(ix) = mMsgCreateOPS("GRAPH_NO.", udt1.bytNo, udt2.bytNo, "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''メータータイプ
            If udt1.bytMeterType <> udt2.bytMeterType Then
                msgSYStemp(ix) = mMsgCreateOPS("METER_TYPE", udt1.bytMeterType, udt2.bytMeterType, "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            ''グラフタイトル
            If Not gCompareString(udt1.strTitle, udt2.strTitle) Then
                msgSYStemp(ix) = mMsgCreateOPS("TITLE", gGetString(udt1.strTitle), gGetString(udt2.strTitle), "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                ix = ix + 1
            End If

            For i As Integer = 0 To UBound(udt1.udtDetail)

                ''チャンネル番号
                If udt1.udtDetail(i).shtChNo <> udt2.udtDetail(i).shtChNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("chno", udt1.udtDetail(i).shtChNo, udt2.udtDetail(i).shtChNo, "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''目盛り分割数
                If udt1.udtDetail(i).bytScale <> udt2.udtDetail(i).bytScale Then
                    msgSYStemp(ix) = mMsgCreateOPS("scale", udt1.udtDetail(i).bytScale, udt2.udtDetail(i).bytScale, "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                    ix = ix + 1
                End If

                ''表示色
                If udt1.udtDetail(i).bytColor <> udt2.udtDetail(i).bytColor Then
                    msgSYStemp(ix) = mMsgCreateOPS("color", udt1.udtDetail(i).bytColor, udt2.udtDetail(i).bytColor, "ANALOG_METER", Str(intNo + 1), "", "", "", "")
                    ix = ix + 1
                End If

            Next

            mCompareOpsGraphAnalogMeter = ix

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "フリーグラフOK"

    Friend Function mCompareSetOpsFreeGraph(ByVal udt1 As gTypSetOpsFreeGraph, _
                                              ByVal udt2 As gTypSetOpsFreeGraph, _
                                              ByVal udtMC As gEnmMachineryCargo) As Integer

        Dim intOpsNo As Integer
        Dim intGraphNo As Integer

        Try
            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　FREE GRAPH（" & mGetMCEng(udtMC) & "）■■■■"

            For i As Integer = LBound(udt1.udtFreeGraphRec) To UBound(udt1.udtFreeGraphRec)

                ''OPS番号
                If udt1.udtFreeGraphRec(i).bytOpsNo <> udt2.udtFreeGraphRec(i).bytOpsNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("OPS_No.", udt1.udtFreeGraphRec(i).bytOpsNo, udt2.udtFreeGraphRec(i).bytOpsNo, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If
                intOpsNo = udt1.udtFreeGraphRec(i).bytOpsNo

                ''グラフ番号
                If udt1.udtFreeGraphRec(i).bytGraphNo <> udt2.udtFreeGraphRec(i).bytGraphNo Then
                    msgSYStemp(ix) = mMsgCreateOPS("GRAPH_No.", udt1.udtFreeGraphRec(i).bytGraphNo, udt2.udtFreeGraphRec(i).bytGraphNo, "OPS", Str(i + 1), "", "", "", "")
                    ix = ix + 1
                End If
                intGraphNo = udt1.udtFreeGraphRec(i).bytGraphNo

                ''タイトル
                If Not gCompareString(udt1.udtFreeGraphRec(i).strGraphTitle, udt2.udtFreeGraphRec(i).strGraphTitle) Then
                    msgSYStemp(ix) = mMsgCreateOPS("TITLE", gGetString(udt1.udtFreeGraphRec(i).strGraphTitle), gGetString(udt2.udtFreeGraphRec(i).strGraphTitle), "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "", "")
                    ix = ix + 1
                End If


                For j As Integer = LBound(udt1.udtFreeGraphRec(i).udtFreeDetail) To UBound(udt1.udtFreeGraphRec(i).udtFreeDetail)

                    ''グラフタイプ
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytType <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytType Then
                        msgSYStemp(ix) = mMsgCreateOPS("TYPE", udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytType, udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytType, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "", "")
                        ix = ix + 1
                    End If

                    ''先頭位置
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytTopPos <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytTopPos Then
                        msgSYStemp(ix) = mMsgCreateOPS("TOP_POS", udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytTopPos, udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytTopPos, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "", "")
                        ix = ix + 1
                    End If

                    ''チャンネル番号
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).shtChNo <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).shtChNo Then
                        msgSYStemp(ix) = mMsgCreateOPS("CHNo", udt1.udtFreeGraphRec(i).udtFreeDetail(j).shtChNo, udt2.udtFreeGraphRec(i).udtFreeDetail(j).shtChNo, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "", "")
                        ix = ix + 1
                    End If

                    ''表示種別
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytIndicatorKind <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytIndicatorKind Then
                        msgSYStemp(ix) = mMsgCreateOPS("inf_kind", udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytIndicatorKind, udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytIndicatorKind, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "", "")
                        ix = ix + 1
                    End If

                    ''表示マスク
                    For k As Integer = 0 To 15
                        If gBitCheck(udt1.udtFreeGraphRec(i).udtFreeDetail(j).shtIndicatorPattern, k) <> gBitCheck(udt2.udtFreeGraphRec(i).udtFreeDetail(j).shtIndicatorPattern, k) Then
                            msgSYStemp(ix) = mMsgCreateOPS("ind_pattern bit" & k, gBitValue(udt1.udtFreeGraphRec(i).udtFreeDetail(j).shtIndicatorPattern, k), gBitValue(udt2.udtFreeGraphRec(i).udtFreeDetail(j).shtIndicatorPattern, k), "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "POS", Str(k))
                            ix = ix + 1
                        End If
                    Next

                    ''メモリ分割数
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytScale <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytScale Then
                        msgSYStemp(ix) = mMsgCreateOPS("scale", udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytScale, udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytScale, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "POS", Str(j))
                        ix = ix + 1
                    End If

                    ''表示色
                    If udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytColor <> udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytColor Then
                        msgSYStemp(ix) = mMsgCreateOPS("color", udt1.udtFreeGraphRec(i).udtFreeDetail(j).bytColor, udt2.udtFreeGraphRec(i).udtFreeDetail(j).bytColor, "OPS", Str(intOpsNo), "GRAPH", Str(intGraphNo), "POS", Str(j))
                        ix = ix + 1
                    End If
                Next
            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "GWS設定 CH設定データOK"

    Friend Function mCompareSetOpsGwsCh(ByVal udt1 As gTypSetOpsGwsCh, _
                                        ByVal udt2 As gTypSetOpsGwsCh) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　GWS SETTING　■■■■"

            For i As Integer = 0 To UBound(udt1.udtGwsFileRec)

                For j As Integer = 0 To UBound(udt1.udtGwsFileRec(i).udtGwsChRec)

                    ''チャンネル番号
                    If udt1.udtGwsFileRec(i).udtGwsChRec(j).shtChId <> udt2.udtGwsFileRec(i).udtGwsChRec(j).shtChId Then
                        msgSYStemp(ix) = mMsgCreateSys1("chno", udt1.udtGwsFileRec(i).udtGwsChRec(j).shtChId, udt2.udtGwsFileRec(i).udtGwsChRec(j).shtChId, "FILE", Str(i + 1), "ChNo.", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''チャンネルID
                    If udt1.udtGwsFileRec(i).udtGwsChRec(j).shtChNo <> udt2.udtGwsFileRec(i).udtGwsChRec(j).shtChNo Then
                        msgSYStemp(ix) = mMsgCreateSys1("CHID", udt1.udtGwsFileRec(i).udtGwsChRec(j).shtChNo, udt2.udtGwsFileRec(i).udtGwsChRec(j).shtChNo, "FILE", Str(i + 1), "ChNo.", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "FU設定OK"

    Friend Function mCompareSetFuChannelDisp(ByVal udt1 As gTypSetFu, _
                                             ByVal udt2 As gTypSetFu) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　FU SETTING　■■■■"

            For i As Integer = LBound(udt1.udtFu) To UBound(udt1.udtFu)

                ''FU使用/未使用フラグ
                If udt1.udtFu(i).shtUse <> udt2.udtFu(i).shtUse Then
                    msgSYStemp(ix) = mMsgCreateSys1("USE", udt1.udtFu(i).shtUse, udt2.udtFu(i).shtUse, "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''CanBus
                If udt1.udtFu(i).shtCanBus <> udt2.udtFu(i).shtCanBus Then
                    msgSYStemp(ix) = mMsgCreateSys1("CANBUS", udt1.udtFu(i).shtCanBus, udt2.udtFu(i).shtCanBus, "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                For j As Integer = LBound(udt1.udtFu(i).udtSlotInfo) To UBound(udt1.udtFu(i).udtSlotInfo)

                    ''スロット種別
                    If udt1.udtFu(i).udtSlotInfo(j).shtType <> udt2.udtFu(i).udtSlotInfo(j).shtType Then
                        msgSYStemp(ix) = mMsgCreateSys1("TYPE", "0x" & Hex(udt1.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), "0x" & Hex(udt2.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), "FU", Str(i + 1), "SLOT", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''端子台設定  ver1.4.0 2011.07.29
                    If udt1.udtFu(i).udtSlotInfo(j).shtTerinf <> udt2.udtFu(i).udtSlotInfo(j).shtTerinf Then
                        msgSYStemp(ix) = mMsgCreateSys1("TERMINAL", "0x" & Hex(udt1.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), "0x" & Hex(udt2.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), "FU", Str(i + 1), "SLOT", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報データOK"

    Friend Function mCompareSetChDisp(ByVal udt1 As gTypSetChDisp, _
                                      ByVal udt2 As gTypSetChDisp) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　CH INFORMATION DATA　■■■■"

            For i As Integer = LBound(udt1.udtChDisp) To UBound(udt1.udtChDisp)

                ''FCU/FU名称
                If Not gCompareString(udt1.udtChDisp(i).strFuName, udt2.udtChDisp(i).strFuName) Then
                    msgSYStemp(ix) = mMsgCreateSys1("FU_NAME", gGetString(udt1.udtChDisp(i).strFuName), gGetString(udt2.udtChDisp(i).strFuName), "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FCU/FU種類
                If Not gCompareString(udt1.udtChDisp(i).strFuType, udt2.udtChDisp(i).strFuType) Then
                    msgSYStemp(ix) = mMsgCreateSys1("FU_TYPE", gGetString(udt1.udtChDisp(i).strFuType), gGetString(udt2.udtChDisp(i).strFuType), "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''FCU/FU盤名
                If Not gCompareString(udt1.udtChDisp(i).strNamePlate, udt2.udtChDisp(i).strNamePlate) Then
                    msgSYStemp(ix) = mMsgCreateSys1("FU_NAME_PLATE", gGetString(udt1.udtChDisp(i).strNamePlate), gGetString(udt2.udtChDisp(i).strNamePlate), "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''コメント
                If Not gCompareString(udt1.udtChDisp(i).strRemarks, udt2.udtChDisp(i).strRemarks) Then
                    msgSYStemp(ix) = mMsgCreateSys1("REMARKS", gGetString(udt1.udtChDisp(i).strRemarks), gGetString(udt2.udtChDisp(i).strRemarks), "FU", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                For j As Integer = LBound(udt1.udtChDisp(i).udtSlotInfo) To UBound(udt1.udtChDisp(i).udtSlotInfo)

                    For ii As Integer = LBound(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo) To UBound(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo)

                        ''CoreNoIn
                        If Not gCompareString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn) Then
                            msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.1", gGetString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), gGetString(udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''CoreNoCom
                        If Not gCompareString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom) Then
                            msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.2", gGetString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), gGetString(udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''WireMark
                        If Not gCompareString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark) Then
                            msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK1", gGetString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), gGetString(udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''WireMarkClass
                        If Not gCompareString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass) Then
                            msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK2", gGetString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), gGetString(udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''Dest
                        If Not gCompareString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest) Then
                            msgSYStemp(ix) = mMsgCreateSys2("CABLE_DEST", gGetString(udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), gGetString(udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''端子台番号
                        If udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo <> udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo Then
                            msgSYStemp(ix) = mMsgCreateSys2("TERMINAL_No", udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            ix = ix + 1
                        End If

                        ''CHID 
                        'If udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtChid <> udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtChid Then
                        '    msgSYStemp(ix) = mMsgCreateSys2("CHID", udt1.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtChid, udt2.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtChid, "FU", Str(i + 1), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                        '    ix = ix + 1
                        'End If

                    Next ii

                Next j

            Next i

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "端子表比較：FU設定とチャンネル情報データで比較する"

    'Ver2.0.0.1 2016.12.08 端子表比較追加
    Friend Function mCompareTerminalInfo(ByVal udt1Fu As gTypSetFu, ByVal udt1Disp As gTypSetChDisp, _
                                      ByVal udt2Fu As gTypSetFu, ByVal udt2Disp As gTypSetChDisp) As Integer

        Try
            'Ver2.0.2.9 基本設定、基板設定と端子表(ワイヤーマーク)のﾙｰﾌﾟを３つに分ける


            'Ver2.0.0.7 FUadr、項目名の表記を変更
            Dim ix As Integer = 1
            Dim strFUadr As String = ""

            msgSYStemp(0) = "■■■■　Terminal(FCU/FU) DATA　■■■■"

            '>>>ﾙｰﾌﾟ１
            For i As Integer = LBound(udt1Fu.udtFu) To UBound(udt1Fu.udtFu)
                'FU使用/未使用フラグ
                If udt1Fu.udtFu(i).shtUse <> udt2Fu.udtFu(i).shtUse Then
                    'msgSYStemp(ix) = mMsgCreateSys1("USE", udt1Fu.udtFu(i).shtUse, udt2Fu.udtFu(i).shtUse, "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("USE", udt1Fu.udtFu(i).shtUse, udt2Fu.udtFu(i).shtUse, strFUadr)
                    ix = ix + 1
                    'FU使用、未使用ﾌﾗｸﾞが異なる場合、以降を比較してもアレなので、次のデータへ
                    Continue For
                End If

                'CanBus
                If udt1Fu.udtFu(i).shtCanBus <> udt2Fu.udtFu(i).shtCanBus Then
                    'msgSYStemp(ix) = mMsgCreateSys1("CANBUS", udt1Fu.udtFu(i).shtCanBus, udt2Fu.udtFu(i).shtCanBus, "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("CANBUS", udt1Fu.udtFu(i).shtCanBus, udt2Fu.udtFu(i).shtCanBus, strFUadr)
                    ix = ix + 1
                End If

                'FCU/FU名称
                If Not gCompareString(udt1Disp.udtChDisp(i).strFuName, udt2Disp.udtChDisp(i).strFuName) Then
                    'msgSYStemp(ix) = mMsgCreateSys1("FU_NAME", gGetString(udt1Disp.udtChDisp(i).strFuName), gGetString(udt2Disp.udtChDisp(i).strFuName), "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("FCU/FU NAME", gGetString(udt1Disp.udtChDisp(i).strFuName), gGetString(udt2Disp.udtChDisp(i).strFuName), strFUadr)
                    ix = ix + 1
                End If

                'FCU/FU種類
                If Not gCompareString(udt1Disp.udtChDisp(i).strFuType, udt2Disp.udtChDisp(i).strFuType) Then
                    'msgSYStemp(ix) = mMsgCreateSys1("FU_TYPE", gGetString(udt1Disp.udtChDisp(i).strFuType), gGetString(udt2Disp.udtChDisp(i).strFuType), "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("FCU/FU TYPE", gGetString(udt1Disp.udtChDisp(i).strFuType), gGetString(udt2Disp.udtChDisp(i).strFuType), strFUadr)
                    ix = ix + 1
                End If

                'FCU/FU盤名
                If Not gCompareString(udt1Disp.udtChDisp(i).strNamePlate, udt2Disp.udtChDisp(i).strNamePlate) Then
                    'msgSYStemp(ix) = mMsgCreateSys1("FU_NAME_PLATE", gGetString(udt1Disp.udtChDisp(i).strNamePlate), gGetString(udt2Disp.udtChDisp(i).strNamePlate), "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("NAME PLATE", gGetString(udt1Disp.udtChDisp(i).strNamePlate), gGetString(udt2Disp.udtChDisp(i).strNamePlate), strFUadr)
                    ix = ix + 1
                End If

                'コメント
                If Not gCompareString(udt1Disp.udtChDisp(i).strRemarks, udt2Disp.udtChDisp(i).strRemarks) Then
                    'msgSYStemp(ix) = mMsgCreateSys1("REMARKS", gGetString(udt1Disp.udtChDisp(i).strRemarks), gGetString(udt2Disp.udtChDisp(i).strRemarks), "FU", Str(i), "", "")
                    strFUadr = GetFUAdrTer(i, -1, -1)
                    msgSYStemp(ix) = mMsgCreateSysTer("REMARKS", gGetString(udt1Disp.udtChDisp(i).strRemarks), gGetString(udt2Disp.udtChDisp(i).strRemarks), strFUadr)
                    ix = ix + 1
                End If
            Next i

            '>>>ﾙｰﾌﾟ２
            For i As Integer = LBound(udt1Fu.udtFu) To UBound(udt1Fu.udtFu)
                ''FU使用/未使用フラグ
                'If udt1Fu.udtFu(i).shtUse <> udt2Fu.udtFu(i).shtUse Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("USE", udt1Fu.udtFu(i).shtUse, udt2Fu.udtFu(i).shtUse, "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("USE", udt1Fu.udtFu(i).shtUse, udt2Fu.udtFu(i).shtUse, strFUadr)
                '    ix = ix + 1
                '    'FU使用、未使用ﾌﾗｸﾞが異なる場合、以降を比較してもアレなので、次のデータへ
                '    Continue For
                'End If

                ''CanBus
                'If udt1Fu.udtFu(i).shtCanBus <> udt2Fu.udtFu(i).shtCanBus Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("CANBUS", udt1Fu.udtFu(i).shtCanBus, udt2Fu.udtFu(i).shtCanBus, "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("CANBUS", udt1Fu.udtFu(i).shtCanBus, udt2Fu.udtFu(i).shtCanBus, strFUadr)
                '    ix = ix + 1
                'End If

                ''FCU/FU名称
                'If Not gCompareString(udt1Disp.udtChDisp(i).strFuName, udt2Disp.udtChDisp(i).strFuName) Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("FU_NAME", gGetString(udt1Disp.udtChDisp(i).strFuName), gGetString(udt2Disp.udtChDisp(i).strFuName), "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("FCU/FU NAME", gGetString(udt1Disp.udtChDisp(i).strFuName), gGetString(udt2Disp.udtChDisp(i).strFuName), strFUadr)
                '    ix = ix + 1
                'End If

                ''FCU/FU種類
                'If Not gCompareString(udt1Disp.udtChDisp(i).strFuType, udt2Disp.udtChDisp(i).strFuType) Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("FU_TYPE", gGetString(udt1Disp.udtChDisp(i).strFuType), gGetString(udt2Disp.udtChDisp(i).strFuType), "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("FCU/FU TYPE", gGetString(udt1Disp.udtChDisp(i).strFuType), gGetString(udt2Disp.udtChDisp(i).strFuType), strFUadr)
                '    ix = ix + 1
                'End If

                ''FCU/FU盤名
                'If Not gCompareString(udt1Disp.udtChDisp(i).strNamePlate, udt2Disp.udtChDisp(i).strNamePlate) Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("FU_NAME_PLATE", gGetString(udt1Disp.udtChDisp(i).strNamePlate), gGetString(udt2Disp.udtChDisp(i).strNamePlate), "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("NAME PLATE", gGetString(udt1Disp.udtChDisp(i).strNamePlate), gGetString(udt2Disp.udtChDisp(i).strNamePlate), strFUadr)
                '    ix = ix + 1
                'End If

                ''コメント
                'If Not gCompareString(udt1Disp.udtChDisp(i).strRemarks, udt2Disp.udtChDisp(i).strRemarks) Then
                '    'msgSYStemp(ix) = mMsgCreateSys1("REMARKS", gGetString(udt1Disp.udtChDisp(i).strRemarks), gGetString(udt2Disp.udtChDisp(i).strRemarks), "FU", Str(i), "", "")
                '    strFUadr = GetFUAdrTer(i, -1, -1)
                '    msgSYStemp(ix) = mMsgCreateSysTer("REMARKS", gGetString(udt1Disp.udtChDisp(i).strRemarks), gGetString(udt2Disp.udtChDisp(i).strRemarks), strFUadr)
                '    ix = ix + 1
                'End If
                For j As Integer = LBound(udt1Fu.udtFu(i).udtSlotInfo) To UBound(udt1Fu.udtFu(i).udtSlotInfo)

                    'スロット種別
                    'Ver2.0.2.6 TerInfが異なる場合も検出
                    If udt1Fu.udtFu(i).udtSlotInfo(j).shtType <> udt2Fu.udtFu(i).udtSlotInfo(j).shtType _
                        Or _
                        ( _
                            udt1Fu.udtFu(i).udtSlotInfo(j).shtType = udt2Fu.udtFu(i).udtSlotInfo(j).shtType And _
                            udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf <> udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf
                            ) _
                        Then
                        'msgSYStemp(ix) = mMsgCreateSys1("TYPE", "0x" & Hex(udt1Fu.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), "0x" & Hex(udt2Fu.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), "FU", Str(i), "SLOT", Str(j + 1))
                        strFUadr = GetFUAdrTer(i, j + 1, -1)
                        'Ver2.0.1.7 スロット種別はｺｰﾄﾞではなく基板型番へ変更 fnGetFuKATA
                        'msgSYStemp(ix) = mMsgCreateSysTer("TYPE", "0x" & Hex(udt1Fu.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), "0x" & Hex(udt2Fu.udtFu(i).udtSlotInfo(j).shtType).PadLeft(2, "0"), strFUadr)
                        Dim shtPreTerInf As Short = udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf
                        msgSYStemp(ix) = mMsgCreateSysTer("TYPE", _
                                                          fnGetFuKATA(udt1Fu.udtFu(i).udtSlotInfo(j).shtType, udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf, 0), _
                                                          fnGetFuKATA(udt2Fu.udtFu(i).udtSlotInfo(j).shtType, udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf, shtPreTerInf), _
                                                          strFUadr)
                        ix = ix + 1
                        'スロット種別が異なる場合、以降を比較してもアレなので、次のデータへ
                        'Ver2.0.2.9スロット種別が異なっても以降を比較する
                        'Continue For
                    End If

                    'Ver2.0.2.9 スロット種別で検知するため削除
                    '端子台設定
                    'If udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf <> udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf Then
                    '    'msgSYStemp(ix) = mMsgCreateSys1("TERMINAL", "0x" & Hex(udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), "0x" & Hex(udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), "FU", Str(i), "SLOT", Str(j + 1))
                    '    strFUadr = GetFUAdrTer(i, j + 1, -1)
                    '    'Ver2.0.1.9 端子台設定もｺｰﾄﾞではなく型式へ変更
                    '    'msgSYStemp(ix) = mMsgCreateSysTer("TERMINAL", "0x" & Hex(udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), "0x" & Hex(udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf).PadLeft(2, "0"), strFUadr)
                    '    Dim strOldTer As String = ""
                    '    Dim strNewTer As String = ""
                    '    Select Case udt1Fu.udtFu(i).udtSlotInfo(j).shtTerinf
                    '        Case 1
                    '            strOldTer = "TMDO"
                    '        Case 2
                    '            strOldTer = "TMRY"
                    '        Case Else
                    '            strOldTer = "Other"
                    '    End Select
                    '    Select Case udt2Fu.udtFu(i).udtSlotInfo(j).shtTerinf
                    '        Case 1
                    '            strNewTer = "TMDO"
                    '        Case 2
                    '            strNewTer = "TMRY"
                    '        Case Else
                    '            strNewTer = "Other"
                    '    End Select
                    '    msgSYStemp(ix) = mMsgCreateSysTer("TERMINAL", strOldTer, strNewTer, strFUadr)
                    '    ix = ix + 1
                    'End If

                    'Ver2.0.2.1 AI3線式の場合、Pinは、３つでワンセット
                    'Dim blAI3 As Boolean = False
                    'If udt1Fu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_3 Then
                    '    blAI3 = True
                    'End If
                    'Dim intPin As Integer = 0

                    'For ii As Integer = LBound(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo) To UBound(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo)
                    '    If blAI3 = True Then
                    '        '3線式の場合３で割った整数商+1
                    '        intPin = (ii \ 3) + 1
                    '    Else
                    '        intPin = ii + 1
                    '    End If


                    '    'CoreNoIn
                    '    If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn) Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.1(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))

                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("TERM NO(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), strFUadr)
                    '        ix = ix + 1
                    '    End If

                    '    'CoreNoCom
                    '    If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom) Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.2(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("TERM NO(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), strFUadr)
                    '        ix = ix + 1
                    '    End If

                    '    'WireMark
                    '    If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark) Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK1", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("CABLE MARK(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), strFUadr)
                    '        ix = ix + 1
                    '    End If

                    '    'WireMarkClass
                    '    If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass) Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK2(CLASS)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("CABLE CLASS(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), strFUadr)
                    '        ix = ix + 1
                    '    End If

                    '    'Dest
                    '    If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest) Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("CABLE_DEST", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("DIST", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), strFUadr)
                    '        ix = ix + 1
                    '    End If

                    '    '端子台番号
                    '    If udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo <> udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo Then
                    '        'msgSYStemp(ix) = mMsgCreateSys2("TERMINAL_No", udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                    '        strFUadr = GetFUAdrTer(i, j + 1, intPin)
                    '        msgSYStemp(ix) = mMsgCreateSysTer("TERMINAL NO", udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, strFUadr)
                    '        ix = ix + 1
                    '    End If

                    'Next ii

                Next j

            Next i


            '>>>ﾙｰﾌﾟ３
            '端子表ﾙｰﾌﾟ
            For i As Integer = LBound(udt1Fu.udtFu) To UBound(udt1Fu.udtFu)
                For j As Integer = LBound(udt1Fu.udtFu(i).udtSlotInfo) To UBound(udt1Fu.udtFu(i).udtSlotInfo)
                    'Ver2.0.2.1 AI3線式の場合、Pinは、３つでワンセット
                    Dim blAI3 As Boolean = False
                    If udt1Fu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_3 Then
                        blAI3 = True
                    End If
                    Dim intPin As Integer = 0

                    For ii As Integer = LBound(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo) To UBound(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo)
                        If blAI3 = True Then
                            '3線式の場合３で割った整数商+1
                            intPin = (ii \ 3) + 1
                        Else
                            intPin = ii + 1
                        End If


                        'CoreNoIn
                        If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn) Then
                            'msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.1(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))

                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("TERM NO(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn), strFUadr)
                            ix = ix + 1
                        End If

                        'CoreNoCom
                        If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom) Then
                            'msgSYStemp(ix) = mMsgCreateSys2("CORE_ No.2(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("TERM NO(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom), strFUadr)
                            ix = ix + 1
                        End If

                        'WireMark
                        If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark) Then
                            'msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK1", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("CABLE MARK(IN)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMark), strFUadr)
                            ix = ix + 1
                        End If

                        'WireMarkClass
                        If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass) Then
                            'msgSYStemp(ix) = mMsgCreateSys2("CABLE_MARK2(CLASS)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("CABLE CLASS(COM)", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass), strFUadr)
                            ix = ix + 1
                        End If

                        'Dest
                        If Not gCompareString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest) Then
                            'msgSYStemp(ix) = mMsgCreateSys2("CABLE_DEST", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("DIST", gGetString(udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), gGetString(udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).strDest), strFUadr)
                            ix = ix + 1
                        End If

                        '端子台番号
                        If udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo <> udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo Then
                            'msgSYStemp(ix) = mMsgCreateSys2("TERMINAL_No", udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, "FU", Str(i), "SLOT", Str(j + 1), "PIN", Str(ii + 1))
                            strFUadr = GetFUAdrTer(i, j + 1, intPin)
                            msgSYStemp(ix) = mMsgCreateSysTer("TERMINAL NO", udt1Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, udt2Disp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo, strFUadr)
                            ix = ix + 1
                        End If

                    Next ii
                Next j
            Next i


            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    'Ver2.0.1.2 FU Typeをｺｰﾄﾞから型番を戻す関数
    Private Function fnGetFuKATA(pintCode As Integer, pintTer As Integer, shtPreTerInf As Short) As String
        Dim strRet As String = ""

        Select Case pintCode
            Case 1
                'DO
                Select Case pintTer
                    Case 1
                        strRet = "M003A(TMDO)"
                    Case 2
                        strRet = "M003A(TMRYa)"
                    Case 3
                        strRet = "M003A(TMRYb)"
                    Case 4
                        strRet = "M003A(TMRYc)"
                    Case 5
                        strRet = "M003A(TMRYd)"
                    Case 6
                        strRet = "M003A(TMRY-1a)"
                    Case 7
                        strRet = "M003A(TMRY-1b)"
                    Case 8
                        strRet = "M003A(TMRY-1c)"
                    Case 9
                        strRet = "M003A(TMRY-1d)"
                    Case 10
                        strRet = "M003A(TMRY-2a)"
                    Case 11
                        strRet = "M003A(TMRY-2b)"
                    Case 12
                        strRet = "M003A(TMRY-2c)"
                    Case 13
                        strRet = "M003A(TMRY-2d)"
                    Case Else
                        'Ver.2.0.8.P M003A(Select)追加
                        If shtPreTerInf >= 1 And shtPreTerInf < 20 Then '変更前はTMDO～TMRY-2d
                            If pintTer < 0 Or pintTer >= 20 Then '変更後は自由設定
                                strRet = "M003A(Select)"
                            End If
                        Else '変更前も自由設定
                            If shtPreTerInf = 0 Then
                                strRet = "M003A(Select)"
                            Else
                                If pintTer < 0 Or pintTer >= 20 Then '変更後も自由設定
                                    If pintTer <> shtPreTerInf Then
                                        strRet = "DO Term Arrange Change"
                                    End If
                                Else
                                    strRet = "M003A(TMDO)"
                                End If
                            End If
                        End If
                End Select
            Case 2
                strRet = "M002A"    'DI
            Case 3
                strRet = "M030A"    'AO
            Case 4
                strRet = "M100A"    'AI(2線式)
            Case 5
                strRet = "M110A"    'AI(3線式)
            Case 6
                strRet = "M500A"    'AI(1-5V)
            Case 7
                strRet = "M400A"    'AI(4-20mA)
            Case 8
                strRet = "M200A"    'AI(K)
                'Ver2.0.8.1 M200Aにも派生基板
                Select Case pintTer
                    Case 1
                        strRet = "M200A(TMK-1)"
                End Select
        End Select

        Return strRet
    End Function


#End Region

#Region "チャンネル追加/削除"

    Friend Function mCompareSetChannelAddDel(ByVal udt1 As gTypSetChInfo, ByVal udt2 As gTypSetChInfo) As Integer

        Try

            Dim AddCHFlg As Boolean = False
            Dim DelCHFlg As Boolean = False
            Dim z As Integer = 1
            Dim y As Integer = 1

            '追加チャンネルの確認
            For i As Integer = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)

                AddCHFlg = False

                '比較するチャンネル取得
                TargetCHNo = udt2.udtChannel(i).udtChCommon.shtChno

                For j As Integer = LBound(udt1.udtChannel) To UBound(udt1.udtChannel)
                    '比較元からチャンネルを探し出す
                    SourceCHNo = udt1.udtChannel(j).udtChCommon.shtChno

                    '同一チャンネルが存在する場合
                    If TargetCHNo = SourceCHNo Then
                        AddCHFlg = True
                        Exit For
                    End If
                Next

                If AddCHFlg = False Then
                    strAddCH(z) = TargetCHNo
                    z = z + 1
                End If

            Next

            '削除チャンネルの確認
            For x As Integer = LBound(udt1.udtChannel) To UBound(udt1.udtChannel)

                DelCHFlg = False

                '比較するチャンネル取得
                SourceCHNo = udt1.udtChannel(x).udtChCommon.shtChno

                For k As Integer = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)
                    '比較元からチャンネルを探し出す
                    TargetCHNo = udt2.udtChannel(k).udtChCommon.shtChno

                    '同一チャンネルが存在する場合は内容比較処理へ
                    If SourceCHNo = TargetCHNo Then
                        DelCHFlg = True
                        Exit For
                    End If
                Next

                If DelCHFlg = False Then
                    strDellCH(y) = SourceCHNo
                    y = y + 1
                End If

            Next

            '何も追加削除がない場合は表示しない
            If z Or y <> 1 Then
                Call mMsgGridAddDel(z, y)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報"

    Friend Function mCompareSetChannelDisp(ByVal udt1 As gTypSetChInfo, ByVal udt2 As gTypSetChInfo) As Integer

        Try

            Dim EqualityCHFlg As Boolean
            Dim ChSttingFlg As Boolean
            Dim TitleFlg As Boolean = True
            Dim CHChgContFlg As Boolean = False
            Dim SNo As Integer = 0
            Dim x As Integer = 0
            Dim intCommData As Integer
            Dim intDtChgCont As Integer

            msgtemp(0) = "■■■■　CHANNEL INFORMATION　■■■■"

            For TNo As Integer = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)

                EqualityCHFlg = False
                ChSttingFlg = False
                intCommData = 0
                SNo = 0

                '比較するチャンネルとID番号取得
                TargetCHNo = udt2.udtChannel(TNo).udtChCommon.shtChno
                TargetIDNo = udt2.udtChannel(TNo).udtChCommon.shtChid

                If TargetCHNo <> "0" Then

                    For SourceNo As Integer = LBound(udt1.udtChannel) To UBound(udt1.udtChannel)
                        '比較元からチャンネルを探し出す
                        SourceCHNo = udt1.udtChannel(SourceNo).udtChCommon.shtChno
                        SourceIDNo = udt1.udtChannel(SourceNo).udtChCommon.shtChid

                        '同一チャンネルが存在する場合は内容比較処理へ
                        If TargetCHNo = SourceCHNo Then
                            EqualityCHFlg = True
                            SNo = SourceNo
                            Exit For
                        End If
                    Next

                End If

                '同一チャンネルが存在する場合
                If EqualityCHFlg = True Then

                    '========================
                    ''共通設定項目
                    '========================

                    intCommData = mCommonSetChannel(udt1, udt2, SNo, TNo, TargetCHNo)   ''関数により変更項目数を取得
                    intDtChgCont = intCommData

                    '========================
                    ''CH種別毎の設定項目
                    '========================

                    ''チャンネル種別が同じ場合のみ比較／バルブCH、パルス積算CHは、データ種別のくくりが同じ場合のみ比較
                    If udt1.udtChannel(SNo).udtChCommon.shtChType = udt2.udtChannel(TNo).udtChCommon.shtChType Then

                        Select Case udt1.udtChannel(SNo).udtChCommon.shtChType

                            Case gCstCodeChTypeAnalog
                                intDtChgCont = mCompareSetChannelAnalogDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                             'アナログCH

                            Case gCstCodeChTypeDigital
                                If udt1.udtChannel(SNo).udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus Then
                                    intDtChgCont = mCompareSetChannelDigitalDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                        'デジタルCH
                                Else
                                    intDtChgCont = mCompareSetChannelSystemDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                         'システムCH
                                End If

                            Case gCstCodeChTypeMotor
                                intDtChgCont = mCompareSetChannelMotorDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                              'モーターCH

                            Case gCstCodeChTypeValve
                                Select Case udt1.udtChannel(SNo).udtChCommon.shtData                                          'バルブCH

                                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveExt, gCstCodeChDataTypeValveJacom55

                                        If udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveDO Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveExt Then

                                            ''DI-DO
                                            intDtChgCont = mCompareSetChannelValveDiDoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                        End If

                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                        If udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                            ''AI-DO
                                            intDtChgCont = mCompareSetChannelValveAiDoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                        End If

                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20

                                        If (udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20) Then

                                            ''AI-AO
                                            intDtChgCont = mCompareSetChannelValveAiAoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                        End If

                                End Select

                            Case gCstCodeChTypeComposite
                                intDtChgCont = mCompareSetChannelCompositeDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                          'コンポジットCH

                            Case gCstCodeChTypePulse

                                ''Data Typeから、パルスCH or 積算CH の判定をする
                                If udt1.udtChannel(SNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                   udt1.udtChannel(SNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                                    If udt2.udtChannel(TNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                       udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                                        intDtChgCont = mCompareSetChannelPulseDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                      'パルスCH
                                    End If

                                Else
                                    If udt2.udtChannel(TNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                       udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                                    Else
                                        intDtChgCont = mCompareSetChannelRevoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                       '積算CH
                                    End If

                                End If

                            Case gCstCodeChTypePID
                                intDtChgCont = mCompareSetChannelPidDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)
                        End Select

                        ChSttingFlg = True

                    End If

                    '何も変更がない場合は表示しない
                    If intDtChgCont <> 2 Then
                        'Ver2.0.0.2 2016.12.09 関連検索
                        '変更chがある場合、関連検索
                        If ChSttingFlg = True Then
                            'Ver2.0.0.7 関連検索するしないはｵﾌﾟｼｮﾝ
                            If gCompareChkBIG(4) = True Then
                                Call ChkProc(udt1.udtChannel(SNo).udtChCommon.shtChno.ToString, intDtChgCont)
                            End If
                            Call mMsgGrid(intDtChgCont, TitleFlg, strFUAddress, True)
                        Else
                            'Ver2.0.0.7 関連検索するしないはｵﾌﾟｼｮﾝ
                            If gCompareChkBIG(4) = True Then
                                Call ChkProc(udt1.udtChannel(SNo).udtChCommon.shtChno.ToString, intCommData)
                            End If
                            Call mMsgGrid(intCommData, TitleFlg, strFUAddress, True)
                        End If
                        TitleFlg = False
                        CHChgContFlg = True
                    End If

                End If
            Next

            'チャンネル変更箇所がない場合
            If CHChgContFlg = False Then
                Call mMsgGrid(intDtChgCont, TitleFlg, "", False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報 MC"
    'Ver2.0.0.2 MC比較
    Friend Function mCompareSetChannelDisp_MC(ByVal udt1 As gTypSetChInfo, ByVal udt2 As gTypSetChInfo) As Integer

        Try

            Dim EqualityCHFlg As Boolean
            Dim ChSttingFlg As Boolean
            Dim TitleFlg As Boolean = True
            Dim CHChgContFlg As Boolean = True
            Dim SNo As Integer = 0
            Dim TNo As Integer = 0
            Dim x As Integer = 0
            Dim intCommData As Integer
            Dim intDtChgCont As Integer = 0

            Dim cargoNo As Integer = 0

            Dim TargetShareType As Integer
            Dim TargetShareCHno As String = ""
            Dim SourceShareType As Integer
            Dim SourceShareCHno As String = ""

            Dim HikakuCHno As String = ""

            msgtemp(0) = "■■■■　CHANNEL INFORMATION(M/C)　■■■■"

            '相手を探す
            For SNo = LBound(udt1.udtChannel) To UBound(udt1.udtChannel)

                EqualityCHFlg = False
                ChSttingFlg = False
                intCommData = 0
                TNo = 0

                '比較するチャンネルとID番号,ShareType,ShareCHNo取得
                SourceCHNo = udt1.udtChannel(SNo).udtChCommon.shtChno
                SourceIDNo = udt1.udtChannel(SNo).udtChCommon.shtChid
                SourceShareType = udt1.udtChannel(SNo).udtChCommon.shtShareType
                SourceShareCHno = udt1.udtChannel(SNo).udtChCommon.shtShareChid

                '自CHnoか、ShareCHnoかいずれかを格納
                If SourceShareType = 3 Then
                    If SourceShareCHno <> "0" And SourceShareCHno <> "" Then
                        'ShareType 3で、ShareCHnoが格納されている場合のみShareCHnoを格納
                        HikakuCHno = SourceShareCHno
                    Else
                        HikakuCHno = SourceCHNo
                    End If
                Else
                    HikakuCHno = SourceCHNo
                End If

                If HikakuCHno <> "0" Then
                    If SourceShareType = 2 Then
                        For cargoNo = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)
                            'Cargoからチャンネルを探し出す
                            TargetCHNo = udt2.udtChannel(cargoNo).udtChCommon.shtChno
                            TargetIDNo = udt2.udtChannel(cargoNo).udtChCommon.shtChid
                            TargetShareType = udt2.udtChannel(cargoNo).udtChCommon.shtShareType
                            TargetShareCHno = udt2.udtChannel(cargoNo).udtChCommon.shtShareChid

                            'ShareCHで同一チャンネルが存在するか？
                            If TargetShareCHno = HikakuCHno Then
                                EqualityCHFlg = True
                                TNo = cargoNo
                                Exit For
                            End If
                        Next
                    End If
                    If EqualityCHFlg = False Then
                        For cargoNo = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)
                            'Cargoからチャンネルを探し出す
                            TargetCHNo = udt2.udtChannel(cargoNo).udtChCommon.shtChno
                            TargetIDNo = udt2.udtChannel(cargoNo).udtChCommon.shtChid
                            TargetShareType = udt2.udtChannel(cargoNo).udtChCommon.shtShareType
                            TargetShareCHno = udt2.udtChannel(cargoNo).udtChCommon.shtShareChid

                            '同一チャンネルが存在する場合は内容比較処理へ
                            If TargetCHNo = HikakuCHno Then
                                EqualityCHFlg = True
                                TNo = cargoNo
                                Exit For
                            End If
                        Next
                    End If
                End If

                If HikakuCHno = 4224 Then
                    Dim DebugA As Integer = 0
                End If

                If EqualityCHFlg = False Then
                    '相手がいない場合
                    'ShareTypeがゼロじゃないなら、エラーを吐く
                    If SourceShareType <> 0 Then
                        Dim strFUAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtFuno, 1)
                        Dim strPortAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtPortno, 2)
                        Dim strPinAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtPin, 3)
                        strFUAddress = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

                        msgtemp(1) = SourceCHNo
                        msgtemp(2) = mMsgCreateStr("No Data", "ShareType", SourceShareType)
                        intDtChgCont = 3
                        Call mMsgGrid(intDtChgCont, TitleFlg, strFUAddress, True)
                        TitleFlg = False
                    End If
                Else
                    '2,3の組み合わせじゃないならエラーを吐く(合計5じゃないなら)
                    If SourceShareType + TargetShareType <> 5 Then
                        'エラーを吐く
                        Dim strFUAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtFuno, 1)
                        Dim strPortAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtPortno, 2)
                        Dim strPinAdd As String = GetFUAddress(udt1.udtChannel(SNo).udtChCommon.shtPin, 3)
                        strFUAddress = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

                        msgtemp(1) = SourceCHNo
                        msgtemp(2) = mMsgCreateStr("Not Share 2 and 3", "ShareType", SourceShareType)
                        intDtChgCont = 3
                        Call mMsgGrid(intDtChgCont, TitleFlg, strFUAddress, True)
                        TitleFlg = False
                    Else
                        '比較を行う
                        '========================
                        ''共通設定項目
                        '========================
                        intCommData = mCommonSetChannel(udt1, udt2, SNo, TNo, TargetCHNo)
                        intDtChgCont = intCommData

                        '========================
                        ''CH種別毎の設定項目
                        '========================

                        ''チャンネル種別が同じ場合のみ比較／バルブCH、パルス積算CHは、データ種別のくくりが同じ場合のみ比較
                        If udt1.udtChannel(SNo).udtChCommon.shtChType = udt2.udtChannel(TNo).udtChCommon.shtChType Then

                            Select Case udt1.udtChannel(SNo).udtChCommon.shtChType

                                Case gCstCodeChTypeAnalog
                                    intDtChgCont = mCompareSetChannelAnalogDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                             'アナログCH

                                Case gCstCodeChTypeDigital
                                    If udt1.udtChannel(SNo).udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus Then
                                        intDtChgCont = mCompareSetChannelDigitalDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                        'デジタルCH
                                    Else
                                        intDtChgCont = mCompareSetChannelSystemDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                         'システムCH
                                    End If

                                Case gCstCodeChTypeMotor
                                    intDtChgCont = mCompareSetChannelMotorDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                              'モーターCH

                                Case gCstCodeChTypeValve
                                    Select Case udt1.udtChannel(SNo).udtChCommon.shtData                                          'バルブCH

                                        Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveExt, gCstCodeChDataTypeValveJacom55

                                            If udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveDO Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveExt Then

                                                ''DI-DO
                                                intDtChgCont = mCompareSetChannelValveDiDoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                            End If

                                        Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                            If udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                                ''AI-DO
                                                intDtChgCont = mCompareSetChannelValveAiDoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                            End If

                                        Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20

                                            If (udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                               udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20) Then

                                                ''AI-AO
                                                intDtChgCont = mCompareSetChannelValveAiAoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)

                                            End If

                                    End Select

                                Case gCstCodeChTypeComposite
                                    intDtChgCont = mCompareSetChannelCompositeDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                          'コンポジットCH

                                Case gCstCodeChTypePulse

                                    ''Data Typeから、パルスCH or 積算CH の判定をする
                                    If udt1.udtChannel(SNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                       udt1.udtChannel(SNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                                        If udt2.udtChannel(TNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                                            intDtChgCont = mCompareSetChannelPulseDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                      'パルスCH
                                        End If

                                    Else
                                        If udt2.udtChannel(TNo).udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                           udt2.udtChannel(TNo).udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                                        Else
                                            intDtChgCont = mCompareSetChannelRevoDisp(udt1, udt2, SNo, TNo, TargetCHNo, intCommData)                       '積算CH
                                        End If

                                    End If

                            End Select

                            ChSttingFlg = True

                        End If
                        'カウントが2件の場合、それはゼロ件
                        If intDtChgCont > 2 Then
                            Call mMsgGrid(intDtChgCont, TitleFlg, strFUAddress, True)
                            TitleFlg = False
                        End If
                    End If
                End If
            Next

            'Ver2.0.2.8 比較元と比較先を入れ替えて比較
            '比較元がShareType=3で相手がいないときのみエラーを吐く
            For SNo = LBound(udt2.udtChannel) To UBound(udt2.udtChannel)
                EqualityCHFlg = False
                ChSttingFlg = False
                intCommData = 0
                TNo = 0

                '比較するチャンネルとID番号,ShareType,ShareCHNo取得
                SourceCHNo = udt2.udtChannel(SNo).udtChCommon.shtChno
                SourceIDNo = udt2.udtChannel(SNo).udtChCommon.shtChid
                SourceShareType = udt2.udtChannel(SNo).udtChCommon.shtShareType
                SourceShareCHno = udt2.udtChannel(SNo).udtChCommon.shtShareChid

                '自CHnoか、ShareCHnoかいずれかを格納
                'Share＝３の場合のみﾁｪｯｸ
                If SourceShareType = 3 Then
                    If SourceShareCHno <> "0" And SourceShareCHno <> "" Then
                        'ShareType 3で、ShareCHnoが格納されている場合のみShareCHnoを格納
                        HikakuCHno = SourceShareCHno
                    Else
                        HikakuCHno = SourceCHNo
                    End If

                    '相手を探す
                    If HikakuCHno <> "0" Then
                        For cargoNo = LBound(udt1.udtChannel) To UBound(udt1.udtChannel)
                            'Cargoからチャンネルを探し出す
                            TargetCHNo = udt1.udtChannel(cargoNo).udtChCommon.shtChno
                            TargetIDNo = udt1.udtChannel(cargoNo).udtChCommon.shtChid
                            TargetShareType = udt1.udtChannel(cargoNo).udtChCommon.shtShareType
                            TargetShareCHno = udt1.udtChannel(cargoNo).udtChCommon.shtShareChid

                            '同一チャンネルが存在する場合は内容比較処理へ
                            If TargetCHNo = HikakuCHno Then
                                EqualityCHFlg = True
                                TNo = cargoNo
                                Exit For
                            End If
                        Next

                        If EqualityCHFlg = False Then
                            '相手がいない場合
                            'ShareTypeがゼロじゃないなら、エラーを吐く
                            If SourceShareType <> 0 Then
                                Dim strFUAdd As String = GetFUAddress(udt2.udtChannel(SNo).udtChCommon.shtFuno, 1)
                                Dim strPortAdd As String = GetFUAddress(udt2.udtChannel(SNo).udtChCommon.shtPortno, 2)
                                Dim strPinAdd As String = GetFUAddress(udt2.udtChannel(SNo).udtChCommon.shtPin, 3)
                                strFUAddress = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

                                msgtemp(1) = SourceCHNo
                                msgtemp(2) = mMsgCreateStr("No Data", "ShareType", SourceShareType)
                                intDtChgCont = 3
                                Call mMsgGrid(intDtChgCont, TitleFlg, strFUAddress, True)
                                TitleFlg = False
                            End If
                        End If
                    End If
                End If
            Next SNo

            'チャンネル変更箇所がない場合
            If CHChgContFlg = False Then
                Call mMsgGrid(intDtChgCont, TitleFlg, "", False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル共通情報OK"

    Friend Function mCommonSetChannel(ByVal udt1 As gTypSetChInfo, _
                                                 ByVal udt2 As gTypSetChInfo, _
                                                 ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String) As Integer

        Dim i As Integer = 2

        Dim strFUAdd As String
        Dim strPortAdd As String
        Dim strPinAdd As String

        Dim debugFlg As String

        'Ver2.0.2.8 ダミーを値に含めるように変更

        '========================
        ''共通ＣＨ
        '========================

        '比較するチャンネル番号
        msgtemp(1) = CHNo

        'デバック用
        If CHNo = "1641" Then
            debugFlg = CHNo
        End If

        'Ver2.0.6.3 CHID比較復活
        'Ver2.0.6.5 MC比較の場合、CHID比較しない
        'Chid
        If chkMC.Checked = False Then
            If gCompareChk_1_Common(39) = True Then
                If udt1.udtChannel(ich).udtChCommon.shtChid <> udt2.udtChannel(zch).udtChCommon.shtChid Then
                    'Ver2.0.6.5 CHID=255=FFはありえるため表示する
                    msgtemp(i) = mMsgCreateSht_CHID("CHID", udt1.udtChannel(ich).udtChCommon.shtChid, udt2.udtChannel(zch).udtChCommon.shtChid)
                    i = i + 1
                End If
            End If
        End If

        ''グループNo
        If gCompareChk_1_Common(0) = True And chkMC.Checked = False Then 'GroupNo Ver2.0.1.5 MC比較からは外す
            If udt1.udtChannel(ich).udtChCommon.shtGroupNo <> udt2.udtChannel(zch).udtChCommon.shtGroupNo Then
                msgtemp(i) = mMsgCreateSht("Group.No.", udt1.udtChannel(ich).udtChCommon.shtGroupNo, udt2.udtChannel(zch).udtChCommon.shtGroupNo)
                i = i + 1
            End If
        End If

        ''表示位置
        If gCompareChk_1_Common(1) = True And chkMC.Checked = False Then 'DispPos Ver2.0.1.5 MC比較からは外す
            If udt1.udtChannel(ich).udtChCommon.shtDispPos <> udt2.udtChannel(zch).udtChCommon.shtDispPos Then
                msgtemp(i) = mMsgCreateSht("Disp.Pos", udt1.udtChannel(ich).udtChCommon.shtDispPos, udt2.udtChannel(zch).udtChCommon.shtDispPos)
                i = i + 1
            End If
        End If

        'Ver2.0.0.4 表記を数値ではなく文字とする(0=Machinery,1=Cargo,2=Common)
        ''System No
        If gCompareChk_1_Common(2) = True Then 'System
            If udt1.udtChannel(ich).udtChCommon.shtSysno <> udt2.udtChannel(zch).udtChCommon.shtSysno Then
                Dim strSys1 As String = ""
                Dim strSys2 As String = ""
                Select Case udt1.udtChannel(ich).udtChCommon.shtSysno
                    Case 0
                        strSys1 = "Machinery"
                    Case 1
                        strSys1 = "Cargo"
                    Case 2
                        strSys1 = "Common"
                    Case Else
                        strSys1 = udt1.udtChannel(ich).udtChCommon.shtSysno.ToString
                End Select
                Select Case udt2.udtChannel(zch).udtChCommon.shtSysno
                    Case 0
                        strSys2 = "Machinery"
                    Case 1
                        strSys2 = "Cargo"
                    Case 2
                        strSys2 = "Common"
                    Case Else
                        strSys2 = udt2.udtChannel(zch).udtChCommon.shtSysno.ToString
                End Select
                'msgtemp(i) = mMsgCreateSht("SysNo.", udt1.udtChannel(ich).udtChCommon.shtSysno, udt2.udtChannel(zch).udtChCommon.shtSysno)
                msgtemp(i) = mMsgCreateStr("SysNo.", strSys1, strSys2)
                i = i + 1
            End If
        End If

        'CH No
        If gCompareChk_1_Common(3) = True Then 'CH No
            If udt1.udtChannel(ich).udtChCommon.shtChno <> udt2.udtChannel(zch).udtChCommon.shtChno Then
                msgtemp(i) = mMsgCreateSht("ChNo.", udt1.udtChannel(ich).udtChCommon.shtChno, udt2.udtChannel(zch).udtChCommon.shtChno)
                i = i + 1
            End If
        End If

        ''CHアイテム名称
        If gCompareChk_1_Common(4) = True Then 'CH ItemName
            If Not gCompareString(udt1.udtChannel(ich).udtChCommon.strChitem, udt2.udtChannel(zch).udtChCommon.strChitem) Then
                msgtemp(i) = mMsgCreateStr("CH Name", udt1.udtChannel(ich).udtChCommon.strChitem, udt2.udtChannel(zch).udtChCommon.strChitem)
                i = i + 1
            End If
        End If

        ''備考
        If gCompareChk_1_Common(5) = True Then 'Remarks
            If Not gCompareString(udt1.udtChannel(ich).udtChCommon.strRemark, udt2.udtChannel(zch).udtChCommon.strRemark) Then
                msgtemp(i) = mMsgCreateStr("Remarks", udt1.udtChannel(ich).udtChCommon.strRemark, udt2.udtChannel(zch).udtChCommon.strRemark)
                i = i + 1
            End If
        End If

        ''延長警報グループ
        If gCompareChk_1_Common(6) = True Then 'ExtGroup
            If (udt1.udtChannel(ich).udtChCommon.shtExtGroup <> udt2.udtChannel(zch).udtChCommon.shtExtGroup) Or _
                (udt1.udtChannel(ich).DummyCommonExtGroup <> udt2.udtChannel(zch).DummyCommonExtGroup) Then
                'msgtemp(i) = mMsgCreateSht("EX", udt1.udtChannel(ich).udtChCommon.shtExtGroup, udt2.udtChannel(zch).udtChCommon.shtExtGroup)
                msgtemp(i) = mMsgCreateint("EX", udt1.udtChannel(ich).udtChCommon.shtExtGroup, udt2.udtChannel(zch).udtChCommon.shtExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup)
                i = i + 1
            End If
        End If

        ''ディレイタイマ値
        If gCompareChk_1_Common(7) = True Then 'Delay
            If (udt1.udtChannel(ich).udtChCommon.shtDelay <> udt2.udtChannel(zch).udtChCommon.shtDelay) Or _
                (udt1.udtChannel(ich).DummyCommonDelay <> udt2.udtChannel(zch).DummyCommonDelay) Then
                'msgtemp(i) = mMsgCreateSht("DLY", udt1.udtChannel(ich).udtChCommon.shtDelay, udt2.udtChannel(zch).udtChCommon.shtDelay)
                msgtemp(i) = mMsgCreateint("DLY", udt1.udtChannel(ich).udtChCommon.shtDelay, udt2.udtChannel(zch).udtChCommon.shtDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyCommonDelay, udt2.udtChannel(zch).DummyCommonDelay)
                i = i + 1
            End If
        End If

        ''グループリポーズ１
        If gCompareChk_1_Common(8) = True Then 'GR1
            If (udt1.udtChannel(ich).udtChCommon.shtGRepose1 <> udt2.udtChannel(zch).udtChCommon.shtGRepose1) Or _
                (udt1.udtChannel(ich).DummyCommonGroupRepose1 <> udt2.udtChannel(zch).DummyCommonGroupRepose1) Then
                'msgtemp(i) = mMsgCreateSht("GR1", udt1.udtChannel(ich).udtChCommon.shtGRepose1, udt2.udtChannel(zch).udtChCommon.shtGRepose1)
                msgtemp(i) = mMsgCreateint("GR1", udt1.udtChannel(ich).udtChCommon.shtGRepose1, udt2.udtChannel(zch).udtChCommon.shtGRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1)
                i = i + 1
            End If
        End If

        ''グループリポーズ２
        If gCompareChk_1_Common(9) = True Then 'GR2
            If (udt1.udtChannel(ich).udtChCommon.shtGRepose2 <> udt2.udtChannel(zch).udtChCommon.shtGRepose2) Or _
                (udt1.udtChannel(ich).DummyCommonGroupRepose2 <> udt2.udtChannel(zch).DummyCommonGroupRepose2) Then
                'msgtemp(i) = mMsgCreateSht("GR2", udt1.udtChannel(ich).udtChCommon.shtGRepose2, udt2.udtChannel(zch).udtChCommon.shtGRepose2)
                msgtemp(i) = mMsgCreateint("GR2", udt1.udtChannel(ich).udtChCommon.shtGRepose2, udt2.udtChannel(zch).udtChCommon.shtGRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2)
                i = i + 1
            End If
        End If

        ''マニュアルリポーズ状態
        If gCompareChk_1_Common(10) = True Then 'M Repose
            If udt1.udtChannel(ich).udtChCommon.shtM_Repose <> udt2.udtChannel(zch).udtChCommon.shtM_Repose Then
                msgtemp(i) = mMsgCreateSht("MR Status", udt1.udtChannel(ich).udtChCommon.shtM_Repose, udt2.udtChannel(zch).udtChCommon.shtM_Repose)
                i = i + 1
            End If
        End If

        ''CH Type
        If gCompareChk_1_Common(11) = True Then 'CH Type
            If udt1.udtChannel(ich).udtChCommon.shtChType <> udt2.udtChannel(zch).udtChCommon.shtChType Then
                'Ver2.0.2.9 結果表示はコードではなく名称にする
                Dim strOldChName As String = GetChTypeName(udt1.udtChannel(ich).udtChCommon.shtChType)
                Dim strNewChName As String = GetChTypeName(udt2.udtChannel(zch).udtChCommon.shtChType)
                'msgtemp(i) = mMsgCreateSht("CH Type", udt1.udtChannel(ich).udtChCommon.shtChType, udt2.udtChannel(zch).udtChCommon.shtChType)
                msgtemp(i) = mMsgCreateStr("CH Type", strOldChName, strNewChName)
                i = i + 1
            End If
        End If

        ''データ種別コード
        If gCompareChk_1_Common(12) = True Then 'Data Type
            'Ver2.0.0.5 データ種別は、shtDataが同じだが、shtSignalが異なると異なる場合があるため比較方法修正
            'If udt1.udtChannel(ich).udtChCommon.shtData <> udt2.udtChannel(zch).udtChCommon.shtData Then

            '    Dim strOldTypeCode As String = ""
            '    Dim strNewTypeCode As String = ""

            '    strOldTypeCode = GetTypeCode(udt1.udtChannel(ich).udtChCommon.shtData, udt1.udtChannel(ich).udtChCommon.shtChType, udt1.udtChannel(ich).udtChCommon.shtSignal)
            '    strNewTypeCode = GetTypeCode(udt2.udtChannel(zch).udtChCommon.shtData, udt2.udtChannel(zch).udtChCommon.shtChType, udt2.udtChannel(zch).udtChCommon.shtSignal)

            '    msgtemp(i) = mMsgCreateStr("Data Type", strOldTypeCode, strNewTypeCode)
            '    i = i + 1
            'End If
            Dim strOldTypeCode As String = ""
            Dim strNewTypeCode As String = ""
            strOldTypeCode = GetTypeCode(udt1.udtChannel(ich).udtChCommon.shtData, udt1.udtChannel(ich).udtChCommon.shtChType, udt1.udtChannel(ich).udtChCommon.shtSignal)
            strNewTypeCode = GetTypeCode(udt2.udtChannel(zch).udtChCommon.shtData, udt2.udtChannel(zch).udtChCommon.shtChType, udt2.udtChannel(zch).udtChCommon.shtSignal)
            If strOldTypeCode <> strNewTypeCode Then
                msgtemp(i) = mMsgCreateStr("Data Type", strOldTypeCode, strNewTypeCode)
                i = i + 1
            End If
        End If

        ''単位種別コード
        If gCompareChk_1_Common(13) = True Then 'Unit Type
            If (udt1.udtChannel(ich).udtChCommon.shtUnit <> udt2.udtChannel(zch).udtChCommon.shtUnit) Or _
                Not gCompareString(udt1.udtChannel(ich).udtChCommon.strUnit, udt2.udtChannel(zch).udtChCommon.strUnit) Or _
                (udt1.udtChannel(ich).DummyCommonUnitName <> udt2.udtChannel(zch).DummyCommonUnitName) Then

                Dim strOldUnit As String = ""
                Dim strNewUnit As String = ""

                Dim strOldManu As String = ""
                Dim strNewManu As String = ""

                Dim strOldValue As String = ""
                Dim strNewValue As String = ""

                Dim strOldDmy As String = ""
                Dim strNewDmy As String = ""

                strOldUnit = GetCHUnit(udt1.udtChannel(ich).udtChCommon.shtUnit)
                strNewUnit = GetCHUnit(udt2.udtChannel(zch).udtChCommon.shtUnit)

                strOldManu = NZf(udt1.udtChannel(ich).udtChCommon.strUnit)
                strNewManu = NZf(udt2.udtChannel(zch).udtChCommon.strUnit)

                If strOldUnit = "" Then
                    strOldValue = strOldManu
                Else
                    strOldValue = strOldUnit
                End If

                If strNewUnit = "" Then
                    strNewValue = strNewManu
                Else
                    strNewValue = strNewUnit
                End If

                If udt1.udtChannel(ich).DummyCommonUnitName = True Then
                    strOldDmy = "#"
                End If
                If udt2.udtChannel(zch).DummyCommonUnitName = True Then
                    strNewDmy = "#"
                End If


                'msgtemp(i) = mMsgCreateStr("UNIT", strOldUnit, strNewUnit)
                msgtemp(i) = mMsgCreateStr("UNIT", strOldDmy & strOldValue, strNewDmy & strNewValue)
                i = i + 1
            End If
        End If

        ''動作設定１ ダミーCH
        If gCompareChk_1_Common(14) = True Then 'Dummy
            i = GetBitCHK("Dummy", udt1.udtChannel(ich).udtChCommon.shtFlag1, 0, udt2.udtChannel(zch).udtChCommon.shtFlag1, 0, i)
        End If

        ''動作設定１ 隠しCH
        If gCompareChk_1_Common(15) = True And chkMC.Checked = False Then 'SC   MC比較時は比較から外す
            i = GetBitCHK("SC", udt1.udtChannel(ich).udtChCommon.shtFlag1, 1, udt2.udtChannel(zch).udtChCommon.shtFlag1, 1, i)
        End If

        ''動作設定１ ワークCH
        If gCompareChk_1_Common(16) = True Then 'WK
            i = GetBitCHK("WK", udt1.udtChannel(ich).udtChCommon.shtFlag1, 2, udt2.udtChannel(zch).udtChCommon.shtFlag1, 2, i)
        End If

        ''動作設定１ 分/秒切替
        If gCompareChk_1_Common(17) = True Then 'DLY
            i = GetBitCHK("DLY UNIT", udt1.udtChannel(ich).udtChCommon.shtFlag1, 3, udt2.udtChannel(zch).udtChCommon.shtFlag1, 3, i)
        End If

        ''プログラマブルコントローラーCH
        If gCompareChk_1_Common(18) = True Then 'PLC
            i = GetBitCHK("PLC CH", udt1.udtChannel(ich).udtChCommon.shtFlag1, 4, udt2.udtChannel(zch).udtChCommon.shtFlag1, 4, i)
        End If

        ''POWER FACTOR
        If gCompareChk_1_Common(19) = True Then 'PF
            i = GetBitCHK("PWR FACTOR", udt1.udtChannel(ich).udtChCommon.shtFlag1, 5, udt2.udtChannel(zch).udtChCommon.shtFlag1, 5, i)
        End If

        'Ver2.0.0.0 2016.12.07 比較追加2件
        ''mmHg
        If gCompareChk_1_Common(20) = True Then 'mmHg
            i = GetBitCHK("mmHg", udt1.udtChannel(ich).udtChCommon.shtFlag1, 6, udt2.udtChannel(zch).udtChCommon.shtFlag1, 6, i)
        End If

        ''cmHg
        If gCompareChk_1_Common(21) = True Then 'cmHg
            i = GetBitCHK("cmHg", udt1.udtChannel(ich).udtChCommon.shtFlag1, 7, udt2.udtChannel(zch).udtChCommon.shtFlag1, 7, i)
        End If


        ''P/S表示     Ver1.11.9.3 2016.11.26 Ver2.0.0.0 比較先の参照ﾋﾞｯﾄの数値が6＝異なるのを8へ修正
        If gCompareChk_1_Common(22) = True Then 'P/S Display
            i = GetBitCHK("P/S Display", udt1.udtChannel(ich).udtChCommon.shtFlag1, 8, udt2.udtChannel(zch).udtChCommon.shtFlag1, 8, i)
            'Ver2.0.7.9 A/F対応
            i = GetBitCHK("A/F Display", udt1.udtChannel(ich).udtChCommon.shtFlag1, 9, udt2.udtChannel(zch).udtChCommon.shtFlag1, 9, i)
        End If

        ''動作設定２ 定時ログ印字
        If gCompareChk_1_Common(23) = True Then 'RL
            i = GetBitCHK("RL", udt1.udtChannel(ich).udtChCommon.shtFlag2, 0, udt2.udtChannel(zch).udtChCommon.shtFlag2, 0, i)
        End If

        ''動作設定２ アラーム印字
        If gCompareChk_1_Common(24) = True And chkMC.Checked = False Then 'AL Ver2.0.1.5 MC比較から外す
            i = GetBitCHK("AL", udt1.udtChannel(ich).udtChCommon.shtFlag2, 1, udt2.udtChannel(zch).udtChCommon.shtFlag2, 1, i)
        End If

        ''動作設定２ イベントプリント印字
        If gCompareChk_1_Common(25) = True Then 'EP
            i = GetBitCHK("EP", udt1.udtChannel(ich).udtChCommon.shtFlag2, 2, udt2.udtChannel(zch).udtChCommon.shtFlag2, 2, i)
        End If

        ''動作設定２ アラームキャンセル設定
        If gCompareChk_1_Common(26) = True And chkMC.Checked = False Then 'AC MC比較時は比較から外す
            i = GetBitCHK("AC", udt1.udtChannel(ich).udtChCommon.shtFlag2, 3, udt2.udtChannel(zch).udtChCommon.shtFlag2, 3, i)
        End If

        ''動作設定２ 打分印字ＰＣ１ →　Data設定のLOCKに変更　Ver2.0.8.7 2018.08.10
        If gCompareChk_1_Common(27) = True Then 'LOCK
            i = GetBitCHK("LOCK", udt1.udtChannel(ich).udtChCommon.shtFlag2, 4, udt2.udtChannel(zch).udtChCommon.shtFlag2, 4, i)
        End If

        ''動作設定２ 打分印字ＰＣ２ →　MOTOR COLORに変更　ver2.0.8.C 2018.11.14
        If gCompareChk_1_Common(28) = True Then 'PC2
            i = GetBitCHK("MOTOR COLOR", udt1.udtChannel(ich).udtChCommon.shtFlag2, 5, udt2.udtChannel(zch).udtChCommon.shtFlag2, 5, i)
        End If

        ''ステータス種別コード        '' Ver1.11.5 2016.08.30  入力ｽﾃｰﾀｽと統合
        If (udt1.udtChannel(ich).udtChCommon.shtStatus <> udt2.udtChannel(zch).udtChCommon.shtStatus) Or _
            Not gCompareString(udt1.udtChannel(ich).udtChCommon.strStatus, udt2.udtChannel(zch).udtChCommon.strStatus) Or _
            (udt1.udtChannel(ich).DummyCommonStatusName <> udt2.udtChannel(zch).DummyCommonStatusName) Then

            Dim strOldStatus As String = ""
            Dim strNewStatus As String = ""

            Dim strOldDumy As String = ""
            Dim strNewDumy As String = ""

            If udt1.udtChannel(ich).DummyCommonStatusName = True Then
                strOldDumy = "#"
            End If
            If udt2.udtChannel(zch).DummyCommonStatusName = True Then
                strNewDumy = "#"
            End If


            '' Ver1.11.6 2016.09.15  ｽﾃｰﾀｽ変化ﾁｪｯｸ
            'Ver2.0.2.4 モーターの場合、ｽﾃｰﾀｽ0x14(20)は0x30へ置き換えて比較
            If gCompareChk_1_Common(29) = True Then 'STATUS
                Dim shtStatus As Short = 0
                If ChkStatusChange(udt1.udtChannel(ich).udtChCommon.shtStatus, udt2.udtChannel(zch).udtChCommon.shtStatus) = True Then
                    If udt1.udtChannel(ich).udtChCommon.shtStatus = &HFF Then     '' 入力ｽﾃｰﾀｽ
                        If udt1.udtChannel(ich).udtChCommon.shtChType = gCstCodeChTypePulse Then    '' Ver2.0.8.8 積算CH 2018.09.03
                            If gGetString(udt1.udtChannel(ich).udtChCommon.strStatus) <> "" Then
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    strOldStatus = "正常/" & gGetString(MidB(udt1.udtChannel(ich).udtChCommon.strStatus, 0, 8))
                                Else
                                    strOldStatus = "NOR/" & gGetString(MidB(udt1.udtChannel(ich).udtChCommon.strStatus, 0, 8))
                                End If

                            End If
                        Else
                            'Ver2.0.7.M (保安庁)日本語対応
                            If gGetString(udt1.udtChannel(ich).udtChCommon.strStatus) <> "" Then
                                strOldStatus = gGetString(MidB(udt1.udtChannel(ich).udtChCommon.strStatus, 0, 8)) & "/" & gGetString(MidB(udt1.udtChannel(ich).udtChCommon.strStatus, 8, 8))
                            End If
                        End If

                    Else
                        If udt1.udtChannel(ich).udtChCommon.shtChType = gCstCodeChTypeMotor Then
                            shtStatus = udt1.udtChannel(ich).udtChCommon.shtStatus
                            If shtStatus = 20 Then
                                shtStatus = &H30
                            End If
                        Else
                            shtStatus = udt1.udtChannel(ich).udtChCommon.shtStatus
                        End If
                        strOldStatus = GetChStatus(shtStatus)
                    End If

                        If udt2.udtChannel(zch).udtChCommon.shtChno = 1990 Then
                            Dim DebugA As Integer = 0
                        End If

                    If udt2.udtChannel(zch).udtChCommon.shtStatus = &HFF Then     '' 入力ｽﾃｰﾀｽ
                        If udt2.udtChannel(zch).udtChCommon.shtChType = gCstCodeChTypePulse Then    '' Ver2.0.8.8 積算CH 2018.09.03
                            If gGetString(udt2.udtChannel(zch).udtChCommon.strStatus) <> "" Then
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    strNewStatus = "正常/" & gGetString(MidB(udt2.udtChannel(zch).udtChCommon.strStatus, 0, 8))
                                Else
                                    strNewStatus = "NOR/" & gGetString(MidB(udt2.udtChannel(zch).udtChCommon.strStatus, 0, 8))
                                End If

                            End If
                        Else
                            If gGetString(udt2.udtChannel(zch).udtChCommon.strStatus) <> "" Then
                                'Ver2.0.7.M (保安庁)日本語対応
                                strNewStatus = gGetString(MidB(udt2.udtChannel(zch).udtChCommon.strStatus, 0, 8)) & "/" & gGetString(MidB(udt2.udtChannel(zch).udtChCommon.strStatus, 8, 8))
                            End If
                        End If

                    Else
                        If udt2.udtChannel(zch).udtChCommon.shtChType = gCstCodeChTypeMotor Then
                            shtStatus = udt2.udtChannel(zch).udtChCommon.shtStatus
                            If shtStatus = 20 Then
                                shtStatus = &H30
                            End If
                        Else
                            shtStatus = udt2.udtChannel(zch).udtChCommon.shtStatus
                        End If
                        strNewStatus = GetChStatus(shtStatus)
                    End If
                        If strOldStatus <> strNewStatus Then
                            msgtemp(i) = mMsgCreateStr("STATUS", strOldDumy & strOldStatus, strNewDumy & strNewStatus)
                            i = i + 1
                        End If
                    End If
            End If
        End If

        '' Ver1.11.5 2016.08.17 FUｱﾄﾞﾚｽﾁｪｯｸ変更
        'Ver2.0.0.2 MC比較の場合と通常の場合で分岐
        'Ver2.0.1.5 MCｱﾄﾞﾚｽ比較は廃止
        If chkMC.Checked = True Then
            'MCの比較
            'MCｱﾄﾞﾚｽ比較の仕様
            'Share側にはFUadrが入ってても良い
            'Remoto側にFUadrが入っていてはならない。(FFと0なら良い)
            'If chkADR.Checked = True Then
            '    'アドレス比較する場合
            '    '通常の比較と同じで良い
            '    If gCompareChk_1_Common(30) = True Then 'FU Adr
            '        If udt1.udtChannel(ich).udtChCommon.shtFuno <> udt2.udtChannel(zch).udtChCommon.shtFuno Or _
            '            udt1.udtChannel(ich).udtChCommon.shtPortno <> udt2.udtChannel(zch).udtChCommon.shtPortno Or _
            '            udt1.udtChannel(ich).udtChCommon.shtPin <> udt2.udtChannel(zch).udtChCommon.shtPin Then
            '            msgtemp(i) = mMsgFUAdd(udt1.udtChannel(ich).udtChCommon, udt2.udtChannel(zch).udtChCommon)
            '            i = i + 1
            '        End If
            '    End If
            'Else
            '    'アドレス比較しない場合
            '    '両方が入力済ならエラー
            '    If (udt1.udtChannel(ich).udtChCommon.shtPortno <> 65535 Or udt1.udtChannel(ich).udtChCommon.shtPin <> 65535) And _
            '        (udt2.udtChannel(zch).udtChCommon.shtPortno <> 65535 Or udt2.udtChannel(zch).udtChCommon.shtPin <> 65535) Then
            '        msgtemp(i) = mMsgCreateStr("FU Adress", "Error", "Has been entered")
            '        i = i + 1
            '    End If
            'End If
            '
            'Remote = 2
            Dim intPort As Integer = 0
            Dim intPin As Integer = 0
            Dim strMYchNo As String = ""
            Dim strFileName As String = ""
            If udt1.udtChannel(ich).udtChCommon.shtShareType = 2 Then
                intPort = udt1.udtChannel(ich).udtChCommon.shtPortno
                intPin = udt1.udtChannel(ich).udtChCommon.shtPin
                strMYchNo = "CH " & udt1.udtChannel(ich).udtChCommon.shtChno
                strFileName = txtSourceFile.Text
                If ((intPort <> 65535 And intPort <> 0) Or (intPin <> 65535 And intPin <> 0)) Then
                    msgtemp(i) = mMsgCreateStr("FU Adress", "(" & strFileName & ")" & strMYchNo, "RemoteCH have FU Adress.")
                    i = i + 1
                End If
            End If
            If udt2.udtChannel(zch).udtChCommon.shtShareType = 2 Then
                intPort = udt2.udtChannel(zch).udtChCommon.shtPortno
                intPin = udt2.udtChannel(zch).udtChCommon.shtPin
                strMYchNo = "CH " & udt2.udtChannel(zch).udtChCommon.shtChno
                strFileName = txtTargetFile.Text
                If ((intPort <> 65535 And intPort <> 0) Or (intPin <> 65535 And intPin <> 0)) Then
                    msgtemp(i) = mMsgCreateStr("FU Adress", "(" & strFileName & ")" & strMYchNo, "RemoteCH have FU Adress.")
                    i = i + 1
                End If
            End If
        Else
            '通常の比較
            If gCompareChk_1_Common(30) = True Then 'FU Adr
                If (udt1.udtChannel(ich).udtChCommon.shtFuno <> udt2.udtChannel(zch).udtChCommon.shtFuno Or _
                    udt1.udtChannel(ich).udtChCommon.shtPortno <> udt2.udtChannel(zch).udtChCommon.shtPortno Or _
                    udt1.udtChannel(ich).udtChCommon.shtPin <> udt2.udtChannel(zch).udtChCommon.shtPin) Or _
                    (udt1.udtChannel(ich).DummyCommonFuAddress <> udt2.udtChannel(zch).DummyCommonFuAddress) Then
                    msgtemp(i) = mMsgFUAdd(udt1.udtChannel(ich).udtChCommon, udt2.udtChannel(zch).udtChCommon _
                                           , udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress)
                    i = i + 1
                End If
            End If
        End If


        '' ''入力FU番号
        ''If udt1.udtChannel(ich).udtChCommon.shtFuno <> udt2.udtChannel(zch).udtChCommon.shtFuno Then
        ''    msgtemp(i) = mMsgCreateSht("FU No.", udt1.udtChannel(ich).udtChCommon.shtFuno, udt2.udtChannel(zch).udtChCommon.shtFuno)
        ''    i = i + 1
        ''End If

        '' ''入力FUポート番号
        ''If udt1.udtChannel(ich).udtChCommon.shtPortno <> udt2.udtChannel(zch).udtChCommon.shtPortno Then
        ''    msgtemp(i) = mMsgCreateSht("FU Slot", udt1.udtChannel(ich).udtChCommon.shtPortno, udt2.udtChannel(zch).udtChCommon.shtPortno)
        ''    i = i + 1
        ''End If

        '' ''入力FU計測点番号
        ''If udt1.udtChannel(ich).udtChCommon.shtPin <> udt2.udtChannel(zch).udtChCommon.shtPin Then
        ''    msgtemp(i) = mMsgCreateSht("FU Pin", udt1.udtChannel(ich).udtChCommon.shtPin, udt2.udtChannel(zch).udtChCommon.shtPin)
        ''    i = i + 1
        ''End If

        ''計測点個数
        If gCompareChk_1_Common(31) = True Then 'FU Count
            If (udt1.udtChannel(ich).udtChCommon.shtPinNo <> udt2.udtChannel(zch).udtChCommon.shtPinNo) Or _
                (udt1.udtChannel(ich).DummyCommonPinNo <> udt2.udtChannel(zch).DummyCommonPinNo) Then
                'msgtemp(i) = mMsgCreateSht("FU Count", udt1.udtChannel(ich).udtChCommon.shtPinNo, udt2.udtChannel(zch).udtChCommon.shtPinNo)
                msgtemp(i) = mMsgCreateint("FU Count", udt1.udtChannel(ich).udtChCommon.shtPinNo, udt2.udtChannel(zch).udtChCommon.shtPinNo, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyCommonPinNo, udt2.udtChannel(zch).DummyCommonPinNo)
                i = i + 1
            End If
        End If

        ''延長警報盤ECC入出力機能種別コード
        If gCompareChk_1_Common(32) = True Then 'ECC Func
            If udt1.udtChannel(ich).udtChCommon.shtEccFunc <> udt2.udtChannel(zch).udtChCommon.shtEccFunc Then
                msgtemp(i) = mMsgCreateStr("ECC Func", "0x" & Hex(udt1.udtChannel(ich).udtChCommon.shtEccFunc).PadLeft(2, "0"), "0x" & Hex(udt2.udtChannel(zch).udtChCommon.shtEccFunc).PadLeft(2, "0"))
                i = i + 1
            End If
        End If

        ''SIOポート使用有無
        If gCompareChk_1_Common(33) = True Then 'SIO
            If udt1.udtChannel(ich).udtChCommon.shtOutPort <> udt2.udtChannel(zch).udtChCommon.shtOutPort Then
                msgtemp(i) = mMsgCreateSht("SIO", udt1.udtChannel(ich).udtChCommon.shtOutPort, udt2.udtChannel(zch).udtChCommon.shtOutPort)
                i = i + 1
            End If
        End If

        ''GWSポート使用有無
        If gCompareChk_1_Common(34) = True Then 'GWS
            If udt1.udtChannel(ich).udtChCommon.shtGwsPort <> udt2.udtChannel(zch).udtChCommon.shtGwsPort Then
                msgtemp(i) = mMsgCreateSht("GWS", udt1.udtChannel(ich).udtChCommon.shtGwsPort, udt2.udtChannel(zch).udtChCommon.shtGwsPort)
                i = i + 1
            End If
        End If

        'Ver2.0.1.9 単位比較はﾏﾆｭｱﾙも既存も統一とする
        ''単位種別名称
        'If gCompareChk_1_Common(35) = True Then 'UNIT Name
        '    If Not gCompareString(udt1.udtChannel(ich).udtChCommon.strUnit, udt2.udtChannel(zch).udtChCommon.strUnit) Then
        '        msgtemp(i) = mMsgCreateStr("UNIT", gGetString(udt1.udtChannel(ich).udtChCommon.strUnit), gGetString(udt2.udtChannel(zch).udtChCommon.strUnit))
        '        i = i + 1
        '    End If
        'End If

        '' Ver1.11.5 2016.08.30 ｽﾃｰﾀｽｺｰﾄﾞに統合
        '' ''ステータス名称
        ''If Not gCompareString(udt1.udtChannel(ich).udtChCommon.strStatus, udt2.udtChannel(zch).udtChCommon.strStatus) Then
        ''    msgtemp(i) = mMsgCreateStr("STATUS", gGetString(udt1.udtChannel(ich).udtChCommon.strStatus), gGetString(udt2.udtChannel(zch).udtChCommon.strStatus))
        ''    i = i + 1
        ''End If

        ''共通CH Local/Remote設定
        If gCompareChk_1_Common(36) = True And chkMC.Checked = False Then 'Share CH Type MC比較時は、比較から外す
            If udt1.udtChannel(ich).udtChCommon.shtShareType <> udt2.udtChannel(zch).udtChCommon.shtShareType Then
                msgtemp(i) = mMsgCreateSht("Share CH Type", udt1.udtChannel(ich).udtChCommon.shtShareType, udt2.udtChannel(zch).udtChCommon.shtShareType)
                i = i + 1
            End If
        End If

        ''リモートCH No
        If gCompareChk_1_Common(37) = True And chkMC.Checked = False Then 'Share CH No MC比較時は、比較から外す
            If udt1.udtChannel(ich).udtChCommon.shtShareChid <> udt2.udtChannel(zch).udtChCommon.shtShareChid Then
                msgtemp(i) = mMsgCreateSht("Share CH No.", udt1.udtChannel(ich).udtChCommon.shtShareChid, udt2.udtChannel(zch).udtChCommon.shtShareChid)
                i = i + 1
            End If
        End If

        ''マニュアルリポーズ
        If gCompareChk_1_Common(38) = True Then 'MR Set
            If udt1.udtChannel(ich).udtChCommon.shtM_ReposeSet <> udt2.udtChannel(zch).udtChCommon.shtM_ReposeSet Then
                msgtemp(i) = mMsgCreateSht("MR Set", udt1.udtChannel(ich).udtChCommon.shtM_ReposeSet, udt2.udtChannel(zch).udtChCommon.shtM_ReposeSet)
                i = i + 1
            End If
        End If

        '' Ver1.11.5 2016.08.30  ﾃﾞｰﾀ種類に含まれるのでｺﾒﾝﾄ
        ''入力信号
        ''If udt1.udtChannel(ich).udtChCommon.shtSignal <> udt2.udtChannel(zch).udtChCommon.shtSignal Then
        ''    msgtemp(i) = mMsgCreateSht("Analog Type", udt1.udtChannel(ich).udtChCommon.shtSignal, udt2.udtChannel(zch).udtChCommon.shtSignal)
        ''    i = i + 1
        ''End If
        ''//

        '比較元FUアドレス保持
        strFUAdd = GetFUAddress(udt1.udtChannel(ich).udtChCommon.shtFuno, 1)
        strPortAdd = GetFUAddress(udt1.udtChannel(ich).udtChCommon.shtPortno, 2)
        strPinAdd = GetFUAddress(udt1.udtChannel(ich).udtChCommon.shtPin, 3)

        strFUAddress = strFUAdd & "-" & strPortAdd & "-" & strPinAdd

        'Ver2.0.1.5 MC比較時FUadrが無い場合は、もう片方からのｱﾄﾞﾚｽとする
        If chkMC.Checked = True Then
            If strFUAddress = "--" Or strFUAddress = "0-0-0" Or IsNothing(strFUAddress) Then
                'C側
                strFUAdd = GetFUAddress(udt2.udtChannel(zch).udtChCommon.shtFuno, 1)
                strPortAdd = GetFUAddress(udt2.udtChannel(zch).udtChCommon.shtPortno, 2)
                strPinAdd = GetFUAddress(udt2.udtChannel(zch).udtChCommon.shtPin, 3)
                strFUAddress = strFUAdd & "-" & strPortAdd & "-" & strPinAdd & "[" & txtTargetFile.Text.Substring(0, 1) & "]"
            Else
                'M側
                strFUAddress = strFUAddress & "[" & txtSourceFile.Text.Substring(0, 1) & "]"
            End If
        End If


        '変更項目数を返す
        mCommonSetChannel = i

    End Function

#End Region

#Region "チャンネル情報(アナログ)OK"

    Friend Function mCompareSetChannelAnalogDisp(ByVal udt1 As gTypSetChInfo, _
                                                 ByVal udt2 As gTypSetChInfo, _
                                                 ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値比較と混ぜる(#をつける)

            Dim i As Integer = Gyo

            '========================
            ''アナログＣＨ
            '========================

            'Ver2.0.0.5 SET値
            ' SET値は、無し＝０で、無しなのかゼロなのかが検知できないため
            ' 両方0の場合、Useの変化で検知させる

            ''アラーム　HH ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_2_Analog(0) = True Then 'Alarm HH Use
                If udt1.udtChannel(ich).AnalogHiHiUse <> udt2.udtChannel(zch).AnalogHiHiUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH Use", udt1.udtChannel(ich).AnalogHiHiUse, udt2.udtChannel(zch).AnalogHiHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_2_Analog(1) = True Then 'Alarm HH DLY
                If (udt1.udtChannel(ich).AnalogHiHiDelay <> udt2.udtChannel(zch).AnalogHiHiDelay) Or _
                    (udt1.udtChannel(ich).DummyDelayHH <> udt2.udtChannel(zch).DummyDelayHH) Then
                    'msgtemp(i) = mMsgCreateSht("Alarm HH DLY", udt1.udtChannel(ich).AnalogHiHiDelay, udt2.udtChannel(zch).AnalogHiHiDelay)
                    msgtemp(i) = mMsgCreateint("Alarm HH DLY", udt1.udtChannel(ich).AnalogHiHiDelay, udt2.udtChannel(zch).AnalogHiHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH)
                    i = i + 1
                End If
            End If

            ''アラームセット値  '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定統合
            If gCompareChk_2_Analog(2) = True Then 'Alarm HH SET
                If (udt1.udtChannel(ich).AnalogHiHiValue <> udt2.udtChannel(zch).AnalogHiHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueHH <> udt2.udtChannel(zch).DummyValueHH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH SET", udt1.udtChannel(ich).AnalogHiHiValue, udt2.udtChannel(zch).AnalogHiHiValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).AnalogHiHiValue = 0) And _
                    (udt2.udtChannel(zch).AnalogHiHiValue = 0) Then
                    If udt1.udtChannel(ich).AnalogHiHiUse <> udt2.udtChannel(zch).AnalogHiHiUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm HH SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).AnalogHiHiUse, _
                                                   "USE:" & udt2.udtChannel(zch).AnalogHiHiUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_2_Analog(3) = True Then 'Alarm HH EX
                If (udt1.udtChannel(ich).AnalogHiHiExtGroup <> udt2.udtChannel(zch).AnalogHiHiExtGroup) Or _
                    (udt1.udtChannel(ich).DummyExtGrHH <> udt2.udtChannel(zch).DummyExtGrHH) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm HH EX", udt1.udtChannel(ich).AnalogHiHiExtGroup, udt2.udtChannel(zch).AnalogHiHiExtGroup)
                    msgtemp(i) = mMsgCreateint("Alarm HH EX", udt1.udtChannel(ich).AnalogHiHiExtGroup, udt2.udtChannel(zch).AnalogHiHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_2_Analog(4) = True Then 'Alarm HH GR1
                If (udt1.udtChannel(ich).AnalogHiHiGroupRepose1 <> udt2.udtChannel(zch).AnalogHiHiGroupRepose1) Or _
                    (udt1.udtChannel(ich).DummyGRep1HH <> udt2.udtChannel(zch).DummyGRep1HH) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm HH GR1", udt1.udtChannel(ich).AnalogHiHiGroupRepose1, udt2.udtChannel(zch).AnalogHiHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("Alarm HH GR1", udt1.udtChannel(ich).AnalogHiHiGroupRepose1, udt2.udtChannel(zch).AnalogHiHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_2_Analog(5) = True Then 'Alarm HH GR2
                If udt1.udtChannel(ich).AnalogHiHiGroupRepose2 <> udt2.udtChannel(zch).AnalogHiHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2HH <> udt2.udtChannel(zch).DummyGRep2HH) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm HH GR2", udt1.udtChannel(ich).AnalogHiHiGroupRepose2, udt2.udtChannel(zch).AnalogHiHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("Alarm HH GR2", udt1.udtChannel(ich).AnalogHiHiGroupRepose2, udt2.udtChannel(zch).AnalogHiHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_2_Analog(6) = True Then 'Alarm HH STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).AnalogHiHiStatusInput), NZfS(udt2.udtChannel(zch).AnalogHiHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmHH <> udt2.udtChannel(zch).DummyStaNmHH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmHH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmHH, "#", "")

                    msgtemp(i) = mMsgCreateStr("Alarm HH STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).AnalogHiHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).AnalogHiHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_2_Analog(7) = True Then 'Alarm HH MR Status
                If udt1.udtChannel(ich).AnalogHiHiManualReposeState <> udt2.udtChannel(zch).AnalogHiHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH MR Status", udt1.udtChannel(ich).AnalogHiHiManualReposeState, udt2.udtChannel(zch).AnalogHiHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_2_Analog(8) = True Then 'Alarm HH MR Set
                If udt1.udtChannel(ich).AnalogHiHiManualReposeSet <> udt2.udtChannel(zch).AnalogHiHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH MR Set", udt1.udtChannel(ich).AnalogHiHiManualReposeSet, udt2.udtChannel(zch).AnalogHiHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　H ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_2_Analog(9) = True Then 'Alarm H Use
                If udt1.udtChannel(ich).AnalogHiUse <> udt2.udtChannel(zch).AnalogHiUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm H Use", udt1.udtChannel(ich).AnalogHiUse, udt2.udtChannel(zch).AnalogHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_2_Analog(10) = True Then 'Alarm H DLY
                If udt1.udtChannel(ich).AnalogHiDelay <> udt2.udtChannel(zch).AnalogHiDelay Or _
                    (udt1.udtChannel(ich).DummyDelayH <> udt2.udtChannel(zch).DummyDelayH) Then
                    'msgtemp(i) = mMsgCreateSht("Alarm H DLY", udt1.udtChannel(ich).AnalogHiDelay, udt2.udtChannel(zch).AnalogHiDelay)
                    msgtemp(i) = mMsgCreateint("Alarm H DLY", udt1.udtChannel(ich).AnalogHiDelay, udt2.udtChannel(zch).AnalogHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定統合
            If gCompareChk_2_Analog(11) = True Then 'Alarm H SET
                If (udt1.udtChannel(ich).AnalogHiValue <> udt2.udtChannel(zch).AnalogHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueH <> udt2.udtChannel(zch).DummyValueH) Then
                    msgtemp(i) = mMsgCreateint("Alarm H SET", udt1.udtChannel(ich).AnalogHiValue, udt2.udtChannel(zch).AnalogHiValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).AnalogHiValue = 0) And _
                    (udt2.udtChannel(zch).AnalogHiValue = 0) Then
                    If udt1.udtChannel(ich).AnalogHiUse <> udt2.udtChannel(zch).AnalogHiUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm H SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).AnalogHiUse, _
                                                   "USE:" & udt2.udtChannel(zch).AnalogHiUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_2_Analog(12) = True Then 'Alarm H EX
                If udt1.udtChannel(ich).AnalogHiExtGroup <> udt2.udtChannel(zch).AnalogHiExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrH <> udt2.udtChannel(zch).DummyExtGrH) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm H EX", udt1.udtChannel(ich).AnalogHiExtGroup, udt2.udtChannel(zch).AnalogHiExtGroup)
                    msgtemp(i) = mMsgCreateint("Alarm H EX", udt1.udtChannel(ich).AnalogHiExtGroup, udt2.udtChannel(zch).AnalogHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_2_Analog(13) = True Then 'Alarm H GR1
                If udt1.udtChannel(ich).AnalogHiGroupRepose1 <> udt2.udtChannel(zch).AnalogHiGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1H <> udt2.udtChannel(zch).DummyGRep1H) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm H GR1", udt1.udtChannel(ich).AnalogHiGroupRepose1, udt2.udtChannel(zch).AnalogHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("Alarm H GR1", udt1.udtChannel(ich).AnalogHiGroupRepose1, udt2.udtChannel(zch).AnalogHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_2_Analog(14) = True Then 'Alarm H GR2
                If udt1.udtChannel(ich).AnalogHiGroupRepose2 <> udt2.udtChannel(zch).AnalogHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2H <> udt2.udtChannel(zch).DummyGRep2H) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm H GR2", udt1.udtChannel(ich).AnalogHiGroupRepose2, udt2.udtChannel(zch).AnalogHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("Alarm H GR2", udt1.udtChannel(ich).AnalogHiGroupRepose2, udt2.udtChannel(zch).AnalogHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_2_Analog(15) = True Then 'Alarm H Status
                If Not gCompareString(NZfS(udt1.udtChannel(ich).AnalogHiStatusInput), NZfS(udt2.udtChannel(zch).AnalogHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmH <> udt2.udtChannel(zch).DummyStaNmH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmH, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm H Status", strOldDmy & gGetString(udt1.udtChannel(ich).AnalogHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).AnalogHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_2_Analog(16) = True Then 'Alarm H MR Status
                If udt1.udtChannel(ich).AnalogHiManualReposeState <> udt2.udtChannel(zch).AnalogHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm H MR Status", udt1.udtChannel(ich).AnalogHiManualReposeState, udt2.udtChannel(zch).AnalogHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_2_Analog(17) = True Then 'Alarm H MR Set
                If udt1.udtChannel(ich).AnalogHiManualReposeSet <> udt2.udtChannel(zch).AnalogHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm H MR Set", udt1.udtChannel(ich).AnalogHiManualReposeSet, udt2.udtChannel(zch).AnalogHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　L ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_2_Analog(18) = True Then 'Alarm L Use
                If udt1.udtChannel(ich).AnalogLoUse <> udt2.udtChannel(zch).AnalogLoUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm L Use", udt1.udtChannel(ich).AnalogLoUse, udt2.udtChannel(zch).AnalogLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_2_Analog(19) = True Then 'Alarm L DLY
                If udt1.udtChannel(ich).AnalogLoDelay <> udt2.udtChannel(zch).AnalogLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayL <> udt2.udtChannel(zch).DummyDelayL) Then
                    'msgtemp(i) = mMsgCreateSht("Alarm L DLY", udt1.udtChannel(ich).AnalogLoDelay, udt2.udtChannel(zch).AnalogLoDelay)
                    msgtemp(i) = mMsgCreateint("Alarm L DLY", udt1.udtChannel(ich).AnalogLoDelay, udt2.udtChannel(zch).AnalogLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定統合
            If gCompareChk_2_Analog(20) = True Then 'Alarm L SET
                If (udt1.udtChannel(ich).AnalogLoValue <> udt2.udtChannel(zch).AnalogLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueL <> udt2.udtChannel(zch).DummyValueL) Then
                    msgtemp(i) = mMsgCreateint("Alarm L SET", udt1.udtChannel(ich).AnalogLoValue, udt2.udtChannel(zch).AnalogLoValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).AnalogLoValue = 0) And _
                    (udt2.udtChannel(zch).AnalogLoValue = 0) Then
                    If udt1.udtChannel(ich).AnalogLoUse <> udt2.udtChannel(zch).AnalogLoUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm L SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).AnalogLoUse, _
                                                   "USE:" & udt2.udtChannel(zch).AnalogLoUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_2_Analog(21) = True Then 'Alarm L EX
                If udt1.udtChannel(ich).AnalogLoExtGroup <> udt2.udtChannel(zch).AnalogLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrL <> udt2.udtChannel(zch).DummyExtGrL) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm L EX", udt1.udtChannel(ich).AnalogLoExtGroup, udt2.udtChannel(zch).AnalogLoExtGroup)
                    msgtemp(i) = mMsgCreateint("Alarm L EX", udt1.udtChannel(ich).AnalogLoExtGroup, udt2.udtChannel(zch).AnalogLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_2_Analog(22) = True Then 'Alarm L GR1
                If udt1.udtChannel(ich).AnalogLoGroupRepose1 <> udt2.udtChannel(zch).AnalogLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1L <> udt2.udtChannel(zch).DummyGRep1L) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm L GR1", udt1.udtChannel(ich).AnalogLoGroupRepose1, udt2.udtChannel(zch).AnalogLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("Alarm L GR1", udt1.udtChannel(ich).AnalogLoGroupRepose1, udt2.udtChannel(zch).AnalogLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_2_Analog(23) = True Then 'Alarm L GR2
                If udt1.udtChannel(ich).AnalogLoGroupRepose2 <> udt2.udtChannel(zch).AnalogLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2L <> udt2.udtChannel(zch).DummyGRep2L) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm L GR2", udt1.udtChannel(ich).AnalogLoGroupRepose2, udt2.udtChannel(zch).AnalogLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("Alarm L GR2", udt1.udtChannel(ich).AnalogLoGroupRepose2, udt2.udtChannel(zch).AnalogLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_2_Analog(24) = True Then 'Alarm L STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).AnalogLoStatusInput), NZfS(udt2.udtChannel(zch).AnalogLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmL <> udt2.udtChannel(zch).DummyStaNmL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmL, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm L STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).AnalogLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).AnalogLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_2_Analog(25) = True Then 'Alarm L MR Status
                If udt1.udtChannel(ich).AnalogLoManualReposeState <> udt2.udtChannel(zch).AnalogLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm L MR Status", udt1.udtChannel(ich).AnalogLoManualReposeState, udt2.udtChannel(zch).AnalogLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_2_Analog(26) = True Then 'Alarm L MR Set
                If udt1.udtChannel(ich).AnalogLoManualReposeSet <> udt2.udtChannel(zch).AnalogLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm L MR Set", udt1.udtChannel(ich).AnalogLoManualReposeSet, udt2.udtChannel(zch).AnalogLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　LL ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_2_Analog(27) = True Then 'Alarm LL Use
                If udt1.udtChannel(ich).AnalogLoLoUse <> udt2.udtChannel(zch).AnalogLoLoUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL Use", udt1.udtChannel(ich).AnalogLoLoUse, udt2.udtChannel(zch).AnalogLoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_2_Analog(28) = True Then 'Alarm LL DLY
                If udt1.udtChannel(ich).AnalogLoLoDelay <> udt2.udtChannel(zch).AnalogLoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayLL <> udt2.udtChannel(zch).DummyDelayLL) Then
                    'msgtemp(i) = mMsgCreateSht("Alarm LL DLY", udt1.udtChannel(ich).AnalogLoLoDelay, udt2.udtChannel(zch).AnalogLoLoDelay)
                    msgtemp(i) = mMsgCreateint("Alarm LL DLY", udt1.udtChannel(ich).AnalogLoLoDelay, udt2.udtChannel(zch).AnalogLoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定統合
            If gCompareChk_2_Analog(29) = True Then 'Alarm LL SET
                If (udt1.udtChannel(ich).AnalogLoLoValue <> udt2.udtChannel(zch).AnalogLoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueLL <> udt2.udtChannel(zch).DummyValueLL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL SET", udt1.udtChannel(ich).AnalogLoLoValue, udt2.udtChannel(zch).AnalogLoLoValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).AnalogLoLoValue = 0) And _
                    (udt2.udtChannel(zch).AnalogLoLoValue = 0) Then
                    If udt1.udtChannel(ich).AnalogLoLoUse <> udt2.udtChannel(zch).AnalogLoLoUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm LL SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).AnalogLoLoUse, _
                                                   "USE:" & udt2.udtChannel(zch).AnalogLoLoUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_2_Analog(30) = True Then 'Alarm LL EX
                If udt1.udtChannel(ich).AnalogLoLoExtGroup <> udt2.udtChannel(zch).AnalogLoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrLL <> udt2.udtChannel(zch).DummyExtGrLL) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm LL EX", udt1.udtChannel(ich).AnalogLoLoExtGroup, udt2.udtChannel(zch).AnalogLoLoExtGroup)
                    msgtemp(i) = mMsgCreateint("Alarm LL EX", udt1.udtChannel(ich).AnalogLoLoExtGroup, udt2.udtChannel(zch).AnalogLoLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_2_Analog(31) = True Then 'Alarm LL GR1
                If udt1.udtChannel(ich).AnalogLoLoGroupRepose1 <> udt2.udtChannel(zch).AnalogLoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1LL <> udt2.udtChannel(zch).DummyGRep1LL) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm LL GR1", udt1.udtChannel(ich).AnalogLoLoGroupRepose1, udt2.udtChannel(zch).AnalogLoLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("Alarm LL GR1", udt1.udtChannel(ich).AnalogLoLoGroupRepose1, udt2.udtChannel(zch).AnalogLoLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_2_Analog(32) = True Then 'Alarm LL GR2
                If udt1.udtChannel(ich).AnalogLoLoGroupRepose2 <> udt2.udtChannel(zch).AnalogLoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2LL <> udt2.udtChannel(zch).DummyGRep2LL) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm LL GR2", udt1.udtChannel(ich).AnalogLoLoGroupRepose2, udt2.udtChannel(zch).AnalogLoLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("Alarm LL GR2", udt1.udtChannel(ich).AnalogLoLoGroupRepose2, udt2.udtChannel(zch).AnalogLoLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_2_Analog(33) = True Then 'Alarm LL STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).AnalogLoLoStatusInput), NZfS(udt2.udtChannel(zch).AnalogLoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmLL <> udt2.udtChannel(zch).DummyStaNmLL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmLL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmLL, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm LL STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).AnalogLoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).AnalogLoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_2_Analog(34) = True Then 'Alarm LL MR Status
                If udt1.udtChannel(ich).AnalogLoLoManualReposeState <> udt2.udtChannel(zch).AnalogLoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL MR Status", udt1.udtChannel(ich).AnalogLoLoManualReposeState, udt2.udtChannel(zch).AnalogLoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_2_Analog(35) = True Then 'Alarm LL MR Set
                If udt1.udtChannel(ich).AnalogLoLoManualReposeSet <> udt2.udtChannel(zch).AnalogLoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL MR Set", udt1.udtChannel(ich).AnalogLoLoManualReposeSet, udt2.udtChannel(zch).AnalogLoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　SF  ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_2_Analog(36) = True Then 'Alarm S Use
                If udt1.udtChannel(ich).AnalogSensorFailUse <> udt2.udtChannel(zch).AnalogSensorFailUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm S Use", udt1.udtChannel(ich).AnalogSensorFailUse, udt2.udtChannel(zch).AnalogSensorFailUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_2_Analog(37) = True Then 'Alarm S DLY
                If udt1.udtChannel(ich).AnalogSensorFailDelay <> udt2.udtChannel(zch).AnalogSensorFailDelay Or _
                    (udt1.udtChannel(ich).DummyDelaySF <> udt2.udtChannel(zch).DummyDelaySF) Then
                    'msgtemp(i) = mMsgCreateSht("Alarm S DLY", udt1.udtChannel(ich).AnalogSensorFailDelay, udt2.udtChannel(zch).AnalogSensorFailDelay)
                    msgtemp(i) = mMsgCreateint("Alarm S DLY", udt1.udtChannel(ich).AnalogSensorFailDelay, udt2.udtChannel(zch).AnalogSensorFailDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_2_Analog(38) = True Then 'Alarm S SET
                If udt1.udtChannel(ich).AnalogSensorFailValue <> udt2.udtChannel(zch).AnalogSensorFailValue Then
                    msgtemp(i) = mMsgCreateint("Alarm S SET", udt1.udtChannel(ich).AnalogSensorFailValue, udt2.udtChannel(zch).AnalogSensorFailValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               False, False)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).AnalogSensorFailValue = 0) And _
                    (udt2.udtChannel(zch).AnalogSensorFailValue = 0) Then
                    If udt1.udtChannel(ich).AnalogSensorFailUse <> udt2.udtChannel(zch).AnalogSensorFailUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm S SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).AnalogSensorFailUse, _
                                                   "USE:" & udt2.udtChannel(zch).AnalogSensorFailUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_2_Analog(39) = True Then 'Alarm S EX
                If udt1.udtChannel(ich).AnalogSensorFailExtGroup <> udt2.udtChannel(zch).AnalogSensorFailExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrSF <> udt2.udtChannel(zch).DummyExtGrSF) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm S EX", udt1.udtChannel(ich).AnalogSensorFailExtGroup, udt2.udtChannel(zch).AnalogSensorFailExtGroup)
                    msgtemp(i) = mMsgCreateint("Alarm S EX", udt1.udtChannel(ich).AnalogSensorFailExtGroup, udt2.udtChannel(zch).AnalogSensorFailExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_2_Analog(40) = True Then 'Alarm S GR1
                If udt1.udtChannel(ich).AnalogSensorFailGroupRepose1 <> udt2.udtChannel(zch).AnalogSensorFailGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1SF <> udt2.udtChannel(zch).DummyGRep1SF) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm S GR1", udt1.udtChannel(ich).AnalogSensorFailGroupRepose1, udt2.udtChannel(zch).AnalogSensorFailGroupRepose1)
                    msgtemp(i) = mMsgCreateint("Alarm S GR1", udt1.udtChannel(ich).AnalogSensorFailGroupRepose1, udt2.udtChannel(zch).AnalogSensorFailGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_2_Analog(41) = True Then 'Alarm S GR2
                If udt1.udtChannel(ich).AnalogSensorFailGroupRepose2 <> udt2.udtChannel(zch).AnalogSensorFailGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2SF <> udt2.udtChannel(zch).DummyGRep2SF) Then
                    'msgtemp(i) = mMsgCreateByt("Alarm S GR2", udt1.udtChannel(ich).AnalogSensorFailGroupRepose2, udt2.udtChannel(zch).AnalogSensorFailGroupRepose2)
                    msgtemp(i) = mMsgCreateint("Alarm S GR2", udt1.udtChannel(ich).AnalogSensorFailGroupRepose2, udt2.udtChannel(zch).AnalogSensorFailGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_2_Analog(42) = True Then 'Alarm S STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).AnalogSensorFailStatusInput), NZfS(udt2.udtChannel(zch).AnalogSensorFailStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmSF <> udt2.udtChannel(zch).DummyStaNmSF) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmSF, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmSF, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm S STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).AnalogSensorFailStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).AnalogSensorFailStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_2_Analog(43) = True Then 'Alarm S MR Status
                If udt1.udtChannel(ich).AnalogSensorFailManualReposeState <> udt2.udtChannel(zch).AnalogSensorFailManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm S MR Status", udt1.udtChannel(ich).AnalogSensorFailManualReposeState, udt2.udtChannel(zch).AnalogSensorFailManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_2_Analog(44) = True Then 'Alarm S MR Set
                If udt1.udtChannel(ich).AnalogSensorFailManualReposeSet <> udt2.udtChannel(zch).AnalogSensorFailManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm S MR Set", udt1.udtChannel(ich).AnalogSensorFailManualReposeSet, udt2.udtChannel(zch).AnalogSensorFailManualReposeSet)
                    i = i + 1
                End If
            End If
            ''-----------------------------------------------------------------------------------------------

            ''スケール値　上限/下限値      '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_2_Analog(45) = True Then 'RANGE
                i = GetRangeAnalog("RANGE", udt1.udtChannel(ich), udt2.udtChannel(zch), udt1.udtChannel(ich).AnalogRangeHigh, udt2.udtChannel(zch).AnalogRangeHigh, _
                             udt1.udtChannel(ich).AnalogRangeLow, udt2.udtChannel(zch).AnalogRangeLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)
            End If

            ''ノーマルレンジ　上限/下限     '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_2_Analog(46) = True Then 'NOR RANGE
                i = GetNorRange("NOR RANGE", udt1.udtChannel(ich).AnalogNormalHigh, udt2.udtChannel(zch).AnalogNormalHigh, _
                             udt1.udtChannel(ich).AnalogNormalLow, udt2.udtChannel(zch).AnalogNormalLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, _
                             udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)
            End If

            ''オフセット値        '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_2_Analog(47) = True Then 'OFFSET
                If udt1.udtChannel(ich).AnalogOffsetValue <> udt2.udtChannel(zch).AnalogOffsetValue Then
                    msgtemp(i) = mMsgCreateint("OFFSET", udt1.udtChannel(ich).AnalogOffsetValue, udt2.udtChannel(zch).AnalogOffsetValue, _
                                               udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                                               False, False)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_2_Analog(48) = True Then 'String
                If udt1.udtChannel(ich).AnalogString <> udt2.udtChannel(zch).AnalogString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).AnalogString, udt2.udtChannel(zch).AnalogString)
                    i = i + 1
                End If
            End If

            ''小数点以下桁数
            If gCompareChk_2_Analog(49) = True Then 'Decimal Point
                If udt1.udtChannel(ich).AnalogDecimalPosition <> udt2.udtChannel(zch).AnalogDecimalPosition Then
                    msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition)
                    i = i + 1
                End If
            End If

            ''センター表示
            If gCompareChk_2_Analog(50) = True Then 'BAR GRAPH CENTER
                i = GetBitCHK("BAR GRAPH CENTER", udt1.udtChannel(ich).AnalogDisplay3, 0, udt2.udtChannel(zch).AnalogDisplay3, 0, i)
            End If

            ''Sensor異常表示
            If gCompareChk_2_Analog(51) = True Then 'SENSOR ALM(UNDER)
                i = GetBitCHK("SENSOR ALM(UNDER)", udt1.udtChannel(ich).AnalogDisplay3, 1, udt2.udtChannel(zch).AnalogDisplay3, 1, i)   '' Ver1.11.9.8 2016.12.15 ﾋﾞｯﾄ位置 2 → 1
            End If
            If gCompareChk_2_Analog(52) = True Then 'SENSOR ALM(OVER)
                i = GetBitCHK("SENSOR ALM(OVER)", udt1.udtChannel(ich).AnalogDisplay3, 2, udt2.udtChannel(zch).AnalogDisplay3, 2, i)    '' Ver1.11.9.8 2016.12.15 ﾋﾞｯﾄ位置 3 → 2
            End If

            ''レンジ種別
            'Ver2.0.1.9 DEL
            'If gCompareChk_2_Analog(53) = True Then 'PT Type
            '    If udt1.udtChannel(ich).AnalogRangeType <> udt2.udtChannel(zch).AnalogRangeType Then
            '        msgtemp(i) = mMsgCreateSht("PT Type(RANGE Changed)", udt1.udtChannel(ich).AnalogRangeType, udt2.udtChannel(zch).AnalogRangeType)
            '        i = i + 1
            '    End If
            'End If

            ''タグ名称
            If gCompareChk_2_Analog(54) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).AnalogTagNo, udt2.udtChannel(zch).AnalogTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).AnalogTagNo), gGetString(udt2.udtChannel(zch).AnalogTagNo))
                    i = i + 1
                End If
            End If

            ''LR Mode
            If gCompareChk_2_Analog(55) = True Then 'LR Mode
                If Not gCompareString(udt1.udtChannel(ich).AnalogLRMode, udt2.udtChannel(zch).AnalogLRMode) Then

                    Dim strOldLRMode As String = ""
                    Dim strNewLRMode As String = ""

                    strOldLRMode = GetMode(udt1.udtChannel(ich).udtChCommon.shtStatus)
                    strNewLRMode = GetMode(udt2.udtChannel(zch).udtChCommon.shtStatus)

                    msgtemp(i) = mMsgCreateStr("LR Mode", strOldLRMode, strNewLRMode)
                    i = i + 1

                End If
            End If

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            ''MimicNo
            If gCompareChk_2_Analog(57) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).AnalogAlmMimic <> udt2.udtChannel(zch).AnalogAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).AnalogAlmMimic, udt2.udtChannel(zch).AnalogAlmMimic)
                    i = i + 1
                End If
            End If

            'Ver2.0.7.G アナログアジャストパスワード対応
            If gCompareChk_2_Analog(57) = True Then
                If udt1.udtChannel(ich).AnalogAdjstPsw <> udt2.udtChannel(zch).AnalogAdjstPsw Then
                    msgtemp(i) = mMsgCreateSht("Adjust Password", udt1.udtChannel(ich).AnalogAdjstPsw, udt2.udtChannel(zch).AnalogAdjstPsw)
                    i = i + 1
                End If
            End If


            'ダミー設定
            If gCompareChk_2_Analog(56) = True Then 'Dummy Setting
                ''i = GetDummy("DMY ALM Value HH", udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH, i)     Ver1.11.6 2016.09.14 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT HH", udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH, i)
                'i = GetDummy("DMY ALM DLY HH", udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH, i)
                'i = GetDummy("DMY ALM GP RP1 HH", udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH, i)
                'i = GetDummy("DMY ALM GP RP2 HH", udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH, i)
                ''i = GetDummy("DMY ALM Value H", udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH, i)        Ver1.11.6 2016.09.14 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT GP H", udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH, i)
                'i = GetDummy("DMY ALM DLY H", udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH, i)
                'i = GetDummy("DMY ALM GP RP1 H", udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H, i)
                'i = GetDummy("DMY ALM GP RP2 H", udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H, i)
                ''i = GetDummy("DMY ALM Value L", udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL, i)        Ver1.11.6 2016.09.14 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT GP L", udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL, i)
                'i = GetDummy("DMY ALM DLY L", udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL, i)
                'i = GetDummy("DMY ALM GP RP1 L", udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L, i)
                'i = GetDummy("DMY ALM GP RP2 L", udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L, i)
                ''i = GetDummy("DMY ALM Value LL", udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL, i)     Ver1.11.6 2016.09.14 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT GP LL", udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL, i)
                'i = GetDummy("DMY ALM DLY LL", udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL, i)
                'i = GetDummy("DMY ALM GP RP1 LL", udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL, i)
                'i = GetDummy("DMY ALM GP RP2 LL", udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL, i)
                'i = GetDummy("DMY ALM EXT GP SF", udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF, i)
                'i = GetDummy("DMY ALM DLY SF", udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF, i)
                'i = GetDummy("DMY ALM GP RP1 SF", udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF, i)
                'i = GetDummy("DMY ALM GP RP2 SF", udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF, i)

                'Ver2.0.0.5 FU AddressのDummy比較追加
                'i = GetDummy("DMY FU Address", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)

                ''i = GetDummy("DMY RANGE Scale", udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)    Ver1.11.6 2016.09.14 ﾚﾝｼﾞﾁｪｯｸに統合
                'i = GetDummy("DMY NOR Range H", udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, i)
                'i = GetDummy("DMY NOR Range L", udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)

                'i = GetDummy("DMY ALM SP1", udt1.udtChannel(ich).DummySp1, udt2.udtChannel(zch).DummySp1, i)
                'i = GetDummy("DMY ALM SP2", udt1.udtChannel(ich).DummySp2, udt2.udtChannel(zch).DummySp2, i)
                'i = GetDummy("DMY ALM Hys Open", udt1.udtChannel(ich).DummyHysOpen, udt2.udtChannel(zch).DummyHysOpen, i)
                'i = GetDummy("DMY ALM Hys Close", udt1.udtChannel(ich).DummyHysClose, udt2.udtChannel(zch).DummyHysClose, i)
                'i = GetDummy("DMY ALM Sampling Time", udt1.udtChannel(ich).DummySmpTime, udt2.udtChannel(zch).DummySmpTime, i)
            End If

            '変更項目数を返す
            mCompareSetChannelAnalogDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(デジタル)OK"

    Friend Function mCompareSetChannelDigitalDisp(ByVal udt1 As gTypSetChInfo, _
                                                  ByVal udt2 As gTypSetChInfo, _
                                                  ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更
            'Ver2.0.4.7 比較先がシステムの場合は比較ﾃﾞｰﾀ変更
            Dim blSYS As Boolean = False
            Dim intAlm As Integer
            Dim intFilter As Integer
            Dim strTag As String
            Dim intFire As Integer
            With udt2.udtChannel(zch)
                If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                    'システム
                    blSYS = True
                    intAlm = .SystemUse
                    intFilter = 0
                    strTag = .SystemTagNo
                    intFire = .SystemAlmMimic
                Else
                    'デジタル
                    blSYS = False
                    intAlm = .DigitalUse
                    intFilter = .DigitalDiFilter
                    strTag = .DigitalTagNo
                    intFire = .DigitalAlmMimic
                End If
            End With
            '========================
            ''デジタルＣＨ
            '========================

            Dim i As Integer = Gyo

            ''アラーム有無        '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_3_Digital(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).DigitalUse <> intAlm Then
                    msgtemp(i) = mMsgCreateint("ALM Use", udt1.udtChannel(ich).DigitalUse, intAlm, 0, 0, False, False)   '' Ver1.11.5 2016.08.25 小数点以下桁数追加

                    i = i + 1
                End If
            End If

            ''ソフトウェアフィルタ定数      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_3_Digital(1) = True Then 'DI Filter
                If blSYS = False Then
                    If udt1.udtChannel(ich).DigitalDiFilter <> udt2.udtChannel(zch).DigitalDiFilter Then
                        msgtemp(i) = mMsgCreateint("DI Filter", udt1.udtChannel(ich).DigitalDiFilter, udt2.udtChannel(zch).DigitalDiFilter, 0, 0, False, False)   '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                        i = i + 1
                    End If
                End If
            End If

            ''タグ名称 
            If gCompareChk_3_Digital(2) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).DigitalTagNo, strTag) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).DigitalTagNo), gGetString(strTag))
                    i = i + 1
                End If
            End If

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            'Alarm MimicNo
            If gCompareChk_3_Digital(4) = True Then
                If udt1.udtChannel(ich).DigitalAlmMimic <> intFire Then
                    msgtemp(i) = mMsgCreateint("Alarm MimicNo", udt1.udtChannel(ich).DigitalAlmMimic, intFire, 0, 0, False, False)   '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            'ダミー設定
            If gCompareChk_3_Digital(3) = True Then 'Dummy Setting
                'i = GetDummy("DMY EXG G", udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup, i)
                'i = GetDummy("DMY DLY", udt1.udtChannel(ich).DummyCommonDelay, udt2.udtChannel(zch).DummyCommonDelay, i)
                'i = GetDummy("DMY GP1", udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1, i)
                'i = GetDummy("DMY GP2", udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2, i)
                'i = GetDummy("DMY FU Address", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY STATUS Name", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)
            End If

            '変更項目数を返す
            mCompareSetChannelDigitalDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(システム)OK"

    Friend Function mCompareSetChannelSystemDisp(ByVal udt1 As gTypSetChInfo, _
                                                 ByVal udt2 As gTypSetChInfo, _
                                                 ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            '========================
            ''システムＣＨ
            '========================
            'Ver2.0.4.3 機器状態コード＝システムコードの名称をｾｯﾄするように変更
            Dim strOLDkikiName As String = ""
            Dim strNEWkikiName As String = ""
            Call gSetDBkikiAry(cmbStatus)

            Dim i As Integer = Gyo

            ''アラーム有無
            If gCompareChk_4_System(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).SystemUse <> udt2.udtChannel(zch).SystemUse Then
                    msgtemp(i) = mMsgCreateSht("ALM Use", udt1.udtChannel(ich).SystemUse, udt2.udtChannel(zch).SystemUse)
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(1) = True Then 'SYS1 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse01 <> udt2.udtChannel(zch).SystemInfoStatusUse01 Then
                    msgtemp(i) = mMsgCreateSht("SYS1 Use", udt1.udtChannel(ich).SystemInfoStatusUse01, udt2.udtChannel(zch).SystemInfoStatusUse01)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(2) = True Then 'SYS1 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode01 <> udt2.udtChannel(zch).SystemInfoKikiCode01 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode01.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode01.ToString)
                    'msgtemp(i) = mMsgCreateSht("SYS1 Code", udt1.udtChannel(ich).SystemInfoKikiCode01, udt2.udtChannel(zch).SystemInfoKikiCode01)
                    msgtemp(i) = mMsgCreateStr("SYS1 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(3) = True Then 'SYS1 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName01, udt2.udtChannel(zch).SystemInfoStatusName01) Then
                    msgtemp(i) = mMsgCreateStr("SYS1 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName01), gGetString(udt2.udtChannel(zch).SystemInfoStatusName01))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報２ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(4) = True Then 'SYS2 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse02 <> udt2.udtChannel(zch).SystemInfoStatusUse02 Then
                    msgtemp(i) = mMsgCreateSht("SYS2 Use", udt1.udtChannel(ich).SystemInfoStatusUse02, udt2.udtChannel(zch).SystemInfoStatusUse02)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(5) = True Then 'SYS2 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode02 <> udt2.udtChannel(zch).SystemInfoKikiCode02 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode02.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode02.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS2 Code", udt1.udtChannel(ich).SystemInfoKikiCode02, udt2.udtChannel(zch).SystemInfoKikiCode02)
                    msgtemp(i) = mMsgCreateStr("SYS2 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(6) = True Then 'SYS2 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName02, udt2.udtChannel(zch).SystemInfoStatusName02) Then
                    msgtemp(i) = mMsgCreateStr("SYS2 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName02), gGetString(udt2.udtChannel(zch).SystemInfoStatusName02))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報３ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(7) = True Then 'SYS3 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse03 <> udt2.udtChannel(zch).SystemInfoStatusUse03 Then
                    msgtemp(i) = mMsgCreateSht("SYS3 Use", udt1.udtChannel(ich).SystemInfoStatusUse03, udt2.udtChannel(zch).SystemInfoStatusUse03)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(8) = True Then 'SYS3 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode03 <> udt2.udtChannel(zch).SystemInfoKikiCode03 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode03.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode03.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS3 Code", udt1.udtChannel(ich).SystemInfoKikiCode03, udt2.udtChannel(zch).SystemInfoKikiCode03)
                    msgtemp(i) = mMsgCreateStr("SYS3 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(9) = True Then 'SYS3 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName03, udt2.udtChannel(zch).SystemInfoStatusName03) Then
                    msgtemp(i) = mMsgCreateStr("SYS3 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName03), gGetString(udt2.udtChannel(zch).SystemInfoStatusName03))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報４ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(10) = True Then 'SYS4 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse04 <> udt2.udtChannel(zch).SystemInfoStatusUse04 Then
                    msgtemp(i) = mMsgCreateSht("SYS4 Use", udt1.udtChannel(ich).SystemInfoStatusUse04, udt2.udtChannel(zch).SystemInfoStatusUse04)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(11) = True Then 'SYS4 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode04 <> udt2.udtChannel(zch).SystemInfoKikiCode04 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode04.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode04.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS4 Code", udt1.udtChannel(ich).SystemInfoKikiCode04, udt2.udtChannel(zch).SystemInfoKikiCode04)
                    msgtemp(i) = mMsgCreateStr("SYS4 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(12) = True Then 'SYS4 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName04, udt2.udtChannel(zch).SystemInfoStatusName04) Then
                    msgtemp(i) = mMsgCreateStr("SYS4 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName04), gGetString(udt2.udtChannel(zch).SystemInfoStatusName04))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報５ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(13) = True Then 'SYS5 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse05 <> udt2.udtChannel(zch).SystemInfoStatusUse05 Then
                    msgtemp(i) = mMsgCreateSht("SYS5 Use", udt1.udtChannel(ich).SystemInfoStatusUse05, udt2.udtChannel(zch).SystemInfoStatusUse05)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(14) = True Then 'SYS5 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode05 <> udt2.udtChannel(zch).SystemInfoKikiCode05 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode05.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode05.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS5 Code", udt1.udtChannel(ich).SystemInfoKikiCode05, udt2.udtChannel(zch).SystemInfoKikiCode05)
                    msgtemp(i) = mMsgCreateStr("SYS5 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(15) = True Then 'SYS5 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName05, udt2.udtChannel(zch).SystemInfoStatusName05) Then
                    msgtemp(i) = mMsgCreateStr("SYS5 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName05), gGetString(udt2.udtChannel(zch).SystemInfoStatusName05))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報６ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(16) = True Then 'SYS6 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse06 <> udt2.udtChannel(zch).SystemInfoStatusUse06 Then
                    msgtemp(i) = mMsgCreateSht("SYS6 Use", udt1.udtChannel(ich).SystemInfoStatusUse06, udt2.udtChannel(zch).SystemInfoStatusUse06)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(17) = True Then 'SYS6 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode06 <> udt2.udtChannel(zch).SystemInfoKikiCode06 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode06.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode06.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS6 Code", udt1.udtChannel(ich).SystemInfoKikiCode06, udt2.udtChannel(zch).SystemInfoKikiCode06)
                    msgtemp(i) = mMsgCreateStr("SYS6 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(18) = True Then 'SYS6 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName06, udt2.udtChannel(zch).SystemInfoStatusName06) Then
                    msgtemp(i) = mMsgCreateStr("SYS6 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName06), gGetString(udt2.udtChannel(zch).SystemInfoStatusName06))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報７ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(19) = True Then 'SYS7 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse07 <> udt2.udtChannel(zch).SystemInfoStatusUse07 Then
                    msgtemp(i) = mMsgCreateSht("SYS7 Use", udt1.udtChannel(ich).SystemInfoStatusUse07, udt2.udtChannel(zch).SystemInfoStatusUse07)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(20) = True Then 'SYS7 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode07 <> udt2.udtChannel(zch).SystemInfoKikiCode07 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode07.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode07.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS7 Code", udt1.udtChannel(ich).SystemInfoKikiCode07, udt2.udtChannel(zch).SystemInfoKikiCode07)
                    msgtemp(i) = mMsgCreateStr("SYS7 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(21) = True Then 'SYS7 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName07, udt2.udtChannel(zch).SystemInfoStatusName07) Then
                    msgtemp(i) = mMsgCreateStr("SYS7 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName07), gGetString(udt2.udtChannel(zch).SystemInfoStatusName07))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報８ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(22) = True Then 'SYS8 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse08 <> udt2.udtChannel(zch).SystemInfoStatusUse08 Then
                    msgtemp(i) = mMsgCreateSht("SYS8 Use", udt1.udtChannel(ich).SystemInfoStatusUse08, udt2.udtChannel(zch).SystemInfoStatusUse08)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(23) = True Then 'SYS8 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode08 <> udt2.udtChannel(zch).SystemInfoKikiCode08 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode08.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode08.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS8 Code", udt1.udtChannel(ich).SystemInfoKikiCode08, udt2.udtChannel(zch).SystemInfoKikiCode08)
                    msgtemp(i) = mMsgCreateStr("SYS8 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(24) = True Then 'SYS8 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName08, udt2.udtChannel(zch).SystemInfoStatusName08) Then
                    msgtemp(i) = mMsgCreateStr("SYS8 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName08), gGetString(udt2.udtChannel(zch).SystemInfoStatusName08))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報９ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(25) = True Then 'SYS9 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse09 <> udt2.udtChannel(zch).SystemInfoStatusUse09 Then
                    msgtemp(i) = mMsgCreateSht("SYS9 Use", udt1.udtChannel(ich).SystemInfoStatusUse09, udt2.udtChannel(zch).SystemInfoStatusUse09)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(26) = True Then 'SYS9 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode09 <> udt2.udtChannel(zch).SystemInfoKikiCode09 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode09.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode09.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS9 Code", udt1.udtChannel(ich).SystemInfoKikiCode09, udt2.udtChannel(zch).SystemInfoKikiCode09)
                    msgtemp(i) = mMsgCreateStr("SYS9 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(27) = True Then 'SYS9 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName09, udt2.udtChannel(zch).SystemInfoStatusName09) Then
                    msgtemp(i) = mMsgCreateStr("SYS9 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName09), gGetString(udt2.udtChannel(zch).SystemInfoStatusName09))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１０ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(28) = True Then 'SYS10 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse10 <> udt2.udtChannel(zch).SystemInfoStatusUse10 Then
                    msgtemp(i) = mMsgCreateSht("SYS10 Use", udt1.udtChannel(ich).SystemInfoStatusUse10, udt2.udtChannel(zch).SystemInfoStatusUse10)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(29) = True Then 'SYS10 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode10 <> udt2.udtChannel(zch).SystemInfoKikiCode10 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode10.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode10.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS10 Code", udt1.udtChannel(ich).SystemInfoKikiCode10, udt2.udtChannel(zch).SystemInfoKikiCode10)
                    msgtemp(i) = mMsgCreateStr("SYS10 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(30) = True Then 'SYS10 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName10, udt2.udtChannel(zch).SystemInfoStatusName10) Then
                    msgtemp(i) = mMsgCreateStr("SYS10 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName10), gGetString(udt2.udtChannel(zch).SystemInfoStatusName10))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１１ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(31) = True Then 'SYS11 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse11 <> udt2.udtChannel(zch).SystemInfoStatusUse11 Then
                    msgtemp(i) = mMsgCreateSht("SYS11 Use", udt1.udtChannel(ich).SystemInfoStatusUse11, udt2.udtChannel(zch).SystemInfoStatusUse11)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(32) = True Then 'SYS11 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode11 <> udt2.udtChannel(zch).SystemInfoKikiCode11 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode11.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode11.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS11 Code", udt1.udtChannel(ich).SystemInfoKikiCode11, udt2.udtChannel(zch).SystemInfoKikiCode11)
                    msgtemp(i) = mMsgCreateStr("SYS11 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(33) = True Then 'SYS11 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName11, udt2.udtChannel(zch).SystemInfoStatusName11) Then
                    msgtemp(i) = mMsgCreateStr("SYS11 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName11), gGetString(udt2.udtChannel(zch).SystemInfoStatusName11))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１２ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(34) = True Then 'SYS12 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse12 <> udt2.udtChannel(zch).SystemInfoStatusUse12 Then
                    msgtemp(i) = mMsgCreateSht("SYS12 Use", udt1.udtChannel(ich).SystemInfoStatusUse12, udt2.udtChannel(zch).SystemInfoStatusUse12)
                    i = i + 1
                End If
            End If
            If gCompareChk_4_System(35) = True Then 'SYS12 Code
                ''機器状態コード
                If udt1.udtChannel(ich).SystemInfoKikiCode12 <> udt2.udtChannel(zch).SystemInfoKikiCode12 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode12.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode12.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS12 Code", udt1.udtChannel(ich).SystemInfoKikiCode12, udt2.udtChannel(zch).SystemInfoKikiCode12)
                    msgtemp(i) = mMsgCreateStr("SYS12 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(36) = True Then 'SYS12 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName12, udt2.udtChannel(zch).SystemInfoStatusName12) Then
                    msgtemp(i) = mMsgCreateStr("SYS12 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName12), gGetString(udt2.udtChannel(zch).SystemInfoStatusName12))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１３ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(37) = True Then 'SYS13 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse13 <> udt2.udtChannel(zch).SystemInfoStatusUse13 Then
                    msgtemp(i) = mMsgCreateSht("SYS13 Use", udt1.udtChannel(ich).SystemInfoStatusUse13, udt2.udtChannel(zch).SystemInfoStatusUse13)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(38) = True Then 'SYS13 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode13 <> udt2.udtChannel(zch).SystemInfoKikiCode13 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode13.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode13.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS13 Code", udt1.udtChannel(ich).SystemInfoKikiCode13, udt2.udtChannel(zch).SystemInfoKikiCode13)
                    msgtemp(i) = mMsgCreateStr("SYS13 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(39) = True Then 'SYS13 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName13, udt2.udtChannel(zch).SystemInfoStatusName13) Then
                    msgtemp(i) = mMsgCreateStr("SYS13 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName13), gGetString(udt2.udtChannel(zch).SystemInfoStatusName13))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１４ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(40) = True Then 'SYS14 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse14 <> udt2.udtChannel(zch).SystemInfoStatusUse14 Then
                    msgtemp(i) = mMsgCreateSht("SYS14 Use", udt1.udtChannel(ich).SystemInfoStatusUse14, udt2.udtChannel(zch).SystemInfoStatusUse14)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(41) = True Then 'SYS14 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode14 <> udt2.udtChannel(zch).SystemInfoKikiCode14 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode14.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode14.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS14 Code", udt1.udtChannel(ich).SystemInfoKikiCode14, udt2.udtChannel(zch).SystemInfoKikiCode14)
                    msgtemp(i) = mMsgCreateStr("SYS14 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(42) = True Then 'SYS14 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName14, udt2.udtChannel(zch).SystemInfoStatusName14) Then
                    msgtemp(i) = mMsgCreateStr("SYS14 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName14), gGetString(udt2.udtChannel(zch).SystemInfoStatusName14))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１５ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(43) = True Then 'SYS15 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse15 <> udt2.udtChannel(zch).SystemInfoStatusUse15 Then
                    msgtemp(i) = mMsgCreateSht("SYS15 Use", udt1.udtChannel(ich).SystemInfoStatusUse15, udt2.udtChannel(zch).SystemInfoStatusUse15)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(44) = True Then 'SYS15 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode15 <> udt2.udtChannel(zch).SystemInfoKikiCode15 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode15.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode15.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS15 Code", udt1.udtChannel(ich).SystemInfoKikiCode15, udt2.udtChannel(zch).SystemInfoKikiCode15)
                    msgtemp(i) = mMsgCreateStr("SYS15 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(45) = True Then 'SYS15 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName15, udt2.udtChannel(zch).SystemInfoStatusName15) Then
                    msgtemp(i) = mMsgCreateStr("SYS15 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName15), gGetString(udt2.udtChannel(zch).SystemInfoStatusName15))
                    i = i + 1
                End If
            End If

            ''システムCH設定値情報１６ ------------------------------------------------------

            ''ステータス使用有無
            If gCompareChk_4_System(46) = True Then 'SYS16 Use
                If udt1.udtChannel(ich).SystemInfoStatusUse16 <> udt2.udtChannel(zch).SystemInfoStatusUse16 Then
                    msgtemp(i) = mMsgCreateSht("SYS16 Use", udt1.udtChannel(ich).SystemInfoStatusUse16, udt2.udtChannel(zch).SystemInfoStatusUse16)
                    i = i + 1
                End If
            End If

            ''機器状態コード
            If gCompareChk_4_System(47) = True Then 'SYS16 Code
                If udt1.udtChannel(ich).SystemInfoKikiCode16 <> udt2.udtChannel(zch).SystemInfoKikiCode16 Then
                    strOLDkikiName = fnGetKikiName(udt1.udtChannel(ich).SystemInfoKikiCode16.ToString)
                    strNEWkikiName = fnGetKikiName(udt2.udtChannel(zch).SystemInfoKikiCode16.ToString)

                    'msgtemp(i) = mMsgCreateSht("SYS16 Code", udt1.udtChannel(ich).SystemInfoKikiCode16, udt2.udtChannel(zch).SystemInfoKikiCode16)
                    msgtemp(i) = mMsgCreateStr("SYS16 Code", strOLDkikiName, strNEWkikiName)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_4_System(48) = True Then 'SYS16 Status
                If Not gCompareString(udt1.udtChannel(ich).SystemInfoStatusName16, udt2.udtChannel(zch).SystemInfoStatusName16) Then
                    msgtemp(i) = mMsgCreateStr("SYS16 Status", gGetString(udt1.udtChannel(ich).SystemInfoStatusName16), gGetString(udt2.udtChannel(zch).SystemInfoStatusName16))
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_4_System(49) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).SystemTagNo, udt2.udtChannel(zch).SystemTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).SystemTagNo), gGetString(udt2.udtChannel(zch).SystemTagNo))
                    i = i + 1
                End If
            End If

            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            'Alarm MimicNo
            If gCompareChk_4_System(50) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).SystemAlmMimic <> udt2.udtChannel(zch).SystemAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).SystemAlmMimic, udt2.udtChannel(zch).SystemAlmMimic)
                    i = i + 1
                End If
            End If

            '変更項目数を返す
            mCompareSetChannelSystemDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function fnGetKikiName(pstrKikiCode As String) As String
        Dim intIDX As Integer = -1
        Dim strRet As String = ""

        intIDX = gAryDBkikiDtlCode.IndexOf(pstrKikiCode)
        If intIDX < 0 Then
            strRet = ""
        Else
            strRet = gAryDBkikiName(intIDX) & "-" & gAryDBkikiDtlName(intIDX)
        End If

        Return strRet
    End Function

#End Region

#Region "チャンネル情報(モーター)OK"

    Friend Function mCompareSetChannelMotorDisp(ByVal udt1 As gTypSetChInfo, _
                                                ByVal udt2 As gTypSetChInfo, _
                                                ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            '========================
            ''モーターＣＨ
            '========================

            Dim i As Integer = Gyo

            ''アラーム有無
            If gCompareChk_5_Motor(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).MotorUse <> udt2.udtChannel(zch).MotorUse Then
                    msgtemp(i) = mMsgCreateSht("ALM Use", udt1.udtChannel(ich).MotorUse, udt2.udtChannel(zch).MotorUse)
                    i = i + 1
                End If
            End If

            ''ソフトウェアフィルタ定数
            If gCompareChk_5_Motor(1) = True Then 'Filter
                If udt1.udtChannel(ich).MotorDiFilter <> udt2.udtChannel(zch).MotorDiFilter Then
                    msgtemp(i) = mMsgCreateSht("Filter", udt1.udtChannel(ich).MotorDiFilter, udt2.udtChannel(zch).MotorDiFilter)
                    i = i + 1
                End If
            End If

            ''フィードバックアラームタイマ値
            If gCompareChk_5_Motor(2) = True Then 'FB Timer
                If udt1.udtChannel(ich).MotorFeedback <> udt2.udtChannel(zch).MotorFeedback Or _
                    (udt1.udtChannel(ich).DummyFaTimeV <> udt2.udtChannel(zch).DummyFaTimeV) Then
                    'msgtemp(i) = mMsgCreateSht("FB Timer", udt1.udtChannel(ich).MotorFeedback, udt2.udtChannel(zch).MotorFeedback)
                    msgtemp(i) = mMsgCreateint("FB Timer", udt1.udtChannel(ich).MotorFeedback, udt2.udtChannel(zch).MotorFeedback, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaTimeV, udt2.udtChannel(zch).DummyFaTimeV)
                    i = i + 1
                End If
            End If

            'Ver2.0.4.1
            '出力FUｱﾄﾞﾚｽは、IN_FUｱﾄﾞﾚｽと同様に一つにまとめる
            ''出力 FU 番号
            'If gCompareChk_5_Motor(3) = True Then 'OUT FU No
            '    If udt1.udtChannel(ich).MotorFuNo <> udt2.udtChannel(zch).MotorFuNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU No", udt1.udtChannel(ich).MotorFuNo, udt2.udtChannel(zch).MotorFuNo)
            '        i = i + 1
            '    End If
            'End If
            ''出力 FU ポート番号
            'If gCompareChk_5_Motor(4) = True Then 'OUT FU Slot
            '    If udt1.udtChannel(ich).MotorPortNo <> udt2.udtChannel(zch).MotorPortNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Slot", udt1.udtChannel(ich).MotorPortNo, udt2.udtChannel(zch).MotorPortNo)
            '        i = i + 1
            '    End If
            'End If
            ''出力 FU 端子番号
            'If gCompareChk_5_Motor(5) = True Then 'OUT FU Pin
            '    If udt1.udtChannel(ich).MotorPin <> udt2.udtChannel(zch).MotorPin Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Pin", udt1.udtChannel(ich).MotorPin, udt2.udtChannel(zch).MotorPin)
            '        i = i + 1
            '    End If
            'End If
            If gCompareChk_5_Motor(3) = True Or gCompareChk_5_Motor(4) = True Or gCompareChk_5_Motor(5) = True Then
                If (udt1.udtChannel(ich).MotorFuNo <> udt2.udtChannel(zch).MotorFuNo Or _
                    udt1.udtChannel(ich).MotorPortNo <> udt2.udtChannel(zch).MotorPortNo Or _
                    udt1.udtChannel(ich).MotorPin <> udt2.udtChannel(zch).MotorPin) Or _
                    (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then

                    msgtemp(i) = mMsgFUAdd_OUT(udt1.udtChannel(ich).MotorFuNo, udt1.udtChannel(ich).MotorPortNo, udt1.udtChannel(ich).MotorPin, _
                                                udt2.udtChannel(zch).MotorFuNo, udt2.udtChannel(zch).MotorPortNo, udt2.udtChannel(zch).MotorPin, _
                                                udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
                    i = i + 1

                End If
            End If



            ''出力点数
            If gCompareChk_5_Motor(6) = True Then 'Output Count
                If udt1.udtChannel(ich).MotorPinNo <> udt2.udtChannel(zch).MotorPinNo Then
                    msgtemp(i) = mMsgCreateSht("Output Count", udt1.udtChannel(ich).MotorPinNo, udt2.udtChannel(zch).MotorPinNo)
                    i = i + 1
                End If
            End If


            ''出力制御種別
            If gCompareChk_5_Motor(7) = True Then 'Output Type
                If udt1.udtChannel(ich).MotorControl <> udt2.udtChannel(zch).MotorControl Then
                    msgtemp(i) = mMsgCreateSht("Output Type", udt1.udtChannel(ich).MotorControl, udt2.udtChannel(zch).MotorControl)
                    i = i + 1
                End If
            End If

            ''出力パルス幅
            If gCompareChk_5_Motor(8) = True Then 'Output width
                If udt1.udtChannel(ich).MotorWidth <> udt2.udtChannel(zch).MotorWidth Then
                    msgtemp(i) = mMsgCreateSht("Output width", udt1.udtChannel(ich).MotorWidth, udt2.udtChannel(zch).MotorWidth)
                    i = i + 1
                End If
            End If

            ''出力ステータス種別コード
            If gCompareChk_5_Motor(9) = True Then 'Output Status
                If udt1.udtChannel(ich).MotorStatus <> udt2.udtChannel(zch).MotorStatus Or _
                    (udt1.udtChannel(ich).DummyOutStatusType <> udt2.udtChannel(zch).DummyOutStatusType) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyOutStatusType, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyOutStatusType, "#", "")

                    'Ver2.0.2.8 Output Status を名称にする
                    Dim strOld As String = strOldDmy & GetChStatus(udt1.udtChannel(ich).MotorStatus)
                    Dim strNew As String = strNewDmy & GetChStatus(udt2.udtChannel(zch).MotorStatus)
                    'msgtemp(i) = mMsgCreateStr("Output Status", "0x" & Hex(udt1.udtChannel(ich).MotorStatus).PadLeft(2, "0"), "0x" & Hex(udt2.udtChannel(zch).MotorStatus).PadLeft(2, "0"))
                    msgtemp(i) = mMsgCreateStr("Output Status", strOld, strNew)
                    i = i + 1
                End If
            End If

            ''出力ステータス情報　--------------------------------------------------------

            ''ステータス名称１
            If gCompareChk_5_Motor(10) = True Then 'STATUS1
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus1, udt2.udtChannel(zch).MotorOutStatus1) Then
                    msgtemp(i) = mMsgCreateStr("STATUS1", gGetString(udt1.udtChannel(ich).MotorOutStatus1), gGetString(udt2.udtChannel(zch).MotorOutStatus1))
                    i = i + 1
                End If
            End If

            ''ステータス名称２
            If gCompareChk_5_Motor(11) = True Then 'STATUS2
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus2, udt2.udtChannel(zch).MotorOutStatus2) Then
                    msgtemp(i) = mMsgCreateStr("STATUS2", gGetString(udt1.udtChannel(ich).MotorOutStatus2), gGetString(udt2.udtChannel(zch).MotorOutStatus2))
                    i = i + 1
                End If
            End If

            ''ステータス名称３
            If gCompareChk_5_Motor(12) = True Then 'STATUS3
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus3, udt2.udtChannel(zch).MotorOutStatus3) Then
                    msgtemp(i) = mMsgCreateStr("STATUS3", gGetString(udt1.udtChannel(ich).MotorOutStatus3), gGetString(udt2.udtChannel(zch).MotorOutStatus3))
                    i = i + 1
                End If
            End If

            ''ステータス名称４
            If gCompareChk_5_Motor(13) = True Then 'STATUS4
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus4, udt2.udtChannel(zch).MotorOutStatus4) Then
                    msgtemp(i) = mMsgCreateStr("STATUS4", gGetString(udt1.udtChannel(ich).MotorOutStatus4), gGetString(udt2.udtChannel(zch).MotorOutStatus4))
                    i = i + 1
                End If
            End If

            ''ステータス名称５
            If gCompareChk_5_Motor(14) = True Then 'STATUS5
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus5, udt2.udtChannel(zch).MotorOutStatus5) Then
                    msgtemp(i) = mMsgCreateStr("STATUS5", gGetString(udt1.udtChannel(ich).MotorOutStatus5), gGetString(udt2.udtChannel(zch).MotorOutStatus5))
                    i = i + 1
                End If
            End If

            ''ステータス名称６
            If gCompareChk_5_Motor(15) = True Then 'STATUS6
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus6, udt2.udtChannel(zch).MotorOutStatus6) Then
                    msgtemp(i) = mMsgCreateStr("STATUS6", gGetString(udt1.udtChannel(ich).MotorOutStatus6), gGetString(udt2.udtChannel(zch).MotorOutStatus6))
                    i = i + 1
                End If
            End If

            ''ステータス名称７
            If gCompareChk_5_Motor(16) = True Then 'STATUS7
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus7, udt2.udtChannel(zch).MotorOutStatus7) Then
                    msgtemp(i) = mMsgCreateStr("STATUS7", gGetString(udt1.udtChannel(ich).MotorOutStatus7), gGetString(udt2.udtChannel(zch).MotorOutStatus7))
                    i = i + 1
                End If
            End If

            ''ステータス名称８
            If gCompareChk_5_Motor(17) = True Then 'STATUS8
                If Not gCompareString(udt1.udtChannel(ich).MotorOutStatus8, udt2.udtChannel(zch).MotorOutStatus8) Then
                    msgtemp(i) = mMsgCreateStr("STATUS8", gGetString(udt1.udtChannel(ich).MotorOutStatus8), gGetString(udt2.udtChannel(zch).MotorOutStatus8))
                    i = i + 1
                End If
            End If

            ''フィードバックアラーム情報 ----------------------------------------------------

            ''フィードバックアラーム有無
            If gCompareChk_5_Motor(18) = True Then 'FB Use
                If udt1.udtChannel(ich).MotorAlarmUse <> udt2.udtChannel(zch).MotorAlarmUse Then
                    msgtemp(i) = mMsgCreateSht("FB Use", udt1.udtChannel(ich).MotorAlarmUse, udt2.udtChannel(zch).MotorAlarmUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_5_Motor(19) = True Then 'FB DLY
                If udt1.udtChannel(ich).MotorAlarmDelay <> udt2.udtChannel(zch).MotorAlarmDelay Or _
                    (udt1.udtChannel(ich).DummyFaDelay <> udt2.udtChannel(zch).DummyFaDelay) Then
                    'msgtemp(i) = mMsgCreateSht("FB DLY", udt1.udtChannel(ich).MotorAlarmDelay, udt2.udtChannel(zch).MotorAlarmDelay)
                    msgtemp(i) = mMsgCreateint("FB DLY", udt1.udtChannel(ich).MotorAlarmDelay, udt2.udtChannel(zch).MotorAlarmDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaDelay, udt2.udtChannel(zch).DummyFaDelay)
                    i = i + 1
                End If
            End If

            ''規定値１
            If gCompareChk_5_Motor(20) = True Then 'FB SP1
                If udt1.udtChannel(ich).MotorAlarmSp1 <> udt2.udtChannel(zch).MotorAlarmSp1 Then
                    msgtemp(i) = mMsgCreateSht("FB SP1", udt1.udtChannel(ich).MotorAlarmSp1, udt2.udtChannel(zch).MotorAlarmSp1)
                    i = i + 1
                End If
            End If

            ''規定値２
            If gCompareChk_5_Motor(21) = True Then 'FB SP2
                If udt1.udtChannel(ich).MotorAlarmSp2 <> udt2.udtChannel(zch).MotorAlarmSp2 Then
                    msgtemp(i) = mMsgCreateSht("FB SP2", udt1.udtChannel(ich).MotorAlarmSp2, udt2.udtChannel(zch).MotorAlarmSp2)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（開処理用）
            If gCompareChk_5_Motor(22) = True Then 'FB hys1
                If udt1.udtChannel(ich).MotorAlarmHys1 <> udt2.udtChannel(zch).MotorAlarmHys1 Then
                    msgtemp(i) = mMsgCreateSht("FB hys1", udt1.udtChannel(ich).MotorAlarmHys1, udt2.udtChannel(zch).MotorAlarmHys1)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（閉処理用）
            If gCompareChk_5_Motor(23) = True Then 'FB hys2
                If udt1.udtChannel(ich).MotorAlarmHys2 <> udt2.udtChannel(zch).MotorAlarmHys2 Then
                    msgtemp(i) = mMsgCreateSht("FB hys2", udt1.udtChannel(ich).MotorAlarmHys2, udt2.udtChannel(zch).MotorAlarmHys2)
                    i = i + 1
                End If
            End If

            ''サンプリング時間
            If gCompareChk_5_Motor(24) = True Then 'FB Sampling
                If udt1.udtChannel(ich).MotorAlarmSt <> udt2.udtChannel(zch).MotorAlarmSt Then
                    msgtemp(i) = mMsgCreateSht("FB Sampling", udt1.udtChannel(ich).MotorAlarmSt, udt2.udtChannel(zch).MotorAlarmSt)
                    i = i + 1
                End If
            End If

            ''変化量       '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_5_Motor(25) = True Then 'FB Variation
                If udt1.udtChannel(ich).MotorAlarmVar <> udt2.udtChannel(zch).MotorAlarmVar Then
                    msgtemp(i) = mMsgCreateint("FB Variation", udt1.udtChannel(ich).MotorAlarmVar, udt2.udtChannel(zch).MotorAlarmVar, 0, 0, False, False)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_5_Motor(26) = True Then 'FB EX
                If udt1.udtChannel(ich).MotorAlarmExtGroup <> udt2.udtChannel(zch).MotorAlarmExtGroup Or _
                    (udt1.udtChannel(ich).DummyFaExtGr <> udt2.udtChannel(zch).DummyFaExtGr) Then
                    'msgtemp(i) = mMsgCreateByt("FB EX", udt1.udtChannel(ich).MotorAlarmExtGroup, udt2.udtChannel(zch).MotorAlarmExtGroup)
                    msgtemp(i) = mMsgCreateint("FB EX", udt1.udtChannel(ich).MotorAlarmExtGroup, udt2.udtChannel(zch).MotorAlarmExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaExtGr, udt2.udtChannel(zch).DummyFaExtGr)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_5_Motor(27) = True Then 'FB GR1
                If udt1.udtChannel(ich).MotorAlarmGroupRepose1 <> udt2.udtChannel(zch).MotorAlarmGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyFaGrep1 <> udt2.udtChannel(zch).DummyFaGrep1) Then
                    'msgtemp(i) = mMsgCreateByt("FB GR1", udt1.udtChannel(ich).MotorAlarmGroupRepose1, udt2.udtChannel(zch).MotorAlarmGroupRepose1)
                    msgtemp(i) = mMsgCreateint("FB GR1", udt1.udtChannel(ich).MotorAlarmGroupRepose1, udt2.udtChannel(zch).MotorAlarmGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaGrep1, udt2.udtChannel(zch).DummyFaGrep1)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_5_Motor(28) = True Then 'FB GR2
                If udt1.udtChannel(ich).MotorAlarmGroupRepose2 <> udt2.udtChannel(zch).MotorAlarmGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyFaGrep2 <> udt2.udtChannel(zch).DummyFaGrep2) Then
                    'msgtemp(i) = mMsgCreateByt("FB GR2", udt1.udtChannel(ich).MotorAlarmGroupRepose2, udt2.udtChannel(zch).MotorAlarmGroupRepose2)
                    msgtemp(i) = mMsgCreateint("FB GR2", udt1.udtChannel(ich).MotorAlarmGroupRepose2, udt2.udtChannel(zch).MotorAlarmGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaGrep2, udt2.udtChannel(zch).DummyFaGrep2)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_5_Motor(29) = True Then 'FB STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).MotorAlarmStatusInput), NZfS(udt2.udtChannel(zch).MotorAlarmStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyFaStaNm <> udt2.udtChannel(zch).DummyFaStaNm) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyFaStaNm, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyFaStaNm, "#", "")
                    msgtemp(i) = mMsgCreateStr("FB STATUS", strOldDmy & udt1.udtChannel(ich).MotorAlarmStatusInput, strNewDmy & udt2.udtChannel(zch).MotorAlarmStatusInput)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_5_Motor(30) = True Then 'FB MR Status
                If udt1.udtChannel(ich).MotorAlarmManualReposeState <> udt2.udtChannel(zch).MotorAlarmManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("FB MR Status", udt1.udtChannel(ich).MotorAlarmManualReposeState, udt2.udtChannel(zch).MotorAlarmManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_5_Motor(31) = True Then 'FB MR Set
                If udt1.udtChannel(ich).MotorAlarmManualReposeSet <> udt2.udtChannel(zch).MotorAlarmManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("FB MR Set", udt1.udtChannel(ich).MotorAlarmManualReposeSet, udt2.udtChannel(zch).MotorAlarmManualReposeSet)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_5_Motor(32) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).MotorTagNo, udt2.udtChannel(zch).MotorTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).MotorTagNo), gGetString(udt2.udtChannel(zch).MotorTagNo))
                    i = i + 1
                End If
            End If

            'ダミー設定
            If gCompareChk_5_Motor(33) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT GP H", udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup, i)
                'i = GetDummy("DMY ALM RP1", udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1, i)
                'i = GetDummy("DMY ALM RP2", udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2, i)
                'i = GetDummy("DMY ALM DI Start", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM OUT Status Type", udt1.udtChannel(ich).DummyOutStatusType, udt2.udtChannel(zch).DummyOutStatusType, i)
                'i = GetDummy("DMY ALM OUT EXT H", udt1.udtChannel(ich).DummyFaExtGr, udt2.udtChannel(zch).DummyFaExtGr, i)
                'i = GetDummy("DMY ALM OUT DLY", udt1.udtChannel(ich).DummyFaDelay, udt2.udtChannel(zch).DummyFaDelay, i)
                'i = GetDummy("DMY ALM OUT GR RP1", udt1.udtChannel(ich).DummyFaGrep1, udt2.udtChannel(zch).DummyFaGrep1, i)
                'i = GetDummy("DMY ALM OUT GR RP2", udt1.udtChannel(ich).DummyFaGrep2, udt2.udtChannel(zch).DummyFaGrep2, i)
                'i = GetDummy("DMY ALM OUT Status Name", udt1.udtChannel(ich).DummyFaStaNm, udt2.udtChannel(zch).DummyFaStaNm, i)
                'i = GetDummy("DMY ALM OUT Time Value", udt1.udtChannel(ich).DummyFaTimeV, udt2.udtChannel(zch).DummyFaTimeV, i)
            End If

            'Alarm MimicNo
            If gCompareChk_5_Motor(34) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).MotorAlmMimic <> udt2.udtChannel(zch).MotorAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).MotorAlmMimic, udt2.udtChannel(zch).MotorAlmMimic)
                    i = i + 1
                End If
            End If

            '変更項目数を返す
            mCompareSetChannelMotorDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(バルブ DI-DO)OK"

    Friend Function mCompareSetChannelValveDiDoDisp(ByVal udt1 As gTypSetChInfo, _
                                                    ByVal udt2 As gTypSetChInfo, _
                                                    ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更

            '========================
            ''バルブ DI-DO
            '========================

            Dim i As Integer = Gyo

            ''アラーム有無
            If gCompareChk_6_DIDO(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).ValveDiDoUse <> udt2.udtChannel(zch).ValveDiDoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM Use", udt1.udtChannel(ich).ValveDiDoUse, udt2.udtChannel(zch).ValveDiDoUse)
                    i = i + 1
                End If
            End If

            ''コンポジット設定テーブルインデックス
            If gCompareChk_6_DIDO(1) = True Then 'Tbl Index
                If udt1.udtChannel(ich).ValveCompositeTableIndex <> udt2.udtChannel(zch).ValveCompositeTableIndex Then
                    msgtemp(i) = mMsgCreateSht("Tbl Index", udt1.udtChannel(ich).ValveCompositeTableIndex, udt2.udtChannel(zch).ValveCompositeTableIndex)
                    i = i + 1
                End If
            End If

            ''フィードバックアラームタイマ値
            If gCompareChk_6_DIDO(2) = True Then 'FB Timer
                If udt1.udtChannel(ich).ValveDiDoFeedback <> udt2.udtChannel(zch).ValveDiDoFeedback Or _
                    (udt1.udtChannel(ich).DummyFaTimeV <> udt2.udtChannel(zch).DummyFaTimeV) Then
                    'msgtemp(i) = mMsgCreateSht("FB Timer", udt1.udtChannel(ich).ValveDiDoFeedback, udt2.udtChannel(zch).ValveDiDoFeedback)
                    msgtemp(i) = mMsgCreateint("FB Timer", udt1.udtChannel(ich).ValveDiDoFeedback, udt2.udtChannel(zch).ValveDiDoFeedback, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaTimeV, udt2.udtChannel(zch).DummyFaTimeV)
                    i = i + 1
                End If
            End If


            'Ver2.0.4.1
            '出力FUｱﾄﾞﾚｽは、IN_FUｱﾄﾞﾚｽと同様に一つにまとめる
            ''外部出力 FU 番号
            'If gCompareChk_6_DIDO(3) = True Then 'OUT FU No
            '    If udt1.udtChannel(ich).ValveDiDoFuNo <> udt2.udtChannel(zch).ValveDiDoFuNo Or _
            '        (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then
            '        'msgtemp(i) = mMsgCreateSht("OUT FU No", udt1.udtChannel(ich).ValveDiDoFuNo, udt2.udtChannel(zch).ValveDiDoFuNo)
            '        msgtemp(i) = mMsgCreateint("OUT FU No", udt1.udtChannel(ich).ValveDiDoFuNo, udt2.udtChannel(zch).ValveDiDoFuNo, _
            '                                   0, 0, _
            '                                   udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU ポート番号
            'If gCompareChk_6_DIDO(4) = True Then 'OUT FU Slot
            '    If udt1.udtChannel(ich).ValveDiDoPortNo <> udt2.udtChannel(zch).ValveDiDoPortNo Or _
            '        (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then
            '        'msgtemp(i) = mMsgCreateSht("OUT FU Slot", udt1.udtChannel(ich).ValveDiDoPortNo, udt2.udtChannel(zch).ValveDiDoPortNo)
            '        msgtemp(i) = mMsgCreateint("OUT FU Slot", udt1.udtChannel(ich).ValveDiDoPortNo, udt2.udtChannel(zch).ValveDiDoPortNo, _
            '                                   0, 0, _
            '                                   udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU 端子番号
            'If gCompareChk_6_DIDO(5) = True Then 'OUT FU Pin
            '    If udt1.udtChannel(ich).ValveDiDoPin <> udt2.udtChannel(zch).ValveDiDoPin Or _
            '        (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then
            '        'msgtemp(i) = mMsgCreateSht("OUT FU Pin", udt1.udtChannel(ich).ValveDiDoPin, udt2.udtChannel(zch).ValveDiDoPin)
            '        msgtemp(i) = mMsgCreateint("OUT FU Pin", udt1.udtChannel(ich).ValveDiDoPin, udt2.udtChannel(zch).ValveDiDoPin, _
            '                                   0, 0, _
            '                                   udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
            '        i = i + 1
            '    End If
            'End If
            If gCompareChk_6_DIDO(3) = True Or gCompareChk_6_DIDO(4) = True Or gCompareChk_6_DIDO(5) = True Then
                If (udt1.udtChannel(ich).ValveDiDoFuNo <> udt2.udtChannel(zch).ValveDiDoFuNo Or _
                    udt1.udtChannel(ich).ValveDiDoPortNo <> udt2.udtChannel(zch).ValveDiDoPortNo Or _
                    udt1.udtChannel(ich).ValveDiDoPin <> udt2.udtChannel(zch).ValveDiDoPin) Or _
                    (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then

                    msgtemp(i) = mMsgFUAdd_OUT(udt1.udtChannel(ich).ValveDiDoFuNo, udt1.udtChannel(ich).ValveDiDoPortNo, udt1.udtChannel(ich).ValveDiDoPin, _
                                                udt2.udtChannel(zch).ValveDiDoFuNo, udt2.udtChannel(zch).ValveDiDoPortNo, udt2.udtChannel(zch).ValveDiDoPin, _
                                                udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
                    i = i + 1
                End If
            End If



            ''出力点数
            If gCompareChk_6_DIDO(6) = True Then 'Output Count
                If udt1.udtChannel(ich).ValveDiDoPinNo <> udt2.udtChannel(zch).ValveDiDoPinNo Then
                    msgtemp(i) = mMsgCreateSht("Output Count", udt1.udtChannel(ich).ValveDiDoPinNo, udt2.udtChannel(zch).ValveDiDoPinNo)
                    i = i + 1
                End If
            End If

            ''出力制御種別
            If gCompareChk_6_DIDO(7) = True Then 'Output Type
                If udt1.udtChannel(ich).ValveDiDoControl <> udt2.udtChannel(zch).ValveDiDoControl Then
                    msgtemp(i) = mMsgCreateSht("Output Type", udt1.udtChannel(ich).ValveDiDoControl, udt2.udtChannel(zch).ValveDiDoControl)
                    i = i + 1
                End If
            End If

            ''出力パルス幅
            If gCompareChk_6_DIDO(8) = True Then 'Output width
                If udt1.udtChannel(ich).ValveDiDoWidth <> udt2.udtChannel(zch).ValveDiDoWidth Then
                    msgtemp(i) = mMsgCreateSht("Output width", udt1.udtChannel(ich).ValveDiDoWidth, udt2.udtChannel(zch).ValveDiDoWidth)
                    i = i + 1
                End If
            End If

            ''出力ステータス種別コード
            If gCompareChk_6_DIDO(9) = True Then 'Output Status
                If udt1.udtChannel(ich).ValveDiDoStatus <> udt2.udtChannel(zch).ValveDiDoStatus Then
                    'Ver2.0.2.8 Output Status を名称にする
                    Dim strOld As String = GetChStatus(udt1.udtChannel(ich).ValveDiDoStatus)
                    Dim strNew As String = GetChStatus(udt2.udtChannel(zch).ValveDiDoStatus)
                    'msgtemp(i) = mMsgCreateStr("Output Status", "0x" & Hex(udt1.udtChannel(ich).ValveDiDoStatus).PadLeft(2, "0"), "0x" & Hex(udt2.udtChannel(zch).ValveDiDoStatus).PadLeft(2, "0"))
                    msgtemp(i) = mMsgCreateStr("Output Status", strOld, strNew)
                    i = i + 1
                End If
            End If

            ''出力ステータス情報　--------------------------------------------------------

            ''ステータス名称１
            If gCompareChk_6_DIDO(10) = True Then 'STATUS1
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus1, udt2.udtChannel(zch).ValveDiDoOutStatus1) Then
                    msgtemp(i) = mMsgCreateStr("STATUS1", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus1), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus1))
                    i = i + 1
                End If
            End If

            ''ステータス名称２
            If gCompareChk_6_DIDO(11) = True Then 'STATUS2
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus2, udt2.udtChannel(zch).ValveDiDoOutStatus2) Then
                    msgtemp(i) = mMsgCreateStr("STATUS2", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus2), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus2))
                    i = i + 1
                End If
            End If

            ''ステータス名称３
            If gCompareChk_6_DIDO(12) = True Then 'STATUS3
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus3, udt2.udtChannel(zch).ValveDiDoOutStatus3) Then
                    msgtemp(i) = mMsgCreateStr("STATUS3", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus3), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus3))
                    i = i + 1
                End If
            End If

            ''ステータス名称４
            If gCompareChk_6_DIDO(13) = True Then 'STATUS4
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus4, udt2.udtChannel(zch).ValveDiDoOutStatus4) Then
                    msgtemp(i) = mMsgCreateStr("STATUS4", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus4), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus4))
                    i = i + 1
                End If
            End If

            ''ステータス名称５
            If gCompareChk_6_DIDO(14) = True Then 'STATUS5
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus5, udt2.udtChannel(zch).ValveDiDoOutStatus5) Then
                    msgtemp(i) = mMsgCreateStr("STATUS5", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus5), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus5))
                    i = i + 1
                End If
            End If

            ''ステータス名称６
            If gCompareChk_6_DIDO(15) = True Then 'STATUS6
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus6, udt2.udtChannel(zch).ValveDiDoOutStatus6) Then
                    msgtemp(i) = mMsgCreateStr("STATUS6", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus6), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus6))
                    i = i + 1
                End If
            End If

            ''ステータス名称７
            If gCompareChk_6_DIDO(16) = True Then 'STATUS7
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus7, udt2.udtChannel(zch).ValveDiDoOutStatus7) Then
                    msgtemp(i) = mMsgCreateStr("STATUS7", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus7), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus7))
                    i = i + 1
                End If
            End If

            ''ステータス名称８
            If gCompareChk_6_DIDO(17) = True Then 'STATUS8
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoOutStatus8, udt2.udtChannel(zch).ValveDiDoOutStatus8) Then
                    msgtemp(i) = mMsgCreateStr("STATUS8", gGetString(udt1.udtChannel(ich).ValveDiDoOutStatus8), gGetString(udt2.udtChannel(zch).ValveDiDoOutStatus8))
                    i = i + 1
                End If
            End If

            ''フィードバックアラーム情報 ----------------------------------------------------

            ''フィードバックアラーム有無
            If gCompareChk_6_DIDO(18) = True Then 'FB Use
                If udt1.udtChannel(ich).ValveDiDoAlarmUse <> udt2.udtChannel(zch).ValveDiDoAlarmUse Then
                    msgtemp(i) = mMsgCreateSht("FB Use", udt1.udtChannel(ich).ValveDiDoAlarmUse, udt2.udtChannel(zch).ValveDiDoAlarmUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_6_DIDO(19) = True Then 'FB DLY
                If udt1.udtChannel(ich).ValveDiDoAlarmDelay <> udt2.udtChannel(zch).ValveDiDoAlarmDelay Or _
                    (udt1.udtChannel(ich).DummyFaDelay <> udt2.udtChannel(zch).DummyFaDelay) Then
                    'msgtemp(i) = mMsgCreateSht("FB DLY", udt1.udtChannel(ich).ValveDiDoAlarmDelay, udt2.udtChannel(zch).ValveDiDoAlarmDelay)
                    msgtemp(i) = mMsgCreateint("FB DLY", udt1.udtChannel(ich).ValveDiDoAlarmDelay, udt2.udtChannel(zch).ValveDiDoAlarmDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaDelay, udt2.udtChannel(zch).DummyFaDelay)
                    i = i + 1
                End If
            End If

            ''規定値１
            If gCompareChk_6_DIDO(20) = True Then 'FB SP1
                If udt1.udtChannel(ich).ValveDiDoAlarmSp1 <> udt2.udtChannel(zch).ValveDiDoAlarmSp1 Then
                    msgtemp(i) = mMsgCreateSht("FB SP1", udt1.udtChannel(ich).ValveDiDoAlarmSp1, udt2.udtChannel(zch).ValveDiDoAlarmSp1)
                    i = i + 1
                End If
            End If

            ''規定値２
            If gCompareChk_6_DIDO(21) = True Then 'FB SP2
                If udt1.udtChannel(ich).ValveDiDoAlarmSp2 <> udt2.udtChannel(zch).ValveDiDoAlarmSp2 Then
                    msgtemp(i) = mMsgCreateSht("FB SP2", udt1.udtChannel(ich).ValveDiDoAlarmSp2, udt2.udtChannel(zch).ValveDiDoAlarmSp2)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（開処理用）
            If gCompareChk_6_DIDO(22) = True Then 'FB hys1
                If udt1.udtChannel(ich).ValveDiDoAlarmHys1 <> udt2.udtChannel(zch).ValveDiDoAlarmHys1 Then
                    msgtemp(i) = mMsgCreateSht("FB hys1", udt1.udtChannel(ich).ValveDiDoAlarmHys1, udt2.udtChannel(zch).ValveDiDoAlarmHys1)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（閉処理用）
            If gCompareChk_6_DIDO(23) = True Then 'FB hys2
                If udt1.udtChannel(ich).ValveDiDoAlarmHys2 <> udt2.udtChannel(zch).ValveDiDoAlarmHys2 Then
                    msgtemp(i) = mMsgCreateSht("FB hys2", udt1.udtChannel(ich).ValveDiDoAlarmHys2, udt2.udtChannel(zch).ValveDiDoAlarmHys2)
                    i = i + 1
                End If
            End If

            ''サンプリング時間
            If gCompareChk_6_DIDO(24) = True Then 'FB Sampling
                If udt1.udtChannel(ich).ValveDiDoAlarmSt <> udt2.udtChannel(zch).ValveDiDoAlarmSt Then
                    msgtemp(i) = mMsgCreateSht("FB Sampling", udt1.udtChannel(ich).ValveDiDoAlarmSt, udt2.udtChannel(zch).ValveDiDoAlarmSt)
                    i = i + 1
                End If
            End If

            ''変化量       '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰ設定ﾌﾗｸﾞ追加
            If gCompareChk_6_DIDO(25) = True Then 'FB Variation
                If udt1.udtChannel(ich).ValveDiDoAlarmVar <> udt2.udtChannel(zch).ValveDiDoAlarmVar Then
                    msgtemp(i) = mMsgCreateint("FB Variation", udt1.udtChannel(ich).ValveDiDoAlarmVar, udt2.udtChannel(zch).ValveDiDoAlarmVar, 0, 0, False, False)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_6_DIDO(26) = True Then 'FB EX
                If udt1.udtChannel(ich).ValveDiDoAlarmExtGroup <> udt2.udtChannel(zch).ValveDiDoAlarmExtGroup Or _
                    (udt1.udtChannel(ich).DummyFaExtGr <> udt2.udtChannel(zch).DummyFaExtGr) Then
                    'msgtemp(i) = mMsgCreateByt("FB EX", udt1.udtChannel(ich).ValveDiDoAlarmExtGroup, udt2.udtChannel(zch).ValveDiDoAlarmExtGroup)
                    msgtemp(i) = mMsgCreateint("FB EX", udt1.udtChannel(ich).ValveDiDoAlarmExtGroup, udt2.udtChannel(zch).ValveDiDoAlarmExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaExtGr, udt2.udtChannel(zch).DummyFaExtGr)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_6_DIDO(27) = True Then 'FB GR1
                If udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose1 <> udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyFaGrep1 <> udt2.udtChannel(zch).DummyFaGrep1) Then
                    'msgtemp(i) = mMsgCreateByt("FB GR1", udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose1, udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose1)
                    msgtemp(i) = mMsgCreateint("FB GR1", udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose1, udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaGrep1, udt2.udtChannel(zch).DummyFaGrep1)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_6_DIDO(28) = True Then 'FB GR2
                If udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose2 <> udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyFaGrep2 <> udt2.udtChannel(zch).DummyFaGrep2) Then
                    'msgtemp(i) = mMsgCreateByt("FB GR2", udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose2, udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose2)
                    msgtemp(i) = mMsgCreateint("FB GR2", udt1.udtChannel(ich).ValveDiDoAlarmGroupRepose2, udt2.udtChannel(zch).ValveDiDoAlarmGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyFaGrep2, udt2.udtChannel(zch).DummyFaGrep2)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_6_DIDO(29) = True Then 'FB STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveDiDoAlarmStatusInput), NZfS(udt2.udtChannel(zch).ValveDiDoAlarmStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyFaStaNm <> udt2.udtChannel(zch).DummyFaStaNm) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyFaStaNm, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyFaStaNm, "#", "")
                    msgtemp(i) = mMsgCreateStr("FB STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveDiDoAlarmStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveDiDoAlarmStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_6_DIDO(30) = True Then 'FB MR Status
                If udt1.udtChannel(ich).ValveDiDoAlarmManualReposeState <> udt2.udtChannel(zch).ValveDiDoAlarmManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("FB MR Status", udt1.udtChannel(ich).ValveDiDoAlarmManualReposeState, udt2.udtChannel(zch).ValveDiDoAlarmManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_6_DIDO(31) = True Then 'FB MR Set
                If udt1.udtChannel(ich).ValveDiDoAlarmManualReposeSet <> udt2.udtChannel(zch).ValveDiDoAlarmManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("FB MR Set", udt1.udtChannel(ich).ValveDiDoAlarmManualReposeSet, udt2.udtChannel(zch).ValveDiDoAlarmManualReposeSet)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_6_DIDO(32) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).ValveDiDoTagNo, udt2.udtChannel(zch).ValveDiDoTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).ValveDiDoTagNo), gGetString(udt2.udtChannel(zch).ValveDiDoTagNo))
                    i = i + 1
                End If
            End If


            'Alarm MimicNo
            If gCompareChk_6_DIDO(34) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).ValveDiDoAlmMimic <> udt2.udtChannel(zch).ValveDiDoAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).ValveDiDoAlmMimic, udt2.udtChannel(zch).ValveDiDoAlmMimic)
                    i = i + 1
                End If
            End If


            'ダミー設定
            If gCompareChk_6_DIDO(33) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup, i)
                'i = GetDummy("DMY ALM DLY", udt1.udtChannel(ich).DummyCommonDelay, udt2.udtChannel(zch).DummyCommonDelay, i)
                'i = GetDummy("DMY ALM GR1", udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1, i)
                'i = GetDummy("DMY ALM GR2", udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2, i)
                'i = GetDummy("DMY ALM DI Start", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM Pin No.", udt1.udtChannel(ich).DummyCommonPinNo, udt2.udtChannel(zch).DummyCommonPinNo, i)
                'i = GetDummy("DMY ALM Status Name", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)

                'i = GetDummy("DMY JACOM EXT G", udt1.udtChannel(ich).DummyFaExtGr, udt2.udtChannel(zch).DummyFaExtGr, i)
                'i = GetDummy("DMY JACOM DLY", udt1.udtChannel(ich).DummyFaDelay, udt2.udtChannel(zch).DummyFaDelay, i)
                'i = GetDummy("DMY JACOM GP1", udt1.udtChannel(ich).DummyFaGrep1, udt2.udtChannel(zch).DummyFaGrep1, i)
                'i = GetDummy("DMY JACOM GP2", udt1.udtChannel(ich).DummyFaGrep2, udt2.udtChannel(zch).DummyFaGrep2, i)
                'i = GetDummy("DMY JACOM STATUS NAME", udt1.udtChannel(ich).DummyFaStaNm, udt2.udtChannel(zch).DummyFaStaNm, i)
                'i = GetDummy("DMY JACOM TIME Value", udt1.udtChannel(ich).DummyFaTimeV, udt2.udtChannel(zch).DummyFaTimeV, i)

                'i = GetDummy("DMY FU Address", udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress, i)
            End If
            '変更項目数を返す()
            mCompareSetChannelValveDiDoDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(バルブ AI-DO)OK"

    Friend Function mCompareSetChannelValveAiDoDisp(ByVal udt1 As gTypSetChInfo, _
                                                    ByVal udt2 As gTypSetChInfo, _
                                                    ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更

            '========================
            ''バルブ AI-DO
            '========================

            Dim i As Integer = Gyo

            ''アラーム　HH ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_7_AIDO(0) = True Then 'ALM HH Use
                If udt1.udtChannel(ich).ValveAiDoHiHiUse <> udt2.udtChannel(zch).ValveAiDoHiHiUse Then
                    msgtemp(i) = mMsgCreateSht("ALM HH Use", udt1.udtChannel(ich).ValveAiDoHiHiUse, udt2.udtChannel(zch).ValveAiDoHiHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(1) = True Then 'ALM HH DLY
                If (udt1.udtChannel(ich).ValveAiDoHiHiDelay <> udt2.udtChannel(zch).ValveAiDoHiHiDelay) Or _
                    (udt1.udtChannel(ich).DummyDelayHH <> udt2.udtChannel(zch).DummyDelayHH) Then
                    'msgtemp(i) = mMsgCreateSht("ALM HH DLY", udt1.udtChannel(ich).ValveAiDoHiHiDelay, udt2.udtChannel(zch).ValveAiDoHiHiDelay)
                    msgtemp(i) = mMsgCreateint("ALM HH DLY", udt1.udtChannel(ich).ValveAiDoHiHiDelay, udt2.udtChannel(zch).ValveAiDoHiHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_7_AIDO(2) = True Then 'ALM HH SET
                If (udt1.udtChannel(ich).ValveAiDoHiHiValue <> udt2.udtChannel(zch).ValveAiDoHiHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueHH <> udt2.udtChannel(zch).DummyValueHH) Then
                    msgtemp(i) = mMsgCreateint("ALM HH SET", udt1.udtChannel(ich).ValveAiDoHiHiValue, udt2.udtChannel(zch).ValveAiDoHiHiValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(3) = True Then 'ALM HH EX
                If (udt1.udtChannel(ich).ValveAiDoHiHiExtGroup <> udt2.udtChannel(zch).ValveAiDoHiHiExtGroup) Or _
                    (udt1.udtChannel(ich).DummyExtGrHH <> udt2.udtChannel(zch).DummyExtGrHH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH EX", udt1.udtChannel(ich).ValveAiDoHiHiExtGroup, udt2.udtChannel(zch).ValveAiDoHiHiExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM HH EX", udt1.udtChannel(ich).ValveAiDoHiHiExtGroup, udt2.udtChannel(zch).ValveAiDoHiHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(4) = True Then 'ALM HH GR1
                If (udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose1) Or _
                    (udt1.udtChannel(ich).DummyGRep1HH <> udt2.udtChannel(zch).DummyGRep1HH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH GR1", udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose1, udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM HH GR1", udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose1, udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_7_AIDO(5) = True Then 'ALM HH GR2
                If (udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose2) Or _
                    (udt1.udtChannel(ich).DummyGRep2HH <> udt2.udtChannel(zch).DummyGRep2HH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH GR2", udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose2, udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM HH GR2", udt1.udtChannel(ich).ValveAiDoHiHiGroupRepose2, udt2.udtChannel(zch).ValveAiDoHiHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(6) = True Then 'ALM HH STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiDoHiHiStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoHiHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmHH <> udt2.udtChannel(zch).DummyStaNmHH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmHH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmHH, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM HH STATUS", strOldDmy & udt1.udtChannel(ich).ValveAiDoHiHiStatusInput, strNewDmy & udt2.udtChannel(zch).ValveAiDoHiHiStatusInput)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(7) = True Then 'ALM HH MR Status
                If udt1.udtChannel(ich).ValveAiDoHiHiManualReposeState <> udt2.udtChannel(zch).ValveAiDoHiHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM HH MR Status", udt1.udtChannel(ich).ValveAiDoHiHiManualReposeState, udt2.udtChannel(zch).ValveAiDoHiHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(8) = True Then 'ALM HH MR Set
                If udt1.udtChannel(ich).ValveAiDoHiHiManualReposeSet <> udt2.udtChannel(zch).ValveAiDoHiHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM HH MR Set", udt1.udtChannel(ich).ValveAiDoHiHiManualReposeSet, udt2.udtChannel(zch).ValveAiDoHiHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　H ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_7_AIDO(9) = True Then 'ALM H Use
                If udt1.udtChannel(ich).ValveAiDoHiUse <> udt2.udtChannel(zch).ValveAiDoHiUse Then
                    msgtemp(i) = mMsgCreateSht("ALM H Use", udt1.udtChannel(ich).ValveAiDoHiUse, udt2.udtChannel(zch).ValveAiDoHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(10) = True Then 'ALM H DLY
                If udt1.udtChannel(ich).ValveAiDoHiDelay <> udt2.udtChannel(zch).ValveAiDoHiDelay Or _
                    (udt1.udtChannel(ich).DummyDelayH <> udt2.udtChannel(zch).DummyDelayH) Then
                    'msgtemp(i) = mMsgCreateSht("ALM H DLY", udt1.udtChannel(ich).ValveAiDoHiDelay, udt2.udtChannel(zch).ValveAiDoHiDelay)
                    msgtemp(i) = mMsgCreateint("ALM H DLY", udt1.udtChannel(ich).ValveAiDoHiDelay, udt2.udtChannel(zch).ValveAiDoHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_7_AIDO(11) = True Then 'ALM H SET
                If (udt1.udtChannel(ich).ValveAiDoHiValue <> udt2.udtChannel(zch).ValveAiDoHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueH <> udt2.udtChannel(zch).DummyValueH) Then
                    msgtemp(i) = mMsgCreateint("ALM H SET", udt1.udtChannel(ich).ValveAiDoHiValue, udt2.udtChannel(zch).ValveAiDoHiValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(12) = True Then 'ALM H EX
                If udt1.udtChannel(ich).ValveAiDoHiExtGroup <> udt2.udtChannel(zch).ValveAiDoHiExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrH <> udt2.udtChannel(zch).DummyExtGrH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H EX", udt1.udtChannel(ich).ValveAiDoHiExtGroup, udt2.udtChannel(zch).ValveAiDoHiExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM H EX", udt1.udtChannel(ich).ValveAiDoHiExtGroup, udt2.udtChannel(zch).ValveAiDoHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(13) = True Then 'ALM H GR1
                If udt1.udtChannel(ich).ValveAiDoHiGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoHiGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1H <> udt2.udtChannel(zch).DummyGRep1H) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H GR1", udt1.udtChannel(ich).ValveAiDoHiGroupRepose1, udt2.udtChannel(zch).ValveAiDoHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM H GR1", udt1.udtChannel(ich).ValveAiDoHiGroupRepose1, udt2.udtChannel(zch).ValveAiDoHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_7_AIDO(14) = True Then 'ALM H GR2
                If udt1.udtChannel(ich).ValveAiDoHiGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2H <> udt2.udtChannel(zch).DummyGRep2H) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H GR2", udt1.udtChannel(ich).ValveAiDoHiGroupRepose2, udt2.udtChannel(zch).ValveAiDoHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM H GR2", udt1.udtChannel(ich).ValveAiDoHiGroupRepose2, udt2.udtChannel(zch).ValveAiDoHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(15) = True Then 'ALM H STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiDoHiStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmH <> udt2.udtChannel(zch).DummyStaNmH) Then

                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmH, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM H STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiDoHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiDoHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(16) = True Then 'ALM H MR Status
                If udt1.udtChannel(ich).ValveAiDoHiManualReposeState <> udt2.udtChannel(zch).ValveAiDoHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM H MR Status", udt1.udtChannel(ich).ValveAiDoHiManualReposeState, udt2.udtChannel(zch).ValveAiDoHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(17) = True Then 'ALM H MR Set
                If udt1.udtChannel(ich).ValveAiDoHiManualReposeSet <> udt2.udtChannel(zch).ValveAiDoHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM H MR Set", udt1.udtChannel(ich).ValveAiDoHiManualReposeSet, udt2.udtChannel(zch).ValveAiDoHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　L ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_7_AIDO(18) = True Then 'ALM L Use
                If udt1.udtChannel(ich).ValveAiDoLoUse <> udt2.udtChannel(zch).ValveAiDoLoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM L Use", udt1.udtChannel(ich).ValveAiDoLoUse, udt2.udtChannel(zch).ValveAiDoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(19) = True Then 'ALM L DLY
                If udt1.udtChannel(ich).ValveAiDoLoDelay <> udt2.udtChannel(zch).ValveAiDoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayL <> udt2.udtChannel(zch).DummyDelayL) Then
                    'msgtemp(i) = mMsgCreateSht("ALM L DLY", udt1.udtChannel(ich).ValveAiDoLoDelay, udt2.udtChannel(zch).ValveAiDoLoDelay)
                    msgtemp(i) = mMsgCreateint("ALM L DLY", udt1.udtChannel(ich).ValveAiDoLoDelay, udt2.udtChannel(zch).ValveAiDoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_7_AIDO(20) = True Then 'ALM L SET
                If (udt1.udtChannel(ich).ValveAiDoLoValue <> udt2.udtChannel(zch).ValveAiDoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueL <> udt2.udtChannel(zch).DummyValueL) Then
                    msgtemp(i) = mMsgCreateint("ALM L SET", udt1.udtChannel(ich).ValveAiDoLoValue, udt2.udtChannel(zch).ValveAiDoLoValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(21) = True Then 'ALM L EX
                If udt1.udtChannel(ich).ValveAiDoLoExtGroup <> udt2.udtChannel(zch).ValveAiDoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrL <> udt2.udtChannel(zch).DummyExtGrL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L EX", udt1.udtChannel(ich).ValveAiDoLoExtGroup, udt2.udtChannel(zch).ValveAiDoLoExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM L EX", udt1.udtChannel(ich).ValveAiDoLoExtGroup, udt2.udtChannel(zch).ValveAiDoLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(22) = True Then 'ALM L GR1
                If udt1.udtChannel(ich).ValveAiDoLoGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1L <> udt2.udtChannel(zch).DummyGRep1L) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L GR1", udt1.udtChannel(ich).ValveAiDoLoGroupRepose1, udt2.udtChannel(zch).ValveAiDoLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM L GR1", udt1.udtChannel(ich).ValveAiDoLoGroupRepose1, udt2.udtChannel(zch).ValveAiDoLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_7_AIDO(23) = True Then 'ALM L GR2
                If udt1.udtChannel(ich).ValveAiDoLoGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2L <> udt2.udtChannel(zch).DummyGRep2L) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L GR2", udt1.udtChannel(ich).ValveAiDoLoGroupRepose2, udt2.udtChannel(zch).ValveAiDoLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM L GR2", udt1.udtChannel(ich).ValveAiDoLoGroupRepose2, udt2.udtChannel(zch).ValveAiDoLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(24) = True Then 'ALM L STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiDoLoStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmL <> udt2.udtChannel(zch).DummyStaNmL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmL, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM L STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiDoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiDoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(25) = True Then 'ALM L MR Status
                If udt1.udtChannel(ich).ValveAiDoLoManualReposeState <> udt2.udtChannel(zch).ValveAiDoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM L MR Status", udt1.udtChannel(ich).ValveAiDoLoManualReposeState, udt2.udtChannel(zch).ValveAiDoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(26) = True Then 'ALM L MR Set
                If udt1.udtChannel(ich).ValveAiDoLoManualReposeSet <> udt2.udtChannel(zch).ValveAiDoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM L MR Set", udt1.udtChannel(ich).ValveAiDoLoManualReposeSet, udt2.udtChannel(zch).ValveAiDoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　LL ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_7_AIDO(27) = True Then 'ALM LL Use
                If udt1.udtChannel(ich).ValveAiDoLoLoUse <> udt2.udtChannel(zch).ValveAiDoLoLoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM LL Use", udt1.udtChannel(ich).ValveAiDoLoLoUse, udt2.udtChannel(zch).ValveAiDoLoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(28) = True Then 'ALM LL DLY
                If udt1.udtChannel(ich).ValveAiDoLoLoDelay <> udt2.udtChannel(zch).ValveAiDoLoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayLL <> udt2.udtChannel(zch).DummyDelayLL) Then
                    'msgtemp(i) = mMsgCreateSht("ALM LL DLY", udt1.udtChannel(ich).ValveAiDoLoLoDelay, udt2.udtChannel(zch).ValveAiDoLoLoDelay)
                    msgtemp(i) = mMsgCreateint("ALM LL DLY", udt1.udtChannel(ich).ValveAiDoLoLoDelay, udt2.udtChannel(zch).ValveAiDoLoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_7_AIDO(29) = True Then 'ALM LL Set
                If (udt1.udtChannel(ich).ValveAiDoLoLoValue <> udt2.udtChannel(zch).ValveAiDoLoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueLL <> udt2.udtChannel(zch).DummyValueLL) Then
                    msgtemp(i) = mMsgCreateint("ALM LL Set", udt1.udtChannel(ich).ValveAiDoLoLoValue, udt2.udtChannel(zch).ValveAiDoLoLoValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(30) = True Then 'ALM LL EX
                If udt1.udtChannel(ich).ValveAiDoLoLoExtGroup <> udt2.udtChannel(zch).ValveAiDoLoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrLL <> udt2.udtChannel(zch).DummyExtGrLL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL EX", udt1.udtChannel(ich).ValveAiDoLoLoExtGroup, udt2.udtChannel(zch).ValveAiDoLoLoExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM LL EX", udt1.udtChannel(ich).ValveAiDoLoLoExtGroup, udt2.udtChannel(zch).ValveAiDoLoLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(31) = True Then 'ALM LL GR1
                If udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1LL <> udt2.udtChannel(zch).DummyGRep1LL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL GR1", udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose1, udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM LL GR1", udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose1, udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_7_AIDO(32) = True Then 'ALM LL GR2
                If udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2LL <> udt2.udtChannel(zch).DummyGRep2LL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL GR2", udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose2, udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM LL GR2", udt1.udtChannel(ich).ValveAiDoLoLoGroupRepose2, udt2.udtChannel(zch).ValveAiDoLoLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(33) = True Then 'ALM LL STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(i).ValveAiDoLoLoStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoLoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmLL <> udt2.udtChannel(zch).DummyStaNmLL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmLL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmLL, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM LL STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiDoLoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiDoLoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(34) = True Then 'ALM LL MR Status
                If udt1.udtChannel(ich).ValveAiDoLoLoManualReposeState <> udt2.udtChannel(zch).ValveAiDoLoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM LL MR Status", udt1.udtChannel(ich).ValveAiDoLoLoManualReposeState, udt2.udtChannel(zch).ValveAiDoLoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(35) = True Then 'ALM LL MR Set
                If udt1.udtChannel(ich).ValveAiDoLoLoManualReposeSet <> udt2.udtChannel(zch).ValveAiDoLoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM LL MR Set", udt1.udtChannel(ich).ValveAiDoLoLoManualReposeSet, udt2.udtChannel(zch).ValveAiDoLoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　SF  ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_7_AIDO(36) = True Then 'ALM S Use
                If udt1.udtChannel(ich).ValveAiDoSensorFailUse <> udt2.udtChannel(zch).ValveAiDoSensorFailUse Then
                    msgtemp(i) = mMsgCreateSht("ALM S Use", udt1.udtChannel(ich).ValveAiDoSensorFailUse, udt2.udtChannel(zch).ValveAiDoSensorFailUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(37) = True Then 'ALM S DLY
                If udt1.udtChannel(ich).ValveAiDoSensorFailDelay <> udt2.udtChannel(zch).ValveAiDoSensorFailDelay Or _
                    (udt1.udtChannel(ich).DummyDelaySF <> udt2.udtChannel(zch).DummyDelaySF) Then
                    'msgtemp(i) = mMsgCreateSht("ALM S DLY", udt1.udtChannel(ich).ValveAiDoSensorFailDelay, udt2.udtChannel(zch).ValveAiDoSensorFailDelay)
                    msgtemp(i) = mMsgCreateint("ALM S DLY", udt1.udtChannel(ich).ValveAiDoSensorFailDelay, udt2.udtChannel(zch).ValveAiDoSensorFailDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_7_AIDO(38) = True Then 'ALM S SET
                If (udt1.udtChannel(ich).ValveAiDoSensorFailValue <> udt2.udtChannel(zch).ValveAiDoSensorFailValue) Or _
                    (udt1.udtChannel(ich).DummyValueSF <> udt2.udtChannel(zch).DummyValueSF) Then
                    msgtemp(i) = mMsgCreateint("ALM S SET", udt1.udtChannel(ich).ValveAiDoSensorFailValue, udt2.udtChannel(zch).ValveAiDoSensorFailValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueSF, udt2.udtChannel(zch).DummyValueSF)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(39) = True Then 'ALM S EX
                If udt1.udtChannel(ich).ValveAiDoSensorFailExtGroup <> udt2.udtChannel(zch).ValveAiDoSensorFailExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrSF <> udt2.udtChannel(zch).DummyExtGrSF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM S EX", udt1.udtChannel(ich).ValveAiDoSensorFailExtGroup, udt2.udtChannel(zch).ValveAiDoSensorFailExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM S EX", udt1.udtChannel(ich).ValveAiDoSensorFailExtGroup, udt2.udtChannel(zch).ValveAiDoSensorFailExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(40) = True Then 'ALM S GR1
                If udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1SF <> udt2.udtChannel(zch).DummyGRep1SF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM S GR1", udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose1, udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM S GR1", udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose1, udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF)
                    i = i + 1
                End If
            End If


            ''グループリポーズ２
            If gCompareChk_7_AIDO(41) = True Then 'ALM S GR2
                If udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2SF <> udt2.udtChannel(zch).DummyGRep2SF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM S GR2", udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose2, udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM S GR2", udt1.udtChannel(ich).ValveAiDoSensorFailGroupRepose2, udt2.udtChannel(zch).ValveAiDoSensorFailGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(42) = True Then 'ALM S STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(i).ValveAiDoSensorFailStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoSensorFailStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmSF <> udt2.udtChannel(zch).DummyStaNmSF) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmH, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM S STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiDoSensorFailStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiDoSensorFailStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(43) = True Then 'ALM S MR Status
                If udt1.udtChannel(ich).ValveAiDoSensorFailManualReposeState <> udt2.udtChannel(zch).ValveAiDoSensorFailManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM S MR Status", udt1.udtChannel(ich).ValveAiDoSensorFailManualReposeState, udt2.udtChannel(zch).ValveAiDoSensorFailManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(44) = True Then 'ALM S MR Set
                If udt1.udtChannel(ich).ValveAiDoSensorFailManualReposeSet <> udt2.udtChannel(zch).ValveAiDoSensorFailManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM S MR Set", udt1.udtChannel(ich).ValveAiDoSensorFailManualReposeSet, udt2.udtChannel(zch).ValveAiDoSensorFailManualReposeSet)
                    i = i + 1
                End If
            End If

            ''-----------------------------------------------------------------------------------------------

            ''スケール値　上限/下限値      '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_7_AIDO(45) = True Then 'RANGE
                i = GetRange("RANGE", udt1.udtChannel(ich).ValveAiDoRangeHigh, udt2.udtChannel(zch).ValveAiDoRangeHigh, _
                             udt1.udtChannel(ich).ValveAiDoRangeLow, udt2.udtChannel(zch).ValveAiDoRangeLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)
            End If

            ''ノーマルレンジ　上限/下限値    '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_7_AIDO(46) = True Then 'NOR RANGE
                i = GetNorRange("NOR RANGE", udt1.udtChannel(ich).ValveAiDoNormalHigh, udt2.udtChannel(zch).ValveAiDoNormalHigh, _
                             udt1.udtChannel(ich).ValveAiDoNormalLow, udt2.udtChannel(zch).ValveAiDoNormalLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, _
                             udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)
            End If

            ''オフセット値        '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_7_AIDO(47) = True Then 'OFFSET
                If udt1.udtChannel(ich).ValveAiDoOffsetValue <> udt2.udtChannel(zch).ValveAiDoOffsetValue Then
                    msgtemp(i) = mMsgCreateint("OFFSET", udt1.udtChannel(ich).ValveAiDoOffsetValue, udt2.udtChannel(zch).ValveAiDoOffsetValue, _
                                               udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition, False, False)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_7_AIDO(48) = True Then 'String
                If udt1.udtChannel(ich).ValveAiDoString <> udt2.udtChannel(zch).ValveAiDoString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).ValveAiDoString, udt2.udtChannel(zch).ValveAiDoString)
                    i = i + 1
                End If
            End If

            ''小数点以下桁数
            If gCompareChk_7_AIDO(49) = True Then 'Decimal Point
                If udt1.udtChannel(ich).ValveAiDoDecimalPosition <> udt2.udtChannel(zch).ValveAiDoDecimalPosition Then
                    msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).ValveAiDoDecimalPosition, udt2.udtChannel(zch).ValveAiDoDecimalPosition)
                    i = i + 1
                End If
            End If

            ''センター表示
            If gCompareChk_7_AIDO(50) = True Then 'BAR GRAPH CENTER
                i = GetBitCHK("BAR GRAPH CENTER", udt1.udtChannel(ich).ValveAiDoDisplay3, 0, udt2.udtChannel(zch).ValveAiDoDisplay3, 0, i)
            End If

            ''Sensor異常表示
            If gCompareChk_7_AIDO(51) = True Then 'SENSOR ALM(UNDER OVER)
                i = GetBitCHK("SENSOR ALM(UNDER)", udt1.udtChannel(ich).ValveAiDoDisplay3, 2, udt2.udtChannel(zch).ValveAiDoDisplay3, 2, i)
                i = GetBitCHK("SENSOR ALM(OVER)", udt1.udtChannel(ich).ValveAiDoDisplay3, 3, udt2.udtChannel(zch).ValveAiDoDisplay3, 3, i)
            End If

            ''フィードバックアラームタイマ値
            If gCompareChk_7_AIDO(52) = True Then 'FB Timer
                If udt1.udtChannel(ich).ValveAiDoFeedback <> udt2.udtChannel(zch).ValveAiDoFeedback Then
                    msgtemp(i) = mMsgCreateSht("FB Timer", udt1.udtChannel(ich).ValveAiDoFeedback, udt2.udtChannel(zch).ValveAiDoFeedback)
                    i = i + 1
                End If
            End If


            'Ver2.0.4.1
            '出力FUｱﾄﾞﾚｽは、IN_FUｱﾄﾞﾚｽと同様に一つにまとめる
            ''外部出力 FU 番号
            'If gCompareChk_7_AIDO(53) = True Then 'OUT FU No
            '    If udt1.udtChannel(ich).ValveAiDoFuNo <> udt2.udtChannel(zch).ValveAiDoFuNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU No", udt1.udtChannel(ich).ValveAiDoFuNo, udt2.udtChannel(zch).ValveAiDoFuNo)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU ポート番号
            'If gCompareChk_7_AIDO(54) = True Then 'OUT FU Slot
            '    If udt1.udtChannel(ich).ValveAiDoPortNo <> udt2.udtChannel(zch).ValveAiDoPortNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Slot", udt1.udtChannel(ich).ValveAiDoPortNo, udt2.udtChannel(zch).ValveAiDoPortNo)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU 端子番号
            'If gCompareChk_7_AIDO(55) = True Then 'OUT FU Pin
            '    If udt1.udtChannel(ich).ValveAiDoPin <> udt2.udtChannel(zch).ValveAiDoPin Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Pin", udt1.udtChannel(ich).ValveAiDoPin, udt2.udtChannel(zch).ValveAiDoPin)
            '        i = i + 1
            '    End If
            'End If
            If gCompareChk_7_AIDO(53) = True Or gCompareChk_7_AIDO(54) = True Or gCompareChk_7_AIDO(55) = True Then
                If (udt1.udtChannel(ich).ValveAiDoFuNo <> udt2.udtChannel(zch).ValveAiDoFuNo Or _
                    udt1.udtChannel(ich).ValveAiDoPortNo <> udt2.udtChannel(zch).ValveAiDoPortNo Or _
                    udt1.udtChannel(ich).ValveAiDoPin <> udt2.udtChannel(zch).ValveAiDoPin) Or _
                    (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then

                    msgtemp(i) = mMsgFUAdd_OUT(udt1.udtChannel(ich).ValveAiDoFuNo, udt1.udtChannel(ich).ValveAiDoPortNo, udt1.udtChannel(ich).ValveAiDoPin, _
                                                udt2.udtChannel(zch).ValveAiDoFuNo, udt2.udtChannel(zch).ValveAiDoPortNo, udt2.udtChannel(zch).ValveAiDoPin, _
                                                udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
                    i = i + 1
                End If
            End If




            ''出力点数
            If gCompareChk_7_AIDO(56) = True Then 'Output Count
                If udt1.udtChannel(ich).ValveAiDoPinNo <> udt2.udtChannel(zch).ValveAiDoPinNo Then
                    msgtemp(i) = mMsgCreateSht("Output Count", udt1.udtChannel(ich).ValveAiDoPinNo, udt2.udtChannel(zch).ValveAiDoPinNo)
                    i = i + 1
                End If
            End If

            ''出力制御種別
            If gCompareChk_7_AIDO(57) = True Then 'Output Type
                If udt1.udtChannel(ich).ValveAiDoOutControl <> udt2.udtChannel(zch).ValveAiDoOutControl Then
                    msgtemp(i) = mMsgCreateSht("Output Type", udt1.udtChannel(ich).ValveAiDoOutControl, udt2.udtChannel(zch).ValveAiDoOutControl)
                    i = i + 1
                End If
            End If

            ''出力パルス幅
            If gCompareChk_7_AIDO(58) = True Then 'Output Width
                If udt1.udtChannel(ich).ValveAiDoWidth <> udt2.udtChannel(zch).ValveAiDoWidth Then
                    msgtemp(i) = mMsgCreateSht("Output Width", udt1.udtChannel(ich).ValveAiDoWidth, udt2.udtChannel(zch).ValveAiDoWidth)
                    i = i + 1
                End If
            End If

            ''出力ステータス種別コード
            If gCompareChk_7_AIDO(59) = True Then 'Output Status
                If udt1.udtChannel(ich).ValveAiDoOutStatus <> udt2.udtChannel(zch).ValveAiDoOutStatus Then
                    'Ver2.0.2.8 Output Status を名称にする
                    Dim strOld As String = GetChStatus(udt1.udtChannel(ich).ValveAiDoOutStatus)
                    Dim strNew As String = GetChStatus(udt2.udtChannel(zch).ValveAiDoOutStatus)
                    'msgtemp(i) = mMsgCreateStr("Output Status", "0x" & Hex(udt1.udtChannel(ich).ValveAiDoOutStatus).PadLeft(2, "0"), "0x" & Hex(udt2.udtChannel(zch).ValveAiDoOutStatus).PadLeft(2, "0"))
                    msgtemp(i) = mMsgCreateStr("Output Status", strOld, strNew)
                    i = i + 1
                End If
            End If

            ''出力ステータス情報　--------------------------------------------------------

            ''ステータス名称１
            If gCompareChk_7_AIDO(60) = True Then 'STATUS1
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus1, udt2.udtChannel(zch).ValveAiDoOutStatus1) Then
                    msgtemp(i) = mMsgCreateStr("STATUS1", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus1), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus1))
                    i = i + 1
                End If
            End If

            ''ステータス名称２
            If gCompareChk_7_AIDO(61) = True Then 'STATUS2
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus2, udt2.udtChannel(zch).ValveAiDoOutStatus2) Then
                    msgtemp(i) = mMsgCreateStr("STATUS2", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus2), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus2))
                    i = i + 1
                End If
            End If

            ''ステータス名称３
            If gCompareChk_7_AIDO(62) = True Then 'STATUS3
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus3, udt2.udtChannel(zch).ValveAiDoOutStatus3) Then
                    msgtemp(i) = mMsgCreateStr("STATUS3", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus3), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus3))
                    i = i + 1
                End If
            End If

            ''ステータス名称４
            If gCompareChk_7_AIDO(63) = True Then 'STATUS4
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus4, udt2.udtChannel(zch).ValveAiDoOutStatus4) Then
                    msgtemp(i) = mMsgCreateStr("STATUS4", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus4), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus4))
                    i = i + 1
                End If
            End If

            ''ステータス名称５
            If gCompareChk_7_AIDO(64) = True Then 'STATUS5
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus5, udt2.udtChannel(zch).ValveAiDoOutStatus5) Then
                    msgtemp(i) = mMsgCreateStr("STATUS5", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus5), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus5))
                    i = i + 1
                End If
            End If

            ''ステータス名称６
            If gCompareChk_7_AIDO(65) = True Then 'STATUS6
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus6, udt2.udtChannel(zch).ValveAiDoOutStatus6) Then
                    msgtemp(i) = mMsgCreateStr("STATUS6", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus6), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus6))
                    i = i + 1
                End If
            End If

            ''ステータス名称７
            If gCompareChk_7_AIDO(66) = True Then 'STATUS7
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus7, udt2.udtChannel(zch).ValveAiDoOutStatus7) Then
                    msgtemp(i) = mMsgCreateStr("STATUS7", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus7), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus7))
                    i = i + 1
                End If
            End If

            ''ステータス名称８
            If gCompareChk_7_AIDO(67) = True Then 'STATUS8
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoOutStatus8, udt2.udtChannel(zch).ValveAiDoOutStatus8) Then
                    msgtemp(i) = mMsgCreateStr("STATUS8", gGetString(udt1.udtChannel(ich).ValveAiDoOutStatus8), gGetString(udt2.udtChannel(zch).ValveAiDoOutStatus8))
                    i = i + 1
                End If
            End If

            ''フィードバックアラーム情報 ----------------------------------------------------

            ''フィードバックアラーム有無
            If gCompareChk_7_AIDO(68) = True Then 'FB USE
                If udt1.udtChannel(ich).ValveAiDoAlarmUse <> udt2.udtChannel(zch).ValveAiDoAlarmUse Then
                    msgtemp(i) = mMsgCreateSht("FB USE", udt1.udtChannel(ich).ValveAiDoAlarmUse, udt2.udtChannel(zch).ValveAiDoAlarmUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_7_AIDO(69) = True Then 'FB DLY
                If udt1.udtChannel(ich).ValveAiDoAlarmDelay <> udt2.udtChannel(zch).ValveAiDoAlarmDelay Then
                    msgtemp(i) = mMsgCreateSht("FB DLY", udt1.udtChannel(ich).ValveAiDoAlarmDelay, udt2.udtChannel(zch).ValveAiDoAlarmDelay)
                    i = i + 1
                End If
            End If

            ''規定値１
            If gCompareChk_7_AIDO(70) = True Then 'FB SP1
                If udt1.udtChannel(ich).ValveAiDoAlarmSp1 <> udt2.udtChannel(zch).ValveAiDoAlarmSp1 Then
                    msgtemp(i) = mMsgCreateSht("FB SP1", udt1.udtChannel(ich).ValveAiDoAlarmSp1, udt2.udtChannel(zch).ValveAiDoAlarmSp1)
                    i = i + 1
                End If
            End If

            ''規定値２
            If gCompareChk_7_AIDO(71) = True Then 'FB SP2
                If udt1.udtChannel(ich).ValveAiDoAlarmSp2 <> udt2.udtChannel(zch).ValveAiDoAlarmSp2 Then
                    msgtemp(i) = mMsgCreateSht("FB SP2", udt1.udtChannel(ich).ValveAiDoAlarmSp2, udt2.udtChannel(zch).ValveAiDoAlarmSp2)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（開処理用）
            If gCompareChk_7_AIDO(72) = True Then 'FB hys1
                If udt1.udtChannel(ich).ValveAiDoAlarmHys1 <> udt2.udtChannel(zch).ValveAiDoAlarmHys1 Then
                    msgtemp(i) = mMsgCreateSht("FB hys1", udt1.udtChannel(ich).ValveAiDoAlarmHys1, udt2.udtChannel(zch).ValveAiDoAlarmHys1)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（閉処理用）
            If gCompareChk_7_AIDO(73) = True Then 'FB hys2
                If udt1.udtChannel(ich).ValveAiDoAlarmHys2 <> udt2.udtChannel(zch).ValveAiDoAlarmHys2 Then
                    msgtemp(i) = mMsgCreateSht("FB hys2", udt1.udtChannel(ich).ValveAiDoAlarmHys2, udt2.udtChannel(zch).ValveAiDoAlarmHys2)
                    i = i + 1
                End If
            End If

            ''サンプリング時間
            If gCompareChk_7_AIDO(74) = True Then 'FB Sampling
                If udt1.udtChannel(ich).ValveAiDoAlarmSt <> udt2.udtChannel(zch).ValveAiDoAlarmSt Then
                    msgtemp(i) = mMsgCreateSht("FB Sampling", udt1.udtChannel(ich).ValveAiDoAlarmSt, udt2.udtChannel(zch).ValveAiDoAlarmSt)
                    i = i + 1
                End If
            End If

            ''変化量       '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_7_AIDO(75) = True Then 'FB Variation
                If udt1.udtChannel(ich).ValveAiDoAlarmVar <> udt2.udtChannel(zch).ValveAiDoAlarmVar Then
                    msgtemp(i) = mMsgCreateint("FB Variation", udt1.udtChannel(ich).ValveAiDoAlarmVar, udt2.udtChannel(zch).ValveAiDoAlarmVar, 0, 0, False, False)    '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_7_AIDO(76) = True Then 'FB EX
                If udt1.udtChannel(ich).ValveAiDoAlarmExtGroup <> udt2.udtChannel(zch).ValveAiDoAlarmExtGroup Then
                    msgtemp(i) = mMsgCreateByt("FB EX", udt1.udtChannel(ich).ValveAiDoAlarmExtGroup, udt2.udtChannel(zch).ValveAiDoAlarmExtGroup)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_7_AIDO(77) = True Then 'FB GR1
                If udt1.udtChannel(ich).ValveAiDoAlarmGroupRepose1 <> udt2.udtChannel(zch).ValveAiDoAlarmGroupRepose1 Then
                    msgtemp(i) = mMsgCreateByt("FB GR1", udt1.udtChannel(ich).ValveAiDoAlarmGroupRepose1, udt2.udtChannel(zch).ValveAiDoAlarmGroupRepose1)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_7_AIDO(78) = True Then 'FB GR2
                If udt1.udtChannel(ich).ValveAiDoAlarmGroupRepose2 <> udt2.udtChannel(zch).ValveAiDoAlarmGroupRepose2 Then
                    msgtemp(i) = mMsgCreateByt("FB GR2", udt1.udtChannel(ich).ValveAiDoAlarmGroupRepose2, udt2.udtChannel(zch).ValveAiDoAlarmGroupRepose2)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_7_AIDO(79) = True Then 'FB STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiDoAlarmStatusInput), NZfS(udt2.udtChannel(zch).ValveAiDoAlarmStatusInput)) Then
                    msgtemp(i) = mMsgCreateStr("FB STATUS", gGetString(udt1.udtChannel(ich).ValveAiDoAlarmStatusInput), gGetString(udt2.udtChannel(zch).ValveAiDoAlarmStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_7_AIDO(80) = True Then 'FB MR Status
                If udt1.udtChannel(ich).ValveAiDoAlarmManualReposeState <> udt2.udtChannel(zch).ValveAiDoAlarmManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("FB MR Status", udt1.udtChannel(ich).ValveAiDoAlarmManualReposeState, udt2.udtChannel(zch).ValveAiDoAlarmManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_7_AIDO(81) = True Then 'FB MR Set
                If udt1.udtChannel(ich).ValveAiDoAlarmManualReposeSet <> udt2.udtChannel(zch).ValveAiDoAlarmManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("FB MR Set", udt1.udtChannel(ich).ValveAiDoAlarmManualReposeSet, udt2.udtChannel(zch).ValveAiDoAlarmManualReposeSet)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_7_AIDO(82) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).ValveAiDoTagNo, udt2.udtChannel(zch).ValveAiDoTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).ValveAiDoTagNo), gGetString(udt2.udtChannel(zch).ValveAiDoTagNo))
                    i = i + 1
                End If
            End If


            'Alarm MimicNo
            If gCompareChk_7_AIDO(84) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).ValveAiDoAlmMimic <> udt2.udtChannel(zch).ValveAiDoAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).ValveAiDoAlmMimic, udt2.udtChannel(zch).ValveAiDoAlmMimic)
                    i = i + 1
                End If
            End If


            'ダミー設定
            If gCompareChk_7_AIDO(83) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonUnitName, udt2.udtChannel(zch).DummyCommonUnitName, i)

                'i = GetDummy("DMY ALM DLY HH", udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH, i)
                ''i = GetDummy("DMY ALM SET HH", udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH, i)   Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT HH", udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH, i)
                'i = GetDummy("DMY ALM GR1 HH", udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH, i)
                'i = GetDummy("DMY ALM GR2 HH", udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH, i)
                'i = GetDummy("DMY ALM STATUS HH", udt1.udtChannel(ich).DummyStaNmHH, udt2.udtChannel(zch).DummyStaNmHH, i)

                'i = GetDummy("DMY ALM DLY H", udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH, i)
                ''i = GetDummy("DMY ALM SET H", udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH, i)      Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT H", udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH, i)
                'i = GetDummy("DMY ALM GR1 H", udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H, i)
                'i = GetDummy("DMY ALM GR2 H", udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H, i)
                'i = GetDummy("DMY ALM STATUS H", udt1.udtChannel(ich).DummyStaNmH, udt2.udtChannel(zch).DummyStaNmH, i)

                'i = GetDummy("DMY ALM DLY L", udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL, i)
                ''i = GetDummy("DMY ALM SET L", udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL, i)      Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT L", udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL, i)
                'i = GetDummy("DMY ALM GR1 L", udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L, i)
                'i = GetDummy("DMY ALM GR2 L", udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L, i)
                'i = GetDummy("DMY ALM STATUS L", udt1.udtChannel(ich).DummyStaNmL, udt2.udtChannel(zch).DummyStaNmL, i)

                'i = GetDummy("DMY ALM DLY LL", udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL, i)
                ''i = GetDummy("DMY ALM SET LL", udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL, i)   Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT LL", udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL, i)
                'i = GetDummy("DMY ALM GR1 LL", udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL, i)
                'i = GetDummy("DMY ALM GR2 LL", udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL, i)
                'i = GetDummy("DMY ALM STATUS L", udt1.udtChannel(ich).DummyStaNmLL, udt2.udtChannel(zch).DummyStaNmLL, i)

                'i = GetDummy("DMY ALM DLY SF", udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF, i)
                ''i = GetDummy("DMY ALM SET SF", udt1.udtChannel(ich).DummyValueSF, udt2.udtChannel(zch).DummyValueSF, i)   Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT SF", udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF, i)
                'i = GetDummy("DMY ALM GR1 SF", udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF, i)
                'i = GetDummy("DMY ALM GR2 SF", udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF, i)
                'i = GetDummy("DMY ALM STATUS SF", udt1.udtChannel(ich).DummyStaNmSF, udt2.udtChannel(zch).DummyStaNmSF, i)

                ''i = GetDummy("DMY RANGE Scale", udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)    ‘’Ver1.11.6 2016.09.15 ﾚﾝｼﾞﾁｪｯｸに統合
                'i = GetDummy("DMY NOR RANGE H", udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, i)
                'i = GetDummy("DMY NOR RANGE L", udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)

                'i = GetDummy("DMY SP1", udt1.udtChannel(ich).DummySp1, udt2.udtChannel(zch).DummySp1, i)
                'i = GetDummy("DMY SP2", udt1.udtChannel(ich).DummySp2, udt2.udtChannel(zch).DummySp2, i)
                'i = GetDummy("DMY Hys Open", udt1.udtChannel(ich).DummyHysOpen, udt2.udtChannel(zch).DummyHysOpen, i)
                'i = GetDummy("DMY Hys Close", udt1.udtChannel(ich).DummyHysClose, udt2.udtChannel(zch).DummyHysClose, i)
                'i = GetDummy("DMY Sampling Time", udt1.udtChannel(ich).DummySmpTime, udt2.udtChannel(zch).DummySmpTime, i)
            End If

            '変更項目数を返す
            mCompareSetChannelValveAiDoDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(バルブ AI-AO)OK"

    Friend Function mCompareSetChannelValveAiAoDisp(ByVal udt1 As gTypSetChInfo, _
                                                    ByVal udt2 As gTypSetChInfo, _
                                                    ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更
            '========================
            ''バルブ AI-AO
            '========================

            Dim i As Integer = Gyo

            ''アラーム　HH ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_8_AIAO(0) = True Then 'ALM HH Use
                If (udt1.udtChannel(ich).ValveAiAoHiHiUse <> udt2.udtChannel(i).ValveAiAoHiHiUse) Then
                    msgtemp(i) = mMsgCreateSht("ALM HH Use", udt1.udtChannel(ich).ValveAiAoHiHiUse, udt2.udtChannel(zch).ValveAiAoHiHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(1) = True Then 'ALM HH DLY
                If (udt1.udtChannel(ich).ValveAiAoHiHiDelay <> udt2.udtChannel(i).ValveAiAoHiHiDelay) Or _
                    (udt1.udtChannel(ich).DummyDelayHH <> udt2.udtChannel(zch).DummyDelayHH) Then
                    'msgtemp(i) = mMsgCreateSht("ALM HH DLY", udt1.udtChannel(ich).ValveAiAoHiHiDelay, udt2.udtChannel(zch).ValveAiAoHiHiDelay)
                    msgtemp(i) = mMsgCreateint("ALM HH DLY", udt1.udtChannel(ich).ValveAiAoHiHiDelay, udt2.udtChannel(zch).ValveAiAoHiHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_8_AIAO(2) = True Then 'ALM HH SET
                If (udt1.udtChannel(ich).ValveAiAoHiHiValue <> udt2.udtChannel(i).ValveAiAoHiHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueHH <> udt2.udtChannel(zch).DummyValueHH) Then
                    msgtemp(i) = mMsgCreateint("ALM HH SET", udt1.udtChannel(ich).ValveAiAoHiHiValue, udt2.udtChannel(zch).ValveAiAoHiHiValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(3) = True Then 'ALM HH EX
                If (udt1.udtChannel(ich).ValveAiAoHiHiExtGroup <> udt2.udtChannel(i).ValveAiAoHiHiExtGroup) Or _
                    (udt1.udtChannel(ich).DummyExtGrHH <> udt2.udtChannel(zch).DummyExtGrHH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH EX", udt1.udtChannel(ich).ValveAiAoHiHiExtGroup, udt2.udtChannel(zch).ValveAiAoHiHiExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM HH EX", udt1.udtChannel(ich).ValveAiAoHiHiExtGroup, udt2.udtChannel(zch).ValveAiAoHiHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(4) = True Then 'ALM HH GR1
                If udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose1 <> udt2.udtChannel(i).ValveAiAoHiHiGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1HH <> udt2.udtChannel(zch).DummyGRep1HH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH GR1", udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose1, udt2.udtChannel(zch).ValveAiAoHiHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM HH GR1", udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose1, udt2.udtChannel(zch).ValveAiAoHiHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(5) = True Then 'ALM HH GR2
                If udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose2 <> udt2.udtChannel(i).ValveAiAoHiHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2HH <> udt2.udtChannel(zch).DummyGRep2HH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM HH GR2", udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose2, udt2.udtChannel(zch).ValveAiAoHiHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM HH GR2", udt1.udtChannel(ich).ValveAiAoHiHiGroupRepose2, udt2.udtChannel(zch).ValveAiAoHiHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(6) = True Then 'ALM HH STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoHiHiStatusInput), NZfS(udt2.udtChannel(i).ValveAiAoHiHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmHH <> udt2.udtChannel(zch).DummyStaNmHH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmHH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmHH, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM HH STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiAoHiHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiAoHiHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(7) = True Then 'ALM HH MR Status
                If udt1.udtChannel(ich).ValveAiAoHiHiManualReposeState <> udt2.udtChannel(i).ValveAiAoHiHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM HH MR Status", udt1.udtChannel(ich).ValveAiAoHiHiManualReposeState, udt2.udtChannel(zch).ValveAiAoHiHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(8) = True Then 'ALM HH MR Set
                If udt1.udtChannel(ich).ValveAiAoHiHiManualReposeSet <> udt2.udtChannel(i).ValveAiAoHiHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM HH MR Set", udt1.udtChannel(ich).ValveAiAoHiHiManualReposeSet, udt2.udtChannel(zch).ValveAiAoHiHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　H ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_8_AIAO(9) = True Then 'ALM H Use
                If udt1.udtChannel(ich).ValveAiAoHiUse <> udt2.udtChannel(zch).ValveAiAoHiUse Then
                    msgtemp(i) = mMsgCreateSht("ALM H Use", udt1.udtChannel(ich).ValveAiAoHiUse, udt2.udtChannel(zch).ValveAiAoHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(10) = True Then 'ALM H DLY
                If udt1.udtChannel(ich).ValveAiAoHiDelay <> udt2.udtChannel(zch).ValveAiAoHiDelay Or _
                    (udt1.udtChannel(ich).DummyDelayH <> udt2.udtChannel(zch).DummyDelayH) Then
                    'msgtemp(i) = mMsgCreateSht("ALM H DLY", udt1.udtChannel(ich).ValveAiAoHiDelay, udt2.udtChannel(zch).ValveAiAoHiDelay)
                    msgtemp(i) = mMsgCreateint("ALM H DLY", udt1.udtChannel(ich).ValveAiAoHiDelay, udt2.udtChannel(zch).ValveAiAoHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_8_AIAO(11) = True Then 'ALM H SET
                If (udt1.udtChannel(ich).ValveAiAoHiValue <> udt2.udtChannel(zch).ValveAiAoHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueH <> udt2.udtChannel(zch).DummyValueH) Then
                    msgtemp(i) = mMsgCreateint("ALM H SET", udt1.udtChannel(ich).ValveAiAoHiValue, udt2.udtChannel(zch).ValveAiAoHiValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(12) = True Then 'ALM H EX
                If udt1.udtChannel(ich).ValveAiAoHiExtGroup <> udt2.udtChannel(zch).ValveAiAoHiExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrH <> udt2.udtChannel(zch).DummyExtGrH) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H EX", udt1.udtChannel(ich).ValveAiAoHiExtGroup, udt2.udtChannel(zch).ValveAiAoHiExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM H EX", udt1.udtChannel(ich).ValveAiAoHiExtGroup, udt2.udtChannel(zch).ValveAiAoHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(13) = True Then 'ALM H GR1
                If udt1.udtChannel(ich).ValveAiAoHiGroupRepose1 <> udt2.udtChannel(zch).ValveAiAoHiGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1H <> udt2.udtChannel(zch).DummyGRep1H) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H GR1", udt1.udtChannel(ich).ValveAiAoHiGroupRepose1, udt2.udtChannel(zch).ValveAiAoHiGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM H GR1", udt1.udtChannel(ich).ValveAiAoHiGroupRepose1, udt2.udtChannel(zch).ValveAiAoHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(14) = True Then 'ALM H GR2
                If udt1.udtChannel(ich).ValveAiAoHiGroupRepose2 <> udt2.udtChannel(zch).ValveAiAoHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2H <> udt2.udtChannel(zch).DummyGRep2H) Then
                    'msgtemp(i) = mMsgCreateByt("ALM H GR2", udt1.udtChannel(ich).ValveAiAoHiGroupRepose2, udt2.udtChannel(zch).ValveAiAoHiGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM H GR2", udt1.udtChannel(ich).ValveAiAoHiGroupRepose2, udt2.udtChannel(zch).ValveAiAoHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(15) = True Then 'ALM H STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoHiStatusInput), NZfS(udt2.udtChannel(zch).ValveAiAoHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmH <> udt2.udtChannel(zch).DummyStaNmH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmH, "#", "")

                    msgtemp(i) = mMsgCreateStr("ALM H STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiAoHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiAoHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(16) = True Then 'ALM H MR Status
                If udt1.udtChannel(ich).ValveAiAoHiManualReposeState <> udt2.udtChannel(zch).ValveAiAoHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM H MR Status", udt1.udtChannel(ich).ValveAiAoHiManualReposeState, udt2.udtChannel(zch).ValveAiAoHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(17) = True Then 'ALM H MR Set
                If udt1.udtChannel(ich).ValveAiAoHiManualReposeSet <> udt2.udtChannel(zch).ValveAiAoHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM H MR Set", udt1.udtChannel(ich).ValveAiAoHiManualReposeSet, udt2.udtChannel(zch).ValveAiAoHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　L ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_8_AIAO(18) = True Then 'ALM L Use
                If udt1.udtChannel(ich).ValveAiAoLoUse <> udt2.udtChannel(zch).ValveAiAoLoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM L Use", udt1.udtChannel(ich).ValveAiAoLoUse, udt2.udtChannel(zch).ValveAiAoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(19) = True Then 'ALM L DLY
                If udt1.udtChannel(ich).ValveAiAoLoDelay <> udt2.udtChannel(zch).ValveAiAoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayL <> udt2.udtChannel(zch).DummyDelayL) Then
                    'msgtemp(i) = mMsgCreateSht("ALM L DLY", udt1.udtChannel(ich).ValveAiAoLoDelay, udt2.udtChannel(zch).ValveAiAoLoDelay)
                    msgtemp(i) = mMsgCreateint("ALM L DLY", udt1.udtChannel(ich).ValveAiAoLoDelay, udt2.udtChannel(zch).ValveAiAoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_8_AIAO(20) = True Then 'ALM L SET
                If (udt1.udtChannel(ich).ValveAiAoLoValue <> udt2.udtChannel(zch).ValveAiAoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueL <> udt2.udtChannel(zch).DummyValueL) Then
                    msgtemp(i) = mMsgCreateint("ALM L SET", udt1.udtChannel(ich).ValveAiAoLoValue, udt2.udtChannel(zch).ValveAiAoLoValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(21) = True Then 'ALM L EX
                If udt1.udtChannel(ich).ValveAiAoLoExtGroup <> udt2.udtChannel(zch).ValveAiAoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrL <> udt2.udtChannel(zch).DummyExtGrL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L EX", udt1.udtChannel(ich).ValveAiAoLoExtGroup, udt2.udtChannel(zch).ValveAiAoLoExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM L EX", udt1.udtChannel(ich).ValveAiAoLoExtGroup, udt2.udtChannel(zch).ValveAiAoLoExtGroup, _
                                              0, 0, _
                                              udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(22) = True Then 'ALM L GR1
                If udt1.udtChannel(ich).ValveAiAoLoGroupRepose1 <> udt2.udtChannel(zch).ValveAiAoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1L <> udt2.udtChannel(zch).DummyGRep1L) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L GR1", udt1.udtChannel(ich).ValveAiAoLoGroupRepose1, udt2.udtChannel(zch).ValveAiAoLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM L GR1", udt1.udtChannel(ich).ValveAiAoLoGroupRepose1, udt2.udtChannel(zch).ValveAiAoLoGroupRepose1, _
                                              0, 0, _
                                              udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(23) = True Then 'ALM L GR2
                If udt1.udtChannel(ich).ValveAiAoLoGroupRepose2 <> udt2.udtChannel(zch).ValveAiAoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2L <> udt2.udtChannel(zch).DummyGRep2L) Then
                    'msgtemp(i) = mMsgCreateByt("ALM L GR2", udt1.udtChannel(ich).ValveAiAoLoGroupRepose2, udt2.udtChannel(zch).ValveAiAoLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM L GR2", udt1.udtChannel(ich).ValveAiAoLoGroupRepose2, udt2.udtChannel(zch).ValveAiAoLoGroupRepose2, _
                                              0, 0, _
                                              udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(24) = True Then 'ALM L STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoLoStatusInput), NZfS(udt2.udtChannel(zch).ValveAiAoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmL <> udt2.udtChannel(zch).DummyStaNmL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmL, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM L STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiAoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiAoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(25) = True Then 'ALM L MR Status
                If udt1.udtChannel(ich).ValveAiAoLoManualReposeState <> udt2.udtChannel(zch).ValveAiAoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM L MR Status", udt1.udtChannel(ich).ValveAiAoLoManualReposeState, udt2.udtChannel(zch).ValveAiAoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(26) = True Then 'ALM L MR Set
                If udt1.udtChannel(ich).ValveAiAoLoManualReposeSet <> udt2.udtChannel(zch).ValveAiAoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM L MR Set", udt1.udtChannel(ich).ValveAiAoLoManualReposeSet, udt2.udtChannel(zch).ValveAiAoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　LL ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_8_AIAO(27) = True Then 'ALM LL Use
                If udt1.udtChannel(ich).ValveAiAoLoLoUse <> udt2.udtChannel(zch).ValveAiAoLoLoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM LL Use", udt1.udtChannel(ich).ValveAiAoLoLoUse, udt2.udtChannel(zch).ValveAiAoLoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(28) = True Then 'ALM LL DLY
                If udt1.udtChannel(ich).ValveAiAoLoLoDelay <> udt2.udtChannel(zch).ValveAiAoLoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayLL <> udt2.udtChannel(zch).DummyDelayLL) Then
                    'msgtemp(i) = mMsgCreateSht("ALM LL DLY", udt1.udtChannel(ich).ValveAiAoLoLoDelay, udt2.udtChannel(zch).ValveAiAoLoLoDelay)
                    msgtemp(i) = mMsgCreateint("ALM LL DLY", udt1.udtChannel(ich).ValveAiAoLoLoDelay, udt2.udtChannel(zch).ValveAiAoLoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.5 2016.08.30  ﾀｲﾄﾙ "ALM L" → "ALM LL"
            '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_8_AIAO(29) = True Then 'ALM LL SET
                If (udt1.udtChannel(ich).ValveAiAoLoLoValue <> udt2.udtChannel(zch).ValveAiAoLoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueLL <> udt2.udtChannel(zch).DummyValueLL) Then
                    msgtemp(i) = mMsgCreateint("ALM LL SET", udt1.udtChannel(ich).ValveAiAoLoLoValue, udt2.udtChannel(zch).ValveAiAoLoLoValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(30) = True Then 'ALM LL EX
                If udt1.udtChannel(ich).ValveAiAoLoLoExtGroup <> udt2.udtChannel(zch).ValveAiAoLoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrLL <> udt2.udtChannel(zch).DummyExtGrLL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL EX", udt1.udtChannel(ich).ValveAiAoLoLoExtGroup, udt2.udtChannel(zch).ValveAiAoLoLoExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM LL EX", udt1.udtChannel(ich).ValveAiAoLoLoExtGroup, udt2.udtChannel(zch).ValveAiAoLoLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(31) = True Then 'ALM LL GR1
                If udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose1 <> udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1LL <> udt2.udtChannel(zch).DummyGRep1LL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL GR1", udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose1, udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM LL GR1", udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose1, udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(32) = True Then 'ALM LL GR2
                If udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose2 <> udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2LL <> udt2.udtChannel(zch).DummyGRep2LL) Then
                    'msgtemp(i) = mMsgCreateByt("ALM LL GR2", udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose2, udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM LL GR2", udt1.udtChannel(ich).ValveAiAoLoLoGroupRepose2, udt2.udtChannel(zch).ValveAiAoLoLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(33) = True Then 'ALM LL STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoLoLoStatusInput), NZfS(udt2.udtChannel(zch).ValveAiAoLoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmLL <> udt2.udtChannel(zch).DummyStaNmLL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmLL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmLL, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM LL STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiAoLoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiAoLoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(34) = True Then 'ALM LL MR Status
                If udt1.udtChannel(ich).ValveAiAoLoLoManualReposeState <> udt2.udtChannel(zch).ValveAiAoLoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM LL MR Status", udt1.udtChannel(ich).ValveAiAoLoLoManualReposeState, udt2.udtChannel(zch).ValveAiAoLoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(35) = True Then 'ALM LL MR Set
                If udt1.udtChannel(ich).ValveAiAoLoLoManualReposeSet <> udt2.udtChannel(zch).ValveAiAoLoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM LL MR Set", udt1.udtChannel(ich).ValveAiAoLoLoManualReposeSet, udt2.udtChannel(zch).ValveAiAoLoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　SF  ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_8_AIAO(36) = True Then 'ALM S Use
                If udt1.udtChannel(ich).ValveAiAoSensorFailUse <> udt2.udtChannel(zch).ValveAiAoSensorFailUse Then
                    msgtemp(i) = mMsgCreateSht("ALM S Use", udt1.udtChannel(ich).ValveAiAoSensorFailUse, udt2.udtChannel(zch).ValveAiAoSensorFailUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(37) = True Then 'ALM S DLY
                If udt1.udtChannel(ich).ValveAiAoSensorFailDelay <> udt2.udtChannel(zch).ValveAiAoSensorFailDelay Or _
                    (udt1.udtChannel(ich).DummyDelaySF <> udt2.udtChannel(zch).DummyDelaySF) Then
                    'msgtemp(i) = mMsgCreateSht("ALM S DLY", udt1.udtChannel(ich).ValveAiAoSensorFailDelay, udt2.udtChannel(zch).ValveAiAoSensorFailDelay)
                    msgtemp(i) = mMsgCreateint("Alarm S DLY", udt1.udtChannel(ich).ValveAiAoSensorFailDelay, udt2.udtChannel(zch).ValveAiAoSensorFailDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_8_AIAO(38) = True Then 'ALM S SET
                If (udt1.udtChannel(ich).ValveAiAoSensorFailValue <> udt2.udtChannel(zch).ValveAiAoSensorFailValue) Or _
                    (udt1.udtChannel(ich).DummyValueSF <> udt2.udtChannel(zch).DummyValueSF) Then
                    msgtemp(i) = mMsgCreateint("ALM S SET", udt1.udtChannel(ich).ValveAiAoSensorFailValue, udt2.udtChannel(zch).ValveAiAoSensorFailValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueSF, udt2.udtChannel(zch).DummyValueSF)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(39) = True Then 'ALM S EX
                If udt1.udtChannel(ich).ValveAiAoSensorFailExtGroup <> udt2.udtChannel(zch).ValveAiAoSensorFailExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrSF <> udt2.udtChannel(zch).DummyExtGrSF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM S EX", udt1.udtChannel(ich).ValveAiAoSensorFailExtGroup, udt2.udtChannel(zch).ValveAiAoSensorFailExtGroup)
                    msgtemp(i) = mMsgCreateint("ALM S EX", udt1.udtChannel(ich).ValveAiAoSensorFailExtGroup, udt2.udtChannel(zch).ValveAiAoSensorFailExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(40) = True Then 'ALM S GR1
                If udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose1 <> udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1SF <> udt2.udtChannel(zch).DummyGRep1SF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM S GR1", udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose1, udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose1)
                    msgtemp(i) = mMsgCreateint("ALM S GR1", udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose1, udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(41) = True Then 'ALM SF GR2
                If udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose2 <> udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2SF <> udt2.udtChannel(zch).DummyGRep2SF) Then
                    'msgtemp(i) = mMsgCreateByt("ALM SF GR2", udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose2, udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose2)
                    msgtemp(i) = mMsgCreateint("ALM S GR2", udt1.udtChannel(ich).ValveAiAoSensorFailGroupRepose2, udt2.udtChannel(zch).ValveAiAoSensorFailGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(42) = True Then 'ALM S STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoSensorFailStatusInput), NZfS(udt2.udtChannel(zch).ValveAiAoSensorFailStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmSF <> udt2.udtChannel(zch).DummyStaNmSF) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmSF, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmSF, "#", "")
                    msgtemp(i) = mMsgCreateStr("ALM S STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).ValveAiAoSensorFailStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).ValveAiAoSensorFailStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(43) = True Then 'ALM SF MR Status
                If udt1.udtChannel(ich).ValveAiAoSensorFailManualReposeState <> udt2.udtChannel(zch).ValveAiAoSensorFailManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("ALM SF MR Status", udt1.udtChannel(ich).ValveAiAoSensorFailManualReposeState, udt2.udtChannel(zch).ValveAiAoSensorFailManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(44) = True Then 'ALM SF MR Set
                If udt1.udtChannel(ich).ValveAiAoSensorFailManualReposeSet <> udt2.udtChannel(zch).ValveAiAoSensorFailManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("ALM SF MR Set", udt1.udtChannel(ich).ValveAiAoSensorFailManualReposeSet, udt2.udtChannel(zch).ValveAiAoSensorFailManualReposeSet)
                    i = i + 1
                End If
            End If

            ''-----------------------------------------------------------------------------------------------
            ''スケール値　上限/下限値      '' Ver1.11.6 2016.09.15  引数追加 Ver2.0.7.J 引数不具合修正
            If gCompareChk_8_AIAO(45) = True Then 'RANGE
                i = GetRange("RANGE", udt1.udtChannel(ich).ValveAiAoRangeHigh, udt2.udtChannel(zch).ValveAiAoRangeHigh, _
                             udt1.udtChannel(ich).ValveAiAoRangeLow, udt2.udtChannel(zch).ValveAiAoRangeLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)
            End If

            ''ノーマルレンジ　上限/下限値    '' Ver1.11.6 2016.09.15  引数追加
            If gCompareChk_8_AIAO(46) = True Then 'NOR RANGE
                i = GetNorRange("NOR RANGE", udt1.udtChannel(ich).ValveAiAoNormalHigh, udt2.udtChannel(zch).ValveAiAoNormalHigh, _
                             udt1.udtChannel(ich).ValveAiAoNormalLow, udt2.udtChannel(zch).ValveAiAoNormalLow, _
                             udt1.udtChannel(ich).AnalogDecimalPosition, udt2.udtChannel(zch).AnalogDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, _
                             udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)
            End If

            ''オフセット値        '' Ver1.11.6 2016.09.15  引数追加
            If gCompareChk_8_AIAO(47) = True Then 'OFFSET
                If udt1.udtChannel(ich).ValveAiAoOffsetValue <> udt2.udtChannel(zch).ValveAiAoOffsetValue Then
                    msgtemp(i) = mMsgCreateint("OFFSET", udt1.udtChannel(ich).ValveAiAoOffsetValue, udt2.udtChannel(zch).ValveAiAoOffsetValue, _
                                               udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition, False, False)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_8_AIAO(48) = True Then 'String
                If udt1.udtChannel(ich).ValveAiAoString <> udt2.udtChannel(zch).ValveAiAoString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).ValveAiAoString, udt2.udtChannel(zch).ValveAiAoString)
                    i = i + 1
                End If
            End If

            ''小数点以下桁数
            If gCompareChk_8_AIAO(49) = True Then 'Decimal Point
                If udt1.udtChannel(ich).ValveAiAoDecimalPosition <> udt2.udtChannel(zch).ValveAiAoDecimalPosition Then
                    msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).ValveAiAoDecimalPosition, udt2.udtChannel(zch).ValveAiAoDecimalPosition)
                    i = i + 1
                End If
            End If

            ''センター表示
            If gCompareChk_8_AIAO(50) = True Then 'BAR GRAPH CENTER
                i = GetBitCHK("BAR GRAPH CENTER", udt1.udtChannel(ich).ValveAiAoDisplay3, 0, udt2.udtChannel(zch).ValveAiAoDisplay3, 0, i)
            End If

            ''Sensor異常表示
            If gCompareChk_8_AIAO(51) = True Then 'SENSOR ALM(UNDER OVER)
                i = GetBitCHK("SENSOR ALM(UNDER)", udt1.udtChannel(ich).ValveAiAoDisplay3, 2, udt2.udtChannel(zch).ValveAiAoDisplay3, 2, i)
                i = GetBitCHK("SENSOR ALM(OVER)", udt1.udtChannel(ich).ValveAiAoDisplay3, 3, udt2.udtChannel(zch).ValveAiAoDisplay3, 3, i)
            End If

            ''フィードバックアラームタイマ値
            If gCompareChk_8_AIAO(52) = True Then 'FB Timer
                If udt1.udtChannel(ich).ValveAiAoFeedback <> udt2.udtChannel(zch).ValveAiAoFeedback Then
                    msgtemp(i) = mMsgCreateSht("FB Timer", udt1.udtChannel(ich).ValveAiAoFeedback, udt2.udtChannel(zch).ValveAiAoFeedback)
                    i = i + 1
                End If
            End If


            'Ver2.0.4.1
            '出力FUｱﾄﾞﾚｽは、IN_FUｱﾄﾞﾚｽと同様に一つにまとめる
            ''外部出力 FU 番号
            'If gCompareChk_8_AIAO(53) = True Then 'OUT FU No
            '    If udt1.udtChannel(ich).ValveAiAoFuNo <> udt2.udtChannel(zch).ValveAiAoFuNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU No", udt1.udtChannel(ich).ValveAiAoFuNo, udt2.udtChannel(zch).ValveAiAoFuNo)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU ポート番号
            'If gCompareChk_8_AIAO(54) = True Then 'OUT FU Slot
            '    If udt1.udtChannel(ich).ValveAiAoPortNo <> udt2.udtChannel(zch).ValveAiAoPortNo Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Slot", udt1.udtChannel(ich).ValveAiAoPortNo, udt2.udtChannel(zch).ValveAiAoPortNo)
            '        i = i + 1
            '    End If
            'End If
            ''外部出力 FU 端子番号
            'If gCompareChk_8_AIAO(55) = True Then 'OUT FU Pin
            '    If udt1.udtChannel(ich).ValveAiAoPin <> udt2.udtChannel(zch).ValveAiAoPin Then
            '        msgtemp(i) = mMsgCreateSht("OUT FU Pin", udt1.udtChannel(ich).ValveAiAoPin, udt2.udtChannel(zch).ValveAiAoPin)
            '        i = i + 1
            '    End If
            'End If
            If gCompareChk_8_AIAO(53) = True Or gCompareChk_8_AIAO(54) = True Or gCompareChk_8_AIAO(55) = True Then
                If (udt1.udtChannel(ich).ValveAiAoFuNo <> udt2.udtChannel(zch).ValveAiAoFuNo Or _
                    udt1.udtChannel(ich).ValveAiAoPortNo <> udt2.udtChannel(zch).ValveAiAoPortNo Or _
                    udt1.udtChannel(ich).ValveAiAoPin <> udt2.udtChannel(zch).ValveAiAoPin) Or _
                    (udt1.udtChannel(ich).DummyOutFuAddress <> udt2.udtChannel(zch).DummyOutFuAddress) Then

                    msgtemp(i) = mMsgFUAdd_OUT(udt1.udtChannel(ich).ValveAiAoFuNo, udt1.udtChannel(ich).ValveAiAoPortNo, udt1.udtChannel(ich).ValveAiAoPin, _
                                                udt2.udtChannel(zch).ValveAiAoFuNo, udt2.udtChannel(zch).ValveAiAoPortNo, udt2.udtChannel(zch).ValveAiAoPin, _
                                                udt1.udtChannel(ich).DummyOutFuAddress, udt2.udtChannel(zch).DummyOutFuAddress)
                    i = i + 1

                End If
            End If
            



            ''出力点数
            If gCompareChk_8_AIAO(56) = True Then 'Output Count
                If udt1.udtChannel(ich).ValveAiAoPinNo <> udt2.udtChannel(zch).ValveAiAoPinNo Then
                    msgtemp(i) = mMsgCreateSht("Output Count", udt1.udtChannel(ich).ValveAiAoPinNo, udt2.udtChannel(zch).ValveAiAoPinNo)
                    i = i + 1
                End If
            End If

            ''出力ステータス種別コード
            If gCompareChk_8_AIAO(57) = True Then 'Output Status
                If udt1.udtChannel(ich).ValveAiAoOutStatus <> udt2.udtChannel(zch).ValveAiAoOutStatus Then
                    'Ver2.0.2.8 Output Status を名称にする
                    Dim strOld As String = GetChStatus(udt1.udtChannel(ich).ValveAiAoOutStatus)
                    Dim strNew As String = GetChStatus(udt2.udtChannel(zch).ValveAiAoOutStatus)
                    'msgtemp(i) = mMsgCreateStr("Output Status", "0x" & Hex(udt1.udtChannel(ich).ValveAiAoOutStatus).PadLeft(2, "0"), "0x" & Hex(udt2.udtChannel(zch).ValveAiAoOutStatus).PadLeft(2, "0"))
                    msgtemp(i) = mMsgCreateStr("Output Status", strOld, strNew)
                    i = i + 1
                End If
            End If

            ''出力ステータス名称
            If gCompareChk_8_AIAO(58) = True Then 'STATUS1
                If Not gCompareString(udt1.udtChannel(ich).ValveAiAoOutStatus1, udt2.udtChannel(zch).ValveAiAoOutStatus1) Then
                    msgtemp(i) = mMsgCreateStr("STATUS1", gGetString(udt1.udtChannel(ich).ValveAiAoOutStatus1), gGetString(udt2.udtChannel(zch).ValveAiAoOutStatus1))
                    i = i + 1
                End If
            End If

            ''フィードバックアラーム情報 ----------------------------------------------------

            ''フィードバックアラーム有無
            If gCompareChk_8_AIAO(59) = True Then 'FB Use
                If udt1.udtChannel(ich).ValveAiAoAlarmUse <> udt2.udtChannel(zch).ValveAiAoAlarmUse Then
                    msgtemp(i) = mMsgCreateSht("FB Use", udt1.udtChannel(ich).ValveAiAoAlarmUse, udt2.udtChannel(zch).ValveAiAoAlarmUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_8_AIAO(60) = True Then 'FB DLY
                If udt1.udtChannel(ich).ValveAiAoAlarmDelay <> udt2.udtChannel(zch).ValveAiAoAlarmDelay Then
                    msgtemp(i) = mMsgCreateSht("FB DLY", udt1.udtChannel(ich).ValveAiAoAlarmDelay, udt2.udtChannel(zch).ValveAiAoAlarmDelay)
                    i = i + 1
                End If
            End If

            ''規定値１
            If gCompareChk_8_AIAO(61) = True Then 'FB SP1
                If udt1.udtChannel(ich).ValveAiAoAlarmSp1 <> udt2.udtChannel(zch).ValveAiAoAlarmSp1 Then
                    msgtemp(i) = mMsgCreateSht("FB SP1", udt1.udtChannel(ich).ValveAiAoAlarmSp1, udt2.udtChannel(zch).ValveAiAoAlarmSp1)
                    i = i + 1
                End If
            End If

            ''規定値２
            If gCompareChk_8_AIAO(62) = True Then 'FB SP2
                If udt1.udtChannel(ich).ValveAiAoAlarmSp2 <> udt2.udtChannel(zch).ValveAiAoAlarmSp2 Then
                    msgtemp(i) = mMsgCreateSht("FB SP2", udt1.udtChannel(ich).ValveAiAoAlarmSp2, udt2.udtChannel(zch).ValveAiAoAlarmSp2)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（開処理用）
            If gCompareChk_8_AIAO(63) = True Then 'FB hys1
                If udt1.udtChannel(ich).ValveAiAoAlarmHys1 <> udt2.udtChannel(zch).ValveAiAoAlarmHys1 Then
                    msgtemp(i) = mMsgCreateSht("FB hys1", udt1.udtChannel(ich).ValveAiAoAlarmHys1, udt2.udtChannel(zch).ValveAiAoAlarmHys1)
                    i = i + 1
                End If
            End If

            ''ヒステリシス値（閉処理用）
            If gCompareChk_8_AIAO(64) = True Then 'FB hys2
                If udt1.udtChannel(ich).ValveAiAoAlarmHys2 <> udt2.udtChannel(zch).ValveAiAoAlarmHys2 Then
                    msgtemp(i) = mMsgCreateSht("FB hys2", udt1.udtChannel(ich).ValveAiAoAlarmHys2, udt2.udtChannel(zch).ValveAiAoAlarmHys2)
                    i = i + 1
                End If
            End If

            ''サンプリング時間
            If gCompareChk_8_AIAO(65) = True Then 'FB Sampling
                If udt1.udtChannel(ich).ValveAiAoAlarmSt <> udt2.udtChannel(zch).ValveAiAoAlarmSt Then
                    msgtemp(i) = mMsgCreateSht("FB Sampling", udt1.udtChannel(ich).ValveAiAoAlarmSt, udt2.udtChannel(zch).ValveAiAoAlarmSt)
                    i = i + 1
                End If
            End If

            ''変化量       '' Ver1.11.6 2016.09.15  引数追加
            If gCompareChk_8_AIAO(66) = True Then 'FB Variation
                If udt1.udtChannel(ich).ValveAiAoAlarmVar <> udt2.udtChannel(zch).ValveAiAoAlarmVar Then
                    msgtemp(i) = mMsgCreateint("FB Variation", udt1.udtChannel(ich).ValveAiAoAlarmVar, udt2.udtChannel(zch).ValveAiAoAlarmVar, 0, 0, False, False)
                    i = i + 1
                End If
            End If

            ''延長警報グループ
            If gCompareChk_8_AIAO(67) = True Then 'FB EX
                If udt1.udtChannel(ich).ValveAiAoAlarmExtGroup <> udt2.udtChannel(zch).ValveAiAoAlarmExtGroup Then
                    msgtemp(i) = mMsgCreateByt("FB EX", udt1.udtChannel(ich).ValveAiAoAlarmExtGroup, udt2.udtChannel(zch).ValveAiAoAlarmExtGroup)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_8_AIAO(68) = True Then 'FB GR1
                If udt1.udtChannel(ich).ValveAiAoAlarmGroupRepose1 <> udt2.udtChannel(zch).ValveAiAoAlarmGroupRepose1 Then
                    msgtemp(i) = mMsgCreateByt("FB GR1", udt1.udtChannel(ich).ValveAiAoAlarmGroupRepose1, udt2.udtChannel(zch).ValveAiAoAlarmGroupRepose1)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_8_AIAO(69) = True Then 'FB GR2
                If udt1.udtChannel(ich).ValveAiAoAlarmGroupRepose2 <> udt2.udtChannel(zch).ValveAiAoAlarmGroupRepose2 Then
                    msgtemp(i) = mMsgCreateByt("FB GR2", udt1.udtChannel(ich).ValveAiAoAlarmGroupRepose2, udt2.udtChannel(zch).ValveAiAoAlarmGroupRepose2)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_8_AIAO(70) = True Then 'FB STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).ValveAiAoAlarmStatusInput), NZfS(udt2.udtChannel(zch).ValveAiAoAlarmStatusInput)) Then
                    msgtemp(i) = mMsgCreateStr("FB STATUS", gGetString(udt1.udtChannel(ich).ValveAiAoAlarmStatusInput), gGetString(udt2.udtChannel(zch).ValveAiAoAlarmStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_8_AIAO(71) = True Then 'FB MR Status
                If udt1.udtChannel(ich).ValveAiAoAlarmManualReposeState <> udt2.udtChannel(zch).ValveAiAoAlarmManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("FB MR Status", udt1.udtChannel(ich).ValveAiAoAlarmManualReposeState, udt2.udtChannel(zch).ValveAiAoAlarmManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_8_AIAO(72) = True Then 'FB MR Set
                If udt1.udtChannel(ich).ValveAiAoAlarmManualReposeSet <> udt2.udtChannel(zch).ValveAiAoAlarmManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("FB MR Set", udt1.udtChannel(ich).ValveAiAoAlarmManualReposeSet, udt2.udtChannel(zch).ValveAiAoAlarmManualReposeSet)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_8_AIAO(73) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).ValveAiAoTagNo, udt2.udtChannel(zch).ValveAiAoTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).ValveAiAoTagNo), gGetString(udt2.udtChannel(zch).ValveAiAoTagNo))
                    i = i + 1
                End If
            End If


            'Alarm MimicNo
            If gCompareChk_8_AIAO(72) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).ValveAiAoAlmMimic <> udt2.udtChannel(zch).ValveAiAoAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).ValveAiAoAlmMimic, udt2.udtChannel(zch).ValveAiAoAlmMimic)
                    i = i + 1
                End If
            End If



            'ダミー設定
            If gCompareChk_8_AIAO(74) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonUnitName, udt2.udtChannel(zch).DummyCommonUnitName, i)

                'i = GetDummy("DMY ALM DLY HH", udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH, i)
                ''i = GetDummy("DMY ALM SET HH", udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH, i)       Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT HH", udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH, i)
                'i = GetDummy("DMY ALM GR1 HH", udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH, i)
                'i = GetDummy("DMY ALM GR2 HH", udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH, i)
                'i = GetDummy("DMY ALM STATUS HH", udt1.udtChannel(ich).DummyStaNmHH, udt2.udtChannel(zch).DummyStaNmHH, i)

                'i = GetDummy("DMY ALM DLY H", udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH, i)
                ''i = GetDummy("DMY ALM SET H", udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH, i)      Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT H", udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH, i)
                'i = GetDummy("DMY ALM GR1 H", udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H, i)
                'i = GetDummy("DMY ALM GR2 H", udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H, i)
                'i = GetDummy("DMY ALM STATUS H", udt1.udtChannel(ich).DummyStaNmH, udt2.udtChannel(zch).DummyStaNmH, i)

                'i = GetDummy("DMY ALM DLY L", udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL, i)
                ''i = GetDummy("DMY ALM SET L", udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL, i)      Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT L", udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL, i)
                'i = GetDummy("DMY ALM GR1 L", udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L, i)
                'i = GetDummy("DMY ALM GR2 L", udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L, i)
                'i = GetDummy("DMY ALM STATUS L", udt1.udtChannel(ich).DummyStaNmL, udt2.udtChannel(zch).DummyStaNmL, i)

                'i = GetDummy("DMY ALM DLY LL", udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL, i)
                ''i = GetDummy("DMY ALM SET LL", udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL, i)       Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT LL", udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL, i)
                'i = GetDummy("DMY ALM GR1 LL", udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL, i)
                'i = GetDummy("DMY ALM GR2 LL", udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL, i)
                'i = GetDummy("DMY ALM STATUS L", udt1.udtChannel(ich).DummyStaNmLL, udt2.udtChannel(zch).DummyStaNmLL, i)

                'i = GetDummy("DMY ALM DLY SF", udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF, i)
                ''i = GetDummy("DMY ALM SET SF", udt1.udtChannel(ich).DummyValueSF, udt2.udtChannel(zch).DummyValueSF, i)       Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM EXT SF", udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF, i)
                'i = GetDummy("DMY ALM GR1 SF", udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF, i)
                'i = GetDummy("DMY ALM GR2 SF", udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF, i)
                'i = GetDummy("DMY ALM STATUS SF", udt1.udtChannel(ich).DummyStaNmSF, udt2.udtChannel(zch).DummyStaNmSF, i)

                ''i = GetDummy("DMY RANGE Scale", udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)      '’Ver1.11.6 2016.09.15 ﾚﾝｼﾞﾁｪｯｸに統合
                'i = GetDummy("DMY NOR RANGE H", udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, i)
                'i = GetDummy("DMY NOR RANGE L", udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)

                'i = GetDummy("DMY SP1", udt1.udtChannel(ich).DummySp1, udt2.udtChannel(zch).DummySp1, i)
                'i = GetDummy("DMY SP2", udt1.udtChannel(ich).DummySp2, udt2.udtChannel(zch).DummySp2, i)
                'i = GetDummy("DMY Hys Open", udt1.udtChannel(ich).DummyHysOpen, udt2.udtChannel(zch).DummyHysOpen, i)
                'i = GetDummy("DMY Hys Close", udt1.udtChannel(ich).DummyHysClose, udt2.udtChannel(zch).DummyHysClose, i)
                'i = GetDummy("DMY Sampling Time", udt1.udtChannel(ich).DummySmpTime, udt2.udtChannel(zch).DummySmpTime, i)
            End If

            '変更項目数を返す
            mCompareSetChannelValveAiAoDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(デジタルコンポジット)OK"

    Friend Function mCompareSetChannelCompositeDisp(ByVal udt1 As gTypSetChInfo, _
                                                    ByVal udt2 As gTypSetChInfo, _
                                                    ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更

            '========================
            ''コンポジットＣＨ
            '========================

            Dim i As Integer = Gyo

            ''コンポジット設定テーブルインデックス
            If gCompareChk_9_Comp(0) = True Then 'Tbl Index
                If udt1.udtChannel(ich).CompositeTableIndex <> udt2.udtChannel(zch).CompositeTableIndex Then
                    msgtemp(i) = mMsgCreateSht("Tbl Index", udt1.udtChannel(ich).CompositeTableIndex, udt2.udtChannel(zch).CompositeTableIndex)
                    i = i + 1
                End If
            End If

            ''ﾀｸﾞ名称     ' 2015.10.22 追加
            If gCompareChk_9_Comp(1) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).CompositeTagNo, udt2.udtChannel(zch).CompositeTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).CompositeTagNo), gGetString(udt2.udtChannel(zch).CompositeTagNo))
                    i = i + 1
                End If
            End If


            'Alarm MimicNo
            If gCompareChk_9_Comp(3) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).CompositeAlmMimic <> udt2.udtChannel(zch).CompositeAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).CompositeAlmMimic, udt2.udtChannel(zch).CompositeAlmMimic)
                    i = i + 1
                End If
            End If


            'ダミー設定
            If gCompareChk_9_Comp(2) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup, i)
                'i = GetDummy("DMY ALM DLY", udt1.udtChannel(ich).DummyCommonDelay, udt2.udtChannel(zch).DummyCommonDelay, i)
                'i = GetDummy("DMY ALM GR1", udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1, i)
                'i = GetDummy("DMY ALM GR2", udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2, i)
                'i = GetDummy("DMY ALM FU Address", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM Pin No.", udt1.udtChannel(ich).DummyCommonPinNo, udt2.udtChannel(zch).DummyCommonPinNo, i)
                'i = GetDummy("DMY ALM Status Name", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)
            End If

            '変更項目数を返す
            mCompareSetChannelCompositeDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(パルス)OK"

    Friend Function mCompareSetChannelPulseDisp(ByVal udt1 As gTypSetChInfo, _
                                                ByVal udt2 As gTypSetChInfo, _
                                                ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.2.8 ダミーを値に含めるように変更

            '========================
            ''パルスＣＨ
            '========================

            Dim i As Integer = Gyo

            ''アラーム有無
            If gCompareChk_10_Puls(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).PulseUse <> udt2.udtChannel(zch).PulseUse Then
                    msgtemp(i) = mMsgCreateSht("ALM Use", udt1.udtChannel(ich).PulseUse, udt2.udtChannel(zch).PulseUse)
                    i = i + 1
                End If
            End If

            ''ソフトウェアフィルタ定数
            If gCompareChk_10_Puls(1) = True Then 'Filter
                If udt1.udtChannel(ich).PulseDiFilter <> udt2.udtChannel(zch).PulseDiFilter Then
                    msgtemp(i) = mMsgCreateSht("Filter", udt1.udtChannel(ich).PulseDiFilter, udt2.udtChannel(zch).PulseDiFilter)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾁｪｯｸ統合
            If gCompareChk_10_Puls(2) = True Then 'ALM SET
                If (udt1.udtChannel(ich).PulseValue <> udt2.udtChannel(zch).PulseValue) Or _
                    (udt1.udtChannel(ich).DummyValueH <> udt2.udtChannel(zch).DummyValueH) Then
                    msgtemp(i) = mMsgCreateint("ALM SET", udt1.udtChannel(ich).PulseValue, udt2.udtChannel(zch).PulseValue, _
                                               udt1.udtChannel(ich).PulseDecPoint, udt2.udtChannel(zch).PulseDecPoint, _
                                               udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH) '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_10_Puls(3) = True Then 'String
                If udt1.udtChannel(ich).PulseString <> udt2.udtChannel(zch).PulseString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).PulseString, udt2.udtChannel(zch).PulseString)
                    i = i + 1
                End If
            End If

            'Ver2.0.2.9 パルスの小数点以下桁数の結果表記はレンジ形式とする
            ''小数点以下桁数
            If gCompareChk_10_Puls(4) = True Then 'Decimal Point
                If udt1.udtChannel(ich).PulseDecPoint <> udt2.udtChannel(zch).PulseDecPoint Then
                    'Ver2.0.6.5 9が7個
                    Dim intValue As Double = 9999999 '99999999
                    Dim strOldValue As String = ""
                    Dim strNewValue As String = ""
                    With udt1.udtChannel(ich)
                        If .PulseDecPoint = 0 Or .PulseDecPoint = Nothing Then
                            'Ver2.0.7.E DecPoint無しは9が8個
                            'strOldValue = intValue
                            strOldValue = "99999999"
                        Else
                            strOldValue = intValue / 10 ^ .PulseDecPoint
                        End If
                    End With
                    With udt2.udtChannel(zch)
                        If .PulseDecPoint = 0 Or .PulseDecPoint = Nothing Then
                            'Ver2.0.7.E DecPoint無しは9が8個
                            'strNewValue = intValue
                            strNewValue = "99999999"
                        Else
                            strNewValue = intValue / 10 ^ .PulseDecPoint
                        End If
                    End With
                    'msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).PulseDecPoint, udt2.udtChannel(zch).PulseDecPoint)
                    msgtemp(i) = mMsgCreateStr("Decimal Point", strOldValue, strNewValue)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_10_Puls(5) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).PulseTagNo, udt2.udtChannel(zch).PulseTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).PulseTagNo), gGetString(udt2.udtChannel(zch).PulseTagNo))
                    i = i + 1
                End If
            End If

            'Alarm MimicNo
            If gCompareChk_10_Puls(7) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).PulseAlmMimic <> udt2.udtChannel(zch).PulseAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).PulseAlmMimic, udt2.udtChannel(zch).PulseAlmMimic)
                    i = i + 1
                End If
            End If


            'ダミー設定
            If gCompareChk_10_Puls(6) = True Then 'Dummy Setting
                'i = GetDummy("DMY ALM EXT G", udt1.udtChannel(ich).DummyCommonExtGroup, udt2.udtChannel(zch).DummyCommonExtGroup, i)
                'i = GetDummy("DMY ALM DLY", udt1.udtChannel(ich).DummyCommonDelay, udt2.udtChannel(zch).DummyCommonDelay, i)
                'i = GetDummy("DMY ALM GR1", udt1.udtChannel(ich).DummyCommonGroupRepose1, udt2.udtChannel(zch).DummyCommonGroupRepose1, i)
                'i = GetDummy("DMY ALM GR2", udt1.udtChannel(ich).DummyCommonGroupRepose2, udt2.udtChannel(zch).DummyCommonGroupRepose2, i)
                'i = GetDummy("DMY ALM FU Address", udt1.udtChannel(ich).DummyCommonFuAddress, udt2.udtChannel(zch).DummyCommonFuAddress, i)
                'i = GetDummy("DMY ALM Unit Name", udt1.udtChannel(ich).DummyCommonUnitName, udt2.udtChannel(zch).DummyCommonUnitName, i)
                'i = GetDummy("DMY ALM Status Name", udt1.udtChannel(ich).DummyCommonStatusName, udt2.udtChannel(zch).DummyCommonStatusName, i)
                ''i = GetDummy("DMY ALM Value H", udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH, i)        Ver1.11.6 2016.09.15 設定値ﾁｪｯｸに統合
                'i = GetDummy("DMY ALM Status Name H", udt1.udtChannel(ich).DummyStaNmH, udt2.udtChannel(zch).DummyStaNmH, i)
            End If

            '変更項目数を返す
            mCompareSetChannelPulseDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(積算)OK"

    Friend Function mCompareSetChannelRevoDisp(ByVal udt1 As gTypSetChInfo, _
                                               ByVal udt2 As gTypSetChInfo, _
                                               ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            '========================
            ''積算ＣＨ
            '========================

            Dim i As Integer = Gyo

            ''アラーム有無
            If gCompareChk_11_RunHour(0) = True Then 'ALM Use
                If udt1.udtChannel(ich).RevoUse <> udt2.udtChannel(zch).RevoUse Then
                    msgtemp(i) = mMsgCreateSht("ALM Use", udt1.udtChannel(ich).RevoUse, udt2.udtChannel(zch).RevoUse)
                    i = i + 1
                End If
            End If

            ''ソフトウェアフィルタ定数
            If gCompareChk_11_RunHour(1) = True Then 'Filter
                If udt1.udtChannel(ich).RevoDiFilter <> udt2.udtChannel(zch).RevoDiFilter Then
                    msgtemp(i) = mMsgCreateSht("Filter", udt1.udtChannel(ich).RevoDiFilter, udt2.udtChannel(zch).RevoDiFilter)
                    i = i + 1
                End If
            End If

            ''アラームセット値      '' Ver1.11.6 2016.09.15 引数追加
            If gCompareChk_11_RunHour(2) = True Then 'ALM SET
                If udt1.udtChannel(ich).RevoValue <> udt2.udtChannel(zch).RevoValue Then
                    msgtemp(i) = mMsgCreateint("ALM SET", udt1.udtChannel(ich).RevoValue, udt2.udtChannel(zch).RevoValue, _
                                               udt1.udtChannel(ich).RevoDecPoint, udt2.udtChannel(zch).RevoDecPoint, False, False)     '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If
            End If

            ''積算トリガCH System No
            If gCompareChk_11_RunHour(3) = True Then 'TRI SYSNo
                If udt1.udtChannel(ich).RevoTrigerSysno <> udt2.udtChannel(zch).RevoTrigerSysno Then
                    msgtemp(i) = mMsgCreateSht("TRI SYSNo.", udt1.udtChannel(ich).RevoTrigerSysno, udt2.udtChannel(zch).RevoTrigerSysno)
                    i = i + 1
                End If
            End If

            ''積算トリガCH CH ID
            If gCompareChk_11_RunHour(4) = True Then 'TRI CHNo
                If udt1.udtChannel(ich).RevoTrigerChid <> udt2.udtChannel(zch).RevoTrigerChid Then
                    msgtemp(i) = mMsgCreateSht("TRI CHNo.", udt1.udtChannel(ich).RevoTrigerChid, udt2.udtChannel(zch).RevoTrigerChid)
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_11_RunHour(5) = True Then 'String
                If udt1.udtChannel(ich).RevoString <> udt2.udtChannel(zch).RevoString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).RevoString, udt2.udtChannel(zch).RevoString)
                    i = i + 1
                End If
            End If

            'Ver2.0.2.9 積算の小数点以下桁数の結果表記はレンジ形式とする
            ''小数点以下桁数
            If gCompareChk_11_RunHour(6) = True Then 'Decimal Point
                If udt1.udtChannel(ich).RevoDecPoint <> udt2.udtChannel(zch).RevoDecPoint Then
                    Dim intValue As Double = 0
                    Dim strOldValue As String = ""
                    Dim strNewValue As String = ""
                    With udt1.udtChannel(ich)
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypePulseRevoTotalHour, gCstCodeChDataTypePulseRevoDayHour, gCstCodeChDataTypePulseRevoLapHour, _
                                gCstCodeChDataTypePulseRevoExtDev, gCstCodeChDataTypePulseRevoExtDevDayHour, gCstCodeChDataTypePulseRevoExtDevLapHour

                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                intValue = 99999999 '9999999 '99999999
                                strOldValue = intValue.ToString
                            Case gCstCodeChDataTypePulseRevoTotalMin, gCstCodeChDataTypePulseRevoDayMin, gCstCodeChDataTypePulseRevoLapMin, _
                                gCstCodeChDataTypePulseRevoExtDevTotalMin, gCstCodeChDataTypePulseRevoExtDevDayMin, gCstCodeChDataTypePulseRevoExtDevLapMin

                                intValue = 9999959
                                If .RevoDecPoint = 0 Or .RevoDecPoint = Nothing Then
                                    strOldValue = intValue
                                Else
                                    strOldValue = intValue / 10 ^ .RevoDecPoint
                                End If
                        End Select
                    End With
                    With udt2.udtChannel(zch)
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypePulseRevoTotalHour, gCstCodeChDataTypePulseRevoDayHour, gCstCodeChDataTypePulseRevoLapHour, _
                                gCstCodeChDataTypePulseRevoExtDev, gCstCodeChDataTypePulseRevoExtDevDayHour, gCstCodeChDataTypePulseRevoExtDevLapHour

                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                intValue = 99999999 '9999999 '99999999
                                strNewValue = intValue.ToString
                            Case gCstCodeChDataTypePulseRevoTotalMin, gCstCodeChDataTypePulseRevoDayMin, gCstCodeChDataTypePulseRevoLapMin, _
                                gCstCodeChDataTypePulseRevoExtDevTotalMin, gCstCodeChDataTypePulseRevoExtDevDayMin, gCstCodeChDataTypePulseRevoExtDevLapMin

                                intValue = 9999959
                                If .RevoDecPoint = 0 Or .RevoDecPoint = Nothing Then
                                    strNewValue = intValue
                                Else
                                    strNewValue = intValue / 10 ^ .RevoDecPoint
                                End If
                        End Select
                    End With

                    'msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).RevoDecPoint, udt2.udtChannel(zch).RevoDecPoint)
                    msgtemp(i) = mMsgCreateStr("Decimal Point", strOldValue, strNewValue)
                    i = i + 1
                End If
            End If

            ''タグ名称
            If gCompareChk_11_RunHour(7) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).RevoTagNo, udt2.udtChannel(zch).RevoTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).RevoTagNo), gGetString(udt2.udtChannel(zch).RevoTagNo))
                    i = i + 1
                End If
            End If

            'Alarm MimicNo
            If gCompareChk_11_RunHour(8) = True Then 'Alarm MimicNo
                If udt1.udtChannel(ich).RevoAlmMimic <> udt2.udtChannel(zch).RevoAlmMimic Then
                    msgtemp(i) = mMsgCreateSht("Alarm MimicNo", udt1.udtChannel(ich).RevoAlmMimic, udt2.udtChannel(zch).RevoAlmMimic)
                    i = i + 1
                End If
            End If


            '変更項目数を返す
            mCompareSetChannelRevoDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

#Region "コンポジット設定OK"

    Friend Function mCompareSetCompositeDisp(ByVal udt1 As gTypSetChComposite, _
                                             ByVal udt2 As gTypSetChComposite) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　COMPOSITE SETTING　■■■■"

            Dim CHIDChange As Integer

            For i As Integer = LBound(udt1.udtComposite) To UBound(udt1.udtComposite)

                CHIDChange = gConvChIdToChNoComp(udt1.udtComposite(i).shtChid)

                ''CH ID
                If udt1.udtComposite(i).shtChid <> udt2.udtComposite(i).shtChid Then
                    msgSYStemp(ix) = mMsgCreateSEQ("ID", udt1.udtComposite(i).shtChid, udt2.udtComposite(i).shtChid, "CHNo.", CHIDChange, "", "")
                    ix = ix + 1
                End If

                ''ソフトウェアフィルタ定数
                If udt1.udtComposite(i).shtDiFilter <> udt2.udtComposite(i).shtDiFilter Then
                    msgSYStemp(ix) = mMsgCreateSEQ("di_Filter", udt1.udtComposite(i).shtDiFilter, udt2.udtComposite(i).shtDiFilter, "CHNo.", CHIDChange, "", "")
                    ix = ix + 1
                End If

                For j As Integer = LBound(udt1.udtComposite(i).udtCompInf) To UBound(udt1.udtComposite(i).udtCompInf)

                    ''ステータスビットパターン
                    If udt1.udtComposite(i).udtCompInf(j).bytBitPattern <> udt2.udtComposite(i).udtCompInf(j).bytBitPattern Then
                        msgSYStemp(ix) = mMsgCreateSEQ("bit_pattern", "0x" & Hex(udt1.udtComposite(i).udtCompInf(j).bytBitPattern).PadLeft(2, "0"), "0x" & Hex(udt2.udtComposite(i).udtCompInf(j).bytBitPattern).PadLeft(2, "0"), "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''ステータスビット使用有無
                    If gBitCheck(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 0) <> gBitCheck(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 0) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("Bit_use use", gBitValue(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 0), gBitValue(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 0), "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''アラーム有無
                    If gBitCheck(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 1) <> gBitCheck(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 1) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("Bit_use alarm", gBitValue(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 1), gBitValue(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 1), "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''リポーズ有無
                    If gBitCheck(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 2) <> gBitCheck(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 2) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("Bit_use repose", gBitValue(udt1.udtComposite(i).udtCompInf(j).bytAlarmUse, 2), gBitValue(udt2.udtComposite(i).udtCompInf(j).bytAlarmUse, 2), "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''ディレイタイマ値
                    If udt1.udtComposite(i).udtCompInf(j).bytDelay <> udt2.udtComposite(i).udtCompInf(j).bytDelay Then
                        msgSYStemp(ix) = mMsgCreateSEQ("delay", udt1.udtComposite(i).udtCompInf(j).bytDelay, udt2.udtComposite(i).udtCompInf(j).bytDelay, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''延長警報グループ
                    If udt1.udtComposite(i).udtCompInf(j).bytExtGroup <> udt2.udtComposite(i).udtCompInf(j).bytExtGroup Then
                        msgSYStemp(ix) = mMsgCreateSEQ("ext_group", udt1.udtComposite(i).udtCompInf(j).bytExtGroup, udt2.udtComposite(i).udtCompInf(j).bytExtGroup, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''グループリポーズ１
                    If udt1.udtComposite(i).udtCompInf(j).bytGRepose1 <> udt2.udtComposite(i).udtCompInf(j).bytGRepose1 Then
                        msgSYStemp(ix) = mMsgCreateSEQ("g_repose1", udt1.udtComposite(i).udtCompInf(j).bytGRepose1, udt2.udtComposite(i).udtCompInf(j).bytGRepose1, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''グループリポーズ２
                    If udt1.udtComposite(i).udtCompInf(j).bytGRepose2 <> udt2.udtComposite(i).udtCompInf(j).bytGRepose2 Then
                        msgSYStemp(ix) = mMsgCreateSEQ("g_repose2", udt1.udtComposite(i).udtCompInf(j).bytGRepose2, udt2.udtComposite(i).udtCompInf(j).bytGRepose2, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''ステータス名称
                    If Not gCompareString(udt1.udtComposite(i).udtCompInf(j).strStatusName, udt2.udtComposite(i).udtCompInf(j).strStatusName) Then
                        msgSYStemp(ix) = mMsgCreateSEQ("status_name", gGetString(udt1.udtComposite(i).udtCompInf(j).strStatusName), gGetString(udt2.udtComposite(i).udtCompInf(j).strStatusName), "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''マニュアルリポーズ状態
                    If udt1.udtComposite(i).udtCompInf(j).bytManualReposeState <> udt2.udtComposite(i).udtCompInf(j).bytManualReposeState Then
                        msgSYStemp(ix) = mMsgCreateSEQ("m_repose", udt1.udtComposite(i).udtCompInf(j).bytManualReposeState, udt2.udtComposite(i).udtCompInf(j).bytManualReposeState, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''マニュアルリポーズ
                    If udt1.udtComposite(i).udtCompInf(j).bytManualReposeSet <> udt2.udtComposite(i).udtCompInf(j).bytManualReposeSet Then
                        msgSYStemp(ix) = mMsgCreateSEQ("m_repose_set", udt1.udtComposite(i).udtCompInf(j).bytManualReposeSet, udt2.udtComposite(i).udtCompInf(j).bytManualReposeSet, "CHNo.", CHIDChange, "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報(PID)OK"

    Friend Function mCompareSetChannelPidDisp(ByVal udt1 As gTypSetChInfo, _
                                                 ByVal udt2 As gTypSetChInfo, _
                                                 ByVal ich As Integer, ByVal zch As Integer, ByVal CHNo As String, ByVal Gyo As Integer) As Integer
        Try
            'Ver2.0.6.5 PID CHの比較を追加

            Dim i As Integer = Gyo

            '========================
            ''PID CH
            '========================

            'SET値
            ' SET値は、無し＝０で、無しなのかゼロなのかが検知できないため
            ' 両方0の場合、Useの変化で検知させる


            ''アラーム　HH ------------------------------------------------------------------------------------
            ''アラーム有無
            If gCompareChk_12_Pid(0) = True Then 'Alarm HH Use
                If udt1.udtChannel(ich).PidHiHiUse <> udt2.udtChannel(zch).PidHiHiUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH Use", udt1.udtChannel(ich).PidHiHiUse, udt2.udtChannel(zch).PidHiHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_12_Pid(1) = True Then 'Alarm HH DLY
                If (udt1.udtChannel(ich).PidHiHiDelay <> udt2.udtChannel(zch).PidHiHiDelay) Or _
                    (udt1.udtChannel(ich).DummyDelayHH <> udt2.udtChannel(zch).DummyDelayHH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH DLY", udt1.udtChannel(ich).PidHiHiDelay, udt2.udtChannel(zch).PidHiHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayHH, udt2.udtChannel(zch).DummyDelayHH)
                    i = i + 1
                End If
            End If

            ''アラームセット値
            If gCompareChk_12_Pid(2) = True Then 'Alarm HH SET
                If (udt1.udtChannel(ich).PidHiHiValue <> udt2.udtChannel(zch).PidHiHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueHH <> udt2.udtChannel(zch).DummyValueHH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH SET", udt1.udtChannel(ich).PidHiHiValue, udt2.udtChannel(zch).PidHiHiValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueHH, udt2.udtChannel(zch).DummyValueHH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'SET値
                If (udt1.udtChannel(ich).PidHiHiValue = 0) And _
                    (udt2.udtChannel(zch).PidHiHiValue = 0) Then
                    If udt1.udtChannel(ich).PidHiHiUse <> udt2.udtChannel(zch).PidHiHiUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm HH SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).PidHiHiUse, _
                                                   "USE:" & udt2.udtChannel(zch).PidHiHiUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_12_Pid(3) = True Then 'Alarm HH EX
                If (udt1.udtChannel(ich).PidHiHiExtGroup <> udt2.udtChannel(zch).PidHiHiExtGroup) Or _
                    (udt1.udtChannel(ich).DummyExtGrHH <> udt2.udtChannel(zch).DummyExtGrHH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH EX", udt1.udtChannel(ich).PidHiHiExtGroup, udt2.udtChannel(zch).PidHiHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrHH, udt2.udtChannel(zch).DummyExtGrHH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_12_Pid(4) = True Then 'Alarm HH GR1
                If (udt1.udtChannel(ich).PidHiHiGroupRepose1 <> udt2.udtChannel(zch).PidHiHiGroupRepose1) Or _
                    (udt1.udtChannel(ich).DummyGRep1HH <> udt2.udtChannel(zch).DummyGRep1HH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH GR1", udt1.udtChannel(ich).PidHiHiGroupRepose1, udt2.udtChannel(zch).PidHiHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1HH, udt2.udtChannel(zch).DummyGRep1HH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_12_Pid(5) = True Then 'Alarm HH GR2
                If udt1.udtChannel(ich).PidHiHiGroupRepose2 <> udt2.udtChannel(zch).PidHiHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2HH <> udt2.udtChannel(zch).DummyGRep2HH) Then
                    msgtemp(i) = mMsgCreateint("Alarm HH GR2", udt1.udtChannel(ich).PidHiHiGroupRepose2, udt2.udtChannel(zch).PidHiHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2HH, udt2.udtChannel(zch).DummyGRep2HH)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_12_Pid(6) = True Then 'Alarm HH STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).PidHiHiStatusInput), NZfS(udt2.udtChannel(zch).PidHiHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmHH <> udt2.udtChannel(zch).DummyStaNmHH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmHH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmHH, "#", "")

                    msgtemp(i) = mMsgCreateStr("Alarm HH STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).PidHiHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).PidHiHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_12_Pid(7) = True Then 'Alarm HH MR Status
                If udt1.udtChannel(ich).PidHiHiManualReposeState <> udt2.udtChannel(zch).PidHiHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH MR Status", udt1.udtChannel(ich).PidHiHiManualReposeState, udt2.udtChannel(zch).PidHiHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_12_Pid(8) = True Then 'Alarm HH MR Set
                If udt1.udtChannel(ich).PidHiHiManualReposeSet <> udt2.udtChannel(zch).PidHiHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm HH MR Set", udt1.udtChannel(ich).PidHiHiManualReposeSet, udt2.udtChannel(zch).PidHiHiManualReposeSet)
                    i = i + 1
                End If
            End If


            ''アラーム　H ------------------------------------------------------------------------------------
            ''アラーム有無
            If gCompareChk_12_Pid(9) = True Then 'Alarm H Use
                If udt1.udtChannel(ich).PidHiUse <> udt2.udtChannel(zch).PidHiUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm H Use", udt1.udtChannel(ich).PidHiUse, udt2.udtChannel(zch).PidHiUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_12_Pid(10) = True Then 'Alarm H DLY
                If udt1.udtChannel(ich).PidHiDelay <> udt2.udtChannel(zch).PidHiDelay Or _
                    (udt1.udtChannel(ich).DummyDelayH <> udt2.udtChannel(zch).DummyDelayH) Then
                    msgtemp(i) = mMsgCreateint("Alarm H DLY", udt1.udtChannel(ich).PidHiDelay, udt2.udtChannel(zch).PidHiDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayH, udt2.udtChannel(zch).DummyDelayH)
                    i = i + 1
                End If
            End If

            ''アラームセット値      
            If gCompareChk_12_Pid(11) = True Then 'Alarm H SET
                If (udt1.udtChannel(ich).PidHiValue <> udt2.udtChannel(zch).PidHiValue) Or _
                    (udt1.udtChannel(ich).DummyValueH <> udt2.udtChannel(zch).DummyValueH) Then
                    msgtemp(i) = mMsgCreateint("Alarm H SET", udt1.udtChannel(ich).PidHiValue, udt2.udtChannel(zch).PidHiValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueH, udt2.udtChannel(zch).DummyValueH)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'SET値
                If (udt1.udtChannel(ich).PidHiValue = 0) And _
                    (udt2.udtChannel(zch).PidHiValue = 0) Then
                    If udt1.udtChannel(ich).PidHiUse <> udt2.udtChannel(zch).PidHiUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm H SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).PidHiUse, _
                                                   "USE:" & udt2.udtChannel(zch).PidHiUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_12_Pid(12) = True Then 'Alarm H EX
                If udt1.udtChannel(ich).PidHiExtGroup <> udt2.udtChannel(zch).PidHiExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrH <> udt2.udtChannel(zch).DummyExtGrH) Then
                    msgtemp(i) = mMsgCreateint("Alarm H EX", udt1.udtChannel(ich).PidHiExtGroup, udt2.udtChannel(zch).PidHiExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrH, udt2.udtChannel(zch).DummyExtGrH)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_12_Pid(13) = True Then 'Alarm H GR1
                If udt1.udtChannel(ich).PidHiGroupRepose1 <> udt2.udtChannel(zch).PidHiGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1H <> udt2.udtChannel(zch).DummyGRep1H) Then
                    msgtemp(i) = mMsgCreateint("Alarm H GR1", udt1.udtChannel(ich).PidHiGroupRepose1, udt2.udtChannel(zch).PidHiGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1H, udt2.udtChannel(zch).DummyGRep1H)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_12_Pid(14) = True Then 'Alarm H GR2
                If udt1.udtChannel(ich).PidHiGroupRepose2 <> udt2.udtChannel(zch).PidHiGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2H <> udt2.udtChannel(zch).DummyGRep2H) Then
                    msgtemp(i) = mMsgCreateint("Alarm H GR2", udt1.udtChannel(ich).PidHiGroupRepose2, udt2.udtChannel(zch).PidHiGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2H, udt2.udtChannel(zch).DummyGRep2H)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_12_Pid(15) = True Then 'Alarm H Status
                If Not gCompareString(NZfS(udt1.udtChannel(ich).PidHiStatusInput), NZfS(udt2.udtChannel(zch).PidHiStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmH <> udt2.udtChannel(zch).DummyStaNmH) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmH, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmH, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm H Status", strOldDmy & gGetString(udt1.udtChannel(ich).PidHiStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).PidHiStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_12_Pid(16) = True Then 'Alarm H MR Status
                If udt1.udtChannel(ich).PidHiManualReposeState <> udt2.udtChannel(zch).PidHiManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm H MR Status", udt1.udtChannel(ich).PidHiManualReposeState, udt2.udtChannel(zch).PidHiManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_12_Pid(17) = True Then 'Alarm H MR Set
                If udt1.udtChannel(ich).PidHiManualReposeSet <> udt2.udtChannel(zch).PidHiManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm H MR Set", udt1.udtChannel(ich).PidHiManualReposeSet, udt2.udtChannel(zch).PidHiManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　L ------------------------------------------------------------------------------------
            ''アラーム有無
            If gCompareChk_12_Pid(18) = True Then 'Alarm L Use
                If udt1.udtChannel(ich).PidLoUse <> udt2.udtChannel(zch).PidLoUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm L Use", udt1.udtChannel(ich).PidLoUse, udt2.udtChannel(zch).PidLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_12_Pid(19) = True Then 'Alarm L DLY
                If udt1.udtChannel(ich).PidLoDelay <> udt2.udtChannel(zch).PidLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayL <> udt2.udtChannel(zch).DummyDelayL) Then
                    msgtemp(i) = mMsgCreateint("Alarm L DLY", udt1.udtChannel(ich).PidLoDelay, udt2.udtChannel(zch).PidLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayL, udt2.udtChannel(zch).DummyDelayL)
                    i = i + 1
                End If
            End If

            ''アラームセット値      
            If gCompareChk_12_Pid(20) = True Then 'Alarm L SET
                If (udt1.udtChannel(ich).PidLoValue <> udt2.udtChannel(zch).PidLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueL <> udt2.udtChannel(zch).DummyValueL) Then
                    msgtemp(i) = mMsgCreateint("Alarm L SET", udt1.udtChannel(ich).PidLoValue, udt2.udtChannel(zch).PidLoValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueL, udt2.udtChannel(zch).DummyValueL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'Ver2.0.0.5 SET値
                If (udt1.udtChannel(ich).PidLoValue = 0) And _
                    (udt2.udtChannel(zch).PidLoValue = 0) Then
                    If udt1.udtChannel(ich).PidLoUse <> udt2.udtChannel(zch).PidLoUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm L SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).PidLoUse, _
                                                   "USE:" & udt2.udtChannel(zch).PidLoUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_12_Pid(21) = True Then 'Alarm L EX
                If udt1.udtChannel(ich).PidLoExtGroup <> udt2.udtChannel(zch).PidLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrL <> udt2.udtChannel(zch).DummyExtGrL) Then
                    msgtemp(i) = mMsgCreateint("Alarm L EX", udt1.udtChannel(ich).PidLoExtGroup, udt2.udtChannel(zch).PidLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrL, udt2.udtChannel(zch).DummyExtGrL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_12_Pid(22) = True Then 'Alarm L GR1
                If udt1.udtChannel(ich).PidLoGroupRepose1 <> udt2.udtChannel(zch).PidLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1L <> udt2.udtChannel(zch).DummyGRep1L) Then
                    msgtemp(i) = mMsgCreateint("Alarm L GR1", udt1.udtChannel(ich).PidLoGroupRepose1, udt2.udtChannel(zch).PidLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1L, udt2.udtChannel(zch).DummyGRep1L)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_12_Pid(23) = True Then 'Alarm L GR2
                If udt1.udtChannel(ich).PidLoGroupRepose2 <> udt2.udtChannel(zch).PidLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2L <> udt2.udtChannel(zch).DummyGRep2L) Then
                    msgtemp(i) = mMsgCreateint("Alarm L GR2", udt1.udtChannel(ich).PidLoGroupRepose2, udt2.udtChannel(zch).PidLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2L, udt2.udtChannel(zch).DummyGRep2L)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_12_Pid(24) = True Then 'Alarm L STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).PidLoStatusInput), NZfS(udt2.udtChannel(zch).PidLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmL <> udt2.udtChannel(zch).DummyStaNmL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmL, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm L STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).PidLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).PidLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_12_Pid(25) = True Then 'Alarm L MR Status
                If udt1.udtChannel(ich).PidLoManualReposeState <> udt2.udtChannel(zch).PidLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm L MR Status", udt1.udtChannel(ich).PidLoManualReposeState, udt2.udtChannel(zch).PidLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_12_Pid(26) = True Then 'Alarm L MR Set
                If udt1.udtChannel(ich).PidLoManualReposeSet <> udt2.udtChannel(zch).PidLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm L MR Set", udt1.udtChannel(ich).PidLoManualReposeSet, udt2.udtChannel(zch).PidLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　LL ------------------------------------------------------------------------------------

            ''アラーム有無
            If gCompareChk_12_Pid(27) = True Then 'Alarm LL Use
                If udt1.udtChannel(ich).PidLoLoUse <> udt2.udtChannel(zch).PidLoLoUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL Use", udt1.udtChannel(ich).PidLoLoUse, udt2.udtChannel(zch).PidLoLoUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_12_Pid(28) = True Then 'Alarm LL DLY
                If udt1.udtChannel(ich).PidLoLoDelay <> udt2.udtChannel(zch).PidLoLoDelay Or _
                    (udt1.udtChannel(ich).DummyDelayLL <> udt2.udtChannel(zch).DummyDelayLL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL DLY", udt1.udtChannel(ich).PidLoLoDelay, udt2.udtChannel(zch).PidLoLoDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelayLL, udt2.udtChannel(zch).DummyDelayLL)
                    i = i + 1
                End If
            End If

            ''アラームセット値
            If gCompareChk_12_Pid(29) = True Then 'Alarm LL SET
                If (udt1.udtChannel(ich).PidLoLoValue <> udt2.udtChannel(zch).PidLoLoValue) Or _
                    (udt1.udtChannel(ich).DummyValueLL <> udt2.udtChannel(zch).DummyValueLL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL SET", udt1.udtChannel(ich).PidLoLoValue, udt2.udtChannel(zch).PidLoLoValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               udt1.udtChannel(ich).DummyValueLL, udt2.udtChannel(zch).DummyValueLL)  '' Ver1.11.5 2016.08.25 小数点以下桁数追加
                    i = i + 1
                End If

                'SET値
                If (udt1.udtChannel(ich).PidLoLoValue = 0) And _
                    (udt2.udtChannel(zch).PidLoLoValue = 0) Then
                    If udt1.udtChannel(ich).PidLoLoUse <> udt2.udtChannel(zch).PidLoLoUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm LL SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).PidLoLoUse, _
                                                   "USE:" & udt2.udtChannel(zch).PidLoLoUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_12_Pid(30) = True Then 'Alarm LL EX
                If udt1.udtChannel(ich).PidLoLoExtGroup <> udt2.udtChannel(zch).PidLoLoExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrLL <> udt2.udtChannel(zch).DummyExtGrLL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL EX", udt1.udtChannel(ich).PidLoLoExtGroup, udt2.udtChannel(zch).PidLoLoExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrLL, udt2.udtChannel(zch).DummyExtGrLL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_12_Pid(31) = True Then 'Alarm LL GR1
                If udt1.udtChannel(ich).PidLoLoGroupRepose1 <> udt2.udtChannel(zch).PidLoLoGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1LL <> udt2.udtChannel(zch).DummyGRep1LL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL GR1", udt1.udtChannel(ich).PidLoLoGroupRepose1, udt2.udtChannel(zch).PidLoLoGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1LL, udt2.udtChannel(zch).DummyGRep1LL)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_12_Pid(32) = True Then 'Alarm LL GR2
                If udt1.udtChannel(ich).PidLoLoGroupRepose2 <> udt2.udtChannel(zch).PidLoLoGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2LL <> udt2.udtChannel(zch).DummyGRep2LL) Then
                    msgtemp(i) = mMsgCreateint("Alarm LL GR2", udt1.udtChannel(ich).PidLoLoGroupRepose2, udt2.udtChannel(zch).PidLoLoGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2LL, udt2.udtChannel(zch).DummyGRep2LL)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_12_Pid(33) = True Then 'Alarm LL STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).PidLoLoStatusInput), NZfS(udt2.udtChannel(zch).PidLoLoStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmLL <> udt2.udtChannel(zch).DummyStaNmLL) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmLL, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmLL, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm LL STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).PidLoLoStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).PidLoLoStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_12_Pid(34) = True Then 'Alarm LL MR Status
                If udt1.udtChannel(ich).PidLoLoManualReposeState <> udt2.udtChannel(zch).PidLoLoManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL MR Status", udt1.udtChannel(ich).PidLoLoManualReposeState, udt2.udtChannel(zch).PidLoLoManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_12_Pid(35) = True Then 'Alarm LL MR Set
                If udt1.udtChannel(ich).PidLoLoManualReposeSet <> udt2.udtChannel(zch).PidLoLoManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm LL MR Set", udt1.udtChannel(ich).PidLoLoManualReposeSet, udt2.udtChannel(zch).PidLoLoManualReposeSet)
                    i = i + 1
                End If
            End If

            ''アラーム　SF  ------------------------------------------------------------------------------------
            ''アラーム有無
            If gCompareChk_12_Pid(36) = True Then 'Alarm S Use
                If udt1.udtChannel(ich).PidSensorFailUse <> udt2.udtChannel(zch).PidSensorFailUse Then
                    msgtemp(i) = mMsgCreateSht("Alarm S Use", udt1.udtChannel(ich).PidSensorFailUse, udt2.udtChannel(zch).PidSensorFailUse)
                    i = i + 1
                End If
            End If

            ''ディレイタイマ値
            If gCompareChk_12_Pid(37) = True Then 'Alarm S DLY
                If udt1.udtChannel(ich).PidSensorFailDelay <> udt2.udtChannel(zch).PidSensorFailDelay Or _
                    (udt1.udtChannel(ich).DummyDelaySF <> udt2.udtChannel(zch).DummyDelaySF) Then
                    msgtemp(i) = mMsgCreateint("Alarm S DLY", udt1.udtChannel(ich).PidSensorFailDelay, udt2.udtChannel(zch).PidSensorFailDelay, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyDelaySF, udt2.udtChannel(zch).DummyDelaySF)
                    i = i + 1
                End If
            End If

            ''アラームセット値
            If gCompareChk_12_Pid(38) = True Then 'Alarm S SET
                If udt1.udtChannel(ich).PidSensorFailValue <> udt2.udtChannel(zch).PidSensorFailValue Then
                    msgtemp(i) = mMsgCreateint("Alarm S SET", udt1.udtChannel(ich).PidSensorFailValue, udt2.udtChannel(zch).PidSensorFailValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               False, False)
                    i = i + 1
                End If

                'SET値
                If (udt1.udtChannel(ich).PidSensorFailValue = 0) And _
                    (udt2.udtChannel(zch).PidSensorFailValue = 0) Then
                    If udt1.udtChannel(ich).PidSensorFailUse <> udt2.udtChannel(zch).PidSensorFailUse Then
                        msgtemp(i) = mMsgCreateStr("Alarm S SET(Nothing or ZERO)", _
                                                   "USE:" & udt1.udtChannel(ich).PidSensorFailUse, _
                                                   "USE:" & udt2.udtChannel(zch).PidSensorFailUse)
                        i = i + 1
                    End If
                End If
            End If

            ''延長警報グループ
            If gCompareChk_12_Pid(39) = True Then 'Alarm S EX
                If udt1.udtChannel(ich).PidSensorFailExtGroup <> udt2.udtChannel(zch).PidSensorFailExtGroup Or _
                    (udt1.udtChannel(ich).DummyExtGrSF <> udt2.udtChannel(zch).DummyExtGrSF) Then
                    msgtemp(i) = mMsgCreateint("Alarm S EX", udt1.udtChannel(ich).PidSensorFailExtGroup, udt2.udtChannel(zch).PidSensorFailExtGroup, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyExtGrSF, udt2.udtChannel(zch).DummyExtGrSF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ１
            If gCompareChk_12_Pid(40) = True Then 'Alarm S GR1
                If udt1.udtChannel(ich).PidSensorFailGroupRepose1 <> udt2.udtChannel(zch).PidSensorFailGroupRepose1 Or _
                    (udt1.udtChannel(ich).DummyGRep1SF <> udt2.udtChannel(zch).DummyGRep1SF) Then
                    msgtemp(i) = mMsgCreateint("Alarm S GR1", udt1.udtChannel(ich).PidSensorFailGroupRepose1, udt2.udtChannel(zch).PidSensorFailGroupRepose1, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep1SF, udt2.udtChannel(zch).DummyGRep1SF)
                    i = i + 1
                End If
            End If

            ''グループリポーズ２
            If gCompareChk_12_Pid(41) = True Then 'Alarm S GR2
                If udt1.udtChannel(ich).PidSensorFailGroupRepose2 <> udt2.udtChannel(zch).PidSensorFailGroupRepose2 Or _
                    (udt1.udtChannel(ich).DummyGRep2SF <> udt2.udtChannel(zch).DummyGRep2SF) Then
                    msgtemp(i) = mMsgCreateint("Alarm S GR2", udt1.udtChannel(ich).PidSensorFailGroupRepose2, udt2.udtChannel(zch).PidSensorFailGroupRepose2, _
                                               0, 0, _
                                               udt1.udtChannel(ich).DummyGRep2SF, udt2.udtChannel(zch).DummyGRep2SF)
                    i = i + 1
                End If
            End If

            ''ステータス名称
            If gCompareChk_12_Pid(42) = True Then 'Alarm S STATUS
                If Not gCompareString(NZfS(udt1.udtChannel(ich).PidSensorFailStatusInput), NZfS(udt2.udtChannel(zch).PidSensorFailStatusInput)) Or _
                    (udt1.udtChannel(ich).DummyStaNmSF <> udt2.udtChannel(zch).DummyStaNmSF) Then
                    Dim strOldDmy As String = IIf(udt1.udtChannel(ich).DummyStaNmSF, "#", "")
                    Dim strNewDmy As String = IIf(udt2.udtChannel(zch).DummyStaNmSF, "#", "")
                    msgtemp(i) = mMsgCreateStr("Alarm S STATUS", strOldDmy & gGetString(udt1.udtChannel(ich).PidSensorFailStatusInput), strNewDmy & gGetString(udt2.udtChannel(zch).PidSensorFailStatusInput))
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ状態
            If gCompareChk_12_Pid(43) = True Then 'Alarm S MR Status
                If udt1.udtChannel(ich).PidSensorFailManualReposeState <> udt2.udtChannel(zch).PidSensorFailManualReposeState Then
                    msgtemp(i) = mMsgCreateSht("Alarm S MR Status", udt1.udtChannel(ich).PidSensorFailManualReposeState, udt2.udtChannel(zch).PidSensorFailManualReposeState)
                    i = i + 1
                End If
            End If

            ''マニュアルリポーズ
            If gCompareChk_12_Pid(44) = True Then 'Alarm S MR Set
                If udt1.udtChannel(ich).PidSensorFailManualReposeSet <> udt2.udtChannel(zch).PidSensorFailManualReposeSet Then
                    msgtemp(i) = mMsgCreateSht("Alarm S MR Set", udt1.udtChannel(ich).PidSensorFailManualReposeSet, udt2.udtChannel(zch).PidSensorFailManualReposeSet)
                    i = i + 1
                End If
            End If
            ''-----------------------------------------------------------------------------------------------

            ''スケール値　上限/下限値
            If gCompareChk_12_Pid(45) = True Then 'RANGE
                i = GetRange("RANGE", udt1.udtChannel(ich).PidRangeHigh, udt2.udtChannel(zch).PidRangeHigh, _
                             udt1.udtChannel(ich).PidRangeLow, udt2.udtChannel(zch).PidRangeLow, _
                             udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeScale, udt2.udtChannel(zch).DummyRangeScale, i)
            End If

            ''ノーマルレンジ　上限/下限
            If gCompareChk_12_Pid(46) = True Then 'NOR RANGE
                i = GetNorRange("NOR RANGE", udt1.udtChannel(ich).PidNormalHigh, udt2.udtChannel(zch).PidNormalHigh, _
                             udt1.udtChannel(ich).PidNormalLow, udt2.udtChannel(zch).PidNormalLow, _
                             udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                             udt1.udtChannel(ich).DummyRangeNormalHi, udt2.udtChannel(zch).DummyRangeNormalHi, _
                             udt1.udtChannel(ich).DummyRangeNormalLo, udt2.udtChannel(zch).DummyRangeNormalLo, i)
            End If

            ''オフセット値
            If gCompareChk_12_Pid(47) = True Then 'OFFSET
                If udt1.udtChannel(ich).PidOffsetValue <> udt2.udtChannel(zch).PidOffsetValue Then
                    msgtemp(i) = mMsgCreateint("OFFSET", udt1.udtChannel(ich).PidOffsetValue, udt2.udtChannel(zch).PidOffsetValue, _
                                               udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition, _
                                               False, False)
                    i = i + 1
                End If
            End If

            ''表示固定位置
            If gCompareChk_12_Pid(48) = True Then 'String
                If udt1.udtChannel(ich).PidString <> udt2.udtChannel(zch).PidString Then
                    msgtemp(i) = mMsgCreateSht("String", udt1.udtChannel(ich).PidString, udt2.udtChannel(zch).PidString)
                    i = i + 1
                End If
            End If

            ''小数点以下桁数
            If gCompareChk_12_Pid(49) = True Then 'Decimal Point
                If udt1.udtChannel(ich).PidDecimalPosition <> udt2.udtChannel(zch).PidDecimalPosition Then
                    msgtemp(i) = mMsgCreateSht("Decimal Point", udt1.udtChannel(ich).PidDecimalPosition, udt2.udtChannel(zch).PidDecimalPosition)
                    i = i + 1
                End If
            End If

            ''センター表示
            If gCompareChk_12_Pid(50) = True Then 'BAR GRAPH CENTER
                i = GetBitCHK("BAR GRAPH CENTER", udt1.udtChannel(ich).PidDisplay3, 0, udt2.udtChannel(zch).PidDisplay3, 0, i)
            End If

            ''Sensor異常表示
            If gCompareChk_12_Pid(51) = True Then 'SENSOR ALM(UNDER)
                i = GetBitCHK("SENSOR ALM(UNDER)", udt1.udtChannel(ich).PidDisplay3, 1, udt2.udtChannel(zch).PidDisplay3, 1, i)   '' Ver1.11.9.8 2016.12.15 ﾋﾞｯﾄ位置 2 → 1
            End If
            If gCompareChk_12_Pid(52) = True Then 'SENSOR ALM(OVER)
                i = GetBitCHK("SENSOR ALM(OVER)", udt1.udtChannel(ich).PidDisplay3, 2, udt2.udtChannel(zch).PidDisplay3, 2, i)    '' Ver1.11.9.8 2016.12.15 ﾋﾞｯﾄ位置 3 → 2
            End If


            ''タグ名称
            If gCompareChk_12_Pid(54) = True Then 'TagNo
                If Not gCompareString(udt1.udtChannel(ich).PidTagNo, udt2.udtChannel(zch).PidTagNo) Then
                    msgtemp(i) = mMsgCreateStr("TagNo.", gGetString(udt1.udtChannel(ich).PidTagNo), gGetString(udt2.udtChannel(zch).PidTagNo))
                    i = i + 1
                End If
            End If

            ''LR Mode
            If gCompareChk_12_Pid(55) = True Then 'LR Mode
                If Not gCompareString(udt1.udtChannel(ich).PidLRMode, udt2.udtChannel(zch).PidLRMode) Then

                    Dim strOldLRMode As String = ""
                    Dim strNewLRMode As String = ""

                    strOldLRMode = GetMode(udt1.udtChannel(ich).udtChCommon.shtStatus)
                    strNewLRMode = GetMode(udt2.udtChannel(zch).udtChCommon.shtStatus)

                    msgtemp(i) = mMsgCreateStr("LR Mode", strOldLRMode, strNewLRMode)
                    i = i + 1

                End If
            End If


            'ダミー設定
            If gCompareChk_12_Pid(56) = True Then 'Dummy Setting
            End If


            'PID
            '58 FU全部
            If gCompareChk_12_Pid(58) = True Then
                If udt1.udtChannel(ich).PidOutFuNo <> udt2.udtChannel(zch).PidOutFuNo Or _
                    udt1.udtChannel(ich).PidOutPortNo <> udt2.udtChannel(zch).PidOutPortNo Or _
                    udt1.udtChannel(ich).PidOutPin <> udt2.udtChannel(zch).PidOutPin Then

                    Dim strOldOutFU As String = udt1.udtChannel(ich).PidOutFuNo & "-" & udt1.udtChannel(ich).PidOutPortNo & "-" & udt1.udtChannel(ich).PidOutPin
                    Dim strNewOutFU As String = udt2.udtChannel(zch).PidOutFuNo & "-" & udt2.udtChannel(zch).PidOutPortNo & "-" & udt2.udtChannel(zch).PidOutPin
                    msgtemp(i) = mMsgCreateStr("OUT FU ADRESS", strOldOutFU, strNewOutFU)
                    i = i + 1
                End If
            End If
            '59 OutPIN NO
            If gCompareChk_12_Pid(59) = True Then
                If udt1.udtChannel(ich).PidOutPinNo <> udt2.udtChannel(zch).PidOutPinNo Then
                    msgtemp(i) = mMsgCreateSht("OUT PIN No", udt1.udtChannel(ich).PidOutPinNo, udt2.udtChannel(zch).PidOutPinNo)
                    i = i + 1
                End If
            End If

            'PID DEF
            '60 sp_high
            If gCompareChk_12_Pid(60) = True Then
                If udt1.udtChannel(ich).PidDefSpHigh <> udt2.udtChannel(zch).PidDefSpHigh Then
                    msgtemp(i) = mMsgCreateSht("PID Sp High", udt1.udtChannel(ich).PidDefSpHigh, udt2.udtChannel(zch).PidDefSpHigh)
                    i = i + 1
                End If
            End If
            '61 sp_low
            If gCompareChk_12_Pid(61) = True Then
                If udt1.udtChannel(ich).PidDefSpLow <> udt2.udtChannel(zch).PidDefSpLow Then
                    msgtemp(i) = mMsgCreateSht("PID Sp Low", udt1.udtChannel(ich).PidDefSpLow, udt2.udtChannel(zch).PidDefSpLow)
                    i = i + 1
                End If
            End If
            '62 mv_high
            If gCompareChk_12_Pid(62) = True Then
                If udt1.udtChannel(ich).PidDefMvHigh <> udt2.udtChannel(zch).PidDefMvHigh Then
                    msgtemp(i) = mMsgCreateSht("PID Mv High", udt1.udtChannel(ich).PidDefMvHigh, udt2.udtChannel(zch).PidDefMvHigh)
                    i = i + 1
                End If
            End If
            '63 mv_low
            If gCompareChk_12_Pid(63) = True Then
                If udt1.udtChannel(ich).PidDefMvLow <> udt2.udtChannel(zch).PidDefMvLow Then
                    msgtemp(i) = mMsgCreateSht("PID Mv Low", udt1.udtChannel(ich).PidDefMvLow, udt2.udtChannel(zch).PidDefMvLow)
                    i = i + 1
                End If
            End If
            '64 pb
            If gCompareChk_12_Pid(64) = True Then
                If udt1.udtChannel(ich).PidDefPB <> udt2.udtChannel(zch).PidDefPB Then
                    msgtemp(i) = mMsgCreateSht("PID PB", udt1.udtChannel(ich).PidDefPB, udt2.udtChannel(zch).PidDefPB)
                    i = i + 1
                End If
            End If
            '65 ti
            If gCompareChk_12_Pid(65) = True Then
                If udt1.udtChannel(ich).PidDefTI <> udt2.udtChannel(zch).PidDefTI Then
                    msgtemp(i) = mMsgCreateSht("PID TI", udt1.udtChannel(ich).PidDefTI, udt2.udtChannel(zch).PidDefTI)
                    i = i + 1
                End If
            End If
            '66 TD
            If gCompareChk_12_Pid(66) = True Then
                If udt1.udtChannel(ich).PidDefTD <> udt2.udtChannel(zch).PidDefTD Then
                    msgtemp(i) = mMsgCreateSht("PID TD", udt1.udtChannel(ich).PidDefTD, udt2.udtChannel(zch).PidDefTD)
                    i = i + 1
                End If
            End If
            '67 gap
            If gCompareChk_12_Pid(67) = True Then
                If udt1.udtChannel(ich).PidDefGAP <> udt2.udtChannel(zch).PidDefGAP Then
                    msgtemp(i) = mMsgCreateSht("PID GAP", udt1.udtChannel(ich).PidDefGAP, udt2.udtChannel(zch).PidDefGAP)
                    i = i + 1
                End If
            End If

            'PID EXT
            ' 1
            '68 para1
            If gCompareChk_12_Pid(68) = True Then
                If udt1.udtChannel(ich).PidExtPara1 <> udt2.udtChannel(zch).PidExtPara1 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA1", udt1.udtChannel(ich).PidExtPara1, udt2.udtChannel(zch).PidExtPara1)
                    i = i + 1
                End If
            End If
            '69 para_high1
            If gCompareChk_12_Pid(69) = True Then
                If udt1.udtChannel(ich).PidExtParaHigh1 <> udt2.udtChannel(zch).PidExtParaHigh1 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA HIGH1", udt1.udtChannel(ich).PidExtParaHigh1, udt2.udtChannel(zch).PidExtParaHigh1)
                    i = i + 1
                End If
            End If
            '70 para_low1
            If gCompareChk_12_Pid(70) = True Then
                If udt1.udtChannel(ich).PidExtParaLow1 <> udt2.udtChannel(zch).PidExtParaLow1 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA LOW1", udt1.udtChannel(ich).PidExtParaLow1, udt2.udtChannel(zch).PidExtParaLow1)
                    i = i + 1
                End If
            End If
            '71 para_name1
            If gCompareChk_12_Pid(71) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaName1, udt2.udtChannel(zch).PidExtParaName1) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA NAME1", gGetString(udt1.udtChannel(ich).PidExtParaName1), gGetString(udt2.udtChannel(zch).PidExtParaName1))
                    i = i + 1
                End If
            End If
            '72 para_unit1
            If gCompareChk_12_Pid(72) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaUnit1, udt2.udtChannel(zch).PidExtParaUnit1) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA UNIT1", gGetString(udt1.udtChannel(ich).PidExtParaUnit1), gGetString(udt2.udtChannel(zch).PidExtParaUnit1))
                    i = i + 1
                End If
            End If
            ' 2
            '73 para2
            If gCompareChk_12_Pid(73) = True Then
                If udt1.udtChannel(ich).PidExtPara2 <> udt2.udtChannel(zch).PidExtPara2 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA2", udt1.udtChannel(ich).PidExtPara2, udt2.udtChannel(zch).PidExtPara2)
                    i = i + 1
                End If
            End If
            '74 para_high2
            If gCompareChk_12_Pid(74) = True Then
                If udt1.udtChannel(ich).PidExtParaHigh2 <> udt2.udtChannel(zch).PidExtParaHigh2 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA HIGH2", udt1.udtChannel(ich).PidExtParaHigh2, udt2.udtChannel(zch).PidExtParaHigh2)
                    i = i + 1
                End If
            End If
            '75 para_low2
            If gCompareChk_12_Pid(75) = True Then
                If udt1.udtChannel(ich).PidExtParaLow2 <> udt2.udtChannel(zch).PidExtParaLow2 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA LOW2", udt1.udtChannel(ich).PidExtParaLow2, udt2.udtChannel(zch).PidExtParaLow2)
                    i = i + 1
                End If
            End If
            '76 para_name2
            If gCompareChk_12_Pid(76) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaName2, udt2.udtChannel(zch).PidExtParaName2) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA NAME2", gGetString(udt1.udtChannel(ich).PidExtParaName2), gGetString(udt2.udtChannel(zch).PidExtParaName2))
                    i = i + 1
                End If
            End If
            '77 para_unit2
            If gCompareChk_12_Pid(77) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaUnit2, udt2.udtChannel(zch).PidExtParaUnit2) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA UNIT2", gGetString(udt1.udtChannel(ich).PidExtParaUnit2), gGetString(udt2.udtChannel(zch).PidExtParaUnit2))
                    i = i + 1
                End If
            End If
            ' 3
            '78 para3
            If gCompareChk_12_Pid(78) = True Then
                If udt1.udtChannel(ich).PidExtPara3 <> udt2.udtChannel(zch).PidExtPara3 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA3", udt1.udtChannel(ich).PidExtPara3, udt2.udtChannel(zch).PidExtPara3)
                    i = i + 1
                End If
            End If
            '79 para_high3
            If gCompareChk_12_Pid(79) = True Then
                If udt1.udtChannel(ich).PidExtParaHigh3 <> udt2.udtChannel(zch).PidExtParaHigh3 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA HIGH3", udt1.udtChannel(ich).PidExtParaHigh3, udt2.udtChannel(zch).PidExtParaHigh3)
                    i = i + 1
                End If
            End If
            '80 para_low3
            If gCompareChk_12_Pid(80) = True Then
                If udt1.udtChannel(ich).PidExtParaLow3 <> udt2.udtChannel(zch).PidExtParaLow3 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA LOW3", udt1.udtChannel(ich).PidExtParaLow3, udt2.udtChannel(zch).PidExtParaLow3)
                    i = i + 1
                End If
            End If
            '81 para_name3
            If gCompareChk_12_Pid(81) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaName3, udt2.udtChannel(zch).PidExtParaName3) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA NAME3", gGetString(udt1.udtChannel(ich).PidExtParaName3), gGetString(udt2.udtChannel(zch).PidExtParaName3))
                    i = i + 1
                End If
            End If
            '82 para_unit3
            If gCompareChk_12_Pid(82) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaUnit3, udt2.udtChannel(zch).PidExtParaUnit3) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA UNIT3", gGetString(udt1.udtChannel(ich).PidExtParaUnit3), gGetString(udt2.udtChannel(zch).PidExtParaUnit3))
                    i = i + 1
                End If
            End If
            ' 4
            '83 para4
            If gCompareChk_12_Pid(83) = True Then
                If udt1.udtChannel(ich).PidExtPara4 <> udt2.udtChannel(zch).PidExtPara4 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA4", udt1.udtChannel(ich).PidExtPara4, udt2.udtChannel(zch).PidExtPara4)
                    i = i + 1
                End If
            End If
            '84 para_high4
            If gCompareChk_12_Pid(84) = True Then
                If udt1.udtChannel(ich).PidExtParaHigh4 <> udt2.udtChannel(zch).PidExtParaHigh4 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA HIGH4", udt1.udtChannel(ich).PidExtParaHigh4, udt2.udtChannel(zch).PidExtParaHigh4)
                    i = i + 1
                End If
            End If
            '85 para_low4
            If gCompareChk_12_Pid(85) = True Then
                If udt1.udtChannel(ich).PidExtParaLow4 <> udt2.udtChannel(zch).PidExtParaLow4 Then
                    msgtemp(i) = mMsgCreateSht("PID PARA LOW4", udt1.udtChannel(ich).PidExtParaLow4, udt2.udtChannel(zch).PidExtParaLow4)
                    i = i + 1
                End If
            End If
            '86 para_name4
            If gCompareChk_12_Pid(86) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaName4, udt2.udtChannel(zch).PidExtParaName4) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA NAME4", gGetString(udt1.udtChannel(ich).PidExtParaName4), gGetString(udt2.udtChannel(zch).PidExtParaName4))
                    i = i + 1
                End If
            End If
            '87 para_unit4
            If gCompareChk_12_Pid(87) = True Then
                If Not gCompareString(udt1.udtChannel(ich).PidExtParaUnit4, udt2.udtChannel(zch).PidExtParaUnit4) Then
                    msgtemp(i) = mMsgCreateStr("PID PARA UNIT4", gGetString(udt1.udtChannel(ich).PidExtParaUnit4), gGetString(udt2.udtChannel(zch).PidExtParaUnit4))
                    i = i + 1
                End If
            End If

            '88 OutMode
            If gCompareChk_12_Pid(88) = True Then
                If udt1.udtChannel(ich).PidOutMode <> udt2.udtChannel(zch).PidOutMode Then
                    msgtemp(i) = mMsgCreateSht("PID Out Mode", udt1.udtChannel(ich).PidOutMode, udt2.udtChannel(zch).PidOutMode)
                    i = i + 1
                End If
            End If
            '89 CasMode
            If gCompareChk_12_Pid(89) = True Then
                If udt1.udtChannel(ich).PidCasMode <> udt2.udtChannel(zch).PidCasMode Then
                    msgtemp(i) = mMsgCreateSht("PID Cas Mode", udt1.udtChannel(ich).PidCasMode, udt2.udtChannel(zch).PidCasMode)
                    i = i + 1
                End If
            End If
            '90 SpTracking
            If gCompareChk_12_Pid(90) = True Then
                If udt1.udtChannel(ich).PidSpTracking <> udt2.udtChannel(zch).PidSpTracking Then
                    msgtemp(i) = mMsgCreateSht("PID Sp Tracking", udt1.udtChannel(ich).PidSpTracking, udt2.udtChannel(zch).PidSpTracking)
                    i = i + 1
                End If
            End If



            '変更項目数を返す
            mCompareSetChannelPidDisp = i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "出力チャンネル設定OK"

    Friend Function mCompareSetCHOutPut(ByVal udt1 As gTypSetChOutput, _
                                        ByVal udt2 As gTypSetChOutput) As Integer

        Dim CHIDChange As Integer
        Dim CHViewFlg As Boolean = True
        Dim ix As Integer = 1

        Try

            msgSYStemp(0) = "■■■■　CH OUTPUT SETTING　■■■■"

            For i As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut)

                CHViewFlg = True

                CHIDChange = gConvChIdToChNoComp(udt1.udtCHOutPut(i).shtChid)

                ''SYSTEM No.
                If udt1.udtCHOutPut(i).shtSysno <> udt2.udtCHOutPut(i).shtSysno Then
                    msgSYStemp(ix) = mMsgCreateTbl("SYSNo", udt1.udtCHOutPut(i).shtSysno, udt2.udtCHOutPut(i).shtSysno, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''CH ID 又は 論理出力 ID
                If udt1.udtCHOutPut(i).shtChid <> udt2.udtCHOutPut(i).shtChid Then
                    msgSYStemp(ix) = mMsgCreateTbl("CHID", udt1.udtCHOutPut(i).shtChid, udt2.udtCHOutPut(i).shtChid, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''CHデータ、論理出力チャネルデータ
                If udt1.udtCHOutPut(i).bytType <> udt2.udtCHOutPut(i).bytType Then
                    msgSYStemp(ix) = mMsgCreateTbl("TYPE", udt1.udtCHOutPut(i).bytType, udt2.udtCHOutPut(i).bytType, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''ステータス種類
                If udt1.udtCHOutPut(i).bytStatus <> udt2.udtCHOutPut(i).bytStatus Then
                    msgSYStemp(ix) = mMsgCreateTbl("STATUS", udt1.udtCHOutPut(i).bytStatus, udt2.udtCHOutPut(i).bytStatus, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''Output Movement マスクデータ（ビットパターン）
                If udt1.udtCHOutPut(i).shtMask <> udt2.udtCHOutPut(i).shtMask Then
                    msgSYStemp(ix) = mMsgCreateTbl("MASK", udt1.udtCHOutPut(i).shtMask, udt2.udtCHOutPut(i).shtMask, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''CH OUT Type Setup
                If udt1.udtCHOutPut(i).bytOutput <> udt2.udtCHOutPut(i).bytOutput Then
                    msgSYStemp(ix) = mMsgCreateTbl("OUTPUT", udt1.udtCHOutPut(i).bytOutput, udt2.udtCHOutPut(i).bytOutput, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''FU 番号
                If udt1.udtCHOutPut(i).bytFuno <> udt2.udtCHOutPut(i).bytFuno Then
                    msgSYStemp(ix) = mMsgCreateTbl("FUNo", udt1.udtCHOutPut(i).bytFuno, udt2.udtCHOutPut(i).bytFuno, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''FU ポート番号
                If udt1.udtCHOutPut(i).bytPortno <> udt2.udtCHOutPut(i).bytPortno Then
                    msgSYStemp(ix) = mMsgCreateTbl("PORTNo", udt1.udtCHOutPut(i).bytPortno, udt2.udtCHOutPut(i).bytPortno, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''FU 計測点番号
                If udt1.udtCHOutPut(i).bytPin <> udt2.udtCHOutPut(i).bytPin Then
                    msgSYStemp(ix) = mMsgCreateTbl("PINNo", udt1.udtCHOutPut(i).bytPin, udt2.udtCHOutPut(i).bytPin, "CHNo.", CHIDChange, "TBL", i, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "出力チャンネル設定：FUアドレスベースで比較版"
    'Ver 2.0.0.2 比較機能追加
    Friend Function mCompareSetCHOutPut_FU(ByVal udt1 As gTypSetChOutput, _
                                        ByVal udt2 As gTypSetChOutput) As Integer

        Dim CHIDChange As Integer
        Dim CHViewFlg As Boolean = True
        Dim ix As Integer = 1
        Dim ixBKUP As Integer = 0

        Dim sNo As Integer = 0          '比較対象レコード番号 
        Dim bytFuno As Byte = 0         'FU 番号
        Dim bytPortno As Byte = 0       'FU ポート番号
        Dim bytPin As Byte = 0          'FU 計測点番号
        Dim bytFuno_B As Byte = 0       'FU 番号
        Dim bytPortno_B As Byte = 0     'FU ポート番号
        Dim bytPin_B As Byte = 0        'FU 計測点番号

        Dim bSAMErec As Boolean = False '比較対象レコードが存在すればTrue

        Dim strOldDisp As String = ""
        Dim strNewDisp As String = ""

        'Ver2.0.5.9 変更が1点の場合に変化無しとなってしまう不具合修正
        Dim blAddNone As Boolean = False
        Dim blDelNone As Boolean = False
        Dim blCmpNone As Boolean = False


        Try
            'Ver2.0.4.7 旧でも取得させる
            Dim aryCHLISTall As New ArrayList
            With mudtTarget.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    aryCHLISTall.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                Next i
            End With

            Dim aryCHLISTallold As New ArrayList
            With mudtSource.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    aryCHLISTallold.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                Next i
            End With



            msgSYStemp(0) = "■■■■　CH OUTPUT SETTING　■■■■"

            'Ver2.0.3.1 追加、削除検出はここで行う
            'ADD
            msgSYStemp(ix) = "■CH OUTPUT ADD"
            ix = ix + 1
            For i As Integer = LBound(udt2.udtCHOutPut) To UBound(udt2.udtCHOutPut)
                CHViewFlg = True


                'FU_Adress格納
                bytFuno = udt2.udtCHOutPut(i).bytFuno
                bytPortno = udt2.udtCHOutPut(i).bytPortno
                bytPin = udt2.udtCHOutPut(i).bytPin

                'FU_Adressベースで比較対象レコードが存在するかチェック
                bSAMErec = False
                'ALL_0 or ALL_255は、比較対象外
                If (bytFuno = 0 And bytPortno = 0 And bytPin = 0) _
                    Or (bytFuno = 255 And bytPortno = 255 And bytPin = 255) Then
                    '>>>対象外
                Else
                    'FU_Adressベースで比較対象レコードを探す
                    For j As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut) Step 1
                        'FU_Adress格納
                        bytFuno_B = udt1.udtCHOutPut(j).bytFuno
                        bytPortno_B = udt1.udtCHOutPut(j).bytPortno
                        bytPin_B = udt1.udtCHOutPut(j).bytPin

                        '同一チャンネルが存在する
                        If bytFuno = bytFuno_B And bytPortno = bytPortno_B And bytPin = bytPin_B Then
                            bSAMErec = True
                            sNo = j
                            Exit For
                        End If
                    Next j

                    '比較対象が存在しなければADD
                    If bSAMErec = False Then
                        'Ver2.0.6.1 AND なのか ORなのか出力ﾒｯｾｰｼﾞに出す
                        Dim strType As String = ""
                        Select Case udt2.udtCHOutPut(i).bytType
                            Case 1
                                strType = "OR Data"
                            Case 2
                                strType = "AND Data"
                            Case Else
                                strType = "CH Data"
                        End Select
                        msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno.ToString, _
                                                               bytPortno.ToString, _
                                                               bytPin.ToString, _
                                                               "ADD CH", strType, udt2.udtCHOutPut(i).shtChid, "FU.", "TBL", i, CHViewFlg)
                        ix = ix + 1
                        CHViewFlg = False
                    End If
                End If
            Next i
            If ix = 2 Then
                'ADD CHが無い場合「NONE」出力
                msgSYStemp(ix) = "NONE"
                ix = ix + 1
                blAddNone = True    'Ver2.0.5.9 変更が1点の場合に変化無しとなってしまう不具合修正
            End If

            'DEL
            msgSYStemp(ix) = "■CH OUTPUT DEL"
            ix = ix + 1
            ixBKUP = ix
            For i As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut)

                CHViewFlg = True

                'FU_Adress格納
                bytFuno = udt1.udtCHOutPut(i).bytFuno
                bytPortno = udt1.udtCHOutPut(i).bytPortno
                bytPin = udt1.udtCHOutPut(i).bytPin

                'FU_Adressベースで比較対象レコードが存在するかチェック
                bSAMErec = False
                'ALL_0 or ALL_255は、比較対象外
                If (bytFuno = 0 And bytPortno = 0 And bytPin = 0) _
                    Or (bytFuno = 255 And bytPortno = 255 And bytPin = 255) Then
                    '>>>対象外
                Else
                    'FU_Adressベースで比較対象レコードを探す
                    For j As Integer = LBound(udt2.udtCHOutPut) To UBound(udt2.udtCHOutPut) Step 1
                        'FU_Adress格納
                        bytFuno_B = udt2.udtCHOutPut(j).bytFuno
                        bytPortno_B = udt2.udtCHOutPut(j).bytPortno
                        bytPin_B = udt2.udtCHOutPut(j).bytPin

                        '同一チャンネルが存在する
                        If bytFuno = bytFuno_B And bytPortno = bytPortno_B And bytPin = bytPin_B Then
                            bSAMErec = True
                            sNo = j
                            Exit For
                        End If
                    Next j

                    '比較対象が存在しなければDEL
                    If bSAMErec = False Then
                        'Ver2.0.6.1 AND なのか ORなのか出力ﾒｯｾｰｼﾞに出す
                        Dim strTypeDel As String = ""
                        Select Case udt1.udtCHOutPut(i).bytType
                            Case 1
                                strTypeDel = "OR Data"
                            Case 2
                                strTypeDel = "AND Data"
                            Case Else
                                strTypeDel = "CH Data"
                        End Select
                        msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno.ToString, _
                                                               bytPortno.ToString, _
                                                               bytPin.ToString, _
                                                               "DEL CH", strTypeDel, udt1.udtCHOutPut(i).shtChid, "FU.", "TBL", i, CHViewFlg)
                        ix = ix + 1
                        CHViewFlg = False
                    End If
                End If
            Next
            If ix = ixBKUP Then
                'DEL CHが無い場合「NONE」出力
                msgSYStemp(ix) = "NONE"
                ix = ix + 1
                blDelNone = True    'Ver2.0.5.9 変更が1点の場合に変化無しとなってしまう不具合修正
            End If


            msgSYStemp(ix) = "■CH OUTPUT COMP"
            ix = ix + 1
            Dim iCmp As Integer = ix
            For i As Integer = LBound(udt2.udtCHOutPut) To UBound(udt2.udtCHOutPut)

                CHViewFlg = True

                'FU_Adress格納
                bytFuno = udt2.udtCHOutPut(i).bytFuno
                bytPortno = udt2.udtCHOutPut(i).bytPortno
                bytPin = udt2.udtCHOutPut(i).bytPin


                CHIDChange = gConvChIdToChNoComp(udt2.udtCHOutPut(i).shtChid)

                'FU_Adressベースで比較対象レコードが存在するかチェック
                bSAMErec = False
                'ALL_0 or ALL_255は、比較対象外
                If (bytFuno = 0 And bytPortno = 0 And bytPin = 0) _
                    Or (bytFuno = 255 And bytPortno = 255 And bytPin = 255) Then
                    '>>>対象外
                Else
                    'FU_Adressベースで比較対象レコードを探す
                    For j As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut) Step 1
                        'FU_Adress格納
                        bytFuno_B = udt1.udtCHOutPut(j).bytFuno
                        bytPortno_B = udt1.udtCHOutPut(j).bytPortno
                        bytPin_B = udt1.udtCHOutPut(j).bytPin

                        '同一チャンネルが存在する場合は内容比較処理へ
                        If bytFuno = bytFuno_B And bytPortno = bytPortno_B And bytPin = bytPin_B Then
                            bSAMErec = True
                            sNo = j
                            Exit For
                        End If
                    Next j

                    '比較対象が存在すれば比較へ
                    If bSAMErec = True Then
                        '詳細比較
                        ''SYSTEM No.
                        If udt1.udtCHOutPut(sNo).shtSysno <> udt2.udtCHOutPut(i).shtSysno Then
                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "SYSNo", udt1.udtCHOutPut(sNo).shtSysno, udt2.udtCHOutPut(i).shtSysno, "FU.", "TBL", i, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False
                        End If

                        ''CH ID 又は 論理出力 ID
                        If udt1.udtCHOutPut(sNo).shtChid <> udt2.udtCHOutPut(i).shtChid Then
                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "CHID", udt1.udtCHOutPut(sNo).shtChid, udt2.udtCHOutPut(i).shtChid, "FU.", "TBL", i, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False
                        End If

                        ''CHデータ、論理出力チャネルデータ
                        If udt1.udtCHOutPut(sNo).bytType <> udt2.udtCHOutPut(i).bytType Then
                            'Ver2.0.4.9 コードを名称化
                            strOldDisp = ""
                            Select Case udt1.udtCHOutPut(sNo).bytType
                                Case 1
                                    strOldDisp = "OR Data"
                                Case 2
                                    strOldDisp = "AND Data"
                                Case Else
                                    strOldDisp = "CH Data"
                            End Select
                            strNewDisp = ""
                            Select Case udt2.udtCHOutPut(i).bytType
                                Case 1
                                    strNewDisp = "OR Data"
                                Case 2
                                    strNewDisp = "AND Data"
                                Case Else
                                    strNewDisp = "CH Data"
                            End Select

                            'msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                            '                                   bytPortno_B.ToString, _
                            '                                   bytPin_B.ToString, _
                            '                                   "TYPE", udt1.udtCHOutPut(sNo).bytType, udt2.udtCHOutPut(i).bytType, "FU.", "TBL", i, CHViewFlg)
                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "TYPE", strOldDisp, strNewDisp, "FU.", "TBL", i, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False
                        End If

                        ''ステータス種類
                        If udt1.udtCHOutPut(sNo).bytStatus <> udt2.udtCHOutPut(i).bytStatus Then
                            'Ver2.0.4.9 コードを名称化
                            strOldDisp = ""
                            Select Case udt1.udtCHOutPut(sNo).bytStatus
                                Case 1
                                    strOldDisp = "MOTOR/COMPOSITE"
                                Case 2
                                    strOldDisp = "ON/OFF"
                                Case Else
                                    strOldDisp = "ALARM"
                            End Select
                            strNewDisp = ""
                            Select Case udt2.udtCHOutPut(i).bytStatus
                                Case 1
                                    strNewDisp = "MOTOR/COMPOSITE"
                                Case 2
                                    strNewDisp = "ON/OFF"
                                Case Else
                                    strNewDisp = "ALARM"
                            End Select

                            'msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                            '                                   bytPortno_B.ToString, _
                            '                                   bytPin_B.ToString, _
                            '                                   "STATUS", udt1.udtCHOutPut(sNo).bytStatus, udt2.udtCHOutPut(i).bytStatus, "FU.", "TBL", i, CHViewFlg)
                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                              bytPortno_B.ToString, _
                                                              bytPin_B.ToString, _
                                                              "STATUS", strOldDisp, strNewDisp, "FU.", "TBL", i, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False
                        End If

                        ''Output Movement マスクデータ（ビットパターン）
                        If udt1.udtCHOutPut(sNo).shtMask <> udt2.udtCHOutPut(i).shtMask Then
                            'Ver2.0.2.9 MASKは、複数変更になる可能性があるため大変更
                            Dim intS As Integer = aryCHLISTall.IndexOf(CHIDChange.ToString)
                            If intS > 0 Then
                                'CHnoからCHtype取得
                                Dim intMaskCHNo1 As Integer
                                Dim intMaskCHNo2 As Integer
                                'OLD側MASK文言生成
                                intMaskCHNo1 = gConvChIdToChNoComp(udt1.udtCHOutPut(sNo).shtChid)
                                intMaskCHNo2 = gConvChIdToChNoCompTarget(udt2.udtCHOutPut(i).shtChid)

                                If intMaskCHNo1 <= 0 Or intMaskCHNo2 <= 0 Then
                                    'Ver2.0.6.3 CH種別がor,andの場合、MASK比較は詳細を出さない(出せない)
                                    msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                              bytPortno_B.ToString, _
                                                              bytPin_B.ToString, _
                                                              "MASK", udt1.udtCHOutPut(sNo).shtMask, udt2.udtCHOutPut(i).shtMask, "FU.", "TBL", i, CHViewFlg)
                                    ix = ix + 1
                                    CHViewFlg = False
                                Else
                                    Dim intS1 As Integer = aryCHLISTallold.IndexOf(intMaskCHNo1.ToString)
                                    Dim intS2 As Integer = aryCHLISTall.IndexOf(intMaskCHNo2.ToString)

                                    Dim intChType1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtChType
                                    Dim intDataType1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtData
                                    Dim intStatus1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtStatus
                                    Dim strStatus1 As String = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.strStatus
                                    Dim intPinNo1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtPinNo
                                    Dim intChType2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtChType
                                    Dim intDataType2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtData
                                    Dim intStatus2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtStatus
                                    Dim strStatus2 As String = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.strStatus
                                    Dim intPinNo2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtPinNo

                                    Dim strOld As String = fnMaskStringFull(intChType1, intDataType1, intStatus1, strStatus1, intPinNo1, _
                                                                            udt1.udtCHOutPut(sNo).bytStatus, udt1.udtCHOutPut(sNo).shtMask)
                                    Dim strNew As String = fnMaskStringFull(intChType2, intDataType2, intStatus2, strStatus2, intPinNo2, _
                                                                            udt2.udtCHOutPut(i).bytStatus, udt2.udtCHOutPut(i).shtMask)

                                    msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                                       bytPortno_B.ToString, _
                                                                       bytPin_B.ToString, _
                                                                       "MASK", strOld, strNew, "(" & CHIDChange & ")" & "FU.", "TBL", i + 1, CHViewFlg)
                                    ix = ix + 1
                                    CHViewFlg = False
                                End If
                            End If
                            'msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                            '                                   bytPortno_B.ToString, _
                            '                                   bytPin_B.ToString, _
                            '                                   "MASK", udt1.udtCHOutPut(sNo).shtMask, udt2.udtCHOutPut(i).shtMask, "FU.", "TBL", i, CHViewFlg)
                            'ix = ix + 1
                            'CHViewFlg = False
                        End If

                        ''CH OUT Type Setup
                        If udt1.udtCHOutPut(sNo).bytOutput <> udt2.udtCHOutPut(i).bytOutput Then
                            Call gSetComboBox(cmbChOutType, gEnmComboType.ctChOutputDoChOutType)
                            cmbChOutType.SelectedValue = udt1.udtCHOutPut(sNo).bytOutput
                            Dim strOldVal As String = cmbChOutType.Text
                            cmbChOutType.SelectedValue = udt2.udtCHOutPut(i).bytOutput
                            Dim strNewVal As String = cmbChOutType.Text
                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "CH OUT TYPE", strOldVal, strNewVal, "FU.", "TBL", i, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False
                        End If

                    End If
                End If
            Next

            '比較が何もない場合は比較部ﾀｲﾄﾙを削る
            If iCmp = ix Then
                msgSYStemp(ix - 1) = ""
                blCmpNone = True    'Ver2.0.5.9 変更が1点の場合に変化無しとなってしまう不具合修正
            End If


            '何も変更がない場合は表示しない
            'Ver2.0.5.9 変更が1点の場合に変化無しとなってしまう不具合修正
            'If ix <> 6 Then
            If blAddNone <> True Or blDelNone <> True Or blCmpNone <> True Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                For z As Integer = 0 To ix Step 1
                    msgSYStemp(z) = ""
                Next z
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    ''Ver 2.0.2.8 CH追加、削除を検出
    'Friend Function mCompareSetCHOutPut_FU_DELADD(ByVal udt1 As gTypSetChOutput, _
    '                                    ByVal udt2 As gTypSetChOutput) As Integer
    '    Dim CHViewFlg As Boolean = True
    '    Dim ix As Integer = 1
    '    Dim ixBKUP As Integer = 0

    '    Dim sNo As Integer = 0          '比較対象レコード番号 
    '    Dim bytFuno As Byte = 0         'FU 番号
    '    Dim bytPortno As Byte = 0       'FU ポート番号
    '    Dim bytPin As Byte = 0          'FU 計測点番号
    '    Dim bytFuno_B As Byte = 0       'FU 番号
    '    Dim bytPortno_B As Byte = 0     'FU ポート番号
    '    Dim bytPin_B As Byte = 0        'FU 計測点番号

    '    Dim bSAMErec As Boolean = False '比較対象レコードが存在すればTrue


    '    Try

    '        msgSYStemp(0) = "■■■■　CH OUTPUT ADD DEL　■■■■"

    '        'ADD
    '        msgSYStemp(ix) = "■CH OUTPUT ADD"
    '        ix = ix + 1
    '        For i As Integer = LBound(udt2.udtCHOutPut) To UBound(udt2.udtCHOutPut)
    '            CHViewFlg = True

    '            'FU_Adress格納
    '            bytFuno = udt2.udtCHOutPut(i).bytFuno
    '            bytPortno = udt2.udtCHOutPut(i).bytPortno
    '            bytPin = udt2.udtCHOutPut(i).bytPin

    '            'FU_Adressベースで比較対象レコードが存在するかチェック
    '            bSAMErec = False
    '            'ALL_0 or ALL_255は、比較対象外
    '            If (bytFuno = 0 And bytPortno = 0 And bytPin = 0) _
    '                Or (bytFuno = 255 And bytPortno = 255 And bytPin = 255) Then
    '                '>>>対象外
    '            Else
    '                'FU_Adressベースで比較対象レコードを探す
    '                For j As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut) Step 1
    '                    'FU_Adress格納
    '                    bytFuno_B = udt1.udtCHOutPut(j).bytFuno
    '                    bytPortno_B = udt1.udtCHOutPut(j).bytPortno
    '                    bytPin_B = udt1.udtCHOutPut(j).bytPin

    '                    '同一チャンネルが存在する
    '                    If bytFuno = bytFuno_B And bytPortno = bytPortno_B And bytPin = bytPin_B Then
    '                        bSAMErec = True
    '                        sNo = j
    '                        Exit For
    '                    End If
    '                Next j

    '                '比較対象が存在しなければADD
    '                If bSAMErec = False Then
    '                    msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno.ToString, _
    '                                                           bytPortno.ToString, _
    '                                                           bytPin.ToString, _
    '                                                           "ADD CH", "", udt2.udtCHOutPut(i).shtChid, "FU.", "TBL", i, CHViewFlg)
    '                    ix = ix + 1
    '                    CHViewFlg = False
    '                End If
    '            End If
    '        Next i
    '        If ix = 2 Then
    '            'ADD CHが無い場合「NONE」出力
    '            msgSYStemp(ix) = "NONE"
    '            ix = ix + 1
    '        End If

    '        'DEL
    '        msgSYStemp(ix) = "■CH OUTPUT DEL"
    '        ix = ix + 1
    '        ixBKUP = ix
    '        For i As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut)

    '            CHViewFlg = True

    '            'FU_Adress格納
    '            bytFuno = udt1.udtCHOutPut(i).bytFuno
    '            bytPortno = udt1.udtCHOutPut(i).bytPortno
    '            bytPin = udt1.udtCHOutPut(i).bytPin

    '            'FU_Adressベースで比較対象レコードが存在するかチェック
    '            bSAMErec = False
    '            'ALL_0 or ALL_255は、比較対象外
    '            If (bytFuno = 0 And bytPortno = 0 And bytPin = 0) _
    '                Or (bytFuno = 255 And bytPortno = 255 And bytPin = 255) Then
    '                '>>>対象外
    '            Else
    '                'FU_Adressベースで比較対象レコードを探す
    '                For j As Integer = LBound(udt2.udtCHOutPut) To UBound(udt2.udtCHOutPut) Step 1
    '                    'FU_Adress格納
    '                    bytFuno_B = udt2.udtCHOutPut(j).bytFuno
    '                    bytPortno_B = udt2.udtCHOutPut(j).bytPortno
    '                    bytPin_B = udt2.udtCHOutPut(j).bytPin

    '                    '同一チャンネルが存在する
    '                    If bytFuno = bytFuno_B And bytPortno = bytPortno_B And bytPin = bytPin_B Then
    '                        bSAMErec = True
    '                        sNo = j
    '                        Exit For
    '                    End If
    '                Next j

    '                '比較対象が存在しなければDEL
    '                If bSAMErec = False Then
    '                    msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno.ToString, _
    '                                                           bytPortno.ToString, _
    '                                                           bytPin.ToString, _
    '                                                           "DEL CH", udt1.udtCHOutPut(i).shtChid, "", "FU.", "TBL", i, CHViewFlg)
    '                    ix = ix + 1
    '                    CHViewFlg = False
    '                End If
    '            End If
    '        Next
    '        If ix = ixBKUP Then
    '            'DEL CHが無い場合「NONE」出力
    '            msgSYStemp(ix) = "NONE"
    '            ix = ix + 1
    '        End If

    '        Call mMsgSysGrid(ix)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Function

#End Region

#Region "論理出力設定OK"

    Friend Function mCompareSetCHAndOr(ByVal udt1 As gTypSetChAndOr, _
                                       ByVal udt2 As gTypSetChAndOr) As Integer

        Dim CHIDChange As Integer
        Dim CHViewFlg As Boolean = True
        Dim ix As Integer = 1

        Dim bytFuno_B As Byte = 0       'FU 番号
        Dim bytPortno_B As Byte = 0     'FU ポート番号
        Dim bytPin_B As Byte = 0        'FU 計測点番号


        'Ver2.0.0.8 TBL番号は、出力テーブルのCHIDと一致させるためi+1とする

        Try

            'Ver2.0.2.9 CHlist Arrayを作成してそこからFUｱﾄﾞﾚｽを取得できるようにする
            Dim aryCHLIST As New ArrayList
            With mudtTarget.SetChOutput
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtCHOutPut)
                    aryCHLIST.Add(.udtCHOutPut(i).shtChid.ToString)
                Next i
            End With
            '配列の番号とaryCHLISTの番号は一致しているため、CHnoを指定すれば
            '配列の番号がわかり、そこから各種値が取得できる。
            '例）Dim intA As Integer = aryCHLIST.IndexOf("207")

            'Ver2.0.2.9 計測点一覧もAray化しておく
            Dim aryCHLISTall As New ArrayList
            With mudtTarget.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    aryCHLISTall.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                Next i
            End With

            'OLD版も取得しておく
            Dim aryCHLISTold As New ArrayList
            With mudtSource.SetChOutput
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtCHOutPut)
                    aryCHLISTold.Add(.udtCHOutPut(i).shtChid.ToString)
                Next i
            End With
            '配列の番号とaryCHLISTの番号は一致しているため、CHnoを指定すれば
            '配列の番号がわかり、そこから各種値が取得できる。
            '例）Dim intA As Integer = aryCHLIST.IndexOf("207")

            'Ver2.0.2.9 計測点一覧もAray化しておく
            Dim aryCHLISTallold As New ArrayList
            With mudtSource.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    aryCHLISTallold.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                Next i
            End With



            msgSYStemp(0) = "■■■■　CH AND OR SETTING　■■■■"

            For i As Integer = LBound(udt1.udtCHOut) To UBound(udt1.udtCHOut)

                For j As Integer = LBound(udt1.udtCHOut(i).udtCHAndOr) To UBound(udt1.udtCHOut(i).udtCHAndOr)

                    CHViewFlg = True

                    CHIDChange = gConvChIdToChNoComp(udt1.udtCHOut(i).udtCHAndOr(j).shtChid)
                    
                    'Ver2.0.6.1 ﾛｸﾞﾒｯｾｰｼﾞにCHIDChangeを排除(常に０のため)

                    'Ver2.0.2.9 CHnoからFUadressGet
                    Dim intA As Integer = -1
                    intA = aryCHLIST.IndexOf((i + 1).ToString)
                    If intA < 0 Then
                        bytFuno_B = 255
                        bytPortno_B = 255
                        bytPin_B = 255
                    Else
                        With gudt.SetChOutput.udtCHOutPut(intA)
                            If .bytFuno > 255 Then
                                bytFuno_B = 255
                            Else
                                bytFuno_B = .bytFuno
                            End If
                            If .bytPortno > 255 Then
                                bytPortno_B = 255
                            Else
                                bytPortno_B = .bytPortno
                            End If
                            If .bytPin > 255 Then
                                bytPin_B = 255
                            Else
                                bytPin_B = .bytPin
                            End If
                        End With
                    End If
                    


                    ''SYSTEM No.
                    If udt1.udtCHOut(i).udtCHAndOr(j).shtSysno <> udt2.udtCHOut(i).udtCHAndOr(j).shtSysno Then
                        msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "SYSNo", udt1.udtCHOut(i).udtCHAndOr(j).shtSysno, udt2.udtCHOut(i).udtCHAndOr(j).shtSysno, "" & "FU.", "TBL", i + 1, CHViewFlg)
                        ix = ix + 1
                        CHViewFlg = False
                    End If

                    ''CH ID
                    If udt1.udtCHOut(i).udtCHAndOr(j).shtChid <> udt2.udtCHOut(i).udtCHAndOr(j).shtChid Then
                        msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "CHID", udt1.udtCHOut(i).udtCHAndOr(j).shtChid, udt2.udtCHOut(i).udtCHAndOr(j).shtChid, "" & "FU.", "TBL", i + 1, CHViewFlg)
                        ix = ix + 1
                        CHViewFlg = False
                    End If

                    ''ステータス種類
                    If udt1.udtCHOut(i).udtCHAndOr(j).bytStatus <> udt2.udtCHOut(i).udtCHAndOr(j).bytStatus Then
                        msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                               bytPortno_B.ToString, _
                                                               bytPin_B.ToString, _
                                                               "STATUS", udt1.udtCHOut(i).udtCHAndOr(j).bytStatus, udt2.udtCHOut(i).udtCHAndOr(j).bytStatus, "" & "FU.", "TBL", i + 1, CHViewFlg)
                        ix = ix + 1
                        CHViewFlg = False
                    End If

                    ''Output Movement マスクデータ（ビットパターン）
                    If udt1.udtCHOut(i).udtCHAndOr(j).shtMask <> udt2.udtCHOut(i).udtCHAndOr(j).shtMask Then
                        'Ver2.0.2.9 MASKは、複数変更になる可能性があるため大変更
                        '
                        Dim intMaskCHNo1 As Integer
                        Dim intMaskCHNo2 As Integer
                        'OLD側MASK文言生成
                        intMaskCHNo1 = gConvChIdToChNoComp(udt1.udtCHOut(i).udtCHAndOr(j).shtChid)
                        intMaskCHNo2 = gConvChIdToChNoCompTarget(udt2.udtCHOut(i).udtCHAndOr(j).shtChid)

                        If intMaskCHNo1 <= 0 Or intMaskCHNo2 <= 0 Then      '' ver2.0.8.B  2018.10.19
                            msgSYStemp(ix) = ""     ''"Nothing"の場合エラーが出るため、""を書き込む
                            ix = ix + 1
                            CHViewFlg = False
                        Else
                            Dim intS1 As Integer = aryCHLISTallold.IndexOf(intMaskCHNo1.ToString)
                            Dim intS2 As Integer = aryCHLISTall.IndexOf(intMaskCHNo2.ToString)

                            Dim intChType1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtChType
                            Dim intDataType1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtData
                            Dim intStatus1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtStatus
                            Dim strStatus1 As String = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.strStatus
                            Dim intPinNo1 As Integer = mudtSource.SetChInfo.udtChannel(intS1).udtChCommon.shtPinNo
                            '
                            Dim intChType2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtChType
                            Dim intDataType2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtData
                            Dim intStatus2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtStatus
                            Dim strStatus2 As String = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.strStatus
                            Dim intPinNo2 As Integer = mudtTarget.SetChInfo.udtChannel(intS2).udtChCommon.shtPinNo


                            Dim strOld As String = fnMaskStringFull(intChType1, intDataType1, intStatus1, strStatus1, intPinNo1, _
                                            udt1.udtCHOut(i).udtCHAndOr(j).bytStatus, udt1.udtCHOut(i).udtCHAndOr(j).shtMask)
                            Dim strnew As String = fnMaskStringFull(intChType2, intDataType2, intStatus2, strStatus2, intPinNo2, _
                                            udt2.udtCHOut(i).udtCHAndOr(j).bytStatus, udt2.udtCHOut(i).udtCHAndOr(j).shtMask)

                            msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                                                                       bytPortno_B.ToString, _
                                                                       bytPin_B.ToString, _
                                                                       "MASK", strOld, strnew, "" & "FU.", "TBL", i + 1, CHViewFlg)
                            ix = ix + 1
                            CHViewFlg = False


                            'msgSYStemp(ix) = mMsgCreateTbl_OUT(bytFuno_B.ToString, _
                            '                                       bytPortno_B.ToString, _
                            '                                       bytPin_B.ToString, _
                            '                                       "MASK", udt1.udtCHOut(i).udtCHAndOr(j).shtMask, udt2.udtCHOut(i).udtCHAndOr(j).shtMask, "(" & CHIDChange & ")" & "FU.", "TBL", i + 1, CHViewFlg)
                            'ix = ix + 1
                            'CHViewFlg = False

                        End If

                        
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    'Ver2.0.3.8 (ALARM時)MASK文言が戻る関数
    Private Function fnMaskString(pintType As Integer, pintDataType As Integer, pintMask As Integer) As String
        Dim strRet As String = "ALL OFF"

        Dim intType As Integer = pintType
        Dim intDataType As Integer = pintDataType
        'MASK対応配列をｾｯﾄ
        Dim haiBit() As Integer = Nothing
        Dim haiBitStr() As String = Nothing
        Select Case intType
            Case gCstCodeChTypeAnalog
                haiBit = mcstBitAnalog
                haiBitStr = mcstStsAnalog
            Case gCstCodeChTypeDigital
                haiBit = mcstBitDigital
                haiBitStr = mcstStsDigital
            Case gCstCodeChTypeMotor
                haiBit = mcstBitMotor
                haiBitStr = mcstStsMotor
            Case gCstCodeChTypeValve
                Select Case intDataType
                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveExt, gCstCodeChDataTypeValveJacom55
                        haiBit = mcstBitValveDiDo
                        haiBitStr = mcstStsValveDiDo
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                        haiBit = mcstBitValveAiDo
                        haiBitStr = mcstStsValveAiDo
                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                        haiBit = mcstBitValveAiAo
                        haiBitStr = mcstStsValveAiAo
                End Select
            Case gCstCodeChTypeComposite
                haiBit = mcstBitComposite
                haiBitStr = mcstStsComposite
            Case gCstCodeChTypePulse
                haiBit = mcstBitPulse
                haiBitStr = mcstStsPulse
            Case Else
                Return strRet
        End Select

        strRet = ""
        For zz As Integer = 0 To UBound(haiBit) Step 1
            If haiBit(zz) <> -1 Then
                Dim blBit As Boolean = gBitCheck(pintMask, haiBit(zz))
                'MASKの項目名ｾｯﾄ
                Dim strBit As String = ""
                If blBit = False Then
                    strBit = "OFF"
                Else
                    strBit = haiBitStr(zz)
                End If

                strRet = strRet & strBit & " "
            End If
        Next zz


        Return strRet
    End Function

    'Ver2.0.4.9 MASK文言を戻す関数
    'CHinfo = pintType, pintDataType, pintStatus, pstrStatus,pintPinNo
    'MASK = pintMaskType As Integer, pintMask As Integer
    Private Function fnMaskStringFull(pintType As Integer, pintDataType As Integer, pintStatus As Integer, pstrStatus As String, pintPinNo As Integer, _
                                      pintMaskType As Integer, pintMask As Integer) As String
        Dim strRet As String = "ALL OFF"

        Try
            Dim strWk() As String

            'MaskTypeによって処理分岐
            Select Case pintMaskType
                Case 0
                    'ALARM
                    strRet = fnMaskString(pintType, pintDataType, pintMask)
                Case 1
                    'MOTOR,COMPOSITE
                    'モーター、コンポジット、バルブ
                    Select Case pintType
                        Case gCstCodeChTypeMotor
                            'モーター
                            If pintDataType = gCstCodeChDataTypeMotorDevice Or pintDataType = gCstCodeChDataTypeMotorDeviceJacom Or pintDataType = gCstCodeChDataTypeMotorRDevice Or _
                               pintDataType = gCstCodeChDataTypeMotorDeviceJacom55 Then
                                strRet = "RUN:" & IIf(pintMask <> 0, "ON", "OFF")
                            Else
                                'ステータス情報を獲得する
                                Dim strMot1() As String = Nothing
                                Dim strMot2() As String = Nothing
                                Call GetStatusMotor2(strMot1, strMot2, "StatusMotor")

                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                cmbStatus.SelectedValue = pintStatus
                                If cmbStatus.SelectedIndex >= 0 Then
                                    If pintStatus.ToString = gCstCodeChManualInputStatus.ToString Then
                                        '手入力
                                        strRet = pstrStatus & ":" & IIf(pintMask <> 0, "ON", "OFF")
                                    Else
                                        '「_」区切りの文字列取得
                                        Erase strWk
                                        If pintDataType >= gCstCodeChDataTypeMotorManRun And _
                                           pintDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                            strWk = strMot1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            If pintDataType >= gCstCodeChDataTypeMotorRManRun And _
                                                pintDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                                '正常Rは正常ステータス扱い
                                                strWk = strMot1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            Else
                                                strWk = strMot2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            End If
                                        End If
                                        strRet = ""
                                        For i As Integer = 0 To UBound(strWk)
                                            If strWk(i).Trim <> "" Then
                                                strRet = strRet & strWk(i) & ":" & IIf(gBitMotorCheck(pintMask, cmbStatus.SelectedIndex, i), "ON", "OFF") & " "
                                                ''2019.02.06 モーターのマスクを変更
                                            End If
                                        Next i
                                    End If
                                End If
                            End If
                        Case gCstCodeChTypeComposite
                            'コンポジット
                            'Pin数の数だけ「BIT-数値」でON,OFFを表記
                            strRet = ""
                            If pintPinNo > 0 Then
                                For i As Integer = 0 To pintPinNo - 1
                                    strRet = strRet & "BIT-" & i.ToString & ":" & IIf(gBitCheck(pintMask, i), "ON", "OFF") & " "
                                Next i
                            End If
                        Case gCstCodeChTypeValve
                            'バルブ
                            'DI/DOの場合のみ
                            If pintDataType = gCstCodeChDataTypeValveDI_DO Then
                                'Pin数の数だけ「BIT-数値」でON,OFFを表記
                                strRet = ""
                                If pintPinNo > 0 Then
                                    For i As Integer = 0 To pintPinNo - 1
                                        strRet = strRet & "BIT-" & i.ToString & ":" & IIf(gBitCheck(pintMask, i), "ON", "OFF") & " "
                                    Next i
                                End If
                            End If
                    End Select
                Case 2
                    'ON/OFF
                    'デジタルの場合はｽﾃｰﾀｽを利用。そうでない場合は単純なON,OFF
                    Select Case pintType
                        Case gCstCodeChTypeDigital
                            'デジタル
                            'ステータスに対してON,ONN
                            If pintStatus.ToString = gCstCodeChManualInputStatus.ToString Then
                                ''手入力値
                                If pstrStatus <> "" Then
                                    ''2つに分解する
                                    'Ver2.0.7.M 保安庁日本語対応
                                    If LenB(pstrStatus) > 8 Then
                                        strRet = ""
                                        'Ver2.0.7.M 保安庁日本語化対応
                                        strRet = strRet & MidB(pstrStatus, 0, 8).Trim & ":" & IIf(gBitCheck(pintMask, 6), "ON", "OFF") & " "
                                        strRet = strRet & MidB(pstrStatus, 8).Trim & ":" & IIf(gBitCheck(pintMask, 6), "OFF", "ON") 'MASK不具合 hori
                                    Else
                                        strRet = strRet & pstrStatus.Trim & ":" & IIf(pintMask <> 0, "ON", "OFF")
                                    End If
                                End If
                            Else
                                If pintStatus = 0 Then
                                    'デジタルのステータス種別コード = 0  <-- ステータスなし
                                    '何もしない＝ﾃﾞﾌｫﾙﾄ値の「ALL OFF」
                                Else
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                    cmbStatus.SelectedValue = pintStatus
                                    Dim strValue As String
                                    strWKCH = Nothing
                                    strValue = cmbStatus.Text
                                    strWk = strValue.Split("/")
                                    strRet = ""
                                    For i As Integer = 0 To UBound(strWk)
                                        If strWk(i).Trim <> "" Then
                                            'Ver2.0.7.C デジタルのMASKは、0なら最初がOFF,64なら後半がOFF
                                            'strRet = strRet & strWk(i) & ":" & IIf(gBitCheck(pintMask, i), "ON", "OFF") & " "
                                            If i = 0 Then
                                                strRet = strRet & strWk(i) & ":" & IIf(pintMask = 64, "ON", "OFF") & " "
                                            Else
                                                strRet = strRet & strWk(i) & ":" & IIf(pintMask = 0, "ON", "OFF") & " "
                                            End If

                                        End If
                                    Next i
                                End If
                            End If
                        Case Else
                            '単純なON,OFF
                            If pintMask <> 0 Then
                                strRet = "ON"
                            Else
                                strRet = "OFF"
                            End If
                    End Select
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRet
    End Function

#End Region

#Region "積算データ設定ファイルOK"

    Friend Function mCompareSetChRunHour(ByVal udt1 As gTypSetChRunHour, _
                                         ByVal udt2 As gTypSetChRunHour) As Integer

        Dim CHIDChange As Integer
        Dim CHViewFlg As Boolean = True
        Dim ix As Integer = 1

        Try

            msgSYStemp(0) = "■■■■　CH RUN HOUR SETTING　■■■■"

            For i As Integer = LBound(udt1.udtDetail) To UBound(udt1.udtDetail)

                CHViewFlg = True

                CHIDChange = gConvChIdToChNoComp(udt1.udtDetail(i).shtChid)

                ''計測CH System No
                If udt1.udtDetail(i).shtSysno <> udt2.udtDetail(i).shtSysno Then
                    msgSYStemp(ix) = mMsgCreateTbl("SYSNo", udt1.udtDetail(i).shtSysno, udt2.udtDetail(i).shtSysno, "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''計測CH CH ID
                If udt1.udtDetail(i).shtChid <> udt2.udtDetail(i).shtChid Then
                    msgSYStemp(ix) = mMsgCreateTbl("CHID", udt1.udtDetail(i).shtChid, udt2.udtDetail(i).shtChid, "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''積算トリガCH System No
                If udt1.udtDetail(i).shtTrgSysno <> udt2.udtDetail(i).shtTrgSysno Then
                    msgSYStemp(ix) = mMsgCreateTbl("TRG_SYSNo", udt1.udtDetail(i).shtTrgSysno, udt2.udtDetail(i).shtTrgSysno, "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''積算トリガCH CH ID
                If udt1.udtDetail(i).shtTrgChid <> udt2.udtDetail(i).shtTrgChid Then
                    msgSYStemp(ix) = mMsgCreateTbl("TRG_CHID", udt1.udtDetail(i).shtTrgChid, udt2.udtDetail(i).shtTrgChid, "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''ステータス
                If udt1.udtDetail(i).shtStatus <> udt2.udtDetail(i).shtStatus Then
                    msgSYStemp(ix) = mMsgCreateTbl("STATUS", "0x" & Hex(udt1.udtDetail(i).shtStatus).PadLeft(2, "0"), "0x" & Hex(udt2.udtDetail(i).shtStatus).PadLeft(2, "0"), "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

                ''マスクデータ（ビットパターン）
                If udt1.udtDetail(i).shtMask <> udt2.udtDetail(i).shtMask Then
                    msgSYStemp(ix) = mMsgCreateTbl("MASK", udt1.udtDetail(i).shtMask, udt2.udtDetail(i).shtMask, "CHNo.", CHIDChange, "TBL", i + 1, CHViewFlg)
                    ix = ix + 1
                    CHViewFlg = False
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "グループ設定OK"

    Friend Function mCompareSetGroupDisp(ByVal udt1 As gTypSetChGroupSet, _
                                         ByVal udt2 As gTypSetChGroupSet, _
                                         ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　GROUP SETTING（" & mGetMCEng(udtMC) & "）　■■■■"

            ''Draw No.
            If Not gCompareString(udt1.udtGroup.strDrawNo, udt2.udtGroup.strDrawNo) Then
                msgSYStemp(ix) = mMsgCreateSys("draw_no", gGetString(udt1.udtGroup.strDrawNo), gGetString(udt2.udtGroup.strDrawNo), "", "")
                ix = ix + 1
            End If

            ''コメント
            If Not gCompareString(udt1.udtGroup.strComment, udt2.udtGroup.strComment) Then
                msgSYStemp(ix) = mMsgCreateSys("comment", gGetString(udt1.udtGroup.strComment), gGetString(udt2.udtGroup.strComment), "", "")
                ix = ix + 1
            End If

            ''船番
            If Not gCompareString(udt1.udtGroup.strShipNo, udt2.udtGroup.strShipNo) Then
                msgSYStemp(ix) = mMsgCreateSys("ship_name", gGetString(udt1.udtGroup.strShipNo), gGetString(udt2.udtGroup.strShipNo), "", "")
                ix = ix + 1
            End If

            For i As Integer = LBound(udt1.udtGroup.udtGroupInfo) To UBound(udt1.udtGroup.udtGroupInfo)

                ''グループ番号
                If udt1.udtGroup.udtGroupInfo(i).shtGroupNo <> udt2.udtGroup.udtGroupInfo(i).shtGroupNo Then
                    msgSYStemp(ix) = mMsgCreateSys("Group_no", udt1.udtGroup.udtGroupInfo(i).shtGroupNo, udt2.udtGroup.udtGroupInfo(i).shtGroupNo, "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

                ''グループ名称 1行目
                If Not gCompareString(udt1.udtGroup.udtGroupInfo(i).strName1, udt2.udtGroup.udtGroupInfo(i).strName1) Then
                    msgSYStemp(ix) = mMsgCreateSys("Name1", gGetString(udt1.udtGroup.udtGroupInfo(i).strName1), gGetString(udt2.udtGroup.udtGroupInfo(i).strName1), "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

                ''グループ名称 2行目
                If Not gCompareString(udt1.udtGroup.udtGroupInfo(i).strName2, udt2.udtGroup.udtGroupInfo(i).strName2) Then
                    msgSYStemp(ix) = mMsgCreateSys("Name2", gGetString(udt1.udtGroup.udtGroupInfo(i).strName2), gGetString(udt2.udtGroup.udtGroupInfo(i).strName2), "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

                ''グループ名称 3行目
                If Not gCompareString(udt1.udtGroup.udtGroupInfo(i).strName3, udt2.udtGroup.udtGroupInfo(i).strName3) Then
                    msgSYStemp(ix) = mMsgCreateSys("Name3", gGetString(udt1.udtGroup.udtGroupInfo(i).strName3), gGetString(udt2.udtGroup.udtGroupInfo(i).strName3), "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

                ''カラー設定
                If udt1.udtGroup.udtGroupInfo(i).shtColor <> udt2.udtGroup.udtGroupInfo(i).shtColor Then
                    msgSYStemp(ix) = mMsgCreateSys("color", udt1.udtGroup.udtGroupInfo(i).shtColor, udt2.udtGroup.udtGroupInfo(i).shtColor, "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

                ''グループ表示位置インデックス
                If udt1.udtGroup.udtGroupInfo(i).shtDisplayPosition <> udt2.udtGroup.udtGroupInfo(i).shtDisplayPosition Then
                    msgSYStemp(ix) = mMsgCreateSys("display_position", udt1.udtGroup.udtGroupInfo(i).shtDisplayPosition, udt2.udtGroup.udtGroupInfo(i).shtDisplayPosition, "GROUP", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "リポーズ入力設定OK"

    Friend Function mCompareSetRepose(ByVal udt1 As gTypSetChGroupRepose, _
                                      ByVal udt2 As gTypSetChGroupRepose) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　REPOSE SETTING　■■■■"

            Dim CHIDChange As Integer

            For i As Integer = LBound(udt1.udtRepose) To UBound(udt1.udtRepose)

                CHIDChange = gConvChIdToChNoComp(udt1.udtRepose(i).shtChId)

                ''データ種別コード
                If udt1.udtRepose(i).shtData <> udt2.udtRepose(i).shtData Then
                    msgSYStemp(ix) = mMsgCreateSys2("DATA", udt1.udtRepose(i).shtData, udt2.udtRepose(i).shtData, "CHNo.", CHIDChange, "REPOSE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''リポーズ CH ID
                If udt1.udtRepose(i).shtChId <> udt2.udtRepose(i).shtChId Then
                    msgSYStemp(ix) = mMsgCreateSys2("REP_chid", udt1.udtRepose(i).shtChId, udt2.udtRepose(i).shtChId, "CHNo.", CHIDChange, "REPOSE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''リポーズ情報
                For j As Integer = LBound(udt1.udtRepose(i).udtReposeInf) To UBound(udt1.udtRepose(i).udtReposeInf)

                    CHIDChange = gConvChIdToChNoComp(udt1.udtRepose(i).udtReposeInf(j).shtChId)

                    ''CH ID
                    If udt1.udtRepose(i).udtReposeInf(j).shtChId <> udt2.udtRepose(i).udtReposeInf(j).shtChId Then
                        msgSYStemp(ix) = mMsgCreateSys2("CHID", udt1.udtRepose(i).shtChId, udt2.udtRepose(i).shtChId, "CHNo.", CHIDChange, "REPOSE", Str(i + 1), "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''マスク値
                    If udt1.udtRepose(i).udtReposeInf(j).bytMask <> udt2.udtRepose(i).udtReposeInf(j).bytMask Then
                        msgSYStemp(ix) = mMsgCreateSys2("MASK", udt1.udtRepose(i).udtReposeInf(j).bytMask, udt2.udtRepose(i).udtReposeInf(j).bytMask, "CHNo.", CHIDChange, "REPOSE", Str(i + 1), "INF", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "排ガス演算処理テーブルOK"

    Friend Function mCompareSetExhGus(ByVal udt1 As gTypSetChExhGus, _
                                      ByVal udt2 As gTypSetChExhGus) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　EXH GAS TABLE SETTING　■■■■"

            Dim CHIDChange As Integer

            For i As Integer = LBound(udt1.udtExhGusRec) To UBound(udt1.udtExhGusRec)

                CHIDChange = gConvChIdToChNoComp(udt1.udtExhGusRec(i).shtAveChid)

                ''シリンダ本数 
                If udt1.udtExhGusRec(i).shtNum <> udt2.udtExhGusRec(i).shtNum Then
                    msgSYStemp(ix) = mMsgCreateSys2("num", udt1.udtExhGusRec(i).shtNum, udt2.udtExhGusRec(i).shtNum, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''平均値出力CH  SYSTEM No.
                If udt1.udtExhGusRec(i).shtAveSysno <> udt2.udtExhGusRec(i).shtAveSysno Then
                    msgSYStemp(ix) = mMsgCreateSys2("ave_sysno", udt1.udtExhGusRec(i).shtAveSysno, udt2.udtExhGusRec(i).shtAveSysno, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''平均値出力CH　CH ID
                If udt1.udtExhGusRec(i).shtAveChid <> udt2.udtExhGusRec(i).shtAveChid Then
                    msgSYStemp(ix) = mMsgCreateSys2("ave_chid", udt1.udtExhGusRec(i).shtAveChid, udt2.udtExhGusRec(i).shtAveChid, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''リポーズCH    SYSTEM No.
                If udt1.udtExhGusRec(i).shtRepSysno <> udt2.udtExhGusRec(i).shtRepSysno Then
                    msgSYStemp(ix) = mMsgCreateSys2("rep_sysno", udt1.udtExhGusRec(i).shtRepSysno, udt2.udtExhGusRec(i).shtRepSysno, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''リポーズCH    CH ID
                If udt1.udtExhGusRec(i).shtRepChid <> udt2.udtExhGusRec(i).shtRepChid Then
                    msgSYStemp(ix) = mMsgCreateSys2("rep_chid", udt1.udtExhGusRec(i).shtRepChid, udt2.udtExhGusRec(i).shtRepChid, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''シリンダCH設定
                For j As Integer = LBound(udt1.udtExhGusRec(i).udtExhGusCyl) To UBound(udt1.udtExhGusRec(i).udtExhGusCyl)

                    ''SYSTEM No.
                    If udt1.udtExhGusRec(i).udtExhGusCyl(j).shtSysno <> udt2.udtExhGusRec(i).udtExhGusCyl(j).shtSysno Then
                        msgSYStemp(ix) = mMsgCreateSys2("sysno", udt1.udtExhGusRec(i).udtExhGusCyl(j).shtSysno, udt2.udtExhGusRec(i).udtExhGusCyl(j).shtSysno, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "CYL", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''CH ID
                    If udt1.udtExhGusRec(i).udtExhGusCyl(j).shtChid <> udt2.udtExhGusRec(i).udtExhGusCyl(j).shtChid Then
                        msgSYStemp(ix) = mMsgCreateSys2("CHID", udt1.udtExhGusRec(i).udtExhGusCyl(j).shtChid, udt2.udtExhGusRec(i).udtExhGusCyl(j).shtChid, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "CYL", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''偏差CH設定
                For j As Integer = LBound(udt1.udtExhGusRec(i).udtExhGusDev) To UBound(udt1.udtExhGusRec(i).udtExhGusDev)

                    ''SYSTEM No.
                    If udt1.udtExhGusRec(i).udtExhGusDev(j).shtSysno <> udt2.udtExhGusRec(i).udtExhGusDev(j).shtSysno Then
                        msgSYStemp(ix) = mMsgCreateSys2("sysno", udt1.udtExhGusRec(i).udtExhGusDev(j).shtSysno, udt2.udtExhGusRec(i).udtExhGusDev(j).shtSysno, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "DEV", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''CH ID
                    If udt1.udtExhGusRec(i).udtExhGusDev(j).shtChid <> udt2.udtExhGusRec(i).udtExhGusDev(j).shtChid Then
                        msgSYStemp(ix) = mMsgCreateSys2("CHID", udt1.udtExhGusRec(i).udtExhGusDev(j).shtChid, udt2.udtExhGusRec(i).udtExhGusDev(j).shtChid, "CHNo.", CHIDChange, "EXH GAS TABLE", Str(i + 1), "DEV", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "コントロール使用可／不可設定OK"

    Friend Function mCompareSetCtrlUseNotuse(ByVal udt1 As gTypSetChCtrlUse, _
                                             ByVal udt2 As gTypSetChCtrlUse, _
                                             ByVal udtMC As gEnmMachineryCargo) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　CONTROL(USE/NOT USE) SETTING（" & mGetMCEng(udtMC) & "）　■■■■"

            For i As Integer = LBound(udt1.udtCtrlUseNotuseRec) To UBound(udt1.udtCtrlUseNotuseRec)

                ''項目番号
                If udt1.udtCtrlUseNotuseRec(i).shtNo <> udt2.udtCtrlUseNotuseRec(i).shtNo Then
                    msgSYStemp(ix) = mMsgCreateSys1("No", udt1.udtCtrlUseNotuseRec(i).shtNo, udt2.udtCtrlUseNotuseRec(i).shtNo, "CONTROL", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''条件数
                If udt1.udtCtrlUseNotuseRec(i).shtCount <> udt2.udtCtrlUseNotuseRec(i).shtCount Then
                    msgSYStemp(ix) = mMsgCreateSys1("count", udt1.udtCtrlUseNotuseRec(i).shtCount, udt2.udtCtrlUseNotuseRec(i).shtCount, "CONTROL", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''条件種類
                If udt1.udtCtrlUseNotuseRec(i).bytFlg <> udt2.udtCtrlUseNotuseRec(i).bytFlg Then
                    msgSYStemp(ix) = mMsgCreateSys1("flg", udt1.udtCtrlUseNotuseRec(i).bytFlg, udt2.udtCtrlUseNotuseRec(i).bytFlg, "CONTROL", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''詳細設定
                For j As Integer = LBound(udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails) To UBound(udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails)

                    ''CH NO.
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno <> udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno Then
                        msgSYStemp(ix) = mMsgCreateSys1("chno", udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno, udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno, "CONTROL", Str(i + 1), "details", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''条件タイプ
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).bytType <> udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).bytType Then
                        msgSYStemp(ix) = mMsgCreateSys1("type", udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).bytType, udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).bytType, "CONTROL", Str(i + 1), "details", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''ビット条件
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtBit <> udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtBit Then
                        msgSYStemp(ix) = mMsgCreateSys1("bit", udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtBit, udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtBit, "CONTROL", Str(i + 1), "details", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''Process1
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess1 <> udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess1 Then
                        msgSYStemp(ix) = mMsgCreateSys1("process1", udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess1, udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess1, "CONTROL", Str(i + 1), "details", Str(j + 1))
                        ix = ix + 1
                    End If

                    ''Process2
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess2 <> udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess2 Then
                        msgSYStemp(ix) = mMsgCreateSys1("process2", udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess2, udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtProcess2, "CONTROL", Str(i + 1), "details", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ転送テーブル設定OK"

    Friend Function mCompareSetChDataForwardTableSet(ByVal udt1 As gTypSetChDataForward, _
                                                     ByVal udt2 As gTypSetChDataForward) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　DATA FORWORD TABLE SETTING　■■■■"

            For i As Integer = LBound(udt1.udtDetail) To UBound(udt1.udtDetail)

                ''データコード
                If udt1.udtDetail(i).shtDataCode <> udt2.udtDetail(i).shtDataCode Then
                    msgSYStemp(ix) = mMsgCreateSys("Data_code", udt1.udtDetail(i).shtDataCode, udt2.udtDetail(i).shtDataCode, "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

                ''データサブコード
                If udt1.udtDetail(i).shtDataSubCode <> udt2.udtDetail(i).shtDataSubCode Then
                    msgSYStemp(ix) = mMsgCreateSys("Data_sub_code", udt1.udtDetail(i).shtDataSubCode, udt2.udtDetail(i).shtDataSubCode, "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

                ''(OPS→FCU)オフセットアドレス
                If udt1.udtDetail(i).intOffsetToFCU <> udt2.udtDetail(i).intOffsetToFCU Then
                    msgSYStemp(ix) = mMsgCreateSys("Offset-to-FCU", "0x" & Hex(udt1.udtDetail(i).intOffsetToFCU).PadLeft(2, "0"), "0x" & Hex(udt2.udtDetail(i).intOffsetToFCU).PadLeft(2, "0"), "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

                ''(OPS→FCU)データサイズ
                If udt1.udtDetail(i).shtSizeToFCU <> udt2.udtDetail(i).shtSizeToFCU Then
                    msgSYStemp(ix) = mMsgCreateSys("Size-to-FCU", "0x" & Hex(udt1.udtDetail(i).shtSizeToFCU).PadLeft(2, "0"), "0x" & Hex(udt2.udtDetail(i).shtSizeToFCU).PadLeft(2, "0"), "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

                ''(FCU→OPS)オフセットアドレス
                If udt1.udtDetail(i).intOffsetToOPS <> udt2.udtDetail(i).intOffsetToOPS Then
                    msgSYStemp(ix) = mMsgCreateSys("Offset-to-OPS", "0x" & Hex(udt1.udtDetail(i).intOffsetToOPS).PadLeft(2, "0"), "0x" & Hex(udt2.udtDetail(i).intOffsetToOPS).PadLeft(2, "0"), "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

                ''(FCU→OPS)データサイズ
                If udt1.udtDetail(i).shtSizeToOps <> udt2.udtDetail(i).shtSizeToOps Then
                    msgSYStemp(ix) = mMsgCreateSys("Size-to-OPS", "0x" & Hex(udt1.udtDetail(i).shtSizeToOps).PadLeft(2, "0"), "0x" & Hex(udt2.udtDetail(i).shtSizeToOps).PadLeft(2, "0"), "DATA FORWORD", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ保存テーブル設定OK"

    Friend Function mCompareSetChDataSaveTable(ByVal udt1 As gTypSetChDataSave, _
                                               ByVal udt2 As gTypSetChDataSave) As Integer

        Try

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　DATA SAVE TABLE SETTING　■■■■"

            For i As Integer = LBound(udt1.udtDetail) To UBound(udt1.udtDetail)

                ''SYSTEM_NO
                If udt1.udtDetail(i).shtSysno <> udt2.udtDetail(i).shtSysno Then
                    msgSYStemp(ix) = mMsgCreateSys("sysno", udt1.udtDetail(i).shtSysno, udt2.udtDetail(i).shtSysno, "DATA SAVE", Str(i + 1))
                    ix = ix + 1
                End If

                ''CH_ID
                If udt1.udtDetail(i).shtChid <> udt2.udtDetail(i).shtChid Then
                    msgSYStemp(ix) = mMsgCreateSys("CHID", udt1.udtDetail(i).shtChid, udt2.udtDetail(i).shtChid, "DATA SAVE", Str(i + 1))
                    ix = ix + 1
                End If

                ''デフォルト値
                If udt1.udtDetail(i).intDefault <> udt2.udtDetail(i).intDefault Then
                    msgSYStemp(ix) = mMsgCreateSys("Default", udt1.udtDetail(i).intDefault, udt2.udtDetail(i).intDefault, "DATA SAVE", Str(i + 1))
                    ix = ix + 1
                End If

                ''立ち上げ時のデータ保存方法
                If udt1.udtDetail(i).shtSet <> udt2.udtDetail(i).shtSet Then
                    msgSYStemp(ix) = mMsgCreateSys("Set", udt1.udtDetail(i).shtSet, udt2.udtDetail(i).shtSet, "DATA SAVE", Str(i + 1))
                    ix = ix + 1
                End If

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "延長警報OK"

    Friend Function mCompareSetExtAlarm(ByVal udt1 As gTypSetExtAlarm, _
                                        ByVal udt2 As gTypSetExtAlarm) As Integer

        Try
            ''========================
            ' ''延長警報盤：共通設定
            ''========================

            Dim ix As Integer = 1

            msgSYStemp(0) = "■■■■　EXTENSION ALARM SETTING　■■■■"

            ''延長警報盤使用有無
            For i As Integer = LBound(udt1.udtExtAlarmCommon.shtUse) To UBound(udt1.udtExtAlarmCommon.shtUse)

                If udt1.udtExtAlarmCommon.shtUse(i) <> udt2.udtExtAlarmCommon.shtUse(i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("use", udt1.udtExtAlarmCommon.shtUse(i), udt2.udtExtAlarmCommon.shtUse(i), "ext panel", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            ''コンバイン設定
            If udt1.udtExtAlarmCommon.shtCombineSet <> udt2.udtExtAlarmCommon.shtCombineSet Then
                msgSYStemp(ix) = mMsgCreateSys1("combine_set", udt1.udtExtAlarmCommon.shtCombineSet, udt2.udtExtAlarmCommon.shtCombineSet, "", "", "", "")
                ix = ix + 1
            End If

            ''Duty機能有無
            If udt1.udtExtAlarmCommon.shtDutyFunc <> udt2.udtExtAlarmCommon.shtDutyFunc Then
                msgSYStemp(ix) = mMsgCreateSys1("Duty_func", udt1.udtExtAlarmCommon.shtDutyFunc, udt2.udtExtAlarmCommon.shtDutyFunc, "", "", "", "")
                ix = ix + 1
            End If

            ''特殊仕様(川汽)設定
            If udt1.udtExtAlarmCommon.shtDutyMethod <> udt2.udtExtAlarmCommon.shtDutyMethod Then
                msgSYStemp(ix) = mMsgCreateSys1("Duty_method", udt1.udtExtAlarmCommon.shtDutyMethod, udt2.udtExtAlarmCommon.shtDutyMethod, "", "", "", "")
                ix = ix + 1
            End If

            ''Group Effect機能
            If udt1.udtExtAlarmCommon.shtEffect <> udt2.udtExtAlarmCommon.shtEffect Then
                msgSYStemp(ix) = mMsgCreateSys1("effect", udt1.udtExtAlarmCommon.shtDutyMethod, udt2.udtExtAlarmCommon.shtDutyMethod, "", "", "", "")
                ix = ix + 1
            End If

            ''NVルール
            If udt1.udtExtAlarmCommon.shtNv <> udt2.udtExtAlarmCommon.shtNv Then
                msgSYStemp(ix) = mMsgCreateSys1("nv", udt1.udtExtAlarmCommon.shtDutyMethod, udt2.udtExtAlarmCommon.shtDutyMethod, "", "", "", "")
                ix = ix + 1
            End If

            ''DutyPart選択(Machinery)
            For i As Integer = 0 To 14
                If gBitCheck(udt1.udtExtAlarmCommon.shtPart1, i) <> gBitCheck(udt2.udtExtAlarmCommon.shtPart1, i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("part_1 bit", gBitValue(udt1.udtExtAlarmCommon.shtPart1, i), gBitValue(udt2.udtExtAlarmCommon.shtPart1, i), "part_1 bit", Str(i + 1), "", "")
                    ix = ix + 1
                End If
            Next

            ''DutyPart選択(Cargo)
            For i As Integer = 0 To 14
                If gBitCheck(udt1.udtExtAlarmCommon.shtPart2, i) <> gBitCheck(udt2.udtExtAlarmCommon.shtPart2, i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("part_2 bit", gBitValue(udt1.udtExtAlarmCommon.shtPart2, i), gBitValue(udt2.udtExtAlarmCommon.shtPart2, i), "part_1 bit", Str(i + 1), "", "")
                    ix = ix + 1
                End If
            Next

            ''エンジニアコール　機能有無
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 0) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 0) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call Function", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 0), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 0), "", "", "", "")
                ix = ix + 1
            End If

            ''エンジニアコール　選択SW有無
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 1) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 1) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call Select SW", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 1), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 1), "", "", "", "")
                ix = ix + 1
            End If

            ''エンジニアコール　Accept機能有無
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 2) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 2) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call Accept Function", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 2), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 2), "", "", "", "")
                ix = ix + 1
            End If

            ''エンジニアコール　自動エンジニアコール出力先(延長警報盤+通路等)
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 3) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 3) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call call timer(Ext+Accom)", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 3), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 3), "", "", "", "")
                ix = ix + 1
            End If

            ''エンジニアコール　自動エンジニアコール出力先(延長警報盤)
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 4) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 4) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call call timer(Ext)", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 4), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 4), "", "", "", "")
                ix = ix + 1
            End If

            ''エンジニアコール　Accept Pattern
            If gBitCheck(udt1.udtExtAlarmCommon.shtEeengineerCall, 5) <> gBitCheck(udt2.udtExtAlarmCommon.shtEeengineerCall, 5) Then
                msgSYStemp(ix) = mMsgCreateSys1("engineer_call Accept Pattern", gBitValue(udt1.udtExtAlarmCommon.shtEeengineerCall, 5), gBitValue(udt2.udtExtAlarmCommon.shtEeengineerCall, 5), "", "", "", "")
                ix = ix + 1
            End If

            ''パトロールマンコール　機能有無
            If gBitCheck(udt1.udtExtAlarmCommon.shtPatrolCall, 0) <> gBitCheck(udt2.udtExtAlarmCommon.shtPatrolCall, 0) Then
                msgSYStemp(ix) = mMsgCreateSys1("Patrol_call Function", gBitValue(udt1.udtExtAlarmCommon.shtPatrolCall, 0), gBitValue(udt2.udtExtAlarmCommon.shtPatrolCall, 0), "", "", "", "")
                ix = ix + 1
            End If

            ''パトロールマンコール　SW有無
            If gBitCheck(udt1.udtExtAlarmCommon.shtPatrolCall, 1) <> gBitCheck(udt2.udtExtAlarmCommon.shtPatrolCall, 1) Then
                msgSYStemp(ix) = mMsgCreateSys1("Patrol_call SW", gBitValue(udt1.udtExtAlarmCommon.shtPatrolCall, 1), gBitValue(udt2.udtExtAlarmCommon.shtPatrolCall, 1), "", "", "", "")
                ix = ix + 1
            End If

            ''パトロールマンコール　Call set 出力方法
            If gBitCheck(udt1.udtExtAlarmCommon.shtPatrolCall, 2) <> gBitCheck(udt2.udtExtAlarmCommon.shtPatrolCall, 2) Then
                msgSYStemp(ix) = mMsgCreateSys1("Patrol_call Call set", gBitValue(udt1.udtExtAlarmCommon.shtPatrolCall, 2), gBitValue(udt2.udtExtAlarmCommon.shtPatrolCall, 2), "", "", "", "")
                ix = ix + 1
            End If

            ''パトロールマンコール　Alarm set 出力方法
            If gBitCheck(udt1.udtExtAlarmCommon.shtPatrolCall, 3) <> gBitCheck(udt2.udtExtAlarmCommon.shtPatrolCall, 3) Then
                msgSYStemp(ix) = mMsgCreateSys1("Patrol_call Alarm set", gBitValue(udt1.udtExtAlarmCommon.shtPatrolCall, 3), gBitValue(udt2.udtExtAlarmCommon.shtPatrolCall, 3), "", "", "", "")
                ix = ix + 1
            End If

            ''Dead Man Alarm 使用有無
            If udt1.udtExtAlarmCommon.shtDeadAlarm <> udt2.udtExtAlarmCommon.shtDeadAlarm Then
                msgSYStemp(ix) = mMsgCreateSys1("dead_alarm", udt1.udtExtAlarmCommon.shtDeadAlarm, udt2.udtExtAlarmCommon.shtDeadAlarm, "", "", "", "")
                ix = ix + 1
            End If

            ''アラームランプ数
            If udt1.udtExtAlarmCommon.shtLamps <> udt2.udtExtAlarmCommon.shtLamps Then
                msgSYStemp(ix) = mMsgCreateSys1("lamps", udt1.udtExtAlarmCommon.shtLamps, udt2.udtExtAlarmCommon.shtLamps, "", "", "", "")
                ix = ix + 1
            End If

            ''ブザーパターン
            If udt1.udtExtAlarmCommon.shtBuzzer <> udt2.udtExtAlarmCommon.shtBuzzer Then
                msgSYStemp(ix) = mMsgCreateSys1("buzzer", udt1.udtExtAlarmCommon.shtBuzzer, udt2.udtExtAlarmCommon.shtBuzzer, "", "", "", "")
                ix = ix + 1
            End If

            ''グループ出力パターン
            If udt1.udtExtAlarmCommon.shtGrpOut <> udt2.udtExtAlarmCommon.shtGrpOut Then
                msgSYStemp(ix) = mMsgCreateSys1("grp_out", udt1.udtExtAlarmCommon.shtGrpOut, udt2.udtExtAlarmCommon.shtGrpOut, "", "", "", "")
                ix = ix + 1
            End If

            ''GrpEffect （12個）
            For i As Integer = 0 To 11
                If gBitCheck(udt1.udtExtAlarmCommon.shtGrpEffct, i) <> gBitCheck(udt2.udtExtAlarmCommon.shtGrpEffct, i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("grp_effect bit", gBitValue(udt1.udtExtAlarmCommon.shtGrpEffct, i), gBitValue(udt2.udtExtAlarmCommon.shtGrpEffct, i), "grp_effect bit", Str(i), "", "")
                    ix = ix + 1
                End If
            Next

            ''FireSoundGrp（12個）
            For i As Integer = 0 To 11
                If gBitCheck(udt1.udtExtAlarmCommon.shtGrpFire, i) <> gBitCheck(udt2.udtExtAlarmCommon.shtGrpFire, i) Then
                    msgSYStemp(ix) = mMsgCreateSys1("grp_fire bit", gBitValue(udt1.udtExtAlarmCommon.shtGrpFire, i), gBitValue(udt2.udtExtAlarmCommon.shtGrpFire, i), "grp_fire bit", Str(i), "", "")
                    ix = ix + 1
                End If
            Next

            ''グループアラームランプ出力選択
            If udt1.udtExtAlarmCommon.shtGrpAlarm <> udt2.udtExtAlarmCommon.shtGrpAlarm Then
                msgSYStemp(ix) = mMsgCreateSys1("grp_alarm", udt1.udtExtAlarmCommon.shtGrpAlarm, udt2.udtExtAlarmCommon.shtGrpAlarm, "", "", "", "")
                ix = ix + 1
            End If

            ''Fireブザーパターン 
            If udt1.udtExtAlarmCommon.shtFireBuzzer <> udt2.udtExtAlarmCommon.shtFireBuzzer Then
                msgSYStemp(ix) = mMsgCreateSys1("fire_buzzer", udt1.udtExtAlarmCommon.shtFireBuzzer, udt2.udtExtAlarmCommon.shtFireBuzzer, "", "", "", "")
                ix = ix + 1
            End If

            ''EXTグループアラーム出力設定
            For i As Integer = 0 To UBound(udt1.udtExtAlarmCommon.intGroupType)

                For j As Integer = 0 To 25

                    If gBitCheck(udt1.udtExtAlarmCommon.intGroupType(i), j) <> gBitCheck(udt2.udtExtAlarmCommon.intGroupType(i), j) Then
                        msgSYStemp(ix) = mMsgCreateSys1("Group_type bit", gBitValue(udt1.udtExtAlarmCommon.intGroupType(i), j), gBitValue(udt2.udtExtAlarmCommon.intGroupType(i), j), "LED", Str(i + 1), "Group_type bit", j)
                        ix = ix + 1
                    End If

                Next

            Next

            ''特殊(川汽)仕様 (W/H)
            If udt1.udtExtAlarmCommon.shtSpecialWh <> udt2.udtExtAlarmCommon.shtSpecialWh Then
                msgSYStemp(ix) = mMsgCreateSys1("special_WH", udt1.udtExtAlarmCommon.shtSpecialWh, udt2.udtExtAlarmCommon.shtSpecialWh, "", "", "", "")
                ix = ix + 1
            End If

            ''特殊(川汽)仕様 (P/R)
            If udt1.udtExtAlarmCommon.shtSpecialPr <> udt2.udtExtAlarmCommon.shtSpecialPr Then
                msgSYStemp(ix) = mMsgCreateSys1("special_PR", udt1.udtExtAlarmCommon.shtSpecialPr, udt2.udtExtAlarmCommon.shtSpecialPr, "", "", "", "")
                ix = ix + 1
            End If

            ''特殊(川汽)仕様 (C/E)
            If udt1.udtExtAlarmCommon.shtSpecialCe <> udt2.udtExtAlarmCommon.shtSpecialCe Then
                msgSYStemp(ix) = mMsgCreateSys1("special_CE", udt1.udtExtAlarmCommon.shtSpecialCe, udt2.udtExtAlarmCommon.shtSpecialCe, "", "", "", "")
                ix = ix + 1
            End If

            ''Eeengineer Call設定
            If udt1.udtExtAlarmCommon.shtEngCall <> udt2.udtExtAlarmCommon.shtEngCall Then
                msgSYStemp(ix) = mMsgCreateSys1("eng_call", udt1.udtExtAlarmCommon.shtEngCall, udt2.udtExtAlarmCommon.shtEngCall, "", "", "", "")
                ix = ix + 1
            End If

            ''LCD EXTグループ表示設定
            For i As Integer = 0 To UBound(udt1.udtExtAlarmCommon.udtExtGroup)

                ''警報グループ番号(LCD設定)
                If udt1.udtExtAlarmCommon.udtExtGroup(i).shtGroup <> udt2.udtExtAlarmCommon.udtExtGroup(i).shtGroup Then
                    msgSYStemp(ix) = mMsgCreateSys1("group", udt1.udtExtAlarmCommon.udtExtGroup(i).shtGroup, udt2.udtExtAlarmCommon.udtExtGroup(i).shtGroup, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''マーク番号(LCD設定)
                If udt1.udtExtAlarmCommon.udtExtGroup(i).shtMark <> udt2.udtExtAlarmCommon.udtExtGroup(i).shtMark Then
                    msgSYStemp(ix) = mMsgCreateSys1("mark", udt1.udtExtAlarmCommon.udtExtGroup(i).shtMark, udt2.udtExtAlarmCommon.udtExtGroup(i).shtMark, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''グループ名称(LCD設定)
                If Not gCompareString(udt1.udtExtAlarmCommon.udtExtGroup(i).strGroupName, udt2.udtExtAlarmCommon.udtExtGroup(i).strGroupName) Then
                    msgSYStemp(ix) = mMsgCreateSys1("group_name", gGetString(udt1.udtExtAlarmCommon.udtExtGroup(i).strGroupName), gGetString(udt2.udtExtAlarmCommon.udtExtGroup(i).strGroupName), "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            ''LCD EXT Duty表示名称設定
            For i As Integer = 0 To UBound(udt1.udtExtAlarmCommon.udtExtDuty)

                ''Duty名称
                If Not gCompareString(udt1.udtExtAlarmCommon.udtExtDuty(i).strDutyName, udt2.udtExtAlarmCommon.udtExtDuty(i).strDutyName) Then
                    msgSYStemp(ix) = mMsgCreateSys1("duty_name", gGetString(udt1.udtExtAlarmCommon.udtExtDuty(i).strDutyName), gGetString(udt2.udtExtAlarmCommon.udtExtDuty(i).strDutyName), "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

            Next

            ''オプション設定1(特殊仕様向け)
            If udt1.udtExtAlarmCommon.Option1 <> udt2.udtExtAlarmCommon.Option1 Then
                msgSYStemp(ix) = mMsgCreateSys1("Comm_Option1", udt1.udtExtAlarmCommon.Option1, udt2.udtExtAlarmCommon.Option1, "", "", "", "")
                ix = ix + 1
            End If

            ''オプション設定1(特殊仕様向け)
            If udt1.udtExtAlarmCommon.Option2 <> udt2.udtExtAlarmCommon.Option2 Then
                msgSYStemp(ix) = mMsgCreateSys1("Comm_Option2", udt1.udtExtAlarmCommon.Option2, udt2.udtExtAlarmCommon.Option2, "", "", "", "")
                ix = ix + 1
            End If

            ''オプション設定1(特殊仕様向け)
            If udt1.udtExtAlarmCommon.Option3 <> udt2.udtExtAlarmCommon.Option3 Then
                msgSYStemp(ix) = mMsgCreateSys1("Comm_Option3", udt1.udtExtAlarmCommon.Option3, udt2.udtExtAlarmCommon.Option3, "", "", "", "")
                ix = ix + 1
            End If



            ''========================
            ' ''延長警報盤：個別設定
            ''========================
            For i As Integer = LBound(udt1.udtExtAlarm) To UBound(udt1.udtExtAlarm)

                ''設置場所
                If Not gCompareString(udt1.udtExtAlarm(i).strPlace, udt2.udtExtAlarm(i).strPlace) Then
                    msgSYStemp(ix) = mMsgCreateSys1("place", gGetString(udt1.udtExtAlarm(i).strPlace), gGetString(udt2.udtExtAlarm(i).strPlace), "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''延長警報パネル通信ID番号
                If udt1.udtExtAlarm(i).shtNo <> udt2.udtExtAlarm(i).shtNo Then
                    msgSYStemp(ix) = mMsgCreateSys1("no", udt1.udtExtAlarm(i).shtNo, udt2.udtExtAlarm(i).shtNo, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Re Alarm設定有無
                If udt1.udtExtAlarm(i).shtReAlarm <> udt2.udtExtAlarm(i).shtReAlarm Then
                    msgSYStemp(ix) = mMsgCreateSys1("re_alarm", udt1.udtExtAlarm(i).shtReAlarm, udt2.udtExtAlarm(i).shtReAlarm, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''ブザーカット有無
                If udt1.udtExtAlarm(i).shtBuzzCut <> udt2.udtExtAlarm(i).shtBuzzCut Then
                    msgSYStemp(ix) = mMsgCreateSys1("buzz_cut", udt1.udtExtAlarm(i).shtBuzzCut, udt2.udtExtAlarm(i).shtBuzzCut, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''フリーエンジニア有無
                If udt1.udtExtAlarm(i).shtFreeEng <> udt2.udtExtAlarm(i).shtFreeEng Then
                    msgSYStemp(ix) = mMsgCreateSys1("free_eng", udt1.udtExtAlarm(i).shtFreeEng, udt2.udtExtAlarm(i).shtFreeEng, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''オプション
                If udt1.udtExtAlarm(i).shtOption <> udt2.udtExtAlarm(i).shtOption Then
                    msgSYStemp(ix) = mMsgCreateSys1("option", udt1.udtExtAlarm(i).shtOption, udt2.udtExtAlarm(i).shtOption, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''パネルタイプ
                If udt1.udtExtAlarm(i).shtPanel <> udt2.udtExtAlarm(i).shtPanel Then
                    msgSYStemp(ix) = mMsgCreateSys1("panel", udt1.udtExtAlarm(i).shtPanel, udt2.udtExtAlarm(i).shtPanel, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''パート設定　マシナリー
                If gBitCheck(udt1.udtExtAlarm(i).shtPart, 0) <> gBitCheck(udt2.udtExtAlarm(i).shtPart, 0) Then
                    msgSYStemp(ix) = mMsgCreateSys1("part Machinery", gBitValue(udt1.udtExtAlarm(i).shtPart, 0), gBitValue(udt2.udtExtAlarm(i).shtPart, 0), "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''パート設定　カーゴ
                If gBitCheck(udt1.udtExtAlarm(i).shtPart, 1) <> gBitCheck(udt2.udtExtAlarm(i).shtPart, 1) Then
                    msgSYStemp(ix) = mMsgCreateSys1("part Cargo", gBitValue(udt1.udtExtAlarm(i).shtPart, 1), gBitValue(udt2.udtExtAlarm(i).shtPart, 1), "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Eeengineer Call No設定
                If udt1.udtExtAlarm(i).shtEngNo <> udt2.udtExtAlarm(i).shtEngNo Then
                    msgSYStemp(ix) = mMsgCreateSys1("eng_no", udt1.udtExtAlarm(i).shtEngNo, udt2.udtExtAlarm(i).shtEngNo, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Duty番号
                If udt1.udtExtAlarm(i).shtDuty <> udt2.udtExtAlarm(i).shtDuty Then
                    msgSYStemp(ix) = mMsgCreateSys1("duty", udt1.udtExtAlarm(i).shtDuty, udt2.udtExtAlarm(i).shtDuty, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Dutyブザーストップ動作設定
                If udt1.udtExtAlarm(i).shtDutyBuzz <> udt2.udtExtAlarm(i).shtDutyBuzz Then
                    msgSYStemp(ix) = mMsgCreateSys1("duty_buzz", udt1.udtExtAlarm(i).shtDutyBuzz, udt2.udtExtAlarm(i).shtDutyBuzz, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''Watch LED 表示方法選択
                If udt1.udtExtAlarm(i).shtWatchLed <> udt2.udtExtAlarm(i).shtWatchLed Then
                    msgSYStemp(ix) = mMsgCreateSys1("watch_led", udt1.udtExtAlarm(i).shtWatchLed, udt2.udtExtAlarm(i).shtWatchLed, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''LED表示方法選択
                If udt1.udtExtAlarm(i).shtLedOut <> udt2.udtExtAlarm(i).shtLedOut Then
                    msgSYStemp(ix) = mMsgCreateSys1("led_out", udt1.udtExtAlarm(i).shtLedOut, udt2.udtExtAlarm(i).shtLedOut, "REC", Str(i + 1), "", "")
                    ix = ix + 1
                End If

                ''LED12個分の遅延タイマ値
                For j As Integer = 0 To UBound(udt1.udtExtAlarm(i).shtLedTimer)

                    If udt1.udtExtAlarm(i).shtLedTimer(j) <> udt2.udtExtAlarm(i).shtLedTimer(j) Then
                        msgSYStemp(ix) = mMsgCreateSys1("led_timer", udt1.udtExtAlarm(i).shtLedTimer(j), udt2.udtExtAlarm(i).shtLedTimer(j), "REC", Str(i + 1), "LED", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

                ''表示位置(LCD設定)
                For j As Integer = 0 To UBound(udt1.udtExtAlarm(i).shtPosition)

                    If udt1.udtExtAlarm(i).shtPosition(j) <> udt2.udtExtAlarm(i).shtPosition(j) Then
                        msgSYStemp(ix) = mMsgCreateSys1("position", udt1.udtExtAlarm(i).shtPosition(j), udt2.udtExtAlarm(i).shtPosition(j), "REC", Str(i + 1), "LCD", Str(j + 1))
                        ix = ix + 1
                    End If

                Next

            Next

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネルID → チャンネルNO 変換"

    'T.Ueki 2015/5/7
    '--------------------------------------------------------------------
    ' 機能      : チャンネルID → チャンネルNO 変換
    ' 返り値    : チャンネルNO
    ' 引き数    : ARG1 - (I ) チャンネルID
    ' 機能説明  : チャンネルIDをチャンネルNOに変換する
    ' 補足　　  : チャンネルIDが 0 の場合は 0 を返す
    ' 補足　　  : チャンネルIDに対応するCH NOが存在しない場合は -1 を返す
    '--------------------------------------------------------------------
    Public Function gConvChIdToChNoComp(ByVal intChId As Integer) As Integer

        Try

            If intChId = 0 Then Return 0

            Dim intChNo As Integer = -1

            For i As Integer = LBound(mudtSource.SetChInfo.udtChannel) To UBound(mudtSource.SetChInfo.udtChannel)

                With mudtSource.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChId Then

                        intChNo = .shtChno

                        Exit For

                    End If

                End With

            Next

            Return intChNo

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    Public Function gConvChIdToChNoCompTarget(ByVal intChId As Integer) As Integer

        Try

            If intChId = 0 Then Return 0

            Dim intChNo As Integer = -1

            For i As Integer = LBound(mudtTarget.SetChInfo.udtChannel) To UBound(mudtTarget.SetChInfo.udtChannel)

                With mudtTarget.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChId Then

                        intChNo = .shtChno

                        Exit For

                    End If

                End With

            Next

            Return intChNo

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

#Region "ｽﾃｰﾀｽ変化ﾁｪｯｸ"

    '--------------------------------------------------------------------
    ' 機能      : ｽﾃｰﾀｽ変化ﾁｪｯｸ     Ver1.11.6 2016.09.15
    ' 返り値    : True：変化あり  False：変化なし
    ' 引き数    : shtStatus1：比較元ｽﾃｰﾀｽｺｰﾄﾞ
    '             shtStatus2：比較先ｽﾃｰﾀｽｺｰﾄﾞ
    ' 機能説明  : ﾃﾞｼﾞﾀﾙとｱﾅﾛｸﾞでｺｰﾄﾞ番号が異なるため見た目に同じｽﾃｰﾀｽかどうかを確認する
    '--------------------------------------------------------------------
    Private Function ChkStatusChange(ByVal shtStatus1 As Short, ByVal shtStatus2 As Short) As Boolean

        '' 手入力ｺｰﾄﾞならば変化あり
        If shtStatus1 = &HFF Or shtStatus2 = &HFF Then
            Return True
        End If

        '' NOR/HIGH
        If ((shtStatus1 = &H41) And (shtStatus2 = &H8)) Or ((shtStatus1 = &H8) And (shtStatus2 = &H41)) Then
            Return False
        End If

        '' NOR/LOW
        If ((shtStatus1 = &H42) And (shtStatus2 = &H9)) Or ((shtStatus1 = &H9) And (shtStatus2 = &H42)) Then
            Return False
        End If

        '' NOR/HH
        If ((shtStatus1 = &H44) And (shtStatus2 = &HB)) Or ((shtStatus1 = &HB) And (shtStatus2 = &H44)) Then
            Return False
        End If

        '' NOR/LL
        If ((shtStatus1 = &H45) And (shtStatus2 = &HA)) Or ((shtStatus1 = &HA) And (shtStatus2 = &H45)) Then
            Return False
        End If

        Return True

    End Function
#End Region

#Region "MIMICファイル比較"
    'Ver2.0.0.2 2016.12.09 MIMICﾌｧｲﾙレベル比較機能
    Friend Function mCompareMimicFiles(ByVal udt1 As gTypCompareFileInfo, _
                                             ByVal udt2 As gTypCompareFileInfo) As Integer

        Try
            'それぞれﾊﾟｽ生成
            'Source
            Dim strPathBase1 As String = ""
            strPathBase1 = System.IO.Path.Combine(udt1.strFilePath, udt1.strFileName)
            strPathBase1 = System.IO.Path.Combine(strPathBase1, gCstFolderNameSave)
            strPathBase1 = System.IO.Path.Combine(strPathBase1, gCstFolderNameMimic)
            strPathBase1 = System.IO.Path.Combine(strPathBase1, "Mimic1")
            'Target
            Dim strPathBase2 As String = ""
            strPathBase2 = System.IO.Path.Combine(udt2.strFilePath, udt2.strFileName)
            strPathBase2 = System.IO.Path.Combine(strPathBase2, gCstFolderNameSave)
            strPathBase2 = System.IO.Path.Combine(strPathBase2, gCstFolderNameMimic)
            strPathBase2 = System.IO.Path.Combine(strPathBase2, "Mimic1")


            Dim ix As Integer = 1

            Dim blFol1 As Boolean = False
            Dim blFol2 As Boolean = False

            blFol1 = System.IO.Directory.Exists(strPathBase1)
            blFol2 = System.IO.Directory.Exists(strPathBase2)

            'Ver2.0.2.8 両方ﾌｫﾙﾀﾞが存在しないなら何もﾒｯｾｰｼﾞを出さずに処理抜け
            If blFol1 = False And blFol2 = False Then
                Return 0
            End If


            msgSYStemp(0) = "■■■■　MIMIC FILE CHECK　■■■■"


            'Mimic1フォルダが存在しない場合処理抜け
            If blFol1 = False Then
                msgSYStemp(ix) = mMsgCreateStr("No Mimic Folder", "Please Check ", strPathBase1)
                ix = ix + 1
                Call mMsgSysGrid(ix)
                Return 0
            End If
            If blFol2 = False Then
                msgSYStemp(ix) = mMsgCreateStr("No Mimic Folder", "Please Check ", strPathBase2)
                ix = ix + 1
                Call mMsgSysGrid(ix)
                Return 0
            End If


            'Source側のMimicファイルS*.mim　を取得する
            Dim strMimicFiles As String() = System.IO.Directory.GetFiles(strPathBase1, "*.*", System.IO.SearchOption.AllDirectories)

            

            Dim strPath As String = ""
            For i As Integer = 0 To UBound(strMimicFiles) Step 1
                strPath = strMimicFiles(i)
                'Targetにﾌｧｲﾙ存在するかチェック　ない場合ﾒｯｾｰｼﾞを出して次へ
                ' ファイル名取り出し
                Dim strFileName As String = System.IO.Path.GetFileName(strPath)
                'Ver2.0.3.5 *.BMPﾌｧｲﾙは対象外
                If strFileName.ToUpper.IndexOf(".BMP") > 0 Then
                    Continue For
                End If

                Dim strTarget As String() = System.IO.Directory.GetFiles(strPathBase2, strFileName, System.IO.SearchOption.AllDirectories)

                'ファイルが存在しない場合、メッセージを出して次へ
                If strTarget.Length <= 0 Then
                    msgSYStemp(ix) = mMsgCreateStr("No Mimic File", strFileName, "")
                    ix = ix + 1
                    Continue For
                End If

                'ファイルが同じﾌｧｲﾙ(パスも含めて)の場合は読み飛ばす
                If strPath = strTarget(0) Then
                    Continue For
                End If

                'ファイルをバイナリレベルで比較
                If BinComp(strPath, strTarget(0)) = False Then
                    msgSYStemp(ix) = mMsgCreateStr("Mimic No Match", strFileName, strFileName)
                    ix = ix + 1
                End If

            Next i

            'Ver2.0.2.4 逆＝新規mimicﾌｧｲﾙ検出
            strPath = ""
            strMimicFiles = System.IO.Directory.GetFiles(strPathBase2, "*.*", System.IO.SearchOption.AllDirectories)
            For i As Integer = 0 To UBound(strMimicFiles) Step 1
                strPath = strMimicFiles(i)
                'Targetにﾌｧｲﾙ存在するかチェック　ない場合ﾒｯｾｰｼﾞを出して次へ
                ' ファイル名取り出し
                Dim strFileName As String = System.IO.Path.GetFileName(strPath)
                'Ver2.0.3.5 *.BMPﾌｧｲﾙは対象外
                If strFileName.ToUpper.IndexOf(".BMP") > 0 Then
                    Continue For
                End If

                Dim strTarget As String() = System.IO.Directory.GetFiles(strPathBase1, strFileName, System.IO.SearchOption.AllDirectories)

                'ファイルが存在しない場合、メッセージを出して次へ(新規ということ)
                If strTarget.Length <= 0 Then
                    msgSYStemp(ix) = mMsgCreateStr("New Mimic File", "", strFileName)
                    ix = ix + 1
                    Continue For
                End If
            Next i


            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    'ファイルバイナリレベル比較関数
    Private Function BinComp(ByVal f1 As String, ByVal f2 As String) As Boolean
        Dim fi1 As New System.IO.FileInfo(f1)
        Dim fi2 As New System.IO.FileInfo(f2)

        ' ファイルサイズの比較
        If fi1.Length <> fi2.Length Then Return False

        Dim sr1 As System.IO.Stream = Nothing
        Dim sr2 As System.IO.Stream = Nothing
        Try
            ' バイナリファイルの読込
            sr1 = System.IO.File.Open(f1, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            sr2 = System.IO.File.Open(f2, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' バイナリファイルの1バイト比較
            Dim b1 As Integer = -1
            Dim b2 As Integer = -1
            Do
                b1 = sr1.ReadByte()
                b2 = sr2.ReadByte()
                If b1 <> b2 Then Return False
                If b1 = -1 Then Exit Do
            Loop While (b1 = b2)

        Catch ex As Exception
            Throw ex
        Finally
            ' バイナリファイルクローズ
            If sr1 Is Nothing = False Then sr1.Close()
            If sr2 Is Nothing = False Then sr2.Close()
        End Try
        Return True
    End Function

#End Region

#Region "CFｶｰﾄﾞ以外比較"
    'Ver2.0.7.M CFｶｰﾄﾞ以外の設定も比較
    Friend Function fnCHKfiles(ByVal udt1 As gTypCompareFileInfo, _
                                             ByVal udt2 As gTypCompareFileInfo) As Integer

        Try
            Dim bytFUSet1 As Byte
            Dim bytNotCombine1 As Byte
            Dim bytOutputPrint1 As Byte
            Dim bytGreenMarkPrint1 As Byte
            Dim bytOrAndPrint1 As Byte
            Dim bytChListOrder1 As Byte
            Dim bytTermRange1 As Byte
            Dim bytTerVer1 As Byte
            Dim bytTerDIMsg1 As Byte
            Dim bytSIOport1 As Byte
            Dim bytHOAN1 As Byte
            Dim bytNEWDES1 As Byte
            Dim byGREPNUM1 As Byte  '2018.12.13 倉重 比較用変数追加
            '
            Dim bytFUSet2 As Byte
            Dim bytNotCombine2 As Byte
            Dim bytOutputPrint2 As Byte
            Dim bytGreenMarkPrint2 As Byte
            Dim bytOrAndPrint2 As Byte
            Dim bytChListOrder2 As Byte
            Dim bytChlistINSGPrint2 As Byte
            Dim bytTermRange2 As Byte
            Dim bytTerVer2 As Byte
            Dim bytTerDIMsg2 As Byte
            Dim bytSIOport2 As Byte
            Dim bytHOAN2 As Byte
            Dim bytNEWDES2 As Byte
            Dim byGREPNUM2 As Byte  '2018.12.13 倉重 比較用変数追加


            'それぞれﾊﾟｽ生成
            'Source
            Dim strPathBase1 As String = ""
            strPathBase1 = System.IO.Path.Combine(udt1.strFilePath, udt1.strFileName)
            strPathBase1 = System.IO.Path.Combine(strPathBase1, gCstFolderNameEditorInfo)
            strPathBase1 = System.IO.Path.Combine(strPathBase1, gCstFolderNameUpdateInfo)
            strPathBase1 = strPathBase1 & "\" & gCstIniFile

            'Target
            Dim strPathBase2 As String = ""
            strPathBase2 = System.IO.Path.Combine(udt2.strFilePath, udt2.strFileName)
            strPathBase2 = System.IO.Path.Combine(strPathBase2, gCstFolderNameEditorInfo)
            strPathBase2 = System.IO.Path.Combine(strPathBase2, gCstFolderNameUpdateInfo)
            strPathBase2 = strPathBase2 & "\" & gCstIniFile


            Dim ix As Integer = 1


            Dim strTemp As String = ""

            'まず値格納
            '>>>1
            If System.IO.File.Exists(strPathBase1) Then
                strTemp = GetIni("System", "FUPrint", "1", strPathBase1)
                bytFUSet1 = CByte(strTemp)

                strTemp = GetIni("System", "NotCombinPrint", "0", strPathBase1)
                bytNotCombine1 = CByte(strTemp)

                strTemp = GetIni("System", "OutputPrint", "0", strPathBase1)
                bytOutputPrint1 = CByte(strTemp)

                strTemp = GetIni("System", "GreenMarkPrint", "0", strPathBase1)
                bytGreenMarkPrint1 = CByte(strTemp)

                strTemp = GetIni("System", "OrAndPrint", "0", strPathBase1)
                bytOrAndPrint1 = CByte(strTemp)

                strTemp = GetIni("System", "ChListOrderPrint", "0", strPathBase1)
                bytChListOrder1 = CByte(strTemp)

                strTemp = GetIni("System", "TermRangePrint", "0", strPathBase1)
                bytTermRange1 = CByte(strTemp)

                strTemp = GetIni("System", "TermVersionPrint", "0", strPathBase1)
                bytTerVer1 = CByte(strTemp)

                strTemp = GetIni("System", "TermDICommonMsg", "0", strPathBase1)
                bytTerDIMsg1 = CByte(strTemp)

                strTemp = GetIni("System", "SIOport", "0", strPathBase1)
                bytSIOport1 = CByte(strTemp)

                strTemp = GetIni("System", "Hoan", "0", strPathBase1)
                bytHOAN1 = CByte(strTemp)

                strTemp = GetIni("System", "Newdes", "0", strPathBase1)
                bytNEWDES1 = CByte(strTemp)

                strTemp = GetIni("System", "GREPNUM", "0", strPathBase1)    '2018.12.13 倉重
                byGREPNUM1 = CByte(strTemp)

            Else
                bytFUSet1 = 1
                bytNotCombine1 = 0
                bytOutputPrint1 = 0
                bytGreenMarkPrint1 = 0
                bytOrAndPrint1 = 0
                bytChListOrder1 = 0
                bytTermRange1 = 1
                bytTerVer1 = 1
                bytTerDIMsg1 = 1
                bytSIOport1 = 0
                bytHOAN1 = 0
                bytNEWDES1 = 0
                byGREPNUM1 = 0  '2018.12.13 倉重

            End If
            '>>>2
            If System.IO.File.Exists(strPathBase2) Then
                'strTemp = GetIni("System", "FUPrint", "1", strPathBase2)
                'bytFUSet2 = CByte(strTemp)

                'strTemp = GetIni("System", "NotCombinPrint", "0", strPathBase2)
                'bytNotCombine2 = CByte(strTemp)

                'strTemp = GetIni("System", "OutputPrint", "0", strPathBase2)
                'bytOutputPrint2 = CByte(strTemp)

                'strTemp = GetIni("System", "GreenMarkPrint", "0", strPathBase2)
                'bytGreenMarkPrint2 = CByte(strTemp)

                'strTemp = GetIni("System", "OrAndPrint", "0", strPathBase2)
                'bytOrAndPrint2 = CByte(strTemp)

                'strTemp = GetIni("System", "ChListOrderPrint", "0", strPathBase2)
                'bytChListOrder2 = CByte(strTemp)

                'strTemp = GetIni("System", "TermRangePrint", "0", strPathBase2)
                'bytTermRange2 = CByte(strTemp)

                'strTemp = GetIni("System", "TermVersionPrint", "0", strPathBase2)
                'bytTerVer2 = CByte(strTemp)

                'strTemp = GetIni("System", "SIOport", "0", strPathBase2)
                'bytSIOport2 = CByte(strTemp)

                'strTemp = GetIni("System", "Hoan", "0", strPathBase2)
                'bytHOAN2 = CByte(strTemp)

                'strTemp = GetIni("System", "Newdes", "0", strPathBase2)
                'bytNEWDES2 = CByte(strTemp)
                '
                'strTemp = GetIni("System", "GREPNUM", "0", strPathBase1)
                'byGREPNUM2 = CByte(strTemp)

                bytFUSet2 = g_bytFUSet

                bytNotCombine2 = g_bytNotCombine

                bytOutputPrint2 = g_bytOutputPrint

                bytGreenMarkPrint2 = g_bytGreenMarkPrint

                bytOrAndPrint2 = g_bytOrAndPrint

                bytChListOrder2 = g_bytChListOrder

                bytChlistINSGPrint2 = g_bytChListINSGprint     ''Ver2.0.8.N  R,W,J,SのINSGを印字するしない

                bytTermRange2 = g_bytTermRange

                bytTerVer2 = g_bytTerVer

                bytTerDIMsg2 = g_bytTerDIMsg    'Ver2.0.8.7 DI端子表に共通コモンのメッセージ

                bytSIOport2 = g_bytSIOport

                bytHOAN2 = g_bytHOAN

                bytNEWDES2 = g_bytNEWDES

                byGREPNUM2 = g_bytGREPNUM '2018.12.13 倉重

            Else
                bytFUSet2 = 1
                bytNotCombine2 = 0
                bytOutputPrint2 = 0
                bytGreenMarkPrint2 = 0
                bytOrAndPrint2 = 0
                bytChListOrder2 = 0
                bytTermRange2 = 1
                bytTerVer2 = 1
                bytTerDIMsg2 = 1
                bytSIOport2 = 0
                bytHOAN2 = 0
                bytNEWDES2 = 0
                byGREPNUM2 = 0  '2018.12.13 倉重
            End If

            msgSYStemp(0) = "■■■■　SETTING FILE CHECK　■■■■"

            'Formats DrawingNo 3 digits
            If bytFUSet1 <> bytFUSet2 Then
                msgSYStemp(ix) = mMsgCreateStr("(Term)Formats DrawingNo 3 digits", bytFUSet1.ToString, bytFUSet2.ToString)
                ix = ix + 1
            End If

            'Prints Not CombineType (Combine Only)
            If bytNotCombine1 <> bytNotCombine2 Then
                msgSYStemp(ix) = mMsgCreateStr("(ChList)Prints Not CombineType (Combine Only)", bytNotCombine1.ToString, bytNotCombine2.ToString)
                ix = ix + 1
            End If

            'Ascending Group No
            If bytChListOrder1 <> bytChListOrder2 Then
                msgSYStemp(ix) = mMsgCreateStr("(ChList)Ascending Group No", bytChListOrder1.ToString, bytChListOrder2.ToString)
                ix = ix + 1
            End If

            'Range Not Print
            If bytTermRange1 <> bytTermRange2 Then
                msgSYStemp(ix) = mMsgCreateStr("(Term)Range Not Print", bytTermRange1.ToString, bytTermRange2.ToString)
                ix = ix + 1
            End If

            'Term Ver Not Print
            If bytTerVer1 <> bytTerVer2 Then
                msgSYStemp(ix) = mMsgCreateStr("(Term)Term Ver Not Print", bytTerVer1.ToString, bytTerVer2.ToString)
                ix = ix + 1
            End If

            'Ver2.0.8.7
            If bytTerDIMsg1 <> bytTerDIMsg2 Then
                msgSYStemp(ix) = mMsgCreateStr("(Term)Term DI Message Print", bytTerDIMsg1.ToString, bytTerDIMsg2.ToString)
                ix = ix + 1
            End If

            'Japanese Menu(Hoan)
            If bytHOAN1 <> bytHOAN2 Then
                msgSYStemp(ix) = mMsgCreateStr("(OverAll)Japanese Menu(Hoan)", bytHOAN1.ToString, bytHOAN2.ToString)
                ix = ix + 1
            End If

            'OPS New Design
            If bytNEWDES1 <> bytNEWDES2 Then
                msgSYStemp(ix) = mMsgCreateStr("(OverAll)OPS New Design", bytNEWDES1.ToString, bytNEWDES2.ToString)
                ix = ix + 1
            End If

            'GRep Max Num 2018.12.13 倉重
            If byGREPNUM1 <> byGREPNUM2 Then
                msgSYStemp(ix) = mMsgCreateStr("GRep Max Num", byGREPNUM1.ToString, byGREPNUM2.ToString)
                ix = ix + 1
            End If

            '何も変更がない場合は表示しない
            If ix <> 1 Then
                Call mMsgSysGrid(ix)
            Else
                '変更がないためタイトルクリア
                msgSYStemp(0) = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "関連検索"
    'Ver2.0.0.2 2016.12.09 関連検索機能
    '--------------------------------------------------------------------
    ' 機能      : CHが設定されている項目をﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkProc(ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim ch_no As Integer

        ch_no = CInt(strCH)

        Call ChkSIOSettingProc(ch_no, strCH, intLogGyo)     '' SIO1～9　設定確認
        Call ChkOrTblProc(ch_no, strCH, intLogGyo)          '' ORﾃｰﾌﾞﾙ設定

        Call ChkGRTblProc(ch_no, strCH, intLogGyo)          '' ｸﾞﾙｰﾌﾟﾘﾎﾟｰｽﾞ設定
        Call ChkDOTblProc(ch_no, strCH, intLogGyo)          '' DO設定
        Call ChkRunHourTblProc(ch_no, strCH, intLogGyo)     '' RUN HOUR設定
        Call ChkExhGasTblProc(ch_no, strCH, intLogGyo)      '' 排ｶﾞｽ設定
        Call ChkGraphTblProc(ch_no, strCH, intLogGyo)       '' ｸﾞﾗﾌ設定
        Call ChkLogTblProc(ch_no, strCH, intLogGyo)         '' ﾛｸﾞ設定  Ver1.7.7 2015.11.10 一旦ｺﾒﾝﾄ

        Call ChkSeqTblProc(ch_no, strCH, intLogGyo)         '' Seq設定
        Call ChkDataSaveTblProc(ch_no, strCH, intLogGyo)    '' DataSave設定
        Call ChkCtrlUseTblProc(ch_no, strCH, intLogGyo)     '' Control USE/NOT USE設定


        Call ChkMimicProc(ch_no, strCH, intLogGyo)          '' Mimicチェック

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : SIO関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkSIOSettingProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        For intPort As Integer = 1 To 9
            ' 送信ﾘｽﾄ　ｸﾘｱ
            With mudtSource.SetChSioCh(intPort - 1)
                For i As Integer = 0 To UBound(.udtSioChRec)

                    If .udtSioChRec(i).shtChNo = ch_no Then
                        strMsg = "    (*) SIO" & intPort & "　送信設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1
                        Exit For
                    End If
                Next
            End With
        Next
    End Sub


    '--------------------------------------------------------------------
    ' 機能      : OR関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkOrTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        For i As Integer = 0 To UBound(mudtSource.SetChAndOr.udtCHOut)

            For j As Integer = 0 To UBound(mudtSource.SetChAndOr.udtCHOut(i).udtCHAndOr)
                If ch_no = mudtSource.SetChAndOr.udtCHOut(i).udtCHAndOr(j).shtChid Then
                    strMsg = "    (*) OR AND TBL" & (i + 1) & " - " & (j + 1) & "　設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1
                End If
            Next
        Next
    End Sub

    '///////////////////////////////////////////////////
    ' Ver1.7.7  追加

    '--------------------------------------------------------------------
    ' 機能      : ｸﾞﾙｰﾌﾟﾘﾎﾟｰｽﾞ関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkGRTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String


        For i As Integer = 0 To UBound(mudtSource.SetChGroupRepose.udtRepose)
            If mudtSource.SetChGroupRepose.udtRepose(i).shtData = 0 Then      '' 標準設定
                If ch_no = mudtSource.SetChGroupRepose.udtRepose(i).shtChId Then
                    strMsg = "    (*) GR TBL" & (i + 1) & "　設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                End If
            Else
                For j As Integer = 0 To UBound(mudtSource.SetChGroupRepose.udtRepose(i).udtReposeInf)
                    If ch_no = mudtSource.SetChGroupRepose.udtRepose(i).udtReposeInf(j).shtChId Then
                        strMsg = "    (*) GR TBL" & (i + 1) & " - " & (j + 1) & "　設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            End If

        Next
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : DO関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkDOTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        Dim strFUno As String = ""
        Dim strFUport As String = ""
        Dim strFUpin As String = ""

        For i As Integer = 0 To UBound(mudtSource.SetChOutput.udtCHOutPut)
            If ch_no = mudtSource.SetChOutput.udtCHOutPut(i).shtChid Then
                Dim strFUadr As String = ""
                If mudtSource.SetChOutput.udtCHOutPut(i).bytFuno = 255 Then
                    strFUno = ""
                Else
                    strFUno = strFUadr & mudtSource.SetChOutput.udtCHOutPut(i).bytFuno
                End If
                If mudtSource.SetChOutput.udtCHOutPut(i).bytPortno = 255 Then
                    strFUport = ""
                Else
                    strFUport = strFUadr & mudtSource.SetChOutput.udtCHOutPut(i).bytPortno
                End If
                If mudtSource.SetChOutput.udtCHOutPut(i).bytPin = 255 Then
                    strFUpin = ""
                Else
                    strFUpin = strFUadr & mudtSource.SetChOutput.udtCHOutPut(i).bytPin
                End If

                strFUadr = strFUno & "-" & strFUport & "-" & strFUpin
                strMsg = "    (*) DO TBL" & (i + 1) & "(" & strFUadr & ")" & "　設定あり"
                msgtemp(intLogGyo) = strMsg
                intLogGyo = intLogGyo + 1

            End If
        Next

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : RUN HOUR関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkRunHourTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        With mudtSource.SetChRunHour

            For i As Integer = 0 To UBound(.udtDetail)
                If ch_no = .udtDetail(i).shtChid Then
                    strMsg = "    (*) RUN HOUR TBL" & (i + 1) & "　設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                ElseIf ch_no = .udtDetail(i).shtTrgChid Then
                    strMsg = "    (*) RUN HOUR TBL" & (i + 1) & "　ﾄﾘｶﾞ設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                End If
            Next
        End With
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 排ガス関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkExhGasTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        With mudtSource.SetChExhGus
            For i As Integer = 0 To UBound(.udtExhGusRec)
                If ch_no = .udtExhGusRec(i).shtAveChid Then
                    strMsg = "    (*) ExhGas TBL" & (i + 1) & "　Ave設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                ElseIf ch_no = .udtExhGusRec(i).shtRepChid Then
                    strMsg = "    (*) ExhGas TBL" & (i + 1) & "　Rep設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                End If

                For j As Integer = 0 To UBound(.udtExhGusRec(i).udtExhGusCyl)       '' ｼﾘﾝﾀﾞ設定
                    If ch_no = .udtExhGusRec(i).udtExhGusCyl(j).shtChid Then
                        strMsg = "    (*) ExhGas TBL" & (i + 1) & "　Cyl " & (j + 1) & " 設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next

                For k As Integer = 0 To UBound(.udtExhGusRec(i).udtExhGusDev)       '' 偏差CH設定
                    If ch_no = .udtExhGusRec(i).udtExhGusDev(k).shtChid Then
                        strMsg = "    (*) ExhGas TBL" & (i + 1) & "　Dev " & (k + 1) & " 設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            Next
        End With
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｸﾞﾗﾌ関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkGraphTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        With mudtSource.SetOpsGraphM
            For i = 0 To UBound(.udtGraphExhaustRec)   '' 排ｶﾞｽｸﾞﾗﾌ
                If ch_no = .udtGraphExhaustRec(i)._shtAveCh Then
                    strMsg = "    (*) Graph TBL" & (i + 1) & "　Ave ExhGraph設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                End If

                For j = 0 To UBound(.udtGraphExhaustRec(i).udtCylinder)     '' ｼﾘﾝﾀﾞｸﾞﾗﾌ設定
                    If ch_no = .udtGraphExhaustRec(i).udtCylinder(j).shtChCylinder Then
                        strMsg = "    (*) Graph TBL" & (i + 1) & "-" & (j + 1) & "　Cyl ExhGrap設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    ElseIf ch_no = .udtGraphExhaustRec(i).udtCylinder(j).shtChDeviation Then
                        strMsg = "    (*) Graph TBL" & (i + 1) & "-" & (j + 1) & "　Dev ExhGrap設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next

                For k = 0 To UBound(.udtGraphExhaustRec(i).udtTurboCharger)    '' T/C
                    If ch_no = .udtGraphExhaustRec(i).udtTurboCharger(k).shtChTurboCharger Then
                        strMsg = "    (*) Graph TBL" & (i + 1) & "-" & (k + 1) & "　T/C ExhGrap設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            Next

            For i = 0 To UBound(.udtGraphBarRec)   '' ﾊﾞｰｸﾞﾗﾌ設定
                For j = 0 To UBound(.udtGraphBarRec(i).udtCylinder)
                    If ch_no = .udtGraphBarRec(i).udtCylinder(j)._shtChCylinder Then
                        strMsg = "    (*) Graph TBL" & (i + 1) & "-" & (j + 1) & "　BarGraph設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            Next

            For i = 0 To UBound(.udtGraphAnalogMeterRec)   '' ｱﾅﾛｸﾞﾒｰﾀ設定
                For j = 0 To UBound(.udtGraphAnalogMeterRec(i).udtDetail)
                    If ch_no = .udtGraphAnalogMeterRec(i).udtDetail(j).shtChNo Then
                        strMsg = "    (*) Graph TBL" & (i + 1) & "-" & (j + 1) & "　AnalogMeter設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            Next
        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞ関連ﾁｪｯｸ
    '               Ver1.7.8 2015.11,12 変更
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkLogTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)


        With mudtSource.SetOpsLogFormatM
            For i As Integer = 0 To UBound(.strCol1)
                If Trim(.strCol1(i)) <> "" Then
                    Call ChkLog(ch_no, Trim(.strCol1(i)), strCH, intLogGyo)
                End If
            Next

            For i As Integer = 0 To UBound(.strCol2)
                If Trim(.strCol2(i)) <> "" Then
                    Call ChkLog(ch_no, Trim(.strCol2(i)), strCH, intLogGyo)
                End If
            Next

        End With

    End Sub


    '///////////////////////////////////////////////////
    ' Ver1.7.8  追加

    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞ関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkLog(ByVal ch_no As Integer, ByVal strSet As String, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String
        Dim strTemp As String

        If strSet.Substring(0, 2) = "CH" Then       '' 先頭2文字がCHになっているもののみﾁｪｯｸ
            strTemp = strSet.Substring(2)
            If IsNumeric(strTemp) And ch_no = CInt(strTemp) Then    '' 後ろ4文字が数値ならばﾁｪｯｸ
                strMsg = "    (*) LOG TBL　設定あり"
                msgtemp(intLogGyo) = strMsg
                intLogGyo = intLogGyo + 1

            End If
        End If

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｼｰｹﾝｽ関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkSeqTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        'Ver2.0.6.5 CHno表示対応
        Dim strInOutCHNO As String = ""

        With mudtSource.SetSeqSet
            For i As Integer = 0 To UBound(.udtDetail)
                If ch_no = .udtDetail(i).shtOutChid Then
                    strMsg = "    (*) Seq TBL" & (i + 1) & "　OUT 設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                    'Ver2.0.6.5 CHno表示対応
                    'CHno表示対応
                    strInOutCHNO = .udtDetail(i).shtOutChid.ToString("0000")
                    strMsg = "        " & strInOutCHNO
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1
                Else
                    For j As Integer = 0 To UBound(.udtDetail(i).udtInput)
                        If ch_no = .udtDetail(i).udtInput(j).shtChid Then
                            strMsg = "    (*) Seq TBL" & (i + 1) & "　IN 設定あり"
                            msgtemp(intLogGyo) = strMsg
                            intLogGyo = intLogGyo + 1

                            'Ver2.0.6.5 CHno表示対応
                            'CHno表示対応
                            strInOutCHNO = ""
                            For z As Integer = 0 To UBound(.udtDetail(i).udtInput) Step 1
                                If .udtDetail(i).udtInput(z).shtChid <= 0 Then
                                    strInOutCHNO = strInOutCHNO & "----" & ","
                                Else
                                    strInOutCHNO = strInOutCHNO & .udtDetail(i).udtInput(z).shtChid.ToString("0000") & ","
                                End If
                            Next z
                            strInOutCHNO = strInOutCHNO.TrimEnd(CType(",", Char))
                            strMsg = "        " & strInOutCHNO
                            msgtemp(intLogGyo) = strMsg
                            intLogGyo = intLogGyo + 1
                        End If
                    Next
                End If

                'Ver2.0.7.W ロジック内の演算式テーブル等もCHNo検索
                Call ChkSeqSetProc(ch_no, i, intLogGyo)
            Next i

        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ﾃﾞｰﾀ保存関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkDataSaveTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        With mudtSource.SetChDataSave
            For i As Integer = 0 To UBound(.udtDetail)
                If ch_no = .udtDetail(i).shtChid Then
                    strMsg = "    (*) DataSave TBL" & (i + 1) & "　設定あり"
                    msgtemp(intLogGyo) = strMsg
                    intLogGyo = intLogGyo + 1

                End If
            Next

        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｺﾝﾄﾛｰﾙUSE/NOt USE関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkCtrlUseTblProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)

        Dim strMsg As String

        With mudtSource.SetChCtrlUseM
            For i As Integer = 0 To UBound(.udtCtrlUseNotuseRec)
                For j As Integer = 0 To UBound(.udtCtrlUseNotuseRec(i).udtUseNotuseDetails)
                    If ch_no = .udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno Then
                        strMsg = "    (*) Control USE/NOT USE TBL" & (i + 1) & "　設定あり"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1

                    End If
                Next
            Next

        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Mimic関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkMimicProc(ByVal ch_no As Integer, ByVal strCH As String, ByRef intLogGyo As Integer)
        Dim strMsg As String

        Dim i As Integer = 0
        Dim j As Integer = 0

        Try
            Dim strPathBase As String = ""
            'パス生成
            'Ver2.0.5.0 参照パスはSource=旧ﾌｧｲﾙパス
            strPathBase = System.IO.Path.Combine(mudtFileSource.strFilePath, mudtFileSource.strFileName)
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameMimic)
            strPathBase = System.IO.Path.Combine(strPathBase, "Mimic1")

            'Mimic1フォルダが存在しない場合処理抜け
            If System.IO.Directory.Exists(strPathBase) = False Then
                Return
            End If

            'MimicファイルS02*.mim　を取得する
            Dim strMimicFiles As String() = System.IO.Directory.GetFiles(strPathBase, "S02*.mim", System.IO.SearchOption.AllDirectories)

            Dim pRet As IntPtr
            Dim strRet As String = ""
            Dim strPath As String = ""
            For i = 0 To UBound(strMimicFiles) Step 1
                strPath = strMimicFiles(i)
                Dim strFileName As String = System.IO.Path.GetFileName(strPath)
                Dim bytesData As Byte()
                'Shift JISとして文字列に変換
                bytesData = System.Text.Encoding.GetEncoding(932).GetBytes(strPath)
                strPath = System.Text.Encoding.GetEncoding(932).GetString(bytesData)
                'DLLコール
                'strRet = mainProc(strPath, pintCHNo)
                pRet = mainProc(strPath, ch_no)
                strRet = Marshal.PtrToStringAnsi(pRet)

                If strRet.Trim <> "" Then
                    strRet = strRet.Replace(vbLf, "")
                    Dim strRet2() As String = strRet.Split(vbCr)
                    For j = 0 To UBound(strRet2) - 1 Step 1
                        strMsg = "    (*) " & strRet2(j)
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1
                    Next j
                End If
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    'Ver2.0.7.W
    '--------------------------------------------------------------------
    ' 機能      : 演算式テーブル等ロジックで使用されるその他CHNo関連ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : ch_no   CHNo.
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub ChkSeqSetProc(ByVal ch_no As Integer, ByVal pintDtl As Integer, ByRef intLogGyo As Integer)

        Dim i As Integer = 0
        Dim strMsg As String = ""
        strMsg = strMsg & "    (*) Seq TBL" & (pintDtl + 1)

        Dim strMsgtemp As String = ""

        Dim intRet As Integer


        With mudtSource.SetSeqSet.udtDetail(pintDtl)
            'ロジックの種類によってチェックポイントが異なるため分岐
            Select Case .shtLogicType
                Case 16, 28, 35, 49
                    '[16]calculate logic, [28]Integer Calculate Logic
                    '[35]methanol control, [49]IDUTSU1181 M/E PUMP CONTROL
                    '1項目目が演算式テーブル番号
                    intRet = ChkSeqCalcProc(ch_no, .shtLogicItem(0))
                    If intRet >= 0 Then
                        strMsg = strMsg & " - Calc " & .shtLogicItem(0).ToString & "-" & (intRet + 1).ToString
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1
                    End If
                Case 19, 22, 29, 30, 31, 32, 45, 48
                    '[19]logic pulse count, [22]Clear Running hour
                    '[29]Valve(AI-DO), [30]Valve(AI-AO), [31]Valve(DI-DO), [32]Motor(Input-Output)
                    '[45]PID CONTROLER, [48]N2086 MODE SET
                    '1項目目がCHNo
                    If ch_no = .shtLogicItem(0) Then
                        strMsg = strMsg & " Logic Set 1"
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1
                    End If
                Case 39, 50
                    '[39](NACKS NE231)Heel Control Seq
                    '[50]IDUTSU1181 AUTO BALLANCE
                    '1,2,3,4項目目が演算式テーブル番号
                    For i = 0 To 3 Step 1
                        intRet = ChkSeqCalcProc(ch_no, .shtLogicItem(i))
                        If intRet >= 0 Then
                            strMsgtemp = strMsgtemp & " - Calc " & .shtLogicItem(i).ToString & "-" & (intRet + 1).ToString
                        End If
                    Next i
                    If strMsgtemp <> "" Then
                        strMsg = strMsg & strMsgtemp
                        msgtemp(intLogGyo) = strMsg
                        intLogGyo = intLogGyo + 1
                    End If

            End Select

        End With
        
    End Sub
    '演算式テーブルに該当CHNoがあるか探す
    Private Function ChkSeqCalcProc(ByVal ch_no As Integer, ByVal pintI As Integer) As Integer
        Dim intValue As Integer

        If pintI - 1 < 0 Then
            Return -1
        End If

        For j As Integer = 0 To UBound(mudtSource.SetSeqOpeExp.udtTables(pintI - 1).udtAryInf)
            With mudtSource.SetSeqOpeExp.udtTables(pintI - 1).udtAryInf(j)
                Select Case .shtType
                    'タイプが、CHNoを含む物だけ
                    Case gCstCodeSeqFixTypeChData _
                        , gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet _
                        , gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                        'ChNo格納
                        intValue = gConnect2Byte(.bytInfo(2), .bytInfo(3))
                        '比較
                        If ch_no = intValue Then
                            Return j
                        End If
                End Select
            End With
        Next

        Return -1
    End Function
    '-


#End Region

#End Region

#Region "表示変換処理"

    '--------------------------------------------------------------------
    ' 機能      : アナログステータス変更処理（共通）
    ' 返り値    : なし
    ' 引き数    :    
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetChStatus(ByVal shtStData As Short) As String

        Dim strStatus As String

        If shtStData = &HFF Then
            strStatus = ""
        Else
            'Ver2.0.7.M (保安庁)
            If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                Select Case shtStData
                    Case &H0
                        strStatus = "*"
                    Case &H1
                        strStatus = "入/切"
                    Case &H2
                        strStatus = "遮断/開"
                    Case &H3
                        strStatus = "閉/開"
                    Case &H4
                        strStatus = "正常/異常"
                    Case &H5
                        strStatus = "正常/故障"
                    Case &H6
                        strStatus = "正常/解除"
                    Case &H8
                        strStatus = "正常/高"
                    Case &H9
                        strStatus = "正常/低"
                    Case &HA
                        strStatus = "正常/低低"
                    Case &HB
                        strStatus = "正常/高高"
                    Case &HC
                        strStatus = "正常/閉"
                    Case &HD
                        strStatus = "正常/喪失"
                    Case &HE
                        strStatus = "USE/NO USE"
                    Case &HF
                        strStatus = "DO/HFO"
                    Case &H10
                        strStatus = "DISEN/ENGA"
                    Case &H11
                        strStatus = "実行/停止"
                    Case &H12
                        strStatus = "始動/停止"
                    Case &H13
                        strStatus = "開/閉"
                    Case &H14
                        strStatus = "運転/停止"
                    Case &HA0
                        strStatus = "入/*"
                    Case &HA1
                        strStatus = "開/*"
                    Case &HA2
                        strStatus = "遮断/*"
                    Case &HA3
                        strStatus = "喪失/*"
                    Case &HA4
                        strStatus = "正常/*"
                    Case &HA5
                        strStatus = "異常/*"
                    Case &HA6
                        strStatus = "FAIL/*"
                    Case &HA7
                        strStatus = "USE/*"
                    Case &HA8
                        strStatus = "自動/*"
                    Case &HA9
                        strStatus = "当直/*"
                    Case &HAA
                        strStatus = "解除/*"
                    Case &HAB
                        strStatus = "準備/*"
                    Case &HAC
                        strStatus = "COND/*"
                    Case &HAD
                        strStatus = "消去/*"
                    Case &HAE
                        strStatus = "運転/*"
                    Case &HAF
                        strStatus = "BLOCK/SERV"
                    Case &HB0
                        strStatus = "*/低"
                    Case &HB1
                        strStatus = "*/高"
                    Case &HB2
                        strStatus = "PASS/*"
                    Case &HB3
                        strStatus = "OK/*"
                    Case &HB4
                        strStatus = "停止/*"
                    Case &HB5
                        strStatus = "閉/*"
                    Case &H30
                        strStatus = "RUN"
                    Case &H31
                        strStatus = "RUN-A"
                    Case &H32
                        strStatus = "RUN-B"
                    Case &H33
                        strStatus = "RUN-C"
                    Case &H34
                        strStatus = "RUN-D"
                    Case &H35
                        strStatus = "RUN-E"
                    Case &H36
                        strStatus = "RUN-F"
                    Case &H37
                        strStatus = "RUN-G"
                    Case &H38
                        strStatus = "RUN-H"
                    Case &H39
                        strStatus = "RUN-I"
                    Case &H3A
                        strStatus = "RUN-J"
                    Case &H3B
                        strStatus = "RUN-K"
                    Case &H3A
                        strStatus = "*"
                    Case &H41
                        strStatus = "正常/高"
                    Case &H42
                        strStatus = "正常/低"
                    Case &H43
                        strStatus = "低/正常/高"
                    Case &H44
                        strStatus = "正常/高高"
                    Case &H45
                        strStatus = "正常/低低"
                    Case &H46
                        strStatus = "低低/正常/高高"
                    Case &H47
                        strStatus = "NOR/EH"
                    Case &H48
                        strStatus = "NOR/EL"
                    Case &H49
                        strStatus = "EL/NOR/EH"
                    Case &H40
                        strStatus = "*"
                    Case &H41
                        strStatus = "正常/高"
                    Case Else
                        strStatus = ""
                End Select
            Else
                Select Case shtStData
                    Case &H0
                        strStatus = "*"
                    Case &H1
                        strStatus = "ON/OFF"
                    Case &H2
                        strStatus = "SHUT/OPEN"
                    Case &H3
                        strStatus = "CLOSE/OPEN"
                    Case &H4
                        strStatus = "NOR/ABNOR"
                    Case &H5
                        strStatus = "NOR/TRBL"
                    Case &H6
                        strStatus = "NOR/CANCL"
                    Case &H8
                        strStatus = "NOR/HIGH"
                    Case &H9
                        strStatus = "NOR/LOW"
                    Case &HA
                        strStatus = "NOR/LL"
                    Case &HB
                        strStatus = "NOR/HH"
                    Case &HC
                        strStatus = "NOR/CLOSE"
                    Case &HD
                        strStatus = "NOR/TRIP"
                    Case &HE
                        strStatus = "USE/NO USE"
                    Case &HF
                        strStatus = "DO/HFO"
                    Case &H10
                        strStatus = "DISEN/ENGA"
                    Case &H11
                        strStatus = "ACT/STOP"
                    Case &H12
                        strStatus = "START/STOP"
                    Case &H13
                        strStatus = "OPEN/CLOSE"
                    Case &H14
                        strStatus = "RUN/STOP"
                    Case &HA0
                        strStatus = "ON/*"
                    Case &HA1
                        strStatus = "OPEN/*"
                    Case &HA2
                        strStatus = "SHUT/*"
                    Case &HA3
                        strStatus = "TRIP/*"
                    Case &HA4
                        strStatus = "NOR/*"
                    Case &HA5
                        strStatus = "ABNOR/*"
                    Case &HA6
                        strStatus = "FAIL/*"
                    Case &HA7
                        strStatus = "USE/*"
                    Case &HA8
                        strStatus = "AUTO/*"
                    Case &HA9
                        strStatus = "DUTY/*"
                    Case &HAA
                        strStatus = "CANCL/*"
                    Case &HAB
                        strStatus = "READY/*"
                    Case &HAC
                        strStatus = "COND/*"
                    Case &HAD
                        strStatus = "CLEAR/*"
                    Case &HAE
                        strStatus = "RUN/*"
                    Case &HAF
                        strStatus = "BLOCK/SERV"
                    Case &HB0
                        strStatus = "*/LOW"
                    Case &HB1
                        strStatus = "*/HIGH"
                    Case &HB2
                        strStatus = "PASS/*"
                    Case &HB3
                        strStatus = "OK/*"
                    Case &HB4
                        strStatus = "STOP/*"
                    Case &HB5
                        strStatus = "CLOSE/*"
                    Case &H30
                        strStatus = "RUN"
                    Case &H31
                        strStatus = "RUN-A"
                    Case &H32
                        strStatus = "RUN-B"
                    Case &H33
                        strStatus = "RUN-C"
                    Case &H34
                        strStatus = "RUN-D"
                    Case &H35
                        strStatus = "RUN-E"
                    Case &H36
                        strStatus = "RUN-F"
                    Case &H37
                        strStatus = "RUN-G"
                    Case &H38
                        strStatus = "RUN-H"
                    Case &H39
                        strStatus = "RUN-I"
                    Case &H3A
                        strStatus = "RUN-J"
                    Case &H3B
                        strStatus = "RUN-K"
                    Case &H3A
                        strStatus = "*"
                    Case &H41
                        strStatus = "NOR/HIGH"
                    Case &H42
                        strStatus = "NOR/LOW"
                    Case &H43
                        strStatus = "LOW/NOR/HIGH"
                    Case &H44
                        strStatus = "NOR/HH"
                    Case &H45
                        strStatus = "NOR/LL"
                    Case &H46
                        strStatus = "LL/NOR/HH"
                    Case &H47
                        strStatus = "NOR/EH"
                    Case &H48
                        strStatus = "NOR/EL"
                    Case &H49
                        strStatus = "EL/NOR/EH"
                    Case &H40
                        strStatus = "*"
                    Case &H41
                        strStatus = "NOR/HIGH"
                    Case Else
                        strStatus = ""
                End Select
            End If
        End If

        Return strStatus

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ユニット単位変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetCHUnit(ByVal shtUnit As Short) As String
        Dim strUnit As String

        Select Case shtUnit
            Case &H0
                strUnit = "*"
            Case &H1
                strUnit = "ﾟC"
            Case &H2
                strUnit = "kg"
            Case &H3
                strUnit = "MPa"
            Case &H4
                strUnit = "bar"
            Case &H5
                strUnit = "mmHg"
            Case &H6
                strUnit = "mmAq"
            Case &H7
                strUnit = "rpm"
            Case &H8
                strUnit = "10rpm"
            Case &H9
                strUnit = "m3"
            Case &H10        '' Ver1.12.0.1 2017.01.05  &HA → &H10
                strUnit = "L"
            Case &H11       '' Ver1.12.0.1 2017.01.05  &HB → &H11
                strUnit = "10L"
            Case &H12       '' Ver1.12.0.1 2017.01.05  &HC → &H12
                strUnit = "100L"
            Case &H13       '' Ver1.12.0.1 2017.01.05  &HD → &H13
                strUnit = "kL"
            Case &H14        '' Ver1.12.0.1 2017.01.05  &HE → &H14
                strUnit = "REV"
            Case &H15        '' Ver1.12.0.1 2017.01.05  &HF → &H15
                strUnit = "10REV"
            Case &H16       '' Ver1.12.0.1 2017.01.05  &H10 → &H16
                strUnit = "N"
            Case &H17       '' Ver1.12.0.1 2017.01.05  &H11 → &H17
                strUnit = "DEG"
            Case &H18       '' Ver1.12.0.1 2017.01.05  &H12 → &H18
                strUnit = "%"
            Case &H19       '' Ver1.12.0.1 2017.01.05  &H13 → &H19
                strUnit = "V"
            Case &H20       '' Ver1.12.0.1 2017.01.05  &H14 → &H20
                strUnit = "A"
            Case &H21       '' Ver1.12.0.1 2017.01.05  &H15 → &H21
                strUnit = "kW"
            Case &H22       '' Ver1.12.0.1 2017.01.05  &H16 → &H22
                strUnit = "Hz"
            Case &H23       '' Ver1.12.0.1 2017.01.05  &H17 → &H23
                strUnit = "PS"
            Case &H24       '' Ver1.12.0.1 2017.01.05  &H18 → &H24
                strUnit = "10PS"
            Case &H25       '' Ver1.12.0.1 2017.01.05  &H19 → &H25
                strUnit = "100PS"
            Case &H26       '' Ver1.12.0.1 2017.01.05  &H1A → &H26
                strUnit = "cSt"
            Case &H27       '' Ver1.12.0.1 2017.01.05  &H1B → &H27
                strUnit = "nm"
            Case &H28       '' Ver1.12.0.1 2017.01.05  &H1C → &H28
                strUnit = "ppm"
            Case &H29       '' Ver1.12.0.1 2017.01.05  &H1D → &H29
                strUnit = "sec"
            Case &H30       '' Ver1.12.0.1 2017.01.05  &H1E → &H30
                strUnit = "min"
            Case &H31       '' Ver1.12.0.1 2017.01.05  &H1F → &H31
                strUnit = "hour"
            Case &H32       '' Ver1.12.0.1 2017.01.05  &H20 → &H32
                strUnit = "HR.M"
            Case &H33       '' Ver1.12.0.1 2017.01.05  &H21 → &H33
                strUnit = "mm"
            Case &H34       '' Ver1.12.0.1 2017.01.05  &H22 → &H34
                strUnit = "cm"
            Case &H35       '' Ver1.12.0.1 2017.01.05  &H23 → &H35
                strUnit = "m"
            Case Else
                strUnit = ""
        End Select

        Return strUnit

    End Function

    '--------------------------------------------------------------------
    ' 機能      : LRモード変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetMode(ByVal shtMode As UShort) As String

        Dim strMode As String = ""

        Select Case shtMode
            Case 0
                strMode = "NO SETTING"
            Case 1
                strMode = "EMG ALARM"
            Case 2
                strMode = "ALARM"
            Case 3
                strMode = "WARNING"
            Case 4
                strMode = "CAUTION"
            Case Else
                strMode = "Error"
        End Select

        Return strMode

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビット変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetBitCHK(ByVal Content As String, ByVal shtOldBitValue As UShort, ByVal i As Integer, ByVal shtNewBitValue As UShort, ByVal x As Integer, ByVal Gyo As Integer) As Integer

        Dim strOldBitValue As String = ""
        Dim strNewBitValue As String = ""

        Dim BitFlg As Boolean = False

        If gBitCheck(shtOldBitValue, i) <> gBitCheck(shtNewBitValue, x) Then

            If gBitCheck(shtOldBitValue, i) = True Then
                strOldBitValue = "o"
            Else
                strOldBitValue = " "
            End If

            If gBitCheck(shtNewBitValue, x) = True Then
                strNewBitValue = "o"
            Else
                strNewBitValue = " "
            End If

            BitFlg = True

        End If

        If BitFlg = True Then
            msgtemp(Gyo) = mMsgCreateStr(Content, strOldBitValue, strNewBitValue)
            Gyo = Gyo + 1
        End If

        GetBitCHK = Gyo

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetFUAddress(ByVal shtAdd As UShort, ByVal Type As Integer) As String

        Dim strFUNo As String = ""
        Dim strFUSlot As String = ""
        Dim strFUPin As String = ""
        Dim strFUAddress As String = ""

        If shtAdd = &HFFFF Then
            Return strFUAddress
            Exit Function
        End If

        'Type : 1→No, 2→Slot, 3→Pin
        Select Case Type

            Case 1 'FUNo
                If shtAdd = 0 Then
                    strFUAddress = "FCU"
                Else
                    strFUAddress = "FU" & Trim(Str(shtAdd))
                End If

            Case 2 'Slot
                strFUAddress = Trim(Str(shtAdd))

            Case 3 'Pin
                strFUAddress = Trim(Str(shtAdd))

        End Select

        Return strFUAddress

    End Function


    'Ver2.0.0.7
    '--------------------------------------------------------------------
    ' 機能      : FUアドレス生成処理(端子表比較専用)
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetFUAdrTer(ByVal pintFU As Integer, pintPort As Integer, pintPin As Integer) As String

        Dim strFUNo As String = ""
        Dim strFUSlot As String = ""
        Dim strFUPin As String = ""
        Dim strFUAddress As String = ""

        '>>>FU No作成
        If pintFU = 0 Then
            strFUNo = "FCU"
        Else
            strFUNo = "FU" & Trim(Str(pintFU))
        End If

        '>>>FU Slot作成
        If pintPort < 0 Then
            strFUSlot = ""
        Else
            strFUSlot = "-" & Trim(Str(pintPort))
        End If

        '>>>FU Pin作成
        If pintPin < 0 Then
            strFUPin = ""
        Else
            strFUPin = "-" & Trim(Str(pintPin))
        End If

        '>>>FUadr生成
        strFUAddress = strFUNo & strFUSlot & strFUPin


        Return strFUAddress

    End Function


    '--------------------------------------------------------------------
    ' 機能      : レンジ変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    ''              Ver1.11.5 2016.08.30  設定なし時条件追加
    ''              Ver1.11.6 2016.09.15  ﾀﾞﾐｰﾁｪｯｸ追加
    '--------------------------------------------------------------------
    Private Function GetRange(ByVal Content As String, ByVal intOldRangeH As Integer, ByVal intNewRangeH As Integer, _
                              ByVal intOldRangeL As Integer, ByVal intNewRangeL As Integer, ByVal OldDeciPosi As UShort, ByVal NewDeciPosi As UShort, _
                              ByVal OldDummyRangeScale As Boolean, ByVal NewDummyRangeScale As Boolean, ByVal Gyo As Integer) As Integer

        Dim strOldRangeH As String
        Dim strNewRangeH As String
        Dim strOldRangeL As String
        Dim strNewRangeL As String

        Dim ScaleFlg As Boolean = False

        Dim OldRangeScale As String = ""
        Dim NewRangeScale As String = ""

        Dim strOldDummy As String = ""
        Dim strNewDummy As String = ""

        If intOldRangeL = &H80000000 Then       '' 設定なし時
            strOldRangeL = ""
        Else
            strOldRangeL = GetNumFormat(intOldRangeL, OldDeciPosi)
        End If

        If intOldRangeH = &H80000000 Then       '' 設定なし時
            strOldRangeH = ""
        Else
            strOldRangeH = GetNumFormat(intOldRangeH, OldDeciPosi)
        End If

        If strOldRangeL = "" And strOldRangeH = "" Then   '' L/Hとも設定なし時
            OldRangeScale = " "
        Else
            OldRangeScale = strOldRangeL & "/" & strOldRangeH
        End If

        If intNewRangeL = &H80000000 Then       '' 設定なし時
            strNewRangeL = ""
        Else
            strNewRangeL = GetNumFormat(intNewRangeL, NewDeciPosi)
        End If

        If intNewRangeH = &H80000000 Then       '' 設定なし時
            strNewRangeH = ""
        Else
            strNewRangeH = GetNumFormat(intNewRangeH, NewDeciPosi)
        End If

        If strNewRangeL = "" And strNewRangeH = "" Then   '' L/Hとも設定なし時
            NewRangeScale = " "
        Else
            NewRangeScale = strNewRangeL & "/" & strNewRangeH
        End If

        If intOldRangeH <> intNewRangeH Then
            ScaleFlg = True
        End If

        If intOldRangeL <> intNewRangeL Then
            ScaleFlg = True
        End If

        '' Ver1.11.5 2016.08.30 小数点以下桁数が変わった場合も表示する
        If OldDeciPosi <> NewDeciPosi Then
            ScaleFlg = True
        End If

        '' Ver1.11.6 2016.09.15  ﾀﾞﾐｰﾁｪｯｸ統合  
        If Content = "RANGE" Then
            If OldDummyRangeScale <> NewDummyRangeScale Then
                ScaleFlg = True
            End If

            If OldDummyRangeScale = True Then
                strOldDummy = "#"
            End If

            If NewDummyRangeScale = True Then
                strNewDummy = "#"
            End If
        End If

        'Ver2.0.1.9 Rangeが空白ならば、小数点もﾀﾞﾐｰも無視
        If OldRangeScale = " " And NewRangeScale = " " Then
            ScaleFlg = False
        End If

        If ScaleFlg = True Then
            '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾏｰｸ追加
            msgtemp(Gyo) = mMsgCreateStr(Content, strOldDummy & OldRangeScale, strNewDummy & NewRangeScale)
            Gyo = Gyo + 1
        End If

        GetRange = Gyo

    End Function
    Private Function GetNorRange(ByVal Content As String, ByVal intOldRangeH As Integer, ByVal intNewRangeH As Integer, _
                              ByVal intOldRangeL As Integer, ByVal intNewRangeL As Integer, ByVal OldDeciPosi As UShort, ByVal NewDeciPosi As UShort, _
                              ByVal OldDummyHiRangeScale As Boolean, ByVal NewDummyHiRangeScale As Boolean _
                              , ByVal OldDummyLoRangeScale As Boolean, ByVal NewDummyLoRangeScale As Boolean, ByVal Gyo As Integer) As Integer

        Dim strOldRangeH As String
        Dim strNewRangeH As String
        Dim strOldRangeL As String
        Dim strNewRangeL As String

        Dim ScaleFlg As Boolean = False

        Dim OldRangeScale As String = ""
        Dim NewRangeScale As String = ""

        Dim strOldDummy As String = ""
        Dim strNewDummy As String = ""

        Dim strOldDmyHi As String = IIf(OldDummyHiRangeScale, "#", "")
        Dim strOldDmyLo As String = IIf(OldDummyLoRangeScale, "#", "")
        Dim strNewDmyHi As String = IIf(NewDummyHiRangeScale, "#", "")
        Dim strNewDmyLo As String = IIf(NewDummyLoRangeScale, "#", "")

        If intOldRangeL = &H80000000 Then       '' 設定なし時
            strOldRangeL = ""
        Else
            strOldRangeL = GetNumFormat(intOldRangeL, OldDeciPosi)
        End If

        If intOldRangeH = &H80000000 Then       '' 設定なし時
            strOldRangeH = ""
        Else
            strOldRangeH = GetNumFormat(intOldRangeH, OldDeciPosi)
        End If

        If strOldRangeL = "" And strOldRangeH = "" Then   '' L/Hとも設定なし時
            OldRangeScale = " "
        Else
            OldRangeScale = strOldDmyLo & strOldRangeL & "/" & strOldDmyHi & strOldRangeH
        End If

        If intNewRangeL = &H80000000 Then       '' 設定なし時
            strNewRangeL = ""
        Else
            strNewRangeL = GetNumFormat(intNewRangeL, NewDeciPosi)
        End If

        If intNewRangeH = &H80000000 Then       '' 設定なし時
            strNewRangeH = ""
        Else
            strNewRangeH = GetNumFormat(intNewRangeH, NewDeciPosi)
        End If

        If strNewRangeL = "" And strNewRangeH = "" Then   '' L/Hとも設定なし時
            NewRangeScale = " "
        Else
            NewRangeScale = strNewDmyLo & strNewRangeL & "/" & strNewDmyHi & strNewRangeH
        End If

        If intOldRangeH <> intNewRangeH Then
            ScaleFlg = True
        End If

        If intOldRangeL <> intNewRangeL Then
            ScaleFlg = True
        End If

        '' Ver1.11.5 2016.08.30 小数点以下桁数が変わった場合も表示する
        If OldDeciPosi <> NewDeciPosi Then
            ScaleFlg = True
        End If

        '' Ver1.11.6 2016.09.15  ﾀﾞﾐｰﾁｪｯｸ統合  
        If Content = "RANGE" Then
            If OldDummyHiRangeScale <> NewDummyHiRangeScale Or _
                OldDummyLoRangeScale <> NewDummyLoRangeScale Then
                ScaleFlg = True
            End If
        End If

        'Ver2.0.1.9 Rangeが空白ならば、小数点もﾀﾞﾐｰも無視
        If OldRangeScale = " " And NewRangeScale = " " Then
            ScaleFlg = False
        End If

        If ScaleFlg = True Then
            '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾏｰｸ追加
            msgtemp(Gyo) = mMsgCreateStr(Content, strOldDummy & OldRangeScale, strNewDummy & NewRangeScale)
            Gyo = Gyo + 1
        End If

        GetNorRange = Gyo

    End Function

    'アナログ専用、レンジ文字生成処理
    Private Function GetRangeAnalog(ByVal Content As String, pudt1 As gTypSetChRec, pudt2 As gTypSetChRec, ByVal intOldRangeH As Integer, ByVal intNewRangeH As Integer, _
                              ByVal intOldRangeL As Integer, ByVal intNewRangeL As Integer, ByVal OldDeciPosi As UShort, ByVal NewDeciPosi As UShort, _
                              ByVal OldDummyRangeScale As Boolean, ByVal NewDummyRangeScale As Boolean, ByVal Gyo As Integer) As Integer

        Dim strOldRangeH As String
        Dim strNewRangeH As String
        Dim strOldRangeL As String
        Dim strNewRangeL As String

        Dim ScaleFlg As Boolean = False

        Dim OldRangeScale As String = ""
        Dim NewRangeScale As String = ""

        Dim strOldDummy As String = ""
        Dim strNewDummy As String = ""

        Dim blOldPT As Boolean = False
        Dim blNewPT As Boolean = False
        Dim strTemp As String = ""


        If pudt1.udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
               pudt1.udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then
            blOldPT = False
        End If
        If pudt2.udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
               pudt2.udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then
            blNewPT = False
        End If

        If intOldRangeL = &H80000000 Then       '' 設定なし時
            strOldRangeL = ""
        Else
            If blOldPT = True Then
                'Ver2.0.7.D 諸処理により、温度基板も通常の処置と同じとなるため、Trueの処理は起きない
                'PTの場合は、小数点を後ろにつけるだけ
                strTemp = ""
                For i = 0 To OldDeciPosi - 1 Step 1
                    strTemp = strTemp & "0"
                Next i
                If strTemp <> "" Then
                    strTemp = "0." & strTemp
                End If
                strOldRangeL = intOldRangeL.ToString(strTemp)
            Else
                strOldRangeL = GetNumFormat(intOldRangeL, OldDeciPosi)
            End If
        End If

        If intOldRangeH = &H80000000 Then       '' 設定なし時
            strOldRangeH = ""
        Else
            If blOldPT = True Then
                'Ver2.0.7.D 諸処理により、温度基板も通常の処置と同じとなるため、Trueの処理は起きない
                'PTの場合は、小数点を後ろにつけるだけ
                strTemp = ""
                For i = 0 To OldDeciPosi - 1 Step 1
                    strTemp = strTemp & "0"
                Next i
                If strTemp <> "" Then
                    strTemp = "0." & strTemp
                End If
                strOldRangeH = intOldRangeH.ToString(strTemp)
            Else
                strOldRangeH = GetNumFormat(intOldRangeH, OldDeciPosi)
            End If
        End If

        If strOldRangeL = "" And strOldRangeH = "" Then   '' L/Hとも設定なし時
            OldRangeScale = " "
        Else
            OldRangeScale = strOldRangeL & "/" & strOldRangeH
        End If

        If intNewRangeL = &H80000000 Then       '' 設定なし時
            strNewRangeL = ""
        Else
            If blNewPT = True Then
                'Ver2.0.7.D 諸処理により、温度基板も通常の処置と同じとなるため、Trueの処理は起きない
                'PTの場合は、小数点を後ろにつけるだけ
                strTemp = ""
                For i = 0 To NewDeciPosi - 1 Step 1
                    strTemp = strTemp & "0"
                Next i
                If strTemp <> "" Then
                    strTemp = "0." & strTemp
                End If
                strNewRangeL = intNewRangeL.ToString(strTemp)
            Else
                strNewRangeL = GetNumFormat(intNewRangeL, NewDeciPosi)
            End If
        End If

        If intNewRangeH = &H80000000 Then       '' 設定なし時
            strNewRangeH = ""
        Else
            If blNewPT = True Then
                'Ver2.0.7.D 諸処理により、温度基板も通常の処置と同じとなるため、Trueの処理は起きない
                'PTの場合は、小数点を後ろにつけるだけ
                strTemp = ""
                For i = 0 To NewDeciPosi - 1 Step 1
                    strTemp = strTemp & "0"
                Next i
                If strTemp <> "" Then
                    strTemp = "0." & strTemp
                End If
                strNewRangeH = intNewRangeH.ToString(strTemp)
            Else
                strNewRangeH = GetNumFormat(intNewRangeH, NewDeciPosi)
            End If
        End If

        If strNewRangeL = "" And strNewRangeH = "" Then   '' L/Hとも設定なし時
            NewRangeScale = " "
        Else
            NewRangeScale = strNewRangeL & "/" & strNewRangeH
        End If

        If intOldRangeH <> intNewRangeH Then
            ScaleFlg = True
        End If

        If intOldRangeL <> intNewRangeL Then
            ScaleFlg = True
        End If

        '' Ver1.11.5 2016.08.30 小数点以下桁数が変わった場合も表示する
        If OldDeciPosi <> NewDeciPosi Then
            ScaleFlg = True
        End If

        '' Ver1.11.6 2016.09.15  ﾀﾞﾐｰﾁｪｯｸ統合  
        If Content = "RANGE" Then
            If OldDummyRangeScale <> NewDummyRangeScale Then
                ScaleFlg = True
            End If

            If OldDummyRangeScale = True Then
                strOldDummy = "#"
            End If

            If NewDummyRangeScale = True Then
                strNewDummy = "#"
            End If
        End If

        'Ver2.0.1.9 Rangeが空白ならば、小数点もﾀﾞﾐｰも無視
        If OldRangeScale = " " And NewRangeScale = " " Then
            ScaleFlg = False
        End If

        If ScaleFlg = True Then
            '' Ver1.11.6 2016.09.15 ﾀﾞﾐｰﾏｰｸ追加
            msgtemp(Gyo) = mMsgCreateStr(Content, strOldDummy & OldRangeScale, strNewDummy & NewRangeScale)
            Gyo = Gyo + 1
        End If

        GetRangeAnalog = Gyo

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ダミー確認処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetDummy(ByVal Content As String, ByVal BlnOldDummy As Boolean, ByVal BlnNewDummy As Boolean, ByVal Gyo As Integer) As Integer

        Dim strDummy As String = ""
        Dim UseFlg As Boolean = False

        If BlnOldDummy = True And BlnNewDummy = False Then
            strDummy = "Dummy setting release"
            UseFlg = True
        End If

        If BlnOldDummy = False And BlnNewDummy = True Then
            strDummy = "Dummy configuration"
            UseFlg = True
        End If

        If UseFlg = True Then
            msgtemp(Gyo) = mMsgDummyCreateStr(Content, strDummy)
            Gyo = Gyo + 1
        End If

        GetDummy = Gyo

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データ種別コード変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetTypeCode(ByVal shtTypeCode As Short, ByVal shtChType As Short, ByVal intSignal As Integer) As String

        Dim strCHType As String

        Select Case shtChType

            'アナログCH
            Case gCstCodeChTypeAnalog

                Select Case shtTypeCode
                    Case gCstCodeChDataTypeAnalogK
                        strCHType = "K"
                    Case gCstCodeChDataTypeAnalog2Pt
                        strCHType = "TR(2)"
                    Case gCstCodeChDataTypeAnalog2Jpt
                        strCHType = "TR(J2)"
                    Case gCstCodeChDataTypeAnalog3Pt
                        strCHType = "TR(3)"
                    Case gCstCodeChDataTypeAnalog3Jpt
                        strCHType = "TR(J3)"
                    Case gCstCodeChDataTypeAnalog1_5v
                        strCHType = "AI(1-5)"
                    Case gCstCodeChDataTypeAnalog4_20mA
                        If intSignal = 2 Then
                            strCHType = "PT"
                        Else
                            strCHType = "AI(4-20)"
                        End If
                    Case gCstCodeChDataTypeAnalogPT4_20mA
                        strCHType = "PT(4-20)"
                    Case gCstCodeChDataTypeAnalogJacom
                        strCHType = "AI(J22)"
                    Case gCstCodeChDataTypeAnalogJacom55
                        strCHType = "AI(J55)"
                    Case gCstCodeChDataTypeAnalogModbus
                        strCHType = "AI(R)"
                    Case gCstCodeChDataTypeAnalogExhAve
                        strCHType = "MT"
                    Case gCstCodeChDataTypeAnalogExhRepose
                        strCHType = "RP"
                    Case gCstCodeChDataTypeAnalogExtDev
                        strCHType = "DV"

                    Case gCstCodeChDataTypeAnalogLatitude
                        strCHType = "AI(LAT)"
                    Case gCstCodeChDataTypeAnalogLongitude
                        strCHType = "AI(LON)"
                    Case gCstCodeChDataTtpeAnalogUTCyear
                        strCHType = "UYEAR"
                    Case gCstCodeChDataTtpeAnalogUTCmonth
                        strCHType = "UMON"
                    Case gCstCodeChDataTtpeAnalogUTCday
                        strCHType = "UDAY"
                    Case gCstCodeChDataTtpeAnalogUTChour
                        strCHType = "UHOUR"
                    Case gCstCodeChDataTtpeAnalogUTCmin
                        strCHType = "UMIN"
                    Case gCstCodeChDataTtpeAnalogUTCsec
                        strCHType = "USEC"


                    Case Else
                        strCHType = gCstNameChTypeAnalog
                End Select

                'デジタルCH
            Case gCstCodeChTypeDigital
                Select Case shtTypeCode
                    Case gCstCodeChDataTypeDigitalNC
                        strCHType = "NC"
                    Case gCstCodeChDataTypeDigitalNO
                        strCHType = "NO"
                    Case gCstCodeChDataTypeDigitalJacomNC
                        strCHType = "NC(J22)"
                    Case gCstCodeChDataTypeDigitalJacomNO
                        strCHType = "NO(J22)"
                    Case gCstCodeChDataTypeDigitalJacom55NC
                        strCHType = "NC(J55)"
                    Case gCstCodeChDataTypeDigitalJacom55NO
                        strCHType = "NO(J55)"
                    Case gCstCodeChDataTypeDigitalModbusNC
                        strCHType = "NC(R)"
                    Case gCstCodeChDataTypeDigitalModbusNO
                        strCHType = "NO(R)"
                    Case gCstCodeChDataTypeDigitalExt
                        strCHType = "NO(ECC)"
                    Case gCstCodeChDataTypeDigitalDeviceStatus
                        strCHType = "NC(SYSTEM)"
                    Case Else
                        strCHType = gCstNameChTypeDigital
                End Select

                'モーターCH
            Case gCstCodeChTypeMotor

                Select Case shtTypeCode
                    Case gCstCodeChDataTypeMotorManRun
                        strCHType = "M1 RUN"
                    Case gCstCodeChDataTypeMotorManRunA
                        strCHType = "M1 RUN-A"
                    Case gCstCodeChDataTypeMotorManRunB
                        strCHType = "M1 RUN-B"
                    Case gCstCodeChDataTypeMotorManRunC
                        strCHType = "M1 RUN-C"
                    Case gCstCodeChDataTypeMotorManRunD
                        strCHType = "M1 RUN-D"
                    Case gCstCodeChDataTypeMotorManRunE
                        strCHType = "M1 RUN-E"
                    Case gCstCodeChDataTypeMotorManRunF
                        strCHType = "M1 RUN-F"
                    Case gCstCodeChDataTypeMotorManRunG
                        strCHType = "M1 RUN-G"
                    Case gCstCodeChDataTypeMotorManRunH
                        strCHType = "M1 RUN-H"
                    Case gCstCodeChDataTypeMotorManRunI
                        strCHType = "M1 RUN-I"
                    Case gCstCodeChDataTypeMotorManRunJ
                        strCHType = "M1 RUN-J"

                    Case gCstCodeChDataTypeMotorManRunK
                        'Ver2.0.0.2 モーター種別増加
                        strCHType = "M1 RUN-K"

                    Case gCstCodeChDataTypeMotorAbnorRun
                        strCHType = "M2 RUN"
                    Case gCstCodeChDataTypeMotorAbnorRunA
                        strCHType = "M2 RUN-A"
                    Case gCstCodeChDataTypeMotorAbnorRunB
                        strCHType = "M2 RUN-B"
                    Case gCstCodeChDataTypeMotorAbnorRunC
                        strCHType = "M2 RUN-C"
                    Case gCstCodeChDataTypeMotorAbnorRunD
                        strCHType = "M2 RUN-D"
                    Case gCstCodeChDataTypeMotorAbnorRunE
                        strCHType = "M2 RUN-E"
                    Case gCstCodeChDataTypeMotorAbnorRunF
                        strCHType = "M2 RUN-F"
                    Case gCstCodeChDataTypeMotorAbnorRunG
                        strCHType = "M2 RUN-G"
                    Case gCstCodeChDataTypeMotorAbnorRunH
                        strCHType = "M2 RUN-H"
                    Case gCstCodeChDataTypeMotorAbnorRunI
                        strCHType = "M2 RUN-I"
                    Case gCstCodeChDataTypeMotorAbnorRunJ
                        strCHType = "M2 RUN-J"

                    Case gCstCodeChDataTypeMotorAbnorRunK
                        'Ver2.0.0.2 モーター種別増加
                        strCHType = "M2 RUN-K"

                    Case gCstCodeChDataTypeMotorDevice
                        strCHType = "M0"
                    Case gCstCodeChDataTypeMotorDeviceJacom
                        strCHType = "M0(J22)"
                    Case gCstCodeChDataTypeMotorDeviceJacom55
                        strCHType = "M0(J55)"

                        'Ver2.0.0.2 モーター種別増加 START
                    Case gCstCodeChDataTypeMotorRManRun
                        strCHType = "R M1 RUN"
                    Case gCstCodeChDataTypeMotorRManRunA
                        strCHType = "R M1 RUN-A"
                    Case gCstCodeChDataTypeMotorRManRunB
                        strCHType = "R M1 RUN-B"
                    Case gCstCodeChDataTypeMotorRManRunC
                        strCHType = "R M1 RUN-C"
                    Case gCstCodeChDataTypeMotorRManRunD
                        strCHType = "R M1 RUN-D"
                    Case gCstCodeChDataTypeMotorRManRunE
                        strCHType = "R M1 RUN-E"
                    Case gCstCodeChDataTypeMotorRManRunF
                        strCHType = "R M1 RUN-F"
                    Case gCstCodeChDataTypeMotorRManRunG
                        strCHType = "R M1 RUN-G"
                    Case gCstCodeChDataTypeMotorRManRunH
                        strCHType = "R M1 RUN-H"
                    Case gCstCodeChDataTypeMotorRManRunI
                        strCHType = "R M1 RUN-I"
                    Case gCstCodeChDataTypeMotorRManRunJ
                        strCHType = "R M1 RUN-J"
                    Case gCstCodeChDataTypeMotorRManRunK
                        strCHType = "R M1 RUN-K"
                    Case gCstCodeChDataTypeMotorRAbnorRun
                        strCHType = "R M2 RUN"
                    Case gCstCodeChDataTypeMotorRAbnorRunA
                        strCHType = "R M2 RUN-A"
                    Case gCstCodeChDataTypeMotorRAbnorRunB
                        strCHType = "R M2 RUN-B"
                    Case gCstCodeChDataTypeMotorRAbnorRunC
                        strCHType = "R M2 RUN-C"
                    Case gCstCodeChDataTypeMotorRAbnorRunD
                        strCHType = "R M2 RUN-D"
                    Case gCstCodeChDataTypeMotorRAbnorRunE
                        strCHType = "R M2 RUN-E"
                    Case gCstCodeChDataTypeMotorRAbnorRunF
                        strCHType = "R M2 RUN-F"
                    Case gCstCodeChDataTypeMotorRAbnorRunG
                        strCHType = "R M2 RUN-G"
                    Case gCstCodeChDataTypeMotorRAbnorRunH
                        strCHType = "R M2 RUN-H"
                    Case gCstCodeChDataTypeMotorRAbnorRunI
                        strCHType = "R M2 RUN-I"
                    Case gCstCodeChDataTypeMotorRAbnorRunJ
                        strCHType = "R M2 RUN-J"
                    Case gCstCodeChDataTypeMotorRAbnorRunK
                        strCHType = "R M2 RUN-K"
                    Case gCstCodeChDataTypeMotorRDevice
                        strCHType = "R M0"
                        'Ver2.0.0.2 モーター種別増加 END

                    Case Else
                        strCHType = gCstNameChTypeMotor
                End Select

                'バルブCH
            Case gCstCodeChTypeValve

                Select Case shtTypeCode

                    Case gCstCodeChDataTypeValveDI_DO
                        strCHType = "DI-DO"
                    Case gCstCodeChDataTypeValveAI_DO1
                        strCHType = "AI-DO1(1-5)"
                    Case gCstCodeChDataTypeValveAI_DO2
                        strCHType = "AI-DO(4-20)"
                    Case gCstCodeChDataTypeValvePT_DO2
                        strCHType = "PT(4-20)-DO"
                    Case gCstCodeChDataTypeValveAI_AO1
                        strCHType = "AI-AO(1-5)"
                    Case gCstCodeChDataTypeValveAI_AO2
                        strCHType = "AI-AO(4-20)"
                    Case gCstCodeChDataTypeValvePT_AO2
                        strCHType = "PT(4-20)-AO"
                    Case gCstCodeChDataTypeValveAO_4_20
                        strCHType = "AI(V)"
                    Case gCstCodeChDataTypeValveDO
                        strCHType = "DI(V)"
                    Case gCstCodeChDataTypeValveJacom
                        strCHType = "DIV(J22)"
                    Case gCstCodeChDataTypeValveJacom55
                        strCHType = "DIV(J55)"
                    Case gCstCodeChDataTypeValveExt
                        strCHType = "DIV(EXT)"
                    Case Else
                        strCHType = gCstNameChTypeValve
                End Select

                'コンポジットCH
            Case gCstCodeChTypeComposite

                Select Case shtTypeCode

                    Case gCstCodeChDataTypeCompTankLevel
                        strCHType = "TKLEV(REP)"
                    Case gCstCodeChDataTypeCompTankLevelIndevi
                        strCHType = "TKLEV(IND)"
                    Case Else
                        strCHType = gCstNameChTypeComposite
                End Select

                'パルスCH
            Case gCstCodeChTypePulse

                Select Case shtTypeCode

                    Case gCstCodeChDataTypePulseTotal1_1
                        strCHType = "PU"
                    Case gCstCodeChDataTypePulseTotal1_10
                        strCHType = "P1"
                    Case gCstCodeChDataTypePulseTotal1_100
                        strCHType = "P2"
                    Case gCstCodeChDataTypePulseDay1_1
                        strCHType = "PUD"
                    Case gCstCodeChDataTypePulseDay1_10
                        strCHType = "P1D"
                    Case gCstCodeChDataTypePulseDay1_100
                        strCHType = "P2D"
                    Case gCstCodeChDataTypePulseRevoTotalHour
                        strCHType = "RH"
                    Case gCstCodeChDataTypePulseRevoTotalMin
                        strCHType = "R2"
                    Case gCstCodeChDataTypePulseRevoDayHour
                        strCHType = "RHD"
                    Case gCstCodeChDataTypePulseRevoDayMin
                        strCHType = "R2D"
                    Case gCstCodeChDataTypePulseRevoLapHour
                        strCHType = "RHL"
                    Case gCstCodeChDataTypePulseRevoLapMin
                        strCHType = "R2L"
                    Case gCstCodeChDataTypePulseExtDev
                        strCHType = "PU(R)"
                    Case gCstCodeChDataTypePulseRevoExtDev      '' Ver1.11.8.3 2016.11.08 運転積算 通信ﾀｲﾌﾟ追加
                        strCHType = "RH(R)"
                    Case gCstCodeChDataTypePulseRevoExtDevTotalMin  '' Ver1.12.0.1 2017.01.13 
                        strCHType = "R2(R)"
                    Case gCstCodeChDataTypePulseRevoExtDevDayHour     '' Ver1.12.0.1 2017.01.13 
                        strCHType = "RHD(R)"
                    Case gCstCodeChDataTypePulseRevoExtDevDayMin      '' Ver1.12.0.1 2017.01.13
                        strCHType = "R2D(R)"
                    Case gCstCodeChDataTypePulseRevoExtDevLapHour     '' Ver1.12.0.1 2017.01.13 
                        strCHType = "RHL(R)"
                    Case gCstCodeChDataTypePulseRevoExtDevLapMin      '' Ver1.12.0.1 2017.01.13 
                        strCHType = "R2L(R)"
                    Case Else
                        strCHType = gCstNameChTypePulse

                End Select

            Case Else
                strCHType = ""
        End Select

        Return strCHType

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データ種別コード変更処理
    ' 返り値    : 
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetChTypeName(ByVal shtChType As Short) As String
        Dim strRet As String = ""

        Select Case shtChType
            Case gCstCodeChTypeAnalog
                'アナログCH
                strRet = gCstNameChTypeAnalog
            Case gCstCodeChTypeDigital
                'デジタルCH
                strRet = gCstNameChTypeDigital
            Case gCstCodeChTypeMotor
                'モーターCH
                strRet = gCstNameChTypeMotor
            Case gCstCodeChTypeValve
                'バルブCH
                strRet = gCstNameChTypeValve
            Case gCstCodeChTypeComposite
                'コンポジットCH
                strRet = gCstNameChTypeComposite
            Case gCstCodeChTypePulse
                'パルスCH
                strRet = gCstNameChTypePulse
            Case Else
                strRet = ""
        End Select

        Return strRet
    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： チャンネル番号の桁合わせ
    ' 引数      ： 
    ' 戻値      ： 
    '----------------------------------------------------------------------------
    Private Function mMsgCHConv(ByVal intChNo As Integer) As String

        Dim StrChNo As String

        If intChNo = 0 Then Return "0000"
        If intChNo = -1 Then Return "None"

        If intChNo < 1000 Then
            StrChNo = "0" & Trim(Str(intChNo))
        Else
            StrChNo = Trim(Str(intChNo))
        End If

        Return StrChNo

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 数値ﾌｫｰﾏｯﾄを整える
    ' 返り値    : 数値文字列
    ' 引き数    : nData   ﾃﾞｰﾀ値
    '           : point   小数点位置
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function GetNumFormat(ByVal nData As Long, ByVal point As Integer) As String

        Dim strData As String
        Dim strNum As String
        Dim strTemp As String
        Dim nLen As Integer
        Dim wkI As Integer

        Dim strNegative As String = CStr(nData)
        Dim blnNegaFlg As Boolean = False

        Dim strMidData As String = ""
        Dim strDataCHG As String = ""

        strData = CStr(nData)
        nLen = Len(strData)

        If point = 0 Then   ' 小数点以下が存在しない場合はそのままの文字を返す
            Return strData
        End If

        '値に負「-」記号があるか確認
        If 0 <= strNegative.IndexOf("-") Then

            nLen = Len(strData)

            For i As Integer = 1 To nLen
                strMidData = Mid(strData, i, 1)

                If strMidData <> "-" Then
                    strDataCHG = strDataCHG & strMidData
                End If
            Next
            blnNegaFlg = True

            nLen = Len(strDataCHG)

        Else
            strDataCHG = CStr(nData)
        End If



        If nLen < point Then
            If (point - nLen) = 1 Then
                strNum = "0.0" & strDataCHG
            Else
                strTemp = ""
                For wkI = 0 To (point - nLen - 1)
                    strTemp = strTemp & "0"
                Next
                strNum = "0." & strTemp & strDataCHG
            End If

            'Ver2.0.0.7 マイナスならば-を付ける
            If blnNegaFlg = True Then
                strNum = "-" & strNum
            End If

        ElseIf nLen = point Then    ' 小数点位置と文字数が同じならば0.を付加

            If blnNegaFlg = True Then
                strNum = "-0." & strDataCHG
            Else
                strNum = "0." & strDataCHG
            End If
        Else
            If blnNegaFlg = True Then
                strNum = "-" & strDataCHG.Substring(0, nLen - point) & "." & strDataCHG.Substring(nLen - point, point)
            Else
                strNum = strDataCHG.Substring(0, nLen - point) & "." & strDataCHG.Substring(nLen - point, point)
            End If
        End If

        Return strNum

    End Function


    '--------------------------------------------------------------------
    ' 機能      : FUｱﾄﾞﾚｽの表示
    '               Ver1.11.5 2016.08.27 
    ' 返り値    : 数値文字列
    ' 引き数    : CHSet1   比較元 CH
    '           : CHSet2   比較先 CH
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function mMsgFUAdd(ByVal CHSet1 As gTypSetChRecCommon, ByVal CHSet2 As gTypSetChRecCommon, pblOLDdumy As Boolean, pblNEWdumy As Boolean) As String
        'Ver2.0.2.8 ダミーを値に含めるように変更


        Dim strOldValue As String
        Dim strNewValue As String

        Dim strFuNo As String
        Dim strSlotNo As String
        Dim strPinNo As String

        Dim strOldDumy As String = ""
        Dim strNewDumy As String = ""

        'ダミー値＝#の判定
        If pblOLDdumy = True Then
            strOldDumy = "#"
        End If
        If pblNEWdumy = True Then
            strNewDumy = "#"
        End If

        '' 比較元 未設定の場合
        If CHSet1.shtFuno = &HFFFF And CHSet1.shtPortno = &HFFFF And CHSet1.shtPin = &HFFFF Then
            strOldValue = " "
        Else
            '' FU番号
            If CHSet1.shtFuno = &HFFFF Then
                strFuNo = " "
            Else
                strFuNo = CStr(CHSet1.shtFuno)
            End If

            '' ｽﾛｯﾄ番号
            If CHSet1.shtPortno = &HFFFF Then
                strSlotNo = " "
            Else
                strSlotNo = CStr(CHSet1.shtPortno)
            End If

            '' ｽﾛｯﾄ番号
            If CHSet1.shtPin = &HFFFF Then
                strPinNo = " "
            Else
                strPinNo = CStr(CHSet1.shtPin)
            End If

            strOldValue = strFuNo & "-" & strSlotNo & "-" & strPinNo
        End If

        '' 比較先 未設定の場合
        If CHSet2.shtFuno = &HFFFF And CHSet2.shtPortno = &HFFFF And CHSet2.shtPin = &HFFFF Then
            strNewValue = " "
        Else
            '' FU番号
            If CHSet2.shtFuno = &HFFFF Then
                strFuNo = " "
            Else
                strFuNo = CStr(CHSet2.shtFuno)
            End If

            '' ｽﾛｯﾄ番号
            If CHSet2.shtPortno = &HFFFF Then
                strSlotNo = " "
            Else
                strSlotNo = CStr(CHSet2.shtPortno)
            End If

            '' ｽﾛｯﾄ番号
            If CHSet2.shtPin = &HFFFF Then
                strPinNo = " "
            Else
                strPinNo = CStr(CHSet2.shtPin)
            End If

            strNewValue = strFuNo & "-" & strSlotNo & "-" & strPinNo
        End If

        mMsgFUAdd = "FU Add" & " " & ":" & " " & strOldDumy & strOldValue & " " & "→" & " " & strNewDumy & strNewValue

        '表示調整
        mMsgFUAdd = mMsgContentLen(mMsgFUAdd, False)

    End Function
    'OUT FUｱﾄﾞﾚｽ版
    Private Function mMsgFUAdd_OUT(pintOldFUno As Integer, pintOldPort As Integer, pintOldPin As Integer, _
                                   pintNewFUno As Integer, pintNewPort As Integer, pintNewPin As Integer, _
                                   pblOLDdumy As Boolean, pblNEWdumy As Boolean) As String


        Dim strOldValue As String
        Dim strNewValue As String

        Dim strFuNo As String
        Dim strSlotNo As String
        Dim strPinNo As String

        Dim strOldDumy As String = ""
        Dim strNewDumy As String = ""

        'ダミー値＝#の判定
        If pblOLDdumy = True Then
            strOldDumy = "#"
        End If
        If pblNEWdumy = True Then
            strNewDumy = "#"
        End If

        '比較元 未設定の場合
        If pintOldFUno = &HFFFF And pintOldPort = &HFFFF And pintOldPin = &HFFFF Then
            strOldValue = " "
        Else
            'FU番号
            If pintOldFUno = &HFFFF Then
                strFuNo = " "
            Else
                strFuNo = CStr(pintOldFUno)
            End If

            'ｽﾛｯﾄ番号
            If pintOldPort = &HFFFF Then
                strSlotNo = " "
            Else
                strSlotNo = CStr(pintOldPort)
            End If

            'ｽﾛｯﾄ番号
            If pintOldPin = &HFFFF Then
                strPinNo = " "
            Else
                strPinNo = CStr(pintOldPin)
            End If

            strOldValue = strFuNo & "-" & strSlotNo & "-" & strPinNo
        End If

        '比較先 未設定の場合
        If pintNewFUno = &HFFFF And pintNewPort = &HFFFF And pintNewPin = &HFFFF Then
            strNewValue = " "
        Else
            'FU番号
            If pintNewFUno = &HFFFF Then
                strFuNo = " "
            Else
                strFuNo = CStr(pintNewFUno)
            End If

            'ｽﾛｯﾄ番号
            If pintNewPort = &HFFFF Then
                strSlotNo = " "
            Else
                strSlotNo = CStr(pintNewPort)
            End If

            'ｽﾛｯﾄ番号
            If pintNewPin = &HFFFF Then
                strPinNo = " "
            Else
                strPinNo = CStr(pintNewPin)
            End If

            strNewValue = strFuNo & "-" & strSlotNo & "-" & strPinNo
        End If

        mMsgFUAdd_OUT = "OUT FU Add" & " " & ":" & " " & strOldDumy & strOldValue & " " & "→" & " " & strNewDumy & strNewValue

        '表示調整
        mMsgFUAdd_OUT = mMsgContentLen(mMsgFUAdd_OUT, False)

    End Function

#End Region

End Class