Public Class frmChSioList_GAI

#Region "変数定義"
    Private printSIOnor As Integer = 5 '4+1     SIOボード通常端子数
    Private printSIOext As Integer = 10 '9+1    SIOボード拡張端子数

    Private mudtSetChSioNew As gTypSetChSio
    Private mudtSetChSioChNew() As gTypSetChSioCh

    Private mudtSetChSioExtNew() As gTypSetChSioExt

    Private prFirst As Boolean = False
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
            Call gShowFormModelessForCloseWait11(Me)

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
    Private Sub frmChSioList_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'コンボボックス初期設定
            'ｸﾘｱのコンボは、iniﾌｧｲﾙではなく自作とする
            cmbPort.Items.Clear()
            If chkSioExt.Checked = True Then
                With cmbPort
                    .Items.Add("")
                    For i As Integer = 1 To printSIOext - 1 Step 1
                        .Items.Add(i.ToString)
                    Next i
                    .Items.Add("ALL")
                End With
            Else
                With cmbPort
                    .Items.Add("")
                    For i As Integer = 1 To printSIOnor - 1 Step 1
                        .Items.Add(i.ToString)
                    Next i
                    .Items.Add("ALL")
                End With
            End If

            'Ver2.0.7.C SIO拡張設定
            If g_bytSIOport = 0 Then
                chkSioExt.Checked = False
            Else
                prFirst = True
                chkSioExt.Checked = True
            End If


            Call mInitialDataGrid()

            ''構造体配列初期化
            Call mudtSetChSioNew.InitArray()
            For i As Integer = LBound(mudtSetChSioNew.udtVdr) To UBound(mudtSetChSioNew.udtVdr)
                Call mudtSetChSioNew.udtVdr(i).InitArray()
            Next

            ReDim mudtSetChSioChNew(gCstCntChSioPort - 1)
            For i As Integer = 0 To UBound(mudtSetChSioChNew)
                Call mudtSetChSioChNew(i).InitArray()
            Next

            ReDim mudtSetChSioExtNew(gCstCntChSioVDRPort - 1)
            For i As Integer = 0 To UBound(mudtSetChSioExtNew)
                Call mudtSetChSioExtNew(i).InitArray()
            Next

            ''構造体コピー
            Call mCopyStructure(gudt.SetChSio, mudtSetChSioNew)

            For i As Integer = 0 To UBound(mudtSetChSioChNew)
                Call mCopyStructure(gudt.SetChSioCh(i), mudtSetChSioChNew(i))
            Next

            '構造体コピー SIO拡張
            Call mCopyStructure(gudt.SetChSioExt, mudtSetChSioExtNew)


            '画面設定
            Call mSetDisplay(mudtSetChSioNew, True)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： Detailsボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetails.Click

        Try
            Dim intRet As Integer = 0

            'セルが選択されてない場合は何もしない
            If grdSIO.SelectedCells.Count = 0 Then Exit Sub
            '選択セルで、Communicationを選んでないなら何もしない
            If grdSIO(2, grdSIO.SelectedCells(0).RowIndex).Value = "" Then Exit Sub


            'SIO設定詳細画面表示(外販専用画面) VDRとMODBUSで分岐
            Select Case grdSIO(2, grdSIO.SelectedCells(0).RowIndex).Value
                Case "VDR"
                    intRet = frmChSioDetail_GAI_VDR.gShow(grdSIO.SelectedCells(0).RowIndex, mudtSetChSioNew.shtNum, mudtSetChSioNew.udtVdr, mudtSetChSioChNew, mudtSetChSioExtNew, Me)
                Case "MODBUS-RTU"
                    intRet = frmChSioDetail_GAI_MOD.gShow(grdSIO.SelectedCells(0).RowIndex, mudtSetChSioNew.shtNum, mudtSetChSioNew.udtVdr, mudtSetChSioChNew, mudtSetChSioExtNew, Me)
                Case "JRCS-STANDARD"
                    intRet = frmChSioDetail_GAI_JRCS.gShow(grdSIO.SelectedCells(0).RowIndex, mudtSetChSioNew.shtNum, mudtSetChSioNew.udtVdr, mudtSetChSioChNew, mudtSetChSioExtNew, Me)
                Case Else
                    intRet = 0
            End Select

            If intRet = 1 Then
                '画面更新
                Call mSetDisplay(mudtSetChSioNew, False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSIO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSIO.CellDoubleClick

        Try

            ''セルが選択されてない場合は何もしない
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            ''Detailsボタンクリックイベントを呼び出す
            Call cmdDetails_Click(cmdDetails, New EventArgs)

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

            Dim blnFlgSio As Boolean = False
            Dim blnFlgSioCh As Boolean = False
            Dim blnFlgSioExt As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChSioNew)

            'SIO設定が変更されている場合は設定を更新する
            If Not mChkStructureEquals(mudtSetChSioNew, gudt.SetChSio) Then
                Call mCopyStructure(mudtSetChSioNew, gudt.SetChSio)
                blnFlgSio = True
                gudt.SetEditorUpdateInfo.udtSave.bytChSio = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChSio = 1
            End If

            'SIO設定Ch設定が変更されている場合は設定を更新する
            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                If Not mChkStructureEquals(mudtSetChSioChNew(i), gudt.SetChSioCh(i)) Then
                    Call mCopyStructure(mudtSetChSioChNew(i), gudt.SetChSioCh(i))
                    blnFlgSioCh = True
                    gudt.SetEditorUpdateInfo.udtSave.bytChSioCh(i) = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytChSioCh(i) = 1
                End If
            Next

            'SIO設定拡張設定が変更されている場合は設定を更新する
            For i As Integer = 0 To UBound(gudt.SetChSioExt)
                If Not mChkStructureEquals(mudtSetChSioExtNew(i), gudt.SetChSioExt(i)) Then
                    blnFlgSioExt = True
                    gudt.SetEditorUpdateInfo.udtSave.bytChSioExt(i) = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytChSioExt(i) = 1
                End If
            Next
            If blnFlgSioExt = True Then
                Call mCopyStructure(mudtSetChSioExtNew, gudt.SetChSioExt)
            End If



            If blnFlgSio Or blnFlgSioCh Or blnFlgSioExt Then

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''全体更新フラグ設定
                gblnUpdateAll = True

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
    ' 機能      : Clearクリック
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        Try
            Dim intPort As Integer = 0
            Dim strPort As String = cmbPort.Text.Trim
            strPort = NZf(strPort)
            If strPort = "" Then
                Exit Sub
            End If
            If strPort <> "ALL" Then
                If IsNumeric(strPort) = False Then
                    Exit Sub
                End If
            End If

            'Ver2.0.2.8 ﾒｯｾｰｼﾞを出す
            If MessageBox.Show("Do you Clear the SIO settings?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'Ver2.0.2.8 ポート選択コンボは自作コンボとなる
                Select Case strPort
                    Case "ALL"
                        'ALLは、全ポート(1-9)実行
                        For i As Integer = 1 To 9 Step 1
                            Call ClearSIOSetting(i)
                        Next i
                    Case Else
                        'それ以外＝該当数値
                        intPort = CInt(strPort)
                        Call ClearSIOSetting(intPort)
                End Select

                'intPort = cmbPort.SelectedValue
                'Call ClearSIOSetting(intPort)
                Call mInitialDataGrid()
                Call mSetDisplay(mudtSetChSioNew, True)
            End If
            'Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : 通信設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : ﾎﾟｰﾄ番号
    '--------------------------------------------------------------------
    Private Sub ClearSIOSetting(intPort As Integer)

        mudtSetChSioNew.shtNum(intPort - 1) = 0        ' 送信CH数0
        gInitSetChSioVdr(mudtSetChSioNew.udtVdr(intPort - 1)) ' 通信設定ｸﾘｱ

        ' 送信ﾘｽﾄ　ｸﾘｱ
        With mudtSetChSioChNew(intPort - 1)
            For i As Integer = 0 To UBound(.udtSioChRec)

                .udtSioChRec(i).shtChId = 0
                .udtSioChRec(i).shtChNo = 0
            Next
        End With
    End Sub


    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim blnFlgSio As Boolean = False
            Dim blnFlgSioChAll As Boolean = False
            Dim blnFlgSioCh(gCstCntChSioPort - 1) As Boolean

            Dim blnFlgSioExt As Boolean


            ''グリッドの保留中の変更を全て適用させる（2010/12/14 追加）
            Call grdSIO.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChSioNew)

            ''SIO設定が変更されているかチェック
            If Not mChkStructureEquals(mudtSetChSioNew, gudt.SetChSio) Then
                blnFlgSio = True
            End If

            ''SIO設定Ch設定が変更されているかチェック
            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                If Not mChkStructureEquals(mudtSetChSioChNew(i), gudt.SetChSioCh(i)) Then
                    blnFlgSioChAll = True
                    blnFlgSioCh(i) = True
                End If
            Next

            'Ver2.0.5.8
            'SIO設定拡張設定が変更されている場合は設定を更新する
            For i As Integer = 0 To UBound(gudt.SetChSioExt)
                If Not mChkStructureEquals(mudtSetChSioExtNew(i), gudt.SetChSioExt(i)) Then
                    blnFlgSioExt = True
                End If
            Next




            ''データが変更されている場合
            If blnFlgSio Or blnFlgSioChAll Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        Call mCopyStructure(mudtSetChSioNew, gudt.SetChSio)
                        For i As Integer = 0 To UBound(gudt.SetChSioCh)
                            Call mCopyStructure(mudtSetChSioChNew(i), gudt.SetChSioCh(i))
                        Next

                        'Ver2.0.5.8
                        Call mCopyStructure(mudtSetChSioExtNew, gudt.SetChSioExt)


                        ''更新フラグ設定
                        gblnUpdateAll = True

                        If blnFlgSio Then
                            gudt.SetEditorUpdateInfo.udtSave.bytChSio = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytChSio = 1
                        End If

                        For i As Integer = 0 To UBound(blnFlgSioCh)
                            If blnFlgSioCh(i) Then
                                gudt.SetEditorUpdateInfo.udtSave.bytChSioCh(i) = 1
                                gudt.SetEditorUpdateInfo.udtCompile.bytChSioCh(i) = 1
                            End If
                        Next

                        'Ver2.0.5.8
                        If blnFlgSioExt Then
                            For i As Integer = 0 To UBound(gudt.SetEditorUpdateInfo.udtSave.bytChSioExt)
                                gudt.SetEditorUpdateInfo.udtSave.bytChSioExt(i) = 1
                                gudt.SetEditorUpdateInfo.udtCompile.bytChSioExt(i) = 1
                            Next i
                        End If


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

    'SIO拡張チェッククリック
    Private Sub chkSioExt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSioExt.CheckedChanged
        Try
            If prFirst = True Then
                prFirst = False
                Exit Sub
            End If

            Dim oldType As Byte

            'Ver2.0.7.C (外販)SIOポート拡張ﾌﾗｸﾞ
            oldType = g_bytSIOport
            If chkSioExt.Checked = True Then
                g_bytSIOport = 1
            Else
                g_bytSIOport = 0
            End If

            '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
            If oldType <> g_bytSIOport Then
                'Verup処理中ならば何もしない
                If gudtFileInfo.blnVersionUP Then
                    'Debug.Print("Verup")
                Else
                    gblnUpdateAll = True
                End If
            End If


            'コンボボックス初期設定
            'ｸﾘｱのコンボは、iniﾌｧｲﾙではなく自作とする
            cmbPort.Items.Clear()
            If chkSioExt.Checked = True Then
                With cmbPort
                    .Items.Add("")
                    For i As Integer = 1 To 9 Step 1
                        .Items.Add(i.ToString)
                    Next i
                    .Items.Add("ALL")
                End With
            Else
                With cmbPort
                    .Items.Add("")
                    For i As Integer = 1 To 5 Step 1
                        .Items.Add(i.ToString)
                    Next i
                    .Items.Add("ALL")
                End With
            End If

            Call mInitialDataGrid()
            Call mSetDisplay(mudtSetChSioNew, True)
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

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

            ''グリッドの保留中の変更を全て適用させる
            grdSIO.EndEdit()

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtPortNo" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chkUse"
            Dim Column3 As New DataGridViewComboBoxColumn : Column3.Name = "cmbComuntication" : Column3.FlatStyle = FlatStyle.Popup


            With grdSIO

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Port No."
                .Columns(0).Width = 80
                .Columns(0).ReadOnly = True
                Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Use/Not Use"
                .Columns(1).Width = 90

                .Columns(2).HeaderText = "Communication"
                .Columns(2).Width = 470

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                'SIO拡張か否かで分岐
                .AllowUserToAddRows = True
                If chkSioExt.Checked = True Then
                    .RowCount = printSIOext
                Else
                    .RowCount = printSIOnor
                End If

                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersVisible = False

                ''Port No.
                For i = 1 To .RowCount
                    .Rows(i - 1).Cells(0).Value = i.ToString
                Next

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtPortNo").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdSIO)


                'Communicationコンボ 初期設定
                'コンボは、iniﾌｧｲﾙではなく自作とする
                With Column3
                    .Items.Add("")
                    .Items.Add("VDR")
                    .Items.Add("MODBUS-RTU")
                    .Items.Add("JRCS-STANDARD")
                End With

            End With


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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChSio, _
                               ByRef udtTarget As gTypSetChSio)

        Try

            ''CH設定数レコードコピー
            For i As Integer = 0 To UBound(udtSource.shtNum)
                udtTarget.shtNum(i) = udtSource.shtNum(i)
            Next

            ''VDR情報コピー
            For i As Integer = LBound(udtSource.udtVdr) To UBound(udtSource.udtVdr)

                udtTarget.udtVdr(i).shtPort = udtSource.udtVdr(i).shtPort                               ''ポート番号
                udtTarget.udtVdr(i).shtExtComID = udtSource.udtVdr(i).shtExtComID                       ''外部機器識別子
                udtTarget.udtVdr(i).shtPriority = udtSource.udtVdr(i).shtPriority                       ''優先度
                udtTarget.udtVdr(i).shtSysno = udtSource.udtVdr(i).shtSysno                             ''SYSTEM NO
                udtTarget.udtVdr(i).shtCommType1 = udtSource.udtVdr(i).shtCommType1                     ''i/o種類
                udtTarget.udtVdr(i).udtCommInf.shtComm = udtSource.udtVdr(i).udtCommInf.shtComm         ''回線情報（回線種類）
                udtTarget.udtVdr(i).udtCommInf.shtDataBit = udtSource.udtVdr(i).udtCommInf.shtDataBit   ''回線情報（データビット）
                udtTarget.udtVdr(i).udtCommInf.shtParity = udtSource.udtVdr(i).udtCommInf.shtParity     ''回線情報（パリティ）
                udtTarget.udtVdr(i).udtCommInf.shtStop = udtSource.udtVdr(i).udtCommInf.shtStop         ''回線情報（ストップビット）
                udtTarget.udtVdr(i).udtCommInf.shtComBps = udtSource.udtVdr(i).udtCommInf.shtComBps     ''回線情報（通信速度）
                udtTarget.udtVdr(i).udtCommInf.shtSpare1 = udtSource.udtVdr(i).udtCommInf.shtSpare1     ''回線情報（予備）
                udtTarget.udtVdr(i).udtCommInf.shtSpare2 = udtSource.udtVdr(i).udtCommInf.shtSpare2     ''回線情報（予備）
                udtTarget.udtVdr(i).udtCommInf.shtSpare3 = udtSource.udtVdr(i).udtCommInf.shtSpare3     ''回線情報（予備）
                udtTarget.udtVdr(i).shtCommType2 = udtSource.udtVdr(i).shtCommType2                     ''通信種類
                udtTarget.udtVdr(i).shtReceiveInit = udtSource.udtVdr(i).shtReceiveInit                 ''受信タイムアウト（秒）起動時
                udtTarget.udtVdr(i).shtReceiveUseally = udtSource.udtVdr(i).shtReceiveUseally           ''受信タイムアウト（秒）起動後
                udtTarget.udtVdr(i).shtSendInit = udtSource.udtVdr(i).shtSendInit                       ''送信間隔（秒）起動時
                udtTarget.udtVdr(i).shtSendUseally = udtSource.udtVdr(i).shtSendUseally                 ''送信間隔（秒）起動後
                udtTarget.udtVdr(i).shtRetry = udtSource.udtVdr(i).shtRetry                             ''リトライ回数
                udtTarget.udtVdr(i).shtDuplexSet = udtSource.udtVdr(i).shtDuplexSet                     ''Duplex 設定
                udtTarget.udtVdr(i).shtSendCH = udtSource.udtVdr(i).shtSendCH                           ''送信CH

                udtTarget.udtVdr(i).shtKakuTbl = udtSource.udtVdr(i).shtKakuTbl                         '拡張ﾃｰﾌﾞﾙ使用有無

                ''ノード情報コピー
                For j As Integer = LBound(udtSource.udtVdr(i).udtNode) To UBound(udtSource.udtVdr(i).udtNode)
                    udtTarget.udtVdr(i).udtNode(j).shtCheck = udtSource.udtVdr(i).udtNode(j).shtCheck
                    udtTarget.udtVdr(i).udtNode(j).shtAddress = udtSource.udtVdr(i).udtNode(j).shtAddress
                Next

                ''バイナリ詳細設定コピー
                For j As Integer = LBound(udtSource.udtVdr(i).bytSetData) To UBound(udtSource.udtVdr(i).bytSetData)
                    udtTarget.udtVdr(i).bytSetData(j) = udtSource.udtVdr(i).bytSetData(j)
                Next

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
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChSio, _
                                         ByVal udt2 As gTypSetChSio) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.shtNum)
                If udt1.shtNum(i) <> udt2.shtNum(i) Then Return False
            Next

            ''VDR情報チェック
            For i As Integer = LBound(udt1.udtVdr) To UBound(udt2.udtVdr)

                If udt1.udtVdr(i).shtPort <> udt2.udtVdr(i).shtPort Then Return False ''ポート番号
                If udt1.udtVdr(i).shtExtComID <> udt2.udtVdr(i).shtExtComID Then Return False ''外部機器識別子
                If udt1.udtVdr(i).shtPriority <> udt2.udtVdr(i).shtPriority Then Return False ''優先度
                If udt1.udtVdr(i).shtSysno <> udt2.udtVdr(i).shtSysno Then Return False ''SYSTEM NO
                If udt1.udtVdr(i).shtCommType1 <> udt2.udtVdr(i).shtCommType1 Then Return False ''i/o種類
                If udt1.udtVdr(i).udtCommInf.shtComm <> udt2.udtVdr(i).udtCommInf.shtComm Then Return False ''回線情報（回線種類）
                If udt1.udtVdr(i).udtCommInf.shtDataBit <> udt2.udtVdr(i).udtCommInf.shtDataBit Then Return False ''回線情報（データビット）
                If udt1.udtVdr(i).udtCommInf.shtParity <> udt2.udtVdr(i).udtCommInf.shtParity Then Return False ''回線情報（パリティ）
                If udt1.udtVdr(i).udtCommInf.shtStop <> udt2.udtVdr(i).udtCommInf.shtStop Then Return False ''回線情報（ストップビット）
                If udt1.udtVdr(i).udtCommInf.shtComBps <> udt2.udtVdr(i).udtCommInf.shtComBps Then Return False ''回線情報（通信速度）
                If udt1.udtVdr(i).udtCommInf.shtSpare1 <> udt2.udtVdr(i).udtCommInf.shtSpare1 Then Return False ''回線情報（予備1）
                If udt1.udtVdr(i).udtCommInf.shtSpare2 <> udt2.udtVdr(i).udtCommInf.shtSpare2 Then Return False ''回線情報（予備2）
                If udt1.udtVdr(i).udtCommInf.shtSpare3 <> udt2.udtVdr(i).udtCommInf.shtSpare3 Then Return False ''回線情報（予備3）
                If udt1.udtVdr(i).shtCommType2 <> udt2.udtVdr(i).shtCommType2 Then Return False ''通信種類
                If udt1.udtVdr(i).shtReceiveInit <> udt2.udtVdr(i).shtReceiveInit Then Return False ''受信タイムアウト（秒）起動時
                If udt1.udtVdr(i).shtReceiveUseally <> udt2.udtVdr(i).shtReceiveUseally Then Return False ''受信タイムアウト（秒）起動後
                If udt1.udtVdr(i).shtSendInit <> udt2.udtVdr(i).shtSendInit Then Return False ''送信間隔（秒）起動時
                If udt1.udtVdr(i).shtSendUseally <> udt2.udtVdr(i).shtSendUseally Then Return False ''送信間隔（秒）起動後
                If udt1.udtVdr(i).shtRetry <> udt2.udtVdr(i).shtRetry Then Return False ''リトライ回数
                If udt1.udtVdr(i).shtDuplexSet <> udt2.udtVdr(i).shtDuplexSet Then Return False ''Duplex 設定
                If udt1.udtVdr(i).shtSendCH <> udt2.udtVdr(i).shtSendCH Then Return False ''送信CH

                If udt1.udtVdr(i).shtKakuTbl <> udt2.udtVdr(i).shtKakuTbl Then Return False '拡張テーブル使用


                ''ノード情報チェック
                For j As Integer = LBound(udt2.udtVdr(i).udtNode) To UBound(udt2.udtVdr(i).udtNode)
                    If udt1.udtVdr(i).udtNode(j).shtCheck <> udt2.udtVdr(i).udtNode(j).shtCheck Then Return False
                    If udt1.udtVdr(i).udtNode(j).shtAddress <> udt2.udtVdr(i).udtNode(j).shtAddress Then Return False
                Next

                ''バイナリ詳細設定チェック (長さ項目変更)　T.Ueki
                For j As Integer = LBound(udt2.udtVdr(i).bytSetData) To UBound(udt2.udtVdr(i).bytSetData)
                    'For j As Integer = LBound(udt2.udtVdr(i).udtNode) To UBound(udt2.udtVdr(i).udtNode)

                    If udt1.udtVdr(i).bytSetData(j) <> udt2.udtVdr(i).bytSetData(j) Then Return False
                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChSioExt, _
                                         ByVal udt2 As gTypSetChSioExt) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.bytSioExtRec)
                If udt1.bytSioExtRec(i) <> udt2.bytSioExtRec(i) Then Return False
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChSioCh, _
                               ByRef udtTarget As gTypSetChSioCh)

        Try

            ''CH設定コピー
            For i As Integer = LBound(udtSource.udtSioChRec) To UBound(udtSource.udtSioChRec)

                udtTarget.udtSioChRec(i).shtChNo = udtSource.udtSioChRec(i).shtChNo             ''チャンネルNo
                udtTarget.udtSioChRec(i).shtChId = udtSource.udtSioChRec(i).shtChId             ''チャンネルID
                ' ''udtTarget.udtSioChRec(i).shtSize = udtSource.udtSioChRec(i).shtSize         ''サイズ 2011.12.15 K.Tanigawa

            Next

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
    Private Sub mCopyStructure(ByVal udtSource() As gTypSetChSioExt, _
                               ByRef udtTarget() As gTypSetChSioExt)

        Try

            '拡張設定コピー
            For i As Integer = LBound(udtSource) To UBound(udtSource) Step 1
                For j As Integer = LBound(udtSource(i).bytSioExtRec) To UBound(udtSource(i).bytSioExtRec)
                    udtTarget(i).bytSioExtRec(j) = udtSource(i).bytSioExtRec(j)   'バイナリ値o
                Next j
            Next i


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
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChSioCh, _
                                         ByVal udt2 As gTypSetChSioCh) As Boolean

        Try

            ''CH設定比較
            For i As Integer = LBound(udt1.udtSioChRec) To UBound(udt1.udtSioChRec)

                If udt1.udtSioChRec(i).shtChNo <> udt2.udtSioChRec(i).shtChNo Then Return False ''チャンネルNo
                If udt1.udtSioChRec(i).shtChId <> udt2.udtSioChRec(i).shtChId Then Return False ''チャンネルID
                ' ''If udt1.udtSioChRec(i).shtSize <> udt2.udtSioChRec(i).shtSize Then Return False ''サイズ   2011.12.15 K.Tanigawa

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) SIO設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChSio)

        Try

            For i As Integer = 0 To grdSIO.Rows.Count - 1

                With udtSet.udtVdr(i)

                    .shtPort = IIf(grdSIO.Item(1, i).Value, 1, 0)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) SIO設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChSio, ByVal blnSetUse As Boolean)

        Try
            For i As Integer = 0 To grdSIO.Rows.Count - 1

                With udtSet.udtVdr(i)

                    '使用可/不可
                    If blnSetUse Then grdSIO.Item(1, i).Value = IIf(.shtPort = 0, False, True)

                    'Communication
                    'CommType1:0x05＝VDR(1)、0x03＝MODBUS(2)
                    Select Case Hex(.shtCommType1)
                        Case &H5    'VDR or JRCS STANDARD
                            Select Case Hex(.shtCommType2)
                                Case &H11
                                    grdSIO.Item(2, i).Value = "VDR"
                                Case &H21
                                    grdSIO.Item(2, i).Value = "JRCS-STANDARD"
                            End Select

                        Case &H3    'MODBUS-RTU
                            grdSIO.Item(2, i).Value = "MODBUS-RTU"
                        Case Else
                            grdSIO.Item(2, i).Value = ""
                    End Select
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
