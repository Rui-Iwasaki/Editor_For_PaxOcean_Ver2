Module modCommonConst

    'Ver2.0.6.5 PID用追加

#Region "列挙型定義"

#Region "ファイルモード"

    Public Enum gEnmFileMode
        fmNew
        fmEdit
        fmRename
    End Enum

#End Region

#Region "アクセスモード"

    Public Enum gEnmAccessMode
        amSave
        amLoad
    End Enum

#End Region

#Region "変換モード"

    Public Enum gEnmConvMode
        cmID_NO
        cmNO_ID
    End Enum

#End Region

#Region "コンボ種別"

    Public Enum gEnmComboType

        '========================
        ' システム設定画面
        '========================
        ''System Set
        ctSysSystemSystemClock
        ctSysSystemDataFormat
        ctSysSystemLanguage
        ctSysSystemManual
        ctSysSystemCombine
        ctSysSystemEthernetLine

        ctSysSystemGL_Spec  '2015/5/27 T.Ueki

        ''FCU Set
        ctSysFcuFcuCount
        ctSysFcuPartSet     '' Ver1.11.8.2 2016.11.01  FCU2台仕様 Cargo/Hull選択

        ''OPS Set
        ctSysOpsChSetup
        ctSysOpsChEdit
        ctSysOpsSystemAlarm
        ctSysOpsDutySetting
        ctSysOpsAlarmDisplay
        ctSysOpsResolution
        ctSysOpsAutoAlarmOrder  '2015/5/27 T.
        ctSysOpsLcdType     '' Ver1.11.8.2 2016.11.01  LCDType追加

        ''GWS Set   2014.02.03
        ctSysGws1Type
        ctSysGws2Type
        ctSysGwsFile
        ctChGwsDetailCH

        ''Printer Set
        ctSysPrinterLogPrinter1
        ctSysPrinterLogPrinter2
        ctSysPrinterAlarmPrinter1
        ctSysPrinterAlarmPrinter2
        ctSysPrinterPrinterType
        ctSysPrinterHcPrinter

        '========================
        ' チャンネル設定画面
        '========================
        ctChListChannelListChType
        ctChListChannelListSysNo
        ctChListChannelListSF
        ctChListChannelListUnit
        ctChListChannelListShareType
        ctChListChannelListTime
        ctChListChannelListOutputControlType
        ctChListChannelListAlmLevel     '' 2015.11.12  Ver1.7.8 ﾛｲﾄﾞ対応

        ctChListChannelListDataTypeAnalog
        ctChListChannelListDataTypeDigital
        ctChListChannelListDataTypeMotor
        ctChListChannelListDataTypeValve
        ctChListChannelListDataTypeComposite
        ctChListChannelListDataTypePulse
        ctChListChannelListDataTypeSystem

        'ctChListChannelListCompExp
        ctChListChannelListRangeType1
        ctChListChannelListRangeType2
        ctChListChannelListDeviceStatus

        ctChListChannelListStatusAnalog
        ctChListChannelListStatusDigital
        ctChListChannelListStatusMotor
        ctChListChannelListStatusValve
        ctChListChannelListStatusPulse

        ctChListChannelListOutStatusMotor

        ctChListAnalogExtDeviceJACOM22DI
        ctChListAnalogExtDeviceJACOM22AI
        ctChListAnalogExtDeviceJACOM22DO
        ctChListAnalogExtDeviceMODBUSDI
        ctChListAnalogExtDeviceMODBUSAI

        ctChTerminalListColumn3
        ctChTerminalListColumn
        ctChTerminalListTerminalNo

        ctChTerminalFunctionFuncName
        ctChTerminalFunctionFuncDI
        ctChTerminalFunctionFuncDO

        ctChOutputDoChOutType
        ctChOutputDoStatus

        ctChOutputDoTermType1
        ctChOutputDoTermType2

        ctChGroupReposeListColumn2

        ctChRunHourColumn3

        ctChExhGusGroupcmbNo

        ctChCtrlUseNotuseDetail
        ctChCtrlUseNotuseDetailgrd

        ctChDataForwardTableListColumn1
        'ctChDataForwardTableListColumn7

        ctChDataSaveTableListColumn3

        ctChSioDetailcmbPort
        ctChSioDetailcmbPriority
        ctChSioDetailcmbMC
        ctChSioDetailcmbCom
        ctChSioDetailcmbParityBit
        ctChSioDetailcmbDataBit
        ctChSioDetailcmbStopBit
        ctChSioDetailcmbDuplet
        ctChSioDetailcmbCommType1
        ctChSioDetailcmbCommType2
        ctChSioDetailcmbTransmisionCh

        '========================
        ' 延長警報設定画面 
        '========================
        ctExtPnlSystem

        ctExtPnlListWh
        ctExtPnlListPr
        ctExtPnlListCe
        ctExtPnlListEngineerCall
        ctExtPnlListNo
        ctExtPnlListDutyNo
        ctExtPnlListReAlm
        ctExtPnlListBzCut
        ctExtPnlListFree
        ctExtPnlListLedLcd
        ctExtPnlLedEngNo
        ctExtPnlLedDutyNo

        ctExtPnlLcdGroupGrpNo
        ctExtPnlLcdGroupMarkNo

        ctExtTimerPart       '' パート追加　ver.1.4.4 2012.05.08
        ctExtTimerType
        ctExtTimerTimeDisp

        'ctExtTermTestBz
        'ctExtTermTestLamp
        'ctExtTermTestFunc

        '========================
        ' SequenceSetシーケンス設定
        '========================
        ctSeqLineTableNo
        ctSeqOpeTableNo
        ctSeqOpeFixedType

        ctSeqSetDetailStatus
        ctSeqSetDetailDataType
        ctSeqSetDetailOutputType
        ctSeqSetDetailFucFu
        ctSeqSetDetailLogic

        '========================
        ' OPSSet
        '========================
        ctOpsScreenTitle
        ctOpsSelectionFunctionList
        ctOpsSelectionFunctionSet
        ctOpsSelectionSelectionDefault
        ctOpsSelectionSelectionDisableList
        ctOpsPulldownColumn1
        ctOpsPulldownColumn2
        ctOpsGraphListColumn2

        ctOpsGraphAnalogDispType

        ctOpsGraphExhaustLimit

        ctOpsGraphAnalogDetailChNameDispPoint
        ctOpsGraphAnalogDetailMarkNumValue
        ctOpsGraphAnalogDetailPointerFrame
        ctOpsGraphAnalogDetailPointerColor
        ctOpsGraphAnalogDetailSideColor

        ctOpsLogFormatListColumn1Type
        ctOpsLogFormatListColumn2Type
        ctOpsLogFormatListSaveStrings

        '========================
        ' Print
        '========================
        'ctPrtLocalCanbus

        '========================
        ' ﾒｲﾝﾒﾆｭｰ   Ver1.11.8.6 2016.11.10 追加
        '========================
        ctSysSystemTag
        ctSysSystemAlmLevel


        ctSysPrinterPrinterName 'Ver2.0.6.5
        ctChListChannelListDataTypePID
        ctExtPnlLedWatchLED     'Ver2.0.6.5 ■外販 延長警報パネル設定のみで使用
    End Enum

#End Region

#Region "コンパイルタイプ"

    Public Enum gEnmCompileType
        cpCompile
        cpErrorCheck
        cpMeasuringCheck    ''2019.03.12追加　Measuring_pointCheck用
    End Enum

#End Region

#Region "IOタイプ"

    Public Enum gEnmIOType
        ioInput
        ioOutput
    End Enum

#End Region

#Region "M/C区分"

    Public Enum gEnmMachineryCargo
        mcMachinery
        mcCargo
    End Enum

#End Region

#Region "コンポジットテーブル編集タイプ"

    Public Enum gEnmCompositeEditType
        cetNone
        cetValve
        cetComposite
    End Enum

#End Region

#End Region

#Region "定数定義"

    ''グリッドの偶数色の背景色
    Public gColorGridRowBack As Color = Color.AliceBlue

    ''グリッドの偶数色の背景色
    Public gColorGridRowBack2 As Color = Color.Cyan

    ''グリッドの基本色
    Public gColorGridRowBackBase As Color = Color.White

    ''グリッドの読込み専用セルの背景色
    Public gColorGridRowBackReadOnly As Color = Color.Gainsboro

    ''バージョンフォルダプレフィックス
    Public Const gCstVersionPrefix As String = "ver"

    ''保存フォルダ名
    Public Const gCstFolderNameSave As String = "Save"

    ''コンパイルフォルダ名
    Public Const gCstFolderNameCompile As String = "Compile"

    ''エディタ用情報保存フォルダ名
    Public Const gCstFolderNameEditorInfo As String = "EditorInfo"

    ''前Ver情報保存フォルダ名
    Public Const gCstFolderNameVerInfoPre As String = "VerInfoPrev"

    ''ファイル更新情報保存フォルダ
    Public Const gCstFolderNameUpdateInfo As String = "UpdateInfo"

    ''デフォルトデータ保存フォルダ名
    Public Const gCstFolderNameDefaultData As String = "DefaultData"

    '2015/5/8 T.Ueki
    ''Compile作成時の空フォルダ
    Public Const gCstFolderNameFUProg As String = "prog"

    '2015/5/8 T.Ueki
    ''Compile作成時の空フォルダ
    Public Const gCstFolderNameFUNew As String = "new"

    '2015/5/8 T.Ueki
    ''Compile作成時の空フォルダ
    Public Const gCstFolderNameFUSave As String = "save"

    '2015/5/8 T.Ueki
    ''Compile作成時のオリジナルフォルダ
    Public Const gCstFolderNameOrg As String = "org"

    '2015/5/8 T.Ueki
    ''Compile作成時のオリジナルフォルダ
    Public Const gCstFolderNameMimic As String = "mimic"

    '2015/6/10 T.Ueki
    ''Compile作成時のオリジナルフォルダ
    Public Const gCstFolderNameLog As String = "log"

    ''チャンネルグループの最大数
    Public Const gCstChannelGroupMax As Integer = 36

    ''１グループに登録可能なチャンネル数
    Public Const gCstOneGroupChannelMax As Integer = 100

    ''チャンネルIDの最大数
    Public Const gCstChannelIdMax As Integer = 3000

    ''１チャンネルのバイト数
    Public Const gCstByteCntChannelOne As Integer = 512

    ''チャンネル個別情報のバイト数
    Public Const gCstByteCntChannelType As Integer = 384

    ''ヘッダーのバイト数
    Public Const gCstByteCntHeader As Integer = 32

    ''FU情報
    Public Const gCstFcuType1 As String = "FCU1"
    Public Const gCstFcuType2 As String = "FCU2"

    Public Const gCstCountFuNo As Integer = 21
    Public Const gCstCountFuPort As Integer = 8
    Public Const gCstCountFuPin As Integer = 64

    ''リポーズ最大値 2019.03.19 追加
    Public Const gCstReposeSet As Integer = 72


    ''端子台印刷の１ページ最大端子数
    Public Const gCstPrtTerminalMaxRow As Integer = 32
    Public Const gCstPrtAnalogAoMaxRow As Integer = 8
    Public Const gCstPrtAnalogAiMaxRow As Integer = 16

    ''グラフ数
    Public Const gCstCountGraphData As Integer = 16
    Public Const gCstCountGraphFree As Integer = 10

    ''Input/Output区分（コンパイルで一時的に使用しているのみ）
    Public Const gCstIOTypeInput As String = "Input"
    Public Const gCstIOTypeOutput As String = "Output"

    ''未出力ファイルのメッセージを表示するか否か（Save、Compile共通）
    Public Const gCstOutputFileMsgDisplay As Boolean = False

    ''ﾛｸﾞ印字 MAX ﾍﾟｰｼﾞ数  Ver1.12.0.8 2017.02.22 追加
    Public Const gCstLogMaxPage As Integer = 10
#End Region

#Region "コード定義"

#Region "システム設定"

    ''システム設定－コンバイン設定
    Public Const gCstCodeSysCombineNone As Integer = 0
    Public Const gCstCodeSysCombineMC As Integer = 1
    Public Const gCstCodeSysCombineMH As Integer = 2

    ''FCU設定－FCU台数
    Public Const gCstCodeSysFcuCnt1 As Integer = 1
    Public Const gCstCodeSysFcuCnt2 As Integer = 2

#End Region

#Region "チャンネル設定"

