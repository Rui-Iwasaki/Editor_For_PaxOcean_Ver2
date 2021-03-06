﻿Public Class frmPrtChannelPreview

#Region "変数定義"

    Private Const mPageDataMax As Integer = 20      ''1Pageは20CH分設定可
    Private Const mCstMaxPage As Single = 360       ''最大ページ数

    Private Const mDmyString As String = "#"        ''仮設定の先頭文字

    Private mudtChannelGroup As gTypChannelGroup    ''CHグループ情報
    Private mSlectPageIndex As Integer              ''表示ページ

    ''総ページ数
    Private mPageMax As Integer

    ''Channel List Print画面　パラメータ
    Private mintPrintMode As Integer        ''0:Print  1:Preview
    Private mintPrintAllPage As Integer     ''0:ALL    1:Pages     
    Private mintPageFrom As Integer         ''Pages From
    Private mintPageTo As Integer           ''Pages To
    Private mFlgSC As Boolean
    Private mFlgDmy As Boolean
    Private mPrinterName As String

    Private mPrintGrp As Integer            ''Ver2.0.0.2 計測点入力画面からのﾌﾟﾚﾋﾞｭｰ対応：ｸﾞﾙｰﾌﾟ番号格納


    Public mCHNoFg As Boolean              '' Ver1.8.5.2 2015.12.02  ﾀｸﾞ表示時にCHNo.を印字するﾌﾗｸﾞ

    Private mChListPrint As mChannelList    ''印字用情報(ページ単位)

    ' 印刷位置調整　2013.07.23 K.Fujimoto
    'Private mPrtColLen() As Integer = {3, 1, 4, 30, 17, 8, 8, 2, 4, 2, 2, 8, 2, 4, 2, 2, 3, 1, 9, 2, 4, 4, 16}
    Private mPrtColLen() As Integer = {3, 1, 4, 30, 17, 8, 8, 2, 4, 2, 2, 8, 2, 4, 2, 2, 3, 1, 10, 2, 4, 4, 16}
    '' 2015.10.16 ﾀｸﾞ表示     ' 2015.10.22 Ver1.7.5 標準に戻すためｺﾒﾝﾄ
    'Private mPrtColLen() As Integer = {2, 1, 5, 30, 17, 8, 8, 2, 4, 2, 2, 8, 2, 4, 2, 2, 3, 1, 10, 2, 4, 4, 16}

    '' 列毎の印字桁
    '' 1:NO,        2:SYS,      3:CHNO,     4:CH ITEM,      5:STATUS,       6:UNIT, 
    '' 7:ALM VAL,   8:ALM EXT   9:ALM DLY   10:ALM GR1      11:ALM GR2      
    '' 12:ALM VAL,  13:ALM EXT  14:ALM DLY  15:ALM GR1      16:ALM GR2
    '' 17:INSG      18:S        19:FU ADD   20:AL/RL        21:SENSOR       22:LOCAL/REMOTE     23:REMARKS

    ''コンバイン仕様、共有CH使用時のみ表示
    Private Const mCstColumnSYS As Integer = 1      ''SYS NO
    Private Const mCstColumnShare As Integer = 21   ''共有CH

    'Ver2.0.0.7 OUTの丸印のために、OUT系CHNoを全点格納
    Public aryOutCHNo As ArrayList

#End Region

#Region "構造体定義"
    ''印字CHリスト
    Private Structure mChannelList

        Dim udtPage() As mPageInfo              ''ページ単位の印字情報

    End Structure

    ''ページ単位の印字情報
    Private Structure mPageInfo

        Dim intGroupNo As Integer               ''ページのグループ番号
        Dim intStartIndex As Integer            ''先頭No
        Dim intListType As Integer              ''印刷タイプ　0:標準、1:デジタルコンポジットテーブル表示
        Dim udtChannelData() As gTypSetChRec    ''CH情報(20CH)

    End Structure

    ''印字文字情報
    Private Structure mChannelStr
        Dim SYSNo As String                     ''SYSTEM No
        Dim CHNo As String                      ''CH番号
        Dim CHNo_temp As String                 ''CH番号  Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時の補助表示用
        Dim CHItem As String                    ''CH名称
        Dim Status As String                    ''ステータス
        Dim Range As String                     ''レンジ
        Dim Unit As String                      ''単位
        Dim AlmInf() As mAlarmInfoStr           ''アラーム情報
        Dim INSIG As String                     ''INPUT SIGNAL
        Dim SIGType As String                   ''SIGNAL TYPE
        Dim OUTSIG As String                    ''OUTPUT SIGNAL
        Dim INAdd As String                     ''INPUT ADDRESS
        Dim INAdd2 As String                    ''JACOM-55ｱﾄﾞﾚｽ分割用に追加　2019/04/15
        Dim Digit_flg As Boolean                 ''入力桁数判別用のﾌﾗｸﾞ100以下の場合は一列　100以上は二列で表示  2019/04/16
        Dim OUTAdd As String                    ''OUTPUT ADDRESS
        Dim AL As String                        ''AL
        Dim RL As String                        ''RL
        Dim ShareType As String                 ''共有CHタイプ
        Dim ShareCHNo As String                 ''共有CH No
        Dim Remarks As String                   ''REMARKS
        Dim AlmLevel As String                  ''ﾛｲﾄﾞ対応表示追加　2015.11.12 Ver1.7.8
        Dim TermCount As Integer                ''端子数  Ver1.11.9.2 2016.11.26追加
        Dim OUT As String                       'Ver2.0.0.4 Output設定アリの場合「o」となる
    End Structure

    ''アラーム情報
    Private Structure mAlarmInfoStr
        Dim Value As String                     ''警報値
        Dim ExtGrp As String                    ''Ext. Group No
        Dim Delay As String                     ''Delay
        Dim GrpRep1 As String                   ''Group Repose 1
        Dim GrpRep2 As String                   ''Group Repose 2
    End Structure

#End Region

#Region "サイズ・位置設定"

    ''フレーム余白サイズ設定
    Private Const mCstMarginLeft As Single = 28
    Private Const mCstMarginUp As Single = 80   '2015.03.12 印字位置調整　60 → 80

    ''CH名称 MAXサイズ
    Private Const mCstItemNameLength As Integer = 30

    ''列幅
    Private Const mCstColumnSize As Single = 6.8F ''7 ''6.5F

    ''行幅
    Private Const mCstRowSize As Single = 10

#End Region

