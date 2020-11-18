Public Class frmPrtTerminalPreview

#Region "定数定義"

    ''１行あたりの最大文字数
    Private Const mCstCodeMaxLength18 As Integer = 18
    Private Const mCstCodeMaxLength14 As Integer = 14
    Private Const mCstCodeMaxLength12 As Integer = 12
    Private Const mCstCodeMaxLength10 As Integer = 10

    ''印字行
    Private Const mCstCodePrintLine1 As Integer = 1
    Private Const mCstCodePrintLine2 As Integer = 2
    Private Const mCstCodePrintLine3 As Integer = 3

    ''PictureBox高さ
    Private Const mCstCodePicboxHight As Single = 1150

#End Region

#Region "変数定義"

    ''構造体定義
    Private mudtFuInfo() As gTypFuInfo = Nothing
    Private udtTerminalPageInfo() As gTypPrintTerminalInfo = Nothing

    ''総ページ数
    Private mintPageMax As Integer

    '現在表示しているページインデックス
    Private mintPageIndex As Integer

    'ページ指定印刷時の末尾ページ番号
    Private mintPageIndexEnd As Integer

    ''全印刷か否か（True：全印刷、False：表示分のみ印刷）
    Private mblnPrintAll As Boolean

    ''印刷設定（mh***：frmPrtTerminalからの引数）
    Private mhblnPrtModePrint As Boolean                ''印刷モード        （True:Print  False:Preview）
    Private mhblnPrintRangeAll As Boolean               ''印刷範囲フラグ    （True:ALL    False:Pages）  
    Private mhintPageFrom As Integer                    ''印刷開始ページ
    Private mhintPageTo As Integer                      ''印刷終了ページ
    Private mhintScFlg As Integer                       ''隠しCHフラグ      （True:Secret Channelを含む  False:含まない）
    Private mhintDmyFlg As Integer                      ''ダミーCHフラグ    （True:Secret Channelを含む  False:含まない）
    Private mhintPagePrint As Integer                   ''ページ印刷     　 （True:ページ印刷する  False:ページ印刷しない）
    Private mhintFuNameType As Integer                  ''FIELD UNIT名称表記（True:和文  False:英文） 2013.11.15

    'Ver2.0.0.2 基板指定印刷用変数
    Private mintFuNo As Integer     '指定FuNo
    Private mintSlotNo As Integer   '指定SlotNo
    Private mintFuNoTo As Integer   '指定FuNo To
    Private mintSlotNoTo As Integer '指定SlotNo To

    Private mstrOrAndStr As String = ""
    Private mintOrAndKI As Integer = 0

    'Ver2.0.0.7 DRAWING NOから数値のみを取得するための正規表現
    Private reg As New System.Text.RegularExpressions.Regex("[^\d]")

#Region "ラベル設定"

    ''==========================
    '' 端子台
    ''==========================
  
    ''和文仕様　20200217 hori
    Private Const mCstLabelFulistNoJpn As String = "番号"
    Private Const mCstLabelFulistFuName1Jpn As String = "フィールドコントロールユニット/"
    Private Const mCstLabelFulistFuName2Jpn As String = "フィールドユニット名称"
    Private Const mCstLabelFulistNamePlateJpn As String = "銘板型式"
    Private Const mCstLabelFulistFuTypeJpn As String = "FCU/FU 型式"
    Private Const mCstLabelFulistCodeNo1Jpn As String = "コード"
    Private Const mCstLabelFulistCodeNo2Jpn As String = "番号"
    Private Const mCstLabelFulistRemarksJpn As String = "備考"

    Private Const mCstLabelFulistNo As String = "NO"
    Private Const mCstLabelFulistFuName As String = "FCU/FU NAME"
    Private Const mCstLabelFulistNamePlate As String = "NAME PLATE"
    Private Const mCstLabelFulistFuType As String = "FCU/FU TYPE"
    Private Const mCstLabelFulistCodeNo1 As String = "CODE"
    Private Const mCstLabelFulistCodeNo2 As String = "NO"
    Private Const mCstLabelFulistRemarks As String = "REMARKS"


  
    ''==========================
    '' デジタル
    ''==========================
  
    ''和文仕様変更　20200217 hori
    Private Const mCstLabelDigitalAddJpn As String = "ADD"
    Private Const mCstLabelDigitalAdd1Jpn As String = "アド"
    Private Const mCstLabelDigitalAdd2Jpn As String = "レス"
    'Private Const mCstLabelDigitalCh As String = "CHNO"    ' 2015.10.16 ﾀｸﾞ表示
    'Private Const mCstLabelDigitalCh As String = "TAGNO"
    Public mCstLabelDigitalCh1Jpn As String = "CH"
    Public mCstLabelDigitalCh2Jpn As String = "番号"
    Private Const mCstLabelDigitalItemJpn As String = "名称"
    Private Const mCstLabelDigitalInCore1Jpn As String = "IN"
    Private Const mCstLabelDigitalInCore2Jpn As String = "芯線"
    Private Const mCstLabelDigitalInCore3Jpn As String = "番号"
    Private Const mCstLabelDigitalInTerm1Jpn As String = "IN"
    Private Const mCstLabelDigitalInTerm2Jpn As String = "端子"
    Private Const mCstLabelDigitalInTerm3Jpn As String = "番号"
    Private Const mCstLabelDigitalSignalJpn As String = "信号"
    Private Const mCstLabelDigitalComTerm1Jpn As String = "COM"
    Private Const mCstLabelDigitalComTerm2Jpn As String = "端子"
    Private Const mCstLabelDigitalComTerm3Jpn As String = "番号"
    Private Const mCstLabelDigitalComCore1Jpn As String = "COM"
    Private Const mCstLabelDigitalComCore2Jpn As String = "芯線"
    Private Const mCstLabelDigitalComCore3Jpn As String = "番号"
    Private Const mCstLabelDigitalCableJpn As String = "ケーブル (ワイヤーマーク)"
    Private Const mCstLabelDigitalMarkJpn As String = "符号(IN)"
    Private Const mCstLabelDigitalClassJpn As String = "種類(COM)"
    Private Const mCstLabelDigitalDistJpn As String = "相手名称"       '' Ver1.8.4  2015.11.27  DEST → DIST

    Private Const mCstLabelDigitalAdd As String = "ADD"
    'Private Const mCstLabelDigitalCh As String = "CHNO"    ' 2015.10.16 ﾀｸﾞ表示
    'Private Const mCstLabelDigitalCh As String = "TAGNO"
    Public mCstLabelDigitalCh As String = "CHNO"        ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    Private Const mCstLabelDigitalItem As String = "ITEM NAME"
    Private Const mCstLabelDigitalInCore1 As String = "IN"
    Private Const mCstLabelDigitalInCore2 As String = "CORE"
    Private Const mCstLabelDigitalInCore3 As String = "NO"
    Private Const mCstLabelDigitalInTerm1 As String = "IN"
    Private Const mCstLabelDigitalInTerm2 As String = "TERM"
    Private Const mCstLabelDigitalInTerm3 As String = "NO"
    Private Const mCstLabelDigitalSignal As String = "SIGNAL"
    Private Const mCstLabelDigitalComTerm1 As String = "COM"
    Private Const mCstLabelDigitalComTerm2 As String = "TERM"
    Private Const mCstLabelDigitalComTerm3 As String = "NO"
    Private Const mCstLabelDigitalComCore1 As String = "COM"
    Private Const mCstLabelDigitalComCore2 As String = "CORE"
    Private Const mCstLabelDigitalComCore3 As String = "NO"
    Private Const mCstLabelDigitalCable As String = "CABLE (WIRE MARK)"
    Private Const mCstLabelDigitalMark As String = "MARK(IN)"
    Private Const mCstLabelDigitalClass As String = "CLASS(COM)"
    Private Const mCstLabelDigitalDist As String = "DIST"       '' Ver1.8.4  2015.11.27  DEST → DIST




    ''==========================
    '' アナログ
    ''==========================
    ''共通名称

    ''和文仕様 20200217 hori
    Private Const mCstLabelAnalogAddJpn As String = "ADD"
    Public mCstLabelAnalogCh As String = "CHNO"      ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    Private Const mCstLabelAnalogAdd1Jpn As String = "アド"
    Private Const mCstLabelAnalogAdd2Jpn As String = "レス"
    'Private Const mCstLabelAnalogCh As String = "CHNO"     ' Ver1.7.4.2 2015.10.19 ﾀｸﾞ表示
    'Private Const mCstLabelAnalogCh As String = "TAGNO"
    Public mCstLabelAnalogCh1Jpn As String = "CH"
    Public mCstLabelAnalogCh2Jpn As String = "番号"
    Private Const mCstLabelAnalogItemNameJpn As String = "名称"
    Private Const mCstLabelAnalogSigJpn As String = "SIG"
    Private Const mCstLabelAnalogCoreNo1Jpn As String = "芯線"
    Private Const mCstLabelAnalogCoreNo2Jpn As String = "符号"
    Private Const mCstLabelAnalogTermNo1Jpn As String = "端子"
    Private Const mCstLabelAnalogTermNo2Jpn As String = "番号"
    Private Const mCstLabelAnalogStatusJpn As String = "信号"
    Private Const mCstLabelAnalogDistJpn As String = "相手名称"

    ''2線式
    Private Const mCstLabelAnalog2LineCableJpn As String = "ケーブル (ワイヤーマーク)"
    Private Const mCstLabelAnalog2LineMarkJpn As String = "符号(IN)"
    Private Const mCstLabelAnalog2LineClassJpn As String = "種類(COM)"

    ''3線式
    Private Const mCstLabelAnalog3LineCableJpn As String = "ケーブル"
    Private Const mCstLabelAnalog3LineMarkJpn As String = "符号"
    Private Const mCstLabelAnalog3LineClassJpn As String = "種類"

    ''AO基板
    Private Const mCstLabelAOLineCableJpn As String = "ケーブル"
    Private Const mCstLabelAOLineMarkJpn As String = "OUT(+) (LOW)"
    Private Const mCstLabelAOLineClassJpn As String = "COM(-) (UPP)"

    Private Const mCstLabelAnalogAdd As String = "ADD"
    'Private Const mCstLabelAnalogCh As String = "CHNO"     ' Ver1.7.4.2 2015.10.19 ﾀｸﾞ表示
    'Private Const mCstLabelAnalogCh As String = "TAGNO"
    Private Const mCstLabelAnalogItemName As String = "ITEM NAME"
    Private Const mCstLabelAnalogSig As String = "SIG"
    Private Const mCstLabelAnalogCoreNo1 As String = "CORE"
    Private Const mCstLabelAnalogCoreNo2 As String = "NO"
    Private Const mCstLabelAnalogTermNo1 As String = "TERM"
    Private Const mCstLabelAnalogTermNo2 As String = "NO"
    Private Const mCstLabelAnalogStatus As String = "SIGNAL"
    Private Const mCstLabelAnalogRange As String = "RANGE"
    Private Const mCstLabelAnalogDist As String = "DIST"       '' Ver1.8.4  2015.11.27  DEST → DIST

    ''2線式
    Private Const mCstLabelAnalog2LineCable As String = "CABLE (WIRE MARK)"
    Private Const mCstLabelAnalog2LineMark As String = "MARK(IN)"
    Private Const mCstLabelAnalog2LineClass As String = "CLASS(COM)"

    ''3線式
    Private Const mCstLabelAnalog3LineCable As String = "CABLE"
    Private Const mCstLabelAnalog3LineMark As String = "MARK"
    Private Const mCstLabelAnalog3LineClass As String = "CLASS"

    ''AO基板
    Private Const mCstLabelAOLineCable As String = "CABLE"
    Private Const mCstLabelAOLineMark As String = "OUT(+) (LOW)"
    Private Const mCstLabelAOLineClass As String = "COM(-) (UPP)"

#End Region

#Region "サイズ・位置設定"

    ''==========================
    '' 端子台
    ''==========================
    ''フレーム余白サイズ設定
    Private Const mCstMarginLeft As Single = 55 '60 '110     2013.11.15
    Private Const mCstMarginUp As Single = 100

    Private Const mCstRowWidthFu As Single = 680 '600   ''行幅        2013.11.15
    Private Const mCstRowHightFu As Single = 30         ''行高

    ''画面左端から該当項目右までの長さ（X軸）                          　2013.11.15 調整
    Private Const mCstPosFuCol1 As Single = 100 '150    ''NO              
    Private Const mCstPosFuCol2 As Single = 310 '280    ''FIELD UNIT NAME  
    Private Const mCstPosFuCol3 As Single = 440 '410    ''NAME PLATE      
    Private Const mCstPosFuCol4 As Single = 560 '530    ''FIELD UNIT TYPE  
    Private Const mCstPosFuCol5 As Single = 600 '570    ''CODE NO         


    ''==========================
    '' フレームボックス
    ''==========================
    Private Const mCstFrameBoxWidth As Single = 265     ''幅
    Private Const mCstFrameBoxHight As Single = 25      ''高さ
    Private Const mCstFrameBoxTB As Single = 25         ''端子台名称位置
    Private Const mCstFrameBoxTime As Single = 650      ''時刻表示位置


    ''==========================
    '' デジタル
    ''==========================
    ''行高
    Private Const mCstRowHightDigitalHeader As Single = 35      ''ヘッダー
    Private Const mCstRowHightDigital As Single = 29            ''共通

    ''画面左端から該当項目右までの長さ（X軸）
    ''位置調整  2013.11.20
    ' 2015.10.16 ﾀｸﾞ表示
    'Private Const mCstPosDigitalAdd As Single = 65                   ''ADD
    'Private Const mCstPosDigitalAdd As Single = 45                   ''ADD   
    Public mCstPosDigitalAdd As Single                   ''ADD  ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    Private Const mCstPosDigitalChNo As Single = 95                  ''CHNO
    Private Const mCstPosDigitalItemName As Single = 305             ''ITEM NAME   
    Private Const mCstPosDigitalInCoreNo As Single = 335             ''IN CORE NO  
    Private Const mCstPosDigitalInTermNo As Single = 365             ''IN TERM NO 
    Private Const mCstPosDigitalStatus As Single = 425               ''STATUS      
    Private Const mCstPosDigitalComTermNo As Single = 455            ''COM TERM NO 
    Private Const mCstPosDigitalComCoreNo As Single = 485            ''COM CORE NO 
    Private Const mCstPosDigitalMarkNo As Single = 515               ''MARK NO    
    Private Const mCstPosDigitalMark As Single = 585 '590            ''MARK NAME  
    Private Const mCstPosDigitalClassNo As Single = 610 '615         ''CLASS NO    
    Private Const mCstPosDigitalClass As Single = 680 '690           ''CLASS NAME  

    Private Const mCstDigitalMsg_X As Single = 335                  '' DIGITAL端子のメッセージX座標
    Private Const mCstDigitalMsg_XJ As Single = 385                 '' DIGITAL端子のメッセージX座標
    Private Const mCstDigitalMsg_Y As Single = 1060                 '' DIGITAL端子のメッセージY座標


    ''==========================
    '' アナログ
    ''==========================
    ''行高
    Private Const mCstRowHightAnalogHeader As Single = 35       ''ヘッダー
    Private Const mCstRowHightAnalog2Line As Single = 51        ''2線式
    '2015/4/13 T.Ueki 高さ調整
    'Private Const mCstRowHightAnalog3Line As Single = 62        ''3線式
    Private Const mCstRowHightAnalog3Line As Single = 61        ''3線式

    ''２線式
    Private Const mCstRowCntAnalog2Mid As Integer = 1           ''中間線の本数
    Private Const mCstRowHightAnalog2Mid As Single = 25         ''中間線の高さ

    ''３線式
    Private Const mCstRowCntAnalog3Mid As Integer = 2           ''中間線の本数
    '2015/4/13 T.Ueki 高さ調整
    'Private Const mCstRowHightAnalog3Mid As Single = 20.6       ''中間線の高さ
    Private Const mCstRowHightAnalog3Mid As Single = 20.3       ''中間線の高さ

    ''画面左端から該当項目右までの長さ（X軸）
    ''位置調整  2013.11.20
    'Private Const mCstPosAnalogAdd As Single = 65                    ''ADD
    'Private Const mCstPosAnalogAdd As Single = 45                    ''ADD
    Public mCstPosAnalogAdd As Single ''ADD  ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    Private Const mCstPosAnalogChNo As Single = 95                   ''CHNO      
    Private Const mCstPosAnalogItemName As Single = 305              ''ITEM NAME 
    Private Const mCstPosAnalogSig As Single = 335                   ''SIG       
    Private Const mCstPosAnalogCoreNo As Single = 365                ''CORE NO   
    Private Const mCstPosAnalogTermNo As Single = 395                ''TERM NO   
    Private Const mCstPosAnalogStaus As Single = 485                 ''STATUS    
    'Private Const mCstPosAnalogRange As Single = 460                ''RANGE     
    Private Const mCstPosAnalogMarkNo As Single = 515                ''MARK NO   
    Private Const mCstPosAnalogMark As Single = 585 '590             ''MARK      
    Private Const mCstPosAnalogClassNo As Single = 610 '615          ''CLASS NO  
    Private Const mCstPosAnalogClass As Single = 680 '690            ''CLASS     

#End Region


