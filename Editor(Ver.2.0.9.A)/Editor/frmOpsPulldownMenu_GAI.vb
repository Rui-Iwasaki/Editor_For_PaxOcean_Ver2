Public Class frmOpsPulldownMenu_GAI
    '■外販
    '外販の場合、こちらの画面とする。


#Region "変数"
    'プルダウンメニュー構造体
    Private mudtSetOpsPulldownMenuWork As gTypSetOpsPulldownMenu = Nothing

    Private mudtSetOpsPulldownMenuNewMach As gTypSetOpsPulldownMenu = Nothing
    Private mudtSetOpsPulldownMenuNewCarg As gTypSetOpsPulldownMenu = Nothing

    Private prblUpdateFlg As Boolean = False    '更新ﾌﾗｸﾞ GRAPH編集した場合、差分による更新判定ができないため。
#End Region

#Region "画面"

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


    Private Sub frmOpsPulldownMenu_GAI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '配列再定義
            Call mInitialArray()

            'OPS/ExtVDUの情報を取得する
            Call mCopyStructure(gudt.SetOpsPulldownMenuM, mudtSetOpsPulldownMenuNewMach)
            Call mCopyStructure(gudt.SetOpsPulldownMenuC, mudtSetOpsPulldownMenuNewCarg)

            '画面設定
            Call mSetDisplay()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub frmOpsPulldownMenu_GAI_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub frmOpsPulldownMenu_GAI_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            '画面を構造体へ反映
            Call subSetStructure()

            'データが変更されているかチェック OPS
            blnMach = mChkStructureEquals(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
            'OSP側のGRAPH,MIMICの内容が変わっているならば ExtVDU側も自動更新
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

            'データが変更されているかチェック VDU
            blnCarg = mChkStructureEquals(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

            'データが変更されている場合
            If (blnMach = True) Or (blnCarg = True) Or (prblUpdateFlg = True) Then
                '変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes
                        prblUpdateFlg = False
                        '変更された場合は設定を更新する
                        If blnMach Then Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
                        If blnCarg Then Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

                        '更新フラグ設定
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


#Region "チェックボックス"
    Private Sub chkVIEW_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVIEW.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkTREND_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTREND.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkFREE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFREE.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkSUMMARY_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSUMMARY.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkHISTORY_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHISTORY.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkGRAPH_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGRAPH.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkMIMIC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMIMIC.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkPRINT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPRINT.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkSYSTEM_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSYSTEM.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
    Private Sub chkHELP_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHELP.CheckedChanged
        Call mSetDisplay_VDU()
    End Sub
#End Region

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            '画面を構造体へ反映
            Call subSetStructure()

            'データが変更されているかチェック OPS
            blnMach = mChkStructureEquals(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
            'OSP側のGRAPH,MIMICの内容が変わっているならば ExtVDU側も自動更新
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

            'データが変更されているかチェック VDU
            blnCarg = mChkStructureEquals(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)

            'データが変更されている場合
            If (blnMach = True) Or (blnCarg = True) Or (prblUpdateFlg = True) Then
                prblUpdateFlg = False

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


    Private Sub btnGRAPH_Click(sender As System.Object, e As System.EventArgs) Handles btnGRAPH.Click
        Try
            'GRAPHチェックOFFなら何もしない
            If chkGRAPH.Checked = False Then
                Exit Sub
            End If

            'GRAPHﾎﾞﾀﾝは、ｸﾞﾗﾌ編集画面を起動する。
            'GRAPH画面でのﾒﾆｭｰ変種はｸﾞﾛｰﾊﾞﾙ変数に対して行うため一時変数をｸﾞﾛｰﾊﾞﾙ変数へ一旦戻す
            Call mCopyStructure(mudtSetOpsPulldownMenuNewMach, gudt.SetOpsPulldownMenuM)
            Call mCopyStructure(mudtSetOpsPulldownMenuNewCarg, gudt.SetOpsPulldownMenuC)
            Call frmOpsGraphList.gShow()
            'GRAPH画面でのﾒﾆｭｰ変種はｸﾞﾛｰﾊﾞﾙ変数に対して行うため。
            Call mCopyStructure(gudt.SetOpsPulldownMenuM, mudtSetOpsPulldownMenuNewMach)
            Call mCopyStructure(gudt.SetOpsPulldownMenuC, mudtSetOpsPulldownMenuNewCarg)
            prblUpdateFlg = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub btnMIMIC_Click(sender As System.Object, e As System.EventArgs) Handles btnMIMIC.Click
        Try
            'MIMICチェックOFFなら何もしない
            If chkMIMIC.Checked = False Then
                Exit Sub
            End If

            'MIMICﾎﾞﾀﾝは、社内版の画面(MIMICのみ)をそのまま表示する（仮）
            'サブグループ画面表示
            If frmOpsPulldownGroup.gShow(mudtSetOpsPulldownMenuNewMach.udtDetail(6).udtGroup, 12, Me) = 0 Then
                Call mCopyDefGrp(mudtSetOpsPulldownMenuNewMach, mudtSetOpsPulldownMenuNewCarg, 6)
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub btnPRINT_Click(sender As System.Object, e As System.EventArgs) Handles btnPRINT.Click
        Try
            Dim intRet As Integer = 0
            Dim intBack As Integer

            intRet = frmOpsPulldownSub_GAI_Print.gShow(mudtSetOpsPulldownMenuNewMach.udtDetail(9).udtGroup(0), intBack, Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "関数"
    '配列のサイズ指定
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
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '構造体コポー
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

    '変数値を画面へ反映
    Private Sub mSetDisplay()
        Try
            Dim i As Integer = 0

            'OPS
            For i = 0 To UBound(mudtSetOpsPulldownMenuNewMach.udtDetail) Step 1
                With mudtSetOpsPulldownMenuNewMach.udtDetail(i)
                    '名称を見てあればチェックON
                    '>>>末尾に0x00が入っているため、それを取り除いてから比較
                    Dim strHikaku As String = .strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    Select Case strHikaku
                        Case "VIEW"
                            chkVIEW.Checked = True
                        Case "TREND"
                            chkTREND.Checked = True
                        Case "FREE"
                            chkFREE.Checked = True
                        Case "SUMMARY"
                            chkSUMMARY.Checked = True
                        Case "HISTORY"
                            chkHISTORY.Checked = True
                        Case "GRAPH"
                            chkGRAPH.Checked = True
                        Case "MIMIC"
                            chkMIMIC.Checked = True
                        Case "PRINT"
                            chkPRINT.Checked = True
                        Case "SYSTEM"
                            chkSYSTEM.Checked = True
                        Case "HELP"
                            chkHELP.Checked = True
                    End Select

                End With
            Next i

            'VDU
            Call mSetDisplay_VDU()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'VDU画面表示
    Private Sub mSetDisplay_VDU()
        Try
            'VDUはOPSの設定状況をみて動作
            '
            If chkVIEW.Checked = True Then
                lblVDU_VIEW.Text = "VIEW"
            Else
                lblVDU_VIEW.Text = ""
            End If
            '
            If chkTREND.Checked = True Then
                lblVDU_TREND.Text = "TREND"
            Else
                lblVDU_TREND.Text = ""
            End If
            '
            If chkFREE.Checked = True Then
                lblVDU_FREE.Text = "FREE"
            Else
                lblVDU_FREE.Text = ""
            End If
            '
            If chkSUMMARY.Checked = True Then
                lblVDU_SUMMARY.Text = "SUMMARY"
            Else
                lblVDU_SUMMARY.Text = ""
            End If
            '
            If chkHISTORY.Checked = True Then
                lblVDU_HISTORY.Text = "HISTORY"
            Else
                lblVDU_HISTORY.Text = ""
            End If
            '
            If chkGRAPH.Checked = True Then
                lblVDU_GRAPH.Text = "GRAPH"
            Else
                lblVDU_GRAPH.Text = ""
            End If
            '
            If chkMIMIC.Checked = True Then
                lblVDU_MIMIC.Text = "MIMIC"
            Else
                lblVDU_MIMIC.Text = ""
            End If
            '
            If chkPRINT.Checked = True Then
                lblVDU_PRINT.Text = "PRINT"
            Else
                lblVDU_PRINT.Text = ""
            End If
            '
            If chkSYSTEM.Checked = True Then
                lblVDU_SYSTEM.Text = "SYSTEM"
            Else
                lblVDU_SYSTEM.Text = ""
            End If
            '
            If chkHELP.Checked = True Then
                lblVDU_HELP.Text = "EXIT"
            Else
                lblVDU_HELP.Text = ""
            End If


            'OPSもOPSの設定状況をみて動作
            '
            If chkVIEW.Checked = True Then
                lblVIEW.Text = "VIEW"
            Else
                lblVIEW.Text = ""
            End If
            '
            If chkTREND.Checked = True Then
                lblTREND.Text = "TREND"
            Else
                lblTREND.Text = ""
            End If
            '
            If chkFREE.Checked = True Then
                lblFREE.Text = "FREE"
            Else
                lblFREE.Text = ""
            End If
            '
            If chkSUMMARY.Checked = True Then
                lblSUMMARY.Text = "SUMMARY"
            Else
                lblSUMMARY.Text = ""
            End If
            '
            If chkHISTORY.Checked = True Then
                lblHISTORY.Text = "HISTORY"
            Else
                lblHISTORY.Text = ""
            End If
            '
            If chkGRAPH.Checked = True Then
                lblGRAPH.Text = "GRAPH"
            Else
                lblGRAPH.Text = ""
            End If
            '
            If chkMIMIC.Checked = True Then
                lblMIMIC.Text = "MIMIC"
            Else
                lblMIMIC.Text = ""
            End If
            '
            If chkPRINT.Checked = True Then
                lblPRINT.Text = "PRINT"
            Else
                lblPRINT.Text = ""
            End If
            '
            If chkSYSTEM.Checked = True Then
                lblSYSTEM.Text = "SYSTEM"
            Else
                lblSYSTEM.Text = ""
            End If
            '
            If chkHELP.Checked = True Then
                lblHELP.Text = "HELP"
            Else
                lblHELP.Text = ""
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '保存時、画面情報を変数へ反映する処理
    Private Sub subSetStructure()
        Try
            'VIEW
            If chkVIEW.Checked = False Then
                Call subSetOFF("VIEW")
            Else
                Call subSetON("VIEW")
            End If
            'TREND
            If chkTREND.Checked = False Then
                Call subSetOFF("TREND")
            Else
                Call subSetON("TREND")
            End If
            'FREE
            If chkFREE.Checked = False Then
                Call subSetOFF("FREE")
            Else
                Call subSetON("FREE")
            End If
            'SUMMARY
            If chkSUMMARY.Checked = False Then
                Call subSetOFF("SUMMARY")
            Else
                Call subSetON("SUMMARY")
            End If
            'HISTORY
            If chkHISTORY.Checked = False Then
                Call subSetOFF("HISTORY")
            Else
                Call subSetON("HISTORY")
            End If
            'GRAPH
            If chkGRAPH.Checked = False Then
                Call subSetOFF("GRAPH")
            Else
                Call subSetON_GRAPH()
            End If
            'MIMIC
            If chkMIMIC.Checked = False Then
                Call subSetOFF("MIMIC")
            Else
                Call subSetON_MIMIC()
            End If
            'PRINT
            If chkPRINT.Checked = False Then
                Call subSetOFF("PRINT")
            Else
                Call subSetON_PRINT()
            End If
            'SYSTEM
            If chkSYSTEM.Checked = False Then
                Call subSetOFF("SYSTEM")
            Else
                Call subSetON("SYSTEM")
            End If
            'HELP
            If chkHELP.Checked = False Then
                Call subSetOFF("HELP")
            Else
                Call subSetON("HELP")
            End If

            Call ViewData(mudtSetOpsPulldownMenuNewMach)
            Call ViewData(mudtSetOpsPulldownMenuNewCarg)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '構造体比較
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

    '保存時ﾁｪｯｸOFFになっていた場合の処理
    Private Sub subSetOFF(pstrName As String)
        Try
            Dim strName As String = ""
            Dim i As Integer = 0

            '該当変数の場所を指定
            Dim intTable As Integer = -1
            With mudtSetOpsPulldownMenuNewMach
                For i = 0 To UBound(.udtDetail) Step 1
                    '名称から0x00を取り除く
                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName = pstrName Then
                        '名称を比較し、一致すればその場所を格納
                        intTable = i
                        Exit For
                    End If
                Next i
            End With

            '該当変数をクリア()
            If intTable <> -1 Then
                'OPS
                With mudtSetOpsPulldownMenuNewMach.udtDetail(intTable)
                    .strName = MojiMake("", 12)
                    .tx = 0 : .ty = 0
                    .bx = 0 : .by = 0
                    .OPSSTFLG1 = 0 : .OPSSTFLG2 = 0
                    .bytMenuNo1 = 0
                    .Spare1 = 0 : .Spare2 = 0 : .Spare3 = 0 : .Spare4 = 0 : .Spare5 = 0
                    .bytMenuType = 0
                    .Yobi1 = 0 : .Yobi2 = 0
                End With
                Call init_menu_data(1, intTable, 0, 0, mudtSetOpsPulldownMenuNewMach)
                'VDU
                With mudtSetOpsPulldownMenuNewCarg.udtDetail(intTable)
                    .strName = MojiMake("", 12)
                    .tx = 0 : .ty = 0
                    .bx = 0 : .by = 0
                    .OPSSTFLG1 = 0 : .OPSSTFLG2 = 0
                    .bytMenuNo1 = 0
                    .Spare1 = 0 : .Spare2 = 0 : .Spare3 = 0 : .Spare4 = 0 : .Spare5 = 0
                    .bytMenuType = 0
                    .Yobi1 = 0 : .Yobi2 = 0
                End With
                Call init_menu_data(1, intTable, 0, 0, mudtSetOpsPulldownMenuNewCarg)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '保存時ﾁｪｯｸONになっていた場合の処理
    Private Sub subSetON(pstrName As String)
        Try
            Dim strName As String = ""
            Dim i As Integer = 0

            '該当変数の場所を指定
            Dim intTable As Integer = -1
            With mudtSetOpsPulldownMenuNewMach
                For i = 0 To UBound(.udtDetail) Step 1
                    '名称から0x00を取り除く
                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName = pstrName Then
                        '名称を比較し、一致すればその場所を格納
                        intTable = i
                        Exit For
                    End If
                Next i
            End With

            If intTable = -1 Then
                '該当変数が無い＝CHK OFFされていた
                'デフォルトファイルから読み込んで、該当値をコピー
                Call subSetDefaultValue(pstrName)
            Else
                '該当変数がある場合は何もしない
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '該当ﾃﾞﾌｫﾙﾄ値をファイルから読み込んでｺﾋﾟｰ
    Private Sub subSetDefaultValue(pstrName As String)
        Try
            Dim strName As String = ""
            Dim i As Integer = 0

            Dim mudtDefOPS As gTypSetOpsPulldownMenu = Nothing
            Dim mudtDefVDU As gTypSetOpsPulldownMenu = Nothing
            'デフォルトファイルを読み込む
            Call gLoadMenuMain(mudtDefOPS)
            Call gLoadMenuMain(mudtDefVDU, True)

            '該当変数の場所を指定
            Dim intTable As Integer = -1
            With mudtDefOPS
                For i = 0 To UBound(.udtDetail) Step 1
                    '名称から0x00を取り除く
                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName = pstrName Then
                        '名称を比較し、一致すればその場所を格納
                        intTable = i
                        Exit For
                    End If
                Next i
            End With

            '該当値をｺﾋﾟｰ
            If intTable <> -1 Then
                'OPS
                Call mCopyDefTable(mudtDefOPS, mudtSetOpsPulldownMenuNewMach, intTable)
                'VDU
                Call mCopyDefTable(mudtDefVDU, mudtSetOpsPulldownMenuNewCarg, intTable)
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'テーブルコピー（subSetDefaultValue関数から呼ばれるのみ）
    Private Sub mCopyDefTable(ByVal udtSource As gTypSetOpsPulldownMenu, _
                               ByRef udtTarget As gTypSetOpsPulldownMenu, _
                               pintTable As Integer)

        Try
            Dim i As Integer = pintTable

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

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'テーブルコピー（btnMIMIC_Click関数から呼ばれるのみ）
    Private Sub mCopyDefGrp(ByVal udtSource As gTypSetOpsPulldownMenu, _
                               ByRef udtTarget As gTypSetOpsPulldownMenu, _
                               pintTable As Integer)

        Try
            Dim i As Integer = pintTable

            'サブグループ
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

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '保存時ﾁｪｯｸONになっていた場合の処理(MIMIC)
    Private Sub subSetON_MIMIC()
        Try
            Dim CststrTname As String = "MIMIC"
            Dim CintMimicTable As Integer = 6
            Dim strName As String = ""
            Dim i As Integer = 0
            Dim intCount As Integer = 0

            '該当変数の場所を指定
            Dim intTable As Integer = -1
            With mudtSetOpsPulldownMenuNewMach
                For i = 0 To UBound(.udtDetail) Step 1
                    '名称から0x00を取り除く
                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName = CststrTname Then
                        '名称を比較し、一致すればその場所を格納
                        intTable = i
                        Exit For
                    End If
                Next i
            End With

            'OPS
            With mudtSetOpsPulldownMenuNewMach.udtDetail(CintMimicTable)
                .bytMenuType = 1
                .strName = MojiMake(CststrTname, 12)
                'MenuSet=Countを数えてｾｯﾄ
                intCount = 0
                For i = 0 To UBound(.udtGroup) Step 1
                    strName = .udtGroup(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName <> "" Then
                        intCount = intCount + 1
                    End If
                Next
                .bytMenuSet = intCount
            End With
            'VDU
            With mudtSetOpsPulldownMenuNewCarg.udtDetail(CintMimicTable)
                .bytMenuType = 1
                .strName = MojiMake(CststrTname, 12)
                'MenuSet=Countを数えてｾｯﾄ
                intCount = 0
                For i = 0 To UBound(.udtGroup) Step 1
                    strName = .udtGroup(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName <> "" Then
                        intCount = intCount + 1
                    End If
                Next
                .bytMenuSet = intCount
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '保存時ﾁｪｯｸONになっていた場合の処理(GRAPH)
    Private Sub subSetON_GRAPH()
        Try
            Dim CststrTname As String = "GRAPH"
            Dim CintGraphTable As Integer = 5
            Dim i As Integer = 0

            Dim intMojiLen As Integer = 0
            Dim intRowCnt As Integer = 0

            'グラフ設定データインポート用構造体
            Dim udtGraphDataMach As gTypImportGraphData = Nothing
            Dim udtGraphDataCarg As gTypImportGraphData = Nothing

            'グラフ設定データを取得する
            Call gMakeOpsDataStructure(True, udtGraphDataMach)
            Call gMakeOpsDataStructure(False, udtGraphDataCarg)


            'OPS
            With mudtSetOpsPulldownMenuNewMach.udtDetail(CintGraphTable)
                .bytMenuType = gCstCodeOpsPullDownTypeSub
                .strName = MojiMake(CststrTname, 12)
                'GRAPHをみてｾｯﾄ
                .udtGroup(0).bytCount = udtGraphDataMach.intUseCountGraph
                intRowCnt = 0
                For i = 0 To UBound(udtGraphDataMach.udtOpsGraph)

                    intMojiLen = Len(udtGraphDataMach.udtOpsGraph(i).strGraphName)
                    If intMojiLen <> 0 Then
                        ' 文字の後ろをNULLで埋める
                        .udtGroup(0).udtSub(intRowCnt).strName = MojiMake(udtGraphDataMach.udtOpsGraph(i).strGraphName, 32)  'SubMenu

                        .udtGroup(0).udtSub(intRowCnt).ViewNo1 = ViewDataExchange(intRowCnt + 51)           'ScreenNo

                        '処理項目1～4を「1」「6」「1」「0」
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType1 = CCbyte("1")                        ''処理項目1
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType2 = CCbyte("6")                        ''処理項目2
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType3 = CCbyte("1")                        ''処理項目3
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType4 = CCbyte("0")                        ''処理項目4
                        intRowCnt += 1
                    End If
                Next i
            End With
            'VDU
            With mudtSetOpsPulldownMenuNewCarg.udtDetail(CintGraphTable)
                .bytMenuType = 1
                .strName = MojiMake(CststrTname, 12)
                'GRAPHをみてｾｯﾄ
                .udtGroup(0).bytCount = udtGraphDataCarg.intUseCountGraph
                intRowCnt = 0
                For i = 0 To UBound(udtGraphDataCarg.udtOpsGraph)

                    intMojiLen = Len(udtGraphDataCarg.udtOpsGraph(i).strGraphName)
                    If intMojiLen <> 0 Then
                        ' 文字の後ろをNULLで埋める
                        .udtGroup(0).udtSub(intRowCnt).strName = MojiMake(udtGraphDataCarg.udtOpsGraph(i).strGraphName, 32)  'SubMenu

                        .udtGroup(0).udtSub(intRowCnt).ViewNo1 = ViewDataExchange(intRowCnt + 51)           'ScreenNo

                        '処理項目1～4を「1」「6」「1」「0」
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType1 = CCbyte("1")                        ''処理項目1
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType2 = CCbyte("6")                        ''処理項目2
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType3 = CCbyte("1")                        ''処理項目3
                        .udtGroup(0).udtSub(intRowCnt).SubbytMenuType4 = CCbyte("0")                        ''処理項目4
                        intRowCnt += 1
                    End If
                Next i
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '保存時ﾁｪｯｸONになっていた場合の処理(PRINT)
    Private Sub subSetON_PRINT()
        Try
            Dim CststrTname As String = "PRINT"
            Dim CintPrintTable As Integer = 9
            Dim i As Integer = 0

            Dim intMojiLen As Integer = 0
            Dim intRowCnt As Integer = 0

            'OPS
            With mudtSetOpsPulldownMenuNewMach.udtDetail(CintPrintTable)
                .bytMenuType = gCstCodeOpsPullDownTypeSub
                .strName = MojiMake(CststrTname, 12)

                'PRINT詳細は、PRINT詳細入力画面で編集済のためノータッチ
            End With
            'VDU
            With mudtSetOpsPulldownMenuNewCarg.udtDetail(CintPrintTable)
                .bytMenuType = gCstCodeOpsPullDownTypeSub
                .strName = MojiMake(CststrTname, 12)

                'PRINT詳細は、PRINT詳細入力画面で編集済のためノータッチ
                'VDUは、Save時にOPSと自動でそろえるためノータッチ
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    'メニューデータ表示位置設定処理
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
        Next i

        ' 不必要なデータをクリア
        Call data_clear(udtView)

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
    'メニューデータのメニュータイプを見て不要なメニューデータをクリア
    Private Sub data_clear(ByVal DataClear As gTypSetOpsPulldownMenu)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        ' 不必要なデータをクリア
        For i = 0 To Main_Menu_Max - 1

            If DataClear.udtDetail(i).bytMenuType = 1 Then       ' グループメニュー
                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, DataClear)
                Next j

                For j = 0 To Group_Menu_Max - 1     ' グループメニューのサブメニュー
                    For k = DataClear.udtDetail(i).udtGroup(j).bytCount To Sub_Menu_Max - 1
                        Call init_menu_data(3, i, j, k, DataClear)
                    Next k
                Next j

            ElseIf DataClear.udtDetail(i).bytMenuType = 2 Or DataClear.udtDetail(i).bytMenuType = 3 Then
                For j = 1 To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, DataClear)
                Next j

                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(4, i, j, 0, DataClear)
                Next j

                For j = DataClear.udtDetail(i).udtGroup(0).bytCount To Sub_Menu_Max - 1
                    Call init_menu_data(3, i, 0, j, DataClear)
                Next j
            Else                            ' メニューなし、MAINONLY
                Call init_menu_data(1, i, 0, 0, DataClear)
            End If

        Next i
    End Sub
    'メニューデータを初期化（メニュー毎、グループ毎、サブ毎のそれぞれ）
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