#Region "画面イベント"



    '----------------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 0:Print  1:Preview
    '           : ARG2 - (I ) 0:ALL    1:Pages  
    '           : ARG3 - (I ) 開始ページ
    '           : ARG4 - (I ) 終了ページ
    '           : ARG5 - (I ) True:Secret Channelを含む  False:含まない
    '           : ARG6 - (I ) True:Dummy Dataを含む      False:含まない
    '           : ARG7 - (I ) True:System Noを含む       False:含まない
    '           : ARG8 - (I ) プリンター名(Print選択時)
    ' 機能説明  : 
    '----------------------------------------------------------------------------
    Friend Function gShow(ByVal intPrintMode As Integer, _
                          ByVal intPrintKubun As Integer, _
                          ByVal intPageFrom As Integer, _
                          ByVal intPageTo As Integer, _
                          ByVal flgSC As Boolean, _
                          ByVal flgDmy As Boolean, _
                          Optional ByVal strPrinterName As String = "", _
                          Optional ByVal pintGrpNo As Integer = 0) As Integer
        'Ver2.0.0.2 グループ番号を引数として取得、ただし他との兼ね合いでOptionalとする

        Try

            mintPrintMode = intPrintMode
            mintPrintAllPage = intPrintKubun
            mintPageFrom = intPageFrom
            mintPageTo = intPageTo
            mFlgSC = flgSC
            mFlgDmy = flgDmy
            mPrinterName = strPrinterName

            mPrintGrp = pintGrpNo   'Ver2.0.0.2 グループ番号取得

            Me.ShowDialog()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtChannelPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'Ver2.0.0.7 OUTch全点配列へ格納
            Call subSetAllOutCH()


            ''配列初期化
            Erase mChListPrint.udtPage

            ''配列再定義
            ReDim mChListPrint.udtPage(mCstMaxPage - 1)
            For i As Integer = 0 To UBound(mChListPrint.udtPage)

                ReDim mChListPrint.udtPage(i).udtChannelData(mPageDataMax - 1)

                For j As Integer = 0 To mPageDataMax - 1
                    ReDim mChListPrint.udtPage(i).udtChannelData(j).udtChTypeData(gCstByteCntChannelType - 1)
                Next

            Next

            ''非表示コンボ
            Call gSetComboBox(cmbControlType, gEnmComboType.ctChListChannelListOutputControlType)

            ''チャンネルグループ情報取得
            Call gMakeChannelData(gudt.SetChInfo, mudtChannelGroup)
            'Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroup)


            ''グループ毎のレコード数とページ数を取得
            Call mGetPageInfo()

            If mintPrintMode = 0 Then
                'Ver2.0.1.3
                'GroupNoが指定されていた場合は、該当グループのみ印刷させるようにページ指定
                Dim blGrp As Boolean = False
                If mPrintGrp <> 0 Then
                    mintPrintAllPage = 1
                    'Fromページ取得
                    For i = 0 To UBound(mChListPrint.udtPage) Step 1
                        With mChListPrint.udtPage(i)
                            If mPrintGrp = .intGroupNo Then
                                mintPageFrom = i + 1
                                blGrp = True
                                Exit For
                            End If
                        End With
                    Next i
                    If blGrp = True Then
                        'Toページ取得
                        For j = mintPageFrom To UBound(mChListPrint.udtPage) Step 1
                            With mChListPrint.udtPage(j)
                                If mPrintGrp <> .intGroupNo Then
                                    mintPageTo = j - 1 + 1
                                    Exit For
                                End If
                            End With
                        Next j
                    Else
                        '存在しないｸﾞﾙｰﾌﾟ指定の場合全ページ印刷となる
                        mintPrintAllPage = 0
                    End If
                End If

                ''Print
                Call cmdAllPrint_Click(cmdAllPrint, New EventArgs)
                Me.Close()

            Else
                ''Preview
                ''Preview時は全ページ指定
                If mintPrintAllPage = 0 Then
                    'Ver2.0.0.2 グループ番号が指定されている場合は、該当グループの一番最初のページ表示とする
                    Dim intGrpPage As Integer = 0
                    Dim blBFpage As Boolean = False
                    If mPrintGrp <> 0 Then
                        For i = 0 To UBound(mChListPrint.udtPage) Step 1
                            With mChListPrint.udtPage(i)
                                If mPrintGrp = .intGroupNo Then
                                    intGrpPage = i
                                    Exit For
                                End If
                            End With
                        Next i
                    End If
                    If intGrpPage > 1 Then
                        '前ページへのボタン（1より大きいなら有効）
                        blBFpage = True
                    End If

                    ''全ページ

                    'Ver2.0.0.2 グループ指定による初期表示ページ変更処理
                    'mSlectPageIndex = 1
                    mSlectPageIndex = intGrpPage + 1

                    cmdBeforePage.Enabled = blBFpage  'False
                    'Ver2.0.0.2 グループ指定による初期表示ページ変更処理
                    'txtPage.Text = "1"
                    txtPage.Text = mSlectPageIndex.ToString

                    lblPage.Text = mPageMax.ToString
                    If mPageMax <= 1 Then cmdNextPage.Enabled = False

                Else

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Next Pageボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 次のページを表示する
    '--------------------------------------------------------------------
    Private Sub cmdNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextPage.Click

        Try

            mSlectPageIndex += 1

            txtPage.Text = mSlectPageIndex.ToString

            cmdNextPage.Enabled = IIf(mSlectPageIndex = mPageMax, False, True)

            If mSlectPageIndex > 1 Then
                cmdBeforePage.Enabled = True
            End If

            picPreview.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Before Pageボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 前のページを表示する
    '--------------------------------------------------------------------
    Private Sub cmdBeforePage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeforePage.Click

        Try

            mSlectPageIndex = mSlectPageIndex - 1

            txtPage.Text = mSlectPageIndex.ToString

            cmdBeforePage.Enabled = IIf(mSlectPageIndex = 1, False, True)

            If mSlectPageIndex < mPageMax Then
                cmdNextPage.Enabled = True
            End If

            picPreview.Refresh()

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
    Private Sub frmPrtChannelPreview_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
    '--------------------------------------------------------------------
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Try

            Me.Close()

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
            Call mDrawGraphics(e.Graphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : PagesPrintボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 表示されているページを印刷する
    '--------------------------------------------------------------------
    Private Sub cmdPagesPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPagesPrint.Click

        Try

            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   ''ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                mintPrintAllPage = 1            ''ページ指定
                mintPageFrom = mSlectPageIndex  ''表示ページ(開始)
                mintPageTo = mSlectPageIndex    ''表示ページ(終了)

                'PrintDocumentオブジェクト作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True

                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''印刷設定反映
                pd.PrinterSettings = PrintDialog1.PrinterSettings

                ''余白設定
                pd.DefaultPageSettings.Margins.Top = 20
                pd.DefaultPageSettings.Margins.Left = 20
                pd.DefaultPageSettings.Margins.Right = 20
                pd.DefaultPageSettings.Margins.Bottom = 20

                ''印刷方向：横
                pd.DefaultPageSettings.Landscape = True

                '印刷を開始する
                pd.Print()

                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

            PrintDialog1.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : AllPrintボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 全てのページを印刷する
    '--------------------------------------------------------------------
    Private Sub cmdAllPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllPrint.Click

        Try

            'If MsgBox("Do you print all the pages?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "ALL Print") = MsgBoxResult.No Then Exit Sub

            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   ''ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                ''選択済みプリンター
                'strPrinterName = PrintDialog1.PrinterSettings.PrinterName

                If mintPrintAllPage = 1 Then        ''印刷範囲指定
                    '------------------
                    ''ページ指定
                    '------------------
                    mSlectPageIndex = mintPageFrom  ''ページ
                    mintPageFrom = mintPageFrom     ''表示ページ(開始)
                    mintPageTo = mintPageTo         ''表示ページ(終了)

                Else
                    '------------------
                    ''全画面印刷
                    '------------------
                    mSlectPageIndex = 1             ''ページ
                    mintPageFrom = 1                ''表示ページ(開始)
                    mintPageTo = mPageMax           ''表示ページ(終了)
                End If



                ''PrintDocumentオブジェクト作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                ''PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True

                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''印刷設定反映
                pd.PrinterSettings = PrintDialog1.PrinterSettings

                ''余白設定
                pd.DefaultPageSettings.Margins.Top = 20
                pd.DefaultPageSettings.Margins.Left = 20
                pd.DefaultPageSettings.Margins.Right = 20
                pd.DefaultPageSettings.Margins.Bottom = 20

                ''印刷方向：横
                pd.DefaultPageSettings.Landscape = True

                ''表示している画面Indexの保持
                Dim intPageBuf As Integer = mSlectPageIndex

                ''印刷開始ページの設定（1ページ目から印刷するためIndexを0にする）
                'mSlectPageIndex = 0

                ''ページ印刷
                pd.Print()

                ''画面Indexを戻す
                mSlectPageIndex = intPageBuf

            
                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

            PrintDialog1.Dispose()

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

            If mintPrintAllPage = 1 Then

                ''-----------------------
                '' ページ指定印刷
                ''-----------------------
                Call mDrawGraphics(e.Graphics)

                If (mSlectPageIndex + 1) > mintPageTo Then
                    e.HasMorePages = False
                Else
                    mSlectPageIndex += 1
                    e.HasMorePages = True
                End If

            Else

                ''-----------------------
                '' 全ページ印刷
                ''-----------------------
                Call mDrawGraphics(e.Graphics)

                If mintPrintAllPage = 0 Then

                    If (mSlectPageIndex + 1) > mPageMax Then
                        mSlectPageIndex = 1
                        e.HasMorePages = False
                    Else
                        mSlectPageIndex += 1
                        e.HasMorePages = True
                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    '--------------------------------------------------------------------
    ' 機能      : ﾍﾟｰｼﾞ番号　ｷｰ入力ﾁｪｯｸ
    ''              Ver1.8.4.1 2015.11.27
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ENTERｷｰの入力にて入力CHに移動する
    '--------------------------------------------------------------------
    Private Sub txtPage_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPage.KeyPress
        Dim tmpPage As Integer

        If e.KeyChar = Chr(13) Then     '' ENTERｷｰが押された

            If mSlectPageIndex = 0 Or IsNumeric(txtPage.Text) = False Then
                Return
            End If

            '' 入力ﾍﾟｰｼﾞ番号ﾁｪｯｸ
            tmpPage = CInt(txtPage.Text)

            If tmpPage = mSlectPageIndex Then
                Return
            End If

            If tmpPage >= mPageMax Then     '' MAX値を超えている場合
                mSlectPageIndex = mPageMax
                cmdNextPage.Enabled = False     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用不可
                cmdBeforePage.Enabled = True    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
            ElseIf tmpPage <= 1 Then        '' 最小値以下の場合
                mSlectPageIndex = 1
                cmdNextPage.Enabled = True     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                cmdBeforePage.Enabled = False    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用不可
            Else
                mSlectPageIndex = tmpPage
                cmdNextPage.Enabled = True     '' 次ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
                cmdBeforePage.Enabled = True    '' 前ﾍﾟｰｼﾞﾎﾞﾀﾝ　使用可
            End If

            picPreview.Refresh()

        End If

    End Sub

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Graph作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 印刷情報構造体
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphics(ByVal objGraphics As System.Drawing.Graphics)

        Try
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                mPrtColLen(7) = 3
                mPrtColLen(8) = 3
                mPrtColLen(12) = 3
                mPrtColLen(13) = 3
            End If


            ' 2015.10.22 Ver1.7.5  通常時とﾀｸﾞ表示時で表示ｴﾘｱを分ける
            If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then
                mPrtColLen(0) = 3
                mPrtColLen(2) = 4
            Else
                mPrtColLen(0) = 2
                mPrtColLen(2) = 5
            End If

            ''ページフレーム作成
            Call mPrtDrawCHFrame(objGraphics)

            ''ヘッダー作成
            Call mPrtDrawCHHeader(objGraphics)

            ''リスト作成
            Call mPrtDrawCHList(objGraphics)

            ''フッター作成
            Call mPrtDrawCHFooter(objGraphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ別のチャンネル数、ページ数を取得する
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '----------------------------------------------------------------------------
    Private Sub mGetPageInfo()

        Try

            Dim i As Integer, j As Integer, z As Integer
            Dim intPageCnt As Integer = 0
            Dim flgSC As Boolean = False '', flgDmy As Boolean = False
            Dim stval As Integer = 0
            Dim endval As Integer = 0
            Dim intType As Integer = 0
            Dim intPos As Integer = 0
            Dim CHflag1 As Boolean = False
            Dim CHflag2 As Boolean = False

            mPageMax = 0
            intPageCnt = 0

            'Ver2.0.4.1 グループは表示順
            'Ver2.0.4.2 グループは名づけ番号順
            Dim arySort As New ArrayList
            If g_bytChListOrder = 0 Then
                '旧フォーマット
                With mudtChannelGroup
                    For i = 0 To UBound(.udtGroup) Step 1
                        arySort.Add(i.ToString("00") & "," & i.ToString("00"))
                    Next i 
                    arySort.Sort()
                End With
            Else
                '名づけ番号順
                With gudt.SetChGroupSetM.udtGroup
                    For i = 0 To UBound(.udtGroupInfo) Step 1
                        arySort.Add(.udtGroupInfo(i).shtGroupNo.ToString("00") & "," & i.ToString("00"))
                    Next i
                    arySort.Sort()
                End With
            End If



            'Ver2.0.4.1 グループは表示順
            'Ver2.0.4.2 グループは名づけ番号順
            'For i = 0 To UBound(mudtChannelGroup.udtGroup)
            For z = 0 To arySort.Count - 1 Step 1

                'Ver2.0.4.1 グループは表示順
                'Ver2.0.4.2 グループは名づけ番号順
                Dim strData() As String = arySort(z).ToString.Split(",")
                i = CInt(strData(1))

                With mudtChannelGroup.udtGroup(i)

                    ''20CH区切りでチェック
                    For j = 0 To 4
                        stval = 0 + (20 * j)
                        endval = 19 + (20 * j)
                        intType = 0
                        intPos = 0
                        CHflag1 = False
                        CHflag2 = False

                        ''CH有無、コンポジットCH確認
                        For k = stval To endval
                            If .udtChannelData(k).udtChCommon.shtChno > 0 Then

                                flgSC = IIf(gBitCheck(.udtChannelData(k).udtChCommon.shtFlag1, 1), True, False)     ''隠しCH
                                If mFlgSC = False And flgSC = True Then
                                    ''隠しCH表示無しの設定で、当該CHが隠しCHであった
                                Else
                                    '' Ver1.11.9.3 2016.11.26 DI/DOの端子数が5以上の場合のみとする
                                    If (.udtChannelData(k).udtChCommon.shtChType = gCstCodeChTypeValve And .udtChannelData(k).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO And .udtChannelData(k).udtChCommon.shtPinNo > 4) Or _
                                       .udtChannelData(k).udtChCommon.shtChType = gCstCodeChTypeComposite Then
                                        intType = 1
                                    End If
                                    If k >= 10 + (20 * j) Then
                                        CHflag2 = True
                                    Else
                                        CHflag1 = True
                                    End If

                                End If
                            End If
                        Next

                        ''CH有り
                        If CHflag1 = True Or CHflag2 = True Then
                            ''印刷タイプにより開始位置判断
                            If intType = 0 Then     '' 通常1-20
                                mChListPrint.udtPage(intPageCnt).intListType = intType  ''印字タイプセット
                                mChListPrint.udtPage(intPageCnt).intGroupNo = i + 1     ''グループ番号セット
                                mChListPrint.udtPage(intPageCnt).intStartIndex = stval + 1 ''No開始位置
                            Else                    '' コンポジットテーブル1-10
                                If CHflag1 = True Then  ''1-10にデータ有り
                                    mChListPrint.udtPage(intPageCnt).intListType = intType  ''印字タイプセット
                                    mChListPrint.udtPage(intPageCnt).intGroupNo = i + 1     ''グループ番号セット
                                    mChListPrint.udtPage(intPageCnt).intStartIndex = stval + 1 ''No開始位置
                                End If
                            End If

                            If mChListPrint.udtPage(intPageCnt).intGroupNo = 0 Then
                                Debug.Print("ERROR")
                            End If
                            For k = stval To endval
                                If intType = 1 Then
                                    If k >= 10 + (20 * j) Then
                                        If CHflag2 = True Then  ''2ページ目データ有り
                                            intPageCnt += 1
                                            mChListPrint.udtPage(intPageCnt).intListType = intType  ''印字タイプセット
                                            mChListPrint.udtPage(intPageCnt).intGroupNo = i + 1     ''グループ番号セット
                                            mChListPrint.udtPage(intPageCnt).intStartIndex = k + 1  ''No開始位置
                                            intPos = 0
                                            intType = 0
                                        Else
                                            Exit For
                                        End If

                                        If mChListPrint.udtPage(intPageCnt).intGroupNo = 0 Then
                                            Debug.Print("ERR")
                                        End If
                                    End If
                                End If

                                If .udtChannelData(k).udtChCommon.shtChno > 0 Then

                                    flgSC = IIf(gBitCheck(.udtChannelData(k).udtChCommon.shtFlag1, 1), True, False)     ''隠しCH
                                    If mFlgSC = False And flgSC = True Then
                                        ''隠しCH表示無しの設定で、当該CHが隠しCHであった
                                    Else
                                        ''CHデータコピー
                                        mChListPrint.udtPage(intPageCnt).udtChannelData(intPos) = .udtChannelData(k)
                                    End If

                                End If
                                intPos += 1
                            Next
                            intPageCnt += 1
                        End If
                    Next

                End With

            Next z

            mPageMax = intPageCnt

            If mPageMax = 0 Then mPageMax = 1

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

#Region "日本語、英語判断"
    '--------------------------------------------------------------------
    ' 機能      : 日本語、英語判断
    ' 返り値    : 日本語:1 英語:0
    ' 引き数    : 文字列
    ' 機能説明  : 日本語か否かを全角/半角SHIFTJISかどうかでチェックする
    ' 履歴    　: 2019.02.05 倉重作成
    '--------------------------------------------------------------------
    Function isjapanese(bytes As Byte()) As Byte

        Dim i As Integer = 0    ''カウンタ

        If bytes.Length < 2 Then    ''文字列が2バイトない場合
            Return Nothing
        End If

        ''2バイト文字が含まれているか確認
        For i = 0 To (bytes.Length - 2)
            If (i Mod 2 = 0) Then
                If (bytes(i + 1) > 0) Then
                    Return 1        ''日本語判定
                End If
            End If
        Next

        Return 0                    ''2バイト文字が含まれていないので英語判定

    End Function
#End Region

#Region "ページフレーム"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ページフレーム作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '----------------------------------------------------------------------------
    Public Sub mPrtDrawCHFrame(ByVal objGraphics As System.Drawing.Graphics)

        Try
            Dim intColMax As Integer = 0
            Dim intPageDataMax As Integer = 0
            Dim intCHDataRow As Integer = 0
            Dim intCHStartRow = 0
            Dim intRowMax As Integer = 0
            Dim intColStatus As Integer = 0     '' 2013.10.25
            Dim pen As New Pen(Color.Black, 0.1F)

            Dim i As Integer = 0

            If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                ''コンポジットテーブル使用時(MAX 10CH)
                intPageDataMax = 8      ''Ch区切り線
                intCHDataRow = 6        ''行数
                intCHStartRow = 9       ''横線スタート位置
                intColStatus = 40       ''STATUS開始位置    2013.10.25
                intRowMax = 67          ''CH行数最大(外枠)
            Else
                ''通常(MAX 20CH)
                intPageDataMax = 18     ''Ch区切り線
                intCHDataRow = 3        ''行数
                intCHStartRow = 6       ''横線スタート位置
                intColStatus = 40       ''STATUS開始位置    2013.10.25
                intRowMax = 64          ''CH行数最大(外枠)
            End If

            For i = 0 To UBound(mPrtColLen)
                ''コンバイン仕様でない場合はSYS非表示
                '' Ver1.10.5 2016.05.09  ｺﾝﾊﾞｲﾝ印刷有無を関数に変更
                ''If gudt.SetSystem.udtSysSystem.shtCombineUse = 0 Then   ''コンバイン設定が無し
                If mChkCombinePrint() = False Then
                    If i = mCstColumnSYS Then   ''1
                        Continue For
                    End If
                Else
                    intColStatus = 42       ''STATUS開始位置    2013.10.25
                End If

                ''共有CH設定が無い場合はLCL/REMT非表示
                If gudt.SetSystem.udtSysFcu.shtShareChUse = 0 Then      ''共有CH設定無し
                    If i = mCstColumnShare Then ''21
                        Continue For
                    End If
                End If


                intColMax += (mPrtColLen(i) + 1)    ''列位置取得

                ''縦線(列区切り)
                objGraphics.DrawLine(Pens.Black, _
                                     mCstMarginLeft + (intColMax * mCstColumnSize), _
                                     mCstMarginUp + (2 * mCstRowSize), _
                                     mCstMarginLeft + (intColMax * mCstColumnSize), _
                                     mCstMarginUp + (2 * mCstRowSize) + intRowMax * mCstRowSize)
            Next

            ''外枠
            objGraphics.DrawRectangle(Pens.Black, mCstMarginLeft, mCstMarginUp + (2 * mCstRowSize), intColMax * mCstColumnSize, intRowMax * mCstRowSize)

            ''横線(CH区切り)
            pen.DashStyle = Drawing2D.DashStyle.Dot     ''点線

            ''項目区分け線追加(STATUS-最終) タイトル 2013.10.25
            For j = 0 To intCHDataRow - 2
                objGraphics.DrawLine(pen, _
                                     mCstMarginLeft + (intColStatus * mCstColumnSize), _
                                     mCstMarginUp + mCstRowSize * (3 + j), _
                                     mCstMarginLeft + intColMax * mCstColumnSize, _
                                     mCstMarginUp + mCstRowSize * (3 + j))
            Next

            ''横線(CH区切り)
            objGraphics.DrawLine(Pens.Black, _
                                 mCstMarginLeft, _
                                 mCstMarginUp + mCstRowSize * intCHStartRow, _
                                 mCstMarginLeft + intColMax * mCstColumnSize, _
                                 mCstMarginUp + mCstRowSize * intCHStartRow)


            For i = 0 To intPageDataMax

                ''項目区分け線追加(STATUS-最終) CH行  2013.10.25
                For j = 0 To intCHDataRow - 2
                    objGraphics.DrawLine(pen, _
                                        mCstMarginLeft + (intColStatus * mCstColumnSize), _
                                        mCstMarginUp + mCstRowSize * ((intCHStartRow + 1 + j) + i * intCHDataRow), _
                                        mCstMarginLeft + intColMax * mCstColumnSize, _
                                        mCstMarginUp + mCstRowSize * ((intCHStartRow + 1 + j) + i * intCHDataRow))
                Next

                ''CH区切り　実線に変更   2013.10.25
                'objGraphics.DrawLine(pen, _
                objGraphics.DrawLine(Pens.Black, _
                                     mCstMarginLeft, _
                                     mCstMarginUp + mCstRowSize * ((intCHStartRow + intCHDataRow) + i * intCHDataRow), _
                                     mCstMarginLeft + intColMax * mCstColumnSize, _
                                     mCstMarginUp + mCstRowSize * ((intCHStartRow + intCHDataRow) + i * intCHDataRow))
            Next

            ''項目区分け線追加(STATUS-最終) CH行最終  2013.10.25
            For j = 0 To intCHDataRow - 2
                objGraphics.DrawLine(pen, _
                                    mCstMarginLeft + (intColStatus * mCstColumnSize), _
                                    mCstMarginUp + mCstRowSize * ((intCHStartRow + 1 + j) + (intPageDataMax + 1) * intCHDataRow), _
                                    mCstMarginLeft + intColMax * mCstColumnSize, _
                                    mCstMarginUp + mCstRowSize * ((intCHStartRow + 1 + j) + (intPageDataMax + 1) * intCHDataRow))
            Next

            pen.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ヘッダー描画"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ヘッダー描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '           ： 和文と英文の判定方法を変更
    '----------------------------------------------------------------------------
    Public Sub mPrtDrawCHHeader(ByVal objGraphics As System.Drawing.Graphics)

        Try
            Dim strValue As String = ""
            Dim strLine As String = ""
            Dim strGroupName As String
            Dim intGroupNo As Integer
            Dim shtGroupNo2 As Short
            Dim intColumnPos As Integer = 0
            Dim intRowPos1 As Integer = 0
            Dim intRowPos2 As Integer = 0
            Dim intRowPos3 As Integer = 0
            Dim intRowPos4 As Integer = 0
            Dim intRowPos5 As Integer = 0
            Dim intRowPos6 As Integer = 0
            Dim JapaneseShift As Integer = 1 '20200217 和文仕様 hori


            ''グループ名称
            intGroupNo = mChListPrint.udtPage(mSlectPageIndex - 1).intGroupNo

            '文字列の作成 2019.02.05
            strGroupName = Nothing ''文字列の初期化
            strGroupName = IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName1 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName1.Trim)
            strGroupName += IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName2 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName2.Trim)
            strGroupName += IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName3 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName3.Trim)

            ''Machinery Part
            ''If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
            If isjapanese(System.Text.Encoding.Unicode.GetBytes(strGroupName)) <> 0 Then
                ''和文の場合、連結時にスペース無し  2014.04.17
                ''既に作成された文字列を使う

            Else
                ''英文の場合、連結時にスペース追加  2014.04.17
                strGroupName = Nothing ''文字列の初期化
                strGroupName = IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName1 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName1.Trim)
                strGroupName += IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName2 = Nothing, "", " " + gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName2.Trim)
                strGroupName += IIf(gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName3 = Nothing, "", " " + gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName3.Trim)
            End If

            ''Cargo Part
            'strGroupName = IIf(gudt.SetChGroupSetC.udtGroup.udtGroupInfo(intGroupNo - 1).strName1 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName1.Trim)
            'strGroupName += IIf(gudt.SetChGroupSetC.udtGroup.udtGroupInfo(intGroupNo - 1).strName2 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName2.Trim)
            'strGroupName += IIf(gudt.SetChGroupSetC.udtGroup.udtGroupInfo(intGroupNo - 1).strName3 = Nothing, "", gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).strName3.Trim)


            ''ヘッダー作成(グループ名称)
            '項目名を和文にしたものを追加 20200217 hori 
            'フォントサイズ：gFnt8→gFnt8jに変更
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 20200306 hori

                shtGroupNo2 = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).shtGroupNo  '' 設定されたグループ番号に変更   2015.01.26
                strLine = "ｸﾞﾙｰﾌﾟ番号 : " & shtGroupNo2.ToString("00") & vbTab & vbTab ''Tab追加 20200511 hori
                'strValue = strGroupName(intGroupNo - 1, 0) & IIf(strGroupName(intGroupNo - 1, 1) = "", "", " / " & strGroupName(intGroupNo - 1, 1))
                strLine += " ｸﾞﾙｰﾌﾟ名称 : " & strGroupName

                objGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, mCstMarginLeft, mCstMarginUp)

                intRowPos1 = 2      ''1行目ヘッダー位置
                intRowPos2 = 3      ''2行目ヘッダー位置
                intRowPos3 = 4      ''3行目ヘッダー位置
                intRowPos4 = 5      ''1行目ヘッダー位置
                intRowPos5 = 6      ''2行目ヘッダー位置
                intRowPos6 = 7      ''3行目ヘッダー位置

                intColumnPos = 0    ''列項目位置

                objGraphics.DrawString("番号", gFnt7j, gFntColorBlack, mCstMarginLeft, (mCstMarginUp + JapaneseShift) + mCstRowSize * 2)
                intColumnPos += (mPrtColLen(0) + 1)

                '' Ver1.10.5 2016.05.09  ｺﾝﾊﾞｲﾝ印刷有無を関数に変更
                ''If gudt.SetSystem.udtSysSystem.shtCombineUse = 1 Then    ''コンバイン設定が有り
                If mChkCombinePrint() = True Then
                    objGraphics.DrawString("S", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("Y", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("S", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                    intColumnPos += (mPrtColLen(1) + 1)
                End If

                ' 2015.10.16 ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  標準時とﾀｸﾞ表示時にて分ける
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString("CH番号", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                Else        ' ﾀｸﾞ表示
                    objGraphics.DrawString("TAG番号", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                End If
                intColumnPos += (mPrtColLen(2) + 1)

                objGraphics.DrawString("名称", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(3) + 1)

                objGraphics.DrawString("状態表示", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                objGraphics.DrawString("計測範囲", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(4) + 1)

                objGraphics.DrawString("単位", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(5) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("警報 高/1", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("警報 低/3", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("警報   5", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                    objGraphics.DrawString("警報   7", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos4)
                    objGraphics.DrawString("警報   9", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos5)
                    objGraphics.DrawString("ﾌｨｰﾄﾞﾊﾞｯｸ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos6)
                Else
                    objGraphics.DrawString("上限設定", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("下限設定", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("ﾌｨｰﾄﾞﾊﾞｯｸ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                End If
                intColumnPos += (mPrtColLen(6) + 1)

                objGraphics.DrawString("警報", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(7) + 1)

                objGraphics.DrawString("ﾀｲﾏ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(8) + 1)

                objGraphics.DrawString("休1", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(9) + 1)

                objGraphics.DrawString("休2", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(10) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("警報 高高/2", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("警報 低低/4", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("警報   6", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                    objGraphics.DrawString("警報   8", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos4)
                Else
                    objGraphics.DrawString("上上限設定", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("下下限設定", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                End If

                intColumnPos += (mPrtColLen(11) + 1)

                objGraphics.DrawString("警報", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(12) + 1)

                objGraphics.DrawString("ﾀｲﾏ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(13) + 1)

                objGraphics.DrawString("休1", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(14) + 1)

                objGraphics.DrawString("休2", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(15) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("入力", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("出力", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos6)
                Else
                    objGraphics.DrawString("入力", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("出力", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                End If

                intColumnPos += (mPrtColLen(16) + 1)

                objGraphics.DrawString("S", gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(17) + 1)

                objGraphics.DrawString("ﾌｨｰﾙﾄﾞｱﾄﾞﾚｽ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(18) + 1)

                objGraphics.DrawString("AL", gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                objGraphics.DrawString("RL", gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)

                'Ver2.0.0.6 Output設定は、設定ファイルでONにしていないと印刷しない
                If g_bytOutputPrint = 1 Then
                    'Ver2.0.0.4 Output設定対応
                    objGraphics.DrawString("OP", gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                End If


                intColumnPos += (mPrtColLen(19) + 1)

                objGraphics.DrawString("ｾﾝｻ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                objGraphics.DrawString("警報", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos2)
                objGraphics.DrawString("ﾀｲﾏ", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(20) + 1)

                If gudt.SetSystem.udtSysFcu.shtShareChUse = 1 Then      ''共有CH設定有り
                    objGraphics.DrawString("共有", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)
                    intColumnPos += (mPrtColLen(21) + 1)
                End If

                objGraphics.DrawString("備考", gFnt7j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, (mCstMarginUp + JapaneseShift) + mCstRowSize * intRowPos1)


            Else    '英文
                shtGroupNo2 = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(intGroupNo - 1).shtGroupNo  '' 設定されたグループ番号に変更   2015.01.26
                strLine = "GROUP NO. : " & shtGroupNo2.ToString("00") & vbTab
                'strValue = strGroupName(intGroupNo - 1, 0) & IIf(strGroupName(intGroupNo - 1, 1) = "", "", " / " & strGroupName(intGroupNo - 1, 1))
                strLine += "GROUP NAME : " & strGroupName

                If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                    objGraphics.DrawString(strLine, gFnt8j, gFntColorBlack, mCstMarginLeft, mCstMarginUp)
                Else
                    objGraphics.DrawString(strLine, gFnt8, gFntColorBlack, mCstMarginLeft, mCstMarginUp)
                End If


                '' ヘッダー(コンバイン仕様)
                ''"NO  S CHNO ITEM NAME                      STATUS            UNIT     ALM H    EX DLY  R1 R2 ALM HH   EX DLY  R1 R2 IN  S FU ADD    AL SEN  LCL/ REMARKS          "
                ''"    Y                                                                ALM L                  ALM LL                                 RL EX   REMT                  "
                ''"    S                                     RANGE                      FEEDBACK                                      OUT                DLY  CHNO                  "
                ''"---+-+----+------------------------------+-----------------+--------+--------+--+----+--+--+--------+--+----+--+--+---+-+---------+--+----+----+----------------+" ''161

                '' ヘッダー(標準)
                ''"NO  CHNO ITEM NAME                      STATUS            UNIT     ALM H    EX DLY  R1 R2 ALM HH   EX DLY  R1 R2 IN  S FU ADD    AL SEN  REMARKS          "
                ''"                                                                   ALM L                  ALM LL                                 RL EX                    "
                ''"                                        RANGE                      FEEDBACK                                      OUT                DLY                   "
                ''"---+----+------------------------------+-----------------+--------+--------+--+----+--+--+--------+--+----+--+--+---+-+---------+--+----+----------------+" ''154

                intRowPos1 = 2      ''1行目ヘッダー位置
                intRowPos2 = 3      ''2行目ヘッダー位置
                intRowPos3 = 4      ''3行目ヘッダー位置
                intRowPos4 = 5      ''1行目ヘッダー位置
                intRowPos5 = 6      ''2行目ヘッダー位置
                intRowPos6 = 7      ''3行目ヘッダー位置

                intColumnPos = 0    ''列項目位置

                objGraphics.DrawString("NO", gFnt8, gFntColorBlack, mCstMarginLeft, mCstMarginUp + mCstRowSize * 2)
                intColumnPos += (mPrtColLen(0) + 1)

                '' Ver1.10.5 2016.05.09  ｺﾝﾊﾞｲﾝ印刷有無を関数に変更
                ''If gudt.SetSystem.udtSysSystem.shtCombineUse = 1 Then    ''コンバイン設定が有り
                If mChkCombinePrint() = True Then
                    objGraphics.DrawString("S", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("Y", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("S", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                    intColumnPos += (mPrtColLen(1) + 1)
                End If

                ' 2015.10.16 ﾀｸﾞ表示
                ' 2015.10.22 Ver1.7.5  標準時とﾀｸﾞ表示時にて分ける
                If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                    objGraphics.DrawString("CHNO", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                Else        ' ﾀｸﾞ表示
                    objGraphics.DrawString("TAGNO", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                End If
                intColumnPos += (mPrtColLen(2) + 1)

                objGraphics.DrawString("ITEM NAME", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(3) + 1)

                objGraphics.DrawString("STATUS", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                objGraphics.DrawString("RANGE", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(4) + 1)

                objGraphics.DrawString("UNIT", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(5) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("ALM H /1", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("ALM L /3", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("ALM    5", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                    objGraphics.DrawString("ALM    7", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos4)
                    objGraphics.DrawString("ALM    9", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos5)
                    objGraphics.DrawString("FEEDBACK", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos6)
                Else
                    objGraphics.DrawString("ALM H", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("ALM L", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("FEEDBACK", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                End If
                intColumnPos += (mPrtColLen(6) + 1)

                objGraphics.DrawString("EX", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(7) + 1)

                objGraphics.DrawString("DLY", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(8) + 1)

                objGraphics.DrawString("R1", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(9) + 1)

                objGraphics.DrawString("R2", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(10) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("ALM HH/2", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("ALM LL/4", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                    objGraphics.DrawString("ALM    6", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                    objGraphics.DrawString("ALM    8", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos4)
                Else
                    objGraphics.DrawString("ALM HH", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("ALM LL", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                End If
                intColumnPos += (mPrtColLen(11) + 1)

                objGraphics.DrawString("EX", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(12) + 1)

                objGraphics.DrawString("DLY", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(13) + 1)

                objGraphics.DrawString("R1", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(14) + 1)

                objGraphics.DrawString("R2", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(15) + 1)

                If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                    objGraphics.DrawString("IN", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("OUT", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos6)
                Else
                    objGraphics.DrawString("IN", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    objGraphics.DrawString("OUT", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                End If

                intColumnPos += (mPrtColLen(16) + 1)

                objGraphics.DrawString("S", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(17) + 1)

                objGraphics.DrawString("FU ADD", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                intColumnPos += (mPrtColLen(18) + 1)

                objGraphics.DrawString("AL", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                objGraphics.DrawString("RL", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)

                'Ver2.0.0.6 Output設定は、設定ファイルでONにしていないと印刷しない
                If g_bytOutputPrint = 1 Then
                    'Ver2.0.0.4 Output設定対応
                    objGraphics.DrawString("OP", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                End If


                intColumnPos += (mPrtColLen(19) + 1)

                objGraphics.DrawString("SEN", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                objGraphics.DrawString("EX", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                objGraphics.DrawString("DLY", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                intColumnPos += (mPrtColLen(20) + 1)

                '' ■Share対応のため変更 土田  2016.10.24
                If gudt.SetSystem.udtSysFcu.shtShareChUse = 1 Then      ''共有CH設定有り
                    ''objGraphics.DrawString("LCL/", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    ''objGraphics.DrawString("REMT", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos2)
                    ''objGraphics.DrawString("CHNO", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos3)
                    objGraphics.DrawString("SHA", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)
                    intColumnPos += (mPrtColLen(21) + 1)
                End If


                objGraphics.DrawString("REMARKS", gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * intRowPos1)

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "リスト描画"

    '----------------------------------------------------------------------------
    ' 機能説明  ： リスト描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '----------------------------------------------------------------------------
    Public Sub mPrtDrawCHList(ByVal objGraphics As System.Drawing.Graphics)

        Try
            Dim CHStr As mChannelStr = Nothing
            Dim strline As String = ""
            Dim intPageDataMax As Integer = 0
            Dim intColumnPos As Integer = 0
            Dim intRowPos1 As Integer = 0
            Dim intRowPos2 As Integer = 0
            Dim intRowPos3 As Integer = 0
            Dim intRowPos6 As Integer = 0
            Dim intCHDataRow As Integer = 0
            Dim intStPos As Integer = 0
            Dim strStatus1 As String = ""
            Dim strStatus2 As String = ""
            Dim strArray() As String
            Dim i As Integer = 0
            Dim j As Integer = 0

            ''配列初期化
            Erase CHStr.AlmInf

            ''配列再定義
            ReDim CHStr.AlmInf(9)

            If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                ''コンポジットテーブル使用時(MAX 10CH)
                intPageDataMax = 9  ''MAX10行
                intCHDataRow = 6        ''行数
                intRowPos1 = 9      ''1行目ヘッダー位置
                intRowPos2 = 10     ''2行目ヘッダー位置
                intRowPos3 = 11     ''3行目ヘッダー位置
                intRowPos6 = 14     ''6行目ヘッダー位置
            Else
                ''通常(MAX 20CH)
                intPageDataMax = 19 ''MAX20行
                intCHDataRow = 3        ''行数
                intRowPos1 = 6      ''1行目ヘッダー位置
                intRowPos2 = 7      ''2行目ヘッダー位置
                intRowPos3 = 8      ''3行目ヘッダー位置
            End If


            For i = 0 To intPageDataMax
                With mChListPrint.udtPage(mSlectPageIndex - 1)

                    ''初期化
                    CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
                    CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
                    For k As Integer = 0 To 9
                        CHStr.AlmInf(k).Value = ""
                        CHStr.AlmInf(k).ExtGrp = ""
                        CHStr.AlmInf(k).Delay = ""
                        CHStr.AlmInf(k).GrpRep1 = ""
                        CHStr.AlmInf(k).GrpRep2 = ""
                    Next
                    CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
                    CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
                    CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
                    CHStr.Remarks = ""
                    CHStr.AlmLevel = ""     '' Ver1.7.8 2015.11.12　　ﾛｲﾄﾞ対応表示追加
                    CHStr.CHNo_temp = ""    '' Ver1.8.5.2  2015.12.02   ﾀｸﾞ表示時補助用変数追加
                    CHStr.OUT = ""          '' Ver2.0.0.4 Output設定があるときに「o」を表記

                    ''CH表示文字列取得
                    Call mMakeDrawCHData(.udtChannelData(i), CHStr)


                    strline = ""
                    intColumnPos = 0

                    ''NO
                    objGraphics.DrawString((.intStartIndex + i).ToString, gFnt8, gFntColorBlack, mCstMarginLeft, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                    intColumnPos += (mPrtColLen(0) + 1)

                    If Not .udtChannelData(i).udtChCommon.shtChno = 0 Then
                        ''SYS NO
                        ' Ver1.10.5 2016.05.09  ｺﾝﾊﾞｲﾝ印刷有無を関数に変更
                        ''If gudt.SetSystem.udtSysSystem.shtCombineUse = 1 Then    ''コンバイン設定が有り   2013.10.25
                        If mChkCombinePrint() = True Then
                            objGraphics.DrawString(CHStr.SYSNo, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(1) + 1)
                        End If

                        ''CHNO      ' 2015.10.16 ﾀｸﾞ表示向けにﾌｫﾝﾄｻｲｽﾞ変更
                        ' 2015.10.22 Ver1.7.5 標準時とﾀｸﾞ表示時にて分ける
                        If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準
                            objGraphics.DrawString(CHStr.CHNo, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        Else
                            objGraphics.DrawString(CHStr.CHNo, gFnt7, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))

                            '' Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時に補助用にCHNo.を印字するﾌﾗｸﾞを追加
                            If mCHNoFg = True Then
                                objGraphics.DrawString("(" & CHStr.CHNo_temp & ")", gFnt7, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            End If

                        End If
                        intColumnPos += (mPrtColLen(2) + 1)

                        ''ITEM NAME     2014.05.19
                        'Ver2.0.4.2
                        'ITEM_NAME は、ｼｽﾃﾑが英文であっても2バイト文字が含まれてる場合は
                        '和文として出す。
                        Dim blWaEi As Boolean = False
                        Dim byte_data As Byte() = System.Text.Encoding.GetEncoding(932).GetBytes(CHStr.CHItem)
                        If (byte_data.Length = CHStr.CHItem.Length) Then
                        Else
                            '2バイト文字が含まれる
                            blWaEi = True
                        End If
                        If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then
                            'システム和文なら無条件和文
                            blWaEi = True
                        End If
                        'If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合、名称を下げて印字 2014.04.28
                        If blWaEi = True Then
                            objGraphics.DrawString(CHStr.CHItem, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow) + 2)
                        Else
                            objGraphics.DrawString(CHStr.CHItem, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        End If
                        intColumnPos += (mPrtColLen(3) + 1)

                        ''STATUS
                        Dim blStatusWaEi As Boolean = False
                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then
                            '保安ﾓｰﾄﾞ及び全和文なら無条件和文
                            blStatusWaEi = True
                        End If

                        '' Ver1.11.9.3 2016.11.26 DI/DOは端子数により設定を変更
                        If CHStr.INSIG = "DC2" Or (CHStr.INSIG = "DC3" And CHStr.TermCount > 4) Then   ''デジタルコンポジットテーブル使用
                            ''STATUS毎に表示
                            strArray = Split(CHStr.Status, "/")
                            For j = 0 To 3
                                strStatus1 = ""
                                If strArray(j * 2) <> "" Then
                                    strStatus1 = strArray(j * 2)
                                End If
                                If strArray((j * 2) + 1) <> "" Then
                                    strStatus1 += "/" & strArray((j * 2) + 1)
                                End If
                                'Ver2.0.7.L
                                If blStatusWaEi = True Then
                                    objGraphics.DrawString(strStatus1, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                                Else
                                    objGraphics.DrawString(strStatus1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                                End If
                            Next
                            objGraphics.DrawString(strArray(8), gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + 4) + i * intCHDataRow))

                            intColumnPos += (mPrtColLen(4) + 1)

                        Else
                            If CHStr.Status.Length <= mPrtColLen(4) Then
                                ''1行表示
                                'Ver2.0.7.L
                                If blStatusWaEi = True Then
                                    objGraphics.DrawString(CHStr.Status, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                Else
                                    objGraphics.DrawString(CHStr.Status, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                End If
                            Else
                                ''2行に分割
                                intStPos = CHStr.Status.Length
                                For j = 0 To 3
                                    intStPos = InStrRev(CHStr.Status, "/", intStPos - 1)
                                    If intStPos <= mPrtColLen(4) Then
                                        strStatus2 = Mid(CHStr.Status, intStPos + 1, CHStr.Status.Length - intStPos)
                                        strStatus1 = CHStr.Status.Remove(intStPos)
                                        Exit For
                                    End If
                                Next

                                'Ver2.0.7.L
                                If blStatusWaEi = True Then
                                    objGraphics.DrawString(strStatus1, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                    objGraphics.DrawString(strStatus2, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                                Else
                                    objGraphics.DrawString(strStatus1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                    objGraphics.DrawString(strStatus2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                                End If


                            End If

                            ''RANGE
                            objGraphics.DrawString(CHStr.Range, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(4) + 1)
                        End If

                        ''UNIT
                        objGraphics.DrawString(CHStr.Unit, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                        intColumnPos += (mPrtColLen(5) + 1)

                        '' Ver1.11.9.3 2016.11.26 DI/DOは端子数により設定を変更
                        'If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                        If CHStr.INSIG = "DC2" Or (CHStr.INSIG = "DC3" And CHStr.TermCount > 4) Then   ''デジタルコンポジットテーブル使用
                            ''VALUE
                            For j = 0 To 4
                                objGraphics.DrawString(CHStr.AlmInf(j * 2).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            objGraphics.DrawString(CHStr.AlmInf(9).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(6) + 1)

                            ''EXT GROUP
                            For j = 0 To 4
                                objGraphics.DrawString(CHStr.AlmInf(j * 2).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            objGraphics.DrawString(CHStr.AlmInf(9).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(7) + 1)

                            ''DELAY
                            For j = 0 To 4
                                objGraphics.DrawString(CHStr.AlmInf(j * 2).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            objGraphics.DrawString(CHStr.AlmInf(9).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(8) + 1)

                            ''G.REP1
                            For j = 0 To 4
                                objGraphics.DrawString(CHStr.AlmInf(j * 2).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            objGraphics.DrawString(CHStr.AlmInf(9).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(9) + 1)

                            ''G.REP2
                            For j = 0 To 4
                                objGraphics.DrawString(CHStr.AlmInf(j * 2).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            objGraphics.DrawString(CHStr.AlmInf(9).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(10) + 1)

                            ''VALUE
                            For j = 0 To 3
                                objGraphics.DrawString(CHStr.AlmInf((j * 2) + 1).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            intColumnPos += (mPrtColLen(11) + 1)

                            ''EXT GROUP
                            For j = 0 To 3
                                objGraphics.DrawString(CHStr.AlmInf((j * 2) + 1).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            intColumnPos += (mPrtColLen(12) + 1)

                            ''DELAY
                            For j = 0 To 3
                                objGraphics.DrawString(CHStr.AlmInf((j * 2) + 1).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            intColumnPos += (mPrtColLen(13) + 1)

                            ''G.REP1
                            For j = 0 To 3
                                objGraphics.DrawString(CHStr.AlmInf((j * 2) + 1).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            intColumnPos += (mPrtColLen(14) + 1)

                            ''G.REP2
                            For j = 0 To 3
                                objGraphics.DrawString(CHStr.AlmInf((j * 2) + 1).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * ((intRowPos1 + j) + i * intCHDataRow))
                            Next j
                            intColumnPos += (mPrtColLen(15) + 1)

                            ''IN/OUT SIGNAL
                            If g_bytChListINSGprint = 1 Then    'Ver2.0.8.N R,W,J,SのINSGを印字しない
                                If CHStr.SIGType = "R" Or CHStr.SIGType = "W" Or CHStr.SIGType = "J" Or CHStr.SIGType = "S" Then
                                    intColumnPos += (mPrtColLen(16) + 1)
                                Else
                                    objGraphics.DrawString(CHStr.INSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                    objGraphics.DrawString(CHStr.OUTSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                                    intColumnPos += (mPrtColLen(16) + 1)
                                End If
                            Else
                                objGraphics.DrawString(CHStr.INSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                objGraphics.DrawString(CHStr.OUTSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                                intColumnPos += (mPrtColLen(16) + 1)
                            End If

                            ''S
                            objGraphics.DrawString(CHStr.SIGType, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(17) + 1)

                            ''FU ADD
                            objGraphics.DrawString(CHStr.INAdd, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.OUTAdd, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos6 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(18) + 1)

                        Else

                            ''VALUE
                            objGraphics.DrawString(CHStr.AlmInf(1).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(2).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(9).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(6) + 1)

                            ''EXT GROUP
                            objGraphics.DrawString(CHStr.AlmInf(1).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(2).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(9).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(7) + 1)

                            ''DELAY
                            objGraphics.DrawString(CHStr.AlmInf(1).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(2).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(9).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(8) + 1)

                            ''G.REP1
                            objGraphics.DrawString(CHStr.AlmInf(1).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(2).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(9).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(9) + 1)

                            ''G.REP2
                            objGraphics.DrawString(CHStr.AlmInf(1).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(2).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(9).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(10) + 1)

                            ''VALUE
                            objGraphics.DrawString(CHStr.AlmInf(0).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(3).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(11) + 1)

                            ''EXT GROUP
                            objGraphics.DrawString(CHStr.AlmInf(0).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(3).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(12) + 1)

                            ''DELAY
                            objGraphics.DrawString(CHStr.AlmInf(0).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(3).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(13) + 1)

                            ''G.REP1
                            objGraphics.DrawString(CHStr.AlmInf(0).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(3).GrpRep1, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(14) + 1)

                            ''G.REP2
                            objGraphics.DrawString(CHStr.AlmInf(0).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            objGraphics.DrawString(CHStr.AlmInf(3).GrpRep2, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(15) + 1)

                            ''IN/OUT SIGNAL
                            If g_bytChListINSGprint = 1 Then    'Ver2.0.8.N R,W,J,SのINSGを印字しない
                                If CHStr.SIGType = "R" Or CHStr.SIGType = "W" Or CHStr.SIGType = "J" Or CHStr.SIGType = "S" Then
                                    intColumnPos += (mPrtColLen(16) + 1)
                                Else
                                    objGraphics.DrawString(CHStr.INSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                    objGraphics.DrawString(CHStr.OUTSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                                    intColumnPos += (mPrtColLen(16) + 1)
                                End If
                            Else
                                objGraphics.DrawString(CHStr.INSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                objGraphics.DrawString(CHStr.OUTSIG, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                                intColumnPos += (mPrtColLen(16) + 1)
                            End If
                            

                            ''S
                            objGraphics.DrawString(CHStr.SIGType, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            intColumnPos += (mPrtColLen(17) + 1)

                            ''FU ADD
                            If CHStr.Digit_flg = True And CHStr.INAdd = "JACOM55-" Then
                                objGraphics.DrawString(CHStr.INAdd, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                objGraphics.DrawString(CHStr.INAdd2.PadLeft(10, " "), gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))


                            Else
                                objGraphics.DrawString(CHStr.INAdd, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))



                            End If
                            objGraphics.DrawString(CHStr.OUTAdd, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow)) ' 2019/04/15
                            intColumnPos += (mPrtColLen(18) + 1)
                        End If

                        ''AL/RL
                        objGraphics.DrawString(CHStr.AL, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        objGraphics.DrawString(CHStr.RL, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                        'Ver2.0.0.6 Output設定は、設定ファイルでONにしていないと印刷しない
                        If g_bytOutputPrint = 1 Then
                            'Ver2.0.0.4 Output設定
                            objGraphics.DrawString(CHStr.OUT, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                        End If
                        intColumnPos += (mPrtColLen(19) + 1)

                        ''SENSOR
                        objGraphics.DrawString(CHStr.AlmInf(4).Value, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        objGraphics.DrawString(CHStr.AlmInf(4).ExtGrp, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                        objGraphics.DrawString(CHStr.AlmInf(4).Delay, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos3 + i * intCHDataRow))
                        intColumnPos += (mPrtColLen(20) + 1)

                        ''共有CH
                        If gudt.SetSystem.udtSysFcu.shtShareChUse = 1 Then      ''共有CH設定有り
                            '■Share対応のため変更  土田  2016.10.24
                            'Ver1.12.0.4 2017.02.01 CHno表示するのを「S」Shareから「R」Remoteへ変更(FU Adrが表示されない方=Remote)
                            'If CHStr.ShareType = "S" Then
                            If CHStr.ShareType = "R" Then
                                If CHStr.ShareCHNo = "0000" Then    '' CHNo.0 ならば現在のCHNo.を表示
                                    objGraphics.DrawString(CHStr.CHNo, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                Else        '' 入力されていればそのCHNo.を表示
                                    objGraphics.DrawString(CHStr.ShareCHNo, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                                End If
                            End If
                            intColumnPos += (mPrtColLen(21) + 1)
                            ''objGraphics.DrawString(CHStr.ShareType, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                            ' ''■Share対応 「0000」は非表示
                            ''If CHStr.ShareCHNo <> "0000" Then
                            ''    objGraphics.DrawString(CHStr.ShareCHNo, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                            ''End If
                            ''intColumnPos += (mPrtColLen(21) + 1)
                        End If

                        ''REMARKS
                        If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                            objGraphics.DrawString(CHStr.Remarks, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        Else
                            objGraphics.DrawString(CHStr.Remarks, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos1 + i * intCHDataRow))
                        End If

                        ''ﾛｲﾄﾞ表示　ｱﾗｰﾑﾚﾍﾞﾙ追加     2015.11.12 Ver1.7.8
                        If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                            objGraphics.DrawString(CHStr.AlmLevel, gFnt8j, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                        Else
                            objGraphics.DrawString(CHStr.AlmLevel, gFnt8, gFntColorBlack, mCstMarginLeft + mCstColumnSize * intColumnPos, mCstMarginUp + mCstRowSize * (intRowPos2 + i * intCHDataRow))
                        End If
                        '//

                        intColumnPos += (mPrtColLen(22) + 1)

                    End If
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "フッター描画"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フッター描画
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    ' 履歴      ： 新規作成　ver.1.4.0 2011.10.13
    '----------------------------------------------------------------------------
    Public Sub mPrtDrawCHFooter(ByVal objGraphics As System.Drawing.Graphics)

        Try
            Dim strLine As String = ""
            Dim strShipNo As String = ""
            Dim strDrawNo As String = ""
            Dim intRowMax As Integer = 0

            If mChListPrint.udtPage(mSlectPageIndex - 1).intListType = 1 Then   ''コンポジットテーブル表示
                ''コンポジットテーブル使用時(MAX 10CH)
                intRowMax = 70          ''CH行数最大
            Else
                ''通常(MAX 20CH)
                intRowMax = 67          ''CH行数最大
            End If

            ''グループ情報GET（ヘッダー、フッター用）
            ''船番(MACHINERY,CARGO共通につきMACHINERYのデータ参照)
            strShipNo = IIf(gudt.SetChGroupSetM.udtGroup.strShipNo = Nothing, "", gudt.SetChGroupSetM.udtGroup.strShipNo.Trim)  ''Machinery
            'Ver2.0.4.9「^」は消す
            strShipNo = strShipNo.Replace("^", "")
            strShipNo = gGetString(strShipNo)

            ''Draw No.(MACHINERY,CARGO共通につきMACHINERYのデータ参照)
            strDrawNo = IIf(gudt.SetChGroupSetM.udtGroup.strDrawNo = Nothing, "", gudt.SetChGroupSetM.udtGroup.strDrawNo.Trim)        ''Machinery
            strDrawNo = gGetString(strDrawNo)

            'フッターセット
            '' 2015.11.13  Ver1.7.8  印刷日時ではなく、ﾌｧｲﾙ日時を印刷
            ''strLine = DateTime.Now.ToString("yyyy/MM/dd HH:mm") & vbTab
            '' Ver1.9.2 2015.12.24  ｺﾝﾊﾞｰﾄしたﾃﾞｰﾀを印刷すると、ﾌｧｲﾙ日時が入っていないのでｴﾗｰが発生する不具合修正
            If gudt.SetChDisp.udtHeader.strDate = "" Then
                strLine = DateTime.Now.ToString("yyyy/MM/dd HH:mm") & vbTab
            Else
                strLine = "'" & gudt.SetChDisp.udtHeader.strDate.Substring(2, 2) & "/" & _
                        gudt.SetChDisp.udtHeader.strDate.Substring(4, 2) & "/" & _
                        gudt.SetChDisp.udtHeader.strDate.Substring(6, 2) & " " & _
                        gudt.SetChDisp.udtHeader.strTime.Substring(0, 2) & ":" & gudt.SetChDisp.udtHeader.strTime.Substring(2, 2) & vbTab
            End If

            ''/
            strLine += "S.No.: " & strShipNo & "".PadRight(83 - strShipNo.Length) & vbTab

            'T.Ueki 表示変更
            strLine += "[" & gudtFileInfo.strFileName & "]" & vbTab
            'strLine += "[" & gudtFileInfo.strFileName & "-" & gudtFileInfo.strFileVersion & "]" & vbTab

            '' DrawNo変更 2013.10.18
            ''strLine += "DRAW No.: " & strDrawNo & "-" & mSlectPageIndex.ToString
            strLine += "DRAW No.: " & strDrawNo & "-" & "DL" & mSlectPageIndex.ToString("000")

            objGraphics.DrawString(strLine, gFnt8, gFntColorBlack, mCstMarginLeft, mCstMarginUp + mCstRowSize * intRowMax)  ''20200513 hori 全和文仕様表示不具合のため復旧
            
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "CHデータ文字列作成"
    '--------------------------------------------------------------------
    ' 機能      : CH表示データ作成
    ' 返り値    : 文字列
    ' 引き数    : ARG1 - (I ) CHデータ
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    ' 履歴      : 新規作成　ver.1.4.0 2011.10.13
    '--------------------------------------------------------------------
    Private Sub mMakeDrawCHData(ByVal udtChannel As gTypSetChRec, ByRef CHStr As mChannelStr)

        Try
            Dim strTemp As String = ""
            Dim intSpace As Integer = 0
            Dim intDecimalP As Integer = 0
            Dim strDecimalFormat As String = ""
            Dim dblValue As Double = 0
            Dim dblLowValue As Double = 0
            Dim dblHiValue As Double = 0
            Dim strHH As String = ""
            Dim strH As String = ""
            Dim strL As String = ""
            Dim strLL As String = ""
            Dim intLen As Integer = 0
            Dim intCompIndex As Integer = 0
            Dim intStatusExist As Integer = 0
            Dim nLen As Integer = 0     '' Vver1.8.1 2015.11.18
            Dim nCenter As Integer = 1  '' Ver1.10.1 2016.02.29 力率表示対応

            ''初期化
            CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
            CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
            For k As Integer = 0 To 9
                CHStr.AlmInf(k).Value = ""
                CHStr.AlmInf(k).ExtGrp = ""
                CHStr.AlmInf(k).Delay = ""
                CHStr.AlmInf(k).GrpRep1 = ""
                CHStr.AlmInf(k).GrpRep2 = ""
            Next
            CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
            CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
            CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
            CHStr.Remarks = ""
            CHStr.AlmLevel = ""     '' Ver1.7.8 2015.11.12 　ﾛｲﾄﾞ対応表示追加
            CHStr.CHNo_temp = ""    '' Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時補助用変数追加
            CHStr.TermCount = 0     '' Ver1.11.9.3 2016.11.26 追加
            CHStr.OUT = ""          '' Ver2.0.0.4 Output設定がある場合「o」表記

            With udtChannel

                If .udtChCommon.shtChno = 0 Then    ''CH番号無し
                    Return
                End If

                ''☆Common---------------------------------------------------------------------------------------
                ''SYS
                CHStr.SYSNo = .udtChCommon.shtSysno.ToString.Trim

                ''CHNo
                CHStr.CHNo = gGet2Byte(.udtChCommon.shtChno).ToString("0000")
                CHStr.CHNo_temp = CHStr.CHNo    '' Ver1.8.5.2  2015.12.02 ﾀｸﾞ表示時補助用変数追加

                ''ITEM NAME
                ''アイテム名称取得
                strTemp = gGetString(.udtChCommon.strChitem)

                ''CH名称の全角対応
                ''PadLeft/PadRightでは全角も1文字換算なので、必要数分スペースで埋める
                If LenB(strTemp) <= mCstItemNameLength Then
                    intSpace = mCstItemNameLength - LenB(strTemp)
                    CHStr.CHItem = strTemp & New String(" "c, intSpace)
                End If

                If .udtChCommon.shtChno = 2001 Then
                    Dim DebugA As Integer = 0
                End If

                ''UNIT
                If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                    'Ver2.0.1.6 ■ﾃﾞｼﾞﾀﾙとモーターは「*」は表示無し
                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeDigital, gCstCodeChTypeMotor
                            If .udtChCommon.shtUnit = 0 Then
                                '「*」
                                CHStr.Unit = ""
                            Else
                                Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                CHStr.Unit = cmbUnit.Text
                            End If
                        Case Else
                            '上記以外は「*」を表示
                            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                            cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                            CHStr.Unit = cmbUnit.Text
                    End Select
                    '単位無し時は表示無し     2013.07.23  K.Fujimoto
                    'Ver2.0.1.5 ■■0=*は表示する■■
                    'If .udtChCommon.shtUnit = 0 Then
                    'CHStr.Unit = ""
                    'Else
                    'コンボボックス
                    'Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                    'cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                    'CHStr.Unit = cmbUnit.Text
                    'End If
                Else
                    CHStr.Unit = gGetString(.udtChCommon.strUnit)         ''特殊コード対応
                End If
                ''Cを℃に変換
                If CHStr.Unit = "C" Then
                    CHStr.Unit = "ﾟC"
                End If

                ''FU ADDRESS(IN)
                CHStr.INAdd = mPrtConvFuAddress(.udtChCommon.shtFuno, .udtChCommon.shtPortno, .udtChCommon.shtPin)

                ' 2015.09.15 M.Kaihara
                ' FUアドレスが不定値の際、リスト印字すると65535の数値が表示（印字）される不具合を修正。
                ' FUアドレス不定値(0xFFFF)の際はFUアドレス表示文字を消す。
                If .udtChCommon.shtPortno = &HFFFF Or .udtChCommon.shtPin = &HFFFF Then
                    CHStr.INAdd = ""
                End If

                ''RL(ログ印字)
                CHStr.RL = IIf(gBitCheck(.udtChCommon.shtFlag2, 0), "o", "")    ''RL

                ''REMARKS
                CHStr.Remarks = gGetString(.udtChCommon.strRemark)

                ' 2015.10.16 ﾀｸﾞ表示向け　強制的に差し替え
                ' 2015.10.22 Ver1.7.5 標準時とﾀｸﾞ表示時にて分ける
                If gudt.SetSystem.udtSysOps.shtTagMode <> 0 Then     ' CH表示でない場合
                    CHStr.CHNo = GetTagNo(udtChannel)
                End If
                '//

                ' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ表示 ｱﾗｰﾑﾚﾍﾞﾙ取得
                If gudt.SetSystem.udtSysOps.shtLRMode <> 0 Then
                    CHStr.AlmLevel = GetAlmLevelName(udtChannel)
                End If
                '//

                ''Share Type ■Share対応
                If .udtChCommon.shtShareType = 1 Then
                    CHStr.ShareType = "L"
                ElseIf .udtChCommon.shtShareType = 2 Then
                    CHStr.ShareType = "R"
                ElseIf .udtChCommon.shtShareType = 3 Then
                    CHStr.ShareType = "S"
                Else
                    CHStr.ShareType = ""
                End If

                CHStr.ShareCHNo = gGet2Byte(.udtChCommon.shtShareChid).ToString("0000")

                ''仮設定(Common) -------------------------------------------------------
                CHStr.INAdd = mPrtDmyCheck(CHStr.INAdd, .DummyCommonFuAddress)
                CHStr.Unit = mPrtDmyCheck(CHStr.Unit, .DummyCommonUnitName)


                'Ver2.0.0.7 Output設定
                'モーターとバルブは「o」しない。それ以外は出す
                Select Case .udtChCommon.shtChType  ''CH種別
                    Case gCstCodeChTypeMotor, gCstCodeChTypeValve
                    Case Else
                        CHStr.OUT = fnGetOrAnd(.udtChCommon.shtChno)
                End Select


                ''☆CH種別毎---------------------------------------------------------------------------------------
                Select Case .udtChCommon.shtChType  ''CH種別

                    Case gCstCodeChTypeAnalog       ''アナログ
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeAnalogK
                                CHStr.INSIG = "K"
                            Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog2Jpt, _
                                 gCstCodeChDataTypeAnalog3Pt, gCstCodeChDataTypeAnalog3Jpt
                                CHStr.INSIG = "TR"
                            Case gCstCodeChDataTypeAnalog1_5v
                                CHStr.INSIG = "AI"
                            Case gCstCodeChDataTypeAnalog4_20mA
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If
                            Case gCstCodeChDataTypeAnalogPT4_20mA
                                CHStr.INSIG = "PT"
                            Case gCstCodeChDataTypeAnalogJacom
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeAnalogJacom55
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-"   ''FU ADDRESS(IN)
                                    CHStr.INAdd2 = .udtChCommon.shtPin.ToString  ''FU ADDRESS(IN) 2019/04/15
                                    If .udtChCommon.shtPin >= 100 Then
                                        CHStr.Digit_flg = True
                                    Else
                                        CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString
                                        CHStr.Digit_flg = False
                                    End If
                                End If

                            Case gCstCodeChDataTypeAnalogModbus
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeAnalogExhAve
                                CHStr.INSIG = "MT"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExhRepose
                                CHStr.INSIG = "RP"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExtDev
                                CHStr.INSIG = "DV"                      '' 2013.10.25 追加
                            Case Else
                                '緯度、経度はAIの通信とする。Rangeの頭にN/S,E/Wをつけることで判別
                                CHStr.INSIG = "AI"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''Decimal Position --------------------------------------------
                        intDecimalP = .AnalogDecimalPosition
                        strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                        'Ver2.0.8.5 mmHgレンジ下限小数点対応
                        Dim intDecMMHG As Integer = 0   'MMHG時の下限専用decpoint
                        Dim strDecMMHG As String = ""   'MMHG時の下限ﾌｫｰﾏｯﾄ
                        Dim dblTempMMHG As Double = 0   'MMHG時の下限を編集する際の一時領域
                        '-


                        ''Range -------------------------------------------------------
                        ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                        If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                            CHStr.Range = ""
                        Else
                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                ''Range Type(2,3線式)     2014.05.19
                                'dblLowValue = .AnalogRangeLow
                                'dblHiValue = .
                                'dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                'dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                'Ver2.0.7.B ２、３線式の小数点表記修正
                                'dblLowValue = .AnalogRangeLow
                                'dblHiValue = .AnalogRangeHigh
                                'CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)
                                'Ver2.0.7.F ２、３線式の小数点表記変更
                                dblLowValue = .AnalogRangeLow
                                dblHiValue = .AnalogRangeHigh
                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)

                                'Ver2.0.8.5 mmHgレンジ下限小数点対応
                                'mmHG該当時Range再編集
                                If .AnalogMmHgFlg = 1 Then
                                    intDecMMHG = .AnalogMmHgDec
                                    strDecMMHG = "0.".PadRight(intDecMMHG + 2, "0"c)
                                    dblTempMMHG = CDbl(dblLowValue.ToString(strDecimalFormat))
                                    CHStr.Range = dblTempMMHG.ToString(strDecMMHG) & "/" & dblHiValue.ToString(strDecimalFormat)
                                End If
                                '-
                            Else
                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)

                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)

                                '' Ver1.10.1 2016.02.29 力率表示対応
                                '' Ver1.10.1.1 2016.03.02 比較時のかっこ漏れ
                                ''If (.udtChCommon.shtFlag1 And &H20) = &H20 Then
                                If gBitCheck(.udtChCommon.shtFlag1, 5) Then
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & nCenter.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.8.5 mmHgレンジ下限小数点対応
                                    'mmHG該当時Range再編集
                                    If .AnalogMmHgFlg = 1 Then
                                        intDecMMHG = .AnalogMmHgDec
                                        strDecMMHG = "0.".PadRight(intDecMMHG + 2, "0"c)
                                        dblTempMMHG = CDbl(dblLowValue.ToString(strDecimalFormat))
                                        CHStr.Range = dblTempMMHG.ToString(strDecMMHG) & "/" & nCenter.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)
                                    End If
                                    '-

                                ElseIf gBitCheck(.udtChCommon.shtFlag1, 8) Then     '' Ver1.11.9.3 2016.11.26 P/S表示対応
                                    CHStr.Range = "P/S" & dblHiValue.ToString(strDecimalFormat)

                                ElseIf gBitCheck(.udtChCommon.shtFlag1, 9) Then     'Ver2.0.7.9 A/F対応
                                    CHStr.Range = "A/F" & dblHiValue.ToString(strDecimalFormat)

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Then     '' Ver1.10.6 2016.06.06 緯度追加
                                    CHStr.Range = "N/S" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 緯度はN/S (E/Wになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする
                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Then    '' Ver1.10.6 2016.06.06 経度追加
                                    CHStr.Range = "E/W" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 経度はE/W (N/Sになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする

                                Else
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.8.5 mmHgレンジ下限小数点対応
                                    'mmHG該当時Range再編集
                                    If .AnalogMmHgFlg = 1 Then
                                        intDecMMHG = .AnalogMmHgDec
                                        strDecMMHG = "0.".PadRight(intDecMMHG + 2, "0"c)
                                        dblTempMMHG = CDbl(dblLowValue.ToString(strDecimalFormat))
                                        CHStr.Range = dblTempMMHG.ToString(strDecMMHG) & "/" & dblHiValue.ToString(strDecimalFormat)
                                    End If
                                    '-
                                End If

                            End If

                            'Ver2.0.0.4
                            'グリーンマーク(ノーマルレンジ)対応
                            '設定アリの場合、「G」を付ける
                            If (.AnalogNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .AnalogNormalHigh <> 0) Or _
                                (.AnalogNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .AnalogNormalLow <> 0) Then
                                'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない　場所は、Remarksの頭に追加とする（仮）
                                If g_bytGreenMarkPrint = 1 Then
                                    CHStr.Remarks = "G:" & CHStr.Remarks
                                End If
                            End If

                        End If

                        ''Value -------------------------------------------------------
                        If .AnalogHiHiUse = 0 Then      ''Use HH アラーム無し
                            CHStr.AlmInf(0).Value = ""
                        Else
                            dblValue = .AnalogHiHiValue / (10 ^ intDecimalP)    ''Value HH
                            CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogHiUse = 0 Then        ''Use H  アラーム無し
                            CHStr.AlmInf(1).Value = ""
                        Else
                            dblValue = .AnalogHiValue / (10 ^ intDecimalP)      ''Value H
                            CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoUse = 0 Then        ''Use L  アラーム無し
                            CHStr.AlmInf(2).Value = ""
                        Else
                            dblValue = .AnalogLoValue / (10 ^ intDecimalP)      ''Value L
                            CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoLoUse = 0 Then      ''Use LL アラーム無し
                            CHStr.AlmInf(3).Value = ""
                        Else
                            dblValue = .AnalogLoLoValue / (10 ^ intDecimalP)    ''Value LL
                            CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        'Ver2.0.0.0 2016.12.06 ｾﾝｻｰﾌｪｲﾙ対応
                        'NotUse=空白
                        'Useだがｾﾝｻｰﾌｪｲﾙ無し=N
                        'Under=U
                        'Over=O
                        'Under,Over両方=o
                        If .AnalogSensorFailUse = 0 Then
                            CHStr.AlmInf(4).Value = ""
                        Else
                            If gBitCheck(.AnalogDisplay3, 1) And gBitCheck(.AnalogDisplay3, 2) Then
                                '両方
                                CHStr.AlmInf(4).Value = "o"
                            Else
                                If gBitCheck(.AnalogDisplay3, 1) Then
                                    'Underのみ
                                    CHStr.AlmInf(4).Value = "UDR"
                                Else
                                    If gBitCheck(.AnalogDisplay3, 2) Then
                                        'Overのみ
                                        CHStr.AlmInf(4).Value = "OVR"
                                    Else
                                        'Useだが無し
                                        CHStr.AlmInf(4).Value = "NON"
                                    End If
                                End If
                            End If
                        End If


                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(0).ExtGrp = IIf(.AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiHiExtGroup)                ''EXT.G HH
                        CHStr.AlmInf(1).ExtGrp = IIf(.AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiExtGroup)                    ''EXT.G H
                        CHStr.AlmInf(2).ExtGrp = IIf(.AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoExtGroup)                    ''EXT.G L
                        CHStr.AlmInf(3).ExtGrp = IIf(.AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoLoExtGroup)                ''EXT.G LL
                        CHStr.AlmInf(4).ExtGrp = IIf(.AnalogSensorFailExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogSensorFailExtGroup)    ''EXT.G SF

                        ''G Repose 1 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep1 = IIf(.AnalogHiHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiHiGroupRepose1)   ''G.Rep1 HH
                        CHStr.AlmInf(1).GrpRep1 = IIf(.AnalogHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiGroupRepose1)       ''G.Rep1 H
                        CHStr.AlmInf(2).GrpRep1 = IIf(.AnalogLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoGroupRepose1)       ''G.Rep1 L
                        CHStr.AlmInf(3).GrpRep1 = IIf(.AnalogLoLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoLoGroupRepose1)   ''G.Rep1 LL

                        ''G Repose 2 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep2 = IIf(.AnalogHiHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiHiGroupRepose2)   ''G.Rep2 HH
                        CHStr.AlmInf(1).GrpRep2 = IIf(.AnalogHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiGroupRepose2)       ''G.Rep2 H
                        CHStr.AlmInf(2).GrpRep2 = IIf(.AnalogLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoGroupRepose2)       ''G.Rep2 L
                        CHStr.AlmInf(3).GrpRep2 = IIf(.AnalogLoLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoLoGroupRepose2)   ''G.Rep2 LL

                        ''Delay -------------------------------------------------------
                        CHStr.AlmInf(0).Delay = IIf(.AnalogHiHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiHiDelay)                     ''Delay HH
                        CHStr.AlmInf(1).Delay = IIf(.AnalogHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiDelay)                         ''Delay H
                        CHStr.AlmInf(2).Delay = IIf(.AnalogLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoDelay)                         ''Delay L
                        CHStr.AlmInf(3).Delay = IIf(.AnalogLoLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoLoDelay)                     ''Delay LL
                        CHStr.AlmInf(4).Delay = IIf(.AnalogSensorFailDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogSensorFailDelay)         ''Delay SF

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            For i As Integer = 0 To 4
                                If CHStr.AlmInf(i).Delay <> "" Then
                                    CHStr.AlmInf(i).Delay += strTemp
                                End If
                            Next
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                            '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                If .udtChCommon.shtStatus = &H43 Then     '' LOW/NOR/HIGHならば差し替え
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        CHStr.Status = "正常/高"
                                    Else
                                        CHStr.Status = "NOR/HIGH"
                                    End If

                                End If
                            End If
                        Else
                            strHH = gGetString(.AnalogHiHiStatusInput)     ''特殊コード対応
                            strH = gGetString(.AnalogHiStatusInput)        ''特殊コード対応
                            strL = gGetString(.AnalogLoStatusInput)        ''特殊コード対応
                            strLL = gGetString(.AnalogLoLoStatusInput)     ''特殊コード対応

                            '' 2015.11.18  Ver1.8.1  ｽﾃｰﾀｽは未定の場合もあるので、表示方法変更
                            If LenB(strHH) = 0 And LenB(strH) = 0 And LenB(strL) = 0 And LenB(strLL) = 0 Then
                                strTemp = ""
                            Else
                                '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                    '' Ver1.9.8 2016.02.20 ｽﾃｰﾀｽﾁｪｯｸ方法変更
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "正常/" & strL
                                            Else
                                                strTemp = "正常/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "正常/" & strL & "/" & strLL
                                            Else
                                                strTemp = "正常/" & strH & "/" & strHH
                                            End If
                                        End If
                                    Else
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "NOR/" & strL
                                            Else
                                                strTemp = "NOR/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "NOR/" & strL & "/" & strLL
                                            Else
                                                strTemp = "NOR/" & strH & "/" & strHH
                                            End If

                                        End If
                                    End If
                                Else
                                    strTemp = ""    '' Ver1.11.5 2016.09.06  初期化追加

                                    '' Ver1.8.6.2  2015.12.04  ﾌﾗｸﾞは参照せずにｽﾃｰﾀｽが設定されていれば表示する
                                    ''                          設定値が決まっていなくてもｽﾃｰﾀｽのみ決まっていることがあるので
                                    ''If .AnalogLoLoUse = 1 And LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                    If LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                        strTemp += strLL & "/"
                                    Else
                                        strTemp = ""
                                    End If

                                    ''If .AnalogLoUse = 1 And LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                    If LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                        strTemp += strL & "/"
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        strTemp += "正常/"
                                    Else
                                        strTemp += "NOR/"
                                    End If

                                    ''If .AnalogHiUse = 1 And LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                    If LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                        strTemp += strH & "/"
                                    End If

                                    ''If .AnalogHiHiUse = 1 And LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                    If LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                        strTemp += strHH
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    'If strTemp = "NOR/" Then    '' NORのみならばｽﾃｰﾀｽ未定とする
                                    If strTemp = "NOR/" Or strTemp = "正常/" Then    'NORのみならばｽﾃｰﾀｽ未定とする
                                        strTemp = ""
                                    Else
                                        '' 文字列の最後尾ならば"/"を削除する
                                        nLen = LenB(strTemp)
                                        'Ver2.0.7.L
                                        'If strTemp.Substring(nLen - 1) = "/" Then
                                        If MidB(strTemp, nLen - 1) = "/" Then
                                            'strTemp = strTemp.Remove(nLen - 1)
                                            strTemp = MidB(strTemp, 0, nLen - 1)
                                        End If
                                    End If
                                End If


                            End If

                            '' ''2015.03.12 HIGH,LOWの並び順を変更
                            ''If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                            ''   (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                            ''    strTemp = ""
                            ''Else
                            ''    strTemp = "NOR/"
                            ''End If

                            '' ''LL/Lステータス
                            '' ''HIGH,LOWの両ステータスがある場合はLL/L、LOWのみはL/LL
                            ''If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                            ''   (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                            ''    If .AnalogLoLoUse = 1 Then
                            ''        strTemp += strLL
                            ''    End If
                            ''    If .AnalogLoUse = 1 Then
                            ''        If .AnalogLoLoUse = 1 Then
                            ''            strTemp += "/" & strL
                            ''        Else
                            ''            strTemp += strL
                            ''        End If
                            ''    End If
                            ''Else
                            ''    If .AnalogLoUse = 1 Then
                            ''        strTemp += strL
                            ''    End If
                            ''    If .AnalogLoLoUse = 1 Then
                            ''        If .AnalogLoUse = 1 Then
                            ''            strTemp += "/" & strLL
                            ''        Else
                            ''            strTemp += strLL
                            ''        End If
                            ''    End If
                            ''End If

                            '' ''HIGH,LOWの両ステータスがある場合は中間に"NOR"
                            ''If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                            ''   (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                            ''    strTemp += "/NOR/"
                            ''End If

                            '' ''H/HHステータス
                            ''If .AnalogHiUse = 1 Then
                            ''    strTemp += strH
                            ''End If
                            ''If .AnalogHiHiUse = 1 Then
                            ''    If .AnalogHiUse = 1 Then
                            ''        strTemp += "/" & strHH
                            ''    Else
                            ''        strTemp += strHH
                            ''    End If
                            ''End If
                            ''//

                            CHStr.Status = strTemp

                        End If

                        If .AnalogHiHiUse = 1 Or .AnalogHiUse = 1 Or .AnalogLoUse = 1 Or .AnalogLoLoUse = 1 Or .AnalogSensorFailUse = 1 Then
                            '排ガスリポーズはアラームではないので除外（フラグは必要)   2013.07.25 K.Fujimoto
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                CHStr.AL = ""
                            Else
                                CHStr.AL = "o"
                            End If
                        Else
                            CHStr.AL = ""
                        End If

                        ''仮設定
                        ''Range -------------------------------------------------------
                        CHStr.Range = mPrtDmyCheck(CHStr.Range, .DummyRangeScale)

                        ''Value -------------------------------------------------------
                        CHStr.AlmInf(0).Value = mPrtDmyCheck(CHStr.AlmInf(0).Value, .DummyValueHH)    ''EXT.G HH
                        CHStr.AlmInf(1).Value = mPrtDmyCheck(CHStr.AlmInf(1).Value, .DummyValueH)     ''EXT.G H
                        CHStr.AlmInf(2).Value = mPrtDmyCheck(CHStr.AlmInf(2).Value, .DummyValueL)     ''EXT.G L
                        CHStr.AlmInf(3).Value = mPrtDmyCheck(CHStr.AlmInf(3).Value, .DummyValueLL)    ''EXT.G LL
                        CHStr.AlmInf(4).Value = mPrtDmyCheck(CHStr.AlmInf(4).Value, .DummyValueSF)    ''EXT.G SF

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(0).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(0).ExtGrp, .DummyExtGrHH)  ''EXT.G HH
                        CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyExtGrH)   ''EXT.G H
                        CHStr.AlmInf(2).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(2).ExtGrp, .DummyExtGrL)   ''EXT.G L
                        CHStr.AlmInf(3).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(3).ExtGrp, .DummyExtGrLL)  ''EXT.G LL
                        CHStr.AlmInf(4).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(4).ExtGrp, .DummyExtGrSF)  ''EXT.G SF

                        ''G Repose 1 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep1, .DummyGRep1HH) ''G.Rep1 HH
                        CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyGRep1H)  ''G.Rep1 H
                        CHStr.AlmInf(2).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep1, .DummyGRep1L)  ''G.Rep1 L
                        CHStr.AlmInf(3).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep1, .DummyGRep1LL) ''G.Rep1 LL

                        ''G Repose 2 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep2, .DummyGRep2HH) ''G.Rep2 HH
                        CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyGRep2H)  ''G.Rep2 H
                        CHStr.AlmInf(2).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep2, .DummyGRep2L)  ''G.Rep2 L
                        CHStr.AlmInf(3).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep2, .DummyGRep2LL) ''G.Rep2 LL

                        ''Delay -------------------------------------------------------
                        CHStr.AlmInf(0).Delay = mPrtDmyCheck(CHStr.AlmInf(0).Delay, .DummyDelayHH)     ''Delay HH
                        CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyDelayH)      ''Delay H
                        CHStr.AlmInf(2).Delay = mPrtDmyCheck(CHStr.AlmInf(2).Delay, .DummyDelayL)      ''Delay L
                        CHStr.AlmInf(3).Delay = mPrtDmyCheck(CHStr.AlmInf(3).Delay, .DummyDelayLL)     ''Delay LL
                        CHStr.AlmInf(4).Delay = mPrtDmyCheck(CHStr.AlmInf(4).Delay, .DummyDelaySF)     ''Delay SF

                        ''STATUS
                        If .DummyStaNmHH Or .DummyStaNmH Or .DummyStaNmL Or .DummyStaNmLL Or .DummyStaNmSF Then
                            CHStr.Status = mPrtDmyCheck(CHStr.Status, True)
                        End If


                    Case gCstCodeChTypeDigital      ''デジタル
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeDigitalNC
                                CHStr.INSIG = "NC"
                            Case gCstCodeChDataTypeDigitalNO
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalJacomNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacom55NC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-"   ''FU ADDRESS(IN)
                                    CHStr.INAdd2 = .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN) 2019/04/15

                                    If .udtChCommon.shtPin >= 100 Then
                                        CHStr.Digit_flg = True
                                    Else
                                        CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString
                                        CHStr.Digit_flg = False
                                    End If

                                End If


                            Case gCstCodeChDataTypeDigitalJacomNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If

                            Case gCstCodeChDataTypeDigitalJacom55NO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-"   ''FU ADDRESS(IN)
                                    CHStr.INAdd2 = .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN) 2019/04/15
                                    If .udtChCommon.shtPin >= 100 Then
                                        CHStr.Digit_flg = True
                                    Else
                                        CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString
                                        CHStr.Digit_flg = False
                                    End If
                                End If

                            Case gCstCodeChDataTypeDigitalModbusNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalModbusNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalExt
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalDeviceStatus
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "S"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                        Else
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            'Ver2.0.7.L
                            'If strTemp.Length > 8 Then
                            If LenB(strTemp) > 8 Then
                                'CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                                CHStr.Status = MidB(strTemp, 0, 8).Trim & "/" & MidB(strTemp, 8).Trim
                            Else
                                CHStr.Status = Trim(strTemp)
                            End If

                        End If

                        If .DigitalUse = 1 Then
                            CHStr.AL = "o"
                        Else
                            CHStr.AL = ""
                        End If

                        ''仮設定
                        CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCommonExtGroup)
                        CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCommonGroupRepose1)
                        CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCommonGroupRepose2)
                        CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCommonDelay)
                        CHStr.Status = mPrtDmyCheck(CHStr.Status, .DummyCommonStatusName)

                    Case gCstCodeChTypeMotor        ''モーター
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        If .udtChCommon.shtData >= gCstCodeChDataTypeMotorManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M1"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M2"


                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK Then
                            CHStr.INSIG = "M1"
                            CHStr.SIGType = "R"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRAbnorRunK Then
                            CHStr.INSIG = "M2"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END


                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Then
                            CHStr.INSIG = "MO"

                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM-"
                            Else
                                CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                            End If
                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM55-"
                            Else
                                CHStr.INAdd = "JACOM55-"   ''FU ADDRESS(IN)
                                CHStr.INAdd2 = .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN) 2019/04/15
                                If .udtChCommon.shtPin >= 100 Then
                                    CHStr.Digit_flg = True
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString
                                    CHStr.Digit_flg = False
                                End If
                            End If
                        End If

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            ' 2013.07.22 MO表示変更  K.Fujimoto
                            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200218 hori

                                If .udtChCommon.shtStatus = 20 Then     ' MO
                                    CHStr.Status = "運転"
                                Else
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text
                                    CHStr.Status = CHStr.Status.Replace("RUN", "運転")    '和文仕様 20200218 hori
                                End If

                            Else
                                If .udtChCommon.shtStatus = 20 Then     ' MO
                                    CHStr.Status = "RUN"
                                Else
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text
                                End If
                            End If
                        Else
                            CHStr.Status = ""
                        End If

                            'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                            'If .MotorUse = 1 Then
                            If .MotorUse = 1 Or .MotorAlarmUse = 1 Then
                                CHStr.AL = "o"
                            Else
                                CHStr.AL = ""
                            End If

                            ''FU ADDRESS(OUT)
                            CHStr.OUTAdd = mPrtConvFuAddress(.MotorFuNo, .MotorPortNo, .MotorPin)

                            If .MotorAlarmUse = 1 Then  ''フィードバックアラーム有り

                                '2015/4/23 T.Ueki Feedback タイマー msec→secに表示
                                CHStr.AlmInf(9).Value = Val(.MotorFeedback) / 10
                                'CHStr.AlmInf(9).Value = .MotorFeedback
                                CHStr.AlmInf(9).ExtGrp = IIf(.MotorAlarmExtGroup = gCstCodeChMotorExtGroupNothing, "", .MotorAlarmExtGroup)                 ''EXT.G
                                CHStr.AlmInf(9).GrpRep1 = IIf(.MotorAlarmGroupRepose1 = gCstCodeChMotorGroupRepose1Nothing, "", .MotorAlarmGroupRepose1)    ''G.Rep1
                                CHStr.AlmInf(9).GrpRep2 = IIf(.MotorAlarmGroupRepose2 = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmGroupRepose2)    ''G.Rep2
                                CHStr.AlmInf(9).Delay = IIf(.MotorAlarmDelay = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmDelay)                    ''DELAY
                            End If

                            ''Delay タイマー切替
                            strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                            If strTemp = "m" Then
                                If CHStr.AlmInf(1).Delay <> "" Then
                                    CHStr.AlmInf(1).Delay += strTemp
                                End If
                                If CHStr.AlmInf(9).Delay <> "" Then
                                    CHStr.AlmInf(9).Delay += strTemp
                                End If
                            End If

                            'Ver2.0.7.2
                            'モーターは、OutFuAdrが設定されていないとOUTSigは出さない
                            If CHStr.OUTAdd <> "" Then
                                If .MotorControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                            End If
                            '-

                            ''仮設定
                            CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCommonExtGroup)
                            CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCommonGroupRepose1)
                            CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCommonGroupRepose2)
                            CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCommonDelay)
                            CHStr.Status = mPrtDmyCheck(CHStr.Status, .DummyCommonStatusName)

                            CHStr.AlmInf(9).Value = mPrtDmyCheck(CHStr.AlmInf(9).Value, .DummyFaTimeV)        ''FA TIMER
                            CHStr.AlmInf(9).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(9).ExtGrp, .DummyFaExtGr)      ''EXT.G
                            CHStr.AlmInf(9).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep1, .DummyFaGrep1)    ''G.Rep1
                            CHStr.AlmInf(9).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep2, .DummyFaGrep2)    ''G.Rep2
                            CHStr.AlmInf(9).Delay = mPrtDmyCheck(CHStr.AlmInf(9).Delay, .DummyFaDelay)        ''DELAY

                            CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS  

                            ''現状下記表示無し
                            ''.DummyOutStatusType
                            ''.DummyFaStaNm

                    Case gCstCodeChTypeValve        ''バルブ
                            ''INSIG, SIGTYPE
                            CHStr.SIGType = ""

                            Select Case .udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO   ''DI/DO
                                    CHStr.INSIG = "DC3"

                                    ''デジタルコンポジット設定テーブルインデックス ----------------
                                    intCompIndex = .ValveCompositeTableIndex
                                    CHStr.Status = ""
                                    intStatusExist = 0
                                    CHStr.AL = ""

                                    '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得処理追加
                                    If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                        Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                        cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                        CHStr.Status = cmbStatus.Text
                                        intStatusExist = -1
                                    End If

                                    With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                        For i As Integer = 0 To 8
                                            ''EXT Group ---------------------------------------------------
                                            CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                            ''G Repose 1 ---------------------------------------------------
                                            CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                            ''G Repose 2 ---------------------------------------------------
                                            CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                            ''Delay --------------------------------------------------------
                                            CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                            ''Status ------------------------------------------------------
                                            If intStatusExist <> -1 Then        '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得済
                                                If intStatusExist = 1 Then
                                                    CHStr.Status += "/"
                                                End If
                                                If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                                    CHStr.Status += .udtCompInf(i).strStatusName
                                                    intStatusExist = 1
                                                Else
                                                    CHStr.Status += ""
                                                End If
                                            End If

                                            If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                                CHStr.AL = "o"
                                            End If
                                        Next

                                    End With

                                    ''FU ADDRESS(OUT)
                                    CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)

                                    If .ValveDiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                        'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                        CHStr.AL = "o"

                                        '2015/4/23 T.Ueki Feedback タイマー msec→secに表示 
                                        CHStr.AlmInf(9).Value = Val(.ValveDiDoFeedback) / 10
                                        'CHStr.AlmInf(9).Value = .ValveDiDoFeedback                                                                                          ''Value
                                        CHStr.AlmInf(9).ExtGrp = IIf(.ValveDiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveDiDoAlarmExtGroup)                 ''EXT.G
                                        CHStr.AlmInf(9).GrpRep1 = IIf(.ValveDiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveDiDoAlarmGroupRepose1)    ''G.Rep1
                                        CHStr.AlmInf(9).GrpRep2 = IIf(.ValveDiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveDiDoAlarmGroupRepose2)    ''G.Rep2
                                        CHStr.AlmInf(9).Delay = IIf(.ValveDiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveDiDoAlarmDelay)                    ''DELAY
                                    End If

                                    ''Delay タイマー切替
                                    strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                    If strTemp = "m" Then
                                        For i As Integer = 0 To 9
                                            If CHStr.AlmInf(i).Delay <> "" Then
                                                CHStr.AlmInf(i).Delay += strTemp
                                            End If
                                        Next
                                    End If

                                    'Ver2.0.7.2
                                    'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                    'If CHStr.OUTAdd <> "" Then
                                    If .ValveDiDoControl = 1 Then
                                        CHStr.OUTSIG = "DOP"
                                    Else
                                        CHStr.OUTSIG = "DOM"
                                    End If
                                    'End If

                                    CHStr.TermCount = .udtChCommon.shtPinNo       '' 端子数  Ver1.11.9.3 2016.11.26 追加

                                    ''仮設定
                                    CHStr.AlmInf(0).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(0).ExtGrp, .DummyCmpStatus1ExtGr)
                                    CHStr.AlmInf(0).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep1, .DummyCmpStatus1GRep1)
                                    CHStr.AlmInf(0).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep2, .DummyCmpStatus1GRep2)
                                    CHStr.AlmInf(0).Delay = mPrtDmyCheck(CHStr.AlmInf(0).Delay, .DummyCmpStatus1Delay)

                                    CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCmpStatus2ExtGr)
                                    CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCmpStatus2GRep1)
                                    CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCmpStatus2GRep2)
                                    CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCmpStatus2Delay)

                                    CHStr.AlmInf(2).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(2).ExtGrp, .DummyCmpStatus3ExtGr)
                                    CHStr.AlmInf(2).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep1, .DummyCmpStatus3GRep1)
                                    CHStr.AlmInf(2).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep2, .DummyCmpStatus3GRep2)
                                    CHStr.AlmInf(2).Delay = mPrtDmyCheck(CHStr.AlmInf(2).Delay, .DummyCmpStatus3Delay)

                                    CHStr.AlmInf(3).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(3).ExtGrp, .DummyCmpStatus4ExtGr)
                                    CHStr.AlmInf(3).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep1, .DummyCmpStatus4GRep1)
                                    CHStr.AlmInf(3).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep2, .DummyCmpStatus4GRep2)
                                    CHStr.AlmInf(3).Delay = mPrtDmyCheck(CHStr.AlmInf(3).Delay, .DummyCmpStatus4Delay)

                                    CHStr.AlmInf(4).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(4).ExtGrp, .DummyCmpStatus5ExtGr)
                                    CHStr.AlmInf(4).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(4).GrpRep1, .DummyCmpStatus5GRep1)
                                    CHStr.AlmInf(4).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(4).GrpRep2, .DummyCmpStatus5GRep2)
                                    CHStr.AlmInf(4).Delay = mPrtDmyCheck(CHStr.AlmInf(4).Delay, .DummyCmpStatus5Delay)

                                    CHStr.AlmInf(5).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(5).ExtGrp, .DummyCmpStatus6ExtGr)
                                    CHStr.AlmInf(5).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(5).GrpRep1, .DummyCmpStatus6GRep1)
                                    CHStr.AlmInf(5).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(5).GrpRep2, .DummyCmpStatus6GRep2)
                                    CHStr.AlmInf(5).Delay = mPrtDmyCheck(CHStr.AlmInf(5).Delay, .DummyCmpStatus6Delay)

                                    CHStr.AlmInf(6).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(6).ExtGrp, .DummyCmpStatus7ExtGr)
                                    CHStr.AlmInf(6).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(6).GrpRep1, .DummyCmpStatus7GRep1)
                                    CHStr.AlmInf(6).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(6).GrpRep2, .DummyCmpStatus7GRep2)
                                    CHStr.AlmInf(6).Delay = mPrtDmyCheck(CHStr.AlmInf(6).Delay, .DummyCmpStatus7Delay)

                                    CHStr.AlmInf(7).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(7).ExtGrp, .DummyCmpStatus8ExtGr)
                                    CHStr.AlmInf(7).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(7).GrpRep1, .DummyCmpStatus8GRep1)
                                    CHStr.AlmInf(7).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(7).GrpRep2, .DummyCmpStatus8GRep2)
                                    CHStr.AlmInf(7).Delay = mPrtDmyCheck(CHStr.AlmInf(7).Delay, .DummyCmpStatus8Delay)

                                    CHStr.AlmInf(8).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(8).ExtGrp, .DummyCmpStatus9ExtGr)
                                    CHStr.AlmInf(8).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(8).GrpRep1, .DummyCmpStatus9GRep1)
                                    CHStr.AlmInf(8).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(8).GrpRep2, .DummyCmpStatus9GRep2)
                                    CHStr.AlmInf(8).Delay = mPrtDmyCheck(CHStr.AlmInf(8).Delay, .DummyCmpStatus9Delay)

                                    CHStr.AlmInf(9).Value = mPrtDmyCheck(CHStr.AlmInf(9).Value, .DummyFaTimeV)        ''FA TIMER
                                    CHStr.AlmInf(9).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(9).ExtGrp, .DummyFaExtGr)      ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep1, .DummyFaGrep1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep2, .DummyFaGrep2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = mPrtDmyCheck(CHStr.AlmInf(9).Delay, .DummyFaDelay)        ''DELAY

                                    If .DummyCmpStatus1StaNm Or .DummyCmpStatus2StaNm Or .DummyCmpStatus3StaNm Or .DummyCmpStatus4StaNm Or _
                                       .DummyCmpStatus5StaNm Or .DummyCmpStatus6StaNm Or .DummyCmpStatus7StaNm Or .DummyCmpStatus8StaNm Then
                                        CHStr.Status = mPrtDmyCheck(CHStr.Status, True)
                                    End If

                                    CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS  

                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                    If .udtChCommon.shtSignal = 2 Then
                                        CHStr.INSIG = "PT"
                                    Else
                                        CHStr.INSIG = "AI"
                                    End If

                                    ''Decimal Position --------------------------------------------
                                    intDecimalP = .ValveAiDoDecimalPosition
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                    ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                    ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                    If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                        CHStr.Range = ""
                                    Else
                                        dblLowValue = .ValveAiDoRangeLow / (10 ^ intDecimalP)
                                        dblHiValue = .ValveAiDoRangeHigh / (10 ^ intDecimalP)
                                        CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                        'Ver2.0.0.4
                                        'グリーンマーク(ノーマルレンジ)対応
                                        '設定アリの場合、「G」を付ける
                                        If (.ValveAiDoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiDoNormalHigh <> 0) Or _
                                            (.ValveAiDoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiDoNormalLow <> 0) Then
                                            'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                            If g_bytGreenMarkPrint = 1 Then
                                                CHStr.Range = "G " & CHStr.Range
                                            End If
                                        End If

                                    End If


                                    ''Value -------------------------------------------------------
                                    If .ValveAiDoHiHiUse = 0 Then      ''Use HH アラーム無し
                                        CHStr.AlmInf(0).Value = ""
                                    Else
                                        dblValue = .ValveAiDoHiHiValue / (10 ^ intDecimalP)    ''Value HH
                                        CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiDoHiUse = 0 Then        ''Use H  アラーム無し
                                        CHStr.AlmInf(1).Value = ""
                                    Else
                                        dblValue = .ValveAiDoHiValue / (10 ^ intDecimalP)      ''Value H
                                        CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiDoLoUse = 0 Then        ''Use L  アラーム無し
                                        CHStr.AlmInf(2).Value = ""
                                    Else
                                        dblValue = .ValveAiDoLoValue / (10 ^ intDecimalP)      ''Value L
                                        CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiDoLoLoUse = 0 Then      ''Use LL アラーム無し
                                        CHStr.AlmInf(3).Value = ""
                                    Else
                                        dblValue = .ValveAiDoLoLoValue / (10 ^ intDecimalP)    ''Value LL
                                        CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiDoSensorFailUse = 0 Then
                                        CHStr.AlmInf(4).Value = ""
                                    Else
                                        CHStr.AlmInf(4).Value = "o"
                                    End If


                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiDoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiHiExtGroup)                ''EXT.G HH
                                    CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiDoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiExtGroup)                    ''EXT.G H
                                    CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiDoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoExtGroup)                    ''EXT.G L
                                    CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiDoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoLoExtGroup)                ''EXT.G LL
                                    CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiDoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoSensorFailExtGroup)    ''EXT.G SF

                                    ''G Repose 1 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiDoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiHiGroupRepose1)   ''G.Rep1 HH
                                    CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiDoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiGroupRepose1)       ''G.Rep1 H
                                    CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiDoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoGroupRepose1)       ''G.Rep1 L
                                    CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiDoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoLoGroupRepose1)   ''G.Rep1 LL

                                    ''G Repose 2 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiDoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiHiGroupRepose2)   ''G.Rep2 HH
                                    CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiDoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiGroupRepose2)       ''G.Rep2 H
                                    CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiDoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoGroupRepose2)       ''G.Rep2 L
                                    CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiDoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoLoGroupRepose2)   ''G.Rep2 LL

                                    ''Delay -------------------------------------------------------
                                    CHStr.AlmInf(0).Delay = IIf(.ValveAiDoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiHiDelay)                     ''Delay HH
                                    CHStr.AlmInf(1).Delay = IIf(.ValveAiDoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiDelay)                         ''Delay H
                                    CHStr.AlmInf(2).Delay = IIf(.ValveAiDoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoDelay)                         ''Delay L
                                    CHStr.AlmInf(3).Delay = IIf(.ValveAiDoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoLoDelay)                     ''Delay LL
                                    CHStr.AlmInf(4).Delay = IIf(.ValveAiDoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoSensorFailDelay)         ''Delay SF

                                    ''Status -----------------------------------------------------
                                    If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                        Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                        cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                        CHStr.Status = cmbStatus.Text

                                    Else
                                        strHH = gGetString(.ValveAiDoHiHiStatusInput)     ''特殊コード対応
                                        strH = gGetString(.ValveAiDoHiStatusInput)        ''特殊コード対応
                                        strL = gGetString(.ValveAiDoLoStatusInput)        ''特殊コード対応
                                        strLL = gGetString(.ValveAiDoLoLoStatusInput)     ''特殊コード対応

                                        ''2015.03.12 HIGH,LOWの並び順を変更
                                        If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                           (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                            strTemp = ""
                                        Else
                                            'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp = "正常/"
                                        Else
                                            strTemp = "NOR/"
                                        End If
                                        End If

                                        ''LL/Lステータス
                                        ''HIGH,LOWの両ステータスがある場合はLL/L、LOWのみはL/LL
                                        If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                           (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                            If .AnalogLoLoUse = 1 Then
                                                strTemp += strLL
                                            End If
                                            If .AnalogLoUse = 1 Then
                                                If .AnalogLoLoUse = 1 Then
                                                    strTemp += "/" & strL
                                                Else
                                                    strTemp += strL
                                                End If
                                            End If
                                        Else
                                            If .AnalogLoUse = 1 Then
                                                strTemp += strL
                                            End If
                                            If .AnalogLoLoUse = 1 Then
                                                If .AnalogLoUse = 1 Then
                                                    strTemp += "/" & strLL
                                                Else
                                                    strTemp += strLL
                                                End If
                                            End If
                                        End If

                                        ''HIGH,LOWの両ステータスがある場合は中間に"NOR"
                                        If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                           (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                            'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "/正常/"
                                        Else
                                            strTemp += "/NOR/"
                                        End If
                                        End If

                                        ''H/HHステータス
                                        If .AnalogHiUse = 1 Then
                                            strTemp += strH
                                        End If
                                        If .AnalogHiHiUse = 1 Then
                                            If .AnalogHiUse = 1 Then
                                                strTemp += "/" & strHH
                                            Else
                                                strTemp += strHH
                                            End If
                                        End If

                                        CHStr.Status = strTemp

                                    End If

                                    If .ValveAiDoHiHiUse = 1 Or .ValveAiDoHiUse = 1 Or .ValveAiDoLoUse = 1 Or .ValveAiDoLoLoUse = 1 Or .ValveAiDoSensorFailUse = 1 Then
                                        CHStr.AL = "o"
                                    Else
                                        CHStr.AL = ""
                                    End If

                                    ''FU ADDRESS(OUT)
                                    CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiDoFuNo, .ValveAiDoPortNo, .ValveAiDoPin)

                                    If .ValveAiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                        'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                        CHStr.AL = "o"

                                        CHStr.AlmInf(9).Value = Val(.ValveAiDoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                        'CHStr.AlmInf(9).Value = .ValveAiDoFeedback                                                                                          ''Value
                                        CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoAlarmExtGroup)                 ''EXT.G
                                        CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoAlarmGroupRepose1)    ''G.Rep1
                                        CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoAlarmGroupRepose2)    ''G.Rep2
                                        CHStr.AlmInf(9).Delay = IIf(.ValveAiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiDoAlarmDelay)                      ''DELAY
                                    End If

                                    ''Delay タイマー切替
                                    strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                    If strTemp = "m" Then
                                        For i As Integer = 0 To 4
                                            If CHStr.AlmInf(i).Delay <> "" Then
                                                CHStr.AlmInf(i).Delay += strTemp
                                            End If
                                        Next
                                        If CHStr.AlmInf(9).Delay <> "" Then
                                            CHStr.AlmInf(9).Delay += strTemp
                                        End If
                                    End If

                                    'Ver2.0.7.2
                                    'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                    'If CHStr.OUTAdd <> "" Then
                                    If .ValveAiDoOutControl = 1 Then
                                        CHStr.OUTSIG = "DOP"
                                    Else
                                        CHStr.OUTSIG = "DOM"
                                    End If
                                    'End If

                                    ''仮設定
                                    ''Range -------------------------------------------------------
                                    CHStr.Range = mPrtDmyCheck(CHStr.Range, .DummyRangeScale)

                                    ''Value -------------------------------------------------------
                                    CHStr.AlmInf(0).Value = mPrtDmyCheck(CHStr.AlmInf(0).Value, .DummyValueHH)    ''EXT.G HH
                                    CHStr.AlmInf(1).Value = mPrtDmyCheck(CHStr.AlmInf(1).Value, .DummyValueH)     ''EXT.G H
                                    CHStr.AlmInf(2).Value = mPrtDmyCheck(CHStr.AlmInf(2).Value, .DummyValueL)     ''EXT.G L
                                    CHStr.AlmInf(3).Value = mPrtDmyCheck(CHStr.AlmInf(3).Value, .DummyValueLL)    ''EXT.G LL
                                    CHStr.AlmInf(4).Value = mPrtDmyCheck(CHStr.AlmInf(4).Value, .DummyValueSF)    ''EXT.G SF

                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(0).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(0).ExtGrp, .DummyExtGrHH)  ''EXT.G HH
                                    CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyExtGrH)   ''EXT.G H
                                    CHStr.AlmInf(2).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(2).ExtGrp, .DummyExtGrL)   ''EXT.G L
                                    CHStr.AlmInf(3).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(3).ExtGrp, .DummyExtGrLL)  ''EXT.G LL
                                    CHStr.AlmInf(4).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(4).ExtGrp, .DummyExtGrSF)  ''EXT.G SF

                                    ''G Repose 1 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep1, .DummyGRep1HH) ''G.Rep1 HH
                                    CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyGRep1H)  ''G.Rep1 H
                                    CHStr.AlmInf(2).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep1, .DummyGRep1L)  ''G.Rep1 L
                                    CHStr.AlmInf(3).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep1, .DummyGRep1LL) ''G.Rep1 LL

                                    ''G Repose 2 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep2, .DummyGRep2HH) ''G.Rep2 HH
                                    CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyGRep2H)  ''G.Rep2 H
                                    CHStr.AlmInf(2).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep2, .DummyGRep2L)  ''G.Rep2 L
                                    CHStr.AlmInf(3).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep2, .DummyGRep2LL) ''G.Rep2 LL

                                    ''Delay -------------------------------------------------------
                                    CHStr.AlmInf(0).Delay = mPrtDmyCheck(CHStr.AlmInf(0).Delay, .DummyDelayHH)     ''Delay HH
                                    CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyDelayH)      ''Delay H
                                    CHStr.AlmInf(2).Delay = mPrtDmyCheck(CHStr.AlmInf(2).Delay, .DummyDelayL)      ''Delay L
                                    CHStr.AlmInf(3).Delay = mPrtDmyCheck(CHStr.AlmInf(3).Delay, .DummyDelayLL)     ''Delay LL
                                    CHStr.AlmInf(4).Delay = mPrtDmyCheck(CHStr.AlmInf(4).Delay, .DummyDelaySF)     ''Delay SF

                                    ''STATUS
                                    If .DummyStaNmHH Or .DummyStaNmH Or .DummyStaNmL Or .DummyStaNmLL Or .DummyStaNmSF Then
                                        CHStr.Status = mPrtDmyCheck(CHStr.Status, True)
                                    End If

                                    CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS

                                    ''FA -------------------------------------------------------
                                    CHStr.AlmInf(9).Value = mPrtDmyCheck(CHStr.AlmInf(9).Value, .DummyFaTimeV)        ''FA TIMER
                                    CHStr.AlmInf(9).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(9).ExtGrp, .DummyFaExtGr)      ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep1, .DummyFaGrep1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep2, .DummyFaGrep2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = mPrtDmyCheck(CHStr.AlmInf(9).Delay, .DummyFaDelay)


                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2    ''AI/AO
                                    If .udtChCommon.shtSignal = 2 Then
                                        CHStr.INSIG = "PT"
                                    Else
                                        CHStr.INSIG = "AI"
                                    End If

                                    ''Decimal Position --------------------------------------------
                                    intDecimalP = .ValveAiAoDecimalPosition
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                    ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                    ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                    If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                        CHStr.Range = ""
                                    Else
                                        dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                        dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                        CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                        'Ver2.0.0.4
                                        'グリーンマーク(ノーマルレンジ)対応
                                        '設定アリの場合、「G」を付ける
                                        If (.ValveAiAoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiAoNormalHigh <> 0) Or _
                                            (.ValveAiAoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiAoNormalLow <> 0) Then
                                            'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                            If g_bytGreenMarkPrint = 1 Then
                                                CHStr.Range = "G " & CHStr.Range
                                            End If
                                        End If
                                    End If

                                    ''Value -------------------------------------------------------
                                    If .ValveAiAoHiHiUse = 0 Then      ''Use HH アラーム無し
                                        CHStr.AlmInf(0).Value = ""
                                    Else
                                        dblValue = .ValveAiAoHiHiUse / (10 ^ intDecimalP)    ''Value HH
                                        CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiAoHiUse = 0 Then        ''Use H  アラーム無し
                                        CHStr.AlmInf(1).Value = ""
                                    Else
                                        dblValue = .ValveAiAoHiUse / (10 ^ intDecimalP)      ''Value H
                                        CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiAoLoUse = 0 Then        ''Use L  アラーム無し
                                        CHStr.AlmInf(2).Value = ""
                                    Else
                                        dblValue = .ValveAiAoLoUse / (10 ^ intDecimalP)      ''Value L
                                        CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiAoLoLoUse = 0 Then      ''Use LL アラーム無し
                                        CHStr.AlmInf(3).Value = ""
                                    Else
                                        dblValue = .ValveAiAoLoLoUse / (10 ^ intDecimalP)    ''Value LL
                                        CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                    End If

                                    If .ValveAiAoSensorFailUse = 0 Then
                                        CHStr.AlmInf(4).Value = ""
                                    Else
                                        CHStr.AlmInf(4).Value = "o"
                                    End If


                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiAoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiHiExtGroup)                ''EXT.G HH
                                    CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiAoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiExtGroup)                    ''EXT.G H
                                    CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiAoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoExtGroup)                    ''EXT.G L
                                    CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiAoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoLoExtGroup)                ''EXT.G LL
                                    CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiAoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoSensorFailExtGroup)    ''EXT.G SF

                                    ''G Repose 1 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiAoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiHiGroupRepose1)   ''G.Rep1 HH
                                    CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiAoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiGroupRepose1)       ''G.Rep1 H
                                    CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiAoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoGroupRepose1)       ''G.Rep1 L
                                    CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiAoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoLoGroupRepose1)   ''G.Rep1 LL

                                    ''G Repose 2 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiAoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiHiGroupRepose2)   ''G.Rep2 HH
                                    CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiAoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiGroupRepose2)       ''G.Rep2 H
                                    CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiAoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoGroupRepose2)       ''G.Rep2 L
                                    CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiAoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoLoGroupRepose2)   ''G.Rep2 LL

                                    ''Delay -------------------------------------------------------
                                    CHStr.AlmInf(0).Delay = IIf(.ValveAiAoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiHiDelay)                     ''Delay HH
                                    CHStr.AlmInf(1).Delay = IIf(.ValveAiAoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiDelay)                         ''Delay H
                                    CHStr.AlmInf(2).Delay = IIf(.ValveAiAoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoDelay)                         ''Delay L
                                    CHStr.AlmInf(3).Delay = IIf(.ValveAiAoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoLoDelay)                     ''Delay LL
                                    CHStr.AlmInf(4).Delay = IIf(.ValveAiAoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoSensorFailDelay)         ''Delay SF

                                    ''Status -----------------------------------------------------
                                    If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                        Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                        cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                        CHStr.Status = cmbStatus.Text

                                    Else
                                        strHH = gGetString(.ValveAiAoHiHiStatusInput)     ''特殊コード対応
                                        strH = gGetString(.ValveAiAoHiStatusInput)        ''特殊コード対応
                                        strL = gGetString(.ValveAiAoLoStatusInput)        ''特殊コード対応
                                        strLL = gGetString(.ValveAiAoLoLoStatusInput)     ''特殊コード対応


                                        If .ValveAiAoHiHiUse = 1 Then
                                            strTemp = strHH
                                            intLen = strTemp.Length
                                        End If
                                        If .ValveAiAoHiUse = 1 Then
                                            If .ValveAiAoHiHiUse = 1 Then
                                                strTemp += "/" & strH
                                            Else
                                                strTemp += strH
                                            End If
                                            intLen += strTemp.Length
                                        End If
                                        If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Then
                                            'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "/正常"
                                        Else
                                            strTemp += "/NOR"
                                        End If
                                        Else
                                            'Ver2.0.7.M (保安庁)
                                        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                            strTemp += "正常"
                                        Else
                                            strTemp += "NOR"
                                        End If
                                        End If
                                        If .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Then
                                            strTemp += "/"
                                        End If
                                        If .ValveAiAoLoUse = 1 Then
                                            strTemp += strL
                                        End If
                                        If .ValveAiAoLoLoUse = 1 Then
                                            If .ValveAiAoLoUse = 1 Then
                                                strTemp += "/" & strLL
                                            Else
                                                strTemp += strLL
                                            End If
                                        End If

                                        CHStr.Status = strTemp

                                    End If

                                    If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Or .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Or .ValveAiAoSensorFailUse = 1 Then
                                        CHStr.AL = "o"
                                    Else
                                        CHStr.AL = ""
                                    End If

                                    ''FU ADDRESS(OUT)
                                    CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)
                                    If .ValveAiAoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                        'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                        CHStr.AL = "o"

                                        CHStr.AlmInf(9).Value = Val(.ValveAiAoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                        'CHStr.AlmInf(9).Value = .ValveAiAoFeedback                                                                                          ''Value
                                        CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                        CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                        CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                        CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY
                                    End If

                                    ''Delay タイマー切替
                                    strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                    If strTemp = "m" Then
                                        For i As Integer = 0 To 4
                                            If CHStr.AlmInf(i).Delay <> "" Then
                                                CHStr.AlmInf(i).Delay += strTemp
                                            End If
                                        Next
                                        If CHStr.AlmInf(9).Delay <> "" Then
                                            CHStr.AlmInf(9).Delay += strTemp
                                        End If
                                    End If

                                    If CHStr.OUTAdd <> "" Then
                                        CHStr.OUTSIG = "AO"
                                    End If

                                    ''仮設定
                                    ''Range -------------------------------------------------------
                                    CHStr.Range = mPrtDmyCheck(CHStr.Range, .DummyRangeScale)

                                    ''Value -------------------------------------------------------
                                    CHStr.AlmInf(0).Value = mPrtDmyCheck(CHStr.AlmInf(0).Value, .DummyValueHH)    ''EXT.G HH
                                    CHStr.AlmInf(1).Value = mPrtDmyCheck(CHStr.AlmInf(1).Value, .DummyValueH)     ''EXT.G H
                                    CHStr.AlmInf(2).Value = mPrtDmyCheck(CHStr.AlmInf(2).Value, .DummyValueL)     ''EXT.G L
                                    CHStr.AlmInf(3).Value = mPrtDmyCheck(CHStr.AlmInf(3).Value, .DummyValueLL)    ''EXT.G LL
                                    CHStr.AlmInf(4).Value = mPrtDmyCheck(CHStr.AlmInf(4).Value, .DummyValueSF)    ''EXT.G SF

                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(0).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(0).ExtGrp, .DummyExtGrHH)  ''EXT.G HH
                                    CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyExtGrH)   ''EXT.G H
                                    CHStr.AlmInf(2).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(2).ExtGrp, .DummyExtGrL)   ''EXT.G L
                                    CHStr.AlmInf(3).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(3).ExtGrp, .DummyExtGrLL)  ''EXT.G LL
                                    CHStr.AlmInf(4).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(4).ExtGrp, .DummyExtGrSF)  ''EXT.G SF

                                    ''G Repose 1 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep1, .DummyGRep1HH) ''G.Rep1 HH
                                    CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyGRep1H)  ''G.Rep1 H
                                    CHStr.AlmInf(2).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep1, .DummyGRep1L)  ''G.Rep1 L
                                    CHStr.AlmInf(3).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep1, .DummyGRep1LL) ''G.Rep1 LL

                                    ''G Repose 2 --------------------------------------------------
                                    CHStr.AlmInf(0).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep2, .DummyGRep2HH) ''G.Rep2 HH
                                    CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyGRep2H)  ''G.Rep2 H
                                    CHStr.AlmInf(2).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep2, .DummyGRep2L)  ''G.Rep2 L
                                    CHStr.AlmInf(3).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep2, .DummyGRep2LL) ''G.Rep2 LL

                                    ''Delay -------------------------------------------------------
                                    CHStr.AlmInf(0).Delay = mPrtDmyCheck(CHStr.AlmInf(0).Delay, .DummyDelayHH)     ''Delay HH
                                    CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyDelayH)      ''Delay H
                                    CHStr.AlmInf(2).Delay = mPrtDmyCheck(CHStr.AlmInf(2).Delay, .DummyDelayL)      ''Delay L
                                    CHStr.AlmInf(3).Delay = mPrtDmyCheck(CHStr.AlmInf(3).Delay, .DummyDelayLL)     ''Delay LL
                                    CHStr.AlmInf(4).Delay = mPrtDmyCheck(CHStr.AlmInf(4).Delay, .DummyDelaySF)     ''Delay SF

                                    ''STATUS
                                    If .DummyStaNmHH Or .DummyStaNmH Or .DummyStaNmL Or .DummyStaNmLL Or .DummyStaNmSF Then
                                        CHStr.Status = mPrtDmyCheck(CHStr.Status, True)
                                    End If

                                    CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS

                                    ''FA -------------------------------------------------------
                                    CHStr.AlmInf(9).Value = mPrtDmyCheck(CHStr.AlmInf(9).Value, .DummyFaTimeV)        ''FA TIMER
                                    CHStr.AlmInf(9).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(9).ExtGrp, .DummyFaExtGr)      ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep1, .DummyFaGrep1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep2, .DummyFaGrep2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = mPrtDmyCheck(CHStr.AlmInf(9).Delay, .DummyFaDelay)

                                Case gCstCodeChDataTypeValveAO_4_20 ''AO
                                    ''Decimal Position --------------------------------------------
                                    intDecimalP = .ValveAiAoDecimalPosition
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                    ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                    ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                    If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                        CHStr.Range = ""
                                    Else
                                        dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                        dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                        CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                    End If

                                    ''FU ADDRESS(OUT)
                                    CHStr.OUTAdd = gConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)

                                    'CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                    'CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                    'CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                    'CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY

                                    If CHStr.OUTAdd <> "" Then
                                        CHStr.OUTSIG = "AO"
                                    End If

                                    ''仮設定
                                    ''Range -------------------------------------------------------
                                    CHStr.Range = mPrtDmyCheck(CHStr.Range, .DummyRangeScale)
                                    CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS

                                Case gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt      ''DO

                                    ''FU ADDRESS(OUT)
                                    CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)

                                    'CHStr.AlmInf(9).ExtGrp = IIf(.ValveDiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveDiDoAlarmExtGroup)                 ''EXT.G
                                    'CHStr.AlmInf(9).GrpRep1 = IIf(.ValveDiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveDiDoAlarmGroupRepose1)    ''G.Rep1
                                    'CHStr.AlmInf(9).GrpRep2 = IIf(.ValveDiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveDiDoAlarmGroupRepose2)    ''G.Rep2
                                    'CHStr.AlmInf(9).Delay = IIf(.ValveDiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveDiDoAlarmDelay)                    ''DELAY

                                    'Ver2.0.7.2
                                    'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                    'If CHStr.OUTAdd <> "" Then
                                    If .ValveDiDoControl = 1 Then
                                        CHStr.OUTSIG = "DOP"
                                    Else
                                        CHStr.OUTSIG = "DOM"
                                    End If
                                    'End If

                                    If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Then
                                        CHStr.SIGType = "J"
                                        If .ValveDiDoPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                            CHStr.OUTAdd = "JACOM-"
                                        Else
                                            CHStr.OUTAdd = "JACOM-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                        End If

                                    End If

                                    If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then
                                        CHStr.SIGType = "J"
                                        If .ValveDiDoPin = gCstCodeChNotSetFuPin Then
                                            CHStr.OUTAdd = "JACOM55-"
                                        Else
                                            CHStr.OUTAdd = "JACOM55-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                        End If

                                    End If

                                    ''仮設定
                                    CHStr.OUTAdd = mPrtDmyCheck(CHStr.OUTAdd, .DummyOutFuAddress)                     ''OUT ADDRESS

                            End Select

                            ''ワークCH
                            If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                                CHStr.SIGType = "W"
                            End If



                    Case gCstCodeChTypeComposite    ''コンポジット
                            ''INSIG, SIGTYPE
                            CHStr.SIGType = ""
                            If .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevel Then              '' 代表ステータス
                                CHStr.INSIG = "DC1"

                                ''EXT Group ---------------------------------------------------
                                CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                                ''G Repose 1 ---------------------------------------------------
                                CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                                ''G Repose 2 ---------------------------------------------------
                                CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                                ''Delay --------------------------------------------------------
                                CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                                ''Status ------------------------------------------------------
                                strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                'Ver2.0.7.L
                                'If strTemp.Length > 8 Then
                                If LenB(strTemp) > 8 Then
                                    'CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                                    CHStr.Status = MidB(strTemp, 0, 8).Trim & "/" & MidB(strTemp, 8).Trim
                                Else
                                    CHStr.Status = Trim(strTemp)
                                End If

                                ''デジタルコンポジット設定テーブルインデックス ----------------
                                intCompIndex = .CompositeTableIndex
                                CHStr.AL = ""

                                With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                    For i As Integer = 0 To 8
                                        If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                            CHStr.AL = "o"
                                        End If
                                    Next

                                End With


                                ''仮設定
                                CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCommonExtGroup)
                                CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCommonGroupRepose1)
                                CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCommonGroupRepose2)
                                CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCommonDelay)
                                CHStr.Status = mPrtDmyCheck(CHStr.Status, .DummyCommonStatusName)

                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevelIndevi Then    '' 個別ステータス
                                CHStr.INSIG = "DC2"

                                ''デジタルコンポジット設定テーブルインデックス ----------------
                                intCompIndex = .CompositeTableIndex
                                CHStr.Status = ""
                                intStatusExist = 0
                                CHStr.AL = ""

                                With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                    For i As Integer = 0 To 8
                                        ''EXT Group ---------------------------------------------------
                                        CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                        ''G Repose 1 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                        ''G Repose 2 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                        ''Delay --------------------------------------------------------
                                        CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                        ''Status ------------------------------------------------------
                                        If intStatusExist = 1 Then
                                            CHStr.Status += "/"
                                        End If
                                        If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                            CHStr.Status += .udtCompInf(i).strStatusName
                                            intStatusExist = 1
                                        Else
                                            CHStr.Status += ""
                                        End If

                                        If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                            CHStr.AL = "o"
                                        End If
                                    Next

                                End With

                                ''仮設定
                                CHStr.AlmInf(0).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(0).ExtGrp, .DummyCmpStatus1ExtGr)
                                CHStr.AlmInf(0).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep1, .DummyCmpStatus1GRep1)
                                CHStr.AlmInf(0).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(0).GrpRep2, .DummyCmpStatus1GRep2)
                                CHStr.AlmInf(0).Delay = mPrtDmyCheck(CHStr.AlmInf(0).Delay, .DummyCmpStatus1Delay)

                                CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCmpStatus2ExtGr)
                                CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCmpStatus2GRep1)
                                CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCmpStatus2GRep2)
                                CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCmpStatus2Delay)

                                CHStr.AlmInf(2).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(2).ExtGrp, .DummyCmpStatus3ExtGr)
                                CHStr.AlmInf(2).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep1, .DummyCmpStatus3GRep1)
                                CHStr.AlmInf(2).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(2).GrpRep2, .DummyCmpStatus3GRep2)
                                CHStr.AlmInf(2).Delay = mPrtDmyCheck(CHStr.AlmInf(2).Delay, .DummyCmpStatus3Delay)

                                CHStr.AlmInf(3).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(3).ExtGrp, .DummyCmpStatus4ExtGr)
                                CHStr.AlmInf(3).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep1, .DummyCmpStatus4GRep1)
                                CHStr.AlmInf(3).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(3).GrpRep2, .DummyCmpStatus4GRep2)
                                CHStr.AlmInf(3).Delay = mPrtDmyCheck(CHStr.AlmInf(3).Delay, .DummyCmpStatus4Delay)

                                CHStr.AlmInf(4).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(4).ExtGrp, .DummyCmpStatus5ExtGr)
                                CHStr.AlmInf(4).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(4).GrpRep1, .DummyCmpStatus5GRep1)
                                CHStr.AlmInf(4).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(4).GrpRep2, .DummyCmpStatus5GRep2)
                                CHStr.AlmInf(4).Delay = mPrtDmyCheck(CHStr.AlmInf(4).Delay, .DummyCmpStatus5Delay)

                                CHStr.AlmInf(5).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(5).ExtGrp, .DummyCmpStatus6ExtGr)
                                CHStr.AlmInf(5).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(5).GrpRep1, .DummyCmpStatus6GRep1)
                                CHStr.AlmInf(5).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(5).GrpRep2, .DummyCmpStatus6GRep2)
                                CHStr.AlmInf(5).Delay = mPrtDmyCheck(CHStr.AlmInf(5).Delay, .DummyCmpStatus6Delay)

                                CHStr.AlmInf(6).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(6).ExtGrp, .DummyCmpStatus7ExtGr)
                                CHStr.AlmInf(6).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(6).GrpRep1, .DummyCmpStatus7GRep1)
                                CHStr.AlmInf(6).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(6).GrpRep2, .DummyCmpStatus7GRep2)
                                CHStr.AlmInf(6).Delay = mPrtDmyCheck(CHStr.AlmInf(6).Delay, .DummyCmpStatus7Delay)

                                CHStr.AlmInf(7).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(7).ExtGrp, .DummyCmpStatus8ExtGr)
                                CHStr.AlmInf(7).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(7).GrpRep1, .DummyCmpStatus8GRep1)
                                CHStr.AlmInf(7).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(7).GrpRep2, .DummyCmpStatus8GRep2)
                                CHStr.AlmInf(7).Delay = mPrtDmyCheck(CHStr.AlmInf(7).Delay, .DummyCmpStatus8Delay)

                                CHStr.AlmInf(8).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(8).ExtGrp, .DummyCmpStatus9ExtGr)
                                CHStr.AlmInf(8).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(8).GrpRep1, .DummyCmpStatus9GRep1)
                                CHStr.AlmInf(8).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(8).GrpRep2, .DummyCmpStatus9GRep2)
                                CHStr.AlmInf(8).Delay = mPrtDmyCheck(CHStr.AlmInf(8).Delay, .DummyCmpStatus9Delay)

                                CHStr.AlmInf(9).Value = mPrtDmyCheck(CHStr.AlmInf(9).Value, .DummyFaTimeV)        ''FA TIMER
                                CHStr.AlmInf(9).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(9).ExtGrp, .DummyFaExtGr)      ''EXT.G
                                CHStr.AlmInf(9).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep1, .DummyFaGrep1)    ''G.Rep1
                                CHStr.AlmInf(9).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(9).GrpRep2, .DummyFaGrep2)    ''G.Rep2
                                CHStr.AlmInf(9).Delay = mPrtDmyCheck(CHStr.AlmInf(9).Delay, .DummyFaDelay)        ''DELAY

                                If .DummyCmpStatus1StaNm Or .DummyCmpStatus2StaNm Or .DummyCmpStatus3StaNm Or .DummyCmpStatus4StaNm Or _
                                   .DummyCmpStatus5StaNm Or .DummyCmpStatus6StaNm Or .DummyCmpStatus7StaNm Or .DummyCmpStatus8StaNm Then
                                    CHStr.Status = mPrtDmyCheck(CHStr.Status, True)
                                End If

                            End If

                            ''ワークCH
                            If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                                CHStr.SIGType = "W"
                            End If

                            ''Delay タイマー切替
                            strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                            If strTemp = "m" Then
                                For i As Integer = 0 To 8
                                    If CHStr.AlmInf(i).Delay <> "" Then
                                        CHStr.AlmInf(i).Delay += strTemp
                                    End If
                                Next
                            End If


                    Case gCstCodeChTypePulse        ''パルス
                            ''INSIG, SIGTYPE
                            CHStr.SIGType = ""
                            Select Case .udtChCommon.shtData
                                Case gCstCodeChDataTypePulseTotal1_1
                                    CHStr.INSIG = "PU"
                                Case gCstCodeChDataTypePulseTotal1_10
                                    CHStr.INSIG = "P1"
                                Case gCstCodeChDataTypePulseTotal1_100
                                    CHStr.INSIG = "P2"
                                Case gCstCodeChDataTypePulseDay1_1
                                    CHStr.INSIG = "PUD"
                                Case gCstCodeChDataTypePulseDay1_10
                                    CHStr.INSIG = "P1D"
                                Case gCstCodeChDataTypePulseDay1_100
                                    CHStr.INSIG = "P2D"
                                Case gCstCodeChDataTypePulseRevoTotalHour
                                    CHStr.INSIG = "RH"
                                Case gCstCodeChDataTypePulseRevoTotalMin
                                    CHStr.INSIG = "R2"
                                Case gCstCodeChDataTypePulseRevoDayHour
                                    CHStr.INSIG = "RHD"
                                Case gCstCodeChDataTypePulseRevoDayMin
                                    CHStr.INSIG = "R2D"
                                Case gCstCodeChDataTypePulseRevoLapHour
                                    CHStr.INSIG = "RHL"
                                Case gCstCodeChDataTypePulseRevoLapMin
                                    CHStr.INSIG = "R2L"
                                Case gCstCodeChDataTypePulseExtDev
                                    CHStr.INSIG = "PU"      '' Ver1.11.8.5 2016.11.10 "RH" → "PU"
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDev   '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                                    'Ver2.0.1.9 CHG
                                    'CHStr.INSIG = "RH"
                                    'Ver2.0.7.E RH
                                    'CHStr.INSIG = "R2"
                                    CHStr.INSIG = "RH"
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDevTotalMin    '' Ver1.12.0.1 2017.01.13 
                                    'CHStr.INSIG = "R2"
                                    'CHStr.INSIG = "R2T" 'Ver2.0.5.9 ↑とかぶるため、打開策としてT付
                                    CHStr.INSIG = "R2"  'Ver2.0.6.0 正しくは、「R2」である。上が違う
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDevDayHour     '' Ver1.12.0.1 2017.01.13 
                                    CHStr.INSIG = "RHD"
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDevDayMin      '' Ver1.12.0.1 2017.01.13
                                    CHStr.INSIG = "R2D"
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDevLapHour     '' Ver1.12.0.1 2017.01.13 
                                    CHStr.INSIG = "RHL"
                                    CHStr.SIGType = "R"
                                Case gCstCodeChDataTypePulseRevoExtDevLapMin      '' Ver1.12.0.1 2017.01.13 
                                    CHStr.INSIG = "R2L"
                                    CHStr.SIGType = "R"
                            End Select

                            ''ワークCH
                            If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                                CHStr.SIGType = "W"
                            End If

                            ''EXT Group ---------------------------------------------------
                            CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                            ''G Repose 1 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                            ''G Repose 2 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                            ''Delay --------------------------------------------------------
                            CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                            ''Delay タイマー切替
                            strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                            If strTemp = "m" Then
                                If CHStr.AlmInf(1).Delay <> "" Then
                                    CHStr.AlmInf(1).Delay += strTemp
                                End If
                            End If

                            ''Status -----------------------------------------------------
                            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                CHStr.Status = cmbStatus.Text
                            Else
                                strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                                'Ver2.0.7.L
                                'If strTemp.Length > 8 Then
                                If LenB(strTemp) > 8 Then
                                    'CHStr.Status = "NOR/" & strTemp.Substring(0, 8).Trim
                                    'Ver2.0.7.M (保安庁)
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    CHStr.Status = "正常/" & MidB(strTemp, 0, 8).Trim
                                Else
                                    CHStr.Status = "NOR/" & MidB(strTemp, 0, 8).Trim
                                End If
                                Else
                                    'Ver2.0.7.M (保安庁)
                                If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                    CHStr.Status = "正常/" & Trim(strTemp)
                                Else
                                    CHStr.Status = "NOR/" & Trim(strTemp)
                                End If
                            End If

                            End If

                            If .udtChCommon.shtData >= gCstCodeChDataTypePulseTotal1_1 And .udtChCommon.shtData <= gCstCodeChDataTypePulseDay1_100 Or _
                               .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                                ''Decimal Position --------------------------------------------
                                intDecimalP = .PulseDecPoint
                                If intDecimalP = 0 Then
                                    'Ver2.0.6.5 9が7個
                                    'Ver2.0.7.E DecPoint無しは9が8個
                                    CHStr.Range = "99999999"  '"9999999" '"99999999"
                                    strDecimalFormat = ""

                                    'Ver2.0.7.7
                                    Dim strRangeTemp As String = ""
                                    'パルス時STRINGの数値に応じた下一桁を0にする
                                    If .PulseString > 0 Then
                                        For i As Integer = 0 To (CHStr.Range.Length - 1) - .PulseString Step 1
                                            strRangeTemp = strRangeTemp & CHStr.Range.Substring(i, 1)
                                        Next i
                                        For i As Integer = 0 To .PulseString - 1 Step 1
                                            strRangeTemp = strRangeTemp & "0"
                                        Next i
                                    Else
                                        strRangeTemp = CHStr.Range
                                    End If
                                    CHStr.Range = strRangeTemp
                                Else
                                    If intDecimalP <= 6 Then
                                        CHStr.Range = ".".PadRight(intDecimalP + 1, "9"c)
                                        strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                                    Else
                                        CHStr.Range = ".".PadRight(7, "9"c)
                                        strDecimalFormat = "0.".PadRight(8, "0"c)
                                    End If
                                End If

                                CHStr.Range = CHStr.Range.PadLeft(8, "9"c)

                                If .PulseUse = 1 Then
                                    dblValue = .PulseValue / (10 ^ intDecimalP)      ''Value H
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AlmInf(1).Value = ""
                                    CHStr.AL = ""
                                End If
                            Else
                                ''Decimal Position --------------------------------------------
                                intDecimalP = .RevoDecPoint
                                If intDecimalP = 0 Then
                                    'Ver2.0.6.5 9が7個
                                    'Ver2.0.7.E DecPoint無しは9が8個
                                    CHStr.Range = "99999999"    '"9999999" '"99999999"
                                    strDecimalFormat = ""
                                Else
                                    CHStr.Range = "99999.59"
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                                End If

                                If .RevoUse = 1 Then        ''Value H
                                    '' Ver1.9.1 2015.12.22  積算CHのｱﾗｰﾑ設定値の印字が異なる不具合修正
                                    If intDecimalP = 2 Then     '' 時分
                                        dblValue = ((.RevoValue And &HFFFFFF00) >> 8)                       ''時
                                        dblValue = dblValue + (.RevoValue And &HFF) / (10 ^ intDecimalP)    ''時 + 分(小数点以下値)
                                    Else
                                        dblValue = .RevoValue
                                    End If

                                    If dblValue = 0 Then
                                        CHStr.AlmInf(1).Value = ""
                                    Else
                                        CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                    End If
                                    ''//
                                    ''dblValue = .RevoValue / (10 ^ intDecimalP)      ''Value H
                                    ''CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AlmInf(1).Value = ""
                                    CHStr.AL = ""
                                End If
                            End If

                            ''仮設定
                            CHStr.AlmInf(1).Value = mPrtDmyCheck(CHStr.AlmInf(1).Value, .DummyValueH)
                            CHStr.AlmInf(1).ExtGrp = mPrtDmyCheck(CHStr.AlmInf(1).ExtGrp, .DummyCommonExtGroup)
                            CHStr.AlmInf(1).GrpRep1 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep1, .DummyCommonGroupRepose1)
                            CHStr.AlmInf(1).GrpRep2 = mPrtDmyCheck(CHStr.AlmInf(1).GrpRep2, .DummyCommonGroupRepose2)
                            CHStr.AlmInf(1).Delay = mPrtDmyCheck(CHStr.AlmInf(1).Delay, .DummyCommonDelay)
                            CHStr.Status = mPrtDmyCheck(CHStr.Status, .DummyStaNmH)

                            'Ver2.0.7.H PID対応
                    Case gCstCodeChTypePID       'PIDは全てPIDとする
                            ''INSIG, SIGTYPE
                            CHStr.SIGType = ""
                            Select Case .udtChCommon.shtData
                                Case gCstCodeChDataTypePID_1_AI1_5
                                    'CHStr.INSIG = "AI"
                                    CHStr.INSIG = "PID"
                                Case gCstCodeChDataTypePID_2_AI4_20
                                    If .udtChCommon.shtSignal = 2 Then
                                        'CHStr.INSIG = "PT"
                                        CHStr.INSIG = "PID"
                                    Else
                                        'CHStr.INSIG = "AI"
                                        CHStr.INSIG = "PID"
                                    End If

                                Case gCstCodeChDataTypePID_3_Pt100_2, gCstCodeChDataTypePID_4_Pt100_3
                                    'CHStr.INSIG = "TR"
                                    CHStr.INSIG = "PID"
                                Case gCstCodeChDataTypePID_5_AI_K
                                    'CHStr.INSIG = "K"
                                    CHStr.INSIG = "PID"
                                Case Else
                                    'それ以外
                                    'CHStr.INSIG = "AI"
                                    CHStr.INSIG = "PID"
                            End Select

                            ''ワークCH
                            If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                                CHStr.SIGType = "W"
                            End If

                            ''Decimal Position --------------------------------------------
                            intDecimalP = .PidDecimalPosition
                            strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                            ''Range -------------------------------------------------------
                            ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                            If .PidRangeLow = 0 And .PidRangeHigh = 0 Then
                                CHStr.Range = ""
                            Else
                                If .udtChCommon.shtData = gCstCodeChDataTypePID_3_Pt100_2 Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypePID_4_Pt100_3 Then

                                    ''Range Type(2,3線式)     2014.05.19
                                    'dblLowValue = .AnalogRangeLow
                                    'dblHiValue = .
                                    'dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                    'dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                    'Ver2.0.7.B ２、３線式の小数点表記修正
                                    'dblLowValue = .AnalogRangeLow
                                    'dblHiValue = .AnalogRangeHigh
                                    'CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)
                                    'Ver2.0.7.F ２、３線式の小数点表記変更
                                    dblLowValue = .PidRangeLow
                                    dblHiValue = .PidRangeHigh
                                    dblLowValue = .PidRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .PidRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)

                                Else
                                    ''Range (線式以外)

                                    dblLowValue = .PidRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .PidRangeHigh / (10 ^ intDecimalP)

                                    If gBitCheck(.udtChCommon.shtFlag1, 5) Then
                                        CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & nCenter.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  'Range

                                    ElseIf gBitCheck(.udtChCommon.shtFlag1, 8) Then     '' Ver1.11.9.3 2016.11.26 P/S表示対応
                                        CHStr.Range = "P/S" & dblHiValue.ToString(strDecimalFormat)

                                    ElseIf gBitCheck(.udtChCommon.shtFlag1, 9) Then     'Ver2.0.7.9 A/F対応
                                        CHStr.Range = "A/F" & dblHiValue.ToString(strDecimalFormat)

                                    Else
                                        CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                    End If

                                End If

                                'Ver2.0.0.4
                                'グリーンマーク(ノーマルレンジ)対応
                                '設定アリの場合、「G」を付ける
                                If (.PidNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .PidNormalHigh <> 0) Or _
                                    (.PidNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .PidNormalLow <> 0) Then
                                    'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない　場所は、Remarksの頭に追加とする（仮）
                                    If g_bytGreenMarkPrint = 1 Then
                                        CHStr.Remarks = "G:" & CHStr.Remarks
                                    End If
                                End If

                            End If

                            ''Value -------------------------------------------------------
                            If .PidHiHiUse = 0 Then      ''Use HH アラーム無し
                                CHStr.AlmInf(0).Value = ""
                            Else
                                dblValue = .PidHiHiValue / (10 ^ intDecimalP)    ''Value HH
                                CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                            End If

                            If .PidHiUse = 0 Then        ''Use H  アラーム無し
                                CHStr.AlmInf(1).Value = ""
                            Else
                                dblValue = .PidHiValue / (10 ^ intDecimalP)      ''Value H
                                CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                            End If

                            If .PidLoUse = 0 Then        ''Use L  アラーム無し
                                CHStr.AlmInf(2).Value = ""
                            Else
                                dblValue = .PidLoValue / (10 ^ intDecimalP)      ''Value L
                                CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                            End If

                            If .PidLoLoUse = 0 Then      ''Use LL アラーム無し
                                CHStr.AlmInf(3).Value = ""
                            Else
                                dblValue = .PidLoLoValue / (10 ^ intDecimalP)    ''Value LL
                                CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                            End If

                            'Ver2.0.0.0 2016.12.06 ｾﾝｻｰﾌｪｲﾙ対応
                            'NotUse=空白
                            'Useだがｾﾝｻｰﾌｪｲﾙ無し=N
                            'Under=U
                            'Over=O
                            'Under,Over両方=o
                            If .PidSensorFailUse = 0 Then
                                CHStr.AlmInf(4).Value = ""
                            Else
                                If gBitCheck(.PidDisplay3, 1) And gBitCheck(.PidDisplay3, 2) Then
                                    '両方
                                    CHStr.AlmInf(4).Value = "o"
                                Else
                                    If gBitCheck(.PidDisplay3, 1) Then
                                        'Underのみ
                                        CHStr.AlmInf(4).Value = "UDR"
                                    Else
                                        If gBitCheck(.PidDisplay3, 2) Then
                                            'Overのみ
                                            CHStr.AlmInf(4).Value = "OVR"
                                        Else
                                            'Useだが無し
                                            CHStr.AlmInf(4).Value = "NON"
                                        End If
                                    End If
                                End If
                            End If


                            ''EXT Group ---------------------------------------------------
                            CHStr.AlmInf(0).ExtGrp = IIf(.PidHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .PidHiHiExtGroup)                ''EXT.G HH
                            CHStr.AlmInf(1).ExtGrp = IIf(.PidHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .PidHiExtGroup)                    ''EXT.G H
                            CHStr.AlmInf(2).ExtGrp = IIf(.PidLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .PidLoExtGroup)                    ''EXT.G L
                            CHStr.AlmInf(3).ExtGrp = IIf(.PidLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .PidLoLoExtGroup)                ''EXT.G LL
                            CHStr.AlmInf(4).ExtGrp = IIf(.PidSensorFailExtGroup = gCstCodeChAnalogExtGroupNothing, "", .PidSensorFailExtGroup)    ''EXT.G SF

                            ''G Repose 1 --------------------------------------------------
                            CHStr.AlmInf(0).GrpRep1 = IIf(.PidHiHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .PidHiHiGroupRepose1)   ''G.Rep1 HH
                            CHStr.AlmInf(1).GrpRep1 = IIf(.PidHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .PidHiGroupRepose1)       ''G.Rep1 H
                            CHStr.AlmInf(2).GrpRep1 = IIf(.PidLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .PidLoGroupRepose1)       ''G.Rep1 L
                            CHStr.AlmInf(3).GrpRep1 = IIf(.PidLoLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .PidLoLoGroupRepose1)   ''G.Rep1 LL

                            ''G Repose 2 --------------------------------------------------
                            CHStr.AlmInf(0).GrpRep2 = IIf(.PidHiHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .PidHiHiGroupRepose2)   ''G.Rep2 HH
                            CHStr.AlmInf(1).GrpRep2 = IIf(.PidHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .PidHiGroupRepose2)       ''G.Rep2 H
                            CHStr.AlmInf(2).GrpRep2 = IIf(.PidLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .PidLoGroupRepose2)       ''G.Rep2 L
                            CHStr.AlmInf(3).GrpRep2 = IIf(.PidLoLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .PidLoLoGroupRepose2)   ''G.Rep2 LL

                            ''Delay -------------------------------------------------------
                            CHStr.AlmInf(0).Delay = IIf(.PidHiHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .PidHiHiDelay)                     ''Delay HH
                            CHStr.AlmInf(1).Delay = IIf(.PidHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .PidHiDelay)                         ''Delay H
                            CHStr.AlmInf(2).Delay = IIf(.PidLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .PidLoDelay)                         ''Delay L
                            CHStr.AlmInf(3).Delay = IIf(.PidLoLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .PidLoLoDelay)                     ''Delay LL
                            CHStr.AlmInf(4).Delay = IIf(.PidSensorFailDelay = gCstCodeChAnalogDelayTimerNothing, "", .PidSensorFailDelay)         ''Delay SF

                            ''Delay タイマー切替
                            strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                            If strTemp = "m" Then
                                For i As Integer = 0 To 4
                                    If CHStr.AlmInf(i).Delay <> "" Then
                                        CHStr.AlmInf(i).Delay += strTemp
                                    End If
                                Next
                            End If

                            ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                            '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                If .udtChCommon.shtStatus = &H43 Then     '' LOW/NOR/HIGHならば差し替え
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        CHStr.Status = "正常/高"
                                    Else
                                        CHStr.Status = "NOR/HIGH"
                                    End If
                                End If
                            End If
                        Else
                            strHH = gGetString(.PidHiHiStatusInput)     ''特殊コード対応
                            strH = gGetString(.PidHiStatusInput)        ''特殊コード対応
                            strL = gGetString(.PidLoStatusInput)        ''特殊コード対応
                            strLL = gGetString(.PidLoLoStatusInput)     ''特殊コード対応

                            '' 2015.11.18  Ver1.8.1  ｽﾃｰﾀｽは未定の場合もあるので、表示方法変更
                            If LenB(strHH) = 0 And LenB(strH) = 0 And LenB(strL) = 0 And LenB(strLL) = 0 Then
                                strTemp = ""
                            Else
                                '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                    '' Ver1.9.8 2016.02.20 ｽﾃｰﾀｽﾁｪｯｸ方法変更
                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "正常/" & strL
                                            Else
                                                strTemp = "正常/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "正常/" & strL & "/" & strLL
                                            Else
                                                strTemp = "正常/" & strH & "/" & strHH
                                            End If
                                        End If
                                    Else
                                        If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                            If LenB(strH) = 0 Then
                                                strTemp = "NOR/" & strL
                                            Else
                                                strTemp = "NOR/" & strH
                                            End If
                                        Else            '' NOR/HIGH/HH
                                            If LenB(strHH) = 0 Then
                                                strTemp = "NOR/" & strL & "/" & strLL
                                            Else
                                                strTemp = "NOR/" & strH & "/" & strHH
                                            End If
                                        End If
                                    End If
                                Else
                                    strTemp = ""    '' Ver1.11.5 2016.09.06  初期化追加

                                    '' Ver1.8.6.2  2015.12.04  ﾌﾗｸﾞは参照せずにｽﾃｰﾀｽが設定されていれば表示する
                                    ''                          設定値が決まっていなくてもｽﾃｰﾀｽのみ決まっていることがあるので
                                    ''If .AnalogLoLoUse = 1 And LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                    If LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                        strTemp += strLL & "/"
                                    Else
                                        strTemp = ""
                                    End If

                                    ''If .AnalogLoUse = 1 And LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                    If LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                        strTemp += strL & "/"
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                                        strTemp += "正常/"
                                    Else
                                        strTemp += "NOR/"
                                    End If

                                    ''If .AnalogHiUse = 1 And LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                    If LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                        strTemp += strH & "/"
                                    End If

                                    ''If .AnalogHiHiUse = 1 And LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                    If LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                        strTemp += strHH
                                    End If

                                    'Ver2.0.7.M (保安庁)
                                    'If strTemp = "NOR/" Then    '' NORのみならばｽﾃｰﾀｽ未定とする
                                    If strTemp = "NOR/" Or strTemp = "正常/" Then    'NORのみならばｽﾃｰﾀｽ未定とする
                                        strTemp = ""
                                    Else
                                        '' 文字列の最後尾ならば"/"を削除する
                                        nLen = LenB(strTemp)
                                        'Ver2.0.7.L
                                        'If strTemp.Substring(nLen - 1) = "/" Then
                                        If MidB(strTemp, nLen - 1) = "/" Then
                                            'strTemp = strTemp.Remove(nLen - 1)
                                            strTemp = MidB(strTemp, 0, nLen - 1)
                                        End If
                                    End If
                                End If
                            End If

                            CHStr.Status = strTemp

                        End If

                            If .PidHiHiUse = 1 Or .PidHiUse = 1 Or .PidLoUse = 1 Or .PidLoLoUse = 1 Or .PidSensorFailUse = 1 Then
                                '排ガスリポーズはアラームではないので除外（フラグは必要)
                                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    CHStr.AL = ""
                                Else
                                    CHStr.AL = "o"
                                End If
                            Else
                                CHStr.AL = ""
                            End If

                            'FU ADDRESS(OUT)
                            CHStr.OUTAdd = mPrtConvFuAddress(.PidOutFuNo, .PidOutPortNo, .PidOutPin)
                            If CHStr.OUTAdd <> "" Then
                                CHStr.OUTSIG = "AO"
                            End If
                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

        End Try

    End Sub

