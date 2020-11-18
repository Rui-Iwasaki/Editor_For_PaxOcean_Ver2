Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Friend Class clsStructure

#Region "変数定義"

    ''システム設定
    Friend SetSystem As gTypSetSystem

    ''FU設定
    Friend SetFu As gTypSetFu

    ''チャンネル情報データ（表示名設定データ）
    Friend SetChDisp As gTypSetChDisp
    Friend SetChDispPrev As gTypSetChDisp

    ''チャンネル情報
    Friend SetChInfo As gTypSetChInfo
    Friend SetChInfoPrev As gTypSetChInfo

    ''コンポジット設定
    Friend SetChComposite As gTypSetChComposite
    Friend SetChCompositePrev As gTypSetChComposite

    ''出力チャンネル設定
    Friend SetChOutput As gTypSetChOutput
    Friend SetChOutputPrev As gTypSetChOutput

    ''論理出力設定
    Friend SetChAndOr As gTypSetChAndOr
    Friend SetChAndOrPrev As gTypSetChAndOr

    ''グループ設定
    Friend SetChGroupSetM As gTypSetChGroupSet
    Friend SetChGroupSetC As gTypSetChGroupSet
    Friend SetChGroupSetMPrev As gTypSetChGroupSet
    Friend SetChGroupSetCPrev As gTypSetChGroupSet

    ''リポーズ入力設定
    Friend SetChGroupRepose As gTypSetChGroupRepose
    Friend SetChGroupReposePrev As gTypSetChGroupRepose

    ''積算データ設定ファイル
    Friend SetChRunHour As gTypSetChRunHour
    Friend SetChRunHourPrev As gTypSetChRunHour

    ''排ガス演算処理設定
    Friend SetChExhGus As gTypSetChExhGus
    Friend SetChExhGusPrev As gTypSetChExhGus

    ''コントロール使用可／不可設定
    Friend SetChCtrlUseM As gTypSetChCtrlUse
    Friend SetChCtrlUseC As gTypSetChCtrlUse

    ''SIO設定
    Friend SetChSio As gTypSetChSio

    ''SIO設定CH設定
    Friend SetChSioCh() As gTypSetChSioCh
    Friend SetChSioChPrev() As gTypSetChSioCh

    'Ver2.0.5.8
    '拡張SIO
    Friend SetChSioExt() As gTypSetChSioExt


    ''データ保存テーブル
    Friend SetChDataSave As gTypSetChDataSave
    Friend SetChDataSavePrev As gTypSetChDataSave

    ''データ転送テーブル設定
    Friend SetChDataForward As gTypSetChDataForward

    ''延長警報設定
    Friend SetExtAlarm As gTypSetExtAlarm

    ''タイマ設定
    Friend SetExtTimerSet As gTypSetExtTimerSet

    ''タイマ表示名称設定
    Friend SetExtTimerName As gTypSetExtTimerName

    ''シーケンスID
    Friend SetSeqID As gTypSetSeqID

    ''シーケンス設定
    Friend SetSeqSet As gTypSetSeqSet
    Friend SetSeqSetPrev As gTypSetSeqSet

    ''リニアライズテーブル
    Friend SetSeqLinear As gTypSetSeqLinear

    ''演算式テーブル
    Friend SetSeqOpeExp As gTypSetSeqOperationExpression
    Friend SetSeqOpeExpPrev As gTypSetSeqOperationExpression

    ''OPS画面タイトル
    Friend SetOpsScreenTitleM As gTypSetOpsScreenTitle
    Friend SetOpsScreenTitleC As gTypSetOpsScreenTitle

    ''プルダウンメニュー
    Friend SetOpsPulldownMenuM As gTypSetOpsPulldownMenu
    Friend SetOpsPulldownMenuC As gTypSetOpsPulldownMenu

    ''セレクションメニュー
    Friend SetOpsSelectionMenuM As gTypSetOpsSelectionMenu
    Friend SetOpsSelectionMenuC As gTypSetOpsSelectionMenu

    ''OPSグラフ設定
    Friend SetOpsGraphM As gTypSetOpsGraph
    Friend SetOpsGraphC As gTypSetOpsGraph

    ''フリーグラフ    ' 2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
    Friend SetOpsFreeGraphM As gTypSetOpsFreeGraph
    Friend SetOpsFreeGraphC As gTypSetOpsFreeGraph

    ''ログフォーマット
    Friend SetOpsLogFormatM As gTypSetOpsLogFormat
    Friend SetOpsLogFormatC As gTypSetOpsLogFormat
    Friend SetOpsLogIdDataM As gTypSetOpsLogIdData      '' ☆ 2012/10/24 K.Tanigawa
    Friend SetOpsLogIdDataC As gTypSetOpsLogIdData      '' ☆ 2012/10/24 K.Tanigawa

    Friend SetOpsLogFormatMPrev As gTypSetOpsLogFormat
    Friend SetOpsLogFormatCPrev As gTypSetOpsLogFormat
    Friend SetOpsLogIdDataMPrev As gTypSetOpsLogIdData      '' ☆ 2012/10/24 K.Tanigawa
    Friend SetOpsLogIdDataCPrev As gTypSetOpsLogIdData      '' ☆ 2012/10/24 K.Tanigawa


    ''CH変換テーブル
    Friend SetChConvNow As gTypSetChConv    ''現Ver
    Friend SetChConvPrev As gTypSetChConv   ''前Ver

    ''デフォルトデータ作成用
    ''ログ印字設定
    Friend SetOtherLogTime As gTypSetOtherLogTime

    '' ﾛｸﾞ設定印字ｵﾌﾟｼｮﾝ設定
    Friend SetOpsLogOption As gTypLogOption      '' Ver1.9.3  2016.01.25 追加

    ''フリーディスプレイ
    Friend SetOpsFreeDisplayM As gTypSetOpsFreeDisplay
    Friend SetOpsFreeDisplayC As gTypSetOpsFreeDisplay

    ''トレンドグラフ
    Friend SetOpsTrendGraphM As gTypSetOpsTrendGraph
    Friend SetOpsTrendGraphC As gTypSetOpsTrendGraph

    Friend SetOpsTrendGraphPID As gTypSetOpsTrendGraph
    Friend SetOpsTrendGraphPIDprev As gTypSetOpsTrendGraph


    '' GWS 管理     ''2014.05.29  T.Ueki
    Friend SetOpsGws As gTypSetSysGws
    Friend SetOpsGwsPrev As gTypSetSysGws

    '' GWS CH設定     ''2014.02.04
    Friend SetOpsGwsCh As gTypSetOpsGwsCh
    Friend SetOpsGwsChPrev As gTypSetOpsGwsCh

    ''ファイル更新情報
    Friend SetEditorUpdateInfo As gTypSetEditorUpdateInfo

#End Region

End Class