#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 端子台情報構造体
    '           : ARG2 - (I ) 印刷設定（True:Print  False:Preview）
    '           : ARG3 - (I ) 印刷枚数（True:ALL    False:Pages）  
    '           : ARG4 - (I ) 印刷開始ページ
    '           : ARG5 - (I ) 印刷終了ページ
    '           : ARG6 - (I ) True:Secret Channelを含む  False:含まない
    '           : ARG7 - (I ) True:Dummy Dataを含む      False:含まない
    '           : ARG8 - (I ) Part選択（True:Machinery、False:Cargo）
    '           : ARG9 - (I ) 基板指定印刷用FuNo 無指定の場合 -1
    '           : ARG10- (I ) 基板指定印刷用SlotNo 無指定の場合 -1
    '           : ARG11- (I ) 基板指定印刷用FuNo To 無指定の場合 -1
    '           : ARG12- (I ) 基板指定印刷用SlotNo To 無指定の場合 -1
    ' 備考      : 
    ' 履歴      : パート選択の引数削除　ver.1.4.0 2011.08.17
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef hudtFuInfo() As gTypFuInfo, _
                     ByVal hblnPrintMode As Boolean, _
                     ByVal hblnPrintRange As Boolean, _
                     ByVal hintPageFrom As Integer, _
                     ByVal hintPageTo As Integer, _
                     ByVal hintScFlg As Integer, _
                     ByVal hintDmyFlg As Integer, _
                     ByVal hintPagePrint As Integer, _
                     ByVal hintFuNameType As Integer, _
                     Optional ByVal pintFuNo As Integer = -1, _
                     Optional ByVal pintSlotNo As Integer = -1, _
                     Optional ByVal pintFuNoTo As Integer = -1, _
                     Optional ByVal pintSlotNoTo As Integer = -1)
        'Ver2.0.0.2 基板指定印刷用に引数追加、ただし既存機能を考慮してoptionalとする
        'Ver2.0.2.9 基板指定印刷用Toに引数追加、ただし既存機能を考慮してoptionalとする
        Try

            ''引数保存
            mudtFuInfo = hudtFuInfo
            mhblnPrtModePrint = hblnPrintMode
            mhblnPrintRangeAll = hblnPrintRange
            mhintPageFrom = hintPageFrom
            mhintPageTo = hintPageTo
            mhintScFlg = hintScFlg
            mhintDmyFlg = hintDmyFlg
            mhintPagePrint = hintPagePrint
            mhintFuNameType = hintFuNameType    ''FIELD UNIT名称表記        2013.11.15

            'Ver2.0.0.2 基板指定印刷用変数格納
            mintFuNo = pintFuNo
            mintSlotNo = pintSlotNo
            mintFuNoTo = pintFuNoTo
            mintSlotNoTo = pintSlotNoTo

            ''本画面表示
            Me.ShowDialog()

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
    Private Sub frmPrtTerminalPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Call SetTermPrintSetting()  ' 2015.10.22 Ver1.7.5 CHNo./ﾀｸﾞ　初期化

            ''PictureBoxのサイズ設定
            picPreview.Height = mCstCodePicboxHight

            Call gGetDigData()         ' Ver1.9.3 2016.01.12 ｱﾅﾛｸﾞ以外のﾃﾞｰﾀを再作成


            'Ver2.0.0.2 LU総ページ数取得
            Call subLUgetPageMax()


            ''印刷情報の取得
            Call mGetPrintPage(mudtFuInfo, udtTerminalPageInfo, mintPageMax, mhintScFlg)


            'Ver2.0.0.2 基板指定印刷用処理
            Dim intSetPageNo As Integer = 0
            If mintFuNo <> -1 Then
                '指定あり
                For i As Integer = 0 To UBound(udtTerminalPageInfo) Step 1
                    With udtTerminalPageInfo(i)
                        '指定Fu,Slotと一致するページ番号とする
                        If .intFuNo = mintFuNo And .intSlotNo = mintSlotNo Then
                            intSetPageNo = i
                            Exit For
                        End If
                    End With
                Next i
            End If

            'FU番号を設定
            Dim strFUpate As String = ""
            If udtTerminalPageInfo(intSetPageNo).intFuNo.ToString >= 0 Then
                strFUpate = udtTerminalPageInfo(intSetPageNo).intFuNo.ToString
            End If
            txtFUpage.Text = strFUpate

            'DRAWING NOを設定
            Dim strDrawNo As String = reg.Replace(NZf(udtTerminalPageInfo(intSetPageNo).strDrawinfNoInfo), "")
            If strDrawNo.Trim <> "" Then
                strDrawNo = CInt(strDrawNo)
            Else
                strDrawNo = strDrawNo.Trim
            End If
            txtDrawingNo.Text = strDrawNo

            ''最大ページ数の設定
            txtMaxPages.Text = mintPageMax

            ''ページカウントのリセット
            mintPageIndex = intSetPageNo '0     'Ver2.0.0.2 基板指定時の開始ページ 
            txtPage.Text = mintPageIndex + 1

            '全ページ印刷フラグを初期化
            mblnPrintAll = False

            ''ページ指定印刷
            If mhblnPrtModePrint = True Then

                'Ver2.0.1.3
                'FuAdr指定で、印刷処理ならばFromToを設定
                Dim blAdr As Boolean = False
                If mintFuNo <> -1 Then
                    For i As Integer = 0 To UBound(udtTerminalPageInfo) Step 1
                        With udtTerminalPageInfo(i)
                            '指定Fu,Slotと一致するページ番号とする
                            If .intFuNo = mintFuNo And .intSlotNo = mintSlotNo Then
                                mhintPageFrom = i + 1
                                blAdr = True
                                Exit For
                            End If
                        End With
                    Next i
                    If blAdr = True Then
                        Dim intFuNoTo As Integer = mintFuNo
                        Dim intSlotNoTo As Integer = mintSlotNo
                        Dim blTo As Boolean = True
                        If mintFuNoTo <> -1 Then
                            intFuNoTo = mintFuNoTo
                            intSlotNoTo = mintSlotNoTo
                            blTo = False
                        End If
                        'Toページ取得
                        For j = mhintPageFrom To UBound(udtTerminalPageInfo) Step 1
                            With udtTerminalPageInfo(j)
                                If .intFuNo = intFuNoTo And .intSlotNo = intSlotNoTo Then
                                    blTo = True
                                Else
                                    If blTo = True Then
                                        mhintPageTo = j - 1
                                        Exit For
                                    End If
                                End If
                            End With
                        Next j
                    Else
                        '存在しないｸﾞﾙｰﾌﾟ指定の場合全ページ印刷となる
                        mhblnPrintRangeAll = True
                    End If
                Else
                    'Ver2.0.2.6 ｱﾄﾞﾚｽではなくページ番号指定
                    'PageFromはあらかじめ-1されているため、以降の矛盾を防ぐために+1
                    mhintPageFrom = mhintPageFrom + 1
                End If

                Call cmdAllPrint_Click(cmdAllPrint, New EventArgs)
                Me.Close()
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtTerminalPreview_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [>>] ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextPage.Click

        Try

            If (mintPageIndex + 1) >= mintPageMax Then Return

            mintPageIndex += 1

            txtPage.Text = CStr(mintPageIndex + 1)

            'FU番号を設定
            Dim strFUpate As String = ""
            If udtTerminalPageInfo(mintPageIndex).intFuNo.ToString >= 0 Then
                strFUpate = udtTerminalPageInfo(mintPageIndex).intFuNo.ToString
            End If
            txtFUpage.Text = strFUpate

            'DRAWING NOを設定
            Dim strDrawNo As String = reg.Replace(NZf(udtTerminalPageInfo(mintPageIndex).strDrawinfNoInfo), "")
            If strDrawNo.Trim <> "" Then
                strDrawNo = CInt(strDrawNo)
            Else
                strDrawNo = strDrawNo.Trim
            End If
            txtDrawingNo.Text = strDrawNo


            picPreview.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [<<] ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdBeforePage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeforePage.Click

        Try

            If (mintPageIndex + 1) <= 1 Then Return

            mintPageIndex = mintPageIndex - 1

            txtPage.Text = CStr(mintPageIndex + 1)

            'FU番号を設定
            Dim strFUpate As String = ""
            If udtTerminalPageInfo(mintPageIndex).intFuNo.ToString >= 0 Then
                strFUpate = udtTerminalPageInfo(mintPageIndex).intFuNo.ToString
            End If
            txtFUpage.Text = strFUpate


            'DRAWING NOを設定
            Dim strDrawNo As String = reg.Replace(NZf(udtTerminalPageInfo(mintPageIndex).strDrawinfNoInfo), "")
            If strDrawNo.Trim <> "" Then
                strDrawNo = CInt(strDrawNo)
            Else
                strDrawNo = strDrawNo.Trim
            End If
            txtDrawingNo.Text = strDrawNo


            picPreview.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Paintイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub picPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPreview.Paint

        Try

            ''グラフ作成 
            Call mDrawGraphics(e.Graphics, udtTerminalPageInfo(mintPageIndex))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Pages Print ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPagesPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPagesPrint.Click

        Try

            ''印刷確認で「キャンセル」を選択した時は処理を抜ける
            'If MessageBox.Show("Do you start printing?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Return

            'PrintDialogオブジェクトの作成
            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   'ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            '印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                '全ページ印刷フラグをFalse
                mblnPrintAll = False

                'PrintDocumentオブジェクト作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage


                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = False
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''印刷設定反映
                pd.PrinterSettings = PrintDialog1.PrinterSettings

                ''余白設定  2013.11.06
                pd.DefaultPageSettings.Margins.Top = 8 '20
                pd.DefaultPageSettings.Margins.Left = 42 '20
                pd.DefaultPageSettings.Margins.Right = 8 '20
                pd.DefaultPageSettings.Margins.Bottom = 20 '20

                '印刷を開始する
                pd.Print()

                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： All Print ボタンクリック（ページ指定と全印刷あり）
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAllPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllPrint.Click

        Try

            '' Ver1.10.1 2016.02.26 ﾌﾟﾘﾝﾀﾀﾞｲｱﾛｸﾞでｷｬﾝｾﾙできるので、確認ﾒｯｾｰｼﾞを表示しないように変更
            ''印刷確認で「キャンセル」を選択した時は処理を抜ける
            ''If MessageBox.Show("Do you start printing?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            ''PrintDialogオブジェクト作成
            Dim PrintDialog1 As New PrintDialog()
            PrintDialog1.AllowPrintToFile = False   'ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                ''PrintDocumentオブジェクト作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                ''PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = False
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''印刷設定反映
                pd.PrinterSettings = PrintDialog1.PrinterSettings

                ''余白設定  2013.11.06
                pd.DefaultPageSettings.Margins.Top = 8 '20
                pd.DefaultPageSettings.Margins.Left = 42 '20
                pd.DefaultPageSettings.Margins.Right = 8 '20
                pd.DefaultPageSettings.Margins.Bottom = 20 '20

                If mhblnPrintRangeAll = False Then '============================================================================

                    '------------------
                    ''ページ指定印刷 
                    '-----------------
                    ''印刷ページの設定
                    mintPageIndex = mhintPageFrom - 1
                    mintPageIndexEnd = mhintPageTo + 1

                    ''ページ印刷
                    pd.Print()

                Else

                    '------------------
                    ''全画面印刷
                    '------------------
                    mblnPrintAll = True

                    ''表示している画面Indexの保持
                    Dim intPageBuf As Integer = mintPageIndex

                    ''印刷開始ページの設定（1ページ目から印刷するためIndexを0にする）
                    mintPageIndex = 0

                    ''ページ印刷
                    pd.Print()

                    ''画面Indexを戻す
                    mintPageIndex = intPageBuf

                End If '=========================================================================================================

                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ページ印刷（ページ指定印刷と全印刷）
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 印刷を行う
    '--------------------------------------------------------------------
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            If mhblnPrintRangeAll = False Then

                ''-----------------------
                '' ページ指定印刷
                ''-----------------------
                Call mDrawGraphics(e.Graphics, udtTerminalPageInfo(mintPageIndex))

                If (mintPageIndex + 1) >= mintPageIndexEnd Then
                    mintPageIndex = 1
                    e.HasMorePages = False
                Else
                    mintPageIndex += 1
                    e.HasMorePages = True
                End If

            ElseIf Not mblnPrintAll Then

                ''-----------------------
                '' 表示ページのみ印刷
                ''-----------------------
                Call mDrawGraphics(e.Graphics, udtTerminalPageInfo(mintPageIndex))

            Else

                ''-----------------------
                '' 全ページ印刷
                ''-----------------------
                Call mDrawGraphics(e.Graphics, udtTerminalPageInfo(mintPageIndex))

                If mhblnPrintRangeAll Then

                    If (mintPageIndex + 1) >= CInt(txtMaxPages.Text) Then
                        mintPageIndex = 1
                        e.HasMorePages = False
                    Else
                        mintPageIndex += 1
                        e.HasMorePages = True
                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： 表示ページテキスト
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub txtPage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPage.KeyPress

        Try
            'e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'Ver2.0.0.2 ADD ページ数を入力すると該当ページに代わる処理

            Dim tmpPage As Integer

            If e.KeyChar = Chr(13) Then     '' ENTERｷｰが押された

                If txtPage.Text.Trim = "" Then
                    Return
                End If

                If IsNumeric(txtPage.Text) = False Then
                    Return
                End If

                If CInt(txtPage.Text) <= 0 Then
                    txtPage.Text = "1"
                End If

                If CInt(txtPage.Text) > mintPageMax Then
                    txtPage.Text = mintPageMax
                End If

                '' 入力ﾍﾟｰｼﾞ番号ﾁｪｯｸ
                tmpPage = CInt(txtPage.Text)

                If tmpPage = mintPageIndex + 1 Then
                    Return
                End If

                If tmpPage >= mintPageMax Then     '' MAX値を超えている場合
                    mintPageIndex = mintPageMax - 1
                    'cmdNextPage.Enabled = False     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用不可
                    'cmdBeforePage.Enabled = True    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                ElseIf tmpPage <= 1 Then        '' 最小値以下の場合
                    mintPageIndex = 0
                    'cmdNextPage.Enabled = True     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                    'cmdBeforePage.Enabled = False    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用不可
                Else
                    mintPageIndex = tmpPage - 1
                    'cmdNextPage.Enabled = True     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                    'cmdBeforePage.Enabled = True    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                End If


                'FU番号を設定
                Dim strFUpate As String = ""
                If udtTerminalPageInfo(mintPageIndex).intFuNo.ToString >= 0 Then
                    strFUpate = udtTerminalPageInfo(mintPageIndex).intFuNo.ToString
                End If
                txtFUpage.Text = strFUpate


                'DRAWING NOを設定
                Dim strDrawNo As String = reg.Replace(NZf(udtTerminalPageInfo(mintPageIndex).strDrawinfNoInfo), "")
                If strDrawNo.Trim <> "" Then
                    strDrawNo = CInt(strDrawNo)
                Else
                    strDrawNo = strDrawNo.Trim
                End If
                txtDrawingNo.Text = strDrawNo


                picPreview.Refresh()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFUpage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFUpage.KeyPress

        Try
            'e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'Ver2.0.0.2 ADD FU番号を入力すると該当ページに代わる処理

            Dim tmpPage As Integer

            If e.KeyChar = Chr(13) Then     '' ENTERｷｰが押された

                If txtFUpage.Text = "" Then
                    txtFUpage.Text = "-1"
                End If

                If IsNumeric(txtFUpage.Text) = False Then
                    Return
                End If

                If CInt(txtFUpage.Text) < 0 Then
                    txtFUpage.Text = "-1"
                End If

                If CInt(txtFUpage.Text) > 20 Then
                    txtFUpage.Text = 20
                End If

                If txtFUpage.Text = "-1" Then
                    txtFUpage.Text = ""
                End If

                Dim intSetPageNo As Integer = 0
                Dim blExist As Boolean = False
                If txtFUpage.Text <> "" Then
                    tmpPage = CInt(txtFUpage.Text)
                    '指定あり
                    For i As Integer = 0 To UBound(udtTerminalPageInfo) Step 1
                        With udtTerminalPageInfo(i)
                            '指定Fu,Slotと一致するページ番号とする
                            If .intFuNo = tmpPage Then
                                intSetPageNo = i
                                blExist = True
                                Exit For
                            End If
                        End With
                    Next i
                End If

                If blExist = False Then
                    txtFUpage.Text = ""
                End If

                mintPageIndex = intSetPageNo
                txtPage.Text = mintPageIndex + 1


                'DRAWING NOを設定
                Dim strDrawNo As String = reg.Replace(NZf(udtTerminalPageInfo(intSetPageNo).strDrawinfNoInfo), "")
                If strDrawNo.Trim <> "" Then
                    strDrawNo = CInt(strDrawNo)
                Else
                    strDrawNo = strDrawNo.Trim
                End If
                txtDrawingNo.Text = strDrawNo


                picPreview.Refresh()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDrawingNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDrawingNo.KeyPress

        Try
            'e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'Ver2.0.0.7 ADD DRAW NO番号を入力すると該当ページに代わる処理

            Dim tmpPage As Integer

            If e.KeyChar = Chr(13) Then     '' ENTERｷｰが押された

                If txtDrawingNo.Text = "" Then
                    txtDrawingNo.Text = "-1"
                End If

                If IsNumeric(txtDrawingNo.Text) = False Then
                    Return
                End If

                If CInt(txtDrawingNo.Text) < 0 Then
                    txtDrawingNo.Text = "-1"
                End If

                If txtDrawingNo.Text = "-1" Then
                    txtDrawingNo.Text = ""
                End If

                Dim intSetPageNo As Integer = 0
                Dim blExist As Boolean = False
                If txtDrawingNo.Text <> "" Then
                    tmpPage = CInt(txtDrawingNo.Text)
                    '指定あり
                    For i As Integer = 0 To UBound(udtTerminalPageInfo) Step 1
                        With udtTerminalPageInfo(i)
                            Dim strDraw As String = reg.Replace(NZf(udtTerminalPageInfo(i).strDrawinfNoInfo), "")
                            Dim intDraw As Integer = -1
                            If strDraw.Trim <> "" Then
                                intDraw = CInt(strDraw)
                            End If
                            '指定と一致するページ番号とする
                            If intDraw = tmpPage Then
                                intSetPageNo = i
                                blExist = True
                                Exit For
                            End If
                        End With
                    Next i
                End If

                If blExist = False Then
                    'みつからなかった場合 近いページとする
                    If tmpPage >= 0 Then
                        For i As Integer = 0 To UBound(udtTerminalPageInfo) Step 1
                            With udtTerminalPageInfo(i)
                                Dim strDraw As String = reg.Replace(NZf(udtTerminalPageInfo(i).strDrawinfNoInfo), "")
                                Dim intDraw As Integer = -1
                                If strDraw.Trim <> "" Then
                                    intDraw = CInt(strDraw)
                                End If
                                '超えたページマイナス１のページとする
                                If intDraw > tmpPage Then
                                    intSetPageNo = i - 1
                                    txtDrawingNo.Text = CInt(reg.Replace(NZf(udtTerminalPageInfo(intSetPageNo).strDrawinfNoInfo), ""))
                                    blExist = True
                                    Exit For
                                End If
                            End With
                        Next i
                        '最大ページより大きいなら最大ページとする
                        If blExist = False Then
                            intSetPageNo = mintPageMax - 1
                            txtDrawingNo.Text = CInt(reg.Replace(NZf(udtTerminalPageInfo(intSetPageNo).strDrawinfNoInfo), ""))
                        End If
                    Else
                        txtDrawingNo.Text = ""
                    End If
                End If

                mintPageIndex = intSetPageNo
                txtPage.Text = mintPageIndex + 1

                'FU番号を設定
                Dim strFUpate As String = ""
                If udtTerminalPageInfo(mintPageIndex).intFuNo.ToString >= 0 Then
                    strFUpate = udtTerminalPageInfo(mintPageIndex).intFuNo.ToString
                End If
                txtFUpage.Text = strFUpate


                picPreview.Refresh()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    'Ver2.0.0.2 DEL
    'Private Sub txtPage_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPage.Validated

    '    Try

    '        ''TODO：落ちないようとりあえず処置
    '        If (CInt(txtPage.Text) < 1) Or (CInt(txtPage.Text) > mintPageMax) Then
    '            txtPage.Text = CStr(1)
    '            picPreview.Refresh()
    '            Return
    '        End If

    '        mintPageIndex = CInt(txtPage.Text - 1)

    '        picPreview.Refresh()

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub



#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Graph作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 印刷情報構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphics(ByVal objGraphics As System.Drawing.Graphics, _
                              ByVal udtPrtPageInfo As gTypPrintTerminalInfo)

        Try

            'objGraphics.PageScale = 0.95

            Dim i As Integer
            Dim intRoopCnt As Integer
            Dim StrComment As String = ""

            If udtPrtPageInfo.udtPageType = gEnmPrintTerminalPageType.tptFU Then
                '★LUの場合は、フレームから全て他のページと異なる
                'ページフレーム作成
                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                    If g_bytFUSet = 0 Then      '出荷済みｵｰﾀﾞｰ用
                        Call gPrtDrawOutFrameLocalUnit(objGraphics, "", " フィールドユニット", "FU001", udtPrtPageInfo.intPageNo, mhintPagePrint)  '' DrawingNo変更
                    Else
                        Call gPrtDrawOutFrameLocalUnit(objGraphics, "", " フィールドユニット", "FU01", udtPrtPageInfo.intPageNo, mhintPagePrint)  '' DrawingNo変更
                    End If

                Else
                    If g_bytFUSet = 0 Then      '出荷済みｵｰﾀﾞｰ用
                        Call gPrtDrawOutFrameLocalUnit(objGraphics, "", " FIELD UNIT", "FU001", udtPrtPageInfo.intPageNo, mhintPagePrint)  '' DrawingNo変更
                    Else
                        Call gPrtDrawOutFrameLocalUnit(objGraphics, "", " FIELD UNIT", "FU01", udtPrtPageInfo.intPageNo, mhintPagePrint)  '' DrawingNo変更
                    End If
                End If

                'グラフ作成
                Call DrawLinesLU(objGraphics, udtPrtPageInfo)
            Else
                '★通常のTerminalInpu

                'ページフレーム作成
                Call gPrtDrawOutFrameTerminal(objGraphics, _
                                              udtPrtPageInfo.strFrameInfoLine1, _
                                              udtPrtPageInfo.strFrameInfoLine2, _
                                              udtPrtPageInfo.strDrawinfNoInfo, _
                                              udtPrtPageInfo.intPageNo, _
                                              mhintPagePrint)

                ''画面上部のフレームボックス作成
                If Not udtPrtPageInfo.udtPageType = gEnmPrintTerminalPageType.tptFuList Then
                    'Ver2.0.2.5 和文の場合消す。英文の場合半角スペースへ置き換え
                    'Ver2.0.2.7 ここにも^あり
                    Dim strTemp As String = udtPrtPageInfo.strBoxRemarks
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        strTemp = strTemp.Replace("^", "")
                    Else
                        strTemp = strTemp.Replace("^", " ")
                    End If
                    Call mPrtDrawHdrFrame(objGraphics, udtPrtPageInfo.strBoxFuInfo, udtPrtPageInfo.strBoxSlotInfo, strTemp)
                End If

                ''ポートタイプによって帳票のフォーマットを変更
                ''（４種類：1.端子台リスト、2.デジタル、3.アナログ２線式、4.アナログ３線式）
                Select Case udtPrtPageInfo.udtPageType

                    Case gEnmPrintTerminalPageType.tptFuList ''①[端子台リスト] ===============================================

                        ''タイトル行作成
                        Call mDrawTitleFulist(objGraphics, mCstMarginLeft, mCstMarginUp)

                        For i = 0 To gCstCountFuNo - 1

                            ''図形描写
                            Call mDrawGraphicsH(objGraphics, mCstMarginLeft, mCstMarginUp + mCstRowHightFu + (i * mCstRowHightFu))

                            ''文字描写
                            '' 2013.11.15 引数にNOを追加
                            Call mDrawStringsH(objGraphics, _
                                               udtPrtPageInfo.udtRowInfoPage1(i), _
                                               mCstMarginUp + mCstRowHightFu + (i * mCstRowHightFu),
                                               i)

                        Next

                        '' 和文表示フォント変更   2014.05.19
                        '' 注釈追加　ver1.4.0 2011.08.17
                        Dim ExplanatoryEJ As Byte   ''説明文の和英設定 0:英文 1:和文 ver2.0.8.I 2019.02.21 
                        ExplanatoryEJ = gudt.SetSystem.udtSysSystem.shtLanguage ''システムの言語を反映
                        ''逆転フラグ有り
                        If g_bytExoTxtEtoJ = 1 Then
                            ''英なら和にする,和なら英にする
                            If ExplanatoryEJ = 0 Then
                                ExplanatoryEJ = 1
                            ElseIf ExplanatoryEJ = 1 Or ExplanatoryEJ = 2 Then
                                ExplanatoryEJ = 0
                            End If
                        End If
                        If ExplanatoryEJ = 1 Or ExplanatoryEJ = 2 Then  '和文仕様追加 20200217 hori
                            StrComment = "※ コード番号は、フィールドユニット内のディップスイッチの設定を示します。"
                            objGraphics.DrawString(StrComment, gFnt8j, gFntColorBlack, mCstPosFuCol1, mCstMarginUp + mCstRowHightFu + (22 * mCstRowHightFu) + 8)
                        Else
                            StrComment = "   CODE NO. IS TO BE SHOWN SETTING OF DIP SWITCH IN FIELD UNIT."
                            objGraphics.DrawString(StrComment, gFnt8, gFntColorBlack, mCstPosFuCol1, mCstMarginUp + mCstRowHightFu + (22 * mCstRowHightFu) + 8)
                        End If

                    Case gEnmPrintTerminalPageType.tptDigital   ''②[デジタル] ================================================

                        ''タイトル行作成
                        Call mDrawTitleDigital(objGraphics, gCstFrameTerminalUp)

                        ''行情報
                        mstrOrAndStr = ""
                        mintOrAndKI = 0
                        For i = 0 To gCstPrtTerminalMaxRow - 1

                            ''図形描写
                            Call mDrawGraphicsDigital(objGraphics, _
                                                      gCstFrameTerminalLeft, _
                                                      (gCstFrameTerminalUp + mCstRowHightDigitalHeader + (i * mCstRowHightDigital)))

                            ''文字描写
                            Call mDrawStringsDigital(objGraphics, _
                                                     udtPrtPageInfo.udtRowInfo(i), _
                                                     i, _
                                                     udtPrtPageInfo.intStartRowIndex, _
                                                     gCstFrameTerminalUp + mCstRowHightDigitalHeader + (i * mCstRowHightDigital), _
                                                     mhintDmyFlg)

                        Next

                        'Ver2.0.1.7 OrAndは設定ONではないと印刷しない
                        If g_bytOrAndPrint = 1 Then
                            'Ver2.0.0.2 一番下にOR情報を表示する
                            '一番下にOrAnd CHを表示
                            If mstrOrAndStr <> "" Then
                                Dim zaX As Integer = (gCstFrameTerminalUp + mCstRowHightDigitalHeader + ((gCstPrtTerminalMaxRow) * mCstRowHightDigital)) + 7
                                objGraphics.DrawString(mstrOrAndStr, gFnt7, gFntColorBlack, gCstFrameTerminalLeft + 2, zaX)
                            End If
                        End If

                        ''Ver2.0.8.7
                        If g_bytTerDIMsg = 1 Then
                            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様　20200520 hori
                                StrComment = "(COMM端子同士はプリント基板内で電気的に接続されています。)"
                                objGraphics.DrawString(StrComment, gFnt8j, gFntColorBlack, mCstDigitalMsg_XJ, mCstDigitalMsg_Y)
                            ElseIf gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                                StrComment = "(ALL COMMON TERMINAL IS CONNECTED IN PRINTED CIRCUIT BOARD.)"
                                objGraphics.DrawString(StrComment, gFnt8j, gFntColorBlack, mCstDigitalMsg_XJ, mCstDigitalMsg_Y)
                            Else
                                StrComment = "(ALL COMMON TERMINAL IS CONNECTED IN PRINTED CIRCUIT BOARD.)"
                                objGraphics.DrawString(StrComment, gFnt8, gFntColorBlack, mCstDigitalMsg_X, mCstDigitalMsg_Y)
                            End If
                        End If


                    Case gEnmPrintTerminalPageType.tptAnalog2   ''③[アナログ２線式] ==========================================

                        ''AIとAOを区別する
                        If udtPrtPageInfo.intPortType = gCstCodeFuSlotTypeAO Then
                            intRoopCnt = gCstPrtAnalogAoMaxRow
                        Else
                            intRoopCnt = gCstPrtAnalogAiMaxRow
                        End If

                        ''タイトル行作成
                        Call mDrawTitleAnalog(objGraphics, gCstFrameTerminalUp, udtPrtPageInfo.udtPageType, udtPrtPageInfo.intPortType)

                        For i = 0 To intRoopCnt - 1

                            ''図形描写
                            Call mDrawGraphicsAnalog(objGraphics, _
                                                     gEnmPrintTerminalPageType.tptAnalog2, _
                                                     udtPrtPageInfo.intPortType, _
                                                     gCstFrameTerminalLeft, _
                                                     (gCstFrameTerminalUp + mCstRowHightAnalogHeader + (i * mCstRowHightAnalog2Line)))

                            ''文字描写
                            Call mDrawStringsAnalog2Line(objGraphics, _
                                                         udtPrtPageInfo.udtRowInfo(i), _
                                                         udtPrtPageInfo.intPortType, _
                                                         i, _
                                                         gCstFrameTerminalUp + mCstRowHightAnalogHeader + (i * mCstRowHightAnalog2Line), _
                                                         mhintDmyFlg)

                        Next

                        '' DC4-20mA基板注釈追加　ver1.4.0 2011.08.24
                        If udtPrtPageInfo.intPortType = gCstCodeFuSlotTypeAI_4_20 Then
                            Call mDrawGraphicsCaution(objGraphics, _
                                                      (gCstFrameTerminalUp + mCstRowHightAnalogHeader + (gCstPrtAnalogAiMaxRow * mCstRowHightAnalog2Line)))
                        End If

                    Case gEnmPrintTerminalPageType.tptAnalog3   ''④[アナログ３線式] ==========================================

                        ''タイトル行作成
                        Call mDrawTitleAnalog(objGraphics, gCstFrameTerminalUp, udtPrtPageInfo.udtPageType, udtPrtPageInfo.intPortType)

                        For i = 0 To gCstPrtAnalogAiMaxRow - 1

                            ''図形描写
                            Call mDrawGraphicsAnalog(objGraphics, _
                                                     gEnmPrintTerminalPageType.tptAnalog3, _
                                                     udtPrtPageInfo.intPortType, _
                                                     gCstFrameTerminalLeft, _
                                                     (gCstFrameTerminalUp + mCstRowHightAnalogHeader + (i * mCstRowHightAnalog3Line)))

                            ''文字描写
                            Call mDrawStringsAnalog3Line(objGraphics, _
                                                         udtPrtPageInfo.udtRowInfo(i), _
                                                         i, _
                                                         gCstFrameTerminalUp + mCstRowHightAnalogHeader + (i * mCstRowHightAnalog3Line), _
                                                         mhintDmyFlg)

                        Next

                End Select
                '↓LUか、通常のTerminalデータかの分岐if
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub





