Public Class frmOpsPulldownMenu

#Region "変数定義"

    ''プルダウンメニュー構造体
    Private mudtSetOpsPulldownMenuWork As gTypSetOpsPulldownMenu = Nothing
    Private mudtSetOpsPulldownMenuWorkCarg As gTypSetOpsPulldownMenu = Nothing

    Private mudtSetOpsPulldownMenuNewMach As gTypSetOpsPulldownMenu = Nothing
    Private mudtSetOpsPulldownMenuNewCarg As gTypSetOpsPulldownMenu = Nothing

    ''グラフ設定データ インポート用構造体
    Private mudtGraphData As gTypImportGraphData = Nothing

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    ''前回選択したページタイプ保存（前回値の保存用）
    Private mintGraphTypeMach() As Integer
    Private mintGraphTypeCarg() As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmOpsPulldownMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            Call mInitialArray()

            ''Machinery/Cargoの情報を取得する
            Call mCopyStructure(gudt.SetOpsPulldownMenuM, mudtSetOpsPulldownMenuNewMach)
            Call mCopyStructure(gudt.SetOpsPulldownMenuC, mudtSetOpsPulldownMenuNewCarg)

            ''コンバイン切替をOPS/Ext.VDU切替に変更  2014.03.12
            ''Machinery/Cargoボタン設定
            ''Call gSetCombineControl(optOPS, optVDU)
            optOPS.Checked = True     ''OPS選択
            optOPS.Enabled = True     ''OPSボタン有効
            optVDU.Enabled = True     ''Ext.VDUボタン有効

            ''画面設定
            If optOPS.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, mudtSetOpsPulldownMenuWork)
            If optVDU.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, mudtSetOpsPulldownMenuWork)
            Call mSetDisplay(mudtSetOpsPulldownMenuWork)

            ''背景色設定
            For i As Integer = 0 To grdMenuName.RowCount - 1
                Call mControlSetCount(i)
            Next

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsPulldownMenu_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdMenuName.CurrentCell = Nothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OPS/Ext.VDUボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : パートの切替え
    '--------------------------------------------------------------------
    Private Sub optOPS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOPS.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''グリッドの保留中の変更を全て適用させる
            Call grdMenuName.EndEdit()

            If optOPS.Checked Then ''OPS選択

                ''設定値の取得
                Call mSetStructure(mudtSetOpsPulldownMenuWork)

                ''Ext.VDU情報の退避
                Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewCarg)

                ''OPS情報を作業用構造体に設定
                Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, mudtSetOpsPulldownMenuWork)

                ''画面表示
                Call mSetDisplay(mudtSetOpsPulldownMenuWork)

                ''[SET_COUNT]項目の制御
                For i As Integer = 0 To grdMenuName.RowCount - 1
                    Call mControlSetCount(i)
                Next

            ElseIf optVDU.Checked Then ''Ext.VDU選択

                ''設定値の取得
                Call mSetStructure(mudtSetOpsPulldownMenuWork)

                ''OPS情報の退避
                Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewMach)

                ''Ext.VDU情報を作業用構造体に設定
                Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, mudtSetOpsPulldownMenuWork)

                ''画面表示
                Call mSetDisplay(mudtSetOpsPulldownMenuWork)

                ''[SET_COUNT]項目の制御
                For i As Integer = 0 To grdMenuName.RowCount - 1
                    Call mControlSetCount(i)
                Next

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
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsPulldownMenuWork)

            ''設定値の保存
            If optOPS.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewMach)
            If optVDU.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)

            'Ver2.0.0.4 OSP側のGRAPH,MIMICの内容が変わっているならば ExtVDU側も自動更新
            If blnMach = True Then
                For i As Integer = 0 To UBound(mudtSetOpsPulldownMenuNewMach.udtDetail) Step 1
                    With mudtSetOpsPulldownMenuNewMach.udtDetail(i)
                        Dim strName As String = .strName.Substring(0, 5)
                        If strName = "GRAPH" Or strName = "MIMIC" Then
                            'mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).bytCount = .udtGroup(0).bytCount
                            'For j As Integer = 0 To UBound(.udtGroup(0).udtSub) Step 1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).strName = .udtGroup(0).udtSub(j).strName
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType1 = .udtGroup(0).udtSub(j).SubbytMenuType1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType2 = .udtGroup(0).udtSub(j).SubbytMenuType2
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType3 = .udtGroup(0).udtSub(j).SubbytMenuType3
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType4 = .udtGroup(0).udtSub(j).SubbytMenuType4
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).ViewNo1 = .udtGroup(0).udtSub(j).ViewNo1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).bytKeyCode = .udtGroup(0).udtSub(j).bytKeyCode
                            'Next j
                            'Ver2.0.7.6 詳細データもｺﾋﾟｰ
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).tx = .tx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).ty = .ty
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bx = .bx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).by = .by
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).OPSSTFLG1 = .OPSSTFLG1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).OPSSTFLG2 = .OPSSTFLG2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuNo1 = .bytMenuNo1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare1 = .Spare1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare2 = .Spare2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare3 = .Spare3
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare4 = .Spare4
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare5 = .Spare5
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuType = .bytMenuType
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Yobi1 = .Yobi1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Yobi2 = .Yobi2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuSet = .bytMenuSet
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupviewx = .groupviewx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupviewy = .groupviewy
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupsizex = .groupsizex
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupsizey = .groupsizey

                            'Ver2.0.7.6 Groupもループ
                            For x As Integer = 0 To UBound(.udtGroup) Step 1
                                '詳細もｺﾋﾟｰ
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).bytCount = .udtGroup(x).bytCount
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).strName = .udtGroup(x).strName
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).grouptx = .udtGroup(x).grouptx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupty = .udtGroup(x).groupty
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupbx = .udtGroup(x).groupbx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupby = .udtGroup(x).groupby
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare1 = .udtGroup(x).groupSpare1
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare2 = .udtGroup(x).groupSpare2
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare3 = .udtGroup(x).groupSpare3
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare4 = .udtGroup(x).groupSpare4
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupbytMenuType = .udtGroup(x).groupbytMenuType
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).SubgroupYobi1 = .udtGroup(x).SubgroupYobi1
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).SubgroupYobi2 = .udtGroup(x).SubgroupYobi2
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subviewx = .udtGroup(x).Subviewx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subviewy = .udtGroup(x).Subviewy
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subsizex = .udtGroup(x).Subsizex
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subsizey = .udtGroup(x).Subsizey
                                For j As Integer = 0 To UBound(.udtGroup(x).udtSub) Step 1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).strName = .udtGroup(x).udtSub(j).strName
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType1 = .udtGroup(x).udtSub(j).SubbytMenuType1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType2 = .udtGroup(x).udtSub(j).SubbytMenuType2
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType3 = .udtGroup(x).udtSub(j).SubbytMenuType3
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType4 = .udtGroup(x).udtSub(j).SubbytMenuType4
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).ViewNo1 = .udtGroup(x).udtSub(j).ViewNo1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).bytKeyCode = .udtGroup(x).udtSub(j).bytKeyCode
                                    '
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubMenutx = .udtGroup(x).udtSub(j).SubMenutx
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubMenuty = .udtGroup(x).udtSub(j).SubMenuty
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubMenubx = .udtGroup(x).udtSub(j).SubMenubx
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubMenuby = .udtGroup(x).udtSub(j).SubMenuby
                                Next j
                            Next x

                        End If
                    End With
                Next i
            End If


            blnCarg = mChkStructureEquals(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

            ''データが変更されている場合
            If (blnMach = True) Or (blnCarg = True) Then

                ''変更された場合は設定を更新する
                If blnMach Then Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
                If blnCarg Then Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                If blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                If blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                If blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

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
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''グリッドの保留中の変更を全て適用させる
            Call grdMenuName.EndEdit()

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsPulldownMenuWork)

            ''設定値の保存
            If optOPS.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewMach)
            If optVDU.Checked Then Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)

            'Ver2.0.0.4 OSP側のGRAPH,MIMICの内容が変わっているならば ExtVDU側も自動更新
            If blnMach = True Then
                For i As Integer = 0 To UBound(mudtSetOpsPulldownMenuNewMach.udtDetail) Step 1
                    With mudtSetOpsPulldownMenuNewMach.udtDetail(i)
                        Dim strName As String = .strName.Substring(0, 5)
                        If strName = "GRAPH" Or strName = "MIMIC" Then
                            'mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).bytCount = .udtGroup(0).bytCount
                            'For j As Integer = 0 To UBound(.udtGroup(0).udtSub) Step 1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).strName = .udtGroup(0).udtSub(j).strName
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType1 = .udtGroup(0).udtSub(j).SubbytMenuType1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType2 = .udtGroup(0).udtSub(j).SubbytMenuType2
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType3 = .udtGroup(0).udtSub(j).SubbytMenuType3
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).SubbytMenuType4 = .udtGroup(0).udtSub(j).SubbytMenuType4
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).ViewNo1 = .udtGroup(0).udtSub(j).ViewNo1
                            '    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(0).udtSub(j).bytKeyCode = .udtGroup(0).udtSub(j).bytKeyCode
                            'Next j

                            'Ver2.0.7.6 詳細データもｺﾋﾟｰ
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).tx = .tx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).ty = .ty
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bx = .bx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).by = .by
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).OPSSTFLG1 = .OPSSTFLG1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).OPSSTFLG2 = .OPSSTFLG2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuNo1 = .bytMenuNo1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare1 = .Spare1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare2 = .Spare2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare3 = .Spare3
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare4 = .Spare4
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Spare5 = .Spare5
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuType = .bytMenuType
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Yobi1 = .Yobi1
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).Yobi2 = .Yobi2
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).bytMenuSet = .bytMenuSet
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupviewx = .groupviewx
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupviewy = .groupviewy
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupsizex = .groupsizex
                            mudtSetOpsPulldownMenuNewCarg.udtDetail(i).groupsizey = .groupsizey

                            'Ver2.0.7.6 Groupもループ
                            For x As Integer = 0 To UBound(.udtGroup) Step 1
                                '詳細もｺﾋﾟｰ
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).bytCount = .udtGroup(x).bytCount
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).strName = .udtGroup(x).strName
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).grouptx = .udtGroup(x).grouptx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupty = .udtGroup(x).groupty
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupbx = .udtGroup(x).groupbx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupby = .udtGroup(x).groupby
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare1 = .udtGroup(x).groupSpare1
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare2 = .udtGroup(x).groupSpare2
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare3 = .udtGroup(x).groupSpare3
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupSpare4 = .udtGroup(x).groupSpare4
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).groupbytMenuType = .udtGroup(x).groupbytMenuType
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).SubgroupYobi1 = .udtGroup(x).SubgroupYobi1
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).SubgroupYobi2 = .udtGroup(x).SubgroupYobi2
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subviewx = .udtGroup(x).Subviewx
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subviewy = .udtGroup(x).Subviewy
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subsizex = .udtGroup(x).Subsizex
                                mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).Subsizey = .udtGroup(x).Subsizey
                                For j As Integer = 0 To UBound(.udtGroup(x).udtSub) Step 1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).strName = .udtGroup(x).udtSub(j).strName
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType1 = .udtGroup(x).udtSub(j).SubbytMenuType1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType2 = .udtGroup(x).udtSub(j).SubbytMenuType2
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType3 = .udtGroup(x).udtSub(j).SubbytMenuType3
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).SubbytMenuType4 = .udtGroup(x).udtSub(j).SubbytMenuType4
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).ViewNo1 = .udtGroup(x).udtSub(j).ViewNo1
                                    mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(x).udtSub(j).bytKeyCode = .udtGroup(x).udtSub(j).bytKeyCode
                                Next j
                            Next x

                        End If
                    End With
                Next i
            End If

            blnCarg = mChkStructureEquals(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

            ''データが変更されている場合
            If (blnMach = True) Or (blnCarg = True) Then

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
                        If blnMach = True Then Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
                        If blnCarg = True Then Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                        If blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                        If blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                        If blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

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


#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： メニュータイプを変更した際の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdMenuName_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdMenuName.CellValueChanged

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return ''初期化中の場合
            If e.RowIndex < 0 Or e.RowIndex > grdMenuName.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdMenuName.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合
            'If e.ColumnIndex <> gCstCodeOpsPullDownMenuColNameType Then Exit Sub ''メニュータイプの列以外は処理を抜ける

            If e.ColumnIndex = gCstCodeOpsPullDownMenuColNameType Or e.ColumnIndex = gCstCodeOpsPullDownMenuColNameArrow Then ''メニュータイプの列以外は処理を抜ける

                If e.ColumnIndex = gCstCodeOpsPullDownMenuColNameType Then

                    Dim dgv As DataGridView = CType(sender, DataGridView)

                    If optOPS.Checked Then

                        ''前回値の削除
                        Call mDeleteSetData(mudtSetOpsPulldownMenuWork, mintGraphTypeMach)

                        ''設定値の取得
                        Call mSetStructure(mudtSetOpsPulldownMenuWork)

                        ''画面表示
                        Call mSetDisplay(mudtSetOpsPulldownMenuWork)

                        ''[SET_COUNT]項目の制御
                        Call mControlSetCount(e.RowIndex)

                    ElseIf optVDU.Checked Then

                        ''前回値の削除
                        Call mDeleteSetData(mudtSetOpsPulldownMenuWork, mintGraphTypeCarg)

                        ''設定値の取得
                        Call mSetStructure(mudtSetOpsPulldownMenuWork)

                        ''画面表示
                        Call mSetDisplay(mudtSetOpsPulldownMenuWork)

                        ''[SET_COUNT]項目の制御
                        Call mControlSetCount(e.RowIndex)

                    End If
                Else
                    ' ''[SET_COUNT]項目の制御
                    'Call mControlSetCount(e.RowIndex)




                End If
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdMenuName_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdMenuName.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdMenuName_KeyPress

                ''イベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name.Substring(0, 3) = "txt" Then
                    AddHandler tb.KeyPress, AddressOf grdMenuName_KeyPress
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdMenuName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdMenuName.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdMenuName.CurrentCell.OwningColumn.Name

            ''[SET_COUNT]
            If strColumnName = "txtSetCount" Then
                e.Handled = gCheckTextInput(2, sender, e.KeyChar)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 'SET COUNT'　入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdMenuName_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdMenuName.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'Dim dgv As DataGridView = CType(sender, DataGridView)

            'If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Return

            ' ''[SET_COUNT]：選択されている定数種類によって入力制限処理を分岐
            'Select Case dgv.Rows(e.RowIndex).Cells(1).Value

            '    Case mCstTypeMainOnly, mCstTypeGroup
            '        e.Cancel = gChkTextNumSpan(0, 12, e.FormattedValue)

            '    Case mCstTypeSub
            '        e.Cancel = gChkTextNumSpan(0, 17, e.FormattedValue)

            '    Case mCstTypeNothing
            '        ''何もしない

            'End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： >>ボタン クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdMenuName_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdMenuName.CellContentClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdMenuName.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdMenuName.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合
            If grdMenuName.CurrentCell.ColumnIndex <> gCstCodeOpsPullDownMenuColNameArrow Then Return ''[>>]ボタン以外は処理を抜ける

            ''--------------------------------------------------------------------
            '' 入力チェック
            ''--------------------------------------------------------------------
            Select Case CCInt(grdMenuName.Rows(e.RowIndex).Cells("cmbType").Value)
                Case gCstCodeOpsPullDownTypeGroup       ''GROUP
                    If Not gChkInputNum(grdMenuName.Rows(e.RowIndex).Cells("txtSetCount"), 1, 12, "Set Count", e.RowIndex + 1, False, True) Then Return
                Case gCstCodeOpsPullDownTypeSub         ''SUB
                    If Not gChkInputNum(grdMenuName.Rows(e.RowIndex).Cells("txtSetCount"), 1, 17, "Set Count", e.RowIndex + 1, False, True) Then Return
                Case gCstCodeOpsPullDownTypeMainOnly    ''MAIN ONLY
                    If Not gChkInputNum(grdMenuName.Rows(e.RowIndex).Cells("txtSetCount"), 1, 1, "Set Count", e.RowIndex + 1, False, True) Then Return
            End Select

            ''[Set Count] 取得
            Dim intCount As Integer = CCInt(gConvNullToZero(grdMenuName.Rows(e.RowIndex).Cells("txtSetCount").Value))

            ''--------------------------------------------------------------------
            '' 画面表示（SetCount：0 もしくは Type：Nothing の時は画面表示なし）
            ''--------------------------------------------------------------------
            If intCount <> 0 And _
               CCInt(grdMenuName.Rows(e.RowIndex).Cells("cmbType").Value) <> gCstCodeOpsPullDownTypeNothing Then

                ''選択したタイプの画面表示
                Call mDisplayDetailGraph(e.RowIndex, intCount)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbMainMenu" : Column1.MaxDropDownItems = 14
            Dim Column2 As New DataGridViewComboBoxColumn : Column2.Name = "cmbType"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtSetCount"
            Dim Column4 As New DataGridViewButtonColumn : Column4.Name = "cmd"

            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdMenuName

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4)
                .AllowUserToResizeColumns = False                                               ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "MAIN MENU NAME" : .Columns(0).Width = 200
                .Columns(1).HeaderText = "TYPE" : .Columns(1).Width = 120
                .Columns(2).HeaderText = "SET COUNT" : .Columns(2).Width = 90
                .Columns(3).HeaderText = "" : .Columns(3).Width = 60
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 13
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

                ''コンボボックス初期設定
                Call gSetComboBox(Column1, gEnmComboType.ctOpsPulldownColumn1)
                Call gSetComboBox(Column2, gEnmComboType.ctOpsPulldownColumn2)

                ''＞＞ボタン　初期値
                Column4.UseColumnTextForButtonValue = True
                Column4.Text = ">>"

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdMenuName)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim i As Integer
            Dim strMenuNo As String = ""
            Dim strMenuType As String = ""

            ''グリッドの保留中の変更を全て適用させる
            Call grdMenuName.EndEdit()

            With grdMenuName

                For i = 0 To .RowCount - 1

                    ''設定値の取得
                    strMenuNo = gGetString(.Rows(i).Cells("cmbMainMenu").Value)
                    strMenuType = gGetString(.Rows(i).Cells("cmbType").Value)

                    ''-----------------------------
                    '' 入力項目確認
                    ''-----------------------------
                    If strMenuNo <> "0" And strMenuType <> "0" Then
                        ''OK
                    ElseIf strMenuNo = "0" And strMenuType = "0" Then
                        ''OK
                    Else

                        If strMenuNo = "0" Then
                            Call MessageBox.Show("Please set 'Main Menu Name' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Function
                        End If

                    End If

                    ''-----------------------------
                    '' SetCount入力レンジ確認
                    ''-----------------------------
                    Select Case strMenuType

                        Case gCstCodeOpsPullDownTypeGroup

                            If Not gChkInputNum(grdMenuName.Rows(i).Cells("txtSetCount"), 1, 12, "Set Count", i + 1, False, True) Then Return False

                        Case gCstCodeOpsPullDownTypeSub

                            If Not gChkInputNum(grdMenuName.Rows(i).Cells("txtSetCount"), 1, 17, "Set Count", i + 1, False, True) Then Return False

                        Case gCstCodeOpsPullDownTypeMainOnly

                            If Not gChkInputNum(grdMenuName.Rows(i).Cells("txtSetCount"), 0, 1, "Set Count", i + 1, False, True) Then Return False

                        Case Else

                            ''チェックなし

                    End Select

                Next

            End With

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsPulldownMenu)

        Try

            ''grdMenuName_CellValueChangedイベントの実行を止める
            mblnInitFlg = True

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    .strName = MojiMake(grdMenuName.Rows(i).Cells("cmbMainMenu").FormattedValue, 12)
                    .bytMenuType = CCbyte(grdMenuName.Rows(i).Cells("cmbType").Value)

                    If .bytMenuType = 2 Or .bytMenuType = 3 Then 'SUBメニューの場合
                        udtSet.udtDetail(i).udtGroup(0).bytCount = CCbyte(grdMenuName.Rows(i).Cells("txtSetCount").Value)
                    Else
                        .bytMenuSet = CCbyte(grdMenuName.Rows(i).Cells("txtSetCount").Value)
                    End If

                End With

            Next

            Call ViewData(mudtSetOpsPulldownMenuWork)

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsPulldownMenu)

        Try

            mblnInitFlg = True

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    grdMenuName.Rows(i).Cells("cmbType").Value = .bytMenuType.ToString

                    'If .bytMenuType = 2 Then 'SUBメニューの場合
                    If .bytMenuType = 2 Or .bytMenuType = 3 Then 'SUBメニュー, MAIN ONLYの場合    2013.12.14
                        grdMenuName.Rows(i).Cells("txtSetCount").Value = udtSet.udtDetail(i).udtGroup(0).bytCount
                    Else
                        grdMenuName.Rows(i).Cells("txtSetCount").Value = .bytMenuSet
                    End If
                    'grdMenuName.Rows(i).Cells("txtSetCount").Value = .bytMenuSet
                    grdMenuName.Rows(i).Cells("cmbMainMenu").Value = MenuStrName(.strName).ToString

                End With

            Next

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG2 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsPulldownMenu, _
                               ByRef udtTarget As gTypSetOpsPulldownMenu)

        Try

            For i As Integer = 0 To UBound(udtSource.udtDetail)

                ''メインメニュー
                udtTarget.udtDetail(i).strName = udtSource.udtDetail(i).strName             ''メインメニュー名称
                udtTarget.udtDetail(i).tx = udtSource.udtDetail(i).tx                       ''メインメニューの動作開始地点（左上X座標）
                udtTarget.udtDetail(i).ty = udtSource.udtDetail(i).ty                       ''メインメニューの動作開始地点（左上Y座標)
                udtTarget.udtDetail(i).bx = udtSource.udtDetail(i).bx                       ''メインメニューの動作開始地点（右下X座標）
                udtTarget.udtDetail(i).by = udtSource.udtDetail(i).by                       ''メインメニューの動作開始地点（右下Y座標）
                udtTarget.udtDetail(i).OPSSTFLG1 = udtSource.udtDetail(i).OPSSTFLG1         ''OPS禁止フラグ
                udtTarget.udtDetail(i).OPSSTFLG2 = udtSource.udtDetail(i).OPSSTFLG2         ''OPS禁止フラグ
                udtTarget.udtDetail(i).Spare1 = udtSource.udtDetail(i).Spare1               ''予備1
                udtTarget.udtDetail(i).Spare2 = udtSource.udtDetail(i).Spare2               ''予備2
                udtTarget.udtDetail(i).Spare3 = udtSource.udtDetail(i).Spare3               ''予備3
                udtTarget.udtDetail(i).Spare4 = udtSource.udtDetail(i).Spare4               ''予備4
                udtTarget.udtDetail(i).Spare5 = udtSource.udtDetail(i).Spare5               ''予備5
                udtTarget.udtDetail(i).bytMenuType = udtSource.udtDetail(i).bytMenuType     ''メニュータイプ
                udtTarget.udtDetail(i).Yobi1 = udtSource.udtDetail(i).Yobi1                 ''セレクトされているグループメニュー番号(未使用)
                udtTarget.udtDetail(i).Yobi2 = udtSource.udtDetail(i).Yobi2                 ''セレクトされているグループメニュー番号(保持型)(未使用)
                udtTarget.udtDetail(i).bytMenuSet = udtSource.udtDetail(i).bytMenuSet       ''グループメニューセット数
                udtTarget.udtDetail(i).groupviewx = udtSource.udtDetail(i).groupviewx       ''グループメニューの表示位置X
                udtTarget.udtDetail(i).groupviewy = udtSource.udtDetail(i).groupviewy       ''グループメニューの表示位置Y
                udtTarget.udtDetail(i).groupsizex = udtSource.udtDetail(i).groupsizex       ''グループメニューの横サイズ位置
                udtTarget.udtDetail(i).groupsizey = udtSource.udtDetail(i).groupsizey       ''グループメニューの縦サイズ位置

                ''サブグループ
                For j As Integer = 0 To UBound(udtSource.udtDetail(i).udtGroup)
                    udtTarget.udtDetail(i).udtGroup(j).strName = udtSource.udtDetail(i).udtGroup(j).strName                     ''サブメニュー名称
                    udtTarget.udtDetail(i).udtGroup(j).grouptx = udtSource.udtDetail(i).udtGroup(j).grouptx                     ''グループメニューの動作開始地点（左上X座標）
                    udtTarget.udtDetail(i).udtGroup(j).groupty = udtSource.udtDetail(i).udtGroup(j).groupty                     ''グループメニューの動作開始地点（左上Y座標)
                    udtTarget.udtDetail(i).udtGroup(j).groupbx = udtSource.udtDetail(i).udtGroup(j).groupbx                     ''グループメニューの動作開始地点（右下X座標）
                    udtTarget.udtDetail(i).udtGroup(j).groupby = udtSource.udtDetail(i).udtGroup(j).groupby                     ''グループメニューの動作開始地点（右下Y座標）
                    udtTarget.udtDetail(i).udtGroup(j).groupSpare1 = udtSource.udtDetail(i).udtGroup(j).groupSpare1             ''予備1
                    udtTarget.udtDetail(i).udtGroup(j).groupSpare2 = udtSource.udtDetail(i).udtGroup(j).groupSpare2             ''予備2
                    udtTarget.udtDetail(i).udtGroup(j).groupSpare3 = udtSource.udtDetail(i).udtGroup(j).groupSpare3             ''予備3
                    udtTarget.udtDetail(i).udtGroup(j).groupSpare4 = udtSource.udtDetail(i).udtGroup(j).groupSpare4             ''予備4
                    udtTarget.udtDetail(i).udtGroup(j).groupbytMenuType = udtSource.udtDetail(i).udtGroup(j).groupbytMenuType   ''メニュータイプ(処理項目・未使用))
                    udtTarget.udtDetail(i).udtGroup(j).SubgroupYobi1 = udtSource.udtDetail(i).udtGroup(j).SubgroupYobi1         ''セレクトされているサブメニュー番号(未使用)
                    udtTarget.udtDetail(i).udtGroup(j).SubgroupYobi2 = udtSource.udtDetail(i).udtGroup(j).SubgroupYobi2         ''セレクトされているサブメニュー番号(保持型)(未使用)
                    udtTarget.udtDetail(i).udtGroup(j).bytCount = udtSource.udtDetail(i).udtGroup(j).bytCount                   ''サブメニュー数
                    udtTarget.udtDetail(i).udtGroup(j).Subviewx = udtSource.udtDetail(i).udtGroup(j).Subviewx                   ''サブメニューの表示位置X
                    udtTarget.udtDetail(i).udtGroup(j).Subviewy = udtSource.udtDetail(i).udtGroup(j).Subviewy                   ''サブメニューの表示位置Y
                    udtTarget.udtDetail(i).udtGroup(j).Subsizex = udtSource.udtDetail(i).udtGroup(j).Subsizex                   ''サブメニューの横サイズ位置
                    udtTarget.udtDetail(i).udtGroup(j).Subsizey = udtSource.udtDetail(i).udtGroup(j).Subsizey                   ''サブメニューの縦サイズ位置


                    ''サブメニュー
                    For k As Integer = 0 To UBound(udtSource.udtDetail(i).udtGroup(j).udtSub)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).strName = udtSource.udtDetail(i).udtGroup(j).udtSub(k).strName                         ''サブメニュー名称
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1         ''メニュータイプ(Bラベル処理項目1)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2         ''メニュータイプ(Bラベル処理項目2)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3         ''メニュータイプ(Bラベル処理項目3)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4         ''メニュータイプ(Bラベル処理項目4)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1                       ''画面モード（未使用）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2                       ''現在操作可能な画面の表示位置（未使用）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode = udtSource.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode                   ''キーコード
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4                       ''予備

                        '画面設定数の反転
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1          ''画面番号0(データを反転する）
                        'udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = DataExchange(gGet2Byte(udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1))           ''画面番号0(データを反転する）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2                         ''画面番号1（未使用）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3                         ''画面番号2（未使用）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4                         ''画面番号3（未使用）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx                     ''サブメニューの動作開始地点（左上X座標）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty                     ''サブメニューの動作開始地点（左上Y座標)
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx                     ''サブメニューの動作開始地点（右下X座標）
                        udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby                     ''サブメニューの動作開始地点（右下Y座標）
                    Next k

                Next j

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違あり、False:相違なし
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsPulldownMenu, _
                                         ByVal udt2 As gTypSetOpsPulldownMenu) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtDetail)

                ''メインメニュー
                'If udt1.udtDetail(i).strName <> udt2.udtDetail(i).strName Then Return False ''メインメニュー名称
                If Not gCompareString(udt1.udtDetail(i).strName, udt2.udtDetail(i).strName) Then Return True ''メインメニュー名称   2014.03.12
                If udt1.udtDetail(i).tx <> udt2.udtDetail(i).tx Then Return True ''メインメニューの動作開始地点（左上X座標）
                If udt1.udtDetail(i).ty <> udt2.udtDetail(i).ty Then Return True ''メインメニューの動作開始地点（左上Y座標)
                If udt1.udtDetail(i).bx <> udt2.udtDetail(i).bx Then Return True ''メインメニューの動作開始地点（右下X座標）
                If udt1.udtDetail(i).by <> udt2.udtDetail(i).by Then Return True ''メインメニューの動作開始地点（右下Y座標）
                If udt1.udtDetail(i).OPSSTFLG1 <> udt2.udtDetail(i).OPSSTFLG1 Then Return True ''OPS禁止フラグ1
                If udt1.udtDetail(i).OPSSTFLG2 <> udt2.udtDetail(i).OPSSTFLG2 Then Return True ''OPS禁止フラグ2
                'If udt1.udtDetail(i).bytMenuNo1 <> udt2.udtDetail(i).bytMenuNo1 Then Return False ''エディター専用    2015.01.19 コメント
                If udt1.udtDetail(i).Spare1 <> udt2.udtDetail(i).Spare1 Then Return True ''予備1
                If udt1.udtDetail(i).Spare2 <> udt2.udtDetail(i).Spare2 Then Return True ''予備2
                If udt1.udtDetail(i).Spare3 <> udt2.udtDetail(i).Spare3 Then Return True ''予備3
                If udt1.udtDetail(i).Spare4 <> udt2.udtDetail(i).Spare4 Then Return True ''予備4
                If udt1.udtDetail(i).Spare5 <> udt2.udtDetail(i).Spare5 Then Return True ''予備5
                If udt1.udtDetail(i).bytMenuType <> udt2.udtDetail(i).bytMenuType Then Return True ''メニュータイプ
                If udt1.udtDetail(i).Yobi1 <> udt2.udtDetail(i).Yobi1 Then Return True ''セレクトされているグループメニュー番号(未使用)
                If udt1.udtDetail(i).Yobi2 <> udt2.udtDetail(i).Yobi2 Then Return True ''セレクトされているグループメニュー番号(保持型)(未使用)
                If udt1.udtDetail(i).bytMenuSet <> udt2.udtDetail(i).bytMenuSet Then Return True ''グループメニューセット数
                If udt1.udtDetail(i).groupviewx <> udt2.udtDetail(i).groupviewx Then Return True ''グループメニューの表示位置X
                If udt1.udtDetail(i).groupviewy <> udt2.udtDetail(i).groupviewy Then Return True ''グループメニューの表示位置Y
                If udt1.udtDetail(i).groupsizex <> udt2.udtDetail(i).groupsizex Then Return True ''グループメニューの横サイズ位置
                If udt1.udtDetail(i).groupsizey <> udt2.udtDetail(i).groupsizey Then Return True ''グループメニューの縦サイズ位置

                ''サブグループ
                For j As Integer = 0 To UBound(gudt.SetOpsPulldownMenuM.udtDetail(i).udtGroup)

                    'If udt1.udtDetail(i).udtGroup(j).strName <> udt2.udtDetail(i).udtGroup(j).strName Then Return False ''サブメニュー名称
                    If Not gCompareString(udt1.udtDetail(i).udtGroup(j).strName, udt2.udtDetail(i).udtGroup(j).strName) Then Return True ''サブメニュー名称       2014.03.12
                    If udt1.udtDetail(i).udtGroup(j).grouptx <> udt2.udtDetail(i).udtGroup(j).grouptx Then Return True ''グループメニューの動作開始地点（左上X座標）
                    If udt1.udtDetail(i).udtGroup(j).groupty <> udt2.udtDetail(i).udtGroup(j).groupty Then Return True ''グループメニューの動作開始地点（左上Y座標)
                    If udt1.udtDetail(i).udtGroup(j).groupbx <> udt2.udtDetail(i).udtGroup(j).groupbx Then Return True ''グループメニューの動作開始地点（右下X座標）
                    If udt1.udtDetail(i).udtGroup(j).groupby <> udt2.udtDetail(i).udtGroup(j).groupby Then Return True ''グループメニューの動作開始地点（右下Y座標）
                    If udt1.udtDetail(i).udtGroup(j).groupSpare1 <> udt2.udtDetail(i).udtGroup(j).groupSpare1 Then Return True ''予備1
                    If udt1.udtDetail(i).udtGroup(j).groupSpare2 <> udt2.udtDetail(i).udtGroup(j).groupSpare2 Then Return True ''予備2
                    If udt1.udtDetail(i).udtGroup(j).groupSpare3 <> udt2.udtDetail(i).udtGroup(j).groupSpare3 Then Return True ''予備3
                    If udt1.udtDetail(i).udtGroup(j).groupSpare4 <> udt2.udtDetail(i).udtGroup(j).groupSpare4 Then Return True ''予備4
                    If udt1.udtDetail(i).udtGroup(j).groupbytMenuType <> udt2.udtDetail(i).udtGroup(j).groupbytMenuType Then Return True ''メニュータイプ(処理項目・未使用))
                    If udt1.udtDetail(i).udtGroup(j).SubgroupYobi1 <> udt2.udtDetail(i).udtGroup(j).SubgroupYobi1 Then Return True ''セレクトされているサブメニュー番号(未使用)
                    If udt1.udtDetail(i).udtGroup(j).SubgroupYobi2 <> udt2.udtDetail(i).udtGroup(j).SubgroupYobi2 Then Return True ''セレクトされているサブメニュー番号(保持型)(未使用)
                    If udt1.udtDetail(i).udtGroup(j).bytCount <> udt2.udtDetail(i).udtGroup(j).bytCount Then Return True ''サブメニュー数
                    If udt1.udtDetail(i).udtGroup(j).Subviewx <> udt2.udtDetail(i).udtGroup(j).Subviewx Then Return True ''サブメニューの表示位置X
                    If udt1.udtDetail(i).udtGroup(j).Subviewy <> udt2.udtDetail(i).udtGroup(j).Subviewy Then Return True ''サブメニューの表示位置Y
                    If udt1.udtDetail(i).udtGroup(j).Subsizex <> udt2.udtDetail(i).udtGroup(j).Subsizex Then Return True ''サブメニューの横サイズ位置
                    If udt1.udtDetail(i).udtGroup(j).Subsizey <> udt2.udtDetail(i).udtGroup(j).Subsizey Then Return True ''サブメニューの縦サイズ位置


                    ''サブメニュー
                    For k As Integer = 0 To UBound(udt1.udtDetail(i).udtGroup(j).udtSub)
                        'If udt1.udtDetail(i).udtGroup(j).udtSub(k).strName <> udt2.udtDetail(i).udtGroup(j).udtSub(k).strName Then Return False ''サブメニュー名称
                        If Not gCompareString(udt1.udtDetail(i).udtGroup(j).udtSub(k).strName, udt2.udtDetail(i).udtGroup(j).udtSub(k).strName) Then Return True ''サブメニュー名称       2014.03.12
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 Then Return True ''メニュータイプ(Bラベル処理項目1)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 Then Return True ''メニュータイプ(Bラベル処理項目2)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 Then Return True ''メニュータイプ(Bラベル処理項目3)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 Then Return True ''メニュータイプ(Bラベル処理項目4)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 Then Return True ''画面モード（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 Then Return True ''現在操作可能な画面の表示位置（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode <> udt2.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode Then Return True ''キーコード
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 Then Return True ''予備
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 Then Return True ''画面番号0
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 Then Return True ''画面番号1（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 Then Return True ''画面番号2（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 <> udt2.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 Then Return True ''画面番号3（未使用）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx Then Return True ''サブメニューの動作開始地点（左上X座標）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty Then Return True ''サブメニューの動作開始地点（左上Y座標)
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx Then Return True ''サブメニューの動作開始地点（右下X座標）
                        If udt1.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby <> udt2.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby Then Return True ''サブメニューの動作開始地点（右下Y座標）
                    Next k

                Next j

            Next i

            Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能      : [SET_COUNT]項目のコントロール設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 選択行数
    ' 機能説明  : コンボボックの状態から[SET_COUNT]項目のコントロール設定を行う
    '----------------------------------------------------------------------------
    Private Sub mControlSetCount(ByVal intRowIndex As Integer)

        Try

            ''選択タイプによってコントロール設定を行う
            Select Case CCInt(grdMenuName.Rows(intRowIndex).Cells("cmbType").Value)

                Case gCstCodeOpsPullDownTypeGroup, gCstCodeOpsPullDownTypeSub

                    ''GROUP, SUB
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").ReadOnly = False                             ''Cell操作可
                    Call mSetBackColor(intRowIndex)                                                                 ''背景色の設定

                Case gCstCodeOpsPullDownTypeMainOnly

                    ''MAIN ONLY
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Value = 1                                    ''SET_COUNT数 1固定
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").ReadOnly = True                              ''SET_COUNT数固定のため操作不可
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Style.BackColor = gColorGridRowBackReadOnly  ''背景色の設定

                Case Else

                    ''NOTHING, 新規追加項目
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Value = 0                                    ''SET_COUNT数 0固定
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").ReadOnly = True                              ''Cell操作不可
                    grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Style.BackColor = gColorGridRowBackReadOnly  ''背景色の設定

            End Select

            ''グリッドの保留中の変更を全て適用させる
            Call grdMenuName.EndEdit()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 背景色の設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 指定行数
    ' 機能説明  : 行数によって背景色設定（偶数：青　奇数：白）
    '--------------------------------------------------------------------
    Private Sub mSetBackColor(ByVal intRowIndex As Integer)

        Try

            Dim CellStyleWhite As New Color
            Dim CellStyleBlue As New Color

            ''背景色の設定
            CellStyleWhite = gColorGridRowBackBase
            CellStyleBlue = gColorGridRowBack

            ''偶数行：青、奇数行：白
            If (intRowIndex + 1) Mod 2 <> 0 Then
                grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Style.BackColor = CellStyleWhite
            Else
                grdMenuName.Rows(intRowIndex).Cells("txtSetCount").Style.BackColor = CellStyleBlue
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : 配列のサイズ指定
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub mInitialArray()

        Try

            Call mudtSetOpsPulldownMenuWork.InitArray()
            Call mudtSetOpsPulldownMenuNewMach.InitArray()
            Call mudtSetOpsPulldownMenuNewCarg.InitArray()

            For i As Integer = LBound(mudtSetOpsPulldownMenuWork.udtDetail) To UBound(mudtSetOpsPulldownMenuWork.udtDetail)
                Call mudtSetOpsPulldownMenuWork.udtDetail(i).InitArray()
                Call mudtSetOpsPulldownMenuNewMach.udtDetail(i).InitArray()
                Call mudtSetOpsPulldownMenuNewCarg.udtDetail(i).InitArray()

                For j As Integer = LBound(mudtSetOpsPulldownMenuWork.udtDetail(i).udtGroup) To UBound(mudtSetOpsPulldownMenuWork.udtDetail(i).udtGroup)
                    Call mudtSetOpsPulldownMenuWork.udtDetail(i).udtGroup(j).InitArray()
                    Call mudtSetOpsPulldownMenuNewMach.udtDetail(i).udtGroup(j).InitArray()
                    Call mudtSetOpsPulldownMenuNewCarg.udtDetail(i).udtGroup(j).InitArray()
                Next

            Next

            ''前回値データ削除用
            ReDim mintGraphTypeMach(UBound(mudtSetOpsPulldownMenuWork.udtDetail))
            ReDim mintGraphTypeCarg(UBound(mudtSetOpsPulldownMenuWork.udtDetail))

            For i = 0 To UBound(mudtSetOpsPulldownMenuWork.udtDetail)
                mintGraphTypeMach(i) = gudt.SetOpsPulldownMenuM.udtDetail(i).bytMenuType
                mintGraphTypeCarg(i) = gudt.SetOpsPulldownMenuC.udtDetail(i).bytMenuType
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 変更前の設定データを削除する
    ' 引数      ： ARG1 - (IO) 作業用構造体
    '           ： ARG2 - (IO) グラフタイプ配列
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDeleteSetData(ByRef hudtPulldownWork As gTypSetOpsPulldownMenu, _
                               ByRef hintGraphType() As Integer)

        Try

            ''行インデックスの取得
            Dim intRowIndex As Integer = grdMenuName.CurrentRow.Index

            ''グラフタイプに変更がない場合は削除を行わない
            If hintGraphType(grdMenuName.CurrentRow.Index) <> CCInt(grdMenuName.CurrentRow.Cells("cmbType").Value) Then

                ''データ削除
                Select Case hintGraphType(grdMenuName.CurrentRow.Index)

                    Case gCstCodeOpsPullDownTypeMainOnly, gCstCodeOpsPullDownTypeSub

                        ''サブメニュー
                        For i = 0 To UBound(hudtPulldownWork.udtDetail(intRowIndex).udtGroup)
                            Call gInitOpsPulldownGroup(intRowIndex, hudtPulldownWork.udtDetail(intRowIndex).udtGroup(i))

                            For k = 0 To UBound(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i).udtSub)
                                Call gInitOpsPulldownSub(k, mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i).udtSub(k))
                            Next
                        Next

                    Case gCstCodeOpsPullDownTypeGroup

                        ''サブグループ
                        For i = 0 To UBound(hudtPulldownWork.udtDetail(intRowIndex).udtGroup)
                            Call gInitOpsPulldownGroup(i, hudtPulldownWork.udtDetail(intRowIndex).udtGroup(i))
                        Next

                    Case gCstCodeOpsPullDownTypeNothing

                        ''何もしない

                End Select

                ''前回値の保存
                hintGraphType(grdMenuName.CurrentRow.Index) = grdMenuName.CurrentRow.Cells("cmbType").Value

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 選択したタイプのグラフ表示
    ' 引数      ： ARG1 - (I ) 処理中の行番号
    '           ： ARG2 - (I ) [SET_COUNT] 入力値
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDisplayDetailGraph(ByVal intRowIndex As Integer, _
                                    ByVal intCnt As Integer)

        Try

            ''選択されている[TYPE]の定数種類によって表示処理を分岐
            Select Case CCInt(grdMenuName.Rows(intRowIndex).Cells("cmbType").Value)

                Case gCstCodeOpsPullDownTypeMainOnly, gCstCodeOpsPullDownTypeSub

                    ''メインメニューの設定値を比較用構造体に格納
                    Call mSetStructure(mudtSetOpsPulldownMenuWork)

                    ''未使用エリアの初期化
                    For i = intCnt To UBound(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup)
                        Call gInitOpsPulldownGroup(i, mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i))

                        For k = 0 To UBound(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i).udtSub)
                            Call gInitOpsPulldownSub(k, mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i).udtSub(k))
                        Next

                    Next

                    ''サブメニュー画面表示
                    If frmOpsPulldownSub.gShow(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(0).udtSub, intCnt, Me) = 0 Then

                        ''詳細画面の設定値を比較用構造体に格納
                        Call mSetStructureInfo()

                    End If

                Case gCstCodeOpsPullDownTypeGroup

                    ''メインメニューの設定値を比較用構造体に格納
                    Call mSetStructure(mudtSetOpsPulldownMenuWork)

                    ''サブグループ
                    For i = intCnt To UBound(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup)
                        Call gInitOpsPulldownGroup(i, mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup(i))
                    Next

                    ''サブグループ画面表示
                    If frmOpsPulldownGroup.gShow(mudtSetOpsPulldownMenuWork.udtDetail(intRowIndex).udtGroup, intCnt, Me) = 0 Then

                        ''詳細画面の設定値を比較用構造体に格納
                        Call mSetStructureInfo()

                    End If

                Case gCstCodeOpsPullDownTypeNothing

                    ''何もしない

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 詳細画面の設定値取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 詳細画面の設定値を比較用構造体に設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureInfo()

        Try

            If optOPS.Checked Then

                ''OPS構造体に設定
                Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewMach)

            ElseIf optVDU.Checked Then

                ''Ext.VDU構造体に設定
                Call mCopyStructure(mudtSetOpsPulldownMenuWork, mudtSetOpsPulldownMenuNewCarg)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 表示位置設定処理
    '--------------------------------------------------------------------
    Private Sub ViewData(ByVal udtView As gTypSetOpsPulldownMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim dd As Integer

        'メインメニューの座標設定()
        For i = 0 To Main_Menu_Max - 1

            udtView.udtDetail(i).tx = Main_Menu_Left + Main_Menu_DX * i
            udtView.udtDetail(i).ty = TitleDY
            udtView.udtDetail(i).bx = Main_Menu_Left + Main_Menu_DX * (i + 1)
            udtView.udtDetail(i).by = TitleDY + Main_Menu_DY

            '右5つの場合、ｸﾞﾙｰﾌﾟﾒﾆｭｰは右揃え
            If i > 6 Then
                udtView.udtDetail(i).groupviewx = Main_Menu_Left + Main_Menu_DX * (i + 1) - Group_Menu_DX
            Else
                udtView.udtDetail(i).groupviewx = Main_Menu_Left + Main_Menu_DX * i
            End If

            udtView.udtDetail(i).groupviewy = TitleDY + Main_Menu_DY

            udtView.udtDetail(i).groupsizex = Group_Menu_DX
            udtView.udtDetail(i).groupsizey = Group_Menu_DY * udtView.udtDetail(i).bytMenuSet

            ' グループメニュー座標を設定
            For j = 0 To Group_Menu_Max - 1
                udtView.udtDetail(i).udtGroup(j).grouptx = 0
                udtView.udtDetail(i).udtGroup(j).groupty = Group_Menu_DY * j
                udtView.udtDetail(i).udtGroup(j).groupbx = Group_Menu_DX
                udtView.udtDetail(i).udtGroup(j).groupby = Group_Menu_DY * (j + 1)

                If (udtView.udtDetail(i).bytMenuType = 1) Then     ' グループメニュー
                    'ｻﾌﾞﾒﾆｭｰは右揃え
                    If i > 6 Then
                        udtView.udtDetail(i).udtGroup(j).Subviewx = udtView.udtDetail(i).groupviewx - Sub_Menu_DX

                        'T.Ueki 総合試験118 修正
                        'udtView.udtDetail(i).udtGroup(j).Subviewx = udtView.udtDetail(i).udtGroup(j).grouptx - Sub_Menu_DX
                    Else
                        dd = Group_Menu_DX + Main_Menu_DX * i
                        If dd > Win7CXPOS Then
                            dd = Main_Menu_DX * i - Group_Menu_DX
                        End If
                        udtView.udtDetail(i).udtGroup(j).Subviewx = Main_Menu_Left + dd
                    End If

                ElseIf udtView.udtDetail(i).bytMenuType = 2 Or udtView.udtDetail(i).bytMenuType = 3 Then ' サブメニュー
                    '右5つの場合、ｻﾌﾞﾒﾆｭｰは右揃えとする
                    If i > 6 Then
                        dd = Main_Menu_Left + Main_Menu_DX * (i + 1) - Sub_Menu_DX
                    Else
                        dd = Main_Menu_DX * i + Main_Menu_Left
                        If dd + Sub_Menu_DX > Win7CXPOS Then
                            dd = Win7CXPOS - Sub_Menu_DX - Main_Menu_Left
                        End If
                    End If

                    udtView.udtDetail(i).udtGroup(j).Subviewx = dd
                Else                            ' メニューなし、MAINONLY
                    udtView.udtDetail(i).udtGroup(j).Subviewx = 0
                End If
                '                          
                udtView.udtDetail(i).udtGroup(j).Subviewy = TitleDY + Main_Menu_DY + Group_Menu_DY * j

                udtView.udtDetail(i).udtGroup(j).Subsizex = Sub_Menu_DX
                udtView.udtDetail(i).udtGroup(j).Subsizey = Sub_Menu_DY * udtView.udtDetail(i).udtGroup(j).bytCount

                ' サブメニュー座標を設定
                For k = 0 To Sub_Menu_Max - 1
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = 0
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = Sub_Menu_DY * k
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = Sub_Menu_DX
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = Sub_Menu_DY * (k + 1)

                    If (k >= udtView.udtDetail(i).udtGroup(j).bytCount) Then
                        udtView.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = 0
                    End If
                Next k
            Next j

            'Next ii
        Next i

        ' 不必要なデータをクリア
        Call data_clear(mudtSetOpsPulldownMenuWork)

        For i = 0 To Main_Menu_Max - 1
            udtView.udtDetail(i).strName = MojiMake(udtView.udtDetail(i).strName, Main_Menu_Byte)
            For j = 0 To Group_Menu_Max - 1
                udtView.udtDetail(i).udtGroup(j).strName = MojiMake(udtView.udtDetail(i).udtGroup(j).strName, Group_Menu_Byte)
                For k = 0 To Sub_Menu_Max - 1
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).strName = MojiMake(udtView.udtDetail(i).udtGroup(j).udtSub(k).strName, Sub_Menu_Byte)
                Next k
            Next j
        Next i

    End Sub

    '************************************************************************************************************'
    '不要なデータをクリア
    '************************************************************************************************************'
    Private Sub data_clear(ByVal DataClear As gTypSetOpsPulldownMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        ' 不必要なデータをクリア
        For i = 0 To Main_Menu_Max - 1

            If DataClear.udtDetail(i).bytMenuType = 1 Then       ' グループメニュー
                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, mudtSetOpsPulldownMenuWork)
                Next j

                For j = 0 To Group_Menu_Max - 1     ' グループメニューのサブメニュー
                    For k = DataClear.udtDetail(i).udtGroup(j).bytCount To Sub_Menu_Max - 1
                        Call init_menu_data(3, i, j, k, mudtSetOpsPulldownMenuWork)
                    Next k
                Next j

            ElseIf DataClear.udtDetail(i).bytMenuType = 2 Or DataClear.udtDetail(i).bytMenuType = 3 Then
                For j = 1 To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, mudtSetOpsPulldownMenuWork)
                Next j

                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(4, i, j, 0, mudtSetOpsPulldownMenuWork)
                Next j

                For j = DataClear.udtDetail(i).udtGroup(0).bytCount To Sub_Menu_Max - 1
                    Call init_menu_data(3, i, 0, j, mudtSetOpsPulldownMenuWork)
                Next j
            Else                            ' メニューなし、MAINONLY
                Call init_menu_data(1, i, 0, 0, mudtSetOpsPulldownMenuWork)
            End If

        Next i
    End Sub

    '************************************************************************************************************'
    'Sub:init_menu_data
    '   目的説明:
    '               メニューデータを初期化します。
    '   入力引数:
    '               fg(0:全クリア,1:メニュー,2:グループ,3:サブメニュー)
    '               m(指定メニュー)
    '               g(指定グループ)
    '               s(指定サブ)
    '   戻 り 値:
    '               なし
    '************************************************************************************************************'
    '
    Private Sub init_menu_data(ByVal fg As Integer, ByVal m As Integer, ByVal g As Integer, ByVal s As Integer, ByVal initDataClear As gTypSetOpsPulldownMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        If fg = 0 Then      ' 全クリア
            For i = 0 To Main_Menu_Max - 1
                initDataClear.udtDetail(i).strName = MojiMake("", 12)
                initDataClear.udtDetail(i).bytMenuType = 0
                initDataClear.udtDetail(i).tx = 0
                initDataClear.udtDetail(i).ty = 0
                initDataClear.udtDetail(i).bx = 0
                initDataClear.udtDetail(i).by = 0
                initDataClear.udtDetail(i).bytMenuSet = 0
                initDataClear.udtDetail(i).groupviewx = 0
                initDataClear.udtDetail(i).groupviewy = 0
                initDataClear.udtDetail(i).groupsizex = 0
                initDataClear.udtDetail(i).groupsizey = 0

                For j = 0 To Group_Menu_Max - 1
                    initDataClear.udtDetail(i).udtGroup(j).strName = MojiMake("", 24)
                    initDataClear.udtDetail(i).udtGroup(j).groupbytMenuType = 0
                    initDataClear.udtDetail(i).udtGroup(j).grouptx = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupty = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupbx = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupby = 0
                    initDataClear.udtDetail(i).udtGroup(j).bytCount = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subviewx = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subviewy = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subsizex = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subsizey = 0

                    For k = 0 To Sub_Menu_Max - 1
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).strName = MojiMake("", 32)
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode = 0
                    Next k
                Next j
            Next i

        ElseIf fg = 1 Then          '  指定メニュークリア
            initDataClear.udtDetail(m).bytMenuSet = 0
            initDataClear.udtDetail(m).groupviewx = 0
            initDataClear.udtDetail(m).groupviewy = 0
            initDataClear.udtDetail(m).groupsizex = 0
            initDataClear.udtDetail(m).groupsizey = 0

            For j = 0 To Group_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(j).strName = MojiMake("", 24)
                initDataClear.udtDetail(m).udtGroup(j).groupbytMenuType = 0
                initDataClear.udtDetail(m).udtGroup(j).grouptx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupty = 0
                initDataClear.udtDetail(m).udtGroup(j).groupbx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupby = 0
                initDataClear.udtDetail(m).udtGroup(j).bytCount = 0
                initDataClear.udtDetail(m).udtGroup(j).Subviewx = 0
                initDataClear.udtDetail(m).udtGroup(j).Subviewy = 0
                initDataClear.udtDetail(m).udtGroup(j).Subsizex = 0
                initDataClear.udtDetail(m).udtGroup(j).Subsizey = 0

                For k = 0 To Sub_Menu_Max - 1
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).strName = MojiMake("", 32)
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType1 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType2 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType3 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType4 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenutx = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenuty = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenubx = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenuby = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo1 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo2 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo3 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo4 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).bytKeyCode = 0
                Next k
            Next j

        ElseIf fg = 2 Then          '  指定グループクリア
            initDataClear.udtDetail(m).udtGroup(g).strName = MojiMake("", 24)
            initDataClear.udtDetail(m).udtGroup(g).groupbytMenuType = 0
            initDataClear.udtDetail(m).udtGroup(g).grouptx = 0
            initDataClear.udtDetail(m).udtGroup(g).groupty = 0
            initDataClear.udtDetail(m).udtGroup(g).groupbx = 0
            initDataClear.udtDetail(m).udtGroup(g).groupby = 0
            initDataClear.udtDetail(m).udtGroup(g).bytCount = 0
            initDataClear.udtDetail(m).udtGroup(g).Subviewx = 0
            initDataClear.udtDetail(m).udtGroup(g).Subviewy = 0
            initDataClear.udtDetail(m).udtGroup(g).Subsizex = 0
            initDataClear.udtDetail(m).udtGroup(g).Subsizey = 0

            For k = 0 To Sub_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).strName = MojiMake("", 32)
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType1 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType2 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType3 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType4 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenutx = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenuty = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenubx = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenuby = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo1 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo2 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo3 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo4 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).bytKeyCode = 0
            Next k

        ElseIf fg = 3 Then          '  指定サブメニュークリア
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).strName = MojiMake("", 32)
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType1 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType2 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType3 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType4 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenutx = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenuty = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenubx = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenuby = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo1 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo2 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo3 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo4 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).bytKeyCode = 0

        ElseIf fg = 4 Then
            initDataClear.udtDetail(m).bytMenuSet = 0
            initDataClear.udtDetail(m).groupviewx = 0
            initDataClear.udtDetail(m).groupviewy = 0
            initDataClear.udtDetail(m).groupsizex = 0
            initDataClear.udtDetail(m).groupsizey = 0

            For j = 0 To Group_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(j).strName = MojiMake("", 24)
                initDataClear.udtDetail(m).udtGroup(j).groupbytMenuType = 0
                initDataClear.udtDetail(m).udtGroup(j).grouptx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupty = 0
                initDataClear.udtDetail(m).udtGroup(j).groupbx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupby = 0
            Next j

        End If
    End Sub

#End Region

End Class
