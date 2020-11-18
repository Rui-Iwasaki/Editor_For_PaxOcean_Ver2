Public Class frmOpsGraphFreeAlignment

#Region "定数定義"

    Private mCstColorNone As Color = Color.DarkGray
    Private mCstColorCounter As Color = Color.SeaGreen
    Private mCstColorBar As Color = Color.DarkKhaki
    Private mCstColorAnalog As Color = Color.Maroon
    Private mCstColorIndicator As Color = Color.CadetBlue

#End Region

#Region "変数定義"

    ''フリーグラフの変更有無判断フラグ（TRUE:変更あり, FALSE:変更なし）
    Private mblnChangeFlg As Boolean = False

    'レイアウト配列
    Private mlblLayout() As System.Windows.Forms.Label

    'Private mintRtn As Integer
    Private mintSelectPos As Integer
    Private mudtFreeGraphOld As gTypSetOpsFreeGraphRec  ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    Private mudtFreeGraphNew As gTypSetOpsFreeGraphRec
    Private mudtMC As gEnmMachineryCargo

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) フリーグラフタイトル構造体
    '           : ARG2 - (IO) グラフリストフォーム
    '           : ARG3 - ( O) フリーグラフ変更有無判断フラグ
    ' 機能説明  : 本画面を表示する
    ' 備考      : 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef udtFreeGraph As gTypSetOpsFreeGraphRec, _
                     ByRef frmOwner As Form, _
                     ByRef hblnChangeFlg As Boolean, _
                     ByVal udtMC As gEnmMachineryCargo)

        Try

            ''引数保存
            Call mudtFreeGraphOld.InitArray()
            Call mudtFreeGraphNew.InitArray()
            Call mCopyStructure(udtFreeGraph, mudtFreeGraphOld)
            Call mCopyStructure(udtFreeGraph, mudtFreeGraphNew)
            mudtMC = udtMC

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            'If mintRtn = 1 Then
            Call mCopyStructure(mudtFreeGraphOld, udtFreeGraph)
            hblnChangeFlg = mblnChangeFlg
            'End If

            Return 'mintRtn

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
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub frmOpsGraphFreeAlignment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''ラベルのコントロール配列作成
            Call mMakeLabelControlArray()

            ''画面表示
            Call mSetDisplay(mudtFreeGraphOld, True)

            ''A1のクリックイベントを呼ぶ
            Call mSetSelectRect(LblLayout1.Tag)

            lblSelectedTag.Text = LblLayout1.Tag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Editクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '              2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try

            ''詳細画面表示
            If frmOpsGraphFreeDetail.gShow(mintSelectPos, mudtFreeGraphNew.udtFreeDetail, Me) = 0 Then

                ''画面表示
                Call mSetDisplay(mudtFreeGraphNew, False)
                Call mSetSelectRect(lblSelectedTag.Text)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Deleteクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '              2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '----------------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            Dim intIndex1 As Integer = 0
            Dim intIndex2 As Integer = 0
            Dim intIndex3 As Integer = 0
            Dim intIndex4 As Integer = 0

            If MessageBox.Show("May I remove selected item? ", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                With mudtFreeGraphNew.udtFreeDetail(mintSelectPos - 1)

                    Select Case .bytType
                        Case gCstCodeOpsFreeGrapTypeCounter

                            '==================
                            ''カウンタ
                            '==================
                            ''位置情報作成
                            intIndex1 = mintSelectPos - 1          ''対象位置
                            intIndex2 = (mintSelectPos - 1) + 1    ''対象位置の１つ右

                            ''データを初期化
                            Call gInitOpsGraphFreeDetail(intIndex1, mudtFreeGraphNew.udtFreeDetail(intIndex1))
                            Call gInitOpsGraphFreeDetail(intIndex2, mudtFreeGraphNew.udtFreeDetail(intIndex2))

                        Case gCstCodeOpsFreeGrapTypeBar

                            '==================
                            ''バー
                            '==================
                            ''位置情報作成
                            intIndex1 = mintSelectPos - 1          ''対象位置
                            intIndex2 = (mintSelectPos - 1) + 8    ''対象位置の１つ下

                            ''データを初期化
                            Call gInitOpsGraphFreeDetail(intIndex1, mudtFreeGraphNew.udtFreeDetail(intIndex1))
                            Call gInitOpsGraphFreeDetail(intIndex2, mudtFreeGraphNew.udtFreeDetail(intIndex2))

                        Case gCstCodeOpsFreeGrapTypeAnalog

                            ''位置情報作成
                            intIndex1 = mintSelectPos - 1              ''対象位置
                            intIndex2 = (mintSelectPos - 1) + 1        ''対象位置の１つ右
                            intIndex3 = (mintSelectPos - 1) + 8        ''対象位置の１つ下
                            intIndex4 = (mintSelectPos - 1) + 8 + 1    ''対象位置の右斜め下

                            ''データを初期化
                            Call gInitOpsGraphFreeDetail(intIndex1, mudtFreeGraphNew.udtFreeDetail(intIndex1))
                            Call gInitOpsGraphFreeDetail(intIndex2, mudtFreeGraphNew.udtFreeDetail(intIndex2))
                            Call gInitOpsGraphFreeDetail(intIndex3, mudtFreeGraphNew.udtFreeDetail(intIndex3))
                            Call gInitOpsGraphFreeDetail(intIndex4, mudtFreeGraphNew.udtFreeDetail(intIndex4))

                        Case gCstCodeOpsFreeGrapTypeIndicator

                            ''位置情報作成
                            intIndex1 = mintSelectPos - 1          ''対象位置

                            ''データを初期化
                            Call gInitOpsGraphFreeDetail(intIndex1, mudtFreeGraphNew.udtFreeDetail(intIndex1))

                    End Select

                    ''画面表示
                    Call mSetDisplay(mudtFreeGraphNew, False)
                    Call mSetSelectRect(lblSelectedTag.Text)

                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フレームクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub LblLayout_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Try

            lblSelectedTag.Text = sender.tag
            Call mSetSelectRect(lblSelectedTag.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フレームダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '              2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '----------------------------------------------------------------------------
    Private Sub LblLayout_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Try

            ''詳細画面表示
            If frmOpsGraphFreeDetail.gShow(mintSelectPos, mudtFreeGraphNew.udtFreeDetail, Me) = 0 Then

                ''画面表示
                Call mSetDisplay(mudtFreeGraphNew, False)
                Call mSetSelectRect(lblSelectedTag.Text)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtFreeGraphNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtFreeGraphNew, mudtFreeGraphOld) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtFreeGraphNew, mudtFreeGraphOld)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''フリーグラフの変更有無判断フラグを有効にする
                mblnChangeFlg = True

                ''更新フラグ設定
                gblnUpdateAll = True
                If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
                If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
                If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1
                If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
                If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

            End If

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
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtFreeGraphNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtFreeGraphNew, mudtFreeGraphOld) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        Call mCopyStructure(mudtFreeGraphNew, mudtFreeGraphOld)

                        ''フリーグラフの変更有無判断フラグを有効にする
                        mblnChangeFlg = True

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
                        If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
                        If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1
                        If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
                        If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                        If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                        If mudtMC = gEnmMachineryCargo.mcMachinery Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                        If mudtMC = gEnmMachineryCargo.mcCargo Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True

                End Select

            End If

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