#Region "共通"

    ''チャンネルタイプ
    Public Const gCstCodeChTypeNothing As Integer = 0
    Public Const gCstCodeChTypeAnalog As Integer = 1
    Public Const gCstCodeChTypeDigital As Integer = 2
    Public Const gCstCodeChTypeMotor As Integer = 3
    Public Const gCstCodeChTypeValve As Integer = 4
    Public Const gCstCodeChTypeComposite As Integer = 5
    Public Const gCstCodeChTypePulse As Integer = 6
    Public Const gCstCodeChTypePID As Integer = 7   'PID CH
    'Public Const gCstCodeChTypeSystem As Integer = 7

    Public Const gCstNameChTypeAnalog As String = "Analog"
    Public Const gCstNameChTypeDigital As String = "Digital"
    Public Const gCstNameChTypeMotor As String = "Motor"
    Public Const gCstNameChTypeValve As String = "Valve"
    Public Const gCstNameChTypeComposite As String = "Composite"
    Public Const gCstNameChTypePulse As String = "Pulse"
    Public Const gCstNameChTypePID As String = "PID"    'PID CH
    'Public Const gCstNameChTypeSystem As String = "System"

    Public Const gCstNameChTypeJpnAnalog As String = "アナログ"
    Public Const gCstNameChTypeJpnDigital As String = "デジタル"
    Public Const gCstNameChTypeJpnMotor As String = "モーター"
    Public Const gCstNameChTypeJpnValve As String = "バルブ"
    Public Const gCstNameChTypeJpnComposite As String = "コンポジット"
    Public Const gCstNameChTypeJpnPulse As String = "パルス"
    Public Const gCstNameChTypeJpnPID As String = "PID"
    'Public Const gCstNameChTypeJpnSystem As String = "システム"

    ''手動入力コード
    Public Const gCstCodeChManualInputStatus As Integer = 255
    Public Const gCstCodeChManualInputUnit As Integer = 255

    ''共通入力FU関連設定なし値
    Public Const gCstCodeChCommonFuNoNothing As Integer = &HFFFF
    Public Const gCstCodeChCommonPortNoNothing As Integer = &HFFFF
    Public Const gCstCodeChCommonPinNothing As Integer = &HFFFF

    ''共通警報関連設定なし値
    Public Const gCstCodeChCommonExtGroupNothing As Integer = &HFFFF
    Public Const gCstCodeChCommonDelayTimerNothing As Integer = &HFFFF
    Public Const gCstCodeChCommonGroupRepose1Nothing As Integer = &HFFFF
    Public Const gCstCodeChCommonGroupRepose2Nothing As Integer = &HFFFF

    ''フラグビット位置
    Public Const gCstCodeChCommonFlagBitPosDmy As Integer = 0
    Public Const gCstCodeChCommonFlagBitPosSc As Integer = 1
    Public Const gCstCodeChCommonFlagBitPosWk As Integer = 2
    Public Const gCstCodeChCommonFlagBitPosRL As Integer = 0
    Public Const gCstCodeChCommonFlagBitPosAC As Integer = 6
    Public Const gCstCodeChCommonFlagBitPosEP As Integer = 2
    Public Const gCstCodeChCommonFlagBitPosPrt1 As Integer = 4
    Public Const gCstCodeChCommonFlagBitPosPrt2 As Integer = 5

#End Region

#Region "アナログ"

    ''データ種別コード（アナログ）
    Public Const gCstCodeChDataTypeAnalogK As Integer = &H10
    Public Const gCstCodeChDataTypeAnalog2Pt As Integer = &H11
    Public Const gCstCodeChDataTypeAnalog2Jpt As Integer = &H12
    Public Const gCstCodeChDataTypeAnalog3Pt As Integer = &H13
    Public Const gCstCodeChDataTypeAnalog3Jpt As Integer = &H14
    Public Const gCstCodeChDataTypeAnalog1_5v As Integer = &H15
    Public Const gCstCodeChDataTypeAnalog4_20mA As Integer = &H16
    Public Const gCstCodeChDataTypeAnalogPT4_20mA As Integer = &H7A     ' ver1.4.0 2011.07.29
    Public Const gCstCodeChDataTypeAnalogJacom As Integer = &H20
    Public Const gCstCodeChDataTypeAnalogJacom55 As Integer = &H22
    Public Const gCstCodeChDataTypeAnalogModbus As Integer = &H21
    Public Const gCstCodeChDataTypeAnalogExhAve As Integer = &H30
    Public Const gCstCodeChDataTypeAnalogExhRepose As Integer = &H31
    Public Const gCstCodeChDataTypeAnalogExtDev As Integer = &H32
    Public Const gCstCodeChDataTypeAnalogLatitude As Integer = &H40     '' Ver1.10.6 2016.06.06  緯度
    Public Const gCstCodeChDataTypeAnalogLongitude As Integer = &H41     '' Ver1.10.6 2016.06.06  経度
    Public Const gCstCodeChDataTtpeAnalogUTCyear As Integer = &H42      'Ver2.0.1.2 UTC YEAR JMU有明5136向け
    Public Const gCstCodeChDataTtpeAnalogUTCmonth As Integer = &H43     'Ver2.0.1.2 UTC YEAR JMU有明5136向け
    Public Const gCstCodeChDataTtpeAnalogUTCday As Integer = &H44       'Ver2.0.1.2 UTC YEAR JMU有明5136向け
    Public Const gCstCodeChDataTtpeAnalogUTChour As Integer = &H45      'Ver2.0.1.2 UTC YEAR JMU有明5136向け
    Public Const gCstCodeChDataTtpeAnalogUTCmin As Integer = &H46       'Ver2.0.1.2 UTC YEAR JMU有明5136向け
    Public Const gCstCodeChDataTtpeAnalogUTCsec As Integer = &H47       'Ver2.0.1.2 UTC YEAR JMU有明5136向け



    ''ステータス種別（アナログ）
    Public Const gCstCodeChStatusAnalogNothing As Integer = &H40
    Public Const gCstCodeChStatusAnalogNorHigh As Integer = &H41
    Public Const gCstCodeChStatusAnalogNorLow As Integer = &H42
    Public Const gCstCodeChStatusAnalogLowNorHigh As Integer = &H43
    Public Const gCstCodeChStatusAnalogNorHiHi As Integer = &H44
    Public Const gCstCodeChStatusAnalogNorLoLo As Integer = &H45
    Public Const gCstCodeChStatusAnalogLoLoNorHiHi As Integer = &H46
    Public Const gCstCodeChStatusAnalogNorEHigh As Integer = &H47
    Public Const gCstCodeChStatusAnalogNorELow As Integer = &H48
    Public Const gCstCodeChStatusAnalogELowNorEHigh As Integer = &H49

    ''レンジ種別（アナログ）
    Public Const gCstCodeChRangeAnalogPt0_700 As Integer = &H600
    Public Const gCstCodeChRangeAnalogPt0_600 As Integer = &H500
    Public Const gCstCodeChRangeAnalogPt0_200 As Integer = &H400
    Public Const gCstCodeChRangeAnalogPt0_150 As Integer = &H300
    Public Const gCstCodeChRangeAnalogPt50_50 As Integer = &H200
    Public Const gCstCodeChRangeAnalogPt70_80 As Integer = &H100
    Public Const gCstCodeChRangeAnalogJpt0_200 As Integer = &H400
    Public Const gCstCodeChRangeAnalogJpt0_150 As Integer = &H300
    Public Const gCstCodeChRangeAnalogJpt50_50 As Integer = &H200
    Public Const gCstCodeChRangeAnalogJpt70_80 As Integer = &H100

    ''標準範囲設定なし値
    Public Const gCstCodeChAlalogNormalRangeNothingHi As Integer = &H80000000
    Public Const gCstCodeChAlalogNormalRangeNothingLo As Integer = &H80000000

    ''延長警報グループ設定なし値
    Public Const gCstCodeChAnalogExtGroupNothing As Integer = &HFF
    Public Const gCstCodeChAnalogDelayTimerNothing As Integer = &HFF
    Public Const gCstCodeChAnalogGroupRepose1Nothing As Integer = &HFF
    Public Const gCstCodeChAnalogGroupRepose2Nothing As Integer = &HFF

    ''入力信号　アナログ4-20mA
    Public Const gCstCodeChAnalogSignalNothing As Integer = 0
    Public Const gCstCodeChAnalogSignalAI As Integer = 1
    Public Const gCstCodeChAnalogSignalPT As Integer = 2

#End Region

#Region "デジタル"

    ''データ種別コード（デジタル）
    Public Const gCstCodeChDataTypeDigitalNC As Integer = &H10
    Public Const gCstCodeChDataTypeDigitalNO As Integer = &H11
    Public Const gCstCodeChDataTypeDigitalJacomNC As Integer = &H20
    Public Const gCstCodeChDataTypeDigitalJacomNO As Integer = &H21
    Public Const gCstCodeChDataTypeDigitalJacom55NC As Integer = &H22
    Public Const gCstCodeChDataTypeDigitalJacom55NO As Integer = &H23
    Public Const gCstCodeChDataTypeDigitalModbusNC As Integer = &H30
    Public Const gCstCodeChDataTypeDigitalModbusNO As Integer = &H31
    Public Const gCstCodeChDataTypeDigitalExt As Integer = &H40
    Public Const gCstCodeChDataTypeDigitalDeviceStatus As Integer = &H50

    ''ステータス種別（デジタル）
    Public Const gCstCodeChStatusDigitalNothing As Integer = &H0

#End Region

#Region "モーター"

    'Ver2.0.0.2 モーター種別増加
    ''データ種別コード（モーター）
    Public Const gCstCodeChDataTypeMotorManRun As Integer = &H10
    Public Const gCstCodeChDataTypeMotorManRunA As Integer = &H11
    Public Const gCstCodeChDataTypeMotorManRunB As Integer = &H12
    Public Const gCstCodeChDataTypeMotorManRunC As Integer = &H13
    Public Const gCstCodeChDataTypeMotorManRunD As Integer = &H14
    Public Const gCstCodeChDataTypeMotorManRunE As Integer = &H15
    Public Const gCstCodeChDataTypeMotorManRunF As Integer = &H16
    Public Const gCstCodeChDataTypeMotorManRunG As Integer = &H17
    Public Const gCstCodeChDataTypeMotorManRunH As Integer = &H18
    Public Const gCstCodeChDataTypeMotorManRunI As Integer = &H19
    Public Const gCstCodeChDataTypeMotorManRunJ As Integer = &H1A
    Public Const gCstCodeChDataTypeMotorManRunK As Integer = &H1B
    Public Const gCstCodeChDataTypeMotorAbnorRun As Integer = &H20
    Public Const gCstCodeChDataTypeMotorAbnorRunA As Integer = &H21
    Public Const gCstCodeChDataTypeMotorAbnorRunB As Integer = &H22
    Public Const gCstCodeChDataTypeMotorAbnorRunC As Integer = &H23
    Public Const gCstCodeChDataTypeMotorAbnorRunD As Integer = &H24
    Public Const gCstCodeChDataTypeMotorAbnorRunE As Integer = &H25
    Public Const gCstCodeChDataTypeMotorAbnorRunF As Integer = &H26
    Public Const gCstCodeChDataTypeMotorAbnorRunG As Integer = &H27
    Public Const gCstCodeChDataTypeMotorAbnorRunH As Integer = &H28
    Public Const gCstCodeChDataTypeMotorAbnorRunI As Integer = &H29
    Public Const gCstCodeChDataTypeMotorAbnorRunJ As Integer = &H2A
    Public Const gCstCodeChDataTypeMotorAbnorRunK As Integer = &H2B
    Public Const gCstCodeChDataTypeMotorDevice As Integer = &H30
    Public Const gCstCodeChDataTypeMotorDeviceJacom As Integer = &H40
    Public Const gCstCodeChDataTypeMotorDeviceJacom55 As Integer = &H41

    '通信
    Public Const gCstCodeChDataTypeMotorRManRun As Integer = &H50
    Public Const gCstCodeChDataTypeMotorRManRunA As Integer = &H51
    Public Const gCstCodeChDataTypeMotorRManRunB As Integer = &H52
    Public Const gCstCodeChDataTypeMotorRManRunC As Integer = &H53
    Public Const gCstCodeChDataTypeMotorRManRunD As Integer = &H54
    Public Const gCstCodeChDataTypeMotorRManRunE As Integer = &H55
    Public Const gCstCodeChDataTypeMotorRManRunF As Integer = &H56
    Public Const gCstCodeChDataTypeMotorRManRunG As Integer = &H57
    Public Const gCstCodeChDataTypeMotorRManRunH As Integer = &H58
    Public Const gCstCodeChDataTypeMotorRManRunI As Integer = &H59
    Public Const gCstCodeChDataTypeMotorRManRunJ As Integer = &H5A
    Public Const gCstCodeChDataTypeMotorRManRunK As Integer = &H5B
    '
    Public Const gCstCodeChDataTypeMotorRAbnorRun As Integer = &H60
    Public Const gCstCodeChDataTypeMotorRAbnorRunA As Integer = &H61
    Public Const gCstCodeChDataTypeMotorRAbnorRunB As Integer = &H62
    Public Const gCstCodeChDataTypeMotorRAbnorRunC As Integer = &H63
    Public Const gCstCodeChDataTypeMotorRAbnorRunD As Integer = &H64
    Public Const gCstCodeChDataTypeMotorRAbnorRunE As Integer = &H65
    Public Const gCstCodeChDataTypeMotorRAbnorRunF As Integer = &H66
    Public Const gCstCodeChDataTypeMotorRAbnorRunG As Integer = &H67
    Public Const gCstCodeChDataTypeMotorRAbnorRunH As Integer = &H68
    Public Const gCstCodeChDataTypeMotorRAbnorRunI As Integer = &H69
    Public Const gCstCodeChDataTypeMotorRAbnorRunJ As Integer = &H6A
    Public Const gCstCodeChDataTypeMotorRAbnorRunK As Integer = &H6B
    Public Const gCstCodeChDataTypeMotorRDevice As Integer = &H70

    ''ステータス種別コード
    Public Const gCstCodeChStatusTypeMotorRun As Integer = &H30
    Public Const gCstCodeChStatusTypeMotorRunA As Integer = &H31
    Public Const gCstCodeChStatusTypeMotorRunB As Integer = &H32
    Public Const gCstCodeChStatusTypeMotorRunC As Integer = &H33
    Public Const gCstCodeChStatusTypeMotorRunD As Integer = &H34
    Public Const gCstCodeChStatusTypeMotorRunE As Integer = &H35
    Public Const gCstCodeChStatusTypeMotorRunF As Integer = &H36
    Public Const gCstCodeChStatusTypeMotorRunG As Integer = &H37
    Public Const gCstCodeChStatusTypeMotorRunH As Integer = &H38
    Public Const gCstCodeChStatusTypeMotorRunI As Integer = &H39
    Public Const gCstCodeChStatusTypeMotorRunJ As Integer = &H3A
    Public Const gCstCodeChStatusTypeMotorRunK As Integer = &H3B
    Public Const gCstCodeChStatusTypeMotorValve As Integer = &H3C
    Public Const gCstCodeChStatusTypeMotorStbySL1 As Integer = &H3D
    Public Const gCstCodeChStatusTypeMotorStbySL2 As Integer = &H3E

    ''延長警報グループ設定なし値
    Public Const gCstCodeChMotorExtGroupNothing As Integer = &HFF
    Public Const gCstCodeChMotorDelayTimerNothing As Integer = &HFF
    Public Const gCstCodeChMotorGroupRepose1Nothing As Integer = &HFF
    Public Const gCstCodeChMotorGroupRepose2Nothing As Integer = &HFF