#Region "Ver2.0.0.7 Output取得関数"
    'OUT系全CHNo格納関数：起動時に一回呼ぶのみ
    Private Sub subSetAllOutCH()
        aryOutCHNo = New ArrayList

        '>>>OUT設定テーブルと、AndOrテーブルから
        '出力チャンネル設定の単体CHに入っているか探す
        With gudt.SetChOutput
            For i As Integer = 0 To UBound(.udtCHOutPut) Step 1
                '0なら処理しない
                If .udtCHOutPut(i).shtChid <> 0 Then
                    'タイプがCHデータ
                    If .udtCHOutPut(i).bytType = gCstCodeFuOutputChTypeCh Then
                        '0以外なら格納
                        aryOutCHNo.Add(CType(.udtCHOutPut(i).shtChid, Integer))
                    Else
                        'タイプが論理CH
                        For j As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr) Step 1
                            If gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid <> 0 Then
                                '格納
                                aryOutCHNo.Add(CType(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid, Integer))
                            End If
                        Next j
                    End If
                End If
            Next i
        End With

        '>>>SeqSetテーブルから
        With gudt.SetSeqSet
            For i = 0 To UBound(.udtDetail) Step 1
                'シーケンスIDがゼロではないこと
                If .udtDetail(i).shtId <> 0 Then
                    'OUTChが1～9999の範囲なら格納
                    If .udtDetail(i).shtOutChid > 0 And .udtDetail(i).shtOutChid < 10000 Then
                        aryOutCHNo.Add(CType(.udtDetail(i).shtOutChid, Integer))
                    End If
                    '8CHのCHが1～9999の範囲なら格納
                    For j = 0 To UBound(.udtDetail(i).udtInput) Step 1
                        If .udtDetail(i).udtInput(j).shtChid > 0 And .udtDetail(i).udtInput(j).shtChid < 10000 Then
                            aryOutCHNo.Add(CType(.udtDetail(i).udtInput(j).shtChid, Integer))
                        End If
                    Next j
                End If
            Next i
        End With

    End Sub


    'CHnoOutput設定されているか否か戻す
    Private Function fnGetOrAnd(pintCHNO As Integer) As String
        Dim strRet As String = ""

        If pintCHNO = 608 Then
            Dim debuga As Integer = 0
        End If

        '配列に存在すれば、「o」をなければ空白を戻す
        If aryOutCHNo.Contains(pintCHNO) = True Then
            strRet = "o"
        End If

        Return strRet
    End Function
