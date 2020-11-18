Imports Microsoft.VisualBasic.FileIO

Module modCommon

#Region "構造体定義"

#Region "ファイル情報"

    Public Structure gTypFileInfo
        Dim strFilePath As String
        Dim strFileName As String

        'T,Ueki フォルダ仕様管理変更
        Dim strFileNameNew As String

        Dim strFileVersion As String
        Dim strFileVersionPrev As String
        Dim blnVersionUP As Boolean

    End Structure

    '2015/6/4 T.Ueki
    Public Structure gTypCompareFileInfo

        Dim strFilePath As String
        Dim strFileName As String
        Dim strFileOrgPath As String
        Dim strFileVersion As String

    End Structure


    'Ver2.0.0.2


#End Region

#Region "チャンネルグループ情報"

    Public Structure gTypChannelGroup

        Dim udtGroup() As gTypChannelGroupGroup

    End Structure

    Public Structure gTypChannelGroupGroup

        Dim intDataCnt As Integer
        Dim strGroupName As String
        Dim udtChannelData() As gTypSetChRec

    End Structure

#End Region

#Region "エラー情報"

    Public Structure gTypExceptionInfo
        Dim strExeName As String
        Dim strFileName As String
        Dim strFuncName As String
        Dim strErrMsg As String
    End Structure

#End Region

#Region "インポート用情報_OPSグラフ設定"

    Public Structure gTypImportGraphData

        Dim intUseCountGraph As Integer                     ''3種グラフの設定数
        Dim intUseCountFree As Integer                      ''フリーグラフの設定数
        Dim udtOpsGraph() As gTypImportGraphDataGraph       ''3種グラフ
        Dim udtOpsFree() As gTypImportGraphDataFree         ''フリーグラフ

    End Structure

    ''グラフ
    Public Structure gTypImportGraphDataGraph

        Dim intNo As Integer                ''グラフ番号(1～16)
        Dim intType As Integer              ''グラフタイプ(1:Exhaust、2:Bar、3:AnalogMeter)
        Dim strGraphName As String          ''グラフ名称(半角26文字)

    End Structure

    ''フリーグラフ
    Public Structure gTypImportGraphDataFree

        Dim intUseCount As Integer          ''グラフ設定してある数
        Dim udtFreeGraphTitle() As gTypImportGraphDataFreeTitle

    End Structure

    ''フリーグラフタイトル
    Public Structure gTypImportGraphDataFreeTitle

        Dim intOpsNo As Integer             ''OPS番号(1～10)
        Dim intGraphNo As Integer           ''グラフ番号(1～16)
        Dim strGraphTitle As String         ''グラフタイトル(半角26文字)

    End Structure

#End Region

#Region "コンバート用情報_OPSグラフ設定"

    Public Structure gTypConvertOps

        Dim intGraphType As Integer                         ''グラフタイプコード
        Dim strGraphType As String                          ''グラフタイプ名（コードだと分かりにくいため）
        Dim udtGraphData As gTypConvertOpsGraphData         ''グラフデータ

    End Structure

    Public Structure gTypConvertOpsGraphData

        Dim intPageNo As Integer        ''ページNo
        Dim intMeterType As Integer     ''アナログメーターの表示タイプ
        Dim strTitle As String          ''画面名称
        Dim strItemUp As String         ''グラフデータ名称（上段）
        Dim strItemDown As String       ''グラフデータ名称（下段）
        Dim strTcTitle As String        ''T/Cグラフのタイトル
        Dim strTcComm1 As String        ''T/Cグラフのコメント1
        Dim strTcComm2 As String        ''T/Cグラフのコメント2
        Dim intDevMark As Integer       ''偏差目盛の上下限値    （0～255）
        Dim intDevision As Integer      ''バーグラフ分割数
        Dim int20Graph As Integer       ''グラフ20本区切り      （0:OFF、1：ON）
        Dim intLine As Integer          ''数値の分け方          （0:設定なし、1:1Line、2:2LINE）
        Dim intCyCnt As Integer         ''シリンダの数(1～24)
        Dim intTcCnt As Integer         ''T/Cの数(1～8)
        Dim strChNoAve As String        ''データ詳細_平均

        ''グラフデータ詳細
        Dim udtGraphDataDetail() As gTypConvertOpsGraphDataDetail

        ''アナログメーター設定
        Dim udtAnalogMeterSetting As gTypConvertOpsAnalogMeterSetting

        ''T/Cグラフ情報
        Dim udtTcGraphInfo() As gTypConvertOpsTcGraphInfo

    End Structure

    ''グラフデータ詳細
    Public Structure gTypConvertOpsGraphDataDetail

        Dim strChno As String           ''CH_NO

        ''排気ガス・バーグラフ
        Dim strExhBarChnoDev As String  ''偏差CH
        Dim strExhBarTitle As String    ''タイトル

        ''アナログメーター
        Dim intAnalogScale As Integer   ''メーター種類（表示分割数）
        Dim intAnalogColor As Integer   ''表示色

    End Structure

    ''アナログメーター設定
    Public Structure gTypConvertOpsAnalogMeterSetting

        Dim intChNameDisplayPoint As Integer        ''チャンネル名称表示位置
        Dim intMarkNumericalValue As Integer        ''アナログメーター目盛数値
        Dim intPointerFrame As Integer              ''指針縁取り
        Dim intPointerColor As Integer              ''指針色変更
        Dim intSideColorSymbol As Integer           ''名称横の色シンボル表示

    End Structure

    ''T/Cグラフ情報
    Public Structure gTypConvertOpsTcGraphInfo

        Dim strTcChNo As String         ''T/CチャンネルNO
        Dim strTcTitle As String        ''T/Cチャンネルタイトル
        Dim intTcSplit As Integer       ''T/C区切り線

    End Structure


#End Region

#Region "ログフォーマット自動生成"

    ''RLフラグが有効になっているCH群
    Public Structure gTypLogFormatPickCH
        Dim udtSetChRL() As gTypLogFormatPickCH_RL
    End Structure

    Public Structure gTypLogFormatPickCH_RL
        Dim intChType As Integer        ''チャンネル種別
        Dim blnChTypeRev As Boolean     ''パルスCHのデータ種別判定（TRUE:運転積算CH, FALSE:パルスCH）
        Dim intGroupNo As Integer       ''グループ番号
        Dim intChno As Integer          ''チャンネル番号
    End Structure

#End Region

#End Region

#Region "比較を行うか、行わないかﾌﾗｸﾞ行数"
    '比較を行うか、行わないかﾌﾗｸﾞ
    ''チャンネル追加/削除
    Public Const gConmCompareSetChannelAddDel As Integer = 0
    ''チャンネル情報
    Public Const gConmCompareSetChannelDisp As Integer = 1
    ''システム設定
    Public Const gConmCompareSetSystem As Integer = 2
    ''端子表比較
    Public Const gConmCompareTerminalInfo As Integer = 3
    ''コンポジット情報
    Public Const gConmCompareSetCompositeDisp As Integer = 4
    ''リポーズ入力設定
    Public Const gConmCompareSetRepose As Integer = 5
    ''出力チャンネル設定
    Public Const gConmCompareSetCHOutPut_FU As Integer = 6
    ''論理出力設定
    Public Const gConmCompareSetCHAndOr As Integer = 7
    ''積算データ設定ファイル
    Public Const gConmCompareSetChRunHour As Integer = 8
    ''コントロール使用可／不可設定
    Public Const gConmCompareSetCtrlUseNotuse As Integer = 9
    ''排ガス演算処理設定
    Public Const gConmCompareSetExhGus As Integer = 10
    ''SIO設定（外部機器VDR情報設定）
    Public Const gConmCompareSetChSio As Integer = 11
    ''SIO設定（外部機器VDR情報設定）CH設定データ
    Public Const gConmCompareSetChSioCh As Integer = 12
    ''延長警報設定
    Public Const gConmCompareSetExtAlarm As Integer = 13
    ''タイマ設定
    Public Const gConmCompareSetTimer As Integer = 14
    ''タイマ表示名称設定
    Public Const gConmCompareSetTimerName As Integer = 15
    ''シーケンス設定
    Public Const gConmCompareSetSeqSequence As Integer = 16
    ''リニアライズテーブル
    Public Const gConmCompareSetSeqLinearTable As Integer = 17
    ''演算式テーブル
    Public Const gConmCompareSetSeqOperationExpression As Integer = 18
    ''データ保存テーブル設定
    Public Const gConmCompareSetChDataSaveTable As Integer = 19
    ''データ保存テーブル設定
    Public Const gConmCompareSetChDataForwardTableSet As Integer = 20
    ''OPS画面タイトル
    Public Const gConmCompareSetOpsDisp As Integer = 21
    ''プルダウンメニュー
    Public Const gConmCompareSetOpsPulldownMenu As Integer = 22
    ''セレクションメニュー
    Public Const gConmCompareSetOpsSelectionMenu As Integer = 23
    ''OPS設定
    Public Const gConmCompareSetOpsGraph As Integer = 24
    ''フリーグラフ
    Public Const gConmCompareSetOpsFreeGraph As Integer = 25
    ''フリーディスプレイ
    Public Const gConmCompareSetOpsFreeDisplay As Integer = 26
    ''グループ設定
    Public Const gConmCompareSetGroupDisp As Integer = 27
    ''トレンドグラフ
    Public Const gConmCompareSetOpsTrendGraph As Integer = 28
    ''ログフォーマット
    Public Const gConmCompareSetOpsLogFormat As Integer = 29
    ''GWS設定 CH設定データ
    Public Const gConmCompareSetOpsGwsCh As Integer = 30
    ''CH変換テーブル
    Public Const gConmCompareSetChConv As Integer = 31
    ''Mimic ﾌｧｲﾙレベル比較
    Public Const gConmCompareMimicFiles As Integer = 32
#End Region

#Region "変数定義"

    ''エディタ全体で設定に更新があるか否かのフラグ
    Public gblnUpdateAll As Boolean

    ''選択中のファイル情報
    Public gudtFileInfo As gTypFileInfo

    ''コンペア用選択中のファイル情報
    Public gudtCompareFileInfo As gTypCompareFileInfo

    ''例外エラー用構造体
    Public gudtExceptionInfo As gTypExceptionInfo

    ''チャンネルリストのフォームを開いているか否かのフラグ
    Public gblnDispChannelList As Boolean

    ''チャンネルリスト以外のフォームを開いているか否かのフラグ
    Public gblnDispOtherForm As Boolean

    ''登録済チャネル数(現グループ以外)
    Public gMaxChannel As Integer


    Public cmbDataNow As String

    '---------------------------------
    ''ログフォーマット設定画面で使用
    '---------------------------------
    ''コピーペースト時の操作制御
    Public gintRow As Integer                   ''クリックした行数                   （例：40行目をクリックした場合=40）
    Public gintMaxRowOfEditPage As Integer      ''クリックした行のページの最大行数   （例：40行目をクリックした場合=60 [※30行/1ページの場合]）
    Public gintErrMsg As Integer                ''clsDataGridViewPlusから受け取ったエラーメッセージコード


    Private m_bStatus As Integer = 0               ' 2015.11.06  Ver1.7.6  ｽﾃｰﾀｽを取得するためのﾌﾗｸﾞ追加

    '' Ver2.0.0.0 2016.12.07 値変更
    '' Ver1.9.3 2016.01.16 追加
    Public m_FuDetailWndH As Integer = 784  '530        '' FU端子設定ｳｨﾝﾄﾞｳ　高さ
    Public m_FuDetailWndW As Integer = 1150 '954        ''                   幅

    'Ver2.0.2.8 計測点一覧も画面のｻｲｽﾞを記憶
    Public m_ChListWndH As Integer = 781        ' 計測点一覧ｳｨﾝﾄﾞｳ　高さ
    Public m_ChListWndW As Integer = 1280       '                   幅

    'T.Ueki 2016/6/27
    Public FileAccessFlg As Boolean


    'Ver2.0.0.7 比較　さらに大枠項目保存
    Public gCompareChkBIG(4) As Boolean
    ' 0:全部比較
    ' 1:計測点のみ比較
    ' 2:端子表のみ比較
    ' 3:FUアドレスを結果につける
    ' 4:関連テーブル検索を行う


    'Ver2.0.0.2
    Public gCompareChk(32) As Boolean   '大枠比較項目選択
    '比較詳細　共通部
    Public gCompareChk_1_Common(39) As Boolean
    '比較詳細　アナログ
    Public gCompareChk_2_Analog(57) As Boolean
    '比較詳細　デジタル
    Public gCompareChk_3_Digital(4) As Boolean
    '比較詳細　システム
    Public gCompareChk_4_System(50) As Boolean
    '比較詳細　モーター
    Public gCompareChk_5_Motor(34) As Boolean
    '比較詳細　バルブDIDO
    Public gCompareChk_6_DIDO(34) As Boolean
    '比較詳細　バルブAIDO
    Public gCompareChk_7_AIDO(84) As Boolean
    '比較詳細　バルブAIAO
    Public gCompareChk_8_AIAO(75) As Boolean
    '比較詳細　コンポジット
    Public gCompareChk_9_Comp(3) As Boolean
    '比較詳細　パルス
    Public gCompareChk_10_Puls(7) As Boolean
    '比較詳細　積算
    Public gCompareChk_11_RunHour(8) As Boolean

    '比較詳細　PID
    Public gCompareChk_12_Pid(90) As Boolean

    'Ver2.0.0.2
    '計測点リスト入力のエラー項目保存　列：CH,Item,AlarmLV,Detail
    Public gChListWarningData(3, 200) As String
    Public gintChListwarning As Integer
    Public gstrChListWarningTemp As String = ""
#Region "計測点リスト入力エラー項目専用関数"
    Public Function gfnGetChListWarningRow() As Integer
        '配列変数の空いてる行数を戻す
        For i As Integer = 0 To UBound(gChListWarningData, 2) Step 1
            If gChListWarningData(0, i) = "" Then
                Return i
            End If
        Next i

        Return -1
    End Function
#End Region

    'Ver2.0.0.2
    '基板指定印刷用
    Public gintTermFuNo As Integer
    Public gintTermSlotNo As Integer

    'Ver2.0.1.3
    '計測点印刷用
    Public gintChPrintGrNo As Integer

    'Ver2.0.1.5
    'チャンネル検索結果表示用変数
    Public gstrChSearchLog As String = ""

    'Ver2.0.3.6
    'Excel取込行った行わない変数
    Public gblExcelInDo As Boolean = False
    'システムチェック時に使う配列
    Public gAryKiki As ArrayList
    Public gAryKikiDtl As ArrayList

    'Ver2.0.4.2
    'Excelを吐き出す吐き出さない変数
    Public gblExcelOut As Boolean = False

    'Ver2.0.4.3
    '機器の名称を素早く取得するための配列群
    Public gAryDBkikiCode As ArrayList
    Public gAryDBkikiName As ArrayList
    Public gAryDBkikiDtlCode As ArrayList
    Public gAryDBkikiDtlName As ArrayList

    'Ver2.0.6.1
    '内部向けか、外部向けかﾌﾗｸﾞ 0=内部,1=外部
    Public gintNaiGai As Integer

#End Region

#Region "ファイル出力構造体初期化"

    '--------------------------------------------------------------------
    ' 機能      : ファイル出力構造体初期化
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル出力構造体に初期値を設定する
    '--------------------------------------------------------------------
    Public Sub gInitOutputStructure(ByRef udt As clsStructure)

        Try

            ''システム設定
            Call gInitSetSystem(udt.SetSystem)

            ''FU設定（チャンネル情報データ）
            Call gInitSetFuChannelDisp(udt.SetFu)

            ''チャンネル情報データ（表示名設定データ）
            Call gInitSetChDisp(udt.SetChDisp)

            ''チャンネル情報
            Call gInitSetChannelDisp(udt.SetChInfo)

            ''コンポジット情報
            Call gInitSetComposite(udt.SetChComposite)

            ''グループ設定
            Call gInitSetGroupDisp(udt.SetChGroupSetM)
            Call gInitSetGroupDisp(udt.SetChGroupSetC)

            ''リポーズ入力設定
            Call gInitSetRepose(udt.SetChGroupRepose)

            ''出力チャンネル設定
            Call gInitSetCHOutPut(udt.SetChOutput)

            ''論理出力設定
            Call gInitSetCHAndOr(udt.SetChAndOr)

            ''積算データ設定ファイル
            Call gInitSetChRunHour(udt.SetChRunHour)

            ''コントロール使用可／不可設定
            Call gInitSetCtrlUseNotuse(udt.SetChCtrlUseM)
            Call gInitSetCtrlUseNotuse(udt.SetChCtrlUseC)

            ''排ガス演算処理設定
            Call gInitSetExhGus(udt.SetChExhGus)

            ''SIO設定（外部機器VDR情報設定）
            Call gInitSetChSio(udt.SetChSio)

            ''SIO設定（外部機器VDR情報設定）CH設定データ
            Call gInitSetChSioCh(udt.SetChSioCh)
            Call gInitSetChSioCh(udt.SetChSioChPrev)

            'Ver2.0.5.8
            'SIO設定　拡張データ
            Call gInitSetChSioExt(udt.SetChSioExt)


            ''延長警報設定
            Call gInitSetExtAlarm(udt.SetExtAlarm)

            ''タイマ設定
            Call gInitSetTimer(udt.SetExtTimerSet)

            ''タイマ表示名称設定
            Call gInitSetTimerName(udt.SetExtTimerName)

            ''シーケンス設定
            Call gInitSetSeqSequence(udt.SetSeqID, udt.SetSeqSet)

            ''リニアライズテーブル
            Call gInitSetSeqLinearTable(udt.SetSeqLinear)

            ''演算式テーブル
            Call gInitSetSeqOperationExpression(udt.SetSeqOpeExp)

            ''データ保存テーブル設定
            Call gInitSetChDataSaveTable(udt.SetChDataSave)

            ''データ保存テーブル設定
            Call gInitSetChDataForwardTableSet(udt.SetChDataForward)

            ''OPS画面タイトル
            Call gInitSetOpsDisp(udt.SetOpsScreenTitleM)
            Call gInitSetOpsDisp(udt.SetOpsScreenTitleC)

            ''プルダウンメニュー
            Call gInitSetOpsPulldownMenu(udt.SetOpsPulldownMenuM)
            Call gInitSetOpsPulldownMenu(udt.SetOpsPulldownMenuC)

            ''セレクションメニュー
            Call gInitSetOpsSelectionMenu(udt.SetOpsSelectionMenuM)
            Call gInitSetOpsSelectionMenu(udt.SetOpsSelectionMenuC)

            ''OPS設定
            Call gInitSetOpsGraph(udt.SetOpsGraphM)
            Call gInitSetOpsGraph(udt.SetOpsGraphC)

            ''フリーグラフ    ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
            Call gInitSetOpsFreeGraph(udt.SetOpsFreeGraphM)
            Call gInitSetOpsFreeGraph(udt.SetOpsFreeGraphC)

            ''フリーディスプレイ
            Call gInitSetOpsFreeDisplay(udt.SetOpsFreeDisplayM)
            Call gInitSetOpsFreeDisplay(udt.SetOpsFreeDisplayC)

            ''トレンドグラフ
            Call gInitSetOpsTrendGraph(udt.SetOpsTrendGraphM)
            Call gInitSetOpsTrendGraph(udt.SetOpsTrendGraphC)
            Call gInitSetOpsTrendGraph(udt.SetOpsTrendGraphPID)
            Call gInitSetOpsTrendGraph(udt.SetOpsTrendGraphPIDprev)

            ''ログフォーマット
            Call gInitSetOpsLogFormat(udt.SetOpsLogFormatM)
            Call gInitSetOpsLogFormat(udt.SetOpsLogFormatC)

            ''ログフォーマット CHID        ☆ 2012/10/26 K.Tanigawa
            Call gInitSetOpsLogIdData(udt.SetOpsLogIdDataM)
            Call gInitSetOpsLogIdData(udt.SetOpsLogIdDataC)

            '' ﾛｸﾞｵﾌﾟｼｮﾝ設定　Ver1.9.3 2016.01.25
            Call gInitSetOpsLogOption(udt.SetOpsLogOption)

            ''GWS設定 CH設定データ 2014.02.04
            Call gInitSetOpsGwsCh(udt.SetOpsGwsCh)
            Call gInitSetOpsGwsCh(udt.SetOpsGwsChPrev)

            ''負荷曲線 Ver.2.0.8.U hori
            Call gInitSetOpsLoadCurve(udtLoadCurve)
            Call gInitSetOpsLoadCurve(mudtSetLoadCurveNew)


            ''CH変換テーブル
            Call gInitSetChConv(udt.SetChConvNow)
            Call gInitSetChConv(udt.SetChConvPrev)

            ''デフォルトデータ作成用
            ''ログ印字設定
            'Call gInitSetOtherLogTime(udt.SetOtherLogTime)

            ''ファイル更新情報
            Call gInitSetEditorUpdateInfo(udt.SetEditorUpdateInfo)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネルID、チャンネルNO 変換"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルNO → チャンネルID 変換
    ' 返り値    : チャンネルID
    ' 引き数    : ARG1 - (I ) チャンネルNO
    ' 機能説明  : チャンネルNOをチャンネルIDに変換する
    ' 補足　　  : チャンネルNOが 0 の場合は 0 を返す
    '     　　  : チャンネルNOに対応するCH IDが存在しない場合は -1 を返す
    '--------------------------------------------------------------------
    Public Function gConvChNoToChId(ByVal intChNo As Integer) As Integer

        Try

            If intChNo = 0 Then Return 0

            Dim shtChID As Short = -1

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChNo Then

                        shtChID = .shtChid

                        Exit For

                    End If

                End With

            Next

            Return shtChID

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルID → チャンネルNO 変換
    ' 返り値    : チャンネルNO
    ' 引き数    : ARG1 - (I ) チャンネルID
    ' 機能説明  : チャンネルIDをチャンネルNOに変換する
    ' 補足　　  : チャンネルIDが 0 の場合は 0 を返す
    ' 補足　　  : チャンネルIDに対応するCH NOが存在しない場合は -1 を返す
    '--------------------------------------------------------------------
    Public Function gConvChIdToChNo(ByVal intChId As Integer) As Integer

        Try

            If intChId = 0 Then Return 0

            Dim intChNo As Integer = -1

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChid = intChId Then

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