#End Region

#Region "バルブ"

    ''データ種別コード（バルブ）
    Public Const gCstCodeChDataTypeValveDI_DO As Integer = &H10
    Public Const gCstCodeChDataTypeValveAI_DO1 As Integer = &H11
    Public Const gCstCodeChDataTypeValveAI_DO2 As Integer = &H12
    Public Const gCstCodeChDataTypeValvePT_DO2 As Integer = &H76    ' ver1.4.0 2011.07.29
    Public Const gCstCodeChDataTypeValveAI_AO1 As Integer = &H13
    Public Const gCstCodeChDataTypeValveAI_AO2 As Integer = &H14
    Public Const gCstCodeChDataTypeValvePT_AO2 As Integer = &H78    ' ver1.4.0 2011.07.29

    Public Const gCstCodeChDataTypeValveAO_4_20 As Integer = &H20
    Public Const gCstCodeChDataTypeValveDO As Integer = &H21
    Public Const gCstCodeChDataTypeValveJacom As Integer = &H40
    Public Const gCstCodeChDataTypeValveExt As Integer = &H41
    Public Const gCstCodeChDataTypeValveJacom55 As Integer = &H42

    ''延長警報グループ設定なし値
    Public Const gCstCodeChValveExtGroupNothing As Integer = &HFF
    Public Const gCstCodeChValveDelayTimerNothing As Integer = &HFF
    Public Const gCstCodeChValveGroupRepose1Nothing As Integer = &HFF
    Public Const gCstCodeChValveGroupRepose2Nothing As Integer = &HFF

#End Region

#Region "コンポジット"

    ''データ種別コード（コンポジット）
    Public Const gCstCodeChDataTypeCompTankLevel As Integer = &H10
    Public Const gCstCodeChDataTypeCompTankLevelIndevi As Integer = &H11

    ''コンポジットテーブルインデックス設定なし
    Public Const gCstCodeChCompTblIdxNothing As Integer = &HFFFF

    ''延長警報グループ設定なし値
    Public Const gCstCodeChCompExtGroupNothing As Integer = &HFF
    Public Const gCstCodeChCompDelayTimerNothing As Integer = &HFF
    Public Const gCstCodeChCompGroupRepose1Nothing As Integer = &HFF
    Public Const gCstCodeChCompGroupRepose2Nothing As Integer = &HFF


#End Region

#Region "パルス"

    ''データ種別コード（パルス）
    Public Const gCstCodeChDataTypePulseTotal1_1 As Integer = &H10
    Public Const gCstCodeChDataTypePulseTotal1_10 As Integer = &H11
    Public Const gCstCodeChDataTypePulseTotal1_100 As Integer = &H12
    Public Const gCstCodeChDataTypePulseDay1_1 As Integer = &H13
    Public Const gCstCodeChDataTypePulseDay1_10 As Integer = &H14
    Public Const gCstCodeChDataTypePulseDay1_100 As Integer = &H15
    Public Const gCstCodeChDataTypePulseRevoTotalHour As Integer = &H20
    Public Const gCstCodeChDataTypePulseRevoTotalMin As Integer = &H21
    Public Const gCstCodeChDataTypePulseRevoDayHour As Integer = &H22
    Public Const gCstCodeChDataTypePulseRevoDayMin As Integer = &H23
    Public Const gCstCodeChDataTypePulseRevoLapHour As Integer = &H24
    Public Const gCstCodeChDataTypePulseRevoLapMin As Integer = &H25
    Public Const gCstCodeChDataTypePulseExtDev As Integer = &H30
    Public Const gCstCodeChDataTypePulseRevoExtDev As Integer = &H31        '' Ver1.11.8.3 2016.11.08 運転積算 通信ﾀｲﾌﾟ追加
    Public Const gCstCodeChDataTypePulseRevoExtDevTotalMin As Integer = &H32    '' Ver1.12.0.1 2017.01.13 
    Public Const gCstCodeChDataTypePulseRevoExtDevDayHour As Integer = &H33     '' Ver1.12.0.1 2017.01.13 
    Public Const gCstCodeChDataTypePulseRevoExtDevDayMin As Integer = &H34      '' Ver1.12.0.1 2017.01.13 
    Public Const gCstCodeChDataTypePulseRevoExtDevLapHour As Integer = &H35     '' Ver1.12.0.1 2017.01.13 
    Public Const gCstCodeChDataTypePulseRevoExtDevLapMin As Integer = &H36      '' Ver1.12.0.1 2017.01.13 

    ''パルス積算チャンネルの最大設定数
    Public Const gCstCntChPulseMax As Integer = 24

    ''運転積算CHの最大設定数
    Public Const gCstCntChPulseRevoMax As Integer = 256

#End Region

#Region "システム"

#End Region

#Region "PID"
    'データ種別(PID)
    Public Const gCstCodeChDataTypePID_1_AI1_5 As Integer = &H10    '16 AI1-5V
    Public Const gCstCodeChDataTypePID_2_AI4_20 As Integer = &H11   '17 AI4-20mA
    Public Const gCstCodeChDataTypePID_3_Pt100_2 As Integer = &H12  '18 Pt100 2線式
    Public Const gCstCodeChDataTypePID_4_Pt100_3 As Integer = &H13  '19 Pt100 3線式
    Public Const gCstCodeChDataTypePID_5_AI_K As Integer = &H14     '20 AI K

    '単位(PID)
    ' アナログと同

    'ステータス種別(PID)
    ' アナログと同

    'レンジ種別(PID)
    ' アナログと同

    '標準範囲設定なし値
    ' アナログと同

#End Region

#End Region

#Region "FU設定"

    ''FUアドレス未設定時のコード
    Public Const gCstCodeChNotSetFuNo As Integer = &HFFFF
    Public Const gCstCodeChNotSetFuPort As Integer = &HFFFF
    Public Const gCstCodeChNotSetFuPin As Integer = &HFFFF

    ''FUアドレス未設定時のコード（１バイト用）
    Public Const gCstCodeChNotSetFuNoByte As Integer = &HFF
    Public Const gCstCodeChNotSetFuPortByte As Integer = &HFF
    Public Const gCstCodeChNotSetFuPinByte As Integer = &HFF

    ''スロット種別
    Public Const gCstCodeFuSlotTypeNothing As Integer = &H0
    Public Const gCstCodeFuSlotTypeDO As Integer = &H1
    Public Const gCstCodeFuSlotTypeDI As Integer = &H2
    Public Const gCstCodeFuSlotTypeAO As Integer = &H3
    Public Const gCstCodeFuSlotTypeAI_2 As Integer = &H4
    Public Const gCstCodeFuSlotTypeAI_3 As Integer = &H5
    Public Const gCstCodeFuSlotTypeAI_1_5 As Integer = &H6
    Public Const gCstCodeFuSlotTypeAI_4_20 As Integer = &H7
    Public Const gCstCodeFuSlotTypeAI_K As Integer = &H8

    ''端子台PIN最大数
    Public Const gCstCntFuSlotPinMax As Integer = 64

    ''FU使用/未使用フラグ
    Public Const gCstCodeFuUse As Integer = 1
    Public Const gCstCodeFuNotUse As Integer = 0

    ''CANBUS使用/未使用フラグ
    Public Const gCstCodeFuCanbusUse As Integer = 1
    Public Const gCstCodeFuCanbusNotUse As Integer = 0

    ''出力設定最大数
    Public Const gCstCntFuOutputDoMax As Integer = 576
    'Public Const gCstCntFuOutputAoMax As Integer = 64

    ''出力タイプ
    Public Const gCstCodeFuOutputTypeInvalid As Integer = 0
    Public Const gCstCodeFuOutputTypeAlmFtLt As Integer = 1
    Public Const gCstCodeFuOutputTypeAlmFt__ As Integer = 2
    Public Const gCstCodeFuOutputTypeAlm__LT As Integer = 3
    Public Const gCstCodeFuOutputTypeAlm____ As Integer = 4
    Public Const gCstCodeFuOutputTypeCh____ As Integer = 5
    Public Const gCstCodeFuOutputTypeRun__LT As Integer = 6

    ''Status(OutputMovement)
    Public Const gCstCodeFuOutputStatusAlarm As Integer = 0
    Public Const gCstCodeFuOutputStatusMotor As Integer = 1
    Public Const gCstCodeFuOutputStatusOnOff As Integer = 2

    ''チャンネルタイプ
    Public Const gCstCodeFuOutputChTypeCh As Integer = 0
    Public Const gCstCodeFuOutputChTypeOr As Integer = 1
    Public Const gCstCodeFuOutputChTypeAnd As Integer = 2

    ''論理出力設定レコード数
    Public Const gCstCntFuAndOrRecCnt As Integer = 64
    Public Const gCstCntFuAndOrChCnt As Integer = 24

    Public Const gCstCntFuAndOrRecCntOr As Integer = 32
    Public Const gCstCntFuAndOrRecCntAnd As Integer = 16

    ''スロット情報
    Public Const gCstFuSlotMaxDO As Integer = 64
    Public Const gCstFuSlotMaxDI As Integer = 64
    Public Const gCstFuSlotMaxAO As Integer = 8
    Public Const gCstFuSlotMaxAI_2Line As Integer = 16
    Public Const gCstFuSlotMaxAI_1_5 As Integer = 16
    Public Const gCstFuSlotMaxAI_K As Integer = 16
    Public Const gCstFuSlotMaxAI_3Line As Integer = 16
    Public Const gCstFuSlotMaxAI_4_20 As Integer = 16

#End Region

#Region "グループ設定"

    ''表示位置設定なし値
    Public Const gCstCodeChGroupDisplayPositionNothing As Integer = &HFFFF
    Public Const gCstCodeChGroupDisplayPositionNothingConvert As Integer = &HFF

#End Region

#Region "コントロール使用可/不可設定"

    ' Ver1.9.6 2016.02.16  32 → 128 拡張
    'Ver2.0.0.7 128→256 拡張 ﾌｧｲﾙｻｲｽﾞは H28E0(10464)とHA320(41760)の2種類
    Public Const gAmxControlUseNotUse As Integer = 256
    Public Const gOldUseNotUseFileSize As Integer = &H28E0   '' 既存ﾌｧｲﾙのｻｲｽﾞ
    Public Const gOldUseNotUseFileSize2 As Integer = &HA320  '' 既存ﾌｧｲﾙのｻｲｽﾞ

    ''条件タイプ
    Public Const gCstCodeChCtrlUseCondTypeNothing As Integer = 0
    'Public Const gCstCodeChCtrlUseCondTypeAndNotuse As Integer = 1
    'Public Const gCstCodeChCtrlUseCondTypeAndUse As Integer = 2
    'Public Const gCstCodeChCtrlUseCondTypeOrNotUseBitAnd As Integer = 3
    'Public Const gCstCodeChCtrlUseCondTypeOrUse As Integer = 4
    ''1～は現状プログラム内では使用していない。使用する場合はコメント解除する

    ''CH条件タイプ
    Public Const gCstCodeChCtrlUseCondTypeChNothing As Integer = 0
    'Public Const gCstCodeChCtrlUseCondTypeChNor As Integer = 1
    'Public Const gCstCodeChCtrlUseCondTypeChAbnor As Integer = 2
    'Public Const gCstCodeChCtrlUseCondTypeChBitAnd As Integer = 3
    'Public Const gCstCodeChCtrlUseCondTypeChBitOr As Integer = 4
    'Public Const gCstCodeChCtrlUseCondTypeChAnd As Integer = 5
    'Public Const gCstCodeChCtrlUseCondTypeChOr As Integer = 6
    ''1～は現状プログラム内では使用していない。使用する場合はコメント解除する