#Region "入力関連"

    '----------------------------------------------------------------------------
    ' 機能説明  ： GraphTitle KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGraphTitle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGraphTitle.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

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

            ''共通テキスト入力チェック
            If Not gChkInputText(txtGraphTitle, "GraphTitle", True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) シーケンス設定詳細構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsFreeGraphRec)

        Try

            With udtSet

                ''グラフタイトル
                .strGraphTitle = Trim(txtGraphTitle.Text)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) シーケンス設定詳細構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsFreeGraphRec, _
                            ByVal blnRefreshTitle As Boolean)

        Try

            With udtSet

                ''グラフタイトル
                If blnRefreshTitle Then txtGraphTitle.Text = gGetString(.strGraphTitle)

                For i As Integer = LBound(udtSet.udtFreeDetail) To UBound(udtSet.udtFreeDetail)

                    With udtSet.udtFreeDetail(i)

                        Select Case .bytType
                            Case gCstCodeOpsFreeGrapTypeCounter

                                mlblLayout(i).Text = IIf(i + 1 = .bytTopPos, gCstNameOpsFreeGrapTypeCounter, "")
                                mlblLayout(i).BackColor = mCstColorCounter

                            Case gCstCodeOpsFreeGrapTypeBar

                                mlblLayout(i).Text = IIf(i + 1 = .bytTopPos, gCstNameOpsFreeGrapTypeBar, "")
                                mlblLayout(i).BackColor = mCstColorBar

                            Case gCstCodeOpsFreeGrapTypeAnalog

                                mlblLayout(i).Text = IIf(i + 1 = .bytTopPos, gCstNameOpsFreeGrapTypeAnalog, "")
                                mlblLayout(i).BackColor = mCstColorAnalog

                            Case gCstCodeOpsFreeGrapTypeIndicator

                                mlblLayout(i).Text = IIf(i + 1 = .bytTopPos, gCstNameOpsFreeGrapTypeIndicator, "")
                                mlblLayout(i).BackColor = mCstColorIndicator

                            Case gCstCodeOpsFreeGrapTypeNone

                                mlblLayout(i).Text = gCstNameOpsFreeGrapTypeNone
                                mlblLayout(i).BackColor = mCstColorNone

                        End Select

                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コントロール配列作成
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ラベルのコントロール配列を作成する
    '--------------------------------------------------------------------
    Private Sub mMakeLabelControlArray()

        Try

            ''レイアウト クリック用　インスタンス作成
            mlblLayout = New System.Windows.Forms.Label(31) { _
                        LblLayout1, LblLayout2, LblLayout3, LblLayout4, LblLayout5, LblLayout6, LblLayout7, LblLayout8, _
                        LblLayout9, LblLayout10, LblLayout11, LblLayout12, LblLayout13, LblLayout14, LblLayout15, LblLayout16, _
                        LblLayout17, LblLayout18, LblLayout19, LblLayout20, LblLayout21, LblLayout22, LblLayout23, LblLayout24, _
                        LblLayout25, LblLayout26, LblLayout27, LblLayout28, LblLayout29, LblLayout30, LblLayout31, LblLayout32}

            ''イベントハンドラに関連付け
            For i = 0 To 31
                AddHandler mlblLayout(i).MouseClick, AddressOf Me.LblLayout_Click
                AddHandler mlblLayout(i).MouseDoubleClick, AddressOf Me.LblLayout_DoubleClick
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 選択枠設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 対象ラベルのTagプロパティ
    ' 機能説明  : 選択枠を画面に設定する
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mSetSelectRect(ByRef strLabelTag As String)

        Try

            Dim intX As Integer
            Dim intY As Integer

            Dim intWidth As Integer
            Dim intHight As Integer

            Dim strwk() As String
            Dim intPosX As Integer
            Dim intPosY As Integer

            Dim objSender() As Object

            ''Tagに格納されている情報を取得
            ''Tag = 横位置(1-8),縦位置(1-4),通し位置(1-32)
            strwk = strLabelTag.Split(",")

            ''選択位置更新
            mintSelectPos = strwk(2)

            ''横位置、縦位置取得
            intPosX = strwk(0)
            intPosY = strwk(1)

            If mudtFreeGraphNew.udtFreeDetail(mintSelectPos - 1).bytType <> gCstCodeOpsFreeGrapTypeNone Then

                If mintSelectPos <> mudtFreeGraphNew.udtFreeDetail(mintSelectPos - 1).bytTopPos Then
                    objSender = Me.Controls.Find("LblLayout" & mudtFreeGraphNew.udtFreeDetail(mintSelectPos - 1).bytTopPos, True)
                    strwk = objSender(0).tag.Split(",")
                    mintSelectPos = strwk(2)
                    intPosX = strwk(0)
                    intPosY = strwk(1)
                End If

            End If

            Call mGetRectPos(intPosX, intPosY, intX, intY)
            shpRect.Location = New System.Drawing.Point(intX, intY)

            Call mGetRectSize(mudtFreeGraphNew.udtFreeDetail(mintSelectPos - 1).bytType, intWidth, intHight)
            shpRect.Size = New System.Drawing.Size(intWidth, intHight)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 選択枠位置取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 横位置
    ' 　　　    : ARG2 - (I ) 縦位置
    ' 　　　    : ARG3 - (I ) 横座標
    ' 　　　    : ARG4 - (I ) 縦座標
    ' 機能説明  : 選択枠の位置から座標を取得して返す
    '--------------------------------------------------------------------
    Private Sub mGetRectPos(ByVal intPosX As Integer, ByVal intPosY As Integer, ByRef intX As Integer, ByRef intY As Integer)

        Try

            intX = 25 + (104 * (intPosX - 1))
            intY = 13 + (104 * (intPosY - 1))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 選択枠大きさ取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グラフタイプ
    ' 　　　    : ARG2 - (I ) 横幅
    ' 　　　    : ARG3 - (I ) 高さ
    ' 機能説明  : グラフタイプから選択枠の大きさを取得して返す
    '--------------------------------------------------------------------
    Private Sub mGetRectSize(ByVal bytType As Byte, ByRef intWidth As Integer, ByRef intHight As Integer)

        Try

            Select Case bytType
                Case gCstCodeOpsFreeGrapTypeNone
                    intWidth = 104
                    intHight = 104
                Case gCstCodeOpsFreeGrapTypeCounter
                    intWidth = 208
                    intHight = 104
                Case gCstCodeOpsFreeGrapTypeBar
                    intWidth = 104
                    intHight = 208
                Case gCstCodeOpsFreeGrapTypeAnalog
                    intWidth = 208
                    intHight = 208
                Case gCstCodeOpsFreeGrapTypeIndicator
                    intWidth = 104
                    intHight = 104
            End Select

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsFreeGraphRec, ByRef udtTarget As gTypSetOpsFreeGraphRec)

        Try

            ''グラフ番号
            udtTarget.bytGraphNo = udtSource.bytGraphNo

            ''OPS番号
            udtTarget.bytOpsNo = udtSource.bytOpsNo

            ''グラフタイトル
            udtTarget.strGraphTitle = udtSource.strGraphTitle

            ''フリーグラフ詳細
            For i As Integer = 0 To UBound(udtSource.udtFreeDetail)

                udtTarget.udtFreeDetail(i).bytType = udtSource.udtFreeDetail(i).bytType
                udtTarget.udtFreeDetail(i).shtChNo = udtSource.udtFreeDetail(i).shtChNo
                udtTarget.udtFreeDetail(i).bytTopPos = udtSource.udtFreeDetail(i).bytTopPos
                udtTarget.udtFreeDetail(i).bytIndicatorKind = udtSource.udtFreeDetail(i).bytIndicatorKind
                udtTarget.udtFreeDetail(i).shtIndicatorPattern = udtSource.udtFreeDetail(i).shtIndicatorPattern
                udtTarget.udtFreeDetail(i).bytScale = udtSource.udtFreeDetail(i).bytScale
                udtTarget.udtFreeDetail(i).bytColor = udtSource.udtFreeDetail(i).bytColor

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsFreeGraphRec, ByVal udt2 As gTypSetOpsFreeGraphRec) As Boolean

        Try

            ''グラフ番号
            If udt1.bytGraphNo <> udt2.bytGraphNo Then Return False

            ''OPS番号
            If udt1.bytOpsNo <> udt2.bytOpsNo Then Return False

            ''グラフタイトル
            If Not gCompareString(udt1.strGraphTitle, udt2.strGraphTitle) Then Return False

            ''フリーグラフ詳細
            For i As Integer = 0 To UBound(udt2.udtFreeDetail)

                If udt1.udtFreeDetail(i).bytType <> udt2.udtFreeDetail(i).bytType Then Return False
                If udt1.udtFreeDetail(i).bytTopPos <> udt2.udtFreeDetail(i).bytTopPos Then Return False
                If udt1.udtFreeDetail(i).shtChNo <> udt2.udtFreeDetail(i).shtChNo Then Return False
                If udt1.udtFreeDetail(i).bytIndicatorKind <> udt2.udtFreeDetail(i).bytIndicatorKind Then Return False
                If udt1.udtFreeDetail(i).shtIndicatorPattern <> udt2.udtFreeDetail(i).shtIndicatorPattern Then Return False
                If udt1.udtFreeDetail(i).bytScale <> udt2.udtFreeDetail(i).bytScale Then Return False
                If udt1.udtFreeDetail(i).bytColor <> udt2.udtFreeDetail(i).bytColor Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