#Region "チャンネルの配列Index取得"

    '----------------------------------------------------------------------------
    ' 機能      : チャンネルNO から チャンネルの配列Index を取得
    ' 返り値    : チャンネルの配列Index
    ' 引き数    : ARG1 - (I ) チャンネルNO
    ' 機能説明  : チャンネルNO から チャンネルの配列Indexを取得する
    ' 補足　　  : チャンネルNOが 0 の場合は 0 を返す
    '     　　  : チャンネルNOに対応する配列が存在しない場合は -1 を返す
    '----------------------------------------------------------------------------
    Public Function gConvChNoToChArrayId(ByVal intChNo As Integer) As Integer

        Try

            If intChNo = 0 Then Return 0

            Dim shtLoopIndex As Short = -1

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChNo Then

                        shtLoopIndex = i

                        Exit For

                    End If

                End With

            Next

            Return shtLoopIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "システムNo取得"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルIDからシステムNoを取得
    ' 返り値    : システムNo
    ' 引き数    : ARG1 - (I ) チャンネルID
    ' 機能説明  : チャンネルIDからシステムNoを取得する
    ' 補足　　  : チャンネルIDが存在しない場合は 255 を返す
    '--------------------------------------------------------------------
    Public Function gConvChIdToSysNo(ByVal intChId As Integer) As Integer

        Try

            Dim intSysNo As Integer = 255

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChid = intChId Then

                        intSysNo = .shtSysno

                        Exit For

                    End If

                End With

            Next

            Return intSysNo

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルNOからシステムNoを取得
    ' 返り値    : システムNo
    ' 引き数    : ARG1 - (I ) チャンネルNO
    ' 機能説明  : チャンネルNOからシステムNoを取得する
    ' 補足　　  : チャンネルNOが存在しない場合は 255 を返す
    '--------------------------------------------------------------------
    Public Function gConvChNoToSysNo(ByVal strChNo As String) As Integer

        Try

            Dim intSysNo As Integer = 255

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = CUShort(strChNo) Then

                        intSysNo = .shtSysno

                        Exit For

                    End If

                End With

            Next

            Return intSysNo

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : FUアドレスの設定チェック
    ' 返り値    : True:設定済、False:未設定
    ' 引き数    : ARG1 - (I ) 入力FU番号
    '           : ARG2 - (I ) 入力FUポート番号
    '           : ARG3 - (I ) 入力FU計測点番号
    ' 機能説明  : FUアドレスの設定可否をチェックする
    '--------------------------------------------------------------------
    Public Function gChkFuAddressSet(ByVal intFuNo As Integer, _
                                     ByVal intSlot As Integer, _
                                     ByVal intPin As Integer) As Boolean

        If intFuNo = 0 And intSlot = 0 And intPin = 0 Then
            Return False
        End If

        '' ver.1.4.5 2012.07.03 条件変更　And → Or
        If intFuNo = gCstCodeChNotSetFuNo Or intSlot = gCstCodeChNotSetFuPort Or intPin = gCstCodeChNotSetFuPin Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "キーチェック"

    '--------------------------------------------------------------------
    ' 機能      : テキスト入力制限
    ' 返り値    : True:入力不可、False:入力可能
    ' 引き数    : ARG1 - (I ) 入力桁数（0:制限なし）
    ' 　　　    : ARG2 - (I ) テキストボックス
    ' 　　　    : ARG3 - (I ) キーコード                  ↓規定値
    ' 　　　    : ARG4 - (I ) 数値のみチェックフラグ    （True :数値のみ入力可      False:数値以外も入力可）
    ' 　　　    : ARG5 - (I ) マイナス入力可能フラグ    （False:入力不可   True :入力可能）
    ' 　　　    : ARG6 - (I ) 小数点入力可能フラグ      （False:入力不可、 True :入力可能）
    ' 　　　    : ARG7 - (I ) バックスラッシュ可能フラグ（False:入力不可   True :入力可能）
    ' 　　　    : ARG8 - (I ) チェック対象のリスト
    ' 機能説明  : 指定された条件での入力可否を返す
    '--------------------------------------------------------------------
    'Public Function gCheckTextInput(ByVal intInputLength As Integer, _
    '                                ByVal objText As Object, _
    '                                ByVal chrKeyChar As Char, _
    '                       Optional ByVal blnCheckNumInput As Boolean = True, _
    '                       Optional ByVal blnUseMinus As Boolean = False, _
    '                       Optional ByVal blnUseDecimalPoint As Boolean = False, _
    '                       Optional ByVal blnCheckBackSpace As Boolean = False, _
    '                       Optional ByVal blnCheckEnter As Boolean = False) As Boolean
    Public Function gCheckTextInput(ByVal intInputLength As Integer, _
                                    ByVal objText As Object, _
                                    ByVal chrKeyChar As Char, _
                           Optional ByVal blnCheckNumInput As Boolean = True, _
                           Optional ByVal blnUseMinus As Boolean = False, _
                           Optional ByVal blnUseDecimalPoint As Boolean = False, _
                           Optional ByVal blnUseBackSlash As Boolean = False, _
                           Optional ByVal strCheckList As String = "") As Boolean

        Try

            Dim blnFlg As Boolean
            Dim strwk() As String
            Dim intChkLenPlus As Integer = 0

            ''例外キーチェック
            If Asc(chrKeyChar) = 8 Then Return False ''BackSpace
            If Asc(chrKeyChar) = 13 Then Return False ''Enter

            ''バックスラッシュ判定
            If Not blnUseBackSlash And chrKeyChar = "\"c Then
                Return True
            End If

            '===================
            ''数値チェック
            '===================
            If blnCheckNumInput Then

                If chrKeyChar < "0"c Or chrKeyChar > "9"c Then

                    ''数値以外のキーが入力された場合
                    Select Case Asc(chrKeyChar)
                        Case 45 '' -（マイナス）

                            '' -（マイナス）を使用しない場合は入力不可
                            If Not blnUseMinus Then
                                Return True
                            Else

                                ''文字中に -（マイナス）が存在する場合
                                If InStr(Mid(objText.Text, 1, objText.Text.Length), "-") <> 0 Then

                                    ''２つめ以降の-（マイナス）は入力不可
                                    Return True

                                Else

                                    ''文字入力位置が先頭ではない場合
                                    If objText.selectionstart <> 0 Then

                                        ''先頭以外への-（マイナス）入力は不可
                                        Return True

                                    End If
                                End If

                            End If

                        Case 46 '' .（小数点）

                            '' .（小数点）を使用しない場合は入力不可
                            If Not blnUseDecimalPoint Then
                                Return True
                            Else

                                ''文字中に .（小数点）が存在する場合
                                If InStr(Mid(objText.Text, 1, objText.Text.Length), ".") <> 0 Then

                                    ''２つめ以降の .（小数点）は入力不可
                                    Return True

                                End If

                            End If

                        Case Else

                            ''数値、-（マイナス）、.（小数点）以外は入力不可
                            Return True

                    End Select

                End If

            End If

            '===================
            ''入力桁数チェック
            '===================
            ''入力桁数をチェックする場合
            If intInputLength <> 0 Then

                ''数値入力時の特定条件で入力桁数を増やす
                If blnCheckNumInput Then

                    If Asc(chrKeyChar) = 45 Then intChkLenPlus += 1 ''入力文字が -（マイナス）の場合
                    If Asc(chrKeyChar) = 46 Then intChkLenPlus += 1 ''入力文字が .（小数点）の場合
                    If InStr(Mid(objText.Text, 1, objText.Text.Length), "-") <> 0 Then intChkLenPlus += 1 ''文字中に -（マイナス）が存在する場合
                    If InStr(Mid(objText.Text, 1, objText.Text.Length), ".") <> 0 Then intChkLenPlus += 1 ''文字中に .（小数点）が存在する場合

                End If

                ''入力桁数チェック
                'If objText.Text.Length >= intInputLength + intChkLenPlus Then
                If LenB(objText.Text) >= intInputLength + intChkLenPlus Then

                    ''指定桁数以上の入力がされようとしている場合はテキストの選択状態を確認
                    If objText.SelectedText.Length >= 1 Then
                    Else

                        ''テキストが未選択状態の場合は入力不可
                        Return True

                    End If

                End If

            End If

            ''チェックリストがある場合
            If strCheckList <> "" Then

                ''チェックリストを分割
                strwk = strCheckList.Split(",")

                ''チェックリストが存在する場合
                If Not strwk Is Nothing Then

                    blnFlg = False
                    For i As Integer = 0 To UBound(strwk)

                        ''チェックリストに存在する文字か確認
                        If chrKeyChar = CChar(strwk(i)) Then
                            blnFlg = True
                        End If

                    Next

                    ''チェックリストに存在しない文字だったら入力不可
                    If Not blnFlg Then Return True

                End If


            End If

            ''全てのチェックに引っかからなかった場合は入力可能
            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function



    ''--------------------------------------------------------------------
    '' 機能      : テキスト入力制限
    '' 返り値    : True:入力不可、False:入力可能
    '' 引き数    : ARG1 - (I ) 入力桁数（0:制限なし）
    '' 　　　    : ARG2 - (I ) テキストボックス
    '' 　　　    : ARG3 - (I ) キーコード
    '' 　　　    : ARG4 - (I ) BackSpaceキー判定フラグ
    '' 　　　    : ARG5 - (I ) Tabキー判定フラグ
    '' 　　　    : ARG6 - (I ) Enterキー判定フラグ
    '' 機能説明  : 指定された条件での入力可否を返す
    ''--------------------------------------------------------------------
    'Public Function gChkTextInput1(ByVal intInputLength As Integer, _
    '                              ByVal objText As Object, _
    '                              ByVal chrKeyChar As Char, _
    '                     Optional ByVal blnCheckNumInput As Boolean = True, _
    '                     Optional ByVal blnUseDecimalPoint As Boolean = False, _
    '                     Optional ByVal blnCheckBackSpace As Boolean = False, _
    '                     Optional ByVal blnCheckTab As Boolean = False, _
    '                     Optional ByVal blnCheckEnter As Boolean = False) As Boolean

    '    ''例外キーチェック
    '    If Not blnCheckBackSpace And Asc(chrKeyChar) = 8 Then Return False ''BackSpace
    '    If Not blnCheckTab And Asc(chrKeyChar) = 9 Then Return False ''Tab
    '    If Not blnCheckEnter And Asc(chrKeyChar) = 13 Then Return False ''Enter

    '    ''数値チェックを行う場合
    '    If blnCheckNumInput Then

    '        ''小数点を使用する場合
    '        If blnUseDecimalPoint Then
    '            If (chrKeyChar >= "0"c And chrKeyChar <= "9"c) Or (chrKeyChar = "."c) Then Return True
    '        Else
    '            If chrKeyChar < "0"c Or chrKeyChar > "9"c Then Return True
    '        End If

    '    End If

    '    ''入力桁数制限なしの場合はチェックしない
    '    If intInputLength = 0 Then
    '        Return False
    '    Else

    '        ''入力桁数チェック
    '        If objText.Text.Length >= intInputLength Then

    '            ''指定桁数以上の入力がされようとしている場合はテキストの選択状態を確認
    '            If objText.SelectedText.Length >= 1 Then

    '                ''１文字以上のテキストが選択状態の場合は入力可能
    '                Return False

    '            Else

    '                ''テキストが未選択状態の場合は入力不可
    '                Return True

    '            End If

    '        Else

    '            ''指定桁数未満の場合は入力可能
    '            Return False

    '        End If

    '    End If

    'End Function

    ''--------------------------------------------------------------------
    '' 機能      : テキスト入力制限（マイナスあり）
    '' 返り値    : True:入力不可、False:入力可能
    '' 引き数    : ARG1 - (I ) 入力桁数（0:制限なし）
    '' 　　　    : ARG2 - (I ) テキストボックス
    '' 　　　    : ARG3 - (I ) キーコード
    '' 　　　    : ARG4 - (I ) BackSpaceキー判定フラグ
    '' 機能説明  : 指定された条件での入力可否を返す
    ''--------------------------------------------------------------------
    'Public Function gChkTextInputMinus1(ByVal intInputLength As Integer, _
    '                                   ByVal objText As Object, _
    '                                   ByVal chrKeyChar As Char, _
    '                          Optional ByVal blnNumInputCheck As Boolean = True, _
    '                          Optional ByVal blnNoCheckBackSpace As Boolean = True, _
    '                          Optional ByVal blnNoCheckTab As Boolean = True, _
    '                          Optional ByVal blnNoCheckEnter As Boolean = True) As Boolean

    '    Dim intChkLen As Integer

    '    ''例外キーチェック
    '    If blnNoCheckBackSpace And Asc(chrKeyChar) = 8 Then Return False ''BackSpace
    '    If blnNoCheckTab And Asc(chrKeyChar) = 9 Then Return False ''Tab
    '    If blnNoCheckEnter And Asc(chrKeyChar) = 13 Then Return False ''Enter

    '    ''数値チェックを行う場合
    '    If blnNumInputCheck Then

    '        ''数値以外が入力された場合
    '        If chrKeyChar < "0"c Or chrKeyChar > "9"c Then

    '            '' -（マイナス）以外入力された場合
    '            If Asc(chrKeyChar) <> 45 Then

    '                ''数値と-（マイナス）以外の文字は入力不可
    '                Return True

    '            Else

    '                ''文字中に-（マイナス）が存在する場合
    '                If InStr(Mid(objText.Text, 1, objText.Text.Length), "-") <> 0 Then

    '                    ''２つめ以降の-（マイナス）は入力不可
    '                    Return True

    '                Else

    '                    ''文字入力位置が先頭ではない場合
    '                    If objText.selectionstart <> 0 Then

    '                        ''先頭以外への-（マイナス）入力は不可
    '                        Return True

    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    ''入力桁数制限なしの場合はチェックしない
    '    If intInputLength = 0 Then
    '        Return False
    '    Else


    '        If blnNumInputCheck And Asc(chrKeyChar) = 45 Then

    '            ''数値チェックありでマイナスキー入力の場合は入力桁数+1
    '            intChkLen = intInputLength + 1

    '        ElseIf blnNumInputCheck And Mid(objText.Text, 1, 1) = "-" Then

    '            ''数値チェックありでマイナス値の場合は入力桁数+1
    '            intChkLen = intInputLength + 1

    '        Else

    '            intChkLen = intInputLength + 1

    '        End If

    '        ''入力桁数チェック
    '        If objText.Text.Length + 1 > intChkLen Then

    '            ''指定桁数以上の入力がされようとしている場合はテキストの選択状態を確認
    '            If objText.SelectedText.Length >= 1 Then

    '                ''１文字以上のテキストが選択状態の場合は入力可能
    '                Return False

    '            Else

    '                ''テキストが未選択状態の場合は入力不可
    '                Return True

    '            End If

    '        Else

    '            ''指定桁数未満の場合は入力可能
    '            Return False

    '        End If

    '    End If

    'End Function

    '--------------------------------------------------------------------
    ' 機能      : テキスト入力範囲チェック
    ' 返り値    : True:範囲外（数値以外）、False:範囲内
    ' 引き数    : ARG1 - (I ) 最小値
    ' 　　　    : ARG2 - (I ) 最大値
    ' 　　　    : ARG3 - (I ) 入力値
    ' 　　　    : ARG4 - (I ) メッセージ表示フラグ
    ' 　　　    : ARG5 - (I ) サブメッセージ文字列
    ' 機能説明  : 指定された条件での入力可否を返す
    '--------------------------------------------------------------------
    Public Function gChkTextNumSpan(ByVal dblMin As Double, _
                                    ByVal dblMax As Double, _
                                    ByVal strInput As String, _
                           Optional ByVal blnShowMessageBox As Boolean = True, _
                           Optional ByVal strSubMessage As String = "") As Boolean

        Try

            Dim dblInput As Double

            ''入力値が数値ではない場合は何もしない
            If Not IsNumeric(strInput) Then
                Return True
            End If

            ''型変換
            dblInput = CDbl(strInput)

            ''数値が範囲外かチェック
            If dblInput < dblMin Or dblInput > dblMax Then

                ''メッセージを出す場合
                If blnShowMessageBox Then

                    If strSubMessage = "" Then
                        Call MessageBox.Show("Please set number among '" & dblMin & "'-'" & dblMax & "'.", _
                                            "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Call MessageBox.Show("Please set number among '" & dblMin & "'-'" & dblMax & "'." & vbNewLine & vbNewLine & strSubMessage, _
                                            "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If

                Return True

            Else

                Return False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    '--------------------------------------------------------------------
    ' 機能      : テキスト入力範囲チェック
    ' 返り値    : True:範囲外（数値以外）、False:範囲内
    ' 引き数    : ARG1 - (I ) 最小値
    ' 　　　    : ARG2 - (I ) 最大値
    ' 　　　    : ARG3 - (I ) 入力値
    ' 　　　    : ARG4 - (I ) メッセージ表示フラグ
    ' 　　　    : ARG5 - (I ) サブメッセージ文字列
    ' 機能説明  : 指定された条件での入力可否を返す
    '--------------------------------------------------------------------
    Public Function gChkTextNumSpanExtGus(ByVal dblMin As Double, _
                                    ByVal dblMax As Double, _
                                    ByVal dblZero As Double, _
                                    ByVal strInput As String, _
                           Optional ByVal blnShowMessageBox As Boolean = True, _
                           Optional ByVal strSubMessage As String = "") As Boolean

        Try

            Dim dblInput As Double

            ''入力値が数値ではない場合は何もしない
            If Not IsNumeric(strInput) Then
                Return True
            End If

            ''型変換
            dblInput = CDbl(strInput)

            ''数値が範囲外かチェック
            If dblZero <> dblInput And dblInput < dblMin Or dblInput > dblMax Then

                ''メッセージを出す場合
                If blnShowMessageBox Then

                    If strSubMessage = "" Then
                        Call MessageBox.Show("Please set number among '0' or '" & dblMin & "'-'" & dblMax & "'.", _
                                            "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Call MessageBox.Show("Please set number among '" & dblMin & "'-'" & dblMax & "'." & vbNewLine & vbNewLine & strSubMessage, _
                                            "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If

                Return True

            Else

                Return False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力キーチェック
    ' 返り値    : True:数値、False:数値以外
    ' 引き数    : ARG1 - (I ) 入力キー
    ' 機能説明  : 入力キーが数値かを判断する
    '--------------------------------------------------------------------
    Public Function gKeyPress(ByVal KeyChar As Char) As Boolean

        Try

            If Asc(KeyChar) = 8 Then    ''BackSpaceキーは例外とする

                gKeyPress = False

            ElseIf KeyChar < "0"c Or KeyChar > "9"c Then

                gKeyPress = True
            Else

                gKeyPress = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "入力不可文字置換"

    '--------------------------------------------------------------------
    ' 機能      : システム全体での入力不可文字置き換え
    ' 返り値    : 置き換え後文字列
    ' 引き数    : ARG1 - (I ) 入力テキスト
    ' 機能説明  : システム全体での入力不可文字を空文字に置き換える
    '--------------------------------------------------------------------
    Public Function gConvSystemNotInput(ByVal strText As String) As String

        Dim strRtn As String = ""

        Try

            ''バックスラッシュ置き換え
            strRtn = Replace(strText, "\", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "Bit操作"

#Region "1バイト"

    '--------------------------------------------------------------------
    ' 機能      : ビット設定
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (I ) ビット設定前の値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 　　　    : ARG3 - (I ) ONフラグ
    ' 機能説明  : 指定位置のビットを操作する
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitSet(ByVal bytInput As Byte, _
                            ByVal intBitPos As Integer, _
                            ByVal blnFlgBitON As Boolean) As Byte

        Try

            Dim bytRtn As Byte = 0

            If gBitCheck(bytInput, intBitPos) Then

                '=====================================
                ''操作するビットが既に立っている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 1 → 1
                    bytRtn = bytInput

                Else

                    ''ビット操作 1 → 0
                    bytRtn = bytInput - (2 ^ intBitPos)

                End If

            Else

                '=====================================
                ''操作するビットが既に寝ている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 0 → 1
                    bytRtn = bytInput Or 2 ^ intBitPos

                Else

                    ''ビット操作 0 → 0
                    bytRtn = bytInput

                End If

            End If

            Return bytRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビットチェック
    ' 返り値    : True :ビットON
    ' 　　　    : False:ビットOFF
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックする
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitCheck(ByVal bytInput As Byte, _
                              ByVal intBitPos As Integer) As Boolean

        Try

            If bytInput And 2 ^ (intBitPos) Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "2バイト"

    '--------------------------------------------------------------------
    ' 機能      : ビット設定
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (I ) ビット設定前の値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 　　　    : ARG3 - (I ) ONフラグ
    ' 機能説明  : 指定位置のビットを操作する
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitSet(ByVal shtInput As Short, _
                            ByVal intBitPos As Integer, _
                            ByVal blnFlgBitON As Boolean) As Short

        Try

            Dim shtRtn As Short = 0

            If gBitCheck(shtInput, intBitPos) Then

                '=====================================
                ''操作するビットが既に立っている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 1 → 1
                    shtRtn = shtInput

                Else

                    ''ビット操作 1 → 0
                    shtRtn = shtInput - (2 ^ intBitPos)

                End If

            Else

                '=====================================
                ''操作するビットが既に寝ている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 0 → 1
                    shtRtn = shtInput Or 2 ^ intBitPos

                Else

                    ''ビット操作 0 → 0
                    shtRtn = shtInput

                End If

            End If

            Return shtRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビット比較
    ' 返り値    : True :ビットON
    ' 　　　    : False:ビットOFF
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックする
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitCheck(ByVal shtInput As Short, _
                              ByVal intBitPos As Integer) As Boolean

        Try

            If shtInput And 2 ^ (intBitPos) Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビット値
    ' 返り値    : ビットON :1
    ' 　　　    : ビットOFF:0
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックして1/0を返す
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitValue(ByVal shtInput As Short, _
                              ByVal intBitPos As Integer) As Integer

        Try

            If shtInput And 2 ^ (intBitPos) Then
                Return 1
            Else
                Return 0
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "4バイト"

    '--------------------------------------------------------------------
    ' 機能      : ビット設定
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (I ) ビット設定前の値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 　　　    : ARG3 - (I ) ONフラグ
    ' 機能説明  : 指定位置のビットを操作する
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitSet(ByVal intInput As Integer, _
                            ByVal intBitPos As Integer, _
                            ByVal blnFlgBitON As Boolean) As Integer

        Try

            Dim intRtn As Integer = 0

            If gBitCheck(intInput, intBitPos) Then

                '=====================================
                ''操作するビットが既に立っている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 1 → 1
                    intRtn = intInput

                Else

                    ''ビット操作 1 → 0
                    intRtn = intInput - (2 ^ intBitPos)

                End If

            Else

                '=====================================
                ''操作するビットが既に寝ている場合
                '=====================================
                If blnFlgBitON Then

                    ''ビット操作 0 → 1
                    intRtn = intInput Or 2 ^ intBitPos

                Else

                    ''ビット操作 0 → 0
                    intRtn = intInput

                End If

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビットチェック
    ' 返り値    : True :ビットON
    ' 　　　    : False:ビットOFF
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックする
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitCheck(ByVal intInput As Integer, _
                              ByVal intBitPos As Integer) As Boolean

        Try

            If intInput And 2 ^ (intBitPos) Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビットチェック
    ' 返り値    : True :ビットON
    ' 　　　    : False:ビットOFF
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックする
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    ' 履歴    　: 2019.02.05 倉重作成
    '--------------------------------------------------------------------
    Public Function gBitMotorCheck(ByVal intMask As Integer, _
                              ByVal intMotorNo As Integer, ByVal intMotorStatus As Integer) As Boolean

        Dim MotorMask As Integer = 0   ''モーター種別からモーターのマスクを調べる

        Select Case intMotorNo
            Case 0, 7  ''RUN ''RUN-G
                Select Case intMotorStatus
                    Case 0
                        MotorMask = 2
                    Case 1
                        MotorMask = 64
                    Case Else
                        MotorMask = 0
                End Select
            Case 1, 8, 9  ''RUN-A ''RUN-H ''RUN-I
                Select Case intMotorStatus
                    Case 0
                        MotorMask = 2
                    Case 1
                        MotorMask = 64
                    Case 2
                        MotorMask = 32
                    Case Else
                        MotorMask = 0
                End Select
            Case 2, 3, 5  ''RUN-B ''RUN-C ''RUN-E
                Select Case intMotorStatus
                    Case 0
                        MotorMask = 2
                    Case 1
                        MotorMask = 4
                    Case 2
                        MotorMask = 64
                    Case Else
                        MotorMask = 0
                End Select
            Case 4, 6  ''RUN-D ''RUN-F
                Select Case intMotorStatus
                    Case 0
                        MotorMask = 2
                    Case 1
                        MotorMask = 4
                    Case 2
                        MotorMask = 8
                    Case 3
                        MotorMask = 16
                    Case 4
                        MotorMask = 64
                    Case Else
                        MotorMask = 0
                End Select

            Case 10, 11  ''RUN-J ''RUN-K
                Select Case intMotorStatus
                    Case 0
                        MotorMask = 2
                    Case 1
                        MotorMask = 4
                    Case 2
                        MotorMask = 64
                    Case 3
                        MotorMask = 32
                    Case Else
                        MotorMask = 0
                End Select
        End Select



        Try

            If intMask = MotorMask Then
                Return 1
            Else
                Return 0
            End If



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ビット値
    ' 返り値    : ビットON :1
    ' 　　　    : ビットOFF:0
    ' 引き数    : ARG1 - (I ) チェックを行う値
    ' 　　　    : ARG2 - (I ) ビット位置
    ' 機能説明  : 指定位置ビットのON/OFFをチェックして1/0を返す
    ' 備考      : ビット位置は 0 から （ 1 からではない）
    '--------------------------------------------------------------------
    Public Function gBitValue(ByVal intInput As Integer, _
                              ByVal intBitPos As Integer) As Integer

        Try

            If intInput And 2 ^ (intBitPos) Then
                Return 1
            Else
                Return 0
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

#Region "バイト配列コピー"

    '--------------------------------------------------------------------
    ' 機能      : ビットコピー
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) コピー元配列
    ' 　　　    : ARG2 - (I ) コピー先配列
    ' 　　　    : ARG3 - (I ) コピー開始配列番号
    ' 機能説明  : バイト配列のコピーを行う
    '--------------------------------------------------------------------
    Public Sub gCopyByteArray(ByVal bytSource() As Byte, _
                              ByVal bytTarget() As Byte, _
                              ByVal intCopyStartIndex As Integer)

        Try

            For i As Integer = LBound(bytSource) To UBound(bytSource)

                bytTarget(intCopyStartIndex + i) = bytSource(i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データマップコンボ作成"

    '--------------------------------------------------------------------
    ' 機能      : データマッピングコンボ作成
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (IO) コンボ
    ' 　　　    : ARG2 - (I ) コード配列
    ' 　　　    : ARG3 - (I ) 名称配列
    ' 　　　    : ARG4 - (I ) ブランクフラグ
    ' 機能説明  : データマッピングコンボボックスを作成する
    ' 備考      : 通常のコンボボックス用
    '--------------------------------------------------------------------
    'Public Function gMakeDataMapCombo(ByRef cmbCombo As ComboBox, _
    '                                  ByVal strCode() As String, _
    '                                  ByVal strName() As String, _
    '                         Optional ByVal blnBlank As Boolean = False, _
    '                         Optional ByVal blnSelectTop As Boolean = False) As Integer
    Public Function gMakeDataMapCombo(ByRef cmbCombo As ComboBox, _
                                      ByVal strCode() As String, _
                                      ByVal strName() As String, _
                             Optional ByVal blnBlank As Boolean = False) As Integer

        Try

            Dim i As Integer
            Dim dstDataset As New DataSet
            Dim strwk(1) As String

            ''テーブル作成
            dstDataset.Tables.Add("Table1")

            ''列作成
            dstDataset.Tables(0).Columns.Add("Code")
            dstDataset.Tables(0).Columns.Add("Name")

            ''行追加
            For i = LBound(strCode) To UBound(strCode)

                strwk(0) = strCode(i)
                strwk(1) = strName(i)
                Call dstDataset.Tables(0).Rows.Add(strwk)

            Next

            ''コンボクリア
            cmbCombo.DataSource = Nothing
            cmbCombo.Items.Clear()

            ''先頭に空白行を追加
            If blnBlank Then Call gAddBlankRowToDataset(dstDataset)

            Try

                ''データ連結
                cmbCombo.ValueMember = dstDataset.Tables(0).Columns(0).ColumnName
                cmbCombo.DisplayMember = dstDataset.Tables(0).Columns(1).ColumnName
                cmbCombo.DataSource = dstDataset.Tables(0)

            Catch ex As Exception
                Return -1
            End Try

            'If blnSelectTop Then
            '    cmbCombo.SelectedIndex = 0
            'Else
            '    cmbCombo.SelectedIndex = -1
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データマッピングコンボ作成
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (IO) コンボ
    ' 　　　    : ARG2 - (I ) コード配列
    ' 　　　    : ARG3 - (I ) 名称配列
    ' 　　　    : ARG4 - (I ) ブランクフラグ
    ' 　　　    : ARG5 - (I ) 先頭行選択フラグ
    ' 機能説明  : データマッピングコンボボックスを作成する
    ' 備考      : DataGridViewのコンボボックス用
    '--------------------------------------------------------------------
    Public Function gMakeDataMapCombo(ByRef cmbCombo As DataGridViewComboBoxColumn, _
                                      ByVal strCode() As String, _
                                      ByVal strName() As String, _
                             Optional ByVal blnBlank As Boolean = False, _
                             Optional ByVal blnSelectTop As Boolean = False) As Integer

        Try

            Dim i As Integer
            Dim dstDataset As New DataSet
            Dim strwk(1) As String

            ''テーブル作成
            dstDataset.Tables.Add("Table1")

            ''列作成
            dstDataset.Tables(0).Columns.Add("Code")
            dstDataset.Tables(0).Columns.Add("Name")

            ''行追加
            For i = LBound(strCode) To UBound(strCode)

                strwk(0) = strCode(i)
                strwk(1) = strName(i)
                Call dstDataset.Tables(0).Rows.Add(strwk)

            Next

            ''コンボクリア
            cmbCombo.DataSource = Nothing
            cmbCombo.Items.Clear()

            ''先頭に空白行を追加
            If blnBlank Then Call gAddBlankRowToDataset(dstDataset)

            Try

                ''データ連結
                cmbCombo.ValueMember = dstDataset.Tables(0).Columns(0).ColumnName
                cmbCombo.DisplayMember = dstDataset.Tables(0).Columns(1).ColumnName
                cmbCombo.DataSource = dstDataset.Tables(0)

            Catch ex As Exception
                Return -1
            End Try

            If blnSelectTop Then
                'cmbCombo.DisplayIndex = 0
            Else
                'cmbCombo.DisplayIndex = -1
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : データマッピングコンボ作成
    ' 返り値    : ビット設定後の値
    ' 引き数    : ARG1 - (IO) コンボ
    ' 　　　    : ARG2 - (I ) コード配列
    ' 　　　    : ARG3 - (I ) 名称配列
    ' 　　　    : ARG4 - (I ) ブランクフラグ
    ' 　　　    : ARG5 - (I ) 先頭行選択フラグ
    ' 機能説明  : データマッピングコンボボックスを作成する
    ' 備考      : DataGridViewのコンボボックス用
    '--------------------------------------------------------------------
    Public Function gMakeDataMapCombo(ByRef cmbCombo As DataGridViewComboBoxCell, _
                                      ByVal strCode() As String, _
                                      ByVal strName() As String, _
                             Optional ByVal blnBlank As Boolean = False, _
                             Optional ByVal blnSelectTop As Boolean = False) As Integer

        Try

            Dim i As Integer
            Dim dstDataset As New DataSet
            Dim strwk(1) As String

            ''テーブル作成
            dstDataset.Tables.Add("Table1")

            ''列作成
            dstDataset.Tables(0).Columns.Add("Code")
            dstDataset.Tables(0).Columns.Add("Name")

            ''行追加
            For i = LBound(strCode) To UBound(strCode)

                strwk(0) = strCode(i)
                strwk(1) = strName(i)
                Call dstDataset.Tables(0).Rows.Add(strwk)

            Next

            ''コンボクリア
            cmbCombo.DataSource = Nothing
            cmbCombo.Items.Clear()

            ''先頭に空白行を追加
            If blnBlank Then Call gAddBlankRowToDataset(dstDataset)

            Try

                ''データ連結
                cmbCombo.ValueMember = dstDataset.Tables(0).Columns(0).ColumnName
                cmbCombo.DisplayMember = dstDataset.Tables(0).Columns(1).ColumnName
                cmbCombo.DataSource = dstDataset.Tables(0)

            Catch ex As Exception
                Return -1
            End Try

            If blnSelectTop Then
                'cmbCombo.DisplayIndex = 0
            Else
                'cmbCombo.DisplayIndex = -1
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データセット操作"

    '--------------------------------------------------------------------
    ' 機能      : 空白行付加
    ' 返り値    : 0:成功  <> 0:失敗
    ' 引き数    : ARG1 - ( O) データセット 
    ' 　　　    : ARG2 - (I ) 表示列番号
    ' 　　　    : ARG3 - (I ) 表示列名
    ' 機能説明  : データセットの先頭に空白行を付加する
    ' 備考      : 引数で渡されるデータセットにはデータが設定されている事
    '--------------------------------------------------------------------
    Public Function gAddBlankRowToDataset(ByRef ds As DataSet, _
                                 Optional ByVal intDisplayColNo As Integer = 0, _
                                 Optional ByVal strDisplayName As String = "") As Integer

        Try

            Dim i As Integer

            ''データセットにデータが設定されていない場合は失敗を返す
            If ds Is Nothing Then Return Not 0

            Try

                ''先頭に空白行を追加
                Dim drRow As DataRow
                drRow = ds.Tables(0).NewRow()

                For i = 0 To ds.Tables(0).Columns.Count - 1

                    If i = intDisplayColNo Then
                        drRow.Item(i) = strDisplayName
                    Else
                        drRow.Item(i) = ""
                    End If

                Next

                ds.Tables(0).Rows.InsertAt(drRow, 0)

            Catch ex As Exception
                Return Not 0
            End Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 行削除
    ' 返り値    : 0:成功  1:行番号なし -1:失敗
    ' 引き数    : ARG1 - (I ) 削除する行番号
    '           : ARG2 - ( O) データセット 
    ' 機能説明  : 指定の行を削除する
    ' 備考      : 引数で渡されるデータセットにはデータが設定されている事
    '--------------------------------------------------------------------
    Public Function gDelRowToDataset(ByVal intIndex As Integer, _
                                     ByRef dstDataset As DataSet) As Integer

        Try

            Dim intCnt As Integer
            Dim dtDataTable As DataTable

            Try

                ''データセットにデータが設定されていない場合は失敗を返す
                If dstDataset Is Nothing Then Return -1

                ''データテーブルを設定
                dtDataTable = dstDataset.Tables(0)

                ''行数分繰り返す
                For intCnt = 0 To dstDataset.Tables(0).Rows.Count

                    ''削除する行番号の場合
                    If intCnt = intIndex Then

                        ''行を削除する
                        Call dtDataTable.Rows.Remove(dstDataset.Tables(0).Rows(intCnt))
                        Return 0

                    End If
                Next

                ''削除する行番号が見つからなかった場合は 1 を返す
                Return 1

            Catch ex As Exception

                Return -1

            End Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 列削除
    ' 返り値    : 0:成功  1:列番号なし -1:失敗
    ' 引き数    : ARG1 - (I ) 削除する列番号
    '           : ARG2 - ( O) データセット 
    ' 機能説明  : 指定の列を削除する
    ' 備考      : 引数で渡されるデータセットにはデータが設定されている事
    '--------------------------------------------------------------------
    Public Function gDelColToDataset(ByVal intIndex As Integer, _
                                     ByRef dstDataset As DataSet) As Integer

        Try

            Dim intCnt As Integer
            Dim dtDataTable As DataTable

            Try

                ''データセットにデータが設定されていない場合は失敗を返す
                If dstDataset Is Nothing Then Return -1

                ''データテーブルを設定
                dtDataTable = dstDataset.Tables(0)

                ''列数分繰り返す
                For intCnt = 0 To dstDataset.Tables(0).Columns.Count

                    ''削除する列番号の場合
                    If intCnt = intIndex Then

                        ''列を削除する
                        Call dtDataTable.Columns.Remove(dstDataset.Tables(0).Columns(intCnt))
                        Return 0

                    End If
                Next

                ''削除する行番号が見つからなかった場合は 1 を返す
                Return 1

            Catch ex As Exception

                Return -1

            End Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "バージョンフォルダ取得"

    '--------------------------------------------------------------------
    ' 機能      : バージョン番号取得
    ' 返り値    : 0:成功
    ' 　　　    : 1:フォルダ自体が存在しない
    ' 　　　    : 2:バージョンフォルダが存在しない
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    '           : ARG2 - ( O) バージョン番号配列
    ' 機能説明  : 指定フォルダのバージョン番号配列を取得する
    '--------------------------------------------------------------------
    Public Function gGetVerNums(ByVal udtFileInfo As gTypFileInfo, _
                                ByRef strVerNums() As String) As Integer

        Try

            Dim intRtn As Integer
            Dim intCnt As Integer
            Dim strPath As String
            Dim strFolders() As String
            Dim strLastFolderName As String

            With udtFileInfo

                ''バージョンフォルダがあるパスを作成
                strPath = System.IO.Path.Combine(.strFilePath, .strFileName)

                ''フォルダが存在しない場合
                If Not System.IO.Directory.Exists(strPath) Then
                    intRtn = 1
                Else

                    ''フォルダを取得
                    strFolders = System.IO.Directory.GetDirectories(strPath)

                    ''フォルダが存在しない場合
                    If strFolders Is Nothing Then
                        intRtn = 1
                    Else
                        'ファイル管理仕様変更 T.Ueki
                        intRtn = 0

                        Erase strVerNums
                        For i As Integer = LBound(strFolders) To UBound(strFolders)

                            ''パスの最終フォルダを取得
                            strLastFolderName = mGetLastFolder(strFolders(i))

                            ''パスの最終フォルダがバージョンフォルダの場合
                            If mChkVersionFolder(strLastFolderName) Then

                                ReDim Preserve strVerNums(intCnt)
                                strVerNums(intCnt) = mGetVerNum(strLastFolderName)

                                intCnt += 1

                            End If

                        Next

                        ' ''バージョンフォルダが１つも無かった場合
                        'If strVerNums Is Nothing Then
                        '    intRtn = 2
                        'Else
                        '    intRtn = 0
                        'End If

                    End If

                End If

            End With

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : バージョンフォルダ存在確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) パス
    ' 機能説明  : バージョン、コンパイルフォルダが存在するか確認する
    '--------------------------------------------------------------------
    Public Function gExistFolderVersion(ByVal strPath As String, ByVal CFRead As Boolean) As Boolean

        Try

            Dim intCnt As Integer = 0
            Dim strFolders() As String
            Dim strLastFolderName As String

            ''フォルダが存在しない場合
            If Not System.IO.Directory.Exists(strPath) Then
                Return False
            Else

                ''フォルダを取得
                strFolders = System.IO.Directory.GetDirectories(strPath)

                If CFRead = True Then
                    strLastFolderName = ""
                    Return True
                Else
                    For i As Integer = LBound(strFolders) To UBound(strFolders)

                        ''パスの最終フォルダを取得
                        strLastFolderName = mGetLastFolder(strFolders(i))
                        Return True

                    Next
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : バージョンフォルダ確認
    ' 返り値    : True :バージョンフォルダである
    ' 　　　    : False:バージョンフォルダでない
    ' 引き数    : ARG1 - (I ) フォルダ名
    ' 機能説明  : フォルダ名がバージョンフォルダであるか確認する
    '--------------------------------------------------------------------
    Private Function mChkVersionFolder(ByVal strFolderName As String) As Boolean

        Try

            Dim strVer As String = ""
            Dim strNum As String = ""

            ''引数で指定されたフォルダ名から、バージョンフォルダプレフィックス部分を切り取る
            strVer = Mid(strFolderName, 1, gCstVersionPrefix.Length)

            ''引数で指定されたフォルダ名から、バージョン番号部分を切り取る
            strNum = Mid(strFolderName, gCstVersionPrefix.Length + 1, strFolderName.Length)

            ''プレフィックス部分が同じでバージョン部分が数値の場合
            If strVer = gCstVersionPrefix And IsNumeric(strNum) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : バージョン番号取得
    ' 返り値    : バージョン番号
    ' 引き数    : ARG1 - (I ) フォルダ名
    ' 機能説明  : バージョンフォルダからバージョン番号を取得する
    '--------------------------------------------------------------------
    Private Function mGetVerNum(ByVal strFolderName As String) As String

        Try

            Return Mid(strFolderName, gCstVersionPrefix.Length + 1, strFolderName.Length)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : フォルダ取得
    ' 返り値    : フォルダ名(船名+アルファベット)
    ' 引き数    : ARG1 - (I ) パス
    ' 機能説明  : 指定パスの[船名+アルファベット]フォルダを取得する
    '--------------------------------------------------------------------
    Private Function mGetLastFolder(ByVal strPath As String) As String

        Dim strRtn As String = ""
        Dim strwk() As String = Nothing

        Try

            strwk = strPath.Split("\")

            For i As Integer = LBound(strwk) To UBound(strwk)

                'T.Ueki フォルダ管理仕様変更
                If i = UBound(strwk) - 1 Then
                    strRtn = strwk(i)
                End If

                'If i = UBound(strwk) - 1 Then
                '    strRtn = strwk(i)
                'End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "フォルダ作成"

    '--------------------------------------------------------------------
    ' 機能      : フォルダ作成
    ' 返り値    : 0:成功
    ' 　　　    :-1:失敗
    ' 引き数    : ARG1 - (I ) パス
    ' 機能説明  : フォルダを作成する
    '--------------------------------------------------------------------
    Public Function gMakeFolder(ByRef strPath As String) As Integer

        Try

            Dim intRtn As Integer = 0

            Try
                Call System.IO.Directory.CreateDirectory(strPath)
            Catch ex As Exception
                intRtn = -1
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "文字列比較"

    '--------------------------------------------------------------------
    ' 機能      : 文字列比較
    ' 返り値    : True:同じ文字、False:異なる文字
    ' 引き数    : ARG1 - (I ) 比較文字１
    ' 　　　    : ARG2 - (I ) 比較文字２
    '           : ｽﾍﾟｰｽ削除ﾌﾗｸﾞ   2015.10.30 Ver1.7.5 追加
    ' 機能説明  : 文字列が同じか比較する
    ' 補足　　  : NULLは削除してから比較する
    '--------------------------------------------------------------------
    Public Function gCompareString(ByVal strCompare1 As String, _
                                   ByVal strCompare2 As String, Optional ByVal blnTrim As Boolean = True) As Boolean

        Try

            Dim strwk1 As String
            Dim strwk2 As String

            strwk1 = gGetString(strCompare1, blnTrim)
            strwk2 = gGetString(strCompare2, blnTrim)

            If strwk1 = strwk2 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "印刷処理"

    '--------------------------------------------------------------------
    ' 機能      : 画面のハードコピー
    ' 返り値    : 0: 印刷成功
    '           : 1: キャンセルボタンクリック
    ' 　　　    :-1: 印刷失敗
    ' 引き数    : ARG1 - (I ) True: アクティブウィンドウ、False: フルスクリーン
    '           : ARG2 - (I ) True: エラーメッセージ表示、False: エラーメッセージ非表示
    ' 機能説明  : 画面印刷を行う
    '--------------------------------------------------------------------
    Public Function gPrintScreen(ByVal blnCapType As Boolean, _
                        Optional ByVal blnShowErrMsg As Boolean = True) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim pdlg As System.Windows.Forms.PrintDialog = Nothing
            Dim objPrinter As New Printing.PrintDocument
            Dim objHardCopy As New NonHCopyNet.HardCopyClass ''ハードコピーオブジェクト

            Try
                pdlg = New System.Windows.Forms.PrintDialog
                pdlg.UseEXDialog = True         '' 64bit版対応 2014.09.18

                ''PrintDialogで[OK]が押された場合のみ印刷
                If pdlg.ShowDialog = Windows.Forms.DialogResult.OK Then

                    '印刷情報の設定（PrintDialogの指定値を引継ぐ）
                    objPrinter.PrinterSettings = pdlg.PrinterSettings

                    ''印刷の実行
                    objHardCopy.HardCopy(blnCapType, objPrinter)
                    intRtn = 0

                Else

                    ''印刷キャンセル
                    intRtn = 1

                End If

            Catch ex As Exception

                ''印刷失敗
                If blnShowErrMsg Then
                    MessageBox.Show("印刷に失敗しました。", "Print Screen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                intRtn = -1

            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "空文字変換"

    '-------------------------------------------------------------------- 
    ' 機能      : 空文字変換処理
    ' 返り値    : Long型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 設定文字列をLong型で返す
    '           : 設定文字列が空文字だった場合は0を返す
    '--------------------------------------------------------------------
    Public Function gConvNullToZero(ByVal strNum As String) As Long

        Try

            If Trim(strNum) = "" Then
                Return 0
            Else
                Return CLng(strNum)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 空文字変換処理
    ' 返り値    : Single型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 設定文字列をSingle型で返す
    '           : 設定文字列が空文字だった場合は0を返す
    '--------------------------------------------------------------------
    Public Function gConvNullToZeroSingle(ByVal strNum As String) As Single

        Try

            If Trim(strNum) = "" Then
                Return 0
            Else
                Return CSng(strNum)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 設定値変換処理
    ' 返り値    : フォーマット文字列
    ' 引き数    : ARG1 - (I ) Long型の設定値
    '           : ARG2 - (I ) フォーマット書式
    ' 機能説明  : 設定値をフォーマット書式の文字列で返す
    '           : 設定値が0だった場合は空文字を返す
    '--------------------------------------------------------------------
    Public Function gConvZeroToNull(ByVal lngNum As Long, _
                           Optional ByVal strFormat As String = "") As String

        Try

            If lngNum = 0 Then
                Return ""
            Else

                If strFormat = "" Then
                    Return CStr(lngNum)
                Else
                    Return Format(lngNum, strFormat)
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値変換処理
    ' 返り値    : フォーマット文字列
    ' 引き数    : ARG1 - (I ) Single型の設定値
    '           : ARG2 - (I ) フォーマット書式
    ' 機能説明  : 設定値をフォーマット書式の文字列で返す
    '           : 設定値が0だった場合は空文字を返す
    '--------------------------------------------------------------------
    Public Function gConvZeroToNull(ByVal sngNum As Single, _
                           Optional ByVal strFormat As String = "") As String

        Try

            If sngNum = 0 Then
                Return ""
            Else

                If strFormat = "" Then
                    Return CStr(sngNum)
                Else
                    Return Format(sngNum, strFormat)
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 空文字変換処理
    ' 返り値    : フォーマット文字列
    ' 引き数    : ARG1 - (I ) Long型の設定値
    '           : ARG2 - (I ) Hexフラグ（True:16進文字列、False:10進文字列）
    ' 機能説明  : HexフラグがTrueだった場合は16進の文字列で返す
    '           : HexフラグがFalseだった場合は10進の文字列で返す
    '           : 設定値が0だった場合は空文字を返す
    '--------------------------------------------------------------------
    Public Function gConvZeroToNull(ByVal lngNum As Long, _
                                    ByVal blnReturnHex As Boolean) As String

        Try

            If lngNum = 0 Then
                Return ""
            Else

                If blnReturnHex Then
                    Return CStr(Hex(lngNum))
                Else
                    Return CStr(lngNum)
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "型変換関数"

#Region "String → Byte"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : byte型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をbyte型で返す
    '--------------------------------------------------------------------
    Public Function CCbyte(ByVal hstrData As String) As Byte

        Try

            Dim ans As Byte = 0
            Dim blnOut As Boolean

            Try
                blnOut = Byte.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : byte型の設定値（16進）
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列を16進のbyte型で返す
    '--------------------------------------------------------------------
    Public Function CCbyteHex(ByVal hstrData As String) As Byte

        Try

            Dim ans As Byte = 0

            Try
                If hstrData = "" Then Return ans

                ans = CByte("&H" & Trim(hstrData))

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Short"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : short型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をshort型で返す
    '--------------------------------------------------------------------
    Public Function CCShort(ByVal hstrData As String) As Short

        Try

            Dim ans As Short = 0
            Dim blnOut As Boolean

            Try
                blnOut = Short.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Integer"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : Int型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をInt型で返す
    '--------------------------------------------------------------------
    Public Function CCInt(ByVal hstrData As String) As Integer

        Try

            Dim ans As Integer = 0
            Dim blnOut As Boolean

            Try
                blnOut = Integer.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Integer"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理(mCableMark用)
    ' 返り値    : Int型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をInt型で返す
    '--------------------------------------------------------------------
    Public Function CCInt2(ByVal hstrData As String) As Integer

        Try

            Dim ans As Integer = 0
            Dim blnOut As Boolean

            If hstrData = "" Then
                blnOut = Integer.TryParse(Trim(gCstCodeChCommonFuNoNothing), ans)
                Return ans
                Exit Function
            End If



            Try
                blnOut = Integer.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Uint16"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : UInt16型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をUint16型で返す
    '--------------------------------------------------------------------
    Public Function CCUInt16(ByVal hstrData As String) As UInt16

        Try

            Dim ans As UInt16 = 0
            Dim blnOut As Boolean

            Try
                blnOut = UInt16.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : UInt16型の設定値（16進）
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列を16進のUint16型で返す
    '--------------------------------------------------------------------
    Public Function CCUInt16Hex(ByVal hstrData As String) As UInt16

        Try

            Dim ans As UInt16 = 0

            Try

                If hstrData = "" Then Return ans

                ans = CUInt("&H" & Trim(hstrData))

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Uint32"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : UInt32型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をUint32型で返す
    '--------------------------------------------------------------------
    Public Function CCUInt32(ByVal hstrData As String) As UInt32

        Try

            Dim ans As UInt32 = 0
            Dim blnOut As Boolean

            Try
                blnOut = UInt32.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : UInt32型の設定値（16進）
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列を16進のUint32型で返す
    '--------------------------------------------------------------------
    Public Function CCUInt32Hex(ByVal hstrData As String) As UInt32

        Try

            Dim ans As UInt32 = 0

            Try

                If hstrData = "" Then Return ans

                ans = CUInt("&H" & Trim(hstrData))

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Long"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : Long型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をLong型で返す
    '--------------------------------------------------------------------
    Public Function CCLong(ByVal hstrData As String) As Long

        Try

            Dim ans As Long = 0
            Dim blnOut As Boolean

            Try
                blnOut = Long.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Single"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : Single型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をSingle型で返す
    '--------------------------------------------------------------------
    Public Function CCSingle(ByVal hstrData As String) As Single

        Try

            Dim ans As Single = 0
            Dim blnOut As Boolean

            Try
                blnOut = Single.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "String → Double"

    '-------------------------------------------------------------------- 
    ' 機能      : 型変換処理
    ' 返り値    : Double型の設定値
    ' 引き数    : ARG1 - (I ) 設定文字列
    ' 機能説明  : 文字列をDouble型で返す
    '--------------------------------------------------------------------
    Public Function CCDouble(ByVal hstrData As String) As Double

        Try

            Dim ans As Double = 0
            Dim blnOut As Boolean

            Try
                blnOut = Double.TryParse(Trim(hstrData), ans)

            Catch ex As Exception

            End Try

            Return ans

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

#Region "チャンネルグループデータ作成"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルグループデータ作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 　　　    : ARG2 - ( O) チャンネルグループ構造体
    ' 機能説明  : チャンネル設定構造体からチャンネルグループ構造体を作成する
    '--------------------------------------------------------------------
    Public Sub gMakeChannelGroupData(ByVal udtSetChannel As gTypSetChInfo, _
                                     ByRef udtChannelGroup As gTypChannelGroup)

        Try

            Dim intCurGroupNo As Integer
            Dim intNextDataSetIndex As Integer

            ''配列初期化
            Erase udtChannelGroup.udtGroup

            ''配列再定義
            ReDim udtChannelGroup.udtGroup(gCstChannelGroupMax - 1)
            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup)

                ReDim udtChannelGroup.udtGroup(i).udtChannelData(gCstOneGroupChannelMax - 1)


                For j As Integer = 0 To gCstOneGroupChannelMax - 1
                    ReDim udtChannelGroup.udtGroup(i).udtChannelData(j).udtChTypeData(gCstByteCntChannelType - 1)
                Next

            Next

            For i As Integer = 0 To UBound(udtSetChannel.udtChannel)

                With udtSetChannel.udtChannel(i)

                    ''対象グループ番号取得
                    intCurGroupNo = .udtChCommon.shtGroupNo

                    ''グループ番号が設定されている場合
                    If intCurGroupNo > 0 Then

                        ''データ設定位置を取得
                        intNextDataSetIndex = udtChannelGroup.udtGroup(intCurGroupNo - 1).intDataCnt

                        If intNextDataSetIndex < gCstOneGroupChannelMax Then

                            ''対象位置にチャンネルデータを設定
                            udtChannelGroup.udtGroup(intCurGroupNo - 1).udtChannelData(intNextDataSetIndex) = udtSetChannel.udtChannel(i)
                            udtChannelGroup.udtGroup(intCurGroupNo - 1).intDataCnt += 1

                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネルグループデータ作成(CH表示設定順)"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルグループデータ作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 　　　    : ARG2 - ( O) チャンネルグループ構造体
    ' 機能説明  : チャンネル設定構造体からチャンネルグループ構造体を作成する
    '--------------------------------------------------------------------
    Public Sub gMakeChannelData(ByVal udtSetChannel As gTypSetChInfo, _
                                ByRef udtChannelGroup As gTypChannelGroup)

        Try

            Dim intCurGroupNo As Integer
            Dim intCHDispPos As Integer

            ''配列初期化
            Erase udtChannelGroup.udtGroup

            ''配列再定義
            ReDim udtChannelGroup.udtGroup(gCstChannelGroupMax - 1)
            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup)

                ReDim udtChannelGroup.udtGroup(i).udtChannelData(gCstOneGroupChannelMax - 1)


                For j As Integer = 0 To gCstOneGroupChannelMax - 1
                    ReDim udtChannelGroup.udtGroup(i).udtChannelData(j).udtChTypeData(gCstByteCntChannelType - 1)
                Next

            Next

            For i As Integer = 0 To UBound(udtSetChannel.udtChannel)

                With udtSetChannel.udtChannel(i)

                    ''対象グループ番号取得
                    intCurGroupNo = .udtChCommon.shtGroupNo


                    ''グループ番号が設定されている場合
                    If intCurGroupNo > 0 Then
                        ''データ表示位置を取得
                        intCHDispPos = udtSetChannel.udtChannel(i).udtChCommon.shtDispPos

                        ''If intCHDispPos < gCstOneGroupChannelMax Then
                        If intCHDispPos <= gCstOneGroupChannelMax Then      '' Ver1.8.9 2015.12.11 表示位置が100は有り得るので含める

                            ''対象位置にチャンネルデータを設定
                            udtChannelGroup.udtGroup(intCurGroupNo - 1).udtChannelData(intCHDispPos - 1) = udtSetChannel.udtChannel(i)

                        End If
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル検索"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル検索
    ' 返り値    : True:存在、False:未存在
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 　　　    : ARG2 - (I ) チャンネルグループ構造体
    ' 　　　    : ARG3 - ( O) グループインデックス
    ' 　　　    : ARG4 - ( O) 行インデックス
    ' 機能説明  : チャンネルグループ構造体からチャンネルを検索して
    ' 　　　　  : グループインデックスと行インデックスを返す
    '--------------------------------------------------------------------
    Public Function gSearchChannel(ByVal strChNo As String, _
                                   ByVal udtChannelGroup As gTypChannelGroup, _
                                   ByRef intGroupIndex As Integer, _
                                   ByRef intRowIndex As Integer) As Boolean

        Try

            If Not IsNumeric(strChNo) Then Return False
            If strChNo = 0 Then Return False

            For i As Integer = 0 To UBound(udtChannelGroup.udtGroup)

                For j As Integer = 0 To UBound(udtChannelGroup.udtGroup(i).udtChannelData)

                    With udtChannelGroup.udtGroup(i).udtChannelData(j)

                        If .udtChCommon.shtChno = strChNo Then

                            intGroupIndex = i
                            intRowIndex = j
                            Return True

                        End If
                    End With
                Next
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報取得
    ' 返り値    : True:チャンネル存在、False:チャンネル未存在
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 　　　    : ARG2 - ( O) チャンネル情報構造体
    ' 機能説明  : 全チャンネル情報構造体からチャンネルを検索して
    ' 　　　　  : 個別のチャンネル情報構造体を返す
    '--------------------------------------------------------------------
    Public Function gGetChannelInfo(ByVal intChNo As Integer, _
                                    ByRef udtChInfo As gTypSetChRec) As Boolean

        Try

            If intChNo = 0 Then Return False

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                If gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno = intChNo Then

                    udtChInfo = DeepCopyHelper.DeepCopy(gudt.SetChInfo.udtChannel(i))
                    Return True

                End If

            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルレンジ取得
    ' 返り値    : True:チャンネル存在、False:チャンネル未存在
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 　　　    : ARG2 - ( O) レンジ上限
    ' 　　　    : ARG3 - ( O) レンジ下限
    ' 機能説明  : チャンネルグループ構造体からチャンネルを検索して
    ' 　　　　  : レンジ上限とレンジ下限を取得する
    '--------------------------------------------------------------------
    Public Function gGetChannelRange(ByVal intChNo As Integer, _
                                     ByRef intRangeHi As Integer, _
                                     ByRef intRangeLo As Integer) As Boolean

        Try

            If intChNo = 0 Then Return False

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno = intChNo Then

                        ''Range  Low
                        intRangeLo = .AnalogRangeLow

                        ''Range Hi
                        intRangeHi = .AnalogRangeHigh

                        Return True

                    End If

                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネルタイプ取得
    ' 返り値    : チャンネルタイプ
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネルグループ構造体からチャンネルを検索して
    ' 　　　　  : チャンネルタイプを返す
    '--------------------------------------------------------------------
    Public Function gGetChannelType(ByVal intChNo As Integer) As Integer

        Try

            If intChNo = 0 Then Return False

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno = intChNo Then

                        Return .udtChCommon.shtChType

                    End If

                End With
            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : システムチャンネルのデバイスステータス取得
    ' 返り値    : デバイスステータス
    ' 引き数    : ARG1 - (IO) チャンネル番号
    '           : ARG2 - (I ) 機器コード
    ' 機能説明  : チャンネルグループ構造体からチャンネルを検索して
    ' 　　　　  : システムチャンネルのデバイスステータスを返す
    '--------------------------------------------------------------------
    Public Function gGetChannelSystemDeviceStatus(ByRef cmbStatus As ComboBox, _
                                                  ByVal intFirstKikiCode As Integer) As Integer

        Try

            Dim udtKiki() As gTypCodeName = Nothing
            Dim intRtn As Integer = 0
            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListDeviceStatus)

            For ii As Integer = 1 To cmbStatus.Items.Count ''全ての機器グループ分 Loop

                Call gGetComboCodeName(udtKiki, _
                                       gEnmComboType.ctChListChannelListDeviceStatus, _
                                       ii.ToString("00"))

                For j As Integer = 0 To UBound(udtKiki)
                    If intFirstKikiCode = udtKiki(j).shtCode Then
                        intRtn = ii    ''機器グループ番号GET
                        Exit For
                    End If
                Next
                If intRtn <> 0 Then Exit For

            Next

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    'Ver2.0.3.6 コンパイルのシステムチェック高速化
    '最初にｺｰﾙする関数(Kikiを配列に格納)
    Public Sub gSetKikiAry(ByRef cmbStatus As ComboBox)
        Try
            '配列初期化
            gAryKiki = Nothing
            gAryKikiDtl = Nothing
            gAryKiki = New ArrayList
            gAryKikiDtl = New ArrayList

            Dim udtKiki() As gTypCodeName = Nothing

            'KiKiを全件取得
            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListDeviceStatus)

            'Kikiをもとに詳細取得
            For ii As Integer = 1 To cmbStatus.Items.Count ''全ての機器グループ分 Loop
                Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                            ii.ToString("00"))

                For j As Integer = 0 To UBound(udtKiki)
                    gAryKiki.Add(ii)
                    gAryKikiDtl.Add(udtKiki(j).shtCode.ToString)
                Next j
            Next ii

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Ver2.0.4.3 機器名等を高速で取得するための関数
    '最初にｺｰﾙする関数(Arayに格納)
    Public Sub gSetDBkikiAry(ByRef cmbStatus As ComboBox)
        Try
            '配列初期化
            gAryDBkikiCode = Nothing
            gAryDBkikiName = Nothing
            gAryDBkikiDtlCode = Nothing
            gAryDBkikiDtlName = Nothing
            '
            gAryDBkikiCode = New ArrayList
            gAryDBkikiName = New ArrayList
            gAryDBkikiDtlCode = New ArrayList
            gAryDBkikiDtlName = New ArrayList

            Dim udtKiki() As gTypCodeName = Nothing

            'KiKiを全件取得
            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListDeviceStatus)

            'Kikiをもとに詳細取得
            For ii As Integer = 1 To cmbStatus.Items.Count  ''全ての機器グループ分 Loop
                Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                            ii.ToString("00"))

                For j As Integer = 0 To UBound(udtKiki)
                    gAryDBkikiCode.Add(cmbStatus.Items.Item(ii - 1).row.itemarray(0))
                    gAryDBkikiName.Add(cmbStatus.Items.Item(ii - 1).row.itemarray(1))
                    '
                    gAryDBkikiDtlCode.Add(udtKiki(j).shtCode.ToString)
                    gAryDBkikiDtlName.Add(udtKiki(j).strName.ToString)
                Next j
            Next ii

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub



    '--------------------------------------------------------------------
    ' 機能      : チャンネル検索
    ' 返り値    : True:アラーム設定あり存在、False:アラーム設定なし
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネルグループ構造体からチャンネルを検索して
    ' 　　　　  : アラーム設定が存在するか返す
    '--------------------------------------------------------------------
    Public Function gChkAlarmUse(ByVal intChNo As Integer) As Integer

        Try

            If intChNo = 0 Then Return False

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno = intChNo Then

                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeAnalog

                                '================
                                ''アナログ
                                '================
                                If .AnalogHiHiUse = 1 Or .AnalogHiUse = 1 Or _
                                   .AnalogLoLoUse = 1 Or .AnalogLoUse = 1 Or _
                                   .AnalogSensorFailUse = 1 Then
                                    Return True
                                End If

                            Case gCstCodeChTypeDigital

                                If .udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus Then

                                    '================
                                    ''デジタル
                                    '================
                                    If .DigitalUse = 1 Then Return True

                                Else

                                    '================
                                    ''システム
                                    '================
                                    If .SystemUse = 1 Then Return True

                                End If

                            Case gCstCodeChTypeMotor

                                '================
                                ''モーター
                                '================
                                If .MotorUse = 1 Then Return True

                            Case gCstCodeChTypeValve

                                '================
                                ''バルブ
                                '================

                                Select Case .udtChCommon.shtData

                                    Case gCstCodeChDataTypeValveDI_DO

                                        If .ValveDiDoAlarmUse = 1 Then
                                            Return True
                                        End If

                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                        If .ValveAiDoHiHiUse = 1 Or .ValveAiDoHiUse = 1 Or _
                                           .ValveAiDoLoLoUse = 1 Or .ValveAiDoLoUse = 1 Or _
                                           .ValveAiDoSensorFailUse = 1 Then
                                            Return True
                                        End If

                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

                                        If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Or _
                                           .ValveAiAoLoLoUse = 1 Or .ValveAiAoLoUse = 1 Or _
                                           .ValveAiAoSensorFailUse = 1 Then
                                            Return True
                                        End If

                                End Select

                            Case gCstCodeChTypeComposite

                                '================
                                ''コンポジット
                                '================
                                For j As Integer = 0 To UBound(gudt.SetChComposite.udtComposite(.CompositeTableIndex - 1).udtCompInf)

                                    With gudt.SetChComposite.udtComposite(.CompositeTableIndex - 1).udtCompInf(j)

                                        If gBitCheck(.bytAlarmUse, 1) Then
                                            Return True
                                        End If

                                    End With

                                Next

                            Case gCstCodeChTypePulse

                                '================
                                ''パルス積算
                                '================
                                If .PulseUse = 1 Then Return True

                        End Select

                    End If

                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネルタイプコード、名称取得"

    '-------------------------------------------------------------------- 
    ' 機能      : チャンネル種別コードの取得
    ' 返り値    : チャンネル種別コード
    ' 引き数    : ARG1 - (I ) チャンネル名称
    ' 機能説明  : チャンネル名称文字列からチャンネル種別コードを取得する
    '--------------------------------------------------------------------
    Public Function gGetCodeChannelType(ByVal strChannelTypeName As String) As Integer

        Try

            Select Case strChannelTypeName
                Case gCstNameChTypeAnalog : Return gCstCodeChTypeAnalog
                Case gCstNameChTypeDigital : Return gCstCodeChTypeDigital
                Case gCstNameChTypeMotor : Return gCstCodeChTypeMotor
                Case gCstNameChTypeValve : Return gCstCodeChTypeValve
                Case gCstNameChTypeComposite : Return gCstCodeChTypeComposite
                Case gCstNameChTypePulse : Return gCstCodeChTypePulse
                    'Case gCstNameChTypeSystem : Return gCstCodeChTypeSystem
                Case Else : Return 0
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : チャンネル名称の取得
    ' 返り値    : チャンネル名称文字列
    ' 引き数    : ARG1 - (I ) チャンネル種別コード
    ' 機能説明  : チャンネル種別コードからチャンネル名称を取得する
    '--------------------------------------------------------------------
    Public Function gGetNameChannelType(ByVal intChannelTypeCode As Integer) As String

        Try

            Select Case intChannelTypeCode
                Case gCstCodeChTypeAnalog : Return gCstNameChTypeAnalog
                Case gCstCodeChTypeDigital : Return gCstNameChTypeDigital
                Case gCstCodeChTypeMotor : Return gCstNameChTypeMotor
                Case gCstCodeChTypeValve : Return gCstNameChTypeValve
                Case gCstCodeChTypeComposite : Return gCstNameChTypeComposite
                Case gCstCodeChTypePulse : Return gCstNameChTypePulse
                    'Case gCstCodeChTypeSystem : Return gCstNameChTypeSystem
                Case Else : Return ""
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "フリーグラフタイプコード、名称取得"

    '-------------------------------------------------------------------- 
    ' 機能      : フリーグラフのグラフタイプコードを取得する
    ' 返り値    : グラフタイプコード
    ' 引き数    : ARG1 - (I ) グラフタイプ名称
    ' 機能説明  : グラフタイプ名称からグラフタイプコードを取得する
    '--------------------------------------------------------------------
    Public Function gGetCodeFreeGraphType(ByVal strFreeGraphTypeName As String) As Integer

        Try

            Select Case strFreeGraphTypeName
                Case gCstNameOpsFreeGrapTypeNone : Return gCstCodeOpsFreeGrapTypeNone
                Case gCstNameOpsFreeGrapTypeCounter : Return gCstCodeOpsFreeGrapTypeCounter
                Case gCstNameOpsFreeGrapTypeBar : Return gCstCodeOpsFreeGrapTypeBar
                Case gCstNameOpsFreeGrapTypeAnalog : Return gCstCodeOpsFreeGrapTypeAnalog
                Case gCstNameOpsFreeGrapTypeIndicator : Return gCstCodeOpsFreeGrapTypeIndicator
                Case Else : Return gCstCodeOpsFreeGrapTypeNone
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : フリーグラフのグラフタイプ名称を取得する
    ' 返り値    : グラフタイプ名称
    ' 引き数    : ARG1 - (I ) グラフタイプコード
    ' 機能説明  : グラフタイプコードからグラフタイプ名称を取得する
    '--------------------------------------------------------------------
    Public Function gGetNameFreeGraphType(ByVal intFreeGraphTypeCode As Integer) As String

        Try

            Select Case intFreeGraphTypeCode
                Case gCstCodeOpsFreeGrapTypeNone : Return gCstNameOpsFreeGrapTypeNone
                Case gCstCodeOpsFreeGrapTypeCounter : Return gCstNameOpsFreeGrapTypeCounter
                Case gCstCodeOpsFreeGrapTypeBar : Return gCstNameOpsFreeGrapTypeBar
                Case gCstCodeOpsFreeGrapTypeAnalog : Return gCstNameOpsFreeGrapTypeAnalog
                Case gCstCodeOpsFreeGrapTypeIndicator : Return gCstNameOpsFreeGrapTypeIndicator
                Case Else : Return ""
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "共通入力チェック"

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェック数値テキスト（テキストボックス用）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) 入力テキスト
    ' 　　　    : ARG2 - (I ) 最小値
    ' 　　　    : ARG3 - (I ) 最大値
    ' 　　　    : ARG4 - (I ) 項目名称
    ' 　　　    : ARG5 - (I ) 空文字許可
    ' 　　　    : ARG6 - (I ) システム使用不可文字置換フラグ
    ' 機能説明  : テキストボックスに入力されている数値の各種チェック処理を行う
    '--------------------------------------------------------------------
    Public Function gChkInputNum(ByVal txtInput As TextBox, _
                                 ByVal intMin As Integer, _
                                 ByVal intMax As Integer, _
                                 ByVal strName As String, _
                                 ByVal blnNullStringOK As Boolean, _
                                 ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try

            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then txtInput.Text = gConvSystemNotInput(txtInput.Text)

            ''入力が空文字の場合
            If Trim(txtInput.Text) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Call txtInput.Focus()
                    Return False
                End If

            End If

            ''数値入力チェック
            If Not IsNumeric(Trim(txtInput.Text)) Then
                Call MessageBox.Show("Please input the numerical value [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtInput.Focus()
                Return False
            End If

            ''数値が範囲内か
            If gChkTextNumSpan(intMin, intMax, Trim(txtInput.Text), True, "[" & strName & "]") Then
                Call txtInput.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    '小数あり版
    Public Function gChkInputNumDbl(ByVal txtInput As TextBox, _
                                 ByVal dblMin As Double, _
                                 ByVal dblMax As Double, _
                                 ByVal strName As String, _
                                 ByVal blnNullStringOK As Boolean, _
                                 ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try

            'システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then txtInput.Text = gConvSystemNotInput(txtInput.Text)

            '入力が空文字の場合
            If Trim(txtInput.Text) = "" Then

                '空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Call txtInput.Focus()
                    Return False
                End If

            End If

            '数値入力チェック
            If Not IsNumeric(Trim(txtInput.Text)) Then
                Call MessageBox.Show("Please input the numerical value [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtInput.Focus()
                Return False
            End If

            '数値が範囲内か
            If gChkTextNumSpan(dblMin, dblMax, Trim(txtInput.Text), True, "[" & strName & "]") Then
                Call txtInput.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェック数値テキスト（グリッドセル用）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) 入力グリッドセル
    ' 　　　    : ARG2 - (I ) 最小値
    ' 　　　    : ARG3 - (I ) 最大値
    ' 　　　    : ARG4 - (I ) 列名
    ' 　　　    : ARG5 - (I ) 行番号
    ' 　　　    : ARG6 - (I ) 空文字許可
    ' 　　　    : ARG7 - (I ) システム使用不可文字置換フラグ
    ' 機能説明  : グリッドセルに入力されている数値の各種チェック処理を行う
    '--------------------------------------------------------------------
    Public Function gChkInputNum(ByVal objCell As DataGridViewCell, _
                                 ByVal intMin As Integer, _
                                 ByVal intMax As Integer, _
                                 ByVal strColName As String, _
                                 ByVal intRowNo As Integer, _
                                 ByVal blnNullStringOK As Boolean, _
                                 ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try

            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then objCell.Value = gConvSystemNotInput(objCell.Value)

            ''入力が空文字の場合
            If Trim(objCell.Value) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input the item." & vbNewLine & vbNewLine & _
                                         "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If

            End If

            ''数値入力チェック
            If Not IsNumeric(Trim(objCell.Value)) Then
                Call MessageBox.Show("Please input the numerical value" & vbNewLine & vbNewLine & _
                                     "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                     "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            ''数値が範囲内か
            If gChkTextNumSpan(intMin, intMax, Trim(objCell.Value), True, "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Public Function gChkInputNum(ByVal objCell As DataGridViewCell, _
                                 ByVal dblMin As Double, _
                                 ByVal dblMax As Double, _
                                 ByVal strColName As String, _
                                 ByVal intRowNo As Integer, _
                                 ByVal blnNullStringOK As Boolean, _
                                 ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try

            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then objCell.Value = gConvSystemNotInput(objCell.Value)

            ''入力が空文字の場合
            If Trim(objCell.Value) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input the item." & vbNewLine & vbNewLine & _
                                         "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If

            End If

            ''数値入力チェック
            If Not IsNumeric(Trim(objCell.Value)) Then
                Call MessageBox.Show("Please input the numerical value" & vbNewLine & vbNewLine & _
                                     "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                     "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            ''数値が範囲内か
            If gChkTextNumSpan(dblMin, dblMax, Trim(objCell.Value), True, "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    ''データ転送画面用 2010/11/26追加
    Public Function gChkInputNum(ByVal lngInputNum As Long, _
                                 ByVal dblMin As Double, _
                                 ByVal dblMax As Double, _
                                 ByVal strColName As String, _
                                 ByVal intRowNo As Integer, _
                                 ByVal blnNullStringOK As Boolean, _
                                 ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try

            ''システム共通入力不可文字置き換えありの場合
            'If blnSystemNotInputReplace Then objCell.Value = gConvSystemNotInput(objCell.Value)

            ''入力が空文字の場合
            If Trim(lngInputNum) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input the item." & vbNewLine & vbNewLine & _
                                         "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If

            End If

            ''数値入力チェック
            'If Not IsNumeric(Trim(objCell.Value)) Then
            '    Call MessageBox.Show("Please input the numerical value" & vbNewLine & vbNewLine & _
            '                         "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
            '                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            ''数値が範囲内か
            If gChkTextNumSpan(dblMin, dblMax, lngInputNum, True, "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェック通常テキスト（テキストボックス用）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) 入力テキスト
    ' 　　　    : ARG2 - (I ) 列名
    ' 　　　    : ARG3 - (I ) 空文字許可
    ' 　　　    : ARG4 - (I ) システム使用不可文字置換フラグ
    ' 機能説明  : テキストボックスに入力されている文字の各種チェック処理を行う
    '--------------------------------------------------------------------
    Public Function gChkInputText(ByVal txtInput As TextBox, _
                                  ByVal strName As String, _
                                  ByVal blnNullStringOK As Boolean, _
                                  ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try


            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then txtInput.Text = gConvSystemNotInput(txtInput.Text)

            ''入力が空文字の場合
            If Trim(txtInput.Text) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Call txtInput.Focus()
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェック通常テキスト（グリッドセル用）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) 入力グリッドセル
    ' 　　　    : ARG2 - (I ) 列名
    ' 　　　    : ARG3 - (I ) 行番号
    ' 　　　    : ARG4 - (I ) 空文字許可
    ' 　　　    : ARG5 - (I ) システム使用不可文字置換フラグ
    ' 機能説明  : グリッドセルに入力されている文字の各種チェック処理を行う
    '--------------------------------------------------------------------
    Public Function gChkInputText(ByVal objCell As DataGridViewCell, _
                                  ByVal strColName As String, _
                                  ByVal intRowNo As Integer, _
                                  ByVal blnNullStringOK As Boolean, _
                                  ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try


            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then objCell.Value = gConvSystemNotInput(objCell.Value)

            ''入力が空文字の場合
            If Trim(objCell.Value) = "" Then

                ''空文字可の場合
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input the item." & vbNewLine & vbNewLine & _
                                         "[ Col ] " & strColName & " " & vbNewLine & "[ Row ] " & intRowNo, _
                                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェックFUアドレス
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) FU番号テキスト
    ' 　　　    : ARG2 - (I ) スロットテキスト
    ' 　　　    : ARG3 - (I ) 端子台番号テキスト
    ' 　　　    : ARG4 - (I ) 端子数最大値
    ' 　　　    : ARG5 - (I ) 空文字許可
    ' 　　　    : ARG6 - (I ) システム使用不可文字置換フラグ
    ' 機能説明  : FUアドレスの入力チェックを行う
    '--------------------------------------------------------------------
    Public Function gChkInputFuAddress(ByVal txtFuNo As TextBox, _
                                       ByVal txtSlot As TextBox, _
                                       ByVal txtPin As TextBox, _
                                       ByVal intPinMax As Integer, _
                                       ByVal blnNullStringOK As Boolean, _
                                       ByVal blnSystemNotInputReplace As Boolean) As Boolean

        Try


            ''システム共通入力不可文字置き換えありの場合
            If blnSystemNotInputReplace Then
                txtFuNo.Text = gConvSystemNotInput(txtFuNo.Text)
                txtSlot.Text = gConvSystemNotInput(txtSlot.Text)
                txtPin.Text = gConvSystemNotInput(txtPin.Text)
            End If

            ''全て未入力時
            '' Ver1.9.3 2016.01.18 ｽﾍﾟｰｽ削除を追加
            If Trim(txtFuNo.Text) = "" And Trim(txtSlot.Text) = "" And Trim(txtPin.Text) = "" Then

                ''空文字許可の場合は入力可能
                If blnNullStringOK Then
                    Return True
                Else
                    Call MessageBox.Show("Please input [FU Address]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Call txtFuNo.Focus()
                    Return False
                End If

            End If

            ''FU番号が入力されていない場合
            If Trim(txtFuNo.Text) = "" Then
                Call MessageBox.Show("Please input [FU No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFuNo.Focus()
                Return False
            End If

            ''ポート番号が入力されていない場合
            If Trim(txtSlot.Text) = "" Then
                Call MessageBox.Show("Please input [Slot No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtSlot.Focus()
                Return False
            End If

            ''端子台番号が入力されていない場合
            If Trim(txtPin.Text) = "" Then
                Call MessageBox.Show("Please input [Pin No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtPin.Focus()
                Return False
            End If

            ''FU番号変換（数値→アルファベット）
            txtFuNo.Text = gConvFuNoToFuName(txtFuNo.Text)

            ''FU番号入力チェック(T.Ueki 入力監視変更)
            If Val(txtFuNo.Text) <= 20 Then
                'If (txtFuNo.Text.Length = 3 And txtFuNo.Text = "FCU") Or _
                '   (txtFuNo.Text.Length = 1 And txtFuNo.Text <= "T") Then
                ''OK
            Else
                Call MessageBox.Show("There are injustice data. [FU No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFuNo.Focus()
                Return False
            End If

            ''ポート番号が数値ではない場合
            If Not IsNumeric(Trim(txtSlot.Text)) Then
                Call MessageBox.Show("Please input the numerical value [Slot No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtSlot.Focus()
                Return False
            End If

            ''端子台番号が数値ではない場合
            If Not IsNumeric(txtPin.Text) Then
                Call MessageBox.Show("Please input the numerical value [Pin No]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtPin.Focus()
                Return False
            End If

            ''数値が範囲内か
            '' ver1.4.3 2012.03.21 9ポートまで指定可能とする(外部機器通信設定)
            If gChkTextNumSpan(1, 9, Trim(txtSlot.Text), True, "[Slot No]") Then Return False
            If gChkTextNumSpan(1, intPinMax, Trim(txtPin.Text), True, "[Pin No]") Then Return False


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "FU Address関連"

    '-------------------------------------------------------------------- 
    ' 機能      : FU/FCU名称変換
    ' 返り値    : FU/FCU名称
    ' 引き数    : ARG1 - (I )FU/FCU番号  
    ' 機能説明  : FU/FCU番号をFU/FCU名称に変換する
    '--------------------------------------------------------------------
    Public Function gConvFuNoToFuName(ByVal strText As String) As String

        Try

            'T.Ueki
            Select Case Trim(strText)
                Case "0" : Return "0"
                Case "1" : Return "1"
                Case "2" : Return "2"
                Case "3" : Return "3"
                Case "4" : Return "4"
                Case "5" : Return "5"
                Case "6" : Return "6"
                Case "7" : Return "7"
                Case "8" : Return "8"
                Case "9" : Return "9"
                Case "10" : Return "10"
                Case "11" : Return "11"
                Case "12" : Return "12"
                Case "13" : Return "13"
                Case "14" : Return "14"
                Case "15" : Return "15"
                Case "16" : Return "16"
                Case "17" : Return "17"
                Case "18" : Return "18"
                Case "19" : Return "19"
                Case "20" : Return "20"
                Case Else : Return strText
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return strText
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通FuNoキーチェック
    ' 返り値    : Ture:入力不可、False:入力可
    ' 引き数    : ARG1 - (I ) テキストボックスオブジェクト
    ' 引き数    : ARG2 - (IO) キーコード
    ' 機能説明  : FuNoのキー入力チェックを行う
    '--------------------------------------------------------------------
    Public Function gChkInputKeyFuNo(ByVal objText As Object, _
                                     ByRef chrKeyChar As Char) As Boolean

        Try

            Dim strCheckList As String = ""

            strCheckList &= "0,1,2,3,4,5,6,7,8,9,"
            'strCheckList &= "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,"
            'strCheckList &= "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u"

            If gCheckTextInput(3, objText, chrKeyChar, False, False, False, False, strCheckList) Then

                Return True

            Else

                ' ''アルファベットの場合は大文字変換
                'If chrKeyChar >= "A"c And chrKeyChar <= "U"c Then
                'ElseIf chrKeyChar >= "a"c And chrKeyChar <= "u"c Then
                '    Select Case chrKeyChar
                '        Case "a"c : chrKeyChar = "A"c
                '        Case "b"c : chrKeyChar = "B"c
                '        Case "c"c : chrKeyChar = "C"c
                '        Case "d"c : chrKeyChar = "D"c
                '        Case "e"c : chrKeyChar = "E"c
                '        Case "f"c : chrKeyChar = "F"c
                '        Case "g"c : chrKeyChar = "G"c
                '        Case "h"c : chrKeyChar = "H"c
                '        Case "i"c : chrKeyChar = "I"c
                '        Case "j"c : chrKeyChar = "J"c
                '        Case "k"c : chrKeyChar = "K"c
                '        Case "l"c : chrKeyChar = "L"c
                '        Case "m"c : chrKeyChar = "M"c
                '        Case "n"c : chrKeyChar = "N"c
                '        Case "o"c : chrKeyChar = "O"c
                '        Case "p"c : chrKeyChar = "P"c
                '        Case "q"c : chrKeyChar = "Q"c
                '        Case "r"c : chrKeyChar = "R"c
                '        Case "s"c : chrKeyChar = "S"c
                '        Case "t"c : chrKeyChar = "T"c
                '        Case "u"c : chrKeyChar = "U"c
                '    End Select

                'End If

                Return False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス番号からFUアドレス名称を返す
    ' 返り値    : FUアドレス名称  FCU or A～T
    ' 引き数    : ARG1 - (I ) FUアドレス番号　0～20
    ' 機能説明  : FUアドレス番号からFUアドレス名称を返す
    '--------------------------------------------------------------------
    Public Function gGetFuName(ByVal intFuNo As Integer) As String

        Try

            gGetFuName = ""

            If intFuNo >= 0 And intFuNo <= 20 Then

                'T.Ueki ローカル名変更
                Select Case intFuNo
                    Case 0 : gGetFuName = "FCU"
                    Case 1 : gGetFuName = "FU1"
                    Case 2 : gGetFuName = "FU2"
                    Case 3 : gGetFuName = "FU3"
                    Case 4 : gGetFuName = "FU4"
                    Case 5 : gGetFuName = "FU5"
                    Case 6 : gGetFuName = "FU6"
                    Case 7 : gGetFuName = "FU7"
                    Case 8 : gGetFuName = "FU8"
                    Case 9 : gGetFuName = "FU9"
                    Case 10 : gGetFuName = "FU10"
                    Case 11 : gGetFuName = "FU11"
                    Case 12 : gGetFuName = "FU12"
                    Case 13 : gGetFuName = "FU13"
                    Case 14 : gGetFuName = "FU14"
                    Case 15 : gGetFuName = "FU15"
                    Case 16 : gGetFuName = "FU16"
                    Case 17 : gGetFuName = "FU17"
                    Case 18 : gGetFuName = "FU18"
                    Case 19 : gGetFuName = "FU19"
                    Case 20 : gGetFuName = "FU20"
                End Select

            Else
                gGetFuName = ""
            End If

            Return gGetFuName

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス番号からFUアドレス名称を返す
    ' 返り値    : FUアドレス名称  FCU or A～T
    ' 引き数    : ARG1 - (I ) FUアドレス番号　0～20
    ' 機能説明  : FUアドレス番号からFUアドレス名称を返す
    '--------------------------------------------------------------------
    Public Function gGetFuName2(ByVal intFuNo As Integer) As String

        Try

            gGetFuName2 = ""

            If intFuNo >= 0 And intFuNo <= 20 Then

                'T.Ueki ローカル名変更
                Select Case intFuNo
                    Case 0 : gGetFuName2 = "0"
                    Case 1 : gGetFuName2 = "1"
                    Case 2 : gGetFuName2 = "2"
                    Case 3 : gGetFuName2 = "3"
                    Case 4 : gGetFuName2 = "4"
                    Case 5 : gGetFuName2 = "5"
                    Case 6 : gGetFuName2 = "6"
                    Case 7 : gGetFuName2 = "7"
                    Case 8 : gGetFuName2 = "8"
                    Case 9 : gGetFuName2 = "9"
                    Case 10 : gGetFuName2 = "10"
                    Case 11 : gGetFuName2 = "11"
                    Case 12 : gGetFuName2 = "12"
                    Case 13 : gGetFuName2 = "13"
                    Case 14 : gGetFuName2 = "14"
                    Case 15 : gGetFuName2 = "15"
                    Case 16 : gGetFuName2 = "16"
                    Case 17 : gGetFuName2 = "17"
                    Case 18 : gGetFuName2 = "18"
                    Case 19 : gGetFuName2 = "19"
                    Case 20 : gGetFuName2 = "20"
                End Select

            Else
                gGetFuName2 = ""
            End If

            Return gGetFuName2

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス名称からFUアドレス番号を返す
    ' 返り値    : FUアドレス番号　0～20
    ' 引き数    : ARG1 - (I ) FUアドレス名称  FCU or A～T
    '           : ARG2 - (I ) Byteフラグ
    ' 機能説明  : FUアドレス名称からFUアドレス番号を返す
    '--------------------------------------------------------------------
    Public Function gGetFuNo(ByVal strFuNo As String, _
                    Optional ByVal blnByte As Boolean = False) As Integer

        Try

            'If strFuNo.Length = 3 Then

            '    If strFuNo = "0" Then
            '        gGetFuNo = 0
            '    Else                            '' 警告回避 Else処理追加 2012.12.15 K.Tanigwa
            '        gGetFuNo = IIf(blnByte, gCstCodeChNotSetFuNoByte, gCstCodeChNotSetFuNo)
            '    End If

            'ElseIf strFuNo.Length = 1 Then

            'T.Ueki
            If strFuNo.Length <= 2 Then

                Select Case strFuNo
                    Case "0" : gGetFuNo = 0
                    Case "1" : gGetFuNo = 1
                    Case "2" : gGetFuNo = 2
                    Case "3" : gGetFuNo = 3
                    Case "4" : gGetFuNo = 4
                    Case "5" : gGetFuNo = 5
                    Case "6" : gGetFuNo = 6
                    Case "7" : gGetFuNo = 7
                    Case "8" : gGetFuNo = 8
                    Case "9" : gGetFuNo = 9
                    Case "10" : gGetFuNo = 10
                    Case "11" : gGetFuNo = 11
                    Case "12" : gGetFuNo = 12
                    Case "13" : gGetFuNo = 13
                    Case "14" : gGetFuNo = 14
                    Case "15" : gGetFuNo = 15
                    Case "16" : gGetFuNo = 16
                    Case "17" : gGetFuNo = 17
                    Case "18" : gGetFuNo = 18
                    Case "19" : gGetFuNo = 19
                    Case "20" : gGetFuNo = 20

                        ' 2013.07.19 22K→50コンバート用に追加 K.Fujimoto
                    Case "A" : gGetFuNo = 1
                    Case "B" : gGetFuNo = 2
                    Case "C" : gGetFuNo = 3
                    Case "D" : gGetFuNo = 4
                    Case "E" : gGetFuNo = 5
                    Case "F" : gGetFuNo = 6
                    Case "G" : gGetFuNo = 7
                    Case "H" : gGetFuNo = 8
                    Case "I" : gGetFuNo = 9
                    Case "J" : gGetFuNo = 10
                    Case "K" : gGetFuNo = 11
                    Case "L" : gGetFuNo = 12
                    Case "M" : gGetFuNo = 13
                    Case "N" : gGetFuNo = 14
                    Case "O" : gGetFuNo = 15
                    Case "P" : gGetFuNo = 16
                    Case "Q" : gGetFuNo = 17
                    Case "R" : gGetFuNo = 18
                    Case "S" : gGetFuNo = 19
                    Case "T" : gGetFuNo = 20
                    Case Else : gGetFuNo = IIf(blnByte, gCstCodeChNotSetFuNoByte, gCstCodeChNotSetFuNo)
                End Select

            Else
                gGetFuNo = IIf(blnByte, gCstCodeChNotSetFuNoByte, gCstCodeChNotSetFuNo)
            End If

            Return gGetFuNo

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレスを連結する
    ' 返り値    : FU Address
    ' 引き数    : ARG1 - (I ) FU 番号
    '           : ARG2 - (I ) FU ポート番号
    '           : ARG3 - (I ) FU 計測点番号
    ' 機能説明  : FU番号が0から20の数値型のFUアドレスを、標記型のFUアドレスに変換する
    '--------------------------------------------------------------------
    Public Function gConvFuAddress(ByVal hFuNo As Integer, _
                                   ByVal hPortNo As Integer, _
                                   ByVal hPin As Integer) As String

        Try

            Dim strFuAddress As String = ""
            Dim hPinLen As Integer
            Dim i As Integer
            Dim NewhPin As String = ""
            Dim MidhPin As String = ""

            '' アドレス設定無し時は表示無し   ver.1.4.5 2012.07.03
            ''If gGet2Byte(hFuNo) = gCstCodeChNotSetFuNo Then
            If gGet2Byte(hFuNo) = gCstCodeChNotSetFuNo Or gGet2Byte(hPortNo) = gCstCodeChNotSetFuPort Or
               gGet2Byte(hPin) = gCstCodeChNotSetFuPin Then

                gConvFuAddress = ""
                Exit Function
            End If

            If gGet2Byte(hFuNo) = gCstCodeChNotSetFuNoByte Then
                gConvFuAddress = ""
                Exit Function
            End If

            'T.Ueki
            Select Case hFuNo
                Case 0 : strFuAddress = "0-"
                Case 1 : strFuAddress = "1-"
                Case 2 : strFuAddress = "2-"
                Case 3 : strFuAddress = "3-"
                Case 4 : strFuAddress = "4-"
                Case 5 : strFuAddress = "5-"
                Case 6 : strFuAddress = "6-"
                Case 7 : strFuAddress = "7-"
                Case 8 : strFuAddress = "8-"
                Case 9 : strFuAddress = "9-"
                Case 10 : strFuAddress = "10-"
                Case 11 : strFuAddress = "11-"
                Case 12 : strFuAddress = "12-"
                Case 13 : strFuAddress = "13-"
                Case 14 : strFuAddress = "14-"
                Case 15 : strFuAddress = "15-"
                Case 16 : strFuAddress = "16-"
                Case 17 : strFuAddress = "17-"
                Case 18 : strFuAddress = "18-"
                Case 19 : strFuAddress = "19-"
                Case 20 : strFuAddress = "20-"
                Case Else : strFuAddress = "*-"
            End Select

            strFuAddress += hPortNo.ToString & "-"

            hPinLen = Len(hPin)

            For i = 1 To hPinLen
                MidhPin = Mid(Str(hPin), i, 1)

                If MidhPin = "" Then
                    Exit For
                End If
                NewhPin = NewhPin + MidhPin

            Next


            If Len(NewhPin) > 2 Then
                strFuAddress += Format(hPin, "00000")
            Else
                strFuAddress += Format(hPin, "00")
            End If

            gConvFuAddress = strFuAddress

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス調査
    '--------------------------------------------------------------------
    Public Function gFuAddress(ByVal AddressFUNo As Integer) As String

        Try

            '' アドレス設定無し時は表示無し
            If gGet2Byte(AddressFUNo) = gCstCodeChNotSetFuNo Then
                gFuAddress = ""
                Exit Function
            End If

            gFuAddress = Trim(Str(AddressFUNo))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス調査
    '--------------------------------------------------------------------
    Public Function gFuAddressPort(ByVal AddressPortNo As Integer) As String

        Try

            '' アドレス設定無し時は表示無し
            If gGet2Byte(AddressPortNo) = gCstCodeChNotSetFuPort Then
                gFuAddressPort = ""
                Exit Function
            End If

            gFuAddressPort = Trim(Str(AddressPortNo))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス調査
    '--------------------------------------------------------------------
    Public Function gFuAddressPin(ByVal AddressPinNo As Integer, ByVal DataType As Integer) As String

        Try

            '' アドレス設定無し時は表示無し
            If gGet2Byte(AddressPinNo) = gCstCodeChNotSetFuPin Then
                gFuAddressPin = ""
                Exit Function
            End If

            '' Ver1.11.1 2016.07.12 緯度・経度追加
            '' Ver1.12.0.1 2017.01.13 運転積算種類追加
            If DataType = gCstCodeChDataTypeDigitalModbusNC Or DataType = gCstCodeChDataTypeDigitalModbusNO Or _
                DataType = gCstCodeChDataTypeAnalogModbus Or DataType = gCstCodeChDataTypePulseExtDev Or _
                DataType = gCstCodeChDataTypePulseRevoExtDev Or DataType = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                DataType = gCstCodeChDataTypePulseRevoExtDevDayHour Or DataType = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                DataType = gCstCodeChDataTypePulseRevoExtDevLapHour Or DataType = gCstCodeChDataTypePulseRevoExtDevLapMin Or _
                DataType = gCstCodeChDataTypeAnalogLatitude Or DataType = gCstCodeChDataTypeAnalogLongitude Then
                gFuAddressPin = Format(AddressPinNo, "00000")
            Else
                gFuAddressPin = Trim(Format(AddressPinNo, "00"))
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス分解
    ' 返り値    : 0
    ' 引き数    : ARG1 - (I ) FUアドレス
    '           : ARG2 - ( O) FU番号
    '           : ARG3 - ( O) FUポート番号
    '           : ARG4 - ( O) FU計測点番号
    ' 機能説明  : FUアドレスをFU番号、FUポート番号、FU計測点番号に分解する
    '--------------------------------------------------------------------
    Public Function gSeparateFuAddress(ByVal hFuAddress As String, _
                                       ByRef hFuNo As Integer, _
                                       ByRef hPortNo As Integer, _
                                       ByRef hPin As Integer) As Integer

        Try

            Dim hPinlen As Integer

            If (hFuAddress IsNot Nothing) AndAlso (hFuAddress.Length >= 3) Then

                If hFuAddress.Length > 5 Then

                    hPinlen = Len(hFuAddress)


                    If hFuAddress.Substring(0, 1) = "0" Then
                        hFuNo = 0
                        hPortNo = hFuAddress.Substring(1, 1)
                        hPin = hFuAddress.Substring(2, hPinlen - 2)
                    Else
                        hFuNo = gCstCodeChNotSetFuNo
                        hPortNo = gCstCodeChNotSetFuPort
                        hPin = gCstCodeChNotSetFuPin
                    End If
                Else

                    If hFuAddress.Length = 5 Then

                        If hFuAddress.Substring(0, 2) = "00" Then
                            hFuNo = 0
                            hPortNo = hFuAddress.Substring(2, 1)
                            hPin = hFuAddress.Substring(3)
                        Else
                            'T.Ueki アドレス整数化のため修正
                            Select Case hFuAddress.Substring(0, 2)
                                Case "10" : hFuNo = 10
                                Case "11" : hFuNo = 11
                                Case "12" : hFuNo = 12
                                Case "13" : hFuNo = 13
                                Case "14" : hFuNo = 14
                                Case "15" : hFuNo = 15
                                Case "16" : hFuNo = 16
                                Case "17" : hFuNo = 17
                                Case "18" : hFuNo = 18
                                Case "19" : hFuNo = 19
                                Case "20" : hFuNo = 20
                                Case Else : hFuNo = gCstCodeChNotSetFuNo
                            End Select

                            hPortNo = hFuAddress.Substring(2, 1)
                            hPin = hFuAddress.Substring(3)

                        End If

                    Else
                        If hFuAddress.Substring(0, 1) = "0" Then
                            hFuNo = 0
                            hPortNo = hFuAddress.Substring(1, 1)
                            hPin = hFuAddress.Substring(2)
                        Else
                            'T.Ueki アドレス整数化のため修正
                            Select Case hFuAddress.Substring(0, 1)
                                Case "1" : hFuNo = 1
                                Case "2" : hFuNo = 2
                                Case "3" : hFuNo = 3
                                Case "4" : hFuNo = 4
                                Case "5" : hFuNo = 5
                                Case "6" : hFuNo = 6
                                Case "7" : hFuNo = 7
                                Case "8" : hFuNo = 8
                                Case "9" : hFuNo = 9
                                Case Else : hFuNo = gCstCodeChNotSetFuNo
                            End Select

                            hPortNo = hFuAddress.Substring(1, 1)
                            hPin = hFuAddress.Substring(2)

                        End If

                    End If

                End If

            Else
                hFuNo = gCstCodeChNotSetFuNo
                hPortNo = gCstCodeChNotSetFuPort
                hPin = gCstCodeChNotSetFuPin

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "アプリケーション実行フォルダ取得"

    '-------------------------------------------------------------------- 
    ' 機能      : アプリケーション実行フォルダ取得
    ' 返り値    : アプリケーション実行フォルダ
    ' 引き数    : なし
    ' 機能説明  : アプリケーション実行フォルダを取得する 
    '--------------------------------------------------------------------
    Public Function gGetAppPath() As String

        Try

            Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "iniファイルフォルダ取得"

    '-------------------------------------------------------------------- 
    ' 機能      : iniファイルフォルダ取得
    ' 返り値    : iniファイルフォルダ
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : iniファイルフォルダを取得する
    '--------------------------------------------------------------------
    Public Function gGetDirNameIniFile() As String

        Try

            Dim strAppPath As String

            ''プログラム実行フォルダ取得
            strAppPath = gGetAppPath()

            ''戻り値作成
            'Ver2.0.7.M (保安庁)
            Dim strIniFolder As String = ""
            If g_bytHOAN = 1 Then
                strIniFolder = gCstIniFileDir & "_HOAN"
            Else
                strIniFolder = gCstIniFileDir
            End If
            'Return System.IO.Path.Combine(strAppPath, gCstIniFileDir)
            Return System.IO.Path.Combine(strAppPath, strIniFolder)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "デフォルトデータ保存フォルダ取得"

    '-------------------------------------------------------------------- 
    ' 機能      : デフォルトデータ保存フォルダ取得 
    ' 返り値    : デフォルトデータ保存フォルダ
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : デフォルトデータ保存フォルダを取得する
    '--------------------------------------------------------------------
    Public Function gGetDirNameDefaultData() As String

        Try

            Dim strAppPath As String

            ''プログラム実行フォルダ取得
            strAppPath = gGetAppPath()

            ''戻り値作成
            Return System.IO.Path.Combine(strAppPath, gCstFolderNameDefaultData)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "バイト変換"

    '--------------------------------------------------------------------
    ' 機能      : Short型(符号なし）を整数に変換する
    ' 返り値    : Integer整数
    '           : ARG1 - (I ) Short型の値
    ' 機能説明  : 構造体でShort型に定義してある値をIntegerに変換する
    '--------------------------------------------------------------------
    Public Function gGet2Byte(ByVal intValue2Byte As Integer) As Integer

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(3) As Byte

            Call gSeparat2Byte(intValue2Byte, bytValue1, bytValue2)

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2

            gGet2Byte = BitConverter.ToInt32(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 2バイトの値を整数に変換する
    ' 返り値    : 整数
    '           : ARG1 - (I ) １バイト目
    '           : ARG2 - (I ) ２バイト目
    ' 機能説明  : 2つのByte型を結合して作った値を整数に変換する
    '--------------------------------------------------------------------
    Public Function gConnect2Byte(ByVal bytValue1 As Byte, _
                                  ByVal bytValue2 As Byte) As Integer

        Try

            Dim bytArray(3) As Byte

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2

            gConnect2Byte = BitConverter.ToInt32(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 2バイトの値を整数(Short)に変換する
    ' 返り値    : 整数
    '           : ARG1 - (I ) １バイト目
    '           : ARG2 - (I ) ２バイト目
    ' 機能説明  : 2つのByte型を結合して作った値を整数(Short)に変換する
    '--------------------------------------------------------------------
    Public Function gConnect2ByteS(ByVal bytValue1 As Byte, _
                                  ByVal bytValue2 As Byte) As Short

        Try

            Dim bytArray(2) As Byte

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2

            gConnect2ByteS = BitConverter.ToInt16(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    Public Function gConnect2ByteSingle(ByVal bytValue1 As Byte, _
                                     ByVal bytValue2 As Byte) As Single
        Try

            Dim bytArray(3) As Byte

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2

            gConnect2ByteSingle = BitConverter.ToSingle(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 数値を2バイトに分割格納する
    ' 返り値    : なし
    '           : ARG1 - (I ) 数値
    '           : ARG2 - ( O) １バイト目
    '           : ARG3 - ( O) ２バイト目
    ' 機能説明  : 整数を2進数にして2BYTEに分解する
    '--------------------------------------------------------------------
    Public Function gSeparat2Byte(ByVal intValue As Integer, _
                                  ByRef bytValue1 As Byte, _
                                  ByRef bytValue2 As Byte) As Integer

        Try

            Dim bytValue As Byte() = BitConverter.GetBytes(intValue)

            bytValue1 = bytValue(0)
            bytValue2 = bytValue(1)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 4バイトの値を数値に変換する
    ' 返り値    : 数値
    '           : ARG1 - (I ) １バイト目
    '           : ARG2 - (I ) ２バイト目
    '           : ARG4 - (I ) ３バイト目
    '           : ARG5 - (I ) ４バイト目
    ' 機能説明  : 2つのByte型を結合して作った値を数値に変換する
    '--------------------------------------------------------------------
    Public Function gConnect4Byte(ByVal bytValue1 As Byte, _
                                  ByVal bytValue2 As Byte, _
                                  ByVal bytValue3 As Byte, _
                                  ByVal bytValue4 As Byte) As Integer

        Try

            Dim bytArray(3) As Byte

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2
            bytArray(2) = bytValue3
            bytArray(3) = bytValue4

            gConnect4Byte = BitConverter.ToInt32(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Public Function gConnect4ByteSingle(ByVal bytValue1 As Byte, _
                                        ByVal bytValue2 As Byte, _
                                        ByVal bytValue3 As Byte, _
                                        ByVal bytValue4 As Byte) As Single

        Try

            Dim bytArray(3) As Byte

            bytArray(0) = bytValue1
            bytArray(1) = bytValue2
            bytArray(2) = bytValue3
            bytArray(3) = bytValue4

            gConnect4ByteSingle = BitConverter.ToSingle(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 数値を4バイトに分割格納する
    ' 返り値    : なし
    '           : ARG1 - (I ) 数値
    '           : ARG2 - ( O) １バイト目
    '           : ARG3 - ( O) ２バイト目
    '           : ARG4 - ( O) ３バイト目
    '           : ARG5 - ( O) ４バイト目
    ' 機能説明  : 数値を4BYTEに分解する
    '--------------------------------------------------------------------
    Public Function gSeparat4Byte(ByVal intValue As Integer, _
                                  ByRef bytValue1 As Byte, _
                                  ByRef bytValue2 As Byte, _
                                  ByRef bytValue3 As Byte, _
                                  ByRef bytValue4 As Byte) As Integer

        Try

            Dim bytValue As Byte() = BitConverter.GetBytes(intValue)

            bytValue1 = bytValue(0)
            bytValue2 = bytValue(1)
            bytValue3 = bytValue(2)
            bytValue4 = bytValue(3)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Public Function gSeparat4Byte(ByVal sinValue As Single, _
                                  ByRef bytValue1 As Byte, _
                                  ByRef bytValue2 As Byte, _
                                  ByRef bytValue3 As Byte, _
                                  ByRef bytValue4 As Byte) As Integer

        Try

            Dim bytValue As Byte() = BitConverter.GetBytes(sinValue)

            bytValue1 = bytValue(0)
            bytValue2 = bytValue(1)
            bytValue3 = bytValue(2)
            bytValue4 = bytValue(3)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Public Function gSeparat4Byte(ByVal lngValue As Long, _
                                  ByRef bytValue1 As Byte, _
                                  ByRef bytValue2 As Byte, _
                                  ByRef bytValue3 As Byte, _
                                  ByRef bytValue4 As Byte) As Integer

        Dim bytValue As Byte() = BitConverter.GetBytes(lngValue)

        bytValue1 = bytValue(0)
        bytValue2 = bytValue(1)
        bytValue3 = bytValue(2)
        bytValue4 = bytValue(3)

    End Function

#End Region

#Region "FU構造体作成"

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体を作成する
    ' 返り値    : なし
    '           : ARG1 - ( O) FU情報構造体
    ' 機能説明  : FU情報の印刷用構造体を作成する
    '--------------------------------------------------------------------
    Public Sub gMakeFuInfoStructure(ByRef udtFuInfo() As gTypFuInfo)

        Try

            Dim i As Integer, j As Integer, k As Integer
            Dim x As Integer
            Dim intFuNo As Integer
            Dim intPortNo As Integer
            Dim intPin As Integer
            Dim intPinNo As Integer
            Dim intLoopNothing As Integer = 0
            Dim aryCheck As New ArrayList
            Dim strChNo As String = ""
            Dim strListIndex As String = ""

            'T.Ueki
            Dim CHViewFlg As Boolean

            '' Ver1.12.0.1 2017.01.13 OUTPUT印字用
            Dim bOutputFlg As Boolean
            Dim bytFUNo As Byte
            Dim bytPortNo As Byte
            Dim bytPinNo As Byte

            ''配列初期化
            ReDim udtFuInfo(gCstCountFuNo - 1)
            For i = 0 To UBound(udtFuInfo)
                ReDim udtFuInfo(i).udtFuPort(gCstCountFuPort - 1)
                For j = 0 To UBound(udtFuInfo(i).udtFuPort)
                    ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(gCstCountFuPin - 1)
                    For k = 0 To UBound(udtFuInfo(i).udtFuPort(j).udtFuPin)
                        ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(k).strWireMark(2)
                        ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(k).strWireMarkClass(2)
                        ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(k).strCoreNoIn(2)
                        ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(k).strCoreNoCom(2)
                        ReDim udtFuInfo(i).udtFuPort(j).udtFuPin(k).strDist(2)
                    Next
                Next
            Next

            ''チャンネル情報データ（表示名設定データ）から端子台リストの情報を移す ==============================

            For i = 0 To UBound(gudt.SetChDisp.udtChDisp)
                Call mSetFuInfoChName(udtFuInfo, i)

                '' Ver1.9.3 2016.01.16  予備基板も印刷可能とするように変更
                For j = 0 To UBound(udtFuInfo(i).udtFuPort)
                    udtFuInfo(i).udtFuPort(j).intPortType = gudt.SetFu.udtFu(i).udtSlotInfo(j).shtType

                    'Ver2.0.3.6
                    'ワイヤーマーク類はCHoutがあってもなくても格納
                    For x = 0 To UBound(udtFuInfo(i).udtFuPort(j).udtFuPin)
                        If gudt.SetFu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_3 Then
                            If x <= 20 Then
                                Call mGetFuInfoSlotInfo(udtFuInfo(i).udtFuPort(j).udtFuPin(x), _
                                   i, _
                                   j + 1, _
                                   x + 1)
                            End If
                        Else
                            Call mGetFuInfoSlotInfo(udtFuInfo(i).udtFuPort(j).udtFuPin(x), _
                                           i, _
                                           j + 1, _
                                           x + 1)
                        End If

                    Next x
                Next



            Next


            ''出力チャンネル構造体からFU構造体へ情報を移す ======================================================

            For i = 0 To UBound(gudt.SetChOutput.udtCHOutPut)

                With gudt.SetChOutput.udtCHOutPut(i)
                    intFuNo = .bytFuno
                    intPortNo = .bytPortno
                    intPin = .bytPin
                End With

                ''各設定値が範囲内のものだけ追加する
                If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                   (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                   (intPin >= 1 And intPin <= gCstCountFuPin) Then

                    ''出力チャンネル構造体からFU構造体に情報を移す
                    Call mSetFuInfoChOutput(udtFuInfo, i, intFuNo, intPortNo, intPin)

                End If

            Next i

            ''チャンネル構造体からFU構造体へ情報を移す ==========================================================

            ''チャンネル番号順に並べ替え 2015.07.10
            gMakeChNoOrderSort(aryCheck)

            'For i = 0 To UBound(gudt.SetChInfo.udtChannel)
            For x = 0 To aryCheck.Count - 1     '' CH順ソートでループ   2015.07.10

                ''ソート結果からリストのインデックス取得   2015.07.10
                gGetChNoOrder(aryCheck, x, strChNo, strListIndex)

                i = Val(strListIndex)   '' リストインデックスをセット    2015.07.10
                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = 1401 Then
                        Dim debuA As Integer = 0
                    End If

                    CHViewFlg = False
                    bOutputFlg = False      '' Ver1.12.0.1 2017.01.13 追加

                    Select Case .shtChType

                        Case gCstCodeChTypeAnalog       'アナログ

                            '' Ver1.11.1 2016.07.12 緯度・経度追加
                            If .shtData = gCstCodeChDataTypeAnalogModbus Or .shtData = gCstCodeChDataTypeAnalogExtDev _
                                   Or .shtData = gCstCodeChDataTypeAnalogExhAve Or .shtData = gCstCodeChDataTypeAnalogExhRepose Or .shtData = gCstCodeChDataTypeAnalogJacom Or _
                                   .shtData = gCstCodeChDataTypeAnalogLatitude Or .shtData = gCstCodeChDataTypeAnalogLongitude Or .shtData = gCstCodeChDataTypeAnalogJacom55 Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeDigital      'デジタル

                            '' Ver1.12.0.1 2017.01.13  JACOM CHの場合はOutputﾃｰﾌﾞﾙ設定有無を確認
                            If .shtData = gCstCodeChDataTypeDigitalJacomNC Or .shtData = gCstCodeChDataTypeDigitalJacomNO Or _
                               .shtData = gCstCodeChDataTypeDigitalJacom55NC Or .shtData = gCstCodeChDataTypeDigitalJacom55NO Then
                                bOutputFlg = ChkOutputCH(._shtChno, bytFUNo, bytPortNo, bytPinNo)
                                If bOutputFlg = False Then
                                    CHViewFlg = True
                                End If
                            Else
                                ''If .shtData = gCstCodeChDataTypeDigitalDeviceStatus Or .shtData = gCstCodeChDataTypeDigitalJacomNC _
                                ''      Or .shtData = gCstCodeChDataTypeDigitalJacomNO Or .shtData = gCstCodeChDataTypeDigitalModbusNC Or .shtData = gCstCodeChDataTypeDigitalModbusNO Then
                                If .shtData = gCstCodeChDataTypeDigitalDeviceStatus Or _
                                      .shtData = gCstCodeChDataTypeDigitalModbusNC Or .shtData = gCstCodeChDataTypeDigitalModbusNO Then
                                    CHViewFlg = True
                                End If
                            End If

                        Case gCstCodeChTypeMotor        'モーター

                            'Ver2.0.7.S JACOMより大きい＝通信CHは非表示
                            'If .shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                            If .shtData >= gCstCodeChDataTypeMotorDeviceJacom Then
                                CHViewFlg = True
                            End If '' Ver1.12.0.1 2017.01.13  JACOM CHの場合はOutputﾃｰﾌﾞﾙ設定有無を確認
                            If (.shtData = gCstCodeChDataTypeMotorDeviceJacom) Or _
                               (.shtData = gCstCodeChDataTypeMotorDeviceJacom55) Then
                                bOutputFlg = ChkOutputCH(._shtChno, bytFUNo, bytPortNo, bytPinNo)
                                If bOutputFlg = False Then
                                    CHViewFlg = True
                                Else
                                    'Ver2.0.2.7 OUTPUTテーブルがあるときは印刷
                                    CHViewFlg = False
                                End If
                            End If

                        Case gCstCodeChTypeValve        'バルブ

                            If .shtData = gCstCodeChDataTypeValveJacom Or .shtData = gCstCodeChDataTypeValveJacom55 Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeComposite    'コンポジット

                            '処理無し

                        Case gCstCodeChTypePulse        'パルス

                            '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                            '' Ver1.12.0.1 2017.01.13 運転積算種類追加
                            If .shtData = gCstCodeChDataTypePulseExtDev Or _
                                .shtData = gCstCodeChDataTypePulseRevoExtDev Or .shtData = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                                .shtData = gCstCodeChDataTypePulseRevoExtDevDayHour Or .shtData = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                                .shtData = gCstCodeChDataTypePulseRevoExtDevLapHour Or .shtData = gCstCodeChDataTypePulseRevoExtDevLapMin Then
                                CHViewFlg = True
                            End If

                        Case Else

                            '処理無し
                    End Select

                    If CHViewFlg = False Then

                        '' Ver1.12.0.1 2017.01.13  Outputﾃｰﾌﾞﾙ設定時 (JACOM出力用)
                        If bOutputFlg = True Then
                            If udtFuInfo(bytFUNo).udtFuPort(bytPortNo - 1).udtFuPin(bytPinNo - 1).strStatus = "" Then
                                Call mGetFuInfoStatus(udtFuInfo(bytFUNo).udtFuPort(bytPortNo - 1).udtFuPin(bytPinNo - 1), _
                                              i, .shtChType, 1, True, False)
                            End If
                        Else
                            Select Case .shtChType

                                Case gCstCodeChTypeAnalog, _
                                     gCstCodeChTypeDigital, _
                                     gCstCodeChTypePulse, gCstCodeChTypePID 'Ver2.0.7.H PID対応

                                    ''各設定値が範囲内のものだけ追加する
                                    If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                       (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                       (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                        ''INPUT：チャンネル構造体からFU構造体に情報を移す
                                        Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin)

                                    End If

                                Case gCstCodeChTypeMotor

                                    ''各設定値が範囲内のものだけ追加する
                                    If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                       (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                       (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                        ''INPUT
                                        'Ver2.0.0.2 モーター種別増加 R Device ADD
                                        If (.shtData = gCstCodeChDataTypeMotorDevice Or .shtData = gCstCodeChDataTypeMotorRDevice) And _
                                           (.shtStatus = gCstCodeChManualInputStatus) Then

                                            ''機器運転かつ手入力の時
                                            Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin)

                                        Else

                                            If .shtPinNo >= 0 Then
                                                If .shtPin - 1 + .shtPinNo <= gCstCountFuPin Then
                                                    For j = 0 To .shtPinNo - 1
                                                        ''チャンネル構造体からFU構造体に情報を移す
                                                        Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin + j, j)
                                                    Next
                                                End If
                                            End If

                                        End If

                                        ''OUTPUT
                                        With gudt.SetChInfo.udtChannel(i)
                                            intFuNo = .MotorFuNo        ''Fu No
                                            intPortNo = .MotorPortNo    ''Port No
                                            intPin = .MotorPin          ''Pin
                                            intPinNo = .MotorPinNo      ''Pin No
                                        End With

                                        If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                           (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                           (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                            If intPinNo >= 0 Then
                                                If intPin - 1 + intPinNo <= gCstCountFuPin Then
                                                    For j = 0 To intPinNo - 1
                                                        ''チャンネル構造体からFU構造体に情報を移す
                                                        Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin + j, j, False)
                                                    Next
                                                End If
                                            End If

                                        End If

                                    End If

                                Case gCstCodeChTypeValve

                                    ''各設定値が範囲内のものだけ追加する。
                                    Select Case .shtData

                                        Case gCstCodeChDataTypeValveDI_DO

                                            ''INPUT
                                            If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                               (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                               (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                                If .shtPinNo >= 0 Then
                                                    If .shtPin - 1 + .shtPinNo <= gCstCountFuPin Then
                                                        For j = 0 To .shtPinNo - 1
                                                            ''チャンネル構造体からFU構造体に情報を移す
                                                            Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin + j, j)
                                                        Next
                                                    End If
                                                End If

                                            End If

                                            ''OUTPUT
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveDiDoFuNo        ''Fu No
                                                intPortNo = .ValveDiDoPortNo    ''Port No
                                                intPin = .ValveDiDoPin          ''Pin
                                                intPinNo = .ValveDiDoPinNo      ''Pin No
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                If intPinNo >= 0 Then
                                                    If intPin - 1 + intPinNo <= gCstCountFuPin Then
                                                        For j = 0 To intPinNo - 1
                                                            ''チャンネル構造体からFU構造体に情報を移す
                                                            Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin + j, j, False)
                                                        Next
                                                    End If
                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveAI_DO1, _
                                             gCstCodeChDataTypeValveAI_DO2

                                            ''INPUT
                                            If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                               (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                               (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                                ''チャンネル構造体からFU構造体に情報を移す
                                                Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin)

                                            End If

                                            ''OUTPUT    
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveAiDoFuNo        ''Fu No
                                                intPortNo = .ValveAiDoPortNo    ''Port No
                                                intPin = .ValveAiDoPin          ''Pin
                                                intPinNo = .ValveAiDoPinNo      ''Pin No
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                If intPinNo >= 0 Then
                                                    If intPin - 1 + intPinNo <= gCstCountFuPin Then
                                                        For j = 0 To intPinNo - 1
                                                            ''チャンネル構造体からFU構造体に情報を移す
                                                            Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin + j, j, False)
                                                        Next
                                                    End If
                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveAI_AO1, _
                                             gCstCodeChDataTypeValveAI_AO2

                                            ''INPUT
                                            If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                               (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                               (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                                ''チャンネル構造体からFU構造体に情報を移す
                                                Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin)

                                            End If

                                            ''OUTPUT
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveAiAoFuNo        ''Fu No
                                                intPortNo = .ValveAiAoPortNo    ''Port No
                                                intPin = .ValveAiAoPin          ''Pin
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                ''チャンネル構造体からFU構造体に情報を移す
                                                Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin, intLoopNothing, False)

                                            End If

                                        Case gCstCodeChDataTypeValveAO_4_20

                                            ''OUTPUT
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveAiAoFuNo        ''Fu No
                                                intPortNo = .ValveAiAoPortNo    ''Port No
                                                intPin = .ValveAiAoPin          ''Pin
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                ''チャンネル構造体からFU構造体に情報を移す
                                                Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin, intLoopNothing, False)

                                            End If

                                        Case gCstCodeChDataTypeValveDO

                                            ''OUTPUT
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveDiDoFuNo        ''Fu No
                                                intPortNo = .ValveDiDoPortNo    ''Port No
                                                intPin = .ValveDiDoPin          ''Pin
                                                'Ver2.0.7.Z DOのPin数は１固定
                                                'intPinNo = .ValveDiDoPinNo      ''Pin No
                                                intPinNo = 1      ''Pin No
                                                '-
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                If intPinNo >= 0 Then
                                                    If intPin - 1 + intPinNo <= gCstCountFuPin Then
                                                        For j = 0 To intPinNo - 1 'Ver2.0.2.1 復活 Ver2.0.0.2 「-1」をDEL
                                                            ''チャンネル構造体からFU構造体に情報を移す
                                                            Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin + j, j, False)
                                                        Next
                                                    End If
                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveExt

                                            ''OUTPUT
                                            With gudt.SetChInfo.udtChannel(i)
                                                intFuNo = .ValveDiDoFuNo        ''Fu No
                                                intPortNo = .ValveDiDoPortNo    ''Port No
                                                intPin = .ValveDiDoPin          ''Pin
                                            End With

                                            If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                                               (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                                               (intPin >= 1 And intPin <= gCstCountFuPin) Then

                                                ''チャンネル構造体からFU構造体に情報を移す
                                                Call mSetFuInfoChData(udtFuInfo, i, intFuNo, intPortNo, intPin, intLoopNothing, False)

                                            End If

                                    End Select

                                Case gCstCodeChTypeComposite

                                    ''各設定値が範囲内のものだけ追加する
                                    If (.shtFuno >= 0 And .shtFuno <= gCstCountFuNo - 1) And _
                                       (.shtPortno >= 1 And .shtPortno <= gCstCountFuPort) And _
                                       (.shtPin >= 1 And .shtPin <= gCstCountFuPin) Then

                                        If .shtPinNo >= 0 Then
                                            If .shtPin - 1 + .shtPinNo <= gCstCountFuPin Then
                                                ''INPUT
                                                For j = 0 To .shtPinNo - 1
                                                    ''INPUT：チャンネル構造体からFU構造体に情報を移す
                                                    Call mSetFuInfoChData(udtFuInfo, i, .shtFuno, .shtPortno, .shtPin + j, j)
                                                Next
                                            End If
                                        End If

                                    End If

                            End Select
                        End If

                    End If

                End With

            Next x

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 端子台情報をFU情報構造体に設定する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) ループIndex 
    ' 機能説明  : 端子台情報をFU情報構造体に設定する
    '--------------------------------------------------------------------
    Private Sub mSetFuInfoChName(ByRef udtFuInfo() As gTypFuInfo, _
                                 ByVal intChIndex As Integer)

        Try

            udtFuInfo(intChIndex).strFuName = gGetString(gudt.SetChDisp.udtChDisp(intChIndex).strFuName)
            udtFuInfo(intChIndex).strNamePlate = gGetString(gudt.SetChDisp.udtChDisp(intChIndex).strNamePlate)
            udtFuInfo(intChIndex).strFuType = gGetString(gudt.SetChDisp.udtChDisp(intChIndex).strFuType)
            udtFuInfo(intChIndex).strRemarks = gGetString(gudt.SetChDisp.udtChDisp(intChIndex).strRemarks)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "出力チャンネルデータをFU情報構造体に設定"

    '--------------------------------------------------------------------
    ' 機能      : 出力チャンネル情報をFU情報構造体に設定する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) 出力チャンネルのループIndex 
    ' 　　　    : ARG3 - (I ) FU番号 
    ' 　　　    : ARG4 - (I ) ポート番号
    ' 　　　    : ARG5 - (I ) ピン番号
    ' 　　　    : ARG6 - (I ) 計測点個数のループIndex
    ' 　　　    : ARG7 - (I ) 入出力処理の判断フラグ（TRUE：In側の処理　FALSE：Out側の処理）
    ' 機能説明  : FU情報の詳細を設定する
    '--------------------------------------------------------------------
    Private Sub mSetFuInfoChOutput(ByRef udtFuInfo() As gTypFuInfo, _
                                   ByVal intChoutputChIndex As Integer, _
                                   ByVal intFuNo As Integer, _
                                   ByVal intPortno As Integer, _
                                   ByVal intPin As Integer, _
                          Optional ByVal intLoopCnt As Integer = 0, _
                          Optional ByVal blnInputProcess As Boolean = True)

        Try

            ''Port種別
            udtFuInfo(intFuNo).udtFuPort(intPortno - 1).intPortType = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtType

            ''端子台種別
            udtFuInfo(intFuNo).udtFuPort(intPortno - 1).intTerinf = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtTerinf

            With udtFuInfo(intFuNo).udtFuPort(intPortno - 1).udtFuPin(intPin - 1)

                ''ChNO
                Select Case gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).bytType
                    Case gCstCodeFuOutputChTypeCh

                        ''ChNO
                        .strChNo = gConvNullToZero(gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).shtChid).ToString("0000")

                    Case gCstCodeFuOutputChTypeOr
                        ''印刷表示用
                        .strChNo = " OR "

                    Case gCstCodeFuOutputChTypeAnd
                        .strChNo = "AND "

                End Select

                ''出力設定
                .bytOutput = gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).bytOutput
                .bytOutStatus = gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).bytStatus
                .intOutMask = gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).shtMask

                ''------------------------------------------------------------
                '' 出力チャンネルのTYPE：論理出力(AND/OR)チャネル時の処理
                ''------------------------------------------------------------
                If gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).bytType = gCstCodeFuOutputChTypeOr Or _
                   gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).bytType = gCstCodeFuOutputChTypeAnd Then

                    .strItemName = mSetFuInfoChOutputItemName(gudt.SetChAndOr, gudt.SetChOutput.udtCHOutPut(intChoutputChIndex))
                    .strStatus = ""

                    ' 2015.11.07  Outputﾀｲﾌﾟ
                    ' ''Public Const gCstCodeFuOutputTypeInvalid As Integer = 0
                    ' ''Public Const gCstCodeFuOutputTypeAlmFtLt As Integer = 1
                    ' ''Public Const gCstCodeFuOutputTypeAlmFt__ As Integer = 2
                    ' ''Public Const gCstCodeFuOutputTypeAlm__LT As Integer = 3
                    ' ''Public Const gCstCodeFuOutputTypeAlm____ As Integer = 4
                    ' ''Public Const gCstCodeFuOutputTypeCh____ As Integer = 5
                    ' ''Public Const gCstCodeFuOutputTypeRun__LT As Integer = 6

                Else

                    ''--------------------------------------------
                    '' 出力チャンネルのTYPE：CHデータ時の処理
                    ''--------------------------------------------
                    .strItemName = mSetFuInfoChOutputItemName(gudt.SetChAndOr, gudt.SetChOutput.udtCHOutPut(intChoutputChIndex))

                    ''出力チャンネルのチャンネル番号から、チャンネルデータの該当Indexを取得
                    Dim intChinfoChIndex As Integer = mGetChIndex(gudt.SetChOutput.udtCHOutPut(intChoutputChIndex).shtChid)

                    If intChinfoChIndex <> -1 Then

                        .blnChComDmy = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtFlag1, 0), 1, 0)
                        .blnChComSc = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtFlag1, 1), 1, 0)
                        .strGroupNo = gGetString(gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtGroupNo)
                        .intChType = gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtChType
                        .intDataType = gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtData  ''データ種別　ver.1.4.0 2011.08.17
                        .intSignal = gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtSignal  ''入力信号　　ver.1.4.0 2011.08.17

                        ''ステータス種別（.strStatus：コードを名称に変換）
                        Call mGetFuInfoStatus(udtFuInfo(intFuNo).udtFuPort(intPortno - 1).udtFuPin(intPin - 1), _
                                              intChinfoChIndex, _
                                              gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.shtChType, _
                                              intLoopCnt, _
                                              blnInputProcess, _
                                              True)
                        ''レンジ設定　
                        Call mGetFuInfoRange(udtFuInfo(intFuNo).udtFuPort(intPortno - 1).udtFuPin(intPin - 1), intChinfoChIndex)

                        '' Ver1.9.3 2016.01.16  出力ｽﾃｰﾀｽ名称追加
                        .strOutStatus = GetOutStatus(gudt.SetChInfo.udtChannel(intChinfoChIndex), _
                                             .bytOutput, .bytOutStatus, .intOutMask)

                    End If

                End If

                ''計測点詳細情報（CableMark1, CableMark2, Core1, Core2, Dist）
                Call mGetFuInfoSlotInfo(udtFuInfo(intFuNo).udtFuPort(intPortno - 1).udtFuPin(intPin - 1), _
                                        intFuNo, _
                                        intPortno, _
                                        intPin)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 出力ｽﾃｰﾀｽ文字列取得
    ' 返り値    : 出力ｽﾃｰﾀｽ名称
    ' 引き数    : udtCH - CHﾃﾞｰﾀ
    ' 　　　    : bytOutType - 出力ﾀｲﾌﾟ
    ' 　　　    : bytOutStatus - 出力ｽﾃｰﾀｽ 
    ' 　　　    : intMask - ﾏｽｸﾋﾞｯﾄ
    ' 機能説明  : FU情報の詳細を設定する
    '--------------------------------------------------------------------
    Public Function GetOutStatus(ByVal udtCH As gTypSetChRec, _
                                  ByVal bytOutType As Byte, _
                                  ByVal bytOutStatus As Byte, _
                                  ByVal intMask As Integer) As String

        Dim strStatus As String = ""
        Dim strString As String = ""
        Dim nIndex As Integer

        Try
            If bytOutStatus = 2 Then        '' ON/OFF
                strStatus = GetStatus(udtCH)
                If strStatus <> "" Then
                    nIndex = strStatus.IndexOf("/")
                    If nIndex = -1 Then     '' / がみつからない場合はｽﾃｰﾀｽを表示
                        strString = strStatus
                    Else
                        If intMask = 0 Then  '' OFFｽﾃｰﾀｽ　　/以降
                            'Ver2.0.7.L
                            'strString = strStatus.Substring(nIndex + 1)
                            strString = MidB(strStatus, nIndex + 1)
                        Else
                            'Ver2.0.7.L
                            'strString = strStatus.Substring(0, nIndex)
                            strString = MidB(strStatus, 0, nIndex)
                        End If
                    End If
                End If
            ElseIf bytOutStatus = 1 Then        '' MOTOR
                strString = GetMotorOutputStatus(intMask, udtCH.udtChCommon.shtData)
            Else    '' Alm 
                strString = GetAlmOutputStatus(intMask, udtCH.udtChCommon.shtChType, udtCH)
            End If

            Return strString

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try


    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output ﾓｰﾀｰ　ｽﾃｰﾀｽ
    ' 引数      ： intMask     ﾏｽｸﾋﾞｯﾄ
    '           ： intDataType   ﾃﾞｰﾀ種別
    ' 戻値      ： OUTPUTｽﾃｰﾀｽ文字列
    '----------------------------------------------------------------------------
    Private Function GetMotorOutputStatus(ByVal intMask As Integer, _
                                  ByVal intDataType As Integer) As String

        Dim strStatus As String = ""
        Dim nLen As Integer

        Try
            'Ver2.0.9.2 モーター種別増加 START
            Select Case intDataType
                Case gCstCodeChDataTypeMotorManRun, gCstCodeChDataTypeMotorAbnorRun, gCstCodeChDataTypeMotorRManRun, gCstCodeChDataTypeMotorRAbnorRun      '' RUN
                    If (intMask And &H2) = &H2 Then '' RUN
                        strStatus = "RUN/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If

                Case gCstCodeChDataTypeMotorManRunA, gCstCodeChDataTypeMotorAbnorRunA, gCstCodeChDataTypeMotorRManRunA, gCstCodeChDataTypeMotorRAbnorRunA '' RUN-A
                    If (intMask And &H2) = &H2 Then '' RUN
                        strStatus = "RUN/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                    If (intMask And &H20) = &H20 Then '' ST/BY
                        strStatus = strStatus & "STBY"
                    End If

                Case gCstCodeChDataTypeMotorManRunB, gCstCodeChDataTypeMotorAbnorRunB, gCstCodeChDataTypeMotorRManRunB, gCstCodeChDataTypeMotorRAbnorRunB   '' RUN-B
                    If (intMask And &H4) = &H4 Then '' RUN-L
                        strStatus = "RUN-L/"
                    End If

                    If (intMask And &H2) = &H2 Then '' RUN-H
                        strStatus = strStatus & "RUN-H/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If

                Case gCstCodeChDataTypeMotorManRunC, gCstCodeChDataTypeMotorAbnorRunC, gCstCodeChDataTypeMotorRManRunC, gCstCodeChDataTypeMotorRAbnorRunC   '' RUN-C
                    If (intMask And &H4) = &H4 Then '' SUP
                        strStatus = "SUP/"
                    End If

                    If (intMask And &H2) = &H2 Then '' EXH
                        strStatus = strStatus & "EXH/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                Case gCstCodeChDataTypeMotorManRunD, gCstCodeChDataTypeMotorAbnorRunD, gCstCodeChDataTypeMotorRManRunD, gCstCodeChDataTypeMotorRAbnorRunD   '' RUN-D
                    If (intMask And &H10) = &H10 Then '' SUP-L
                        strStatus = "SUP-L/"
                    End If

                    If (intMask And &H8) = &H8 Then '' SUP-H
                        strStatus = strStatus & "SUP-H/"
                    End If

                    If (intMask And &H4) = &H4 Then '' EXH-L
                        strStatus = strStatus & "EXH-L/"
                    End If

                    If (intMask And &H2) = &H2 Then '' EXH-H
                        strStatus = strStatus & "EXH-H/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                Case gCstCodeChDataTypeMotorManRunE, gCstCodeChDataTypeMotorAbnorRunE, gCstCodeChDataTypeMotorRManRunE, gCstCodeChDataTypeMotorRAbnorRunE   '' RUN-E
                    If (intMask And &H4) = &H4 Then '' FWD
                        strStatus = "FWD/"
                    End If

                    If (intMask And &H2) = &H2 Then '' REV
                        strStatus = strStatus & "REV/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If

                Case gCstCodeChDataTypeMotorManRunF, gCstCodeChDataTypeMotorAbnorRunF, gCstCodeChDataTypeMotorRManRunF, gCstCodeChDataTypeMotorRAbnorRunF   '' RUN-F
                    If (intMask And &H10) = &H10 Then '' FWD-L
                        strStatus = "FWD-L/"
                    End If

                    If (intMask And &H8) = &H8 Then '' FWD-H
                        strStatus = strStatus & "FWD-H/"
                    End If

                    If (intMask And &H4) = &H4 Then '' REV-L
                        strStatus = strStatus & "REV-L/"
                    End If

                    If (intMask And &H2) = &H2 Then '' REV-H
                        strStatus = strStatus & "REV-H/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunG, gCstCodeChDataTypeMotorAbnorRunG, gCstCodeChDataTypeMotorRManRunG, gCstCodeChDataTypeMotorRAbnorRunG   '' RUN-G
                    If (intMask And &H2) = &H2 Then '' AUTO
                        strStatus = "AUTO/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If

                Case gCstCodeChDataTypeMotorManRunH, gCstCodeChDataTypeMotorAbnorRunH, gCstCodeChDataTypeMotorRManRunH, gCstCodeChDataTypeMotorRAbnorRunH   '' RUN-H
                    If (intMask And &H2) = &H2 Then '' RUN
                        strStatus = "RUN/"
                    End If

                    If (intMask And &H20) = &H20 Then '' AUTO
                        strStatus = strStatus & "AUTO/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP"
                    End If

                Case gCstCodeChDataTypeMotorManRunI, gCstCodeChDataTypeMotorAbnorRunI, gCstCodeChDataTypeMotorRManRunI, gCstCodeChDataTypeMotorRAbnorRunI   '' RUN-I

                    If (intMask And &H2) = &H2 Then '' AUTO
                        strStatus = "AUTO/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                    If (intMask And &H20) = &H20 Then '' ST/BY
                        strStatus = strStatus & "STBY"
                    End If

                Case gCstCodeChDataTypeMotorManRunJ, gCstCodeChDataTypeMotorAbnorRunJ, gCstCodeChDataTypeMotorRManRunJ, gCstCodeChDataTypeMotorRAbnorRunJ   '' RUN-J
                    If (intMask And &H4) = &H4 Then '' RUN-L
                        strStatus = "RUN-L/"
                    End If

                    If (intMask And &H2) = &H2 Then '' RUN-H
                        strStatus = strStatus & "RUN-H/"
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                    If (intMask And &H20) = &H20 Then '' ST/BY
                        strStatus = strStatus & "STBY"
                    End If

                Case gCstCodeChDataTypeMotorManRunK, gCstCodeChDataTypeMotorAbnorRunK, gCstCodeChDataTypeMotorRManRunK, gCstCodeChDataTypeMotorRAbnorRunK   '' RUN-K
                    If (intMask And &H4) = &H4 Then '' ECO RUN
                        strStatus = "ECO-RUN/"  '' Ver1.12.0.0 2016.12.22  '-'追加
                    End If

                    If (intMask And &H2) = &H2 Then '' BYPS RUN
                        strStatus = strStatus & "BYPS-RUN/" '' Ver1.12.0.0 2016.12.22  '-'追加
                    End If

                    If (intMask And &H40) = &H40 Then '' STOP
                        strStatus = strStatus & "STOP/"
                    End If

                    If (intMask And &H20) = &H20 Then '' ST/BY
                        strStatus = strStatus & "STBY"
                    End If

                    'Ver2.0.0.2 モーター種別増加 R Device ADD
                Case gCstCodeChDataTypeMotorDevice, gCstCodeChDataTypeMotorRDevice  '' M0 RUN
                    If (intMask And &H2) = &H2 Then '' RUN
                        strStatus = "RUN"
                    End If

            End Select
            'Ver2.0.0.2 モーター種別増加 END


            '' M2ならばSTOP →　NORMALに変換
            'Ver2.0.0.2 モーター種別増加
            If intDataType >= gCstCodeChDataTypeMotorAbnorRun And intDataType <= gCstCodeChDataTypeMotorAbnorRunK _
                Or _
                (intDataType >= gCstCodeChDataTypeMotorRAbnorRun And intDataType <= gCstCodeChDataTypeMotorRAbnorRunK) _
                Then
                strStatus = strStatus.Replace("STOP", "NORMAL")
            End If

            '' 最後の1文字が"/"ならば削除
            If strStatus.EndsWith("/") Then
                'Ver2.0.7.L
                'nLen = Len(strStatus)
                'strStatus = strStatus.Substring(0, nLen - 1)
                nLen = LenB(strStatus)
                strStatus = MidB(strStatus, 0, nLen - 1)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

        Return strStatus

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Alm　ｽﾃｰﾀｽ
    ' 引数      ： intMask     ﾏｽｸﾋﾞｯﾄ
    '           ： intCHType     CH種別
    '           ： intDataType   ﾃﾞｰﾀ種別
    ' 戻値      ： OUTPUTｽﾃｰﾀｽ文字列
    '----------------------------------------------------------------------------
    Private Function GetAlmOutputStatus(ByVal intMask As Integer, _
                                        ByVal intCHType As Integer, _
                                        ByVal udtCH As gTypSetChRec) As String
        Try

            Dim strStatus As String = ""
            Dim nLen As Integer
            Dim strLL As String = "LL"
            Dim strL As String = "L"
            Dim strH As String = "H"
            Dim strHH As String = "HH"

            If udtCH.udtChCommon.shtChType = gCstCodeChTypeAnalog Then      '' ｱﾅﾛｸﾞ
                If udtCH.udtChCommon.shtStatus = &HFF Then  '' 手入力
                    strLL = gGetString(udtCH.AnalogLoLoStatusInput)
                    strL = gGetString(udtCH.AnalogLoStatusInput)
                    strH = gGetString(udtCH.AnalogHiStatusInput)
                    strHH = gGetString(udtCH.AnalogHiHiStatusInput)
                Else
                    '' Lｽﾃｰﾀｽ
                    Select Case udtCH.udtChCommon.shtStatus
                        Case &H42, &H43
                            strL = "LOW"
                        Case &H48, &H49
                            strL = "EL"
                        Case Else
                            strL = "L"
                    End Select

                    '' Hｽﾃｰﾀｽ
                    Select Case udtCH.udtChCommon.shtStatus
                        Case &H41, &H43
                            strH = "HIGH"
                        Case &H47, &H49
                            strH = "EH"
                        Case Else
                            strH = "H"
                    End Select
                End If

                '' ｽﾃｰﾀｽ名称作成
                If (intMask And &H4) = &H4 Then
                    strStatus = strStatus & strLL & "/"
                End If

                If (intMask And &H1) = &H1 Then
                    strStatus = strStatus & strL & "/"
                End If

                If (intMask And &H2) = &H2 Then
                    strStatus = strStatus & strH & "/"
                End If

                If (intMask And &H8) = &H8 Then
                    strStatus = strStatus & strHH & "/"
                End If

                If (intMask And &H60) <> 0 Then
                    strStatus = strStatus & "SENSOR"
                End If
            Else
                strStatus = "ALM"
            End If

            '' 最後の1文字が"/"ならば削除
            If strStatus.EndsWith("/") Then
                'Ver2.0.7.L
                'nLen = Len(strStatus)
                'strStatus = strStatus.Substring(0, nLen - 1)
                nLen = LenB(strStatus)
                strStatus = MidB(strStatus, 0, nLen - 1)
            End If

            Return strStatus

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

            Return ""

        End Try

    End Function


    '--------------------------------------------------------------------
    ' 機能      : 論理出力設定ファイルのインデックスを取得
    ' 返り値    : インデックス
    ' 引き数    : ARG1 - (I ) 1:OR　2:AND
    '           : ARG2 - (I ) 出力チャンネル設定構造体のインデックス
    '           : ARG3 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 出力チャンネルの中で何番目のAND(OR)かを判断する
    '--------------------------------------------------------------------
    Private Function mGetIndexOrAnd(ByVal hintOrAnd As Integer, _
                                    ByVal hintIndex As Integer, _
                                    ByVal udtSet As gTypSetChOutput) As Integer

        Try

            Dim intCnt As Integer = 0

            For i As Integer = LBound(udtSet.udtCHOutPut) To hintIndex

                With udtSet.udtCHOutPut(i)

                    If .bytType = hintOrAnd Then
                        intCnt += 1
                    End If

                End With

            Next

            Return intCnt - 1

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号からチャンネルデータのIndexを取得
    ' 返り値    : ChInfo-チャンネルIndex
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネル番号からチャンネルデータのIndexを取得する
    ' 補足　　  : チャンネル番号が 0 の場合は 0 を返す
    '--------------------------------------------------------------------
    Public Function mGetChIndex(ByVal intChNo As Integer) As Integer

        Try

            Dim intLoopCnt As Integer = -1

            ''チャンネルNOが0の時は処理を抜ける
            If intChNo = 0 Then Exit Function

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChNo Then

                        ''チャンネルデータのIndexを取得
                        intLoopCnt = i

                        ''一致したら処理を抜ける
                        Exit For

                    End If

                End With

            Next

            Return intLoopCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能      : チャンネルNO から チャンネルの配列Index を取得
    ' 返り値    : チャンネルの配列Index
    ' 引き数    : ARG1 - (I ) チャンネルNO
    ' 機能説明  : チャンネルNO から チャンネルの配列Indexを取得する
    ' 補足　　  : チャンネルNOが 0 の場合は 0 を返す
    '     　　  : チャンネルNOに対応する配列が存在しない場合は -1 を返す
    '----------------------------------------------------------------------------
    Private Function mConvChNoToChArrayId(ByVal intChNo As Integer) As Integer

        Try

            If intChNo = 0 Then Return 0

            Dim shtLoopIndex As Short = -1

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    If .shtChno = intChNo Then

                        shtLoopIndex = i

                        Exit For

                    End If

                End With

            Next

            Return shtLoopIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のItemName作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 論理出力チャンネルデータ構造体
    ' 　　　    : ARG2 - (I ) 出力チャネル構造体
    ' 機能説明  : ItemName設定  
    '--------------------------------------------------------------------
    Private Function mSetFuInfoChOutputItemName(ByVal hudtSetChAndOr As gTypSetChAndOr, _
                                                ByVal hudtSetChOutput As gTypSetChOutputRec) As String
        Dim strRtn As String = ""

        Try

            Dim intChinfoChIndex As Integer
            Dim strChStart As String = ""
            Dim strChEnd As String = ""
            Dim intChIndex As Integer

            '' Ver1.9.3 2016.01.16 追加
            Dim strTemp As String = ""
            Dim intPreCH As Integer = 0
            Dim intCH As Integer
            ''//

            If hudtSetChOutput.bytType <> gCstCodeFuOutputChTypeCh Then

                ''------------------------------------------------------------------------
                '' 論理出力チャンネル（ANDデータ／ORデータ）の場合
                '' ※論理出力構造体から該当チャンネル番号を取得し、ItemName文字列を作成する
                ''------------------------------------------------------------------------
                intChIndex = hudtSetChOutput.shtChid - 1

                With gudt.SetChOutput.udtCHOutPut(intChIndex)

                    ''スタートCH番号を取得する
                    strChStart = gGet2Byte(hudtSetChAndOr.udtCHOut(intChIndex).udtCHAndOr(0).shtChid)

                    ''ItemName文字列を作成する際の終了CH番号を取得する
                    For j = 0 To UBound(hudtSetChAndOr.udtCHOut(intChIndex).udtCHAndOr)

                        With hudtSetChAndOr.udtCHOut(intChIndex).udtCHAndOr(j)

                            If gGet2Byte(.shtChid) <> 0 Then strChEnd = gGet2Byte(.shtChid)

                            '' Ver1.9.3 2016.01.16 追加
                            intCH = gGet2Byte(.shtChid)
                            If intCH <> 0 Then
                                If intPreCH = 0 Then        '' 最初のﾃﾞｰﾀ
                                    If intCH < 1000 Then
                                        strTemp = intCH.ToString("0000")
                                    Else
                                        strTemp = intCH.ToString
                                    End If

                                ElseIf CInt(intPreCH / 100) = CInt(intCH / 100) Then     '' 
                                    If intCH < 1000 Then
                                        strTemp = strTemp & "/" & intCH.ToString.Substring(1)
                                    Else
                                        strTemp = strTemp & "/" & intCH.ToString.Substring(2)
                                    End If
                                Else
                                    If intCH < 1000 Then
                                        strTemp = strTemp & "/" & intCH.ToString("0000")
                                    Else
                                        strTemp = strTemp & "/" & intCH.ToString
                                    End If
                                End If
                                intPreCH = intCH
                            End If
                            ''//

                        End With

                    Next

                    '' Ver1.9.3 2016.01.29  CHOR のCH番号表示　先頭と最終のみ表示する方法にて保留
                    ''If LenB(strTemp) > 30 Then
                    ''ItemName文字列の作成
                    strRtn = "CH No." & strChStart & " - CH No." & strChEnd
                    ''Else
                    ''strRtn = "CH" & strTemp
                    ''End If


                End With

            Else

                ''------------------------------------------------------------------------
                '' CHデータの場合
                '' ※チャンネル設定構造体からItemName文字列を取得する
                ''------------------------------------------------------------------------
                ''出力チャンネルのチャンネル番号から、チャンネルデータの該当Indexを取得する
                intChinfoChIndex = mGetChIndex(hudtSetChOutput.shtChid)

                If intChinfoChIndex <> -1 Then

                    strRtn = gGetString(gudt.SetChInfo.udtChannel(intChinfoChIndex).udtChCommon.strChitem)

                End If

            End If

            Return strRtn

        Catch ex As Exception
            Return strRtn
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネルデータをFU情報構造体に設定"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報をFU情報構造体に設定する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) チャンネルデータのループIndex 
    ' 　　　    : ARG3 - (I ) FU番号 
    ' 　　　    : ARG4 - (I ) ポート番号
    ' 　　　    : ARG5 - (I ) ピン番号
    ' 　　　    : ARG6 - (I ) 計測点個数のループIndex
    ' 　　　    : ARG7 - (I ) 入出力判断フラグ（TRUE：In側の処理　FALSE：Out側の処理）
    ' 機能説明  : FU情報の詳細を設定する
    '--------------------------------------------------------------------
    Private Sub mSetFuInfoChData(ByRef udtFuInfo() As gTypFuInfo, _
                                 ByVal intChIndex As Integer, _
                                 ByVal intFuNo As Integer, _
                                 ByVal intPortno As Integer, _
                                 ByVal intPin As Integer, _
                        Optional ByVal intLoopCnt As Integer = 0, _
                        Optional ByVal blnInputProcess As Boolean = True)


        Try

            Dim RH_flg As Integer = 0

            ''運転積算は2CH目の設定とする  2013.11.23
            ''パルス積算CHの場合
            If gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType = gCstCodeChTypePulse Then

                ''データ種別が運転積算の場合
                '' Ver1.11.8.3 2016.11.08  運転積算 通信CH追加
                'If gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                'Or gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then
                '' Ver1.12.0.1 2017.01.13 関数に変更
                If gChkRunHourCH(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon._shtChno) Then
                    RH_flg = 1  ' 運転積算CH
                End If

            End If

            With udtFuInfo(intFuNo).udtFuPort(intPortno - 1)
                ' 1CHにしか対応していない為、既にCH設定されていない場合のみ　ver.1.4.0 2011.08.17
                If .udtFuPin(intPin - 1).strChNo = "" And RH_flg = 0 Then   '' 運転積算は2CH目の設定とする  2013.11.23

                    '' 運転積算CH設定済で対象CHが隠しCHの場合はCH番号のみセット 2015.04.23
                    If .udtFuPin(intPin - 1).strChNo2 <> "" And gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 1) = True Then
                        ''チャンネル番号
                        ''.udtFuPin(intPin - 1).strChNo = gConvNullToZero(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChno).ToString("0000")
                    Else

                        ''ダミーフラグ
                        .udtFuPin(intPin - 1).blnChComDmy = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 0), 1, 0)

                        ''SCフラグ
                        .udtFuPin(intPin - 1).blnChComSc = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 1), 1, 0)
                        'Ver2.0.2.6
                        .udtFuPin(intPin - 1).blnChComSc2 = False
                        .udtFuPin(intPin - 1).blnChComSc3 = False


                        ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                        If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                            ''チャンネル番号
                            .udtFuPin(intPin - 1).strChNo = gConvNullToZero(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChno).ToString("0000")
                        Else
                            .udtFuPin(intPin - 1).strChNo = GetTagNo(gudt.SetChInfo.udtChannel(intChIndex))
                        End If

                        '' 2015.10.16  ﾀｸﾞ表示強制
                        'If gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Length < 6 Then
                        '    .udtFuPin(intPin - 1).strChNo = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark
                        'Else
                        '    .udtFuPin(intPin - 1).strChNo = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Substring(0, 6)
                        'End If
                        ' //

                        ''チャンネル種別
                        .udtFuPin(intPin - 1).intChType = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType

                        ''データ種別　ver.1.4.0 2011.08.17
                        .udtFuPin(intPin - 1).intDataType = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData

                        ''入力信号　ver.1.4.0 2011.08.17
                        .udtFuPin(intPin - 1).intSignal = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtSignal

                        ''グループ番号
                        .udtFuPin(intPin - 1).strGroupNo = gGetString(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtGroupNo)

                        ''アイテム名称
                        .udtFuPin(intPin - 1).strItemName = mSetFuInfoChDataItemName(gudt.SetChAndOr, intChIndex)

                        ''Port種別
                        .intPortType = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtType

                        ''端子種別
                        .intTerinf = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtTerinf

                        ''ステータス名称
                        Call mGetFuInfoStatus(.udtFuPin(intPin - 1), _
                                              intChIndex, _
                                              gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType, _
                                              intLoopCnt, _
                                              blnInputProcess)

                        ''レンジ設定
                        Call mGetFuInfoRange(.udtFuPin(intPin - 1), intChIndex)

                        ''端子台情報
                        Call mGetFuInfoSlotInfo(.udtFuPin(intPin - 1), intFuNo, intPortno, intPin)

                    End If

                ElseIf .udtFuPin(intPin - 1).strChNo2 = "" Then  ' CH_NO (同一端子CH表示用)     2013.11.23
                    ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        ''チャンネル番号
                        .udtFuPin(intPin - 1).strChNo2 = gConvNullToZero(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChno).ToString("0000")
                    Else
                        .udtFuPin(intPin - 1).strChNo2 = GetTagNo(gudt.SetChInfo.udtChannel(intChIndex))
                    End If

                    '' 2015.10.16  ﾀｸﾞ表示強制
                    'If gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Length < 6 Then
                    '    .udtFuPin(intPin - 1).strChNo2 = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark
                    'Else
                    '    .udtFuPin(intPin - 1).strChNo2 = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Substring(0, 6)
                    'End If
                    ' //

                    If .udtFuPin(intPin - 1).strChNo = "" Then   ' 1CH目の設定がない場合に情報をセットしておく(運転積算用)
                        ''ダミーフラグ
                        .udtFuPin(intPin - 1).blnChComDmy = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 0), 1, 0)

                        ''SCフラグ
                        .udtFuPin(intPin - 1).blnChComSc = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 1), 1, 0)
                        'Ver2.0.2.6
                        .udtFuPin(intPin - 1).blnChComSc2 = False
                        .udtFuPin(intPin - 1).blnChComSc3 = False

                        ''チャンネル種別
                        .udtFuPin(intPin - 1).intChType = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType

                        ''データ種別　ver.1.4.0 2011.08.17
                        .udtFuPin(intPin - 1).intDataType = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData

                        ''入力信号　ver.1.4.0 2011.08.17
                        .udtFuPin(intPin - 1).intSignal = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtSignal

                        ''グループ番号
                        .udtFuPin(intPin - 1).strGroupNo = gGetString(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtGroupNo)

                        ''アイテム名称
                        .udtFuPin(intPin - 1).strItemName = mSetFuInfoChDataItemName(gudt.SetChAndOr, intChIndex)

                        ''Port種別
                        .intPortType = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtType

                        ''端子種別
                        .intTerinf = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortno - 1).shtTerinf

                        ''ステータス名称
                        Call mGetFuInfoStatus(.udtFuPin(intPin - 1), _
                                              intChIndex, _
                                              gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType, _
                                              intLoopCnt, _
                                              blnInputProcess)

                        ''レンジ設定
                        Call mGetFuInfoRange(.udtFuPin(intPin - 1), intChIndex)

                        ''端子台情報
                        Call mGetFuInfoSlotInfo(.udtFuPin(intPin - 1), intFuNo, intPortno, intPin)
                    Else
                        'Ver2.0.2.6
                        .udtFuPin(intPin - 1).blnChComSc2 = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 1), 1, 0)
                        .udtFuPin(intPin - 1).blnChComSc3 = False
                    End If

                ElseIf .udtFuPin(intPin - 1).strChNo3 = "" Then  ' CH_NO (同一端子CH表示用)     2014.09.18
                    ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        ''チャンネル番号
                        .udtFuPin(intPin - 1).strChNo3 = gConvNullToZero(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChno).ToString("0000")

                    Else
                        .udtFuPin(intPin - 1).strChNo3 = GetTagNo(gudt.SetChInfo.udtChannel(intChIndex))
                    End If

                    'Ver2.0.2.6
                    .udtFuPin(intPin - 1).blnChComSc3 = IIf(gBitCheck(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtFlag1, 1), 1, 0)

                    '' 2015.10.16  ﾀｸﾞ表示強制
                    'If gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Length < 6 Then
                    '    .udtFuPin(intPin - 1).strChNo3 = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark
                    'Else
                    '    .udtFuPin(intPin - 1).strChNo3 = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strRemark.Substring(0, 6)
                    'End If
                    ' //

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のItemName作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 出力チャネル構造体
    ' 　　　    : ARG2 - (I ) チャンネルデータのIndex
    ' 機能説明  : ItemName設定
    '--------------------------------------------------------------------
    Private Function mSetFuInfoChDataItemName(ByVal hudtSetChAndOr As gTypSetChAndOr, _
                                              ByVal hintChIndex As Integer) As String


        Dim strRtn As String = ""

        Try

            '' ECCもCH名称表示に変更    2013.10.22
            ''-----------------------
            '' デジタルCH-外部機器
            ''-----------------------
            'If (gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeDigital) And _
            '   (gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeDigitalExt) Then

            '    strRtn = gGetComboItemName(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtEccFunc, gEnmComboType.ctChTerminalFunctionFuncDI)


            '    ''-----------------------
            '    '' バルブCH-外部機器
            '    ''-----------------------
            'ElseIf (gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve) And _
            '       (gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveExt) Then

            '    strRtn = gGetComboItemName(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtEccFunc, gEnmComboType.ctChTerminalFunctionFuncDO)

            'Else

            strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strChitem)

            'End If

            Return strRtn

        Catch ex As Exception
            Return strRtn
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region