#End Region

#Region "グループリポーズ"

    ''データ種別コード
    Public Const gCstCodeChGroupReposeTypeNormal As Integer = 0
    'Public Const gCstCodeChGroupReposeTypeOr As Integer = 1
    'Public Const gCstCodeChGroupReposeTypeAnd As Integer = 2
    'Public Const gCstCodeChGroupReposeTypeMotor As Integer = 3
    'Public Const gCstCodeChGroupReposeTypeAnd2Or As Integer = 4
    ''1～は現状プログラム内では使用していない。使用する場合はコメント解除する

#End Region

#Region "データ転送"

    ''データコード
    Public Const gCstCodeChDataForwardCodeNone As Integer = 0
    Public Const gCstCodeChDataForwardCodeCalc As Integer = 1
    Public Const gCstCodeChDataForwardCodeComm As Integer = 2

#End Region

#Region "SIO設定"

    ''通信タイプ１
    Public Const gCstCodeChSioCommType1Nothing As Integer = 0

    ''通信CHタイプ
    Public Const gCstCodeChSioCommChType1Nothing As Integer = 0
    Public Const gCstCodeChSioCommChType1ChData As Integer = 1
    Public Const gCstCodeChSioCommChType1DataLength As Integer = 2
    Public Const gCstCodeChSioCommChType1NEXT As Integer = 3        ' 2014.01.14 区切り文字

    ''ポート数
    Public Const gCstCntChSioPort As Integer = 16
    'VDRﾎﾟｰﾄ数 9 2019.03.18
    'Public Const gCstCntChSioVDRPort As Integer = 9
    Public Const gCstCntChSioVDRPort As Integer = 16

#End Region

#Region "シーケンス設定"

    ''入力タイプ
    Public Const gCstCodeSeqInputTypeNonInvert As Byte = 0    ''正転
    Public Const gCstCodeSeqInputTypeInvert As Byte = 1       ''反転
    Public Const gCstCodeSeqInputTypeOneShot As Byte = 2      ''１ショット

    ''選択型
    Public Const gCstCodeSeqChSelectData As UShort = 1
    Public Const gCstCodeSeqChSelectAnalog As UShort = 2
    Public Const gCstCodeSeqChSelectCalc As UShort = 3
    Public Const gCstCodeSeqChSelectExtGroup As UShort = 4
    Public Const gCstCodeSeqChSelectManual As UShort = 5
    Public Const gCstCodeSeqChSelectFixed As UShort = 6      '' K.Tanigawa 2012/02/15 項目追加（定数:6）

    ''DATA型
    Public Const gCstCodeSeqStatusDataAnalog As UShort = &H11
    Public Const gCstCodeSeqStatusDataDigital As UShort = &H12
    Public Const gCstCodeSeqStatusDataPulse As UShort = &H13
    Public Const gCstCodeSeqStatusDataRunning As UShort = &H14
    Public Const gCstCodeSeqStatusDataMotor As UShort = &H15
    Public Const gCstCodeSeqStatusDataComposite As UShort = &H16
    Public Const gCstCodeSeqStatusDataHigh As UShort = &H1E
    Public Const gCstCodeSeqStatusDataLow As UShort = &H1F

    Public Const gCstCodeSeqMaskDataAnalog As UShort = &HFFFF
    Public Const gCstCodeSeqMaskDataDigital As UShort = &H40
    Public Const gCstCodeSeqMaskDataPulse As UShort = &HFFFF
    Public Const gCstCodeSeqMaskDataRunning As UShort = &HFFFF

    Public Const gCstCodeSeqMaskDataMotorRun1 As UShort = &H2
    Public Const gCstCodeSeqMaskDataMotorRun2 As UShort = &H4
    Public Const gCstCodeSeqMaskDataMotorRun3 As UShort = &H8
    Public Const gCstCodeSeqMaskDataMotorRun4 As UShort = &H10
    Public Const gCstCodeSeqMaskDataMotorSTBY As UShort = &H20

    Public Const gCstCodeSeqMaskDataCompositeBit1 As UShort = &H1
    Public Const gCstCodeSeqMaskDataCompositeBit2 As UShort = &H2
    Public Const gCstCodeSeqMaskDataCompositeBit3 As UShort = &H4
    Public Const gCstCodeSeqMaskDataCompositeBit4 As UShort = &H8
    Public Const gCstCodeSeqMaskDataCompositeBit5 As UShort = &H10
    Public Const gCstCodeSeqMaskDataCompositeBit6 As UShort = &H20
    Public Const gCstCodeSeqMaskDataCompositeBit7 As UShort = &H40
    Public Const gCstCodeSeqMaskDataCompositeBit8 As UShort = &H80

    Public Const gCstCodeSeqMaskDataHigh As UShort = &H0
    Public Const gCstCodeSeqMaskDataLow As UShort = &H0

    ''ALARM型
    Public Const gCstCodeSeqStatusAlarmAnalog As UShort = &H21
    Public Const gCstCodeSeqStatusAlarmDigital As UShort = &H22
    Public Const gCstCodeSeqStatusAlarmMotor As UShort = &H23
    Public Const gCstCodeSeqStatusAlarmComposite As UShort = &H24
    Public Const gCstCodeSeqStatusAlarmHigh As UShort = &H2E
    Public Const gCstCodeSeqStatusAlarmLow As UShort = &H2F

    Public Const gCstCodeSeqMaskAlarmAnalogLo As UShort = &H1
    Public Const gCstCodeSeqMaskAlarmAnalogHi As UShort = &H2
    Public Const gCstCodeSeqMaskAlarmAnalogLoLo As UShort = &H4
    Public Const gCstCodeSeqMaskAlarmAnalogHiHi As UShort = &H8
    Public Const gCstCodeSeqMaskAlarmAnalogSensor As UShort = &H70

    Public Const gCstCodeSeqMaskAlarmDigital As UShort = &H1
    Public Const gCstCodeSeqMaskAlarmMotor As UShort = &H1

    Public Const gCstCodeSeqMaskAlarmCompositeSt1 As UShort = &H1000
    Public Const gCstCodeSeqMaskAlarmCompositeSt2 As UShort = &H2000
    Public Const gCstCodeSeqMaskAlarmCompositeSt3 As UShort = &H4000
    Public Const gCstCodeSeqMaskAlarmCompositeSt4 As UShort = &H8000
    Public Const gCstCodeSeqMaskAlarmCompositeSt5 As UShort = &H1
    Public Const gCstCodeSeqMaskAlarmCompositeSt6 As UShort = &H2
    Public Const gCstCodeSeqMaskAlarmCompositeSt7 As UShort = &H4
    Public Const gCstCodeSeqMaskAlarmCompositeSt8 As UShort = &H8
    Public Const gCstCodeSeqMaskAlarmCompositeSt9 As UShort = &H10
    Public Const gCstCodeSeqMaskAlarmCompositeStFB As UShort = &H100

    Public Const gCstCodeSeqMaskAlarmHigh As UShort = &H0
    Public Const gCstCodeSeqMaskAlarmLow As UShort = &H0

    ''CalcInput型
    Public Const gCstCodeSeqStatusCalcOutput1 As UShort = &H31
    Public Const gCstCodeSeqStatusCalcOutput2 As UShort = &H32
    Public Const gCstCodeSeqStatusCalcOutput3 As UShort = &H33
    Public Const gCstCodeSeqStatusCalcOutput4 As UShort = &H34
    Public Const gCstCodeSeqStatusCalcOutput5 As UShort = &H35
    Public Const gCstCodeSeqStatusCalcOutput6 As UShort = &H36
    Public Const gCstCodeSeqMaskCalcOutput1 As UShort = &HFFFF
    Public Const gCstCodeSeqMaskCalcOutput2 As UShort = &HFFFF
    Public Const gCstCodeSeqMaskCalcOutput3 As UShort = &HFFFF
    Public Const gCstCodeSeqMaskCalcOutput4 As UShort = &HFFFF
    Public Const gCstCodeSeqMaskCalcOutput5 As UShort = &HFFFF
    Public Const gCstCodeSeqMaskCalcOutput6 As UShort = &HFFFF

    ''ExtGroupInput型
    Public Const gCstCodeSeqStatusExtGroupOut As UShort = &H41
    Public Const gCstCodeSeqStatusExtBzOut As UShort = &H42

    ' ''ManualInput型
    'Public Const gCstCodeSeqStatusManualRefStatus As UShort = &H31
    'Public Const gCstCodeSeqStatusManualInputMask As UShort = &H32
    'Public Const gCstCodeSeqStatusManualInputType As UShort = &H33

    ''演算式テーブル - 定数種類
    Public Const gCstCodeSeqFixTypeChData As Integer = 0
    Public Const gCstCodeSeqFixTypeFixFloat As Integer = 1
    Public Const gCstCodeSeqFixTypeFixLong As Integer = 2
    Public Const gCstCodeSeqFixTypeLowSet As Integer = 3
    Public Const gCstCodeSeqFixTypeHighSet As Integer = 4
    Public Const gCstCodeSeqFixTypeLLSet As Integer = 5
    Public Const gCstCodeSeqFixTypeHHSet As Integer = 6

    ''シーケンスロジックサブ設定（データ種類）
    Public Const gCstCodeSeqLogicSubDataTypeChNo As Integer = 1
    Public Const gCstCodeSeqLogicSubDataTypeBit As Integer = 2
    Public Const gCstCodeSeqLogicSubDataTypeLinear As Integer = 3
    Public Const gCstCodeSeqLogicSubDataTypeExpresion As Integer = 4
    Public Const gCstCodeSeqLogicSubDataTypeFixed As Integer = 5

    ''出力ステータス
    Public Const gCstCodeSeqOutputStatusNothing As Integer = 0

    ''出力データタイプ
    Public Const gCstCodeSeqOutputTypeChNothing As Integer = 0

    ''出力タイプ
    Public Const gCstCodeSeqOutputTypeFuNothing As Integer = 0

#End Region

#Region "OPS設定"

#Region "プルダウン設定"

    ''メニュータイプ
    Public Const gCstCodeOpsPullDownTypeNothing As Integer = 0
    Public Const gCstCodeOpsPullDownTypeGroup As Integer = 1
    Public Const gCstCodeOpsPullDownTypeSub As Integer = 2
    Public Const gCstCodeOpsPullDownTypeMainOnly As Integer = 3

    ''メニュー番号
    Public Const gCstCodeOpsPullDownMenuNothing As Integer = 0
    Public Const gCstCodeOpsPullDownMenuMenuView As Integer = 1
    Public Const gCstCodeOpsPullDownMenuTrend As Integer = 2
    Public Const gCstCodeOpsPullDownMenuFree As Integer = 3
    Public Const gCstCodeOpsPullDownMenuSummary As Integer = 4
    Public Const gCstCodeOpsPullDownMenuHistory As Integer = 5
    Public Const gCstCodeOpsPullDownMenuGraph As Integer = 6
    Public Const gCstCodeOpsPullDownMenuMimic As Integer = 7
    Public Const gCstCodeOpsPullDownMenuControl As Integer = 8
    Public Const gCstCodeOpsPullDownMenuCalcu As Integer = 9
    Public Const gCstCodeOpsPullDownMenuPrint As Integer = 10
    Public Const gCstCodeOpsPullDownMenuSystem As Integer = 11

    ''メインメニュー　列番号
    Public Const gCstCodeOpsPullDownMenuColNameMainMenuName As Integer = 0
    Public Const gCstCodeOpsPullDownMenuColNameType As Integer = 1
    Public Const gCstCodeOpsPullDownMenuColNameSetCount As Integer = 2
    Public Const gCstCodeOpsPullDownMenuColNameArrow As Integer = 3

#End Region

