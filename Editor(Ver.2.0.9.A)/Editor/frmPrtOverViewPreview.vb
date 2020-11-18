Public Class frmPrtOverViewPreview

#Region "変数定義"

    ''グループ情報作業用
    Private mudtWorkGroupM As gTypSetChGroupSet
    Private mudtWorkGroupC As gTypSetChGroupSet
    Private mudtWork As gTypSetChGroupSet

    ''イメージ
    Private img As Image

    ''ヒストリカル番号
    Private strHisNo As String

    ''シップ番号の表示非表示
    Private mblnShipNo As Boolean

    ''Part選択情報（True:Machinery、False:Cargo）
    Private mblnSelectMachinery As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) HistoryNo名称
    '           : ARG2 - (I ) ShipNo表示フラグ
    '           : ARG3 - (I ) 印刷モード（0:Print、1:Preview）
    '           : ARG4 - (I ) Part選択（True:Machinery、False:Cargo）
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByVal hstrHistoryNo As String, _
                     ByVal hblnShipNo As Boolean, _
                     ByVal hintPrintMode As Integer, _
                     ByVal hblnSelectMach As Boolean)

        Try

            ''引数保存
            mblnShipNo = hblnShipNo
            strHisNo = hstrHistoryNo
            mblnSelectMachinery = hblnSelectMach

            '構造体初期化
            Call mudtWorkGroupM.udtGroup.InitArray()
            Call mudtWorkGroupC.udtGroup.InitArray()
            Call mudtWork.udtGroup.InitArray()

            'チャンネルグループ名称取得
            Call mCopyStructure(gudt.SetChGroupSetM, mudtWorkGroupM)
            Call mCopyStructure(gudt.SetChGroupSetC, mudtWorkGroupC)
            If mblnSelectMachinery Then Call mCopyStructure(gudt.SetChGroupSetM, mudtWork)
            If Not mblnSelectMachinery Then Call mCopyStructure(gudt.SetChGroupSetC, mudtWork)

            ''------------------------
            '' 印刷設定
            ''------------------------
            If hintPrintMode = 0 Then
                ''Print
                Call cmdPrint_Click(cmdPrint, New EventArgs)
                Me.Close()
            Else
                ''本画面表示
                Me.ShowDialog()
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能説明  ： プレビュー画面表示
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub imgPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles imgPreview.Paint

        Try

            'グラフ作成
            Call mDrawGraphics(e.Graphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： PRINTボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   'ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            '印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                'PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = True
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies
                pd.DefaultPageSettings.Margins.Top = 50
                pd.DefaultPageSettings.Margins.Left = 50
                pd.DefaultPageSettings.Margins.Right = 50
                pd.DefaultPageSettings.Margins.Bottom = 50

                '印刷を開始する
                pd.Print()

                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            'グラフ作成
            Call mDrawGraphics(e.Graphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： CLOSEボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Try

            Me.Close()

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
    Private Sub frmPrtOverViewPreview_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能説明  ： グラフ作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mDrawGraphics(ByVal objGraphics As System.Drawing.Graphics)

        Try

            Dim strGroupNo As String = ""
            Dim strcom1 As String = ""
            Dim strcom2 As String = ""
            Dim strcom3 As String = ""
            Dim strShipNoView As String = ""

            '' OVERVIEWタイトル追加　ver.1.4.0 2011.08.01
            If gudt.SetSystem.udtSysSystem.shtCombineUse = 0 Then                                   'コンバイン設定無
                '2015.03.12 印字位置調整 Y:4 → 44→ 37 2015/5/26 T.Ueki
                objGraphics.DrawString("OVERVIEW", gFnt12, Brushes.Black, 492, 37)
            Else                                                                                    'コンバイン設定有
                If mblnSelectMachinery Then
                    '2015.03.12 印字位置調整 Y:4 → 44→ 37 2015/5/26 T.Ueki
                    'Ver2.0.4.9 MACHINERYの文字を消す
                    'objGraphics.DrawString("OVERVIEW(MACHINERY)", gFnt12, Brushes.Black, 492, 37)    'MACHINERY
                    objGraphics.DrawString("OVERVIEW", gFnt12, Brushes.Black, 492, 37)    'MACHINERY
                Else
                    '2015.03.12 印字位置調整 Y:4 → 44→ 37 2015/5/26 T.Ueki
                    'Ver2.0.4.9 CARGOの文字を消す
                    'objGraphics.DrawString("OVERVIEW(CARGO)", gFnt12, Brushes.Black, 492, 37)        'CARGO
                    objGraphics.DrawString("OVERVIEW", gFnt12, Brushes.Black, 492, 37)        'CARGO
                End If
            End If


            'ヒストリカルナンバー表示/非表示確認
            If strHisNo <> "" Then
                'Fontサイズ変更12→9　ver.1.4.0 20110801
                'objGraphics.DrawString("History NO :" & strHisNo, gFnt12, Brushes.Black, 850, 690)
                objGraphics.DrawString("History NO :" & strHisNo, gFnt9, Brushes.Black, 900, 690)
            End If

            'シップ番号表示/非表示確認
            If mblnShipNo = True Then

                'Mach/Carg 船番取得
                If mblnSelectMachinery Then strShipNoView = gudt.SetChGroupSetM.udtGroup.strShipNo.Replace("^", "")
                If Not mblnSelectMachinery Then strShipNoView = gudt.SetChGroupSetC.udtGroup.strShipNo.Replace("^", "")

                '船番表示
                '"S.No."追加、Fontサイズ変更12→9　ver.1.4.0 20110801
                'objGraphics.DrawString("S.No." & strShipNoView, gFnt9, Brushes.Black, 25, 690)
                '2015/5/26 T.Ueki 表示位置修正
                objGraphics.DrawString("S.No." & strShipNoView, gFnt9, Brushes.Black, 25, 710)
            End If

            'グループボックス設定
            For intRowIdx As Integer = 0 To 5

                For intColIdx As Integer = 0 To 5

                    '▼▼▼ 20110705 NEW時iniファイルフォルダに保存してあるMainMenu.datを読み込む ▼▼▼▼▼▼▼▼▼
                    ''グループを使用しているかの判断はここで行う
                    ''グループを使用しているかの判断はチャンネル情報構造体の３０００行を調べる必要がある
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    ''グループCH情報を取得する
                    Call mGetGroupChInfo((6 * intRowIdx + intColIdx), _
                                         strGroupNo, _
                                         strcom1, _
                                         strcom2, _
                                         strcom3)

                    '図形描画
                    '2015.03.12 印字位置調整 Y:27 → 67
                    'Call mDrawGraphicsBoxOutline(objGraphics, 26 + 170 * intColIdx, 67 + 110 * intRowIdx, strGroupNo, strcom1, strcom2, strcom3)

                    'Ver2.0.7.N (新デザイン)OVERVIEW印字のﾎﾞｯｸｽデザインを分岐
                    If g_bytNEWDES = 1 Then
                        '新デザインはｸﾞﾙｰﾌﾟ名称が無い場合、枠線表示しない
                        If strcom1 <> "" Or strcom2 <> "" Or strcom3 <> "" Then
                            Call mDrawGraphicsBoxOutline(objGraphics, 26 + 170 * intColIdx, 59 + 110 * intRowIdx, strGroupNo, strcom1, strcom2, strcom3)
                        End If
                    Else
                        Call mDrawGraphicsBoxOutline(objGraphics, 26 + 170 * intColIdx, 59 + 110 * intRowIdx, strGroupNo, strcom1, strcom2, strcom3)
                    End If
                    '' グループ名称がない場合は中枠非表示　ver.1.4.0 2011.08.01
                    '' 文字列で空文字比較しても空文字でないと認識される
                    ''If strcom1(0) <> Nothing Or strcom2(0) <> Nothing Or strcom3(0) <> Nothing Then
                    If strcom1 <> "" Or strcom2 <> "" Or strcom3 <> "" Then
                        '2015.03.12 印字位置調整 Y:29 → 69
                        'Call mDrawGraphicsBoxOutlineDetail(objGraphics, 28 + 170 * intColIdx, 69 + 110 * intRowIdx)
                        Call mDrawGraphicsBoxOutlineDetail(objGraphics, 28 + 170 * intColIdx, 61 + 110 * intRowIdx)
                    End If


                Next intColIdx

            Next intRowIdx

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループCHの情報を取得する
    ' 引数      ： ARG1 - (I ) グループ表示位置Index
    '           ： ARG2 - ( O) グループ番号
    '           ： ARG3 - ( O) グループ名称 1行目
    '           ： ARG4 - ( O) グループ名称 2行目
    '           ： ARG5 - ( O) グループ名称 3行目
    ' 戻値      ： グループCHの情報を取得する
    '----------------------------------------------------------------------------
    Private Sub mGetGroupChInfo(ByVal hintDispIndex As Integer, _
                                ByRef hstrGroupNo As String, _
                                ByRef hstrCom1 As String, _
                                ByRef hstrCom2 As String, _
                                ByRef hstrCom3 As String)

        Try

            ''引数初期化
            hstrGroupNo = ""
            hstrCom1 = ""
            hstrCom2 = ""
            hstrCom3 = ""

            ''処理中のグループ表示位置Indexを取得（1～36）
            Dim intDispNo As Integer = hintDispIndex + 1

            ''表示するグループ情報の配列番号を取得
            Dim intDispArrayIndex As Integer = mGetGroupChArrayIndex(intDispNo)

            ''グループCH設定情報を表示する
            If intDispArrayIndex <> gCstCodePrintGraphViewExceptionNo Then

                ''グループ情報を取得する
                With mudtWork.udtGroup.udtGroupInfo(intDispArrayIndex)

                    hstrCom1 = .strName1
                    hstrCom2 = .strName2
                    hstrCom3 = .strName3
                    hstrGroupNo = mSetDispPosition(.shtGroupNo)

                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループCHの配列Indexを取得する
    ' 引数      ： ARG1 - (I ) 現在処理中のグループ表示Index
    ' 戻値      ： グループCHの配列Index
    '----------------------------------------------------------------------------
    Private Function mGetGroupChArrayIndex(ByVal hintExecuteDispNo As Integer) As Integer

        Dim intRtn As Integer = 65535

        Try

            For i As Integer = 0 To UBound(mudtWork.udtGroup.udtGroupInfo)

                With mudtWork.udtGroup.udtGroupInfo(i)

                    If .shtDisplayPosition = hintExecuteDispNo Then

                        intRtn = i
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グラフ作成（36個のボックスと表示位置NO）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    '           ： ARG4 - (I ) グループ番号
    '           ： ARG5 - (I ) １行目
    '           ： ARG6 - (I ) ２行目
    '           ： ARG7 - (I ) ３行目
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsBoxOutline(ByVal objGraphics As System.Drawing.Graphics, _
                                        ByVal sngX As Single, _
                                        ByVal sngY As Single, _
                                        ByVal strGrNo As String, _
                                        ByVal strCom1 As String, _
                                        ByVal strCom2 As String, _
                                        ByVal strCom3 As String)

        Try
            '' 外枠
            Dim intWidth As Integer = 160
            Dim intHeight As Integer = 100
            'Ver2.0.7.M (新デザイン)OVERVIEW印字のﾎﾞｯｸｽデザインを分岐
            If g_bytNEWDES = 1 Then
                '左縦線
                objGraphics.DrawLine(Pens.Black, sngX, sngY, sngX, sngY + intHeight)
                '下線
                objGraphics.DrawLine(Pens.Black, sngX, sngY + intHeight, sngX + intWidth, sngY + intHeight)
                '上線1
                objGraphics.DrawLine(Pens.Black, sngX, sngY, sngX + (intWidth - 76), sngY)
                '縦線1
                objGraphics.DrawLine(Pens.Black, sngX + (intWidth - 76), sngY, sngX + (intWidth - 76), sngY + 22)
                '上線2
                objGraphics.DrawLine(Pens.Black, sngX + (intWidth - 76), sngY + 22, sngX + intWidth, sngY + 22)
                '縦線2
                objGraphics.DrawLine(Pens.Black, sngX + intWidth, sngY + 22, sngX + intWidth, sngY + intHeight)
            Else
                objGraphics.DrawRectangle(Pens.Black, sngX, sngY, 160, 100)
            End If

            '' グループ名称がない場合は番号、名称非表示　ver.1.4.0 2011.08.01
            '' 文字列で空文字比較しても空文字でないと認識される
            'If strCom1(0) <> Nothing Or strCom2(0) <> Nothing Or strCom3(0) <> Nothing Then
            If strCom1 <> "" Or strCom2 <> "" Or strCom3 <> "" Then
                ''グループ表示位置Index
                ''グループ番号のみ　ver.1.4.0 2011.08.01
                'objGraphics.DrawString("GroupNo:" & strGrNo, gFnt9, Brushes.Black, sngX + 10, sngY + 5)
                objGraphics.DrawString(strGrNo, gFnt9, Brushes.Black, sngX + 10, sngY + 5)

                ''グループ名称
                If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                    objGraphics.DrawString(strCom1, gFnt9j, Brushes.Black, sngX + 10, sngY + 30)
                    objGraphics.DrawString(strCom2, gFnt9j, Brushes.Black, sngX + 10, sngY + 50)
                    objGraphics.DrawString(strCom3, gFnt9j, Brushes.Black, sngX + 10, sngY + 70)
                Else
                    objGraphics.DrawString(strCom1, gFnt9, Brushes.Black, sngX + 10, sngY + 30)
                    objGraphics.DrawString(strCom2, gFnt9, Brushes.Black, sngX + 10, sngY + 50)
                    objGraphics.DrawString(strCom3, gFnt9, Brushes.Black, sngX + 10, sngY + 70)
                End If
                
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グラフ作成（描画詳細）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsBoxOutlineDetail(ByVal objGraphics As System.Drawing.Graphics, _
                                              ByVal sngX As Single, _
                                              ByVal sngY As Single)

        Try
            'Ver2.0.7.M (新デザイン)OVERVIEW印字のﾎﾞｯｸｽデザインを分岐
            If g_bytNEWDES = 1 Then
                '新デザインは中枠は無し
            Else
                objGraphics.DrawRectangle(Pens.Black, sngX, sngY, 156, 20)
                objGraphics.DrawRectangle(Pens.Black, sngX, sngY + 22, 156, 74)
            End If
            objGraphics.DrawEllipse(Pens.Black, sngX + 90, sngY + 2, 16, 16)
            objGraphics.DrawEllipse(Pens.Black, sngX + 90 + 21, sngY + 2, 16, 16)
            objGraphics.DrawEllipse(Pens.Black, sngX + 90 + 42, sngY + 2, 16, 16)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChGroupSet, _
                               ByRef udtTarget As gTypSetChGroupSet)

        Try

            udtTarget.udtGroup.strDrawNo = udtSource.udtGroup.strDrawNo
            udtTarget.udtGroup.strShipNo = udtSource.udtGroup.strShipNo
            udtTarget.udtGroup.strComment = udtSource.udtGroup.strComment

            For j As Integer = LBound(udtSource.udtGroup.udtGroupInfo) To UBound(udtSource.udtGroup.udtGroupInfo)

                udtTarget.udtGroup.udtGroupInfo(j).shtGroupNo = udtSource.udtGroup.udtGroupInfo(j).shtGroupNo
                udtTarget.udtGroup.udtGroupInfo(j).strName1 = udtSource.udtGroup.udtGroupInfo(j).strName1
                udtTarget.udtGroup.udtGroupInfo(j).strName2 = udtSource.udtGroup.udtGroupInfo(j).strName2
                udtTarget.udtGroup.udtGroupInfo(j).strName3 = udtSource.udtGroup.udtGroupInfo(j).strName3
                udtTarget.udtGroup.udtGroupInfo(j).shtColor = udtSource.udtGroup.udtGroupInfo(j).shtColor
                udtTarget.udtGroup.udtGroupInfo(j).shtDisplayPosition = udtSource.udtGroup.udtGroupInfo(j).shtDisplayPosition

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グループ表示位置
    ' 引数      ： ARG1 - (I ) グループ表示位置Index（1～99）
    ' 戻値      ： グループ表示位置を返す（01～99）
    '--------------------------------------------------------------------
    Private Function mSetDispPosition(ByVal hintGroupNo As Integer) As String

        Dim strRtn As String = ""

        Try

            If hintGroupNo = gCstCodeChGroupDisplayPositionNothing Then
                '表示位置未設定の時は空欄表示
                strRtn = ""
            Else
                ''グループ番号を2桁で統一　ver.1.4.0 2011.08.01
                strRtn = hintGroupNo.ToString("00")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

End Class