#Region "共通処理・設定"

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のステータス情報作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) チャンネルデータのIndex 
    ' 　　　    : ARG3 - (I ) チャンネル種別
    ' 　　　    : ARG4 - (I ) 計測点個数のループIndex
    ' 　　　    : ARG5 - (I ) 入出力処理の判断フラグ（TRUE：In側の処理　FALSE：Out側の処理）
    ' 　　　    : ARG6 - (I ) 設定タイプの判断（True：出力CH設定、False：通常のCH設定）
    ' 機能説明  : ステータス設定
    '--------------------------------------------------------------------
    Private Sub mGetFuInfoStatus(ByRef udtSet As gTypFuInfoPin, _
                                 ByVal hintChIndex As Integer, _
                                 ByVal hintChType As Integer, _
                                 ByVal hintLoopCnt As Integer, _
                                 ByVal hblnInputProcess As Boolean, _
                        Optional ByVal hblnOutputCH As Boolean = False)

        Try

            Dim intCompositeIdx As Integer                      ''コンポジットNo
            Dim intStatus As Integer                            ''ステータス種別コード
            Dim mMotorStatusNormal() As String = Nothing        ''モーターのステータス情報格納①
            Dim mMotorStatusAbnormal() As String = Nothing      ''モーターのステータス情報格納②
            Dim strwk() As String = Nothing                     ''モーターのステータス名称
            Dim strValue As String = ""

            With udtSet

                Select Case hintChType

                    Case gCstCodeChTypeAnalog

                        ''仮設定フラグ
                        .blnDmyStatusIn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                        'Ver2.0.2.4 FUdummyフラグ
                        .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonFuAddress

                        ''ManualInputの時は手入力した値を取得。その他はiniファイルより取得
                        If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = gCstCodeChManualInputStatus Then
                            .strStatus = ""
                        Else
                            .strStatus = gGetComboItemName(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus, _
                                                           gEnmComboType.ctChListChannelListStatusAnalog)
                        End If


                    Case gCstCodeChTypeDigital

                        ''仮設定フラグ
                        .blnDmyStatusIn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                        'Ver2.0.2.4 FUdummyフラグ
                        .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonFuAddress

                        ''ManualInputの時は手入力した値を取得。その他はiniファイルより取得
                        If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = gCstCodeChManualInputStatus Then
                            ''MANUAL INPUTに'/'を追加　ver1.4.0 2011.08.17
                            '.strStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus

                            strValue = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus
                            'Ver2.0.7.L
                            'If strValue.Length > 8 Then
                            If LenB(strValue) > 8 Then
                                '.strStatus = strValue.Substring(0, 8).Trim & "/" & strValue.Substring(8).Trim
                                .strStatus = MidB(strValue, 0, 8).Trim & "/" & MidB(strValue, 8).Trim
                            Else
                                .strStatus = Trim(strValue)
                            End If
                        Else
                            .strStatus = gGetComboItemName(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus, _
                                                           gEnmComboType.ctChListChannelListStatusDigital)
                        End If


                    Case gCstCodeChTypeMotor

                        '--------------------------------------------
                        ''入力ステータスの仮設定フラグは出力側のみ
                        '--------------------------------------------

                        'Ver2.0.0.2 モーター種別増加 R Device ADD
                        If hblnInputProcess And _
                           gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Or _
                           gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then

                            ''Status I：DeviceOperation
                            ' 2013.07.22 MO表示変更  K.Fujimoto
                            '.strStatus = "RUN/STOP"
                            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様
                                .strStatus = "運転"
                            Else
                                .strStatus = "RUN"
                            End If

                        ElseIf hblnInputProcess And _
                               gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                            ''Status I：Manual Input
                            .strStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus

                        ElseIf Not hblnInputProcess And _
                               gudt.SetChInfo.udtChannel(hintChIndex).MotorStatus = gCstCodeChManualInputStatus Then

                            ''仮設定フラグ
                            .blnDmyStatusOut = mGetFuInfoStatusDmyMotor(hintChIndex, hintLoopCnt)

                            'Ver2.0.2.4 FUdummyフラグ
                            .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutFuAddress

                            ''Status O：Manual Input
                            .strStatus = mGetFuInfoStatusDoMotor(hintChIndex, hintLoopCnt)

                        Else

                            ''ステータス種別コード（※モーターのStatusはInput側のコードをI/O共通で参照）
                            intStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus

                            ''モーターチャンネルのステータス種別コードを取得
                            Call GetStatusMotor2(mMotorStatusNormal, mMotorStatusAbnormal, "StatusMotor")
                            '' Ver1.7.6 ﾓｰﾀｰのｽﾃｰﾀｽの考え方が違うので変更
                            If hblnOutputCH = True Then     '' Ver1.8.6 2015.12.02 Outputの場合のみ
                                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    Select Case intStatus
                                        Case &H30
                                            .strStatus = "運転"
                                        Case &H31
                                            .strStatus = "運転-A"
                                        Case &H32
                                            .strStatus = "運転-B"
                                        Case &H33
                                            .strStatus = "運転-C"
                                        Case &H34
                                            .strStatus = "運転-D"
                                        Case &H35
                                            .strStatus = "運転-E"
                                        Case &H36
                                            .strStatus = "運転-F"
                                        Case &H37
                                            .strStatus = "運転-G"
                                        Case &H38
                                            .strStatus = "運転-H"
                                        Case &H39
                                            .strStatus = "運転-I"
                                        Case &H3A
                                            .strStatus = "運転-J"
                                        Case &H3B
                                            .strStatus = "運転-K"
                                        Case Else
                                            .strStatus = ""
                                    End Select
                                Else
                                    Select Case intStatus
                                        Case &H30
                                            .strStatus = "RUN"
                                        Case &H31
                                            .strStatus = "RUN-A"
                                        Case &H32
                                            .strStatus = "RUN-B"
                                        Case &H33
                                            .strStatus = "RUN-C"
                                        Case &H34
                                            .strStatus = "RUN-D"
                                        Case &H35
                                            .strStatus = "RUN-E"
                                        Case &H36
                                            .strStatus = "RUN-F"
                                        Case &H37
                                            .strStatus = "RUN-G"
                                        Case &H38
                                            .strStatus = "RUN-H"
                                        Case &H39
                                            .strStatus = "RUN-I"
                                        Case &H3A
                                            .strStatus = "RUN-J"
                                        Case &H3B
                                            .strStatus = "RUN-K"
                                        Case Else
                                            .strStatus = ""
                                    End Select
                                End If

                                ''仮設定フラグ()
                                .blnDmyStatusOut = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                                'Ver2.0.2.4 FUdummyフラグ
                                .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutFuAddress
                            Else

                                ''データ種別によって、表示するステータス種別が異なる
                                ''　[データ種別]ManualStop  → [ステータス種別]備考欄'/'の左側表示
                                ''　[データ種別]Abnormal　　→ [ステータス種別]備考欄'/'の右側表示
                                'Ver1.11.9.6 モーター種別増加 START
                                'Ver1.12.0.1 2017.01.13 JACOM追加
                                If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                                    If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = &H14 Then
                                        .strStatus = "RUN"
                                    Else
                                        .strStatus = ""
                                    End If
                                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Then
                                    If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = &H14 Then
                                        .strStatus = "RUN"
                                    Else
                                        .strStatus = ""
                                    End If
                                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK _
                                        Or ( _
                                            gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun _
                                            And _
                                            gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK
                                            ) _
                                        Then
                                    'Ver1.11.9.6 モーター種別増加 END

                                    '-------------
                                    ''ManualStop
                                    '-------------
                                    ''引き算の結果、0より小さければ空文字返す
                                    If intStatus - gCstCodeChStatusTypeMotorRun < 0 Then
                                        .strStatus = ""
                                    Else
                                        strwk = mMotorStatusNormal(intStatus - gCstCodeChStatusTypeMotorRun).Split("_")
                                        .strStatus = strwk(hintLoopCnt)
                                    End If

                                Else

                                    ''引き算の結果、0より小さければ空文字返す
                                    If intStatus - gCstCodeChStatusTypeMotorRun < 0 Then
                                        .strStatus = ""
                                    Else

                                        '-------------
                                        ''Abnormal
                                        '-------------
                                        ''仮設定フラグ
                                        .blnDmyStatusOut = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                                        'Ver2.0.2.4 FUdummyフラグ
                                        .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutFuAddress

                                        ''ステータス
                                        .strStatus = mGetFuInfoStatusMotorAbnormal(hblnInputProcess, intStatus, hintChIndex, hintLoopCnt)

                                    End If

                                End If
                            End If



                        End If


                    Case gCstCodeChTypeValve

                        If hblnOutputCH Then

                            ''出力CHの場合は空欄
                            .strStatus = ""

                        Else

                            If hblnInputProcess Then

                                '-----------
                                '' Input
                                '-----------
                                ''仮設定フラグ
                                .blnDmyStatusIn = mGetFuInfoStatusDmyCmp(hintChIndex, hintLoopCnt)

                                'Ver2.0.2.4 FUdummyフラグ
                                .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonFuAddress

                                If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Or _
                                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Then

                                    ''[AIDO][AIAO]
                                    intStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus
                                    If intStatus = gCstCodeChManualInputStatus Then
                                        .strStatus = "" ''Manual Input
                                    Else
                                        .strStatus = gGetComboItemName(intStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    End If

                                Else

                                    ''[DIDO] コンポジットテーブルのステータスを参照する
                                    intCompositeIdx = gudt.SetChInfo.udtChannel(hintChIndex).ValveCompositeTableIndex - 1
                                    .strStatus = gGetString(gudt.SetChComposite.udtComposite(intCompositeIdx).udtCompInf(hintLoopCnt).strStatusName)

                                End If

                            Else

                                '-----------
                                '' Output
                                '-----------
                                ''仮設定フラグ
                                .blnDmyStatusOut = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                                'Ver2.0.2.4 FUdummyフラグ
                                .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutFuAddress

                                If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveDO Then

                                    ''DIDO, Digital, 外部機器(Jacom-22/延長警報盤)
                                    .strStatus = mGetFuInfoStatusDoValDIDO(hintChIndex, hintLoopCnt)

                                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                    ''AIDO(1-5V/4-20mA)
                                    .strStatus = mGetFuInfoStatusDoValAIDO(hintChIndex, hintLoopCnt)

                                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then

                                    ''AIAO(1-5V/4-20mA), Analog(4-20mA)
                                    intStatus = gudt.SetChInfo.udtChannel(hintChIndex).ValveAiAoOutStatus
                                    If intStatus = gCstCodeChManualInputStatus Then
                                        ''Manual Input
                                        .strStatus = gudt.SetChInfo.udtChannel(hintChIndex).ValveAiAoOutStatus1
                                    Else
                                        .strStatus = gGetComboItemName(intStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    End If

                                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Or _
                                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveExt Then

                                    ''外部機器(Jacom-22/延長警報盤)
                                    intStatus = gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoStatus
                                    .strStatus = gGetComboItemName(intStatus, gEnmComboType.ctChListChannelListStatusDigital)

                                End If

                            End If

                        End If

                    Case gCstCodeChTypeComposite

                        ''仮設定フラグ
                        .blnDmyStatusIn = mGetFuInfoStatusDmyCmp(hintChIndex, hintLoopCnt)

                        'Ver2.0.2.4 FUdummyフラグ
                        .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonFuAddress

                        ''コンポジットテーブルのステータスを参照する
                        intCompositeIdx = gudt.SetChInfo.udtChannel(hintChIndex).CompositeTableIndex - 1
                        .strStatus = gGetString(gudt.SetChComposite.udtComposite(intCompositeIdx).udtCompInf(hintLoopCnt).strStatusName)

                    Case gCstCodeChTypePulse

                        ''仮設定フラグ
                        .blnDmyStatusIn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonStatusName

                        'Ver2.0.2.4 FUdummyフラグ
                        .blnDmyFUadress = gudt.SetChInfo.udtChannel(hintChIndex).DummyCommonFuAddress

                        intStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus
                        If intStatus = gCstCodeChManualInputStatus Then
                            .strStatus = "" ''Manual Input
                        Else
                            .strStatus = gGetComboItemName(intStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                        End If

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 仮設定データ：コンポジットステータス情報
    ' 返り値    : TRUE:仮設定データ , FALSE:通常データ
    ' 引き数    : ARG1 - (I ) チャンネルデータIndex
    ' 　　　    : ARG2 - (I ) 計測点個数のループIndex 
    ' 機能説明  : 仮設定データかを判定する
    '--------------------------------------------------------------------
    Private Function mGetFuInfoStatusDmyCmp(ByVal hintChIndex As Integer, _
                                            ByVal hintLoopIdx As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            Select Case (hintLoopIdx + 1)
                Case 1 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus1StaNm
                Case 2 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus2StaNm
                Case 3 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus3StaNm
                Case 4 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus4StaNm
                Case 5 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus5StaNm
                Case 6 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus6StaNm
                Case 7 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus7StaNm
                Case 8 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus8StaNm
                Case 9 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyCmpStatus9StaNm
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 仮設定データ：出力ステータス情報
    ' 返り値    : TRUE:仮設定データ , FALSE:通常データ
    ' 引き数    : ARG1 - (I ) チャンネルデータIndex
    ' 　　　    : ARG2 - (I ) 計測点個数のループIndex 
    ' 機能説明  : 仮設定データかを判定する
    '--------------------------------------------------------------------
    Private Function mGetFuInfoStatusDmyMotor(ByVal hintChIndex As Integer, _
                                              ByVal hintLoopIdx As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            Select Case (hintLoopIdx + 1)
                Case 1 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus1
                Case 2 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus2
                Case 3 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus3
                Case 4 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus4
                Case 5 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus5
                Case 6 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus6
                Case 7 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus7
                Case 8 : blnRtn = gudt.SetChInfo.udtChannel(hintChIndex).DummyOutStatus8
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のバルブステータス情報作成
    ' 返り値    : ステータス情報
    ' 引き数    : ARG1 - (I ) チャンネルデータIndex
    ' 　　　    : ARG2 - (I ) 計測点個数のループIndex 
    ' 機能説明  : バルブステータスのAbnormal情報取得
    '--------------------------------------------------------------------
    Private Function mGetFuInfoStatusDoValDIDO(ByVal hintChIndex As Integer, _
                                               ByVal hintLoopIdx As Integer) As String

        Dim strRtn As String = ""

        Try

            Select Case (hintLoopIdx + 1)
                Case 1 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus1)
                Case 2 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus2)
                Case 3 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus3)
                Case 4 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus4)
                Case 5 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus5)
                Case 6 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus6)
                Case 7 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus7)
                Case 8 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveDiDoOutStatus8)
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    Private Function mGetFuInfoStatusDoValAIDO(ByVal hintChIndex As Integer, _
                                               ByVal hintLoopIdx As Integer) As String

        Dim strRtn As String = ""

        Try

            Select Case (hintLoopIdx + 1)
                Case 1 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus1)
                Case 2 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus2)
                Case 3 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus3)
                Case 4 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus4)
                Case 5 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus5)
                Case 6 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus6)
                Case 7 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus7)
                Case 8 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).ValveAiDoOutStatus8)
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のモーターステータス情報作成
    ' 返り値    : ステータス情報
    ' 引き数    : ARG1 - (I ) チャンネルデータIndex
    ' 　　　    : ARG2 - (I ) 計測点個数のループIndex
    ' 機能説明  : モーターステータスの情報取得
    '--------------------------------------------------------------------
    Private Function mGetFuInfoStatusDoMotor(ByVal hintChIndex As Integer, _
                                             ByVal hintLoopIdx As Integer) As String

        Dim strRtn As String = ""

        Try

            Select Case (hintLoopIdx + 1)
                Case 1 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus1)
                Case 2 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus2)
                Case 3 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus3)
                Case 4 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus4)
                Case 5 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus5)
                Case 6 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus6)
                Case 7 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus7)
                Case 8 : strRtn = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).MotorOutStatus8)
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のモーターステータス（Abnormal）情報作成
    ' 返り値    : ステータス情報
    ' 引き数    : ARG1 - (I ) 入出力処理判断フラグ（TRUE：In側の処理　FALSE：Out側の処理）
    ' 　　　    : ARG2 - (I ) ステータスコード 
    ' 　　　    : ARG3 - (I ) チャンネルデータIndex
    ' 　　　    : ARG4 - (I ) 計測点個数のループIndex
    ' 機能説明  : モーターステータスのAbnormal情報取得
    '--------------------------------------------------------------------
    Private Function mGetFuInfoStatusMotorAbnormal(ByVal blnInput As Boolean, _
                                                   ByVal hintStatus As Integer, _
                                                   ByVal hintChIndex As Integer, _
                                                   ByVal hintLoopCnt As Integer) As String

        Dim strRtn As String = ""

        Try

            Dim i As Integer
            Dim udtIniMotorStatus() As gTypCodeName = Nothing   ''iniファイルから情報取得
            Dim strwk() As String = Nothing                     ''モーターのステータス名称
            Dim strbp() As String = Nothing                     ''モーターのビット位置情報
            Dim wkMotorStatus(7) As String                      ''作業用配列

            ''作業用配列の初期化
            For i = 0 To UBound(wkMotorStatus)
                wkMotorStatus(i) = ""
            Next

            ''iniファイルからモーターのステータス情報取得
            If gGetComboCodeName(udtIniMotorStatus, gEnmComboType.ctChListChannelListStatusMotor) = 0 Then

                ''該当ステータス情報・Bit位置情報の取得
                For i = 0 To UBound(udtIniMotorStatus)

                    If udtIniMotorStatus(i).shtCode = hintStatus Then
                        strwk = Split(udtIniMotorStatus(i).strOption2, "_") 'ステータス名称
                        strbp = Split(udtIniMotorStatus(i).strOption4, "_") 'Bit位置
                        Exit For
                    End If

                Next

                If blnInput Then

                    '-------------------
                    ''DIスロットの場合
                    '-------------------
                    For i = 0 To UBound(strwk)
                        wkMotorStatus(i) = strwk(i)
                    Next

                Else

                    '-------------------
                    ''DOスロットの場合
                    '-------------------
                    ''出力ステータス情報の取得
                    With gudt.SetChInfo.udtChannel(hintChIndex)
                        wkMotorStatus(0) = gGetString(.MotorOutStatus1)
                        wkMotorStatus(1) = gGetString(.MotorOutStatus2)
                        wkMotorStatus(2) = gGetString(.MotorOutStatus3)
                        wkMotorStatus(3) = gGetString(.MotorOutStatus4)
                        wkMotorStatus(4) = gGetString(.MotorOutStatus5)
                        wkMotorStatus(5) = gGetString(.MotorOutStatus6)
                        wkMotorStatus(6) = gGetString(.MotorOutStatus7)
                        wkMotorStatus(7) = gGetString(.MotorOutStatus8)
                    End With

                    ''Bit情報通りに並び替え
                    If strbp(0) <> "" Then strwk(0) = wkMotorStatus(strbp(0))
                    If strbp(1) <> "" Then strwk(1) = wkMotorStatus(strbp(1))
                    If strbp(2) <> "" Then strwk(2) = wkMotorStatus(strbp(2))
                    If strbp(3) <> "" Then strwk(3) = wkMotorStatus(strbp(3))
                    If strbp(4) <> "" Then strwk(4) = wkMotorStatus(strbp(4))

                End If

                ''該当情報を戻す
                strRtn = strwk(hintLoopCnt)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のレンジ情報作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) チャンネルデータのIndex 
    ' 機能説明  : レンジの上下限設定
    '--------------------------------------------------------------------
    Private Sub mGetFuInfoRange(ByRef udtSet As gTypFuInfoPin, _
                                ByVal hintChIndex As Integer)

        Try

            Dim dblRangeHigh As Double      ''dbl 上限値
            Dim dblRangeLow As Double       ''dbl 下限値
            Dim strRangeHigh As String      ''文字長確認用
            Dim strRangeLow As String       ''文字長確認用
            Dim strDecimalFormat As String  ''フォーマットタイプ

            With udtSet

                ''仮設定フラグ
                .blnDmyScaleRange = gudt.SetChInfo.udtChannel(hintChIndex).DummyRangeScale

                .intDecimalPosition = gudt.SetChInfo.udtChannel(hintChIndex).AnalogDecimalPosition
                strDecimalFormat = "0.".PadRight(.intDecimalPosition + 2, "0"c)

                ''====================
                '' レンジ上限値
                ''====================
                'Ver2.0.7.B アナログの２、３線式の場合、小数点編集しない
                'Ver2.0.7.F アナログの２、３線式の場合(温度基板)、も小数点処理
                'dblRangeHigh = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeHigh / (10 ^ .intDecimalPosition)
                Dim blAna23 As Boolean = False
                If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                    If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then
                        '判定の結果2,3線式である。
                        blAna23 = True
                    End If
                End If

                If blAna23 = True Then
                    '2,3線式
                    'dblRangeHigh = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeHigh
                    dblRangeHigh = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeHigh / (10 ^ .intDecimalPosition)
                Else
                    '2,3線式　以外
                    dblRangeHigh = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeHigh / (10 ^ .intDecimalPosition)
                End If

                strRangeHigh = dblRangeHigh.ToString(strDecimalFormat)

                If .intChType = gCstCodeChTypePulse Then

                    .strRangeHigh = ""

                Else

                    ''最大桁数：9byte（例：123456789）
                    If strRangeHigh.Length > 8 Then
                        .strRangeHigh = strRangeHigh.Substring(0, 9)
                    Else
                        .strRangeHigh = strRangeHigh
                    End If

                End If

                ''====================
                '' レンジ下限値
                ''====================
                'Ver2.0.7.B アナログの２、３線式の場合、小数点編集しない
                'Ver2.0.7.F アナログの２、３線式の場合(温度基板)、も小数点処理
                'dblRangeLow = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeLow / (10 ^ .intDecimalPosition)
                If blAna23 = True Then
                    '2,3線式
                    'dblRangeLow = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeLow
                    dblRangeLow = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeLow / (10 ^ .intDecimalPosition)
                Else
                    '2,3線式　以外
                    dblRangeLow = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeLow / (10 ^ .intDecimalPosition)
                End If

                strRangeLow = dblRangeLow.ToString(strDecimalFormat)

                'Ver2.0.8.5 mmHgレンジ下限小数点対応
                Dim intDecMMHG As Integer = 0   'MMHG時の下限専用decpoint
                Dim strDecMMHG As String = ""   'MMHG時の下限ﾌｫｰﾏｯﾄ
                Dim dblTempMMHG As Double = 0   'MMHG時の下限を編集する際の一時領域
                If gudt.SetChInfo.udtChannel(hintChIndex).AnalogMmHgFlg = 1 Then
                    intDecMMHG = gudt.SetChInfo.udtChannel(hintChIndex).AnalogMmHgDec
                    strDecMMHG = "0.".PadRight(intDecMMHG + 2, "0"c)
                    dblTempMMHG = CDbl(dblRangeLow.ToString(strDecimalFormat))
                    strRangeLow = dblTempMMHG.ToString(strDecMMHG)
                End If
                '-

                If .intChType = gCstCodeChTypePulse Then

                    .strRangeLow = ""

                Else

                    ''最大桁数：9byte（例：-1.234567）
                    If strRangeLow.Length > 8 Then
                        .strRangeLow = strRangeLow.Substring(0, 9)
                    Else
                        .strRangeLow = strRangeLow
                    End If

                End If

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FU情報構造体のSlot情報作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) FU情報構造体
    ' 　　　    : ARG2 - (I ) FU番号 
    ' 　　　    : ARG3 - (I ) ポート番号
    ' 　　　    : ARG4 - (I ) ピン番号
    ' 機能説明  : CableMark1, CableMark2, Core1, Core2, Dist 設定
    '--------------------------------------------------------------------
    Private Sub mGetFuInfoSlotInfo(ByRef udtSet As gTypFuInfoPin, _
                                   ByVal hintFuNo As Integer, _
                                   ByVal hintPortno As Integer, _
                                   ByVal hintPin As Integer)

        Try

            Dim intKeisu As Integer     ''係数値（1線式～3線式の場合の行数）

            If hintFuNo = 8 And hintPortno = 5 Then
                Dim debugA As Integer = 0
            End If

            With udtSet

                Select Case gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType

                    '' 3線式のみ3行表示　ver1.4.0 2011.08.22
                    'Case gCstCodeFuSlotTypeAO, _
                    '     gCstCodeFuSlotTypeAI_2, _
                    '     gCstCodeFuSlotTypeAI_1_5, _
                    '     gCstCodeFuSlotTypeAI_K

                    '    intKeisu = 2

                    Case gCstCodeFuSlotTypeAI_3

                        intKeisu = 3

                    Case Else

                        intKeisu = 1

                End Select

                .strWireMark(0) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo((hintPin - 1) * intKeisu).strWireMark
                .strWireMarkClass(0) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo((hintPin - 1) * intKeisu).strWireMarkClass
                .strCoreNoIn(0) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo((hintPin - 1) * intKeisu).strCoreNoIn
                .strCoreNoCom(0) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo((hintPin - 1) * intKeisu).strCoreNoCom
                .strDist(0) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo((hintPin - 1) * intKeisu).strDest

                '' 3線式のみ3行表示　ver1.4.0 2011.08.22
                'If gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAO _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_2 _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_1_5 _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_K _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_4_20 _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_3 Then
                If gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_3 Then
                    .strWireMark(1) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 1).strWireMark
                    .strWireMarkClass(1) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 1).strWireMarkClass
                    .strCoreNoIn(1) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 1).strCoreNoIn
                    .strCoreNoCom(1) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 1).strCoreNoCom
                    .strDist(1) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 1).strDest

                    .strWireMark(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strWireMark
                    .strWireMarkClass(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strWireMarkClass
                    .strCoreNoIn(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strCoreNoIn
                    .strCoreNoCom(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strCoreNoCom
                    .strDist(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strDest
                End If

                'If gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_4_20 _
                'Or gudt.SetFu.udtFu(hintFuNo).udtSlotInfo(hintPortno - 1).shtType = gCstCodeFuSlotTypeAI_3 Then
                '    .strWireMark(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strWireMark
                '    .strWireMarkClass(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strWireMarkClass
                '    .strCoreNoIn(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strCoreNoIn
                '    .strCoreNoCom(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strCoreNoCom
                '    .strDist(2) = gudt.SetChDisp.udtChDisp(hintFuNo).udtSlotInfo(hintPortno - 1).udtPinInfo(((hintPin - 1) * intKeisu) + 2).strDest
                'End If

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#End Region

#Region "OPSグラフ設定インポート"

    '--------------------------------------------------------------------
    ' 機能      : OPS設定のインポート用構造体を作成する
    ' 返り値    : なし
    ' 引数      : ARG1 - (I ) パート選択情報
    '   　      : ARG2 - ( O) インポート情報構造体
    ' 機能説明  : 自動インポート用の構造体を作成する
    '--------------------------------------------------------------------
    Public Sub gMakeOpsDataStructure(ByVal hblnMachinery As Boolean, _
                                     ByRef udtGraphData As gTypImportGraphData)

        Try

            Dim i As Integer
            Dim intCountGraph As Integer = 0
            Dim intCountFree As Integer = 0
            Dim intCountFreeSub As Integer = 0

            ''配列初期化
            ReDim udtGraphData.udtOpsGraph(gCstCountGraphData - 1)
            ReDim udtGraphData.udtOpsFree(gCstCountGraphFree - 1)
            For i = 0 To UBound(udtGraphData.udtOpsFree)
                ReDim udtGraphData.udtOpsFree(i).udtFreeGraphTitle(gCstCountGraphData - 1)
            Next

            ''--------------------------------
            '' ３種グラフ設定
            ''--------------------------------
            For i = 0 To UBound(udtGraphData.udtOpsGraph)

                With udtGraphData.udtOpsGraph(i)

                    ''データ取得
                    If hblnMachinery Then

                        ''Machinery情報
                        .intNo = gudt.SetOpsGraphM.udtGraphTitleRec(i).bytNo
                        .intType = gudt.SetOpsGraphM.udtGraphTitleRec(i).bytType
                        .strGraphName = gGetString(gudt.SetOpsGraphM.udtGraphTitleRec(i).strName)

                        ''データ設定数のカウント
                        If .intType <> gCstCodeOpsTitleGraphTypeNothing Then
                            intCountGraph += 1
                        End If

                    Else

                        ''Cargo情報
                        .intNo = gudt.SetOpsGraphC.udtGraphTitleRec(i).bytNo
                        .intType = gudt.SetOpsGraphC.udtGraphTitleRec(i).bytType
                        .strGraphName = gGetString(gudt.SetOpsGraphC.udtGraphTitleRec(i).strName)

                        ''データ設定数のカウント
                        If .intType <> gCstCodeOpsTitleGraphTypeNothing Then
                            intCountGraph += 1
                        End If

                    End If

                End With

            Next

            ''データ設定数 設定
            udtGraphData.intUseCountGraph = intCountGraph
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            ' ''--------------------------------
            ' '' フリーグラフ
            ' ''--------------------------------

            'For i = 0 To UBound(udtGraphData.udtOpsFree)

            '    ''フリーグラフのタブ数分
            '    For j = 0 To UBound(udtGraphData.udtOpsFree(i).udtFreeGraphTitle)

            '        With udtGraphData.udtOpsFree(i).udtFreeGraphTitle(j)

            '            ''データ取得
            '            If hblnMachinery Then

            '                ''Machinery情報
            '                .intGraphNo = gudt.SetOpsFreeGraphM.udtFreeGraphRec(i).bytGraphNo
            '                .intOpsNo = gudt.SetOpsFreeGraphM.udtFreeGraphRec(i).bytOpsNo
            '                .strGraphTitle = gGetString(gudt.SetOpsFreeGraphM.udtFreeGraphRec(i).strGraphTitle)

            '                ''タブ毎のデータ設定数カウント
            '                If .strGraphTitle <> "" Then
            '                    intCountFreeSub += 1
            '                End If

            '            Else

            '                ''Cargo情報
            '                .intGraphNo = gudt.SetOpsFreeGraphC.udtFreeGraphRec(i).bytGraphNo
            '                .intOpsNo = gudt.SetOpsFreeGraphC.udtFreeGraphRec(i).bytOpsNo
            '                .strGraphTitle = gGetString(gudt.SetOpsFreeGraphC.udtFreeGraphRec(i).strGraphTitle)

            '                ''タブ毎のデータ設定数カウント
            '                If .strGraphTitle <> "" Then
            '                    intCountFreeSub += 1
            '                End If

            '            End If

            '        End With

            '    Next j

            '    ''フリーグラフ全体のデータ設定数カウント
            '    udtGraphData.udtOpsFree(i).intUseCount = intCountFreeSub
            '    If udtGraphData.udtOpsFree(i).intUseCount <> 0 Then
            '        intCountFree += 1
            '    End If

            '    intCountFreeSub = 0

            'Next i

            ' ''データ設定数 設定
            'udtGraphData.intUseCountFree = intCountFree

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル名称取得"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号からチャンネル名称を取得
    ' 返り値    : チャンネル名称
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネル番号からチャンネル名称を取得する
    '--------------------------------------------------------------------
    Public Function gGetChNoToChName(ByVal intChNo As Integer) As String

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            ''名称を返す
                            Return gGetString(.udtChCommon.strChitem)

                        End If

                    End If
                End With
            Next

            Return ""

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "チャンネル小数点位置取得"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号から小数点位置を取得
    ' 返り値    : 小数点位置
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネル番号から小数点位置を取得する
    ' 履歴      : 2011.07.22 作成
    '--------------------------------------------------------------------
    Public Function gGetChNoToDecimalPoint(ByVal intChNo As Integer) As UInt16

        Dim decimal_p As UInt16 = 0

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then           ''<アナログ> -------------------
                                decimal_p = .AnalogDecimalPosition

                            ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then        ''<バルブ> ---------------------
                                If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                    decimal_p = .ValveAiDoDecimalPosition
                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                    decimal_p = .ValveAiAoDecimalPosition
                                End If

                            ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then        ''<パルス> ---------------------
                                ''Data Typeから、パルスCH or 積算CH の判定をする
                                If .udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                                    decimal_p = .PulseDecPoint
                                Else
                                    decimal_p = .RevoDecPoint
                                End If
                            End If

                            ''小数点位置を返す
                            Return decimal_p

                        End If
                    End If
                End With
            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return 0
        End Try

    End Function

#End Region

#Region "データ種別コード取得"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号からデータ種別コードを取得
    ' 返り値    : データ種別コード
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネル番号からデータ種別コードを取得する
    '--------------------------------------------------------------------
    Public Function gGetChNoToDataTypeCode(ByVal intChNo As Integer) As Integer

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            ''データ種別コードを返す
                            Return .udtChCommon.shtData

                        End If

                    End If
                End With
            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return 0
        End Try

    End Function

#End Region

#Region "チャンネル番号存在確認"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号存在確認
    ' 返り値    : True:存在する、False:存在しない
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : チャンネル番号がチャンネル情報構造体に存在するか確認する
    '--------------------------------------------------------------------
    Public Function gExistChNo(ByVal intChNo As Integer) As Boolean

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            Return True

                        End If

                    End If
                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号存在確認
    ' 返り値    : True:存在する、False:存在しない
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 　　　    : ARG2 - ( O) チャンネルタイプ
    ' 　　　    : ARG3 - ( O) FU番号
    ' 　　　    : ARG4 - ( O) Port番号
    ' 　　　    : ARG5 - ( O) Pin番号
    ' 機能説明  : チャンネル番号がチャンネル情報構造体に存在するか確認する
    ' 　　　　  : 存在した場合は、チャンネルタイプとFUアドレスを返す
    '--------------------------------------------------------------------
    Public Function gExistChNo(ByVal intChNo As Integer, _
                               ByRef intChType As Integer, _
                               ByRef intDataType As Integer, _
                               ByRef intFuNo As Integer, _
                               ByRef intFuPort As Integer, _
                               ByRef intFuPin As Integer) As Boolean

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            intChType = .udtChCommon.shtChType
                            intDataType = .udtChCommon.shtData
                            intFuNo = .udtChCommon.shtFuno
                            intFuPort = .udtChCommon.shtPortno
                            intFuPin = .udtChCommon.shtPin

                            Return True

                        End If

                    End If
                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号存在確認 ID変換 
    ' 返り値    : True:存在する、False:存在しない
    ' 引き数    : ARG1 - ( I) チャンネル番号
    ' 　　　    : ARG2 - ( O) チャンネルタイプ
    ' 　　　    : ARG3 - ( O) チャンネルIDNo.
    ' 機能説明  : チャンネル番号がチャンネル情報構造体に存在するか確認する
    ' 　　　　  : 存在した場合は、チャンネルタイプとチャンネルIDを返す
    '
    ' ☆2012.10.26 K.Tanigawa
    '--------------------------------------------------------------------
    Public Function gExistChIDNo(ByVal intChNo As Integer, _
                               ByRef intChType As Integer, _
                               ByRef intChIDNo As Integer
                               ) As Boolean

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''同じチャンネル番号が見つかった場合
                        If .udtChCommon.shtChno = intChNo Then

                            intChType = .udtChCommon.shtChType
                            intChIDNo = .udtChCommon.shtChid

                            Return True

                        End If

                    End If
                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "NULL削除"

    '--------------------------------------------------------------------
    ' 機能      : 文字列取得
    ' 返り値    : 変換後文字列
    ' 引き数    : ARG1 - (I ) 変換元文字列
    '           : ARG2 - (I ) 前後スペース削除フラグ
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    '--------------------------------------------------------------------
    Public Function gGetString(ByVal strInput As String, _
                      Optional ByVal blnTrim As Boolean = True) As String

        Try

            Dim strRtn As String

            strRtn = strInput
            'strRtn = Replace(strRtn, vbNull, "")
            strRtn = Replace(strRtn, vbNullChar, "")

            If blnTrim Then
                strRtn = Trim(strRtn)
            End If

            'Ver2.0.1.2 0x00は""扱いとする
            If strRtn <> "" Then
                If Asc(strRtn) = 0 Then
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

#Region "運転積算CHチェック"

    '-------------------------------------------------------------------- 
    ' 機能      : 運転積算CHチェック
    ' 返り値    : True:運転積算CH, False:非運転積算CH
    ' 引き数    : ARG1 - (I ) チャンネル番号
    ' 機能説明  : 運転積算CHかチェックする
    '--------------------------------------------------------------------
    Public Function gChkRunHourCH(ByVal intChNo As Integer) As Boolean

        Try

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネル番号が同じ場合
                    If .shtChno = intChNo Then

                        ''パルス積算チャンネルの場合
                        If .shtChType = gCstCodeChTypePulse Then

                            ''データタイプが運転積算の場合
                            '' Ver1.11.8.3 2016.11.08 通信CH追加
                            '' Ver1.12.0.1 2017.01.13 通信CH追加
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

                                Return True

                            End If
                        End If
                    End If
                End With
            Next

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "エラー処理"

    '--------------------------------------------------------------------
    ' 機能      : エラー情報作成
    ' 返り値    : エラー情報構造体
    ' 引き数    : ARG1 - (I ) メソッド情報
    ' 　　　    : ARG2 - (I ) エラーメッセージ
    ' 機能説明  : エラー情報構造体を作成して返す
    '--------------------------------------------------------------------
    Public Function gMakeExceptionInfo(ByVal hMethod As System.Reflection.MethodBase, _
                                       ByVal strErrMsg As String) As gTypExceptionInfo

        Try

            Dim udtwk As gTypExceptionInfo

            With udtwk

                .strExeName = hMethod.ReflectedType.Namespace
                .strFileName = hMethod.DeclaringType.Name
                .strFuncName = hMethod.Name
                .strErrMsg = strErrMsg

            End With

            Return udtwk

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return Nothing
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ログファイル出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ログメッセージ
    ' 機能説明  : ログファイルを出力する
    '--------------------------------------------------------------------
    Public Sub gOutputErrorLog(ByVal udtErrorInfo As gTypExceptionInfo, _
                      Optional ByVal blnShowMessage As Boolean = True)

        Try

            Dim strBasePath As String = ""
            Dim strFullPath As String = ""
            Dim strFileNameHeader As String = ""

            ''ログファイル情報出力
            strFileNameHeader = "ErrorLog"
            strBasePath = gGetAppPath()

            ''出力パス作成
            strFullPath = System.IO.Path.Combine(strBasePath, strFileNameHeader & Now.ToString("yyyyMMdd") & ".log")

            Try

                ''ログ出力
                Dim intFileNum As Integer = FreeFile()
                Call FileOpen(intFileNum, strFullPath, OpenMode.Append)
                Call Print(intFileNum, gMakeErrMsg(udtErrorInfo) & vbNewLine)
                Call FileClose(intFileNum)

            Catch ex As Exception

            End Try

            ''メッセージ表示
            If blnShowMessage Then
                Call MessageBox.Show("System Error!!" & vbNewLine & vbNewLine & _
                                     "【FileName】" & udtErrorInfo.strFileName & vbNewLine & _
                                     "【FuncName】" & udtErrorInfo.strFuncName & vbNewLine & vbNewLine & _
                                     "【ErrorMsg】" & vbNewLine & udtErrorInfo.strErrMsg & vbNewLine, _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Call MessageBox.Show(udtErrorInfo.strErrMsg & vbNewLine & vbNewLine & _
                '                     "【FileName】" & udtErrorInfo.strFileName & vbNewLine & _
                '                     "【FuncName】" & udtErrorInfo.strFuncName & vbNewLine, _
                '                     "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ログ出力用エラーメッセージ作成
    ' 返り値    : ログ出力用エラーメッセージ
    ' 引き数    : ARG1 - (I ) フォーム名
    '           : ARG2 - (I ) モジュール名
    '           : ARG3 - (I ) エラー内容
    ' 機能説明  : ログ出力用のエラーメッセージを作成する
    '--------------------------------------------------------------------
    Public Function gMakeErrMsg(ByVal udtErrorInfo As gTypExceptionInfo) As String

        Try

            Dim strwk As String

            With udtErrorInfo

                'strwk = Format(Now, "yyyy/MM/dd HH:mm:ss") & " " & _
                '        "(ExecName=" & .strExeName & ")" & _
                '        "(FileName=" & .strFileName & ")" & _
                '        "(FuncName=" & .strFuncName & ")" & _
                '        "(ErrorMsg=" & .strErrMsg & ")"
                strwk = Format(Now, "yyyy/MM/dd HH:mm:ss") & " " & _
                        "(FileName=" & .strFileName & ")" & _
                        "(FuncName=" & .strFuncName & ")" & _
                        "(ErrorMsg=" & .strErrMsg & ")"

            End With

            Return strwk

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "CSVデータ取得"

    '--------------------------------------------------------------------
    ' 機能      : CSVデータ取得
    ' 返り値    : 0:成功、<>0:失敗
    ' 引き数    : ARG1 - (I ) ファイルフルパス
    ' 　　　    : ARG2 - (I ) 列数
    ' 　　　    : ARG3 - (I ) Xデータ
    ' 　　　    : ARG4 - (I ) Yデータ
    ' 機能説明  : CSVファイルからデータを取得する
    '--------------------------------------------------------------------
    Public Function gGetCsvData(ByVal strFullPath As String, _
                                ByVal intCnt As Integer, _
                                ByRef strCol1() As String, _
                                ByRef strCol2() As String) As Integer

        Dim intRow As Integer = 0
        Dim intCol As Integer = 0

        ''ファイルが存在しない場合
        If Not System.IO.File.Exists(strFullPath) Then
            Call MessageBox.Show("The file doesn't exist." & vbNewLine & vbNewLine & strFullPath, _
                                 "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Not 0
        End If

        Try

            ReDim strCol1(intCnt)
            ReDim strCol2(intCnt)

            Using parser As New TextFieldParser(strFullPath, System.Text.Encoding.GetEncoding("Shift_JIS"))

                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",") ' 区切り文字はコンマ

                While Not parser.EndOfData

                    Dim strData() As String = parser.ReadFields() ' 1行読み込み

                    intCol = 0
                    For Each strField As String In strData

                        Select Case intCol
                            Case 0 : strCol1(intRow) = IIf(IsNumeric(strField), strField, "")
                            Case 1 : strCol2(intRow) = IIf(IsNumeric(strField), strField, "")
                        End Select

                        intCol += 1

                    Next

                    intRow += 1

                    If intRow > intCnt + 1 Then
                        Call MessageBox.Show("The data size is different.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return Not 0
                    End If

                End While

            End Using

        Catch ex As Exception
            Call MessageBox.Show("Import error!!" & vbNewLine & vbNewLine & ex.Message, _
                                 "Import error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Not 0
        End Try

    End Function

#End Region

#Region "モーターステータス取得"

    '--------------------------------------------------------------------
    ' 機能説明  ： モーターチャンネルのステータス情報を獲得する
    ' 戻値      ： 0:成功、<>0:失敗
    ' 引数      ： ARG1 - (IO) モーターステータス格納領域
    '           ： ARG1 - (I ) 区分    （1:手動停止    2:異常）
    '--------------------------------------------------------------------
    Public Function GetStatusMotor(ByRef hMotorStatus() As String, _
                                   ByVal hintKubun As Integer) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing
            Dim strStatus1() As String = Nothing
            Dim strStatus2() As String = Nothing

            ''iniファイル名取得
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200219 hori
                strIniFileName = gCstIniFileNameComboChListJpn
            Else
                strIniFileName = gCstIniFileNameComboChList
            End If

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = "StatusMotor"

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)
                ReDim Preserve strStatus1(intCnt - 1)
                ReDim Preserve strStatus2(intCnt - 1)

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)

                If UBound(strwk) >= 2 Then
                    strStatus1(intCnt - 1) = strwk(2)
                    strStatus2(intCnt - 1) = strwk(3)
                Else
                    strStatus1(intCnt - 1) = ""
                    strStatus2(intCnt - 1) = ""
                End If

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''モーターステータス格納
            If hintKubun = 1 Then
                ReDim hMotorStatus(UBound(strStatus1))
                hMotorStatus = strStatus1
            Else
                ReDim hMotorStatus(UBound(strStatus2))
                hMotorStatus = strStatus2
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： モーターチャンネルのステータス情報を獲得する
    ' 戻値      ： 0:成功、<>0:失敗
    ' 引数      ： ARG1 - (IO) モーターステータス格納領域1
    '       　　： ARG2 - (IO) モーターステータス格納領域2
    '       　　： ARG3 - (I ) セクション名
    '       　　： ARG4 - (IO) ビット位置1
    '       　　： ARG5 - (IO) ビット位置2
    '----------------------------------------------------------------------------
    Public Function GetStatusMotor2(ByRef hMotorStatus1() As String, _
                                    ByRef hMotorStatus2() As String, _
                                    ByVal strSectionName As String, _
                           Optional ByRef hMotorBitPos1() As String = Nothing, _
                           Optional ByRef hMotorBitPos2() As String = Nothing) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing
            Dim strStatus1() As String = Nothing
            Dim strStatus2() As String = Nothing
            Dim strBitPos1() As String = Nothing
            Dim strBitPos2() As String = Nothing

            ''iniファイル名取得
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200219 hori
                strIniFileName = gCstIniFileNameComboChListJpn
            Else
                strIniFileName = gCstIniFileNameComboChList
            End If

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ' ''セクション名取得
            'strSectionName = "BitMotor"

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1

            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)
                ReDim Preserve strStatus1(intCnt - 1)
                ReDim Preserve strStatus2(intCnt - 1)
                ReDim Preserve strBitPos1(intCnt - 1)
                ReDim Preserve strBitPos2(intCnt - 1)

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)

                If UBound(strwk) >= 4 Then
                    strStatus1(intCnt - 1) = strwk(2)
                    strStatus2(intCnt - 1) = strwk(3)
                    strBitPos1(intCnt - 1) = strwk(4)
                    strBitPos2(intCnt - 1) = strwk(5)
                ElseIf UBound(strwk) >= 2 Then
                    strStatus1(intCnt - 1) = strwk(2)
                    strStatus2(intCnt - 1) = strwk(3)
                    strBitPos1(intCnt - 1) = ""
                    strBitPos2(intCnt - 1) = ""
                Else
                    strStatus1(intCnt - 1) = ""
                    strStatus2(intCnt - 1) = ""
                    strBitPos1(intCnt - 1) = ""
                    strBitPos2(intCnt - 1) = ""
                End If

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''モーターステータス格納
            ReDim hMotorStatus1(UBound(strStatus1))
            hMotorStatus1 = strStatus1

            ReDim hMotorStatus2(UBound(strStatus2))
            hMotorStatus2 = strStatus2

            ''モータービット位置保存
            ReDim hMotorBitPos1(UBound(strBitPos1))
            hMotorBitPos1 = strBitPos1

            ReDim hMotorBitPos2(UBound(strBitPos2))
            hMotorBitPos2 = strBitPos2

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定の通信CHデータ作成"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定の通信CHデータ作成
    ' 返り値    : 0:成功、<>0:失敗
    ' 引き数    : ARG1 - (I ) ポート番号のインデックス
    '           : ARG2 - (IO) SIOチャンネル構造体
    ' 機能説明  : CH設定情報からSIO設定の通信CHデータを作成する
    '--------------------------------------------------------------------
    Public Function gMakeSioTransmissionChData(ByVal intPortIndex As Integer, _
                                               ByRef udtSioCh As gTypSetChSioCh) As Integer

        Try

            Dim intSetCnt As Integer = 0

            ''既存データのクリア
            Call gInitSetChSioChDetail(udtSioCh)

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが設定されている場合
                    If .shtChno <> 0 Then

                        ''対象ポートのビットが立っている場合
                        If gBitCheck(.shtOutPort, intPortIndex) Then

                            ''情報セット
                            udtSioCh.udtSioChRec(intSetCnt).shtChNo = .shtChno

                            ''カウントアップ
                            intSetCnt += 1

                        End If

                    End If

                End With

            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function

    'Ver2.0.0.7
    '--------------------------------------------------------------------
    ' 機能      : SIO設定の通信CHデータ作成(計測点で設定されていないﾎﾟｰﾄは消さない版)
    ' 返り値    : 0:成功、<>0:失敗
    ' 引き数    : ARG1 - (I ) ポート番号のインデックス
    '           : ARG2 - (IO) SIOチャンネル構造体
    ' 機能説明  : CH設定情報からSIO設定の通信CHデータを作成する
    '--------------------------------------------------------------------
    Public Function gMakeSioTransmissionChData_MakeOnly(ByVal intPortIndex As Integer, _
                                               ByRef udtSioCh As gTypSetChSioCh) As Integer

        Try

            Dim intSetCnt As Integer = 0
            Dim blOK As Boolean = False

            '該当ポートが計測点に存在するかチェック
            blOK = False
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが設定されている場合
                    If .shtChno <> 0 Then

                        ''対象ポートのビットが立っている場合
                        If gBitCheck(.shtOutPort, intPortIndex) Then
                            blOK = True
                            Exit For
                        End If
                    End If
                End With
            Next i

            '計測点に存在しないなら、何もせずに処理抜け
            If blOK = False Then
                Return 1
            End If


            ''既存データのクリア
            Call gInitSetChSioChDetail(udtSioCh)

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが設定されている場合
                    If .shtChno <> 0 Then

                        ''対象ポートのビットが立っている場合
                        If gBitCheck(.shtOutPort, intPortIndex) Then

                            ''情報セット
                            udtSioCh.udtSioChRec(intSetCnt).shtChNo = .shtChno

                            ''カウントアップ
                            intSetCnt += 1

                        End If

                    End If

                End With

            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function
#End Region

#Region "GWS設定の送信CHデータ作成"       '' 2014.02.04

    '--------------------------------------------------------------------
    ' 機能      : GWS設定の送信CHデータ作成
    ' 返り値    : 0:成功、<>0:失敗
    ' 引き数    : ARG1 - (I ) ポート番号のインデックス
    '           : ARG2 - (IO) SIOチャンネル構造体
    ' 機能説明  : CH設定情報からSIO設定の通信CHデータを作成する
    '--------------------------------------------------------------------
    Public Function gMakeGwsTransmissionChData(ByVal intPortIndex As Integer, _
                                               ByRef udtGwsCh As gTypSetOpsGwsCh) As Integer

        Try

            Dim intSetCnt As Integer = 0

            ''既存データのクリア
            Call gInitSetOpsGwsChDetail(udtGwsCh.udtGwsFileRec(intPortIndex))

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが設定されている場合
                    If .shtChno <> 0 Then

                        ''対象ポートのビットが立っている場合
                        If gBitCheck(.shtGwsPort, intPortIndex) Then

                            ''情報セット
                            udtGwsCh.udtGwsFileRec(intPortIndex).udtGwsChRec(intSetCnt).shtChNo = .shtChno

                            ''カウントアップ
                            intSetCnt += 1

                        End If

                    End If

                End With

            Next

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function

#End Region

#Region "グリッドのコピー＆ペースト共通設定"

    '-------------------------------------------------------------------- 
    ' 機能      : グリッドのコピー＆ペースト共通設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト  
    ' 機能説明  : グリッドのコピー＆ペースト共通設定を行う
    '--------------------------------------------------------------------
    Public Sub gSetGridCopyAndPaste(ByRef grdGrid As Editor.clsDataGridViewPlus)

        With grdGrid

            .MultiSelect = True
            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

            If grdGrid.Name <> "grdCHList" Then
                .ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
            Else
                .ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            End If

        End With

    End Sub

#End Region

#Region "セルの種類（コンボボックス）判定"

    '--------------------------------------------------------------------
    ' 機能      : セルの種類（コンボボックス）判定
    ' 返り値    : True:コンボボックス、False:それ以外
    ' 引き数    : ARG1 - (I ) グリッドの列名称
    ' 機能説明  : コンボボックス列の場合はTrueを返す
    '--------------------------------------------------------------------
    Public Function gChkCellIsCmb(ByVal strColName As String) As Boolean

        Try

            ''列名称が空欄の場合はFalseを返して抜ける
            If strColName = "" Then Return False

            If strColName.Substring(0, 3) = "cmb" Then
                Return True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return False

    End Function

#End Region

#Region "コンポジット関連"

    '-------------------------------------------------------------------- 
    ' 機能      : コンポジット関連コントロール初期化
    ' 返り値    : なし 
    ' 引き数    : ARG1 - (IO) bitグリッド  
    '           : ARG2 - (IO) anyグリッド
    '           : ARG3 - (IO) フィルタテキスト
    '           : ARG4 - (I ) 編集フラグ
    ' 機能説明  : コンポジット関連コントロールの初期化を行う
    '--------------------------------------------------------------------
    Public Sub gCompInitControl(ByRef grdBit As DataGridView, _
                                ByRef grdAny As DataGridView, _
                                ByRef txtFilter As TextBox, _
                                ByVal blnEdit As Boolean)

        Try

            Dim i As Integer
            Dim intStartIndex As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chkUse"
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chkRepose"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chkAlarm"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtExtG"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtDelay"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtGrep1"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtGrep2"
            Dim Column8 As New DataGridViewCheckBoxColumn : Column8.Name = "chkBit0"
            Dim Column9 As New DataGridViewCheckBoxColumn : Column9.Name = "chkBit1"
            Dim Column10 As New DataGridViewCheckBoxColumn : Column10.Name = "chkBit2"
            Dim Column11 As New DataGridViewCheckBoxColumn : Column11.Name = "chkBit3"
            Dim Column12 As New DataGridViewCheckBoxColumn : Column12.Name = "chkBit4"
            Dim Column13 As New DataGridViewCheckBoxColumn : Column13.Name = "chkBit5"
            Dim Column14 As New DataGridViewCheckBoxColumn : Column14.Name = "chkBit6"
            Dim Column15 As New DataGridViewCheckBoxColumn : Column15.Name = "chkBit7"
            Dim Column16 As New DataGridViewTextBoxColumn : Column16.Name = "txtStatus"

            Dim Column20 As New DataGridViewCheckBoxColumn : Column20.Name = "chkUse"
            Dim Column21 As New DataGridViewCheckBoxColumn : Column21.Name = "chkRepose"
            Dim Column22 As New DataGridViewCheckBoxColumn : Column22.Name = "chkAlarm"
            Dim Column23 As New DataGridViewTextBoxColumn : Column23.Name = "txtExtG"
            Dim Column24 As New DataGridViewTextBoxColumn : Column24.Name = "txtDelay"
            Dim Column25 As New DataGridViewTextBoxColumn : Column25.Name = "txtGrep1"
            Dim Column26 As New DataGridViewTextBoxColumn : Column26.Name = "txtGrep2"
            Dim Column27 As New DataGridViewTextBoxColumn : Column27.Name = "txtStatus"

            With grdBit

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(Column4)
                .Columns.Add(Column5) : .Columns.Add(Column6) : .Columns.Add(Column7) : .Columns.Add(Column8)
                .Columns.Add(Column9) : .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15) : .Columns.Add(Column16)

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "Repose" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "Alarm" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "EXT.G" : .Columns(3).Width = 50 : .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).HeaderText = "Delay" : .Columns(4).Width = 50 : .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).HeaderText = "G.REP1" : .Columns(5).Width = 50 : .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).HeaderText = "G.REP2" : .Columns(6).Width = 50 : .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).HeaderText = "0" : .Columns(7).Width = 30
                .Columns(8).HeaderText = "1" : .Columns(8).Width = 30
                .Columns(9).HeaderText = "2" : .Columns(9).Width = 30
                .Columns(10).HeaderText = "3" : .Columns(10).Width = 30
                .Columns(11).HeaderText = "4" : .Columns(11).Width = 30
                .Columns(12).HeaderText = "5" : .Columns(12).Width = 30
                .Columns(13).HeaderText = "6" : .Columns(13).Width = 30
                .Columns(14).HeaderText = "7" : .Columns(14).Width = 30
                .Columns(15).HeaderText = "Status Name" : .Columns(15).Width = 190
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
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

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdBit)

                ''編集不可の場合
                If Not blnEdit Then

                    intStartIndex = 0       ''編集不可列の開始番号を 0 に設定
                    .CurrentCell = Nothing  ''セルの選択を解除
                    .Enabled = False        ''グリッド使用不可

                Else

                    ''編集不可列の開始番号を 1 に設定
                    intStartIndex = 1

                End If

                ''USE 以外はロック
                For i = 0 To .Rows.Count - 1
                    For j = intStartIndex To .ColumnCount - 1
                        .Rows(i).Cells(j).ReadOnly = True
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
                Next

            End With

            With grdAny

                ''列
                .Columns.Clear()
                .Columns.Add(Column20) : .Columns.Add(Column21) : .Columns.Add(Column22) : .Columns.Add(Column23)
                .Columns.Add(Column24) : .Columns.Add(Column25) : .Columns.Add(Column26) : .Columns.Add(Column27)


                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Use" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "Repose" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "Alarm" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "EXT.G" : .Columns(3).Width = 50 : .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).HeaderText = "Delay" : .Columns(4).Width = 50 : .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).HeaderText = "G.REP1" : .Columns(5).Width = 50 : .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).HeaderText = "G.REP2" : .Columns(6).Width = 50 : .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).HeaderText = "Status Name" : .Columns(7).Width = 190
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 2
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 50

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None
                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdAny)

                ''編集不可の場合
                If Not blnEdit Then

                    intStartIndex = 0       ''編集不可列の開始番号を 0 に設定
                    .CurrentCell = Nothing  ''セルの選択を解除
                    .Enabled = False        ''グリッド使用不可

                Else

                    ''編集不可列の開始番号を 1 に設定
                    intStartIndex = 1

                End If

                ''USE 以外はロック
                For i = 0 To .Rows.Count - 1
                    For j = intStartIndex To .ColumnCount - 1
                        .Rows(i).Cells(j).ReadOnly = True
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
                Next

            End With

            If blnEdit Then

                ''Filterテキスト使用可
                txtFilter.ReadOnly = False
            Else

                ''Filterテキスト使用不可
                txtFilter.ReadOnly = True
                txtFilter.BackColor = gColorGridRowBackReadOnly
                txtFilter.TabStop = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '-------------------------------------------------------------------- 
    ' 機能      : コンポジットデータ表示
    ' 返り値    : なし 
    ' 引き数    : ARG1 - (I ) コンポジットテーブル構造体  
    '           : ARG2 - (IO) bitグリッド  
    '           : ARG3 - (IO) anyグリッド
    '           : ARG4 - (IO) フィルタテキスト
    ' 機能説明  : コンポジットデータの表示を行う
    '--------------------------------------------------------------------
    Public Sub gCompSetDisplay(ByVal udtCompositeRec As gTypSetChCompositeRec, _
                               ByRef grdBit As DataGridView, _
                               ByRef grdAny As DataGridView, _
                               ByRef txtFilter As TextBox)

        Dim intCur As Integer

        With udtCompositeRec

            ''DI Filter
            txtFilter.Text = gConvZeroToNull(.shtDiFilter)

            'T.ueki 
            'If txtFilter.Text = "" Then
            txtFilter.Text = "12"   '' フィルタ定数 "1" → "12" に変更    2014.04.17
            'End If

            ''Bit Status Map
            For i = 0 To UBound(udtCompositeRec.udtCompInf) - 1

                grdBit(0, i).Value = gBitCheck(udtCompositeRec.udtCompInf(i).bytAlarmUse, 0)
                grdBit(1, i).Value = gBitCheck(udtCompositeRec.udtCompInf(i).bytAlarmUse, 2)
                grdBit(2, i).Value = gBitCheck(udtCompositeRec.udtCompInf(i).bytAlarmUse, 1)

                grdBit(3, i).Value = IIf(udtCompositeRec.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", udtCompositeRec.udtCompInf(i).bytExtGroup)
                grdBit(4, i).Value = IIf(udtCompositeRec.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", udtCompositeRec.udtCompInf(i).bytDelay)
                grdBit(5, i).Value = IIf(udtCompositeRec.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", udtCompositeRec.udtCompInf(i).bytGRepose1)
                grdBit(6, i).Value = IIf(udtCompositeRec.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", udtCompositeRec.udtCompInf(i).bytGRepose2)

                grdBit(7, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 0), True, False)
                grdBit(8, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 1), True, False)
                grdBit(9, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 2), True, False)
                grdBit(10, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 3), True, False)
                grdBit(11, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 4), True, False)
                grdBit(12, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 5), True, False)
                grdBit(13, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 6), True, False)
                grdBit(14, i).Value = IIf(gBitCheck(udtCompositeRec.udtCompInf(i).bytBitPattern, 7), True, False)

                grdBit(15, i).Value = Trim(udtCompositeRec.udtCompInf(i).strStatusName)

            Next i

            ''カレントインデックス取得
            intCur = UBound(udtCompositeRec.udtCompInf)

            ''Any Map
            grdAny(0, 0).Value = gBitCheck(udtCompositeRec.udtCompInf(intCur).bytAlarmUse, 0)
            grdAny(1, 0).Value = gBitCheck(udtCompositeRec.udtCompInf(intCur).bytAlarmUse, 2)
            grdAny(2, 0).Value = gBitCheck(udtCompositeRec.udtCompInf(intCur).bytAlarmUse, 1)

            grdAny(3, 0).Value = IIf(udtCompositeRec.udtCompInf(intCur).bytExtGroup = gCstCodeChCompExtGroupNothing, "", udtCompositeRec.udtCompInf(intCur).bytExtGroup)
            grdAny(4, 0).Value = IIf(udtCompositeRec.udtCompInf(intCur).bytDelay = gCstCodeChCompDelayTimerNothing, "", udtCompositeRec.udtCompInf(intCur).bytDelay)
            grdAny(5, 0).Value = IIf(udtCompositeRec.udtCompInf(intCur).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", udtCompositeRec.udtCompInf(intCur).bytGRepose1)
            grdAny(6, 0).Value = IIf(udtCompositeRec.udtCompInf(intCur).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", udtCompositeRec.udtCompInf(intCur).bytGRepose2)

            grdAny(7, 0).Value = Trim(udtCompositeRec.udtCompInf(intCur).strStatusName)

        End With

    End Sub

    '-------------------------------------------------------------------- 
    ' 機能      : コンポジット仮設定表示
    ' 返り値    : なし 
    ' 引き数    : ARG1 - (I ) コンポジットテーブル構造体  
    '           : ARG2 - (IO) bitグリッド  
    '           : ARG3 - (IO) anyグリッド
    ' 機能説明  : コンポジットの仮設定の表示を行う
    '--------------------------------------------------------------------
    Public Sub gCompSetDummySetting(ByVal udtCompositeDetail As frmChListChannelList.mCompositeInfo, _
                                    ByRef grdBit As DataGridView, _
                                    ByRef grdAny As DataGridView)

        With udtCompositeDetail

            grdBit(3, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1ExtGr, 0, 3, grdBit)
            grdBit(4, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1Delay, 0, 4, grdBit)
            grdBit(5, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1GRep1, 0, 5, grdBit)
            grdBit(6, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1GRep2, 0, 6, grdBit)
            grdBit(15, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1StaNm, 0, 15, grdBit)

            grdBit(3, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2ExtGr, 1, 3, grdBit)
            grdBit(4, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2Delay, 1, 4, grdBit)
            grdBit(5, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2GRep1, 1, 5, grdBit)
            grdBit(6, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2GRep2, 1, 6, grdBit)
            grdBit(15, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2StaNm, 1, 15, grdBit)

            grdBit(3, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3ExtGr, 2, 3, grdBit)
            grdBit(4, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3Delay, 2, 4, grdBit)
            grdBit(5, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3GRep1, 2, 5, grdBit)
            grdBit(6, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3GRep2, 2, 6, grdBit)
            grdBit(15, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3StaNm, 2, 15, grdBit)

            grdBit(3, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4ExtGr, 3, 3, grdBit)
            grdBit(4, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4Delay, 3, 4, grdBit)
            grdBit(5, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4GRep1, 3, 5, grdBit)
            grdBit(6, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4GRep2, 3, 6, grdBit)
            grdBit(15, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4StaNm, 3, 15, grdBit)

            grdBit(3, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5ExtGr, 4, 3, grdBit)
            grdBit(4, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5Delay, 4, 4, grdBit)
            grdBit(5, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5GRep1, 4, 5, grdBit)
            grdBit(6, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5GRep2, 4, 6, grdBit)
            grdBit(15, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5StaNm, 4, 15, grdBit)

            grdBit(3, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6ExtGr, 5, 3, grdBit)
            grdBit(4, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6Delay, 5, 4, grdBit)
            grdBit(5, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6GRep1, 5, 5, grdBit)
            grdBit(6, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6GRep2, 5, 6, grdBit)
            grdBit(15, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6StaNm, 5, 15, grdBit)

            grdBit(3, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7ExtGr, 6, 3, grdBit)
            grdBit(4, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7Delay, 6, 4, grdBit)
            grdBit(5, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7GRep1, 6, 5, grdBit)
            grdBit(6, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7GRep2, 6, 6, grdBit)
            grdBit(15, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7StaNm, 6, 15, grdBit)

            grdBit(3, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8ExtGr, 7, 3, grdBit)
            grdBit(4, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8Delay, 7, 4, grdBit)
            grdBit(5, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8GRep1, 7, 5, grdBit)
            grdBit(6, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8GRep2, 7, 6, grdBit)
            grdBit(15, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8StaNm, 7, 15, grdBit)

            grdAny(3, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9ExtGr, 0, 3, grdAny)
            grdAny(4, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9Delay, 0, 4, grdAny)
            grdAny(5, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9GRep1, 0, 5, grdAny)
            grdAny(6, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9GRep2, 0, 6, grdAny)
            grdAny(7, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9StaNm, 0, 7, grdAny)

        End With

    End Sub

    Public Sub gCompSetDummySetting(ByVal udtValveDetail As frmChListChannelList.mValveInfo, _
                                    ByRef grdBit As DataGridView, _
                                    ByRef grdAny As DataGridView)

        With udtValveDetail

            grdBit(3, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1ExtGr, 0, 3, grdBit)
            grdBit(4, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1Delay, 0, 4, grdBit)
            grdBit(5, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1GRep1, 0, 5, grdBit)
            grdBit(6, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1GRep2, 0, 6, grdBit)
            grdBit(15, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus1StaNm, 0, 15, grdBit)

            grdBit(3, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2ExtGr, 1, 3, grdBit)
            grdBit(4, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2Delay, 1, 4, grdBit)
            grdBit(5, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2GRep1, 1, 5, grdBit)
            grdBit(6, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2GRep2, 1, 6, grdBit)
            grdBit(15, 1).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus2StaNm, 1, 15, grdBit)

            grdBit(3, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3ExtGr, 2, 3, grdBit)
            grdBit(4, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3Delay, 2, 4, grdBit)
            grdBit(5, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3GRep1, 2, 5, grdBit)
            grdBit(6, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3GRep2, 2, 6, grdBit)
            grdBit(15, 2).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus3StaNm, 2, 15, grdBit)

            grdBit(3, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4ExtGr, 3, 3, grdBit)
            grdBit(4, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4Delay, 3, 4, grdBit)
            grdBit(5, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4GRep1, 3, 5, grdBit)
            grdBit(6, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4GRep2, 3, 6, grdBit)
            grdBit(15, 3).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus4StaNm, 3, 15, grdBit)

            grdBit(3, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5ExtGr, 4, 3, grdBit)
            grdBit(4, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5Delay, 4, 4, grdBit)
            grdBit(5, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5GRep1, 4, 5, grdBit)
            grdBit(6, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5GRep2, 4, 6, grdBit)
            grdBit(15, 4).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus5StaNm, 4, 15, grdBit)

            grdBit(3, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6ExtGr, 5, 3, grdBit)
            grdBit(4, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6Delay, 5, 4, grdBit)
            grdBit(5, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6GRep1, 5, 5, grdBit)
            grdBit(6, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6GRep2, 5, 6, grdBit)
            grdBit(15, 5).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus6StaNm, 5, 15, grdBit)

            grdBit(3, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7ExtGr, 6, 3, grdBit)
            grdBit(4, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7Delay, 6, 4, grdBit)
            grdBit(5, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7GRep1, 6, 5, grdBit)
            grdBit(6, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7GRep2, 6, 6, grdBit)
            grdBit(15, 6).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus7StaNm, 6, 15, grdBit)

            grdBit(3, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8ExtGr, 7, 3, grdBit)
            grdBit(4, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8Delay, 7, 4, grdBit)
            grdBit(5, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8GRep1, 7, 5, grdBit)
            grdBit(6, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8GRep2, 7, 6, grdBit)
            grdBit(15, 7).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus8StaNm, 7, 15, grdBit)

            grdAny(3, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9ExtGr, 0, 3, grdAny)
            grdAny(4, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9Delay, 0, 4, grdAny)
            grdAny(5, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9GRep1, 0, 5, grdAny)
            grdAny(6, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9GRep2, 0, 6, grdAny)
            grdAny(7, 0).Style.BackColor = gDummyGetBackColorGrid(.DummyCmpStatus9StaNm, 0, 7, grdAny)

        End With

    End Sub