#Region "フレームボックス"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ページフレームの上にあるボックスの作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Digitalかどうか
    '           ： ARG3 - (I ) FU機器名
    '           ： ARG4 - (I ) FuName＋ポート名
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Public Sub mPrtDrawHdrFrame(ByVal objGraphics As System.Drawing.Graphics, _
                                ByVal hstrFuName As String, _
                                ByVal hstrSlotInfo As String, _
                                ByVal hstrRemarks As String)

        Try

            Dim sngTB As Single
            Dim sngHight As Single
            Dim strTime As String
            Dim sngPosNo As Single
            Dim p2 As New Pen(Color.Black, 2)


            sngTB = gCstFrameTerminalLeft + mCstFrameBoxTB  ''位置
            sngHight = gCstFrameTerminalUp - mCstFrameBoxHight      ''位置
            ' 2015.11.07  Ver1.7.6 印刷日時ではなく、ﾌｧｲﾙﾍｯﾀﾞ内の日時を表示
            'strTime = "'" & Format(Now, "yy/MM/dd HH:mm")   ''表示時刻
            'Ver2.0.6.1 ｺﾝﾊﾞｰﾄしたﾃﾞｰﾀを印刷すると、ﾌｧｲﾙ日時が入っていないのでｴﾗｰが発生する不具合修正
            If NZfS(gudt.SetChDisp.udtHeader.strDate) = "" Then
                strTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm")
            Else
                strTime = "'" & gudt.SetChDisp.udtHeader.strDate.Substring(2, 2) & "/" & _
                        gudt.SetChDisp.udtHeader.strDate.Substring(4, 2) & "/" & _
                        gudt.SetChDisp.udtHeader.strDate.Substring(6, 2) & " " & _
                        gudt.SetChDisp.udtHeader.strTime.Substring(0, 2) & ":" & gudt.SetChDisp.udtHeader.strTime.Substring(2, 2)
            End If
            
            '//

            '上部区画移動 2015/4/10 T.Ueki
            ''アウトフレーム
            'objGraphics.DrawRectangle(p2, gCstFrameTerminalLeft, sngHight, mCstFrameBoxWidth, mCstFrameBoxHight)
            objGraphics.DrawRectangle(p2, gCstFrameTerminalWidth + gCstFrameTerminalLeft - mCstFrameBoxWidth, sngHight, mCstFrameBoxWidth, mCstFrameBoxHight)

            ''縦線 上部区画移動 2015/4/10 T.Ueki
            'objGraphics.DrawLine(p2, sngTB, sngHight, sngTB, gCstFrameTerminalUp)
            objGraphics.DrawLine(p2, gCstFrameTerminalWidth + gCstFrameTerminalLeft - mCstFrameBoxTB, sngHight, gCstFrameTerminalWidth + gCstFrameTerminalLeft - mCstFrameBoxTB, gCstFrameTerminalUp)

            ''文字列   FU10以上は枠をはみ出るので、フォントを小さくして位置を調整 2015.01.26
            If hstrFuName.Length > 3 Then

                '上部区画移動 2015/4/10 T.Ueki
                'sngPosNo = gCstFrameTerminalLeft + ((mCstFrameBoxTB / 2) - 14)
                sngPosNo = gCstFrameTerminalLeft + ((gCstFrameTerminalWidth - (mCstFrameBoxTB / 2)) - 14)

                objGraphics.DrawString(hstrFuName, gFnt7, gFntColorBlack, sngPosNo, sngHight + 5)
            Else
                '上部区画移動 2015/4/10 T.Ueki
                'sngPosNo = gCstFrameTerminalLeft + ((mCstFrameBoxTB / 2) - ((hstrFuName.Length * gFntScale7) / 2))
                sngPosNo = gCstFrameTerminalLeft + ((gCstFrameTerminalWidth - (mCstFrameBoxTB / 2)) - ((hstrFuName.Length * gFntScale7) / 2))

                objGraphics.DrawString(hstrFuName, gFnt8, gFntColorBlack, sngPosNo, sngHight + 5)
            End If


            ''SLOT, Remarks '上部区画移動 2015/4/10 T.Ueki
            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                'objGraphics.DrawString(hstrSlotInfo, gFnt8j, gFntColorBlack, sngTB + 5, sngHight + 5)
                objGraphics.DrawString(hstrSlotInfo, gFnt8j, gFntColorBlack, gCstFrameTerminalWidth + gCstFrameTerminalLeft - mCstFrameBoxWidth + 5, sngHight + 5)
                'objGraphics.DrawString(hstrRemarks, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft + mCstFrameBoxWidth, sngHight + 5)
                objGraphics.DrawString(hstrRemarks, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft + mCstFrameBoxWidth, sngHight + 5)
            Else
                'objGraphics.DrawString(hstrSlotInfo, gFnt8, gFntColorBlack, sngTB + 5, sngHight + 5)
                objGraphics.DrawString(hstrSlotInfo, gFnt8, gFntColorBlack, gCstFrameTerminalWidth + gCstFrameTerminalLeft - mCstFrameBoxWidth + 5, sngHight + 5)
                objGraphics.DrawString(hstrRemarks, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + mCstFrameBoxWidth, sngHight + 5)
            End If

            ''時刻 左側へ移動 2015/4/10 T.Ueki
            'objGraphics.DrawString(strTime, gFnt8, gFntColorBlack, mCstFrameBoxTime, sngHight + 5)
            objGraphics.DrawString(strTime, gFnt8, gFntColorBlack, gCstFrameTerminalLeft, sngHight + 5)

            p2.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "端子台設定 概要"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子台 タイトル行の印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始 X軸
    '           ： ARG3 - (I ) Draw開始 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawTitleFulist(ByVal objGraphics As System.Drawing.Graphics, _
                                 ByVal sngX As Single, _
                                 ByVal sngY As Single)

        Try

            Dim sngPosNo As Single
            Dim sngPosCode As Single

            sngPosNo = mCstMarginLeft + ((mCstPosFuCol1 - mCstMarginLeft) / 2) - (mCstLabelFulistNo.Length * 4)
            sngPosCode = mCstPosFuCol4 + ((mCstPosFuCol5 - mCstPosFuCol4) / 2) - (mCstLabelFulistNo.Length * 14)

            ''===================
            '' ライン描画    
            ''===================
            ''四角形
            objGraphics.DrawRectangle(Pens.Black, sngX, sngY, mCstRowWidthFu, mCstRowHightFu)

            ''縦線
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol1, sngY, mCstPosFuCol1, mCstRowHightFu + sngY)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol2, sngY, mCstPosFuCol2, mCstRowHightFu + sngY)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol3, sngY, mCstPosFuCol3, mCstRowHightFu + sngY)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol4, sngY, mCstPosFuCol4, mCstRowHightFu + sngY)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol5, sngY, mCstPosFuCol5, mCstRowHightFu + sngY)


            ''===================
            '' 文字列印字
            ''===================
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori

                objGraphics.DrawString(mCstLabelFulistNoJpn, gFnt8j, gFntColorBlack, sngPosNo - 4, sngY + 9)                ''No
                objGraphics.DrawString(mCstLabelFulistFuName1Jpn, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 16, sngY + 3)     ''FCU/FU Name
                objGraphics.DrawString(mCstLabelFulistFuName2Jpn, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 16, sngY + 16)    ''FCU/FU Name
                objGraphics.DrawString(mCstLabelFulistNamePlateJpn, gFnt8j, gFntColorBlack, mCstPosFuCol2 + 38, sngY + 9)   ''Name Plate
                objGraphics.DrawString(mCstLabelFulistFuTypeJpn, gFnt8j, gFntColorBlack, mCstPosFuCol3 + 25, sngY + 9)      ''FCU/FU Type
                objGraphics.DrawString(mCstLabelFulistCodeNo1Jpn, gFnt8j, gFntColorBlack, sngPosCode + 9, sngY + 3)        ''Code
                objGraphics.DrawString(mCstLabelFulistCodeNo2Jpn, gFnt8j, gFntColorBlack, sngPosCode + 14, sngY + 16)       ''No
                objGraphics.DrawString(mCstLabelFulistRemarksJpn, gFnt8j, gFntColorBlack, mCstPosFuCol5 + 50, sngY + 9)     ''Remarks

            Else
                objGraphics.DrawString(mCstLabelFulistNo, gFnt8, gFntColorBlack, sngPosNo, sngY + 8)                    ''No
                objGraphics.DrawString(mCstLabelFulistFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 65, sngY + 8)      ''FCU/FU Name
                objGraphics.DrawString(mCstLabelFulistNamePlate, gFnt8, gFntColorBlack, mCstPosFuCol2 + 30, sngY + 8)   ''Name Plate
                objGraphics.DrawString(mCstLabelFulistFuType, gFnt8, gFntColorBlack, mCstPosFuCol3 + 23, sngY + 8)      ''FCU/FU Type
                objGraphics.DrawString(mCstLabelFulistCodeNo1, gFnt8, gFntColorBlack, sngPosCode + 13, sngY + 3)        ''Code
                objGraphics.DrawString(mCstLabelFulistCodeNo2, gFnt8, gFntColorBlack, sngPosCode + 20, sngY + 15)       ''No
                objGraphics.DrawString(mCstLabelFulistRemarks, gFnt8, gFntColorBlack, mCstPosFuCol5 + 40, sngY + 8)     ''Remarks
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子台 各行の図形描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始 X軸
    '           ： ARG3 - (I ) Draw開始 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsH(ByVal objGraphics As System.Drawing.Graphics, _
                               ByVal sngLeft As Single, _
                               ByVal sngHigh As Single)

        Try

            ''四角形
            objGraphics.DrawRectangle(Pens.Black, sngLeft, sngHigh, mCstRowWidthFu, mCstRowHightFu)

            ''縦線
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol1, sngHigh, mCstPosFuCol1, mCstRowHightFu + sngHigh)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol2, sngHigh, mCstPosFuCol2, mCstRowHightFu + sngHigh)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol3, sngHigh, mCstPosFuCol3, mCstRowHightFu + sngHigh)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol4, sngHigh, mCstPosFuCol4, mCstRowHightFu + sngHigh)
            objGraphics.DrawLine(Pens.Black, mCstPosFuCol5, sngHigh, mCstPosFuCol5, mCstRowHightFu + sngHigh)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子台 各行の文字列印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 印刷用構造体
    '           ： ARG3 - (I ) Draw開始 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsH(ByVal objGraphics As System.Drawing.Graphics, _
                              ByVal hudtPrtPageInfo As gTypPrintTerminalPage1, _
                              ByVal sngY As Single, _
                              ByVal no As Integer)

        Try

            Dim sngPosNo As Single

            sngPosNo = mCstMarginLeft + ((mCstPosFuCol1 - mCstMarginLeft) / 2) - (hudtPrtPageInfo.strNo.Length * 4)

            With hudtPrtPageInfo

                objGraphics.DrawString(.strNo, gFnt8, gFntColorBlack, sngPosNo, sngY + 8)                    ''No

                If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                    '' 2013.11.15   FUに「FIELD UNIT」名称追加
                    If no = 0 Then
                        objGraphics.DrawString(.strFuName, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                    Else
                        If .strFuName <> "" Then
                            '' 2014.10.29   FUの「FIELD UNIT」名称削除
                            'If mhintFuNameType = 1 Then
                            '    objGraphics.DrawString("ﾌｨｰﾙﾄﾞﾕﾆｯﾄ " & .strFuName, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                            'Else
                            '    objGraphics.DrawString("FIELD UNIT " & .strFuName, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                            'End If
                            objGraphics.DrawString(.strFuName, gFnt8j, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                        End If
                    End If
                    'objGraphics.DrawString(.strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                    objGraphics.DrawString(.strNamePlate, gFnt8j, gFntColorBlack, mCstPosFuCol2 + 9, sngY + 8)    ''Name Plate
                    objGraphics.DrawString(.strFuType, gFnt8, gFntColorBlack, mCstPosFuCol3 + 6, sngY + 8)       ''FCU/FU Type
                    objGraphics.DrawString(.strCodeNo, gFnt8, gFntColorBlack, mCstPosFuCol4 + 6 + 6, sngY + 8)   ''Code No

                    'Ver2.0.2.5 和文の場合消す。英文の場合半角スペースへ置き換え
                    Dim strTemp As String = .strRemarks
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        strTemp = strTemp.Replace("^", "")
                    Else
                        strTemp = strTemp.Replace("^", " ")
                    End If

                    objGraphics.DrawString(strTemp, gFnt8j, gFntColorBlack, mCstPosFuCol5 + 10, sngY + 8)     ''Remarks
                Else
                    '' 2013.11.15   FUに「FIELD UNIT」名称追加
                    If no = 0 Then
                        objGraphics.DrawString(.strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                    Else
                        If .strFuName <> "" Then
                            '' 2014.10.29   FUの「FIELD UNIT」名称削除
                            'If mhintFuNameType = 1 Then
                            '    objGraphics.DrawString("ﾌｨｰﾙﾄﾞﾕﾆｯﾄ " & .strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                            'Else
                            '    objGraphics.DrawString("FIELD UNIT " & .strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                            'End If
                            objGraphics.DrawString(.strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                        End If
                    End If
                    'objGraphics.DrawString(.strFuName, gFnt8, gFntColorBlack, mCstPosFuCol1 + 9, sngY + 8)       ''FCU/FU Name
                    objGraphics.DrawString(.strNamePlate, gFnt8, gFntColorBlack, mCstPosFuCol2 + 9, sngY + 8)    ''Name Plate
                    objGraphics.DrawString(.strFuType, gFnt8, gFntColorBlack, mCstPosFuCol3 + 6, sngY + 8)       ''FCU/FU Type
                    objGraphics.DrawString(.strCodeNo, gFnt8, gFntColorBlack, mCstPosFuCol4 + 6 + 6, sngY + 8)   ''Code No
                    'Ver2.0.1.7 Remarksに「^」が入っていることがあるため、それは削る
                    'Ver2.0.2.5 和文の場合消す。英文の場合半角スペースへ置き換え
                    Dim strTemp As String = .strRemarks
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        strTemp = strTemp.Replace("^", "")
                    Else
                        strTemp = strTemp.Replace("^", " ")
                    End If

                    'objGraphics.DrawString(.strRemarks, gFnt8, gFntColorBlack, mCstPosFuCol5 + 10, sngY + 8)     ''Remarks
                    objGraphics.DrawString(strTemp, gFnt8, gFntColorBlack, mCstPosFuCol5 + 10, sngY + 8)     ''Remarks
                End If

                

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "デジタル設定"

    '----------------------------------------------------------------------------
    ' 機能説明  ： デジタル タイトル行印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawTitleDigital(ByVal objGraphics As System.Drawing.Graphics, _
                                  ByVal sngY As Single)

        Try

            Dim sngHalfRowHight As Single
            Dim sngMidLine As Single
            Dim sngRowHight As Single
            Dim sngMarkPos As Single

            sngHalfRowHight = (mCstRowHightDigitalHeader / 2)
            sngMidLine = gCstFrameTerminalUp + (mCstRowHightDigitalHeader / 2)
            sngRowHight = mCstRowHightDigitalHeader + gCstFrameTerminalUp
            sngMarkPos = sngY + (mCstRowHightDigitalHeader / 2)

            ''===================
            '' ライン描画
            ''===================
            ''縦線
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalAdd, gCstFrameTerminalUp, mCstPosDigitalAdd, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalChNo, gCstFrameTerminalUp, mCstPosDigitalChNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalItemName, gCstFrameTerminalUp, mCstPosDigitalItemName, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInCoreNo, gCstFrameTerminalUp, mCstPosDigitalInCoreNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInTermNo, gCstFrameTerminalUp, mCstPosDigitalInTermNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalStatus, gCstFrameTerminalUp, mCstPosDigitalStatus, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalComTermNo, gCstFrameTerminalUp, mCstPosDigitalComTermNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalComCoreNo, gCstFrameTerminalUp, mCstPosDigitalComCoreNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalMark, sngMidLine, mCstPosDigitalMark, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalClass, gCstFrameTerminalUp, mCstPosDigitalClass, sngRowHight)

            ''横線（中間線）
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalComCoreNo, sngMidLine, mCstPosDigitalClass, sngMidLine)

            ''横線（最下線）   
            objGraphics.DrawLine(Pens.Black, gCstFrameTerminalLeft, gCstFrameTerminalUp + mCstRowHightDigitalHeader, gCstFrameTerminalLeft + gCstFrameTerminalWidth, sngRowHight)


            ''===================
            '' 文字列印字
            ''===================
            ' 2015.10.16 ﾀｸﾞ表示
            ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加

            ''和文仕様　20200217 hori
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then

                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString(mCstLabelDigitalAdd1Jpn, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft, sngY + 6)         ''Add
                    objGraphics.DrawString(mCstLabelDigitalAdd2Jpn, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft, sngY + 18)        ''Add
                    objGraphics.DrawString(mCstLabelDigitalCh1Jpn, gFnt8, gFntColorBlack, mCstPosDigitalAdd + 5, sngY + 4)           ''Ch No
                    objGraphics.DrawString(mCstLabelDigitalCh2Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalAdd + 2, sngY + 20)         ''Ch No
                Else
                    objGraphics.DrawString(mCstLabelDigitalAdd1Jpn, gFnt7j, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 6)     ''Add
                    objGraphics.DrawString(mCstLabelDigitalAdd2Jpn, gFnt7j, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 18)    ''Add
                    objGraphics.DrawString(mCstLabelDigitalCh1Jpn, gFnt7, gFntColorBlack, mCstPosDigitalAdd + 10, sngY + 6)          ''Ch No
                    objGraphics.DrawString(mCstLabelDigitalCh2Jpn, gFnt7j, gFntColorBlack, mCstPosDigitalAdd + 10, sngY + 18)        ''Ch No
                End If

                objGraphics.DrawString(mCstLabelDigitalItemJpn, gFnt8j, gFntColorBlack, mCstPosDigitalChNo + 80, sngY + 11)             ''Item Name
                objGraphics.DrawString(mCstLabelDigitalInCore1Jpn, gFnt8, gFntColorBlack, mCstPosDigitalItemName + 6, sngY)             ''In Core No1
                objGraphics.DrawString(mCstLabelDigitalInCore2Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalItemName + 3, sngY + 13)       ''In Core No2
                objGraphics.DrawString(mCstLabelDigitalInCore3Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalItemName + 3, sngY + 24)       ''In Core No3
                objGraphics.DrawString(mCstLabelDigitalInTerm1Jpn, gFnt8, gFntColorBlack, mCstPosDigitalInCoreNo + 6, sngY)             ''In Term No1
                objGraphics.DrawString(mCstLabelDigitalInTerm2Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalInCoreNo + 3, sngY + 13)       ''In Term No2
                objGraphics.DrawString(mCstLabelDigitalInTerm3Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalInCoreNo + 3, sngY + 24)       ''In Term No3

                objGraphics.DrawString(mCstLabelDigitalSignalJpn, gFnt8j, gFntColorBlack, mCstPosDigitalInTermNo + 18, sngY + 11)      ''Signal

                objGraphics.DrawString(mCstLabelDigitalComTerm1Jpn, gFnt8, gFntColorBlack, mCstPosDigitalStatus + 3, sngY)              ''Com Term No1
                objGraphics.DrawString(mCstLabelDigitalComTerm2Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalStatus + 3, sngY + 13)        ''Com Term No2
                objGraphics.DrawString(mCstLabelDigitalComTerm3Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalStatus + 3, sngY + 24)        ''Com Term No3
                objGraphics.DrawString(mCstLabelDigitalComCore1Jpn, gFnt8, gFntColorBlack, mCstPosDigitalComTermNo + 3, sngY)           ''Com Core No1
                objGraphics.DrawString(mCstLabelDigitalComCore2Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalComTermNo + 3, sngY + 13)     ''Com Core No2
                objGraphics.DrawString(mCstLabelDigitalComCore3Jpn, gFnt8j, gFntColorBlack, mCstPosDigitalComTermNo + 3, sngY + 24)     ''Com Core No3

                objGraphics.DrawString(mCstLabelDigitalCableJpn, gFnt8j, gFntColorBlack, mCstPosDigitalComCoreNo + 34, sngY + 3)      ''Cable(Wire Mark)
                objGraphics.DrawString(mCstLabelDigitalMarkJpn, gFnt8j, gFntColorBlack, mCstPosDigitalComCoreNo + 24, sngMarkPos + 3) ''Mark(In)
                objGraphics.DrawString(mCstLabelDigitalClassJpn, gFnt8j, gFntColorBlack, mCstPosDigitalMark + 20, sngMarkPos + 3)     ''Class(Com)
                objGraphics.DrawString(mCstLabelDigitalDistJpn, gFnt8j, gFntColorBlack, mCstPosDigitalClass + 11, sngY + 11)          ''Dist

            Else
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString(mCstLabelDigitalAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 11)          ''Add
                    objGraphics.DrawString(mCstLabelDigitalCh, gFnt8, gFntColorBlack, mCstPosDigitalAdd, sngY + 11)                   ''Ch No
                Else
                    objGraphics.DrawString(mCstLabelDigitalAdd, gFnt7, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 11)          ''Add
                    objGraphics.DrawString(mCstLabelDigitalCh, gFnt7, gFntColorBlack, mCstPosDigitalAdd + 10, sngY + 11)                   ''Ch No
                End If
                objGraphics.DrawString(mCstLabelDigitalItem, gFnt8, gFntColorBlack, mCstPosDigitalChNo + 70, sngY + 11)           ''Item Name

                objGraphics.DrawString(mCstLabelDigitalInCore1, gFnt8, gFntColorBlack, mCstPosDigitalItemName, sngY)              ''In Core No1
                objGraphics.DrawString(mCstLabelDigitalInCore2, gFnt8, gFntColorBlack, mCstPosDigitalItemName, sngY + 10)         ''In Core No2
                objGraphics.DrawString(mCstLabelDigitalInCore3, gFnt8, gFntColorBlack, mCstPosDigitalItemName, sngY + 20)         ''In Core No3
                objGraphics.DrawString(mCstLabelDigitalInTerm1, gFnt8, gFntColorBlack, mCstPosDigitalInCoreNo, sngY)              ''In Term No1
                objGraphics.DrawString(mCstLabelDigitalInTerm2, gFnt8, gFntColorBlack, mCstPosDigitalInCoreNo, sngY + 10)         ''In Term No2
                objGraphics.DrawString(mCstLabelDigitalInTerm3, gFnt8, gFntColorBlack, mCstPosDigitalInCoreNo, sngY + 20)         ''In Term No3

                objGraphics.DrawString(mCstLabelDigitalSignal, gFnt8, gFntColorBlack, mCstPosDigitalInTermNo + 8, sngY + 11)      ''Signal

                objGraphics.DrawString(mCstLabelDigitalComTerm1, gFnt8, gFntColorBlack, mCstPosDigitalStatus, sngY)               ''Com Term No1
                objGraphics.DrawString(mCstLabelDigitalComTerm2, gFnt8, gFntColorBlack, mCstPosDigitalStatus, sngY + 10)          ''Com Term No2
                objGraphics.DrawString(mCstLabelDigitalComTerm3, gFnt8, gFntColorBlack, mCstPosDigitalStatus, sngY + 20)          ''Com Term No3
                objGraphics.DrawString(mCstLabelDigitalComCore1, gFnt8, gFntColorBlack, mCstPosDigitalComTermNo, sngY)            ''Com Core No1
                objGraphics.DrawString(mCstLabelDigitalComCore2, gFnt8, gFntColorBlack, mCstPosDigitalComTermNo, sngY + 10)       ''Com Core No2
                objGraphics.DrawString(mCstLabelDigitalComCore3, gFnt8, gFntColorBlack, mCstPosDigitalComTermNo, sngY + 20)       ''Com Core No3

                ''位置調整  2013.11.20
                'objGraphics.DrawString(mCstLabelDigitalCable, gFnt8, gFntColorBlack, mCstPosDigitalComCoreNo + 46, sngY + 1)      ''Cable(Wire Mark)
                'objGraphics.DrawString(mCstLabelDigitalMark, gFnt8, gFntColorBlack, mCstPosDigitalComCoreNo + 26, sngMarkPos + 1) ''Mark(In)
                'objGraphics.DrawString(mCstLabelDigitalClass, gFnt8, gFntColorBlack, mCstPosDigitalMark + 16, sngMarkPos + 1)     ''Class(Com)
                'objGraphics.DrawString(mCstLabelDigitalDist, gFnt8, gFntColorBlack, mCstPosDigitalClass + 23, sngY + 11)          ''Dist
                objGraphics.DrawString(mCstLabelDigitalCable, gFnt8, gFntColorBlack, mCstPosDigitalComCoreNo + 44, sngY + 1)      ''Cable(Wire Mark)
                objGraphics.DrawString(mCstLabelDigitalMark, gFnt8, gFntColorBlack, mCstPosDigitalComCoreNo + 24, sngMarkPos + 1) ''Mark(In)
                objGraphics.DrawString(mCstLabelDigitalClass, gFnt8, gFntColorBlack, mCstPosDigitalMark + 14, sngMarkPos + 1)     ''Class(Com)
                objGraphics.DrawString(mCstLabelDigitalDist, gFnt8, gFntColorBlack, mCstPosDigitalClass + 21, sngY + 11)          ''Dist
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： デジタル 図形描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始 X軸
    '           ： ARG3 - (I ) Draw開始 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsDigital(ByVal objGraphics As System.Drawing.Graphics, _
                                     ByVal sngLeft As Single, _
                                     ByVal sngHight As Single)

        Try

            ''縦線
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalAdd, sngHight, mCstPosDigitalAdd, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalChNo, sngHight, mCstPosDigitalChNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalItemName, sngHight, mCstPosDigitalItemName, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInCoreNo, sngHight, mCstPosDigitalInCoreNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInTermNo, sngHight, mCstPosDigitalInTermNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalStatus, sngHight, mCstPosDigitalStatus, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalComTermNo, sngHight, mCstPosDigitalComTermNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalComCoreNo, sngHight, mCstPosDigitalComCoreNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalMarkNo, sngHight, mCstPosDigitalMarkNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalMark, sngHight, mCstPosDigitalMark, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalClassNo, sngHight, mCstPosDigitalClassNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalClass, sngHight, mCstPosDigitalClass, mCstRowHightDigital + sngHight)

            ''横線（行の下線）
            'objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightDigital + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightDigital + sngHight, mCstPosDigitalInTermNo, mCstRowHightDigital + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalStatus, mCstRowHightDigital + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightDigital + sngHight)

            ''横線（SIGNAL）
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInTermNo, mCstRowHightDigital + sngHight - 14, mCstPosDigitalInTermNo + 10, mCstRowHightDigital + sngHight - 14)
            objGraphics.DrawLine(Pens.Black, mCstPosDigitalInTermNo + 50, mCstRowHightDigital + sngHight - 14, mCstPosDigitalStatus, mCstRowHightDigital + sngHight - 14)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： デジタル 文字列印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ページ情報
    '           ： ARG3 - (I ) 行カウント
    '           ： ARG4 - (I ) スタート行Index
    '           ： ARG5 - (I ) Draw開始位置 Y軸
    '           ： ARG6 - (I ) 仮設定データ表示フラグ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsDigital(ByVal objGraphics As System.Drawing.Graphics, _
                                    ByVal hudtPrtPageInfo As gTypFuInfoPin, _
                                    ByVal intRowCnt As Integer, _
                                    ByVal intStartRowIndex As Integer, _
                                    ByVal sngY As Single, _
                                    ByVal hblnDispDmyData As Boolean)

        Try

            Dim strRowAdd As String = (intStartRowIndex + intRowCnt + 1).ToString("00")
            Dim strRowNo As String = (intRowCnt + 1).ToString           '' 2013.10.22 0埋めを廃止
            Dim strCH As String = gGetString(hudtPrtPageInfo.strChNo)
            Dim strCh2 As String = gGetString(hudtPrtPageInfo.strChNo2)

            'Ver2.0.6.5 2.0.7.0
            'CHが4文字未満=OR AND の場合、CH2,3は消す
            'If strCH.Length > 0 And strCH.Length < 4 Then
            '    hudtPrtPageInfo.strChNo2 = ""
            '    hudtPrtPageInfo.strChNo3 = ""
            'End If



            ''描画必須の固定文字列
            ' 2015.10.16 ﾀｸﾞ表示
            ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                objGraphics.DrawString(strRowAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 5, sngY + 8)       ''Add
            Else
                objGraphics.DrawString(strRowAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 2, sngY + 8)       ''Add   ' 2015.10.16 ﾀｸﾞ表示
            End If
            objGraphics.DrawString(strRowNo, gFnt8, gFntColorBlack, mCstPosDigitalInCoreNo + 7, sngY + 8)       ''In Term No
            objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosDigitalStatus + 3, sngY + 8)   ''Com Term No
            objGraphics.DrawString(strRowNo, gFnt8, gFntColorBlack, mCstPosDigitalComCoreNo + 7, sngY + 8)      ''Mark No
            objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosDigitalMark + 1, sngY + 8)     ''Class No

            'Ver2.0.3.6 ワイヤーマーク系が設定されていれば処理抜けしない
            Dim blWire As Boolean = False
            Dim intW1 As Integer = 0
            With hudtPrtPageInfo

                If IsNothing(.strWireMark) = False Then
                    For intW1 = LBound(.strWireMark) To UBound(.strWireMark)
                        If NZfS(.strWireMark(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strWireMarkClass) = False Then
                    For intW1 = LBound(.strWireMarkClass) To UBound(.strWireMarkClass)
                        If NZfS(.strWireMarkClass(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoIn) = False Then
                    For intW1 = LBound(.strCoreNoIn) To UBound(.strCoreNoIn)
                        If NZfS(.strCoreNoIn(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoCom) = False Then
                    For intW1 = LBound(.strCoreNoCom) To UBound(.strCoreNoCom)
                        If NZfS(.strCoreNoCom(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strDist) = False Then
                    For intW1 = LBound(.strDist) To UBound(.strDist)
                        If NZfS(.strDist(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
            End With

            ''●CH未設定の場合は処理をスキップ 
            If strCH = "" And strCh2 = "" And blWire = False Then Exit Sub
            'If strCH = "" And strCh2 = "" Then Exit Sub '' 1CH,2CHともに設定がない場合　2015.04.21


            'Ver2.0.2.4 FUdummyでダミー印刷しないなら処理抜け
            If hblnDispDmyData = False Then
                ''OUTPUTの場合、該当CH(INPUT)のﾀﾞﾐｰの有無に関わらず印字する    20200525 hori
                If Not udtTerminalPageInfo(mintPageIndex).intPortType = gCstCodeFuSlotTypeDO Then
                    If hudtPrtPageInfo.blnDmyFUadress = True Then
                        Exit Sub
                    End If
                End If
            End If


            'Ver2.0.0.2 OrAnd対応
            Dim strOrAndCH As String = ""
            If strCH.Trim = "OR" Or strCH.Trim = "AND" Then
                strOrAndCH = fnGetOrAnd( _
                                        udtTerminalPageInfo(mintPageIndex).intFuNo, _
                                        udtTerminalPageInfo(mintPageIndex).intSlotNo + 1, _
                                        intStartRowIndex + intRowCnt + 1)
                If strOrAndCH <> "" Then
                    '自動改行 MAX4個で改行、印刷幅より大きくなるなら即改行 
                    mintOrAndKI = mintOrAndKI + 1
                    Dim strSpli As String() = mstrOrAndStr.Split(vbCrLf)
                    If gCstFrameTerminalWidth <= (strSpli(UBound(strSpli)) & strOrAndCH & "   ").Length * gFntScale7 Then
                        mintOrAndKI = 4
                    End If
                    Select Case mintOrAndKI
                        Case 1, 2, 3
                            mstrOrAndStr = mstrOrAndStr & strOrAndCH & "   "
                        Case 4
                            mstrOrAndStr = mstrOrAndStr & strOrAndCH & vbCrLf
                            mintOrAndKI = 0
                    End Select
                End If
            End If

            Dim strItemName As String = gGetString(hudtPrtPageInfo.strItemName)
            Dim strStatus As String = gGetString(hudtPrtPageInfo.strStatus)
            Dim strWireMarkIn As String = gGetString(hudtPrtPageInfo.strWireMark(0))
            Dim strWireMarkCom As String = gGetString(hudtPrtPageInfo.strWireMarkClass(0))
            Dim strDist As String = gGetString(hudtPrtPageInfo.strDist(0))
            Dim blnDmyFlg As Boolean = hudtPrtPageInfo.blnChComDmy
            Dim sngPosChNo As Single
            Dim sngPosInCoreNo As Single
            Dim sngPosComCoreNo As Single

            ''仮設定データの表示判断 ----------------------------------------------------------------------------
            If strStatus <> "" Then Call mCheckDmyStatus(strStatus, hudtPrtPageInfo, hblnDispDmyData)
            ''---------------------------------------------------------------------------------------------------

            'Ver2.0.0.0 ステータスが手入力時、表示されない対策
            If strStatus = "" Then
                If IsNumeric(strCH) = True Then
                    GetStatus(CInt(strCH), strStatus)
                End If
            End If

            ''ChNo
            If strCH = "" And strCh2 <> "" Then     ''運転積算用 2015.04.21 1CHなし、2CHのみ設定時
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCh2.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 8)
                Else
                    sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCh2.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 8)
                End If
                '//
            Else
                If hudtPrtPageInfo.strChNo3 <> "" Then  '' CH_NO (同一端子CH表示用)     2014.09.18
                    ' 2015.10.16  ﾀｸﾞ表示
                    ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 0)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 8)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt8, gFntColorBlack, sngPosChNo, sngY + 16)
                    Else
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 0)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 8)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt7, gFntColorBlack, sngPosChNo, sngY + 16)
                    End If
                    '//
                ElseIf hudtPrtPageInfo.strChNo2 <> "" Then  '' CH_NO (同一端子CH表示用)     2013.11.23
                    ' 2015.10.16  ﾀｸﾞ表示
                    ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 3)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 14)
                    Else
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 3)
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 14)
                    End If
                    ' //
                Else
                    ' 2015.10.16  ﾀｸﾞ表示
                    ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 8)
                    Else
                        sngPosChNo = mCstPosDigitalAdd + (((mCstPosDigitalChNo - mCstPosDigitalAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                        objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 8)
                    End If
                    '//
                End If
            End If

            ''ItemName
            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                objGraphics.DrawString(strItemName, gFnt8j, gFntColorBlack, mCstPosDigitalChNo, sngY + 14)
            Else
                objGraphics.DrawString(strItemName, gFnt8, gFntColorBlack, mCstPosDigitalChNo, sngY + 14)
            End If

            ''CoreNo(IN)
            sngPosInCoreNo = mCstPosDigitalItemName + (((mCstPosDigitalInCoreNo - mCstPosDigitalItemName) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoIn(0)).Length * gFntScale7) / 2))
            objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoIn(0)), gFnt8, gFntColorBlack, sngPosInCoreNo, sngY + 8)

            ''Status
            'If (hudtPrtPageInfo.intChType <> gCstCodeChTypePulse) And (strStatus <> "") Then    'パルス、運転積算は除外
            If strStatus <> "" Then    'パルス、運転積算は除外
                'Ver2.0.7.L Padding
                'objGraphics.DrawString("(" & strStatus.PadRight(17) & ")", gFnt8, gFntColorBlack, mCstPosDigitalChNo + 72, sngY + 3)
                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '保安庁または全和文仕様の場合 20200306 hori
                    objGraphics.DrawString("(" & PadB(strStatus, "LEFT", 17, " ") & ")", gFnt8j, gFntColorBlack, mCstPosDigitalChNo + 72, sngY + 3)
                Else
                    objGraphics.DrawString("(" & PadB(strStatus, "LEFT", 17, " ") & ")", gFnt8, gFntColorBlack, mCstPosDigitalChNo + 72, sngY + 3)
                End If
            End If

            '' Ver1.9.3 2016.01.15  OUTPUTｽﾃｰﾀｽ 保留
            ''If hudtPrtPageInfo.bytOutput <> 0 And hudtPrtPageInfo.strOutStatus <> "" Then
            ''    objGraphics.DrawString("[" & hudtPrtPageInfo.strOutStatus & "]", gFnt8, gFntColorBlack, mCstPosDigitalChNo, sngY + 3)
            ''End If

            ''Signal
            'Ver2.0.5.7
            'SignalはCHNoが無いなら出さない
            Dim strSignal As String = ""
            If strCH = "" And strCh2 = "" Then
                strSignal = ""
            Else
                strSignal = hudtPrtPageInfo.strSignal
            End If
            'objGraphics.DrawString(hudtPrtPageInfo.strSignal, gFnt8, gFntColorBlack, mCstPosDigitalInTermNo + 9, sngY + 8)
            objGraphics.DrawString(strSignal, gFnt8, gFntColorBlack, mCstPosDigitalInTermNo + 9, sngY + 8)

            ''CoreNo(COM)
            sngPosComCoreNo = mCstPosDigitalComTermNo + (((mCstPosDigitalComCoreNo - mCstPosDigitalComTermNo) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoCom(0)).Length * gFntScale7) / 2))
            objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoCom(0)), gFnt8, gFntColorBlack, sngPosComCoreNo, sngY + 8)

            ''WireMark
            Call mDrawStringsDigitalLine2(objGraphics, strWireMarkIn, mCstCodeMaxLength10, mCstPosDigitalMarkNo, sngY)

            ''WireCls
            Call mDrawStringsDigitalLine2(objGraphics, strWireMarkCom, mCstCodeMaxLength10, mCstPosDigitalClassNo, sngY)

            ''DEST 
            Call mDrawStringsDigitalLine2(objGraphics, strDist, mCstCodeMaxLength10, mCstPosDigitalClass, sngY)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "Ver2.0.0.0 マニュアル入力時ステータスが表示されない対応関数"
    ' CHリストからひっぱってくる
    Private Sub GetStatus(pintCHNo As Integer, ByRef pstrStatus As String)
        Dim iData As Integer
        Dim iLen As Integer

        Dim strHH As String = ""
        Dim strH As String = ""
        Dim strL As String = ""
        Dim strLL As String = ""

        Dim strTemp As String = ""

        '該当CHデータを探す
        Dim bHitFlg As Boolean = False
        For iData = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
            If gudt.SetChInfo.udtChannel(iData).udtChCommon.shtChno = pintCHNo Then
                bHitFlg = True
                Exit For
            End If
        Next iData

        'CHNoがみつからないなら何もしないで処理抜け
        If bHitFlg = False Then
            Return
        End If

        With gudt.SetChInfo.udtChannel(iData)
            'アナログCHじゃないなら処理抜け、PIDも処理抜けしない Ver2.0.7.H
            If .udtChCommon.shtChType <> gCstCodeChTypeAnalog And .udtChCommon.shtChType <> gCstCodeChTypePID Then
                Return
            End If

            'ステータスマニュアル入力じゃないなら処理抜け
            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                Return
            End If

            'Ver2.0.7.H PID対応
            If .udtChCommon.shtChType = gCstCodeChTypePID Then
                strHH = gGetString(.PidHiHiStatusInput)
                strH = gGetString(.PidHiStatusInput)
                strL = gGetString(.PidLoStatusInput)
                strLL = gGetString(.PidLoLoStatusInput)
            Else
                strHH = gGetString(.AnalogHiHiStatusInput)
                strH = gGetString(.AnalogHiStatusInput)
                strL = gGetString(.AnalogLoStatusInput)
                strLL = gGetString(.AnalogLoLoStatusInput)
            End If

            If LenB(strHH) = 0 And LenB(strH) = 0 And LenB(strL) = 0 And LenB(strLL) = 0 Then
                strTemp = ""
            Else
                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                    'Ver2.0.7.M (保安庁)
                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then
                            If LenB(strH) = 0 Then
                                strTemp = "正常/" & strL
                            Else
                                strTemp = "正常/" & strH
                            End If
                        Else
                            If LenB(strHH) = 0 Then
                                strTemp = "正常/" & strL & "/" & strLL
                            Else
                                strTemp = "正常/" & strH & "/" & strHH
                            End If
                        End If
                    Else
                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then
                            If LenB(strH) = 0 Then
                                strTemp = "NOR/" & strL
                            Else
                                strTemp = "NOR/" & strH
                            End If
                        Else
                            If LenB(strHH) = 0 Then
                                strTemp = "NOR/" & strL & "/" & strLL
                            Else
                                strTemp = "NOR/" & strH & "/" & strHH
                            End If
                        End If
                    End If
                Else
                    If LenB(strLL) <> 0 Then
                        strTemp += strLL & "/"
                    Else
                        strTemp = ""
                    End If

                    If LenB(strL) <> 0 Then
                        strTemp += strL & "/"
                    End If

                    'Ver2.0.7.M (保安庁)
                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                        strTemp += "正常/"
                    Else
                        strTemp += "NOR/"
                    End If

                    If LenB(strH) <> 0 Then
                        strTemp += strH & "/"
                    End If

                    If LenB(strHH) <> 0 Then
                        strTemp += strHH
                    End If

                    'Ver2.0.7.M (保安庁)
                    'If strTemp = "NOR/" Then
                    If strTemp = "NOR/" Or strTemp = "正常/" Then
                        strTemp = ""
                    Else
                        '' 文字列の最後尾ならば"/"を削除する
                        iLen = LenB(strTemp)
                        'Ver2.0.7.L
                        'If strTemp.Substring(iLen - 1) = "/" Then
                        If MidB(strTemp, iLen - 1) = "/" Then
                            'strTemp = strTemp.Remove(iLen - 1)
                            strTemp = MidB(strTemp, 0, iLen - 1)
                        End If
                    End If
                End If
            End If
            pstrStatus = strTemp
        End With
    End Sub