#Region "グラフ設定"

    ''フリーグラフの画面数（タブ数）
    Public Const gCstCodeFreeGraphTabCnt As Integer = 10

    ''行数設定
    Public Const gCstCodeOpsGraphNo As Integer = 16                 ''レコード数
    Public Const gCstCodeOpsRowCountExhBar As Integer = 24          ''シリンダ数
    Public Const gCstCodeOpsRowCountAnalog As Integer = 8           ''アナログメーター設定数
    Public Const gCstCodeOpsRowCountTC As Integer = 8               ''T/Cグラフ情報設定数

    ''20Graph上下限値
    Public Const gCstCodeOps20GraphSplitLower As Integer = 17       ''下限値
    Public Const gCstCodeOps20GraphSplitUpper As Integer = 20       ''上限値

    ''------------------------------

    ''グラフタイトルグラフタイプ
    Public Const gCstCodeOpsTitleGraphTypeNothing As Integer = 0
    Public Const gCstCodeOpsTitleGraphTypeExhaust As Integer = 1
    Public Const gCstCodeOpsTitleGraphTypeBar As Integer = 2
    Public Const gCstCodeOpsTitleGraphTypeAnalogMeter As Integer = 3

    ''排気ガスグラフ
    Public Const gCstCodeOpsExhGraphLineNothing As Integer = 0
    Public Const gCstCodeOpsExhGraphLine1 As Integer = 1
    Public Const gCstCodeOpsExhGraphLine2 As Integer = 2

    ''バーグラフ
    Public Const gCstCodeOpsBarGraphRangeTypeMeasure As Integer = 0
    Public Const gCstCodeOpsBarGraphRangeType100 As Integer = 1
    Public Const gCstCodeOpsBarGraphDivisionNoting As Integer = 1
    Public Const gCstCodeOpsBarGraphDivision4 As Integer = 1
    Public Const gCstCodeOpsBarGraphDivision6 As Integer = 2
    Public Const gCstCodeOpsBarGraphDivision3_5 As Integer = 3
    Public Const gCstCodeOpsBarGraphLineNothing As Integer = 0
    Public Const gCstCodeOpsBarGraphLine1 As Integer = 1
    Public Const gCstCodeOpsBarGraphLine2 As Integer = 2
    Public Const gCstCodeOpsBarGraphScale3 As Integer = 3
    Public Const gCstCodeOpsBarGraphScale4 As Integer = 4
    Public Const gCstCodeOpsBarGraphScale5 As Integer = 5
    Public Const gCstCodeOpsBarGraphScale6 As Integer = 6
    Public Const gCstCodeOpsBarGraphScale7 As Integer = 7

    ''アナログメーターグラフ
    Public Const gCstCodeOpsAnalogMeterMeterType8 As Integer = 1
    Public Const gCstCodeOpsAnalogMeterMeterType1_4 As Integer = 2
    Public Const gCstCodeOpsAnalogMeterMeterType4_1 As Integer = 3
    Public Const gCstCodeOpsAnalogMeterMeterType2_1_2 As Integer = 4
    Public Const gCstCodeOpsAnalogMeterMeterType2 As Integer = 5
    Public Const gCstCodeOpsAnalogMeterScale3 As Integer = 3
    Public Const gCstCodeOpsAnalogMeterScale4 As Integer = 4
    Public Const gCstCodeOpsAnalogMeterScale5 As Integer = 5
    Public Const gCstCodeOpsAnalogMeterScale6 As Integer = 6
    Public Const gCstCodeOpsAnalogMeterScale7 As Integer = 7

    ''フリーグラフタイプ
    Public Const gCstCodeOpsFreeGrapTypeNone As Integer = 0
    Public Const gCstCodeOpsFreeGrapTypeCounter As Integer = 1
    Public Const gCstCodeOpsFreeGrapTypeBar As Integer = 2
    Public Const gCstCodeOpsFreeGrapTypeAnalog As Integer = 3
    Public Const gCstCodeOpsFreeGrapTypeIndicator As Integer = 4
    Public Const gCstNameOpsFreeGrapTypeNone As String = ""
    Public Const gCstNameOpsFreeGrapTypeCounter As String = "COUNTER"
    Public Const gCstNameOpsFreeGrapTypeBar As String = "BAR GRAPH"
    Public Const gCstNameOpsFreeGrapTypeAnalog As String = "ANALOG METER"
    Public Const gCstNameOpsFreeGrapTypeIndicator As String = "INDICATOR"

    ''フリーグラフ表示種別
    Public Const gCstNameOpsFreeIndKindNoSet As Integer = 0
    Public Const gCstNameOpsFreeIndKindData As Integer = 1
    Public Const gCstNameOpsFreeIndKindAlarm As Integer = 2
    Public Const gCstNameOpsFreeIndKindRepose As Integer = 3
    Public Const gCstNameOpsFreeIndKindSensor As Integer = 4

    ' ''フリーグラフ表示灯デジタル
    'Public Const gCstNameOpsFreeIndDigitalNoting As Integer = 0
    'Public Const gCstNameOpsFreeIndDigitalOFF As Integer = 1
    'Public Const gCstNameOpsFreeIndDigitalON As Integer = 2

    ' ''フリーグラフ表示灯アナログ
    'Public Const gCstNameOpsFreeIndAnalogNoting As Integer = 0
    'Public Const gCstNameOpsFreeIndAnalogALM As Integer = 1
    'Public Const gCstNameOpsFreeIndAnalogREPOSE As Integer = 2
    'Public Const gCstNameOpsFreeIndAnalogSENSOR As Integer = 3

    ''フリーグラフ表示灯モーター
    'Public Const gCstNameOpsFreeIndMotorRun1 As Integer = 0
    'Public Const gCstNameOpsFreeIndMotorRun2 As Integer = 0
    'Public Const gCstNameOpsFreeIndMotorRun3 As Integer = 0
    'Public Const gCstNameOpsFreeIndMotorRun4 As Integer = 0
    'Public Const gCstNameOpsFreeIndMotorRun5 As Integer = 0

#End Region

#Region "ログフォーマット"

    Public Const gCstCodeOpsLogFormatTypeNothing As Integer = 0
    Public Const gCstCodeOpsLogFormatTypeCounterTitle As Integer = 1
    Public Const gCstCodeOpsLogFormatTypeAnalogTitle As Integer = 2
    Public Const gCstCodeOpsLogFormatTypeCh As Integer = 3
    Public Const gCstCodeOpsLogFormatTypeGroup As Integer = 4
    Public Const gCstCodeOpsLogFormatTypeSpace As Integer = 5
    Public Const gCstCodeOpsLogFormatTypePage As Integer = 6
    Public Const gCstCodeOpsLogFormatTypeDate As Integer = 7

    ''保存用ログフォーマット文字列の頭文字指定
    Public Const gCstNameOpsLogFormatStringsCH As String = "CH"
    Public Const gCstNameOpsLogFormatStringsGROUP As String = "GR"

#End Region

#End Region

#Region "GWS設定"
    ''通信CHタイプ
    Public Const gCstCodeChGwsCommChNothing As Integer = 0
    Public Const gCstCodeChGwsCommChChData As Integer = 1

    ''ポート数
    Public Const gCstCntOpsGwsPort As Integer = 8   '' GWS1:1～4, GWS2:5～8

#End Region

#Region "延長警報"

#Region "Duty Set"

    ''GroupEffect
    Public Const gCstCodeExtDutyEffectNormal As Integer = 0
    Public Const gCstCodeExtDutyEffectFix As Integer = 1
    Public Const gCstCodeExtDutyEffectExtOutput As Integer = 2


#End Region

#Region "ExtPanel"

    ''WatchLED
    Public Const gCstCodeExtPanelWatchLedNone As Integer = 0
    Public Const gCstCodeExtPanelWatchLedMan As Integer = 1
    Public Const gCstCodeExtPanelWatchLedUnman As Integer = 2
    Public Const gCstCodeExtPanelWatchLedManUnman As Integer = 3


#End Region

#Region "Timer"

    ''種類
    Public Const gCstCodeExtTimerTypeNothing As Integer = 0
    Public Const gCstCodeExtTimerTypeDeadman1 As Integer = 1
    Public Const gCstCodeExtTimerTypeDeadman2 As Integer = 2
    Public Const gCstCodeExtTimerTypeEeengineerCall As Integer = 3

    ''分/秒 切替設定
    Public Const gCstCodeExtTimerTimerDispSec As Integer = 0
    Public Const gCstCodeExtTimerTimerDispMin As Integer = 1

    '-----------------
    ''有効範囲（秒）
    '-----------------
    ''最小値
    Public Const gCstCodeExtTimerLimitLowSecEng As Short = 0
    Public Const gCstCodeExtTimerLimitLowSecDeadMan1 As Short = 0
    Public Const gCstCodeExtTimerLimitLowSecDeadMan2 As Short = 0
    Public Const gCstCodeExtTimerLimitLowSecElse As Short = 0

    ''最大値
    Public Const gCstCodeExtTimerLimitHighSecEng As Short = 3600
    Public Const gCstCodeExtTimerLimitHighSecDeadMan1 As Short = 3600
    Public Const gCstCodeExtTimerLimitHighSecDeadMan2 As Short = 3600
    Public Const gCstCodeExtTimerLimitHighSecElse As Short = 3600

    '-----------------
    ''有効範囲（分）
    '-----------------
    ''最小値
    Public Const gCstCodeExtTimerLimitLowMinEng As Double = 0.0
    Public Const gCstCodeExtTimerLimitLowMinDeadMan1 As Double = 0.0
    Public Const gCstCodeExtTimerLimitLowMinDeadMan2 As Double = 0.0
    Public Const gCstCodeExtTimerLimitLowMinElse As Double = 0.0

    ''最大値
    Public Const gCstCodeExtTimerLimitHighMinEng As Double = 10.0
    Public Const gCstCodeExtTimerLimitHighMinDeadMan1 As Double = 60.0
    Public Const gCstCodeExtTimerLimitHighMinDeadMan2 As Double = 10.0
    Public Const gCstCodeExtTimerLimitHighMinElse As Double = 60.0

#End Region

#End Region

#Region "印字設定"

    ''SelectPartコンボボックス 設定名称
    Public Const gCstNamePrintCmbSelectPartMach As String = "Machinery"
    Public Const gCstNamePrintCmbSelectPartCarg As String = "Cargo"

    ''-----------------
    '' Graph View
    ''-----------------
    ''ディスプレイ位置が空欄の場合
    Public Const gCstCodePrintGraphViewExceptionNo As Integer = 65535

    ''印刷モード
    Public Const gCstCodePrintGraphViewPrintModePrint As Integer = 0
    Public Const gCstCodePrintGraphViewPrintModePreview As Integer = 1
    Public Const gCstCodePrintGraphViewPrintModeAllPrint As Integer = 2 'Ver2.0.0.2 グラフ全印刷モード

    ''グラフタイプ
    Public Const gCstCodePrintGraphViewGraphTypeExhaust As Integer = 1
    Public Const gCstCodePrintGraphViewGraphTypeBar As Integer = 2
    Public Const gCstCodePrintGraphViewGraphTypeAnalogMeter As Integer = 3
    Public Const gCstCodePrintGraphViewGraphTypeFree As Integer = 4

    '' ｵﾌﾟｼｮﾝ設定     Ver1.9.3 2016.01.25
    Public Const gCstOpsLogOptionMax As Integer = 1500
#End Region

#End Region