#End Region

#Region "フォーム終了待ち関数"

    '-------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから直接呼ばれる画面用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWaitChannel1(ByVal frmForm As Form)

        Try

            Dim blnOpenFlg As Boolean
            Dim intfrmFormLeft As Integer = frmMenuMain.Location.X
            Dim intfrmFormTop As Integer = frmMenuMain.Location.Y
            Dim intHeight As Integer = frmForm.Height

            'frmForm.Top = intfrmFormTop
            'frmForm.Left = intfrmFormLeft
            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = intfrmFormTop
            frmForm.Left = intfrmFormLeft
            frmForm.Height = intHeight + 21

            frmForm.Show()

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If


                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから呼ばれた画面が次画面を呼ぶ用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWaitChannel2(ByRef frmForm As Form, ByRef frmOwner As Form)

        Try

            Dim blnOpenFlg As Boolean
            Dim intfrmFormLeft As Integer = frmOwner.Location.X
            Dim intfrmFormTop As Integer = frmOwner.Location.Y

            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = intfrmFormTop
            frmForm.Left = intfrmFormLeft
            frmForm.Height += 25

            frmForm.Show()

            frmOwner.Enabled = False

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If

                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

            frmOwner.Enabled = True
            Call frmOwner.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '-------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから直接呼ばれる画面用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWait1(ByVal frmForm As Form)

        Try

            Dim blnOpenFlg As Boolean
            Dim intfrmFormLeft As Integer = frmMenuMain.Location.X
            Dim intfrmFormTop As Integer = frmMenuMain.Location.Y
            Dim intHeight As Integer = frmForm.Height

            'frmForm.Top = intfrmFormTop
            'frmForm.Left = intfrmFormLeft
            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = intfrmFormTop
            frmForm.Left = intfrmFormLeft
            frmForm.Height = intHeight + 21

            frmForm.Show()

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If


                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから呼ばれた画面が次画面を呼ぶ用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWait2(ByRef frmForm As Form, ByRef frmOwner As Form)

        Try

            Dim blnOpenFlg As Boolean
            Dim intfrmFormLeft As Integer = frmOwner.Location.X
            Dim intfrmFormTop As Integer = frmOwner.Location.Y

            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = intfrmFormTop
            frmForm.Left = intfrmFormLeft
            frmForm.Height += 25

            frmOwner.Enabled = False

            frmForm.Show()

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If

                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

            frmOwner.Enabled = True
            Call frmOwner.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから直接呼ばれる画面用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWait11(ByVal frmForm As Form)

        Try

            Dim blnOpenFlg As Boolean

            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = frmMenuMain.Top
            frmForm.Left = frmMenuMain.Left

            frmForm.Show()

            'mtrd100 = New System.Threading.Thread(AddressOf gthrWaitCloseForm)

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If


                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォーム終了待ち（メニューから呼ばれた画面が次画面を呼ぶ用）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) フォーム
    ' 機能説明  : 引数で渡されたフォームが終了するまで待機する
    '--------------------------------------------------------------------
    Public Sub gShowFormModelessForCloseWait22(ByRef frmForm As Form, ByRef frmOwner As Form)

        Try

            Dim blnOpenFlg As Boolean

            frmForm.StartPosition = FormStartPosition.Manual
            frmForm.Top = frmOwner.Top
            frmForm.Left = frmOwner.Left

            frmForm.Show()

            frmOwner.Enabled = False

            Do

                Try

                    blnOpenFlg = False
                    For Each f As Form In My.Application.OpenForms

                        If f.Name = frmForm.Name Then
                            blnOpenFlg = True
                        End If

                    Next

                    If Not blnOpenFlg Then
                        Exit Do
                    End If

                    Call Application.DoEvents()
                    Call System.Threading.Thread.Sleep(1)

                Catch ex As Exception
                    'Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                End Try

            Loop

            frmOwner.Enabled = True
            Call frmOwner.Focus()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネルリスト関連画面の参照モード設定"

    '-------------------------------------------------------------------- 
    ' 機能      : チャンネルリスト関連画面の参照モード設定 
    ' 返り値    : なし 
    ' 引き数    : ARG1 - (IO) フォーム  
    '           : ARG2 - (IO) セーブボタン
    ' 機能説明  : フォームのキャプションに参照モードの表示を行う  
    '--------------------------------------------------------------------
    Public Sub gSetChListDispOnly(ByRef frmForm As Form, _
                                  ByRef cmdSave As Button)

        ''参照モードの設定
        If gblnDispOtherForm Then
            frmForm.Text &= "(Display Only)"
            cmdSave.Enabled = False
        End If


    End Sub