#End Region


#Region "Ver2.0.0.2 OR取得関数"
    'FU,Port,PINから、ORのCHデータを全て取得する
    Private Function fnGetOrAnd(pintFU As Integer, pintPort As Integer, pintPIN As Integer) As String
        Dim strRet As String = ""

        With gudt.SetChOutput
            For i As Integer = 0 To UBound(.udtCHOutPut) Step 1
                If .udtCHOutPut(i).bytFuno = pintFU And _
                    .udtCHOutPut(i).bytPortno = pintPort And _
                    .udtCHOutPut(i).bytPin = pintPIN Then
                    '一致したら、該当OrAnd
                    If .udtCHOutPut(i).bytType <> gCstCodeFuOutputChTypeCh Then
                        For j As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr) Step 1
                            If gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid <> 0 Then
                                strRet = strRet & gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid & " "
                            End If
                        Next j
                        strRet = strRet.Trim
                        strRet = strRet.Replace(" ", ",")
                        strRet = pintPIN.ToString & ":" & strRet
                        Exit For
                    End If
                End If
            Next i
        End With

        Return strRet
    End Function
#End Region

    '----------------------------------------------------------------------------
    ' 機能説明  ： ２行用の文字描写関数
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 描く文字
    '           ： ARG3 - (I ) １行あたりの最大文字数
    '           ： ARG4 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG5 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsDigitalLine2(ByVal hobjGraphics As Object, _
                                         ByVal hstrTargetStrings As String, _
                                         ByVal hintLineLength As Integer, _
                                         ByVal hintX As Integer, _
                                         ByVal hsngY As Single)

        Try

            Dim strLine As String = ""
            Dim all_len As Integer
            Dim line_len As Integer

            Dim iFind1 As Integer = hstrTargetStrings.IndexOf("^"c)     '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞ検索

            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                ''MAXを24文字から20文字に変更、フォントサイズの変更はなし    2013.11.19
                ''全角時の分割処理変更    2015.02.03
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞ対応
                If iFind1 <> -1 Then
                    ''1行目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 3)

                    ''2行目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 14)
                Else
                    all_len = LenB(hstrTargetStrings)
                    If all_len > hintLineLength Then
                        ''1行目
                        strLine = fStrCut(hstrTargetStrings, 0, hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 3)

                        ''2行目
                        line_len = LenB(strLine.Trim)
                        strLine = fStrCut(hstrTargetStrings, line_len, all_len - line_len)
                        hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 14)
                    Else
                        ''1行表示
                        hobjGraphics.DrawString(hstrTargetStrings, gFnt8j, gFntColorBlack, hintX, hsngY + 8)
                    End If
                End If
            Else
                ''MAXを24文字から20文字に変更、フォントサイズの変更はなし    2013.11.19
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞに変更
                If iFind1 <> -1 Then
                    ''1行目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 3)

                    ''2行目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 14)
                Else
                    If hstrTargetStrings.Length > hintLineLength Then
                        ''1行目
                        strLine = hstrTargetStrings.Substring(0, hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 3)

                        ''2行目
                        strLine = hstrTargetStrings.Substring(hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 14)
                    Else
                        ''1行表示
                        hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 8)
                    End If
                End If
            End If

            ''WIRE MARK, DESTは1行MAX10文字までは通常のフォントサイズ
            ''11文字以上はフォントサイズを小さくし、13文字以上で2行とする
            'If hstrTargetStrings.Length > hintLineLength Then

            '    If hstrTargetStrings.Length > mCstCodeMaxLength12 Then
            '        ''1行目
            '        strLine = hstrTargetStrings.Substring(0, mCstCodeMaxLength12)
            '        hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 3)

            '        ''2行目
            '        strLine = hstrTargetStrings.Substring(mCstCodeMaxLength12)
            '        hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 14)
            '    Else
            '        ''1行表示
            '        hobjGraphics.DrawString(hstrTargetStrings, gFnt7, gFntColorBlack, hintX, hsngY + 8)
            '    End If

            'Else
            '    ''1行表示
            '    hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 8)
            'End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "アナログ設定"

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログ タイトル行印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始 Y軸
    '           ： ARG3 - (I ) スロットタイプ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawTitleAnalog(ByVal objGraphics As System.Drawing.Graphics, _
                                 ByVal sngY As Single, _
                                 ByVal udtPageType As gEnmPrintTerminalPageType, _
                                 ByVal hPortType As Integer)

        Try

            Dim sngHalfPos As Single
            Dim sngHalfRowHight As Single
            Dim sngMidLine As Single
            Dim sngRowHight As Single

            sngHalfRowHight = (mCstRowHightAnalogHeader / 2)
            sngMidLine = gCstFrameTerminalUp + (mCstRowHightAnalogHeader / 2)
            sngRowHight = mCstRowHightAnalogHeader + gCstFrameTerminalUp
            sngHalfPos = sngY + 1 + (mCstRowHightAnalogHeader / 2)

            ''===================
            '' ライン描画
            ''===================
            ''縦線
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogAdd, gCstFrameTerminalUp, mCstPosAnalogAdd, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogChNo, gCstFrameTerminalUp, mCstPosAnalogChNo, sngRowHight)

            ''AO基板は"SIG"欄無し
            If hPortType <> gCstCodeFuSlotTypeAO Then
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogItemName, gCstFrameTerminalUp, mCstPosAnalogItemName, sngRowHight)
            End If

            objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig, gCstFrameTerminalUp, mCstPosAnalogSig, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, gCstFrameTerminalUp, mCstPosAnalogCoreNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, gCstFrameTerminalUp, mCstPosAnalogTermNo, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogStaus, gCstFrameTerminalUp, mCstPosAnalogStaus, sngRowHight)
            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogRange, gCstFrameTerminalUp, mCstPosAnalogRange, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogMark, sngMidLine, mCstPosAnalogMark, sngRowHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogClass, gCstFrameTerminalUp, mCstPosAnalogClass, sngRowHight)

            ''横線（中間線）
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogStaus, sngMidLine, mCstPosAnalogClass, sngMidLine)

            ''横線（最下線）
            objGraphics.DrawLine(Pens.Black, gCstFrameTerminalLeft, gCstFrameTerminalUp + mCstRowHightAnalogHeader, gCstFrameTerminalLeft + gCstFrameTerminalWidth, sngRowHight)


            ''===================
            '' 文字列印字
            ''===================
            ' 2015.10.16
            ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加

            '和文仕様 20200217 hori
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString(mCstLabelAnalogAdd1Jpn, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft, sngY + 6)     ''Add
                    objGraphics.DrawString(mCstLabelAnalogAdd2Jpn, gFnt8j, gFntColorBlack, gCstFrameTerminalLeft, sngY + 18)    ''Add
                    objGraphics.DrawString(mCstLabelAnalogCh1Jpn, gFnt8, gFntColorBlack, mCstPosAnalogAdd + 5, sngY + 4)        ''Ch No
                    objGraphics.DrawString(mCstLabelAnalogCh2Jpn, gFnt8j, gFntColorBlack, mCstPosAnalogAdd + 2, sngY + 20)     ''Ch No

                Else
                    objGraphics.DrawString(mCstLabelAnalogAdd1Jpn, gFnt7j, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 6)     ''Add
                    objGraphics.DrawString(mCstLabelAnalogAdd2Jpn, gFnt7j, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 18)    ''Add
                    objGraphics.DrawString(mCstLabelAnalogCh1Jpn, gFnt7, gFntColorBlack, mCstPosAnalogAdd + 10, sngY + 6)           ''Ch No
                    objGraphics.DrawString(mCstLabelAnalogCh2Jpn, gFnt7j, gFntColorBlack, mCstPosAnalogAdd + 10, sngY + 18)          ''Ch No

                End If
                objGraphics.DrawString(mCstLabelAnalogItemNameJpn, gFnt8j, gFntColorBlack, mCstPosAnalogChNo + 80, sngY + 12)   ''Item Name

                ''AO基板は"SIG"欄無し
                If hPortType <> gCstCodeFuSlotTypeAO Then
                    objGraphics.DrawString(mCstLabelAnalogSigJpn, gFnt8, gFntColorBlack, mCstPosAnalogItemName + 3, sngY + 11)     ''Sig
                End If

                objGraphics.DrawString(mCstLabelAnalogCoreNo1Jpn, gFnt8j, gFntColorBlack, mCstPosAnalogSig + 2, sngY + 7)           ''Core No1
                objGraphics.DrawString(mCstLabelAnalogCoreNo2Jpn, gFnt8j, gFntColorBlack, mCstPosAnalogSig + 2, sngY + 21)          ''Core No2
                objGraphics.DrawString(mCstLabelAnalogTermNo1Jpn, gFnt8j, gFntColorBlack, mCstPosAnalogCoreNo + 2, sngY + 7)        ''Term No1
                objGraphics.DrawString(mCstLabelAnalogTermNo2Jpn, gFnt8j, gFntColorBlack, mCstPosAnalogCoreNo + 2, sngY + 21)       ''Term No2

                objGraphics.DrawString(mCstLabelAnalogStatusJpn, gFnt8j, gFntColorBlack, mCstPosAnalogTermNo + 30, sngY + 11)   ''Status
                'objGraphics.DrawString(mCstLabelAnalogRange, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 15, sngY + 11)     ''Range
                objGraphics.DrawString(mCstLabelAnalogDistJpn, gFnt8j, gFntColorBlack, mCstPosAnalogClass + 11, sngY + 11)      ''Dist

                ''2線式と3線式でヘッダーラベルの表示切替え
                Select Case udtPageType

                    Case gEnmPrintTerminalPageType.tptAnalog2
                        If hPortType = gCstCodeFuSlotTypeAO Then    ''AO基板
                            objGraphics.DrawString(mCstLabelAOLineCableJpn, gFnt8j, gFntColorBlack, mCstPosAnalogStaus + 84, sngY + 3)     ''CABLE
                            objGraphics.DrawString(mCstLabelAOLineMarkJpn, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 12, sngHalfPos)    ''OUT(+) (LOW)
                            objGraphics.DrawString(mCstLabelAOLineClassJpn, gFnt8, gFntColorBlack, mCstPosAnalogMark + 10, sngHalfPos)    ''COM(-) (UPP)
                        Else
                            '' 位置調整 2013.11.20
                            'objGraphics.DrawString(mCstLabelAnalog2LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 46, sngY + 1)     ''CABLE (WIRE MARK)
                            'objGraphics.DrawString(mCstLabelAnalog2LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 26, sngHalfPos)    ''MARK (IN)
                            'objGraphics.DrawString(mCstLabelAnalog2LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 16, sngHalfPos)    ''CLASS (COM)
                            objGraphics.DrawString(mCstLabelAnalog2LineCableJpn, gFnt8j, gFntColorBlack, mCstPosAnalogStaus + 34, sngY + 3)     ''CABLE (WIRE MARK)
                            objGraphics.DrawString(mCstLabelAnalog2LineMarkJpn, gFnt8j, gFntColorBlack, mCstPosAnalogStaus + 24, sngHalfPos + 2)    ''MARK (IN)
                            objGraphics.DrawString(mCstLabelAnalog2LineClassJpn, gFnt8j, gFntColorBlack, mCstPosAnalogMark + 20, sngHalfPos + 2)    ''CLASS (COM)
                        End If

                    Case gEnmPrintTerminalPageType.tptAnalog3
                        '' 位置調整 2013.11.20
                        'objGraphics.DrawString(mCstLabelAnalog3LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 84, sngY + 1)     ''Cable
                        'objGraphics.DrawString(mCstLabelAnalog3LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 37, sngHalfPos)    ''Mark
                        'objGraphics.DrawString(mCstLabelAnalog3LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 32, sngHalfPos)    ''Class
                        objGraphics.DrawString(mCstLabelAnalog3LineCableJpn, gFnt8j, gFntColorBlack, mCstPosAnalogStaus + 82, sngY + 3)     ''Cable
                        objGraphics.DrawString(mCstLabelAnalog3LineMarkJpn, gFnt8j, gFntColorBlack, mCstPosAnalogStaus + 40, sngHalfPos + 3)    ''Mark
                        objGraphics.DrawString(mCstLabelAnalog3LineClassJpn, gFnt8j, gFntColorBlack, mCstPosAnalogMark + 35, sngHalfPos + 3)    ''Class

                End Select
            Else
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString(mCstLabelAnalogAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 11)     ''Add
                    objGraphics.DrawString(mCstLabelAnalogCh, gFnt8, gFntColorBlack, mCstPosAnalogAdd, sngY + 11)           ''Ch No
                Else
                    objGraphics.DrawString(mCstLabelAnalogAdd, gFnt7, gFntColorBlack, gCstFrameTerminalLeft + 1, sngY + 11)     ''Add
                    objGraphics.DrawString(mCstLabelAnalogCh, gFnt7, gFntColorBlack, mCstPosAnalogAdd + 10, sngY + 11)           ''Ch No
                End If
                objGraphics.DrawString(mCstLabelAnalogItemName, gFnt8, gFntColorBlack, mCstPosAnalogChNo + 70, sngY + 11)   ''Item Name

                ''AO基板は"SIG"欄無し
                If hPortType <> gCstCodeFuSlotTypeAO Then
                    objGraphics.DrawString(mCstLabelAnalogSig, gFnt8, gFntColorBlack, mCstPosAnalogItemName + 3, sngY + 11)     ''Sig
                End If

                objGraphics.DrawString(mCstLabelAnalogCoreNo1, gFnt8, gFntColorBlack, mCstPosAnalogSig, sngY + 7)           ''Core No1
                objGraphics.DrawString(mCstLabelAnalogCoreNo2, gFnt8, gFntColorBlack, mCstPosAnalogSig, sngY + 17)          ''Core No2
                objGraphics.DrawString(mCstLabelAnalogTermNo1, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo, sngY + 7)        ''Term No1
                objGraphics.DrawString(mCstLabelAnalogTermNo2, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo, sngY + 17)       ''Term No2

                objGraphics.DrawString(mCstLabelAnalogStatus, gFnt8, gFntColorBlack, mCstPosAnalogTermNo + 24, sngY + 11)   ''Status
                'objGraphics.DrawString(mCstLabelAnalogRange, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 15, sngY + 11)     ''Range
                objGraphics.DrawString(mCstLabelAnalogDist, gFnt8, gFntColorBlack, mCstPosAnalogClass + 21, sngY + 11)      ''Dist

                ''2線式と3線式でヘッダーラベルの表示切替え
                Select Case udtPageType

                    Case gEnmPrintTerminalPageType.tptAnalog2
                        If hPortType = gCstCodeFuSlotTypeAO Then    ''AO基板
                            objGraphics.DrawString(mCstLabelAOLineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 84, sngY + 1)     ''CABLE
                            objGraphics.DrawString(mCstLabelAOLineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 12, sngHalfPos)    ''OUT(+) (LOW)
                            objGraphics.DrawString(mCstLabelAOLineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 10, sngHalfPos)    ''COM(-) (UPP)
                        Else
                            '' 位置調整 2013.11.20
                            'objGraphics.DrawString(mCstLabelAnalog2LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 46, sngY + 1)     ''CABLE (WIRE MARK)
                            'objGraphics.DrawString(mCstLabelAnalog2LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 26, sngHalfPos)    ''MARK (IN)
                            'objGraphics.DrawString(mCstLabelAnalog2LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 16, sngHalfPos)    ''CLASS (COM)
                            objGraphics.DrawString(mCstLabelAnalog2LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 44, sngY + 1)     ''CABLE (WIRE MARK)
                            objGraphics.DrawString(mCstLabelAnalog2LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 24, sngHalfPos)    ''MARK (IN)
                            objGraphics.DrawString(mCstLabelAnalog2LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 14, sngHalfPos)    ''CLASS (COM)
                        End If

                    Case gEnmPrintTerminalPageType.tptAnalog3
                        '' 位置調整 2013.11.20
                        'objGraphics.DrawString(mCstLabelAnalog3LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 84, sngY + 1)     ''Cable
                        'objGraphics.DrawString(mCstLabelAnalog3LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 37, sngHalfPos)    ''Mark
                        'objGraphics.DrawString(mCstLabelAnalog3LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 32, sngHalfPos)    ''Class
                        objGraphics.DrawString(mCstLabelAnalog3LineCable, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 82, sngY + 1)     ''Cable
                        objGraphics.DrawString(mCstLabelAnalog3LineMark, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 35, sngHalfPos)    ''Mark
                        objGraphics.DrawString(mCstLabelAnalog3LineClass, gFnt8, gFntColorBlack, mCstPosAnalogMark + 30, sngHalfPos)    ''Class

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログ 図形描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) グラフタイプ（2線式 or 3線式）
    '           ： ARG3 - (I ) Draw開始位置 X軸
    '           ： ARG4 - (I ) Draw開始位置 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsAnalog(ByVal objGraphics As System.Drawing.Graphics, _
                                    ByVal udtGraphType As gEnmPrintTerminalPageType, _
                                    ByVal hPortType As Integer, _
                                    ByVal sngLeft As Single, _
                                    ByVal sngHight As Single)

        Try

            Dim intRowHight As Integer

            ''縦線
            If udtGraphType = gEnmPrintTerminalPageType.tptAnalog2 Then intRowHight = mCstRowHightAnalog2Line
            If udtGraphType = gEnmPrintTerminalPageType.tptAnalog3 Then intRowHight = mCstRowHightAnalog3Line

            objGraphics.DrawLine(Pens.Black, mCstPosAnalogAdd, sngHight, mCstPosAnalogAdd, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogChNo, sngHight, mCstPosAnalogChNo, intRowHight + sngHight)

            ''AO基板は"SIG"欄無し
            If hPortType <> gCstCodeFuSlotTypeAO Then
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogItemName, sngHight, mCstPosAnalogItemName, intRowHight + sngHight)
            End If

            objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig, sngHight, mCstPosAnalogSig, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, sngHight, mCstPosAnalogCoreNo, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight, mCstPosAnalogTermNo, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogStaus, sngHight, mCstPosAnalogStaus, intRowHight + sngHight)
            '            objGraphics.DrawLine(Pens.Black, mCstPosAnalogRange, sngHight, mCstPosAnalogRange, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogMarkNo, sngHight, mCstPosAnalogMarkNo, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogMark, sngHight, mCstPosAnalogMark, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogClassNo, sngHight, mCstPosAnalogClassNo, intRowHight + sngHight)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogClass, sngHight, mCstPosAnalogClass, intRowHight + sngHight)

            ''横線
            Select Case udtGraphType
                Case gEnmPrintTerminalPageType.tptAnalog2 ''2線式

                    For i As Integer = 0 To mCstRowCntAnalog2Mid
                        ''AO基板は"SIG"欄無し
                        If hPortType = gCstCodeFuSlotTypeAO Then
                            objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig, sngHight + mCstRowHightAnalog2Mid, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid)
                        Else
                            objGraphics.DrawLine(Pens.Black, mCstPosAnalogItemName, sngHight + mCstRowHightAnalog2Mid, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid)
                        End If
                    Next
                    'objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightAnalog2Line + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightAnalog2Line + sngHight)
                    objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightAnalog2Line + sngHight, mCstPosAnalogTermNo, mCstRowHightAnalog2Line + sngHight)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogStaus, mCstRowHightAnalog2Line + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightAnalog2Line + sngHight)

                    ''横線（SIGNAL）
                    ''AO基板はMETER表示
                    If hPortType = gCstCodeFuSlotTypeAO Then
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid - 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid - 13)         '上線
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid + 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid + 13)         '下線
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid - 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid - 10)    '縦線
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid + 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid + 10)    '縦線
                    Else
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid - 13, mCstPosAnalogTermNo + 10, sngHight + mCstRowHightAnalog2Mid - 13)         '上線(左)
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 50, sngHight + mCstRowHightAnalog2Mid - 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid - 13)    '上線(右)
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog2Mid + 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid + 13)         '下線
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid - 13, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog2Mid + 13)    '縦線
                    End If


                Case gEnmPrintTerminalPageType.tptAnalog3 ''3線式

                    For i = 0 To mCstRowCntAnalog3Mid
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogItemName, sngHight + mCstRowHightAnalog3Mid, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog3Mid)
                        objGraphics.DrawLine(Pens.Black, mCstPosAnalogItemName, sngHight + (mCstRowHightAnalog3Mid * 2), mCstPosAnalogTermNo, sngHight + (mCstRowHightAnalog3Mid * 2))
                    Next
                    'objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightAnalog3Line + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightAnalog3Line + sngHight)
                    objGraphics.DrawLine(Pens.Black, sngLeft, mCstRowHightAnalog3Line + sngHight, mCstPosAnalogTermNo, mCstRowHightAnalog3Line + sngHight)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogStaus, mCstRowHightAnalog3Line + sngHight, gCstFrameTerminalWidth + sngLeft, mCstRowHightAnalog3Line + sngHight)

                    ''横線（SIGNAL）
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog3Mid - 10, mCstPosAnalogTermNo + 10, sngHight + mCstRowHightAnalog3Mid - 10)         '上線(左)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 50, sngHight + mCstRowHightAnalog3Mid - 10, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog3Mid - 10)    '上線(右)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog3Mid * 2 - 10, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog3Mid * 2 - 10) '中線
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo, sngHight + mCstRowHightAnalog3Mid * 3 - 10, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog3Mid * 3 - 10) '下線
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog3Mid - 10, mCstPosAnalogTermNo + 60, sngHight + mCstRowHightAnalog3Mid * 3 - 10) '縦線

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログ２線式 文字列印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 印刷用構造体
    '           ： ARG3 - (I ) 行カウント
    '           ： ARG4 - (I ) Draw開始位置 Y軸
    '           ： ARG5 - (I ) 仮設定データ表示フラグ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalog2Line(ByVal objGraphics As System.Drawing.Graphics, _
                                        ByVal hudtPrtPageInfo As gTypFuInfoPin, _
                                        ByVal hPortType As Integer, _
                                        ByVal intRowCnt As Integer, _
                                        ByVal sngY As Single, _
                                        ByVal hblnDispDmyData As Boolean)

        Try

            Dim strRowAdd As String = (intRowCnt + 1).ToString("00")
            Dim strRowNo As String = (intRowCnt + 1).ToString     '' 2013.10.22 0埋めを廃止
            Dim strCH As String = gGetString(hudtPrtPageInfo.strChNo)
            Dim strStatus As String = gGetString(hudtPrtPageInfo.strStatus)

            'Ver2.0.6.5 2.0.7.0
            'CHが4文字未満=OR AND の場合、CH2,3は消す
            'If strCH.Length < 4 Then
            '    hudtPrtPageInfo.strChNo2 = ""
            '    hudtPrtPageInfo.strChNo3 = ""
            'End If


            ''描画必須の固定文字列
            objGraphics.DrawString(strRowAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 4, sngY + 19)       ''Add
            objGraphics.DrawString(strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 4, sngY + 19)          ''Mark No

            If hudtPrtPageInfo.strSignal = "[ AI ]" Then
                objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogMark + 1, sngY + 19) ''Class No
            ElseIf hudtPrtPageInfo.strSignal = "[ PT ]" Then
                objGraphics.DrawString("P" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogMark + 1, sngY + 19)
            Else
                objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogMark + 1, sngY + 19)
            End If

            ''AO基板は"SIG"欄無し
            If hPortType <> gCstCodeFuSlotTypeAO Then
                objGraphics.DrawString("IN" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + 5)     ''Sig1

                If (hPortType = gCstCodeFuSlotTypeAI_2) Or (hPortType = gCstCodeFuSlotTypeAI_K) Then
                    objGraphics.DrawString("COM", gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + mCstRowHightAnalog2Mid)
                    objGraphics.DrawString("  " & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + mCstRowHightAnalog2Mid + 13)
                Else
                    If hudtPrtPageInfo.strSignal = "[ AI ]" Then
                        objGraphics.DrawString(" 0V ", gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + 31)         ''Sig2
                    ElseIf hudtPrtPageInfo.strSignal = "[ PT ]" Then
                        objGraphics.DrawString("+24V", gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + 31)         ''Sig2
                    Else
                        objGraphics.DrawString(" 0V ", gFnt8, gFntColorBlack, mCstPosAnalogItemName, sngY + 31)
                    End If
                End If
            End If

            objGraphics.DrawString(strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 7, sngY + 5)          ''Term No1

            If hudtPrtPageInfo.strSignal = "[ AI ]" Then
                objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 31)   ''Term No2
            ElseIf hudtPrtPageInfo.strSignal = "[ PT ]" Then
                objGraphics.DrawString("P" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 31)   ''Term No2
            Else
                objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 31)   ''Term No2
            End If

            ''●CH未設定の場合は処理をスキップ
            'Ver2.0.3.6 ワイヤーマーク系が設定されていれば処理抜けしない
            Dim blWire As Boolean = False
            Dim intW1 As Integer = 0
            With hudtPrtPageInfo
                If IsNothing(.strWireMark) = False Then
                    For intW1 = LBound(.strWireMark) To UBound(.strWireMark)
                        If NZfS(.strWireMark(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strWireMarkClass) = False Then
                    For intW1 = LBound(.strWireMarkClass) To UBound(.strWireMarkClass)
                        If NZfS(.strWireMarkClass(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoIn) = False Then
                    For intW1 = LBound(.strCoreNoIn) To UBound(.strCoreNoIn)
                        If NZfS(.strCoreNoIn(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoCom) = False Then
                    For intW1 = LBound(.strCoreNoCom) To UBound(.strCoreNoCom)
                        If NZfS(.strCoreNoCom(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strDist) = False Then
                    For intW1 = LBound(.strDist) To UBound(.strDist)
                        If NZfS(.strDist(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
            End With

            If strCH = "" And blWire = False Then Exit Sub

            'Ver2.0.2.4 FUdummyでダミー印刷しないなら処理抜け
            If hblnDispDmyData = False Then
                If hudtPrtPageInfo.blnDmyFUadress = True Then
                    Exit Sub
                End If
            End If

            Dim strItemName As String = gGetString(hudtPrtPageInfo.strItemName)
            Dim strRangeLabel As String = ""
            Dim strRangeHigh As String = gGetString(hudtPrtPageInfo.strRangeHigh)
            Dim strRangeLow As String = gGetString(hudtPrtPageInfo.strRangeLow)
            Dim strWireMark1 As String = gGetString(hudtPrtPageInfo.strWireMark(0))
            Dim strWireCls1 As String = gGetString(hudtPrtPageInfo.strWireMarkClass(0))
            Dim strDist1 As String = gGetString(hudtPrtPageInfo.strDist(0))
            Dim sngPosChNo As Single
            Dim sngPosCoreNo1 As Single, sngPosCoreNo2 As Single

            ''仮設定データの表示判断 ------------------------------------------------------------------------------------
            If strStatus <> "" Then Call mCheckDmyStatus(strStatus, hudtPrtPageInfo, hblnDispDmyData)
            If strRangeLow <> "" And strRangeHigh <> "" Then
                mCheckDmyRange(strRangeLow, strRangeHigh, strRangeLabel, hudtPrtPageInfo, hblnDispDmyData)
            End If
            ''-----------------------------------------------------------------------------------------------------------

            ''ChNo 重複CHの印字追加    2015.02.03
            If hudtPrtPageInfo.strChNo3 <> "" Then  '' CH_NO (同一端子CH表示用)
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 8)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 19)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt8, gFntColorBlack, sngPosChNo, sngY + 30)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 8)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 19)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt7, gFntColorBlack, sngPosChNo, sngY + 30)
                End If
                '//
            ElseIf hudtPrtPageInfo.strChNo2 <> "" Then  '' CH_NO (同一端子CH表示用)
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 14)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 25)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 14)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 25)
                End If
                '//
            Else
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 19)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 19)
                End If
                '//
            End If


            ''ItemName
            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                objGraphics.DrawString(strItemName, gFnt8j, gFntColorBlack, mCstPosAnalogChNo, sngY + 19)
            Else
                objGraphics.DrawString(strItemName, gFnt8, gFntColorBlack, mCstPosAnalogChNo, sngY + 19)
            End If

            ''CoreNo
            sngPosCoreNo1 = mCstPosAnalogSig + (((mCstPosAnalogCoreNo - mCstPosAnalogSig) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoIn(0)).Length * gFntScale7) / 2))
            sngPosCoreNo2 = mCstPosAnalogSig + (((mCstPosAnalogCoreNo - mCstPosAnalogSig) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoCom(0)).Length * gFntScale7) / 2))
            objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoIn(0)), gFnt8, gFntColorBlack, sngPosCoreNo1, sngY + 5)      ''上段
            objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoCom(0)), gFnt8, gFntColorBlack, sngPosCoreNo2, sngY + 31)    ''下段

            ''Status
            'objGraphics.DrawString("(" & strStatus.PadRight(17) & ")", gFnt8, gFntColorBlack, mCstPosAnalogChNo + 72, sngY + 3)

            ''Signal
            ''AO基板はMETER表示
            If hPortType = gCstCodeFuSlotTypeAO Then
                objGraphics.DrawString("METER", gFnt8, gFntColorBlack, mCstPosAnalogTermNo + 24, sngY + 19)
                objGraphics.DrawString("+", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 65, sngY + 8)
                objGraphics.DrawString("-", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 65, sngY + 30)
            Else
                objGraphics.DrawString(hudtPrtPageInfo.strSignal, gFnt8, gFntColorBlack, mCstPosAnalogTermNo + 9, sngY + 4)
                If hudtPrtPageInfo.strSignal = "[ AI ]" Then
                    objGraphics.DrawString("+", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 5, sngY + 13)
                    objGraphics.DrawString("-", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 47, sngY + 13)
                ElseIf hudtPrtPageInfo.strSignal = "[ PT ]" Then
                    objGraphics.DrawString("-", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 5, sngY + 13)
                    objGraphics.DrawString("+", gFnt7, gFntColorBlack, mCstPosAnalogTermNo + 47, sngY + 13)
                End If
            End If

            ''AO基板は"SIG"欄無し
            If hPortType <> gCstCodeFuSlotTypeAO Then
                ''Range（※MAX 9byte表示）
                If strRangeLow <> "" And strRangeHigh <> "" Then
                    'Ver2.0.5.0 レンジのP/S対応　Ver2.0.6.5 レンジの力率対応
                    Dim strPS As String = ""
                    Dim strPF As String = ""
                    If IsNumeric(strCH) = True Then
                        Call GetPS(CInt(strCH), strPS)
                        Call GetPF(CInt(strCH), strPF)
                    End If
                    'Ver2.0.4.2 レンジ印刷しない
                    If g_bytTermRange = 0 Then
                        'する
                        'Ver2.0.7.5 レンジ文字列長さが13文字を超えたらフォントを一つ小さく
                        Dim strLen As String = ""
                        If strPS = "" Then
                            'objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog2Mid + 13)
                            strLen = strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh
                            If strLen.Length > 13 Then
                                objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt7, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog2Mid + 13)
                            Else
                                objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog2Mid + 13)
                            End If

                        Else
                            'Ver2.0.5.1 PSありの場合はレンジはHI側のみ採用
                            objGraphics.DrawString(strRangeLabel & strPS & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog2Mid + 13)
                        End If

                    End If

                End If
            End If

            ''Mark
            Call mDrawStringsAnalogLine2(objGraphics, strWireMark1, mCstCodeMaxLength10, mCstPosAnalogMarkNo, sngY)

            ''Class
            Call mDrawStringsAnalogLine2(objGraphics, strWireCls1, mCstCodeMaxLength10, mCstPosAnalogClassNo, sngY)

            ''Dist
            Call mDrawStringsAnalogLine2(objGraphics, strDist1, mCstCodeMaxLength10, mCstPosAnalogClass, sngY)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログ３線式 文字列印字
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 印刷用構造体
    '           ： ARG3 - (I ) 行カウント
    '           ： ARG4 - (I ) Draw開始位置 Y軸
    '           ： ARG5 - (I ) 仮設定データ表示フラグ ＠TerminalPrint画面
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalog3Line(ByVal objGraphics As System.Drawing.Graphics, _
                                        ByVal hudtPrtPageInfo As gTypFuInfoPin, _
                                        ByVal intRowCnt As Integer, _
                                        ByVal sngY As Single, _
                                        ByVal hblnDispDmyData As Boolean)

        Try

            Dim strRowAdd As String = (intRowCnt + 1).ToString("00")
            Dim strRowNo As String = (intRowCnt + 1).ToString     '' 2013.10.22 0埋めを廃止
            Dim strCH As String = gGetString(hudtPrtPageInfo.strChNo)

            'Ver2.0.6.5 2.0.7.0
            'CHが4文字未満=OR AND の場合、CH2,3は消す
            'If strCH.Length < 4 Then
            '    hudtPrtPageInfo.strChNo2 = ""
            '    hudtPrtPageInfo.strChNo3 = ""
            'End If

            ''描画必須の固定文字列
            objGraphics.DrawString(strRowAdd, gFnt8, gFntColorBlack, gCstFrameTerminalLeft + 4, sngY + 24)           ''Add
            objGraphics.DrawString(strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogStaus + 4, sngY + 24)              ''Mark No
            objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogMark + 1, sngY + 24)         ''Class No
            objGraphics.DrawString("A" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogItemName + 3, sngY + 4)      ''Sig1
            objGraphics.DrawString("B" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogItemName + 3, sngY + 24)     ''Sig2
            objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogItemName + 3, sngY + 45)     ''Sig3
            objGraphics.DrawString("A" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 4)        ''TermNo1
            objGraphics.DrawString("B" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 24)       ''TermNo2
            objGraphics.DrawString("C" & strRowNo, gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 3, sngY + 45)       ''TermNo3

            ''●CH未設定の場合は処理をスキップ
            'Ver2.0.3.6 ワイヤーマーク系が設定されていれば処理抜けしない
            Dim blWire As Boolean = False
            Dim intW1 As Integer = 0
            With hudtPrtPageInfo
                If IsNothing(.strWireMark) = False Then
                    For intW1 = LBound(.strWireMark) To UBound(.strWireMark)
                        If NZfS(.strWireMark(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strWireMarkClass) = False Then
                    For intW1 = LBound(.strWireMarkClass) To UBound(.strWireMarkClass)
                        If NZfS(.strWireMarkClass(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoIn) = False Then
                    For intW1 = LBound(.strCoreNoIn) To UBound(.strCoreNoIn)
                        If NZfS(.strCoreNoIn(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strCoreNoCom) = False Then
                    For intW1 = LBound(.strCoreNoCom) To UBound(.strCoreNoCom)
                        If NZfS(.strCoreNoCom(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
                If IsNothing(.strDist) = False Then
                    For intW1 = LBound(.strDist) To UBound(.strDist)
                        If NZfS(.strDist(intW1)) <> "" Then
                            blWire = True
                            Exit For
                        End If
                    Next intW1
                End If
            End With

            If strCH = "" And blWire = False Then Exit Sub

            'Ver2.0.2.4 FUdummyでダミー印刷しないなら処理抜け
            If hblnDispDmyData = False Then
                If hudtPrtPageInfo.blnDmyFUadress = True Then
                    Exit Sub
                End If
            End If

            Dim strItemName As String = gGetString(hudtPrtPageInfo.strItemName)
            Dim strRangeLabel As String = ""
            Dim strRangeHigh As String = gGetString(hudtPrtPageInfo.strRangeHigh)
            Dim strRangeLow As String = gGetString(hudtPrtPageInfo.strRangeLow)
            Dim strStatus As String = gGetString(hudtPrtPageInfo.strStatus)
            Dim strWireMark1 As String = gGetString(hudtPrtPageInfo.strWireMark(0))
            Dim strWireMark2 As String = gGetString(hudtPrtPageInfo.strWireMark(1))
            Dim strWireMark3 As String = gGetString(hudtPrtPageInfo.strWireMark(2))
            Dim strWireCls1 As String = gGetString(hudtPrtPageInfo.strWireMarkClass(0))
            Dim strWireCls2 As String = gGetString(hudtPrtPageInfo.strWireMarkClass(1))
            Dim strWireCls3 As String = gGetString(hudtPrtPageInfo.strWireMarkClass(2))
            Dim strDist1 As String = gGetString(hudtPrtPageInfo.strDist(0))
            Dim strDist2 As String = gGetString(hudtPrtPageInfo.strDist(1))
            Dim strDist3 As String = gGetString(hudtPrtPageInfo.strDist(2))
            Dim sngPosChNo As Single
            Dim sngPosCoreNoLine1 As Single, sngPosCoreNoLine2 As Single, sngPosCoreNoLine3 As Single

            ''仮設定データの表示判断 ------------------------------------------------------------------------------------
            If strStatus <> "" Then Call mCheckDmyStatus(strStatus, hudtPrtPageInfo, hblnDispDmyData)
            If strRangeLow <> "" And strRangeHigh <> "" Then
                mCheckDmyRange(strRangeLow, strRangeHigh, strRangeLabel, hudtPrtPageInfo, hblnDispDmyData)
            End If
            ''-----------------------------------------------------------------------------------------------------------

            ''ChNo  重複CHの印字追加    2015.02.03
            If hudtPrtPageInfo.strChNo3 <> "" Then  '' CH_NO (同一端子CH表示用)
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 13)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 24)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt8, gFntColorBlack, sngPosChNo, sngY + 35)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 13)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 24)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo3), gFnt7, gFntColorBlack, sngPosChNo, sngY + 35)
                End If
                '//
            ElseIf hudtPrtPageInfo.strChNo2 <> "" Then  '' CH_NO (同一端子CH表示用)
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 19)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt8, gFntColorBlack, sngPosChNo, sngY + 30)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 19)
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo2), gFnt7, gFntColorBlack, sngPosChNo, sngY + 30)
                End If
                '//
            Else
                ' 2015.10.16  ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  CHNo./ﾀｸﾞ表示切替処理追加
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    ''チャンネル番号
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale7) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt8, gFntColorBlack, sngPosChNo, sngY + 24)
                Else
                    sngPosChNo = mCstPosAnalogAdd + (((mCstPosAnalogChNo - mCstPosAnalogAdd) / 2) - ((strCH.Length * gFntScale6) / 2))
                    objGraphics.DrawString(gGetString(hudtPrtPageInfo.strChNo), gFnt7, gFntColorBlack, sngPosChNo, sngY + 24)
                End If
                '//

            End If


                ''Status
                'objGraphics.DrawString("(" & strStatus.PadRight(17) & ")", gFnt8, gFntColorBlack, mCstPosAnalogChNo + 72, sngY + 3)

                ''Signal
                objGraphics.DrawString(hudtPrtPageInfo.strSignal, gFnt8, gFntColorBlack, mCstPosAnalogTermNo + 9, sngY + 4)

                ''ItemName
            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                objGraphics.DrawString(strItemName, gFnt8j, gFntColorBlack, mCstPosAnalogChNo, sngY + 24)
            Else
                objGraphics.DrawString(strItemName, gFnt8, gFntColorBlack, mCstPosAnalogChNo, sngY + 24)
            End If

                ''Range（※MAX 9byte表示）
            If strRangeLow <> "" And strRangeHigh <> "" Then
                'Ver2.0.0.0 レンジのP/S対応 Ver2.0.6.5 レンジの力率対応
                Dim strPS As String = ""
                Dim strPF As String = ""
                If IsNumeric(strCH) = True Then
                    Call GetPS(CInt(strCH), strPS)
                    Call GetPF(CInt(strCH), strPF)
                End If
                'Ver2.0.4.2 レンジ印刷しない
                If g_bytTermRange = 0 Then
                    'する
                    'Ver2.0.7.5 レンジ文字列長さが13文字を超えたらフォントを一つ小さく
                    If strPS = "" Then
                        'objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog3Mid * 2 + 10)
                        Dim strLen As String = strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh
                        If strLen.Length > 13 Then
                            objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt7, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog3Mid * 2 + 10)
                        Else
                            objGraphics.DrawString(strRangeLabel & strPS & strRangeLow & "/" & strPF & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog3Mid * 2 + 10)
                        End If
                    Else
                        'Ver2.0.5.1 PSありの場合はレンジはHI側のみ採用
                        objGraphics.DrawString(strRangeLabel & strPS & strRangeHigh, gFnt8, gFntColorBlack, mCstPosAnalogTermNo, sngY + mCstRowHightAnalog3Mid * 2 + 10)
                    End If
                End If

            End If

                ''CoreNo
                sngPosCoreNoLine1 = mCstPosAnalogSig + (((mCstPosAnalogCoreNo - mCstPosAnalogSig) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoIn(0)).Length * gFntScale7) / 2))
                sngPosCoreNoLine2 = mCstPosAnalogSig + (((mCstPosAnalogCoreNo - mCstPosAnalogSig) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoIn(1)).Length * gFntScale7) / 2))
                sngPosCoreNoLine3 = mCstPosAnalogSig + (((mCstPosAnalogCoreNo - mCstPosAnalogSig) / 2) - ((gGetString(hudtPrtPageInfo.strCoreNoIn(2)).Length * gFntScale7) / 2))
                objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoIn(0)), gFnt8, gFntColorBlack, sngPosCoreNoLine1, sngY + 4)
                objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoIn(1)), gFnt8, gFntColorBlack, sngPosCoreNoLine2, sngY + 24)
                objGraphics.DrawString(gGetString(hudtPrtPageInfo.strCoreNoIn(2)), gFnt8, gFntColorBlack, sngPosCoreNoLine3, sngY + 45)

                ''WireMark
                Call mDrawStringsAnalogLine6(mCstCodePrintLine1, objGraphics, strWireMark1, mCstCodeMaxLength10, mCstPosAnalogMarkNo, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine2, objGraphics, strWireMark2, mCstCodeMaxLength10, mCstPosAnalogMarkNo, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine3, objGraphics, strWireMark3, mCstCodeMaxLength10, mCstPosAnalogMarkNo, sngY)

                ''WireClass
                Call mDrawStringsAnalogLine6(mCstCodePrintLine1, objGraphics, strWireCls1, mCstCodeMaxLength10, mCstPosAnalogClassNo, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine2, objGraphics, strWireCls2, mCstCodeMaxLength10, mCstPosAnalogClassNo, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine3, objGraphics, strWireCls3, mCstCodeMaxLength10, mCstPosAnalogClassNo, sngY)

                ''Dist
                Call mDrawStringsAnalogLine6(mCstCodePrintLine1, objGraphics, strDist1, mCstCodeMaxLength10, mCstPosAnalogClass, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine2, objGraphics, strDist2, mCstCodeMaxLength10, mCstPosAnalogClass, sngY)
                Call mDrawStringsAnalogLine6(mCstCodePrintLine3, objGraphics, strDist3, mCstCodeMaxLength10, mCstPosAnalogClass, sngY)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "Ver2.0.0.0 レンジのP/S対応関数"
    ' CHリストからひっぱってくる
    Private Sub GetPS(pintCHNo As Integer, ByRef pstrPS As String)
        Dim iData As Integer

        Dim strTemp As String = ""

        '該当CHデータを探す
        Dim bHitFlg As Boolean = False
        For iData = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
            If gudt.SetChInfo.udtChannel(iData).udtChCommon.shtChno = pintCHNo Then
                bHitFlg = True
                Exit For
            End If
        Next iData

        'CHNoがみつからないなら何もしないで処理抜け
        If bHitFlg = False Then
            Return
        End If

        With gudt.SetChInfo.udtChannel(iData)
            strTemp = ""
            If gBitCheck(.udtChCommon.shtFlag1, 8) Then
                strTemp = "P/S"
            ElseIf gBitCheck(.udtChCommon.shtFlag1, 9) Then
                'Ver2.0.7.9 A/F対応
                strTemp = "A/F"
            End If
            pstrPS = strTemp
        End With
    End Sub