#End Region

#End Region

#Region "FUアドレス作成(印刷用)"
    '--------------------------------------------------------------------
    ' 機能      : FUアドレスを連結する(印刷用)
    ' 返り値    : FU Address
    ' 引き数    : ARG1 - (I ) FU 番号
    '           : ARG2 - (I ) FU ポート番号
    '           : ARG3 - (I ) FU 計測点番号
    ' 機能説明  : FU番号が0から20の数値型のFUアドレスを、標記型のFUアドレスに変換する
    ' 履歴      : 新規作成　ver.1.4.0 2011.10.13
    '--------------------------------------------------------------------
    Private Function mPrtConvFuAddress(ByVal hFuNo As Integer, _
                                       ByVal hPortNo As Integer, _
                                       ByVal hPin As Integer) As String

        Try

            Dim strFuAddress As String = ""

            If gGet2Byte(hFuNo) = gCstCodeChNotSetFuNo Then
                mPrtConvFuAddress = ""
                Exit Function
            End If

            If gGet2Byte(hFuNo) = gCstCodeChNotSetFuNoByte Then
                mPrtConvFuAddress = ""
                Exit Function
            End If

            Select Case hFuNo
                Case 0 : strFuAddress = "FCU -"
                Case 1 : strFuAddress = "FU 1-"
                Case 2 : strFuAddress = "FU 2-"
                Case 3 : strFuAddress = "FU 3-"
                Case 4 : strFuAddress = "FU 4-"
                Case 5 : strFuAddress = "FU 5-"
                Case 6 : strFuAddress = "FU 6-"
                Case 7 : strFuAddress = "FU 7-"
                Case 8 : strFuAddress = "FU 8-"
                Case 9 : strFuAddress = "FU 9-"
                Case 10 : strFuAddress = "FU 10-"
                Case 11 : strFuAddress = "FU 11-"
                Case 12 : strFuAddress = "FU 12-"
                Case 13 : strFuAddress = "FU 13-"
                Case 14 : strFuAddress = "FU 14-"
                Case 15 : strFuAddress = "FU 15-"
                Case 16 : strFuAddress = "FU 16-"
                Case 17 : strFuAddress = "FU 17-"
                Case 18 : strFuAddress = "FU 18-"
                Case 19 : strFuAddress = "FU 19-"
                Case 20 : strFuAddress = "FU 20-"
                Case Else : strFuAddress = "   "
            End Select

            strFuAddress += hPortNo.ToString & "-"
            strFuAddress += Format(hPin, "00")
            'strFuAddress += hPin.ToString

            mPrtConvFuAddress = strFuAddress

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "仮設定判別"
    '--------------------------------------------------------------------
    ' 機能      : 仮設定判別
    ' 返り値    : 文字列
    ' 引き数    : ARG1 - (I ) 文字列(元)
    '           : ARG2 - (I ) 仮設定フラグ
    ' 機能説明  : CHリストに"#"付きで表示するか、非表示とするか判別
    ' 履歴      : 新規作成　ver.1.4.0 2011.10.13
    '--------------------------------------------------------------------
    Private Function mPrtDmyCheck(ByVal strInput As String, ByVal hInfoFlg As Boolean) As String

        Try
            Dim strTemp As String = ""

            If mFlgDmy = True Then      ''仮設定を"#"付きで表示
                If hInfoFlg = True Then ''仮設定
                    strTemp = "#" & strInput
                Else
                    strTemp = strInput
                End If
            Else                        ''仮設定非表示
                If hInfoFlg = True Then ''仮設定
                    strTemp = ""
                Else
                    strTemp = strInput
                End If

            End If

            Return strTemp

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#End Region



#Region "ｺﾝﾊﾞｲﾝ仕様ﾌｫｰﾏｯﾄ 印刷ﾁｪｯｸ"
    '--------------------------------------------------------------------
    ' 機能      : ｺﾝﾊﾞｲﾝ仕様ﾌｫｰﾏｯﾄ 印刷ﾁｪｯｸ
    ' 返り値    : False：標準印刷  True:ｺﾝﾊﾞｲﾝ仕様
    ' 引き数    : ARG1 - (I ) 文字列(元)
    '           : ARG2 - (I ) 仮設定フラグ
    ' 機能説明  : CHリストに"#"付きで表示するか、非表示とするか判別
    ' 履歴      : 新規作成　Ver1.10.5 2016.05.09
    '--------------------------------------------------------------------
    Private Function mChkCombinePrint() As Boolean
        If gudt.SetSystem.udtSysSystem.shtCombineUse = 0 Then   ''コンバイン設定ではない
            Return False
        Else
            If g_bytNotCombine = 1 Then
                Return False
            End If
        End If

        Return True

    End Function
#End Region

End Class