#End Region

#Region "アナログレンジ設定値チェック"

    '-------------------------------------------------------------------------------------------------------------------
    ' 機能説明  ： アナログレンジの設定値チェック
    ' 戻値      ： TRUE:不整合あり　FALSE:不整合なし
    ' 引数      ： ARG 1 - (I ) 設定値（レンジHi）
    '           ：   |
    '           ： ARG 8 - (I ) 設定値（レンジLo）
    '           ： ARG 9 - (I ) レンジ設定 有無フラグ（レンジHi）
    '           ：   |
    '           ： ARG16 - (I ) レンジ設定 有無フラグ（レンジLo）
    '           ： ARG17 - ( O) エラーメッセージ 英語
    '           ： ARG18 - ( O) エラーメッセージ 日本語
    ' 備考①    ： ARG9～16 - (I ) bln***は、TRUE:設定あり FALSE:設定なし
    ' 備考②    ： レンジの大小関係は以下の通り。
    '           ： intRangeHi >= intHiHi >= intHi >= intNormalHi >= intNormalLo >= intLo >= intLoLo >= intRangeLo
    '-------------------------------------------------------------------------------------------------------------------
    Public Function gChkAnalogRangeSetValue(ByVal intRangeHi As Integer, ByVal intHiHi As Integer, ByVal intHi As Integer, ByVal intNormalHi As Integer, _
                                            ByVal intNormalLo As Integer, ByVal intLo As Integer, ByVal intLoLo As Integer, ByVal intRangeLo As Integer, _
                                            ByVal blnRangeHi As Boolean, ByVal blnHiHi As Boolean, ByVal blnHi As Boolean, ByVal blnNormalHi As Boolean, _
                                            ByVal blnNormalLo As Boolean, ByVal blnLo As Boolean, ByVal blnLoLo As Boolean, ByVal blnRangeLo As Boolean, _
                                            ByRef strMsgEng As String, ByRef strMsgJpn As String) As Boolean

        ''初期設定（不整合なし）
        Dim blnRtn As Boolean = False

        Try

            Dim intBitPos7 As Integer = 7
            Dim intBitPos6 As Integer = 6
            Dim intBitPos5 As Integer = 5
            Dim intBitPos4 As Integer = 4
            Dim intBitPos3 As Integer = 3
            Dim intBitPos2 As Integer = 2
            Dim intBitPos1 As Integer = 1
            Dim intBitPos0 As Integer = 0
            Dim strRangeSourceEng As String = ""
            Dim strRangeSourceJpn As String = ""
            Dim strRangeTargetEng As String = ""
            Dim strRangeTargetJpn As String = ""

            ''アナログレンジの設定状態を取得
            Dim intSetValueStatus As Integer = (2 ^ intBitPos7) * IIf(blnRangeHi, 1, 0) + _
                                               (2 ^ intBitPos6) * IIf(blnHiHi, 1, 0) + _
                                               (2 ^ intBitPos5) * IIf(blnHi, 1, 0) + _
                                               (2 ^ intBitPos4) * IIf(blnNormalHi, 1, 0) + _
                                               (2 ^ intBitPos3) * IIf(blnNormalLo, 1, 0) + _
                                               (2 ^ intBitPos2) * IIf(blnLo, 1, 0) + _
                                               (2 ^ intBitPos1) * IIf(blnLoLo, 1, 0) + _
                                               (2 ^ intBitPos0) * IIf(blnRangeLo, 1, 0)

            '------------------------------
            ''比較元の設定ビットチェック
            '------------------------------
            For i As Integer = intBitPos7 To intBitPos0 + 1 Step -1

                If gBitCheck(intSetValueStatus, i) Then

                    '------------------------------
                    ''比較先の設定ビットチェック
                    '------------------------------
                    For j As Integer = i - 1 To intBitPos0 Step -1

                        If gBitCheck(intSetValueStatus, j) Then

                            ''レンジチェック
                            If gChkAnalogRangeSetValueDetail(i, _
                                                             j, _
                                                             intRangeHi, intHiHi, intHi, intNormalHi, _
                                                             intNormalLo, intLo, intLoLo, intRangeLo) Then

                                ''「不整合あり」フラグ設定
                                blnRtn = True

                                ''レンジ名称取得
                                Call gChkAnalogRangeSetValueGetErrPointName(i, strRangeSourceEng, strRangeSourceJpn)  ''比較元
                                Call gChkAnalogRangeSetValueGetErrPointName(j, strRangeTargetEng, strRangeTargetJpn)  ''比較先

                                ''メッセージ作成
                                strMsgEng = " , Desc=[" & strRangeSourceEng & ">=" & strRangeTargetEng & "] incorrect."
                                strMsgJpn = " , 詳細=[" & strRangeSourceJpn & ">=" & strRangeTargetJpn & "] になっていません。"

                                Return blnRtn

                            End If

                            ''比較終了。次の比較処理へ
                            Exit For

                        End If

                    Next j

                End If

            Next i

            ''レンジ設定正常メッセージ
            strMsgEng = ""
            strMsgJpn = ""

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            blnRtn = True
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : アナログレンジの設定値チェック
    ' 返り値    : TRUE:不整合あり　FALSE:不整合なし
    ' 引き数    : ARG1 - (I ) ビットポジション（比較元）
    '           : ARG2 - (I ) ビットポジション（比較先）
    '           : ARG3 - (I ) レンジ設定 有無フラグ（レンジHi）
    '           :   |
    '           : ARG10 - (I ) レンジ設定 有無フラグ（レンジLo）
    ' 機能説明  : 設定値チェックの判定がNGの際、ポイント名称を返す
    '--------------------------------------------------------------------
    Private Function gChkAnalogRangeSetValueDetail(ByVal intBitPosSource As Integer, _
                                                   ByVal intBitPosTarget As Integer, _
                                                   ByVal intRangeHi As Integer, ByVal intHiHi As Integer, ByVal intHi As Integer, ByVal intNormalHi As Integer, _
                                                   ByVal intNormalLo As Integer, ByVal intLo As Integer, ByVal intLoLo As Integer, ByVal intRangeLo As Integer) As Boolean

        ''初期設定（不整合なし）
        Dim blnRtn As Boolean = False

        Try

            Dim intBitPos7 As Integer = 7
            Dim intBitPos6 As Integer = 6
            Dim intBitPos5 As Integer = 5
            Dim intBitPos4 As Integer = 4
            Dim intBitPos3 As Integer = 3
            Dim intBitPos2 As Integer = 2
            Dim intBitPos1 As Integer = 1
            Dim intBitPos0 As Integer = 0
            Dim intCompSource As Integer = 0
            Dim intCompTarget As Integer = 0

            ''比較元の値取得
            Select Case intBitPosSource
                Case intBitPos7 : intCompSource = intRangeHi
                Case intBitPos6 : intCompSource = intHiHi
                Case intBitPos5 : intCompSource = intHi
                Case intBitPos4 : intCompSource = intNormalHi
                Case intBitPos3 : intCompSource = intNormalLo
                Case intBitPos2 : intCompSource = intLo
                Case intBitPos1 : intCompSource = intLoLo
                Case intBitPos0 : intCompSource = intRangeLo
            End Select

            ''比較先の値取得
            Select Case intBitPosTarget
                Case intBitPos7 : intCompTarget = intRangeHi
                Case intBitPos6 : intCompTarget = intHiHi
                Case intBitPos5 : intCompTarget = intHi
                Case intBitPos4 : intCompTarget = intNormalHi
                Case intBitPos3 : intCompTarget = intNormalLo
                Case intBitPos2 : intCompTarget = intLo
                Case intBitPos1 : intCompTarget = intLoLo
                Case intBitPos0 : intCompTarget = intRangeLo
            End Select

            ''レンジチェック
            If intCompSource < intCompTarget Then
                blnRtn = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            blnRtn = True
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ポイント名称の取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ビットポジション
    '           : ARG2 - ( O) ポイント名称（英語表記）
    '           : ARG3 - ( O) ポイント名称（日本語表記）
    ' 機能説明  : 設定値チェックの判定がNGの際、ポイント名称を返す
    '--------------------------------------------------------------------
    Private Sub gChkAnalogRangeSetValueGetErrPointName(ByVal intBitPos As Integer, _
                                                       ByRef hstrRangeStringsEng As String, _
                                                       ByRef hstrRangeStringsJpn As String)

        Try

            Dim intBitPos7 As Integer = 7
            Dim intBitPos6 As Integer = 6
            Dim intBitPos5 As Integer = 5
            Dim intBitPos4 As Integer = 4
            Dim intBitPos3 As Integer = 3
            Dim intBitPos2 As Integer = 2
            Dim intBitPos1 As Integer = 1
            Dim intBitPos0 As Integer = 0

            Select Case intBitPos
                Case intBitPos7
                    hstrRangeStringsEng = "RangeHi"
                    hstrRangeStringsJpn = "レンジ上限"

                Case intBitPos6
                    hstrRangeStringsEng = "HiHi"
                    hstrRangeStringsJpn = "上上限"

                Case intBitPos5
                    hstrRangeStringsEng = "Hi"
                    hstrRangeStringsJpn = "上限"

                Case intBitPos4
                    hstrRangeStringsEng = "NormalHi"
                    hstrRangeStringsJpn = "ノーマルレンジ上限"

                Case intBitPos3
                    hstrRangeStringsEng = "NormalLo"
                    hstrRangeStringsJpn = "ノーマルレンジ下限"

                Case intBitPos2
                    hstrRangeStringsEng = "Lo"
                    hstrRangeStringsJpn = "下限"

                Case intBitPos1
                    hstrRangeStringsEng = "LoLo"
                    hstrRangeStringsJpn = "下下限"

                Case intBitPos0
                    hstrRangeStringsEng = "RangeLo"
                    hstrRangeStringsJpn = "レンジ下限"
            End Select


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "バージョン文字列取得"

    '-------------------------------------------------------------------- 
    ' 機能      : バージョン文字列取得
    ' 返り値    : バージョン文字列
    ' 引き数    : なし
    ' 機能説明  : 画面表示用のバージョン文字列を取得する
    '--------------------------------------------------------------------
    Public Function gGetVersionChar() As String

        Dim strRtn As String = ""
        '' Ver1.9.0 2015.12.15 追加
        Dim strAppPath As String
        Dim strPath As String

        Try
            '' Ver1.9.0 2015.12.15 VerはIniファイルより取得するように変更
            strAppPath = gGetAppPath()
            strPath = strAppPath & "\iniFile\" & gCstVerIniFile

            If System.IO.File.Exists(strPath) Then
                strRtn = GetIni("SYSTEM", "Ver", "", strPath)
            End If

            If strRtn = "" Then        '' Verが取得できない場合は従来通りに取得
                strRtn &= My.Application.Info.Version.Major
                strRtn &= "." & My.Application.Info.Version.Minor
                strRtn &= "." & My.Application.Info.Version.Build

                If My.Application.Info.Version.Revision <> 0 Then
                    strRtn &= "." & My.Application.Info.Version.Revision
                End If
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "内外取得"

    '-------------------------------------------------------------------- 
    ' 機能      : 内外文字列取得
    ' 返り値    : 内外文字列
    ' 引き数    : なし
    ' 機能説明  : 処理用の内外文字列を取得する
    '--------------------------------------------------------------------
    Public Function gGetNaiGaiChar() As Integer

        Dim intRtn As Integer = 0
        Dim strRtn As String = ""
        Dim strAppPath As String
        Dim strPath As String

        Try
            strAppPath = gGetAppPath()
            strPath = strAppPath & "\iniFile\" & gCstVerIniFile

            If System.IO.File.Exists(strPath) Then
                strRtn = GetIni("SYSTEM", "NaiGai", "", strPath)
            End If

            If strRtn = "" Then        '取得できない場合は0
                intRtn = 0
            Else
                If IsNumeric(strRtn) = True Then
                    intRtn = CInt(strRtn)
                Else
                    intRtn = 0
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

