Public Class frmChExtLANDetail

#Region "変数定義"

    Private mintRtn As Integer
    Private mintNowSelectIndex As Integer
    Private mblnInitFlg As Boolean
    Private mshtNum() As Short
    Private mudtVdr() As gTypSetChSioVdr
    Private mudtSioCh() As gTypSetChSioCh

    Private mudtSioExt() As gTypSetChSioExt

    Private mblnCopyPasteFlg As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (I ) 画面表示時のインデックス
    ' 　　　    : ARG2 - (IO) VDR情報構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intCurIndex As Integer, _
                          ByRef shtNum() As Short, _
                          ByRef udtVdr() As gTypSetChSioVdr, _
                          ByRef udtSioCh() As gTypSetChSioCh, _
                          ByRef udtSioExt() As gTypSetChSioExt, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mintNowSelectIndex = intCurIndex
            mshtNum = DeepCopyHelper.DeepCopy(shtNum)
            mudtVdr = DeepCopyHelper.DeepCopy(udtVdr)
            mudtSioCh = udtSioCh

            mudtSioExt = udtSioExt

            ''本画面表示
            Call gShowFormModelessForCloseWait22(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                shtNum = DeepCopyHelper.DeepCopy(mshtNum)
                udtVdr = DeepCopyHelper.DeepCopy(mudtVdr)
                udtSioCh = mudtSioCh
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChSioDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化開始
            mblnInitFlg = True

            ''コンボボックス初期設定
            Call gSetComboBox(cmbPort, gEnmComboType.ctChSioDetailcmbPort)
            Call gSetComboBox(cmbPriority, gEnmComboType.ctChSioDetailcmbPriority)
            Call gSetComboBox(cmbMC, gEnmComboType.ctChSioDetailcmbMC)
            Call gSetComboBox(cmbCom, gEnmComboType.ctChSioDetailcmbCom)
            Call gSetComboBox(cmbParityBit, gEnmComboType.ctChSioDetailcmbParityBit)
            Call gSetComboBox(cmbDataBit, gEnmComboType.ctChSioDetailcmbDataBit)
            Call gSetComboBox(cmbStopBit, gEnmComboType.ctChSioDetailcmbStopBit)
            Call gSetComboBox(cmbDuplet, gEnmComboType.ctChSioDetailcmbDuplet)
            Call gSetComboBox(cmbCommType1, gEnmComboType.ctChSioDetailcmbCommType1)
            Call gSetComboBox(cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, 1)
            cmbPort.SelectedIndex = mintNowSelectIndex

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ' ''画面設定
            Call mSetDisplay(mudtVdr((cmbPort.SelectedIndex + 14)))         ''2019.03.16 ExtLAN用に+14する
            Call mSetDisplaySioCh(mudtSioCh((cmbPort.SelectedIndex + 14)))  ''2019.03.16 ExtLAN用に+14する

            ''初期化終了
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Makeボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMake.Click

        Try

            Dim udtSioCh As gTypSetChSioCh = Nothing

            ''確認メッセージ
            If MessageBox.Show("Do you make transmission CH data of port " & mintNowSelectIndex + 1 & "?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Call udtSioCh.InitArray()
                Call gMakeSioTransmissionChData(mintNowSelectIndex, udtSioCh)
                Call mSetDisplaySioCh(udtSioCh)
                Call MessageBox.Show("Data creation was completed.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Importクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : CSVからデータを読み込む
    '--------------------------------------------------------------------
    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click

        Try

            Dim strCol1() As String = Nothing
            Dim strCol2() As String = Nothing
            Dim dlgFile As New OpenFileDialog

            With dlgFile

                ''[ファイルの種類] ボックスに表示される選択肢を設定する
                .Filter = "csv file (*.csv)|*.csv"

                ''ダイアログ ボックスを表示
                If dlgFile.ShowDialog() = DialogResult.OK Then

                    ''CSVデータ取得
                    If gGetCsvData(dlgFile.FileName, grdCH.RowCount - 1, strCol1, strCol2) = 0 Then

                        For i As Integer = 0 To grdCH.RowCount - 1
                            'grdCH(0, i).Value = strCol1(i)
                            grdCH(1, i).Value = strCol1(i)      '' 1列目をCH NOとする     2014.02.04
                        Next

                    End If

                End If

            End With

            For i As Integer = 0 To grdCH.RowCount - 1
                Call mDispTransmisionChName(i)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    Private Sub grdCH_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdCH.DataError

        Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub




    '----------------------------------------------------------------------------
    ' 機能説明  ： ポートコンボチェンジ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPort.SelectedIndexChanged

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            ''ここでの項目変更イベントは処理しない
            mblnInitFlg = True

            ''入力チェック
            If Not mChkInput() Then

                ''入力NGの場合はTableNoを元に戻す
                cmbPort.SelectedIndex = mintNowSelectIndex

            Else

                ''現在のPortNoに設定されている値を保存
                Call mSetStructure(mshtNum(mintNowSelectIndex), mudtVdr(mintNowSelectIndex), mudtSioCh(mintNowSelectIndex))

                ''選択されたPortNoの情報を表示
                Call mSetDisplay(mudtVdr(cmbPort.SelectedIndex))
                Call mSetDisplaySioCh(mudtSioCh(cmbPort.SelectedIndex))

                ''現在のTableNoを更新
                mintNowSelectIndex = cmbPort.SelectedIndex

            End If

            ''元に戻す
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CommType1選択
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbCommType1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCommType1.SelectedValueChanged

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            ''CommType1に対応した項目をCommType2に設定する
            If gSetComboBox(cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, cmbCommType1.SelectedValue) = 1 Then
                Call gSetComboBox(cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, 99)
            End If

            cmbCommType2.SelectedIndex = 0

            'If mSetComboAdd(.bytCommType1, cmbCommType1, gEnmComboType.ctChSioDetailcmbCommType1) = 0 Then

            '    Call gSetComboBox(cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, .bytCommType1)
            '    cmbCommType2.SelectedValue = .bytCommType2
            '    If cmbCommType2.Text = "" Then cmbCommType2.SelectedIndex = 0

            'Else

            '    Call gSetComboBox(cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, 99)
            '    cmbCommType2.SelectedValue = 0
            '    'Call mSetComboAdd(.bytCommType2, cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2)

            'End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値格納 '2019.03.18 +14にする
            Call mSetStructure(mshtNum(mintNowSelectIndex) + 14, mudtVdr((cmbPort.SelectedIndex) + 14), mudtSioCh((cmbPort.SelectedIndex) + 14))

            mintRtn = 1
            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Binaryボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdBinary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBinary.Click

        Try

            Dim bytArray() As Byte = Nothing
            Dim shtNum As Short = Nothing
            Dim udtVdr As gTypSetChSioVdr = Nothing

            ''入力チェック
            If Not mChkInput() Then Return

            ''配列確保
            Call gInitSetChSioVdr(udtVdr)

            ''画面設定を構造体に格納
            Call mSetStructure(shtNum, udtVdr, mudtSioCh(cmbPort.SelectedIndex + 14))

            ''構造体データをバイト配列に変換
            Call mConvTypeToByteArray(udtVdr, bytArray)

            '' Ver2.0.8.N バイナリデータ参照元番号修正
            ''バイナリエディタ画面を開く
            If frmChSioBinary.gShow(cmbPort.Text, bytArray, mudtVdr(cmbPort.SelectedIndex + 14).bytSetData, Me) = 1 Then

                ''バイト配列を構造体データに戻す
                Call mConvByteArrayToType(bytArray, udtVdr)

                ''初期化中フラグ
                mblnInitFlg = True

                ''構造体データを画面に表示
                Call mSetDisplay(udtVdr)
                Call mSetDisplaySioCh(mudtSioCh(cmbPort.SelectedIndex + 14))

                ''初期化中フラグ
                mblnInitFlg = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'SIO拡張用のBinary画面表示
    Private Sub btnBinaryExt_Click(sender As System.Object, e As System.EventArgs) Handles btnBinaryExt.Click
        '使用チェックオフなら何もしない
        If chkSioExt.Checked = True Then
            'バイナリエディタ画面を開く'2019.03.18 +14にする
            If frmChSioBinaryext.gShow(cmbPort.Text, mudtSioExt(cmbPort.SelectedIndex + 14).bytSioExtRec, Me) = 1 Then
                ''初期化中フラグ
                mblnInitFlg = True

                ''初期化中フラグ
                mblnInitFlg = False
            End If
        End If
    End Sub

    Private Sub txtCommType2ManualInputValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCommType2ManualInputValue.KeyPress

        Try

            ''文字を大文字に変換
            Select Case e.KeyChar
                Case "a"c : e.KeyChar = "A"c
                Case "b"c : e.KeyChar = "B"c
                Case "c"c : e.KeyChar = "C"c
                Case "d"c : e.KeyChar = "D"c
                Case "e"c : e.KeyChar = "E"c
                Case "f"c : e.KeyChar = "F"c
            End Select

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, False, False, False, "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdCommType2Set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommType2Set.Click

        Try

            ''入力可能範囲外の場合は処理を抜ける
            If Not gChkInputText(txtCommType2ManualInputValue, "ManualInputValue", False, True) Then Return

            ''CommTypeコンボ設定
            Call mSetCommTypeCombo(cmbCommType1.SelectedValue, CCUInt16Hex(txtCommType2ManualInputValue.Text))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdNode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdNode.KeyPress

        Try

            If grdNode.SelectedCells.Count = 1 Then

                If grdNode.CurrentCell.OwningColumn.Name = "txtAddress" Then

                    'Dim objText As DataGridViewTextBoxEditingControl
                    'objText = CType(sender, DataGridViewTextBoxEditingControl)

                    'e.Handled = gCheckTextInput(2, objText, e.KeyChar, False)

                    ''文字を大文字に変換
                    Select Case e.KeyChar
                        Case "a"c : e.KeyChar = "A"c
                        Case "b"c : e.KeyChar = "B"c
                        Case "c"c : e.KeyChar = "C"c
                        Case "d"c : e.KeyChar = "D"c
                        Case "e"c : e.KeyChar = "E"c
                        Case "f"c : e.KeyChar = "F"c
                    End Select

                    e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Key操作イベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCH.KeyDown

        If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.C Then

            mblnCopyPasteFlg = True

        ElseIf (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then

            mblnCopyPasteFlg = True

            For i As Integer = 0 To grdCH.RowCount - 1

                If Trim(grdCH(1, i).Value) <> "" And _
                   Trim(grdCH(2, i).Value) = "" Then

                    Call mDispTransmisionChName(i)

                End If

            Next

        Else
            mblnCopyPasteFlg = False
        End If

    End Sub

    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress

        Try

            If mblnCopyPasteFlg Then Return

            If grdCH.SelectedCells.Count = 1 Then

                If grdCH.CurrentCell.OwningColumn.Name = "txtChNo" Then

                    'Dim objText As DataGridViewTextBoxEditingControl
                    'objText = CType(sender, DataGridViewTextBoxEditingControl)

                    e.Handled = gCheckTextInput(5, sender, e.KeyChar, False)    '' 数値以外も入力可

                    'Dim objText As DataGridViewTextBoxEditingControl
                    'objText = CType(sender, DataGridViewTextBoxEditingControl)

                    'e.Handled = gCheckTextInput(2, objText, e.KeyChar, False)

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub grdCH_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCH.KeyUp
        mblnCopyPasteFlg = False
    End Sub

    Private Sub txtExtComID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtExtComID.KeyPress

        Try

            ''文字を大文字に変換
            Select Case e.KeyChar
                Case "a"c : e.KeyChar = "A"c
                Case "b"c : e.KeyChar = "B"c
                Case "c"c : e.KeyChar = "C"c
                Case "d"c : e.KeyChar = "D"c
                Case "e"c : e.KeyChar = "E"c
                Case "f"c : e.KeyChar = "F"c
            End Select

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, False, False, False, "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtUseallyTimeout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUseallyTimeout.KeyPress

        Try
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする　ver.1.4.0 2011.09.21
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtInitialTimeout_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInitialTimeout.KeyPress

        Try
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする　ver.1.4.0 2011.09.21
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtInitialTransmit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInitialTransmit.KeyPress

        Try
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする　ver.1.4.0 2011.09.21
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtUseallyTransmit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUseallyTransmit.KeyPress

        Try
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする　ver.1.4.0 2011.09.21
            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtNumberCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumberCH.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub txtRetryCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRetryCount.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbMC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbMC.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbCom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCom.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbParityBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbParityBit.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbDataBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbDataBit.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbStopBit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbStopBit.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbDuplet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbDuplet.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbCommType1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCommType1.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub cmbCommType2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCommType2.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCH.CellValidating

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            Call grdCH.EndEdit()

            If strColumnName = "cmbType" Or strColumnName = "txtChNo" Then
                Call mDispTransmisionChName(e.RowIndex)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    'コピペ対応：複数件コピペで名称が変わるように処理
    Private Sub grdCH_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellValueChanged
        Try
            If e.RowIndex < 0 Then
                Return
            End If
            If e.ColumnIndex < 0 Then
                Return
            End If

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            Call grdCH.EndEdit()

            If strColumnName = "cmbType" Or strColumnName = "txtChNo" Then
                Call mDispTransmisionChName(e.RowIndex)
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub grdNode_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdNode.CellValidating

    '    If e.RowIndex < 0 Or e.ColumnIndex <> 2 Then Exit Sub

    '    Dim dgv As DataGridView = CType(sender, DataGridView)
    '    Dim strValue As String

    '    ''グリッドの保留中の変更を全て適用させる
    '    dgv.EndEdit()

    '    If dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then

    '        strValue = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

    '        If IsNumeric(strValue) Then

    '            If (strValue = "0") Or (strValue >= "49" And strValue <= "63") Or (strValue >= "65" And strValue <= "79") Then
    '                ''OK
    '            Else
    '                MsgBox("Please set Input_Signal number among '49'-'63' '65'-'79'.", MsgBoxStyle.Exclamation, "SIO_Set")
    '                e.Cancel = True
    '                Exit Sub
    '            End If

    '        Else

    '            If (strValue >= "RA" And strValue <= "RO") Or (strValue >= "PA" And strValue <= "PO") Or _
    '               (strValue >= "ra" And strValue <= "ro") Or (strValue >= "pa" And strValue <= "po") Then
    '                ''OK
    '            Else
    '                MsgBox("Please set Input_Signal among 'RA'-'RO' 'PA'-'PO'.", MsgBoxStyle.Exclamation, "SIO_Set")
    '                e.Cancel = True
    '                Exit Sub
    '            End If

    '        End If

    '    End If

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtUseallyTimeout_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUseallyTimeout.Validating
    '    e.Cancel = gChkTextNumSpan(0, 240, sender.Text)
    'End Sub
    'Private Sub txtInitialTimeout_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInitialTimeout.Validating
    '    e.Cancel = gChkTextNumSpan(0, 600, sender.Text)
    'End Sub
    'Private Sub txtInitialTransmit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInitialTransmit.Validating
    '    e.Cancel = gChkTextNumSpan(0, 300, sender.Text)
    'End Sub
    'Private Sub txtUseallyTransmit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUseallyTransmit.Validating
    '    e.Cancel = gChkTextNumSpan(0, 120, sender.Text)
    'End Sub
    'Private Sub txtNumberCH_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNumberCH.Validating
    '    e.Cancel = gChkTextNumSpan(0, 3000, sender.Text)
    'End Sub
    'Private Sub cmbMC_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbMC.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbCom_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCom.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbParityBit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbParityBit.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbDataBit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbDataBit.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbStopBit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbStopBit.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbDuplet_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbDuplet.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbCommType1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCommType1.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub
    'Private Sub cmbCommType2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbCommType2.Validating
    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)
    'End Sub

#End Region

#Region "イベントハンドラ操作"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdNode_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdNode.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdNode_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdNode_KeyPress

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
    Private Sub grdCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コンボ"

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボボックスを1回のクリックでドロップダウンさせるようにする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellEnter

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            'Dim dgv As DataGridView = CType(sender, DataGridView)

            'If dgv.Columns(e.ColumnIndex).Name.Substring(0, 3) = "cmb" Then

            '    SendKeys.Send("{F4}")

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

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

            'Dim strValue As String

            ''共通数値入力チェック
            ''SIOタイムアウト、送信間隔を0xFFFFまで設定可能とする　ver.1.4.0 2011.09.21
            If Not gChkInputNum(txtInitialTimeout, 0, 65535, "InitialTimeout", True, True) Then Return False
            If Not gChkInputNum(txtUseallyTimeout, 0, 65535, "UsuallyTimeout", True, True) Then Return False
            If Not gChkInputNum(txtInitialTransmit, 0, 65535, "InitialTransmit", True, True) Then Return False
            If Not gChkInputNum(txtUseallyTransmit, 0, 65535, "UsuallyTransmit", True, True) Then Return False
            If Not gChkInputNum(txtNumberCH, 0, 3000, "NumberCH", True, True) Then Return False
            If Not gChkInputNum(txtRetryCount, 0, 5, "RetryCount", True, True) Then Return False

            ''特殊入力チェック
            For i As Integer = 0 To grdNode.RowCount - 1

                If Not gChkInputNum(CCUInt32Hex(grdNode.Rows(i).Cells(2).Value), 0, 65535, "NODE", i + 1, True, True) Then Return False


                ' ''値取得
                'strValue = grdNode.Rows(i).Cells(2).Value

                ' ''数値として判断出来る場合
                'If IsNumeric(strValue) Then

                '    ''入力可能な数値かチェック
                '    If (strValue = "0") Or (strValue >= "49" And strValue <= "63") Or (strValue >= "65" And strValue <= "79") Then
                '        ''OK
                '    Else
                '        Call MessageBox.Show("Please set [Node Info] number among '49'-'63' '65'-'79'." & vbNewLine & vbNewLine & _
                '                             "[ Col ] Address " & vbNewLine & "[ Row ] " & i + 1, _
                '                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Return False
                '    End If

                'Else

                '    ''入力可能な文字かチェック
                '    If (strValue >= "RA" And strValue <= "RO") Or (strValue >= "PA" And strValue <= "PO") Or _
                '       (strValue >= "ra" And strValue <= "ro") Or (strValue >= "pa" And strValue <= "po") Then
                '        ''OK
                '    Else
                '        Call MessageBox.Show("Please set [Node Info] among 'RA'-'RO' 'PA'-'PO'." & vbNewLine & vbNewLine & _
                '                             "[ Col ] Address " & vbNewLine & "[ Row ] " & i + 1, _
                '                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Return False
                '    End If

                'End If

            Next

            For i As Integer = 0 To grdCH.RowCount - 1

                With grdCH.Rows(i)

                    If .Cells(0).Value > 0 Then

                        If .Cells(1).Value <> Nothing Then

                            ''Data
                            ''区切り文字「@NEXT」追加
                            If (.Cells(1).Value = "@NEXT") Then

                            Else
                                If Not IsNumeric(.Cells(1).Value) Or .Cells(1).Value = "0000" Or .Cells(1).Value = "00000" Then
                                    MsgBox("Please set Transmission CH [Data]." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                                    Return False
                                End If

                                If Integer.Parse(.Cells(1).Value) > 65535 Then
                                    MsgBox("Please set Transmission CH [Data]. '0'-'65535'." & vbCrLf & "Line:" & i + 1.ToString, MsgBoxStyle.Exclamation, "Input error")
                                    Return False
                                End If
                            End If

                        End If

                    End If

                End With

            Next


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mDispTransmisionChName(ByVal intRow As Integer)

        Try

            '' If CCInt(grdCH("cmbType", intRow).Value) = 1 Then
            If grdCH("txtCHNo", intRow).Value <> "" Then
                grdCH("txtItemName", intRow).Value = gGetChNoToChName(CCInt(grdCH("txtChNo", intRow).Value))
            Else
                grdCH("txtItemName", intRow).Value = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbType"  ' ''★★★ 2011.12.13 K.Tanigawa
            ' ''Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "cmbType"  ' ''★★★
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChNo"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtItemName" : Column3.ReadOnly = True

            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtNodeNo" : Column4.ReadOnly = True
            Dim Column5 As New DataGridViewCheckBoxColumn : Column5.Name = "chkCheck"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtAddress"

            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Column1.Items.Add("CH NO.")
            'Column1.Items.Add("Data Length")

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)  ' ''★★★ 2012.12.15 K.Tanigawa
                ' '' ''.Columns.Add(Column2) : .Columns.Add(Column3)   ' ''★★★
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Type"        ''★★★ 2012.12.15 K.Tanigwa
                .Columns(0).Width = 0                  ' '' 100 -> 0
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Data"
                .Columns(1).Width = 80

                .Columns(2).HeaderText = "Item Name"
                .Columns(2).Width = 288                 ' '' 188 -> 288

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 3000 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                'Ver2.0.3.6 行番号を出す
                .RowHeadersVisible = True
                For i = 0 To .RowCount - 1 Step 1
                    .Rows(i).HeaderCell.Value = (i + 1).ToString
                Next i
                .RowHeadersWidth = 60

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtItemName").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ' '' '' コンボボックス初期設定    ''★★★ 2011.12.15 K.Tanigawa
                ' ''Call gSetComboBox(Column1, gEnmComboType.ctChSioDetailcmbTransmisionCh)

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

            End With

            With grdNode

                ''列
                .Columns.Clear()
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "NODE No."
                .Columns(0).Width = 88

                .Columns(1).HeaderText = "Check"
                .Columns(1).Width = 80

                .Columns(2).HeaderText = "Address(Hex)"
                .Columns(2).Width = 149

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersVisible = False

                ''Node No. 列
                For i = 1 To .Rows.Count
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
                    .Rows(i).Cells("txtNodeNo").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdNode)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub mSetCommTypeCombo(ByVal shtValueCommType1 As Short, ByVal shtValueCommType2 As Short)

        Try

            ''CommType1
            If mSetComboAdd(shtValueCommType1, cmbCommType1, gEnmComboType.ctChSioDetailcmbCommType1) = 0 Then

                '================================================
                ''CommType1の項目がリストに存在した場合
                '================================================
                Call mSetComboAdd(shtValueCommType2, cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, shtValueCommType1)
                cmbCommType2.SelectedValue = shtValueCommType2
                If cmbCommType2.Text = "" Then cmbCommType2.SelectedIndex = 0

            Else

                '================================================
                ''CommType1の項目がリストに存在せず追加した場合
                '================================================
                Call mSetComboAdd(shtValueCommType2, cmbCommType2, gEnmComboType.ctChSioDetailcmbCommType2, 99)
                cmbCommType2.SelectedValue = shtValueCommType2
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) VDR情報構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef shtNum As Short, _
                              ByRef udtVdr As gTypSetChSioVdr, _
                              ByRef udtSioCh As gTypSetChSioCh)

        Try

            Dim intChCnt As Integer = 0

            With udtVdr

                .shtPriority = cmbPriority.SelectedValue            ''Priority
                .shtSysno = cmbMC.SelectedValue                     ''M/C set
                .udtCommInf.shtComBps = cmbCom.SelectedValue        ''COM bps
                .udtCommInf.shtParity = cmbParityBit.SelectedValue  ''Parity bit
                .udtCommInf.shtDataBit = cmbDataBit.SelectedValue   ''Data bit
                .udtCommInf.shtStop = cmbStopBit.SelectedValue      ''Stop bit
                .shtDuplexSet = cmbDuplet.SelectedValue             ''Duplet set
                .shtCommType1 = cmbCommType1.SelectedValue          ''CommType1
                .shtCommType2 = cmbCommType2.SelectedValue          ''CommType2

                .shtExtComID = CCUInt16Hex(txtExtComID.Text)            ''ExtComID
                .shtReceiveInit = CCUInt16(txtInitialTimeout.Text)      ''Receive time out(initial)
                .shtReceiveUseally = CCUInt16(txtUseallyTimeout.Text)   ''Receive time out(useally)
                .shtSendInit = CCUInt16(txtInitialTransmit.Text)        ''Transmit Waiting time(initial)
                .shtSendUseally = CCUInt16(txtUseallyTransmit.Text)     ''Transmit Waiting time(useally)
                .shtSendCH = CCUInt16(txtNumberCH.Text)                 ''The number of CH
                .shtRetry = CCUInt16(txtRetryCount.Text)                ''RetryCount

                ''Nodeテーブル
                For i As Integer = LBound(.udtNode) To UBound(.udtNode)

                    .udtNode(i).shtCheck = IIf(grdNode(1, i).Value, 1, 0)

                    ''アドレス格納処理
                    If Trim(grdNode(2, i).Value) = "" Then

                        ''入力されていない場合は 0 をセット
                        .udtNode(i).shtAddress = 0

                    Else

                        ''入力されている場合は 16→10進変換を行って保存
                        .udtNode(i).shtAddress = CCUInt16Hex(grdNode(2, i).Value)

                        ' ''数値が入力されている場合
                        'If IsNumeric(Trim(grdNode(2, i).Value)) Then

                        '    ''入力値をそのままセット
                        '    .udtNode(i).shtAddress = Trim(grdNode(2, i).Value)

                        'Else

                        '    ''入力値を変換してセット
                        '    .udtNode(i).shtAddress = mConvNodeAddress(Trim(grdNode(2, i).Value))

                        'End If

                    End If

                Next

                'Ver2.0.5.8 SIO拡張対応
                If chkSioExt.Checked = True Then
                    .shtKakuTbl = 1
                Else
                    .shtKakuTbl = 0
                End If

            End With

            For i As Integer = 0 To UBound(udtSioCh.udtSioChRec)

                With udtSioCh.udtSioChRec(i)

                    If grdCH(1, i).Value <> "" Then      'CHNoが0で無ければ

                        If grdCH(1, i).Value = "@NEXT" Then
                            .shtChNo = 0
                            .shtChId = &HFFFE
                        Else
                            .shtChNo = CCUInt16(grdCH(1, i).Value)
                            .shtChId = 0
                            ' ''.shtSpare2 = 0
                        End If


                        intChCnt += 1
                    Else
                        .shtChNo = 0
                        .shtChId = 0
                        ' ''.shtSpare2 = 0

                    End If


                    ' ''Select Case grdCH(0, i).Value
                    ' ''    Case gCstCodeChSioCommChType1Nothing

                    ' ''        .shtChNo = 0
                    ' ''        .shtChId = 0
                    ' ''        .shtSize = 0

                    ' ''    Case gCstCodeChSioCommChType1ChData

                    ' ''        .shtChNo = CCUInt16(grdCH(1, i).Value)
                    ' ''        .shtChId = 0
                    ' ''        .shtSize = 0

                    ' ''        intChCnt += 1

                    ' ''    Case gCstCodeChSioCommChType1DataLength

                    ' ''        .shtChNo = 0
                    ' ''        .shtChId = 0
                    ' ''        .shtSize = CCUInt16(grdCH(1, i).Value)

                    ' ''End Select

                End With

            Next

            ''CH設定数
            shtNum = intChCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : コンボ設定
    ' 返り値    : 0:コンボに存在する値を選択
    ' 　　　    : 1:コンボに存在しない値を追加して選択
    ' 引き数    : ARG1 - (I ) 指定値
    ' 　　　    : ARG2 - (IO) コンボボックス
    ' 　　　    : ARG3 - (I ) コンボタイプ
    ' 　　　    : ARG4 - (I ) サブコード
    ' 機能説明  : 指定値が存在するか確認して存在しない場合は追加する
    '--------------------------------------------------------------------
    Private Function mSetComboAdd(ByVal shtValue As UInt16, _
                                  ByRef cmbCombo As ComboBox, _
                                  ByVal udtComboType As gEnmComboType, _
                         Optional ByVal strSub As String = "") As Integer

        Try

            Dim intRtn As Integer
            Dim blnFlg As Boolean = False

            ''指定された値がコンボボックスに存在するかチェック
            For i As Integer = 0 To cmbCombo.Items.Count - 1
                If shtValue = cmbCombo.Items(i).item(0) Then
                    blnFlg = True
                End If
            Next

            If blnFlg Then

                '===============================================
                ''指定された値がコンボボックスに存在する場合
                '===============================================
                ''コンボアイテムを再セットして値を選択する
                'Ver2.0.2.0 DEL
                'Call gSetComboBox(cmbCombo, udtComboType, strSub, shtValue, Hex(shtValue))
                cmbCombo.SelectedValue = shtValue
                intRtn = 0

            Else

                '===============================================
                ''指定された値がコンボボックスに存在しない場合
                '===============================================
                ''指定項目を追加して値を選択する
                Call gSetComboBox(cmbCombo, udtComboType, strSub, shtValue, Hex(shtValue))
                cmbCombo.SelectedValue = shtValue
                intRtn = 1

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mSetDisplay(ByVal udtVdr As gTypSetChSioVdr)

        Try

            With udtVdr

                Call mSetComboAdd(.shtPriority, cmbPriority, gEnmComboType.ctChSioDetailcmbPriority)            ''Priority
                Call mSetComboAdd(.shtSysno, cmbMC, gEnmComboType.ctChSioDetailcmbMC)                           ''M/C set
                Call mSetComboAdd(.udtCommInf.shtComBps, cmbCom, gEnmComboType.ctChSioDetailcmbCom)             ''COM bps
                Call mSetComboAdd(.udtCommInf.shtParity, cmbParityBit, gEnmComboType.ctChSioDetailcmbParityBit) ''Parity bit
                Call mSetComboAdd(.udtCommInf.shtDataBit, cmbDataBit, gEnmComboType.ctChSioDetailcmbDataBit)    ''Data bit
                Call mSetComboAdd(.udtCommInf.shtStop, cmbStopBit, gEnmComboType.ctChSioDetailcmbStopBit)       ''Stop bit
                Call mSetComboAdd(.shtDuplexSet, cmbDuplet, gEnmComboType.ctChSioDetailcmbDuplet)               ''Duplet set

                ''CommTypeコンボ設定
                Call mSetCommTypeCombo(.shtCommType1, .shtCommType2)

                '' '0'表示追加　ver1.4.0 2011.09.02
                txtExtComID.Text = Hex(.shtExtComID)            ''ExtComID
                txtInitialTimeout.Text = .shtReceiveInit        ''Receive time out(initial)
                txtUseallyTimeout.Text = .shtReceiveUseally     ''Receive time out(useally)
                txtInitialTransmit.Text = .shtSendInit          ''Transmit Waiting time(initial)
                txtUseallyTransmit.Text = .shtSendUseally       ''Transmit Waiting time(useally)
                txtNumberCH.Text = .shtSendCH                   ''The number of CH
                txtRetryCount.Text = .shtRetry                  ''RetryCount

                ''Nodeテーブル
                For i As Integer = LBound(.udtNode) To UBound(.udtNode)

                    grdNode(1, i).Value = IIf(.udtNode(i).shtCheck = 0, False, True)
                    grdNode(2, i).Value = Hex(.udtNode(i).shtAddress)

                Next


                'Ver2.0.5.8 SIO拡張対応
                If .shtKakuTbl = 1 Then
                    chkSioExt.Checked = True
                Else
                    chkSioExt.Checked = False
                End If
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplaySioCh(ByVal udtSioCh As gTypSetChSioCh)

        Dim intCommChType As Integer

        For i As Integer = 0 To UBound(udtSioCh.udtSioChRec)

            With udtSioCh.udtSioChRec(i)

                ''通信チャンネルタイプを取得
                If .shtChId = 0 And .shtChNo = 0 Then
                    ''全て 0 ならタイプ未選択
                    intCommChType = gCstCodeChSioCommChType1Nothing

                ElseIf .shtChId = &HFFFE And .shtChNo = 0 Then      '' 2014.01.14
                    ''NEXT 区切り文字
                    intCommChType = gCstCodeChSioCommChType1NEXT

                ElseIf .shtChId <> 0 Or .shtChNo <> 0 Then

                    ''チャンネルIDかNoが設定されていてデータ長が 0 ならCHデータ
                    intCommChType = gCstCodeChSioCommChType1ChData

                Else
                    ''上記以外ならタイプ未選択
                    intCommChType = gCstCodeChSioCommChType1Nothing
                End If


                ' ''If .shtChId = 0 And .shtChNo = 0 And .shtSize = 0 Then

                ' ''    ''全て 0 ならタイプ未選択
                ' ''    intCommChType = gCstCodeChSioCommChType1Nothing

                ' ''ElseIf (.shtChId <> 0 Or .shtChNo <> 0) And .shtSize = 0 Then

                ' ''    ''チャンネルIDかNoが設定されていてデータ長が 0 ならCHデータ
                ' ''    intCommChType = gCstCodeChSioCommChType1ChData

                ' ''ElseIf (.shtChId = 0 And .shtChNo = 0) And .shtSize <> 0 Then

                ' ''    ''チャンネルIDかNoが設定されていなくてデータ長が 0 以外ならデータ長
                ' ''    intCommChType = gCstCodeChSioCommChType1DataLength

                ' ''Else

                ' ''    ''上記以外ならタイプ未選択
                ' ''    intCommChType = gCstCodeChSioCommChType1Nothing

                ' ''End If

                Select Case intCommChType
                    Case gCstCodeChSioCommChType1Nothing
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1Nothing)
                        grdCH(1, i).Value = ""
                        grdCH(2, i).Value = ""
                    Case gCstCodeChSioCommChType1ChData
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1ChData)
                        grdCH(1, i).Value = .shtChNo.ToString("0000")
                        Call mDispTransmisionChName(i)
                        ' ''Case gCstCodeChSioCommChType1DataLength
                        ' ''    grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1DataLength)
                        ' ''    grdCH(1, i).Value = .shtSize
                        ' ''    grdCH(2, i).Value = ""
                    Case gCstCodeChSioCommChType1NEXT   '区切り文字　2014.01.14
                        grdCH(0, i).Value = CStr(gCstCodeChSioCommChType1NEXT)
                        grdCH(1, i).Value = "@NEXT"
                End Select

            End With

        Next

    End Sub