#Region "出力ファイル定義"

    ''システム設定データ
    Public Const gCstPathSystem As String = "set\sys"
    Public Const gCstFileSystem As String = "system.cfg"
    Public Const gCstRecsSystem As Integer = 22
    Public Const gCstSizeSystem As Integer = 56
    Public Const gCstFnumSystem As Integer = 1100

    ''FU設定データ（チャンネル情報）
    Public Const gCstPathFuChannel As String = "set\fu"
    Public Const gCstFileFuChannel As String = "channel.inf"
    Public Const gCstRecsFuChannel As Integer = 21
    Public Const gCstSizeFuChannel As Integer = 36
    Public Const gCstFnumFuChannel As Integer = 1300

    ''チャンネル情報データ（表示名設定データ）
    Public Const gCstPathChDisp As String = "set\fu"
    Public Const gCstFileChDisp As String = "channel_name.tbl"
    Public Const gCstRecsChDisp As Integer = 21
    Public Const gCstSizeChDisp As Integer = 43072
    Public Const gCstFnumChDisp As Integer = 1301

    ''チャンネル情報
    Public Const gCstPathChannel As String = "meas"
    Public Const gCstFileChannel As String = "channel.cfg"
    'Public Const gCstRecsChannel As Integer = 0
    Public Const gCstSizeChannel As Integer = 512
    Public Const gCstFnumChannel As Integer = 2000

    ''コンポジット情報
    Public Const gCstPathComposite As String = "meas"
    Public Const gCstFileComposite As String = "ch_composite.cfg"
    'Public Const gCstRecsComposite As Integer = 0
    Public Const gCstSizeComposite As Integer = 148
    Public Const gCstFnumComposite As Integer = 2103

    ''グループ設定
    Public Const gCstPathGroup As String = "meas"
    Public Const gCstFileGroupM As String = "ch_groupset1.cfg"
    Public Const gCstFileGroupC As String = "ch_groupset2.cfg"
    Public Const gCstRecsGroup As Integer = 1
    Public Const gCstSizeGroup As Integer = 2144
    Public Const gCstFnumGroupM As Integer = 2010
    Public Const gCstFnumGroupC As Integer = 2011

    ''リポーズ入力設定
    Public Const gCstPathRepose As String = "meas"
    Public Const gCstFileRepose As String = "ch_repose.cfg"
    Public Const gCstRecsRepose As Integer = 48
    Public Const gCstSizeRepose As Integer = 28
    Public Const gCstFnumRepose As Integer = 2100

    ''出力チャンネル設定
    Public Const gCstPathOutPut As String = "meas"
    Public Const gCstFileOutPut As String = "ch_output.cfg"
    Public Const gCstRecsOutPut As Integer = 576
    Public Const gCstSizeOutPut As Integer = 16
    Public Const gCstFnumOutPut As Integer = 2101

    ''論理出力設定
    Public Const gCstPathOrAnd As String = "meas"
    Public Const gCstFileOrAnd As String = "ch_orand.cfg"
    Public Const gCstRecsOrAnd As Integer = 64
    Public Const gCstSizeOrAnd As Integer = 192
    Public Const gCstFnumOrAnd As Integer = 2102

    ''積算データ設定
    Public Const gCstPathChAdd As String = "meas"
    Public Const gCstFileChAdd As String = "ch_add.cfg"
    Public Const gCstRecsChAdd As Integer = 256
    Public Const gCstSizeChAdd As Integer = 16
    Public Const gCstFnumChAdd As Integer = 2200

    ''排ガス処理演算設定
    Public Const gCstPathExhGus As String = "meas"
    Public Const gCstFileExhGus As String = "cal_exhaust.tbl"
    Public Const gCstRecsExhGus As Integer = 16
    Public Const gCstSizeExhGus As Integer = 204
    Public Const gCstFnumExhGus As Integer = 2302

    ''コントロール使用可／不可設定
    Public Const gCstPathCtrlUseNouse As String = "set\ops"
    Public Const gCstFileCtrlUseNouseM As String = "ControlUse1.dat"
    Public Const gCstFileCtrlUseNouseC As String = "ControlUse2.dat"
    Public Const gCstRecsCtrlUseNouse As Integer = 32
    Public Const gCstSizeCtrlUseNouse As Integer = 326
    Public Const gCstFnumCtrlUseNouseM As Integer = 1205
    Public Const gCstFnumCtrlUseNouseC As Integer = 1215

    ''SIO設定
    Public Const gCstPathChSio As String = "set\ext"
    Public Const gCstFileChSio As String = "ExtDev.inf"
    Public Const gCstRecsChSio As Integer = 33
    Public Const gCstSizeChSio1 As Integer = 32
    Public Const gCstSizeChSio2 As Integer = 588
    Public Const gCstFnumChSio As Integer = 1440

    ''SIO設定CH設定
    Public Const gCstPathChSioCh As String = "set\ext"
    Public Const gCstFileChSioChName As String = "ExtDev_ch_port"
    Public Const gCstFileChSioChExt As String = ".inf"
    Public Const gCstRecsChSioCh As Integer = 3000
    Public Const gCstSizeChSioCh As Integer = 8
    Public Const gCstFnumChSioChStart As Integer = 1441

    'SIO設定拡張設定
    Public Const gCstPathChSioExt As String = "set\ext"
    Public Const gCstFileChSioExtName As String = "ExtDev_ext_table_port"
    Public Const gCstFileChSioExtExt As String = ".inf"
    Public Const gCstRecsChSioExt As Integer = 4032
    Public Const gCstSizeChSioExt As Integer = 9


    ''データ転送テーブル設定
    Public Const gCstPathChDataForwardTableSet As String = "set\sys"
    Public Const gCstFileChDataForwardTableSet As String = "trans_table.cfg"
    Public Const gCstRecsChDataForwardTableSet As Integer = 64
    Public Const gCstSizeChDataForwardTableSet As Integer = 24
    Public Const gCstFnumChDataForwardTableSet As Integer = 1140

    ''データ保存テーブル設定
    Public Const gCstPathChDataSaveTable As String = "set\sys"
    Public Const gCstFileChDataSaveTable As String = "save_table.cfg"
    Public Const gCstRecsChDataSaveTable As Integer = 64
    Public Const gCstSizeChDataSaveTable As Integer = 12
    Public Const gCstFnumChDataSaveTable As Integer = 1150

    ''延長警報設定
    Public Const gCstPathExtAlarm As String = "set\ext"
    Public Const gCstFileExtAlarm As String = "EXT.inf"
    Public Const gCstRecsExtAlarm As Integer = 21
    Public Const gCstSizeExtAlarm1 As Integer = 624
    Public Const gCstSizeExtAlarm2 As Integer = 80
    Public Const gCstFnumExtAlarm As Integer = 1430

    ''タイマ設定
    Public Const gCstPathTimer As String = "meas"
    Public Const gCstFileTimer As String = "delay_time.tbl"
    Public Const gCstRecsTimer As Integer = 16
    Public Const gCstSizeTimer As Integer = 12
    Public Const gCstFnumTimer As Integer = 2500

    ''タイマ表示名称設定
    Public Const gCstPathTimerName As String = "meas"
    Public Const gCstFileTimerName As String = "timer_name.tbl"
    Public Const gCstRecsTimerName As Integer = 16
    Public Const gCstSizeTimerName As Integer = 32
    Public Const gCstFnumTimerName As Integer = 2501

    ''シーケンスID
    Public Const gCstPathSeqSequenceID As String = "meas"
    Public Const gCstFileSeqSequenceID As String = "seq_id.tbl"
    Public Const gCstRecsSeqSequenceID As Integer = 1
    Public Const gCstSizeSeqSequenceID As Integer = 2048
    Public Const gCstFnumSeqSequenceID As Integer = 2400

    ''シーケンス設定
    Public Const gCstPathSeqSequenceSet As String = "meas"
    Public Const gCstFileSeqSequenceSet As String = "seq_data.tbl"
    Public Const gCstRecsSeqSequenceSet As Integer = 1024
    Public Const gCstSizeSeqSequenceSet As Integer = 192
    Public Const gCstFnumSeqSequenceSet As Integer = 2401

    ''リニアライズテーブル
    Public Const gCstPathSeqLinear As String = "meas"
    Public Const gCstFileSeqLinear As String = "cal_linear.tbl"
    Public Const gCstRecsSeqLinear As Integer = 257
    Public Const gCstSizeSeqLinear1 As Integer = 512
    Public Const gCstSizeSeqLinear2 As Integer = 8192
    Public Const gCstFnumSeqLinear As Integer = 2301

    ''演算式テーブル
    Public Const gCstPathSeqOperationExpression As String = "meas"
    Public Const gCstFileSeqOperationExpression As String = "cal_formula.tbl"
    Public Const gCstRecsSeqOperationExpression As Integer = 64
    Public Const gCstSizeSeqOperationExpression As Integer = 320
    Public Const gCstFnumSeqOperationExpression As Integer = 2300

    ''OPSスクリーンタイトルデータ 
    Public Const gCstPathOpsScreenTitle As String = "set\ops"
    Public Const gCstFileOpsScreenTitleM As String = "ScreenTitle1.dat"
    Public Const gCstFileOpsScreenTitleC As String = "ScreenTitle2.dat"
    Public Const gCstRecsOpsScreenTitle As Integer = 100
    Public Const gCstSizeOpsScreenTitle As Integer = 32
    Public Const gCstFnumOpsScreenTitleM As Integer = 1201
    Public Const gCstFnumOpsScreenTitleC As Integer = 1211

    ''プルダウンメニュー
    Public Const gCstPathOpsPulldownMenu As String = "set\ops"
    Public Const gCstFileOpsPulldownMenuM As String = "MainMenu1.dat"
    Public Const gCstFileOpsPulldownMenuC As String = "MainMenu2.dat"
    '▼▼▼ 20110705 NEW時iniファイルフォルダに保存してあるMainMenu.datを読み込む ▼▼▼▼▼▼▼▼▼
    Public Const gCstFileOpsPulldownMenuInit As String = "MainMenu.dat"
    Public Const gCstFileOpsPulldownMenuInit_VDU As String = "MainMenuVDU.dat"

    '和文仕様 20200218 hori
    Public Const gCstFileOpsPulldownMenuInitJpn As String = "MainMenuJpn.dat"
    Public Const gCstFileOpsPulldownMenuInitJpn_VDU As String = "MainMenuJpnVDU.dat"
    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
    Public Const gCstRecsOpsPulldownMenu As Integer = 12
    Public Const gCstSizeOpsPulldownMenu As Integer = 12040 ''9192 Ver1.6.5(2014.08.28) サイズ変更
    Public Const gCstFnumOpsPulldownMenuM As Integer = 1202
    Public Const gCstFnumOpsPulldownMenuC As Integer = 1212

    ''プルダウンメニュー
    Public Const gCstPathOpsSelectionMenu As String = "set\ops"
    Public Const gCstFileOpsSelectionMenuM As String = "SelectionMenu1.dat"
    Public Const gCstFileOpsSelectionMenuC As String = "SelectionMenu2.dat"
    '▼▼▼ 20150120 NEW時iniファイルフォルダに保存してあるSelectionMenu.datを読み込む ▼▼▼▼▼▼▼
    Public Const gCstFileOpsSelectionMenuInit As String = "SelectionMenu.dat"
    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
    Public Const gCstRecsOpsSelectionMenu As Integer = 100
    Public Const gCstSizeOpsSelectionMenu As Integer = 24
    Public Const gCstFnumOpsSelectionMenuM As Integer = 1208
    Public Const gCstFnumOpsSelectionMenuC As Integer = 1218

    ''OPSグラフ設定
    Public Const gCstPathOpsGraph As String = "set\ops"
    Public Const gCstFileOpsGraphM As String = "GraphTitle1.dat"
    Public Const gCstFileOpsGraphC As String = "GraphTitle2.dat"
    Public Const gCstRecsOpsGraph As Integer = 65 '2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
    'Public Const gCstSizeOpsGraph As Integer = 9192
    Public Const gCstFnumOpsGraphM As Integer = 1203
    Public Const gCstFnumOpsGraphC As Integer = 1213

    ''フリーグラフ    2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
    Public Const gCstPathOpsFreeGraph As String = "set\ops"
    Public Const gCstFileOpsFreeGraphM As String = "FreeGraph1.dat"
    Public Const gCstFileOpsFreeGraphC As String = "FreeGraph2.dat"
    Public Const gCstRecsOpsFreeGraph As Integer = 160
    Public Const gCstSizeOpsFreeGraph As Integer = 420
    Public Const gCstFnumOpsFreeGraphM As Integer = 1209
    Public Const gCstFnumOpsFreeGraphC As Integer = 1219

    ''フリーディスプレイ
    Public Const gCstPathOpsFreeDisplay As String = "set\ops"
    Public Const gCstFileOpsFreeDisplayM As String = "FreeDisplay1.dat"
    Public Const gCstFileOpsFreeDisplayC As String = "FreeDisplay2.dat"
    Public Const gCstRecsOpsFreeDisplay As Integer = 80
    Public Const gCstSizeOpsFreeDisplay As Integer = 73
    Public Const gCstFnumOpsFreeDisplayM As Integer = 1206
    Public Const gCstFnumOpsFreeDisplayC As Integer = 1216

    ''トレンドグラフ
    Public Const gCstPathOpsTrendGraph As String = "set\ops"
    Public Const gCstFileOpsTrendGraphM As String = "TrendGraph1.dat"
    Public Const gCstFileOpsTrendGraphC As String = "TrendGraph2.dat"
    Public Const gCstRecsOpsTrendGraph As Integer = 160
    Public Const gCstSizeOpsTrendGraph As Integer = 129
    Public Const gCstFnumOpsTrendGraphM As Integer = 1207
    Public Const gCstFnumOpsTrendGraphC As Integer = 1217

    'Ver2.0.7.8 PID用トレンドグラフ
    Public Const gCstFileOpsTrendGraphPID As String = "PidTrend1.dat"
    Public Const gCstFnumOpsTrendGraphPID As Integer = 1223
    Public Const gCstFileOpsTrendGraphPID2 As String = "PidTrend2.dat"
    Public Const gCstFnumOpsTrendGraphPID2 As Integer = 1224


    ''ログフォーマット
    Public Const gCstPathOpsLogFormat As String = "set\ops"
    Public Const gCstFileOpsLogFormatM As String = "LogFormatTxt1.dat"  '' ☆LogFormat 変換前 Text
    Public Const gCstFileOpsLogFormatC As String = "LogFormatTxt2.dat"  '' ☆LogFormat 変換前 Text
    Public Const gCstRecsOpsLogFormat As Integer = 10       ''☆2012/10/26 K.Tanigawa
    Public Const gCstSizeOpsLogFormat As Integer = 1200     ''☆2012/10/26 K.Tanigawa
    Public Const gCstFnumOpsLogFormatM As Integer = 1204
    Public Const gCstFnumOpsLogFormatC As Integer = 1214

    ''ログフォーマットCHID  ☆2012/10/26 K.Tanigawa
    Public Const gCstPathOpsLogIdData As String = "set\ops"
    Public Const gCstFileOpsLogIdDataM As String = "LogFormat1.dat"     '' ☆LogFormat コンパイル後 ID
    Public Const gCstFileOpsLogIdDataC As String = "LogFormat2.dat"     '' ☆LogFormat コンパイル後 ID
    Public Const gCstRecsOpsLogIdData As Integer = 10       ''☆2012/10/26 K.Tanigawa
    Public Const gCstSizeOpsLogIdData As Integer = 1200     ''☆2012/10/26 K.Tanigawa
    Public Const gCstFnumOpsLogIdDataM As Integer = 1204
    Public Const gCstFnumOpsLogIdDataC As Integer = 1214

    ''ﾛｸﾞｵﾌﾟｼｮﾝ設定　Ver1.9.3 2016.01.25
    Public Const gCstPathOpsLogOption As String = "set\ops"
    Public Const gCstFileOpsLogOption As String = "LogOption1.dat"
    Public Const gCstRecsOpsLogOption As Integer = 2
    Public Const gCstSizeOpsLogOption As Integer = 2000
    Public Const gCstFnumOpsLogOption As Integer = 1222

    ''GWS設定CH設定     2014.02.04
    Public Const gCstPathOpsGwsCh As String = "set\ops"
    Public Const gCstFileOpsGwsChName As String = "GwsChSet"
    Public Const gCstFileOpsGwsChExt As String = ".dat"
    Public Const gCstRecsOpsGwsCh As Integer = 3000
    Public Const gCstSizeOpsGwsCh As Integer = 4
    Public Const gCstFnumOpsGwsChStart As Integer = 1221

    ''CH変換テーブル
    Public Const gCstPathChConv As String = "meas"
    Public Const gCstFileChConv As String = "CHConv.tbl"
    Public Const gCstRecsChConv As Integer = 3080
    Public Const gCstSizeChConv As Integer = 2
    Public Const gCstFnumChConv As Integer = 2020

    ''ログ印字時刻
    Public Const gCstPathOtherLogTime As String = "log"
    Public Const gCstFileOtherLogTimeM As String = "logging1.cnf"
    Public Const gCstFileOtherLogTimeC As String = "logging2.cnf"
    Public Const gCstRecsOtherLogTime As Integer = 1
    Public Const gCstSizeOtherLogTime As Integer = 32
    Public Const gCstFnumOtherLogTimeM As Integer = 3000
    Public Const gCstFnumOtherLogTimeC As Integer = 3001

    Public Const gCstFileOtherLogTimeM_Set As String = "logging1_Set.cnf"
    Public Const gCstFileOtherLogTimeC_Set As String = "logging2_Set.cnf"
    Public Const gCstFileOtherLogTimeM_Unset As String = "logging1_Unset.cnf"
    Public Const gCstFileOtherLogTimeC_Unset As String = "logging2_Unset.cnf"

    '' Ver1.11.2 2016.08.02  ﾛｸﾞｵﾌﾟｼｮﾝ 特殊設定追加
    Public Const gCstFileOtherLogTimeM_Set_Opt1 As String = "logging1_Set_Opt1.cnf"
    Public Const gCstFileOtherLogTimeC_Set_Opt1 As String = "logging2_Set_Opt1.cnf"
    ''//

    ''負荷曲線データ 20200304 hori
    Public Const gCstPathLoadCurve As String = "Mimic"
    Public Const gCstFileLoadCurve As String = "T311.com"
    Public Const gCstRecsLoadCurve As Integer = 4
    Public Const gCstSizeLoadCurve As Integer = 96
    Public Const gCstFnumLoadCurveM As Integer = 3000
    Public Const gCstFnumLoadCurveC As Integer = 3001


    ''ファイル更新情報
    Public Const gCstPathEditorUpdateInfo As String = ""
    Public Const gCstFileEditorUpdateInfo As String = "FileUpdate.inf"
    Public Const gCstRecsEditorUpdateInfo As Integer = 1
    Public Const gCstSizeEditorUpdateInfo As Integer = 200
    Public Const gCstFnumEditorUpdateInfo As Integer = 0

    '' ｴﾃﾞｨﾀ設定  Ver1.8.3 2015.11.26 追加
    Public Const gCstIniFile As String = "Setup.ini"

    '' ｴﾃﾞｨﾀVer情報  Ver1.9.0 2015.12.15 追加
    Public Const gCstVerIniFile As String = "EditVer.ini"