#End Region

#Region "DefaultEthernetLine取得"
    '-------------------------------------------------------------------- 
    ' 機能      : DefaultEthernetLine文字列取得
    ' 返り値    : DefaultEthernetLine文字列
    ' 引き数    : GWSType値
    ' 機能説明  : 処理用のDefaultEthernetLine文字列を取得する
    '--------------------------------------------------------------------
    Public Function gDefaultEthernetLineChar(pintGWStype As Integer) As Integer

        Dim intRtn As Integer = 0
        Dim strRtn As String = ""
        Dim strAppPath As String
        Dim strPath As String
        Dim strGWStype As String = ""


        Try
            strGWStype = "Item0" & pintGWStype.ToString

            strAppPath = gGetAppPath()
            strPath = strAppPath & "\iniFile\" & gCstIniFileNameComboSysGws

            If System.IO.File.Exists(strPath) Then
                strRtn = GetIni("DefaultEhernetLine", strGWStype, "", strPath)
            End If

            If strRtn = "" Then        '取得できない場合は0
                intRtn = 0
            Else
                If IsNumeric(strRtn) = True Then
                    intRtn = CInt(strRtn)
                Else
                    intRtn = 0
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function
#End Region

#Region "DriverDevice取得"
    '-------------------------------------------------------------------- 
    ' 機能      : DriverDevice文字列取得
    ' 返り値    : DriverDevice文字列
    ' 引き数    : PrinterName値
    ' 機能説明  : 処理用のDriverDevice文字列を取得する
    '--------------------------------------------------------------------
    Public Function gDriverDeviceChar(pintPrintName As Integer) As String
        Dim strRtn As String = ""
        Dim strAppPath As String
        Dim strPath As String
        Dim strIni As String = ""


        Try
            strIni = "Item" & pintPrintName.ToString("00")

            strAppPath = gGetAppPath()
            strPath = strAppPath & "\iniFile\" & gCstIniFileNameComboSysPrinter

            If System.IO.File.Exists(strPath) Then
                strRtn = GetIni("DriverDevice", strIni, "", strPath)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn
    End Function