#End Region

#Region "バイト配列変換"

    '--------------------------------------------------------------------
    ' 機能      : 構造体からバイト配列に変換
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) VDR情報詳細構造体
    ' 　　　    : ARG2 - ( O) バイト配列
    ' 機能説明  : 構造体の設定値をバイト配列に変換する
    '--------------------------------------------------------------------
    Private Sub mConvTypeToByteArray(ByVal udtVdr As gTypSetChSioVdr, ByRef bytArray() As Byte)

        Try

            Const cstByteCnt As Integer = 76

            ''配列確保
            ReDim bytArray(cstByteCnt - 1)

            With udtVdr

                Call gCopyByteArray(BitConverter.GetBytes(.shtPort), bytArray, 0)                   ''00～01（ポート番号）
                Call gCopyByteArray(BitConverter.GetBytes(.shtExtComID), bytArray, 2)               ''02～03（外部機器識別子）
                Call gCopyByteArray(BitConverter.GetBytes(.shtPriority), bytArray, 4)               ''04～05（優先度）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSysno), bytArray, 6)                  ''06～07（M/C設定）
                Call gCopyByteArray(BitConverter.GetBytes(.shtCommType1), bytArray, 8)              ''08～09（通信種類１）
                Call gCopyByteArray(BitConverter.GetBytes(.shtCommType2), bytArray, 10)             ''10～11（通信種類２）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtComm), bytArray, 12)       ''12～13（回線情報：回線種類）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtDataBit), bytArray, 14)    ''14～15（回線情報：データビット）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtParity), bytArray, 16)     ''16～17（回線情報：パリティ）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtStop), bytArray, 18)       ''18～19（回線情報：ストップビット）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtComBps), bytArray, 20)     ''20～21（回線情報：通信速度）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare1), bytArray, 22)     ''22～23（回線情報：予備1）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare2), bytArray, 24)     ''24～25（回線情報：予備2）
                Call gCopyByteArray(BitConverter.GetBytes(.udtCommInf.shtSpare3), bytArray, 26)     ''26～27（回線情報：予備3）
                Call gCopyByteArray(BitConverter.GetBytes(.shtReceiveInit), bytArray, 28)           ''28～29（受信タイムアウト（秒）起動時
                Call gCopyByteArray(BitConverter.GetBytes(.shtReceiveUseally), bytArray, 30)        ''30～31（受信タイムアウト（秒）起動後
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendInit), bytArray, 32)              ''32～33（送信間隔（秒）起動時）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendUseally), bytArray, 34)           ''34～35（送信間隔（秒）起動後）
                Call gCopyByteArray(BitConverter.GetBytes(.shtRetry), bytArray, 36)                 ''36～37（リトライ回数）
                Call gCopyByteArray(BitConverter.GetBytes(.shtDuplexSet), bytArray, 38)             ''38～39（Duplex 設定）
                Call gCopyByteArray(BitConverter.GetBytes(.shtSendCH), bytArray, 40)                ''40～41（送信CH）
                Call gCopyByteArray(BitConverter.GetBytes(.shtKakuTbl), bytArray, 42)               ''42～43（拡張ﾃｰﾌﾞﾙ有無）Ver2.0.5.8
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(0).shtCheck), bytArray, 44)      ''44～45（ノード情報１使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(0).shtAddress), bytArray, 46)    ''46～47（ノード情報１アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(1).shtCheck), bytArray, 48)      ''48～49（ノード情報２使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(1).shtAddress), bytArray, 50)    ''50～51（ノード情報２アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(2).shtCheck), bytArray, 52)      ''52～53（ノード情報３使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(2).shtAddress), bytArray, 54)    ''54～55（ノード情報３アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(3).shtCheck), bytArray, 56)      ''56～57（ノード情報４使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(3).shtAddress), bytArray, 58)    ''58～59（ノード情報４アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(4).shtCheck), bytArray, 60)      ''60～61（ノード情報５使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(4).shtAddress), bytArray, 62)    ''62～63（ノード情報５アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(5).shtCheck), bytArray, 64)      ''64～65（ノード情報６使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(5).shtAddress), bytArray, 66)    ''66～67（ノード情報６アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(6).shtCheck), bytArray, 68)      ''68～69（ノード情報７使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(6).shtAddress), bytArray, 70)    ''70～71（ノード情報７アドレス）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(7).shtCheck), bytArray, 72)      ''72～73（ノード情報８使用有無）
                Call gCopyByteArray(BitConverter.GetBytes(.udtNode(7).shtAddress), bytArray, 74)    ''74～75（ノード情報８アドレス）

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バイト配列から構造体に変換
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) バイト配列
    ' 　　　    : ARG2 - ( O) VDR情報詳細構造体
    ' 機能説明  : バイト配列を構造体の設定値に変換する
    '--------------------------------------------------------------------
    Private Sub mConvByteArrayToType(ByVal bytArray() As Byte, ByRef udtVdr As gTypSetChSioVdr)

        Try

            With udtVdr

                .shtPort = BitConverter.ToUInt16(bytArray, 0)                                           ''00～01（ポート番号）
                .shtExtComID = BitConverter.ToUInt16(bytArray, 2)                                       ''02～03（外部機器識別子）
                .shtPriority = BitConverter.ToUInt16(bytArray, 4)                                       ''04～05（優先度）
                .shtSysno = BitConverter.ToUInt16(bytArray, 6)                                          ''06～07（M/C設定）
                .shtCommType1 = BitConverter.ToUInt16(bytArray, 8)                                      ''08～09（通信種類１）
                .shtCommType2 = BitConverter.ToUInt16(bytArray, 10)                                     ''10～11（通信種類２）
                .udtCommInf.shtComm = BitConverter.ToUInt16(bytArray, 12)                               ''12～13（回線情報：回線種類）
                .udtCommInf.shtDataBit = BitConverter.ToUInt16(bytArray, 14)                            ''14～15（回線情報：データビット）
                .udtCommInf.shtParity = BitConverter.ToUInt16(bytArray, 16)                             ''16～17（回線情報：パリティ）
                .udtCommInf.shtStop = BitConverter.ToUInt16(bytArray, 18)                               ''18～19（回線情報：ストップビット）
                .udtCommInf.shtComBps = BitConverter.ToUInt16(bytArray, 20)                             ''20～21（回線情報：通信速度）
                .udtCommInf.shtSpare1 = BitConverter.ToUInt16(bytArray, 22)                              ''22～23（回線情報：予備1）
                .udtCommInf.shtSpare2 = BitConverter.ToUInt16(bytArray, 24)                              ''24～25（回線情報：予備2）
                .udtCommInf.shtSpare3 = BitConverter.ToUInt16(bytArray, 26)                              ''26～27（回線情報：予備3）
                .shtReceiveInit = BitConverter.ToUInt16(bytArray, 28)                                   ''28～29（受信タイムアウト（秒）起動時）
                .shtReceiveUseally = BitConverter.ToUInt16(bytArray, 30)                                ''30～31（受信タイムアウト（秒）起動後）
                .shtSendInit = BitConverter.ToUInt16(bytArray, 32)                                      ''32～33（送信間隔（秒）起動時）
                .shtSendUseally = BitConverter.ToUInt16(bytArray, 34)                                   ''34～35（送信間隔（秒）起動後）
                .shtRetry = BitConverter.ToUInt16(bytArray, 36)                                         ''36～37（リトライ回数）
                .shtDuplexSet = BitConverter.ToUInt16(bytArray, 38)                                     ''38～39（Duplex 設定）
                .shtSendCH = BitConverter.ToUInt16(bytArray, 40)                                        ''40～41（送信CH）
                .shtKakuTbl = BitConverter.ToUInt16(bytArray, 42)                                       ''42～43（拡張ﾃｰﾌﾞﾙ有無）Ver2.0.5.8
                .udtNode(0).shtCheck = BitConverter.ToUInt16(bytArray, 44)                              ''44～45（ノード情報１使用有無）
                .udtNode(0).shtAddress = BitConverter.ToUInt16(bytArray, 46)                            ''46～47（ノード情報１アドレス）
                .udtNode(1).shtCheck = BitConverter.ToUInt16(bytArray, 48)                              ''48～49（ノード情報２使用有無）
                .udtNode(1).shtAddress = BitConverter.ToUInt16(bytArray, 50)                            ''50～51（ノード情報２アドレス）
                .udtNode(2).shtCheck = BitConverter.ToUInt16(bytArray, 52)                              ''52～53（ノード情報３使用有無）
                .udtNode(2).shtAddress = BitConverter.ToUInt16(bytArray, 54)                            ''54～55（ノード情報３アドレス）
                .udtNode(3).shtCheck = BitConverter.ToUInt16(bytArray, 56)                              ''56～57（ノード情報４使用有無）
                .udtNode(3).shtAddress = BitConverter.ToUInt16(bytArray, 58)                            ''58～59（ノード情報４アドレス）
                .udtNode(4).shtCheck = BitConverter.ToUInt16(bytArray, 60)                              ''60～61（ノード情報５使用有無）
                .udtNode(4).shtAddress = BitConverter.ToUInt16(bytArray, 62)                            ''62～63（ノード情報５アドレス）
                .udtNode(5).shtCheck = BitConverter.ToUInt16(bytArray, 64)                              ''64～65（ノード情報６使用有無）
                .udtNode(5).shtAddress = BitConverter.ToUInt16(bytArray, 66)                            ''66～67（ノード情報６アドレス）
                .udtNode(6).shtCheck = BitConverter.ToUInt16(bytArray, 68)                              ''68～69（ノード情報７使用有無）
                .udtNode(6).shtAddress = BitConverter.ToUInt16(bytArray, 70)                            ''70～71（ノード情報７アドレス）
                .udtNode(7).shtCheck = BitConverter.ToUInt16(bytArray, 72)                              ''72～73（ノード情報８使用有無）
                .udtNode(7).shtAddress = BitConverter.ToUInt16(bytArray, 74)                            ''74～75（ノード情報８アドレス）

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "アドレス変換"

    '--------------------------------------------------------------------
    ' 機能      : アドレス数値変換
    ' 返り値    : 変換後値
    ' 引き数    : ARG1 - ( O) 変換前文字列
    ' 機能説明  : アドレスを数値に変換する
    '--------------------------------------------------------------------
    Private Function mConvNodeAddress(ByVal intInput As Integer) As String

        Dim strRtn As Integer

        Try

            Select Case intInput
                Case 49 : strRtn = "RA"
                Case 50 : strRtn = "RB"
                Case 51 : strRtn = "RC"
                Case 52 : strRtn = "RD"
                Case 53 : strRtn = "RE"
                Case 54 : strRtn = "RF"
                Case 55 : strRtn = "RG"
                Case 56 : strRtn = "RH"
                Case 57 : strRtn = "RI"
                Case 58 : strRtn = "RJ"
                Case 59 : strRtn = "RK"
                Case 60 : strRtn = "RL"
                Case 61 : strRtn = "RM"
                Case 62 : strRtn = "RN"
                Case 63 : strRtn = "RO"
                Case 65 : strRtn = "PA"
                Case 66 : strRtn = "PB"
                Case 67 : strRtn = "PC"
                Case 68 : strRtn = "PD"
                Case 69 : strRtn = "PE"
                Case 70 : strRtn = "PF"
                Case 71 : strRtn = "PG"
                Case 72 : strRtn = "PH"
                Case 73 : strRtn = "PI"
                Case 74 : strRtn = "PJ"
                Case 75 : strRtn = "PK"
                Case 76 : strRtn = "PL"
                Case 77 : strRtn = "PM"
                Case 78 : strRtn = "PN"
                Case 79 : strRtn = "PO"
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : アドレス数値変換
    ' 返り値    : 変換後値
    ' 引き数    : ARG1 - ( O) 変換前文字列
    ' 機能説明  : アドレスを数値に変換する
    '--------------------------------------------------------------------
    Private Function mConvNodeAddress(ByVal strInput As String) As Integer

        Try

            Dim intRtn As Integer

            Select Case strInput
                Case "RA", "ra" : intRtn = "49"
                Case "RB", "rb" : intRtn = "50"
                Case "RC", "rc" : intRtn = "51"
                Case "RD", "rd" : intRtn = "52"
                Case "RE", "re" : intRtn = "53"
                Case "RF", "rf" : intRtn = "54"
                Case "RG", "rg" : intRtn = "55"
                Case "RH", "rh" : intRtn = "56"
                Case "RI", "ri" : intRtn = "57"
                Case "RJ", "rj" : intRtn = "58"
                Case "RK", "rk" : intRtn = "59"
                Case "RL", "rl" : intRtn = "60"
                Case "RM", "rm" : intRtn = "61"
                Case "RN", "rn" : intRtn = "62"
                Case "RO", "ro" : intRtn = "63"
                Case "PA", "pa" : intRtn = "65"
                Case "PB", "pb" : intRtn = "66"
                Case "PC", "pc" : intRtn = "67"
                Case "PD", "pd" : intRtn = "68"
                Case "PE", "pe" : intRtn = "69"
                Case "PF", "pf" : intRtn = "70"
                Case "PG", "pg" : intRtn = "71"
                Case "PH", "ph" : intRtn = "72"
                Case "PI", "pi" : intRtn = "73"
                Case "PJ", "pj" : intRtn = "74"
                Case "PK", "pk" : intRtn = "75"
                Case "PL", "pl" : intRtn = "76"
                Case "PM", "pm" : intRtn = "77"
                Case "PN", "pn" : intRtn = "78"
                Case "PO", "po" : intRtn = "79"
            End Select

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region


End Class