#End Region

#Region "初期化ファイル"

    '********************************************************
    '========================================================
    ''注意！！
    ''初期化ファイルが追加になり、ここにファイル名定義を追加した場合
    ''コンパイルでの初期化ファイル存在確認にも追加が必要になります！！
    ''追加箇所：frmCmpCompier - mChkIniFileExist
    '========================================================
    '********************************************************

    ''初期化ファイルフォルダ
    Public Const gCstIniFileDir As String = "iniFile"

    ''初期化ファイル名
    Public Const gCstIniFileNameComboSysSystem As String = "ComboSysSystem.ini"
    Public Const gCstIniFileNameComboSysFcu As String = "ComboSysFcu.ini"
    Public Const gCstIniFileNameComboSysOps As String = "ComboSysOps.ini"
    Public Const gCstIniFileNameComboSysGws As String = "ComboSysGws.ini"           '' GWS処理追加  2014.02.03
    Public Const gCstIniFileNameComboSysPrinter As String = "ComboSysPrinter.ini"
    Public Const gCstIniFileNameComboSystem As String = "ComboSystem.ini"       '' Ver1.11.8.6 2016.11.10 追加

    Public Const gCstIniFileNameComboExtPnlSystem As String = "ComboExtPnlSystem.ini"
    Public Const gCstIniFileNameComboExtPnlList As String = "ComboExtPnlList.ini"
    Public Const gCstIniFileNameComboExtPnlLed As String = "ComboExtPnlLed.ini"
    Public Const gCstIniFileNameComboExtPnlLcdGroup As String = "ComboExtPnlLcdGroup.ini"
    'Public Const gCstIniFileNameComboExtTermTest As String = "ComboExtTermTest.ini"
    Public Const gCstIniFileNameComboExtTimer As String = "ComboExtTimer.ini"

    Public Const gCstIniFileNameComboSeqOpe As String = "ComboSeqOperationFixed.ini"
    Public Const gCstIniFileNameComboSeqLine As String = "ComboSeqLinearTable.ini"
    Public Const gCstIniFileNameComboSeqSetDetail As String = "ComboSeqSetSequenceDetail.ini"

    Public Const gCstIniFileNameComboOpsPulldown As String = "ComboOpsPulldownMenu.ini"
    Public Const gCstIniFileNameComboOpsPulldownJpn As String = "ComboOpsPulldownMenuJpn.ini"  ''全和文追加　hori 20200603
    Public Const gCstIniFileNameComboOpsGraphList As String = "ComboOpsGraphList.ini"
    Public Const gCstIniFileNameComboOpsGraphAnalogMeter As String = "ComboOpsGraphAnalogMeter.ini"
    'Public Const gCstIniFileNameComboOpsGraphExhaust As String = "ComboOpsGraphExhaust.ini"
    Public Const gCstIniFileNameComboOpsGraphAnalogMeterDetail As String = "ComboOpsGraphAnalogMeterDetail.ini"
    Public Const gCstIniFileNameComboOpsLogFormat As String = "ComboOpsLogFormat.ini"

    'Public Const gCstIniFileNameComboPrtLocalUnit As String = "ComboPrtLocalUnit.ini"

    Public Const gCstIniFileNameComboChList As String = "ComboChList.ini"
    Public Const gCstIniFileNameComboChListJpn As String = "ComboChListJpn.ini"
    Public Const gCstIniFileNameComboChTerminalList As String = "ComboChTerminalList.ini"
    Public Const gCstIniFileNameComboChTerminalFunction As String = "ComboChTerminalFunction.ini"
    Public Const gCstIniFileNameComboChOutputDo As String = "ComboChOutputDo.ini"
    Public Const gCstIniFileNameComboChGroupReposeList As String = "ComboChGroupReposeList.ini"
    Public Const gCstIniFileNameComboChRunHour As String = "ComboChRunHour.ini"
    Public Const gCstIniFileNameComboChExhGusGroup As String = "ComboChExhGusGroup.ini"
    Public Const gCstIniFileNameComboChDataForwardTableList As String = "ComboChDataForwardTableList.ini"
    Public Const gCstIniFileNameComboChDataSaveTableList As String = "ComboChDataSaveTableList.ini"
    Public Const gCstIniFileNameComboChSioDetail As String = "ComboChSioDetail.ini"
    Public Const gCstIniFileNameComboChCtrlUseNotuseDetail As String = "ComboChControlUseNotuseDetail.ini"

    ''シーケンス設定のロジック
    Public Const gCstIniFileNameListSeqLogic As String = "ListSeqLogic.ini"

    ''OPSスクリーンタイトル
    Public Const gCstIniFileNameListOpsScreenTitle As String = "ListOpsScreenTitle.ini"
    Public Const gCstIniFileNameListOpsScreenTitleJpn As String = "ListOpsScreenTitleJpn.ini"  ''全和文追加　hori 20200603

    ''OPSセレクションメニュー
    Public Const gCstIniFileNameListOpsSelectionMenu As String = "ListOpsSelectionMenu.ini"
    Public Const gCstIniFileNameListOpsSelectionMenuJpn As String = "ListOpsSelectionMenuJpn.ini"  ''全和文追加　hori 20200603

    ''システムCH情報
    Public Const gCstIniFileNameListChSystem As String = "ListChSystemChannel.ini"
    Public Const gCstIniFileNameListChSystemJpn As String = "ListChSystemChannelJpn.ini"  ''全和文追加　hori 20200603

#End Region