#End Region


#Region "ディレクトリコピー"

    '-------------------------------------------------------------------- 
    ' 機能      : ディレクトリコピー
    ' 返り値    : 0:成功, <>0:失敗 
    ' 引き数    : ARG1 - (I ) コピーするディレクトリ
    '           : ARG2 - (I ) コピー先のディレクトリ 
    ' 機能説明  : ディレクトリをコピーする 
    '--------------------------------------------------------------------
    Public Function gCopyDirectory(ByVal strSourceDirName As String, _
                                   ByVal strDestDirName As String) As Integer

        Dim intRtn As Integer = 0

        Try

            ''コピー元ディレクトリにある全てのファイルのパスを取得
            Dim strSourceDirAllFiles() As String = IO.Directory.GetFiles(strSourceDirName, "*.*", IO.SearchOption.AllDirectories)

            ''コピー元ディレクトリにある全てのファイルの読み取り専用属性を解除
            For Each strFileName As String In strSourceDirAllFiles

                IO.File.SetAttributes(strFileName, IO.File.GetAttributes(strFileName) And (Not IO.FileAttributes.ReadOnly))

                If System.IO.Path.GetExtension(strFileName) = ".scc" Then
                    Call System.IO.File.Delete(strFileName)
                End If

            Next

            ''ディレクトリをまるごとコピー
            Call My.Computer.FileSystem.CopyDirectory(strSourceDirName, strDestDirName, True)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : ディレクトリコピー
    ' 返り値    : 0:成功, <>0:失敗 
    ' 引き数    : ARG1 - (I ) コピーするディレクトリ
    '           : ARG2 - (I ) コピー先のディレクトリ 
    '           : ARG3 - (I ) UIオプション 
    '           : ARG4 - (I ) UIキャンセルオプション
    ' 機能説明  : ディレクトリをコピーする 
    '--------------------------------------------------------------------
    Public Function gCopyDirectory(ByVal strSourceDirName As String, _
                                   ByVal strDestDirName As String, _
                                   ByVal udtUIOption As Microsoft.VisualBasic.FileIO.UIOption, _
                                   ByVal udtUICancelOption As Microsoft.VisualBasic.FileIO.UICancelOption) As Integer

        Dim intRtn As Integer = 0

        Try

            ''コピー元ディレクトリにある全てのファイルのパスを取得
            Dim strSourceDirAllFiles() As String = IO.Directory.GetFiles(strSourceDirName, "*.*", IO.SearchOption.AllDirectories)

            ''コピー元ディレクトリにある全てのファイルの読み取り専用属性を解除
            For Each strFileName As String In strSourceDirAllFiles

                IO.File.SetAttributes(strFileName, IO.File.GetAttributes(strFileName) And (Not IO.FileAttributes.ReadOnly))

                If System.IO.Path.GetExtension(strFileName) = ".scc" Then
                    Call System.IO.File.Delete(strFileName)
                End If

            Next

            ''ディレクトリをまるごとコピー
            Call My.Computer.FileSystem.CopyDirectory(strSourceDirName, strDestDirName, udtUIOption, udtUICancelOption)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : ディレクトリコピー
    ' 返り値    : 0:成功, <>0:失敗 
    ' 引き数    : ARG1 - (I ) コピーするディレクトリ
    '           : ARG2 - (I ) コピー先のディレクトリ 
    '           : ARG3 - (I ) 前バージョン番号
    '           : ARG4 - (I ) 現バージョン番号
    ' 機能説明  : ディレクトリをコピーする 
    '--------------------------------------------------------------------
    Public Function gCopyDirectory(ByVal strSourceDirName As String, _
                                   ByVal strDestDirName As String, _
                                   ByVal strVerPrev As String, _
                                   ByVal strVerNew As String) As Integer

        Dim intRtn As Integer = 0
        Dim strDestFile As String
        Dim strDestPath As String

        Try

            ''コピー元ディレクトリにある全てのファイルのパスを取得
            Dim strSourceDirAllFiles() As String = IO.Directory.GetFiles(strSourceDirName, "*.*", IO.SearchOption.AllDirectories)

            ''コピー元ディレクトリにある全てのファイルの読み取り専用属性を解除
            For Each strFileName As String In strSourceDirAllFiles

                IO.File.SetAttributes(strFileName, IO.File.GetAttributes(strFileName) And (Not IO.FileAttributes.ReadOnly))

                If System.IO.Path.GetExtension(strFileName) = ".scc" Then
                    Call System.IO.File.Delete(strFileName)
                End If

            Next

            'ファイル管理仕様変更 T.Ueki
            ''ディレクトリをまるごとコピー    2013.12.18
            Call My.Computer.FileSystem.CopyDirectory(strSourceDirName, strDestDirName, True)

            'コピー先ディレクトリにある全てのファイルのパスを取得
            Dim strDestDirAllFiles() As String = IO.Directory.GetFiles(strDestDirName, "*.*", IO.SearchOption.AllDirectories)

            ''コピー先ディレクトリにある全てのファイルのバージョン番号を変更
            For Each strFileName As String In strDestDirAllFiles
                'ファイル管理仕様変更 2013.12.18
                'IO.File.Move(strFileName, Replace(strFileName, "_" & strVerPrev.PadLeft(3, "0") & "_", "_" & strVerNew.PadLeft(3, "0") & "_"))

                'ファイル名処理変更 「****A」 → 「****A1」でエラーになるため  2014.09.19
                ''IO.File.Move(strFileName, Replace(strFileName, strVerPrev, strVerNew))
                strDestFile = Mid(strFileName, strDestDirName.Length + 1, (strFileName.Length - (strDestDirName.Length)))   'ファイル名取り出し
                strDestFile = Replace(strDestFile, strVerPrev, strVerNew)   ' ファイル名置換
                strDestPath = strDestDirName + strDestFile
                IO.File.Move(strFileName, strDestPath)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function