#End Region

#Region "Ver2.0.6.5 レンジの力率対応関数"
    ' CHリストからひっぱってくる
    Private Sub GetPF(pintCHNo As Integer, ByRef pstrPF As String)
        Dim iData As Integer

        Dim strTemp As String = ""

        '該当CHデータを探す
        Dim bHitFlg As Boolean = False
        For iData = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
            If gudt.SetChInfo.udtChannel(iData).udtChCommon.shtChno = pintCHNo Then
                bHitFlg = True
                Exit For
            End If
        Next iData

        'CHNoがみつからないなら何もしないで処理抜け
        If bHitFlg = False Then
            Return
        End If

        With gudt.SetChInfo.udtChannel(iData)
            strTemp = ""
            If gBitCheck(.udtChCommon.shtFlag1, 5) Then
                strTemp = "1.00/"
            End If
            pstrPF = strTemp
        End With
    End Sub
#End Region


    '----------------------------------------------------------------------------
    ' 機能説明  ： DC4-20アナログ基板 CAUITON図描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) グラフタイプ（2線式 or 3線式）
    '           ： ARG3 - (I ) Draw開始位置 X軸
    '           ： ARG4 - (I ) Draw開始位置 Y軸
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsCaution(ByVal objGraphics As System.Drawing.Graphics, ByVal sngY As Single)

        Try
            'Dim intRowHight As Integer = 0
            'Dim sinRowHight As Single = 0
            Dim Base_y As Single
            Dim i As Integer
            Dim p2 As New Pen(Color.Black, 2)
            Dim f2 As New Font("Courier New", 8, FontStyle.Bold)

            objGraphics.DrawString("[CAUTION]", f2, gFntColorBlack, mCstPosAnalogAdd, sngY + 10)

            '高さ調整 2015/4/10 T.Ueki
            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, sngY, mCstPosAnalogCoreNo, sngY + 185)    '縦線
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, sngY, mCstPosAnalogCoreNo, sngY + 170)    '縦線

            For i = 0 To 1
                Base_y = sngY + (80 * i)

                ''===================
                '' ライン描画
                ''===================
                objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 10, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX1
                objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 25, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX2
                objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 40, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX3

                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 48, mCstPosAnalogSig, Base_y + 48)                '横線

                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 48, mCstPosAnalogSig - 100, Base_y + 26)          '縦線1(上)
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 16, mCstPosAnalogSig - 100, Base_y + 6)          '縦線2(上)
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 105, Base_y + 26, mCstPosAnalogSig - 100, Base_y + 16)
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 95, Base_y + 26, mCstPosAnalogSig - 100, Base_y + 16)
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 105, Base_y + 26, mCstPosAnalogSig - 95, Base_y + 26)

                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 60, Base_y + 48, mCstPosAnalogSig - 60, Base_y + 56)            '抵抗縦線1(下)
                objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 60, Base_y + 70, mCstPosAnalogSig - 60, Base_y + 78)            '抵抗縦線2(下)
                objGraphics.DrawLine(p2, mCstPosAnalogSig - 64, Base_y + 78, mCstPosAnalogSig - 56, Base_y + 78)                    'GND

                ''===================
                '' 文字列印字
                ''===================
                '' Ver1.8.3 2015.11.26  SMS-22-Kに合わせるため変更DC4-20mA基板の特記事項は不具合のため修正
                '' Ver1.8.4 2015.11.27  
                '' ''If g_bytFUSet = 0 Then      '' 出荷済みｵｰﾀﾞｰ用
                '' ''    objGraphics.DrawString(" C" & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 10)
                '' ''    objGraphics.DrawString(" P" & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 25)
                '' ''    objGraphics.DrawString("  " & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 40)
                '' ''Else
                objGraphics.DrawString(" C" & (i + 1), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 10)
                objGraphics.DrawString(" P" & (i + 1), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 25)
                objGraphics.DrawString("  " & (i + 1), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 40)
                '' ''End If

                objGraphics.DrawString("  0V ←", gFnt8, gFntColorBlack, mCstPosAnalogSig - 48, Base_y + 10)
                objGraphics.DrawString("+24V →", gFnt8, gFntColorBlack, mCstPosAnalogSig - 48, Base_y + 25)

                objGraphics.DrawString("[100 Ω]", gFnt8, gFntColorBlack, mCstPosAnalogSig - 84, Base_y + 56)
                objGraphics.DrawString("GND", gFnt8, gFntColorBlack, mCstPosAnalogSig - 90, Base_y + 71)

                If i = 0 Then
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 33, mCstPosAnalogCoreNo + 90, Base_y + 33)           'SIGNAL(上線)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 48, mCstPosAnalogCoreNo + 25, Base_y + 48)           'SIGNAL(下線1)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 65, Base_y + 48, mCstPosAnalogCoreNo + 90, Base_y + 48)      'SIGNAL(下線2)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 90, Base_y + 33, mCstPosAnalogCoreNo + 90, Base_y + 48)      'SIGNAL(縦線)

                    objGraphics.DrawString("[ PT ]", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 23, Base_y + 40)
                    objGraphics.DrawString("-", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 17, Base_y + 48)
                    objGraphics.DrawString("+", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, Base_y + 48)

                    If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                        objGraphics.DrawString("4-20mA", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 10)  '和文仕様 20200217 hori
                        objGraphics.DrawString("フィールドユニットからDC24V電源を供給する場合", gFnt7j, gFntColorBlack, mCstPosAnalogCoreNo + 150, Base_y + 10)
                        objGraphics.DrawString("(圧力発信器等)", gFnt7j, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 25)
                    Else
                        objGraphics.DrawString("4-20mA WITHOUT ELECTRIC POWER", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 10)
                        objGraphics.DrawString("(PRESSURE TRANSMITTER etc...)", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 25)
                    End If
                Else
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 18, mCstPosAnalogCoreNo + 90, Base_y + 18)           'SIGNAL(上線)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 48, mCstPosAnalogCoreNo + 25, Base_y + 48)           'SIGNAL(下線1)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 65, Base_y + 48, mCstPosAnalogCoreNo + 90, Base_y + 48)      'SIGNAL(下線2)
                    objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 90, Base_y + 18, mCstPosAnalogCoreNo + 90, Base_y + 48)      'SIGNAL(縦線)

                    objGraphics.DrawString("[ AI ]", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 23, Base_y + 40)
                    objGraphics.DrawString("+", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 17, Base_y + 34)
                    objGraphics.DrawString("-", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, Base_y + 34)

                    If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                        objGraphics.DrawString("4-20mA", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 10) '和文仕様 20200217 hori
                        objGraphics.DrawString("フィールドユニットからDC24V電源を供給しない場合", gFnt7j, gFntColorBlack, mCstPosAnalogCoreNo + 150, Base_y + 10) '和文仕様 20200217 hori
                        objGraphics.DrawString("(アイソレータ等)", gFnt7j, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 25)
                    Else
                        objGraphics.DrawString("4-20mA WITH ELECTRIC POWER", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 10)
                        objGraphics.DrawString("(ISOLATOR etc...)", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 25)
                    End If

                End If

            Next

            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 158, mCstPosAnalogCoreNo + 65, sngY + 158)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 158, mCstPosAnalogCoreNo + 45, sngY + 134)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 40, sngY + 144, mCstPosAnalogCoreNo + 45, sngY + 134)
            objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 134, mCstPosAnalogCoreNo + 50, sngY + 144)

            objGraphics.DrawString("DC24V", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, sngY + 151)

            'For i = 0 To 1
            '    Base_y = sngY + (80 * i)

            '    ''===================
            '    '' ライン描画
            '    ''===================
            '    objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 20, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX1
            '    objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 35, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX2
            '    objGraphics.DrawRectangle(Pens.Black, mCstPosAnalogSig, Base_y + 50, mCstPosAnalogCoreNo - mCstPosAnalogSig, 15)  'BOX3

            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 58, mCstPosAnalogSig, Base_y + 58)                '横線

            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 58, mCstPosAnalogSig - 100, Base_y + 36)          '縦線1(上)
            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 100, Base_y + 26, mCstPosAnalogSig - 100, Base_y + 16)          '縦線2(上)
            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 105, Base_y + 36, mCstPosAnalogSig - 100, Base_y + 26)
            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 95, Base_y + 36, mCstPosAnalogSig - 100, Base_y + 26)
            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 105, Base_y + 36, mCstPosAnalogSig - 95, Base_y + 36)

            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 60, Base_y + 58, mCstPosAnalogSig - 60, Base_y + 66)            '抵抗縦線1(下)
            '    objGraphics.DrawLine(Pens.Black, mCstPosAnalogSig - 60, Base_y + 80, mCstPosAnalogSig - 60, Base_y + 88)            '抵抗縦線2(下)
            '    objGraphics.DrawLine(p2, mCstPosAnalogSig - 64, Base_y + 88, mCstPosAnalogSig - 56, Base_y + 88)                    'GND

            '    ''===================
            '    '' 文字列印字
            '    ''===================
            '    objGraphics.DrawString(" C" & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 20)
            '    objGraphics.DrawString(" P" & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 35)
            '    objGraphics.DrawString("  " & (i + 1).ToString("00"), gFnt8, gFntColorBlack, mCstPosAnalogSig, Base_y + 50)

            '    objGraphics.DrawString("  0V ←", gFnt8, gFntColorBlack, mCstPosAnalogSig - 48, Base_y + 20)
            '    objGraphics.DrawString("+24V →", gFnt8, gFntColorBlack, mCstPosAnalogSig - 48, Base_y + 35)

            '    objGraphics.DrawString("[100 Ω]", gFnt8, gFntColorBlack, mCstPosAnalogSig - 84, Base_y + 66)
            '    objGraphics.DrawString("GND", gFnt8, gFntColorBlack, mCstPosAnalogSig - 90, Base_y + 81)

            '    If i = 0 Then
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 43, mCstPosAnalogCoreNo + 90, Base_y + 43)           'SIGNAL(上線)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 58, mCstPosAnalogCoreNo + 25, Base_y + 58)           'SIGNAL(下線1)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 65, Base_y + 58, mCstPosAnalogCoreNo + 90, Base_y + 58)      'SIGNAL(下線2)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 90, Base_y + 43, mCstPosAnalogCoreNo + 90, Base_y + 58)      'SIGNAL(縦線)

            '        objGraphics.DrawString("[ PT ]", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 23, Base_y + 50)
            '        objGraphics.DrawString("-", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 17, Base_y + 58)
            '        objGraphics.DrawString("+", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, Base_y + 58)

            '        objGraphics.DrawString("4-20mA WITHOUT ELECTRIC POWER", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 20)
            '        objGraphics.DrawString("(PRESSURE TRANSMITTER etc...)", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 35)
            '    Else
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 28, mCstPosAnalogCoreNo + 90, Base_y + 28)           'SIGNAL(上線)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo, Base_y + 58, mCstPosAnalogCoreNo + 25, Base_y + 58)           'SIGNAL(下線1)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 65, Base_y + 58, mCstPosAnalogCoreNo + 90, Base_y + 58)      'SIGNAL(下線2)
            '        objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 90, Base_y + 28, mCstPosAnalogCoreNo + 90, Base_y + 58)      'SIGNAL(縦線)

            '        objGraphics.DrawString("[ AI ]", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 23, Base_y + 50)
            '        objGraphics.DrawString("+", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 17, Base_y + 44)
            '        objGraphics.DrawString("-", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, Base_y + 44)

            '        objGraphics.DrawString("4-20mA WITH ELECTRIC POWER", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 20)
            '        objGraphics.DrawString("(ISOLATOR etc...)", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 100, Base_y + 35)
            '    End If

            'Next

            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 168, mCstPosAnalogCoreNo + 65, sngY + 168)
            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 168, mCstPosAnalogCoreNo + 45, sngY + 144)
            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 40, sngY + 154, mCstPosAnalogCoreNo + 45, sngY + 144)
            'objGraphics.DrawLine(Pens.Black, mCstPosAnalogCoreNo + 45, sngY + 144, mCstPosAnalogCoreNo + 50, sngY + 154)

            'objGraphics.DrawString("DC24V", gFnt8, gFntColorBlack, mCstPosAnalogCoreNo + 65, sngY + 161)

            p2.Dispose()
            f2.Dispose()

            'Dim ArrowPen As New Pen(Color.Black, 1)         'ラインの色・ラインの太さを設定
            'ArrowPen.EndCap = Drawing2D.LineCap.ArrowAnchor 'ラインの先端を矢印に
            'objGraphics.DrawLine(ArrowPen, mCstPosAnalogCoreNo + 45, sngY + 168, mCstPosAnalogCoreNo + 45, sngY + 144)                  'フォームにラインを描画
            'ArrowPen.Dispose()



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "文字描写詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ２行用の文字描写関数（Status表示）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 描く文字
    '           ： ARG3 - (I ) １行あたりの最大文字数
    '           ： ARG4 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG5 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalogLine2(ByVal hobjGraphics As Object, _
                                        ByVal hstrTargetStrings As String, _
                                        ByVal hintLineLength As Integer, _
                                        ByVal hintX As Integer, _
                                        ByVal hsngY As Single)

        Try

            Dim strLine As String = ""
            Dim all_len As Integer
            Dim line_len As Integer

            Dim iFind1 As Integer = hstrTargetStrings.IndexOf("^"c)     '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞ検索

            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                ''MAXを24文字から20文字に変更、フォントサイズの変更はなし    2013.11.19
                ''全角時の分割処理変更    2015.02.03
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞに変更
                If iFind1 <> -1 Then
                    ''1行目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 14)

                    ''2行目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 25)
                Else
                    all_len = LenB(hstrTargetStrings)
                    If all_len > hintLineLength Then
                        ''1行目
                        strLine = fStrCut(hstrTargetStrings, 0, hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 14)

                        ''2行目
                        line_len = LenB(strLine.Trim)
                        strLine = fStrCut(hstrTargetStrings, line_len, all_len - line_len)
                        hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 25)
                    Else
                        ''1行表示
                        hobjGraphics.DrawString(hstrTargetStrings, gFnt8j, gFntColorBlack, hintX, hsngY + 20)
                    End If
                End If
            Else
                ''MAXを24文字から20文字に変更、フォントサイズの変更はなし    2013.11.19
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞに変更
                If iFind1 <> -1 Then
                    ''1行目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 14)

                    ''2行目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 25)
                Else
                    If hstrTargetStrings.Length > hintLineLength Then
                        ''1行目
                        strLine = hstrTargetStrings.Substring(0, hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 14)

                        ''2行目
                        strLine = hstrTargetStrings.Substring(hintLineLength)
                        hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 25)
                    Else
                        ''1行表示
                        hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 20)
                    End If

                End If
            End If


            ''WIRE MARK, DESTは1行MAX10文字までは通常のフォントサイズ
            ''11文字以上はフォントサイズを小さくし、13文字以上で2行とする
            'If hstrTargetStrings.Length > hintLineLength Then

            '    If hstrTargetStrings.Length > mCstCodeMaxLength12 Then
            '        ''1行目
            '        strLine = hstrTargetStrings.Substring(0, mCstCodeMaxLength12)
            '        hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 14)

            '        ''2行目
            '        strLine = hstrTargetStrings.Substring(mCstCodeMaxLength12)
            '        hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 25)

            '    Else
            '        ''1行表示
            '        hobjGraphics.DrawString(hstrTargetStrings, gFnt7, gFntColorBlack, hintX, hsngY + 19)

            '    End If

            'Else
            '    ''1行表示
            '    hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 20)
            'End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ３行用の文字描写関数
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 描く文字
    '           ： ARG3 - (I ) １行あたりの最大文字数
    '           ： ARG4 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG5 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalogLine3(ByVal hobjGraphics As Object, _
                                        ByVal hstrTargetStrings As String, _
                                        ByVal hintLineLength As Integer, _
                                        ByVal hintX As Integer, _
                                        ByVal hsngY As Single)

        Try

            Dim strLine1 As String, strLine2 As String, strLine3 As String
            Dim intCase As Integer = 1
            Dim intStringLength As Integer = hstrTargetStrings.Length

            ''文字長から何行表示になるかを判断（標準：1行表示）
            If (0 < intStringLength) And (intStringLength <= hintLineLength * 1) Then intCase = 1
            If (hintLineLength * 1 < intStringLength) And (intStringLength <= hintLineLength * 2) Then intCase = 2
            If (hintLineLength * 2 < intStringLength) And (intStringLength <= hintLineLength * 3) Then intCase = 3

            Select Case intCase

                Case 2

                    ''1行目
                    strLine1 = hstrTargetStrings.Substring(0, hintLineLength)
                    hobjGraphics.DrawString(strLine1, gFnt8, gFntColorBlack, hintX, hsngY + 10)

                    ''2行目
                    strLine1 = hstrTargetStrings.Substring(hintLineLength)
                    hobjGraphics.DrawString(strLine1, gFnt8, gFntColorBlack, hintX, hsngY + 27)

                Case 3

                    ''1行目
                    strLine1 = hstrTargetStrings.Substring(0, hintLineLength)
                    hobjGraphics.DrawString(strLine1, gFnt8, gFntColorBlack, hintX, hsngY + 2)

                    ''2行目
                    strLine1 = hstrTargetStrings.Substring(hintLineLength)
                    strLine2 = strLine1.Substring(0, hintLineLength)
                    hobjGraphics.DrawString(strLine2, gFnt8, gFntColorBlack, hintX, hsngY + 18)

                    ''3行目
                    strLine3 = strLine1.Substring(hintLineLength)
                    hobjGraphics.DrawString(strLine3, gFnt8, gFntColorBlack, hintX, hsngY + 34)

                Case Else

                    ''1行表示
                    hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 20)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ４行用の文字描写関数（２線式×２行）
    ' 引数      ： ARG1 - (I ) 行数判定（TRUE:1行目、FALSE:2行目）
    '           ： ARG2 - (I ) Graphicsオブジェクト
    '           ： ARG3 - (I ) 描く文字
    '           ： ARG4 - (I ) 1行あたりの最大文字数
    '           ： ARG5 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG6 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalogLine4(ByVal hintPrtLine As Integer, _
                                        ByVal hobjGraphics As Object, _
                                        ByVal hstrTargetStrings As String, _
                                        ByVal hintLineLength As Integer, _
                                        ByVal hintX As Integer, _
                                        ByVal hsngY As Single)

        Try

            Dim strLine As String = ""

            If hstrTargetStrings.Length > hintLineLength Then

                ''1ブロック目
                strLine = hstrTargetStrings.Substring(0, hintLineLength)
                If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 1)
                If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 25)

                ''2ブロック目
                strLine = hstrTargetStrings.Substring(hintLineLength)
                If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 13)
                If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 36)

            Else
                ''1ブロック表示
                If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 5)
                If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 31)
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ６行用の文字描写関数（３線式×２行）
    ' 引数      ： ARG1 - (I ) 行数判定（TRUE:1行目、FALSE:2行目）
    '           ： ARG2 - (I ) Graphicsオブジェクト
    '           ： ARG3 - (I ) 描く文字
    '           ： ARG4 - (I ) 1行あたりの最大文字数
    '           ： ARG5 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG6 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsAnalogLine6(ByVal hintPrtLine As Integer, _
                                        ByVal hobjGraphics As Object, _
                                        ByVal hstrTargetStrings As String, _
                                        ByVal hintLineLength As Integer, _
                                        ByVal hintX As Integer, _
                                        ByVal hsngY As Single)

        Try

            Dim strLine As String = ""
            Dim all_len As Integer
            Dim line_len As Integer

            Dim iFind1 As Integer = hstrTargetStrings.IndexOf("^"c)     '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞ検索

            If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                ''全角時の分割処理変更    2015.02.03
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞに変更
                If iFind1 <> -1 Then
                    ''1ブロック目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY)
                    If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 20)
                    If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 39)

                    ''2ブロック目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 10)
                    If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 30)
                    If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 49)
                Else
                    all_len = LenB(hstrTargetStrings)
                    If all_len > hintLineLength Then
                        ''1ブロック目
                        strLine = fStrCut(hstrTargetStrings, 0, hintLineLength)
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 20)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 39)

                        ''2ブロック目
                        line_len = LenB(strLine.Trim)
                        strLine = fStrCut(hstrTargetStrings, line_len, all_len - line_len)
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 10)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 30)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, hintX, hsngY + 49)
                    Else
                        ''1ブロック表示
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8j, gFntColorBlack, hintX, hsngY + 4)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8j, gFntColorBlack, hintX, hsngY + 24)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8j, gFntColorBlack, hintX, hsngY + 45)
                    End If
                End If
            Else
                '' Ver1.9.2 2016.01.06  改行ｺｰﾄﾞに変更
                If iFind1 <> -1 Then
                    ''1ブロック目
                    strLine = hstrTargetStrings.Substring(0, iFind1)
                    If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY)
                    If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 20)
                    If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 39)

                    ''2ブロック目
                    strLine = hstrTargetStrings.Substring(iFind1 + 1)
                    If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 10)
                    If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 30)
                    If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 49)
                Else
                    If hstrTargetStrings.Length > hintLineLength Then
                        ''1ブロック目
                        strLine = hstrTargetStrings.Substring(0, hintLineLength)
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 20)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 39)

                        ''2ブロック目
                        strLine = hstrTargetStrings.Substring(hintLineLength)
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 10)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 30)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt8, gFntColorBlack, hintX, hsngY + 49)
                    Else
                        ''1ブロック表示
                        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 4)
                        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 24)
                        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 45)
                    End If
                End If
            End If

            ''WIRE MARK, DESTは1行MAX10文字までは通常のフォントサイズ
            ''11文字以上はフォントサイズを小さくし、13文字以上で2行とする
            'If hstrTargetStrings.Length > hintLineLength Then

            '    If hstrTargetStrings.Length > mCstCodeMaxLength12 Then
            '        ''1ブロック目
            '        strLine = hstrTargetStrings.Substring(0, mCstCodeMaxLength12)
            '        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY)
            '        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 20)
            '        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 39)

            '        ''2ブロック目
            '        strLine = hstrTargetStrings.Substring(mCstCodeMaxLength12)
            '        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 10)
            '        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 30)
            '        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(strLine, gFnt7, gFntColorBlack, hintX, hsngY + 49)

            '    Else
            '        ''1ブロック表示
            '        If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt7, gFntColorBlack, hintX, hsngY + 4)
            '        If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt7, gFntColorBlack, hintX, hsngY + 24)
            '        If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt7, gFntColorBlack, hintX, hsngY + 45)
            '    End If

            'Else
            '    ''1ブロック表示
            '    If hintPrtLine = mCstCodePrintLine1 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 4)
            '    If hintPrtLine = mCstCodePrintLine2 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 24)
            '    If hintPrtLine = mCstCodePrintLine3 Then hobjGraphics.DrawString(hstrTargetStrings, gFnt8, gFntColorBlack, hintX, hsngY + 45)
            'End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region





    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ別チャンネル数、ページ数を取得する
    ' 引数      ： ARG1 - (I ) FU情報構造体
    '           ： ARG2 - (IO) 印刷用構造体   
    '           ： ARG3 - (IO) 最大ページ数
    '           ： ARG4 - (I ) 隠しCHフラグ（1：表示, 0：非表示）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetPrintPage(ByVal udtFuInfo() As gTypFuInfo, _
                              ByRef udtTerminalPageInfo() As gTypPrintTerminalInfo, _
                              ByRef intPageMax As Integer, _
                              ByVal intScFlg As Integer)

        Try

            Dim i As Integer
            Dim j As Integer
            Dim intPageIndex As Integer = 0
            Dim intRowCnt As Integer = 0
            Dim blnDigitalFlg As Boolean = False
            Dim intLoopCnt As Integer

            ReDim udtTerminalPageInfo(400)
            ReDim udtTerminalPageInfo(intPageIndex).udtRowInfoPage1(gCstCountFuNo - 1)

            ''◆１ページ目 [端子台リスト] -------------------------------------------------------------

            With udtTerminalPageInfo(intPageIndex)

                .intPageNo = 1
                .udtPageType = gEnmPrintTerminalPageType.tptFuList
                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori

                    .strFrameInfoLine1 = ""
                    .strFrameInfoLine2 = "フィールドユニット"
                Else    '英文
                    .strFrameInfoLine1 = ""
                    .strFrameInfoLine2 = "FIELD UNIT"
                    '' Ver1.8.3 2015.11.26  FUのNoは2桁に変更
                End If
                If g_bytFUSet = 0 Then      '' 出荷済みｵｰﾀﾞｰ用
                    .strDrawinfNoInfo = "FU001"     '' DrawingNo変更  2013.10.18
                Else
                    .strDrawinfNoInfo = "FU01"     '' DrawingNo変更  2013.10.18
                End If


                For i = 0 To UBound(udtFuInfo)
                    .udtRowInfoPage1(i).strNo = gGetFuName(i)
                    .udtRowInfoPage1(i).strFuName = gGetString(udtFuInfo(i).strFuName)
                    .udtRowInfoPage1(i).strNamePlate = gGetString(udtFuInfo(i).strNamePlate)
                    .udtRowInfoPage1(i).strFuType = gGetString(udtFuInfo(i).strFuType)
                    .udtRowInfoPage1(i).strCodeNo = IIf(i <= 15, "0" & Hex(i), Hex(i))
                    .udtRowInfoPage1(i).strRemarks = gGetString(udtFuInfo(i).strRemarks)
                Next

                'Ver2.0.0.2 基板指定印刷用変数
                .intFuNo = -1
                .intSlotNo = -1
            End With


            ''◆２ページ目[Local Unit Print(FU印字)]
            '印刷データそのものは、FU変数から生成するのでここでは不要
            For j = 0 To printLUpageMax - 1 Step 1
                intPageIndex += 1
                With udtTerminalPageInfo(intPageIndex)

                    .intPageNo = intPageIndex + 1
                    .udtPageType = gEnmPrintTerminalPageType.tptFU
                    .strFrameInfoLine1 = ""
                    .strFrameInfoLine2 = ""


                    If g_bytFUSet = 0 Then      '出荷済みｵｰﾀﾞｰ用
                        .strDrawinfNoInfo = "FU001"     '' DrawingNo変更  2013.10.18
                    Else
                        .strDrawinfNoInfo = "FU01"     '' DrawingNo変更  2013.10.18
                    End If

                    'Ver2.0.0.2 基板指定印刷用変数
                    .intFuNo = -2
                    .intSlotNo = -2
                End With
            Next j



            ''◆３ページ目以降 ------------------------------------------------------------------------

            intPageIndex += 1

            ''FU機器単位（縦ループ）
            For i = 0 To UBound(udtFuInfo)

                ''スロット単位（横ループ）
                For j = 0 To UBound(udtFuInfo(i).udtFuPort)

                    ''スロット割付けがない場合は次へ
                    If udtFuInfo(i).udtFuPort(j).intPortType <> 0 Then

                        If udtFuInfo(i).udtFuPort(j).intPortType = 1 Or _
                           udtFuInfo(i).udtFuPort(j).intPortType = 2 Then
                            blnDigitalFlg = True
                            intLoopCnt = 1
                        Else
                            blnDigitalFlg = False
                            intLoopCnt = 0
                        End If

                        For ii = 0 To intLoopCnt

                            'DI,DO 2ページ目にCHが無ければ印字しない 2013.07.24 K.Fujimoto
                            If ii = 1 Then  '' Di,DO 2ページ目
                                '' Loopcnt修正    2013.10.22
                                'Ver2.0.2.1 DOの場合ﾁｪｯｸは分岐
                                If udtFuInfo(i).udtFuPort(j).intPortType = 1 Then
                                    'DO の場合、3と4でﾁｪｯｸ
                                    If (ChkTerminalCount(udtFuInfo(i), 3, i, j) = False) And _
                                        (ChkTerminalCount(udtFuInfo(i), 4, i, j) = False) Then
                                        Exit For
                                    End If
                                Else
                                    'DI
                                    If ChkTerminalCount(udtFuInfo(i), intLoopCnt + 1, i, j) = False Then
                                        Exit For
                                    End If
                                End If
                                
                            End If


                            Call mGetPrintPageInfo(udtFuInfo(i), _
                                                   udtTerminalPageInfo(intPageIndex), _
                                                   i, _
                                                   j, _
                                                   intPageIndex, _
                                                   intScFlg, _
                                                   ii)
                            intPageIndex += 1

                        Next

                        'ワイヤーマークは強制入力
                        'Else


                    End If

                Next j

            Next i

            ''最大ページ数の設定
            intPageMax = intPageIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ別チャンネル数、ページ数を取得する
    ' 引数      ： ARG1 - (I ) FU情報構造体
    '           ： ARG2 - (IO) 印刷用構造体   
    '           ： ARG3 - (I ) Fu番号
    '           ： ARG4 - (I ) Port番号
    '           ： ARG5 - (I ) 印刷用ページIndex
    '           ： ARG6 - (I ) 隠しCHフラグ     （1：表示　　0：非表示）
    '           ： ARG7 - (I ) ループカウント
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetPrintPageInfo(ByVal hudtFuInfo As gTypFuInfo, _
                                  ByRef hudtTerminalPageInfo As gTypPrintTerminalInfo, _
                                  ByVal hintFuNo As Integer, _
                                  ByVal hintPortNo As Integer, _
                                  ByVal hintPageIndex As Integer, _
                                  ByVal hintScFlg As Integer, _
                                  ByVal hintLoopCnt As Integer)

        Try

            Dim blnScFlg As Boolean = False
            Dim blnScFlg2 As Boolean = False
            Dim blnScFlg3 As Boolean = False

            ReDim hudtTerminalPageInfo.udtRowInfo(gCstPrtTerminalMaxRow - 1)
            Dim mCstIntStartRowNo As Integer
            Dim strTer As String
            Dim blnDigitalFlg As Boolean = False    '' Ver1.9.3 2016.01.13 ﾃﾞｼﾞﾀﾙﾌﾗｸﾞ追加

            '' Ver1.9.3 2016.01.13 ﾃﾞｼﾞﾀﾙﾌﾗｸﾞ追加
            If hudtFuInfo.udtFuPort(hintPortNo).intPortType = gCstCodeFuSlotTypeDI Then
                blnDigitalFlg = True
            End If

            With hudtTerminalPageInfo

                'Ver2.0.0.2 基板指定印刷用変数
                .intFuNo = hintFuNo
                .intSlotNo = hintPortNo

                .intPageNo = hintPageIndex + 1                                              ''ページ番号
                .udtPageType = gConvPortType(hudtFuInfo.udtFuPort(hintPortNo).intPortType)  ''フォーマットタイプ
                .intPortType = hudtFuInfo.udtFuPort(hintPortNo).intPortType                 ''ポートタイプ（0x01:DO ～ 0x08:AI_K）
                .intTerinf = hudtFuInfo.udtFuPort(hintPortNo).intTerinf                     ''端子台設定
                .intStartRowIndex = IIf(hintLoopCnt = 0, 0, 32)                             ''スタート行Index
                .strBoxFuInfo = gGetFuName(hintFuNo)                                        ''BOX：FU機器名

                ''[ページヘッダー] ポート名
                If hudtTerminalPageInfo.udtPageType = gEnmPrintTerminalPageType.tptDigital Then

                    ''[Digital]
                    If hudtTerminalPageInfo.intStartRowIndex = 0 Then
                        .strBoxSlotInfo = hudtFuInfo.strFuName & " － " & "TB" & (hintPortNo + 1) & "A"
                        strTer = "A"
                    Else
                        .strBoxSlotInfo = hudtFuInfo.strFuName & " － " & "TB" & (hintPortNo + 1) & "B"
                        strTer = "B"
                    End If

                Else
                    ''[Analog]
                    .strBoxSlotInfo = hudtFuInfo.strFuName & " － " & "TB" & (hintPortNo + 1)
                    strTer = ""
                End If

                ''[ページヘッダー] Remarks
                .strBoxRemarks = hudtFuInfo.strRemarks

                ''[ページフッター]
                Call mGetFooterType(hudtFuInfo, hudtTerminalPageInfo, hintPortNo)

                ''[ページフッター] DrawingNo   2013.10.18
                '' Ver1.8.3 2015.11.26  FUのNoは2桁に変更
                '' Ver1.8.4 2015.11.27  3桁も表示できるように変更
                If g_bytFUSet = 0 Then      '' 出荷済みｵｰﾀﾞｰ用
                    If hintFuNo = 0 Then
                        .strDrawinfNoInfo = "FU" & (2 + hintPortNo).ToString("000") & strTer
                    Else
                        .strDrawinfNoInfo = "FU" & (7 + (8 * (hintFuNo - 1)) + hintPortNo).ToString("000") & strTer
                    End If
                Else
                    If hintFuNo = 0 Then
                        If (2 + hintPortNo) >= 100 Then
                            .strDrawinfNoInfo = "FU" & (2 + hintPortNo).ToString("000") & strTer
                        Else
                            .strDrawinfNoInfo = "FU" & (2 + hintPortNo).ToString("00") & strTer
                        End If

                    Else
                        If (7 + (8 * (hintFuNo - 1)) + hintPortNo) >= 100 Then
                            .strDrawinfNoInfo = "FU" & (7 + (8 * (hintFuNo - 1)) + hintPortNo).ToString("000") & strTer
                        Else
                            .strDrawinfNoInfo = "FU" & (7 + (8 * (hintFuNo - 1)) + hintPortNo).ToString("00") & strTer
                        End If

                    End If
                End If


                ''先頭行Index設定
                If hudtTerminalPageInfo.intStartRowIndex = 0 Then
                    mCstIntStartRowNo = 0
                Else
                    mCstIntStartRowNo = 32
                End If


                For i As Integer = 0 To gCstPrtTerminalMaxRow - 1

                    ''SecretCH表示/非表示フラグの取得
                    blnScFlg = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).blnChComSc

                    blnScFlg2 = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).blnChComSc2
                    blnScFlg3 = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).blnChComSc3

                    'T.Ueki 先頭行処理追加
                    'blnScFlg = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i).blnChComSc

                    ' 2015.10.27 Ver1.7.5   ' FU 0　の場合は隠しCHも全て表示
                    If hintFuNo = 0 Then
                        .udtRowInfo(i) = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo)

                        If blnDigitalFlg = True Then        '' Ver1.9.3 2016.01.13 ﾃﾞｼﾞﾀﾙﾌﾗｸﾞ追加
                            Call SetPreviewDigData(hintFuNo, hintPortNo + 1, i + mCstIntStartRowNo + 1, True, hudtTerminalPageInfo.udtRowInfo(i))
                        End If
                    Else

                        If hintScFlg <> 1 And blnScFlg = True Then
                            ''[OPTION] >> [SCチャンネル表示フラグ] が有効になっていない時は処理をスキップ
                            'Ver2.0.7.M ワイヤーマークは表示させたいため、ワイヤーマークのみｺﾋﾟｰ
                            .udtRowInfo(i).strWireMark = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).strWireMark
                            .udtRowInfo(i).strWireMarkClass = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).strWireMarkClass
                            .udtRowInfo(i).strCoreNoIn = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).strCoreNoIn
                            .udtRowInfo(i).strCoreNoCom = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).strCoreNoCom
                            .udtRowInfo(i).strDist = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo).strDist
                        Else
                            .udtRowInfo(i) = hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(i + mCstIntStartRowNo)


                            'Ver2.0.2.6 CH2,CH3のSC対応
                            If hintScFlg <> 1 And blnScFlg2 = True Then
                                .udtRowInfo(i).strChNo2 = ""
                            End If
                            If hintScFlg <> 1 And blnScFlg3 = True Then
                                .udtRowInfo(i).strChNo3 = ""
                            End If

                        End If

                        If blnDigitalFlg = True Then        '' Ver1.9.3 2016.01.13 ﾃﾞｼﾞﾀﾙﾌﾗｸﾞ追加
                            Call SetPreviewDigData(hintFuNo, hintPortNo + 1, i + mCstIntStartRowNo + 1, hintScFlg, hudtTerminalPageInfo.udtRowInfo(i))
                        End If

                    End If
                    ''Signal
                    Call mGetSignal(hudtFuInfo, hudtTerminalPageInfo, hintPortNo, i + mCstIntStartRowNo, i)
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 仮設定データ表示チェック（ステータス）
    ' 引数      ： ARG1 - (I ) ステータス名称
    '           ： ARG2 - (I ) 印刷用構造体
    '           ： ARG3 - (I ) 仮設定データ表示フラグ
    ' 戻値      ： 設定データが仮設定データの場合は表示しない
    '----------------------------------------------------------------------------
    Private Sub mCheckDmyStatus(ByRef hstrStatus As String, _
                                ByVal hudtSet As gTypFuInfoPin, _
                                ByVal hblnDmyFlg As Boolean)

        Try

            ''仮設定データであるか判断
            If hudtSet.blnDmyStatusIn Or _
               hudtSet.blnDmyStatusOut Then

                ''仮設定データを表示しない場合
                If Not hblnDmyFlg Then
                    hstrStatus = ""
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 仮設定データ表示チェック（上下限レンジ）
    ' 引数      ： ARG1 - (I ) 表示レンジ
    '           ： ARG2 - (I ) 上下限ラベル
    '           ： ARG3 - (I ) 印刷用構造体
    '           ： ARG4 - (I ) 仮設定データ表示フラグ
    ' 戻値      ： 設定データが仮設定データの場合は表示しない
    '----------------------------------------------------------------------------
    Private Sub mCheckDmyRange(ByRef hstrRangeL As String, _
                               ByRef hstrRangeH As String, _
                               ByRef hstrRangeLabel As String, _
                               ByVal hudtSet As gTypFuInfoPin, _
                               ByVal hblnDmyFlg As Boolean)

        Try

            ''仮設定データであるか判断
            If hudtSet.blnDmyScaleRange Then

                ''仮設定データを表示しない場合
                If Not hblnDmyFlg Then
                    hstrRangeL = ""
                    hstrRangeH = ""
                    hstrRangeLabel = hstrRangeLabel
                Else
                    hstrRangeLabel = "#" & hstrRangeLabel
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Signal文字描写関数
    ' 引数      ： ARG1 - (I ) FU情報構造体
    '           ： ARG2 - (IO) 印刷用構造体
    '           ： ARG3 - (I ) Port番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetSignal(ByVal hudtFuInfo As gTypFuInfo, _
                           ByRef hudtTerminalPageInfo As gTypPrintTerminalInfo, _
                           ByVal hintPortNo As Integer, _
                           ByVal intPinNo As Integer, _
                           ByVal intRowCnt As Integer)

        Try

            'Dim strLine As String = ""
            With hudtTerminalPageInfo

                If hudtFuInfo.udtFuPort(hintPortNo).intPortType = gCstCodeFuSlotTypeDO Then
                    'Ver2.0.1.6 TMRもDOと同じ書式で印字
                    'If hudtFuInfo.udtFuPort(hintPortNo).intTerinf > 1 Then      'TMRY
                    If hudtFuInfo.udtFuPort(hintPortNo).intTerinf < -1 Then      'IFthenへは絶対にいかないﾛｼﾞｯｸ
                        .udtRowInfo(intRowCnt).strSignal = "[TMR ]"
                    Else
                        If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).bytOutput = gCstCodeFuOutputTypeCh____ Then         'CH
                            .udtRowInfo(intRowCnt).strSignal = "[ CH ]"
                        ElseIf hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).bytOutput = gCstCodeFuOutputTypeRun__LT Then    'RUN
                            .udtRowInfo(intRowCnt).strSignal = "[RUN ]"
                        ElseIf hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).bytOutput = gCstCodeFuOutputTypeInvalid Then    'INVALID
                            .udtRowInfo(intRowCnt).strSignal = "[ DO ]"     '' CHOUTテーブルでInvalid設定時はCHでの出力設定
                        Else                                                                                                        'ALM
                            .udtRowInfo(intRowCnt).strSignal = "[ALM ]"

                        End If

                        'MOTOR、ON/OFF指定時はマスクステータスを表示
                        'If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).bytOutStatus = 1 Or _   'MOTOR
                        '   hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).bytOutStatus = 2 Then   'ON/OFF

                        'End If
                    End If

                Else

                    '' Ver1.9.3 2016.01.15 処理変更
                    '' Ver1.9.5 2016.02.02 ｱﾅﾛｸﾞｽﾃｰﾀｽを消してしまっていたので修正

                    ''Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intChType

                    'Case gCstCodeChTypeAnalog       ''☆☆☆　アナログ
                    If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intChType = gCstCodeChTypeAnalog Then    ''☆☆☆　アナログ
                        Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType
                            Case gCstCodeChDataTypeAnalogK
                                .udtRowInfo(intRowCnt).strSignal = "[  K ]"
                            Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog2Jpt, gCstCodeChDataTypeAnalog3Pt, gCstCodeChDataTypeAnalog3Jpt ''2線式Pt, 2線式JPt, 3線式Pt, 3線式JPt
                                .udtRowInfo(intRowCnt).strSignal = "[ TR ]"
                            Case gCstCodeChDataTypeAnalog1_5v
                                .udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                            Case gCstCodeChDataTypeAnalog4_20mA, gCstCodeChDataTypeAnalogPT4_20mA
                                If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intSignal = gCstCodeChAnalogSignalPT Then
                                    .udtRowInfo(intRowCnt).strSignal = "[ PT ]"
                                Else
                                    .udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                                End If
                        End Select
                    ElseIf hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intChType = gCstCodeChTypePID Then
                        'Ver2.0.7.H PID対応
                        'PIDは全て[PID ]とする
                        Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType
                            Case gCstCodeChDataTypePID_5_AI_K
                                '.udtRowInfo(intRowCnt).strSignal = "[  K ]"
                                .udtRowInfo(intRowCnt).strSignal = "[PID ]"
                            Case gCstCodeChDataTypePID_3_Pt100_2, gCstCodeChDataTypePID_4_Pt100_3   '2線式, 3線式Pt
                                '.udtRowInfo(intRowCnt).strSignal = "[ TR ]"
                                .udtRowInfo(intRowCnt).strSignal = "[PID ]"
                            Case gCstCodeChDataTypePID_1_AI1_5
                                '.udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                                .udtRowInfo(intRowCnt).strSignal = "[PID ]"
                            Case gCstCodeChDataTypePID_2_AI4_20
                                If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intSignal = gCstCodeChAnalogSignalPT Then
                                    '.udtRowInfo(intRowCnt).strSignal = "[ PT ]"
                                    .udtRowInfo(intRowCnt).strSignal = "[PID ]"
                                Else
                                    '.udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                                    .udtRowInfo(intRowCnt).strSignal = "[PID ]"
                                End If
                        End Select

                    Else
                        .udtRowInfo(intRowCnt).strSignal = mGetSigname(hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intChType, _
                                hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType, _
                                hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intSignal, _
                                .udtRowInfo(intRowCnt).strStatus)
                        ''    Select Case .udtRowInfo(intRowCnt).intChType
                        ''        Case gCstCodeChTypeDigital      ''☆☆☆　デジタル

                        ''            ''If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType = gCstCodeChDataTypeDigitalNC Then
                        ''            If .udtRowInfo(intRowCnt).intDataType = gCstCodeChDataTypeDigitalNC Then
                        ''                .udtRowInfo(intRowCnt).strSignal = "[ NC ]"

                        ''                ''ElseIf hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType = gCstCodeChDataTypeDigitalNO Or _
                        ''                ''       hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType = gCstCodeChDataTypeDigitalExt Then
                        ''            ElseIf .udtRowInfo(intRowCnt).intDataType = gCstCodeChDataTypeDigitalNO Or _
                        ''               .udtRowInfo(intRowCnt).intDataType = gCstCodeChDataTypeDigitalExt Then
                        ''                .udtRowInfo(intRowCnt).strSignal = "[ NO ]"

                        ''            Else

                        ''                .udtRowInfo(intRowCnt).strSignal = "[    ]"

                        ''            End If

                        ''        Case gCstCodeChTypeMotor        ''☆☆☆  モーター

                        ''            'モーターの信号をセンター寄せに変更  2013.07.24 K.Fujimoto
                        ''            ''Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).strStatus
                        ''            Select Case .udtRowInfo(intRowCnt).strStatus
                        ''                Case "RUN" : .udtRowInfo(intRowCnt).strSignal = "[ 88 ]"
                        ''                Case "STOP"
                        ''                    ''If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                        ''                    If .udtRowInfo(intRowCnt).intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[  5 ]"
                        ''                    Else
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[ 51 ]"
                        ''                    End If
                        ''                Case "NORMAL"   '' M2用　2014.11.17
                        ''                    ''If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                        ''                    If .udtRowInfo(intRowCnt).intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[  5 ]"
                        ''                    Else
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[ 51 ]"
                        ''                    End If
                        ''                Case "ST/BY" : .udtRowInfo(intRowCnt).strSignal = "[ 48 ]"
                        ''                Case "RUN-L" : .udtRowInfo(intRowCnt).strSignal = "[ 88L]"
                        ''                Case "RUN-H" : .udtRowInfo(intRowCnt).strSignal = "[ 88H]"
                        ''                Case "SUP" : .udtRowInfo(intRowCnt).strSignal = "[ 88S]"
                        ''                Case "EXH" : .udtRowInfo(intRowCnt).strSignal = "[ 88E]"
                        ''                Case "SUP-L" : .udtRowInfo(intRowCnt).strSignal = "[88SL]"
                        ''                Case "SUP-H" : .udtRowInfo(intRowCnt).strSignal = "[88SH]"
                        ''                Case "EXH-L" : .udtRowInfo(intRowCnt).strSignal = "[88EL]"
                        ''                Case "EXH-H" : .udtRowInfo(intRowCnt).strSignal = "[88EH]"
                        ''                Case "FWD" : .udtRowInfo(intRowCnt).strSignal = "[ 88F]"
                        ''                Case "REV" : .udtRowInfo(intRowCnt).strSignal = "[ 88R]"
                        ''                Case "FWD-L" : .udtRowInfo(intRowCnt).strSignal = "[88FL]"
                        ''                Case "FWD-H" : .udtRowInfo(intRowCnt).strSignal = "[88FH]"
                        ''                Case "REV-L" : .udtRowInfo(intRowCnt).strSignal = "[88RL]"
                        ''                Case "REV-H" : .udtRowInfo(intRowCnt).strSignal = "[88RH]"
                        ''                Case "AUTO" : .udtRowInfo(intRowCnt).strSignal = "[ AT ]"
                        ''                Case Else : .udtRowInfo(intRowCnt).strSignal = "[    ]"
                        ''            End Select

                        ''        Case gCstCodeChTypeValve        ''☆☆☆　バルブ

                        ''            ''Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intRowCnt).intDataType
                        ''            Select Case .udtRowInfo(intRowCnt).intDataType
                        ''                Case gCstCodeChDataTypeValveDI_DO
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ DC ]"
                        ''                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_AO1
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                        ''                Case gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                        ''                    If hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intSignal = gCstCodeChAnalogSignalPT Then
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[ PT ]"
                        ''                    Else
                        ''                        .udtRowInfo(intRowCnt).strSignal = "[ AI ]"
                        ''                    End If
                        ''            End Select

                        ''        Case gCstCodeChTypeComposite    ''☆☆☆  デジタルコンポジット
                        ''            .udtRowInfo(intRowCnt).strSignal = "[ DC ]"

                        ''        Case gCstCodeChTypePulse        ''☆☆☆  パルス積算
                        ''            ''Select Case hudtFuInfo.udtFuPort(hintPortNo).udtFuPin(intPinNo).intDataType
                        ''            Select Case .udtRowInfo(intRowCnt).intDataType
                        ''                Case gCstCodeChDataTypePulseTotal1_1
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ PU ]"
                        ''                Case gCstCodeChDataTypePulseTotal1_10
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ P1 ]"
                        ''                Case gCstCodeChDataTypePulseTotal1_100
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ P2 ]"
                        ''                Case gCstCodeChDataTypePulseDay1_1
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[PUD ]"
                        ''                Case gCstCodeChDataTypePulseDay1_10
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[P1D ]"
                        ''                Case gCstCodeChDataTypePulseDay1_100
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[P2D ]"
                        ''                Case gCstCodeChDataTypePulseRevoTotalHour
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ RH ]"
                        ''                Case gCstCodeChDataTypePulseRevoTotalMin
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[ R2 ]"
                        ''                Case gCstCodeChDataTypePulseRevoDayHour
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[RHD ]"
                        ''                Case gCstCodeChDataTypePulseRevoDayMin
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[R2D ]"
                        ''                Case gCstCodeChDataTypePulseRevoLapHour
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[RHL ]"
                        ''                Case gCstCodeChDataTypePulseRevoLapMin
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[R2L ]"
                        ''                Case Else
                        ''                    .udtRowInfo(intRowCnt).strSignal = "[    ]"
                        ''            End Select
                        ''    End Select

                    End If

                    ''End Select

                End If

            End With

            ''モーターチャンネルのステータス情報を獲得する
            'Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Function mGetSigname(intCHType As Integer, intDataType As Integer, _
                                 intSignal As Integer, strStatus As String) As String

        Dim strName As String = ""

        Select Case intCHType
            Case gCstCodeChTypeDigital      ''☆☆☆　デジタル

                If intDataType = gCstCodeChDataTypeDigitalNC Then       '' NC
                    strName = "[ NC ]"
                ElseIf intDataType = gCstCodeChDataTypeDigitalNO Or _
                   intDataType = gCstCodeChDataTypeDigitalExt Then      '' NO or 延長警報
                    strName = "[ NO ]"
                Else
                    strName = "[    ]"
                End If

            Case gCstCodeChTypeMotor        ''☆☆☆  モーター
                'Ver2.0.7.Q 保安庁 モーターステータス日本語対応
                Select Case strStatus
                    Case "RUN", "運転" : strName = "[ 88 ]"
                    Case "STOP", "停止"
                        If intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                            strName = "[  5 ]"
                        Else
                            strName = "[ 51 ]"
                        End If
                    Case "NORMAL", "正常"   '' M2用　ver.2.0.8.A 2018.10.12 通常→正常
                        If intDataType < gCstCodeChDataTypeMotorAbnorRun Then
                            strName = "[  5 ]"
                        Else
                            strName = "[ 51 ]"
                        End If
                    Case "ST/BY", "ｽﾀﾝﾊﾞｲ" : strName = "[ 48 ]"
                    Case "RUN-L", "低速運転" : strName = "[ 88L]"
                    Case "RUN-H", "高速運転" : strName = "[ 88H]"
                    Case "SUP", "給気" : strName = "[ 88S]"
                    Case "EXH", "排気" : strName = "[ 88E]"
                    Case "SUP-L", "低速給気" : strName = "[88SL]"
                    Case "SUP-H", "高速給気" : strName = "[88SH]"
                    Case "EXH-L", "低速排気" : strName = "[88EL]"
                    Case "EXH-H", "高速排気" : strName = "[88EH]"
                    Case "FWD", "正転" : strName = "[ 88F]"
                    Case "REV", "逆転" : strName = "[ 88R]"
                    Case "FWD-L", "低速正転" : strName = "[88FL]"
                    Case "FWD-H", "高速正転" : strName = "[88FH]"
                    Case "REV-L", "低速逆転" : strName = "[88RL]"
                    Case "REV-H", "高速逆転" : strName = "[88RH]"
                    Case "AUTO", "自動" : strName = "[ AT ]"
                    Case "ECO-RUN" : strName = "[ 88E]"     '' Ver1.12.0.1 2017.01.16 RUN-K追加漏れ
                    Case "BYPS-RUN" : strName = "[ 88B]"    '' Ver1.12.0.1 2017.01.16 RUN-K追加漏れ
                    Case Else : strName = "[    ]"
                End Select

            Case gCstCodeChTypeValve        ''☆☆☆　バルブ

                Select Case intDataType
                    Case gCstCodeChDataTypeValveDI_DO
                        strName = "[ DC ]"
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_AO1
                        strName = "[ AI ]"
                    Case gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                        If intSignal = gCstCodeChAnalogSignalPT Then
                            strName = "[ PT ]"
                        Else
                            strName = "[ AI ]"
                        End If
                End Select

            Case gCstCodeChTypeComposite    ''☆☆☆  デジタルコンポジット
                strName = "[ DC ]"

            Case gCstCodeChTypePulse        ''☆☆☆  パルス積算
                Select Case intDataType
                    Case gCstCodeChDataTypePulseTotal1_1
                        strName = "[ PU ]"
                    Case gCstCodeChDataTypePulseTotal1_10
                        strName = "[ P1 ]"
                    Case gCstCodeChDataTypePulseTotal1_100
                        strName = "[ P2 ]"
                    Case gCstCodeChDataTypePulseDay1_1
                        strName = "[PUD ]"
                    Case gCstCodeChDataTypePulseDay1_10
                        strName = "[P1D ]"
                    Case gCstCodeChDataTypePulseDay1_100
                        strName = "[P2D ]"
                    Case gCstCodeChDataTypePulseRevoTotalHour
                        strName = "[ RH ]"
                    Case gCstCodeChDataTypePulseRevoTotalMin
                        strName = "[ R2 ]"
                    Case gCstCodeChDataTypePulseRevoDayHour
                        strName = "[RHD ]"
                    Case gCstCodeChDataTypePulseRevoDayMin
                        strName = "[R2D ]"
                    Case gCstCodeChDataTypePulseRevoLapHour
                        strName = "[RHL ]"
                    Case gCstCodeChDataTypePulseRevoLapMin
                        strName = "[R2L ]"
                    Case Else
                        strName = "[    ]"
                End Select
        End Select

        Return strName

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： フッターの基板種類取得
    ' 引数      ： ARG1 - (I ) FU情報構造体
    '           ： ARG2 - (IO) 印刷用構造体
    '           ： ARG3 - (I ) Port番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetFooterType(ByVal hudtFuInfo As gTypFuInfo, _
                               ByRef hudtTerminalPageInfo As gTypPrintTerminalInfo, _
                               ByVal hintPortNo As Integer)

        Try

            With hudtTerminalPageInfo
                '' 2013.10.21 型名変更

                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori

                    Select Case hudtFuInfo.udtFuPort(hintPortNo).intPortType
                        Case gCstCodeFuSlotTypeDO 'DO(TMDO)/DO(TMRY)
                            If hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 1 Then          'TMDO
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMDO)"                 '１行目：基板、端子台型式
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 2 Then      'TMRY:101 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY)"                 '１行目：基板、端子台型式 
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 3 Then      'TMRY:111 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY-1)"               '１行目：基板、端子台型式
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 4 Then      'TMRY:121 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY-2)"               '１行目：基板、端子台型式
                            Else                                                            'その他                        
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMDO)"                 '１行目：基板、端子台型式
                            End If
                            .strFrameInfoLine2 = "ON-OFF 出力"            '２行目：スロット種別
                            
                        Case gCstCodeFuSlotTypeDI 'DI
                            .strFrameInfoLine1 = "FCU-M002A (FCU-TMDI)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "ON-OFF 入力"             '２行目：スロット種別
                            
                        Case gCstCodeFuSlotTypeAO 'AO
                            .strFrameInfoLine1 = "FCU-M030A (FCU-TMAO-1)"   '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC4-20mA 出力"          '２行目：スロット種別
                            
                        Case gCstCodeFuSlotTypeAI_2 'AI(2LINE)
                            .strFrameInfoLine1 = "FCU-M100A (FCU-TM15)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "PT100Ω"                  '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_3 'AI(3LINE)
                            .strFrameInfoLine1 = "FCU-M110A (FCU-TMTEMP)"   '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "PT100Ω (3線式)"          '２行目：スロット種別
                            
                        Case gCstCodeFuSlotTypeAI_1_5 'AI(1-5V)
                            .strFrameInfoLine1 = "FCU-M500A (FCU-TM15)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC1-5V"                   '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_4_20 'AI(4-20mA)
                            .strFrameInfoLine1 = "FCU-M400A (FCU-TM42)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC4-20mA"                 '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_K 'AI(K)
                            .strFrameInfoLine1 = "FCU-M200A (FCU-TMK)"      '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "K INPUT"                  '２行目：スロット種別
                        Case Else
                    End Select

                Else

                    Select Case hudtFuInfo.udtFuPort(hintPortNo).intPortType

                        Case gCstCodeFuSlotTypeDO 'DO(TMDO)/DO(TMRY)
                            If hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 1 Then          'TMDO
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMDO)"                 '１行目：基板、端子台型式
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 2 Then      'TMRY:101 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY)"                 '１行目：基板、端子台型式 
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 3 Then      'TMRY:111 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY-1)"               '１行目：基板、端子台型式
                            ElseIf hudtFuInfo.udtFuPort(hintPortNo).intTerinf = 4 Then      'TMRY:121 (DO:1)
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMRY-2)"               '１行目：基板、端子台型式
                            Else                                                            'その他                        
                                .strFrameInfoLine1 = "FCU-M003A (FCU-TMDO)"                 '１行目：基板、端子台型式
                            End If
                            .strFrameInfoLine2 = "ON-OFF OUTPUT"            '２行目：スロット種別

                        Case gCstCodeFuSlotTypeDI 'DI
                            .strFrameInfoLine1 = "FCU-M002A (FCU-TMDI)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "ON-OFF INPUT"             '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAO 'AO
                            .strFrameInfoLine1 = "FCU-M030A (FCU-TMAO-1)"   '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC4-20mA OUTPUT"          '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_2 'AI(2LINE)
                            .strFrameInfoLine1 = "FCU-M100A (FCU-TM15)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "PT100Ω"                  '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_3 'AI(3LINE)
                            .strFrameInfoLine1 = "FCU-M110A (FCU-TMTEMP)"   '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "PT100Ω (3Wire type)"     '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_1_5 'AI(1-5V)
                            .strFrameInfoLine1 = "FCU-M500A (FCU-TM15)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC1-5V"                   '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_4_20 'AI(4-20mA)
                            .strFrameInfoLine1 = "FCU-M400A (FCU-TM42)"     '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "DC4-20mA"                 '２行目：スロット種別

                        Case gCstCodeFuSlotTypeAI_K 'AI(K)
                            .strFrameInfoLine1 = "FCU-M200A (FCU-TMK)"      '１行目：基板、端子台型式
                            .strFrameInfoLine2 = "K INPUT"                  '２行目：スロット種別
                        Case Else
                    End Select
                End If

                'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応
                If g_bytTerVer = 1 Then
                    .strFrameInfoLine1 = .strFrameInfoLine1.Replace("A ", " ")
                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： D/I、D/Oの点数確認
    ' 引数      ： ARG1 - (I ) 基板種類
    '           ： ARG2 - (I ) 1 or 2
    '           ： ARG3 - (I ) FU番号
    '           ： ARG4 - (I ) SLOT番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Function ChkTerminalCount(ByVal hudtFuInfo As gTypFuInfo, ByVal page As Integer, ByVal funo As Integer, ByVal Slotno As Integer) As Boolean

        Dim i As Integer
        Dim j As Integer
        Dim exist_flg As Boolean = False
        Dim mtype As Integer = 0

        mtype = hudtFuInfo.udtFuPort(Slotno).intPortType


        Select Case mtype
            Case 1 ''DO
                'Ver2.0.1.9 記述が違うため変更
                'If page = 2 Then
                '    For i = 32 To 63
                '        '' 参照変数変更   2013.11.02
                '        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                '        ' 2015.09.16 M.Kaihara
                '        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                '        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                '        ' If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                '        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                '            exist_flg = True
                '            Exit For
                '        End If
                '    Next
                'End If
                If page = 2 Then
                    For i = 16 To 31
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If

                        'Ver2.0.3.6
                        'ワイヤーマークがあれば印刷可能とする
                        With hudtFuInfo.udtFuPort(Slotno).udtFuPin(i)
                            For j = LBound(.strWireMark) To UBound(.strWireMark) Step 1
                                If NZfS(.strWireMark(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strWireMarkClass) To UBound(.strWireMarkClass) Step 1
                                If NZfS(.strWireMarkClass(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoIn) To UBound(.strCoreNoIn) Step 1
                                If NZfS(.strCoreNoIn(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoCom) To UBound(.strCoreNoCom) Step 1
                                If NZfS(.strCoreNoCom(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strDist) To UBound(.strDist) Step 1
                                If NZfS(.strDist(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                        End With

                    Next
                ElseIf page = 3 Then
                    For i = 32 To 47
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If

                        'Ver2.0.3.6
                        'ワイヤーマークがあれば印刷可能とする
                        With hudtFuInfo.udtFuPort(Slotno).udtFuPin(i)
                            For j = LBound(.strWireMark) To UBound(.strWireMark) Step 1
                                If NZfS(.strWireMark(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strWireMarkClass) To UBound(.strWireMarkClass) Step 1
                                If NZfS(.strWireMarkClass(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoIn) To UBound(.strCoreNoIn) Step 1
                                If NZfS(.strCoreNoIn(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoCom) To UBound(.strCoreNoCom) Step 1
                                If NZfS(.strCoreNoCom(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strDist) To UBound(.strDist) Step 1
                                If NZfS(.strDist(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                        End With

                    Next
                ElseIf page = 4 Then
                    For i = 48 To 63
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If

                        'Ver2.0.3.6
                        'ワイヤーマークがあれば印刷可能とする
                        With hudtFuInfo.udtFuPort(Slotno).udtFuPin(i)
                            For j = LBound(.strWireMark) To UBound(.strWireMark) Step 1
                                If NZfS(.strWireMark(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strWireMarkClass) To UBound(.strWireMarkClass) Step 1
                                If NZfS(.strWireMarkClass(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoIn) To UBound(.strCoreNoIn) Step 1
                                If NZfS(.strCoreNoIn(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strCoreNoCom) To UBound(.strCoreNoCom) Step 1
                                If NZfS(.strCoreNoCom(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                            For j = LBound(.strDist) To UBound(.strDist) Step 1
                                If NZfS(.strDist(j)) <> "" Then
                                    exist_flg = True
                                    Exit For
                                End If
                            Next j
                        End With

                    Next
                End If

            Case 2 ''DI
                For i = 32 To 63
                    '' 参照変数変更   2013.11.02
                    'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                    ' 2015.09.16 M.Kaihara
                    ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                    ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                    ' If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                    If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                        exist_flg = True
                        Exit For
                    End If

                    'Ver2.0.3.6
                    'ワイヤーマークがあれば印刷可能とする
                    With hudtFuInfo.udtFuPort(Slotno).udtFuPin(i)
                        For j = LBound(.strWireMark) To UBound(.strWireMark) Step 1
                            If NZfS(.strWireMark(j)) <> "" Then
                                exist_flg = True
                                Exit For
                            End If
                        Next j
                        For j = LBound(.strWireMarkClass) To UBound(.strWireMarkClass) Step 1
                            If NZfS(.strWireMarkClass(j)) <> "" Then
                                exist_flg = True
                                Exit For
                            End If
                        Next j
                        For j = LBound(.strCoreNoIn) To UBound(.strCoreNoIn) Step 1
                            If NZfS(.strCoreNoIn(j)) <> "" Then
                                exist_flg = True
                                Exit For
                            End If
                        Next j
                        For j = LBound(.strCoreNoCom) To UBound(.strCoreNoCom) Step 1
                            If NZfS(.strCoreNoCom(j)) <> "" Then
                                exist_flg = True
                                Exit For
                            End If
                        Next j
                        For j = LBound(.strDist) To UBound(.strDist) Step 1
                            If NZfS(.strDist(j)) <> "" Then
                                exist_flg = True
                                Exit For
                            End If
                        Next j
                    End With

                Next
        End Select

        Return exist_flg

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： ﾃﾞｼﾞﾀﾙﾃﾞｰﾀ設定
    ' 引数      ： hintFuNo    FU番号
    '           ： hintPortNo    基板番号
    '           ： hintTermNo    端子番号
    '           ： hintScFlg     隠しCH表示ﾌﾗｸﾞ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub SetPreviewDigData(ByVal hintFuNo As Integer, _
                                  ByVal hintPortNo As Integer, _
                                  ByVal hintTermNo As Integer, _
                                  ByVal hintScFlg As Integer, _
                                  ByRef udtPrtData As gTypFuInfoPin)

        Dim wkI As Integer
        Dim nCount As Integer = 0
        Dim strCHNo As String

        Try

            '' CH設定をｸﾘｱする
            udtPrtData.strChNo = ""
            udtPrtData.strChNo2 = ""
            udtPrtData.strChNo3 = ""
            udtPrtData.intChType = 0
            udtPrtData.intDataType = 0

            For wkI = 0 To gCstChannelIdMax - 1
                '' FU番号順に並べ替えているので、FUの番号が超えたら終了
                If gPrtDigData(wkI).FCUNo > hintFuNo Then
                    Exit For
                End If
                '' FU番号順に並べ替えているので、基板番号が超えたら終了
                If gPrtDigData(wkI).FCUNo = hintFuNo And gPrtDigData(wkI).PortNo > hintPortNo Then
                    Exit For
                End If
                '' FU番号順に並べ替えているので、端子番号が超えたら終了
                If gPrtDigData(wkI).FCUNo = hintFuNo And gPrtDigData(wkI).PortNo = hintPortNo And _
                    gPrtDigData(wkI).TermNo > hintTermNo Then
                    Exit For
                End If


                If gPrtDigData(wkI).FCUNo = hintFuNo And gPrtDigData(wkI).PortNo = hintPortNo And _
                    gPrtDigData(wkI).TermNo = hintTermNo Then

                    If hintScFlg = 0 And gPrtDigData(wkI).bSCFg = True Then      '' 隠しCH表示なし
                        Continue For
                    End If

                    If gPrtDigData(wkI).bDummyFg = True Then        '' 
                        Continue For
                    End If

                    If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                        If gPrtDigData(wkI).nCHNo < 1000 Then
                            strCHNo = gPrtDigData(wkI).nCHNo.ToString("0000")
                        Else
                            strCHNo = gPrtDigData(wkI).nCHNo
                        End If
                    Else        '' ﾀｸﾞ表示
                        strCHNo = GetTagNoFromCHNo(gPrtDigData(wkI).nCHNo)
                    End If

                    If nCount = 0 Then
                        udtPrtData.strChNo = strCHNo
                        udtPrtData.strItemName = gPrtDigData(wkI).strItemName
                        udtPrtData.strStatus = gPrtDigData(wkI).strStatus
                        udtPrtData.intChType = gPrtDigData(wkI).CHType
                        udtPrtData.intDataType = gPrtDigData(wkI).DataType
                        udtPrtData.blnChComSc = False

                    ElseIf nCount = 1 Then
                        udtPrtData.strChNo2 = strCHNo
                    Else
                        udtPrtData.strChNo3 = strCHNo
                    End If

                    nCount = nCount + 1

                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    '  DO端子設定 端子毎の設定取得
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Private Function getTermType(shtSetValue As Short, term As Integer) As Integer

        Dim UshtSetValue As UShort
        Dim ret As Integer = 0
        Dim a As Integer

        UshtSetValue = Convert.ToUInt16(shtSetValue.ToString("X4"), 16)

        If UshtSetValue >= 0 And UshtSetValue < 20 Then
            Return 0
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
            Select Case UshtSetValue
                Case 0  'None
                    ret = 0
                Case 2  'TMDO
                    ret = 1
                Case 3  'TMRY
                    ret = 2
                Case 4  'TMRY-1
                    ret = 3
                Case 5  'TMRY-2
                    ret = 4
                Case Else
                    ret = 0
            End Select
        End If

        Return ret

    End Function



#End Region



#Region "LocalUnit Print関係 frmPrtLocalUnitPreviewから移植するだけ"

#Region "LU用_変数"
    Private printLUpageMax As Integer
#End Region

#Region "LU用_関数"
    'LU総ページ数取得
    Private Sub subLUgetPageMax()
        Dim intTemp As Integer = 0

        For i = 0 To UBound(gudt.SetFu.udtFu)
            If gudt.SetFu.udtFu(i).shtUse = 1 Then
                intTemp = i
            End If
        Next

        ''Page総数
        If intTemp = 0 Then
            '何も設定されていない場合は、ページ数「1」
            intTemp = 1
        Else
            intTemp = CInt((intTemp + 1) / 7 + 0.4)
        End If

        printLUpageMax = intTemp
    End Sub

    'LU内容印刷
    Private Sub DrawLinesLU(ByVal e As System.Drawing.Graphics, ByVal udtPrtPageInfo As gTypPrintTerminalInfo)

        Try

            Dim p1 As New Pen(Color.Black, 1)
            Dim p2 As New Pen(Color.Black, 2)
            Dim p1d As New Pen(Color.Black, 1)
            Dim i As Integer, j As Integer
            Dim intTemp As Integer, intTempH As Integer, intTemp2 As Integer
            Dim frmLeft As Integer, frmUp As Integer, frmWidth As Integer, frmHeight As Integer
            Dim wNO As Integer, wCPU As Integer, wREMA As Integer
            Dim hGrp As Integer, hBaseHeader As Integer, hBaseGrp As Integer
            Dim strTemp As String = ""
            Dim strTemp2 As String = ""
            Dim strRL As String = ""
            Dim strRYtype As String = ""
            Dim intRYNo As Integer = 0
            Dim intPage As Integer, intCnt As Integer = 0
            Dim funo As Integer = 0

            '2015/4/9 T.Ueki 斜線書込み対応
            Dim PrintFUNo As String
            Dim PrintSlotType As String
            Dim PrintSlotTypeLen As Integer
            Dim PrintPLCSW As Integer

            intPage = udtPrtPageInfo.intPageNo - 1

            ''フレームサイズ取得
            frmLeft = gCstFrameLocalUnitLeft
            frmUp = gCstFrameLocalUnitUp
            frmWidth = gCstFrameLocalUnitWidth
            frmHeight = gCstFrameLocalUnitHight

            wNO = 50
            wCPU = 80
            wREMA = 80

            hGrp = 110
            hBaseHeader = 30
            hBaseGrp = hGrp / 5

            ''端子台のページNoの初期値を獲得する************
            'intCnt = mGetPageIndex(mintPageCount)
            'intCnt += 1     ''端子台の帳票の開始ページは2ページ目からなので＋1

            '' 印刷フォーム調整　ver.1.4.0 2011.08.10
            '**HEADER LINE***********************************
            p1d.DashStyle = Drawing.Drawing2D.DashStyle.Dash

            e.DrawLine(p2, frmLeft, frmUp + hBaseHeader, frmLeft + frmWidth, frmUp + hBaseHeader)     'H2

            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader, frmLeft + wNO, frmUp + hBaseHeader * 4 + hGrp * 7)     'V left2
            e.DrawLine(p1, frmLeft + wNO * 2, frmUp + hBaseHeader, frmLeft + wNO * 2, frmUp + hBaseHeader * 4 + hGrp * 7)   'V left3

            intTemp = ((frmLeft + frmWidth - (wCPU + wREMA)) - (frmLeft + wNO * 2)) / 8

            For j = 1 To 8
                e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * j, frmUp + hBaseHeader * 2, (frmLeft + wNO * 2) + intTemp * j, frmUp + hBaseHeader * 4)

                e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), frmUp + hBaseHeader * 3, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), frmUp + hBaseHeader * 4)
            Next

            e.DrawLine(p1, frmLeft + wNO * 2, frmUp + hBaseHeader * 2, frmLeft + frmWidth - wCPU - wREMA, frmUp + hBaseHeader * 2)  'H3
            e.DrawLine(p1, frmLeft, frmUp + hBaseHeader * 3, frmLeft + frmWidth - wCPU - wREMA, frmUp + hBaseHeader * 3)  'H4

            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader, frmLeft + wNO * 2, frmUp + hBaseHeader * 3)  'H4
            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader * 3, frmLeft + wNO * 2, frmUp + hBaseHeader * 4)  'H4

            '*********************************************


            '**BODY LINE**********************************
            For i = 0 To 6

                'Grp段頭の高さ
                intTempH = frmUp + hBaseHeader * 4 + hGrp * i

                e.DrawLine(p2, frmLeft, intTempH, frmLeft + frmWidth, intTempH)  '段の区切り線

                e.DrawLine(p1, frmLeft, intTempH + hBaseGrp, (frmLeft + wNO * 2), intTempH + hBaseGrp)  'Grp横線1-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp)  'Grp横線1-2

                e.DrawLine(p1, frmLeft, intTempH + hBaseGrp * 2, (frmLeft + wNO * 2), intTempH + hBaseGrp * 2)  'Grp横線2-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp * 2, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 2)  'Grp横線2-2
                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 2, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 2)  'Grp横線2-3

                e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 3, frmLeft + wNO * 2, intTempH + hBaseGrp * 3)  'Grp横線3-1

                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 3, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 3)  'Grp横線3-2

                e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 4, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 4)  'Grp横線4
                e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 4, frmLeft + wNO * 2, intTempH + hBaseGrp * 4)  'Grp横線4-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp * 4, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 4)  'Grp横線4-2
                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 4, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 4)  'Grp横線4-3

                'Grp内縦線描画
                For j = 1 To 8
                    e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * j, intTempH, (frmLeft + wNO * 2) + intTemp * j, intTempH + hGrp)
                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), intTempH + hGrp)
                Next

                For j = 1 To 5
                    e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, intTempH + hBaseGrp * 2, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, intTempH + hBaseGrp * 4)
                Next

            Next

            e.DrawLine(p2, frmLeft, frmUp + hBaseHeader * 4 + hGrp * 7, frmLeft + frmWidth, frmUp + hBaseHeader * 4 + hGrp * 7)  '段の区切り線

            e.DrawLine(p1, frmLeft, frmUp + frmHeight, frmLeft + frmWidth, frmUp + frmHeight)

            'Ver2.0.7.4 備考欄の縦線消去
            'e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * 8, frmUp + hBaseHeader, (frmLeft + wNO * 2) + intTemp * 8, 1070 + 3)  'V
            e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * 8, frmUp + hBaseHeader, (frmLeft + wNO * 2) + intTemp * 8, frmUp + hBaseHeader * 4 + hGrp * 7)  'V

            e.DrawLine(p1, frmLeft + frmWidth - wREMA, frmUp + hBaseHeader, frmLeft + frmWidth - wREMA, frmUp + hBaseHeader * 4 + hGrp * 7)         'V r2
            '************************************************************************************

            '**TEXT PRINT************************************************************************
            Dim fnt As New Font("Courier New", 14)
            Dim fnt1 As New Font("Courier New", 10) ''和文仕様 20200217 hori
            Dim fnt1j As New Font("ＭＳ 明朝", 10)
            Dim fnt2 As New Font("Courier New", 8)
            Dim fnt2j As New Font("ＭＳ 明朝", 8)
            Dim fnt3 As New Font("Courier New", 7)


            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori

                e.DrawString("フィールドユニット基板 ＆ 端子台配列", fnt1j, Brushes.Black, frmLeft + frmWidth / 100 * 30, frmUp + 7)
                e.DrawString("基板型式", fnt1j, Brushes.Black, frmLeft + wNO * 2 + 180, frmUp + hBaseHeader + 3)
                e.DrawString("端子台型式 (TB*A/TB*B)", fnt1j, Brushes.Black, frmLeft + wNO * 2 + 130, frmUp + hBaseHeader + 17)
                e.DrawString("番号", fnt1j, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader + 10)
                e.DrawString("型式", fnt1j, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader * 3 + 10)
                
                For i = 0 To 7
                    e.DrawString("TB" & Str(i + 1), fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 6, frmUp + hBaseHeader * 2 + 6)
                    e.DrawString("A", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 7, frmUp + hBaseHeader * 3 + 6)
                    e.DrawString("B", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + CInt(intTemp / 2) + 7, frmUp + hBaseHeader * 3 + 6)
                Next

                e.DrawString("CPU", fnt1, Brushes.Black, frmLeft + wNO * 2 + 466, frmUp + hBaseHeader * 2 + 6)
                e.DrawString("備考", fnt1j, Brushes.Black, frmLeft + wNO * 2 + 542, frmUp + hBaseHeader * 2 + 6)
                
            Else    '英文
                e.DrawString("FIELD UNIT MODULE & TERMINAL ARRANGEMENT", fnt, Brushes.Black, frmLeft + frmWidth / 100 * 20, frmUp + 5)

                e.DrawString("MODULE TYPE", fnt1, Brushes.Black, frmLeft + wNO * 2 + 150, frmUp + hBaseHeader + 1)
                e.DrawString("TERMINAL TYPE (TB*A/TB*B)", fnt1, Brushes.Black, frmLeft + wNO * 2 + 120, frmUp + hBaseHeader + 12)

                e.DrawString("NO", fnt1, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader + 10)
                e.DrawString("TYPE", fnt1, Brushes.Black, frmLeft + 5, frmUp + hBaseHeader * 3 + 10)

                For i = 0 To 7
                    e.DrawString("TB" & Str(i + 1), fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 6, frmUp + hBaseHeader * 2 + 6)
                    e.DrawString("A", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 7, frmUp + hBaseHeader * 3 + 6)
                    e.DrawString("B", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + CInt(intTemp / 2) + 7, frmUp + hBaseHeader * 3 + 6)
                Next

                '' CPU追加　ver.1.4.0 2011.08.02
                e.DrawString("CPU", fnt1, Brushes.Black, frmLeft + wNO * 2 + 466, frmUp + hBaseHeader * 2 + 6)

                e.DrawString("Remarks", fnt1, Brushes.Black, frmLeft + wNO * 2 + 528, frmUp + hBaseHeader * 2 + 6)

                'SMS-U650-A-
            End If

            'SMS-U650-A-
            For i = 0 To 6  '行ループ
                '' LCU    → FCUに変更　ver.1.4.0 2011.08.02
                '' LCU-TM → -TM
                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                    e.DrawString("FCU-", fnt2, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hGrp * i + 6)
                    e.DrawString("-TM", fnt2, Brushes.Black, frmLeft + wNO + 4, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)
                    e.DrawString("ケーブル", fnt2j, Brushes.Black, frmLeft + wNO + 2, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 6)
                    e.DrawString("端子台", fnt2j, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 6)

                Else
                    e.DrawString("FCU-", fnt2, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hGrp * i + 6)
                    e.DrawString("-TM", fnt2, Brushes.Black, frmLeft + wNO + 4, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)
                    e.DrawString("CABLE", fnt2, Brushes.Black, frmLeft + wNO + 5, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 6)
                    e.DrawString("TERM", fnt2, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 6)
                End If

                funo = i + (((udtPrtPageInfo.intPageNo - 1) - 1) * 7)
                If gudt.SetFu.udtFu(funo).shtUse = 1 Then
                    '**TYPE ALPH ******************
                    Select Case funo
                        Case 0 : strTemp = "FCU"
                        Case 1 : strTemp = "FU1"
                        Case 2 : strTemp = "FU2"
                        Case 3 : strTemp = "FU3"
                        Case 4 : strTemp = "FU4"
                        Case 5 : strTemp = "FU5"
                        Case 6 : strTemp = "FU6"
                        Case 7 : strTemp = "FU7"
                        Case 8 : strTemp = "FU8"
                        Case 9 : strTemp = "FU9"
                        Case 10 : strTemp = "FU10"
                        Case 11 : strTemp = "FU11"
                        Case 12 : strTemp = "FU12"
                        Case 13 : strTemp = "FU13"
                        Case 14 : strTemp = "FU14"
                        Case 15 : strTemp = "FU15"
                        Case 16 : strTemp = "FU16"
                        Case 17 : strTemp = "FU17"
                        Case 18 : strTemp = "FU18"
                        Case 19 : strTemp = "FU19"
                        Case 20 : strTemp = "FU20"
                    End Select

                    If funo <= 9 Then
                        e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    Else
                        e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 5, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    End If

                    'FU番号保持 T.Ueki 斜線書込み対応
                    PrintFUNo = strTemp

                    '******************************

                    '**CPU ************************
                    If gudt.SetFu.udtFu(funo).shtCanBus = 1 Then    ' CANBUS有
                        strTemp = "M001A-C"
                    Else
                        If funo = 0 Then                            ' FCU SUB
                            strTemp = "M001A-S"
                        Else                                        ' FU
                            strTemp = "M001A"
                        End If
                    End If
                    'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応
                    If g_bytTerVer = 1 Then
                        strTemp = strTemp.Replace("A", "")
                    End If

                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + wNO * 2 + 450, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    '******************************

                    '**COM ************************
                    If funo = 0 Then                            ' FCU SUB
                        strTemp = ""
                    Else                                        ' FU
                        strTemp = "COM"
                    End If
                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + wNO * 2 + 466, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)
                    '******************************

                    '**DIP SW *********************
                    e.DrawString("ON", fnt3, Brushes.Black, frmLeft + wNO * 2 + 440, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 6)
                    e.DrawString("OFF", fnt3, Brushes.Black, frmLeft + wNO * 2 + 440, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 6)

                    For j = 1 To 5
                        If funo And (&H1 << (j - 1)) Then
                            e.DrawString("o", fnt1, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 2)
                        Else
                            e.DrawString("o", fnt1, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 2)
                        End If
                        e.DrawString(j.ToString, fnt2, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 4 + hGrp * i)
                    Next
                    '******************************

                    'T.Ueki Reamrksの2行表示処理変更
                    '**Remarks*********************
                    'Ver2.0.0.2 Remarksのﾌｫﾝﾄｻｲｽﾞ
                    Dim fnt_remark As New Font("Courier New", 10)
                    Dim fnt_remarkj As New Font("ＭＳ 明朝", 10)

                    '改行するバイト数
                    Dim KaigyoByte As Integer = 8

                    Dim LenMoji As Integer      '文字の長さ
                    Dim SearchMoji As String    '検索対象文字(1文字)
                    Dim LineMoji1 As String     '1行目
                    Dim LineMoji2 As String     '2行目
                    Dim LineMoji3 As String     '3行目
                    Dim LineMoji4 As String     '4行目
                    Dim LenGyo As Integer = 1   '行

                    Dim k As Long

                    strTemp = gGetString(gudt.SetChDisp.udtChDisp(funo).strRemarks)
                    'Ver2.0.1.5 「^」をｽﾍﾟｰｽと置き換える
                    'Ver2.0.1.7 「^」は改行文字とする
                    'Ver2.0.2.5 和文対応 行数を4行までOKとする
                    'strTemp = strTemp.Replace("^", " ")

                    '初期化
                    SearchMoji = ""
                    LineMoji1 = ""
                    LineMoji2 = ""
                    LineMoji3 = ""
                    LineMoji4 = ""
                    LenMoji = 0

                    For k = 1 To Len(strTemp)
                        SearchMoji = Mid(strTemp, k, 1)

                        Select Case SearchMoji
                            Case "^"
                                LenMoji = KaigyoByte + 1
                                SearchMoji = ""
                            Case " " To "z"     '0x20 - 0x7A
                                LenMoji = LenMoji + 1
                                'Case "A" To "Z"
                                '    LenMoji = LenMoji + 1
                                'Case "0" To "9"
                                '    LenMoji = LenMoji + 1
                            Case "｡" To "ﾟ"   '0xA1 - 0xDF (｡ - ﾟ)半角ｶﾀｶﾅ  2014.11.17
                                LenMoji = LenMoji + 1
                            Case Else
                                LenMoji = LenMoji + 2
                        End Select

                        '8ﾊﾞｲﾄ以下なら1行目
                        If LenMoji >= KaigyoByte + 1 Then
                            'LineMoji2 = LineMoji2 + SearchMoji
                            LenGyo = LenGyo + 1
                            LenMoji = 0
                            Select Case LenGyo
                                Case 1
                                    LineMoji1 = LineMoji1 + SearchMoji
                                Case 2
                                    LineMoji2 = LineMoji2 + SearchMoji
                                Case 3
                                    LineMoji3 = LineMoji3 + SearchMoji
                                Case Else
                                    LineMoji4 = LineMoji4 + SearchMoji
                            End Select
                        Else
                            'LineMoji1 = LineMoji1 + SearchMoji
                            Select Case LenGyo
                                Case 1
                                    LineMoji1 = LineMoji1 + SearchMoji
                                Case 2
                                    LineMoji2 = LineMoji2 + SearchMoji
                                Case 3
                                    LineMoji3 = LineMoji3 + SearchMoji
                                Case Else
                                    LineMoji4 = LineMoji4 + SearchMoji
                            End Select
                        End If
                    Next k

                    'Ver2.0.0.2 Reamrksのﾌｫﾝﾄは他と変更可能にしておく
                    'Ver2.0.2.5 RemarksMAX4行対応
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '' 和文表示の場合  20200306 hori
                        'If LenMoji <= KaigyoByte Then
                        '    e.DrawString(strTemp, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        'Else
                        '    e.DrawString(LineMoji1, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        '    e.DrawString(LineMoji2, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                        'End If
                        Select Case LenGyo
                            Case 1
                                e.DrawString(strTemp, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                            Case 2
                                e.DrawString(LineMoji1, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                            Case 3
                                e.DrawString(LineMoji1, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                                e.DrawString(LineMoji3, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16)
                            Case Else
                                e.DrawString(LineMoji1, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                                e.DrawString(LineMoji3, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16)
                                e.DrawString(LineMoji4, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16 + 16)
                        End Select
                    Else
                        'If LenMoji <= KaigyoByte Then
                        '    e.DrawString(strTemp, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        'Else
                        '    e.DrawString(LineMoji1, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        '    e.DrawString(LineMoji2, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                        'End If
                        Select Case LenGyo
                            Case 1
                                e.DrawString(strTemp, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                            Case 2
                                e.DrawString(LineMoji1, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                            Case 3
                                e.DrawString(LineMoji1, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                                e.DrawString(LineMoji3, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16)
                            Case Else
                                e.DrawString(LineMoji1, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                                e.DrawString(LineMoji2, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                                e.DrawString(LineMoji3, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16)
                                e.DrawString(LineMoji4, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16 + 16 + 16)
                        End Select
                    End If


                    'strTemp = gGetString(gudt.SetChDisp.udtChDisp(funo).strRemarks)
                    'If strTemp.Length <= 8 Then
                    '    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    'Else
                    '    e.DrawString(strTemp.Substring(0, 8), fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    '    e.DrawString(strTemp.Substring(8), fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 12)
                    'End If

                    '******************************

                    '**TYPE NUMBER*****************
                    ''表示文字数変更　ver.1.4.0 2011.08.02
                    ''型名変更  2013.10.21
                    strTemp = Trim(Mid(gudt.SetChDisp.udtChDisp(funo).strFuType, _
                                       Len("SMS-U5650") + 1, _
                                       Len(gudt.SetChDisp.udtChDisp(funo).strFuType) - Len("SMS-U5650")))

                    'If Len(strTemp) = 2 Then
                    '    intTemp2 = 10
                    'Else
                    intTemp2 = 5
                    'End If

                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + intTemp2, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 5)
                    '******************************

                    'SLOTﾀｲﾌﾟ保持 T.Ueki 斜線書込み対応
                    PrintSlotTypeLen = Len(strTemp)

                    Select Case PrintSlotTypeLen
                        Case 0
                            PrintSlotType = ""
                        Case 2
                            PrintSlotType = Mid(strTemp, 2, 1)
                        Case Else
                            PrintSlotType = Mid(strTemp, 3, 1)
                    End Select


                    '**INPUT TYPE******************
                    For j = 0 To 7  '列ループ

                        'If mintRecCntSlot(i + ((mintPageCount - 1) * 7), j) > 0 Then

                        strTemp = ""
                        strTemp2 = ""


                        If j = 0 Then
                            ''型名変更  2013.10.21
                            'Ver2.0.4.1 R分岐端子種類追加 -23～-35P
                            'Ver2.0.7.P 端子種類追加(J)
                            'Ver2.0.7.Z 端子種類追加(J2)
                            'Ver2.0.8.7 端子種類追加(J3)
                            'Ver2.0.8.9 端子種類追加(42) 2018.10.01
                            If Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-23" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-32" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-33" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-35" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-33P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-35P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12J" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13J" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15J" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18J" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12J2" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13J2" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15J2" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18J2" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12J3" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13J3" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15J3" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18J3" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-42" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-42A" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-42B" Then
                                ''Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15FH" Then

                                strRL = "-R"
                            Else
                                strRL = "-L"
                            End If
                        Else
                            strRL = "-L"
                        End If

                        Select Case gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType
                            Case 1
                                strTemp = "M003A"   ''DO

                                If (gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf > 1) Then    'TMRY
                                    strTemp2 = "RY"
                                Else
                                    strTemp2 = "DO"
                                End If

                                'If gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 2 Then
                                '    strTemp2 = "RY"
                                'ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 3 Then
                                '    strTemp2 = "RY1"
                                'ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 4 Then
                                '    strTemp2 = "RY2"
                                'Else
                                '    strTemp2 = "DO"
                                'End If

                            Case 2
                                strTemp = "M002A"   ''DI
                                strTemp2 = "DI"

                            Case 3
                                strTemp = "M030A"   ''AO
                                strTemp2 = "AO"

                            Case 4
                                strTemp = "M100A"   ''AI(2線式)
                                strTemp2 = "15"

                            Case 5
                                strTemp = "M110A"   ''AI(3線式)
                                strTemp2 = "TEMP"   ''型名変更  2013.10.21

                            Case 6
                                strTemp = "M500A"   ''AI(1-5V)
                                strTemp2 = "15"

                            Case 7
                                strTemp = "M400A"   ''AI(4-20mA)
                                strTemp2 = "42"

                            Case 8
                                strTemp = "M200A"   ''AI(K)
                                strTemp2 = "K"      ''型名変更  2013.10.21

                                'Ver2.0.8.1 M200Aに派生基板あり 型名が違う
                                If (gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf >= 1) Then
                                    strTemp2 = "K-1"
                                End If

                        End Select

                        'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応
                        If g_bytTerVer = 1 Then
                            strTemp = strTemp.Replace("A", "")
                        End If

                        If strTemp <> "" Then

                            e.DrawString(strTemp, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 6, frmUp + hBaseHeader * 4 + hGrp * i + 5)                     '基板型式

                            If gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 1 Then   ' DO

                                'Ver.2.0.8.P
                                Dim shtTerinf As Short = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf
                                Dim ushtTerinf As UShort = Convert.ToUInt16(shtTerinf.ToString("X4"), 16)

                                '' TMRYの場合は4分割
                                'If (gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf > 1 And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf < 20) Then 
                                If (ushtTerinf > 1 And ushtTerinf < 20) Then 'TMRY
                                    '' 4分割ライン
                                    intTempH = frmUp + hBaseHeader * 4 + hGrp * i
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hGrp)
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hGrp)

                                    '' 2013.10.25
                                    If 2 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 5 Then
                                        strRYtype = Mid(strRL, 2, 1) & " "
                                        'strRYtype = " " & Mid(strRL, 2, 1)
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 1
                                    ElseIf 6 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 9 Then
                                        strRYtype = Mid(strRL, 2, 1) & "1"
                                        'strRYtype = "1" & Mid(strRL, 2, 1)
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 5
                                    ElseIf 10 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 13 Then
                                        strRYtype = "2"
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 9
                                    Else
                                        strRYtype = ""
                                    End If


                                    e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                    e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2

                                    e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE(A)

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 2, funo, j) Then
                                    If intRYNo > 1 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(B)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(B)-2
                                        e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(B)
                                    End If

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 3, funo, j) Then
                                    If intRYNo > 2 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(C)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(C)-2
                                        e.DrawString((j + 1).ToString & "C", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(C)
                                    End If

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 4, funo, j) Then
                                    If intRYNo > 3 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(D)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(D)-2
                                        e.DrawString((j + 1).ToString & "D", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(D)
                                    End If

                                ElseIf (ushtTerinf > 20) Then    'Ver.2.0.8.P DO端子台混在

                                    Dim intTermSet(4) As Integer
                                    Dim idx As Integer

                                    '端子台種類取得
                                    '0:設定無し　1:TMDO　2:TMRY　3:TMRY1　4:TMRY2
                                    For idx = 0 To intTermSet.Length - 1
                                        intTermSet(idx) = getTermType(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf, idx)
                                    Next

                                    If intTermSet(0) = 1 And intTermSet(2) = 1 Then 'TMDOが2枚
                                        strTemp2 = "DO"
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)      '端子台型式(A)
                                        e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)          'CABLE(A)

                                        ''If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Or ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then    '' DOの場合、33-64をチェックするように変更  2015.01.26
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                        e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(B)
                                        ''End If

                                    ElseIf intTermSet(0) = 1 And intTermSet(2) <> 1 Then 'TMDOが1枚 + TMRY*が2枚

                                        'C,D端子を分割線
                                        intTempH = frmUp + hBaseHeader * 4 + hGrp * i
                                        e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hGrp)

                                        strTemp2 = "DO"
                                        'A&B側の線
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)      '端子台型式(A)
                                        e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)          'CABLE(A)

                                        Dim strRYtype2 As String
                                        If intTermSet(2) = 2 Then
                                            strRYtype = Mid(strRL, 2, 1) & " "
                                        ElseIf intTermSet(2) = 3 Then
                                            strRYtype = Mid(strRL, 2, 1) & "1"
                                        ElseIf intTermSet(2) = 4 Then
                                            strRYtype = "2"
                                        Else
                                            strRYtype = ""
                                        End If

                                        If intTermSet(3) = 2 Then
                                            strRYtype2 = Mid(strRL, 2, 1) & " "
                                        ElseIf intTermSet(3) = 3 Then
                                            strRYtype2 = Mid(strRL, 2, 1) & "1"
                                        ElseIf intTermSet(3) = 4 Then
                                            strRYtype2 = "2"
                                        Else
                                            strRYtype2 = ""
                                        End If

                                        strTemp2 = "RY"

                                        If intTermSet(2) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            ''If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Then
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(C)-1
                                            e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(C)-2
                                            e.DrawString((j + 1).ToString & "C", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(C)
                                            ''End If
                                        End If

                                        If intTermSet(3) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            '' If ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(D)-1
                                            e.DrawString(strRYtype2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(D)-2
                                            e.DrawString((j + 1).ToString & "D", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(D)
                                            ''End If
                                        End If

                                    ElseIf intTermSet(0) <> 1 And intTermSet(2) = 1 Then 'TMRY*が2枚 + TMDOが1枚
                                        intTempH = frmUp + hBaseHeader * 4 + hGrp * i
                                        e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hGrp)

                                        strTemp2 = "DO"
                                        ''If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Or ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then    '' DOの場合、33-64をチェックするように変更  2015.01.26
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                        e.DrawString((j + 1).ToString & "C", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(B)
                                        ''End If

                                        strTemp2 = "RY"
                                        Dim strRYtype2 As String
                                        If intTermSet(0) = 2 Then
                                            strRYtype = Mid(strRL, 2, 1) & " "
                                        ElseIf intTermSet(0) = 3 Then
                                            strRYtype = Mid(strRL, 2, 1) & "1"
                                        ElseIf intTermSet(0) = 4 Then
                                            strRYtype = "2"
                                        Else
                                            strRYtype = ""
                                        End If

                                        If intTermSet(1) = 2 Then
                                            strRYtype2 = Mid(strRL, 2, 1) & " "
                                        ElseIf intTermSet(1) = 3 Then
                                            strRYtype2 = Mid(strRL, 2, 1) & "1"
                                        ElseIf intTermSet(1) = 4 Then
                                            strRYtype2 = "2"
                                        Else
                                            strRYtype2 = ""
                                        End If

                                        If intTermSet(0) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            If strRYtype <> "" Then
                                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                                e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2
                                                e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE(A)
                                            End If
                                        End If

                                        If intTermSet(1) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            If strRYtype <> "" Then
                                                ''If ChkTerminalCount(mudtFuInfo(funo), 2, funo, j) Then
                                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(B)-1
                                                e.DrawString(strRYtype2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(B)-2
                                                e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(B)
                                                ''End If
                                            End If
                                        End If

                                    ElseIf intTermSet(0) <> 1 And intTermSet(2) <> 1 Then 'TMRY*で構成
                                        '4分割ライン
                                        intTempH = frmUp + hBaseHeader * 4 + hGrp * i
                                        e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hGrp)
                                        e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hGrp)

                                        strTemp2 = "RY"

                                        Dim strRYtypeMulti(4) As String
                                        Dim m As Integer

                                        For m = 0 To strRYtypeMulti.Length - 1
                                            If intTermSet(m) = 2 Then
                                                strRYtypeMulti(m) = Mid(strRL, 2, 1) & " "
                                            ElseIf intTermSet(m) = 3 Then
                                                strRYtypeMulti(m) = Mid(strRL, 2, 1) & "1"
                                            ElseIf intTermSet(m) = 4 Then
                                                strRYtypeMulti(m) = "2"
                                            Else
                                                strRYtypeMulti(m) = ""
                                            End If
                                        Next

                                        If intTermSet(0) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)                 '端子台型式(A)-1
                                            e.DrawString(strRYtypeMulti(0), fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)   '端子台型式(A)-2
                                            e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j, frmUp + hBaseHeader * 4 + hGrp * i + 60)             'CABLE(A)
                                        End If

                                        If intTermSet(1) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            ''If ChkTerminalCount(mudtFuInfo(funo), 2, funo, j) Then
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)                '端子台型式(B)-1
                                            e.DrawString(strRYtypeMulti(1), fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)  '端子台型式(B)-2
                                            e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hGrp * i + 60)        'CABLE(B)
                                            ''End If
                                        End If

                                        If intTermSet(2) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            ''If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Then
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)                '端子台型式(C)-1
                                            e.DrawString(strRYtypeMulti(2), fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)  '端子台型式(C)-2
                                            e.DrawString((j + 1).ToString & "C", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hGrp * i + 60)        'CABLE(C)
                                            ''End If
                                        End If

                                        If intTermSet(3) <> 0 Then  ''「None」の場合は非表示 20200428 hori
                                            ''If ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then
                                            e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)                '端子台型式(D)-1
                                            e.DrawString(strRYtypeMulti(3), fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)  '端子台型式(D)-2
                                            e.DrawString((j + 1).ToString & "D", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hGrp * i + 60)        'CABLE(D)
                                            ''End If
                                        End If

                                        Else
                                            e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)      '端子台型式(A)
                                            e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)          'CABLE(A)

                                            If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Or ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then    '' DOの場合、33-64をチェックするように変更  2015.01.26
                                                e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                                e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(B)
                                            End If
                                        End If
                                Else
                                    e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)      '端子台型式(A)
                                    e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)          'CABLE(A)

                                    If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Or ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then    '' DOの場合、33-64をチェックするように変更  2015.01.26
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                        e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(B)
                                    End If

                                End If

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 2 Then   ' DI
                                e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(A)
                                e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(A)

                                If ChkTerminalCount(mudtFuInfo(funo), 2, funo, j) Then
                                    e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                    e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60) 'CABLE(B)
                                End If

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 5 Then   'AI(3線式)    2013.10.21
                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                e.DrawString(strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 3 Or gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 7 Then
                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i) '端子台型式

                                'T.Ueki 端子R L表示位置修正
                                'e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式
                                e.DrawString(strRL & "-1", fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            Else
                                'Ver2.0.8.1
                                '長い文字列の場合,LRを改行
                                If LenB(strTemp2) >= 3 Then
                                    e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                    e.DrawString(strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2
                                Else
                                    e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式
                                End If
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            End If


                        Else

                            '2015.4.9 T.Ueki 斜線書込み対応
                            intTempH = frmUp + hBaseHeader * 4 + hGrp * i   '高さ指定

                            If PrintFUNo = "FCU" Then

                                '5ｽﾛｯﾄ目から斜線
                                If j > 4 Then
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j), intTempH, CInt((frmLeft + wNO * 2) + intTemp * (j + 1)), intTempH + hGrp)
                                End If
                            Else

                                Select Case PrintSlotType

                                    Case "2"
                                        PrintPLCSW = 1
                                    Case "3"
                                        PrintPLCSW = 2
                                    Case "5"
                                        PrintPLCSW = 4
                                    Case "8"
                                        PrintPLCSW = 7
                                    Case Else
                                        '処理無し

                                End Select

                                If j > PrintPLCSW Then
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j), intTempH, CInt((frmLeft + wNO * 2) + intTemp * (j + 1)), intTempH + hGrp)


                                End If

                            End If

                        End If

                    Next j
                    '********************
                End If
            Next i

            p1.Dispose()
            p2.Dispose()
            p1d.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

End Class