#Region "チャンネル一覧カラム位置 Ver2.0.0.9 BACKUP"
    ''チャンネル一覧カラム位置
    'Public Const gCstChListColPosChType As Integer = 0
    'Public Const gCstChListColPosSysNo As Integer = 1
    'Public Const gCstChListColPosChNo As Integer = 2
    'Public Const gCstChListColPosTagRow As Integer = 3
    'Public Const gCstChListColPosItemName As Integer = 4
    'Public Const gCstChListColPosStatusIn As Integer = 5
    'Public Const gCstChListColPosRangeLo As Integer = 6
    'Public Const gCstChListColPosRangeHi As Integer = 7
    'Public Const gCstChListColPosUnit As Integer = 8
    'Public Const gCstChListColPosValueH As Integer = 9
    'Public Const gCstChListColPosExtGrH As Integer = 10
    'Public Const gCstChListColPosDelayH As Integer = 11
    'Public Const gCstChListColPosGrep1H As Integer = 12
    'Public Const gCstChListColPosGrep2H As Integer = 13
    'Public Const gCstChListColPosValueL As Integer = 14
    'Public Const gCstChListColPosExtGrL As Integer = 15
    'Public Const gCstChListColPosDelayL As Integer = 16
    'Public Const gCstChListColPosGrep1L As Integer = 17
    'Public Const gCstChListColPosGrep2L As Integer = 18
    'Public Const gCstChListColPosOutExtgH As Integer = 19
    'Public Const gCstChListColPosOutDelayH As Integer = 20
    'Public Const gCstChListColPosOutGrep1H As Integer = 21
    'Public Const gCstChListColPosOutGrep2H As Integer = 22
    'Public Const gCstChListColPosValueHH As Integer = 23
    'Public Const gCstChListColPosExtGrHH As Integer = 24
    'Public Const gCstChListColPosDelayHH As Integer = 25
    'Public Const gCstChListColPosGrep1HH As Integer = 26
    'Public Const gCstChListColPosGrep2HH As Integer = 27
    'Public Const gCstChListColPosValueLL As Integer = 28
    'Public Const gCstChListColPosExtGrLL As Integer = 29
    'Public Const gCstChListColPosDelayLL As Integer = 30
    'Public Const gCstChListColPosGrep1LL As Integer = 31
    'Public Const gCstChListColPosGrep2LL As Integer = 32
    'Public Const gCstChListColPosDataType As Integer = 33
    'Public Const gCstChListColPosSSig As Integer = 34
    'Public Const gCstChListColPosFuAddress As Integer = 35
    'Public Const gCstChListColPosPortAddress As Integer = 36
    'Public Const gCstChListColPosPinAddress As Integer = 37
    'Public Const gCstChListColPosFlagRL As Integer = 38
    'Public Const gCstChListColPosExtGrSF As Integer = 39
    'Public Const gCstChListColPosDelaySF As Integer = 40
    'Public Const gCstChListColPosRemarks As Integer = 41
    'Public Const gCstChListColPosLRRow As Integer = 42
    'Public Const gCstChListColPosNormalLo As Integer = 43
    'Public Const gCstChListColPosNormalHi As Integer = 44
    'Public Const gCstChListColPosFlagDmy As Integer = 45
    'Public Const gCstChListColPosFlagSC As Integer = 46
    'Public Const gCstChListColPosDelayTime As Integer = 47
    'Public Const gCstChListColPosFlagEP As Integer = 48
    'Public Const gCstChListColPosFlagAC As Integer = 49
    'Public Const gCstChListColPosFlagPLC As Integer = 50
    'Public Const gCstChListPowerFactorRow As Integer = 51
    'Public Const gCstChListColPosString As Integer = 52
    'Public Const gCstChListColPosCenterGraph As Integer = 53
    'Public Const gCstChListColPosEccFunc As Integer = 54
    'Public Const gCstChListColPosFlagSIO As Integer = 55
    'Public Const gCstChListColPosFlagGWS As Integer = 56
    'Public Const gCstChListColPosShareType As Integer = 57
    'Public Const gCstChListColPosShareChid As Integer = 58
    'Public Const gCstChListColPosStaNmHH As Integer = 59
    'Public Const gCstChListColPosStaNmH As Integer = 60
    'Public Const gCstChListColPosStaNmL As Integer = 61
    'Public Const gCstChListColPosStaNmLL As Integer = 62
    'Public Const gCstChListColPosOffset As Integer = 63
    'Public Const gCstChListColPosFilterCoef As Integer = 64
    'Public Const gCstChListColPosComposite As Integer = 65
    'Public Const gCstChListColPosAlarmTimeup As Integer = 66
    'Public Const gCstChListColPosDoStart As Integer = 67
    'Public Const gCstChListColPosPortDoStart As Integer = 68
    'Public Const gCstChListColPosPinDoStart As Integer = 69
    'Public Const gCstChListColPosBitCount As Integer = 70
    'Public Const gCstChListColPosControlType As Integer = 71
    'Public Const gCstChListColPosPulseWidth As Integer = 72
    'Public Const gCstChListColPosStatusOut As Integer = 73
    'Public Const gCstChListColPosDoStatus1 As Integer = 74
    'Public Const gCstChListColPosDoStatus2 As Integer = 75
    'Public Const gCstChListColPosDoStatus3 As Integer = 76
    'Public Const gCstChListColPosDoStatus4 As Integer = 77
    'Public Const gCstChListColPosDoStatus5 As Integer = 78
    'Public Const gCstChListColPosDoStatus6 As Integer = 79
    'Public Const gCstChListColPosDoStatus7 As Integer = 80
    'Public Const gCstChListColPosDoStatus8 As Integer = 81
    'Public Const gCstChListColPosOutSp1 As Integer = 82
    'Public Const gCstChListColPosOutSp2 As Integer = 83
    'Public Const gCstChListColPosOutHys1 As Integer = 84
    'Public Const gCstChListColPosOutHys2 As Integer = 85
    'Public Const gCstChListColPosOutSt As Integer = 86
    'Public Const gCstChListColPosOutVar As Integer = 87
    'Public Const gCstChListColPosOutStatusH As Integer = 88

    ' ''↓非表示エリア
    'Public Const gCstChListColPosCompositeIndex As Integer = 89
    'Public Const gCstChListColPosIndex As Integer = 90
    'Public Const gCstChListColPosTriggerCH As Integer = 91
    'Public Const gCstChListColPosCopyRow As Integer = 92
    'Public Const gCstChListColPosDeviceStatus As Integer = 93
    'Public Const gCstChListColPosRangeType As Integer = 94
    'Public Const gCstChListColPosValueSF As Integer = 95
    'Public Const gCstChListColPosGrep1SF As Integer = 96
    'Public Const gCstChListColPosGrep2SF As Integer = 97
    'Public Const gCstChListColPosOutDelayTime As Integer = 98
    'Public Const gCstChListColPosStatusAlarm As Integer = 99
    'Public Const gCstChListColPosStaNmSF As Integer = 100
    'Public Const gCstChListColPosFlagWK As Integer = 101
    'Public Const gCstChListColPosFlagSP As Integer = 102
    'Public Const gCstChListColPosPortBitCount As Integer = 103
    'Public Const gCstChListColPosPinBitCount As Integer = 104
    'Public Const gCstChListColPosDiStart As Integer = 105
    'Public Const gCstChListColPosPortDiStart As Integer = 106
    'Public Const gCstChListColPosPinDiStart As Integer = 107
    'Public Const gCstChListColPosAiTerm As Integer = 108
    'Public Const gCstChListColPosPortAiTerm As Integer = 109
    'Public Const gCstChListColPosPinAiTerm As Integer = 110
    'Public Const gCstChListColPosAoTerm As Integer = 111
    'Public Const gCstChListColPosPortAoTerm As Integer = 112
    'Public Const gCstChListColPosPinAoTerm As Integer = 113
    'Public Const gCstChListColPosPortNo As Integer = 114
    'Public Const gCstChListColPosDecimalPoint As Integer = 115

    ''Ver2.0.0.2 南日本M761対応 2017.02.27追加
    'Public Const gCstChListColPosAlmMimic As Integer = 116

    ''-------------------------------------------
#End Region
#Region "チャンネル一覧カラム位置"
    'Ver2.0.0.9 FeedBack を一か所にまとめる＝19～22,66を、82の前へ
    ''チャンネル一覧カラム位置
    Public Const gCstChListColPosChType As Integer = 0
    Public Const gCstChListColPosSysNo As Integer = 1
    Public Const gCstChListColPosChNo As Integer = 2
    Public Const gCstChListColPosTagRow As Integer = 3
    Public Const gCstChListColPosItemName As Integer = 4
    Public Const gCstChListColPosStatusIn As Integer = 5
    Public Const gCstChListColPosRangeLo As Integer = 6
    Public Const gCstChListColPosRangeHi As Integer = 7
    Public Const gCstChListColPosUnit As Integer = 8
    Public Const gCstChListColPosValueH As Integer = 9
    Public Const gCstChListColPosExtGrH As Integer = 10
    Public Const gCstChListColPosDelayH As Integer = 11
    Public Const gCstChListColPosGrep1H As Integer = 12
    Public Const gCstChListColPosGrep2H As Integer = 13
    Public Const gCstChListColPosValueL As Integer = 14
    Public Const gCstChListColPosExtGrL As Integer = 15
    Public Const gCstChListColPosDelayL As Integer = 16
    Public Const gCstChListColPosGrep1L As Integer = 17
    Public Const gCstChListColPosGrep2L As Integer = 18
    Public Const gCstChListColPosValueHH As Integer = 19
    Public Const gCstChListColPosExtGrHH As Integer = 20
    Public Const gCstChListColPosDelayHH As Integer = 21
    Public Const gCstChListColPosGrep1HH As Integer = 22
    Public Const gCstChListColPosGrep2HH As Integer = 23
    Public Const gCstChListColPosValueLL As Integer = 24
    Public Const gCstChListColPosExtGrLL As Integer = 25
    Public Const gCstChListColPosDelayLL As Integer = 26
    Public Const gCstChListColPosGrep1LL As Integer = 27
    Public Const gCstChListColPosGrep2LL As Integer = 28
    Public Const gCstChListColPosDataType As Integer = 29
    Public Const gCstChListColPosSSig As Integer = 30
    Public Const gCstChListColPosFuAddress As Integer = 31
    Public Const gCstChListColPosPortAddress As Integer = 32
    Public Const gCstChListColPosPinAddress As Integer = 33
    Public Const gCstChListColPosFlagRL As Integer = 34
    Public Const gCstChListColPosExtGrSF As Integer = 35
    Public Const gCstChListColPosDelaySF As Integer = 36
    Public Const gCstChListColPosRemarks As Integer = 37
    Public Const gCstChListColPosLRRow As Integer = 38
    Public Const gCstChListColPosNormalLo As Integer = 39
    Public Const gCstChListColPosNormalHi As Integer = 40
    Public Const gCstChListColPosFlagDmy As Integer = 41
    Public Const gCstChListColPosFlagSC As Integer = 42
    Public Const gCstChListColPosDelayTime As Integer = 43
    Public Const gCstChListColPosFlagEP As Integer = 44
    Public Const gCstChListColPosFlagAC As Integer = 45
    Public Const gCstChListColPosFlagPLC As Integer = 46
    Public Const gCstChListPowerFactorRow As Integer = 47
    Public Const gCstChListColPosString As Integer = 48
    Public Const gCstChListColPosCenterGraph As Integer = 49
    Public Const gCstChListColPosEccFunc As Integer = 50
    Public Const gCstChListColPosFlagSIO As Integer = 51
    Public Const gCstChListColPosFlagGWS As Integer = 52
    Public Const gCstChListColPosShareType As Integer = 53
    Public Const gCstChListColPosShareChid As Integer = 54
    Public Const gCstChListColPosStaNmHH As Integer = 55
    Public Const gCstChListColPosStaNmH As Integer = 56
    Public Const gCstChListColPosStaNmL As Integer = 57
    Public Const gCstChListColPosStaNmLL As Integer = 58
    Public Const gCstChListColPosOffset As Integer = 59
    Public Const gCstChListColPosFilterCoef As Integer = 60
    Public Const gCstChListColPosComposite As Integer = 61
    Public Const gCstChListColPosDoStart As Integer = 62
    Public Const gCstChListColPosPortDoStart As Integer = 63
    Public Const gCstChListColPosPinDoStart As Integer = 64
    Public Const gCstChListColPosBitCount As Integer = 65
    Public Const gCstChListColPosControlType As Integer = 66
    Public Const gCstChListColPosPulseWidth As Integer = 67
    Public Const gCstChListColPosStatusOut As Integer = 68
    Public Const gCstChListColPosDoStatus1 As Integer = 69
    Public Const gCstChListColPosDoStatus2 As Integer = 70
    Public Const gCstChListColPosDoStatus3 As Integer = 71
    Public Const gCstChListColPosDoStatus4 As Integer = 72
    Public Const gCstChListColPosDoStatus5 As Integer = 73
    Public Const gCstChListColPosDoStatus6 As Integer = 74
    Public Const gCstChListColPosDoStatus7 As Integer = 75
    Public Const gCstChListColPosDoStatus8 As Integer = 76
    Public Const gCstChListColPosOutExtgH As Integer = 77
    Public Const gCstChListColPosOutDelayH As Integer = 78
    Public Const gCstChListColPosOutGrep1H As Integer = 79
    Public Const gCstChListColPosOutGrep2H As Integer = 80
    Public Const gCstChListColPosAlarmTimeup As Integer = 81
    Public Const gCstChListColPosOutSp1 As Integer = 82
    Public Const gCstChListColPosOutSp2 As Integer = 83
    Public Const gCstChListColPosOutHys1 As Integer = 84
    Public Const gCstChListColPosOutHys2 As Integer = 85
    Public Const gCstChListColPosOutSt As Integer = 86
    Public Const gCstChListColPosOutVar As Integer = 87
    Public Const gCstChListColPosOutStatusH As Integer = 88

    ''↓非表示エリア
    Public Const gCstChListColPosCompositeIndex As Integer = 89
    Public Const gCstChListColPosIndex As Integer = 90
    Public Const gCstChListColPosTriggerCH As Integer = 91
    Public Const gCstChListColPosCopyRow As Integer = 92
    Public Const gCstChListColPosDeviceStatus As Integer = 93
    Public Const gCstChListColPosRangeType As Integer = 94
    Public Const gCstChListColPosValueSF As Integer = 95
    Public Const gCstChListColPosGrep1SF As Integer = 96
    Public Const gCstChListColPosGrep2SF As Integer = 97
    Public Const gCstChListColPosOutDelayTime As Integer = 98
    Public Const gCstChListColPosStatusAlarm As Integer = 99
    Public Const gCstChListColPosStaNmSF As Integer = 100
    Public Const gCstChListColPosFlagWK As Integer = 101
    Public Const gCstChListColPosFlagSP As Integer = 102
    Public Const gCstChListColPosPortBitCount As Integer = 103
    Public Const gCstChListColPosPinBitCount As Integer = 104
    Public Const gCstChListColPosDiStart As Integer = 105
    Public Const gCstChListColPosPortDiStart As Integer = 106
    Public Const gCstChListColPosPinDiStart As Integer = 107
    Public Const gCstChListColPosAiTerm As Integer = 108
    Public Const gCstChListColPosPortAiTerm As Integer = 109
    Public Const gCstChListColPosPinAiTerm As Integer = 110
    Public Const gCstChListColPosAoTerm As Integer = 111
    Public Const gCstChListColPosPortAoTerm As Integer = 112
    Public Const gCstChListColPosPinAoTerm As Integer = 113
    Public Const gCstChListColPosPortNo As Integer = 114
    Public Const gCstChListColPosDecimalPoint As Integer = 115

    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
    Public Const gCstChListColPosAlmMimic As Integer = 116

    'ver2.0.8.C 2018.11.14
    Public Const gCstChListColPosFlagMotorColor As Integer = 117

    ''-------------------------------------------
#End Region


#Region "SIGNAL POSITON設定"

    Public Const gCstChListSSigJacom As String = "J"
    Public Const gCstChListSSigCOMM As String = "R"
    Public Const gCstChListSSigSystem As String = "S"
    Public Const gCstChListSSigWork As String = "W"

#End Region

#Region "仮設定関連"

    ''仮設定キー
    Public Const gCstDummySetKey As System.Windows.Forms.Keys = Keys.F5

    ''仮設定色（Color型はConstで定義できないので通常変数）
    Public gCstDummySetColorDummy As Color = Color.Orange
    Public gCstDummySetColorNormal As Color = Color.White

#End Region

End Module