#End Region

#Region "チャンネル数取得"

    '-------------------------------------------------------------------- 
    ' 機能      : チャンネル数取得 
    ' 返り値    : チャンネル数
    ' 引き数    : なし
    ' 機能説明  : チャンネル数を取得する  
    '--------------------------------------------------------------------
    Public Function gGetSetChannelCount() As Integer

        Dim intCnt As Integer = 0

        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

            With gudt.SetChInfo.udtChannel(i)

                If .udtChCommon.shtChno <> 0 Then
                    intCnt += 1
                End If

            End With

        Next

        Return intCnt

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : チャンネルパケットサイズ取得 
    ' 返り値    : チャンネルパケットサイズ
    ' 引き数    : なし
    ' 機能説明  : チャンネルパケットサイズを算出する
    '--------------------------------------------------------------------
    Public Function gGetChConvPacketSize(ByRef udtSetChannel As gTypSetChInfo) As Integer

        Dim intRtn As Integer

        ' '' 2012/02/14 K.Tanigawa 修正 udtSetChannel引数を追加
        intRtn = ((gGetRecCntChannel(udtSetChannel) - 1) \ 88) + 1
        ''        intRtn = ((gGetSetChannelCount() - 1) \ 88) + 1  '' 2012/02/14 K.Tanigawa CH数からレコード数に修正

        Return intRtn

    End Function


#End Region

#Region "画面名称取得"

    '-------------------------------------------------------------------- 
    ' 機能      : 画面名称取得 
    ' 返り値    : 画面名称
    ' 引き数    : ARG1 - (I ) 画面番号
    '           : ARG2 - (I ) Machinery/Cargo区分
    ' 機能説明  : 画面番号から画面名称を取得する 
    '--------------------------------------------------------------------
    Public Function gGetScreenNoToScreenName(ByVal intScreenNo As Integer, _
                                             ByVal udtMC As gEnmMachineryCargo) As String

        Dim strRtn As String = ""

        Select Case udtMC
            Case gEnmMachineryCargo.mcMachinery

                For i As Integer = 0 To UBound(gudt.SetOpsScreenTitleM.udtOpsScreenTitle)

                    If gudt.SetOpsScreenTitleM.udtOpsScreenTitle(i).bytScreenNo = intScreenNo Then
                        strRtn = Trim(gudt.SetOpsScreenTitleM.udtOpsScreenTitle(i).strScreenName)
                        Exit For
                    End If

                Next

            Case gEnmMachineryCargo.mcCargo

                For i As Integer = 0 To UBound(gudt.SetOpsScreenTitleC.udtOpsScreenTitle)

                    If gudt.SetOpsScreenTitleC.udtOpsScreenTitle(i).bytScreenNo = intScreenNo Then
                        strRtn = Trim(gudt.SetOpsScreenTitleC.udtOpsScreenTitle(i).strScreenName)
                        Exit For
                    End If

                Next

        End Select

        Return strRtn

    End Function

#End Region

#Region "LenB メソッド"

    '--------------------------------------------------------------------
    ' 機能      : 入力バイト数獲得
    ' 返り値    : バイト数
    ' 引き数    : ARG1 - (I ) 対象文字列
    ' 機能説明  : 半角1バイト、全角2バイトとして、指定した文字列のバイト数を返す
    '--------------------------------------------------------------------
    Public Function LenB(ByVal stTarget As String) As Integer

        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)

    End Function
    Public Function MidB(ByVal stTarget As String, ByVal iStart As Integer, ByVal iByteSize As Integer) As String
        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

        Return hEncoding.GetString(btBytes, iStart, iByteSize)
    End Function
    Public Function MidB(ByVal stTarget As String, ByVal iStart As Integer) As String
        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

        Return hEncoding.GetString(btBytes, iStart, btBytes.Length - iStart)
    End Function

    Public Function PadB(ByRef targetData As String,
                               ByRef yose As String,
                               ByRef keta As Integer,
                               ByRef moji As String) As String
        If targetData Is Nothing Then
            targetData = ""
        End If

        Dim value As String = String.Empty

        'パディングする文字数を演算
        '（文字数　=　桁　-　(対象文字列のバイト数 - 対象文字列の文字列数)）
        Dim padLength As Integer = keta - (System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetData) - targetData.Length)

        If yose.Equals("RIGHT") Then
            '右寄せでパディング
            value = targetData.PadLeft(padLength, moji.ToCharArray()(0))
        ElseIf yose.Equals("LEFT") Then
            '左寄せでパディング
            value = targetData.PadRight(padLength, moji.ToCharArray()(0))
        End If
        Return value
    End Function

    Public Function LeftB(ByVal stTarget As String, ByVal iByteSize As Integer) As String
        Return MidB(stTarget, 0, iByteSize)
    End Function
    Public Function RightB(ByVal stTarget As String, ByVal iLength As Integer) As String
        If iLength <= stTarget.Length Then
            Return stTarget.Substring(stTarget.Length - iLength)
        End If

        Return stTarget
    End Function

#End Region

#Region "全角文字分割"

    '--------------------------------------------------------------------
    ' 機能      : 全角文字分割
    ' 返り値    : 分割文字
    ' 引き数    : ARG1 - (I ) 対象文字列
    ' 機能説明  : 半角1バイト、全角2バイトとして、指定した文字列のバイト数を返す
    '--------------------------------------------------------------------
    Public Function fStrCut(ByVal Mystring As String, ByVal nStart As Integer, ByVal nLen As Integer) As String

        '文字列を指定のバイト数にカットする関数(漢字分断回避）
        Dim sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim TempLen As Integer = sjis.GetByteCount(Mystring)

        If nLen < 1 Or Mystring.Length < 1 Then Return Mystring

        If TempLen <= nLen Then   '文字列が指定のバイト数未満の場合スペースを付加する
            Return Mystring.PadRight(nLen - (TempLen - Mystring.Length), CChar(" "))
        End If

        Dim tempByt() As Byte = sjis.GetBytes(Mystring)
        Dim strTemp As String = sjis.GetString(tempByt, nStart, nLen)

        '末尾が漢字分断されたら半角スペースと置き換え(VB2005="・" で.NET2003=NullChar になります）
        If strTemp.EndsWith(ControlChars.NullChar) Or strTemp.EndsWith("・") Then
            strTemp = sjis.GetString(tempByt, nStart, nLen - 1) & " "
        End If

        Return strTemp

    End Function

#End Region

#Region "コンポジットテーブル使用フラグ初期化"

    '-------------------------------------------------------------------- 
    ' 機能      : コンポジットテーブル使用フラグ初期化  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) コンポジット使用フラグ
    ' 機能説明  : コンポジットテーブル使用フラグを初期化する 
    '--------------------------------------------------------------------
    Public Sub gInitCompositeTableUse(ByRef blnCompositeTableUse() As Boolean)

        ''配列再定義
        Erase blnCompositeTableUse
        ReDim blnCompositeTableUse(UBound(gudt.SetChComposite.udtComposite))

        For i As Integer = 0 To UBound(gudt.SetChComposite.udtComposite)

            With gudt.SetChComposite.udtComposite(i)

                If .shtChid <> 0 Then
                    blnCompositeTableUse(i) = True
                Else
                    blnCompositeTableUse(i) = False
                End If

            End With

        Next

    End Sub


#End Region

#Region "仮設定関連"

    '-------------------------------------------------------------------- 
    ' 機能      : 仮設定色設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コントロール
    '           : ARG2 - (I ) 仮設定フラグ
    ' 機能説明  : 仮設定色を設定する 
    '--------------------------------------------------------------------
    Public Sub gDummySetColor(ByRef txt As TextBox, _
                              ByVal blnFlg As Boolean)

        If txt.Enabled Then

            If blnFlg Then
                txt.BackColor = gCstDummySetColorDummy
            Else
                txt.BackColor = gCstDummySetColorNormal
            End If

        End If

    End Sub

    Public Sub gDummySetColor(ByRef cmb As ComboBox, _
                              ByVal blnFlg As Boolean)

        If cmb.Enabled Then

            If blnFlg Then
                cmb.BackColor = gCstDummySetColorDummy
            Else
                cmb.BackColor = gCstDummySetColorNormal
            End If

        End If

    End Sub

    '--------------------------------------------------------------------
    'コピー元がダミー設定中かどうか確認
    '--------------------------------------------------------------------
    Public Function cSetDummy(ByVal SetColor As System.Drawing.Color) As Boolean

        'コピー元がダミー設定中かどうか
        If SetColor = gCstDummySetColorDummy Then
            cSetDummy = True
        Else
            cSetDummy = False
        End If

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : 仮設定色変更
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コントロール
    ' 機能説明  : 仮設定色を変更する 
    '--------------------------------------------------------------------
    Public Sub gDummySetColorChange(ByRef txt As TextBox)

        If txt.Enabled Then

            If txt.BackColor = gCstDummySetColorDummy Then
                txt.BackColor = gCstDummySetColorNormal
            Else
                txt.BackColor = gCstDummySetColorDummy
            End If

        End If

    End Sub

    Public Sub gDummySetColorChange(ByRef cmb As ComboBox)

        If cmb.Enabled Then

            If cmb.BackColor = gCstDummySetColorDummy Then
                cmb.BackColor = gCstDummySetColorNormal
            Else
                cmb.BackColor = gCstDummySetColorDummy
            End If

        End If

    End Sub

    '-------------------------------------------------------------------- 
    ' 機能      : 仮設定色変更
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) セルオブジェクト
    '           : ARG2 - (I ) グリッドオブジェクト
    ' 機能説明  : 仮設定色を変更する
    '--------------------------------------------------------------------
    Public Sub gDummySetColorChangeGrid(ByRef grdCell As DataGridViewCell, _
                                        ByVal grdGrid As DataGridView)

        If grdCell.Style.BackColor = gCstDummySetColorDummy Then
            grdCell.Style.BackColor = gDummyGetBackColorGrid(False, grdCell.RowIndex, grdCell.ColumnIndex, grdGrid)
        Else

            If Not grdCell.Style.BackColor = gColorGridRowBackReadOnly Then
                grdCell.Style.BackColor = gCstDummySetColorDummy
            End If

        End If

    End Sub

    '-------------------------------------------------------------------- 
    ' 機能      : 仮設定確認
    ' 返り値    : True:仮設定, False:非仮設定
    ' 引き数    : ARG1 - (I ) コントロール
    ' 機能説明  : 仮設定かどうかを確認する
    '--------------------------------------------------------------------
    Public Function gDummyCheckControl(ByVal txt As TextBox) As Boolean

        If txt.BackColor = gCstDummySetColorDummy Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function gDummyCheckControl(ByVal cmb As ComboBox) As Boolean

        If cmb.BackColor = gCstDummySetColorDummy Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function gDummyCheckControl(ByVal grdCell As DataGridViewCell) As Boolean

        If grdCell.Style.BackColor = gCstDummySetColorDummy Then
            Return True
        Else
            Return False
        End If

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : グリッド背景色取得
    ' 返り値    : グリッド背景色
    ' 引き数    : ARG1 - (I ) 仮設定フラグ
    '           : ARG1 - (I ) 行番号
    '           : ARG1 - (I ) 列番号
    '           : ARG1 - (I ) グリッドオブジェクト
    ' 機能説明  : グリッドの背景色を取得する
    '--------------------------------------------------------------------
    Public Function gDummyGetBackColorGrid(ByVal blnFlg As Boolean, _
                                           ByVal intRow As Integer, _
                                           ByVal intCol As Integer, _
                                           ByVal grdGrid As DataGridView) As Color

        If blnFlg Then

            If grdGrid(intCol, intRow).Style.BackColor <> gColorGridRowBackReadOnly Then
                Return gCstDummySetColorDummy
            Else
                Return gColorGridRowBackReadOnly
            End If

        Else

            If grdGrid(intCol, intRow).Style.BackColor <> gColorGridRowBackReadOnly Then

                If intRow Mod 2 <> 0 Then

                    If intRow >= 0 And intRow <= 19 Then
                        Return gColorGridRowBack
                    ElseIf intRow >= 20 And intRow <= 39 Then
                        Return Color.LavenderBlush
                    ElseIf intRow >= 40 And intRow <= 59 Then
                        Return Color.Lavender
                    ElseIf intRow >= 60 And intRow <= 79 Then
                        Return Color.Beige
                    ElseIf intRow >= 80 And intRow <= 99 Then
                        Return Color.Honeydew
                    End If

                Else
                    Return gColorGridRowBackBase
                End If

            Else
                Return gColorGridRowBackReadOnly
            End If

        End If

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : グリッド背景色変更
    ' 返り値    : グリッド背景色
    ' 引き数    : ARG1 - (I ) 仮設定フラグ
    '           : ARG1 - (I ) 行番号
    '           : ARG1 - (I ) 列番号
    '           : ARG1 - (I ) グリッドオブジェクト
    ' 機能説明  : グリッドの背景色を変更する
    '--------------------------------------------------------------------
    Public Function gDummyGetChangeBackColorGrid(ByVal clrNowColor As Color, _
                                                 ByVal intRow As Integer, _
                                                 ByVal intCol As Integer, _
                                                 ByVal grdGrid As DataGridView) As Color

        If clrNowColor = gCstDummySetColorDummy Then

            If grdGrid(intCol, intRow).ReadOnly Then
                Return gColorGridRowBackReadOnly
            Else

                If intRow Mod 2 <> 0 Then

                    If intRow >= 0 And intRow <= 19 Then
                        Return gColorGridRowBack
                    ElseIf intRow >= 20 And intRow <= 39 Then
                        Return Color.LavenderBlush
                    ElseIf intRow >= 40 And intRow <= 59 Then
                        Return Color.Lavender
                    ElseIf intRow >= 60 And intRow <= 79 Then
                        Return Color.Beige
                    ElseIf intRow >= 80 And intRow <= 99 Then
                        Return Color.Honeydew
                    End If

                Else
                    Return gColorGridRowBackBase
                End If

            End If

        Else

            If clrNowColor = gColorGridRowBackReadOnly Then
                Return gColorGridRowBackReadOnly
            Else
                Return gCstDummySetColorDummy
            End If

        End If

    End Function
    '戻す
    Public Function gDummyGetChangeBackColorGrid2(ByVal clrNowColor As Color, _
                                                 ByVal intRow As Integer, _
                                                 ByVal intCol As Integer, _
                                                 ByVal grdGrid As DataGridView) As Color

        If grdGrid(intCol, intRow).ReadOnly Then
            Return gColorGridRowBackReadOnly
        Else

            If intRow Mod 2 <> 0 Then

                If intRow >= 0 And intRow <= 19 Then
                    Return gColorGridRowBack
                ElseIf intRow >= 20 And intRow <= 39 Then
                    Return Color.LavenderBlush
                ElseIf intRow >= 40 And intRow <= 59 Then
                    Return Color.Lavender
                ElseIf intRow >= 60 And intRow <= 79 Then
                    Return Color.Beige
                ElseIf intRow >= 80 And intRow <= 99 Then
                    Return Color.Honeydew
                End If

            Else
                Return gColorGridRowBackBase
            End If
        End If

    End Function

#End Region

#Region "ファイル名変更"

    '-------------------------------------------------------------------- 
    ' 機能      : ファイル名変更  
    ' 返り値    : 0:成功, <>0:失敗 
    ' 引き数    : ARG1 - (I ) ベースパス
    '           : ARG2 - (I ) 旧ファイル名
    '           : ARG3 - (I ) 新ファイル名
    ' 機能説明  : 全ファイル名を変更する
    '--------------------------------------------------------------------
    Public Function gRenameAllFile(ByVal strBasePath As String, _
                                   ByVal strFileNameOld As String, _
                                   ByVal strFileNameNew As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim strwk As String
            Dim objFolder As New System.IO.DirectoryInfo(strBasePath)
            Dim objFiles() As System.IO.FileInfo

            ''フォルダ内に存在する全てのファイルを取得
            objFiles = objFolder.GetFiles("*", IO.SearchOption.AllDirectories)

            Try

                For i As Integer = 0 To UBound(objFiles)

                    ''変更後ファイル名作成
                    strwk = objFiles(i).FullName.Replace("\" & strFileNameOld & "_", "\" & strFileNameNew & "_")

                    ''ファイル名変更
                    Call System.IO.File.Move(objFiles(i).FullName, strwk)

                Next

            Catch ex As Exception

                intRtn = -1

            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "FUボード種別取得"

    '-------------------------------------------------------------------- 
    ' 機能      : FUボード種別取得  
    ' 返り値    : FUボード種別 
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    '           : ARG2 - (I ) 入出力区分
    '           : ARG3 - (I ) ワークCHフラグ
    ' 機能説明  : FUのボード種別を取得する
    '--------------------------------------------------------------------
    Public Function gGetFuBordType(ByVal udtChannel As gTypSetChRec, _
                                   ByVal udtIOType As gEnmIOType, _
                                   ByVal blnWorkFlg As Boolean) As String

        Try

            '▼▼▼ 20110308 ワークCHの場合は対応ボードなしを返す（対応ボードチェックで対応ボードなしはチェックを行わない）▼▼▼▼▼▼▼▼
            If blnWorkFlg Then Return gCstCodeFuSlotTypeNothing
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            Select Case udtChannel.udtChCommon.shtChType
                Case gCstCodeChTypeAnalog

                    '==================
                    ''アナログ
                    '==================
                    ''データ種別コードごとの対応ボードを返す
                    '' Ver1.11.1 2016.07.12 緯度・経度追加
                    Select Case udtChannel.udtChCommon.shtData
                        Case gCstCodeChDataTypeAnalogK : Return gCstCodeFuSlotTypeAI_K
                        Case gCstCodeChDataTypeAnalog2Pt : Return gCstCodeFuSlotTypeAI_2
                        Case gCstCodeChDataTypeAnalog2Jpt : Return gCstCodeFuSlotTypeAI_2
                        Case gCstCodeChDataTypeAnalog3Pt : Return gCstCodeFuSlotTypeAI_3
                        Case gCstCodeChDataTypeAnalog3Jpt : Return gCstCodeFuSlotTypeAI_3
                        Case gCstCodeChDataTypeAnalog1_5v : Return gCstCodeFuSlotTypeAI_1_5
                        Case gCstCodeChDataTypeAnalog4_20mA : Return gCstCodeFuSlotTypeAI_4_20
                        Case gCstCodeChDataTypeAnalogJacom : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogJacom55 : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogModbus : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogExhAve : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogExhRepose : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogExtDev : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogLatitude : Return gCstCodeFuSlotTypeNothing
                        Case gCstCodeChDataTypeAnalogLongitude : Return gCstCodeFuSlotTypeNothing
                    End Select

                Case gCstCodeChTypeDigital

                    '==================
                    ''デジタル
                    '==================
                    Select Case udtChannel.udtChCommon.shtData
                        Case gCstCodeChDataTypeDigitalNC, gCstCodeChDataTypeDigitalNC

                            ''通常のNC、NOはDIボード
                            Return gCstCodeFuSlotTypeDI

                        Case gCstCodeChDataTypeDigitalJacomNC, gCstCodeChDataTypeDigitalJacomNO, _
                             gCstCodeChDataTypeDigitalModbusNC, gCstCodeChDataTypeDigitalModbusNO, _
                             gCstCodeChDataTypeDigitalJacom55NC, gCstCodeChDataTypeDigitalJacom55NO

                            ''通常のJACOM、MODBUSは対応ボードなし
                            Return gCstCodeFuSlotTypeNothing

                        Case gCstCodeChDataTypeDigitalExt

                            ''通常の延長警報は 0x81以降は対応ボードなし（それ以外はDI）
                            If udtChannel.udtChCommon.shtEccFunc >= &H81 Then
                                Return gCstCodeFuSlotTypeNothing
                            Else
                                Return gCstCodeFuSlotTypeDI
                            End If

                        Case gCstCodeChDataTypeDigitalDeviceStatus

                            '==================
                            ''システム
                            '==================
                            ''システムは対応ボードなし
                            Return gCstCodeFuSlotTypeNothing

                    End Select

                Case gCstCodeChTypeMotor

                    '==================
                    ''モーター
                    '==================
                    ''JACOMは対応ボードなし、それ以外はInputがDI、OutputがDO
                    If (udtChannel.udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom) Or _
                       (udtChannel.udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55) Then
                        Return gCstCodeFuSlotTypeNothing
                    Else
                        Select Case udtIOType
                            Case gEnmIOType.ioInput : Return gCstCodeFuSlotTypeDI
                            Case gEnmIOType.ioOutput : Return gCstCodeFuSlotTypeDO
                        End Select
                    End If

                Case gCstCodeChTypeValve

                    '==================
                    ''バルブ
                    '==================
                    ''IO区分、データ種別コードごとの対応ボードを返す
                    Select Case udtIOType
                        Case gEnmIOType.ioInput

                            Select Case udtChannel.udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO : Return gCstCodeFuSlotTypeDI
                                Case gCstCodeChDataTypeValveAI_DO1 : Return gCstCodeFuSlotTypeAI_1_5
                                Case gCstCodeChDataTypeValveAI_DO2 : Return gCstCodeFuSlotTypeAI_4_20
                                Case gCstCodeChDataTypeValveAI_AO1 : Return gCstCodeFuSlotTypeAI_1_5
                                Case gCstCodeChDataTypeValveAI_AO2 : Return gCstCodeFuSlotTypeAI_4_20
                                Case gCstCodeChDataTypeValveAO_4_20 : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveDO : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveJacom : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveJacom55 : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveExt : Return gCstCodeFuSlotTypeDO
                            End Select

                        Case gEnmIOType.ioOutput

                            Select Case udtChannel.udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO : Return gCstCodeFuSlotTypeDO
                                Case gCstCodeChDataTypeValveAI_DO1 : Return gCstCodeFuSlotTypeDO
                                Case gCstCodeChDataTypeValveAI_DO2 : Return gCstCodeFuSlotTypeDO
                                Case gCstCodeChDataTypeValveAI_AO1 : Return gCstCodeFuSlotTypeAO
                                Case gCstCodeChDataTypeValveAI_AO2 : Return gCstCodeFuSlotTypeAO
                                Case gCstCodeChDataTypeValveAO_4_20 : Return gCstCodeFuSlotTypeAO
                                Case gCstCodeChDataTypeValveDO : Return gCstCodeFuSlotTypeDO
                                Case gCstCodeChDataTypeValveJacom : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveJacom55 : Return gCstCodeFuSlotTypeNothing
                                Case gCstCodeChDataTypeValveExt : Return gCstCodeFuSlotTypeDO
                            End Select

                    End Select

                Case gCstCodeChTypeComposite

                    '==================
                    ''コンポジット
                    '==================
                    ''コンポジットはDI
                    Return gCstCodeFuSlotTypeDI

                Case gCstCodeChTypePulse

                    '' Ver1.9.4 2016.02.02 ﾊﾟﾙｽ 通信の場合は対応ﾎﾞｰﾄﾞなし
                    '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                    '' Ver1.12.0.1 2017.01.13 運転積算種類追加
                    If udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Or _
                        udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Or udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                        udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevDayHour Or udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                        udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevLapHour Or udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevLapMin Then
                        Return gCstCodeFuSlotTypeNothing
                    End If

                    '==================
                    ''パルス
                    '==================
                    ''パルスはDI
                    Return gCstCodeFuSlotTypeDI

            End Select

            Return gCstCodeFuSlotTypeNothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return gCstCodeFuSlotTypeNothing
        End Try

    End Function

#End Region

#Region "コンバイン設定"

    '-------------------------------------------------------------------- 
    ' 機能      : コンバイン設定 
    ' 返り値    : True:コンバインあり, False:コンバインなし
    ' 引き数    : なし
    ' 機能説明  : コンバイン設定かどうかを確認する
    '--------------------------------------------------------------------
    Public Function gChkCombineSetting() As Boolean

        Try

            ''コンバイン設定がMachinery/Cargoの場合のみ、コンバインあり
            ''（FCU2台はコンバインなし）
            If gudt.SetSystem.udtSysSystem.shtCombineUse = gCstCodeSysCombineMC Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '-------------------------------------------------------------------- 
    ' 機能      : コンバインコントロール設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) Machineryボタン
    '           : ARG2 - ( O) Cargoボタン
    ' 機能説明  : コンバインのコントロールを設定する
    '--------------------------------------------------------------------
    Public Sub gSetCombineControl(ByRef optMachinery As RadioButton, _
                                  ByRef optCargo As RadioButton)

        Try

            ''コンバイン時はファイルを分けないように変更     2014.03.12
            ''コンバイン設定が有り
            'If gChkCombineSetting() Then
            '    optMachinery.Checked = True     ''マシナリー選択
            '    optMachinery.Enabled = True     ''マシナリーボタン有効
            '    optCargo.Enabled = True         ''カーゴボタン有効
            'Else
            optMachinery.Checked = True     ''マシナリー選択
            optMachinery.Enabled = False    ''マシナリーボタン無効
            optCargo.Enabled = False        ''カーゴボタン無効

            optMachinery.Visible = False    ''ボタン非表示    切替無し    2014.03.12
            optCargo.Visible = False        ''ボタン非表示    切替無し    2014.03.12
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Ver1.12.0.8
    '--------------------------------------------------------------------
    ' 機能      : ﾌﾟﾘﾝﾀコンバイン設定値判定
    ' 返り値    : ﾋﾞｯﾄ立てるならTrue,落とすならFalse
    ' 引き数    : なし
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Public Function fnSetCombinePrinter() As Boolean
        Try
            'SYSTEM のコンバイン設定が0ならfalse処理抜け
            If gudt.SetSystem.udtSysSystem.shtCombineUse = 0 Then
                Return False
            End If


            ''Ver2.0.7.H
            'Dim blLogPr1 As Boolean = False
            'Dim blLogPr2 As Boolean = False
            ''ログプリンタが1台のみの場合は、Trueを戻す
            'With gudt.SetSystem.udtSysPrinter
            '    'LogPrinter1
            '    If .udtPrinterDetail(0).bytPrinter <> 0 Then
            '        blLogPr1 = True
            '    End If
            '    'LogPrinter2
            '    If .udtPrinterDetail(1).bytPrinter <> 0 Then
            '        blLogPr2 = True
            '    End If
            '    If (blLogPr1 = True And blLogPr2 = False) Or _
            '       (blLogPr1 = False And blLogPr2 = True) Then
            '        Return True
            '    End If
            'End With
            ''-


            'OPS_Printer設定が全台ONか見る
            With gudt.SetSystem.udtSysOps
                Dim bAllON As Boolean = True
                For i As Integer = 0 To UBound(.udtOpsDetail) Step 1
                    With .udtOpsDetail(i)
                        If .shtExist = 1 Then
                            If gBitCheck(.shtPrintPart, 0) = True And gBitCheck(.shtPrintPart, 1) = True Then
                                'mac,cargo両方ONなら良い
                            Else
                                'mac,cargoどちらかでもOFFならフラグを落としﾙｰﾌﾟ抜け  
                                bAllON = False
                                Exit For
                            End If
                        End If
                    End With
                Next i

                '全台ONじゃないなら、false処理抜け
                If bAllON = False Then
                    Return False
                End If
            End With


            '条件を満たした場合Trueを戻す
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Function
#End Region

#Region "メニュー設定(デフォルト)"
    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Public Function gLoadMenuMain(ByRef udtManuMain As gTypSetOpsPulldownMenu, Optional ByVal pblVDU As Boolean = False) As Integer
        'Ver2.0.1.8 デフォルト読み込みで、ExtVDUの場合は、VDUを読み込む
        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strFullPath As String
            Dim strCurFileName As String

            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200218 hori
                If pblVDU = False Then
                    'OPS
                    strCurFileName = gCstFileOpsPulldownMenuInitJpn
                Else
                    'VDU
                    strCurFileName = gCstFileOpsPulldownMenuInitJpn_VDU
                End If

            Else
                If pblVDU = False Then
                    'OPS
                    strCurFileName = gCstFileOpsPulldownMenuInit
                Else
                    'VDU
                    strCurFileName = gCstFileOpsPulldownMenuInit_VDU
                End If
            End If

            ' ''メッセージ更新
            'lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(gGetDirNameIniFile, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call MessageBox.Show("Under '" & gGetDirNameIniFile() & "' Folder, There is no '" & strCurFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtManuMain)

                Catch ex As Exception
                    Call MessageBox.Show("Under '" & gGetDirNameIniFile() & "' Folder, There is no '" & strCurFileName & "' File.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

#Region "セレクションメニュー設定(デフォルト)"
    '--------------------------------------------------------------------
    ' 機能      : セレクションメニューテーブル設定読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Public Function gLoadSelectionMenuMain(ByRef udtSet As gTypSetOpsSelectionMenu) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strFullPath As String
            Dim strCurFileName As String = gCstFileOpsSelectionMenuInit

            ' ''メッセージ更新
            'lblMessage.Text = "Loading " & strCurFileName : Call lblMessage.Refresh()

            ''フルパス作成
            strFullPath = System.IO.Path.Combine(gGetDirNameIniFile, strCurFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then

                Call MessageBox.Show("Under '" & gGetDirNameIniFile() & "' Folder, There is no '" & strCurFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                intRtn = -1

            Else

                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSet)

                Catch ex As Exception
                    Call MessageBox.Show("Under '" & gGetDirNameIniFile() & "' Folder, There is no '" & strCurFileName & "' File.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

#Region "計測点リスト（デフォルト）"
    Public Function gLoadMPTdefault() As Integer
        Dim intRet As Integer = -1
        Try
            'iniFileﾌｫﾙﾀﾞにある、DEF_channel.55defが計測点リストﾃﾞﾌｫﾙﾄﾃﾞｰﾀ
            'DEF_channel.55defは、単にGR36に値が入ったcfgﾌｧｲﾙなだけ
            Dim strAppPath As String
            Dim strPath As String
            Dim intFileNo As Integer

            strAppPath = gGetAppPath()
            strPath = strAppPath & "\iniFile\DEF_channel.55def"

            'ファイル存在確認
            If Not System.IO.File.Exists(strPath) Then
                '該当ﾃﾞﾌｫﾙﾄﾌｧｲﾙが無いなら取り込まない
                intRet = -2
            Else
                'ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    'ファイル読込み
                    FileGet(intFileNo, gudt.SetChInfo)
                    FileClose(intFileNo)

                    intRet = 0
                Catch ex As Exception
                    intRet = -3
                End Try

            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRet
    End Function
#End Region


#Region "CH No順ソート"
    '--------------------------------------------------------------------
    ' 機能      : CH No順ソート
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (IO) ソート用構造体
    ' 機能説明  : CH No順にソート(チャンネル番号と配列番号)  2015.07.10
    '--------------------------------------------------------------------
    Public Function gMakeChNoOrderSort(ByRef aryCheck As ArrayList) As Integer

        Try

            Dim intRtn As Integer = 0

            With gudt.SetChInfo
                ''設定されているチャンネル番号のみチャンネル番号と配列番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    If .udtChannel(i).udtChCommon.shtChno <> 0 Then
                        aryCheck.Add(.udtChannel(i).udtChCommon.shtChno.ToString("0000") & "," & i)
                    End If
                Next
            End With

            ''チャンネル番号が存在する場合
            If Not aryCheck Is Nothing Then
                ''チャンネル番号順に並べ替え
                Call aryCheck.Sort()
            Else
                intRtn = -1
            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "CH No順ソート結果インデックス取得"
    '--------------------------------------------------------------------
    ' 機能      : CH No順ソート
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ソート用構造体
    ' 　　　    : ARG2 - (I ) ソートのインデックス
    ' 　　　    : ARG3 - ( O) CH No
    '           : ARG3 - ( O) リストのインデックス(配列番号)
    ' 機能説明  : CH No順のソート結果取得(チャンネル番号と配列番号)  2015.07.10
    '--------------------------------------------------------------------
    Public Function gGetChNoOrder(ByVal aryCheck As ArrayList, _
                                  ByVal sort_index As Integer, _
                                  ByRef chno As String, _
                                  ByRef list_index As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim strwk1() As String

            ''チャンネル番号とチャンネル配列番号を分割
            strwk1 = aryCheck(sort_index).ToString.Split(",")

            chno = strwk1(0)
            list_index = strwk1(1)

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "OUTPUT設定CHチェック"
    '--------------------------------------------------------------------
    ' 機能      : OUTPUT設定CHチェック
    ' 返り値    : True：設定あり、False：設定なし
    ' 引き数    : shtCHNo ： CHNo.
    '          ： FUNo   FU番号
    '          ： PortNo ｽﾛｯﾄ番号
    '          ： PinNo  端子番号
    ' 機能説明  : '' Ver1.12.0.1 2017.01.13 追加
    '--------------------------------------------------------------------

    Private Function ChkOutputCH(ByVal shtCHNo As Integer, ByRef FUNo As Byte, ByRef PortNo As Byte, ByRef PinNo As Byte) As Boolean

        For i = 0 To UBound(gudt.SetChOutput.udtCHOutPut)
            If gudt.SetChOutput.udtCHOutPut(i).shtChid = shtCHNo Then
                FUNo = gudt.SetChOutput.udtCHOutPut(i).bytFuno
                PortNo = gudt.SetChOutput.udtCHOutPut(i).bytPortno
                PinNo = gudt.SetChOutput.udtCHOutPut(i).bytPin

                Return True
            End If

        Next i

        Return False

    End Function

#End Region


#Region "CH変換ｸﾘｱ関数"
    ' CH変換削除
    Public Sub gsubCHconvDEL()
        Call ClearCHConvertSetting(gudt.SetChConvPrev)
        gudt.SetEditorUpdateInfo.udtSave.bytChConvPrev = 1
        Call ClearCHConvertSetting(gudt.SetChConvNow)
        gudt.SetEditorUpdateInfo.udtSave.bytChConvNow = 1

        gblnUpdateAll = True
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : CH 変換設定ｸﾘｱ
    '--------------------------------------------------------------------
    Private Sub ClearCHConvertSetting(ByRef ConvertData As gTypSetChConv)

        For i As Integer = LBound(ConvertData.udtChConv) To UBound(ConvertData.udtChConv)
            ConvertData.udtChConv(i).shtChid = 0
        Next
    End Sub
#End Region


#Region "端子表一括変換系関数"
    Public Sub gsubSetPTJPT()
        Dim i As Integer

        Dim blPTorJPT As Boolean = False

        Dim intFUno As Integer
        Dim intPortNo As Integer
        Dim intPinNo As Integer
        Dim blFUerr As Boolean

        'FCU設定からPT,JPTを取得
        If gudt.SetSystem.udtSysFcu.shtPtJptFlg <> 0 Then
            blPTorJPT = True
        End If

        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'CHNoが入っていること
                If .udtChannel(i).udtChCommon.shtChno <> 0 Then
                    'アナログCHであること
                    If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                        'ﾃﾞｰﾀﾀｲﾌﾟが2PT,2JPT,3PT,3JPTのいずれかであること
                        If .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt Or _
                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Jpt Or _
                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt Or _
                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Jpt Then

                            '該当CHのFUｱﾄﾞﾚｽ取得(Pinは不要だが取得しておく)
                            intFUno = .udtChannel(i).udtChCommon.shtFuno
                            intPortNo = .udtChannel(i).udtChCommon.shtPortno
                            intPinNo = .udtChannel(i).udtChCommon.shtPin

                            'FUｱﾄﾞﾚｽが範囲内であること
                            blFUerr = False
                            'FU=0-20,Port=1-8,Pinはﾁｪｯｸ不要
                            If intFUno < 0 Or intFUno > 20 Then
                                blFUerr = True
                            End If
                            If intPortNo < 1 Or intPortNo > 8 Then
                                blFUerr = True
                            End If
                            If blFUerr = False Then
                                '該当FUの基板によって処理分岐してChInfoのﾃﾞｰﾀﾀｲﾌﾟ書き換え
                                Select Case gudt.SetFu.udtFu(intFUno).udtSlotInfo(intPortNo - 1).shtType
                                    Case 4
                                        'M100A=2線式
                                        If blPTorJPT = True Then
                                            'JPT
                                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Jpt
                                        Else
                                            'PT
                                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt
                                        End If
                                    Case 5
                                        'M110A=3線式
                                        If blPTorJPT = True Then
                                            'JPT
                                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Jpt
                                        Else
                                            'PT
                                            .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt
                                        End If
                                    Case Else
                                        '該当外は何もしない
                                End Select
                            End If
                        End If
                    End If
                End If
            Next i
        End With


    End Sub
#End Region

#Region "DO端子選択値保存関数"
    '----------------------------------------------------------------------------
    '  DO端子選択値保存
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Public Function calcDoTypeSetValue() As UShort

        Dim shtSetValue As UShort

        Dim shtAtermSelectItem As Short
        Dim shtBtermSelectItem As Short
        Dim shtCtermSelectItem As Short
        Dim shtDtermSelectItem As Short

        shtAtermSelectItem = frmChTerminalDetail.cmbDoTerm_a.SelectedValue.ToString * 10
        shtBtermSelectItem = frmChTerminalDetail.cmbDoTerm_b.SelectedValue.ToString * 100
        shtCtermSelectItem = frmChTerminalDetail.cmbDoTerm_c.SelectedValue.ToString * 1000
        shtDtermSelectItem = frmChTerminalDetail.cmbDoTerm_d.SelectedValue.ToString * 10000

        shtSetValue = shtAtermSelectItem + shtBtermSelectItem + shtCtermSelectItem + shtDtermSelectItem

        Return shtSetValue
    End Function
#End Region

End Module
